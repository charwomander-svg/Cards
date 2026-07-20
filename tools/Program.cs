using System;
using System.IO;
using System.Linq;
using Cards.Core.Data;
using Cards.Core.Engine.Profiles;

class Program
{
    static int Main()
    {
        string outDir = Path.Combine(Environment.CurrentDirectory, "artifacts");
        Directory.CreateDirectory(outDir);
        string outPath = Path.Combine(outDir, "catalog-mapping-report.csv");

        using var writer = new StreamWriter(outPath, false);
        writer.WriteLine("GameName,ProfileExists,MappedRuleType");

        foreach (var game in CardGameCatalog.Games)
        {
            bool hasProfile = RuleProfileCatalog.ByGameName.ContainsKey(game.Name);
            var profile = hasProfile ? RuleProfileCatalog.ByGameName[game.Name] : new RuleProfile(game.Name, RuleFamily.Klondike, 1, 1);
            string typeName;
            try
            {
                var rules = RuleProfileCatalog.CreateRules(profile);
                typeName = rules.GetType().Name;
            }
            catch (Exception ex)
            {
                typeName = "ERROR:" + ex.GetType().Name;
            }

            writer.WriteLine($"\"{game.Name}\",{hasProfile},{typeName}");
        }

        Console.WriteLine($"Report written to {outPath}");
        return 0;
    }
}
