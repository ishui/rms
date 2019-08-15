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

namespace RmsPM.Web.SelectBox
{
	/// <summary>
	/// SelectSystemGroup 的摘要说明。
	/// </summary>
	public partial class SelectSystemGroup : PageBase
	{
	
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Page.IsPostBack) 
			{
				string classCode = Request["ClassCode"]+"";
				this.lblClassName.Text = "classCode:" + classCode + " : " +  BLL.SystemRule.GetFunctionStructureName(classCode);

				//返回函数名
				string ReturnFunc = Request.QueryString["ReturnFunc"] + "";
				if (ReturnFunc == "") 
				{
					ReturnFunc = "getReturnSelectGroup";
				}
				ViewState["ReturnFunc"] = ReturnFunc;
                //this.lblClassName.Text = this.lblClassName.Text + " : " + ViewState["ReturnFunc"];

            }
		}
		public string ProjectCode
		{
			get
			{
				return Request["ProjectCode"]+"";
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
	}
}
