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
using Infragistics.WebUI.WebDataInputT1;

public partial class UserControls_inputExchangeRate : System.Web.UI.UserControl
{
    private DataTable up_dtMoneyTypeDataSource;

    protected bool IsBind 
    {
        get
        {
            if (hidIsBind.Value == "1")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        set
        {
            if (value)
            {
                hidIsBind.Value = "1";
            }
            else 
            {
                hidIsBind.Value = "0";
            }
        }
    }


    public FormViewMode Mode
    {
        get
        {
            return this.fvExchangeRate.CurrentMode;
        }
        set
        {
            this.fvExchangeRate.ChangeMode(value);
        }
    }

    public decimal Cash
    {
        get 
        {
            decimal ud_deCash = decimal.Zero;

            if (this.IsBind)
            {
                switch (this.fvExchangeRate.CurrentMode)
                {
                    case FormViewMode.Edit:
                        WebNumericEdit txtCash = (WebNumericEdit)this.fvExchangeRate.FindControl("txtCash");
                        ud_deCash = txtCash.ValueDecimal;
                        break;
                    case FormViewMode.ReadOnly:
                        ud_deCash = hidCash.Value == "" ? decimal.Zero : decimal.Parse(hidCash.Value);
                        break;
                    default:
                        ud_deCash = hidCash.Value == "" ? decimal.Zero : decimal.Parse(hidCash.Value);
                        break;
                }
            }
            else
            {
                ud_deCash = hidCash.Value == "" ? decimal.Zero : decimal.Parse(hidCash.Value);
            }

            return ud_deCash;

        }
        set
        {
            hidCash.Value = value.ToString();
        }
    }

    public string MoneyType
    {
        get
        {
            string ud_sMoneyType = string.Empty;

            if (this.IsBind)
            {
                switch (this.fvExchangeRate.CurrentMode)
                {
                    case FormViewMode.Edit:
                        DropDownList ddlMoneyType = (DropDownList)this.fvExchangeRate.FindControl("ddlMoneyType");
                        ud_sMoneyType = ddlMoneyType.SelectedItem.Text;
                        break;
                    case FormViewMode.ReadOnly:
                        ud_sMoneyType = hidMoneyType.Value.Trim();
                        break;
                    default:
                        ud_sMoneyType = hidMoneyType.Value.Trim();
                        break;
                }
            }
            else
            {
                ud_sMoneyType = hidMoneyType.Value.Trim();
            }

            return ud_sMoneyType;

        }
        set
        {
            hidMoneyType.Value = value;
            DropDownList ddlMoneyType = (DropDownList)this.fvExchangeRate.FindControl("ddlMoneyType");
        }
    }

    public decimal ExchangeRate
    {
        get
        {
            decimal ud_deExchangeRate = decimal.Zero;

            if (this.IsBind)
            {
                switch (this.fvExchangeRate.CurrentMode)
                {
                    case FormViewMode.Edit:
                        WebNumericEdit txtExchangeRate = (WebNumericEdit)this.fvExchangeRate.FindControl("txtExchangeRate");
                        ud_deExchangeRate = txtExchangeRate.ValueDecimal;
                        break;
                    case FormViewMode.ReadOnly:
                        ud_deExchangeRate = hidExchangeRate.Value == "" ? decimal.Zero : decimal.Parse(hidExchangeRate.Value);
                        break;
                    default:
                        ud_deExchangeRate = hidExchangeRate.Value == "" ? decimal.Zero : decimal.Parse(hidExchangeRate.Value);
                        break;
                }
            }
            else
            {
                ud_deExchangeRate = hidExchangeRate.Value == "" ? decimal.Zero : decimal.Parse(hidExchangeRate.Value);
            }

            return ud_deExchangeRate;

        }
        set
        {
            hidExchangeRate.Value = value.ToString();
        }
    }

    public decimal Money
    {
        get
        {
            return this.Cash * this.ExchangeRate;
        }
    }

    public string ValueChange
    {
        get
        {
            return this.hidValueChange.Value.Trim();
        }
        set
        {
            this.hidValueChange.Value = value;
        }
    }

    public DataTable MoneyTypeDataSource
    {
        get
        {
            return GetMoneyTypeDataSource();
        }
        set
        {
            up_dtMoneyTypeDataSource = value;
        }
    }

    protected DataTable GetMoneyTypeDataSource()
    {
        if (up_dtMoneyTypeDataSource == null)
        {
            up_dtMoneyTypeDataSource = RmsPM.BLL.ExchangeRateRule.GetMoneyTypeDataSource();
        }

        return up_dtMoneyTypeDataSource;

    }

    protected DataTable GetDataSource()
    {

        DataTable ud_dtDataSource = new DataTable();

        ud_dtDataSource.Columns.Add("Cash", typeof(decimal));
        ud_dtDataSource.Columns.Add("MoneyType", typeof(string));
        ud_dtDataSource.Columns.Add("ExchangeRate", typeof(decimal));
        ud_dtDataSource.Columns.Add("Money", typeof(decimal));

        DataRow ud_drNew = ud_dtDataSource.NewRow();

        ud_drNew["Cash"] = this.Cash;
        ud_drNew["MoneyType"] = this.MoneyType;
        ud_drNew["ExchangeRate"] = this.ExchangeRate;
        ud_drNew["Money"] = this.Money;

        ud_dtDataSource.Rows.Add(ud_drNew);


        return ud_dtDataSource;
    }

    public void DataBind()
    {
        this.fvExchangeRate.DataSource = GetDataSource();
        this.fvExchangeRate.DataBind();

        this.IsBind = true;
        
    }

    protected void fvExchangeRate_DataBound(object sender, EventArgs e)
    {
        DataRowView ud_drvItem = (DataRowView)this.fvExchangeRate.DataItem;

        switch (this.fvExchangeRate.CurrentMode )
        {
            case FormViewMode.Edit:
                DropDownList ddlMoneyType = (DropDownList)this.fvExchangeRate.FindControl("ddlMoneyType");
                WebNumericEdit txtCash = (WebNumericEdit)this.fvExchangeRate.FindControl("txtCash");
                WebNumericEdit txtExchangeRate = (WebNumericEdit)this.fvExchangeRate.FindControl("txtExchangeRate");

                txtCash.ClientSideEvents.ValueChange = this.UniqueID + "InfraCashMoneyChange";
                txtExchangeRate.ClientSideEvents.ValueChange = this.UniqueID + "InfraCashMoneyChange";

                ddlMoneyType.DataSource = this.MoneyTypeDataSource;
                ddlMoneyType.DataBind();
                ddlMoneyType.SelectedIndex = ddlMoneyType.Items.IndexOf(ddlMoneyType.Items.FindByText(ud_drvItem["MoneyType"].ToString()));

                if (ddlMoneyType.SelectedItem.Text == "人民币 (RMB)")
                {
                    txtExchangeRate.Enabled = false;
                    txtExchangeRate.ValueDecimal = 1m;
                }
                else
                {
                    txtExchangeRate.Enabled = true;
                }



                if ( this.hidExchangeRate.Value.Trim() == string.Empty ) 
                {
                    txtExchangeRate.ValueDecimal = RmsPM.BLL.ConvertRule.ToDecimal(ddlMoneyType.SelectedValue);
                }
                break;
            case FormViewMode.ReadOnly:
                HtmlTableCell tdForeignCurrency = (HtmlTableCell)this.fvExchangeRate.FindControl("tdForeignCurrency");

                if (ud_drvItem["MoneyType"].ToString() == "人民币 (RMB)")
                {
                    tdForeignCurrency.Visible = false;
                }
                else
                {
                    tdForeignCurrency.Visible = true;
                }
                break;

        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsBind)
        {
            this.DataBind();
        }

    }
}
