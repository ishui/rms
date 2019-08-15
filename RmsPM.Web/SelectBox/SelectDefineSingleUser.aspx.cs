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
using RmsPM.Web;
using RmsOA.MODEL;
using RmsOA.BFL;



public partial class SelectBox_SelectDefineSingleUser : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string returnFunc = Request.QueryString["ReturnFunc"];
        if (string.IsNullOrEmpty(returnFunc))
        {
            returnFunc = "DoSelectUser";
        }
        ViewState["ReturnFunc"] = returnFunc;

        InitControl();
    }
    void InitControl()
    {
        string groupCode = Request["GroupCode"];

        try
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("RelationCode");
            dt.Columns.Add("AccessRangeType");
            dt.Columns.Add("ImageFileName");
            dt.Columns.Add("Name");

            if (!string.IsNullOrEmpty(groupCode))
            {
                UserHelpUserQueryModel queryModel = new UserHelpUserQueryModel();
                queryModel.GroupCodeEqual = int.Parse(groupCode);
                RmsOA.BFL.UserHelpUserBFL bfl = new RmsOA.BFL.UserHelpUserBFL();
                List<UserHelpUserModel> listModel = bfl.GetUserHelpUserList(queryModel);
                if (listModel != null)
                {
                    foreach (UserHelpUserModel model in listModel)
                    {
                        DataRow drNew = dt.NewRow();
                        drNew["RelationCode"] = model.UserCode;
                        drNew["Name"] = RmsPM.BLL.SystemRule.GetUserName(model.UserCode);
                        drNew["AccessRangeType"] = 0;
                        drNew["ImageFileName"] = "user.gif";
                        dt.Rows.Add(drNew);
                    }
                }
            }

            this.repeaterSU.DataSource = dt;
            this.repeaterSU.DataBind();
            dt.Dispose();
        }
        catch (Exception ex)
        {
            ApplicationLog.WriteLog(this.ToString(), ex, "加载人员列表失败");
        }
    }
}
