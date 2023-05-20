using ConsoleApp1;
using Newtonsoft.Json;

// See https://aka.ms/new-console-template for more information
var serialized = JsonConvert.DeserializeObject<Test>("{\"name\": \"foo\"}");
var deserialized = JsonConvert.SerializeObject(serialized);
Console.WriteLine(deserialized);
