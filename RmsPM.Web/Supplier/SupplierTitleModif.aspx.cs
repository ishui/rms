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
using Rms.Web;

public partial class Supplier_SupplierTitleModif : PageBase
{


    /// <summary>
    /// 项目代码
    /// </summary>
    private string _SupplierTitleCode = "";

    private string _SupplierCode = "";


    /// <summary>
    /// 业务数据代码
    /// </summary>
    public string SupplierTitleCode
    {
        get
        {
            if (_SupplierTitleCode == "")
            {
                if (this.ViewState["_SupplierTitleCode"] != null)
                    return this.ViewState["_SupplierTitleCode"].ToString();
                return "";
            }
            return _SupplierTitleCode;
        }
        set
        {
            _SupplierTitleCode = value;
            this.ViewState["_SupplierTitleCode"] = value;
        }
    }

    /// <summary>
    /// 厂商CODE
    /// </summary>
    public string SupplierCode
    {
        get
        {
            if (_SupplierCode == "")
            {
                if (this.ViewState["_SupplierCode"] != null)
                    return this.ViewState["_SupplierCode"].ToString();
                return "";
            }
            return _SupplierCode;
        }
        set
        {
            _SupplierCode = value;
            this.ViewState["_SupplierCode"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            if (!this.user.HasRight("141801"))
            {
                Response.Redirect("../RejectAccess.aspx");
                Response.End();
            }
            this.LoadDate();
        }
    }

    private void LoadDate()
    {
        this.SupplierTitleCode = Request["SupplierTitleCode"] + "";
        this.SupplierCode = Request["SupplierCode"] + "";

        if (!this.user.HasRight("141802") && !this.user.HasRight("141803"))
            this.btnSave.Visible = false;

        if (!this.user.HasRight("141804"))
        {
            this.btnDelete.Visible = false;
        }

        if (this.SupplierTitleCode != "")
        {
            RmsPM.BLL.SupplierTitle cSupplierTitle = new RmsPM.BLL.SupplierTitle();
            cSupplierTitle.SupplierTitleCode = this.SupplierTitleCode;
            DataTable dt = cSupplierTitle.GetSupplierTitles();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                this.lblCompanyName.Text = RmsPM.BLL.SupplierRule.GetSupplierName(dt.Rows[i]["SupplierCode"].ToString());
                this.TxtCompanyTitle.Value = dt.Rows[i]["Title"].ToString();
                this.TxtBankAccount.Value = dt.Rows[i]["BankAccount"].ToString();
            }
        }
        else
        {
            this.lblCompanyName.Text = RmsPM.BLL.SupplierRule.GetSupplierName(this.SupplierCode);
            this.btnDelete.Visible = false;
        }





    }
    protected void btnSave_ServerClick(object sender, EventArgs e)
    {
        try
        {
            if (this.TxtCompanyTitle.Value.Trim() == "")
            {
                Response.Write(JavaScript.Alert(true, "公司标题不允许为空"));
                return;
            }
            if (this.TxtBankAccount.Value.Trim() == "")
            {
                Response.Write(JavaScript.Alert(true, "银行帐号不允许为空"));
                return;
            }

            RmsPM.BLL.SupplierTitle cSupplierTitle = new RmsPM.BLL.SupplierTitle();

            cSupplierTitle.SupplierTitleCode = SupplierTitleCode;
            cSupplierTitle.SupplierCode = this.SupplierCode;
            cSupplierTitle.Title = this.TxtCompanyTitle.Value;
            cSupplierTitle.BankAccount = this.TxtBankAccount.Value;

           
            cSupplierTitle.SupplierTitleAdd();


            Response.Write(Rms.Web.JavaScript.ScriptStart);
            Response.Write("window.opener.location = window.opener.location;");
            Response.Write(Rms.Web.JavaScript.WinClose(false));
      
            Response.Write(Rms.Web.JavaScript.ScriptEnd);
        }
        catch (Exception ex)
        {
            Response.Write(JavaScript.Alert(true, "保存失败：" + ex.Message));

            return;
        }
    }
    protected void btnDelete_ServerClick(object sender, EventArgs e)
    {
        try
        {
            RmsPM.BLL.SupplierTitle cSupplierTitle = new RmsPM.BLL.SupplierTitle();
            cSupplierTitle.SupplierTitleCode = SupplierTitleCode;
            cSupplierTitle.SupplierTitleDelete();


            Response.Write(Rms.Web.JavaScript.ScriptStart);
            Response.Write("window.opener.location = window.opener.location;");
            Response.Write(Rms.Web.JavaScript.WinClose(false));
  
            Response.Write(Rms.Web.JavaScript.ScriptEnd);
        }
        catch (Exception ex)
        {
            Response.Write(JavaScript.Alert(true, "删除失败：" + ex.Message));

            return;
        }
    }
}
