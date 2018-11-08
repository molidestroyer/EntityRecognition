using CognitivePlayground.Shared;
using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics.Models;
using Microsoft.JSInterop;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CognitivePlayground.Client.Pages
{
    public class TextAnalyticsModel : BlazorComponent
    {
        protected string _text { get; set; } = "And when I think about Coretta Scott King, I think about the little girl who walked five miles to school on those rural Alabama roads and felt the heat of racism each day she passed the door of the Whites-only school, so much closer to home. It didn't matter, because she studied and succeeded and excelled beyond most of her classmates, Black and White. She earned a college degree, and an acceptance to a prestigious graduate school up North.";
        protected bool _loading { get; set; }
        [Inject]
        protected HttpClient Http { get; set; }

        public EntityLinkingResponse _resultEntities { get; set; } = new EntityLinkingResponse();
        
        protected async void GetEntityLinkingResult()
        {
            _loading = true;
            _resultEntities = await Http.PostJsonAsync<EntityLinkingResponse>("api/SampleData/GetEntityLinkTextAnalytics", new EntityLinkingRequest() { Text = _text });
            await this.GenerateWordCloud();
            _loading = false;
            this.StateHasChanged();

        }

        private async Task GenerateWordCloud()
        {
            await JSRuntime.Current.InvokeAsync<bool>("GenerateWordCloud", _resultEntities.KeyPhraseBatchResult.Documents[0].KeyPhrases);
        }
    }
}