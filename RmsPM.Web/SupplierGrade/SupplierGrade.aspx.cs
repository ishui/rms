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
using RmsPM.BLL;
using Rms.ORMap;
using RmsPM.DAL.EntityDAO;
using RmsPM.DAL.QueryStrategy;
using RmsPM.Web;

public partial class SupplierGrade_SupplierGrade : PageBase
{
    /// <summary>
    /// 获取单位信息页面
    /// </summary>
    public string SupplierGradeInfoPage
    {
        get
        {
           // return "../SupplierGrade/SupplierGradeModif.aspx";
            return RmsPM.BLL.WorkFlowRule.GetProcedureURLByName("单位评分审核");
        }
    }


    /// <summary>
    /// 获取供应商评分页面

    /// </summary>
    public string PursveSupplierGradeInfoPage
    {
        get
        {
            // return "../SupplierGrade/SupplierGradeModif.aspx";
            return RmsPM.BLL.WorkFlowRule.GetProcedureURLByName("供应商评分审核");
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadData();

        }
    }
    private void LoadData()
    {
        if (user.HasRight("2702"))
        {
            this.btnNewWorkFlow.Visible = true;
        }
        if (user.HasRight("2709"))
        {
            this.btnNewPursveWorkflow.Visible = true;
        }

        GradeMessage gm = new GradeMessage();
        string projectCode = Request["ProjectCode"] + "";
        string SupplierCode = this.txtSupplierCode.Value;
        string ProjectManage = this.txtProjectManage.Value;
        string Status = "";
        for (int i = 0; i < this.cblStatus.Items.Count; i++)
        {
            if (this.cblStatus.Items[i].Selected)
            {
                if (Status != "")
                {
                    Status = Status + ",";
                }
                Status = this.cblStatus.Items[i].Value;
            }
        }

        if (ProjectManage != "")
        {
            gm.ProjectManage = "%" + ProjectManage + "%";

        }
        if (SupplierCode != "")
        {
            gm.SupplierCode = SupplierCode;
        }
        if (Status != "")
        {
            gm.State = Status;
        }
        if (this.ddlWorkFlowTypeView.SelectedValue != "")
        {
            gm.MainDefineCode = this.ddlWorkFlowTypeView.SelectedValue;
        }

        gm.ProjectCode = projectCode;
        
        DataTable dtGradeMessage=gm.GetGradeMessages();

        RmsPM.DAL.QueryStrategy.WorkFlowHistory sb = new RmsPM.DAL.QueryStrategy.WorkFlowHistory();
        sb.AddStrategy(new Strategy(WorkFlowHistoryStrategyName.ProcedureNameAndApplicationCodein, this.GetWorkFlowListString(dtGradeMessage)));

        

        if (!((User)Session["User"]).HasOperationRight("090102"))
        {
            sb.AddStrategy(new Strategy(WorkFlowHistoryStrategyName.ActUserCode, ((User)Session["User"]).UserCode));
        }
        sb.AddOrder("CreateDate", false);



        string sql = sb.BuildMainQueryString();

        QueryAgent qa = new QueryAgent();
        DataSet ds = qa.ExecSqlForDataSet(sql);
        qa.Dispose();
        if (ds != null)
        {
            DataTable dttempgradeMessage = ds.Tables[0];
            dttempgradeMessage.Columns.Add("ProjectManage",System.Type.GetType("System.String"));
            dttempgradeMessage.Columns.Add("State", System.Type.GetType("System.String"));
            dttempgradeMessage.Columns.Add("SupplierCode", System.Type.GetType("System.String"));

            foreach (DataRow dr in dttempgradeMessage.Select())
            {
                if (dtGradeMessage.Select("GradeMessageCode='" + dr["ApplicationCode"] + "'").Length != 0)
                {
                    dr["ProjectManage"] = dtGradeMessage.Select("GradeMessageCode='" + dr["ApplicationCode"] + "'")[0]["ProjectManage"].ToString();
                    dr["State"] = dtGradeMessage.Select("GradeMessageCode='" + dr["ApplicationCode"] + "'")[0]["State"].ToString();
                    dr["SupplierCode"] = dtGradeMessage.Select("GradeMessageCode='" + dr["ApplicationCode"] + "'")[0]["SupplierCode"].ToString();
                }
            }

            this.dgList.DataSource = dttempgradeMessage;
            this.dgList.DataBind();
        }


    }


    private string GetWorkFlowListString(DataTable dtGradeMessage)
    {
        string ListString = "";
        int i = 0;
        foreach (DataRow drGrade in dtGradeMessage.Select("MainDefineCode='100001'"))
        {
            if (i != dtGradeMessage.Select("MainDefineCode='100001'").Length - 1)
                ListString += "'单位评分审核" + drGrade["GradeMessageCode"].ToString() + "',";
            else
                ListString += "'单位评分审核" + drGrade["GradeMessageCode"].ToString() + "'";
            i++;
        }
        if (dtGradeMessage.Select("MainDefineCode='100001'").Length != 0 && dtGradeMessage.Select("MainDefineCode='100002'").Length != 0)
            ListString += ",";
        i = 0;
        foreach (DataRow drPursveGrade in dtGradeMessage.Select("MainDefineCode='100002'"))
        {
            if (i != dtGradeMessage.Select("MainDefineCode='100002'").Length - 1)
                ListString += "'供应商评分审核" + drPursveGrade["GradeMessageCode"].ToString() + "',";
            else
                ListString += "'供应商评分审核" + drPursveGrade["GradeMessageCode"].ToString() + "'";
            i++;
        }

        if (ListString =="")
        {
            ListString += "'单位评分审核" + "" + "'";
        }
      

        return ListString;
    }

    /// <summary>
    /// 分页事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gpControl_PageIndexChange(object sender, System.EventArgs e)
    {
        try
        {
            LoadData();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }



    protected void btnSearch_ServerClick(object sender, EventArgs e)
    {

        LoadData();
    }
}
