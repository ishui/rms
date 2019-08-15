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

public partial class RmsDM_DocumentDirectoryList : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string NodeValue = Request["NodeValue"];
        if (!IsPostBack)
        {
            if (NodeValue != "0"&&NodeValue!=null)
            {
                this.btnCopy.Visible = true;            
                
            }
            if (!user.HasRight("3601"))
            {
                this.btnAdd.Visible = true;
            }
        }
        
    }
}
