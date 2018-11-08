using System;
using System.Collections.Generic;
using System.Text;

namespace CognitivePlayground.Shared
{
    public class MatchRecord
    {
        public string Text { get; set; }
        public int? Offset { get; set; }
        public int? Length { get; set; }
    }
}
