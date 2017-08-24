using System;
using Tweetinvi.Models;

namespace DTuriasConnectorTwitter.Utils
{
    class Mappings
    {
        public static GeoCode GeoCode(DTuriasConnectorTwitter.Config.Location location)
        {
            if (location.latitude == 0 && location.longitude == 0)
            {
                return null;
            }
            else
            {
                return new GeoCode(location.latitude, location.longitude, location.radius, DistanceMeasure.Kilometers);
            }    
        }

        public static LanguageFilter? Lang(String language)
        {
            switch (language)
            {
                case "EN":
                    return LanguageFilter.English;
                case "ES":
                    return LanguageFilter.Spanish;
                default:
                    return null;
            }
        }

        public static DateTime Until(String until)
        {
            string[] splited = until.Split("-");
            return new DateTime(Int32.Parse(splited[0]), Int32.Parse(splited[1]), Int32.Parse(splited[2]));
        }

        public static SearchResultType? SearchType(String searchResultType)
        {
            switch (searchResultType)
            {
                case "MIXED":
                    return SearchResultType.Mixed;
                case "POPULAR":
                    return SearchResultType.Popular;
                default:
                    return SearchResultType.Recent;
            }
        }

        public static DateTime Since(String since)
        {
            string[] splited = since.Split("-");
            return new DateTime(Int32.Parse(splited[0]), Int32.Parse(splited[1]), Int32.Parse(splited[2]));
        }
    }
}
