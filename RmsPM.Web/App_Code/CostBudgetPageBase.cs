using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// CostBudgetPageBase 的摘要说明

/// </summary>
public class CostBudgetPageBase:RmsPM.Web.PageBase
{
    /// <summary>
    /// 动态费用表是否要显示“已结算”列
    /// </summary>
    /// <returns></returns>
    public bool ShowContractAccountMoney
    {
        get { return (this.up_sPMName.ToUpper() == "ShidaiPM".ToUpper()); }
    }

    /// <summary>
    /// “差额”是否要着重提醒

    /// </summary>
    /// <returns></returns>
    public bool IsRemindContractBudgetBalance
    {
        get { return (this.up_sPMName.ToUpper() == "ShidaiPM".ToUpper()); }
    }

    /// <summary>
    /// 动态费用表是否要显示变更前成本对比
    /// </summary>
    public bool ShowColBeforeChange
    {
        get { return (RmsPM.BLL.ConvertRule.ToString(Request["FullCost"]) == "1"); }
    }

    /// <summary>
    /// 动态费用表是否要显示预算

    /// </summary>
    public bool ShowColBudget
    {
        get { return (RmsPM.BLL.ConvertRule.ToString(Request["HideBudget"]) != "1"); }
    }
}
