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

using Rms.ORMap;
using Rms.Web;
using RmsPM.DAL.EntityDAO;
using RmsPM.BLL;

public partial class Supplier_SupplierLinkman :RmsPM.Web.PageBase
{


    private string _SupplierLinkmanCode;

    public string SupplierLinkmanCode
    {
        get
        {
            if (_SupplierLinkmanCode == null)
            {
                if (this.ViewState["_SupplierLinkmanCode"] != null)
                    return this.ViewState["_SupplierLinkmanCode"].ToString();
                return "";
            }
            return _SupplierLinkmanCode;
        }
        set
        {

            this._SupplierLinkmanCode = value;
            this.ViewState["_SupplierLinkmanCode"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!this.IsPostBack)
        {
            this.LoadData();
        }

    }


    private void LoadData()
    {
        SupplierLinkmanCode = Request["SupplierLinkmanCode"] + "";
        if (!this.user.HasRight("141501") && !this.user.HasRight("141502") && !this.user.HasRight("14103") && !this.user.HasRight("14104"))
        {
            Response.Redirect("../RejectAccess.aspx");
            Response.End();
        }

        if (!this.user.HasRight("141503") && !this.user.HasRight("141504"))
        {
            this.btnSave.Visible = false;
        }
        if (!this.user.HasRight("141502"))
        {
            this.btnDelete.Visible = false;
        }




        if (SupplierLinkmanCode != "")
        {
            RmsPM.BLL.SupplierLinkman csupplierLinkman = new RmsPM.BLL.SupplierLinkman();
            csupplierLinkman.SupplierLinkmanCode = SupplierLinkmanCode;
            System.Data.DataTable dtsupplierLinkman = csupplierLinkman.GetSupplierLinkmans();
            for (int i = 0; i < dtsupplierLinkman.Rows.Count; i++)
            {
                this.txtContractPerson.Text = dtsupplierLinkman.Rows[i]["ContractPerson"].ToString();


                this.txtOfficePhone.Text = dtsupplierLinkman.Rows[i]["OfficePhone"].ToString();
                this.txtPostCode.Text = dtsupplierLinkman.Rows[i]["PostCode"].ToString();
                this.TxtProjectName.Text = dtsupplierLinkman.Rows[i]["ProjectName"].ToString();
                this.TxtAreaName.Text = dtsupplierLinkman.Rows[i]["AreaName"].ToString();
                this.txtMobile.Text = dtsupplierLinkman.Rows[i]["Mobile"].ToString();
                this.txtFax.Text = dtsupplierLinkman.Rows[i]["Fax"].ToString();
                this.txtEmail.Text = dtsupplierLinkman.Rows[i]["EMail"].ToString();
                this.TxtRemark.Text = dtsupplierLinkman.Rows[i]["Remark"].ToString();

            }
        }
        else
        {
            this.btnDelete.Visible = false;
        }
    }


    protected void btnSave_ServerClick(object sender, EventArgs e)
    {
        try
        {
            if (this.txtContractPerson.Text.Trim() == "")
            {
                Response.Write(JavaScript.Alert(true, "联系人名称不允许为空"));
                return;
            }
            if (this.txtOfficePhone.Text.Trim() == "")
            {
                Response.Write(JavaScript.Alert(true, "联系电话不允许为空"));
                return;
            }
            string SupplierCode = Request["SupplierCode"] + "";
            RmsPM.BLL.SupplierLinkman csupplierLinkman = new RmsPM.BLL.SupplierLinkman();
            csupplierLinkman.SupplierLinkmanCode = SupplierLinkmanCode;
            csupplierLinkman.SupplierCode = SupplierCode;
            csupplierLinkman.ContractPerson = this.txtContractPerson.Text.Trim();
            csupplierLinkman.OfficePhone = this.txtOfficePhone.Text.Trim();
            csupplierLinkman.PostCode = this.txtPostCode.Text.Trim();
            csupplierLinkman.ProjectName = this.TxtProjectName.Text.Trim();
            csupplierLinkman.AreaName = this.TxtAreaName.Text.Trim();
            csupplierLinkman.Mobile = this.txtMobile.Text.Trim();
            csupplierLinkman.Fax = this.txtFax.Text.Trim();
            csupplierLinkman.EMail = this.txtEmail.Text.Trim();
            csupplierLinkman.Remark = this.TxtRemark.Text.Trim();
            csupplierLinkman.SupplierLinkmanAdd();

            Response.Write(Rms.Web.JavaScript.ScriptStart);
            Response.Write(Rms.Web.JavaScript.OpenerReload(false));
            Response.Write(Rms.Web.JavaScript.WinClose(false));
            //			string FromUrl = this.txtFromUrl.Text.Trim();
            //			if (FromUrl != "") 
            //			{
            //				Response.Write(string.Format("window.location = '{0}';", FromUrl));
            //			}
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
            RmsPM.BLL.SupplierLinkman csupplierLinkman = new RmsPM.BLL.SupplierLinkman();
            csupplierLinkman.SupplierLinkmanCode = SupplierLinkmanCode;
            csupplierLinkman.SupplierLinkmanDelete();

            Response.Write(Rms.Web.JavaScript.ScriptStart);
            Response.Write(Rms.Web.JavaScript.OpenerReload(false));
            Response.Write(Rms.Web.JavaScript.WinClose(false));
            //			string FromUrl = this.txtFromUrl.Text.Trim();
            //			if (FromUrl != "") 
            //			{
            //				Response.Write(string.Format("window.location = '{0}';", FromUrl));
            //			}
            Response.Write(Rms.Web.JavaScript.ScriptEnd);
        }
        catch (Exception ex)
        {
            Response.Write(JavaScript.Alert(true, "删除失败：" + ex.Message));

            return;
        }
    }
}
