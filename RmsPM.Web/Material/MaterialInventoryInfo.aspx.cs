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

public partial class Material_MaterialInventoryInfo : RmsPM.Web.PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
       /*
        string MaterialCode = Request.QueryString["MaterialCode"] + "";
        ArrayList ar = user.GetResourceRight(MaterialCode, "V_MaterialInventory");
        if (!ar.Contains("150101"))
        {
            Response.Redirect("../RejectAccess.aspx");
            Response.End();
        }*/
    }
    protected void ObjectDataSource2_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        decimal InQty = 0;
        decimal OutQty = 0;
        System.Collections.Generic.List<TiannuoPM.MODEL.V_MaterialInventoryIOModel> lst = (System.Collections.Generic.List<TiannuoPM.MODEL.V_MaterialInventoryIOModel>)e.ReturnValue;
        foreach (TiannuoPM.MODEL.V_MaterialInventoryIOModel Model in lst)
        {
            InQty += Model.InQty;
            OutQty += Model.OutQty;
        }

        ((GridView)this.FormView1.Row.FindControl("GridView1")).Columns[3].FooterText = RmsPM.BLL.MathRule.GetDecimalShowString(InQty);
        ((GridView)this.FormView1.Row.FindControl("GridView1")).Columns[4].FooterText = RmsPM.BLL.MathRule.GetDecimalShowString(OutQty);
    }
}
