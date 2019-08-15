using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using RmsOA.BFL;
using RmsOA.MODEL;
using System.Collections.Generic;

public partial class RmsOA_TY_OA_MgrTaskDtlInfo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["_AuditingURL"] = RmsPM.BLL.WorkFlowRule.GetProcedureURLByName("总经理交办工作流程");
            WorkFlowList1.ProcedureNameAndApplicationCodeList = "'总经理交办工作流程" + Request["MgrDtlCode"] + "'";
            WorkFlowList1.DataBound();
        }
    }

    protected void FormView1_ItemDeleted(object sender, FormViewDeletedEventArgs e)
    {
        if (e.Exception != null)
        {
            Response.Write(Rms.Web.JavaScript.Alert(true, "删除具体任务出错：" + e.Exception.Message));
        }

        else
        {
            DateTime dt = GetTaskDeadLine();
            if (dt != DateTime.MinValue)
            {
                UpdateMgrTaskDeadLine(dt);
            }
            Response.Write("<script>window.opener.location = window.opener.location;window.close();</script>");
            Response.End();
        }
    }
    protected void FormView1_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
    {
        DateTime dt = GetTaskDeadLine();
        if (dt != DateTime.MinValue)
        {
            UpdateMgrTaskDeadLine(dt);
        }
        Response.Write("<script>window.opener.location = window.opener.location;</script>");
        //for refresh
    }

    protected void FormView1_DataBound(object sender, EventArgs e)
    {
        if (FormView1.CurrentMode == FormViewMode.ReadOnly)
        {
            RmsOA.BFL.TY_OA_MgrTaskDtlBFL dtlbfl = new RmsOA.BFL.TY_OA_MgrTaskDtlBFL();
            RmsOA.MODEL.TY_OA_MgrTaskDtlModel dtlmdl = dtlbfl.GetTY_OA_MgrTaskDtl(RmsPM.BLL.ConvertRule.ToInt(Request.QueryString["MgrDtlCode"]));
            Button btnDelete = ((Button)this.FormView1.Row.FindControl("btnDelete"));
            dtlmdl = dtlbfl.GetTY_OA_MgrTaskDtl(RmsPM.BLL.ConvertRule.ToInt(Request.QueryString["MgrDtlCode"]));
            if (btnDelete != null)
            {
                btnDelete.Attributes["OnClick"] = "javascript:return confirm('确实要删除当前任务吗？')";

             
                //有审核中或审核完的明细的任务不能删除
                if (dtlmdl.State == "2" || dtlmdl.State == "3")
                {
                    btnDelete.Visible = false;
                    
                }
                
            }


            if (dtlmdl.State == "1")//只有待审状态可见提交
            {
                ((System.Web.UI.HtmlControls.HtmlInputButton)this.FormView1.Row.FindControl("btnRequisition")).Visible = true;
            }
            else
            {
                ((System.Web.UI.HtmlControls.HtmlInputButton)this.FormView1.Row.FindControl("btnRequisition")).Visible = false;

            }

            System.Web.UI.WebControls.Label lblteamers = (System.Web.UI.WebControls.Label)this.FormView1.Row.FindControl("Eyeteamer");
            if (!string.IsNullOrEmpty(lblteamers.Text))
            {
                lblteamers.Text = lblteamers.Text.Substring(0, lblteamers.Text.Length - 1);
            }
            string[] teamers = lblteamers.Text.Split(',');
            string newteamers = "";
            foreach (string teamer in teamers)
            {
                newteamers += RmsPM.BLL.SystemRule.GetUserName(teamer) + ";";
            }
            if (!string.IsNullOrEmpty(newteamers))
            {
                newteamers = newteamers.Substring(0, newteamers.Length - 1);
            }
            lblteamers.Text = newteamers;


        }
    }




    private DateTime GetTaskDeadLine()
    {
        //主表要求完成时间为明细中最晚的那个
        DateTime LastDate=new DateTime();
        TY_OA_MgrTaskBFL TaskBFL = new TY_OA_MgrTaskBFL();
        TY_OA_MgrTaskDtlBFL TaskDtlBFL = new TY_OA_MgrTaskDtlBFL();
    
        TY_OA_MgrTaskDtlModel TaskDtlMdl = TaskDtlBFL.GetTY_OA_MgrTaskDtl(RmsPM.BLL.ConvertRule.ToInt(Request.QueryString["MgrDtlCode"]));
        List<TY_OA_MgrTaskDtlModel> TaskDtlMdls = TaskDtlBFL.GetTY_OA_MgrTaskDtlList(TaskDtlMdl.MgrCodeID);
        TY_OA_MgrTaskModel TaskMdl = TaskBFL.GetTY_OA_MgrTask(TaskDtlMdl.MgrCodeID);
        
        int i = 0;
        foreach(TY_OA_MgrTaskDtlModel SingleTaskMdl in TaskDtlMdls)
        {
            if (0 == i)
            {
                LastDate = SingleTaskMdl.DeadLine;
            }
            else
            {
                System.TimeSpan ts = LastDate.Subtract(SingleTaskMdl.DeadLine);
                if (ts.Days < 0)
                {
                    LastDate = SingleTaskMdl.DeadLine;
                }
            }
            i++;
        }
        if (TaskMdl.DeadLine==LastDate)
            LastDate = DateTime.MinValue;
  
        return LastDate;
     }

    private void UpdateMgrTaskDeadLine(DateTime dt)
    {
        TY_OA_MgrTaskBFL TaskBFL = new TY_OA_MgrTaskBFL();
        TY_OA_MgrTaskDtlBFL TaskDtlBFL = new TY_OA_MgrTaskDtlBFL();
        TY_OA_MgrTaskDtlModel TaskDtlMdl = TaskDtlBFL.GetTY_OA_MgrTaskDtl(RmsPM.BLL.ConvertRule.ToInt(Request.QueryString["MgrDtlCode"]));
        TY_OA_MgrTaskModel TaskMdl = TaskBFL.GetTY_OA_MgrTask(TaskDtlMdl.MgrCodeID);
        TaskMdl.DeadLine = dt;
        TaskBFL.Update(TaskMdl);
    }

        
       
    
}
