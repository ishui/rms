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
using RmsDM.BFL;
using RmsDM.MODEL;

using RmsPM.Web;

public partial class RmsDM_DocumentFileList : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void ButtonSearch_ServerClick(object sender, EventArgs e)
    {
        this.ObjectDataSource2.SelectParameters.Clear();
        this.ObjectDataSource2.SelectParameters.Add("SortColumns", "");
        this.ObjectDataSource2.SelectParameters.Add("StartRecord", "0");
        this.ObjectDataSource2.SelectParameters.Add("MaxRecords", "-1");

        this.ObjectDataSource2.SelectParameters.Add("NodeCode", Request["NodeValue"] + "");
        this.ObjectDataSource2.SelectParameters.Add("FileCodeEqual",this.FileCodeTextBox.Text);
        this.ObjectDataSource2.SelectParameters.Add("DoucmentMarkingSNEqual", this.DoucmentMarkingSNTextBox.Text);
        this.ObjectDataSource2.SelectParameters.Add("SubjectEqual", this.SubjectTextBox.Text);
        this.ObjectDataSource2.SelectParameters.Add("ArchiveDateTimeEqualStart", this.AarchiveDateStart.Value);
        this.ObjectDataSource2.SelectParameters.Add("ArchiveDateTimeEqualEnd", this.ArchiveDateEnd.Value);
        this.GridView1.DataSourceID = "ObjectDataSource2";
        this.GridView1.DataBind();
    }
}
