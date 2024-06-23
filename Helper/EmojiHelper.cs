using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mist.Helper
{
    internal class EmojiHelper
    {
        public static string ConvertUnicodeToEmoji(string unicodeString)
        {
            // Split the Unicode code points
            string[] codePoints = unicodeString.Split(' ');

            // Initialize a string to hold the emoji
            string emoji = string.Empty;

            // Convert each Unicode code point to a char or surrogate pair
            foreach (var codePoint in codePoints)
            {
                // Remove "U+" prefix and convert the hexadecimal string to an integer
                int value = int.Parse(codePoint.Substring(2), NumberStyles.HexNumber);

                if (value <= 0xFFFF)
                {
                    // BMP character (no surrogate pair)
                    emoji += (char)value;
                }
                else
                {
                    // Surrogate pair
                    value -= 0x10000;
                    emoji += (char)((value >> 10) + 0xD800);
                    emoji += (char)((value & 0x3FF) + 0xDC00);
                }
            }

            return emoji;
        }
    }
}
