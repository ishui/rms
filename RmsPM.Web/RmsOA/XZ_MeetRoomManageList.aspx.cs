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
using RmsOA.BFL;
using RmsPM.Web;

public partial class RmsOA_XZ_MeetRoomManageList : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!user.HasRight(OperRightCollection.MeetRoomOper.Add))
        {
            this.NewButton.Disabled = true ;
        }
    }
}
