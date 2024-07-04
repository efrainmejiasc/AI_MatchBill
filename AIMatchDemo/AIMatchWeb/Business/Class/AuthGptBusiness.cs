using AIMatchWeb.Business.Interfaces;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.Net.Http.Headers;
using AIMatchWeb.Models;
using System.Collections.Generic;

namespace AIMatchWeb.Business.Class
{
    public class AuthGptBusiness: IAuthGptBusiness
    {

        public async Task<TOutput> PostHttpRequestAuthApiGpt<TInput, TOutput>(TInput model)
        {
            var requestModel = new AuthGptRequestDto
            {
                Model = "gpt-3.5-turbo",
                Messages = new List<Message>
                {
                    new Message { Role = "system", Content = "You are a helpful assistant." },
                    new Message { Role = "user", Content = "Hola" }
                }
            };

            var url = "";
            var apiKey = "";
            var responseModel = default(TOutput);
            var respuesta = string.Empty;
            var content = JsonConvert.SerializeObject(requestModel);

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //client.DefaultRequestHeaders.TryAddWithoutValidation("OpenAI-Project", "aimatchdemo");

                // Crear contenido de la solicitud
                var httpContent = new StringContent(content, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(url, httpContent);
                if (response.IsSuccessStatusCode)
                {
                    respuesta = await response.Content.ReadAsStringAsync();
                    responseModel = JsonConvert.DeserializeObject<TOutput>(respuesta);
                }
                else
                {
                    respuesta = await response.Content.ReadAsStringAsync();
                    responseModel = JsonConvert.DeserializeObject<TOutput>(respuesta);
                }
            }

            return responseModel;
        }
    }

}

