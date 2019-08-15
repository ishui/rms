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
using Rms.ORMap;
using Rms.Web;
using RmsPM.DAL;
using RmsPM.DAL.EntityDAO;
using RmsPM.BLL;

namespace RmsPM.Web.SelectBox
{
	/// <summary>
	/// SelectSUMain 的摘要说明。
	/// </summary>
	public partial class SelectSUMain : PageBase
	{

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                string str = this.Request.Url.ToString();
                IniPage();
            }
            if (user.HasRight("3506"))
            {
                titlehead.Visible = true;
            }
        }

        private void IniPage()
        {
            this.txtUserCodes.Value = Request["UserCodes"];
            this.txtStationCodes.Value = Request["StationCodes"];
            this.txtType.Value = Request["Type"];

            //返回函数名
            string ReturnFunc = Request.QueryString["ReturnFunc"] + "";
            if (ReturnFunc == "")
            {
                ReturnFunc = "getReturnStationUser";
            }
            ViewState["ReturnFunc"] = ReturnFunc;

            string[] arrU = txtUserCodes.Value.Split(",".ToCharArray());
            string[] arrS = txtStationCodes.Value.Split(",".ToCharArray());

            string sTemp = "";
            foreach (string code in arrU)
            {
                if (code != "")
                {
                    sTemp += code + ",0," + BLL.SystemRule.GetUserName(code) + ",;";
                }
            }

            this.txtSelectU.Value = sTemp;

            sTemp = "";
            foreach (string code in arrS)
            {
                if (code != "")
                {
                    sTemp += code + ",1," + BLL.SystemRule.GetStationName(code) + "," + BLL.SystemRule.GetUnitFullName(BLL.SystemRule.GetUnitByStationCode(code)) + ";";
                }
            }

            this.txtSelectS.Value = sTemp;

        }
		
	}
}
