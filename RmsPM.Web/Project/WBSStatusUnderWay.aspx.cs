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
	/// WBSStatusTask : ���������������Ŀ����ɸѡ�б�
	/// </summary>
	/// <author>unm</author>
	/// <date>2004/11/10</date>
	/// <version>1.5</version>
	public partial class WBSStatusUnderWay : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlInputHidden TaskStatus;
		protected System.Web.UI.HtmlControls.HtmlSelect lstStatus;
		protected System.Web.UI.HtmlControls.HtmlInputHidden TaskExceed;


		protected void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				if(!this.IsPostBack)
				{
					LoadData();				
				}
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"��������ɸѡ�б�ʧ��");
			}
		}

		private void LoadData()
		{
			try
			{
				ViewState["ProjectCode"] = Request["ProjectCode"].ToString();

				this.lstImportantLevel.SelectedIndex = (Request["lstImportantLevel"]+"" == "")?0:(int.Parse(Request["lstImportantLevel"]+"") + 1);

				DAL.QueryStrategy.WBSStrategyBuilder WSB = new RmsPM.DAL.QueryStrategy.WBSStrategyBuilder();
				ArrayList arA = new ArrayList();
				arA.Add("070107");
				arA.Add(base.user.UserCode);
				WSB.AddStrategy( new Strategy( DAL.QueryStrategy.WBSStrategyName.AccessRange,arA));
				if((string)ViewState["ProjectCode"]!="")
					WSB.AddStrategy( new Strategy( DAL.QueryStrategy.WBSStrategyName.ProjectCode,(string)ViewState["ProjectCode"]));
				WSB.AddOrder(" PlannedStartDate ",false);				
				
				WSB.AddStrategy( new Strategy( DAL.QueryStrategy.WBSStrategyName.Status,"1"));
				if(this.txtTaskName.Value.Length>0)
				{
					WSB.AddStrategy( new Strategy( DAL.QueryStrategy.WBSStrategyName.TaskName,this.txtTaskName.Value));
					WSB.AddStrategy( new Strategy( DAL.QueryStrategy.WBSStrategyName.CodeLike,this.txtTaskName.Value));
				}
				if(this.txtMaster.Value.Length>0)
					WSB.AddStrategy( new Strategy( DAL.QueryStrategy.WBSStrategyName.Master,this.txtMaster.Value));
				if(this.lstImportantLevel.Value.Length>0)
					WSB.AddStrategy( new Strategy( DAL.QueryStrategy.WBSStrategyName.ImportantLevel,this.lstImportantLevel.Value));
				
				if(this.dtbStartFromDate.Value!=""||this.dtbStartToDate.Value!="")
				{
					ArrayList arB = new ArrayList();
					arB.Add(this.dtbStartFromDate.Value);
					arB.Add(this.dtbStartToDate.Value);
					WSB.AddStrategy( new Strategy( DAL.QueryStrategy.WBSStrategyName.PlannedStartDate,arB));
				}
				if(this.dtbEndFromDate.Value!=""||this.dtbEndToDate.Value!="")
				{
					ArrayList arB = new ArrayList();
					arB.Add(this.dtbEndFromDate.Value);
					arB.Add(this.dtbEndToDate.Value);
					WSB.AddStrategy( new Strategy( DAL.QueryStrategy.WBSStrategyName.PlannedStartDate,arB));
				}
				string sql = WSB.BuildMainQueryString();
				QueryAgent QA = new QueryAgent();
				DataSet dsTask = QA.ExecSqlForDataSet(sql);
				DataTable dbTable = DisposeTask(dsTask.Tables[0]);
				QA.Dispose();			

				DataView dvTask = new DataView(dbTable,"","",System.Data.DataViewRowState.CurrentRows);
				this.trNoTask.Visible = (dvTask.Count>0)?false:true;
				this.dgTaskList.DataSource = dvTask;
				this.dgTaskList.DataBind();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
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
			this.dgTaskList.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.dgTaskList_SortCommand);

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
				dtNew.Columns.Add("PicName",System.Type.GetType("System.String"));
				for ( int i = 0 ; i < dtNew.Rows.Count ; i++)
				{
					dtNew.Rows[i]["StatusName"] = (dtNew.Rows[i]["Status"] == System.DBNull.Value)?"":ComSource.GetTaskStatusName(dtNew.Rows[i]["Status"].ToString());
					dtNew.Rows[i]["ImportantName"] = (dtNew.Rows[i]["ImportantLevel"] == System.DBNull.Value)?"":ComSource.GetImportantName(dtNew.Rows[i]["ImportantLevel"].ToString());
					dtNew.Rows[i]["Master"] = this.GetMaster(dtNew.Rows[i]["WBSCode"].ToString());
					//dtNew.Rows[i]["PicName"] = this.GetStatusImg(dtNew.Rows[i]["Status"].ToString())+"&nbsp;&nbsp;"+dtNew.Rows[i]["SortID"].ToString()+"&nbsp;&nbsp;"+BLL.StringRule.TruncText(dtNew.Rows[i]["TaskName"].ToString(),15);
					dtNew.Rows[i]["PicName"] = this.GetStatusImg(dtNew.Rows[i]["Status"].ToString())+"&nbsp;&nbsp;"+BLL.StringRule.TruncText(dtNew.Rows[i]["TaskName"].ToString(),15);
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
		/// ��ȡ�����Ա�б�
		/// </summary>
		/// <param name="WBSCode">���������</param>
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
						if(dtUserNew.Rows[i]["RoleType"].ToString()=="0") // ����Ϊ��
						{
							if (dtUserNew.Rows[i]["Type"].ToString() == "2") // ����
							{
								strMaster +=(strMaster == "")?"":",";
								strMaster += BLL.SystemRule.GetUserName(dtUserNew.Rows[i]["UserCode"].ToString());
							}
						}
						if(dtUserNew.Rows[i]["RoleType"].ToString()=="1") // ����Ϊ��λ
						{
							if (dtUserNew.Rows[i]["Type"].ToString() == "2") // ����
							{
								strMaster +=(strMaster == "")?"":",";
								strMaster += BLL.SystemRule.GetStationName(dtUserNew.Rows[i]["UserCode"].ToString());
							}
						}

					}
				}
				entityUser.Dispose();
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"��ȡ��Ա�б�ʧ��");
			}
			return strMaster;
		}
		private string GetStatusImg(string strVal)
		{
			switch(strVal)
			{
				case "0":
					return "<img src=\"../Images/icon_unbegin.gif\" title=\"δ��ʼ����\" border=0>";
				case "1":
					return "<img src=\"../Images/icon_going.gif\" title=\"�����й���\" border=0>";
				case "2":
					return "<img src=\"../Images/icon_pause.gif\" title=\"����ͣ����\" border=0>";
				case "3":
					return "<img src=\"../Images/icon_cancel.gif\" title=\"��ȡ������\" border=0>";
				case "4":
					return "<img src=\"../Images/icon_over.gif\" title=\"����ɹ���\" border=0>";
				default:
					return "<img src=\"../Images/icon_unbegin.gif\" title=\"δ��ʼ����\" border=0>";
			}
		}

		protected void btnSearch_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				LoadData();		
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"��������ɸѡ�б�ʧ��");
			}
		}

	}


}
