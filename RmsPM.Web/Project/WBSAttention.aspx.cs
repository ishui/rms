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
using RmsPM.Web;
using RmsPM.DAL.QueryStrategy;

namespace RmsPM.Web.Project
{
	/// <summary>
	/// WBSAttention 的摘要说明。
	/// </summary>
	public partial class WBSAttention : PageBase
	{
	
		EntityData entityAttention= null;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			try
			{			
				if(!this.IsPostBack)
				{
					InitPage();
					LoadData();
				}
			}		
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"载入关注工作列表失败");
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
			this.dgAttention.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgAttention_DeleteCommand);

		}
		#endregion

		/// <summary>
		/// 初始化基本数据
		/// </summary>
		private void InitPage()
		{		
			ViewState["ProjectCode"] = Request["ProjectCode"].ToString();
		}

		/// <summary>
		/// 载入数据
		/// </summary>
		private void LoadData()
		{

			
			User objUser = (User)Session["User"];
			AttentionStrategyBuilder asb = new AttentionStrategyBuilder();
			ArrayList arA = new ArrayList();
			arA.Add("070110");
			arA.Add(objUser.UserCode);
			arA.Add(user.BuildStationCodes());
			asb.AddStrategy( new Strategy( DAL.QueryStrategy.AttentionStrategyName.AccessRange,arA));
			asb.AddStrategy( new Strategy( DAL.QueryStrategy.AttentionStrategyName.ProjectCode,(string)ViewState["ProjectCode"]));
			if(this.txtType.Value.Length>0) 
				asb.AddStrategy( new Strategy( DAL.QueryStrategy.AttentionStrategyName.AddModule,this.txtType.Value));
			if(this.txtTitle.Value.Length>0) 
			asb.AddStrategy( new Strategy( DAL.QueryStrategy.AttentionStrategyName.AddTitle,this.txtTitle.Value));
			if(this.dtStartDate.Value.Length>0||this.dtEndDate.Value.Length>0)
			{
				ArrayList arB = new ArrayList();
				arB.Add(this.dtStartDate.Value);
				arB.Add(this.dtEndDate.Value);
				asb.AddStrategy( new Strategy( DAL.QueryStrategy.AttentionStrategyName.AddTime,arB));
			}
			asb.AddOrder("AddTime",false);
			string sql = asb.BuildMainQueryString();						
			QueryAgent qa = new QueryAgent();
			entityAttention = qa.FillEntityData("TaskAttention",sql);
			qa.Dispose();
			this.dgAttention.DataSource = entityAttention;
			this.dgAttention.DataBind();
			this.tbNoAttention.Visible = (entityAttention.CurrentTable.Rows.Count > 0)?false:true;
			entityAttention.Dispose();
		}


		private void dgAttention_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			try
			{
				User objUser = (User)Session["User"];
				EntityData entity = WBSDAO.GetTaskAttentionByCode(this.dgAttention.DataKeys[e.Item.ItemIndex].ToString());
				WBSDAO.DeleteTaskAttention(entity);
				entity.Dispose();

				entityAttention.CurrentTable.Rows[e.Item.ItemIndex].Delete();
				DataView dv = entityAttention.CurrentTable.DefaultView;
				this.dgAttention.DataSource = dv;
				this.dgAttention.DataBind();
				this.tbNoAttention.Visible = (entityAttention.HasRecord())?false:true;
				entityAttention.Dispose();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"状态改变失败");
			}
		}

		protected void btnSearch_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				this.LoadData();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"状态改变失败");
			}
		}

	}
}
