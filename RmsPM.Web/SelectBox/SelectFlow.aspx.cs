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
using System.Web;
using Rms.ORMap;

namespace RmsPM.Web.SelectBox
{
    /// <summary>
    /// WorkFlowSelectBox 的摘要说明。
    /// </summary>
    public partial class SelectFlow : PageBase
    {
        
        public string _ReturnFunc;
        //public string ReturnFunc
        //{
        //    get { return Request.QueryString["ReturnFunc"] + ""; }
            
        //}



        protected void Page_Load(object sender, System.EventArgs e)
        {
            _ReturnFunc = Request.QueryString["ReturnFunc"] + "";
            this.WorkFlowMonitor1.ReturnFunc = "doSelectFlow";

            if (!IsPostBack)
            {
                IniPage();
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
                for (int i = 0; i < iCount; i++)
                {
                    entity.SetCurrentRow(i);
                    if (tempProcedureName.IndexOf(entity.GetString("ProcedureName")) == -1)
                    {
                        this.sltProcedure.Items.Add(new ListItem(entity.GetString("description"), entity.GetString("ProcedureName")));
                        tempProcedureName += "," + entity.GetString("ProcedureName");
                    }
                }

                this.DropDownProject.DataSource = new DataView(user.m_EntityDataAccessProject.CurrentTable, "", "ProjectName", DataViewRowState.CurrentRows);
                this.DropDownProject.DataTextField = "ProjectShortName";
                this.DropDownProject.DataValueField = "ProjectCode";
                this.DropDownProject.DataBind();
                ListItem li = new ListItem("--所有项目--", "");
                this.DropDownProject.Items.Add(li);
                this.DropDownProject.SelectedIndex = this.DropDownProject.Items.IndexOf(this.DropDownProject.Items.FindByValue(""));

                entity.Dispose();
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
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

