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
using Rms.ORMap;
using Rms.Web;
using RmsPM.DAL.EntityDAO;
using RmsPM.BLL;

namespace RmsPM.Web.Systems
{
	/// <summary>
	/// TaskAccessInfo ��ժҪ˵����
	/// </summary>
	public partial class TaskAccessInfo : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlTextArea txtDescription;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlInputText txtGroupName;
		protected System.Web.UI.WebControls.Label lblGroupName;
		protected System.Web.UI.UserControl ucGroupTree;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)	
			{
				IniPage();
				LoadData();
			}
		}

		private void IniPage() 
		{
			try 
			{
//				this.txtFromUrl.Value = Request.QueryString["FromUrl"];
				this.txtWBSCode.Value = Request.QueryString["WBSCode"];
				this.txtAct.Value = Request.QueryString["Action"];
				this.txtFromUrl.Value = Request.QueryString["FromUrl"];
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
			}
		}

		private void LoadData()
		{
			try
			{
				string WBSCode = this.txtWBSCode.Value;

				if (WBSCode == "")
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "δ���빤�������"));
					return;
				}

				EntityData entity = RmsPM.DAL.EntityDAO.WBSDAO.GetTaskByCode(WBSCode);
				if (entity.HasRecord())
				{
					this.lblTaskName.Text = entity.GetString("SortID").ToString() + " " + entity.GetString("TaskName");
					this.txtParentCode.Value = entity.GetString("ParentCode");
					this.txtProjectCode.Value = entity.GetString("ProjectCode");
					this.lblProjectName.Text = BLL.ProjectRule.GetProjectName(this.txtProjectCode.Value);
				}
				else 
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "���������"));
					return;
				}
				entity.Dispose();

				LoadAccessRange();
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ������" + ex.Message));
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
		/// ��ʾȨ���б�
		/// </summary>
		private void LoadAccessRange() 
		{
			try 
			{
				string WBSCode = this.txtWBSCode.Value;

				DataTable tb = BLL.WBSRule.GetTaskPersonNameGroupByTypeIncludeEmpty(WBSCode);

				BindDataGrid(tb);
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ�б����" + ex.Message));
			}
		}

		private void BindDataGrid(DataTable tb)
		{
			try 
			{
				DataView dv = new DataView(tb, "", "SortID", DataViewRowState.CurrentRows);

				this.dgList.DataSource = dv;
				this.dgList.DataBind();
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ�б����" + ex.Message));
			}
		}

		/// <summary>
		/// <summary>
		/// ����
		/// </summary>
		private void GoBack() 
		{
			Response.Write(Rms.Web.JavaScript.ScriptStart);
			Response.Write("window.parent.location = window.parent.location;");

//			Response.Write("window.location = '../Blank.aspx';");

//			string FromUrl = this.txtFromUrl.Value.Trim();
//			if (FromUrl != "") 
//			{
//				Response.Write(string.Format("window.location = '{0}';", FromUrl));
//			}
			Response.Write(Rms.Web.JavaScript.ScriptEnd);
		}

	}
}
