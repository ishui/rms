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
using RmsPM.DAL;
using Rms.Web;
using RmsPM.BLL;
using RmsPM.DAL.QueryStrategy;

namespace RmsPM.Web.PBS
{
	/// <summary>
	/// RoomModelList 的摘要说明。
	/// </summary>
	public partial class RoomModelList : PageBase
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack)
			{
				this.txtModelCode.Value = Request.QueryString["ModelCode"];
				this.txtProjectCode.Value = Request.QueryString["ProjectCode"];

				//权限
				this.btnAdd.Visible = base.user.HasRight("010502");

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
			try
			{
				ModelStrategyBuilder sb = new ModelStrategyBuilder();

				sb.AddStrategy( new Strategy( ModelStrategyName.ProjectCode, txtProjectCode.Value));

				sb.AddOrder("ModelName", true);
				string sql = sb.BuildQueryDoorNumString();

				QueryAgent qa = new QueryAgent();
				EntityData entity = qa.FillEntityData( "Model",sql );
				qa.Dispose();

				this.dgList.DataSource = entity.CurrentTable;
				this.dgList.DataBind();
				entity.Dispose();
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}
		}

	}
}
