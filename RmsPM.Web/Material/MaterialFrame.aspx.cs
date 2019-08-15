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

using System.Collections.Generic;
using TiannuoPM.MODEL;

public partial class Material_materialframe : RmsPM.Web.PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!user.HasRight("150101"))
            {
                Response.Redirect("../RejectAccess.aspx");
                Response.End();
            }
          //  Response.Write(user.HasRight("150102"));
            if (!user.HasRight("150102"))
            {
                this.btnNew.Visible = false;
            
            }
            if (!user.HasRight("150105"))
            {
                this.btnInputMaterial.Visible = false;

            }

        }
    }
    /*
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        GridView1.DataBind();
    }
    protected void GridView1_DataBinding(object sender, EventArgs e)
    {
    //    string ViseStatusStr = "0";
  //      ObjectDataSource1.SelectParameters["ViseStatusInStr"].DefaultValue = ViseStatusStr;
    }
     */
}
