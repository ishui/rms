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

using RmsPM.Web;
using RmsPM.DAL;
using RmsPM.BLL;
using RmsPM.DAL.EntityDAO;
using Rms.Web;
using Rms.ORMap;


namespace RmsPM.Web.Project
{
	/// <summary>
	/// WBSStatusTask : 工作任务的区分项目条件筛选列表。
	/// </summary>
	/// <author>unm</author>
	/// <date>2004/11/10</date>
	/// <version>1.5</version>
    public partial class WBSStatusTask : PageBase
	{

		private string strStatus = "";
		private string strMaster = "";
		private string strTaskName = "";
		private string strImportantLevel = "";
		private string strStartDate = "";
		private string strEndDate = "";
		private string strProjectCode = "";
		private string strUserCode = "";
		private string strExceed = "";


        protected void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				InitPage();
				LoadData();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"工作条件筛选列表失败");
			}
		}

		private void InitPage()
		{
			this.strStatus = Request["TaskStatus"] + "";
			if (!this.IsPostBack)
			{
				this.dtbEndDate.Value = "";
				this.dtbStartDate.Value = "";
			}
			this.strMaster =  Request["Master"] + "";
			this.strTaskName = Request["TaskName"] + "";
			this.strImportantLevel = "1";
			this.strExceed = Request["Exceed"]+"";

			if(this.strExceed!="") this.lblTitle.Text = "超期工作";
			if(this.strImportantLevel!="") this.lblTitle.Text = "重要工作";
			switch (this.strStatus)
			{
				case "0":
					this.lblTitle.Text = "未开始工作";
					break;

				case "1":
					this.lblTitle.Text = "进行中工作";
					break;

				case "2":
					this.lblTitle.Text = "暂停工作";
					break;

				case "3":
					this.lblTitle.Text = "取消工作";
					break;

				case "4":
					this.lblTitle.Text = "已完成工作";
					break;
			}				
			
			//this.lstStatus.SelectedIndex = (this.strStatus == "" )?0:(int.Parse(this.strStatus) + 1);
			this.lstExceed.SelectedIndex = (this.strExceed == "" )?0:int.Parse(this.strExceed)+1; 
			this.lstImportantLevel.SelectedIndex = (this.strImportantLevel == "")?0:(int.Parse(this.strImportantLevel) + 1);
			this.strStartDate = Request["StartDate"] + "";
			this.strEndDate = Request["EndDate"] + "";
			this.strProjectCode = (string)Session["ProjectCode"];
			this.strUserCode = ((User)Session["User"]).UserCode;
			this.strExceed = Request["Exceed"] + "";

		}

		private void LoadData()
		{
			try
			{
				DAL.QueryStrategy.WBSStrategyBuilder WSB = new RmsPM.DAL.QueryStrategy.WBSStrategyBuilder();
//				WSB.AddStrategy(new Strategy(DAL.QueryStrategy.WBSStrategyName.UserAccess,this.strUserCode));
//				WSB.AddStrategy(new Strategy(DAL.QueryStrategy.WBSStrategyName.ProjectCode,this.strProjectCode));
				
				ArrayList arA = new ArrayList();
				arA.Add("070107");
				arA.Add(this.strUserCode);
				arA.Add(base.user.BuildStationCodes());
				WSB.AddStrategy( new Strategy( DAL.QueryStrategy.WBSStrategyName.AccessRange,arA));
				WSB.AddStrategy( new Strategy( DAL.QueryStrategy.WBSStrategyName.ProjectCode,base.ProjectCode));
				WSB.AddOrder(" PlannedStartDate ",false);

				string sql = WSB.BuildMainQueryString();
				QueryAgent QA = new QueryAgent();
				DataSet dsTask = QA.ExecSqlForDataSet(sql);
				DataTable dbTable = DisposeTask(dsTask.Tables[0]);
				QA.Dispose();
				
				string strCondition = "";
				if(this.strStatus.Length>0&&this.strStatus!="5"&&this.strStatus!="6")
				{
					strCondition +="Status='"+this.strStatus+"'";
				}
				if(this.strTaskName.Length>0)
				{
					strCondition += (strCondition == ""?"":" and ");
					strCondition += " ((TaskName LIKE '%" + Server.UrlDecode(this.strTaskName).ToString() + "%') OR ";
					strCondition +="(WBSCode like '%" + this.strTaskName + "%')) ";
				}
				if(this.strMaster.Length>0)
				{
					strCondition += (strCondition == ""?"":" and ");
					strCondition += " Master LIKE '%" + Server.UrlDecode(this.strMaster).ToString() + "%'";				
				}
				if(this.strImportantLevel.Length>0)
				{
					strCondition += (strCondition == ""?"":" and ");
					strCondition +="ImportantLevel='" + this.strImportantLevel + "'";
				}
				if(this.strStartDate!="")
				{
					strCondition += (strCondition == ""?"":" and ");
					strCondition+="CONVERT(PlannedStartDate,'System.DateTime')>='"+this.strStartDate+"'";
				}
				if(this.strEndDate!="")
				{
					strCondition += (strCondition == ""?"":" and ");
					strCondition+="CONVERT(PlannedStartDate,'System.DateTime')<'"+this.strEndDate+"'";
				}
				if(this.strExceed != "")
				{
					strCondition+=(strCondition==""?"":" and ");
					strCondition +="Exceed='" + this.strExceed + "'";
				}
				if(this.strStatus=="6")
				{
					strCondition+=(strCondition==""?"":" and ");
					strCondition +="ImportantLevel='" + this.strImportantLevel + "'";
				}

				DataView dvTask = new DataView(dbTable,strCondition,"",System.Data.DataViewRowState.CurrentRows);
				this.trNoTask.Visible = (dvTask.Count>0)?false:true;
				this.dgTaskList.DataSource = dvTask;
				this.dgTaskList.DataBind();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
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
			this.dgTaskList.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.dgTaskList_SortCommand);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void dgTaskList_SortCommand(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
		{
			DataView dv = (DataView)this.dgTaskList.DataSource;
			dv.Sort = e.SortExpression + " DESC";				
			this.dgTaskList.DataSource = dv;
			this.dgTaskList.DataBind();
		}


		private DataTable DisposeTask(System.Data.DataTable dtTask)
		{
			try
			{
				DataTable dtNew = dtTask.Copy();
				dtNew.Columns.Add("StatusName");
				dtNew.Columns.Add("ImportantName");
				dtNew.Columns.Add("Master");
				dtNew.Columns.Add("Exceed");
				dtNew.Columns.Add("PicName",System.Type.GetType("System.String"));
				for ( int i = 0 ; i < dtNew.Rows.Count ; i++)
				{
					dtNew.Rows[i]["StatusName"] = (dtNew.Rows[i]["Status"] == System.DBNull.Value)?"":ComSource.GetTaskStatusName(dtNew.Rows[i]["Status"].ToString());
					dtNew.Rows[i]["ImportantName"] = (dtNew.Rows[i]["ImportantLevel"] == System.DBNull.Value)?"":ComSource.GetImportantName(dtNew.Rows[i]["ImportantLevel"].ToString());
					dtNew.Rows[i]["Master"] = this.GetMaster(dtNew.Rows[i]["WBSCode"].ToString());
					dtNew.Rows[i]["Exceed"] = "0";
					if (dtNew.Rows[i]["PlannedStartDate"] != System.DBNull.Value && dtNew.Rows[i]["Status"].ToString() == "0") // 未开始的工作延期
					{
						dtNew.Rows[i]["Exceed"] = (DateTime.Now.Date.Subtract(DateTime.Parse(dtNew.Rows[i]["PlannedStartDate"].ToString())).Days> 0)?"1":"0";
					}
					if (dtNew.Rows[i]["PlannedFinishDate"] != System.DBNull.Value && dtNew.Rows[i]["Status"].ToString() != "4" && dtNew.Rows[i]["Exceed"].ToString() != "1")  // 非完成的工作，非延期时再次判断
					{
						dtNew.Rows[i]["Exceed"] = (DateTime.Now.Date.Subtract(DateTime.Parse(dtNew.Rows[i]["PlannedFinishDate"].ToString())).Days > 0)?"1":"0";
					}
					dtNew.Rows[i]["PicName"] = this.GetStatusImg(dtNew.Rows[i]["Status"].ToString())+"&nbsp;&nbsp;"+dtNew.Rows[i]["SortID"].ToString()+"&nbsp;&nbsp;"+dtNew.Rows[i]["TaskName"].ToString();
				}
				return dtNew;
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				return null;
			}
		}

		/// <summary>
		/// 获取相关人员列表
		/// </summary>
		/// <param name="WBSCode">工作项编码</param>
		private string GetMaster(string strWBSCode)
		{
			string strMaster = "";
			try
			{
				EntityData entityUser = WBSDAO.GetTaskPersonByWBSCode(strWBSCode);
				if (entityUser.HasRecord())
				{
					DataTable dtUserNew = entityUser.CurrentTable.Copy();					
					for (int i = 0; i < dtUserNew.Rows.Count; i++)
					{
						if (dtUserNew.Rows[i]["Type"].ToString() == "2") // 负责
						{
							strMaster +=(strMaster == "")?"":",";
							strMaster += BLL.SystemRule.GetUserName(dtUserNew.Rows[i]["UserCode"].ToString());
						}					
					}
				}
				entityUser.Dispose();
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"获取人员列表失败");
			}
			return strMaster;
		}
		private string GetStatusImg(string strVal)
		{
			switch(strVal)
			{
				case "0":
					return "<img src=\"../Images/icon_unbegin.gif\" title=\"未开始工作\" border=0>";
				case "1":
					return "<img src=\"../Images/icon_going.gif\" title=\"进行中工作\" border=0>";
				case "2":
					return "<img src=\"../Images/icon_pause.gif\" title=\"已暂停工作\" border=0>";
				case "3":
					return "<img src=\"../Images/icon_cancel.gif\" title=\"已取消工作\" border=0>";
				case "4":
					return "<img src=\"../Images/icon_over.gif\" title=\"已完成工作\" border=0>";
				default:
					return "<img src=\"../Images/icon_unbegin.gif\" title=\"未开始工作\" border=0>";
			}
		}

	}


}
