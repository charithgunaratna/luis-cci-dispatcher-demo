// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using System.Threading.Tasks;

namespace AspNetCore_Luis_Dispatch_Bot
{
    public class CCIRecognizerMiddleware : IMiddleware
    {
        public const string CCIRecognizerResultKey = "CCIRecognizerResult";
        private CCIIntentScorerClient _client;

        public CCIRecognizerMiddleware(string baseUri)
        {
            _client = new CCIIntentScorerClient(baseUri);
        }

        public async Task OnTurn(ITurnContext context, MiddlewareSet.NextDelegate next)
        {
            if (context.Activity.Type is ActivityTypes.Message)
            {
                var score = await _client.GetIntentScoreAsync(context.Activity.Text.Trim());
                context.Services.Add(CCIRecognizerResultKey, score);
            }

            await next();
        }
    }
}
