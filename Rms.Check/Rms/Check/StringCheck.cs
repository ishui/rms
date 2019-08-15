namespace Rms.Check
{
    using System;
    using System.Text.RegularExpressions;

    public sealed class StringCheck
    {
        public static bool IsDateTime(string inputString)
        {
            try
            {
                DateTime time = DateTime.Parse(inputString);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsInt(string inputString)
        {
            try
            {
                int num = int.Parse(inputString);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsNumber(string inputString)
        {
            try
            {
                double num = double.Parse(inputString);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsPositiveLongInt(string inputString)
        {
            string pattern = "^[0-9]*[1-9][0-9]*$";
            return Regex.IsMatch(inputString, pattern);
        }
    }
}

