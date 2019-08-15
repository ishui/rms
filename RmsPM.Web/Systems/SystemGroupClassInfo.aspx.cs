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
	/// SystemGroupClassInfo 的摘要说明。
	/// </summary>
	public partial class SystemGroupClassInfo : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlTextArea txtDescription;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlInputText txtGroupName;
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
				this.txtClassCode.Value = Request.QueryString["ClassCode"];
				this.txtAct.Value = Request.QueryString["Action"];
				this.txtFromUrl.Value = Request.QueryString["FromUrl"];
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}
		}

		private void LoadData()
		{
			try
			{
				string ClassCode = this.txtClassCode.Value;

				if (ClassCode == "")
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "未传入大类代码"));
					return;
				}

				EntityData entity = RmsPM.DAL.EntityDAO.SystemManageDAO.GetFunctionStructureByCode(ClassCode);
				if (entity.HasRecord())
				{
					this.lblClassName.Text = entity.GetString("FunctionStructureName");
				}
				else 
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "类别不存在"));
					return;
				}
				entity.Dispose();

				if (ClassCode == "0401") 
				{
					//费用项
					this.trToolbar.Style["display"] = "none";
				}
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示出错：" + ex.Message));
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

		/// <summary>
		/// 返回
		/// </summary>
		private void GoBack() 
		{
			Response.Write(Rms.Web.JavaScript.ScriptStart);
			string FromUrl = this.txtFromUrl.Value.Trim();
			if (FromUrl != "") 
			{
				Response.Write(string.Format("window.location = '{0}';", FromUrl));
			}
			Response.Write(Rms.Web.JavaScript.ScriptEnd);
		}

		/// <summary>
		/// 粘贴
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnPaste_ServerClick(object sender, System.EventArgs e)
		{
			string act = "";

			try 
			{
				if (this.txtIsCut.Value == "1") 
				{
					act = "move";
					BLL.SystemGroupRule.MoveSystemGroup(this.txtSrcGroupCode.Value, "", this.txtClassCode.Value);
				}
				else
				{
					act = "insert";
					BLL.SystemGroupRule.CopySystemGroup(this.txtSrcGroupCode.Value, "", this.txtClassCode.Value);
				}
			}
			catch(Exception ex) 
			{
				Response.Write(JavaScript.Alert(true, "粘贴失败：" + ex.Message));
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				return;
			}

			string s = Rms.Web.JavaScript.ScriptStart
				+ string.Format("Refresh('{0}');", act)
				+ Rms.Web.JavaScript.ScriptEnd;
			this.RegisterStartupScript("start", s);
		}
	}
}
