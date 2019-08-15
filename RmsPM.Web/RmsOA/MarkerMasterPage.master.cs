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

public partial class RmsOA_MarkerMasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public bool DeptManage
    {
        set 
        {
            if (value.Equals(true))
            {
                this.deptManager.Style["Display"] = "block";
            }
        
        }
    }
    public bool VicePresident
    {
        set
        {
            if (value.Equals(true))
            {
                this.vicePresident.Style["Display"] = "block";
            }

        }
    }
    public bool President
    {
        set
        {
            if (value.Equals(true))
            {
                this.presidents.Style["Display"] = "block";
            }

        }
    }
    public bool MonthState
    {
        set
        {
            if (value.Equals(true))
            {
                this.monthState.Style["Display"] = "block";
            }

        }
    }
}
