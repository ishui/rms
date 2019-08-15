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
	/// ����������޸�
	/// 2004-11-5��unm ���䣺����û����빤������ظ�������
	/// </summary>
	/// <version>1.1</version>
	/// <modify>
	///		<descrtption>
	///		��д��������������޸�
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
				//����
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
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼҳ�����" + ex.Message));
			}
		}

		/// <summary>
		/// �����ʼҳ��Ļ�������
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
				// ��Insertʱ��������Ǹ��ڵ�ı��
				this.lblName.Text = "�����¹�����";
				
				EntityData entitySort = WBSDAO.GetV_TaskByCode(strWBSCode);				
				this.lblFather.Text = entitySort.GetString("SortID")+"&nbsp;&nbsp;"+ BLL.WBSRule.GetWBSName(strWBSCode);

				// ȡ�ñ���SortIDȻ��+5
				DAL.QueryStrategy.WBSStrategyBuilder WSB = new RmsPM.DAL.QueryStrategy.WBSStrategyBuilder();
				WSB.AddStrategy(new Strategy(DAL.QueryStrategy.WBSStrategyName.ProjectCode,(string)ViewState["ProjectCode"]));				
				WSB.AddStrategy(new Strategy(DAL.QueryStrategy.WBSStrategyName.ParentCode,strWBSCode));	
				WSB.AddOrder("SortID", false);
				string sql = WSB.BuildMainQueryString();

				//��Ҫת�����ͣ������ 2004.12.9
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

				//���ڵ�ĸ�����Ĭ����Ϊ�ӽڵ�ĸ����ˡ�
				DataView dv1 = entityUser.CurrentTable.DefaultView;
				dv1.RowFilter = " WBSCode='"+strWBSCode+"' and Type='9'"; //{"9","����"}
				foreach(DataRowView drv in dv1)
				{
					this.txtMaster.Value += (this.txtMaster.Value.Length>0)?",":"";
					this.txtMaster.Value += drv["UserCode"].ToString();					
					this.SelectName9.InnerText += (this.SelectName9.InnerText.Length>0)?",":"";
					this.SelectName9.InnerText += BLL.SystemRule.GetUserName(drv["UserCode"].ToString());
				}				

				//���ڵ��¼����Ĭ����Ϊ�ӽڵ��¼���ˡ�
				dv1 = entityUser.CurrentTable.DefaultView;
				dv1.RowFilter = " WBSCode='"+strWBSCode+"' and Type='2'"; //{"2","¼��"}
				foreach(DataRowView drv in dv1)
				{
					this.txtInputor.Value += (this.txtInputor.Value.Length>0)?",":"";
					this.txtInputor.Value += drv["UserCode"].ToString();					
					this.SelectName2.InnerText += (this.SelectName2.InnerText.Length>0)?",":"";
					this.SelectName2.InnerText += BLL.SystemRule.GetUserName(drv["UserCode"].ToString());
				}				

				entityUser.Dispose();
		
				// ȡ�ø��ڵ��ʱ��ΪĬ�Ͽ�ʼʱ��
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
				// ���Ȩ��
				if(!BLL.WBSRule.IsTaskAccess(ViewState["strWBSCode"].ToString(),user.UserCode))
				{
					Response.Redirect( "../RejectAccess.aspx" );
					Response.End();
				}

				this.lblName.Text = "�޸Ĺ�����Ϣ";
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
						// ȡ��ǰ��������Ϣ
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

				//��ʾ��������
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
					if(dtUserNew.Rows[i]["RoleType"].ToString()=="0") // ����Ϊ��
					{
						if (dtUserNew.Rows[i]["Type"].ToString() == "9") // ����
						{   
							if(this.txtMaster.Value.IndexOf(dtUserNew.Rows[i]["UserCode"].ToString())<0)
							{
								this.txtMaster.Value +=(this.txtMaster.Value == "")?"":",";
								this.txtMaster.Value += dtUserNew.Rows[i]["UserCode"].ToString();
								this.SelectName9.InnerText +=(this.SelectName9.InnerText == "")?"":",";
								this.SelectName9.InnerText += BLL.SystemRule.GetUserName(dtUserNew.Rows[i]["UserCode"].ToString());
							}
						}

						if (dtUserNew.Rows[i]["Type"].ToString() == "2") // ¼��
						{   
							if(this.txtInputor.Value.IndexOf(dtUserNew.Rows[i]["UserCode"].ToString())<0)
							{
								this.txtInputor.Value +=(this.txtInputor.Value == "")?"":",";
								this.txtInputor.Value += dtUserNew.Rows[i]["UserCode"].ToString();
								this.SelectName2.InnerText +=(this.SelectName2.InnerText == "")?"":",";
								this.SelectName2.InnerText += BLL.SystemRule.GetUserName(dtUserNew.Rows[i]["UserCode"].ToString());
							}
						}

						if (dtUserNew.Rows[i]["Type"].ToString() == "1") // �ල
						{
							if(this.txtMonitor.Value.IndexOf(dtUserNew.Rows[i]["UserCode"].ToString())<0)
							{
								this.txtMonitor.Value +=(this.txtMonitor.Value == "")?"":",";
								this.txtMonitor.Value += dtUserNew.Rows[i]["UserCode"].ToString();
								this.SelectName1.InnerText +=(this.SelectName1.InnerText == "")?"":",";
								this.SelectName1.InnerText += BLL.SystemRule.GetUserName(dtUserNew.Rows[i]["UserCode"].ToString());
							}
						}

						if (dtUserNew.Rows[i]["Type"].ToString() == "0") // ����
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
					if(dtUserNew.Rows[i]["RoleType"].ToString()=="1") // ����Ϊ��λ
					{
						if (dtUserNew.Rows[i]["Type"].ToString() == "9") // ����
						{  
							if(this.txtMasterStations.Value.IndexOf(dtUserNew.Rows[i]["UserCode"].ToString())<0)
							{
								this.txtMasterStations.Value +=(this.txtMasterStations.Value == "")?"":",";
								this.txtMasterStations.Value += dtUserNew.Rows[i]["UserCode"].ToString();
								this.SelectName9.InnerText +=(this.SelectName9.InnerText == "")?"":",";
								this.SelectName9.InnerText += BLL.SystemRule.GetStationName(dtUserNew.Rows[i]["UserCode"].ToString());
							}
						}

						if (dtUserNew.Rows[i]["Type"].ToString() == "2") // ¼��
						{  
							if(this.txtInputorStations.Value.IndexOf(dtUserNew.Rows[i]["UserCode"].ToString())<0)
							{
								this.txtInputorStations.Value +=(this.txtInputorStations.Value == "")?"":",";
								this.txtInputorStations.Value += dtUserNew.Rows[i]["UserCode"].ToString();
								this.SelectName2.InnerText +=(this.SelectName2.InnerText == "")?"":",";
								this.SelectName2.InnerText += BLL.SystemRule.GetStationName(dtUserNew.Rows[i]["UserCode"].ToString());
							}
						}

						if (dtUserNew.Rows[i]["Type"].ToString() == "1") // �ල
						{
							if(this.txtMonitorStations.Value.IndexOf(dtUserNew.Rows[i]["UserCode"].ToString())<0)
							{
								this.txtMonitorStations.Value +=(this.txtMonitorStations.Value == "")?"":",";
								this.txtMonitorStations.Value += dtUserNew.Rows[i]["UserCode"].ToString();
								this.SelectName1.InnerText +=(this.SelectName1.InnerText == "")?"":",";
								this.SelectName1.InnerText += BLL.SystemRule.GetStationName(dtUserNew.Rows[i]["UserCode"].ToString());
							}
						}

						if (dtUserNew.Rows[i]["Type"].ToString() == "0") // ����
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
				// ֻ�и����˲ſ����޸Ľ���
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
		/// ����ʱ�����븸�ڵ�Ĳ�����Ϊ���ڵ�Ĭ�ϲ�����
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
					if(dtUserNew.Rows[i]["RoleType"].ToString()=="0") // ����Ϊ��
					{						
						if (dtUserNew.Rows[i]["Type"].ToString() == "0") // ����
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
					if(dtUserNew.Rows[i]["RoleType"].ToString()=="1") // ����Ϊ��λ
					{						
						if (dtUserNew.Rows[i]["Type"].ToString() == "0") // ����
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


		#region Web ������������ɵĴ���
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: �õ����� ASP.NET Web ���������������ġ�
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion


		/// <summary>
		/// �����µĹ�������
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
					// InsertʱWBS�Ǹ��ڵ��WBS
					ViewState["strFSDate"] = parentView[0]["PlannedStartDate"].ToString();
					ViewState["strFEDate"] = parentView[0]["PlannedFinishDate"].ToString();					
				}

				DataRow drTask=entityTask.GetNewRecord();

				#region �������ݴ���
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
				drTask["Flag"] = 0; // �Ǹ��ڵ㣬�����趨��ģ�嵼��ɾ��ʱ��Ҫ
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
						this.JSAction("Alert","��ʼʱ�䲻���ڸ�����Ŀ�ʼʱ��֮ǰ��");
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
                        this.JSAction("Alert", "����ʱ�䲻���ڸ�����Ľ���ʱ��֮��");
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

				// ��ǰ�����ʱ����
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
				
				//����
				drTask["RelaType"] = this.txtRelaType.Value;
				drTask["RelaCode"] = this.txtRelaCode.Value;

				drTask["ImageFileName"] = this.txtImageFileName.Value;

				drTask["Unit"] = this.ucUnit.Value;
				//if(this.txtPreTask.Value.Length>0)
				drTask["PreWBSCode"] = this.ucTask.Value;

				#endregion 


				entityTask.AddNewRecord(drTask);
				WBSDAO.InsertTask(entityTask);
                //������������ʱ��
                BLL.WBSRule.UpdateParentTaskData(ViewState["strWBSCode"].ToString(), strNewWBSCode);
				entityTask.Dispose();	

				// �����Ⱥ�״̬
				CheckPercent(strNewWBSCode,drTask["CompletePercent"].ToString(),(string)ViewState["ProjectCode"]);

				// �û���Ȩ�޵Ĵ���
				UserAndRoleProcess(strNewWBSCode);

				return true;				
			}
			else
			{
				this.JSAction("Alert","�������Ӽ�����ͬһ���²��ɳ���ͬ���ڵ㣡");
				return false;
			}
		}


		/// <summary>
		/// �û���Ȩ�޵Ĵ���
		/// </summary>
		private void UserAndRoleProcess(string strTWBSCode)
		{
			// �����Ա���� {"0","����"},{"1","�ල"},{"2","¼��"},{"9","����"}

			// ������
			string strMaster = this.txtMaster.Value;
			//strMaster+=","+base.user.UserCode; ����Ҫ����Լ��ˣ�Ҫ��ͳһȨ�޴�����
			this.AddUser(strTWBSCode,strMaster,"9");

			// ¼����
			string strInputor = this.txtInputor.Value;
			this.AddUser(strTWBSCode,strInputor,"2");

			// �ල��
			string strMonitor = this.txtMonitor.Value;
			this.AddUser(strTWBSCode,strMonitor,"1");

			// ������
			string strExecuter = this.txtExecuter.Value;
			this.AddUser(strTWBSCode,strExecuter,"0");	

			// ��ظ�λ�Ĵ���

			// ������
			string strMasterStation = this.txtMasterStations.Value;
			this.AddStation(strTWBSCode,strMasterStation,"9");	

			// ¼����
			string strInputorStation = this.txtInputorStations.Value;
			this.AddStation(strTWBSCode,strInputorStation,"2");	

			// �ල��
			string strMonitorStation = this.txtMonitorStations.Value;
			this.AddStation(strTWBSCode,strMonitorStation,"1");

			// ������
			string strExecuterStation = this.txtExecuterStations.Value;
			this.AddStation(strTWBSCode,strExecuterStation,"0");	

			this.SelectName9.InnerText = "";
			this.SelectName2.InnerText = "";
			this.SelectName1.InnerText = "";
			this.SelectName0.InnerText = "";

			//			// �����˷�������ʱ�����Լ���Ȩ��
			//			if(strMaster.Length>0&&strMaster.IndexOf(base.user.UserCode)<0)
			//				strMaster+=","+base.user.UserCode;
			//
			//			//������,������ӹ������ͬ�������ĵ�����,�����ĵ����������桢��ع������޸ģ�����״̬ά������ӹ�ע�ˣ���ɾ���ӹ������ͬ�������ĵ����������桢��ع���
			//			//�ල��,ֻ����ӹ������������ĵ�,����ָʾ�����޸�ɾ��������,��Ȼ����ɾ���������Ҳ����ɾ����������鿴������
			//			//������,ֻ����ӹ������������ĵ�,�鿴������
			//			ArrayList arOperator = new ArrayList();
			//			this.SaveRS(arOperator,strMaster,strMasterStation,"070101,070102,070103,070104,070105,070107,070108,070109,070111");
			//			this.SaveRS(arOperator,strMonitor,strMonitorStation,"070102,070104,070105,070106,070107");
			//			this.SaveRS(arOperator,strExecuter,strExecuterStation,"070102,070105,070107");
			//			
			//			if(arOperator.Count>0)  
			//				BLL.ResourceRule.SetResourceAccessRange(strTWBSCode,"0701","",arOperator,false);
		}

		/// <summary>
		/// ���Ȩ����Դ
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
		/// ��ӹ��������Ա
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
			dv.RowFilter = " Type = '"+strUserType+"' and isnull(RoleType, 0)=0 and isnull(ExecuteCode, '')=''";// ExecuteCode=''�뱨���ָʾ���ֿ�

			string[] arUser = strUser.Split(',');

			//ɾ��ԭ��������û�е�
			foreach(DataRowView drv in dv)
			{
				DataRow dr = drv.Row;

				string Code = BLL.ConvertRule.ToString(dr["UserCode"]);

				if ((Code == "") || (BLL.ConvertRule.FindArray(arUser, Code) < 0)) 
				{
					dr.Delete();
				}
			}

			//���
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
						drUser["RoleType"] = "0"; // 0������
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
		/// ��ӹ�����ظ�λ
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
			dv.RowFilter = " Type = '"+strUserType+"' and isnull(RoleType, 0)=1 and isnull(ExecuteCode, '')=''";// ExecuteCode=''�뱨���ָʾ���ֿ�

			string[] arStation = strStation.Split(',');

			//ɾ��ԭ��������û�е�
			foreach(DataRowView drv in dv)
			{
				DataRow dr = drv.Row;

				string Code = BLL.ConvertRule.ToString(dr["UserCode"]);

				if ((Code == "") || (BLL.ConvertRule.FindArray(arStation, Code) < 0)) 
				{
					dr.Delete();
				}
			}

			//���
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
						drStation["RoleType"] = "1"; // 1�����ɫ
						drStation["Type"] = strUserType;
						drStation["ExecuteCode"] = "";
						entity.AddNewRecord(drStation);
					}
				}
			}				

			DAL.EntityDAO.WBSDAO.SubmitAllTaskPerson(entity);
			entity.Dispose();
		}

		//		// ȥ���ظ��ִ� // �Ѿ�����BLL
		//		private string CutRepeat(string strTmp)
		//		{
		//			if(strTmp.Length<1) return strTmp;
		//			string strOut = "";
		//			string strTmp1 = "";
		//			foreach(string str in strTmp.Split(','))
		//			{
		//				if(str.Length<1) continue;
		//				if(strTmp.IndexOf(',')==0) strTmp=strTmp.Substring(1);
		//				if(strTmp.IndexOf(',')>0) // δ�����
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
		/// ����Ƿ���ͬ����Ż���ͬ���ڵ�
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
		/// javascript�Ĵ���
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

			// �����޸���ʷ��¼
			this.SaveLog(drTask);

			#region ��������
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
			 //�ж���Ҫ���븸�ڵ�
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
					this.JSAction("Alert","��ʼʱ�䲻���ڸ�����Ŀ�ʼʱ��֮ǰ��");
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
                    this.JSAction("Alert", "����ʱ�䲻���ڸ�����Ľ���ʱ��֮��");
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
			

			//����
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
            //������������ʱ��
            BLL.WBSRule.UpdateParentTaskData(fatherwbscode, WBSCode);


			entityTask.Dispose();

			#endregion 

			// �����Ⱥ�״̬
			CheckPercent(ViewState["strWBSCode"].ToString(),this.txtCompletePercent.Text.Trim(),(string)ViewState["ProjectCode"]);

			// ���Ȩ��
			//CheckProportion(ViewState["strWBSCode"].ToString(),this.txtCompletePercent.Text.Trim());
			
			//���¸��ڵ���ɽ���(�ݹ�������и��ڵ�)
			BLL.WBSRule.UpdateParentCompletePercent(strParentCode);
		
			// �û���Ȩ�޵Ĵ���
			UserAndRoleProcess(ViewState["strWBSCode"].ToString());

			//������غ�ͬ�ĸ�������
			BLL.ContractRule.UpdateContractPayDateFromTask(WBSCode);

			return true;		
		}

		private void CheckPercent(string strWBSCode,string strPercent,string strProjectCode)
		{			
			WBSStatus myChangeStatus = new WBSStatus();
			
			if(int.Parse(strPercent)>=100)
			{
				// ��������״̬Ϊ���
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
				// ���½ڵ����
				BLL.WBSRule.UpDateProportion(strWBSCode,strPercent);

		}


		/// <summary>
		/// ɾ����ǰ����
		/// </summary>
		private void Delete()
		{
			//�����жϸýڵ��Ƿ�����ӽڵ�
			if (!IsHasChild(ViewState["strWBSCode"].ToString()))
			{
				EntityData entityTask = RmsPM.DAL.EntityDAO.WBSDAO.GetTaskByCode(ViewState["strWBSCode"].ToString());
				DAL.EntityDAO.WBSDAO.DeleteTask(entityTask);
			}
			else
				this.JSAction("Alert","�����ӽڵ㣬�޷�ɾ����");
		}

		/// <summary>
		/// �жϸù�����ڵ��Ƿ����ӽڵ�
		/// </summary>
		/// <param name="WBSCode">ѡ��Ĺ�����ڵ���</param>
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
				
				//�����µĹ�����ڵ㲢ˢ�¸������б�
				if(ViewState["strAction"].ToString() =="Insert")
				{
					if(this.InsertTask())
					{
						// ˢ�¸�����
						Response.Write(JavaScript.ScriptStart);
						Response.Write("window.opener.SelectTask();");

						//ˢ�¸�-������
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
				//���¹�����ڵ���Ϣ��ˢ�¸������б�
				if(ViewState["strAction"].ToString() =="Modify")
				{
					if(this.UpDateTask())
					{
						Response.Write(JavaScript.ScriptStart);
						Response.Write("window.opener.SelectTask();");

						//ˢ�¸�-������
						//Response.Write("if (window.opener.opener) window.opener.opener.location = window.opener.opener.location;");

						Response.Write("window.close();");
						Response.Write(JavaScript.ScriptEnd);
					}
				}
			}				
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "�������" + ex.Message));
			}
		}

		private bool CheckValue()
		{
			//����У��
			if (this.txtTaskName.Text.Trim() == "")
			{
				this.JSAction("Alert","�������빤�������ƣ�");
				return false;
			}

			/* ����飬����Ϊ�ַ� 2004.12.9
			if (this.txtSortID.Text.Trim() != "")
			{
				if (!Rms.Check.StringCheck.IsInt(this.txtSortID.Text.Trim()))
				{
					this.JSAction("Alert","������ű����������֣�");
					return false;
				}
			}
			*/

			if (this.txtCompletePercent.Text.Trim() != "")
			{
				if (!Rms.Check.StringCheck.IsInt(this.txtCompletePercent.Text.Trim()))
				{
					this.JSAction("Alert","���ȱ����������֣�");
					return false;
				}
			}
			else
			{
				this.JSAction("Alert","���ȱ����������֣�");
				return false;
			}

			if (this.dtbPlannedStartDate.Value == "")
			{
				this.JSAction("Alert","�������뿪ʼʱ�䣡");
				return false;
			}

			if (this.dtbPlannedFinishDate.Value != "")
			{
				System.TimeSpan ts = (DateTime.Parse(this.dtbPlannedFinishDate.Value)).Subtract(DateTime.Parse(this.dtbPlannedStartDate.Value));
				if (ts.Days < 0)
				{
					this.JSAction("Alert","�ƻ�����ʱ�䲻�����ڿ�ʼʱ�䣡");
					return false;
				}
			}

			if (this.dtbActualStartDate.Value != "" && this.dtbActualFinishDate.Value !="")
			{
				System.TimeSpan ts = (DateTime.Parse(this.dtbActualFinishDate.Value)).Subtract(DateTime.Parse(this.dtbActualStartDate.Value));
				if (ts.Days < 0)
				{
					this.JSAction("Alert","ʵ�ʽ���ʱ�䲻�����ڿ�ʼʱ�䣡");
					return false;
				}
			}
			if(ViewState["strAction"].ToString()=="Modify"&&(int.Parse(this.txtCompletePercent.Text)>=100||this.lstTaskStatus.Value=="4"))
			{
				WBSStatus myChangeStatus = new WBSStatus();
				// ��������Ƿ����
				if(!myChangeStatus.IsAllFinish(ViewState["strWBSCode"].ToString(),(string)ViewState["ProjectCode"])) 
				{
					this.JSAction("Alert","��ǰ����������û�н���������״̬����Ϊ��ɻ��߽��Ȳ���Ϊ100����");
					return false;
				}
				
				
			}
			return true;
		}


		protected void SaveToolsButtonNext_ServerClick(object sender, System.EventArgs e)
		{
			if(!this.CheckValue()) return ;
			//�����µĹ�����ڵ㲢ˢ�¸������б�
			if(ViewState["strAction"].ToString() =="Insert")
			{
				if(this.InsertTask())
				{
					// ˢ�¸�����
					Response.Write(JavaScript.ScriptStart);
					Response.Write("window.opener.SelectTask();");

					//ˢ�¸�-������
					Response.Write("if (window.opener.opener) window.opener.opener.location = window.opener.opener.location;");

					Response.Write(JavaScript.ScriptEnd);
				}
				long intTmp = BLL.ConvertRule.ToLong(this.txtSortID.Text)+5;
				this.txtSortID.Text = "0"+intTmp.ToString();
				this.txtCompletePercent.Text = "0";
				this.rblImportLevel.SelectedIndex = 0;
			}
			//���¹�����ڵ���Ϣ��ˢ�¸������б�
			if(ViewState["strAction"].ToString() =="Modify")
			{
				if(this.UpDateTask())
				{
					Response.Write(JavaScript.ScriptStart);
					Response.Write("window.opener.SelectTask();");

					//ˢ�¸�-������
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
			int intEdition = int.Parse(maxEdition)+1; // ȡ���°汾��

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
			bool isSystemProportion = true;// ȡ��ϵͳ�趨
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
