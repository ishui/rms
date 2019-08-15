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
using RmsPM.BLL;
using RmsPM.DAL.EntityDAO;
using RmsPM.Web;
using Rms.Web;
using RmsPM.Web.Project;
using RmsPM.DAL.QueryStrategy;

namespace RmsPM.Web.Project
{
	/// <summary>
	/// 工作项创建、修改
	/// 2004-11-5：unm 补充：检测用户输入工作编号重复的问题
	/// </summary>
	/// <version>1.1</version>
	/// <modify>
	///		<descrtption>
	///		重写工作项的新增和修改
	///		</descrtption>
	///		<date>2004/11/10</date>
	///		<author>unm</author>
	///		<version>2.0</version>
	/// </modify>
	/// 
	public partial class WBSModify : PageBase
	{
		protected System.Web.UI.WebControls.DropDownList droTaskStatus;
		protected System.Web.UI.WebControls.DropDownList droImportantLevel;
		protected System.Web.UI.WebControls.Button btSelectExecute;
		

		protected void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				//类型
				this.spanRelaName.InnerText = this.txtRelaName.Value;

				if(!this.IsPostBack)
				{
					InitPage();
					LoadData();
				}
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始页面出错：" + ex.Message));
			}
		}

		/// <summary>
		/// 载入初始页面的基本数据
		/// </summary>
		private void InitPage()
		{
			string strWBSCode = Request.QueryString["WBSCode"]+"";
			ViewState["strWBSCode"] = strWBSCode;
			ViewState["ProjectCode"] = Request["ProjectCode"].ToString();
			string strAction = Request.QueryString["Action"]+"";
			ViewState["strAction"] = strAction;

			this.txtProjectCode.Value = (string)ViewState["ProjectCode"];
			this.ucUnit.ProjectCode = (string)ViewState["ProjectCode"];
			this.ucTask.ProjectCode = (string)ViewState["ProjectCode"];
			
			if(strAction == "Insert")
			{			
				// 当Insert时，传入的是父节点的编号
				this.lblName.Text = "插入新工作项";
				
				EntityData entitySort = WBSDAO.GetV_TaskByCode(strWBSCode);				
				this.lblFather.Text = entitySort.GetString("SortID")+"&nbsp;&nbsp;"+ BLL.WBSRule.GetWBSName(strWBSCode);

				// 取得本级SortID然后+5
				DAL.QueryStrategy.WBSStrategyBuilder WSB = new RmsPM.DAL.QueryStrategy.WBSStrategyBuilder();
				WSB.AddStrategy(new Strategy(DAL.QueryStrategy.WBSStrategyName.ProjectCode,(string)ViewState["ProjectCode"]));				
				WSB.AddStrategy(new Strategy(DAL.QueryStrategy.WBSStrategyName.ParentCode,strWBSCode));	
				WSB.AddOrder("SortID", false);
				string sql = WSB.BuildMainQueryString();

				//不要转成整型（溢出） 2004.12.9
				//				sql+=" Order by cast(SortID as int) desc";

				QueryAgent QA = new QueryAgent();
				QA.SetTopNumber(1);
				DataSet dsTask = QA.ExecSqlForDataSet(sql);
				QA.Dispose();
				if(dsTask.Tables.Count>0&&dsTask.Tables[0].Rows.Count>0)
				{
					long intTmp = BLL.ConvertRule.ToLong(dsTask.Tables[0].Rows[0]["SortID"])+1;
					this.txtSortID.Text = "0"+intTmp.ToString();
				}
				else
				{
					//EntityData entitySortID = WBSDAO.GetV_TaskByCode(strWBSCode);
					this.txtSortID.Text = entitySort.GetString("SortID")+"01";
				}
				entitySort.Dispose();
				this.txtCompletePercent.Text = "0";
				this.txtProportion.Value = "1";

				EntityData entityUser = WBSDAO.GetAllTaskPerson();

				//父节点的负责人默认作为子节点的负责人。
				DataView dv1 = entityUser.CurrentTable.DefaultView;
				dv1.RowFilter = " WBSCode='"+strWBSCode+"' and Type='9'"; //{"9","负责"}
				foreach(DataRowView drv in dv1)
				{
					this.txtMaster.Value += (this.txtMaster.Value.Length>0)?",":"";
					this.txtMaster.Value += drv["UserCode"].ToString();					
					this.SelectName9.InnerText += (this.SelectName9.InnerText.Length>0)?",":"";
					this.SelectName9.InnerText += BLL.SystemRule.GetUserName(drv["UserCode"].ToString());
				}				

				//父节点的录入人默认作为子节点的录入人。
				dv1 = entityUser.CurrentTable.DefaultView;
				dv1.RowFilter = " WBSCode='"+strWBSCode+"' and Type='2'"; //{"2","录入"}
				foreach(DataRowView drv in dv1)
				{
					this.txtInputor.Value += (this.txtInputor.Value.Length>0)?",":"";
					this.txtInputor.Value += drv["UserCode"].ToString();					
					this.SelectName2.InnerText += (this.SelectName2.InnerText.Length>0)?",":"";
					this.SelectName2.InnerText += BLL.SystemRule.GetUserName(drv["UserCode"].ToString());
				}				

				entityUser.Dispose();
		
				// 取得父节点的时间为默认开始时间
				EntityData entityDate = WBSDAO.GetTaskByCode(strWBSCode);
				if(entityDate.HasRecord())
				{
					this.dtbPlannedStartDate.Value = entityDate.GetDateTimeOnlyDate("PlannedStartDate");
					this.dtbPlannedFinishDate.Value = entityDate.GetDateTimeOnlyDate("PlannedFinishDate");
				}
				entityDate.Dispose();
				this.dtbActualStartDate.Value = DateTime.Now.ToString("yyyy-MM-dd");

				this.tdProportionText.Visible = false;
				this.tdProportionValue.Visible = false;
			}
			else
			{
				this.txtCompletePercent.ReadOnly = true;
			}

			if(strAction == "Modify")
			{
				// 检查权限
				if(!BLL.WBSRule.IsTaskAccess(ViewState["strWBSCode"].ToString(),user.UserCode))
				{
					Response.Redirect( "../RejectAccess.aspx" );
					Response.End();
				}

				this.lblName.Text = "修改工作信息";
				this.SaveToolsButtonNext.Visible = false;				
				ViewState["FatherCode"] = Request["FatherCode"].ToString();
				if((string)ViewState["FatherCode"]!="")
					this.lblFather.Text = (string)ViewState["FatherCode"]+"&nbsp;&nbsp;"+ BLL.WBSRule.GetWBSName((string)ViewState["FatherCode"]);
				//				if(!user.HasResourceRight(strWBSCode,"070102"))
				//				{
				//					Response.Redirect( "../RejectAccess.aspx" );
				//					Response.End();
				//				}

				
				if(!IsSystemProportion())
				{
					this.tdProportionText.Visible = false;
					this.tdProportionValue.Visible = false;
					this.tdPreProportion.ColSpan = 3;
				}
				
			}
		}

		private void LoadData()
		{
			if(!this.IsPostBack)
			{
				if(ViewState["strAction"].ToString()=="Modify")
				{
					try
					{
						EntityData entityTask=WBSDAO.GetV_TaskByCode(ViewState["strWBSCode"].ToString());
						DataRow drWBS=entityTask.CurrentRow;
						this.txtTaskName.Text = Server.HtmlEncode(drWBS["TaskName"].ToString());
						this.txtSortID.Text = drWBS["SortID"].ToString();
						this.rblImportLevel.SelectedIndex = (drWBS["ImportantLevel"] == System.DBNull.Value)?0:(int.Parse(drWBS["ImportantLevel"].ToString()));
						this.lstTaskStatus.SelectedIndex = (drWBS["Status"] == System.DBNull.Value)?0:(int.Parse(drWBS["Status"].ToString()));
						this.txtCompletePercent.Text = drWBS["CompletePercent"].ToString();
						this.taCancelReason.Value = Server.HtmlEncode(drWBS["CancelReason"].ToString());
						this.taPauseReason.Value = Server.HtmlEncode(drWBS["PauseReason"].ToString());
						this.taTaskDesc.Value = Server.HtmlEncode(drWBS["Remark"].ToString());
						this.dtbPlannedStartDate.Value = (drWBS["PlannedStartDate"] == System.DBNull.Value)?"":drWBS["PlannedStartDate"].ToString();
						this.dtbPlannedFinishDate.Value = (drWBS["PlannedFinishDate"] == System.DBNull.Value)?"":drWBS["PlannedFinishDate"].ToString();
						this.dtbActualStartDate.Value = (drWBS["ActualStartDate"] == System.DBNull.Value)?DateTime.Now.ToString("yyyy-MM-dd"):drWBS["ActualStartDate"].ToString();
						this.dtbActualFinishDate.Value = (drWBS["ActualFinishDate"] == System.DBNull.Value)?"":drWBS["ActualFinishDate"].ToString();
                        this.dtbEarlyFinishDate.Value = BLL.ConvertRule.ToDateString(drWBS["EarlyFinishDate"], "yyyy-MM-dd");
						// 取得前置任务信息
						//						if(entityTask.GetString("PreWBSCode").Length>0)
						//						{
						//							string[] arPreTask =  entityTask.GetString("PreWBSCode").Split(',');
						//							this.txtPreTask.Value =entityTask.GetString("PreWBSCode");
						//							foreach(string tmp in arPreTask)
						//							{
						//								if(tmp.Length==0) continue;
						//								if(this.spPreTask.InnerHtml.Length>0) this.spPreTask.InnerHtml += "&nbsp;&nbsp;";
						//								this.spPreTask.InnerHtml += "<a href=\"javascript:onclick=OpenTask('"+tmp+"')\">"+BLL.WBSRule.GetFieldName(tmp,"SortID")+"&nbsp;"+BLL.WBSRule.GetWBSName(tmp)+"</a>";
						//							}
						//						}
						this.txtRelaType.Value = entityTask.GetString("RelaType");
						this.txtRelaCode.Value = entityTask.GetString("RelaCode");
						this.txtImageFileName.Value = entityTask.GetString("ImageFileName");

						this.ucUnit.Value = entityTask.GetString("Unit");
						this.ucTask.Value = entityTask.GetString("PreWBSCode");
						string tProportion = entityTask.GetDouble("Proportion").ToString();;
						if(tProportion.IndexOf('.')>0&&tProportion.Substring(tProportion.IndexOf('.')).Length>4)
							this.txtProportion.Value = tProportion.Substring(0,tProportion.IndexOf('.')+4);
						else
							this.txtProportion.Value = tProportion;

						entityTask.Dispose();

						LoadUser();
					}
					catch (Exception ex)
					{
						ApplicationLog.WriteLog(this.ToString(),ex,"");
					}
				}
				else
				{
					LoadExecuter();
				}

				//显示类型名称
				this.txtRelaName.Value = BLL.TaskRule.GetTaskRelaName(this.txtRelaType.Value, this.txtRelaCode.Value);
				this.spanRelaName.InnerText = this.txtRelaName.Value;
			}
		}

		private void LoadUser()
		{
			EntityData entityUser = WBSDAO.GetTaskPersonByWBSCode(ViewState["strWBSCode"].ToString());
			if (entityUser.HasRecord())
			{
				DataTable dtUserNew = entityUser.CurrentTable.Copy();
				
				this.txtMaster.Value += "";
				this.txtInputor.Value += "";
				this.txtMonitor.Value += "";
				this.txtExecuter.Value += "";

				for (int i = 0; i < dtUserNew.Rows.Count; i++)
				{
					if(dtUserNew.Rows[i]["RoleType"].ToString()=="0") // 类型为人
					{
						if (dtUserNew.Rows[i]["Type"].ToString() == "9") // 负责
						{   
							if(this.txtMaster.Value.IndexOf(dtUserNew.Rows[i]["UserCode"].ToString())<0)
							{
								this.txtMaster.Value +=(this.txtMaster.Value == "")?"":",";
								this.txtMaster.Value += dtUserNew.Rows[i]["UserCode"].ToString();
								this.SelectName9.InnerText +=(this.SelectName9.InnerText == "")?"":",";
								this.SelectName9.InnerText += BLL.SystemRule.GetUserName(dtUserNew.Rows[i]["UserCode"].ToString());
							}
						}

						if (dtUserNew.Rows[i]["Type"].ToString() == "2") // 录入
						{   
							if(this.txtInputor.Value.IndexOf(dtUserNew.Rows[i]["UserCode"].ToString())<0)
							{
								this.txtInputor.Value +=(this.txtInputor.Value == "")?"":",";
								this.txtInputor.Value += dtUserNew.Rows[i]["UserCode"].ToString();
								this.SelectName2.InnerText +=(this.SelectName2.InnerText == "")?"":",";
								this.SelectName2.InnerText += BLL.SystemRule.GetUserName(dtUserNew.Rows[i]["UserCode"].ToString());
							}
						}

						if (dtUserNew.Rows[i]["Type"].ToString() == "1") // 监督
						{
							if(this.txtMonitor.Value.IndexOf(dtUserNew.Rows[i]["UserCode"].ToString())<0)
							{
								this.txtMonitor.Value +=(this.txtMonitor.Value == "")?"":",";
								this.txtMonitor.Value += dtUserNew.Rows[i]["UserCode"].ToString();
								this.SelectName1.InnerText +=(this.SelectName1.InnerText == "")?"":",";
								this.SelectName1.InnerText += BLL.SystemRule.GetUserName(dtUserNew.Rows[i]["UserCode"].ToString());
							}
						}

						if (dtUserNew.Rows[i]["Type"].ToString() == "0") // 参与
						{
							if(this.txtExecuter.Value.IndexOf(dtUserNew.Rows[i]["UserCode"].ToString())<0)
							{
								this.txtExecuter.Value +=(this.txtExecuter.Value == "")?"":",";
								this.txtExecuter.Value += dtUserNew.Rows[i]["UserCode"].ToString();
								this.SelectName0.InnerText +=(this.SelectName0.InnerText == "")?"":",";
								this.SelectName0.InnerText += BLL.SystemRule.GetUserName(dtUserNew.Rows[i]["UserCode"].ToString());
							}
						}
					}
					if(dtUserNew.Rows[i]["RoleType"].ToString()=="1") // 类型为岗位
					{
						if (dtUserNew.Rows[i]["Type"].ToString() == "9") // 负责
						{  
							if(this.txtMasterStations.Value.IndexOf(dtUserNew.Rows[i]["UserCode"].ToString())<0)
							{
								this.txtMasterStations.Value +=(this.txtMasterStations.Value == "")?"":",";
								this.txtMasterStations.Value += dtUserNew.Rows[i]["UserCode"].ToString();
								this.SelectName9.InnerText +=(this.SelectName9.InnerText == "")?"":",";
								this.SelectName9.InnerText += BLL.SystemRule.GetStationName(dtUserNew.Rows[i]["UserCode"].ToString());
							}
						}

						if (dtUserNew.Rows[i]["Type"].ToString() == "2") // 录入
						{  
							if(this.txtInputorStations.Value.IndexOf(dtUserNew.Rows[i]["UserCode"].ToString())<0)
							{
								this.txtInputorStations.Value +=(this.txtInputorStations.Value == "")?"":",";
								this.txtInputorStations.Value += dtUserNew.Rows[i]["UserCode"].ToString();
								this.SelectName2.InnerText +=(this.SelectName2.InnerText == "")?"":",";
								this.SelectName2.InnerText += BLL.SystemRule.GetStationName(dtUserNew.Rows[i]["UserCode"].ToString());
							}
						}

						if (dtUserNew.Rows[i]["Type"].ToString() == "1") // 监督
						{
							if(this.txtMonitorStations.Value.IndexOf(dtUserNew.Rows[i]["UserCode"].ToString())<0)
							{
								this.txtMonitorStations.Value +=(this.txtMonitorStations.Value == "")?"":",";
								this.txtMonitorStations.Value += dtUserNew.Rows[i]["UserCode"].ToString();
								this.SelectName1.InnerText +=(this.SelectName1.InnerText == "")?"":",";
								this.SelectName1.InnerText += BLL.SystemRule.GetStationName(dtUserNew.Rows[i]["UserCode"].ToString());
							}
						}

						if (dtUserNew.Rows[i]["Type"].ToString() == "0") // 参与
						{
							if(this.txtExecuterStations.Value.IndexOf(dtUserNew.Rows[i]["UserCode"].ToString())<0)
							{
								this.txtExecuterStations.Value +=(this.txtExecuterStations.Value == "")?"":",";
								this.txtExecuterStations.Value += dtUserNew.Rows[i]["UserCode"].ToString();
								this.SelectName0.InnerText +=(this.SelectName0.InnerText == "")?"":",";
								this.SelectName0.InnerText += BLL.SystemRule.GetStationName(dtUserNew.Rows[i]["UserCode"].ToString());
							}
						}
					}
				}
				/*
				// 只有负责人才可以修改进度
				if(this.txtMaster.Value.IndexOf(base.user.UserCode)<0) this.txtCompletePercent.Enabled = false;
				bool isIN = false;
				string strBulidStations = base.user.BuildStationCodes();
				foreach(string str in this.txtMasterStations.Value.Split(','))
				{
					if(strBulidStations.IndexOf(str)>-1)
					{
						isIN = true;break;
					}
				}
				this.txtCompletePercent.Enabled = isIN;
				*/
			}
			entityUser.Dispose();
		}

		/// <summary>
		/// 新增时，加入父节点的参与人为本节点默认参与人
		/// </summary>
		private void LoadExecuter()
		{
			EntityData entityUser = WBSDAO.GetTaskPersonByWBSCode(ViewState["strWBSCode"].ToString());
			if (entityUser.HasRecord())
			{
				DataTable dtUserNew = entityUser.CurrentTable.Copy();
				
				this.txtMaster.Value += "";
				this.txtInputor.Value += "";
				this.txtMonitor.Value += "";
				this.txtExecuter.Value += "";

				for (int i = 0; i < dtUserNew.Rows.Count; i++)
				{
					if(dtUserNew.Rows[i]["RoleType"].ToString()=="0") // 类型为人
					{						
						if (dtUserNew.Rows[i]["Type"].ToString() == "0") // 参与
						{
							if(this.txtExecuter.Value.IndexOf(dtUserNew.Rows[i]["UserCode"].ToString())<0)
							{
								this.txtExecuter.Value +=(this.txtExecuter.Value == "")?"":",";
								this.txtExecuter.Value += dtUserNew.Rows[i]["UserCode"].ToString();
								this.SelectName0.InnerText +=(this.SelectName0.InnerText == "")?"":",";
								this.SelectName0.InnerText += BLL.SystemRule.GetUserName(dtUserNew.Rows[i]["UserCode"].ToString());
							}
						}
					}
					if(dtUserNew.Rows[i]["RoleType"].ToString()=="1") // 类型为岗位
					{						
						if (dtUserNew.Rows[i]["Type"].ToString() == "0") // 参与
						{
							if(this.txtExecuterStations.Value.IndexOf(dtUserNew.Rows[i]["UserCode"].ToString())<0)
							{
								this.txtExecuterStations.Value +=(this.txtExecuterStations.Value == "")?"":",";
								this.txtExecuterStations.Value += dtUserNew.Rows[i]["UserCode"].ToString();
								this.SelectName0.InnerText +=(this.SelectName0.InnerText == "")?"":",";
								this.SelectName0.InnerText += BLL.SystemRule.GetStationName(dtUserNew.Rows[i]["UserCode"].ToString());
							}
						}
					}
				}				
			}
			entityUser.Dispose();
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
		/// 插入新的工作任务
		/// </summary>
		private bool InsertTask()
		{
			if(!this.IsRepeat())
			{
				string strParentFullCode = "";

				EntityData entityTask=WBSDAO.GetTaskByCode((string)ViewState["strWBSCode"]);
				
				//				DAL.QueryStrategy.WBSStrategyBuilder WSB = new RmsPM.DAL.QueryStrategy.WBSStrategyBuilder();
				//				if((string)ViewState["ProjectCode"]!="")
				//					WSB.AddStrategy( new Strategy( DAL.QueryStrategy.WBSStrategyName.ProjectCode,(string)ViewState["ProjectCode"]));
				//				WSB.AddStrategy( new Strategy( DAL.QueryStrategy.WBSStrategyName.WBSCode,(string)ViewState["strWBSCode"]));
				//				string sql = WSB.BuildMainQueryString();
				//				QueryAgent QA = new QueryAgent();
				//				DataSet dsTask = QA.ExecSqlForDataSet(sql);
				//				QA.Dispose();	
				
				string strOutLineNum = "";
				string strDeep = "0";
				DataView parentView=new DataView(entityTask.CurrentTable,"","",DataViewRowState.CurrentRows);
				if(parentView.Count>0)
				{
					strOutLineNum=parentView[0]["OutLineNumber"].ToString();
					strDeep=parentView[0]["deep"].ToString();
					strParentFullCode = parentView[0]["FullCode"].ToString();
					// Insert时WBS是父节点的WBS
					ViewState["strFSDate"] = parentView[0]["PlannedStartDate"].ToString();
					ViewState["strFEDate"] = parentView[0]["PlannedFinishDate"].ToString();					
				}

				DataRow drTask=entityTask.GetNewRecord();

				#region 基本数据处理
				string strNewWBSCode = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("WBS");
				drTask["WBSCode"]=strNewWBSCode;
				drTask["ProjectCode"]=(string)ViewState["ProjectCode"];
				drTask["TaskName"]=StringRule.FormartInput(this.txtTaskName.Text.Trim());
				drTask["SortID"]= this.txtSortID.Text.Trim();
				drTask["Deep"]=int.Parse(strDeep)+1;
				drTask["ParentCode"]=ViewState["strWBSCode"].ToString();
				drTask["CompletePercent"]=(int.Parse(this.txtCompletePercent.Text.Trim())>100)?100:int.Parse(this.txtCompletePercent.Text.Trim());
				drTask["CancelReason"] = StringRule.FormartInput(this.taCancelReason.Value.Trim());
				drTask["PauseReason"] = StringRule.FormartInput(this.taPauseReason.Value.Trim());
				drTask["Remark"] = StringRule.FormartInput(this.taTaskDesc.Value.Trim());
				drTask["Flag"] = 0; // 非根节点，必须设定，模板导入删除时需要
				if ( strParentFullCode == "" )
					drTask["FullCode"] = strNewWBSCode ;
				else
					drTask["FullCode"] = strParentFullCode + "-" + strNewWBSCode;

				if (this.lstTaskStatus.Value != "")
				{
					drTask["Status"] = int.Parse(this.lstTaskStatus.Value);
				}
				else
				{
					drTask["Status"] = 0;
				}
				
				if (this.rblImportLevel.SelectedValue != "")
				{
					drTask["ImportantLevel"] = int.Parse(this.rblImportLevel.SelectedValue);
				}
				else
				{
					drTask["ImportantLevel"] = 0;
				}
				if (this.dtbPlannedStartDate.Value != "")
				{
					if(ViewState["strFSDate"].ToString()!=""&&DateTime.Parse(this.dtbPlannedStartDate.Value.ToString()).CompareTo(DateTime.Parse(ViewState["strFSDate"].ToString()))<0)
					{
						this.JSAction("Alert","开始时间不能在父任务的开始时间之前！");
						return false;
					}
					drTask["PlannedStartDate"] = this.dtbPlannedStartDate.Value.ToString();
				}
				else
				{
					drTask["PlannedStartDate"] = System.DBNull.Value;
				}
				if (this.dtbPlannedFinishDate.Value != "")
				{
					if(ViewState["strFEDate"].ToString()!=""&&DateTime.Parse(this.dtbPlannedFinishDate.Value.ToString()).CompareTo(DateTime.Parse(ViewState["strFEDate"].ToString()))>0)
					{
                        this.JSAction("Alert", "结束时间不能在父任务的结束时间之后！");
						return false;
					}
					drTask["PlannedFinishDate"] = this.dtbPlannedFinishDate.Value.ToString();
				}
				else
				{
					drTask["PlannedFinishDate"] = System.DBNull.Value;
				}

				if (this.dtbActualStartDate.Value != ""&&this.lstTaskStatus.Value=="1")
				{
					drTask["ActualStartDate"] = this.dtbActualStartDate.Value.ToString();
				}
				else
				{
					drTask["ActualStartDate"] = System.DBNull.Value;
				}

				drTask["ActualFinishDate"] = BLL.ConvertRule.ToDate(this.dtbActualFinishDate.Value);
                drTask["EarlyFinishDate"] = BLL.ConvertRule.ToDate(this.dtbEarlyFinishDate.Value);

				// 当前任务的时间间隔
				if (drTask["PlannedStartDate"] != System.DBNull.Value && drTask["PlannedFinishDate"] != System.DBNull.Value)
				{
					DateTime dtFinish = DateTime.Parse(drTask["PlannedFinishDate"].ToString());
					DateTime dtStart = DateTime.Parse(drTask["PlannedStartDate"].ToString());
					System.TimeSpan tsTemp = dtFinish.Subtract(dtStart);
					drTask["Duration"] = tsTemp.Days.ToString();
				}
				else
				{
					drTask["Duration"] = "0";
				}
				
				//类型
				drTask["RelaType"] = this.txtRelaType.Value;
				drTask["RelaCode"] = this.txtRelaCode.Value;

				drTask["ImageFileName"] = this.txtImageFileName.Value;

				drTask["Unit"] = this.ucUnit.Value;
				//if(this.txtPreTask.Value.Length>0)
				drTask["PreWBSCode"] = this.ucTask.Value;

				#endregion 


				entityTask.AddNewRecord(drTask);
				WBSDAO.InsertTask(entityTask);
                //关联父项子项时间
                BLL.WBSRule.UpdateParentTaskData(ViewState["strWBSCode"].ToString(), strNewWBSCode);
				entityTask.Dispose();	

				// 检测进度和状态
				CheckPercent(strNewWBSCode,drTask["CompletePercent"].ToString(),(string)ViewState["ProjectCode"]);

				// 用户和权限的处理
				UserAndRoleProcess(strNewWBSCode);

				return true;				
			}
			else
			{
				this.JSAction("Alert","父级与子级或者同一级下不可出现同名节点！");
				return false;
			}
		}


		/// <summary>
		/// 用户和权限的处理
		/// </summary>
		private void UserAndRoleProcess(string strTWBSCode)
		{
			// 相关人员处理 {"0","参与"},{"1","监督"},{"2","录入"},{"9","负责"}

			// 负责人
			string strMaster = this.txtMaster.Value;
			//strMaster+=","+base.user.UserCode; 不需要添加自己了，要在统一权限处处理
			this.AddUser(strTWBSCode,strMaster,"9");

			// 录入人
			string strInputor = this.txtInputor.Value;
			this.AddUser(strTWBSCode,strInputor,"2");

			// 监督人
			string strMonitor = this.txtMonitor.Value;
			this.AddUser(strTWBSCode,strMonitor,"1");

			// 参与人
			string strExecuter = this.txtExecuter.Value;
			this.AddUser(strTWBSCode,strExecuter,"0");	

			// 相关岗位的处理

			// 负责人
			string strMasterStation = this.txtMasterStations.Value;
			this.AddStation(strTWBSCode,strMasterStation,"9");	

			// 录入人
			string strInputorStation = this.txtInputorStations.Value;
			this.AddStation(strTWBSCode,strInputorStation,"2");	

			// 监督人
			string strMonitorStation = this.txtMonitorStations.Value;
			this.AddStation(strTWBSCode,strMonitorStation,"1");

			// 参与人
			string strExecuterStation = this.txtExecuterStations.Value;
			this.AddStation(strTWBSCode,strExecuterStation,"0");	

			this.SelectName9.InnerText = "";
			this.SelectName2.InnerText = "";
			this.SelectName1.InnerText = "";
			this.SelectName0.InnerText = "";

			//			// 责任人分配任务时加入自己的权限
			//			if(strMaster.Length>0&&strMaster.IndexOf(base.user.UserCode)<0)
			//				strMaster+=","+base.user.UserCode;
			//
			//			//责任人,可添加子工作项、合同关联、文档关联,新增文档、工作报告、相关工作，修改，工作状态维护，添加关注人，可删除子工作项、合同关联、文档、工作报告、相关工作
			//			//监督人,只可添加工作报告和相关文档,工作指示，可修改删除工作项,既然可以删除工作项，那也可以删除工作子项，查看工作项
			//			//参与人,只可添加工作报告和相关文档,查看工作项
			//			ArrayList arOperator = new ArrayList();
			//			this.SaveRS(arOperator,strMaster,strMasterStation,"070101,070102,070103,070104,070105,070107,070108,070109,070111");
			//			this.SaveRS(arOperator,strMonitor,strMonitorStation,"070102,070104,070105,070106,070107");
			//			this.SaveRS(arOperator,strExecuter,strExecuterStation,"070102,070105,070107");
			//			
			//			if(arOperator.Count>0)  
			//				BLL.ResourceRule.SetResourceAccessRange(strTWBSCode,"0701","",arOperator,false);
		}

		/// <summary>
		/// 添加权限资源
		/// </summary>
		private void SaveRS(ArrayList arOperator,string strUser,string strStation,string strOption)
		{		

			if(strUser.Length>0)
			{
				foreach(string strTUser in strUser.Split(','))
				{
					if(strTUser=="") continue;
					AccessRange acRang = new AccessRange();
					acRang.AccessRangeType = 0;
					acRang.RelationCode = strTUser;
					acRang.Operations = strOption;
					arOperator.Add(acRang);
				}
			}			
			if(strStation.Length>0)
			{
				foreach(string strTStation in strStation.Split(','))
				{
					if(strTStation=="") continue;
					AccessRange acRang = new AccessRange();
					acRang.AccessRangeType = 1;
					acRang.RelationCode = strTStation;
					acRang.Operations = strOption;
					arOperator.Add(acRang);
				}
			}
		}

		/// <summary>
		/// 添加工作相关人员
		/// </summary>
		/// <param name="strTWBSCode"></param>
		/// <param name="strUser"></param>
		/// <param name="strUserType"></param>
		private void AddUser(string strTWBSCode,string strUser,string strUserType)
		{
			strUser = BLL.StringRule.CutRepeat(strUser);

			EntityData entity = DAL.EntityDAO.WBSDAO.GetTaskPersonByWBSCode(strTWBSCode);
			DataTable tb = entity.CurrentTable;
			
			DataView dv = entity.CurrentTable.DefaultView;
			dv.RowFilter = " Type = '"+strUserType+"' and isnull(RoleType, 0)=0 and isnull(ExecuteCode, '')=''";// ExecuteCode=''与报告和指示区分开

			string[] arUser = strUser.Split(',');

			//删除原来有现在没有的
			foreach(DataRowView drv in dv)
			{
				DataRow dr = drv.Row;

				string Code = BLL.ConvertRule.ToString(dr["UserCode"]);

				if ((Code == "") || (BLL.ConvertRule.FindArray(arUser, Code) < 0)) 
				{
					dr.Delete();
				}
			}

			//添加
			foreach(string sUser in arUser)
			{
				if (sUser != "")
				{
					string filter = string.Format("Type='{0}' and isnull(RoleType,0)={1} and UserCode='{2}' and isnull(ExecuteCode, '')=''", strUserType, 0, sUser);
					if (tb.Select(filter).Length == 0) 
					{
						DataRow drUser = entity.GetNewRecord();
						User objUser = (User)Session["User"];
						drUser["WBSCode"] = strTWBSCode;
						drUser["TaskPersonCode"] = SystemManageDAO.GetNewSysCode("TaskPerson");
						drUser["UserCode"] = sUser;
						drUser["RoleType"] = "0"; // 0代表人
						drUser["Type"] = strUserType;
						drUser["ExecuteCode"] = "";
						entity.AddNewRecord(drUser);
					}
				}
			}	

			DAL.EntityDAO.WBSDAO.SubmitAllTaskPerson(entity);
			entity.Dispose();
		}

		/// <summary>
		/// 添加工作相关岗位
		/// </summary>
		/// <param name="strTWBSCode"></param>
		/// <param name="strStation"></param>
		/// <param name="strStationType"></param>
		private void AddStation(string strTWBSCode,string strStation,string strUserType)
		{
			strStation = BLL.StringRule.CutRepeat(strStation);

			EntityData entity = DAL.EntityDAO.WBSDAO.GetTaskPersonByWBSCode(strTWBSCode);
			DataTable tb = entity.CurrentTable;
			
			DataView dv = entity.CurrentTable.DefaultView;
			dv.RowFilter = " Type = '"+strUserType+"' and isnull(RoleType, 0)=1 and isnull(ExecuteCode, '')=''";// ExecuteCode=''与报告和指示区分开

			string[] arStation = strStation.Split(',');

			//删除原来有现在没有的
			foreach(DataRowView drv in dv)
			{
				DataRow dr = drv.Row;

				string Code = BLL.ConvertRule.ToString(dr["UserCode"]);

				if ((Code == "") || (BLL.ConvertRule.FindArray(arStation, Code) < 0)) 
				{
					dr.Delete();
				}
			}

			//添加
			foreach(string sStation in arStation)
			{
				if (sStation != "")
				{
					string filter = string.Format("Type='{0}' and isnull(RoleType,0)={1} and UserCode='{2}' and isnull(ExecuteCode, '')=''", strUserType, 1, sStation);
					if (tb.Select(filter).Length == 0) 
					{
						DataRow drStation = entity.GetNewRecord();
						drStation["WBSCode"] = strTWBSCode;
						drStation["TaskPersonCode"] = SystemManageDAO.GetNewSysCode("TaskPerson");
						drStation["UserCode"] = sStation;
						drStation["RoleType"] = "1"; // 1代表角色
						drStation["Type"] = strUserType;
						drStation["ExecuteCode"] = "";
						entity.AddNewRecord(drStation);
					}
				}
			}				

			DAL.EntityDAO.WBSDAO.SubmitAllTaskPerson(entity);
			entity.Dispose();
		}

		//		// 去除重复字串 // 已经移入BLL
		//		private string CutRepeat(string strTmp)
		//		{
		//			if(strTmp.Length<1) return strTmp;
		//			string strOut = "";
		//			string strTmp1 = "";
		//			foreach(string str in strTmp.Split(','))
		//			{
		//				if(str.Length<1) continue;
		//				if(strTmp.IndexOf(',')==0) strTmp=strTmp.Substring(1);
		//				if(strTmp.IndexOf(',')>0) // 未到最后
		//				{
		//					strTmp1 = strTmp.Substring(0,strTmp.IndexOf(','));
		//					strTmp = strTmp.Substring(strTmp.IndexOf(',')+1);
		//					if(strTmp.IndexOf(strTmp1)<0)
		//						strOut+=","+strTmp1;
		//				}
		//				else
		//				{
		//					if(str==strTmp) strOut+=","+str;
		//				}
		//
		//			}
		//			if(strOut.Length<1) return "";
		//			return strOut.Substring(1);
		//		}

		/// <summary>
		/// 检测是否有同名编号或者同名节点
		/// </summary>
		/// <returns></returns>
		private bool IsRepeat()
		{
			DAL.QueryStrategy.WBSStrategyBuilder WSB = new RmsPM.DAL.QueryStrategy.WBSStrategyBuilder();
			WSB.AddStrategy( new Strategy( DAL.QueryStrategy.WBSStrategyName.ProjectCode,(string)ViewState["ProjectCode"]));
			string sql = WSB.BuildMainQueryString();
			sql += " and (WBSCode='"+ViewState["strWBSCode"].ToString()+"' or ParentCode='"+ViewState["strWBSCode"].ToString()+"') ";
			QueryAgent QA = new QueryAgent();
			DataSet dsTask = QA.ExecSqlForDataSet(sql);
			QA.Dispose();	
			DataView parentView = new DataView(dsTask.Tables[0],"","",DataViewRowState.CurrentRows);
			if(parentView.Count>0)
			{
				foreach (DataRowView drv in parentView)
				{
					if (drv["TaskName"].ToString() == this.txtTaskName.Text.Trim())
						return true;
					if (drv["SortID"].ToString() == this.txtSortID.Text.Trim())
						return true;
				}				
				return false;
			}
			else
			{				
				return false;
			}			
		}


		/// <summary>
		/// javascript的处理
		/// </summary>
		/// <param name="strType"></param>
		/// <param name="strVal"></param>
		private void JSAction(string strType,string strVal)
		{
			Response.Write(JavaScript.ScriptStart);
			if(strType=="Alert")			Response.Write("alert('"+strVal+"');");
			if(strType=="Refresh")			Response.Write("window.document.Form1.submit();");
			if(strType=="ParentRefresh")	Response.Write("window.parent.document.Form1.submit();");
			if(strType=="Function")			Response.Write("window.parent."+strVal+"();");
			if(strType=="Close")			Response.Write("window.close();");

			Response.Write(JavaScript.ScriptEnd);
		}



		private bool UpDateTask()
		{
			
			string WBSCode = ViewState["strWBSCode"].ToString();
			EntityData entityTask = RmsPM.DAL.EntityDAO.WBSDAO.GetV_TaskByCode(WBSCode);
			DataRow drTask = entityTask.CurrentRow;

			string strParentCode = drTask["ParentCode"].ToString();

			// 保存修改历史记录
			this.SaveLog(drTask);

			#region 基本数据
			drTask["TaskName"] = StringRule.FormartInput(this.txtTaskName.Text.Trim());
			drTask["CompletePercent"]=(int.Parse(this.txtCompletePercent.Text.Trim())>100)?100:int.Parse(this.txtCompletePercent.Text.Trim());
			drTask["SortID"]=this.txtSortID.Text.Trim();
			drTask["CancelReason"] = StringRule.FormartInput(this.taCancelReason.Value.Trim());
			drTask["PauseReason"] = StringRule.FormartInput(this.taPauseReason.Value.Trim());
			drTask["Remark"] = StringRule.FormartInput(this.taTaskDesc.Value.Trim());
			if (this.lstTaskStatus.Value != "")
			{
				drTask["Status"] = int.Parse(this.lstTaskStatus.Value);
			}
			else
			{
				drTask["Status"] = 0;
			}				

			if (this.rblImportLevel.SelectedValue != "")
			{
				drTask["ImportantLevel"] = int.Parse(this.rblImportLevel.SelectedValue);
			}
			else
			{
				drTask["ImportantLevel"] = 0;
			}
			if (this.dtbPlannedStartDate.Value != "")
			{
				drTask["PlannedStartDate"] = this.dtbPlannedStartDate.Value.ToString();
			}
			else
			{
				drTask["PlannedStartDate"] = System.DBNull.Value;
			}
			if (this.dtbPlannedFinishDate.Value != "")
			{
				drTask["PlannedFinishDate"] = this.dtbPlannedFinishDate.Value.ToString();
			}
			else
			{
				drTask["PlannedFinishDate"] = System.DBNull.Value;
			}
			 //判断需要导入父节点
            string fatherwbscode = Request.QueryString["FatherCode"]+"";
            string strFSDate="";
            string strFEDate = "";
            EntityData fatherentity = RmsPM.DAL.EntityDAO.WBSDAO.GetTaskByCode(fatherwbscode);
            if(fatherentity.HasRecord())
            {
                strFSDate = fatherentity.CurrentRow["PlannedStartDate"].ToString();
                strFEDate = fatherentity.CurrentRow["PlannedFinishDate"].ToString();
			}

            if (this.dtbPlannedStartDate.Value != "" && strFSDate!="")
			{
				if(DateTime.Parse(this.dtbPlannedStartDate.Value.ToString()).CompareTo(DateTime.Parse(strFSDate))<0)
				{
					this.JSAction("Alert","开始时间不能在父任务的开始时间之前！");
					return false;
				}
				drTask["PlannedStartDate"] = this.dtbPlannedStartDate.Value.ToString();
			}
			else if(this.dtbPlannedStartDate.Value=="")
			{
				drTask["PlannedStartDate"] = System.DBNull.Value;
			}
            //if (strFSDate == "")
            //{ 
            //   BLL.WBSRule.UpdateParentTaskData
            //}
            if (this.dtbPlannedFinishDate.Value != "" && strFSDate != "")
			{
				if(DateTime.Parse(this.dtbPlannedFinishDate.Value.ToString()).CompareTo(DateTime.Parse(strFEDate))>0)
				{
                    this.JSAction("Alert", "结束时间不能在父任务的结束时间之后！");
					return false;
				}
				drTask["PlannedFinishDate"] = this.dtbPlannedFinishDate.Value.ToString();
			}
			else if(this.dtbPlannedFinishDate.Value=="")
			{
				drTask["PlannedFinishDate"] = System.DBNull.Value;
			}

			if (this.dtbActualStartDate.Value != "")
			{
				drTask["ActualStartDate"] = this.dtbActualStartDate.Value.ToString();
			}
			else
			{
				drTask["ActualStartDate"] = System.DBNull.Value;
			}

            drTask["ActualFinishDate"] = BLL.ConvertRule.ToDate(this.dtbActualFinishDate.Value);
            drTask["EarlyFinishDate"] = BLL.ConvertRule.ToDate(this.dtbEarlyFinishDate.Value);

			if (drTask["PlannedStartDate"] != System.DBNull.Value && drTask["PlannedFinishDate"] != System.DBNull.Value)
			{
				DateTime dtFinish = DateTime.Parse(drTask["PlannedFinishDate"].ToString());
				DateTime dtStart = DateTime.Parse(drTask["PlannedStartDate"].ToString());
				System.TimeSpan tsTemp = dtFinish.Subtract(dtStart);
				drTask["Duration"] = tsTemp.Days.ToString();
			}
			else
			{
				drTask["Duration"] = "0";
			}
			

			//类型
			drTask["RelaType"] = this.txtRelaType.Value;
			drTask["RelaCode"] = this.txtRelaCode.Value;

			drTask["ImageFileName"] = this.txtImageFileName.Value;

			drTask["Unit"] = this.ucUnit.Value;
			//if(this.txtPreTask.Value.Length>0)
			drTask["PreWBSCode"] = this.ucTask.Value;

			drTask["LastModifyDate"] = DateTime.Now;
			drTask["LastModifyPerson"] = base.user.UserCode;
			if(this.txtProportion.Value.Length>0)
				drTask["Proportion"] = float.Parse(this.txtProportion.Value);

			WBSDAO.UpdateTask(entityTask);
            //关联父项子项时间
            BLL.WBSRule.UpdateParentTaskData(fatherwbscode, WBSCode);


			entityTask.Dispose();

			#endregion 

			// 检测进度和状态
			CheckPercent(ViewState["strWBSCode"].ToString(),this.txtCompletePercent.Text.Trim(),(string)ViewState["ProjectCode"]);

			// 检测权重
			//CheckProportion(ViewState["strWBSCode"].ToString(),this.txtCompletePercent.Text.Trim());
			
			//更新父节点完成进度(递归更新所有父节点)
			BLL.WBSRule.UpdateParentCompletePercent(strParentCode);
		
			// 用户和权限的处理
			UserAndRoleProcess(ViewState["strWBSCode"].ToString());

			//更新相关合同的付款日期
			BLL.ContractRule.UpdateContractPayDateFromTask(WBSCode);

			return true;		
		}

		private void CheckPercent(string strWBSCode,string strPercent,string strProjectCode)
		{			
			WBSStatus myChangeStatus = new WBSStatus();
			
			if(int.Parse(strPercent)>=100)
			{
				// 更新任务状态为完成
				myChangeStatus.FinishProcess(strWBSCode,DateTime.Now.ToString("yyyy-MM-dd"),(string) ViewState["ProjectCode"]);
				return;
			}
			if((this.lstTaskStatus.Value=="0"&&int.Parse(strPercent)>0)||this.lstTaskStatus.Value=="1")
				myChangeStatus.StartProcess(strWBSCode,DateTime.Now.ToString("yyyy-MM-dd"));
			if(this.lstTaskStatus.Value=="2")
				myChangeStatus.PauseProcess(strWBSCode,(string)ViewState["ProjectCode"],this.taPauseReason.Value,DateTime.Now.ToString("yyyy-MM-dd"));
			if(this.lstTaskStatus.Value=="3")
				myChangeStatus.CancelProcess(strWBSCode,this.taCancelReason.Value,DateTime.Now.ToString("yyyy-MM-dd"),strProjectCode);
			if(this.lstTaskStatus.Value=="4")
				myChangeStatus.FinishProcess(strWBSCode,DateTime.Now.ToString("yyyy-MM-dd"),(string) ViewState["ProjectCode"]);
		}

		private void CheckProportion(string strWBSCode,string strPercent)
		{
			
			if(IsSystemProportion())
				// 更新节点进度
				BLL.WBSRule.UpDateProportion(strWBSCode,strPercent);

		}


		/// <summary>
		/// 删除当前任务
		/// </summary>
		private void Delete()
		{
			//首先判断该节点是否存在子节点
			if (!IsHasChild(ViewState["strWBSCode"].ToString()))
			{
				EntityData entityTask = RmsPM.DAL.EntityDAO.WBSDAO.GetTaskByCode(ViewState["strWBSCode"].ToString());
				DAL.EntityDAO.WBSDAO.DeleteTask(entityTask);
			}
			else
				this.JSAction("Alert","包含子节点，无法删除！");
		}

		/// <summary>
		/// 判断该工作项节点是否有子节点
		/// </summary>
		/// <param name="WBSCode">选择的工作项节点编号</param>
		/// <returns></returns>
		private bool IsHasChild(string WBSCode)
		{
			DAL.QueryStrategy.WBSStrategyBuilder WBS = new RmsPM.DAL.QueryStrategy.WBSStrategyBuilder();
			WBS.AddStrategy(new Strategy(DAL.QueryStrategy.WBSStrategyName.ParentCode,ViewState["WBSCode"].ToString()));
			string sql = WBS.BuildMainQueryString();
			QueryAgent QA = new QueryAgent();
			EntityData entityChild = QA.FillEntityData("Task",sql);
			bool m_bFlag = entityChild.HasRecord();
			entityChild.Dispose();
			QA.Dispose();
			return m_bFlag;
		}

		protected void SaveToolsButton_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				if(!this.CheckValue()) return;
				
				//增加新的工作项节点并刷新父窗口列表
				if(ViewState["strAction"].ToString() =="Insert")
				{
					if(this.InsertTask())
					{
						// 刷新父窗口
						Response.Write(JavaScript.ScriptStart);
						Response.Write("window.opener.SelectTask();");

						//刷新父-父窗口
						//Response.Write("if (window.opener.opener) window.opener.opener.location = window.opener.opener.location;");

						Response.Write("window.close();");
						Response.Write(JavaScript.ScriptEnd);
					}
					//					int intTmp = int.Parse(this.txtSortID.Text)+5;
					//					this.txtSortID.Text = "0"+intTmp.ToString();
					//					this.txtMaster.Value = "";
					//					this.txtTaskName.Text = "";
					//					this.txtCompletePercent.Text = "0";
				}
				//更新工作项节点信息并刷新父窗口列表
				if(ViewState["strAction"].ToString() =="Modify")
				{
					if(this.UpDateTask())
					{
						Response.Write(JavaScript.ScriptStart);
						Response.Write("window.opener.SelectTask();");

						//刷新父-父窗口
						//Response.Write("if (window.opener.opener) window.opener.opener.location = window.opener.opener.location;");

						Response.Write("window.close();");
						Response.Write(JavaScript.ScriptEnd);
					}
				}
			}				
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "保存出错：" + ex.Message));
			}
		}

		private bool CheckValue()
		{
			//数据校验
			if (this.txtTaskName.Text.Trim() == "")
			{
				this.JSAction("Alert","必须输入工作项名称！");
				return false;
			}

			/* 不检查，可以为字符 2004.12.9
			if (this.txtSortID.Text.Trim() != "")
			{
				if (!Rms.Check.StringCheck.IsInt(this.txtSortID.Text.Trim()))
				{
					this.JSAction("Alert","工作编号必须输入数字！");
					return false;
				}
			}
			*/

			if (this.txtCompletePercent.Text.Trim() != "")
			{
				if (!Rms.Check.StringCheck.IsInt(this.txtCompletePercent.Text.Trim()))
				{
					this.JSAction("Alert","进度必须输入数字！");
					return false;
				}
			}
			else
			{
				this.JSAction("Alert","进度必须输入数字！");
				return false;
			}

			if (this.dtbPlannedStartDate.Value == "")
			{
				this.JSAction("Alert","必须输入开始时间！");
				return false;
			}

			if (this.dtbPlannedFinishDate.Value != "")
			{
				System.TimeSpan ts = (DateTime.Parse(this.dtbPlannedFinishDate.Value)).Subtract(DateTime.Parse(this.dtbPlannedStartDate.Value));
				if (ts.Days < 0)
				{
					this.JSAction("Alert","计划结束时间不能早于开始时间！");
					return false;
				}
			}

			if (this.dtbActualStartDate.Value != "" && this.dtbActualFinishDate.Value !="")
			{
				System.TimeSpan ts = (DateTime.Parse(this.dtbActualFinishDate.Value)).Subtract(DateTime.Parse(this.dtbActualStartDate.Value));
				if (ts.Days < 0)
				{
					this.JSAction("Alert","实际结束时间不能早于开始时间！");
					return false;
				}
			}
			if(ViewState["strAction"].ToString()=="Modify"&&(int.Parse(this.txtCompletePercent.Text)>=100||this.lstTaskStatus.Value=="4"))
			{
				WBSStatus myChangeStatus = new WBSStatus();
				// 检测子项是否完成
				if(!myChangeStatus.IsAllFinish(ViewState["strWBSCode"].ToString(),(string)ViewState["ProjectCode"])) 
				{
					this.JSAction("Alert","当前任务还有子项没有结束，所以状态不能为完成或者进度不能为100％！");
					return false;
				}
				
				
			}
			return true;
		}


		protected void SaveToolsButtonNext_ServerClick(object sender, System.EventArgs e)
		{
			if(!this.CheckValue()) return ;
			//增加新的工作项节点并刷新父窗口列表
			if(ViewState["strAction"].ToString() =="Insert")
			{
				if(this.InsertTask())
				{
					// 刷新父窗口
					Response.Write(JavaScript.ScriptStart);
					Response.Write("window.opener.SelectTask();");

					//刷新父-父窗口
					Response.Write("if (window.opener.opener) window.opener.opener.location = window.opener.opener.location;");

					Response.Write(JavaScript.ScriptEnd);
				}
				long intTmp = BLL.ConvertRule.ToLong(this.txtSortID.Text)+5;
				this.txtSortID.Text = "0"+intTmp.ToString();
				this.txtCompletePercent.Text = "0";
				this.rblImportLevel.SelectedIndex = 0;
			}
			//更新工作项节点信息并刷新父窗口列表
			if(ViewState["strAction"].ToString() =="Modify")
			{
				if(this.UpDateTask())
				{
					Response.Write(JavaScript.ScriptStart);
					Response.Write("window.opener.SelectTask();");

					//刷新父-父窗口
					Response.Write("if (window.opener.opener) window.opener.opener.location = window.opener.opener.location;");

					Response.Write(JavaScript.ScriptEnd);
				}
			}	
		}

		private void SaveLog(DataRow odr)
		{
			EntityData entity = WBSDAO.GetTaskHistoryByCode(ViewState["strWBSCode"].ToString());
			string maxEdition = "";
			if(entity.HasRecord())
			{
				maxEdition = entity.CurrentTable.Rows[0]["Edition"].ToString();
			}
			maxEdition = (maxEdition.Length>0)?maxEdition:"0";
			int intEdition = int.Parse(maxEdition)+1; // 取得新版本号

			DataRow dr = entity.GetNewRecord();
			dr["WBSCode"] 		=        odr["WBSCode"];
			dr["TaskCode"] 		=        odr["TaskCode"]; 
			dr["TaskName"] 		=        odr["TaskName"]; 
			dr["ProjectCode"] 	=        odr["ProjectCode"]; 
			dr["ParentCode"] 	=        odr["ParentCode"]; 
			dr["PreWBSCode"] 	=        odr["PreWBSCode"]; 
			dr["OutlineNumber"] =        odr["OutlineNumber"]; 
			dr["Deep"] 			=        odr["Deep"]; 
			dr["SortID"] 		=        odr["SortID"]; 
			dr["FullCode"] 		=        odr["FullCode"]; 
			dr["PlannedStartDate"] 	=        odr["PlannedStartDate"]; 
			dr["PlannedFinishDate"] =        odr["PlannedFinishDate"]; 
			dr["ActualStartDate"] 	=        odr["ActualStartDate"]; 
			dr["ActualFinishDate"] 	=        odr["ActualFinishDate"]; 
			dr["EarlyFinishDate"] 	=        odr["EarlyFinishDate"]; 
			dr["EarlyStartDate"] 	=        odr["EarlyStartDate"];
			dr["LastFinishDate"] 	=        odr["LastFinishDate"]; 
			dr["LastStartDate"] 	=        odr["LastStartDate"]; 
			dr["PauseDate"] 		=        odr["PauseDate"]; 
			dr["CancelDate"] 		=        odr["CancelDate"]; 
			dr["CompletePercent"] 	=        odr["CompletePercent"]; 
			dr["RemainingDuration"] =        odr["RemainingDuration"]; 
			dr["Duration"] 			=        odr["Duration"]; 
			dr["ImportantLevel"] 	=        odr["ImportantLevel"]; 
			dr["Status"] 			=        odr["Status"]; 
			dr["PreStatus"] 		=        odr["PreStatus"]; 
			dr["PauseReason"] 		=        odr["PauseReason"]; 
			dr["CancelReason"] 		=        odr["CancelReason"]; 
			dr["Remark"] 			=        odr["Remark"]; 
			dr["Flag"] 				=        odr["Flag"]; 
			dr["RelaType"] 			=        odr["RelaType"]; 
			dr["RelaCode"] 			=        odr["RelaCode"]; 
			dr["ImageFileName"] 	=        odr["ImageFileName"]; 
			dr["Unit"] 				=        odr["Unit"]; 
			dr["LastModifyDate"] 	=        DateTime.Now;
			dr["LastModifyPerson"] 	=        base.user.UserCode; 
			dr["Edition"] 			=        intEdition.ToString();
			dr["Master"] 			=        BLL.StringRule.CutRepeat(this.txtMaster.Value)+":"+BLL.StringRule.CutRepeat(this.txtMasterStations.Value); 
			dr["Inputor"] 			=        BLL.StringRule.CutRepeat(this.txtInputor.Value)+":"+BLL.StringRule.CutRepeat(this.txtInputorStations.Value); 
			dr["Monitor"] 			=        BLL.StringRule.CutRepeat(this.txtMonitor.Value)+":"+BLL.StringRule.CutRepeat(this.txtMonitorStations.Value); 
			dr["Executer"] 			=        BLL.StringRule.CutRepeat(this.txtExecuter.Value)+":"+BLL.StringRule.CutRepeat(this.txtExecuterStations.Value); 

			entity.AddNewRecord(dr);
			WBSDAO.InsertTaskHistory(entity);			
			entity.Dispose();	
		}

		private bool IsSystemProportion()
		{
			bool isSystemProportion = true;// 取得系统设定
			ProjectConfigStrategyBuilder sb = new ProjectConfigStrategyBuilder();
			sb.AddStrategy( new Strategy( ProjectConfigStrategyName.ProjectCode , (string)ViewState["ProjectCode"] ) );
			string sql = sb.BuildMainQueryString();
			QueryAgent qa = new QueryAgent();
			EntityData projectConfig = qa.FillEntityData( "ProjectConfig",sql );
			qa.Dispose();
				
			DataRow[] drSelects = projectConfig.CurrentTable.Select( String.Format(  " ConfigName='Proportion'" ));
			if ( drSelects.Length>0)
			{
				if ( !drSelects[0].IsNull("ConfigData"))
					if((string)drSelects[0]["ConfigData"]=="1")
						isSystemProportion = false;
			}
			projectConfig.Dispose();

			return isSystemProportion;
		}
	}

	
}
