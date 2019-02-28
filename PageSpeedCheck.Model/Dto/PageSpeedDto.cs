using System;
using System.Collections.Generic;
using System.Text;

namespace PageSpeedCheck.Model.Dto
{
    public class PageSpeedDto
    {
        public string captchaResult { get; set; }
        public string kind { get; set; }
        public string id { get; set; }
        public int responseCode { get; set; }
        public string title { get; set; }
        public int score { get; set; }
        public PageStats pageStats { get; set; }
        public FormattedResults formattedResults { get; set; }
        public Version version { get; set; }

        public class PageStats
        {
            public int numberResources { get; set; }
            public int numberHosts { get; set; }
            public string totalRequestBytes { get; set; }
            public int numberStaticResources { get; set; }
            public string htmlResponseBytes { get; set; }
            public string textResponseBytes { get; set; }
            public string cssResponseBytes { get; set; }
            public string imageResponseBytes { get; set; }
            public string javascriptResponseBytes { get; set; }
            public string otherResponseBytes { get; set; }
            public int numberJsResources { get; set; }
            public int numberCssResources { get; set; }
        }

        public class Arg
        {
            public string type { get; set; }
            public string value { get; set; }
        }

        public class Header
        {
            public string format { get; set; }
            public List<Arg> args { get; set; }
        }

        public class UrlBlock
        {
            public Header header { get; set; }
            public List<Url> urls { get; set; }
        }

        public class MajorRules
        {
            public string localizedRuleName { get; set; }
            public double ruleImpact { get; set; }
            public List<UrlBlock> urlBlocks { get; set; }
        }


        public class Result
        {
            public string format { get; set; }
            public List<Arg> args { get; set; }
        }

        public class Url
        {
            public Result result { get; set; }
        }

        public class FormattedResults
        {
            public string locale { get; set; }
            public IDictionary<string, MajorRules> ruleResults { get; set; }
        }

        public class Version
        {
            public int major { get; set; }
            public int minor { get; set; }
        }
    }
}
