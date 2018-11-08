using Microsoft.Azure.CognitiveServices.Language.TextAnalytics.Models;
using System.Collections.Generic;

namespace CognitivePlayground.Shared
{
    public class EntityLinkingResponse
    {
        public IEnumerable<EntityRecord> EntityRecords { get; set; } = new List<EntityRecord>();
        public DetectedLanguage DetectedLanguage { get; set; }

    }
}
