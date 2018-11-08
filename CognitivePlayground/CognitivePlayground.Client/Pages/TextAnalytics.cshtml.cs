using CognitivePlayground.Shared;
using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics.Models;
using System.Collections.Generic;
using System.Net.Http;
namespace CognitivePlayground.Client.Pages
{
    public class TextAnalyticsModel : BlazorComponent
    {
        protected string _language { get; set; } = "en";
        protected string _text { get; set; } = "";

        [Inject]
        protected HttpClient Http { get; set; }

        public EntityLinkingResponse _resultEntities { get; set; } = new EntityLinkingResponse();

        protected List<string> _languages { get; set; } = new List<string>
        {
        "en",
        "es",
        "it"
        };

        protected async void GetEntityLinkingResult()
        {
            _resultEntities = await Http.PostJsonAsync<EntityLinkingResponse>("api/SampleData/GetEntityLinkTextAnalytics", new EntityLinkingRequest() { Language = _language, Text = _text });
            this.StateHasChanged();
        }
    }
}