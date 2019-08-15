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

namespace RmsPM.Web.Finance
{
	/// <summary>
	/// SelectSubjectTree 的摘要说明。
	/// </summary>
	public partial class SelectSubjectTree : PageBase
	{
		protected string ReturnFunc = "";
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack) 
			{
				this.txtProjectCode.Value = Request.QueryString["ProjectCode"];
                this.txtSubjectSetCode.Value = Request.QueryString["SubjectSetCode"];
                this.txtSubjectCode.Value = Request.QueryString["SubjectCode"];
				this.txtAct.Value = Request.QueryString["Act"];
				this.txtReturnFunc.Value = Request["ReturnFunc"];
				this.txtDefine1.Value = Request["Define1"];
				this.txtSearchSubjectCode.Value = this.txtSubjectCode.Value;

				if (this.txtReturnFunc.Value == "") 
				{
					this.txtReturnFunc.Value = "SelectSubjectReturn";
				}

				this.ReturnFunc = this.txtReturnFunc.Value;

                if (this.txtSubjectSetCode.Value == "")
                {
                    this.txtSubjectSetCode.Value = BLL.ProjectRule.GetSubjectSetCodeByProject(txtProjectCode.Value);
                }

                this.LabelSubjectSetName.Text = BLL.SubjectRule.GetSubjectSetName(this.txtSubjectSetCode.Value);

				switch (this.txtAct.Value.ToLower()) 
				{
					case "auto":
						AutoLoad();
						break;
				}
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
		/// 按代码查询，直接返回名称
		/// </summary>
		private void AutoLoad() 
		{
			try 
			{
				string SubjectCode = this.txtSearchSubjectCode.Value;
				string SubjectName = BLL.SubjectRule.GetSubjectFullName(SubjectCode, this.txtSubjectSetCode.Value);

				Response.Write(Rms.Web.JavaScript.ScriptStart);
				Response.Write(string.Format("window.opener.{0}('{1}', '{2}', '{3}');", this.txtReturnFunc.Value, SubjectCode, SubjectName, this.txtDefine1.Value));
				Response.Write("window.close();");
				Response.Write(Rms.Web.JavaScript.ScriptEnd);
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "查询出错：" + ex.Message));
			}
		}
	}
}
