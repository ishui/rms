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

public partial class RmsOA_CL_CarMaintenanceList : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void ObjectDataSource1_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        decimal OutMil = 0;
        decimal OutPrice = 0;
        System.Collections.Generic.List<RmsOA.MODEL.GK_OA_CarMaintenanceModel> lst = (System.Collections.Generic.List<RmsOA.MODEL.GK_OA_CarMaintenanceModel>)e.ReturnValue;
        foreach (RmsOA.MODEL.GK_OA_CarMaintenanceModel Model in lst)
        {

            OutMil += Model.Mil;
            OutPrice += Model.MPrice;
        }
        this.GridView1.Columns[2].FooterText = "合计："+RmsPM.BLL.MathRule.GetDecimalShowString(OutMil);
        this.GridView1.Columns[3].FooterText ="合计："+ RmsPM.BLL.MathRule.GetDecimalShowString(OutPrice);

    }
}
