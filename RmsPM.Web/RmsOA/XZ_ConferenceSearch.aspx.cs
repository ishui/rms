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
using System.Text;

using RmsPM.Web;
using RmsOA.MODEL;
using RmsOA.BFL;

public partial class RmsOA_XZ_ConferenceSearch : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ConferenceManageBFL cmBFL = new ConferenceManageBFL();
        this.GridView1.DataSource = cmBFL.GetSearchResultConferenceList(this.BuildQueryString());
        this.GridView1.DataBind();
        this.MeetPlace.DataTextField = "RoomName";
        this.MeetPlace.DataValueField = "RoomCode";
    }

    public void searchButton_Click(object sender, EventArgs e)
    {
        ConferenceManageBFL cmBFL = new ConferenceManageBFL();
        this.GridView1.DataSource = cmBFL.GetSearchResultConferenceList(this.BuildQueryString());
        this.GridView1.DataBind();
    }

    protected string BuildQueryString()
    {
        string queryString;
        string topic = this.TextBoxTopic.Text.Trim();
        string startTime = this.dtStartDate.Value.ToString();
        string endTime = this.dtEndDate.Value.ToString();
        string dept = this.InputDept.Text;
        string place = this.MeetPlace.SelectedValue.Trim();
        string chaterMember = this.InputuserChaterMember.Value;
        ConferenceManageBFL cmBFL = new ConferenceManageBFL();
        queryString = cmBFL.BuildQueryString(topic,startTime,endTime,dept,place,chaterMember);
        return queryString;
    }
}
