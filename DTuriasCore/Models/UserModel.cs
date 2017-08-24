using System.ComponentModel.DataAnnotations;

namespace DTuriasCore.Models
{
    public class UserModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string ScreenName { get; set; }

        public override string ToString()
        {
            return $@"
ScreenName: {ScreenName}
    Name: {Name}
";
        }
    }
}
