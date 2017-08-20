namespace DTuriasCore.Models
{
    public class User
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
