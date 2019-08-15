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
using System.Collections.Generic;

using RmsOA.MODEL;
using RmsOA.BFL;
using Rms.ORMap;
using RmsPM.Web;

public partial class RmsOA_XZ_Conference : PageBase
{
    private RmsPM.Web.UserControls.AttachMentAdd ucadd;
    private static string Mess;
    private RmsPM.Web.UserControls.InputUnit ucDept;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(Request.QueryString["Type"]))
        {
            string type;
            type = Request.QueryString["Type"];
            if (!IsPostBack)
            {
                if (type.Equals("Add"))
                {
                    this.FormView1.ChangeMode(FormViewMode.Insert);
                    //DropDownList ddlType = (DropDownList)(this.FormView1.FindControl("DropDownListType"));
                    //ddlType.DataSource
                    //    = RmsPM.DAL.EntityDAO.SystemManageDAO.GetDictionaryItemByName("会议类型");
                    //ddlType.DataBind();
                    EntityData ed = RmsPM.DAL.EntityDAO.SystemManageDAO.GetDictionaryItemByName("会议类型");
                    //ed.Tables[0].WriteXml("D:\\Temp\\1.xml");
                    Label lblname = (Label)(this.FormView1.FindControl("LabelChaterMember"));
                    lblname.Text = user.UserName;
                    AspWebControl.Calendar startDate = (AspWebControl.Calendar)(this.FormView1.FindControl("startDate"));
                    AspWebControl.Calendar endDate = (AspWebControl.Calendar)(this.FormView1.FindControl("endDate"));
                    startDate.Value = System.DateTime.Now.ToString();
                    endDate.Value = System.DateTime.Now.ToString();
                }
            }
            if (type.Equals("Read"))
            {
                if (!String.IsNullOrEmpty(Request.QueryString["Code"]))
                {
                    int conferenceCode = Int32.Parse(Request.QueryString["Code"]);
                    Label lblAttendPerson = (Label)(this.FormView1.Row.FindControl("LabelAttendPerson"));
                    Label lblOtherAttendPerson = (Label)(this.FormView1.Row.FindControl("LabelOtherAttendPerson"));
                    lblAttendPerson.Text = this.GetAttendPersonList(conferenceCode, "Attend");
                    lblOtherAttendPerson.Text = this.GetAttendPersonList(conferenceCode, "OtherAttend");
                    Label lblDept = (Label)(this.FormView1.FindControl("LabelDept"));
                    HiddenField hidDept = (HiddenField)(this.FormView1.FindControl("hidDept"));
                    lblDept.Text = RmsPM.BLL.SystemRule.GetUnitName(hidDept.Value);
                }
            }
        }
        else
        {
            if (!String.IsNullOrEmpty(Request.QueryString["Code"]))
            {
                int conferenceCode = Int32.Parse(Request.QueryString["Code"]);
                Label lblAttendPerson = (Label)(this.FormView1.Row.FindControl("LabelAttendPerson"));
                Label lblOtherAttendPerson = (Label)(this.FormView1.Row.FindControl("LabelOtherAttendPerson"));
                lblAttendPerson.Text = this.GetAttendPersonList(conferenceCode, "Attend");
                lblOtherAttendPerson.Text = this.GetAttendPersonList(conferenceCode, "OtherAttend");
            }
        }
    }

    #region "自定义函数"

    public string[] GetUserList(string controlName)
    {
        string[] nameList;
        TextBox userList = (TextBox)(this.FormView1.Row.FindControl(controlName));
        string text = userList.Text;
        nameList = this.SplitString(text);
        return nameList;
    }

    public string[] SplitString(string text)
    {
        string[] userList;
        userList = text.Split(',');
        return userList;
    }

    public void SaveDataIntoUserList(string type, int code)
    {
        string[] userName;
        string[] userCode;
        if (type.Equals("Attend"))
        {
            userName = this.GetUserList("AttendPersonnelTextBox");
            userCode = this.GetUserList("AttendPersonnelCodeTextBox");
        }
        else
        {
            userName = this.GetUserList("OtherAttendPersonnelTextBox");
            userCode = this.GetUserList("OtherAttendPersonnelCodeTextBox");
        }
        ConferenceUserListModel userListModel = new ConferenceUserListModel();
        ConferenceUserListBFL userListBFL = new ConferenceUserListBFL();
        if (userName.Length == userCode.Length)
        {
            for (int i = 0; i < userName.Length; i++)
            {
                userListModel = new ConferenceUserListModel();
                userListModel.UserCode = userCode[i];
                userListModel.UserName = userName[i];
                userListModel.Type = type;
                userListModel.ConferenceCode = code;
                userListModel.State = MeetStateType.UnAuthored.ToString();
                userListBFL.Insert(userListModel);
            }
        }
        else
        {
            for (int i = 0; i < userName.Length; i++)
            {
                userListModel = new ConferenceUserListModel();
                userListModel.UserName = userName[i];
                userListModel.Type = type;
                userListModel.ConferenceCode = code;
                userListModel.State = MeetStateType.UnRead.ToString();
                userListBFL.Insert(userListModel);
            }
        }
    }

    public string GetAttendPersonList(int conferenceCode, string type)
    {
        string text = "";
        ConferenceUserListQueryModel cmQueryModel = new ConferenceUserListQueryModel();
        ConferenceUserListBFL culBFL = new ConferenceUserListBFL();
        List<ConferenceUserListModel> culModel = new List<ConferenceUserListModel>();
        cmQueryModel.ConferenceCodeEqual = conferenceCode;
        cmQueryModel.TypeEqual = type;
        culModel = culBFL.GetConferenceUserListList(cmQueryModel);
        for (int i = 0; i < culModel.Count; i++)
        {
            text += culModel[i].UserName + "  ";
        }
        return text;
    }

    string ClushMessage()
    {
        string roomCode, begin, end, message;
        AspWebControl.Calendar startDate = (AspWebControl.Calendar)(this.FormView1.FindControl("startDate"));
        AspWebControl.Calendar endDate = (AspWebControl.Calendar)(this.FormView1.FindControl("endDate"));
        HiddenField hidPlace = (HiddenField)(this.FormView1.FindControl("HidRoomCode"));
        roomCode = hidPlace.Value;
        begin = startDate.Value;
        end = endDate.Value;
        ConferenceUserListBFLFacade bfl = new ConferenceUserListBFLFacade();
        message =  bfl.GetMeetClashMessage(null,roomCode,begin,end);
        return message;
    }

    bool IsClush()
    {
        Mess = this.ClushMessage();
        if (string.IsNullOrEmpty(Mess))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    void ShowMessage(string message)
    {
        Response.Write(string.Format("<script>window.alert('{0}')</script>",message));
    }

    void SetInsertButtonAttribute()
    {
        Button btnAdd = (Button)(this.FormView1.FindControl("InsertButton"));
        btnAdd.Attributes.Add("onclick","return window.confirm('确认添加？');");
    }

    void Insert()
    {
        int conferenceCode;
        ConferenceManageBFL cmBFL = new ConferenceManageBFL();
        ConferenceManageModel cmModel = new ConferenceManageModel();
        AspWebControl.Calendar startDate = (AspWebControl.Calendar)(this.FormView1.FindControl("startDate"));
        AspWebControl.Calendar endDate = (AspWebControl.Calendar)(this.FormView1.FindControl("endDate"));

        DropDownList ddlType = (DropDownList)(this.FormView1.Row.FindControl("DropDownListType"));
        ucDept = (RmsPM.Web.UserControls.InputUnit)(this.FormView1.Row.FindControl("Inputunit1"));
        TextBox tbxTopic = (TextBox)(this.FormView1.FindControl("TextBoxTopic"));
        TextBox tbxRemark = (TextBox)(this.FormView1.FindControl("TextBoxRemark"));
        HiddenField hidPlace = (HiddenField)(this.FormView1.FindControl("HidRoomCode"));
        cmModel.ChaterMember = user.UserCode;
        cmModel.Dept = ucDept.Value;
        cmModel.Place = hidPlace.Value;
        cmModel.Remark = tbxRemark.Text;
        cmModel.State = MeetStateType.UnAuthored.ToString();
        cmModel.Topic = tbxTopic.Text;
        cmModel.Type = ddlType.SelectedItem.Text;
        cmModel.StartTime = cmBFL.GetUsefulDate(startDate.Value);
        cmModel.EndTime = cmBFL.GetUsefulDate(endDate.Value);
        conferenceCode = cmBFL.Insert(cmModel);
        this.ucadd = (RmsPM.Web.UserControls.AttachMentAdd)(this.FormView1.Row.FindControl("Attachmentadd1"));
        ucadd.SaveAttachMent(conferenceCode.ToString());
        this.SaveDataIntoUserList("Attend", conferenceCode);
        this.SaveDataIntoUserList("OtherAttend", conferenceCode);
        Response.Redirect("XZ_ConferenceWeek.aspx");
    }

    #endregion

    protected void InsertButton_Click(object sender, EventArgs e)
    {
        if (ViewState["Count"] == null)
        {
            if (!IsClush())
            {
                Insert();
            }
            else
            {
                this.ShowMessage(Mess);
                this.SetInsertButtonAttribute();
                ViewState["Count"] = "Sure";
            }
        }
        else
        {
            Insert();
        }
    }

    protected void Page_PreLoad(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(Request.QueryString["Type"]))
        {
            if (Request.QueryString["Type"].Equals("Add"))
            {
                if (!user.HasRight("320101"))
                {
                    Response.Write("<script> window.alert('你没有权限访问该页面，如果有疑问请与管理员联系！');history.back(-1);</script>");
                }
            }
        }
    }
    protected void FormView1_ItemDeleted(object sender, FormViewDeletedEventArgs e)
    {
        Response.Write("<script>window.opener.location.reload();window.close();</script>");

    }
    protected void FormView1_DataBound(object sender, EventArgs e)
    {
        if (this.FormView1.CurrentMode == FormViewMode.ReadOnly)
        {
            if (!user.HasRight("320103"))
            {
                Button btnDelete = (Button)(this.FormView1.Row.FindControl("DeleteButton"));
                btnDelete.Visible = false;
            }
        }
    }
    protected void ObjectDataSource1_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        this.ObjectDataSource1.SelectParameters.Clear();
        this.ObjectDataSource1.SelectParameters.Add("Code", e.ReturnValue.ToString());
    }
}