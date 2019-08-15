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
using RmsPM.DAL.EntityDAO;
using RmsPM.DAL.QueryStrategy;
using RmsPM.Web;
using Rms.ORMap;
using Rms.WorkFlow;

public partial class WorkFlowControl_WorkFlowList : System.Web.UI.UserControl
{

    private int _WorkFlowCount;
    /// <summary>
    /// 总数
    /// </summary>
    public int WorkFlowCount
    {
        get
        {
            if (_WorkFlowCount == null)
            {
                if (this.ViewState["_WorkFlowCount"] != null)
                    return (int)this.ViewState["_WorkFlowCount"];
                return 0;
            }
            return (int)_WorkFlowCount;
        }
        set
        {
            this._WorkFlowCount = value;
            this.ViewState["_WorkFlowCount"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void GridPagination1_PageIndexChange(object sender, System.EventArgs e)
    {
        try
        {
            LoadData();
        }
        catch (Exception ex)
        {
            ApplicationLog.WriteLog(this.ToString(), ex, "");
        }
    }
    private void LoadData()
    {
        try
        {
            string sql = (string)this.ViewState["SqlString"];
            QueryAgent qa = new QueryAgent();
            DataSet ds = qa.ExecSqlForDataSet(sql);
            qa.Dispose();
            this.dgList.DataSource = ds;
            this.dgList.DataBind();
            this.GridPagination1.RowsCount = ds.Tables[0].Rows.Count.ToString();
            this.WorkFlowCount = ds.Tables[0].Rows.Count;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    /// <summary>
    /// 生成sql语句
    /// </summary>
    private void BuildSqlString()
    {
        try
        {
            RmsPM.DAL.QueryStrategy.WorkFlowHistory sb = new RmsPM.DAL.QueryStrategy.WorkFlowHistory();
            sb.AddStrategy(new Strategy(WorkFlowHistoryStrategyName.ProcedureNameAndApplicationCodein, this.ProcedureNameAndApplicationCodeList));

            if(IsEndFlow)
                sb.AddStrategy(new Strategy(RmsPM.DAL.QueryStrategy.WorkFlowHistoryStrategyName.Status, "End"));
            
            if (!((User)Session["User"]).HasOperationRight("090102"))
            {
                sb.AddStrategy(new Strategy(WorkFlowHistoryStrategyName.ActUserCode, ((User)Session["User"]).UserCode));
            }
            sb.AddOrder("CreateDate", false);

            string sql = sb.BuildMainQueryString();
            this.ViewState.Add("SqlString", sql);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    /// <summary>
    /// 帮定数据
    /// </summary>
    public void DataBound()
    {
        try
        {
            BuildSqlString();
            this.GridPagination1.CurrentPageIndex = 1;
            LoadData();
        }
        catch(Exception ex)
        {
            ApplicationLog.WriteLog(this.ToString(), ex, "");
        }
    }

    

    /// <summary>
    /// 是否为结束流程

    /// </summary>
    public bool IsEndFlow
    {
        get 
        {
            if (this.ViewState["IsEndFlow"] == null)
                return false;
            return (bool)this.ViewState["IsEndFlow"];
        }
        set 
        {
            this.ViewState["IsEndFlow"] = value;
        }
    }
    /// <summary>
    /// 流程名称+业务代码 集合 例如：'合同请款审核100001','合同审核100001','合同结算审核100001' 
    /// </summary>
    public string ProcedureNameAndApplicationCodeList
    {
        get
        {
            if (this.ViewState["ProcedureNameAndApplicationCodeList"] == null)
                return "";
            return this.ViewState["ProcedureNameAndApplicationCodeList"].ToString();
        }
        set
        {
            this.ViewState["ProcedureNameAndApplicationCodeList"] = value;
        }
    }
}
