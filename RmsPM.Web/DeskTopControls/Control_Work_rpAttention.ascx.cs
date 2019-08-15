namespace RmsPM.Web.DeskTopControl
{
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
	using RmsPM.BLL;


	/// <summary>
	///		Control_Work_rpAttention 的摘要说明。
	/// </summary>
	public partial class Control_Work_rpAttention : Components.BaseControl
	{

		int intListAttentionNum=4;
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			DefaultSet();
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
		///		设计器支持所需的方法 - 不要使用代码编辑器
		///		修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion
		private void DefaultSet()
		{
			LoadData();
		}
		private void LoadData()
		{
			try
			{
				string TaskEnable = BLL.ConvertRule.ToString(System.Configuration.ConfigurationSettings.AppSettings["TaskEnable"]);
				if (TaskEnable != "0")			
				LoadAttention();
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"获取首页数据失败");
			}
		}
		
		#region 我关注的工作
		private void LoadAttention()
		{
			AttentionStrategyBuilder asb = new AttentionStrategyBuilder();
			ArrayList arA = new ArrayList();
			arA.Add("070110");
			arA.Add(user.UserCode);
			arA.Add(user.BuildStationCodes());
			asb.AddStrategy( new Strategy( DAL.QueryStrategy.AttentionStrategyName.AccessRange,arA));
			asb.AddOrder(" AddTime ",false);
			string sql = asb.BuildMainQueryString();
			QueryAgent qa = new QueryAgent();
			qa.SetTopNumber(this.intListAttentionNum);
			EntityData entityAttention = qa.FillEntityData("TaskAttention",sql);
			qa.Dispose();
			rpAttention.DataSource = entityAttention;					
			//			EntityData entityAttention = WBSDAO.GetAllTaskAttention();	
			if (entityAttention.HasRecord())
			{
                //点更多时，查看第1条所在项目
                this.imgOpenMoreWBSAttention.Attributes["ProjectCode"] = entityAttention.CurrentTable.Rows[0]["ProjectCode"].ToString();

                DataTable dtAttentionNew = entityAttention.CurrentTable;
				dtAttentionNew.Columns.Add("Img",System.Type.GetType("System.String"));
				//				int i = 0;
				//				foreach(DataRow dr in dtAttentionNew.Rows)
				//				{
				//					if(dr["AddModule"].ToString()=="工作信息")
				//					{
				//						EntityData entityTask = WBSDAO.GetTaskByWBSCode(dr["MasterCode"].ToString());
				//						string strImportantLevel = entityTask.GetInt("ImportantLevel").ToString();
				//						if(strImportantLevel=="1")
				//							dr["Img"] =  "<img src=\"images/icon_important.gif\" width=\"17\" height=\"18\">";
				//					}
				//					if(dr["AddModule"].ToString()=="其他信息")
				//					{}
				//
				//					i++;
				//					if(i>this.intListAttentionNum) break;
				//				}
				this.rpAttention.DataSource = dtAttentionNew;
				this.rpAttention.DataBind();
			}			
			entityAttention.Dispose();
		}

		#endregion

	}
}
