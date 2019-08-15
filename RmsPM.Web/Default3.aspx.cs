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
using Rms.LogHelper;
namespace RmsPM.Web
{
    public partial class Default3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
       
        protected void Button1_Click1(object sender, EventArgs e)
        {
 Response.Write(txtPerCash1.ValueDecimal);
        LogHelper.WriteLog("1234342334");
        LogHelper.WriteLog("ffsd", new Exception("test"));
        }
}
}
