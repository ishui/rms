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
using RmsPM.BLL;
namespace Web
{
    public partial class SelectBox_SelectSessionUserUnit : System.Web.UI.UserControl
    {

        public string SelectedValue
        {
            set
            {
                this.UnitDropDownList.SelectedIndex = this.UnitDropDownList.Items.IndexOf(this.UnitDropDownList.Items.FindByValue(value));
            }
            get
            {
                return this.UnitDropDownList.SelectedValue;
            }
        }
        public string UserCode
        {
            set
            {                
                if (UserCode!=value.ToString())
                {
                ViewState["UserCode"] = value;
                System.Data.DataTable dt = SystemRule.GetUnitListByUserCode(ViewState["UserCode"].ToString());
                //DataRow unitdr = dt.NewRow();
                //unitdr["UnitCode"] = "";
                //unitdr["UnitName"] = "请选择";
                //dt.Rows.InsertAt(unitdr,0);
                this.UnitDropDownList.DataSource = dt;
                this.UnitDropDownList.DataTextField = "UnitName";
                this.UnitDropDownList.DataValueField = "UnitCode"; 
                this.UnitDropDownList.DataBind();
                }
            }
            get
            {
                if (ViewState["UserCode"] == null)
                {
                    return "";
                }
                return ViewState["UserCode"].ToString();
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

    }
}