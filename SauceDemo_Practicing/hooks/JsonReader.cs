using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SauceDemo_Practicing.hooks
{
    internal class JsonReader
    {
        public static String getDataJson(String tokenName)
        {
            String myJsonString = File.ReadAllText("C:/Users/Murillo/source/repos/SauceDemo_Practicing/SauceDemo_Practicing/data.json");
            var jsonObject = JToken.Parse(myJsonString);
            //TestContext.Progress.WriteLine(jsonObject.SelectToken("user_valid_credentials").Values<string>());
            return jsonObject.SelectToken(tokenName).Value<string>();
        }
    }
}
