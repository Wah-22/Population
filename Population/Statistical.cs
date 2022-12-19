using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Population
{
   public class Statistical
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);

        public class Root
        {
            public GET_STATS_DATA GET_STATS_DATA { get; set; }
        }

        public class GET_STATS_DATA
        {
            public RESULT RESULT { get; set; }
            public PARAMETER PARAMETER { get; set; }
            public STATISTICAL_DATA STATISTICAL_DATA { get; set; }
        }

        public class RESULT
        {
            public int STATUS { get; set; }
            public string ERROR_MSG { get; set; }
            public DateTime DATE { get; set; }
        }

        public class PARAMETER
        {
            public string LANG { get; set; }
            public string STATS_DATA_ID { get; set; }
            public string DATA_FORMAT { get; set; }
            public int START_POSITION { get; set; }
            public string METAGET_FLG { get; set; }
            public string EXPLANATION_GET_FLG { get; set; }
            public string ANNOTATION_GET_FLG { get; set; }
            public int REPLACE_SP_CHARS { get; set; }
            public string CNT_GET_FLG { get; set; }
            public int SECTION_HEADER_FLG { get; set; }
        }

        public class STATISTICAL_DATA
        {
            public RESULTINF RESULT_INF { get; set; }
            public TABLE_INF TABLE_INF { get; set; }
            public CLASS_INF CLASS_INF { get; set; }
            public DATA_INF DATA_INF { get; set; }
        }

        public class RESULTINF
        {
            public int TOTAL_NUMBER { get; set; }
            public int FROM_NUMBER { get; set; }
            public int TO_NUMBER { get; set; }
        }

        public class TABLE_INF
        {
            [JsonProperty("@id")]
            public string Id { get; set; }
            public STAT_NAME STAT_NAME { get; set; }
            public GOV_ORG GOV_ORG { get; set; }
            public string STATISTICS_NAME { get; set; }
            public TITLE TITLE { get; set; }
            public string CYCLE { get; set; }
            public int SURVEY_DATE { get; set; }
            public string OPEN_DATE { get; set; }
            public int SMALL_AREA { get; set; }
            public string COLLECT_AREA { get; set; }
            public MAIN_CATEGORY MAIN_CATEGORY { get; set; }
            public SUB_CATEGORY SUB_CATEGORY { get; set; }
            public int OVERALL_TOTAL_NUMBER { get; set; }
            public string UPDATED_DATE { get; set; }
            public STATISTICS_NAME_SPEC STATISTICS_NAME_SPEC { get; set; }
            public string DESCRIPTION { get; set; }
            public TITLESPEC TITLE_SPEC { get; set; }
        }

        public class CLASS_INF
        {
            public List<CLASS_OBJ> CLASS_OBJ { get; set; }
        }

        public class DATA_INF
        {
            public List<NOTE> NOTE { get; set; }
            public List<VALUE> VALUE { get; set; }
        }

        public class STAT_NAME
        {
            [JsonProperty("@code")]
            public string Code { get; set; }
            public string sign { get; set; }
        }

        public class GOV_ORG
        {
            [JsonProperty("@code")]
            public string Code { get; set; }
            public string sign { get; set; }
        }

        public class TITLE
        {
            [JsonProperty("@no")]
            public string No { get; set; }
            public string sign { get; set; }
        }

        public class MAIN_CATEGORY
        {
            [JsonProperty("@code")]
            public string Code { get; set; }
            public string sign { get; set; }
        }

        public class SUB_CATEGORY
        {
            [JsonProperty("@code")]
            public string Code { get; set; }
            public string sign { get; set; }
        }

        public class STATISTICS_NAME_SPEC
        {
            public string TABULATION_CATEGORY { get; set; }
            public string TABULATION_SUB_CATEGORY1 { get; set; }
            public string TABULATION_SUB_CATEGORY2 { get; set; }
        }

        public class TITLESPEC
        {
            public string TABLE_CATEGORY { get; set; }
            public string TABLE_NAME { get; set; }
        }

        public class CLASS_OBJ
        {
            [JsonProperty("@id")]
            public string Id { get; set; }

            [JsonProperty("@name")]
            public string Name { get; set; }
            public object CLASS { get; set; }
         
        }
       
        public class CLASS_OBJ_Tab
        {
            [JsonProperty("@id")]
            public string Id { get; set; }

            [JsonProperty("@name")]
            public string Name { get; set; }
            public List<Statistical.CLASS> CLASS { get; set; }
        }

        public class CLASS
        {
            [JsonProperty("@code")]
            public string code { get; set; }

            [JsonProperty("@name")]
            public string name { get; set; }

            [JsonProperty("@level")]
            public string level { get; set; }

            [JsonProperty("@unit")]
            public string unit { get; set; }

            [JsonProperty("@parentCode")]
            public string parentCode { get; set; }
        }

        public class NOTE
        {
            [JsonProperty("@char")]
            public string Char { get; set; }
            public string sign { get; set; }
        }

        public class VALUE
        {
            [JsonProperty("@tab")]
            public string Tab { get; set; }

            [JsonProperty("@cat01")]
            public string Cat01 { get; set; }

            [JsonProperty("@cat02")]
            public string Cat02 { get; set; }

            [JsonProperty("@cat03")]
            public string Cat03 { get; set; }

            [JsonProperty("@cat04")]
            public string Cat04 { get; set; }

            [JsonProperty("@area")]
            public string Area { get; set; }

            [JsonProperty("@time")]
            public string Time { get; set; }

            [JsonProperty("@unit")]
            public string Unit { get; set; }
            public string sign { get; set; }
        }



    }
}
