namespace Rms.ORMap
{
    using System;
    using System.Collections;
    using System.Text;

    public class UserDefinedFormatKeyStringBuilder : IKeyStringBuilder
    {
        private string m_FormatStringName;
        private Hashtable segments;

        public UserDefinedFormatKeyStringBuilder()
        {
            this.m_FormatStringName = "";
            this.segments = new Hashtable(20);
        }

        public UserDefinedFormatKeyStringBuilder(string formatStringName)
        {
            this.m_FormatStringName = "";
            this.segments = new Hashtable(20);
            this.m_FormatStringName = formatStringName;
            this.ReGetSegments();
            this.ReGetNewId();
        }

        public string GetKeyString()
        {
            if (this.segments.Count == 0)
            {
                return "";
            }
            StringBuilder builder = new StringBuilder();
            IDictionaryEnumerator enumerator = this.segments.GetEnumerator();
            while (enumerator.MoveNext())
            {
                FormatStringSegment segment = (FormatStringSegment) enumerator.Value;
                switch (segment.Type)
                {
                    case FormatStringSegmentType.StaticString:
                    {
                        builder.Append(segment.StaticString);
                        continue;
                    }
                    case FormatStringSegmentType.Year:
                    {
                        builder.Append(DateTime.Now.Year.ToString());
                        continue;
                    }
                    case FormatStringSegmentType.Month:
                    {
                        builder.Append(DateTime.Now.Month.ToString());
                        continue;
                    }
                    case FormatStringSegmentType.Day:
                    {
                        builder.Append(DateTime.Now.Date.ToShortDateString());
                        continue;
                    }
                    case FormatStringSegmentType.Hour:
                    {
                        builder.Append(DateTime.Now.Hour.ToString());
                        continue;
                    }
                    case FormatStringSegmentType.Minute:
                    {
                        builder.Append(DateTime.Now.Minute.ToString());
                        continue;
                    }
                    case FormatStringSegmentType.Second:
                    {
                        builder.Append(DateTime.Now.Second.ToString());
                        continue;
                    }
                    case FormatStringSegmentType.IncreaseInteger:
                    {
                        if (!segment.IsFillEmptyChar)
                        {
                            break;
                        }
                        builder.Append(segment.CurrentID.ToString().PadLeft(segment.Lenth, segment.FillChar));
                        continue;
                    }
                    default:
                    {
                        continue;
                    }
                }
                builder.Append(segment.CurrentID);
            }
            return builder.ToString();
        }

        public void ReGetNewId()
        {
        }

        public void ReGetSegments()
        {
        }

        private string FormatStringName
        {
            get
            {
                return this.m_FormatStringName;
            }
            set
            {
                this.m_FormatStringName = value;
            }
        }
    }
}

