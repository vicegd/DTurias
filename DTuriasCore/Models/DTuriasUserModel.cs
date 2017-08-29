using System.ComponentModel.DataAnnotations;

namespace DTuriasCore.Models
{
    public class DTuriasUserModel
    {
        public long Id { get; set; }
        public string Nick { get; set; }

        public override string ToString()
        {
            return $@"
Nick: {Nick}
";
        }
    }
}
