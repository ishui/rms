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
using RmsPM.DAL;
using RmsPM.BLL;

public partial class ProjectCost_ProjectCostList : RmsPM.Web.PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ObjectDataSource1.SelectParameters["AccessRange"].DefaultValue = "152101" + "\n" + user.UserCode + "\n" + user.BuildStationCodes();
        
    }
    protected void ObjectDataSource1_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (this.GridView1.PageIndex == 0)
        {
            System.Collections.Generic.List<TiannuoPM.MODEL.ProjectCostModel> lst = (System.Collections.Generic.List<TiannuoPM.MODEL.ProjectCostModel>)e.ReturnValue;
            this.lblRecordCount.Text = lst.Count.ToString();
        }
        
    }

}
