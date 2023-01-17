using System.Collections.Generic;
using System.Text;
using UnityEngine;
public static class Localization
{
    private static Dictionary<string, string[]> dictionary = new Dictionary<string, string[]>();

    public static string Localize(string key)
    {
        if(dictionary.Count == 0) Read();
        if (!dictionary.ContainsKey(key)) Debug.LogError("There is no such key " + key);
        return dictionary[key][0]; // change zero to language id
    }

    private static void Read()
    {
        var textAssets = Resources.LoadAll<TextAsset>("Localization");
        foreach (var textAsset in textAssets)
        {
            string[] lines = textAsset.text.Split("\n");
            foreach (var line in lines)
            {
                var value = ReadLine(line);
                string key = value[0];
                value.RemoveAt(0);
                dictionary.Add(key, value.ToArray());
            }
        }
    }

    private static List<string> ReadLine(string line)
    {
        line = line.Trim();
        List<string> values = new List<string>();
        StringBuilder value = new StringBuilder();
        bool insideQuotes = false;
        for (int i = 0; i < line.Length; i++)
        {
            char c = line[i];
            if (c == ',' && !insideQuotes) { values.Add(value.ToString()); value.Clear(); continue; }
            if (c == '"' && line[i - 1] == ',') { insideQuotes = true; continue; }
            if (insideQuotes && c == '"' && i + 1 < line.Length && line[i + 1] == ',') { insideQuotes = false; continue; }
            if (insideQuotes && c == '"' && i + 1 == line.Length) { break; }
            if (c == '"' && line[i - 1] == '"' && line[i + 1] == '"') continue;
            value.Append(c);
        }
        values.Add(value.ToString());
        return values;
    }
}