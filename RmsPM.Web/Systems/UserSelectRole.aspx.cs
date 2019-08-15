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
	/// UserSelectRole ��ժҪ˵����
	/// </summary>
	public partial class UserSelectRole : PageBase
	{
		protected System.Web.UI.WebControls.DataGrid dgList;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack) 
			{
				string userCode = Request["UserCode"] + "";
				this.txtFrom.Value = Request["from"] + "";
				this.txtUserCode.Value = userCode;
				this.txtRefreshScript.Value = Request["RefreshScript"] + "";
				this.lblUserName.Text = BLL.SystemRule.GetUserName(userCode);

				if (this.txtFrom.Value.ToLower() == "project") 
				{
					this.txtRootUnitCode.Value = "-1";

					string ProjectCode = Session["ProjectCode"].ToString();
					this.txtProjectCode.Value = ProjectCode;

					EntityData entity = DAL.EntityDAO.ProjectDAO.GetProjectByCode(ProjectCode);
					if (entity.CurrentTable.Rows.Count > 0) 
					{
						this.txtRootUnitCode.Value = entity.GetString("UnitCode");
					}
					entity.Dispose();
				}
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

		}
		#endregion

		/// <summary>
		/// ��ʾ�б�
		/// </summary>
		private void LoadDataGrid() 
		{
			try
			{
				string UnitCode = this.txtSelectUnitCode.Value;
				EntityData entity = DAL.EntityDAO.OBSDAO.GetStationByCode(UnitCode);
				this.dgList.DataSource = entity.CurrentTable;
				this.dgList.DataBind();
				entity.Dispose();

			}
			catch ( Exception ex )
			{
				this.lblMessage.Text = "ѡ���ɫ��ѯ����";
				ApplicationLog.WriteLog(this.ToString(), ex, "ѡ���ɫ��ѯ����");
			}
		}

		private void btnUnitClick_ServerClick(object sender, System.EventArgs e)
		{
			LoadDataGrid();
		}

		private bool SaveUserRole() 
		{
			try
			{
				string UserCode = this.txtUserCode.Value;
				string select = this.txtSelectRoleCode.Value;
				string[] arr = select.Split(",".ToCharArray());

				EntityData entity = DAL.EntityDAO.SystemManageDAO.GetUserRoleByUserCode(UserCode);
				entity.DeleteAllTableRow("UserRole");


				//ɾ��
//				foreach(DataRow dr in tb.Rows) 
//				{
//					string RoleCode = dr["RoleCode"].ToString();
//					if (Array.IndexOf(arr, RoleCode) < 0) 
//					{
//						dr.Delete();
//						DAL.EntityDAO.SystemManageDAO.DeleteUserRole(entity);
//					}
//				}

				//���
				for(int i=0;i<arr.Length;i++)
				{

					string RoleCode = arr[i];
					if ( RoleCode != "" )
					{
						DataRow dr;
						dr = entity.GetNewRecord();
						dr["UserCode"]= UserCode;
						dr["RoleCode"] = RoleCode;

						entity.AddNewRecord(dr);
						DAL.EntityDAO.SystemManageDAO.InsertUserRole(entity);
					}
				}

				entity.Dispose();

				return true;
			}
			catch(Exception ex )
			{
				this.lblMessage.Text = "��ӽ�ɫ�û�ʧ��";
				ApplicationLog.WriteLog(this.ToString(),ex,"��ӽ�ɫ�û�ʧ��");
				return false;
			}
		}

		protected void btnOK_ServerClick(object sender, System.EventArgs e)
		{
			if (!SaveUserRole())
			{
				return;
			}

			Response.Write(Rms.Web.JavaScript.ScriptStart);
			if (this.txtRefreshScript.Value.Trim() == "" || this.txtRefreshScript.Value.Trim() == ";" ) 
			{
				Response.Write(Rms.Web.JavaScript.OpenerReload(false));
			}
			else 
			{
				Response.Write( "window.opener." + this.txtRefreshScript.Value.Trim());
			}
			Response.Write(Rms.Web.JavaScript.WinClose(false));
			Response.Write(Rms.Web.JavaScript.ScriptEnd);
			Response.End();
		}
	}
}
