using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using EcareMob.Clients.Base;
using EcareMob.Helpers;
using EcareMob.Models;
using EcareMob.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Thesis.Invetory.Shared.Services;

namespace EcareMob.Clients.Base
{ 
    public class RequestProvider:IRequestProvider
    {
        private IAuthenticationService _authenticator;

        public RequestProvider(IAuthenticationService authenticator)
        {
            _authenticator = authenticator;
        }


        public class JsonContent : StringContent
        {
            public JsonContent(object obj) :
                base(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json")
            { }
        }

        public async Task<T> PostAuthRequest<T, TK>(string apiUrl, TK postObject)
        {
            T result = default(T);

            using (var client = new HttpClient())
            {
                try
                {
                    var json = JsonConvert.SerializeObject(postObject);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = null;
                    response = await client.PostAsync(apiUrl, content);


                    //var postcontent = JsonConvert.SerializeObject(postObject,
                    //    Newtonsoft.Json.Formatting.None,
                    //    new JsonSerializerSettings
                    //    {
                    //        NullValueHandling = NullValueHandling.Ignore
                    //    });
                    //var response = await client.PostAsync(apiUrl, new StringContent(postcontent, Encoding.UTF8, "application/json")).ConfigureAwait(false);

                    await HandleResponse(response);
                    string responseData = await response.Content.ReadAsStringAsync();

                    result = await Task.Run(() => JsonConvert.DeserializeObject<T>(responseData));

                    return result;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    throw;
                }

            }

        }









        public async Task<T> PostRequest<T, TK>(string apiUrl, TK postObject)
        {
            T result = default(T);

            using (var client = new HttpClient())
            {
                await _authenticator.RefreshTokenAsync();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",Settings.AccessToken);
                var postcontent = JsonConvert.SerializeObject(postObject);
                var response = await client.PostAsync(apiUrl, new StringContent(postcontent, Encoding.UTF8, "text/json")).ConfigureAwait(false);

                await HandleResponse(response);
                string responseData = await response.Content.ReadAsStringAsync();
                
                result = await Task.Run(() => JsonConvert.DeserializeObject<T>(responseData));
            }

            return result;
        }

        public async Task<List<T>> PostItemWithMultipleResultRequest<T, TK>(string apiUrl, TK postObject)
        {
            List<T> result = null;

            using (var client = new HttpClient())
            {
                await _authenticator.RefreshTokenAsync();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Settings.AccessToken);
                var postcontent = JsonConvert.SerializeObject(postObject);
                var response = await client.PostAsync(apiUrl, new StringContent(postcontent, Encoding.UTF8, "text/json")).ConfigureAwait(false);
                await HandleResponse(response);
                string responseData = await response.Content.ReadAsStringAsync();
                result = await Task.Run(() => JsonConvert.DeserializeObject<List<T>>(responseData));
            }

            return result;
        }

        public async Task<List<T>> PostItemsWithMultipleResultRequest<T, TK>(string apiUrl, List<TK> postObject)
        {
            List<T> result = null;

            using (var client = new HttpClient())
            {
                await _authenticator.RefreshTokenAsync();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Settings.AccessToken);
                var postcontent = JsonConvert.SerializeObject(postObject);
                var response = await client.PostAsync(apiUrl, new StringContent(postcontent, Encoding.UTF8, "text/json")).ConfigureAwait(false);
                await HandleResponse(response);
                string responseData = await response.Content.ReadAsStringAsync();
                result = await Task.Run(() => JsonConvert.DeserializeObject<List<T>>(responseData));
            }

            return result;
        }

        public async Task<TK> PutRequest<T,TK>(string apiUrl, T postObject)
        {
            TK result = default(TK);

            using (var client = new HttpClient())
            {
                await _authenticator.RefreshTokenAsync();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Settings.AccessToken);
                var postcontent = JsonConvert.SerializeObject(postObject);
                var response = await client.PutAsync(apiUrl, new StringContent(postcontent, Encoding.UTF8, "text/json")).ConfigureAwait(false);
                await HandleResponse(response);
                string responseData = await response.Content.ReadAsStringAsync();
                result = await Task.Run(() => JsonConvert.DeserializeObject<TK>(responseData));
            }

            return result;
        }

        public async Task PutStreamRequest(string apiUrl, Stream postObject)
        {
            using (var client = new HttpClient())
            {
                await _authenticator.RefreshTokenAsync();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Settings.AccessToken);
                HttpContent fileStreamContent = new StreamContent(postObject);
                using (var formData = new MultipartFormDataContent())
                {
                    formData.Add(fileStreamContent, "file","profile.jpg");
                    var response = await client.PostAsync(apiUrl, formData);
                    await HandleResponse(response);
                }
            }
            
        }

        public async Task PutMultipleStreamRequest(string apiUrl, Stream postObject)
        {
            using (var client = new HttpClient())
            {
                await _authenticator.RefreshTokenAsync();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Settings.AccessToken);
                HttpContent fileStreamContent = new StreamContent(postObject);
                fileStreamContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                {
                    Name = "files",
                    FileName = "test.jpg"
                }; // the extra quotes are key here
                using (var formData = new MultipartFormDataContent())
                {
                    formData.Add(fileStreamContent);
                    var response = await client.PostAsync(apiUrl, formData);
                    await HandleResponse(response);
                }
            }

        }

        public async Task<byte[]> GetByteArray(string apiUrl)
        {
            using (var client = new HttpClient())
            {
                await _authenticator.RefreshTokenAsync();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Settings.AccessToken);

                var response = await client.GetAsync(apiUrl).ConfigureAwait(false);
                await HandleResponse(response);
                byte[] responseData = await response.Content.ReadAsByteArrayAsync();
                return responseData;
            }
        }

        public async Task<T> GetSingleItemRequest<T>(string apiUrl)
        {
            T result = default(T);

            using (var client = new HttpClient())
            {
                await _authenticator.RefreshTokenAsync();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Settings.AccessToken);

                var response = await client.GetAsync(apiUrl).ConfigureAwait(false);
                await HandleResponse(response);
                string responseData = await response.Content.ReadAsStringAsync();
                result = await Task.Run(() => JsonConvert.DeserializeObject<T>(responseData));
            }

            return result;
        }

        public async Task<List<T>> GetMultipleItemsRequest<T>(string apiUrl)
        {
            List<T> result = null;

            using (var client = new HttpClient())
            {
                await _authenticator.RefreshTokenAsync();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Settings.AccessToken);

                var response = await client.GetAsync(apiUrl).ConfigureAwait(false);
                await HandleResponse(response);
                string responseData = await response.Content.ReadAsStringAsync();
                result = await Task.Run(() => JsonConvert.DeserializeObject<List<T>>(responseData));
            }

            return result;
        }

        public async Task DeleteRequest(string apiUrl)
        {
            using (var client = new HttpClient())
            {
                await _authenticator.RefreshTokenAsync();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Settings.AccessToken);

                var response = await client.DeleteAsync(apiUrl).ConfigureAwait(false);
                await HandleResponse(response);
            }
        }

        public async Task<List<JObject>> GetRawJsonObjects(string apiUrl)
        {
            List<JObject> items = null;

            using (var client = new HttpClient())
            {
                await _authenticator.RefreshTokenAsync();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Settings.AccessToken);

                var response = await client.GetAsync(apiUrl).ConfigureAwait(false);
                await HandleResponse(response);
                var stream = await response.Content.ReadAsStreamAsync();

                using (var streamReader = new StreamReader(stream))
                using (var jsonReader = new JsonTextReader(streamReader))
                {
                    var serializer = new JsonSerializer();
                    items = await Task.Run(()=>serializer.Deserialize<List<JObject>>(jsonReader));
                }
            }

            return items;
        }

        private async Task HandleResponse(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == HttpStatusCode.Forbidden || response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new ServiceAuthenticationException("Not Authorized");
                }
                else if (response.StatusCode == HttpStatusCode.InternalServerError)
                {
                    var exception = await Task.Run(() => JsonConvert.DeserializeObject<ApiError>(content));
                    throw new ServiceManagedErrorException(exception.Message);
                }

                throw new HttpRequestException(content);
            }
        }
    }
}
