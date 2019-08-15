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

public partial class WorkFlowContral_WorkFlowStartup : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.LoadData();
        }
    }


    protected void LoadData()
    {
        DataTable dtCommons = RmsPM.BLL.WorkFlowRule.GetWorkFlowCommons("1","1");
        this.dgList.DataSource = dtCommons;
        this.dgList.DataBind();

    }


    protected void dgList_SelectedIndexChanged(object sender, EventArgs e)
    {
        //string ProcedureCode = dgList.SelectedItem.Cells[7].Text.ToString();
        //Rms.WorkFlow.Procedure procedure = Rms.WorkFlow.DefinitionManager.GetProcedureDifinition(ProcedureCode, false);
        //Rms.WorkFlow.DefinitionManager.SaveAsProcedureDifinition(procedure);
        LoadData();
    }
}
