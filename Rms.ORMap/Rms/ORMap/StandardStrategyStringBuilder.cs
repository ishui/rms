namespace Rms.ORMap
{
    using System;
    using System.Collections;

    /// <summary>
    /// SQL语句生成
    /// </summary>
    public sealed class StandardStrategyStringBuilder
    {
        private static string AddLikeChar(string val, string type)
        {
            string text = val;
            string str = type.ToUpper();
            if (str == null)
            {
                return text;
            }
            str = string.IsInterned(str);
            if (str == "L")
            {
                return (text + "%");
            }
            if (str == "R")
            {
                return ("%" + text);
            }
            if (str != "F")
            {
                return text;
            }
            return ("%" + text + "%");
        }

        /// <summary>
        /// 判断月份是否相等
        /// </summary>
        /// <param name="strategy"></param>
        /// <returns></returns>
        public static string BuildDateTimeEqualMonthString(Strategy strategy)
        {
            return string.Format(" Year({0})={1}  and  Month({0})={2} ", strategy.RelationFieldName, strategy.GetParameter(0), strategy.GetParameter(1));
        }

        /// <summary>
        /// 判断日期是否相等
        /// </summary>
        /// <param name="strategy"></param>
        /// <returns></returns>
        public static string BuildDateTimeEqualOnlyDateString(Strategy strategy)
        {
            return (" convert(datetime,convert(varchar(10)," + strategy.RelationFieldName + ",121)) = '" + strategy.GetParameter(0) + "' ");
        }

        /// <summary>
        /// 判断时间是否相等
        /// </summary>
        /// <param name="strategy"></param>
        /// <returns></returns>
        public static string BuildDateTimeEqualString(Strategy strategy)
        {
            return (" " + strategy.RelationFieldName + " = '" + strategy.GetParameter(0) + "' ");
        }

        /// <summary>
        /// 判断年份是否相等
        /// </summary>
        /// <param name="strategy"></param>
        /// <returns></returns>
        public static string BuildDateTimeEqualYearString(Strategy strategy)
        {
            return (" Year(" + strategy.RelationFieldName + ") = " + strategy.GetParameter(0) + " ");
        }

        /// <summary>
        /// 日期搜索
        /// </summary>
        /// <param name="strategy"></param>
        /// <returns></returns>
        public static string BuildDateTimeRangeOnlyDateString(Strategy strategy)
        {
            string text = strategy.GetParameter(0).Trim();
            string text2 = strategy.GetParameter(1).Trim();
            string text3 = "";
            if ((text == "") && (text2 == ""))
            {
                return " 1=1 ";
            }
            if ((text != "") && (text2 != ""))
            {
                return (" convert(datetime,convert(varchar(10)," + strategy.RelationFieldName + ",121)) BETWEEN '" + text + "' and  '" + text2 + "' ");
            }
            if (text != "")
            {
                return (" convert(datetime,convert(varchar(10)," + strategy.RelationFieldName + ",121)) >= '" + text + "' ");
            }
            if (text2 != "")
            {
                text3 = " convert(datetime,convert(varchar(10)," + strategy.RelationFieldName + ",121)) <= '" + text2 + "' ";
            }
            return text3;
        }

        /// <summary>
        /// 时间搜索
        /// </summary>
        /// <param name="strategy"></param>
        /// <returns></returns>
        public static string BuildDateTimeRangeString(Strategy strategy)
        {
            string text = strategy.GetParameter(0).Trim();
            string text2 = strategy.GetParameter(1).Trim();
            string text3 = "";
            if ((text == "") && (text2 == ""))
            {
                return " 1=1 ";
            }
            if ((text != "") && (text2 != ""))
            {
                return (" " + strategy.RelationFieldName + " BETWEEN '" + text + "' and  '" + text2 + "' ");
            }
            if (text != "")
            {
                return (" " + strategy.RelationFieldName + " >= '" + text + "' ");
            }
            if (text2 != "")
            {
                text3 = " " + strategy.RelationFieldName + " <= '" + text2 + "' ";
            }
            return text3;
        }

        /// <summary>
        /// 浮点数搜索
        /// </summary>
        /// <param name="strategy"></param>
        /// <returns></returns>
        public static string BuildFloatRangeString(Strategy strategy)
        {
            string parameter = strategy.GetParameter(0);
            string text2 = strategy.GetParameter(1);
            if ((parameter == "") && (text2 == ""))
            {
                return " 1=1 ";
            }
            if (parameter == "")
            {
                return (" " + strategy.RelationFieldName + " <= " + text2 + " ");
            }
            if (text2 == "")
            {
                return (" " + strategy.RelationFieldName + " >= " + parameter + " ");
            }
            return (" " + strategy.RelationFieldName + " BETWEEN " + parameter + " and  " + text2 + " ");
        }

        /// <summary>
        /// 整数相等
        /// </summary>
        /// <param name="strategy"></param>
        /// <returns></returns>
        public static string BuildIntegerEqualString(Strategy strategy)
        {
            return (" " + strategy.RelationFieldName + " = " + strategy.GetParameter(0) + " ");
        }

        /// <summary>
        /// 整数搜索
        /// </summary>
        /// <param name="strategy"></param>
        /// <returns></returns>
        public static string BuildIntegerRangeString(Strategy strategy)
        {
            string parameter = strategy.GetParameter(0);
            string text2 = strategy.GetParameter(1);
            if ((parameter == "") && (text2 == ""))
            {
                return " 1=1 ";
            }
            if (parameter == "")
            {
                return (" " + strategy.RelationFieldName + " <= " + text2 + " ");
            }
            if (text2 == "")
            {
                return (" " + strategy.RelationFieldName + " >= " + parameter + " ");
            }
            return (" " + strategy.RelationFieldName + " BETWEEN " + parameter + " and  " + text2 + " ");
        }

        /// <summary>
        /// 数字在字符串中
        /// </summary>
        /// <param name="strategy"></param>
        /// <returns></returns>
        public static string BuildNumberInString(Strategy strategy)
        {
            string parameter = strategy.GetParameter(0);
            return string.Format(" {0} in ( {1} ) ", strategy.RelationFieldName, parameter);
        }

        /// <summary>
        /// 创建SQL查询条件
        /// </summary>
        /// <param name="strategy"></param>
        /// <returns></returns>
        public static string BuildStrategyString(Strategy strategy)
        {
            switch (strategy.Type)
            {
                case StrategyType.StringEqual:
                    return BuildStringEqualString(strategy);

                case StrategyType.StringEqualEx:
                    return BuildStringEqualStringEx(strategy);

                case StrategyType.IntegerEqual:
                    return BuildIntegerEqualString(strategy);

                case StrategyType.DateTimeEqual:
                    return BuildDateTimeEqualString(strategy);

                case StrategyType.DateTimeEqualOnlyDate:
                    return BuildDateTimeEqualOnlyDateString(strategy);

                case StrategyType.DateTimeEqualMonth:
                    return BuildDateTimeEqualMonthString(strategy);

                case StrategyType.DateTimeEqualYear:
                    return BuildDateTimeEqualYearString(strategy);

                case StrategyType.IntegerRange:
                    return BuildIntegerRangeString(strategy);

                case StrategyType.FloatRange:
                    return BuildFloatRangeString(strategy);

                case StrategyType.DateTimeRange:
                    return BuildDateTimeRangeString(strategy);

                case StrategyType.DateTimeRangeOnlyDate:
                    return BuildDateTimeRangeOnlyDateString(strategy);

                case StrategyType.StringRange:
                    return BuildStringRangeString(strategy);

                case StrategyType.StringIn:
                    return BuildStringInString(strategy);

                case StrategyType.StringLike:
                    return BuildStringLikeString(strategy);

                case StrategyType.StringLikeEx0:
                    return BuildStringLikeEx0String(strategy);

                case StrategyType.StringLikeEx1:
                    return BuildStringLikeEx1String(strategy);

                case StrategyType.NumberIn:
                    return BuildNumberInString(strategy);
            }
            return "";
        }

        /// <summary>
        /// 判断字符串相等条件
        /// </summary>
        /// <param name="strategy"></param>
        /// <returns></returns>
        public static string BuildStringEqualString(Strategy strategy)
        {
            return (" " + strategy.RelationFieldName + " =  '" + strategy.GetParameter(0) + "' ");
        }

        public static string BuildStringEqualStringEx(Strategy strategy)
        {
            string parameter = strategy.GetParameter(0);
            string relationFieldName = strategy.RelationFieldName;
            if (parameter == BlankType._Blank.ToString("F"))
            {
                return string.Format(" len( isnull({0},'')) = 0  ", relationFieldName);
            }
            if (parameter == BlankType._Not_Blank.ToString("F"))
            {
                return string.Format(" len( isnull({0},'')) > 0  ", relationFieldName);
            }
            return string.Format(" {1}  =  '{0}' ", parameter, relationFieldName);
        }

        /// <summary>
        /// 判断参数在某字符串里
        /// </summary>
        /// <param name="strategy"></param>
        /// <returns></returns>
        public static string BuildStringInString(Strategy strategy)
        {
            string parameter = strategy.GetParameter(0);
            if (parameter.Length == 0)
            {
                return " 1=1 ";
            }
            string[] textArray = parameter.Split(new char[] { ',', ' ' });
            int length = textArray.Length;
            string text2 = "";
            string relationFieldName = strategy.RelationFieldName;
            for (int i = 0; i < length; i++)
            {
                string text4 = textArray[i];
                if (text2 != "")
                {
                    text2 = text2 + ", ";
                }
                text2 = text2 + string.Format("'{1}'", relationFieldName, text4);
            }
            return (" " + strategy.RelationFieldName + " in  (" + text2 + ") ");
        }

        /// <summary>
        /// 判断参数匹配某字符串
        /// </summary>
        /// <param name="strategy"></param>
        /// <returns></returns>
        public static string BuildStringLikeEx0String(Strategy strategy)
        {
            if (strategy.GetParameterCount() == 0)
            {
                return " 1=1 ";
            }
            string text = "";
            string relationFieldName = strategy.RelationFieldName;
            IEnumerator parameterEnumerator = strategy.GetParameterEnumerator();
            while (parameterEnumerator.MoveNext())
            {
                string current = (string) parameterEnumerator.Current;
                if (text != "")
                {
                    text = text + " or ";
                }
                text = text + string.Format(" {0} like ('{1}' ) ", relationFieldName, current);
            }
            return ("( " + text + " )");
        }

        public static string BuildStringLikeEx1String(Strategy strategy)
        {
            string parameter = strategy.GetParameter(0);
            if (parameter.Length == 0)
            {
                return " 1=1 ";
            }
            string[] textArray = parameter.Split(new char[] { ',', ' ' });
            int length = textArray.Length;
            string text2 = "";
            string relationFieldName = strategy.RelationFieldName;
            string type = "";
            if (strategy.GetParameterCount() > 1)
            {
                type = strategy.GetParameter(1);
            }
            for (int i = 0; i < length; i++)
            {
                string val = textArray[i];
                if (text2 != "")
                {
                    text2 = text2 + " or ";
                }
                val = AddLikeChar(val, type);
                text2 = text2 + string.Format(" {0} like ('{1}' ) ", relationFieldName, val);
            }
            return ("( " + text2 + " )");
        }

        /// <summary>
        /// 判断参数匹配某字符串
        /// </summary>
        /// <param name="strategy"></param>
        /// <returns></returns>
        public static string BuildStringLikeString(Strategy strategy)
        {
            return (" " + strategy.RelationFieldName + " like ('" + strategy.GetParameter(0) + "') ");
        }

        /// <summary>
        /// 判断参数在某字符串
        /// </summary>
        /// <param name="strategy"></param>
        /// <returns></returns>
        public static string BuildStringRangeString(Strategy strategy)
        {
            return (" " + strategy.RelationFieldName + " in  (" + strategy.GetParameter(0) + ") ");
        }
    }
}

