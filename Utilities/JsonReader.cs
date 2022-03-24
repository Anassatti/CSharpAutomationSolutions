using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumAutomationProject1.Utilities
{
    public class JsonReader
    {
        public JsonReader()
        {

        }

        public string extractData(string tokenName)
        {
            //Put test data on Json file
            var myJsonTexts = File.ReadAllText("Utilities/TestData.json");
            var JsonObject = JToken.Parse(myJsonTexts);
            return JsonObject.SelectToken(tokenName).Value<string>();
        }
        public string[] extractDataArray(string tokenName)
        {
            //Put test data on Json file
            var myJsonTexts = File.ReadAllText("Utilities/TestData.json");
            var JsonObject = JToken.Parse(myJsonTexts);
            List<string> productlists = JsonObject.SelectTokens(tokenName).Values<string>().ToList();
            return productlists.ToArray();
        }


    }
}
