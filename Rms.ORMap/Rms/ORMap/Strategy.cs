namespace Rms.ORMap
{
    using System;
    using System.Collections;

    public class Strategy
    {
        private object m_Name;
        private string m_RelationFieldName;
        private StrategyType m_Type;
        private ArrayList parameters;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name"></param>
        public Strategy(object name)
        {
            this.parameters = new ArrayList();
            this.m_Name = name;
            this.m_Type = StrategyType.StringEqual;
            this.m_RelationFieldName = "";
        }

        /// <summary>
        /// 数组，2个参数
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pas">数组</param>
        public Strategy(object name, ArrayList pas)
        {
            this.parameters = new ArrayList();
            this.m_Name = name;
            this.m_Type = StrategyType.StringEqual;
            this.m_RelationFieldName = "";
            this.parameters = pas;
        }

        /// <summary>
        /// 字符串，2个参数
        /// </summary>
        /// <param name="name"></param>
        /// <param name="param0">字符串</param>
        public Strategy(object name, string param0)
        {
            this.parameters = new ArrayList();
            this.m_Name = name;
            this.m_Type = StrategyType.StringEqual;
            this.m_RelationFieldName = "";
            this.parameters.Add(param0);
        }

        /// <summary>
        /// 字符串，3个参数
        /// </summary>
        /// <param name="name"></param>
        /// <param name="param0"></param>
        /// <param name="param1"></param>
        public Strategy(object name, string param0, string param1)
        {
            this.parameters = new ArrayList();
            this.m_Name = name;
            this.m_Type = StrategyType.StringEqual;
            this.m_RelationFieldName = "";
            this.parameters.Add(param0);
            this.parameters.Add(param1);
        }

        /// <summary>
        /// 枚组、字符串，3个参数
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="relationFieldName"></param>
        public Strategy(object name, StrategyType type, string relationFieldName)
        {
            this.parameters = new ArrayList();
            this.m_Name = name;
            this.m_Type = type;
            this.m_RelationFieldName = relationFieldName;
        }

        /// <summary>
        /// 枚组、字符串、数组，4个参数
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="relationFieldName"></param>
        /// <param name="pas"></param>
        public Strategy(object name, StrategyType type, string relationFieldName, ArrayList pas)
        {
            this.parameters = new ArrayList();
            this.m_Name = name;
            this.m_Type = type;
            this.m_RelationFieldName = relationFieldName;
            this.parameters = pas;
        }

        /// <summary>
        /// 枚组、字符串，4个参数
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="relationFieldName"></param>
        /// <param name="param0"></param>
        public Strategy(object name, StrategyType type, string relationFieldName, string param0)
        {
            this.parameters = new ArrayList();
            this.m_Name = name;
            this.m_Type = type;
            this.m_RelationFieldName = relationFieldName;
            this.parameters.Add(param0);
        }

        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="parameterString"></param>
        public virtual void AddParameter(string parameterString)
        {
            this.parameters.Add(parameterString);
        }

        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public virtual string GetParameter(int index)
        {
            if ((index < 0) || (index > this.parameters.Count))
            {
                throw new ApplicationException("索引越界");
            }
            if (((this.Type != StrategyType.StringIn) && (this.Type != StrategyType.Other)) && ((this.Type != StrategyType.StringRange) && (this.Type != StrategyType.NumberIn)))
            {
                return ((string) this.parameters[index]).Replace("'", "''");
            }
            return (string) this.parameters[index];
        }

        /// <summary>
        /// 获取参数数量
        /// </summary>
        /// <returns></returns>
        public int GetParameterCount()
        {
            return this.parameters.Count;
        }

        public IEnumerator GetParameterEnumerator()
        {
            return this.parameters.GetEnumerator();
        }

        public static string ReplaceSingleQuote(string s)
        {
            return s.Replace("'", "''");
        }

        public virtual void SetParameter(int index, string param)
        {
            this.parameters[index] = param;
        }

        public object Name
        {
            get
            {
                return this.m_Name;
            }
        }

        public string RelationFieldName
        {
            get
            {
                return this.m_RelationFieldName;
            }
            set
            {
                this.m_RelationFieldName = value;
            }
        }

        public StrategyType Type
        {
            get
            {
                return this.m_Type;
            }
            set
            {
                this.m_Type = value;
            }
        }
    }
}

