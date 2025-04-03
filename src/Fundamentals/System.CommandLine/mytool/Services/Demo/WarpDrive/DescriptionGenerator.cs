namespace MyTool.Services.Demo.WarpDrive;

/// <see href="https://github.com/spectreconsole/examples/tree/main/examples/Console/Progress"/>
public static class DescriptionGenerator
{
    private static readonly string[] Verbs = ["Downloading", "Rerouting", "Retriculating", "Collapsing", "Folding", "Solving", "Colliding", "Measuring"];
    private static readonly string[] Nouns = ["internet", "splines", "space", "capacitators", "quarks", "algorithms", "data structures", "spacetime"];

    private static readonly Random Random = new Random(DateTime.Now.Millisecond);
    private static readonly HashSet<string> Used = [];

    public static bool TryGenerate(out string name)
    {
        var iterations = 0;
        while (iterations < 25)
        {
            name = Generate();
            if (Used.Add(name))
                return true;

            iterations++;
        }

        name = Generate();
        return false;
    }

    public static string Generate()
    {
        return $"{Verbs[Random.Next(0, Verbs.Length)]} {Nouns[Random.Next(0, Nouns.Length)]}";
    }
}