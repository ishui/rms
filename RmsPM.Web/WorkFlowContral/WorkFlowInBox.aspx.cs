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
using RmsPM.BLL;
using RmsPM.DAL.EntityDAO;
using RmsPM.DAL.QueryStrategy;
using RmsPM.Web;
using Rms.ORMap;
using Rms.WorkFlow;
using System.Collections.Generic;


namespace RmsPM.Web.WorkFlowContral
{
	/// <summary>
	/// 
	/// </summary>
	public partial class WorkFlowInBox :PageBase
	{

	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			if(!IsPostBack)
			{
				IniPage();
				BuildSqlString();
				LoadData();
			}
		}
		private void IniPage()
		{
			try
			{
				EntityData entity = DAL.EntityDAO.WorkFlowDAO.GetAllWorkFlowProcedure();
				int iCount = entity.CurrentTable.Rows.Count;
                string tempProcedureName = "";

                List<string> ProcedureNameList = new List<string>();

				for ( int i=0;i<iCount;i++)
				{
					entity.SetCurrentRow(i);
                    string Activity = entity.GetInt("Activity").ToString();
                    if (Activity == "0")
                    {
                        continue;
                    }
                    string ProcedureName = entity.GetString("ProcedureName");
                    if (!ProcedureNameList.Contains(ProcedureName))
                    {
                        this.sltProcedure.Items.Add(new ListItem(entity.GetString("description"), entity.GetString("ProcedureName")));
                        ProcedureNameList.Add(ProcedureName);
                    }
				}
				entity.Dispose();
				if(Request["ProcedureName"] != null)
				{
					this.sltProcedure.SelectedIndex = this.sltProcedure.Items.IndexOf(this.sltProcedure.Items.FindByValue(Request["ProcedureName"].ToString()));
				}

                this.DropDownProject.DataSource = new DataView(user.m_EntityDataAccessProject.CurrentTable, "", "ProjectName", DataViewRowState.CurrentRows);
                this.DropDownProject.DataTextField = "ProjectShortName";
                this.DropDownProject.DataValueField = "ProjectCode";
                this.DropDownProject.DataBind();
                ListItem li = new ListItem("--������Ŀ--", "");
                this.DropDownProject.Items.Add(li);
                this.DropDownProject.SelectedIndex = this.DropDownProject.Items.IndexOf(this.DropDownProject.Items.FindByValue(""));

			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}

		private void LoadData()
		{
			try
			{
                //temp,����Ҫ��ʱ��Ͻ� update by kenny 20070209
                string company = System.Configuration.ConfigurationManager.AppSettings["PMName"].ToLower();
                switch (company)
                {
                    case "tianyangoa":
                        this.dgList.Columns[7].Visible = true;
                        break;
                    default:
                        this.dgList.Columns[7].Visible = false;
                        break;
                }
				string sql = (string)this.ViewState["SqlString"];
				QueryAgent qa = new QueryAgent();
				EntityData entity = qa.FillEntityData( "WorkFlowAct",sql );
				qa.Dispose();
				this.dgList.DataSource = entity.CurrentTable;
				this.dgList.DataBind();
				this.gpControl.RowsCount = entity.CurrentTable.Rows.Count.ToString();
				entity.Dispose();
				
			}
			catch( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}


		private void BuildSqlString()
		{
			WorkFlowActStrategyBuilder sb = new WorkFlowActStrategyBuilder();
			sb.AddStrategy( new Strategy( WorkFlowActStrategyName.Status,"'DealWith'" ));
			sb.AddStrategy( new Strategy( WorkFlowActStrategyName.ActUserCode,user.UserCode ));
			
            if (this.sltProcedure.Value != "")
                sb.AddStrategy(new Strategy(WorkFlowActStrategyName.ProcedureCodeIn, BLL.WorkFlowRule.GetProcedureCodeListByName(this.sltProcedure.Value)));
            if (this.txtCaseCode.Value != "")
                sb.AddStrategy(new Strategy(WorkFlowActStrategyName.FlowNumber, this.txtCaseCode.Value));
            if (this.txtTaskName.Value != "")
                sb.AddStrategy(new Strategy(WorkFlowActStrategyName.CurrentTaskName, this.txtTaskName.Value));
            if (this.txtTitle.Value != "")
                sb.AddStrategy(new Strategy(WorkFlowActStrategyName.Title, this.txtTitle.Value));
            if (this.DropDownProject.SelectedValue != "")
                sb.AddStrategy(new Strategy(WorkFlowActStrategyName.ProjectCode, this.DropDownProject.SelectedValue));
            if (this.ucPerson.Value != "")
                sb.AddStrategy(new Strategy(WorkFlowActStrategyName.FromUserCode, this.ucPerson.Value));
            if (this.DateStart.Value != "" || this.DateEnd.Value != "")
            {
                string sDate = this.DateStart.Value;
                string eDate = this.DateEnd.Value;
                if (this.DateStart.Value == "")
                    sDate = DateTime.MinValue.ToShortDateString();
                if (this.DateEnd.Value == "")
                    eDate = DateTime.MaxValue.ToShortDateString();
                ArrayList arrlist = new ArrayList();
                arrlist.Add(sDate);
                arrlist.Add(eDate);
                sb.AddStrategy(new Strategy(WorkFlowActStrategyName.FromDate, arrlist));
            }

			string sql = sb.BuildMainQueryString();
            string sortsql = BLL.GridSort.GetSortSQL(ViewState, "FromDate desc");
            if (sortsql != "")
            {
                sql = sql + " order by " + sortsql;
            }
			this.ViewState.Add("SqlString",sql);
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

		protected void btnSearch_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				BuildSqlString();
				this.gpControl.CurrentPageIndex = 1 ;
				LoadData();
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		
		}

		protected void gpControl_PageIndexChange(object sender, System.EventArgs e)
		{
			LoadData();
		}


        protected void dgList_SortCommand(object source, DataGridSortCommandEventArgs e)
        {
            this.gpControl.CurrentPageIndex = 1;
            BLL.GridSort.SortCommand((DataGrid)source, ViewState, source, e);
            BuildSqlString();
            LoadData();
        }
}
}
