using System;
using System.IO;
using System.Text.Json;

namespace Day19
{
    class Program
    {
        static void Main(string[] args)
        {
            //string data = File.ReadAllText("nasa2.json");

            //using var doc = JsonDocument.Parse(data);
            //JsonElement root = doc.RootElement;

            //Console.WriteLine("The root is of type " + root.ValueKind);

            //EnumerateDoc("nasa2.json");
            EnumerateDoc("countries.json");

        }

        static void EnumerateDoc(string fileName)
        {
            string data = File.ReadAllText(fileName);
            JsonDocument doc = JsonDocument.Parse(data);
            EnumerateElement(doc.RootElement, "");
        }

        static void EnumerateElement(JsonElement elem, string indentation)
        {
            switch (elem.ValueKind)
            {
                case JsonValueKind.Array:
                    for (int i = 0; i < elem.GetArrayLength(); i++)
                    {
                        Console.WriteLine(indentation + "Item " + (i + 1) + " of " + elem.GetArrayLength());
                        EnumerateElement(elem[i], indentation + "    ");
                        Console.WriteLine();
                    }
                    break;

                case JsonValueKind.Object:
                    var objEnum = elem.EnumerateObject();
                    while (objEnum.MoveNext())
                    {
                        JsonProperty prop = objEnum.Current;
                        string name = prop.Name;
                        JsonElement value = prop.Value;
                        switch (value.ValueKind)
                        {
                            case JsonValueKind.Array:
                                Console.WriteLine(indentation + name + " is an array of length " + value.GetArrayLength());
                                EnumerateElement(value, indentation + "    ");
                                break;

                            case JsonValueKind.Object:
                                Console.WriteLine(indentation + name + " is an object");
                                EnumerateElement(value, indentation + "    ");
                                break;

                            default:
                                Console.WriteLine(indentation + name + ": " + value);
                                break;
                        }
                    }
                    break;

                default:
                    Console.WriteLine(indentation + elem);
                    break;
            }
        }

    }
}
