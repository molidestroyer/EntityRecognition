using System.Collections.Generic;

namespace CognitivePlayground.Shared
{

    public class EntityRecord
    {
        public string Name { get; set; }
        public IList<MatchRecord> Matches { get; set; }
        public string WikipediaLanguage { get; set; }
        public string WikipediaId { get; set; }
        public string WikipediaUrl { get; set; }
        public string BingId { get; set; }
        public string Type { get; set; }
        public string SubType { get; set; }
        public string WikipediaImageUrl { get; set; }


    }

}
