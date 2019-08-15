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
using Rms.ORMap;

public partial class Finance_PaymentItemDescriptionView : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string ud_sPaymentCode = Request["PaymentCode"] + "";
        EntityData entity = RmsPM.DAL.EntityDAO.PaymentDAO.GetPaymentByCode(ud_sPaymentCode);
        if (entity.HasRecord())
        {
            this.OpinionDiv.InnerHtml = entity.GetString("Remark").Replace("\n", "<br>"); ;
        }
    }
}
