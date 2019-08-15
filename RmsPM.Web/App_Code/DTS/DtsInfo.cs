using System;
using System.Collections;
using System.ComponentModel;
using System.Data;

namespace RmsPM.Web.DTS
{
	/// <summary>
	/// 数据导入导出
	/// </summary>
	public class DtsInfo
	{
		private int m_ErrCount;
		private int m_Count;
		private int m_CurrentIndex;
		private DataTable m_DataSource;
		private Hashtable m_ErrMess;

		public int CurrentIndex 
		{
			get {return m_CurrentIndex;}
			set {m_CurrentIndex = value;}
		}

		public int Count 
		{
			get {return m_Count;}
		}

		public int ErrCount 
		{
			get {return m_ErrCount;}
		}

		public int SuccCount 
		{
			get {return CurrentIndex + 1 - ErrCount;}
		}

		public int ExecCount 
		{
			get {return CurrentIndex + 1;}
		}

		public DataTable DataSource 
		{
			get {return m_DataSource;}
		}

		public string GetErr(int i)
		{
			return m_ErrMess[i].ToString();
		}

		public void AddErr(string ErrMess) 
		{
			m_ErrMess.Add(m_ErrCount, ErrMess);
			m_ErrCount = m_ErrCount + 1;
		}

		public DtsInfo(DataTable tb) 
		{
			m_DataSource = tb;
			m_CurrentIndex = -1;
			m_Count = tb.Rows.Count;
			m_ErrCount = 0;
			m_CurrentIndex = -1;
			m_ErrMess = new Hashtable();
		}

		public string GetResult()
		{
			string s = "";

			if (ExecCount < Count) 
			{
				s = string.Format("导入终止，共 {0} 条记录，已执行 {1} 条", Count, ExecCount);
			}
			else 
			{
				s = string.Format("导入完成，共 {0} 条记录", Count);
			}

			if (ErrCount == 0) 
			{
				s = s + "，全部成功";
			}
			else 
			{
				s = s + string.Format("，其中出错 {0} 条", ErrCount);
			}

			return s;
		}

		/// <summary>
		/// 是否完成
		/// </summary>
		public bool EOF 
		{
			get
			{
				if (CurrentIndex >= Count - 1)
					return true;
				else
					return false;
			}
		}
	}
}
