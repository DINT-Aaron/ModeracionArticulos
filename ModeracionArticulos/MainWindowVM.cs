using Microsoft.Azure.CognitiveServices.ContentModerator;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;

namespace ModeracionArticulos
{


    public class MainWindowVM
    {
        private readonly string _apiKey = "4001e1e5b6094f71a6af15cde53d3a68";
        private readonly string _endpoint = "https://westeurope.api.cognitive.microsoft.com";

        public async void ModerateText(string text)
        {
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _apiKey);

            queryString["autocorrect"] = "true";
            queryString["PII"] = "true";
            //queryString["listId"] = "{string}";
            queryString["classify"] = "True";
            queryString["language"] = "spa";
            var uri = "https://westeurope.api.cognitive.microsoft.com/contentmoderator/moderate/v1.0/ProcessText/Screen?" + queryString;

            HttpResponseMessage response;
            // Request body
            byte[] byteData = Encoding.UTF8.GetBytes(text);

            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("text/plain");
                response = await client.PostAsync(uri, content);
            }
            Console.WriteLine(response);
            Console.WriteLine("_________________________________________");
            Console.WriteLine(response.Content);
            Console.WriteLine("_________________________________________");
            Console.WriteLine(response.Headers);
            Console.WriteLine("_________________________________________");
            Console.WriteLine(response.StatusCode);
        }

    }
}

