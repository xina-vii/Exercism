using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public static class Tournament
{
    private class TeamStats
    {
        public int Won, Drawn, Lost;
        public int Played => Won + Drawn + Lost;
        public int Points => Won * 3 + Drawn;
    }

    public static void Tally(Stream inStream, Stream outStream)
    {
        var teams = new Dictionary<string, TeamStats>();

        using (var reader = new StreamReader(inStream, Encoding.UTF8, true, 1024, leaveOpen: true))
        {
            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var parts = line.Split(';');
                if (parts.Length != 3)
                    continue;

                string first = parts[0], second = parts[1], result = parts[2];

                var a = GetOrAdd(teams, first);
                var b = GetOrAdd(teams, second);

                switch (result)
                {
                    case "win":
                        a.Won++; b.Lost++;
                        break;
                    case "loss":
                        a.Lost++; b.Won++;
                        break;
                    case "draw":
                        a.Drawn++; b.Drawn++;
                        break;
                }
            }
        }

        var lines = new List<string>
        {
            Format("Team", "MP", "W", "D", "L", "P")
        };

        foreach (var kv in teams
                     .OrderByDescending(t => t.Value.Points)
                     .ThenBy(t => t.Key, StringComparer.Ordinal))
        {
            var s = kv.Value;
            lines.Add(Format(kv.Key, s.Played.ToString(), s.Won.ToString(),
                             s.Drawn.ToString(), s.Lost.ToString(), s.Points.ToString()));
        }

        // Без завершального переносу рядка
        var bytes = new UTF8Encoding(false).GetBytes(string.Join("\n", lines));
        outStream.Write(bytes, 0, bytes.Length);
        outStream.Flush();
    }

    private static TeamStats GetOrAdd(Dictionary<string, TeamStats> teams, string name)
    {
        if (!teams.TryGetValue(name, out var stats))
        {
            stats = new TeamStats();
            teams[name] = stats;
        }
        return stats;
    }

    private static string Format(string team, string mp, string w, string d, string l, string p)
    {
        return $"{team,-31}| {mp,2} | {w,2} | {d,2} | {l,2} | {p,2}";
    }
}