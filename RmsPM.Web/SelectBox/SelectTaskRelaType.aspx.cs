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
using RmsPM.DAL;
using RmsPM.DAL.QueryStrategy;
using RmsPM.BLL;

namespace RmsPM.Web.SelectBox
{
	/// <summary>
	/// SelectTaskRelaType 的摘要说明。
	/// </summary>
	public partial class SelectTaskRelaType : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlInputButton Button1;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnDelete;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( !IsPostBack)
			{
				IniPage();
				LoadData();
			}
		}

		private void IniPage()
		{
			this.txtProjectCode.Value = Request["ProjectCode"];
			this.txtRelaType.Value = Request["RelaType"];
			this.txtPBSTypeCode.Value = Request["PBSTypeCode"];
			this.txtInputCode.Value = Request["SelectCode"];
//			this.txtInputName.Value = Request["SelectName"];
			this.txtReturnFunc.Value = Request["ReturnFunc"];
			this.txtCanSelectArea.Value = Request["CanSelectArea"];

			if (this.txtReturnFunc.Value == "") 
			{
				this.txtReturnFunc.Value = "SelectBuildingReturn";
			}

			if (this.txtRelaType.Value == "") 
			{
				this.txtRelaType.Value = " ";
			}
			this.rdoType.SelectedValue = this.txtRelaType.Value;
		}

		private void LoadData()
		{
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

		protected void btnSave_ServerClick(object sender, System.EventArgs e)
		{
			string OutputFullName = BLL.TaskRule.ConcatTaskRelaName(this.rdoType.SelectedItem.Text, this.txtOutputName.Value);

			Response.Write(Rms.Web.JavaScript.ScriptStart);
			Response.Write("window.opener." + this.txtReturnFunc.Value + "('" + this.rdoType.SelectedValue.Trim() + "', '" + this.txtOutputCode.Value + "', '" + this.txtOutputName.Value + "', '" + OutputFullName + "');");
			Response.Write(Rms.Web.JavaScript.WinClose(false));
			Response.Write(Rms.Web.JavaScript.ScriptEnd);
			Response.End();
		}
	}
}
