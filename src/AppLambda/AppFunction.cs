using System;
using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using System.Collections.Generic;
using Newtonsoft.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace AppLambda
{
    public class AppFunction
    {
        
        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public APIGatewayProxyResponse AppFunctionHandler(APIGatewayProxyRequest request, ILambdaContext context)
        {
            string apps = "[{\"name\":\"John\",\"questions\":[{\"Id\":1,\"QuestionText\":\"Have you ever been convicted of a felony?\",\"AnswerText\":\"no\"},{\"Id\":2,\"QuestionText\":\"Do you currently own your own truck?\",\"AnswerText\":\"yes\"},{\"Id\":3,\"QuestionText\":\"Would you pass a background check?\",\"AnswerText\":\"yes\"},{\"Id\":4,\"QuestionText\":\"Are you authorized to work in the United States?\",\"AnswerText\":\"yes\"}]},{\"name\":\"Bob\",\"questions\":[{\"Id\":1,\"QuestionText\":\"Have you ever been convicted of a felony?\",\"AnswerText\":\"no\"},{\"Id\":2,\"QuestionText\":\"Do you currently own your own truck?\",\"AnswerText\":\"yes\"},{\"Id\":3,\"QuestionText\":\"Would you pass a background check?\",\"AnswerText\":\"yes\"},{\"Id\":4,\"QuestionText\":\"Are you authorized to work in the United States?\",\"AnswerText\":\"no\"}]},{\"name\":\"Mary\",\"questions\":[{\"Id\":1,\"QuestionText\":\"Have you ever been convicted of a felony?\",\"AnswerText\":\"no\"},{\"Id\":2,\"QuestionText\":\"Do you currently own your own truck?\",\"AnswerText\":\"no\"},{\"Id\":3,\"QuestionText\":\"Would you pass a background check?\",\"AnswerText\":\"no\"},{\"Id\":4,\"QuestionText\":\"Are you authorized to work in the United States?\",\"AnswerText\":\"no\"}]},{\"name\":\"Alex\",\"questions\":[{\"Id\":1,\"QuestionText\":\"Have you ever been convicted of a felony?\",\"AnswerText\":\"no\"},{\"Id\":2,\"AnswerText\":\"yes\"},{\"Id\":3,\"QuestionText\":\"Would you pass a background check?\",\"AnswerText\":\"yes\"},{\"Id\":4,\"QuestionText\":\"Are you authorized to work in the United States?\",\"AnswerText\":\"yes\"}]}]";
            string questions = "[{\"Id\":1,\"QuestionText\":\"Have you ever been convicted of a felony?\",\"AnswerText\":\"no\"},{\"Id\":2,\"QuestionText\":\"Do you currently own your own truck?\",\"AnswerText\":\"yes\"},{\"Id\":3,\"QuestionText\":\"Would you pass a background check?\",\"AnswerText\":\"yes\"},{\"Id\":4,\"QuestionText\":\"Are you authorized to work in the United States?\",\"AnswerText\":\"yes\"}]";

            var applications = new List<Application>{};
            var answerKey = new List<Question>{};
            var passedApps = new List<Application>{};
            applications = JsonConvert.DeserializeObject<List<Application>>(apps);
            answerKey = JsonConvert.DeserializeObject<List<Question>>(questions);

            foreach (Application app in applications){
                Boolean pass = true;
                foreach (Question question in app.Questions){
                   foreach (Question answer in answerKey){
                       if (question.Id == answer.Id && question.AnswerText.ToLower() != answer.AnswerText.ToLower()){
                           pass = false;
                       }
                   }
               }
               if (pass == true){
                   passedApps.Add(app);
               }
            }
                       
            return new APIGatewayProxyResponse {
                StatusCode = 200,
                Body = JsonConvert.SerializeObject(passedApps)
            };
        }
    }
}
