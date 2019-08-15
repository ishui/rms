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
using RmsPM.DAL.QueryStrategy;

namespace RmsPM.Web.SelectBox
{
	/// <summary>
	/// SelectTask 的摘要说明。
	/// </summary>
	public partial class SelectTask : PageBase
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				// 在此处放置用户代码以初始化页面
				ViewState["ProjectCode"] = Request["ProjectCode"].ToString()+"";
				if (Request["Flag"]+"" == "1")
				{
					this.dgTaskList.Columns.Remove(this.dgTaskList.Columns[dgTaskList.Columns.Count-1]);
					this.tbButton.Visible = false;
				}
				if (!this.IsPostBack)
				{
					//返回函数名
					string ReturnFunc = Request.QueryString["ReturnFunc"] + "";
					if (ReturnFunc == "") 
					{
						ReturnFunc = "SelectTaskReturn";
					}
					ViewState["ReturnFunc"] = ReturnFunc;

					this.dtbPlannedFinishFromDate.Value = "";
					this.dtbPlannedFinishToDate.Value = "";
					this.dtbPlannedStartFromDate.Value = "";
					this.dtbPlannedStartToDate.Value = "";
					LoadData();

					ViewState["SelectCode"] = "";
					ViewState["SelectName"] = "";
				}
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
			this.dgTaskList.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgTaskList_PageIndexChanged);

		}
		#endregion

		private void LoadData()
		{
			if(!this.IsPostBack)
			{
				//初始化“合同类型”下拉框
				int StatusIndex = this.SelectStatus.SelectedIndex;
				this.SelectStatus.Items.Add(new ListItem("未开始","0"));
				this.SelectStatus.Items.Add(new ListItem("进行中","1"));
				this.SelectStatus.Items.Add(new ListItem("暂停","2"));
				this.SelectStatus.Items.Add(new ListItem("中断","3"));
				this.SelectStatus.Items.Add(new ListItem("继续","4"));
				this.SelectStatus.SelectedIndex = StatusIndex;
			}
			//初始化符合条件的数据列表
			string TaskName = "";
			string Master = "";
			string PlannedStartFromDate = "";
			string PlannedStartToDate = "";
			string PlannedFinishFromDate = "";
			string PlannedFinishToDate ="";
			string Status;
			
			if (this.txtTaskName.Value.Trim() != "")
			{
				TaskName = this.txtTaskName.Value.Trim();
			}
			if (this.txtMaster.Value.Trim() != "")
			{
				Master = this.txtMaster.Value.Trim();
			}
			Status = this.SelectStatus.Value;
			PlannedStartFromDate = this.dtbPlannedStartFromDate.Value;
			PlannedStartToDate = this.dtbPlannedStartToDate.Value;
			PlannedFinishFromDate = this.dtbPlannedFinishFromDate.Value;
			PlannedFinishToDate = this.dtbPlannedFinishToDate.Value;
			
			try
			{
				WBSStrategyBuilder WSB = new WBSStrategyBuilder();
				ArrayList arA = new ArrayList();
				arA.Add("070107");
				arA.Add(user.UserCode);
				WSB.AddStrategy( new Strategy( DAL.QueryStrategy.WBSStrategyName.AccessRange,arA));
				WSB.AddStrategy( new Strategy( DAL.QueryStrategy.WBSStrategyName.ProjectCode,(string)ViewState["ProjectCode"]));
				if (TaskName.Length > 0)
				{
					WSB.AddStrategy( new Strategy (RmsPM.DAL.QueryStrategy.WBSStrategyName.TaskName,TaskName));
				}
				if (Master.Length > 0 )
				{
					WSB.AddStrategy( new Strategy (RmsPM.DAL.QueryStrategy.WBSStrategyName.Master,Master));
				}
				if ( Status.Length > 0 )
				{
					WSB.AddStrategy( new Strategy (RmsPM.DAL.QueryStrategy.WBSStrategyName.Status,Status));
				}
				if ( PlannedStartFromDate.Length > 0 || PlannedStartToDate.Length >0)
				{
					ArrayList arParam = new ArrayList();
					arParam.Add(PlannedStartFromDate);
					arParam.Add(PlannedStartToDate);

					WSB.AddStrategy( new Strategy (RmsPM.DAL.QueryStrategy.WBSStrategyName.PlannedStartDate,arParam));
				}
				if ( PlannedFinishFromDate.Length > 0 || PlannedFinishToDate.Length >0)
				{
					ArrayList arParam = new ArrayList();
					arParam.Add(PlannedFinishFromDate);
					arParam.Add(PlannedFinishToDate);

					WSB.AddStrategy( new Strategy (RmsPM.DAL.QueryStrategy.WBSStrategyName.PlannedFinishDate,arParam));
				}					
				WSB.AddOrder("PlannedStartDate",false);
				string Sql = WSB.BuildMainQueryString();
				QueryAgent QA = new QueryAgent();
				DataSet ds = QA.ExecSqlForDataSet(Sql);
				QA.Dispose();
				
//				string WBSCode = Request["WBSCode"] + "";// 新权限不需过虑了   1.25
//				string strRowFilter = (WBSCode == "")?" Flag = 0 ":" FullCode NOT LIKE '*" + WBSCode + "*' ";
//				DataView dv = new DataView(ds.Tables[0],strRowFilter,"Deep",DataViewRowState.CurrentRows);
				DataView dv = new DataView(ds.Tables[0],"","Deep",DataViewRowState.CurrentRows);

				dv.Table.Columns.Add("myTaskName",System.Type.GetType("System.String"));
				dv.Table.Columns.Add("StatusName",System.Type.GetType("System.String"));
				dv.Table.Columns.Add("Master",System.Type.GetType("System.String"));
				EntityData entityUser = new EntityData("TaskPerson");
				foreach(DataRowView drv in dv)
				{
					drv["StatusName"] = BLL.ComSource.GetTaskStatusName(drv["Status"].ToString());
					drv["myTaskName"] = this.GetStatusImg(drv["Status"].ToString())+"&nbsp;"+drv["SortID"].ToString()+"&nbsp;"+drv["TaskName"].ToString();
					entityUser = WBSDAO.GetTaskPersonByWBSCode(drv["WBSCode"].ToString());
					string strTUser = "";// 取得当前任务负责人					
					for (int j = 0; j < entityUser.CurrentTable.Rows.Count; j++)
					{
						if (entityUser.CurrentTable.Rows[j]["Type"].ToString() == "2") // 负责
						{
							strTUser +=(strTUser == "")?"":",";
							strTUser = BLL.SystemRule.GetUserName(entityUser.CurrentTable.Rows[j]["UserCode"].ToString());
						}						
					}					
					drv["Master"] = strTUser;
				}
				this.dgTaskList.DataSource = dv;
				this.dgTaskList.DataBind();
			}
			catch ( Exception ex)
			{
				ApplicationLog.WriteLog ( this.ToString(),ex,"载入工作项列表失败");
			}
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


		protected void btnSearch_ServerClick(object sender, System.EventArgs e)
		{
			this.dgTaskList.CurrentPageIndex = 0;
			LoadData();
		}

		/// <summary>
		/// 翻页事件
		/// </summary>
		/// <param name="source"></param>
		/// <param name="e"></param>
		private void dgTaskList_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			System.Web.UI.WebControls.CheckBox chkWBS ;
			try
			{
				StringBuilder strBuilder = new StringBuilder();
				StringBuilder strBuilderName = new StringBuilder();
				foreach(DataGridItem oDataGridItem in this.dgTaskList.Items)
				{
					chkWBS = (CheckBox)oDataGridItem.FindControl("checkTask");
					if (chkWBS.Checked == true)
					{
						strBuilder.Append(this.dgTaskList.DataKeys[oDataGridItem.ItemIndex].ToString());
						strBuilder.Append(",");
						System.Web.UI.HtmlControls.HtmlAnchor anchor = (System.Web.UI.HtmlControls.HtmlAnchor)this.dgTaskList.Items[oDataGridItem.ItemIndex].FindControl("taskName");
						strBuilderName.Append(anchor.Attributes["name"]);
						strBuilderName.Append(",");
					}					
				}
				ViewState["SelectCode"] = ViewState["SelectCode"]+","+strBuilder.ToString();
				ViewState["SelectName"] = ViewState["SelectName"]+","+strBuilderName.ToString();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"保存相关工作项失败");
			}

			this.dgTaskList.CurrentPageIndex = e.NewPageIndex;
			LoadData();

			try
			{				
				int i=0;
				foreach(DataGridItem oDataGridItem in this.dgTaskList.Items)
				{
					chkWBS = (CheckBox)oDataGridItem.FindControl("checkTask");
					if (ViewState["SelectCode"].ToString().IndexOf(this.dgTaskList.DataKeys[i].ToString())>0)
						chkWBS.Checked = true;
					i++;			
				}				
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"保存相关工作项失败");
			}
		}

		/// <summary>
		///返回选中工作项的编码字符串，以","分隔
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void SaveToolsButton_ServerClick(object sender, System.EventArgs e)
		{
			System.Web.UI.WebControls.CheckBox chkWBS ;
			StringBuilder strBuilder = new StringBuilder();
			StringBuilder strBuilderName = new StringBuilder();

			try
			{
				foreach(DataGridItem oDataGridItem in this.dgTaskList.Items)
				{
					chkWBS = (CheckBox)oDataGridItem.FindControl("checkTask");
					if (chkWBS.Checked == true)
					{
						strBuilder.Append(this.dgTaskList.DataKeys[oDataGridItem.ItemIndex].ToString());
						strBuilder.Append(",");
						System.Web.UI.HtmlControls.HtmlAnchor anchor = (System.Web.UI.HtmlControls.HtmlAnchor)this.dgTaskList.Items[oDataGridItem.ItemIndex].FindControl("taskName");
						strBuilderName.Append(anchor.Attributes["name"]);
						strBuilderName.Append(",");
					}					
				}
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"保存相关工作项失败");
			}

			string Code = (string)ViewState["SelectCode"]+","+strBuilder.ToString();
			string Name = (string)ViewState["SelectName"]+","+strBuilderName.ToString();
			if (Code.Length > 0)
			{
				Code = Code.Substring(0,Code.Length - 1);
				Response.Write(JavaScript.ScriptStart);
				Response.Write("window.opener.SelectTaskReturn('" + CutRepeat(Code) + "','"+CutRepeat(Name)+"');");
				Response.Write("window.close();");
				Response.Write(JavaScript.ScriptEnd);
			}
			else
			{
				Response.Write(JavaScript.ScriptStart);
				Response.Write("window.close();");
				Response.Write(JavaScript.ScriptEnd);
			}
		}

		// 去除重复字串
		private string CutRepeat(string strTmp)
		{
			if(strTmp.Length<1) return strTmp;
			string strOut = "";
			string strTmp1 = "";
			foreach(string str in strTmp.Split(','))
			{
				if(str.Length<1) continue;
				if(strTmp.IndexOf(',')==0) strTmp=strTmp.Substring(1);
				if(strTmp.IndexOf(',')>0) // 未到最后
				{
					strTmp1 = strTmp.Substring(0,strTmp.IndexOf(','));
					strTmp = strTmp.Substring(strTmp.IndexOf(',')+1);
					if(strTmp.IndexOf(strTmp1)<0)
						strOut+=","+strTmp1;
				}
				else
				{
					if(str==strTmp) strOut+=","+str;
				}

			}
			if(strOut.Length<1) return "";
			return strOut.Substring(1);
		}

	}
}
