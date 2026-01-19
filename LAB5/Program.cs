using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Xml.Serialization;
using System.Globalization;

namespace DataProcessor;

public record PersonProfile(string Name, string Surname, List<int> Scores)
{
    public PersonProfile() : this(string.Empty, string.Empty, new List<int>()) { }

    public override string ToString() => $"{Name} {Surname} | {string.Join(", ", Scores)}";
}

class Program
{
    static void Main()
    {
        const string txtFile = "archive.txt";
        const string jsonFile = "data.json";
        const string xmlFile = "data.xml";
        const string csvFile = "Iris.csv";

        ProcessTextFile(txtFile);

        var group = new List<PersonProfile>
        {
            new("Jan", "Kowalski", [3, 4, 5]),
            new("Anna", "Nowak", [5, 5, 4]),
            new("Piotr", "Wisniewski", [2, 3, 3])
        };

        ExportToJson(group, jsonFile);
        ImportFromJson(jsonFile);

        ExportToXml(group, xmlFile);
        ImportFromXml(xmlFile);

        if (File.Exists(csvFile))
        {
            AnalyzeCsv(csvFile);
            FilterCsv(csvFile, "filtered_results.csv");
        }
    }

    static void ProcessTextFile(string path)
    {
        using (var sw = new StreamWriter(path))
        {
            for (int i = 1; i <= 3; i++)
            {
                Console.Write($"> ");
                sw.WriteLine(Console.ReadLine());
            }
        }

        File.AppendAllText(path, Console.ReadLine() + Environment.NewLine);
        Console.WriteLine(File.ReadAllText(path));
    }

    static void ExportToJson(List<PersonProfile> items, string path)
    {
        var cfg = new JsonSerializerOptions { WriteIndented = true };
        File.WriteAllText(path, JsonSerializer.Serialize(items, cfg));
    }

    static void ImportFromJson(string path)
    {
        if (!File.Exists(path)) return;
        var data = JsonSerializer.Deserialize<List<PersonProfile>>(File.ReadAllText(path));
        data?.ForEach(x => Console.WriteLine(x));
    }

    static void ExportToXml(List<PersonProfile> items, string path)
    {
        var xs = new XmlSerializer(typeof(List<PersonProfile>));
        using var fs = File.Create(path);
        xs.Serialize(fs, items);
    }

    static void ImportFromXml(string path)
    {
        if (!File.Exists(path)) return;
        var xs = new XmlSerializer(typeof(List<PersonProfile>));
        using var fs = File.OpenRead(path);
        var data = (List<PersonProfile>)xs.Deserialize(fs);
        data.ForEach(x => Console.WriteLine(x));
    }

    static void AnalyzeCsv(string path)
    {
        var lines = File.ReadLines(path).Skip(1).Select(l => l.Split(',')).ToList();
        string[] headers = { "SL", "SW", "PL", "PW" };

        for (int i = 0; i < 4; i++)
        {
            var avg = lines.Select(c => double.TryParse(c[i], CultureInfo.InvariantCulture, out var v) ? v : 0).Average();
            Console.WriteLine($"{headers[i]}: {avg:F2}");
        }
    }

    static void FilterCsv(string src, string dest)
    {
        var output = File.ReadLines(src)
            .Skip(1)
            .Select(line => line.Split(','))
            .Where(c => double.TryParse(c[0], CultureInfo.InvariantCulture, out var val) && val < 5.0)
            .Select(c => $"{c[0]},{c[1]},{c[4]}")
            .Prepend("L1,L2,Type");

        File.WriteAllLines(dest, output);
    }
}