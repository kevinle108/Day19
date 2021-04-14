using System;
using System.IO;
using System.Text.Json;

namespace Day19
{
    class Program
    {
        static void Main(string[] args)
        {
            EnumerateDoc("nasa1.json");
        }

        static void EnumerateDoc(string fileName)
        {
            string data = File.ReadAllText("countries.json");
            using JsonDocument doc = JsonDocument.Parse(data);
            JsonElement root = doc.RootElement;
            Console.WriteLine($"Root is an {root.ValueKind}");
            EnumerateEle(root);
            
        }

        static void EnumerateEle(JsonElement ele)
        {
            switch (ele.ValueKind)
            {
                case JsonValueKind.Object:
                    Console.WriteLine("Enumerating object...");
                    var objEnum = ele.EnumerateObject();                    
                    while (objEnum.MoveNext())
                    {
                        string propName = objEnum.Current.Name;
                        JsonElement propValue = objEnum.Current.Value;
                        JsonValueKind propValueType = objEnum.Current.Value.ValueKind;
                        Console.WriteLine($"{propName} is an {propValueType}");                        
                        //Console.WriteLine($"  Value: {propValue}");
                        EnumerateEle(propValue);
                    }
                    break;
                case JsonValueKind.Array:
                    Console.WriteLine("Enumerating array...");
                    for (int i = 0; i < ele.GetArrayLength(); i++)
                    {
                        Console.WriteLine($"Displaying {i+1} of {ele.GetArrayLength()}");
                        EnumerateEle(ele[i]);
                        Console.WriteLine("***********");
                    }
                    break;
                default:
                    Console.WriteLine("Here is the value: " + ele);
                    Console.WriteLine();
                    break;
            }
        }
        
    }
}
