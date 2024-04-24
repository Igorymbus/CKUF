using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace Practical_5._Calendar
{
    public class De_Serealize_
    {
        public static void Serialize<T>(List<T> notes, string way)
        {
            string json = JsonConvert.SerializeObject(notes);
            if (!way.Contains("allChoices.json"))
            {
                way += "\\allChoices.json";
            }
            File.WriteAllText(way, json);
        }
        public static List<T> Deserialize<T>(string way)
        {
            List<T> list = new List<T>();

            Directory.CreateDirectory(way);
            way += "\\allChoices.json";

            if (!File.Exists(way))
            {
                File.Create(way);
            }

            string json = File.ReadAllText(way);
            if (json is "" or null)
            {
                Serialize(list, way);
                return list;
            }

            list = JsonConvert.DeserializeObject<List<T>>(json);
            return list;
        }
    }
}
