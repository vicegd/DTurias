using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTuriasConnectorTwitter.Config
{
    public class Query
    {
        public string query { get; set; }
    }

    public class Location
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
        public double radius { get; set; }
    }

    public class MessagesToSearch
    {
        public List<Query> queries { get; set; }
        public int startIn { get; set; }
        public int endsIn { get; set; }
        public int every { get; set; }
        public Location location { get; set; }
        public string resultType { get; set; }
        public string language { get; set; }
        public string since { get; set; }
        public string until { get; set; }
        public int maxResultsPerCycle { get; set; }
    }

    public class Location2
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
        public int radius { get; set; }
    }

    public class PlacesToSearch
    {
        public string text { get; set; }
        public int startIn { get; set; }
        public long endsIn { get; set; }
        public int every { get; set; }
        public Location2 location { get; set; }
        public string granularity { get; set; }
        public int maxResultsPerCycle { get; set; }
    }

    public class UsersToSearch
    {
        public int startIn { get; set; }
        public long endsIn { get; set; }
        public int every { get; set; }
        public string text { get; set; }
        public int maxResultsPerCycle { get; set; }
    }

    public class TrendsToSearch
    {
        public int woeid { get; set; }
        public int startIn { get; set; }
        public long endsIn { get; set; }
        public int every { get; set; }
    }

    public class Queries
    {
        public List<MessagesToSearch> messagesToSearch { get; set; }
        public List<PlacesToSearch> placesToSearch { get; set; }
        public List<UsersToSearch> usersToSearch { get; set; }
        public List<TrendsToSearch> trendsToSearch { get; set; }
    }
}
