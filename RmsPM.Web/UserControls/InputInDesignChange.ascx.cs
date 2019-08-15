using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System;

namespace RmsPM.Web.UserControls
{
    public partial class UserControls_InputInDesignChange : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        private void SetCode(string code)
        {
            RmsPM.BFL.DesignChangeBFL dc = new RmsPM.BFL.DesignChangeBFL();
            TiannuoPM.MODEL.DesignChangeModel dcmdl = dc.GetDesignChange(RmsPM.BLL.ConvertRule.ToInt(code));
            this.divName.InnerText = dcmdl.SolutionName;
            this.hideValue.Value = code;
        
        }




        public string Text
        {
            set { this.divName.InnerText = value; }
            //get { return this.divName.Value; }
        }
        
        public string Value
        {
            set { SetCode(value); }
            get { return this.hideValue.Value; }
        }
    }
}
