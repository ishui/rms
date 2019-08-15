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
using RmsPM.DAL.QueryStrategy;
using RmsPM.DAL.EntityDAO;
using Rms.ORMap;
using Rms.Web;

namespace RmsPM.Web.Project
{
	/// <summary>
	/// WBSBatchModify ��ժҪ˵����
	/// </summary>
	public partial class WBSBatchModify : PageBase
	{
		private string txtMaster = "";
		private string txtMonitor = "";
		private string txExecuter = "";
		private string txtMasterStations = ""; 
		private string txtMonitorStations = "" ;
		private string txtExecuterStations = "";
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			try
			{
				if(!this.IsPostBack)
				{
					this.InitPage();
					this.LoadData();
				}
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��" + ex.Message));
			}
		}

		private void InitPage()
		{
			ViewState["ProjectCode"] = Request["ProjectCode"].ToString();
			ViewState["SelectCode"] = Request["SelectCode"].ToString();
		}

		private void LoadData()
		{	
			DAL.QueryStrategy.WBSStrategyBuilder WSB = new RmsPM.DAL.QueryStrategy.WBSStrategyBuilder();
			if((string)ViewState["ProjectCode"]!="")
				WSB.AddStrategy( new Strategy( DAL.QueryStrategy.WBSStrategyName.ProjectCode,(string)ViewState["ProjectCode"]));
			WSB.AddStrategy( new Strategy( DAL.QueryStrategy.WBSStrategyName.ParentCode,(string)ViewState["SelectCode"]));
			WSB.AddOrder(" PlannedStartDate ",false);			
			string sql = WSB.BuildMainQueryString();
			QueryAgent QA = new QueryAgent();
			DataSet dsTask = QA.ExecSqlForDataSet(sql);
			QA.Dispose();		
			this.dgDetailData.DataSource = dsTask;
			this.dgDetailData.DataBind();


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
		/// ���水ť�¼�(�����޸�)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnSave_ServerClick(object sender, System.EventArgs e)
		{
			try
			{				
				EntityData entity;
				// ��⵱ǰ�ڵ��Ƿ������ӽڵ㶼�Ѿ���ɡ�
				string noFinishID = CheckFinish();
				if(noFinishID.Length>0)
				{
					entity = WBSDAO.GetTaskByCode(noFinishID);
					Response.Write(Rms.Web.JavaScript.Alert(true,"[ "+entity.GetString("TaskName")+" ]�����ӽڵ��ڽ����л�����ͣ��û�н��������Ƚ����ӽڵ�")); 
					return ;
				}

				for(int i=0;i<this.dgDetailData.Items.Count;i++)
				{
					string txtTaskName = ((TextBox)this.dgDetailData.Items[i].FindControl("txtTaskName")).Text.Trim();
					string txtPercent = ((TextBox)this.dgDetailData.Items[i].FindControl("txtPercent")).Text.Trim();
					string txtProportion = ((TextBox)this.dgDetailData.Items[i].FindControl("txtProportion")).Text.Trim();
					string dtbPlannedStartDate =  ((AspWebControl.Calendar)this.dgDetailData.Items[i].FindControl("dtbPlannedStartDate")).Value;
					string dtbPlannedFinishDate = ((AspWebControl.Calendar)this.dgDetailData.Items[i].FindControl("dtbPlannedFinishDate")).Value;
					string dtbActualStartDate =   ((AspWebControl.Calendar)this.dgDetailData.Items[i].FindControl("dtbActualStartDate")).Value;
					string dtbActualFinishDate =  ((AspWebControl.Calendar)this.dgDetailData.Items[i].FindControl("dtbActualFinishDate")).Value;

				
					string Code = this.dgDetailData.DataKeys[i].ToString();
					entity = WBSDAO.GetTaskByCode(Code);
					DataRow dr = entity.CurrentRow;

					// �����޸ļ�¼
					this.SaveLog(dr,Code);

					// �����ȣ�����״̬
					this.CheckPercent(Code,txtPercent);

					dr["TaskName"] = txtTaskName;
					dr["CompletePercent"] = BLL.ConvertRule.ToInt(txtPercent);
					dr["Proportion"] = BLL.ConvertRule.ToInt(txtProportion);
					if(dtbPlannedStartDate.Length>0)
						dr["PlannedStartDate"] = dtbPlannedStartDate;
					else
						dr["PlannedStartDate"] = System.DBNull.Value;
					if(dtbPlannedFinishDate.Length>0)
						dr["PlannedFinishDate"] = dtbPlannedFinishDate;
					else
						dr["PlannedFinishDate"] = System.DBNull.Value;
					if(dtbActualStartDate.Length>0)
						dr["ActualStartDate"] = dtbActualStartDate;
					else
						dr["ActualStartDate"] = System.DBNull.Value;
					if(dtbActualFinishDate.Length>0)
						dr["ActualFinishDate"] = dtbActualFinishDate;
					else
						dr["ActualFinishDate"] = System.DBNull.Value;
					WBSDAO.UpdateTask(entity);
					entity.Dispose();


				}

                //���¸��ڵ���ɽ���(�ݹ�������и��ڵ�) xyq 2006.7.18 add
                BLL.WBSRule.UpdateParentCompletePercent((string)ViewState["SelectCode"]);
                
                //ˢ�¸�����
				Response.Write(JavaScript.ScriptStart);
				Response.Write("window.opener.SelectTask();");
				Response.Write("window.close();");
				Response.Write(JavaScript.ScriptEnd);

			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��" + ex.Message));
			}
		}

		// ��⵱ǰ�ڵ��Ƿ������ӽڵ㶼�Ѿ���ɡ�
		private string CheckFinish()
		{
			for(int i=0;i<this.dgDetailData.Items.Count;i++)
			{
				string txtPercent = ((TextBox)this.dgDetailData.Items[i].FindControl("txtPercent")).Text.Trim();
				// û�е�100������Ҫ�������
				if(int.Parse(txtPercent)<100)
					return "";
				string Code = this.dgDetailData.DataKeys[i].ToString();
				WBSStatus myStatus = new WBSStatus();	
				if(!myStatus.IsAllFinish(Code,(string) ViewState["ProjectCode"]))
					return Code;
			}
			return "";
		}

		private void CheckPercent(string strWBSCode,string strPercent)
		{				
			if(int.Parse(strPercent)>=100)
			{
				WBSStatus myChangeStatus = new WBSStatus();			
				// ��������״̬Ϊ���
				myChangeStatus.FinishProcess(strWBSCode,DateTime.Now.ToString("yyyy-MM-dd"),(string) ViewState["ProjectCode"]);
			}			
		}

		private void SaveLog(DataRow odr,string code)
		{
			EntityData entity = WBSDAO.GetTaskHistoryByCode(code);
			string maxEdition = "";
			if(entity.HasRecord())
			{
				maxEdition = entity.CurrentTable.Rows[0]["Edition"].ToString();
			}
			maxEdition = (maxEdition.Length>0)?maxEdition:"0";
			int intEdition = int.Parse(maxEdition)+1; // ȡ���°汾��

			// ������Ա��Ϣ
			this.GetPerson(code);

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
			dr["Master"] 			=        BLL.StringRule.CutRepeat(this.txtMaster)+":"+BLL.StringRule.CutRepeat(this.txtMasterStations); 
			dr["Monitor"] 			=        BLL.StringRule.CutRepeat(this.txtMonitor)+":"+BLL.StringRule.CutRepeat(this.txtMonitorStations); 
			dr["Executer"] 			=        BLL.StringRule.CutRepeat(this.txExecuter)+":"+BLL.StringRule.CutRepeat(this.txtExecuterStations); 

			entity.AddNewRecord(dr);
			WBSDAO.InsertTaskHistory(entity);			
			entity.Dispose();	
		}

		private void GetPerson(string code)
		{
			EntityData entityUser = WBSDAO.GetTaskPersonByWBSCode(code);
			if (entityUser.HasRecord())
			{
				DataTable dtUserNew = entityUser.CurrentTable;
				
				this.txtMaster = "";
				this.txtMonitor = "";
				this.txExecuter = "";
				txtMasterStations = "";
				txtMonitorStations = "";
				txtExecuterStations = "";

				for (int i = 0; i < dtUserNew.Rows.Count; i++)
				{
					if(dtUserNew.Rows[i]["RoleType"].ToString()=="0") // ����Ϊ��
					{
						if (dtUserNew.Rows[i]["Type"].ToString() == "2") // ����
						{   
							if(this.txtMaster.IndexOf(dtUserNew.Rows[i]["UserCode"].ToString())<0)
							{
								this.txtMaster +=(this.txtMaster == "")?"":",";
								this.txtMaster += dtUserNew.Rows[i]["UserCode"].ToString();
							}
						}
						if (dtUserNew.Rows[i]["Type"].ToString() == "1") // �ල
						{
							if(this.txtMonitor.IndexOf(dtUserNew.Rows[i]["UserCode"].ToString())<0)
							{
								this.txtMonitor +=(this.txtMonitor == "")?"":",";
								this.txtMonitor += dtUserNew.Rows[i]["UserCode"].ToString();
							}
						}
						if (dtUserNew.Rows[i]["Type"].ToString() == "0") // ����
						{
							if(this.txExecuter.IndexOf(dtUserNew.Rows[i]["UserCode"].ToString())<0)
							{
								this.txExecuter +=(this.txExecuter == "")?"":",";
								this.txExecuter += dtUserNew.Rows[i]["UserCode"].ToString();
							}
						}
					}
					if(dtUserNew.Rows[i]["RoleType"].ToString()=="1") // ����Ϊ��λ
					{
						if (dtUserNew.Rows[i]["Type"].ToString() == "2") // ����
						{  
							if(this.txtMasterStations.IndexOf(dtUserNew.Rows[i]["UserCode"].ToString())<0)
							{
								this.txtMasterStations +=(this.txtMasterStations == "")?"":",";
								this.txtMasterStations += dtUserNew.Rows[i]["UserCode"].ToString();
							}
						}
						if (dtUserNew.Rows[i]["Type"].ToString() == "1") // �ල
						{
							if(this.txtMonitorStations.IndexOf(dtUserNew.Rows[i]["UserCode"].ToString())<0)
							{
								this.txtMonitorStations +=(this.txtMonitorStations == "")?"":",";
								this.txtMonitorStations += dtUserNew.Rows[i]["UserCode"].ToString();
							}
						}
						if (dtUserNew.Rows[i]["Type"].ToString() == "0") // ����
						{
							if(this.txtExecuterStations.IndexOf(dtUserNew.Rows[i]["UserCode"].ToString())<0)
							{
								this.txtExecuterStations +=(this.txtExecuterStations == "")?"":",";
								this.txtExecuterStations += dtUserNew.Rows[i]["UserCode"].ToString();
							}
						}
					}
				}			
			}
			entityUser.Dispose();
		}

	}
}
