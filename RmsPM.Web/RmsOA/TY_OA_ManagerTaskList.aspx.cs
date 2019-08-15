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
public partial class RmsOA_TY_OA_ManagerTaskList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            IniPage();
        }
    }

    private void IniPage()
    {
        string status = Request["Status"] + "";
        try
        {
            //this.StateID.SelectedValue.
            //Response.Write("<script>alert('this.StateID.SelectedValue');</script>");
            RmsPM.BLL.PageFacade.SetListGroupSelectedValues(this.StateID, status.Split(new char[] { ';' }));
            getstatuslist();
        }
        catch (Exception ex)
        {
            Response.Write(Rms.Web.JavaScript.Alert(true, "º”‘ÿ¡–±Ì¥ÌŒÛ£∫" + ex.Message));
        }
         
    }
    protected void ObjectDataSource1_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if ((this.GridView1.PageIndex == 0) && (e.ReturnValue != null))
        {
            System.Collections.Generic.List<RmsOA.MODEL.TY_OA_MgrTaskModel> lst = (System.Collections.Generic.List<RmsOA.MODEL.TY_OA_MgrTaskModel>)e.ReturnValue;
            this.lblRecordCount.Text = lst.Count.ToString();
        }
    }



    protected void btnSearch_ServerClick(object sender, EventArgs e)
    {

        getstatuslist();
        GridView1.DataBind();
        //  this.divAdvSearch.display = this.txtAdvSearch;
    }

    private void getstatuslist()
    {
        CheckBoxList cblist = this.StateID;
        lbltblist.Text = "";
        foreach (ListItem item in cblist.Items)
        {
            if (item.Selected)
            {
                lbltblist.Text += item.Value + ",";
            }
        }
        if (lbltblist.Text.Length >= 1)
        {
            lbltblist.Text = lbltblist.Text.Remove(lbltblist.Text.Length - 1, 1);
        }
    }

}
