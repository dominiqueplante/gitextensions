using System;
using System.Drawing;

namespace GitCommands
{
    public static class FontParser
    {
        public static string AsString(this Font value)
        {
            return String.Format("{0};{1}", value.FontFamily.Name, value.Size);
        }

        public static Font Parse(this string value, Font defaultValue)
        {
            if (value == null)
                return defaultValue;

            string[] parts = value.Split(';');

            if (parts.Length < 2)
                return defaultValue;

            try
            {
                return new Font(parts[0], Single.Parse(parts[1]));
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }
    }
}