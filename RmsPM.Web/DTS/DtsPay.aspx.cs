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
using RmsPM.DAL;

namespace RmsPM.Web.DTS
{
	/// <summary>
	/// DtsPay 的摘要说明。
	/// </summary>
	public partial class DtsPay : PageBase
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			if (!Page.IsPostBack) 
			{
				IniPage();
				ClearAll();
			}
		}

		#region Web 窗体设计器生成的代码
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion

		private void IniPage()
		{
			BLL.PageFacade.LoadSalProjectSelect(this.sltProject," ");
		}

		private void ClearAll() 
		{
			this.txtErr.Value = "";
			this.tbProgress.Visible = false;

			this.lbHint.Text = "";
			this.btnStop.Visible = false;

//			((DtsProgress)this.DtsProgress).Visible = false;
		}

		protected void btnDtc_ServerClick(object sender, System.EventArgs e)
		{
			ClearAll();

//			string server = System.Configuration.ConfigurationSettings.AppSettings["SaleServerName"];
//			string database = System.Configuration.ConfigurationSettings.AppSettings["SaleDatabaseName"];
//
//			this.txtServer.Value = server;
//			this.txtDatabase.Value = database;

//			DataTable tb = BLL.DtsPayRule.GetDtsPay(server, database);

			string ProjectCode = this.sltProject.Value.Trim();
            string ClientName = this.txtClientName.Value.Trim();

			DataTable tb;

            if (ClientName != "") //按客户
            {
                tb = BLL.DtsPayRule.GetDtsPayByClient(ProjectCode, ClientName);
            }
            else
            {
                tb = BLL.DtsPayRule.GetDtsPayByProject(ProjectCode);
            }

            /*
			//取要导入的数据
			switch (this.rdoOption.SelectedValue) 
			{
				case "0":
					tb = BLL.DtsPayRule.GetDtsPayByProject(ProjectCode);
					break;
				case "1":
                    tb = BLL.DtsPayRule.GetDtsPayByClient(ProjectCode, ClientName);
//                    tb = BLL.DtsPayRule.GetDtsPayByContract(ContractID);
                    break;
				default:
					return;
			}
            */

			if (tb.Rows.Count == 0) 
			{
				Response.Write(JavaScript.Alert(true, "无数据"));
				return;
			}

			DtsInfo dts = new DtsInfo(tb);
			Session["DtsSale"] = dts;

			DtsStart(dts);

			//			((DtsProgress)this.DtsProgress).Start(dts.Count);

/*				string script = "<script language='javascript'>\n"
				  + "isDtsContinue = true;\n"
				  + "</script>\n";
				Page.RegisterStartupScript("ContinueDts", script);
*/
		}

		private void DtsStart(DtsInfo dts) 
		{
			//			this.lbHint.Text = "共 " + tb.Rows.Count.ToString() + " 条记录，正在导入...";
			this.lbHint.Text = "正在导入，请稍候...";
			this.btnDtc.Disabled = true;
			this.btnClear.Disabled = true;
			this.btnStop.Visible = true;
			this.tbProgress.Visible = true;
			int Count = dts.DataSource.Rows.Count;
			this.txtCount.Value = Count.ToString();
			this.tdCount.InnerText = Count.ToString();
			this.txtIsContinue.Value = "1";
		}

		private void DtsFinish() 
		{
			if (Session["DtsSale"] != null) 
			{
				this.txtIsContinue.Value = "0";
				DtsInfo dts = (DtsInfo)Session["DtsSale"];
				Session["DtsSale"] = null;

				this.lbHint.Text = dts.GetResult();
//				((DtsProgress)this.DtsProgress).Over();
			}

			this.btnDtc.Disabled = false;
			this.btnClear.Disabled = false;
			this.tbProgress.Visible = false;
			this.btnStop.Visible = false;
			this.txtIsContinue.Value = "0";
		}

//		private void DtsContinue() 
//		{
//			if (Session["DtsSale"] == null) 
//			{
//				return;
//			}
//
//			string server = this.txtServer.Value;
//			string database = this.txtDatabase.Value;
//
//			DtsInfo dts = (DtsInfo)Session["DtsSale"];
//
//			if (dts.EOF) 
//			{
//				DtsFinish();
//			}
//			else 
//			{
//				dts.CurrentIndex = dts.CurrentIndex + 1;
//				DataRow dr = dts.DataSource.Rows[dts.CurrentIndex];
//				string case_id = dr["case_id"].ToString();
//				string case_name = dr["case_name"].ToString();
//
//				try 
//				{
//					BLL.DtsPayRule.DtsPaySingle(server, database, case_id);
//				}
//				catch (Exception ex)
//				{
//					dts.AddErr("“" + case_name + "”:" + ex.Message);
////					this.txtErr.Value = this.txtErr.Value +  + "\n";
//				}
//
//				((DtsProgress)this.DtsProgress).SetCurrentIndex(dts.CurrentIndex);
//
///*				string script = "<script language='javascript'>\n"
//					+ "isDtsContinue = true;\n"
//					+ "</script>\n";
//				Page.RegisterStartupScript("ContinueDts", script);*/
//			}
//
//
//		}
//
		protected void btnDtsContinue_ServerClick(object sender, System.EventArgs e)
		{
//			DtsContinue();
		}

		protected void btnDtsFinish_ServerClick(object sender, System.EventArgs e)
		{
			DtsFinish();
		}

		protected void btnClear_ServerClick(object sender, System.EventArgs e)
		{
			string ProjectCode = this.sltProject.Value.Trim();

			if (ProjectCode == "") 
			{
				//只清空有销售系统的项目
				EntityData entity = DAL.EntityDAO.ProjectDAO.GetAllSalProjectCode();

				int c = entity.CurrentTable.Rows.Count;
				for (int i=0;i<c;i++) 
				{
					entity.SetCurrentRow(i);
					BLL.DtsPayRule.ClearDtsPay(entity.GetString("ProjectCode"));
				}

				entity.Dispose();
			}
			else 
			{
				BLL.DtsPayRule.ClearDtsPay(ProjectCode);
			}

			Response.Write(JavaScript.Alert(true, "清空完成"));
		}
	}
}
