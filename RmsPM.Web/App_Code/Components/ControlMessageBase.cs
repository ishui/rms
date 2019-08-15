using System;
using RmsPM.Web;
using System.Data;
using RmsPM.Web.WorkFlowControl;

namespace RmsPM.Web.Components
{
	/// <summary>
	/// ControlMessageBase 的摘要说明。
	/// </summary>
	/// <summary>
	/// 控制信息
	/// </summary>
	public class ModuleMessageBase
	{
		/// <summary>
		/// 模块名字
		/// </summary>
		private string _ModuleName=null;
		/// <summary>
		/// 模块ID
		/// </summary>
		private string _ModuleID=null;
		/// <summary>
		/// 模块备注
		/// </summary>
		private string _Remark=null;
		/// <summary>
		/// 控件状态
		/// </summary>
		private string _State;
		private DataTable _ControlDataTable=null;

		#region 页面共公属性
		/// <summary>
		/// 模块名字
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
		/// 模块ID
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
		/// 模块备注
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
		/// 状态值
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
		/// 内存控制表
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
		#region 数据操作
		/// <summary>
		/// 建立一张初始化新表
		/// </summary>
		/// <returns></returns>
		public DataTable CreateTable()
		{
			DataTable dt = new DataTable();
			dt.Columns.Add("ModuleName",System.Type.GetType("System.String"));//模块名字
			dt.Columns.Add("ModuleID",System.Type.GetType("System.String"));//模块ID
			dt.Columns.Add("Remark",System.Type.GetType("System.String"));//备注
			dt.Columns.Add("State",System.Type.GetType("System.String"));//状态
			return dt;
		}
		/// <summary>
		/// 搜索表格
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
		/// 得到结果
		/// </summary>
		/// <param name="name"></param>
		/// <param name="key"></param>
		private void GetResult(string name,string key)
		{
			if(_ControlDataTable==null)
			{
				throw new Exception("未初始化表");
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