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

public partial class SelectBox_selectmaterialinlist : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            //���غ�����
            string ReturnFunc = Request.QueryString["ReturnFunc"] + "";
            if (ReturnFunc == "")
            {
                ReturnFunc = "DoSelectMaterial";
            }
            ViewState["ReturnFunc"] = ReturnFunc;

        }

    }
    protected void ObjectDataSource1_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (this.GridView1.PageIndex == 0)
        {
            System.Collections.Generic.List<TiannuoPM.MODEL.MaterialInDtlModel> lst = (System.Collections.Generic.List<TiannuoPM.MODEL.MaterialInDtlModel>)e.ReturnValue;
            this.lblRecordCount.Text = lst.Count.ToString();
        }

    }
}
