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
using RmsPM.BFL;
using RmsPM.BLL;
using System.Collections.Generic;

public partial class test : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void UnitDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        Response.Write("event");
    }
}
