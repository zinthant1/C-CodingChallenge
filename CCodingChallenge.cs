using System;
using System.Collections.Generic;
using System.Text;

public class OldPhonePadSolution
{
    public static string OldPhonePad(string input)
    {
        // Dictionary mapping each key to its corresponding characters.
        Dictionary<char, string> keypad = new Dictionary<char, string>()
        {
            {'2', "ABC"}, {'3', "DEF"}, {'4', "GHI"}, {'5', "JKL"},
            {'6', "MNO"}, {'7', "PQRS"}, {'8', "TUV"}, {'9', "WXYZ"}
        };
        
        StringBuilder result = new StringBuilder();
        char lastDigit = '\0';
        int pressCount = 0;

        foreach (char c in input)
        {
            if (c == '#')
            {
                // Send command, finish processing
                break;
            }
            else if (c == '*')
            {
                // Backspace, remove last character
                if (result.Length > 0)
                {
                    result.Remove(result.Length - 1, 1);
                }
            }
            else if (c == ' ')
            {
                // Pause, reset for next character from the same key
                if (lastDigit != '\0')
                {
                    int index = (pressCount - 1) % keypad[lastDigit].Length;
                    result.Append(keypad[lastDigit][index]);
                }
                lastDigit = '\0';
                pressCount = 0;
            }
            else if (keypad.ContainsKey(c))
            {
                // Handling digit press
                if (c == lastDigit)
                {
                    // Same key pressed again
                    pressCount++;
                }
                else
                {
                    // New key pressed
                    if (lastDigit != '\0')
                    {
                        int index = (pressCount - 1) % keypad[lastDigit].Length;
                        result.Append(keypad[lastDigit][index]);
                    }
                    lastDigit = c;
                    pressCount = 1;
                }
            }
        }

        // Final check for any last input before '#'
        if (lastDigit != '\0')
        {
            int index = (pressCount - 1) % keypad[lastDigit].Length;
            result.Append(keypad[lastDigit][index]);
        }

        return result.ToString();
    }

    public static void Main(string[] args)
    {
        Console.WriteLine(OldPhonePad("33#")); // Output: E
        Console.WriteLine(OldPhonePad("227*#")); // Output: B
        Console.WriteLine(OldPhonePad("4433555 555666#")); // Output: HELLO
        Console.WriteLine(OldPhonePad("8 88777444666*664#")); // Output: TURING
    }
}

