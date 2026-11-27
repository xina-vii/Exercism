using System;
using System.Linq;

public class RailFenceCipher
{
    private readonly int _rails;

    public RailFenceCipher(int rails)
    {
        if (rails < 1)
            throw new ArgumentOutOfRangeException(nameof(rails), "Кількість рейок має бути додатною");

        _rails = rails;
    }

    public string Encode(string input)
    {
        var order = Order(input.Length);
        var result = new char[input.Length];
        for (int i = 0; i < order.Length; i++)
            result[i] = input[order[i]];
        return new string(result);
    }

    public string Decode(string input)
    {
        var order = Order(input.Length);
        var result = new char[input.Length];
        for (int i = 0; i < order.Length; i++)
            result[order[i]] = input[i];
        return new string(result);
    }

    // Номер рейки для кожної позиції (зигзаг)
    private int[] RailPattern(int length)
    {
        var pattern = new int[length];
        if (_rails == 1) return pattern;

        int rail = 0, step = 1;
        for (int i = 0; i < length; i++)
        {
            pattern[i] = rail;
            if (rail == 0) step = 1;
            else if (rail == _rails - 1) step = -1;
            rail += step;
        }
        return pattern;
    }

    // Індекси вихідного тексту в порядку читання по рейках
    private int[] Order(int length)
    {
        var pattern = RailPattern(length);
        return Enumerable.Range(0, length)
                         .OrderBy(i => pattern[i])
                         .ThenBy(i => i)
                         .ToArray();
    }
}