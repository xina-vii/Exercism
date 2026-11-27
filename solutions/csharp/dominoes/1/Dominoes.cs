using System;
using System.Collections.Generic;
using System.Linq;

public static class Dominoes
{
    public static bool CanChain(IEnumerable<(int, int)> dominoes)
    {
        var list = dominoes.ToList();

        // Порожній набір утворює порожній (валідний) ланцюжок
        if (list.Count == 0)
            return true;

        var degree = new Dictionary<int, int>();
        var parent = new Dictionary<int, int>();

        foreach (var (a, b) in list)
        {
            // Петля [a|a] додає 2 до степеня вершини, що теж дає парне число
            degree[a] = degree.GetValueOrDefault(a) + 1;
            degree[b] = degree.GetValueOrDefault(b) + 1;

            parent.TryAdd(a, a);
            parent.TryAdd(b, b);
            Union(parent, a, b);
        }

        // Умова 1: усі степені вершин парні
        if (degree.Values.Any(d => d % 2 != 0))
            return false;

        // Умова 2: усі вершини з ребрами належать одній компоненті
        int root = Find(parent, list[0].Item1);
        return parent.Keys.All(v => Find(parent, v) == root);
    }

    private static int Find(Dictionary<int, int> parent, int x)
    {
        while (parent[x] != x)
        {
            parent[x] = parent[parent[x]]; // стиснення шляху
            x = parent[x];
        }
        return x;
    }

    private static void Union(Dictionary<int, int> parent, int a, int b)
    {
        int ra = Find(parent, a);
        int rb = Find(parent, b);
        if (ra != rb)
            parent[ra] = rb;
    }
}