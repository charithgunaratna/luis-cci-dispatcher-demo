using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AspNetCore_Luis_Dispatch_Bot
{
    public class QueryIntentScore
    {
        public string IntentId { get; set; }
        public double RankScore { get; set; }
    }

    public class CCIIntentScorerClient
    {
        private HttpClient _client;
        private string _baseUri;

        public CCIIntentScorerClient(string baseUri)
        {
            _baseUri = baseUri;
            _client = new HttpClient();
        }

        public async Task<QueryIntentScore> GetIntentScoreAsync(string query)
        {
            var response = await _client.GetAsync(_baseUri + $"/api/v1/intentscore/getintentscoreasync?query={query}");

            if (response.IsSuccessStatusCode)
            {
                var score = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<QueryIntentScore>(score);
            }

            return null;
        }
    }
}
