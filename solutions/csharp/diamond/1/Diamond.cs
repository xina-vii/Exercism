using System;
using System.Collections.Generic;

public static class Diamond
{
    public static string Make(char target)
    {
        if (target < 'A' || target > 'Z')
            throw new ArgumentOutOfRangeException(nameof(target), "Літера має бути від 'A' до 'Z'");

        int n = target - 'A';        // номер цільової літери
        int width = 2 * n + 1;       // ширина = висота

        var rows = new List<string>(width);

        // Верхня половина разом із середнім рядком
        for (int i = 0; i <= n; i++)
            rows.Add(MakeRow((char)('A' + i), i, n, width));

        // Нижня половина: дзеркальне відображення верхньої без середнього рядка
        for (int i = n - 1; i >= 0; i--)
            rows.Add(rows[i]);

        return string.Join("\n", rows);
    }

    private static string MakeRow(char letter, int index, int n, int width)
    {
        var row = new char[width];
        Array.Fill(row, ' ');

        // Дві симетричні позиції для літери (для 'A' вони збігаються)
        row[n - index] = letter;
        row[n + index] = letter;

        return new string(row);
    }
}