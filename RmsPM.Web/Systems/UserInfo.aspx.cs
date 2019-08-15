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
using RmsPM.DAL.EntityDAO;
using RmsPM.DAL.QueryStrategy;

namespace RmsPM.Web.Systems
{
	/// <summary>
	/// UserInfo 的摘要说明。
	/// </summary>
	public partial class UserInfo : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlTableCell c;


		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			
			if (!IsPostBack)
			{
				IniPage();
				LoadData();


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
			this.txtRefreshScript.Value = Request["RefreshScript"] + "";
			string userCode = Request["UserCode"] + "";
		}

		private void LoadData()
		{
			string userCode=Request["UserCode"] + "";

			try
			{
				EntityData ds = SystemManageDAO.GetStandard_SystemUserByCode(userCode);
				if (ds.HasRecord())
				{
					this.lblUserID.Text=ds.GetString("UserID");
					this.lblUserName.Text=ds.GetString("UserName");

                    this.lblShortName.Text = ds.GetString("ShortUserName");
					this.lblEMail.Text= RmsPM.BLL.PageHelpDisplay.ChangeMessageForDisplay(ds.GetString("MailBox"),"</br>",';');
					this.lblPhone.Text=ds.GetString("Phone");
					this.lblMobile.Text=ds.GetString("Mobile");
					this.lblAddress.Text=ds.GetString("Address");
					this.lblFax.Text=ds.GetString("Fax");
					this.lblBirthDay.Text=ds.GetDateTimeOnlyDate("BirthDay");
					this.lblPhoneHome.Text = ds.GetString("PhoneHome");
					this.lblSortID.Text = ds.GetString("SortID");

					this.tdStationName.InnerHtml = BLL.SystemRule.GetUserStationNameHtml(userCode);


					this.lblSex.Text = ds.GetString("Sex");
					int status = ds.GetInt("Status");
					if ( status == 1 )
						this.lblU.Text = "禁用";
					else
						this.lblU.Text = "启用";

				}

				//显示财务编码
				this.lblSubjectSetDesc.Text = BLL.FinanceRule.GetFinanceSubjectSetDesc(ds.Tables["SystemUserSubjectSet"]);

				ds.Dispose();

//				RoleStrategyBuilder sb = new RoleStrategyBuilder();
//				sb.AddStrategy( new Strategy( RoleStrategyName.UserCode,userCode ));
//				string sql = sb.BuildMainQueryString();
//				QueryAgent qa = new QueryAgent();
//				EntityData entity = qa.FillEntityData("Role",sql);
//				this.dgList.DataSource = entity.CurrentTable;
//				this.dgList.DataBind();
//				entity.Dispose();
//				qa.Dispose();

			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示出错"));
			}
		}


		private void WriteRefreshScript(  ) 
		{
			Response.Write(Rms.Web.JavaScript.ScriptStart);
			if (this.txtRefreshScript.Value.Trim() != "") 
			{
				Response.Write( "window.opener." + this.txtRefreshScript.Value.Trim());
			}
			else 
			{
				Response.Write(Rms.Web.JavaScript.OpenerReload(false));
			}
			Response.Write(Rms.Web.JavaScript.WinClose(false));
			Response.Write(Rms.Web.JavaScript.ScriptEnd);
		}

		protected void btnDelete_ServerClick(object sender, System.EventArgs e)
		{
			string UserCode = Request["UserCode"] + "" ;
			try
			{
				EntityData entity = DAL.EntityDAO.SystemManageDAO.GetStandard_SystemUserByCode(UserCode);
				DAL.EntityDAO.SystemManageDAO.DeleteStandard_SystemUser(entity);
				entity.Dispose();
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "删除出错"));
				return;
			}

			WriteRefreshScript();
		}


	}
}

