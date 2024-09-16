using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

using AutoShopAppLibrary.Data;

using Microsoft.Extensions.Logging;

namespace AutoShopAppLibrary.Services
{
    public class FBAPIService
    {
        private readonly List<string> _postURLs = new List<string>();
        private readonly int _MaxPosts = 0;
        private readonly HttpClient _httpClient;
        private readonly ILogger<FBAPIService> _logger;


        public async Task GetNewsPostsFacebook()
        {
            //var posts = await _httpClient.GetFromJsonAsync("");
        }


        public FBAPIService(HttpClient newclient, ILogger<FBAPIService> logger)
        {
            _httpClient = newclient;
            _logger = logger;
        }
    }
}