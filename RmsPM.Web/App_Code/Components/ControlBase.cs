using Rms.ORMap;
using System;

namespace RmsPM.Web.Components
{
	/// <summary>
	/// ControlBase 的摘要说明。
	/// </summary>
	public class ControlBase : System.Web.UI.UserControl
	{
		public ControlBase()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		#region 私有属性 -----------------------------------------------
		/// <summary>
		/// 事务对象
		/// </summary>
		private StandardEntityDAO _dao;
		#endregion


		#region 共公属性 -----------------------------------------------
		public bool IsEditMode
		{
			get
			{
				if(ViewState["IsEditMode"]==null)
				{
					return true;
				}
				else
				{
					return (bool)ViewState["IsEditMode"];
				}
			}

			set
			{
				ViewState["IsEditMode"] = value;
			}
		}
		public string State
		{
			get
			{
				if(ViewState["State"]==null)
				{
					return "";
				}
				else
				{
					return (string)ViewState["State"];
				}
			}

			set
			{
				ViewState["State"] = value;
			}
		}
		/// <summary>
		/// 事务对象
		/// </summary>
		public StandardEntityDAO dao
		{
			get
			{
				return this._dao;
			}
			set
			{
				_dao = value;
			}
		}
		public string ApplicationCode
		{
			get
			{
				if(Request["ApplicationCode"]+""!="")
				{
					ViewState["ApplicationCode"]=Request["ApplicationCode"]+"";
				}
				else if(ViewState["ApplicationCode"]==null)
				{
					ViewState["ApplicationCode"]="";
				}
				return (string)ViewState["ApplicationCode"];
			}

			set
			{
				ViewState["State"] = value;//ToDo:这里是不是打错了？
			}
		}
		#endregion


		#region 私有方法 -----------------------------------------------
		virtual protected void LoadData()
		{
		}
		#endregion


		#region 公共方法 -----------------------------------------------
		/// <summary>
		/// 得到新的主键标识
		/// </summary>
		/// <param name="codeName"></param>
		public void GetNewApplicationCode(string codeName)
		{
			this.ApplicationCode=DAL.EntityDAO.SystemManageDAO.GetNewSysCode(codeName);
		}
		virtual public void OnitControl()
		{
		}
		virtual public void SumitData()
		{
		}
		#endregion
	}
}
