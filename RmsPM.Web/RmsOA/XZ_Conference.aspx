<%@ Page Language="C#" MasterPageFile="~/RmsOA/XZ_ConferenceMasterPage.master"
    AutoEventWireup="true" CodeFile="XZ_Conference.aspx.cs" Inherits="RmsOA_XZ_Conference"
    Title="会议详细信息" %>

<%@ Register Src="../UserControls/inputunit.ascx" TagName="inputunit" TagPrefix="uc5" %>
<%@ Register Src="../UserControls/attachmentlist.ascx" TagName="attachmentlist" TagPrefix="uc1" %>
<%@ Register Src="../UserControls/inputusers.ascx" TagName="inputusers" TagPrefix="uc3" %>
<%@ Register Src="../UserControls/inputusers.ascx" TagName="inputusers" TagPrefix="uc4" %>
<%@ Register Assembly="AspWebControl" Namespace="AspWebControl" TagPrefix="cc1" %>
<%@ Register Src="../UserControls/attachmentadd.ascx" TagName="attachmentadd" TagPrefix="uc2" %>
<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">

    <script src="../Rms.js" type="text/javascript"></script>

    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetConferenceManageListOne"
        TypeName="RmsOA.BFL.ConferenceManageBFL" DataObjectTypeName="RmsOA.MODEL.ConferenceManageModel"
        DeleteMethod="Delete" OldValuesParameterFormatString="original_{0}" UpdateMethod="Update" OnInserted="ObjectDataSource1_Inserted">
        <SelectParameters>
            <asp:QueryStringParameter Name="Code" QueryStringField="Code" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>

    <script language="javascript" type="text/javascript">
        function SelectUsers(ReturnFunc)
        {
            if (ReturnFunc=="SelectUserReturn")
            {
                var myUserid = window.document.all("_ctl0_ContentPlaceHolder_FormView1_AttendPersonnelCodeTextBox");
               
            }else
            {
                var myUserid = window.document.all("_ctl0_ContentPlaceHolder_FormView1_OtherAttendPersonnelCodeTextBox");               
            }          
            OpenMiddleWindow("../SelectBox/SelectSUMain.aspx?Type=U&ReturnFunc=" + ReturnFunc +"&UserCodes=" + myUserid.value , "选择用户");
            
        }    
　　	
	    function SelectUserReturn(userCodes,userNames,stationCodes,stationNames,flag)
	    {
	        var AttendPersonnel=window.document.all("_ctl0_ContentPlaceHolder_FormView1_AttendPersonnelTextBox");
	        var AttendPersonnelCode=window.document.all("_ctl0_ContentPlaceHolder_FormView1_AttendPersonnelCodeTextBox");
	        
	        AttendPersonnel.value=userNames;
	        AttendPersonnelCode.value=userCodes;
	    }
	    function SelectOtherUserReturn(userCodes,userNames,stationCodes,stationNames,flag)
	    {
	       var OtherAttendPersonnel=window.document.all("_ctl0_ContentPlaceHolder_FormView1_OtherAttendPersonnelTextBox");
	       var OtherAttendPersonnelCode=window.document.all("_ctl0_ContentPlaceHolder_FormView1_OtherAttendPersonnelCodeTextBox");
	      
	        OtherAttendPersonnel.value=userNames;
	        OtherAttendPersonnelCode.value=userCodes;
	    }
	    function test()
	    {
	     var StartYear=window.document.all("_ctl0:ContentPlaceHolder:FormView1:startDate_month").value;
	     window.alert(StartYear);
	    }
	    function SelectRoom(ReturnFunc)
        {
           var StartYear=window.document.all("_ctl0:ContentPlaceHolder:FormView1:startDate_year").value;
           var StartMonth=window.document.all("_ctl0:ContentPlaceHolder:FormView1:startDate_month").value;
           var StartDay=window.document.all("_ctl0:ContentPlaceHolder:FormView1:startDate_day").value;
           var StartHour=window.document.all("_ctl0:ContentPlaceHolder:FormView1:startDate_hour").value;
           var StartMinute=window.document.all("_ctl0:ContentPlaceHolder:FormView1:startDate_minute").value;
           var StartDateTime = StartYear + "-" + StartMonth + "-" + StartDay + " " + StartHour + ":" + StartMinute ; 
	       
           var EndYear=window.document.all("_ctl0:ContentPlaceHolder:FormView1:endDate_year").value;
           var EndMonth=window.document.all("_ctl0:ContentPlaceHolder:FormView1:endDate_month").value;
           var EndDay=window.document.all("_ctl0:ContentPlaceHolder:FormView1:endDate_day").value;
           var EndHour=window.document.all("_ctl0:ContentPlaceHolder:FormView1:endDate_hour").value;
           var EndMinute=window.document.all("_ctl0:ContentPlaceHolder:FormView1:endDate_minute").value;
           var EndDateTime = EndYear + "-" + EndMonth + "-" + EndDay + " " + EndHour + ":" + EndMinute ;	        
           OpenMiddleWindow("../RmsOA/XZ_SelectMeetRoom.aspx?ReturnFunc=" + ReturnFunc + "&begin="+ StartDateTime +"&end="+ EndDateTime +" ");
        }
        function ChooseReturnRoom(RoomCode,RoomName)
        {	       
            var AssemblyRoomName=window.document.all("_ctl0_ContentPlaceHolder_FormView1_TextBoxPlace");
            var AssemblyRoomCode=window.document.all("_ctl0_ContentPlaceHolder_FormView1_HidRoomCode");
	        
            AssemblyRoomCode.value=RoomCode;
            AssemblyRoomName.value=RoomName;
        }
    </script>

    <asp:FormView ID="FormView1" runat="server" DataKeyNames="Code" DataSourceID="ObjectDataSource1"
        Width="100%" OnItemDeleted="FormView1_ItemDeleted" OnDataBound="FormView1_DataBound">
        <EditItemTemplate>
            <table class="form" width="100%">
                <tr>
                    <td class="form-item" style="width: 120px;">
                        会议主题：</td>
                    <td colspan="3">
                        <asp:TextBox ID="TextBoxTopic" runat="server" CssClass="input" Text='<%# Bind("Topic") %>'
                            Width="60%"></asp:TextBox><font color="red">*</font>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBoxTopic"
                            ErrorMessage="不能为空"></asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td class="form-item" style="width: 120px;">
                        会议发起人：</td>
                    <span style="color: Red;"></span>
                    <td>
                        <asp:Label ID="LabelChaterMember" runat="server" CssClass="input" Text='<%# Bind("ChaterMember") %>'></asp:Label></td>
                    <td class="form-item" style="width: 120px;">
                        会议类型：</td>
                    <td>
                        <asp:TextBox ID="TextBoxType" runat="server" CssClass="input" Text='<%# Bind("Type") %>'></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="form-item" style="width: 120px;">
                        会议地点：</td>
                    <td>
                        <asp:TextBox ID="TextBoxPlace" runat="server" CssClass="input" Text='<%# Bind("Place") %>'></asp:TextBox><font
                            color="red">*<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                ControlToValidate="TextBoxPlace" ErrorMessage="不能为空"></asp:RequiredFieldValidator></font>
                    </td>
                    <td class="form-item" style="width: 120px;">
                        主办单位：</td>
                    <td>
                        <asp:TextBox ID="TextBoxDept" runat="server" CssClass="input" Text='<%# Bind("Dept") %>'></asp:TextBox><font
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
                        <asp:TextBox ID="TextBoxAttend" runat="server" TextMode="MultiLine"></asp:TextBox>
                        <uc3:inputusers ID="AttendUser" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="form-item" valign="top">
                        其他与会人员：</td>
                    <td colspan="3">
                    </td>
                    <asp:TextBox ID="OtherAttendPersonnelTextBox" runat="server" TextMode="MultiLine"
                        Width="400px">
                    </asp:TextBox><asp:TextBox ID="OtherAttendPersonnelCodeTextBox" runat="server" Height="0px"
                        Text='<%# Bind("OtherAttendPersonnel") %>' Width="0px"></asp:TextBox><img alt="点击选择人员"
                            onclick="SelectUsers('SelectOtherUserReturn');return false;" src="../images/ToolsItemSearch.gif"
                            style="cursor: hand" />
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
                        <asp:TextBox ID="TextBoxRemark" runat="server" Height="70px" Text='<%# Bind("Remark") %>'
                            TextMode="MultiLine" Width="100%"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <table cellpadding="10" width="100%">
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
                        <asp:TextBox ID="TextBoxTopic" runat="server" CssClass="input" Text='<%# Bind("Topic") %>'
                            Width="60%"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBoxTopic"
                            ErrorMessage="不能为空"></asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td class="form-item" style="width: 120px;">
                        会议发起人：</td>
                    <td>
                        <asp:Label ID="LabelChaterMember" runat="server" CssClass="input" Text='<%# Bind("ChaterMember") %>'></asp:Label><font
                            color="red"></font></td>
                    <td class="form-item" style="width: 120px;">
                        会议类型：</td>
                    <td>
                        <asp:DropDownList ID="DropDownListType" runat="server" DataSourceID="ObjectDataSource2">
                        </asp:DropDownList><span style="color: #ff0000">*</span></td>
                </tr>
                <tr>
                    <td class="form-item" style="width: 120px;">
                        会议开始时间：</td>
                    <td>
                        <span style="color: Red;">
                            <cc1:Calendar ID="startDate" runat="server" CalendarMode="All" ReadOnly="false" CalendarResource="../Images/CalendarResource/"
                                Value="">
                            </cc1:Calendar>
                        </span>
                    </td>
                    <td class="form-item" style="width: 120px;">
                        会议结束时间：</td>
                    <td>
                        <cc1:Calendar ID="endDate" runat="server" CalendarMode="All" CalendarResource="../Images/CalendarResource/"
                            ReadOnly="false" Value="">
                        </cc1:Calendar>
                    </td>
                </tr>
                <tr>
                    <td class="form-item" style="width: 120px;">
                        会议地点：</td>
                    <td>
                        <asp:TextBox ID="TextBoxPlace" runat="server" CssClass="input"></asp:TextBox>
                        <asp:HiddenField ID="HidRoomCode" runat="server" Value='<%#Bind("Place") %>' />
                        <a href="#" onclick="SelectRoom('ChooseReturnRoom');return false;">选择会议室</a>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TextBoxPlace"
                            ErrorMessage="不能为空"></asp:RequiredFieldValidator></td>
                    <td class="form-item" style="width: 120px;">
                        主办单位：</td>
                    <td>
                        <uc5:inputunit ID="Inputunit1" runat="server" />
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
                        <img alt="点击选择与会人员" onclick="SelectUsers('SelectUserReturn');return false;" src="../images/ToolsItemSearch.gif"
                            style="cursor: hand" /></td>
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
                        <img alt="点击选择其他与会人员" onclick="SelectUsers('SelectOtherUserReturn');return false;"
                            src="../images/ToolsItemSearch.gif" style="cursor: hand" />
                    </td>
                </tr>
                <tr>
                    <td class="form-item" style="width: 120px;" valign="top">
                        附件：</td>
                    <td colspan="3">
                        <uc2:attachmentadd ID="Attachmentadd1" runat="server" AttachMentType="ConferenceManage" />
                        <div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="form-item" style="width: 120px;" valign="top">
                        备注：</td>
                    <td colspan="3">
                        <asp:TextBox ID="TextBoxRemark" runat="server" Height="60px" Text='<%# Bind("Remark") %>'
                            TextMode="MultiLine" Width="100%">
                        </asp:TextBox>
                    </td>
                </tr>
            </table>
            <table cellpadding="10" width="100%">
                <tr align="center">
                    <td>
                        <asp:Button ID="InsertButton" runat="server" CausesValidation="True" CssClass="button"
                            OnClick="InsertButton_Click" Text="添加" />
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
                        <asp:Label ID="LabelChaterMember" runat="server" Text='<%# RmsPM.BLL.SystemRule.GetUserName((string)Eval("ChaterMember"))%>'>
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
                    <td class="form-item" style="width: 120px;">
                        会议地点：</td>
                    <td>
                        <asp:Label ID="LabelPlace" runat="server" Text='<%# Bind("Place") %>'></asp:Label>
                    </td>
                    <td class="form-item" style="width: 120px;">
                        主办单位：</td>
                    <td>
                        <asp:Label ID="LabelDept" runat="server"></asp:Label>
                        <asp:HiddenField runat="server" ID="hidDept" Value='<%# Bind("Dept") %>' />
                    </td>
                </tr>
                <tr>
                    <td class="form-item" style="width: 120px;" valign="top">
                        会议应到人员：</td>
                    <td colspan="3">
                        <asp:Label ID="LabelAttendPerson" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="form-item" style="width: 120px;" valign="top">
                        其他与会人员：</td>
                    <td colspan="3">
                        <asp:Label ID="LabelOtherAttendPerson" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="form-item" style="width: 120px;" valign="top">
                        附件：</td>
                    <td colspan="3">
                    <uc1:attachmentlist ID="Attachmentlist2" runat="server" AttachMentType="ConferenceManage"
                            MasterCode='<%# Bind("Code") %>' />
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
            <table cellpadding="10" width="100%">
                <tr align="center">
                    <td>
                        <asp:Button runat="server" ID="DeleteButton" CssClass="button" Text="删除" CommandName="Delete" />
                        <input id="btClose" class="button" onclick="window.close();" type="button" value="关闭" />
                    </td>
                </tr>
            </table>
        </ItemTemplate>
    </asp:FormView>
    <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="GetConferenceType"
        TypeName="RmsOA.BFL.ConferenceManageBFL"></asp:ObjectDataSource>
</asp:Content>
