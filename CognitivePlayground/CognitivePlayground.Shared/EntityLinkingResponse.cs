using Microsoft.Azure.CognitiveServices.Language.TextAnalytics.Models;
using System.Collections.Generic;

namespace CognitivePlayground.Shared
{
    public class EntityLinkingResponse
    {
        public IEnumerable<EntityRecordV2dot1> EntityRecords { get; set; } = new List<EntityRecordV2dot1>();
        public DetectedLanguage DetectedLanguage { get; set; }

    }
}
