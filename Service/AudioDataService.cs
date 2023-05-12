
using MauiApp3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MauiApp3.Service
{
    internal class AudioDataService : IAudioDataService
    {
        private HttpClient _httpClient;
        private string _baseAddress;
        private string _url;
        private readonly JsonSerializerOptions _jsonSerlizationOptions;
        public AudioDataService()
        {
            _httpClient = new HttpClient();
            _baseAddress = "https://localhost:7079/api/Audio";
            _url = _baseAddress;
            _jsonSerlizationOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        public async Task<List<AudioFile>> Get()
        {
            List<AudioFile> list = new();
            try
            {
                HttpResponseMessage response = _httpClient.GetAsync(_url).Result;
                if (response.IsSuccessStatusCode)
                {
                    list = JsonSerializer.Deserialize<List<AudioFile>>(await response.Content.ReadAsStringAsync(), _jsonSerlizationOptions);
                }
            }
            catch (Exception e)
            {
                //
            }
            return list;
        }
       
        public async Task<AudioFile> Get(int id)
        {
            AudioFile user = new();
            try
            {
                HttpResponseMessage response = _httpClient.GetAsync(_url).Result;
                if (response.IsSuccessStatusCode)
                {
                    user = JsonSerializer.Deserialize<AudioFile>(await response.Content.ReadAsStringAsync(), _jsonSerlizationOptions);
                }
            }
            catch (Exception e)
            {
                //
            }
            return user;
        }
        public async Task<bool> Get(AudioFile values)
        {
            bool result = false;
            _url = $"{_url}/{values.AudioName}/{values.AudioDescription}";
            try
            {
                HttpResponseMessage response = _httpClient.GetAsync(_url).Result;
                if (response.IsSuccessStatusCode)
                {
                    result = true;
                }
            }
            catch (Exception e)
            {
                //
            }
            return result;
        }
        public async Task<bool> Post(AudioFile value)
        {
            bool process = false;
            try
            {
                HttpResponseMessage response = _httpClient.PostAsJsonAsync(_url, value).Result;
                if (response.IsSuccessStatusCode)
                {
                    process = true;
                }
            }
            catch (Exception e)
            {
                //
            }
            return process;
        }

        public async Task<bool> Put(int id, AudioFile value)
        {
            bool process = false;
            try
            {
                HttpResponseMessage response = _httpClient.PutAsJsonAsync(_url, value).Result;
                if (response.IsSuccessStatusCode)
                {
                    process = true;
                }
            }
            catch (Exception e)
            {
                //
            }
            return process;
        }

        public async Task<bool> Delete(int id)
        {
            bool process = false;
            try
            {
                HttpResponseMessage response = _httpClient.DeleteAsync($"{_url}/{id}").Result;
                if (response.IsSuccessStatusCode)
                {
                    process = true;
                }
            }
            catch (Exception e)
            {
                //
            }
            return process;
        }
    }
}
