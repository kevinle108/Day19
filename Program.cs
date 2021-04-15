using System;
using System.IO;
using System.Text.Json;

namespace Day19
{
    class Program
    {
        static void Main(string[] args)
        {

            //EnumerateDoc("nasa2.json");
            EnumerateDoc("countries.json");


        }        
        static void EnumerateDoc(string fileName)
        {
            using JsonDocument doc = JsonDocument.Parse(File.ReadAllText(fileName));
            JsonElement root = doc.RootElement;
            EnumerateEle(root, "");
        }
        static void EnumerateEle(JsonElement ele, string indentation)
        {
            switch (ele.ValueKind)
            {
                
                case JsonValueKind.Object:
                    Console.WriteLine(indentation + "This is an Object");
                    JsonElement.ObjectEnumerator objEnum = ele.EnumerateObject();
                    while (objEnum.MoveNext())
                    {
                        JsonProperty prop = objEnum.Current;
                        string propName = prop.Name;
                        JsonElement propValue = prop.Value;
                        Console.WriteLine(indentation + " " + $"{propName} is an {propValue.ValueKind}");
                        EnumerateEle(propValue, indentation + "  ");
                    }
                    break;
                case JsonValueKind.Array:
                    Console.WriteLine(indentation + $"This is an Array of {ele.GetArrayLength()} items:");

                    JsonElement.ArrayEnumerator arrEnum = ele.EnumerateArray();
                    int i = 1;
                    
                    while (arrEnum.MoveNext())
                    {                        
                        Console.WriteLine(indentation + $"Item {i} of {ele.GetArrayLength()}");
                        EnumerateEle(arrEnum.Current, indentation + "  ");
                        Console.WriteLine();
                        i++;
                    }
                    break;               
                default:
                    Console.WriteLine(indentation + "This is a Primitive");
                    Console.WriteLine(indentation + ele);
                    break;
            }
        }
    }
}
