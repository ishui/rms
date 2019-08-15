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
using RmsPM.DAL.QueryStrategy;
using RmsPM.DAL.EntityDAO;
using RmsPM.Web;
using Rms.ORMap;


public partial class Material_MaterialInList : RmsPM.Web.PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ObjectDataSource1.SelectParameters["AccessRange"].DefaultValue = "150301" + "\n" + user.UserCode + "\n" + user.BuildStationCodes();
    }
    protected void ObjectDataSource1_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        /*
        if (this.GridView1.PageIndex == 0)
        {
            System.Collections.Generic.List<TiannuoPM.MODEL.MaterialInModel> lst = (System.Collections.Generic.List<TiannuoPM.MODEL.MaterialInModel>)e.ReturnValue;
            this.lblRecordCount.Text = lst.Count.ToString();
        }
        */
    }
    protected void btnSearch_ServerClick(object sender, EventArgs e)
    {
        GridView1.DataBind();
      //  this.divAdvSearch.display = this.txtAdvSearch;
    }
}
