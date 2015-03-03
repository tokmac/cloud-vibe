using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Cloud_Vibe.Utilities
{
    public static class VideoUtility
    {
        public static string YoutubeWatchToEmbededLink(string link)
        {
            var id = Regex.Match(link, @"(?:youtube\.com\/(?:[^\/]+\/.+\/|(?:v|e(?:mbed)?)\/|.*[?&amp;]v=)|youtu\.be\/)([^""&amp;?\/ ]{11})").Groups[1].Value;
            var result = "https://www.youtube.com/embed/" + id;
            //https://www.youtube.com/embed/6vYnas6q3Sg

            //https://www.youtube.com/watch?v=6vYnas6q3Sg&t=36

            return result;
        }
    }
}