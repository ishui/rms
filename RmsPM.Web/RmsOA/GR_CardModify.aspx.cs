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


using RmsOA.MODEL;
using RmsOA.BFL;
using RmsPM.Web;

public partial class RmsOA_GR_CardModify : PageBase
{
    #region "事件"
    /// <summary>
    /// 如果QueryString["Type"]非空并且等于"Add"
    /// FormView转换为Insert状态
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            string type = Request.QueryString["Type"];
            if (!String.IsNullOrEmpty(type))
            {
                if (type.Equals("Add"))
                {
                    CardFormView.ChangeMode(FormViewMode.Insert);
                }
            }
        }
    }
    /// <summary>
    /// 将FormView转换为编辑状态
    /// </summary>
    protected void EditButton_Click(object sender, EventArgs e)
    {
        this.CardFormView.ChangeMode(FormViewMode.Edit);
    }
    /// <summary>
    ///更新表格GK_OA_CardsForder数据
    /// </summary>
    protected void UpdateButton_Click(object sender, EventArgs e)
    {
        int code = Int32.Parse(Request.QueryString["Code"]);
        GK_OA_CardsFolderModel cfModel = this._SetCardFolderModel();
        cfModel.Code = code;
        GK_OA_CardsFolderBFL cfBFL = new GK_OA_CardsFolderBFL();
        cfBFL.Update(cfModel);
        Response.Write("<script>window.opener.location.reload();</script>");
        this.CardFormView.ChangeMode(FormViewMode.ReadOnly);
        //this._ReloadPage(code.ToString());
    }

    /// <summary>
    /// 添加新数据到GK_OA_CardsForder数据
    /// </summary>
    protected void InsertButton_Click(object sender, EventArgs e)
    {
        GK_OA_CardsFolderModel cfModel = this._SetCardFolderModel();
        GK_OA_CardsFolderBFL cfBFL = new GK_OA_CardsFolderBFL();
        int code = cfBFL.Insert(cfModel);
        //this.CardFormView.ChangeMode(FormViewMode.ReadOnly);
        //this._ReloadPage(code.ToString());
        Response.Write("<script>window.opener.location.reload();window.close();</script>");
        
    }
    /// <summary>
    /// 删除一条名片记录，关闭当前页，更新父页面！
    /// </summary>
    protected void DeleteButton_Click(object sender, EventArgs e)
    {
        GK_OA_CardsFolderBFL cfBFL = new GK_OA_CardsFolderBFL();
        GK_OA_CardsFolderModel cfModel = new GK_OA_CardsFolderModel();
        cfModel.Code = Int32.Parse(Request.QueryString["Code"]);
        cfBFL.Delete(cfModel);
        Response.Write("<script>window.opener.location.reload();window.close();</script>");
    }
    #endregion

    #region "函数"
    private void _ReloadPage(string code)
    {
        Response.Redirect("GR_CardModify.aspx?Type=Read&Code=" + code);
    }
    private GK_OA_CardsFolderModel _SetCardFolderModel()
    {
        GK_OA_CardsFolderModel cfModel = new GK_OA_CardsFolderModel();
        TextBox tbxName = (TextBox)(this.CardFormView.Row.FindControl("UserNameTextBox"));
        DropDownList ddlSex = (DropDownList)(this.CardFormView.Row.FindControl("SexDropDownList"));
        TextBox tbxAge = (TextBox)(this.CardFormView.Row.FindControl("AgeTextBox"));
        TextBox tbxCompanyName = (TextBox)(this.CardFormView.Row.FindControl("CompanyNameTextBox"));
        TextBox tbxCompanyAddress = (TextBox)(this.CardFormView.Row.FindControl("CompanyAddressTextBox"));
        TextBox tbxHeaderShip = (TextBox)(this.CardFormView.Row.FindControl("HeadshipTextBox"));
        TextBox tbxDept = (TextBox)(this.CardFormView.Row.FindControl("DeptTextBox"));
        TextBox tbxPostalcode = (TextBox)(this.CardFormView.Row.FindControl("PostalcodeTextBox"));
        TextBox tbxPhone = (TextBox)(this.CardFormView.Row.FindControl("PhoneTextBox"));
        TextBox tbxFax = (TextBox)(this.CardFormView.Row.FindControl("FaxTextBox"));
        TextBox tbxMobile = (TextBox)(this.CardFormView.Row.FindControl("MobileTextBox"));
        TextBox tbxHomePhone = (TextBox)(this.CardFormView.Row.FindControl("HomePhoneTextBox"));
        TextBox tbxEmail = (TextBox)(this.CardFormView.Row.FindControl("EmailTextBox"));
        TextBox tbxNetAddress = (TextBox)(this.CardFormView.Row.FindControl("NetAddressTextBox"));
        TextBox tbxHobby = (TextBox)(this.CardFormView.Row.FindControl("HobbyTextBox"));
        TextBox tbxHomeAddress = (TextBox)(this.CardFormView.Row.FindControl("HomeAddressTextBox"));
        TextBox tbxNativePlace = (TextBox)(this.CardFormView.Row.FindControl("NativePlaceTextBox"));
        DropDownList ddlWedLock = (DropDownList)(this.CardFormView.Row.FindControl("WedLockDropDownList"));
        DropDownList ddlCardType = (DropDownList)(this.CardFormView.Row.FindControl("CardTypeDropDownList"));
        TextBox tbxRemark = (TextBox)(this.CardFormView.Row.FindControl("RemarkTextBox"));
        RadioButtonList rblPublicStatus = (RadioButtonList)(this.CardFormView.Row.FindControl("PublicSatuesRadioButtonList"));
        AspWebControl.Calendar birthday = (AspWebControl.Calendar)(this.CardFormView.Row.FindControl("Birthday"));
        AspWebControl.Calendar contactTime = (AspWebControl.Calendar)(this.CardFormView.Row.FindControl("ContactTime"));
        cfModel.UserId = this.user.UserID;
        cfModel.UserName = tbxName.Text.Trim();
        int age;
        if (Int32.TryParse(tbxAge.Text.Trim(),out age))
        {
            cfModel.Age = age;
        }
        cfModel.CompanyName = tbxCompanyName.Text.Trim();
        cfModel.CompanyAddress = tbxCompanyAddress.Text.Trim();
        cfModel.Headship = tbxHeaderShip.Text.Trim();
        cfModel.Dept = tbxDept.Text.Trim();
        cfModel.Postalcode = tbxPostalcode.Text.Trim();
        cfModel.Phone = tbxPhone.Text.Trim();
        cfModel.Fax = tbxFax.Text.Trim();
        cfModel.Mobile = tbxMobile.Text.Trim();
        cfModel.HomePhone = tbxHomePhone.Text.Trim();
        cfModel.Email = tbxEmail.Text.Trim();
        cfModel.NetAddress = tbxNetAddress.Text.Trim();
        cfModel.Hobby = tbxHobby.Text.Trim();
        cfModel.HomeAddress = tbxHomeAddress.Text.Trim();
        cfModel.NativePlace = tbxNativePlace.Text.Trim();
        cfModel.Remark = tbxRemark.Text.Trim();
        cfModel.PublicStatus = rblPublicStatus.SelectedItem.Text;
        if (!ddlSex.SelectedIndex.Equals(0))
        {
            cfModel.Sex = ddlSex.SelectedItem.Text.Trim();
        }
        else 
        {
            cfModel.Sex = "";
        }
        if (!ddlCardType.SelectedIndex.Equals(0))
        {
            cfModel.CardType = ddlCardType.SelectedItem.Text.Trim();
        }
        else
        {
            cfModel.CardType = "";
        }
        if (!ddlWedLock.SelectedIndex.Equals(0))
        {
            cfModel.Wedlock = ddlWedLock.SelectedItem.Text.Trim();
        }
        else
        {
            cfModel.Wedlock = "";
        }
        DateTime time;
        if (DateTime.TryParse(birthday.Value,out time))
        {
            if (time.Year < 1800)
            {
                cfModel.Birthday = DateTime.Now;
            }
            else
            {
                cfModel.Birthday = time;
            }
        }
        else
        {
            cfModel.Birthday = DateTime.Now;
        }
        if (DateTime.TryParse(contactTime.Value,out time))
        {
            if(time.Year < 1800)
            {
                cfModel.ContactTime = DateTime.Now;
            }
            else
            {
                cfModel.ContactTime = time;
            }
        }
        else
        {
            cfModel.ContactTime = DateTime.Now;
        }
        return cfModel;
    }
    #endregion

    protected void CardFormView_DataBound(object sender, EventArgs e)
    {
        if (this.CardFormView.CurrentMode == FormViewMode.ReadOnly)
        {
            if (!user.HasRight("350102"))
            {
                Button btnEdit = (Button)(this.CardFormView.Row.FindControl("EditButton"));
                btnEdit.Visible = false;
            }
            if (!user.HasRight("350103"))
            {
                Button btnDelete = (Button)(this.CardFormView.Row.FindControl("DeleteButton"));
                btnDelete.Visible = false;
            }
        }
    }
}

