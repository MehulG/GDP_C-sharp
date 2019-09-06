using System;
using Xunit;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using GDP;

namespace GDP.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {

            List<string> line_exp = new List<string>();
            List<string> line_act = new List<string>();

            GDP.Program.GDP_Prog();

            var reader = new StreamReader("../../../../expected-output.json");
                while (!reader.EndOfStream)
                {
                    line_exp.Add(reader.ReadLine().Replace(" ",""));
                }


            var reader_act = new StreamReader("../../../../actual-output.json");
            
                while (!reader_act.EndOfStream)
                {
                    line_act.Add(reader_act.ReadLine().Replace(" ", ""));
                }
            
            Assert.Equal(line_exp, line_act);


        }

        [Fact]
        public void Test2()
        {



            var reader1 = JsonConvert.DeserializeObject<JToken>(File.ReadAllText("../../../../expected-output.json"));
            var reader2 = JsonConvert.DeserializeObject<JToken>(File.ReadAllText("../../../../actual-output.json"));

            Assert.True(JToken.DeepEquals(reader1, reader2));
        }
        [Fact]
        public void IsNotEmpty()
        {
            bool flag = true;
            if(new FileInfo("../../../../actual-output.json").Length == 0)
            {
                flag = false;
            }
            Assert.True(flag);
        }
        [Fact]
        public void filePresent()
        {
            bool flag = false;
            if (File.Exists("../../../../actual-output.json"))
            {
                flag = true;
            }
            Assert.True(flag);
        }
    }
}
