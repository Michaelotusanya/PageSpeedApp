using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static PageSpeedCheck.Model.Dto.PageSpeedDto;

namespace PageSpeedCheck.Web.Utility
{
    public static class StringExtensionsHelper
    {

        public static string FormatString(this string formattedString, List<Arg> args)
        {
            var matches = Regex.Matches(formattedString, "[$\\d]{2}").Cast<Match>().ToArray();

            foreach (var match in matches)
            {
                var number = Convert.ToInt32(match.Value.Replace("$", string.Empty));
                formattedString = formattedString.Replace(match.Value, args.Skip(number - 1).FirstOrDefault()?.value);
            }

            return formattedString;
        }
    }
}
