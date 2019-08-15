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
using RmsPM.Web;
using RmsOA.BFL;
/// <summary>
/// Author Yiwl
/// CreateDate:2007-02-07
/// </summary>
public partial class RmsOA_XZ_WeekMeetShow : PageBase
{
    #region "Field"
    private const string Date = "Date";
    private DateTime meetDate;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            meetDate = DateTime.Now;
            ViewState[Date] = meetDate.Date;
        }
    }

    #region "Method"
    public void WirteYear()
    {
        Response.Write(((DateTime)(ViewState[Date])).Year);
    }
    public void WriteWeekIndex()
    {
        Response.Write(TimeExtend.GetWeekOFYear((DateTime)(ViewState[Date])));
    }
    public void WriteWeekTime()
    {
        Response.Write(ConferenceUserListBFLFacade.WeekAge((DateTime)(ViewState[Date])));
    }
    public void WriteMeetContent()
    {
        ConferenceUserListBFLFacade bfl = new ConferenceUserListBFLFacade();
        Response.Write(bfl.ShowAUserWeekMeetMessage(user.UserCode,(DateTime)(ViewState[Date])));
    }
    #endregion

    #region "Event"
    protected void WeekButton_Click(object sender, EventArgs e)
    {
        ViewState[Date] = DateTime.Now.Date;
    }
    protected void PreButton_Click(object sender, EventArgs e)
    {
        ViewState[Date] = ((DateTime)(ViewState[Date])).AddDays(-7);
    }
    protected void NextWeekButtom_Click(object sender, EventArgs e)
    {
        ViewState[Date] = ((DateTime)(ViewState[Date])).AddDays(7);
    }
    #endregion
}