using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTuriasCore.Models
{
    public class HashTagModel
    {
        public long Id { get; set; }
        public string Text { get; set; }

        public override string ToString()
        {
            return $@"
HASHTAG
    Text: {Text}
";
        }
    }
}
