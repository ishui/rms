using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;

using Rms.ORMap;
using Rms.Web;
using RmsPM.DAL;
using RmsPM.DAL.EntityDAO;
using RmsPM.BLL;
using RmsOA.MODEL;

namespace RmsPM.Web.SelectBox
{
    public partial class SelectDefineUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            // 在此处放置用户代码以初始化页面
            if (!this.IsPostBack)
            {
                LoadData();
            }

        }

        private void LoadData()
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
}
