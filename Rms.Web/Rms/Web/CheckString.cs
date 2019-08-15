namespace Rms.Web
{
    using System;
    using System.Text;

    public class CheckString
    {
        public static string ClearInputString(string m_strString)
        {
            StringBuilder builder = new StringBuilder();
            if ((m_strString != null) && (m_strString != string.Empty))
            {
                m_strString = m_strString.Trim();
                for (int i = 0; i < m_strString.Length; i++)
                {
                    switch (m_strString[i])
                    {
                        case '<':
                        {
                            builder.Append("&lt;");
                            continue;
                        }
                        case '>':
                        {
                            builder.Append("&gt;");
                            continue;
                        }
                        case '"':
                        {
                            builder.Append("&quot;");
                            continue;
                        }
                    }
                    builder.Append(m_strString[i]);
                }
                builder.Replace("'", " ");
            }
            return builder.ToString();
        }

        public static string ClearJavaScriptString(string m_strString)
        {
            StringBuilder builder = new StringBuilder();
            if ((m_strString != null) && (m_strString != string.Empty))
            {
                m_strString = m_strString.Trim();
                for (int i = 0; i < m_strString.Length; i++)
                {
                    switch (m_strString[i])
                    {
                        case '"':
                            builder.Append("\\\"");
                            break;

                        case '\\':
                            builder.Append(@"\\");
                            break;

                        default:
                            builder.Append(m_strString[i]);
                            break;
                    }
                }
                builder.Replace(Environment.NewLine, @"\n");
            }
            return builder.ToString();
        }
    }
}

