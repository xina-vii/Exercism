using System;
using System.Text;

public static class RotationalCipher
{
    public static string Rotate(string text, int shiftKey)
    {
        // Нормалізуємо ключ у діапазон 0..25 (враховує 26 та від'ємні значення)
        int shift = ((shiftKey % 26) + 26) % 26;

        var result = new StringBuilder(text.Length);
        foreach (char c in text)
        {
            if (c >= 'a' && c <= 'z')
                result.Append((char)('a' + (c - 'a' + shift) % 26));
            else if (c >= 'A' && c <= 'Z')
                result.Append((char)('A' + (c - 'A' + shift) % 26));
            else
                result.Append(c); // пробіли, цифри, розділові знаки не змінюємо
        }
        return result.ToString();
    }
}