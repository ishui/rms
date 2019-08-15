using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Rms.ORMap;
using Rms.Web;
using RmsPM.DAL;
using RmsPM.DAL.EntityDAO;
using RmsPM.BLL;

namespace RmsPM.Web.SelectBox
{
	/// <summary>
	/// SelectPerson_List 的摘要说明。
	/// </summary>
	public partial class SelectPerson_List : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (Request.QueryString["Type"] + "" == "Single")
			{
				this.dgPersonList.Columns.Remove(this.dgPersonList.Columns[dgPersonList.Columns.Count-1]);
				this.dgPersonList.Columns[2].Visible = false;
				this.tbButton.Visible = false;
			}
			else
			{
				this.dgPersonList.Columns[1].Visible = false;
			}

			// 在此处放置用户代码以初始化页面
			if (!this.IsPostBack)
			{
				//返回函数名
				string ReturnFunc = Request.QueryString["ReturnFunc"] + "";
				if (ReturnFunc == "") 
				{
					ReturnFunc = "DoSelectUser";
				}
				ViewState["ReturnFunc"] = ReturnFunc;

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
		
		private void LoadData()
		{

			//初始化符合条件的数据列表
			string Name = this.txtUserName.Text.Trim();

			try
			{
				RmsPM.DAL.QueryStrategy.UserStrategyBuilder USB = new RmsPM.DAL.QueryStrategy.UserStrategyBuilder();
				if (Name.Length > 0)
				{
					USB.AddStrategy( new Strategy (RmsPM.DAL.QueryStrategy.UserStrategyName.UserName,"%" + Name + "%"));
				}
				string Sql = USB.BuildMainQueryString();
				QueryAgent QA = new QueryAgent();
				EntityData ds = QA.FillEntityData("SystemUser",Sql);
				QA.Dispose();

				this.dgPersonList.DataSource =new DataView(ds.CurrentTable);
				this.dgPersonList.DataBind();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"加载人员列表失败");
			}
		}

		protected void btnSearch_ServerClick(object sender, System.EventArgs e)
		{
			this.dgPersonList.CurrentPageIndex = 0;
			LoadData();
		}

		protected void SaveToolsButton_ServerClick(object sender, System.EventArgs e)
		{
			StringBuilder strBuilder = new StringBuilder();

			foreach(DataGridItem oItem in this.dgPersonList.Items)
			{
				System.Web.UI.WebControls.CheckBox chkPerson = (CheckBox)oItem.FindControl("chkPerson");
				string code = oItem.Cells[0].Text;
				if (chkPerson.Checked == true)
				{
					strBuilder.Append(code + "," );
				}
			}

			if (strBuilder.Length == 0)
			{
				Response.Write(JavaScript.ScriptStart);
				Response.Write("window.close();");
				Response.Write(JavaScript.ScriptEnd);
			}
			else
			{
				string Code = strBuilder.ToString().Remove(strBuilder.Length-1,1);
				Response.Write(JavaScript.ScriptStart);
				Response.Write("window.opener.DoSelectUser('" + Code + "','1');");
				Response.Write("window.close();");
				Response.Write(JavaScript.ScriptEnd);
			}
		}
	}
}
