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
using RmsOA.MODEL;
using RmsOA.BFL;

public partial class RmsOA_XZ_ConferenceAudit : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(Request.QueryString["Code"]))
        {
            ConferenceUserListBFLFacade culBFLF = new ConferenceUserListBFLFacade();
            culBFLF.AuditAndSendMeetMsg(Request.QueryString["Code"]);
        }
    }

    protected void Page_PreLoad(object sender, EventArgs e)
    {
        if (!user.HasRight("320102"))
        {
            Response.Write("<script> window.alert('��û��Ȩ�޷��ʸ�ҳ�棬����������������Ա��ϵ��');history.back(-1);</script>");
        }
    }
}
