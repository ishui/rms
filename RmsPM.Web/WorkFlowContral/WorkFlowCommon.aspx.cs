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
using RmsPM.Web;
using RmsPM.DAL.QueryStrategy;
using Rms.ORMap;

public partial class WorkFlowContral_WorkFlowCommon : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            IniPage();

            BuildSearchString();
            LoadDataGrid();
        }

    }


    private void IniPage()
    {
        string ud_sProjectCode = Request["ProjectCode"] + "";
        string ud_sStatus = Request["Status"] + "";
        RmsPM.BLL.PageFacade.SetListGroupSelectedValues(this.cblStatus, ud_sStatus.Split(new char[] { ';' }));
        //			BLL.PageFacade.LoadUnitSelect(this.sltUnit,"",projectCode);

        this.inputSystemGroup.ClassCode = "0902";

        initDDLWorkFlowCommonByRight(this.ddlWorkFlowTypeNew);
        initDDLWorkFlowCommon(this.ddlWorkFlowTypeView);

        ListItem ud_Item = new ListItem();

        ud_Item.Text = "所有类型";
        ud_Item.Value = "";

        this.ddlWorkFlowTypeView.Items.Insert(0, ud_Item);

        ViewState["ImagePath"] = "../Images/";


        if (!user.HasRight("090202"))
        {
            this.ddlWorkFlowTypeNew.Visible = false;
            this.btnNew.Visible = false;
        }

    }

    private void initDDLWorkFlowCommon(DropDownList pm_ddlWorkFlowCommon)
    {
        EntityData entity = RmsPM.DAL.EntityDAO.WorkFlowDAO.GetWorkFlowCommon();


        pm_ddlWorkFlowCommon.DataSource = entity.CurrentTable;
        pm_ddlWorkFlowCommon.DataTextField = "ProcedureName";
        pm_ddlWorkFlowCommon.DataValueField = "ProcedureCode";
        pm_ddlWorkFlowCommon.DataBind();

    }

    private void initDDLWorkFlowCommonByRight(DropDownList pm_ddlWorkFlowCommon)
    {
        RmsPM.DAL.QueryStrategy.WorkFlowProcedureStrategyBuilder WFPSB = new RmsPM.DAL.QueryStrategy.WorkFlowProcedureStrategyBuilder();

        ArrayList arA = new ArrayList();
        arA.Add("090202");
        arA.Add(user.UserCode);
        arA.Add(user.BuildStationCodes());
        WFPSB.AddStrategy(new Strategy(WorkFlowProcedureStrategyName.AccessRange, arA));
        WFPSB.AddStrategy(new Strategy(WorkFlowProcedureStrategyName.Activity,"1"));

        WFPSB.IsNeedWhere = false;

        string sql = WFPSB.BuildQueryViewString();

        QueryAgent qa = new QueryAgent();
        EntityData entity = qa.FillEntityData("WorkFlowProcedure", sql);
        qa.Dispose();

        pm_ddlWorkFlowCommon.DataSource = entity.CurrentTable;
        pm_ddlWorkFlowCommon.DataTextField = "ProcedureName";
        pm_ddlWorkFlowCommon.DataValueField = "ProcedureCode";
        pm_ddlWorkFlowCommon.DataBind();

    }


    private void LoadDataGrid()
    {

        try
        {
            string sql = (string)this.ViewState["SearchString"];
            QueryAgent qa = new QueryAgent();
            EntityData entity = qa.FillEntityData("WorkFlowCommon", sql);
            qa.Dispose();


            DataView ud_dvWorkFlowCommon = new DataView(entity.CurrentTable);

            ud_dvWorkFlowCommon.Sort = "WorkFlowID";

            this.dgList.DataSource = ud_dvWorkFlowCommon;
            this.dgList.DataBind();
            this.GridPagination1.RowsCount = entity.CurrentTable.Rows.Count.ToString();

            entity.Dispose();

        }
        catch (Exception ex)
        {
            ApplicationLog.WriteLog(this.ToString(), ex, "加载通用流程列表错误。");
            Response.Write(Rms.Web.JavaScript.Alert(true, "加载通用流程列表错误：" + ex.Message));
        }
    }


    private void BuildSearchString()
    {
        string ud_sProjectCode = Request["ProjectCode"] + "";
        string ud_sStatus = RmsPM.BLL.PageFacade.GetListGroupSelectedValues(this.cblStatus);
        string ud_sProcedureCode = this.ddlWorkFlowTypeView.SelectedItem.Value;
        string ud_sWorkFlowTitle = this.txtWorkFlowTitle.Value.Trim();
        string ud_sWorkFlowID = this.txtWorkFlowID.Value.Trim();
        string ud_sType = this.inputSystemGroup.Value;

        
        RmsPM.DAL.QueryStrategy.WorkFlowCommonStrategyBuilder WFCSB = new RmsPM.DAL.QueryStrategy.WorkFlowCommonStrategyBuilder();

        if (ud_sProjectCode != "")
        {
            WFCSB.AddStrategy(new Strategy(WorkFlowCommonStrategyName.ProjectCode, ud_sProjectCode));
        }

        if (ud_sStatus != "")
        {
            WFCSB.AddStrategy(new Strategy(WorkFlowCommonStrategyName.Status, ud_sStatus));
        }

        if (ud_sProcedureCode != "")
        {
            WFCSB.AddStrategy(new Strategy(WorkFlowCommonStrategyName.ProcedureCode, ud_sProcedureCode));
        }

        if (ud_sWorkFlowTitle != "")
        {
            WFCSB.AddStrategy(new Strategy(WorkFlowCommonStrategyName.WorkFlowTitle, ud_sWorkFlowTitle));
        }

        if (ud_sWorkFlowID != "")
        {
            WFCSB.AddStrategy(new Strategy(WorkFlowCommonStrategyName.WorkFlowID, ud_sWorkFlowID));
        }

        ArrayList arA = new ArrayList();
        arA.Add("090201");
        arA.Add(user.UserCode);
        arA.Add(user.BuildStationCodes());
        WFCSB.AddStrategy(new Strategy(WorkFlowCommonStrategyName.AccessRange, arA));



        //排序
        string sortsql = RmsPM.BLL.GridSort.GetSortSQL(ViewState, "WorkFlowCommon.CreateDate DESC,WorkFlowID");

        string sql = WFCSB.BuildQueryViewString();

        if (sortsql != "")
        {
            //点列标题排序
            sql +=  " order by " + sortsql;
        }

        this.ViewState.Add("SearchString", sql);
    }

    protected void btnSearch_ServerClick(object sender, System.EventArgs e)
    {
        BuildSearchString();
        this.dgList.CurrentPageIndex = 0;
        LoadDataGrid();
    }


    protected void GridPagination1_PageIndexChange(object sender, System.EventArgs e)
    {
        try
        {
            this.LoadDataGrid();
        }
        catch (Exception ex)
        {
            ApplicationLog.WriteLog(this.ToString(), ex, "");
            Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
        }
    }


}
