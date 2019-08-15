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

public partial class RmsOA_Conference : System.Web.UI.Page
{
    private RmsPM.Web.UserControls.AttachMentAdd ucadd;
    private RmsPM.Web.UserControls.InputUnit ucDept;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(Request.QueryString["Type"]))
        {
            string type;
            type = Request.QueryString["Type"];
            if (type.Equals("Add"))
            {
                this.FormView1.ChangeMode(FormViewMode.Insert);
                DropDownList ddlType = (DropDownList)(this.FormView1.FindControl("DropDownListType"));
                ddlType.DataSource 
                    = RmsPM.DAL.EntityDAO.SystemManageDAO.GetDictionaryItemByName("会议管理");
                ddlType.DataBind();
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
                lblAttendPerson.Text = this.GetAttendPersonList(conferenceCode,"Attend");
                lblOtherAttendPerson.Text = this.GetAttendPersonList(conferenceCode,"OtherAttend");
            }
        }
        
        //EntityData ed = RmsPM.DAL.EntityDAO.GetDictionaryItemByName("会议管理");
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
                userListModel.State = "未读";
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
                userListModel.State = "未读";
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

    #endregion

    protected void InsertButton_Click(object sender, EventArgs e)
    {
        int conferenceCode;
        ConferenceManageBFL cmBFL = new ConferenceManageBFL();
        ConferenceManageModel cmModel = new ConferenceManageModel();
        AspWebControl.Calendar startDate = (AspWebControl.Calendar)(this.FormView1.FindControl("startDate"));
        AspWebControl.Calendar endDate = (AspWebControl.Calendar)(this.FormView1.FindControl("endDate"));
        DropDownList ddlType = (DropDownList)(this.FormView1.Row.FindControl("DropDownListType"));
        ucDept = (RmsPM.Web.UserControls.InputUnit)(this.FormView1.Row.FindControl("Inputunit1"));
        //DropDownList ddlDept = (DropDownList)(this.FormView1.Row.FindControl("DropDownListDept"));
        TextBox tbxTopic = (TextBox)(this.FormView1.FindControl("TextBoxTopic"));
        TextBox tbxCharter = (TextBox)(this.FormView1.FindControl("TextBoxChaterMember"));
        TextBox tbxRemark = (TextBox)(this.FormView1.FindControl("TextBoxRemark"));
        TextBox tbxPlace = (TextBox)(this.FormView1.FindControl("TextBoxPlace"));
        cmModel.ChaterMember = tbxCharter.Text;
        cmModel.Dept = ucDept.Text;
        cmModel.EndTime = DateTime.Parse(endDate.Value);
        cmModel.Place = tbxPlace.Text;
        cmModel.Remark = tbxRemark.Text;
        cmModel.StartTime = DateTime.Parse(startDate.Value);
        cmModel.State = "未通过";
        cmModel.Topic = tbxTopic.Text;
        cmModel.Type = ddlType.SelectedItem.Text;
        conferenceCode = cmBFL.Insert(cmModel);
        this.ucadd = (RmsPM.Web.UserControls.AttachMentAdd)(this.FormView1.Row.FindControl("Attachmentadd1"));
        ucadd.SaveAttachMent(conferenceCode.ToString());
        this.SaveDataIntoUserList("Attend", conferenceCode);
        this.SaveDataIntoUserList("OtherAttend", conferenceCode);
        Response.Redirect("ConferenceWeek.aspx");
    }
}
