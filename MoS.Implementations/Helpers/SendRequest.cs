using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MoS.Implementations.Helpers
{
    public static class SendRequest
    {
        private static readonly HttpClient client = new HttpClient();

        public class Account
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

        public static async Task PostAsync<T, R>(string url, T data, Account account, Action<R> onSuccess, Action onFail)
        {
            string json = System.Text.Json.JsonSerializer.Serialize(data);

            string encoded = Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1")
                               .GetBytes(account.Username + ":" + account.Password));

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(url),
                Content = new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json /* or "application/json" in older versions */),
            };

            request.Headers.Add("Authorization", "Basic " + encoded);

            var response = await client.SendAsync(request).ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                onFail();
                return;
            }

            var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            var result = JsonConvert.DeserializeObject<R>(responseString);

            onSuccess(result);
        }

        public static async Task PutAsync<T, R>(string url, T data, Account account, Action<R> onSuccess, Action onFail)
        {
            string json = System.Text.Json.JsonSerializer.Serialize(data);

            string encoded = Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1")
                               .GetBytes(account.Username + ":" + account.Password));

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri(url),
                Content = new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json /* or "application/json" in older versions */),
            };

            request.Headers.Add("Authorization", "Basic " + encoded);

            var response = await client.SendAsync(request).ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                onFail();
                return;
            }

            var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            var result = JsonConvert.DeserializeObject<R>(responseString);

            onSuccess(result);
        }

        public static async Task GetAsync<T, R>(string url, T data, Account account, Action<R> onSuccess, Action onFail)
        {
            string json = System.Text.Json.JsonSerializer.Serialize(data);

            string encoded = Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1")
                               .GetBytes(account.Username + ":" + account.Password));

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(url),
                Content = new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json /* or "application/json" in older versions */),
            };

            request.Headers.Add("Authorization", "Basic " + encoded);

            var response = await client.SendAsync(request).ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                onFail();
                return;
            }

            var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            var result = JsonConvert.DeserializeObject<R>(responseString);

            onSuccess(result);
        }

        public static async Task DeleteAsync<T>(string url, Account account, Action<T> onSuccess, Action onFail)
        {
            string encoded = Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1")
                               .GetBytes(account.Username + ":" + account.Password));

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri(url)
            };

            request.Headers.Add("Authorization", "Basic " + encoded);

            var response = await client.SendAsync(request).ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                onFail();
                return;
            }

            var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            var result = JsonConvert.DeserializeObject<T>(responseString);

            onSuccess(result);
        }
    }
}
