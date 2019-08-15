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
	///		Control_Work_ThisWeek 的摘要说明。
	/// </summary>
	public partial class Control_Work_ThisWeek : Components.BaseControl
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			if(!this.IsPostBack)
			{
				DefaultSet();
			}
		}
		private int intListUnderwayNum=4;
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
					LoadUnderWay();
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"获取首页数据失败");
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
		///		设计器支持所需的方法 - 不要使用代码编辑器
		///		修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion
		#region 进行中的工作
		private void LoadUnderWay()
		{
			WBSStrategyBuilder asb = new WBSStrategyBuilder();
			ArrayList arA = new ArrayList();
			arA.Add("070107");
			arA.Add(user.UserCode);
			asb.AddStrategy( new Strategy( DAL.QueryStrategy.WBSStrategyName.AccessRange,arA));
			asb.AddStrategy( new Strategy( DAL.QueryStrategy.WBSStrategyName.Status,"1"));
			asb.AddOrder(" PlannedStartDate ",false);			
			string sql = asb.BuildQueryDeskTopString();
			QueryAgent QA = new QueryAgent();
			QA.SetTopNumber(this.intListUnderwayNum);
			DataSet dsUnderWay = QA.ExecSqlForDataSet(sql);
			QA.Dispose();
			DataTable dt = dsUnderWay.Tables[0];
			dt.Columns.Add("Img",System.Type.GetType("System.String"));
			dt.Columns.Add("StatusName",System.Type.GetType("System.String"));
			dt.Columns.Add("Master",System.Type.GetType("System.String"));
			EntityData entityUser = new EntityData("TaskPerson");

            if (dt.Rows.Count > 0)
            {
                //点更多时，查看第1条所在项目
                this.imgOpenMoreUnderWayTask.Attributes["ProjectCode"] = dt.Rows[0]["ProjectCode"].ToString();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["ImportantLevel"].ToString() == "1")
                        dt.Rows[i]["Img"] = "<img src=\"images/icon_important.gif\" width=\"17\" height=\"18\">";
                    dt.Rows[i]["StatusName"] = BLL.ComSource.GetTaskStatusName(dt.Rows[i]["Status"].ToString());
                    entityUser = WBSDAO.GetTaskPersonByWBSCode(dt.Rows[i]["WBSCode"].ToString());
                    string strTUser = "";// 取得当前任务负责人					
                    for (int j = 0; j < entityUser.CurrentTable.Rows.Count; j++)
                    {
                        if (entityUser.CurrentTable.Rows[j]["Type"].ToString() == "2") // 负责
                        {
                            strTUser += (strTUser == "") ? "" : ",";
                            strTUser += BLL.SystemRule.GetUserName(entityUser.CurrentTable.Rows[j]["UserCode"].ToString());
                        }
                    }			//strTUser.Substring(0,strTUser.Length-1)		
                    dt.Rows[i]["Master"] = (strTUser.Length > 0) ? strTUser.Substring(0, strTUser.Length - 1) : "";
                }
            }

			this.rpUnderWay.DataSource = dsUnderWay;
			this.rpUnderWay.DataBind();
			dsUnderWay.Dispose();
		}

		#endregion
	}
}
