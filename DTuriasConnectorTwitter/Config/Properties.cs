using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTuriasConnectorTwitter.Config
{
    public class Twitter
    {
        public string consumerKey { get; set; }
        public string consumerSecret { get; set; }
        public string token { get; set; }
        public string tokenSecret { get; set; }
    }

    public class User
    {
        public string id { get; set; }
        public string name { get; set; }
        public Twitter twitter { get; set; }
    }

    public class Database
    {
        public string url { get; set; }
    }

    public class Properties
    {
        public User user { get; set; }
        public Database database { get; set; }
    }
}
