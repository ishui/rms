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

public partial class RmsOA_GK_OA_SubmitAccoutList : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (user.HasRight("320501"))
            {
                this.NewButton.Visible = true;
                this.NewButton.Attributes["OnClick"] = "javascript:OpenMiddleWindow('GK_OA_SubmitAccountEdit.aspx?ActType=add','SubmitAccountEdit');";
            }
            else
            {
                this.NewButton.Visible = false;
            }
        }
    }
    protected void Button1_ServerClick(object sender, EventArgs e)
    {
        this.GridView1.DataBind();
    }

   
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.DataRow:
                decimal sumStandardCost = 0;
                RmsOA.BFL.GK_OA_SubmitAccountDtlBFL bfl = new RmsOA.BFL.GK_OA_SubmitAccountDtlBFL();
                System.Collections.Generic.List<RmsOA.MODEL.GK_OA_SubmitAccountDtlModel> lst = bfl.GetGK_OA_SubmitAccountDtlList(((RmsOA.MODEL.GK_OA_SubmitAccountMainModel)e.Row.DataItem).Code.ToString());

                foreach (RmsOA.MODEL.GK_OA_SubmitAccountDtlModel Model in lst)
                {

                    sumStandardCost += Model.StandardCost;
                   
                }
                ((Label)e.Row.FindControl("StandardCostLabel")).Text = RmsPM.BLL.MathRule.GetDecimalShowString(sumStandardCost);
                
                break;
            default:
                break;
        }
    }
}
