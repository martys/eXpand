using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Xpand.ExpressApp.Win.Editors
{
    public static class XpandDurationAsTextHelper
    {
        
        public static readonly string Hint = " Examples:  " + Environment.NewLine +
                                             " 1d                     = 1 Day" + Environment.NewLine +
                                             " 48 H                   = 2 Days" + Environment.NewLine +
                                             " 2d 5h 45 m             = 2 Days 5 Hours 45 minutes" + Environment.NewLine +
                                             " 2 days 4 hours 25 min  = 2 Days 4 Hours 25 minutes" + Environment.NewLine;

        const string Quantity = "quantity";
        const string Unit = "unit";

        const string Days = @"(D(ays?)?)";
        const string Hours = @"(H((ours?)|(rs?))?)";
        const string Minutes = @"(M((inutes?)|(ins?))?)";
        const string Seconds = @"(S((econds?)|(ecs?))?)";
        
        public static string Mask =
            string.Format(@"\s*((\d?\d?\d?\s*{0}))?\s*((\d?\d?\s*{1}))?\s*((\d?\d?\s*{2}))?\s*"
                        , Days
                        , Hours
                        , Minutes);


        /// <summary>
        /// Converts the string to TimeSpan
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static TimeSpan ConvertStringToTimeSpan(string s)
        {
            var timeSpanRegex = new Regex(
                string.Format(@"\s*(?<{0}>\d+)\s*(?<{1}>({2}|{3}|{4}|{5}|\Z))",
                              Quantity, Unit, Days, Hours, Minutes, Seconds),
                              RegexOptions.IgnoreCase);
            var matches = timeSpanRegex.Matches(s);

            var ts = new TimeSpan();
            foreach (Match match in matches)
            {
                if (Regex.IsMatch(match.Groups[Unit].Value, @"\A" + Days))
                {
                    ts = ts.Add(TimeSpan.FromDays(double.Parse(match.Groups[Quantity].Value)));
                }
                else if (Regex.IsMatch(match.Groups[Unit].Value, Hours))
                {
                    ts = ts.Add(TimeSpan.FromHours(double.Parse(match.Groups[Quantity].Value)));
                }
                else if (Regex.IsMatch(match.Groups[Unit].Value, Minutes))
                {
                    ts = ts.Add(TimeSpan.FromMinutes(double.Parse(match.Groups[Quantity].Value)));
                }
                else if (Regex.IsMatch(match.Groups[Unit].Value, Seconds))
                {
                    ts = ts.Add(TimeSpan.FromSeconds(double.Parse(match.Groups[Quantity].Value)));
                }
                else
                {
                    // Quantity given but no unit, default to Hours
                    ts = ts.Add(TimeSpan.FromHours(double.Parse(match.Groups[Quantity].Value)));
                }
            }
            return ts;
        }

        /// <summary>
        /// Converts timeSpan to a readable String like : 2 days 4 hours 25 minutes  
        /// </summary>
        /// <param name="timeSpan"></param>
        /// <returns>Text</returns>
        public static string ConvertTimeSpanToReadableString(TimeSpan timeSpan)
        {

            var time = string.Empty;

            if (timeSpan.Days > 0)
                time = timeSpan.Days + " Day" +(timeSpan.Days > 1 ? "s" : "");

            if (timeSpan.Hours > 0)
                time += (time != string.Empty ? " " : "") + timeSpan.Hours + " Hour"
                        +(timeSpan.Hours.ToString().ToCharArray().Last() != '1' ? "s" : "");

            if (timeSpan.Minutes > 0)
                time += (time != string.Empty ? " " : "") + timeSpan.Minutes + " Minute"
                    + (timeSpan.Minutes.ToString().ToCharArray().Last() != '1' ? "s" : ""); 

            if (timeSpan.Seconds > 0)
                time += (time != string.Empty ? " " : "") + timeSpan.Seconds + " Second"
                    + (timeSpan.Seconds.ToString().ToCharArray().Last() != '1' ? "s" : ""); 

            return time;
        }
    }
}
