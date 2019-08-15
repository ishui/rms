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
namespace RmsPM.Web.UserControls
{
    public partial class UserControls_InputDictItem : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }



        public string DictName
        {

            set { this.txtDictName.Value = value; }
        }

        public string Text
        {
            set { this.txtSearchPlace.Value = value; }
            get { return this.txtSearchPlace.Value; }
        }
    }
}
