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
using System.Xml;
using System.Collections.Generic;

using RmsOA.BFL;
using RmsPM.Web;
using RmsOA.MODEL;

namespace RmsPM.Web.SelectBox
{
    public partial class SelectDefineGroup : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            InitControl();
        }
        void InitControl()
        {
            UserHelpGroupQueryModel queryModel = new UserHelpGroupQueryModel();
            queryModel.UserCodeEqual = user.UserCode;
            UserHelpGroupBFL bfl = new UserHelpGroupBFL();
            this.GroupName.DataSource = bfl.GetUserHelpGroupList(queryModel);
            this.GroupName.DataBind();
        }
    }
}
