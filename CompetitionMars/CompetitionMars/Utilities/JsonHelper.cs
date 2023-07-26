using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace CompetitionMars.Utilities
{
    public class JsonHelper
    {
       public List<LoginTestModel> ReadTestDataFromJson(string jsonFilePath)
        {
            string jsonContent = File.ReadAllText(jsonFilePath);
            List<LoginTestModel> testData = JsonConvert.DeserializeObject<List<LoginTestModel>>(jsonContent);
            return testData;
        }

        public List<AddEducationTestModel> ReadTestDataAddEducationFromJson(string jsonFilePath)
        {
            string jsonContent = File.ReadAllText(jsonFilePath);
            List<AddEducationTestModel> testData = JsonConvert.DeserializeObject<List<AddEducationTestModel>>(jsonContent);
            return testData;
        }
    }

}
