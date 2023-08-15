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
using CompetitionMars.DataModel;

namespace CompetitionMars.Utilities
{
    public class JsonHelper
    {
      
        public List<Education> ReadTestDataFromJson(string jsonFilePath)
        {
            string jsonContent = File.ReadAllText(jsonFilePath);
            List<Education> testData = JsonConvert.DeserializeObject<List<Education>>(jsonContent);
            return testData;
        }

        public List<Certification> ReadCertificateTestDataFromJson(string jsonFilePath)
        {
            string jsonContent = File.ReadAllText(jsonFilePath);
            List<Certification> testData = JsonConvert.DeserializeObject<List<Certification>>(jsonContent);
            return testData;
        }
    }

}
