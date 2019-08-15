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

public partial class LocaleVise_LocaleViseAudit : RmsPM.Web.PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnPassAudit_Click(object sender, EventArgs e)
    {
        List<TiannuoPM.MODEL.LocaleViseCostModel> CostList = new List<TiannuoPM.MODEL.LocaleViseCostModel>();
        RmsPM.BFL.LocaleViseBFL ViseBFL = new RmsPM.BFL.LocaleViseBFL();
        foreach(GridViewRow grow in this.GridView1.Rows)
        {
            if (grow.FindControl("TxtCheckMoney") != null)
            {
                
                TiannuoPM.MODEL.LocaleViseCostModel CostModel= ViseBFL.GetLocalViseCost(int.Parse(this.GridView1.DataKeys[grow.RowIndex].Value.ToString()))[0];
                CostModel.CheckMoney = decimal.Parse(((Infragistics.WebUI.WebDataInput.WebNumericEdit)grow.FindControl("TxtCheckMoney")).Value.ToString());
                CostList.Add(CostModel);
            }
        }
        ViseBFL.PassAudit(int.Parse(Request["ViseCode"].ToString()),CostList);
        Response.Write("<script>window.opener.WinReload();window.close();</script>");
        Response.End();
    }
    protected void btnNoPassAudit_Click(object sender, EventArgs e)
    {
        RmsPM.BFL.LocaleViseBFL ViseBFL = new RmsPM.BFL.LocaleViseBFL();
        ViseBFL.NoPassAudit(int.Parse(Request["ViseCode"].ToString()));
        Response.Write("<script>window.opener.WinReload();window.close();</script>");
        Response.End();
    }
}
