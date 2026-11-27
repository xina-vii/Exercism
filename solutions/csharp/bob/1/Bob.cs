using System;
using System.Linq;

public static class Bob
{
    public static string Response(string statement)
    {
        string text = statement.Trim();

        if (text.Length == 0)
            return "Fine. Be that way!";

        // Крик: є хоча б одна літера, і жодної малої
        bool isYelling = text.Any(char.IsLetter) && !text.Any(char.IsLower);
        bool isQuestion = text.EndsWith('?');

        if (isYelling && isQuestion)
            return "Calm down, I know what I'm doing!";
        if (isYelling)
            return "Whoa, chill out!";
        if (isQuestion)
            return "Sure.";

        return "Whatever.";
    }
}