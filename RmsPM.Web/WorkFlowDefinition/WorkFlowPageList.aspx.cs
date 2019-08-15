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
using Rms.ORMap;

public partial class WorkFlowDefinition_WorkFlowPageList : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    private void IniPage()
    { }

    private void LoadDataGrid()
    {
        try
        {
            EntityData entity = RmsPM.DAL.EntityDAO.WorkFlowDAO.GetAllWorkFlowPage();
            this.gvPageList.DataSource = entity.CurrentTable;
            this.gvPageList.DataBind();
            entity.Dispose();
        }
        catch (Exception ex)
        { throw ex; }
    }

}
