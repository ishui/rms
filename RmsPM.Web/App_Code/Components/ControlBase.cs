using Rms.ORMap;
using System;

namespace RmsPM.Web.Components
{
	/// <summary>
	/// ControlBase ��ժҪ˵����
	/// </summary>
	public class ControlBase : System.Web.UI.UserControl
	{
		public ControlBase()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		#region ˽������ -----------------------------------------------
		/// <summary>
		/// �������
		/// </summary>
		private StandardEntityDAO _dao;
		#endregion


		#region �������� -----------------------------------------------
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
		/// �������
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
				ViewState["State"] = value;//ToDo:�����ǲ��Ǵ���ˣ�
			}
		}
		#endregion


		#region ˽�з��� -----------------------------------------------
		virtual protected void LoadData()
		{
		}
		#endregion


		#region �������� -----------------------------------------------
		/// <summary>
		/// �õ��µ�������ʶ
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
