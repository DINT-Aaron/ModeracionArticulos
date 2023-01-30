using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ModeracionArticulos
{


    public class MainWindowVM
    {
        private readonly string _apiKey = "4001e1e5b6094f71a6af15cde53d3a68";
        private readonly string _endpoint = "westeurope.api.cognitive.microsoft.com";

        /*public ContentModerator(string apiKey, string endpoint)
        {
            _apiKey = apiKey;
            _endpoint = endpoint;
        }*/

        public async void ModerateText(string text)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _apiKey);
                //var uri = $"{_endpoint}/text/moderation/v1.0/Screen?autocorrect=false&PII=false&listId=<listId>&language=eng";
                var uri = $"{_endpoint}/text/moderation/v1.0/Screen";

                var content = new StringContent(JsonConvert.SerializeObject(new { text }), Encoding.UTF8, "application/json");
                var response = await client.PostAsync(uri, content);

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Moderation failed with status code: " + response.StatusCode);
                    return;
                }

                var moderationResult = JsonConvert.DeserializeObject<ModerationResult>(await response.Content.ReadAsStringAsync());
                Console.WriteLine("Moderation status: " + moderationResult.Status);
            }
        }

        private class ModerationResult
        {
            public string Status { get; set; }
        }
    }


}

