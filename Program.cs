using System;
using System.IO;
using System.Text.Json;

namespace Day19
{
    class Program
    {
        static void Main(string[] args)
        {
            //EnumerateDoc("nasa1.json");
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
                    JsonElement.ObjectEnumerator objEnum = ele.EnumerateObject();
                    while (objEnum.MoveNext())
                    {
                        JsonProperty prop = objEnum.Current;
                        string propName = prop.Name;
                        JsonElement propValue = prop.Value;
                        switch (propValue.ValueKind)    
                        {
                            case JsonValueKind.Object:
                                EnumerateEle(propValue, indentation + "  ");
                                break;
                            case JsonValueKind.Array:
                                Console.WriteLine(indentation + $"{propName} is an array of length {propValue.GetArrayLength()}");
                                EnumerateEle(propValue, indentation + "      ");
                                break;
                            default:
                                Console.WriteLine(indentation + $"{propName}: {propValue}");
                                break;
                        }
                    }
                    break;
                case JsonValueKind.Array:
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
                    Console.WriteLine(indentation + ele);
                    break;
            }
        }
    }
}
