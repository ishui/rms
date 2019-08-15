<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Conference.aspx.cs" Inherits="RmsOA_Conference" %>

<%@ Register Src="../UserControls/inputunit.ascx" TagName="inputunit" TagPrefix="uc5" %>

<%@ Register Src="../UserControls/attachmentlist.ascx" TagName="attachmentlist" TagPrefix="uc1" %>
<%@ Register Src="../UserControls/inputusers.ascx" TagName="inputusers" TagPrefix="uc3" %>
<%@ Register Src="../UserControls/inputusers.ascx" TagName="inputusers" TagPrefix="uc4" %>
<%@ Register Assembly="AspWebControl" Namespace="AspWebControl" TagPrefix="cc1" %>
<%@ Register Src="../UserControls/attachmentadd.ascx" TagName="attachmentadd" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>详细会议</title>
    <link href="../Images/index.css" rel="stylesheet" type="text/css" />
    <link href="head.css" rel="stylesheet" type="text/css" />

    <script language="javascript" src="../Rms.js"></script>

    <script language="javascript" type="text/javascript">
    var str;
    var selectId;
    function OnPageLoad()
    {
        var type = '<%= Request["Type"] %>';
        if (type == "Add")
        {
            document.getElementById("Navigator").style.display = "block";
        }
        var string;
        string = location.href.split("/");
        str = string[string.length - 1];
        if( str.search(/Audit/) != -1)
        {
            selectId = "audit";
        }
        if(str.search(/Search/) != -1)
        {
            selectId = "search";
        }
        if(str.search(/Add/) != -1)
        {
            selectId = "add";
        }
        if(str.search(/Week/) != -1)
        {
           selectId = "week";
        }
        document.getElementById(selectId).className = "td-over";
    }
      
    function onmouseovers(id)
    {
        if( id != selectId)
        {   
            document.getElementById(selectId).className = "";
            return document.getElementById(id).className="td-over";
        }
    }
    
    function onmouseouts(id)
    {
        return document.getElementById(id).className="";
    }
    
    function wholeout()
    {
        return document.getElementById(selectId).className = "td-over";
    }
    
    function onmouseclick(id)
    {
        if( id == selectId)
        {
            return;
        }
        switch(id)
        {
            case "week":
                location.href("ConferenceWeek.aspx");break;
            case "search":
                location.href("ConferenceSearch.aspx");break;
            case "add":
                location.href("Conference.aspx?Type=Add");break;
            case "audit":
                location.href("ConferenceAudit.aspx");break;
            default: break; 
        }   
    }
    
    
      
    </script>

    <link href="/../Images/index.css" rel="stylesheet" type="text/css" />
</head>
<body onload="OnPageLoad()">
    <form id="form1" runat="server">
        <div>
            <table id="Navigator" border="0" cellpadding="0" cellspacing="0" class="head-eve-table"
                onmouseout="wholeout()" width="50%" style="display: none;">
                <tr>
                    <td id="week" onclick="onmouseclick('week')" onmouseout="onmouseouts('week')" onmouseover="onmouseovers('week')">
                        本周会议</td>
                    <td id="search" onclick="onmouseclick('search')" onmouseout="onmouseouts('search')"
                        onmouseover="onmouseovers('search')">
                        会议查询</td>
                    <td id="add" onclick="onmouseclick('add')" onmouseout="onmouseouts('add')" onmouseover="onmouseovers('add')">
                        新增会议</td>
                    <td id="audit" onclick="onmouseclick('audit')" onmouseout="onmouseouts('audit')"
                        onmouseover="onmouseovers('audit')">
                        会议审核</td>
                </tr>
            </table>
        </div>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetConferenceManageListOne"
            TypeName="RmsOA.BFL.ConferenceManageBFL" >
            <SelectParameters>
                <asp:QueryStringParameter Name="Code" QueryStringField="Code" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>

        <script language="javascript">
        function SelectUsers(ReturnFunc)
        {
            if (ReturnFunc=="SelectUserReturn")
            {
                var myUserid = window.document.all("FormView1_AttendPersonnelCodeTextBox");
               
            }else
            {
                var myUserid = window.document.all("FormView1_OtherAttendPersonnelCodeTextBox");               
            }          
            OpenMiddleWindow("../SelectBox/SelectSUMain.aspx?Type=U&ReturnFunc=" + ReturnFunc +"&UserCodes=" + myUserid.value , "选择用户");
            
        }    
　　	
	    function SelectUserReturn(userCodes,userNames,stationCodes,stationNames,flag)
	    {
	        var AttendPersonnel=window.document.all("FormView1_AttendPersonnelTextBox");
	        var AttendPersonnelCode=window.document.all("FormView1_AttendPersonnelCodeTextBox");
	        
	        AttendPersonnel.value=userNames;
	        AttendPersonnelCode.value=userCodes;
	    }
	    function SelectOtherUserReturn(userCodes,userNames,stationCodes,stationNames,flag)
	    {
	       var OtherAttendPersonnel=window.document.all("FormView1_OtherAttendPersonnelTextBox");
	       var OtherAttendPersonnelCode=window.document.all("FormView1_OtherAttendPersonnelCodeTextBox");
	      
	         OtherAttendPersonnel.value=userNames;
	        OtherAttendPersonnelCode.value=userCodes;
	    }
        </script>

        <asp:FormView ID="FormView1" runat="server" DataSourceID="ObjectDataSource1" Width="100%"
           >
            <EditItemTemplate>
                <table class="form" width="100%">
                    <tr>
                        <td class="form-item" style="width: 120px;">
                            会议主题：</td>
                        <td colspan="3">
                            <asp:TextBox ID="TextBoxTopic" runat="server" Width="60%" Text='<%# Bind("Topic") %>'
                                CssClass="input"></asp:TextBox><font color="red">*</font>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBoxTopic"
                                ErrorMessage="不能为空"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 120px;">
                            会议发起人：</td>
                        <span style="color: Red;"></span>
                        <td>
                            <asp:TextBox ID="TextBoxChaterMember" runat="server" Text='<%# Bind("ChaterMember") %>'
                                CssClass="input"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TextBoxChaterMember"
                                ErrorMessage="不能为空"></asp:RequiredFieldValidator></td>
                        <td class="form-item" style="width: 120px;">
                            会议类型：</td>
                        <td>
                            <asp:TextBox ID="TextBoxType" runat="server" Text='<%# Bind("Type") %>' CssClass="input"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 120px;">
                            会议地点：</td>
                        <td>
                            <asp:TextBox ID="TextBoxPlace" runat="server" Text='<%# Bind("Place") %>' CssClass="input"></asp:TextBox><font
                                color="red">*<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                    ControlToValidate="TextBoxPlace" ErrorMessage="不能为空"></asp:RequiredFieldValidator></font>
                        </td>
                        <td class="form-item" style="width: 120px;">
                            主办单位：</td>
                        <td>
                            <asp:TextBox ID="TextBoxDept" runat="server" Text='<%# Bind("Dept") %>' CssClass="input"></asp:TextBox><font
                                color="red">*<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                                    ControlToValidate="TextBoxDept" ErrorMessage="不能为空"></asp:RequiredFieldValidator></font>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 120px;">
                            会议开始时间：</td>
                        <td>
                            <cc1:Calendar ID="startDate" runat="server" CalendarMode="All" CalendarResource="../Images/CalendarResource/"
                                Value="">
                            </cc1:Calendar>
                            <font color="red">*</font>
                        </td>
                        <td class="form-item" style="width: 120px;">
                            会议结束时间：</td>
                        <td>
                            <cc1:Calendar ID="enddate" runat="server" CalendarMode="All" CalendarResource="../Images/CalendarResource/"
                                Value="">
                            </cc1:Calendar>
                            <font color="red">*</font>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" valign="top">
                            会议应到人员：</td>
                        <td colspan="3">
                            <asp:TextBox runat="server" ID="TextBoxAttend" TextMode="MultiLine"></asp:TextBox>
                            <uc3:inputusers ID="AttendUser" runat="server"></uc3:inputusers>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" valign="top">
                            其他与会人员：</td>
                        <td colspan="3">
                            <asp:TextBox ID="OtherAttendPersonnelTextBox" runat="server" TextMode="MultiLine"
                                Width="400px">
                            </asp:TextBox>
                            <asp:TextBox ID="OtherAttendPersonnelCodeTextBox" runat="server" Height="0px" Text='<%# Bind("OtherAttendPersonnel") %>'
                                Width="0px"></asp:TextBox>
                            <img onclick="SelectUsers('SelectOtherUserReturn');return false;" src="../images/ToolsItemSearch.gif"
                                style="cursor: hand" alt="点击选择人员" />
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 120px;" valign="top">
                            附件：</td>
                        <td colspan="3">
                            <uc2:attachmentadd ID="Attachmentadd1" runat="server" AttachMentType="ConferenceManage"
                                MasterCode='<%#Bind("Code") %>' />
                            <div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 120px;" valign="top">
                            备注：</td>
                        <td colspan="3">
                            <asp:TextBox ID="TextBoxRemark" runat="server" Text='<%# Bind("Remark") %>' TextMode="MultiLine"
                                Width="100%" Height="70px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <table width="100%" cellpadding="10">
                    <tr align="center">
                        <td>
                            <asp:Button ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update"
                                CssClass="button" Text="更新" />
                            <asp:Button ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                                CssClass="button" Text="取消" /></td>
                    </tr>
                </table>
            </EditItemTemplate>
            <InsertItemTemplate>
                <table class="form" width="100%">
                    <tr>
                        <td class="form-item" style="width: 120px;">
                            会议主题：</td>
                        <td colspan="3">
                            <asp:TextBox ID="TextBoxTopic" runat="server" Text='<%# Bind("Topic") %>' Width="60%"
                                CssClass="input"></asp:TextBox><font color="red">*</font>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBoxTopic"
                                ErrorMessage="不能为空"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 120px;">
                            会议发起人：</td>
                        <td>
                            <asp:TextBox ID="TextBoxChaterMember" runat="server" Text='<%# Bind("ChaterMember") %>'
                                CssClass="input"></asp:TextBox><font color="red">*<asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                                    runat="server" ControlToValidate="TextBoxChaterMember" ErrorMessage="不能为空"></asp:RequiredFieldValidator></font>
                        </td>
                        <td class="form-item" style="width: 120px;">
                            会议类型：</td>
                        <td>
                            &nbsp;<asp:DropDownList ID="DropDownListType" runat="server">
                                <asp:ListItem Selected="True" Value="0">==请选择==</asp:ListItem>
                                <asp:ListItem Value="1">部门例会</asp:ListItem>
                                <asp:ListItem Value="2">部门协调会议</asp:ListItem>
                                <asp:ListItem Value="3">其他性质会议</asp:ListItem>
                            </asp:DropDownList><span style="color: #ff0000">*</span></td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 120px;">
                            会议地点：</td>
                        <td>
                            <asp:TextBox ID="TextBoxPlace" runat="server" Text='<%# Bind("Place") %>' CssClass="input"></asp:TextBox>
                            <font color="red">*</font>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TextBoxPlace"
                                ErrorMessage="不能为空"></asp:RequiredFieldValidator></td>
                        <td class="form-item" style="width: 120px;">
                            主办单位：</td>
                        <td>
                            <uc5:inputunit ID="Inputunit1" runat="server" />
                            <span style="color: #ff0000">*</span>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 120px;">
                            会议开始时间：</td>
                        <td>
                            <cc1:Calendar ID="startDate" runat="server" CalendarMode="All" CalendarResource="../Images/CalendarResource/"
                                Value="">
                            </cc1:Calendar>
                            &nbsp; <font color="red">*</font> <span style="color: Red;"></span>
                        </td>
                        <td class="form-item" style="width: 120px;">
                            会议结束时间：</td>
                        <td>
                            <cc1:Calendar ID="endDate" runat="server" CalendarMode="All" CalendarResource="../Images/CalendarResource/"
                                Value="">
                            </cc1:Calendar>
                            &nbsp;<font color="red">*</font>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 120px;" valign="top">
                            会议应到人员：</td>
                        <td colspan="3">
                            <asp:TextBox ID="AttendPersonnelTextBox" runat="server" TextMode="MultiLine" Width="400px">
                            </asp:TextBox>
                            <asp:TextBox ID="AttendPersonnelCodeTextBox" runat="server" Height="0px" Text='<%# Bind("AttendPersonnel") %>'
                                Width="0px"></asp:TextBox>
                            <img onclick="SelectUsers('SelectUserReturn');return false;" src="../images/ToolsItemSearch.gif"
                                style="cursor: hand" alt="点击选择与会人员" /></td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 120px;" valign="top">
                            其他与会人员：</td>
                        <td colspan="3">
                            <asp:TextBox ID="OtherAttendPersonnelTextBox" runat="server" TextMode="MultiLine"
                                Width="400px">
                            </asp:TextBox>
                            <asp:TextBox ID="OtherAttendPersonnelCodeTextBox" runat="server" Height="0px" Text='<%# Bind("OtherAttendPersonnel") %>'
                                Width="0px"></asp:TextBox>
                            <img onclick="SelectUsers('SelectOtherUserReturn');return false;" src="../images/ToolsItemSearch.gif"
                                style="cursor: hand" alt="点击选择其他与会人员" />
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 120px;" valign="top">
                            附件：</td>
                        <td colspan="3">
                            <uc2:attachmentadd ID="Attachmentadd1" AttachMentType="ConferenceManage" MasterCode='<%# Bind("Code") %>'
                                runat="server"></uc2:attachmentadd>
                            <div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 120px;" valign="top">
                            备注：</td>
                        <td colspan="3">
                            <asp:TextBox ID="TextBoxRemark" runat="server" Text='<%# Bind("Remark") %>' TextMode="MultiLine"
                                Width="100%" Height="60px">
                            </asp:TextBox>
                        </td>
                    </tr>
                </table>
                <table width="100%" cellpadding="10">
                    <tr align="center">
                        <td>
                            <asp:Button ID="InsertButton" runat="server" CausesValidation="True"
                                CssClass="button" Text="添加" OnClick="InsertButton_Click" />
                            <input class="button" type="reset" value="重置" />
                        </td>
                    </tr>
                </table>
            </InsertItemTemplate>
            <ItemTemplate>
                <table class="form" width="100%">
                    <tr>
                        <td class="form-item" style="width: 120px;">
                            会议主题：</td>
                        <td colspan="3">
                            <asp:Label ID="LabelTopic" runat="server" Text='<%# Bind("Topic") %>'></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 120px;">
                            会议发起人：</td>
                        <td>
                            <asp:Label ID="LabelChaterMember" runat="server" Text='<%# Bind("ChaterMember") %>'>
                            </asp:Label>
                        </td>
                        <td class="form-item" style="width: 120px;">
                            会议类型：</td>
                        <td>
                            <asp:Label ID="LabelType" runat="server" Text='<%# Bind("Type") %>'></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 120px;">
                            会议地点：</td>
                        <td>
                            <asp:Label ID="LabelPlace" runat="server" Text='<%# Bind("Place") %>'></asp:Label>
                        </td>
                        <td class="form-item" style="width: 120px;">
                            主办单位：</td>
                        <td>
                            <asp:Label ID="LabelDept" runat="server" Text='<%# Bind("Dept") %>'></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 120px;">
                            会议开始时间：</td>
                        <td>
                            <asp:Label ID="LabelStartTime" runat="server" Text='<%# Bind("StartTime") %>'></asp:Label>
                        </td>
                        <td class="form-item" style="width: 120px;">
                            会议结束时间：</td>
                        <td>
                            <asp:Label ID="LabelEndTime" runat="server" Text='<%# Bind("EndTime") %>'></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 120px;" valign="top">
                            会议应到人员：</td>
                        <td colspan="3">
                        <asp:Label runat="server" ID="LabelAttendPerson"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 120px;" valign="top">
                            其他与会人员：</td>
                        <td colspan="3">
                        <asp:Label runat="server" ID="LabelOtherAttendPerson"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 120px;" valign="top">
                            附件：</td>
                        <td colspan="3">
                            <uc1:attachmentlist ID="Attachmentlist2" AttachMentType="ConferenceManage" MasterCode='<%# Bind("Code") %>'
                                runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 120px;" valign="top">
                            备注：</td>
                        <td colspan="3">
                            <asp:Label ID="LabelRemark" runat="server" Text='<%# Bind("Remark") %>'></asp:Label>
                        </td>
                    </tr>
                </table>
                <table width="100%" cellpadding="10">
                    <tr align="center">
                        <td>
                            <input type="button" id="btClose" onclick="window.close();" value="关闭本页" class="button" />
                        </td>
                    </tr>
                </table>
            </ItemTemplate>
        </asp:FormView>
    </form>
</body>
</html>
