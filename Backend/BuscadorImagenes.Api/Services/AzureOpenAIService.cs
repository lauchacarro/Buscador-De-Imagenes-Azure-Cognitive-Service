using Azure;
using Azure.AI.OpenAI;

using BuscadorImagenes.Api.Models;

using System.Text.Json;
using System.Text.RegularExpressions;

namespace BuscadorImagenes.Api.Services
{
    public class AzureOpenAIService
    {
        private readonly OpenAIClient _client;

        public AzureOpenAIService()
        {
            _client = CreateOpenAIClient();
        }

        private static OpenAIClient CreateOpenAIClient()
        {
            string endpoint = "https://buscador-imagenes-openai.openai.azure.com/";

            string apikey = null;

            OpenAIClient openAIClient = new OpenAIClient(new(endpoint), new AzureKeyCredential(apikey));
            return openAIClient;
        }


        public async Task<ImagenMultidioma> GetImagenMultidiomaAsync(Imagen imagen)
        {
            string json = JsonSerializer.Serialize(imagen);

            var response = await GetCompletionsAsync(Prompts.MultidiomaPrompt.Replace("{0}", json));

            string jsonResponse = GetJSON(response);

            ImagenMultidioma imagenMultidioma = JsonSerializer.Deserialize<ImagenMultidioma>(jsonResponse);

            return imagenMultidioma;

        }


        string GetJSON(string message)
        {
            Regex regex = new Regex(@"\{(?:[^{}]|(?<open>\{)|(?<-open>\}))+(?(open)(?!))\}");

            // Encuentra todas las coincidencias de JSON en el texto
            MatchCollection matches = regex.Matches(message);

            var jsonResponse = matches.Last().Value;

            return jsonResponse;
        }


        public async Task<string> GetCompletionsAsync(string prompt)
        {
            Response<ChatCompletions> responseWithoutStream = await _client.GetChatCompletionsAsync(
    "GPTModel",
    new ChatCompletionsOptions()
                {
                    Messages =
                                        {
                                            new ChatMessage(ChatRole.User, prompt),
                                        },
                    Temperature = (float)0.7,
                    MaxTokens = 2041,
                    NucleusSamplingFactor = (float)0.95,
                    FrequencyPenalty = 0,
                    PresencePenalty = 0,
                });

            ChatCompletions completions = responseWithoutStream.Value;

            return completions.Choices.Last().Message.Content;
        }
    }
}
