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

public partial class TemplateOpinionManage_TemplateOpinionQuery : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LoadData();

    }
    /// ****************************************************************************
    /// <summary>
    /// 数据加载
    /// </summary>
    /// ****************************************************************************
    private void LoadData()
    {
        try
        {
            TemplateOpinion cTemplateOpinion = new TemplateOpinion();
            cTemplateOpinion.UserCode = user.UserCode;

            DataTable dt = cTemplateOpinion.GetTemplateOpinions();
            this.dgList.DataSource = dt;
            this.dgList.DataBind();
            this.gpControl.RowsCount = dt.Rows.Count.ToString();
            dt.Dispose();
        }
        catch (Exception ex)
        {
            ApplicationLog.WriteLog(this.ToString(), ex, "");
        }
    }
    /// ****************************************************************************
    /// <summary>
    /// 分页事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// ****************************************************************************
    protected void gpControl_PageIndexChange(object sender, System.EventArgs e)
    {
        LoadData();
    }
}
