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

public partial class RmsOA_XZ_SelectMeetRoom : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string timeBegin = Request.QueryString["begin"];
            string timeEnd = Request.QueryString["end"];
            if (string.IsNullOrEmpty(timeBegin) || string.IsNullOrEmpty(timeEnd))
            {

            }
            else
            {
                this.startDate.Value = timeBegin;
                this.endDate.Value = timeEnd;
                this.ObjectDataSource1.SelectParameters["begin"].DefaultValue = timeBegin;
                this.ObjectDataSource1.SelectParameters["end"].DefaultValue = timeEnd;
                this.successGridView.DataBind();
            }
        }
        else
        {
 
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(this.startDate.Value))
        {
            ShowMessage("选择查询的开始时间");
        }
        else if (string.IsNullOrEmpty(this.endDate.Value))
        {
            ShowMessage("选择查询的结束时间");
        }
        else
        {
            this.ObjectDataSource1.SelectParameters["begin"].DefaultValue = this.startDate.Value;
            this.ObjectDataSource1.SelectParameters["end"].DefaultValue = this.endDate.Value;
            this.successGridView.DataBind();
        }
    }

    void ShowMessage(string message)
    {
        Response.Write(string.Format("<script>window.alert('{0}')</script>",message));
    }
    protected void successGridView_SelectedIndexChanged(object sender, EventArgs e)
    {
        string functionName = Request.QueryString["ReturnFunc"];
        Response.Write(functionName);
        string roomCode = this.successGridView.SelectedDataKey.Value.ToString();
        string roomName = RmsOA.BFL.ConferenceUserListBFLFacade.GetMeetRoomName(roomCode);
        Response.Write(string.Format("<script>window.opener.{0}('{1}','{2}');window.close();</script>", functionName.Trim(), roomCode,roomName));
    }
}
