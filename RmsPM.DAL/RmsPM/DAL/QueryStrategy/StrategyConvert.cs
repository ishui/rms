namespace RmsPM.DAL.QueryStrategy
{
    using System;

    public class StrategyConvert
    {
        public static string BuildInStr(string val)
        {
            string text3;
            try
            {
                string text = "";
                string[] textArray = val.Replace("'", "").Split(",".ToCharArray());
                for (int i = 0; i < textArray.Length; i++)
                {
                    if (text != "")
                    {
                        text = text + ",";
                    }
                    text = text + "'" + textArray[i] + "'";
                }
                text3 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text3;
        }
    }
}

