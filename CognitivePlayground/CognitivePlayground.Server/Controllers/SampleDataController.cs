using CognitivePlayground.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CognitivePlayground.Server.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        private readonly ITextAnalyticsClient _textAnalyticsClient;

        public SampleDataController(ITextAnalyticsClient textAnalyticsClient)
        {
            _textAnalyticsClient = textAnalyticsClient;
        }

        private static string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };


        [HttpGet("[action]")]
        public IEnumerable<WeatherForecast> WeatherForecasts()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            });
        }

        [HttpPost("[action]")]
        public EntityLinkingResponse GetEntityLinkTextAnalytics([FromBody] EntityLinkingRequest request)
        {
            var response = new EntityLinkingResponse();
            if (!string.IsNullOrEmpty(request.Text))
            {
                var resultLanguage = _textAnalyticsClient.DetectLanguageAsync(new BatchInput(
                       new List<Input>()
                           {
                          new Input("1", request.Text),
                       })).Result;

                if (resultLanguage != null && resultLanguage.Documents.Any())
                {
                    var maxScore = resultLanguage.Documents[0].DetectedLanguages.Max(y => y.Score);
                    response.DetectedLanguage = resultLanguage.Documents[0].DetectedLanguages.FirstOrDefault(x => x.Score == maxScore);

                    var result4 = _textAnalyticsClient.EntitiesAsync(
                        new MultiLanguageBatchInput(
                            new List<MultiLanguageInput>()
                            {
                          new MultiLanguageInput(response.DetectedLanguage.Iso6391Name, "0", request.Text)
                            })).Result;

                    response.EntityRecords = result4.Documents[0].Entities;
                }


            }

            return response;
        }
    }
}
