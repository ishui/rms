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
using System.Text;


namespace RmsPM.Web.WorkFlowDefinition
{
    /// <summary>
    /// ProcedureList 的摘要说明。
    /// </summary>
    public partial class ProcedureList : PageBase
    {
        protected System.Web.UI.WebControls.Label lblU8Code;
        protected System.Web.UI.HtmlControls.HtmlInputButton btnNew;

       
        /// <summary>
        /// 文件选择框显示模式
        /// </summary>
         public string FileUploadDisplay
        {
            get
            {
                return this.filediv.Style["display"].ToString();
            }
            set
            {
                this.filediv.Attributes["Display"] = value;
            }
        }
        /// <summary>
        /// 文件选择框显示模式
        /// </summary>
        public string FileRoleUploadDisplay
        {
            get
            {
                return this.fileRolediv.Style["display"].ToString();
            }
            set
            {
                this.fileRolediv.Attributes["Display"] = value;
            }
        }
        


        protected void Page_Load(object sender, System.EventArgs e)
        {
            // 在此处放置用户代码以初始化页面
            if (!IsPostBack)
            {
                BuildSqlString();
                LoadData();
                LoadDataGrid();
                this.DropDownProject.DataSource = new DataView(user.m_EntityDataAccessProject.CurrentTable, "", "ProjectName", DataViewRowState.CurrentRows);
                this.DropDownProject.DataTextField = "ProjectShortName";
                this.DropDownProject.DataValueField = "ProjectCode";
                this.DropDownProject.DataBind();
            }
        }

        public string GetServerPath
        {
            get
            {
                return Server.MapPath("../Temp/");
            }
        }
        public string XmlName
        {
            get
            {
                return "WorkFlowDB" + DateTime.Now.ToShortDateString() + "_" + DateTime.Now.ToShortTimeString().Replace(":", "-") + ".xml";
            }
        }

        private void LoadDataGrid()
        {
            try
            {
                this.GridView1.DataSource = BLL.ProjectRule.GetAllProject();
                this.GridView1.DataBind();
                this.RadioButtonList1.SelectedIndex = this.RadioButtonList1.Items.IndexOf(this.RadioButtonList1.Items.FindByValue(BLL.SystemRule.GetProjectConfigValue("FlowNumberLength")));
            }
            catch (Exception ex)
            { throw ex; }
        }
        private void LoadData()
        {
            string sql = (string)this.ViewState["SqlString"];
            QueryAgent qa = new QueryAgent();
            DataSet ds = qa.ExecSqlForDataSet(sql);
            qa.Dispose();
            this.dgList.DataSource = ds;
            this.dgList.DataBind();
        }

        private void BuildSqlString()
        {
            try
            {
                WorkFlowProcedureStrategyBuilder sb = new WorkFlowProcedureStrategyBuilder();
                if (this.txtDescription.Value != "")
                {
                    sb.AddStrategy(new Strategy(WorkFlowProcedureStrategyName.DescriptionLike, "%" + this.txtDescription.Value + "%"));
                }
                if (this.txtProcedureName.Value != "")
                {
                    sb.AddStrategy(new Strategy(WorkFlowProcedureStrategyName.ProcedureNameLike, "%" + this.txtProcedureName.Value + "%"));
                }
                if (this.DropDownProject.SelectedValue != "")
                {
                    sb.AddStrategy(new Strategy(WorkFlowProcedureStrategyName.ProjectCode, this.DropDownProject.SelectedValue));
                }
                if (this.DropDownActivity.SelectedValue != "")
                {
                    sb.AddStrategy(new Strategy(WorkFlowProcedureStrategyName.Activity, this.DropDownActivity.SelectedValue));
                }
                if (this.DropDownType.SelectedValue != "")
                {
                    sb.AddStrategy(new Strategy(WorkFlowProcedureStrategyName.Type, this.DropDownType.SelectedValue));
                }
                if (this.VersionNumber.Value != "")
                {
                    sb.AddStrategy(new Strategy(WorkFlowProcedureStrategyName.VersionNumber, this.VersionNumber.Value));
                }

                string sql = sb.BuildMainQueryString();
                //排序
                string sortsql = BLL.GridSort.GetSortSQL(ViewState, "VersionDescription asc");
                if (sortsql != "")
                {
                    sql = sql + " order by " + sortsql;
                }
                this.ViewState.Add("SqlString", sql);
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
            }
        }

        protected void BT_OutAll_ServerClick(object sender, System.EventArgs e)
        {
            try
            {
                WorkFlowInOut wio = new WorkFlowInOut();
                wio.BackDataToXml(this.GetServerPath, this.XmlName);
                //WorkFlowInOut.WriteAllWorkFlow(GetServerPath);
                Response.Write(Rms.Web.JavaScript.WriteJS("window.open(\"../Temp/" + this.XmlName + "\",\"_blank\")"));
            }
            catch (Exception ex)
            {
                Response.Write(Rms.Web.JavaScript.Alert(true, ex.Message));
            }
        }

        protected void BT_OutWorkFlow_ServerClick(object sender, System.EventArgs e)
        {
            try
            {
                //检查文件选择框是否为可见状态，update by karen;date by 2006-01-15
                string file = this.FileUploadDisplay;
                switch (this.FileUploadDisplay)
                {
                    case "none":
                        this.BT_OutWorkFlow.Attributes["onclick"] = "javascript:if(!window.confirm('将清空原有流程表,确实要导入数据吗?')) return false;";
                       
                        this.filediv.Style["display"] = "block";
                        return;

                }

                HttpPostedFile UpFile = this.UpFile.PostedFile;
                WorkFlowInOut.UpXmlDB(this.GetServerPath, UpFile, this.XmlName);
                WorkFlowInOut wio = new WorkFlowInOut();
                wio.SetNewDataSet(this.GetServerPath, this.GetServerPath, this.XmlName);
                Response.Write(Rms.Web.JavaScript.Alert(true, "已成功导入数据"));
                this.BT_OutWorkFlow.Attributes["onclick"] = "";
                this.filediv.Style["display"] = "none";
            }
            catch (Exception ex)
            {
                Response.Write(Rms.Web.JavaScript.Alert(true, ex.Message));
            }
        }


        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.FlowConfigTable.Visible = true;
            this.lblProjectName.Text = RmsPM.BLL.ProjectRule.GetProjectName(this.GridView1.SelectedValue.ToString());
            this.txtFlowNumber.Text = RmsPM.BLL.SystemRule.GetProjectConfigValue(this.GridView1.SelectedValue.ToString(), "FlowNumber");
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            this.FlowConfigTable.Visible = false;
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            BLL.SystemRule.UpdateProjectConfigValue(this.GridView1.SelectedValue.ToString(), "FlowNumber", this.txtFlowNumber.Text);
            this.FlowConfigTable.Visible = false;
            this.GridView1.DataSource = BLL.ProjectRule.GetAllProject();
            this.GridView1.DataBind();
        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            BLL.SystemRule.UpdateProjectConfigValue("FlowNumberLength", this.RadioButtonList1.SelectedValue);
            this.RegisterClientScriptBlock("flownumberlength", "<script>alert('流水号中序列号部分长度已经成功改为 " + this.RadioButtonList1.SelectedValue.ToString() + "位！');</script>");

        }
        protected void dgList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ProcedureCode = dgList.SelectedItem.Cells[10].Text.ToString();
            Rms.WorkFlow.Procedure procedure = Rms.WorkFlow.DefinitionManager.GetProcedureDifinition(ProcedureCode, false);
            Rms.WorkFlow.DefinitionManager.SaveAsProcedureDifinition(procedure);
            LoadData();
        }
        protected void dgList_SortCommand(object source, DataGridSortCommandEventArgs e)
        {
            BLL.GridSort.SortCommand((DataGrid)source, ViewState, source, e);
            BuildSqlString();
            LoadData();

        }
        protected void btnSearch_ServerClick(object sender, EventArgs e)
        {
            BuildSqlString();
            LoadData();
        }
        protected void Button4_ServerClick(object sender, EventArgs e)
        {
            string ProcedureCodeStr = "";
            for (int i = 0; i < this.dgList.Items.Count; i++)
            {
                if (((CheckBox)this.dgList.Items[i].FindControl("CheckBox1")).Checked)
                    ProcedureCodeStr += "'" + this.dgList.Items[i].Cells[10].Text + "',";
            }
            if (ProcedureCodeStr.Length > 0)
            {
                ProcedureCodeStr = ProcedureCodeStr.Remove(ProcedureCodeStr.Length - 1, 1);
                WorkFlowInOut wio = new WorkFlowInOut();
                wio.BackDataToXml(this.GetServerPath, this.XmlName, ProcedureCodeStr);
                //WorkFlowInOut.WriteAllWorkFlow(GetServerPath);
                Response.Write(Rms.Web.JavaScript.WriteJS("window.open(\"../Temp/" + this.XmlName + "\",\"_blank\")"));
            }
            else
            {
                Response.Write(Rms.Web.JavaScript.WriteJS("alert('请选择要导出的流程！');"));

            }
        }
        protected void Button5_ServerClick(object sender, EventArgs e)
        {
            try
            {
                //检查文件选择框是否为可见状态，update by karen;date by 2006-01-15
                string file = this.FileUploadDisplay;
                switch (this.FileUploadDisplay)
                {
                    case "none":
                        this.Button5.Attributes["onclick"] = "javascript:if(!window.confirm('将清空原有流程表,确实要导入数据吗?')) return false;";
                        
                        this.filediv.Style["display"] = "block";
                        return;

                }

                HttpPostedFile UpFile = this.UpFile.PostedFile;
                WorkFlowInOut.UpXmlDB(this.GetServerPath, UpFile, this.XmlName);
                WorkFlowInOut wio = new WorkFlowInOut();
                wio.SetNewDataSet(this.GetServerPath, this.GetServerPath, this.XmlName, false);
                Response.Write(Rms.Web.JavaScript.Alert(true, "已成功导入数据"));
                this.Button5.Attributes["onclick"] = "";
                this.filediv.Style["display"] = "none";
            }
            catch (Exception ex)
            {
                Response.Write(Rms.Web.JavaScript.Alert(true, ex.Message));
            }

        }
        protected void btnWorkflowRoleOut_ServerClick(object sender, EventArgs e)
        {
            string XmlName = "流程全局角色" + DateTime.Now.ToShortDateString() + "_" + DateTime.Now.ToShortTimeString().Replace(":", "-") + ".xml";
            WorkFlowInOut wio = new WorkFlowInOut();
            wio.BackRoleDataToXml(this.GetServerPath, XmlName);
            Response.Write(Rms.Web.JavaScript.WriteJS("window.open(\"../Temp/" + this.XmlName + "\",\"_blank\")"));
        }
        protected void btnWorkflowRoleIn_ServerClick(object sender, EventArgs e)
        {
            try
            {
                //检查文件选择框是否为可见状态，update by karen;date by 2006-01-15
                string file = this.FileUploadDisplay;
                switch (this.FileRoleUploadDisplay)
                {
                    case "none":
                        this.btnWorkflowRoleIn.Attributes["onclick"] = "javascript:if(!window.confirm('将清空原有全局角色,确实要导入数据吗?')) return false;";
                        this.fileRolediv.Style["display"] = "block";
                        
                        return;
                      
                }
                HttpPostedFile UpRoleFile = this.UpRoleFile.PostedFile;
                WorkFlowInOut.UpXmlDB(this.GetServerPath, UpRoleFile, this.XmlName);
                WorkFlowInOut wio = new WorkFlowInOut();
                wio.SetNewRoleDataSet(this.GetServerPath, this.GetServerPath, this.XmlName, false);
                Response.Write(Rms.Web.JavaScript.Alert(true, "已成功导入数据"));
                this.btnWorkflowRoleIn.Attributes["onclick"] = "";
                this.fileRolediv.Style["display"] = "none";
            }
            catch (Exception ex)
            {
                Response.Write(Rms.Web.JavaScript.Alert(true, ex.Message));
            }
        }
    }
}
