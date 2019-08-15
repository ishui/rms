using System;
using RmsPM.Web;
using System.Data;
using RmsPM.Web.WorkFlowControl;

namespace RmsPM.Web.Components
{
	/// <summary>
	/// ControlMessageBase ��ժҪ˵����
	/// </summary>
	/// <summary>
	/// ������Ϣ
	/// </summary>
	public class ModuleMessageBase
	{
		/// <summary>
		/// ģ������
		/// </summary>
		private string _ModuleName=null;
		/// <summary>
		/// ģ��ID
		/// </summary>
		private string _ModuleID=null;
		/// <summary>
		/// ģ�鱸ע
		/// </summary>
		private string _Remark=null;
		/// <summary>
		/// �ؼ�״̬
		/// </summary>
		private string _State;
		private DataTable _ControlDataTable=null;

		#region ҳ�湲������
		/// <summary>
		/// ģ������
		/// </summary>
		public string ModuleName
		{
			get
			{
				//if(_ModuleName==null)
				return _ModuleName;
			}
			set
			{
				_ModuleName=value;
			}
		}
		/// <summary>
		/// ģ��ID
		/// </summary>
		public string ModuleID
		{
			get
			{
				return _ModuleID;
			}
			set
			{
				_ModuleID=value;
			}
		}
		/// <summary>
		/// ģ�鱸ע
		/// </summary>
		public string Remark
		{
			get
			{
				if(State==null)
				{
					SearchTable();
				}
				return _Remark;
			}
			set
			{
				_Remark=value;
			}
		}
		/// <summary>
		/// ״ֵ̬
		/// </summary>
		public string State
		{
			get
			{
				if(State==null)
				{
					SearchTable();
				}
				return _Remark;
			}
			set
			{
				_Remark=value;
			}
		}
		/// <summary>
		/// �ڴ���Ʊ�
		/// </summary>
		public DataTable ControlDataTable
		{
			get
			{
				return _ControlDataTable;
			}
			set
			{
				_ControlDataTable=value;
			}
		}
		#endregion
		#region ���ݲ���
		/// <summary>
		/// ����һ�ų�ʼ���±�
		/// </summary>
		/// <returns></returns>
		public DataTable CreateTable()
		{
			DataTable dt = new DataTable();
			dt.Columns.Add("ModuleName",System.Type.GetType("System.String"));//ģ������
			dt.Columns.Add("ModuleID",System.Type.GetType("System.String"));//ģ��ID
			dt.Columns.Add("Remark",System.Type.GetType("System.String"));//��ע
			dt.Columns.Add("State",System.Type.GetType("System.String"));//״̬
			return dt;
		}
		/// <summary>
		/// �������
		/// </summary>
		public void SearchTable()
		{
			if(this._ModuleID!=null)
			{
				GetResult("ModuleID",this._ModuleID);
				return;
			}
			else if(this._ModuleName!=null)
			{
				GetResult("ModuleName",_ModuleName);
				return;
			}
		}
		/// <summary>
		/// �õ����
		/// </summary>
		/// <param name="name"></param>
		/// <param name="key"></param>
		private void GetResult(string name,string key)
		{
			if(_ControlDataTable==null)
			{
				throw new Exception("δ��ʼ����");
			}
			int count = _ControlDataTable.Rows.Count;
			for(int i=0;i<count;i++)
			{
				if(_ControlDataTable.Rows[i][name].ToString()==key)
				{
					this._ModuleName=_ControlDataTable.Rows[i]["ModuleName"].ToString();
					this._ModuleID=_ControlDataTable.Rows[i]["ModuleID"].ToString();
					this._Remark=_ControlDataTable.Rows[i]["Remark"].ToString();
					this._State=_ControlDataTable.Rows[i]["State"].ToString();
					return;
				}
			}
			return;
		}
		#endregion	
	}
}