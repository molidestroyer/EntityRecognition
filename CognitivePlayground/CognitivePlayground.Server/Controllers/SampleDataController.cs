﻿using CognitivePlayground.Shared;
using MediawikiSharp_API;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics.Models;
using System;
using System.Collections.Generic;
using System.Linq;

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
                    EntitiesBatchResultV2dot1 result4 = DetectLanguage(request, response, resultLanguage);

                    // TODO use automapper
                    Map(response, result4);

                    var imageAddedCollection = response.EntityRecords.Select(t =>
                    {
                        if (!string.IsNullOrEmpty(t.WikipediaId))
                        {
                            var mediawiki = new Mediawiki(response.DetectedLanguage.Iso6391Name);

                            var images = mediawiki.GetImagesURLAsync(t.WikipediaId).Result;
                            if (images.Any())
                            {
                                var excludedList = new List<string> { "Padlock", "Search", "ogg" };
                                t.WikipediaImageUrl = images.FirstOrDefault(x => !excludedList.Any(e => x.URL.Contains(e)))?.URL;
                            }
                        }
                        return t;
                    });
                    response.EntityRecords = imageAddedCollection;
                }
            }

            return response;
        }

        private EntitiesBatchResultV2dot1 DetectLanguage(EntityLinkingRequest request, EntityLinkingResponse response, LanguageBatchResult resultLanguage)
        {
            var maxScore = resultLanguage.Documents[0].DetectedLanguages.Max(y => y.Score);
            response.DetectedLanguage = resultLanguage.Documents[0].DetectedLanguages.FirstOrDefault(x => x.Score == maxScore);

            var result4 = _textAnalyticsClient.EntitiesAsync(
                new MultiLanguageBatchInput(
                    new List<MultiLanguageInput>()
                    {
                          new MultiLanguageInput(response.DetectedLanguage.Iso6391Name, "0", request.Text)
                    })).Result;
            return result4;
        }

        private static void Map(EntityLinkingResponse response, EntitiesBatchResultV2dot1 result4)
        {
            response.EntityRecords = result4.Documents[0].Entities.Select(t => new EntityRecord()
            {
                BingId = t.BingId,
                Name = t.Name,
                SubType = t.SubType,
                Type = t.Type,
                WikipediaId = t.WikipediaId,
                WikipediaLanguage = t.WikipediaLanguage,
                WikipediaUrl = t.WikipediaUrl
            });
        }
    }
}
