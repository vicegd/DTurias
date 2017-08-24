namespace DTuriasCore.Models
{
    public class PlaceModel
    {
        //[Key]
        public long Id { get; set; }
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public string FullName { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return $@"
FullName: {FullName}
   Name: {Name}
   Country: {Country} ({CountryCode})
";
        }
    }
}
