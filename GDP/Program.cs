using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace GDP
{

    public class cont
    {
        public double GDP_2012{ get; set; }
        public double POPULATION_2012 { get; set; }
        public cont(double gdp, double pop)
        {
            GDP_2012 = gdp;
            POPULATION_2012 = pop;
        }
    }

    public static class Program
    {

        public static void Main(string[] args)
        {
           /* GDP_Prog();*/
        }
        public static void GDP_Prog()
        {
            Console.WriteLine("Hello World!");

            List<string> Country = new List<string>();
            List<string> GDP = new List<string>();
            List<string> pop = new List<string>();
            using (var reader = new StreamReader("../../../data/datafile.csv"))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    //Console.WriteLine(values[2]);
                    Country.Add(values[0].Replace("\"", ""));
                    GDP.Add(values[7].Replace("\"", ""));
                    pop.Add(values[4].Replace("\"", ""));

                }
                /*foreach (string p in pop)
                {
                    Console.WriteLine(p);
                };
*/

            }

            int list_no = Country.Count;

            List<string> contenent_array = new List<string>();
            List<string> country_array = new List<string>();

            string[] continentArray = { "South America", "Oceania", "North America", "Asia", "Europe", "Africa" };
            double[] GDP_Array = { 0, 0, 0, 0, 0, 0 };
            double[] pop_Array = { 0, 0, 0, 0, 0, 0 };

            using (var reader = new StreamReader("../../../data/continent.csv"))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    contenent_array.Add(values[0]);
                    country_array.Add(values[1]);

                    /*
                                        //Console.WriteLine(values[2]);
                                        Country.Add(values[0].Replace("\"", ""));
                                        GDP.Add(values[7].Replace("\"", ""));
                                        pop.Add(values[4].Replace("\"", ""));*/

                }
                /*foreach (string p in country_array)
                {
                    Console.WriteLine(p);
                };
*/
                int list_no_cont = country_array.Count;
                /*Console.WriteLine(list_no_cont);*/

                

                for (int i = 1; i < list_no; i++)
                {
                    for (int j = 1; j < list_no_cont; j++)
                    {
                        if (Country[i] == country_array[j])
                        {
                            switch (contenent_array[j])
                            {
                                case "Africa":
                                    GDP_Array[5] += Convert.ToDouble(GDP[i]);
                                    pop_Array[5] += Convert.ToDouble(pop[i]);
                                    break;
                                case "Asia":
                                    GDP_Array[3] += Convert.ToDouble(GDP[i]);
                                    pop_Array[3] += Convert.ToDouble(pop[i]);

                                    break;
                                case "Europe":
                                    GDP_Array[4] += Convert.ToDouble(GDP[i]);
                                    pop_Array[4] += Convert.ToDouble(pop[i]);

                                    break;
                                case "North America":
                                    GDP_Array[2] += Convert.ToDouble(GDP[i]);
                                    pop_Array[2] += Convert.ToDouble(pop[i]);

                                    break;
                                case "South America":
                                    GDP_Array[0] += Convert.ToDouble(GDP[i]);
                                    pop_Array[0] += Convert.ToDouble(pop[i]);

                                    break;
                                case "Oceania":
                                    GDP_Array[1] += Convert.ToDouble(GDP[i]);
                                    pop_Array[1] += Convert.ToDouble(pop[i]);

                                    break;
                            }
                        }
                    }

                }
                for (int i = 0; i < GDP_Array.Length; i++)
                {
                    Console.WriteLine(GDP_Array[i]);
                }

                /*string output = "{\n";
                for (int i = 0; i < continentArray.Length; i++)
                {
                    output += "\"" + continentArray[i] + "\"";
                    output += ":{\n\"GDP_2012\": " + GDP_Array[i] + ",\n\"POPULATION_2012\": " + pop_Array[i] + "},\n";

                }
                output = output.Remove(output.Length - 2, 2);
                output += "\n}";
                Console.WriteLine(output);
                File.WriteAllText("../../../../actual-output.json", output);*/
                Dictionary<string, cont> dict = new Dictionary<string, cont>();
                for(int i = 0; i< continentArray.Length; i++)
                {
                    cont NewCont = new cont(GDP_Array[i], pop_Array[i]);
                    dict.Add(continentArray[i], NewCont);
                }

                string json_file = JsonConvert.SerializeObject(dict, Formatting.Indented);
                Console.WriteLine(json_file);
                System.IO.File.WriteAllText("../../../../actual-output.json", json_file);

            }
        }
    }
}
