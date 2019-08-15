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
using System.Text;

using RmsOA.BFL;
using RmsOA.MODEL;
using RmsPM.Web;

public partial class RmsOA_CardsFolder : PageBase
{
    /// <summary>
    /// 根据UserID，加载个人名片夹到桌面显示
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!user.HasRight("350101"))
        {
            this.NewButton.Visible = false;
        }
        GK_OA_CardsFolderQueryModel cfQueryModel = new GK_OA_CardsFolderQueryModel();
        cfQueryModel.UserIdEqual = user.UserID;
        GK_OA_CardsFolderBFL cfBFL = new GK_OA_CardsFolderBFL();
        this.GridView1.DataSource = cfBFL.GetGK_OA_CardsFolderList(cfQueryModel);
        this.GridView1.DataBind();
    }

    /// <summary>
    /// 
    /// </summary>
    protected void btSearch_Click(object sender, EventArgs e)
    {
        GK_OA_CardsFolderQueryModel cfQueryModel = new GK_OA_CardsFolderQueryModel();
        cfQueryModel.QueryConditionStr = this._ConstructQueryString();
        GK_OA_CardsFolderBFL cfBFL = new GK_OA_CardsFolderBFL();
        this.GridView1.DataSource = cfBFL.GetGK_OA_CardsFolderList(cfQueryModel);
        this.GridView1.DataBind();
    }

    private string _ConstructQueryString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("WHERE ");
        if (!String.IsNullOrEmpty(this.tbxName.Text.Trim()))
        {
            sb.Append("UserName='" + this.tbxName.Text.Trim() + "' AND ");
        }
        if (!String.IsNullOrEmpty(this.tbxDept.Text.Trim()))
        {
            sb.Append("Dept LIKE'%" + this.tbxDept.Text.Trim() + "%' AND ");
        }
        if (!String.IsNullOrEmpty(this.tbxMobile.Text.Trim()))
        {
            sb.Append("Mobile ='" + this.tbxMobile.Text.Trim() + "' AND ");
        }
        if (this.PhoneTextBox.Text.Trim().Equals(String.Empty))
        {
            sb.Append("Phone ='" + this.PhoneTextBox.Text.Trim() + "' AND ");
        }
        if (!ddlType.SelectedIndex.Equals(0))
        {
            sb.Append("CardType='" + this.ddlType.SelectedItem.Text.Trim() + "' AND ");
        }
        if (ddlScope.SelectedIndex.Equals(0))
        {
            sb.Append("UserId='" + user.UserID + "' AND ");
        }
        else
        {
            sb.Append("PublicStatus = '公开' AND ");
        }
        sb.Append("1=1");
        return sb.ToString();
    }
}
