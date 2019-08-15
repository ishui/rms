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
using System.Collections.Generic;

using Rms.ORMap;

namespace RmsPM.Web.WorkFlowControl
{
	/// <summary>
	/// WorkFlowQuery ��ժҪ˵����
	/// </summary>
	public partial class WorkFlowQuery : PageBase
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack)
			{
				IniPage();
                LoadData();
			}
		}
		private void IniPage()
		{
			try
			{
                if (!user.HasOperationRight("090102"))
                {
                    this.CheckBox1.Visible = false;
                    this.CheckBox2.Visible = false;
                }
                else {
                    this.CheckBox1.Visible = true;
                    this.CheckBox2.Visible = true;
                }

				EntityData entity = DAL.EntityDAO.WorkFlowDAO.GetAllWorkFlowProcedure();
				int iCount = entity.CurrentTable.Rows.Count;
                string tempProcedureName = "";
                List<string> ProcedureNameList = new List<string>();
                for (int i = 0; i < iCount; i++)
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
                        this.sltProcedure.Items.Add(new ListItem(entity.GetString("description"), ProcedureName));
                        ProcedureNameList.Add(ProcedureName);
                    }
                }

                this.DropDownProject.DataSource = new DataView(user.m_EntityDataAccessProject.CurrentTable, "", "ProjectName", DataViewRowState.CurrentRows);
                this.DropDownProject.DataTextField = "ProjectShortName";
                this.DropDownProject.DataValueField = "ProjectCode";
                this.DropDownProject.DataBind();
                ListItem li = new ListItem("--������Ŀ--", "");
                this.DropDownProject.Items.Add(li);
                this.DropDownProject.SelectedIndex = this.DropDownProject.Items.IndexOf(this.DropDownProject.Items.FindByValue(""));

				entity.Dispose();
			}
			catch ( Exception ex )
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

		}
		#endregion

		protected void btnSearch_ServerClick(object sender, System.EventArgs e)
		{
            LoadData();
		}
        private void LoadData()
        {
            if (!user.HasOperationRight("090102"))
            {
                this.WorkFlowMonitor1.ActUser = this.user.UserCode;
            }
            else
            {
                if (this.CheckBox1.Checked == true && this.CheckBox2.Checked == true)//ȫ��
                {
                }
                else if (this.CheckBox1.Checked == true && this.CheckBox2.Checked == false)//����
                {
                    this.WorkFlowMonitor1.ActUser = this.user.UserCode;
                }
                else if (this.CheckBox1.Checked == false && this.CheckBox2.Checked == true)//���
                {
                    this.WorkFlowMonitor1.IsNotActUser = this.user.UserCode;
                }
                else if (this.CheckBox1.Checked == false && this.CheckBox2.Checked == false)//��
                {
                    this.WorkFlowMonitor1.ActUser = "NotSystemUser";
                }
            }
            this.WorkFlowMonitor1.ProcedureName = this.sltProcedure.Value;
            this.WorkFlowMonitor1.CaseCode = this.txtCaseCode.Value;
            this.WorkFlowMonitor1.TaskName = this.txtTaskName.Value;
            this.WorkFlowMonitor1.Title = this.txtTitle.Value;
            this.WorkFlowMonitor1.ProjectCode = this.DropDownProject.SelectedValue;
            this.WorkFlowMonitor1.ucPerson = this.ucPerson.Value;
            this.WorkFlowMonitor1.DateStart = this.DateStart.Value;
            this.WorkFlowMonitor1.DateEnd = this.DateEnd.Value;
            this.WorkFlowMonitor1.ucToPerson = this.ucToPerson.Value;
            this.WorkFlowMonitor1.CalendarStart = this.CalendarStart.Value;
            this.WorkFlowMonitor1.CalendarEnd = this.CalendarEnd.Value;
            this.WorkFlowMonitor1.DataBound();


        }
	}
}
