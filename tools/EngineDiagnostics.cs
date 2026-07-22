using System;
using System.Linq;
using Cards.Core.Engine;
using Cards.Core.Engine.Profiles;
using Cards.Core.Data;

class EngineDiagnostics
{
    static int Main()
    {
        var engine = new CardGameEngine();
        var supported = engine.SupportedGames.OrderBy(n => n).ToArray();
        Console.WriteLine($"Registered rule engines: {supported.Length}");
        Console.WriteLine("First 40 registered games:");
        for (int i = 0; i < Math.Min(40, supported.Length); i++)
            Console.WriteLine(supported[i]);

        string[] samples = new[] { "Accordion", "Aces Up", "Aces Up Relaxed", "Assembly" };
        foreach (var s in samples)
            Console.WriteLine($"Has '{s}': {supported.Contains(s, StringComparer.OrdinalIgnoreCase)}");

        return 0;
    }
}
