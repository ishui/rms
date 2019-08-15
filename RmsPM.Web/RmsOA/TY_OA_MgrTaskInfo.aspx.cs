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
using RmsOA.MODEL;
using RmsOA.BLL;
using System.Collections.Generic;
using RmsOA.BFL;
using RmsPM.Web.WorkFlowControl;
using RmsPM.Web.UserControls;
//1待审,2审核中,3已审,
public partial class RmsOA_TY_OA_MgrTaskInfo : RmsPM.Web.PageBase
{

    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (!IsPostBack )
        {

            if (Request["MgrCode"] + "" != "")
            {
                this.MgrCode.Text = Request["MgrCode"] + "";
            }

            if (this.MgrCode.Text == "")
            {
                FormView1.ChangeMode(FormViewMode.Insert);
                ((Label)this.FormView1.Row.FindControl("MgrTaskID")).Text = "task" + RmsPM.DAL.EntityDAO.SystemManageDAO.GetFormatSysCode("MgrTaskCode", "{###}");
                ((Label)this.FormView1.Row.FindControl("MgrTaskID")).Text += "-" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString();
            }
            //修改状态
            else
            {
                string MgrCode = Request.QueryString["MgrCode"] + "";

                //ArrayList ar = user.GetResourceRight(MaterialInCode, "MaterialIn");
                //if (!ar.Contains("150301"))
                //{
                //    Response.Redirect("../RejectAccess.aspx");
                //    Response.End();
                //}
                //if (!ar.Contains("150303"))
                //{
                //    ((Button)this.FormView1.Row.FindControl("btnModify")).Visible = false;
                //}
                //if (!ar.Contains("150304"))
                //{
                //    ((Button)this.FormView1.Row.FindControl("btnDelete")).Visible = false;
                //}
            }
        }
    }

    protected void FormView1_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
        e.Values["MgrTaskID"] = ((Label)this.FormView1.Row.FindControl("MgrTaskID")).Text;
        e.Values["State"] = "1";
        //e.Values["Isfinish"] = "";
        e.Values["Code"] = RmsPM.DAL.EntityDAO.SystemManageDAO.GetNewSysCode("MgrTaskCode");
        e.Values["TaskTail"] = "";
        e.Values["CreateMan"] = base.user.UserCode;
        e.Values["IsFinish"] = "0";


        e.Values["DeadLine"] = GetTaskDeadLine();
        e.Cancel = checkvalue();
    }

    protected void FormView1_ItemDeleted(object sender, FormViewDeletedEventArgs e)
    {
        if (e.Exception != null)
        {
            Response.Write(Rms.Web.JavaScript.Alert(true, "删除任务出错：" + e.Exception.Message));
        }
        else
        {
            Response.Write("<script>window.opener.location = window.opener.location;window.close();</script>");
            //this.FormView1.DataBind();
            Response.End();
        }
    }
    protected void FormView1_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
    {
        try
        {
            UpdateTY_OA_MgrTaskDtl();
        }
        catch (Exception ex)
        {
            Response.Write(Rms.Web.JavaScript.Alert(true, "保存总经理交办事件明细出错：" + ex.Message));
        }
        Response.Write("<script>window.opener.location = window.opener.location;</script>");

    }
    protected void FormView1_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
        e.NewValues["DeadLine"] = GetTaskDeadLine();
        e.Cancel = checkvalue();
    }
    protected void ObjectDataSource1_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        try
        {
            RmsPM.Web.UserControls.AttachMentAdd ucadd = (RmsPM.Web.UserControls.AttachMentAdd)this.FormView1.Row.FindControl("Attachmentadd1");
            ucadd.SaveAttachMent(((int)e.ReturnValue).ToString());
            ObjectDataSource1.SelectParameters["Code"].DefaultValue = e.ReturnValue.ToString();
            ObjectDataSource2.SelectParameters["MgrCodeID"].DefaultValue = e.ReturnValue.ToString();

            this.MgrCode.Text = e.ReturnValue.ToString();
        }
        catch(Exception ex)
        {
            Response.Write(Rms.Web.JavaScript.Alert(true, "保存总经理交办事件主表出错：" + ex.Message));
        }
        try
        {
            UpdateTY_OA_MgrTaskDtl();
        }
        catch (Exception ex)
        {
            Response.Write(Rms.Web.JavaScript.Alert(true, "保存总经理交办事件明细出错：" + ex.Message));
        }
        Response.Write("<script>window.opener.location = window.opener.location;</script>");
    }

     
    protected void FormView1_DataBound(object sender, EventArgs e)
    {
        
        if (FormView1.CurrentMode == FormViewMode.ReadOnly)
        {
            Button btnDelete = ((Button)this.FormView1.Row.FindControl("btnDelete"));
            if (btnDelete != null)
                btnDelete.Attributes["OnClick"] = "javascript:return confirm('确实要删除当前事宜吗？')";


            TY_OA_MgrTaskBFL Taskbfl=new TY_OA_MgrTaskBFL();
            TY_OA_MgrTaskDtlBFL Taskdtlbfl = new TY_OA_MgrTaskDtlBFL();
            TY_OA_MgrTaskModel TaskMl = Taskbfl.GetTY_OA_MgrTask(RmsPM.BLL.ConvertRule.ToInt(this.MgrCode.Text));

            List<TY_OA_MgrTaskDtlModel> TaskDtlMl = Taskdtlbfl.GetTY_OA_MgrTaskDtlList(RmsPM.BLL.ConvertRule.ToInt(this.MgrCode.Text));
            foreach (TY_OA_MgrTaskDtlModel DtlMl in TaskDtlMl)
            { 
                //有审核中或审核完的明细的任务不能删除
                if (DtlMl.State == "2" || DtlMl.State == "3")
                {
                    btnDelete.Visible = false;
                    break;
                }
            }

            string[] Links = TaskMl.ReferLink.Split(';');
            int i = -1;
          
            foreach (string EachLink in Links)
            {
                i++;
                if (!string.IsNullOrEmpty(EachLink))
                {
                    
                    string[] ForLink = EachLink.Split(',');
                    //lblWorkFlowTitle.Text = ShowApplicationHyperLink(entity.GetString("WorkFlowTitle"), string.Format(ud_sHyperLinkFormat, this.OperationCode, this.ProjectCode));
                    ((System.Web.UI.HtmlControls.HtmlTableCell)this.FormView1.Row.FindControl("ReferLinkID")).InnerHtml += "<a href='##' onclick='MonitorgotoDirect(\"" + ForLink[0] + "\",\"" + ForLink[1] + "\",\"" + ForLink[2] + "\",\"" + ForLink[3] + "\",\"" + ForLink[4] + "\"); return false;'>" + RmsPM.BLL.WorkFlowRule.GetWorkFlowCaseTitle(ForLink[0]) + "</a>  ";
                    //((System.Web.UI.HtmlControls.HtmlTableCell)this.FormView1.Row.FindControl("ReferLinkID")).InnerHtml += "<a href='##' onclick='MonitorgotoDirect(" + EachLink + "," + "," + "," + "," + "); return false;'>" + RmsPM.BLL.WorkFlowRule.GetWorkFlowCaseTitle(EachLink) + "</a>&nbsp;";
                }
            }


            Label status = (Label)this.FormView1.Row.FindControl("StatusID");
            bool isend = true;
            TY_OA_MgrTaskDtlBFL dtbfl = new TY_OA_MgrTaskDtlBFL();
            if (this.MgrCode.Text != "")
            {
                List<TY_OA_MgrTaskDtlModel> dtlmdLst = dtbfl.GetTY_OA_MgrTaskDtlList(RmsPM.BLL.ConvertRule.ToInt(this.MgrCode.Text));

                foreach (TY_OA_MgrTaskDtlModel mdl in dtlmdLst)
                {
                    //有审核中流程则状态为未完成
                    if (mdl.State == "2")
                    {
                        isend = false;
                        if (status != null)
                            status.Text = "流程审评中";
                        break;
                    }
                }
            }
            if (isend && status != null)
            {
                status.Text = "无审核中流程";
            }

            //RmsOA.BFL.TY_OA_MgrTaskDtlBFL bfl = new RmsOA.BFL.TY_OA_MgrTaskDtlBFL();
            //List<TY_OA_MgrTaskDtlModel> Models = bfl.GetTY_OA_MgrTaskDtlList(RmsPM.BLL.ConvertRule.ToInt(this.MgrCode.Text));
            //ViewState["Models"] = Models;

            

        }
        else
        {

            DataGrid dgDtl = (DataGrid)this.FormView1.Row.FindControl("dgDtl");
            if (dgDtl != null)
            {
                RmsOA.BFL.TY_OA_MgrTaskDtlBFL bfl = new RmsOA.BFL.TY_OA_MgrTaskDtlBFL();
                List<TY_OA_MgrTaskDtlModel> Models = bfl.GetTY_OA_MgrTaskDtlList(RmsPM.BLL.ConvertRule.ToInt(this.MgrCode.Text));
                ViewState["Models"] = Models;
                BindDtl(Models);
            }
        }

    }


    /// <summary>
    /// 取屏幕明细的List
    /// </summary>
    /// <returns></returns>
    private List<TY_OA_MgrTaskDtlModel> GetScreenDtl()
    {
        GridView gvDtl = (GridView)this.FormView1.Row.FindControl("GridView1");
        DataGrid dgDtl = (DataGrid)this.FormView1.Row.FindControl("dgDtl");
        List<TY_OA_MgrTaskDtlModel> Models = (List<TY_OA_MgrTaskDtlModel>)ViewState["Models"];
        
        int i = -1;
        if (dgDtl != null)
        {
            foreach (DataGridItem item in dgDtl.Items)
            {
                i++;
                string DtlCode = dgDtl.DataKeys[i].ToString();

                TY_OA_MgrTaskDtlModel mgrObj = Models[i];

                mgrObj.MgrDtlInfo = ((HtmlTextArea)item.FindControl("TxtTaskDetail")).Value.ToString();
                mgrObj.DeadLine = DateTime.Parse(((AspWebControl.Calendar)item.FindControl("CalendarInDate")).Value);
                mgrObj.ResponsePerson = ((RmsPM.Web.UserControls.InputUser)item.FindControl("ResponsePerson")).Value;
                mgrObj.Assistpersons = ((InputUsers)item.FindControl("txtteamer")).Value;

            }
            
        }
        return Models;

    }

    /// <summary>
    /// 绑定明细
    /// </summary>
    /// <param name="Models"></param>
    private void BindDtl(List<TY_OA_MgrTaskDtlModel> Models)
    {
        if (FormView1.CurrentMode != FormViewMode.ReadOnly)
        {
            Workflowselect WorkFlowMonitor1 = ((Workflowselect)this.FormView1.Row.FindControl("WorkFlowMonitor1"));
            if (WorkFlowMonitor1 != null && WorkFlowMonitor1.Value != "")
            {
                //让Value再set次,否则显示会有问题
                WorkFlowMonitor1.Value = WorkFlowMonitor1.Value;
            }
        }
        DataGrid dgDtl = (DataGrid)this.FormView1.Row.FindControl("dgDtl");
        dgDtl.DataSource = Models;
        dgDtl.DataBind();
    }

    /// <summary>
    /// 保存明细
    /// </summary>
    private void UpdateTY_OA_MgrTaskDtl()
    {
        List<TY_OA_MgrTaskDtlModel> Models = GetScreenDtl();
        RmsOA.BFL.TY_OA_MgrTaskDtlBFL bfl = new RmsOA.BFL.TY_OA_MgrTaskDtlBFL();
        bfl.UpdateTY_OA_MgrTaskDtlList(Models, RmsPM.BLL.ConvertRule.ToInt(this.MgrCode.Text));

    }
    /// <summary>
    /// 新增明细
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAddDtl_ServerClick(object sender, EventArgs e)
    {
        List<TY_OA_MgrTaskDtlModel> Models = GetScreenDtl();

        TY_OA_MgrTaskDtlModel mgrObj = new TY_OA_MgrTaskDtlModel();
        mgrObj.DeadLine=System.DateTime.Now.Date;
        Models.Add(mgrObj);

        BindDtl(Models);
    }

    /// <summary>
    /// 删除明细
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void dgDtl_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        List<TY_OA_MgrTaskDtlModel> Models = GetScreenDtl();
        if (Models[e.Item.ItemIndex].State == "1" || string.IsNullOrEmpty(Models[e.Item.ItemIndex].State))//待审的时候才能删除
        {
            Models.RemoveAt(e.Item.ItemIndex);

            BindDtl(Models);
        }
        else
        {
            Response.Write(Rms.Web.JavaScript.Alert(true, "不是待审状态不能删除"));
        }
    }

    private bool checkvalue()
    {
        bool ReturnCancel = false;
        if (((TextBox)this.FormView1.Row.FindControl("TxtTaskName")).Text == "")
        {
            ((HtmlGenericControl)this.FormView1.Row.FindControl("GroupSpan")).InnerHtml = "<font color='red'>必填</font>";
            ReturnCancel = true;

        }
        else
        {
            ((HtmlGenericControl)this.FormView1.Row.FindControl("GroupSpan")).InnerHtml = "";
        }
        return ReturnCancel;
    }

    private DateTime GetTaskDeadLine()
    {
        //主表要求完成时间为明细中最晚的那个
        List<TY_OA_MgrTaskDtlModel> TaskDtlMdl = GetScreenDtl();
        int i = 0;
        DateTime LastDate =  System.DateTime.Now;
        foreach (TY_OA_MgrTaskDtlModel SingleMdl in TaskDtlMdl)
        {
            if (i == 0)
            {
                LastDate = SingleMdl.DeadLine;
            }
            else
            {
                System.TimeSpan ts = LastDate.Subtract(SingleMdl.DeadLine); 
                if (ts.Days < 0)
                LastDate = SingleMdl.DeadLine;
            }
            i++;
            
        }
        return LastDate;
    }

    protected void GridView1_DataBound(object sender, EventArgs e)
    {
        int rowscounts = ((GridView)sender).Rows.Count;
        for (int i = 0; i < rowscounts; i++)
        {
            Label lblteamers = (Label)(((GridView)sender).Rows[i].FindControl("Eyeteamer"));
            if(!string.IsNullOrEmpty(lblteamers.Text))
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

}
