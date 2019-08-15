using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Rms.Web;
using Rms.ORMap;
using RmsPM.DAL.EntityDAO;

namespace RmsPM.Web.Systems
{
	/// <summary>
	/// SelectRoleList ��ժҪ˵����
	/// </summary>
    public partial class SelectRoleList : PageBase
	{
	
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack) 
			{
				this.txtUnitCode.Value = Request["UnitCode"] + "";
				this.txtUserCode.Value = Request["UserCode"] + "";
				this.txtSelectRoleCode.Value = Request["SelectRoleCode"] + "";
				this.txtRefreshScript.Value = Request["RefreshScript"] + "";

				if (this.txtSelectRoleCode.Value.ToLower() == "first") 
				{
					LoadCheckFromDB();
				}

				LoadDataGrid();

			}
		}

		#region Web ������������ɵĴ���
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: �õ����� ASP.NET Web ���������������ġ�
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{    
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		/// <summary>
		/// ��ʾ�б�
		/// </summary>
		private void LoadDataGrid() 
		{
			try
			{
				string UnitCode = this.txtUnitCode.Value;
				string UserCode = this.txtUserCode.Value;

				EntityData entity;

				if (UnitCode == "") 
				{
					entity = DAL.EntityDAO.OBSDAO.GetAllStation();
				}
				else 
				{
					entity = DAL.EntityDAO.OBSDAO.GetStationByUnitCode(UnitCode);
				}

				this.dgList.DataSource = entity.CurrentTable;
				this.dgList.DataBind();
				entity.Dispose();

				DisplayCheck();
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(), ex, "ѡ���λ��ѯ����");
			}
		}

		private void LoadCheckFromDB() 
		{
			try
			{
				string UserCode = this.txtUserCode.Value;
				string stationCodes = "";

				EntityData entity = DAL.EntityDAO.OBSDAO.GetStationByUserCode(UserCode);

				foreach(DataRow dr in entity.CurrentTable.Rows) 
				{
					if (stationCodes != "") 
					{
						stationCodes = stationCodes + ",";
					}

					stationCodes = stationCodes + dr["StationCode"].ToString();
				}

				this.txtSelectRoleCode.Value = stationCodes;

				Response.Write(JavaScript.ScriptStart);
				Response.Write("window.parent.document.all.txtSelectRoleCode.value = '" + stationCodes + "';");
				Response.Write(JavaScript.ScriptEnd);

				entity.Dispose();
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(), ex, "ѡ���ɫ��ѯ�û���ɫ����");
			}
		}

		private void DisplayCheck() 
		{
			string SelectValue = this.txtSelectRoleCode.Value;

			if (SelectValue == "") 
			{
				return;
			}

			string[] arr = SelectValue.Split(",".ToCharArray());

//			SelectValue = "," + SelectValue + ",";

			for (int i=0;i<this.dgList.Items.Count;i++) 
			{
				HtmlInputCheckBox chk = (HtmlInputCheckBox)this.dgList.Items[i].FindControl("chk");
				if ( chk != null )
				{
					string stationCode = this.dgList.DataKeys[i].ToString();
//					RoleCode = "," + RoleCode + ",";

					if (Array.IndexOf(arr, stationCode) >= 0)
					{
						chk.Checked = true;
					}
					else 
					{
						chk.Checked = false;
					}
				}

			}
		}

/*
		private void LoadCheck() 
		{
			EntityData entity = DAL.EntityDAO.SystemManageDAO.GetUserRoleByUserCode(this.txtUserCode.Value);
			DataTable tb = entity.CurrentTable;

			for (int i=0;i<this.dgList.Items.Count;i++) 
			{
				HtmlInputCheckBox chk = (HtmlInputCheckBox)this.dgList.Items[i].FindControl("chk");
				if ( chk != null )
				{
					string RoleCode = this.dgList.DataKeys[i].ToString();
					if (tb.Select("RoleCode='" + RoleCode + "'").Length > 0)
					{
						chk.Checked = true;
					}
					else 
					{
						chk.Checked = false;
					}
				}

			}

			entity.Dispose();
		}
*/
        protected void dgList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
}
}
