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

public partial class DesignChange_DesignChangeAudit : RmsPM.Web.PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnPassAudit_Click(object sender, EventArgs e)
    {
        RmsPM.BFL.DesignChangeBFL dc = new RmsPM.BFL.DesignChangeBFL();
        bool isYFintel = false;
        string number="";
        if (this.up_sPMNameLower == "yefengpm")
        {
            TiannuoPM.MODEL.DesignChangeModel design = dc.GetDesignChange(int.Parse(Request["DesignChangeCode"].ToString()));
            number = design.Number;
            if (design.Type == "1")
            {
                if (number.IndexOf('-') > 0)
                {
                    string[] numbers = number.Split('-');
                    if (numbers.Length == 5)
                    {
                        number = numbers[0] + "-" + numbers[1] + "-" + numbers[3] + "(业)" + RmsPM.DAL.EntityDAO.SystemManageDAO.GetFormatSysCode("YFID" + Request["projectCode"] + numbers[1], "{####}", 1001);
                        isYFintel = true;
                    }
                }

            }
        }
        if (isYFintel)
        {
            dc.YFPassAudit(int.Parse(Request["DesignChangeCode"].ToString()), number);
        }
        else
        {
            dc.PassAudit(int.Parse(Request["DesignChangeCode"].ToString()));
        }
        dc.UpdateAuditMoney(int.Parse(Request["DesignChangeCode"].ToString()), txtChangeMoney.Text, decimal.Parse(TxtTotalMoney.Value.ToString()));
        Response.Write("<script>window.opener.location.reload();window.close();</script>");
    }
    protected void btnNoPassAudit_Click(object sender, EventArgs e)
    {
        RmsPM.BFL.DesignChangeBFL dc = new RmsPM.BFL.DesignChangeBFL();
        bool isYFintel = false;
        string number="";
        if (this.up_sPMNameLower == "yefengpm")
        {
            TiannuoPM.MODEL.DesignChangeModel design = dc.GetDesignChange(int.Parse(Request["DesignChangeCode"].ToString()));
            number = design.Number;
            if(design.Type=="1"){
                if (number.IndexOf('-') > 0)
                {
                    string[] numbers = number.Split('-');
                    if (numbers.Length == 5)
                    {
                        number = numbers[0] + "-" + numbers[1] + "-" + numbers[3] + "(审)" + RmsPM.DAL.EntityDAO.SystemManageDAO.GetFormatSysCode("YFID" + Request["projectCode"] + numbers[1], "{####}", 1001);
                        isYFintel = true;
                    }
                } 
               
            }           
        }
        if (isYFintel)
        {
            dc.YFNoPass(int.Parse(Request["DesignChangeCode"].ToString()), number);
        }
        else
        {
            dc.NoPass(int.Parse(Request["DesignChangeCode"].ToString()));
        }
        Response.Write("<script>window.opener.location.reload();window.close();</script>");
    }
}
