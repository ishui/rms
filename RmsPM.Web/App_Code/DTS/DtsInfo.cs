using System;
using System.Collections;
using System.ComponentModel;
using System.Data;

namespace RmsPM.Web.DTS
{
	/// <summary>
	/// ���ݵ��뵼��
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
				s = string.Format("������ֹ���� {0} ����¼����ִ�� {1} ��", Count, ExecCount);
			}
			else 
			{
				s = string.Format("������ɣ��� {0} ����¼", Count);
			}

			if (ErrCount == 0) 
			{
				s = s + "��ȫ���ɹ�";
			}
			else 
			{
				s = s + string.Format("�����г��� {0} ��", ErrCount);
			}

			return s;
		}

		/// <summary>
		/// �Ƿ����
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
