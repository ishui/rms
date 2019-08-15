<%@ Page Language="C#" AutoEventWireup="true" CodeFile="XZ_MeetSummaryEdit.aspx.cs"
    Inherits="RmsOA_XZ_MeetSummaryEdit" ValidateRequest="false" %>
<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>
<%@ Register Src="../WorkFlowControl/WorkFlowList.ascx" TagName="WorkFlowList" TagPrefix="uc4" %>
<%@ Register Src="../UserControls/inputunit.ascx" TagName="inputunit" TagPrefix="uc1" %>
<%@ Register Assembly="AspWebControl" Namespace="AspWebControl" TagPrefix="cc1" %>
<%@ Register TagPrefix="uc1" TagName="InputUser" Src="../UserControls/InputUser.ascx" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../Images/index.css" type="text/css" rel="stylesheet" />

    <script language="javascript" type="text/javascript" src="../Rms.js"></script>

    <script language="javascript" type="text/javascript">
        function SelectUnit()
		{
			OpenSmallWindow("../SelectBox/SelectUnit.aspx?UnitCode=000000");
		}
		function SelectUnitReturn(code, name)
		{
			window.document.all.FormView1_txtUnitName.value = name;
			window.document.all.FormView1_txtUnit.value = code;
		}
		
		  function SelectUsers(ReturnFunc)
                {
                    if (ReturnFunc=="SelectUserReturn")
                    {
                        var myUserid = document.all("MeetFormView_AttendPersonnelCodeTextBox");
                       
                    }else
                    {
                        var myUserid = document.all("MeetFormView_OtherAttendPersonnelCodeTextBox");               
                    }           
                    OpenMiddleWindow("../SelectBox/SelectSUMain.aspx?Type=U&ReturnFunc=" + ReturnFunc +"&UserCodes=" + myUserid.value , "选择用户");
                    
                }    
        　　	
	            function SelectUserReturn(userCodes,userNames,stationCodes,stationNames,flag)
	            {
	                var AttendPersonnel=window.document.all("MeetFormView_AttendPersonnelTextBox");
	                var AttendPersonnelCode=window.document.all("MeetFormView_AttendPersonnelCodeTextBox");
        	        
	                AttendPersonnel.value=userNames;
	                AttendPersonnelCode.value=userCodes;
	            }
	            function SelectOtherUserReturn(userCodes,userNames,stationCodes,stationNames,flag)
	            {
	               var OtherAttendPersonnel=window.document.all("MeetFormView_OtherAttendPersonnelTextBox");
	               var OtherAttendPersonnelCode=window.document.all("MeetFormView_OtherAttendPersonnelCodeTextBox");
        	      
	                 OtherAttendPersonnel.value=userNames;
	                OtherAttendPersonnelCode.value=userCodes;
	            }
    </script>

    <title>会议纪要</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td background="../images/topic_bg.gif" class="topic" style="height: 25px; text-align: center;">
                        会议纪要</td>
                </tr>
            </table>
            <asp:FormView ID="MeetFormView" runat="server" DataSourceID="ObjectDataSource1" Width="100%"
                OnItemInserting="MeetFormView_ItemInserting" OnItemInserted="MeetFormView_ItemInserted"
                OnItemUpdating="MeetFormView_ItemUpdating" DataKeyNames="Code" OnDataBound="MeetFormView_DataBound"
                OnItemUpdated="MeetFormView_ItemUpdated" OnItemDeleted="MeetFormView_ItemDeleted">
                <EditItemTemplate>
                    <table id="Table2" class="table" width="100%">
                        <tr>
                            <td class="tools-area" width="16">
                                <img align="absMiddle" src="../images/btn_li.gif" /></td>
                            <td class="tools-area">
                                <asp:Button ID="btnSave" runat="server" CommandName="Update" CssClass="button" Text=" 保存 " />&nbsp;
                                <input id="btnClose" runat="server" class="button" name="btnClose" onclick="javascript:window.close();"
                                    type="button" value=" 关闭 " />
                            </td>
                        </tr>
                    </table>
                    <table border="0" cellpadding="0" cellspacing="0" class="form" width="100%">
                        <tr>
                            <td class="form-item" style="width: 100px;">
                                会议主题：</td>
                            <td colspan="3">
                                <asp:TextBox ID="TitleTextBox" runat="server" Text='<%# Bind("Title") %>' CssClass="input"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TitleTextBox"
                                    ErrorMessage="*"></asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td class="form-item" style="width: 100px;">
                                质量记录分类号：</td>
                            <td>
                                <asp:TextBox ID="SortCodeTextBox" runat="server" Text='<%# Bind("SortCode") %>' CssClass="input" />
                            </td>
                            <td class="form-item" style="width: 100px;">
                                标识序号：</td>
                            <td>
                                <asp:TextBox ID="CodeRuleTextBox" runat="server" Text='<%# Bind("CodeRule") %>' CssClass="input" />
                            </td>
                        </tr>
                        <tr>
                            <td class="form-item" style="width: 100px;">
                                主持人：</td>
                            <td>
                                <uc1:InputUser ID="InPersonBox" runat="server" Value='<%# Bind("CharterMember") %>'>
                                </uc1:InputUser>
                            </td>
                            <td class="form-item" style="width: 100px;">
                                会议类型：</td>
                            <td>
                                <asp:DropDownList ID="TypeDropDownList" runat="server" DataSourceID="ObjectDataSource2"
                                    Font-Size="9pt" SelectedValue='<%# Bind("Type") %>'>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TypeDropDownList"
                                    ErrorMessage="*"></asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td class="form-item" style="width: 100px;">
                                记 &nbsp; &nbsp;&nbsp;录：</td>
                            <td>
                                <uc1:InputUser ID="RecorderInputUser" runat="server" Value='<%# Bind("Recoder") %>'>
                                </uc1:InputUser>
                            </td>
                            <td class="form-item" style="width: 100px;">
                                会议部门：</td>
                            <td>
                                <uc1:inputunit ID="Inputunit1" runat="server" Value='<%# Bind("Dept") %>' />
                            </td>
                        </tr>
                        <tr>
                        </tr>
                        <tr>
                            <td class="form-item" style="width: 100px;">
                                会议地点：</td>
                            <td>
                                <asp:DropDownList ID="PlaceDropDownList" runat="server" DataSourceID="ObjectDataSource3"
                                    DataTextField="RoomName" DataValueField="RoomCode" SelectedValue='<%# Bind("Place") %>'>
                                </asp:DropDownList>
                            </td>
                            <td class="form-item" style="width: 100px;">
                                会议时间：</td>
                            <td>
                                <cc1:Calendar ID="MeetStartTime" runat="server" CalendarMode="All" CalendarResource="../Images/CalendarResource/"
                                    Value='<%# Bind("MeetStartTime") %>'>
                                </cc1:Calendar>
                                至
                                <cc1:Calendar ID="MeetEndTime" runat="server" CalendarMode="All" CalendarResource="../Images/CalendarResource/"
                                    Value='<%# Bind("MeetEndTime") %>'>
                                </cc1:Calendar>
                            </td>
                        </tr>
                        <tr>
                            <td class="form-item" style="width: 100px;">
                                出席人员：</td>
                            <td colspan="3">
                             <asp:TextBox ID="AttendPersonnelTextBox" runat="server" TextMode="MultiLine" Width="400px">
                                </asp:TextBox>
                                <asp:TextBox ID="AttendPersonnelCodeTextBox" runat="server" Text='<%# Bind("AttendPersons") %>'
                                    Width="0px" Height="0px"></asp:TextBox>
                                <img style="cursor: hand" onclick="SelectUsers('SelectUserReturn');return false;"
                                    src="../images/ToolsItemSearch.gif" alt="" />
                            </td>
                        </tr>
                        <tr>
                            <td class="form-item" style="width: 100px;">
                                列席人员：</td>
                            <td colspan="3">
                                 <asp:TextBox ID="OtherAttendPersonnelTextBox" runat="server" TextMode="MultiLine"
                                    Width="400px">
                                </asp:TextBox>
                                <asp:TextBox ID="OtherAttendPersonnelCodeTextBox" runat="server" Text='<%# Bind("OtherPerson") %>'
                                    Width="0px" Height="0px"></asp:TextBox>
                                <img style="cursor: hand" onclick="SelectUsers('SelectOtherUserReturn');return false;"
                                    src="../images/ToolsItemSearch.gif" alt="" />
                            </td>
                        </tr>
                        <tr>
                            <td class="form-item" style="width: 100px;">
                                会议内容：
                            </td>
                            <td colspan="3">
                            </td>
                        </tr>
                    </table>
                    <div>
                        <FTB:FreeTextBox ID="ContextTextBox" Text='<%# Bind("Context") %>' Width="100%" runat="server"
                            ButtonPath="../images/ftb/office2003/" Height="500px" StyleMenuTitle="" ToolbarBackGroundImage="True"
                            ToolbarType="Office2003">
                        </FTB:FreeTextBox>
                    </div>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <table id="Table1" class="table" width="100%">
                        <tr>
                            <td class="tools-area" width="16">
                                <img align="absMiddle" src="../images/btn_li.gif" /></td>
                            <td class="tools-area">
                                <asp:Button ID="btnSave" runat="server" CommandName="Insert" CssClass="button" Text=" 保存 " />
                                <input id="btnClose" runat="server" class="button" name="btnClose" onclick="javascript:window.close();"
                                    type="button" value=" 关闭 " />
                            </td>
                        </tr>
                    </table>
                    <table border="0" cellpadding="0" cellspacing="0" class="form" width="100%">
                        <tr>
                            <td class="form-item" style="width: 100px;">
                                会议主题：</td>
                            <td colspan="3">
                                <asp:TextBox ID="TitleTextBox" runat="server" Text='<%# Bind("Title") %>' CssClass="input"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TitleTextBox"
                                    ErrorMessage="*"></asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td class="form-item" style="width: 100px;">
                                质量记录分类号：</td>
                            <td>
                                <asp:TextBox ID="SortCodeTextBox" runat="server" Text='<%# Bind("SortCode") %>' CssClass="input" />
                            </td>
                            <td class="form-item" style="width: 100px;">
                                标识序号：</td>
                            <td>
                                <asp:TextBox ID="CodeRuleTextBox" runat="server" Text='<%# Bind("CodeRule") %>' CssClass="input" />
                            </td>
                        </tr>
                        <tr>
                            <td class="form-item" style="width: 100px;">
                                主持人：</td>
                            <td>
                                <uc1:InputUser ID="InPersonBox" runat="server" Value='<%# Bind("CharterMember") %>'>
                                </uc1:InputUser>
                            </td>
                            <td class="form-item" style="width: 100px;">
                                会议类型：</td>
                            <td>
                                <asp:DropDownList ID="TypeDropDownList" runat="server" DataSourceID="ObjectDataSource2"
                                    Font-Size="9pt" SelectedValue='<%# Bind("Type") %>'>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TypeDropDownList"
                                    ErrorMessage="*"></asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td class="form-item" style="width: 100px;">
                                记 &nbsp; &nbsp;&nbsp;录：</td>
                            <td>
                                <uc1:InputUser ID="RecorderInputUser" runat="server" Value='<%# Bind("Recoder") %>'>
                                </uc1:InputUser>
                            </td>
                            <td class="form-item" style="width: 100px;">
                                会议部门：</td>
                            <td>
                                <uc1:inputunit ID="Inputunit1" runat="server" Value='<%# Bind("Dept") %>' />
                            </td>
                        </tr>
                        <tr>
                        </tr>
                        <tr>
                            <td class="form-item" style="width: 100px;">
                                会议地点：</td>
                            <td>
                                <asp:DropDownList ID="PlaceDropDownList" runat="server" DataSourceID="ObjectDataSource3"
                                    DataTextField="RoomName" DataValueField="RoomCode" SelectedValue='<%# Bind("Place") %>'>
                                </asp:DropDownList>
                            </td>
                            <td class="form-item" style="width: 100px;">
                                会议时间：</td>
                            <td>
                                <cc1:Calendar ID="MeetStartTime" runat="server" CalendarMode="All" CalendarResource="../Images/CalendarResource/"
                                    Value='<%# Bind("MeetStartTime") %>'>
                                </cc1:Calendar>
                                至
                                <cc1:Calendar ID="MeetEndTime" runat="server" CalendarMode="All" CalendarResource="../Images/CalendarResource/"
                                    Value='<%# Bind("MeetEndTime") %>'>
                                </cc1:Calendar>
                            </td>
                        </tr>
                        <tr>
                            <td class="form-item" style="width: 100px;">
                                出席人员：</td>
                            <td colspan="3">
                             <asp:TextBox ID="AttendPersonnelTextBox" runat="server" TextMode="MultiLine" Width="400px">
                                </asp:TextBox>
                                <asp:TextBox ID="AttendPersonnelCodeTextBox" runat="server" Text='<%# Bind("AttendPersons") %>'
                                    Width="0px" Height="0px"></asp:TextBox>
                                <img style="cursor: hand" onclick="SelectUsers('SelectUserReturn');return false;"
                                    src="../images/ToolsItemSearch.gif" alt="" />
                            </td>
                        </tr>
                        <tr>
                            <td class="form-item" style="width: 100px;">
                                列席人员：</td>
                            <td colspan="3">
                                 <asp:TextBox ID="OtherAttendPersonnelTextBox" runat="server" TextMode="MultiLine"
                                    Width="400px">
                                </asp:TextBox>
                                <asp:TextBox ID="OtherAttendPersonnelCodeTextBox" runat="server" Text='<%# Bind("OtherPerson") %>'
                                    Width="0px" Height="0px"></asp:TextBox>
                                <img style="cursor: hand" onclick="SelectUsers('SelectOtherUserReturn');return false;"
                                    src="../images/ToolsItemSearch.gif" alt="" />
                            </td>
                        </tr>
                        <tr>
                            <td class="form-item" style="width: 100px;">
                                会议内容：
                            </td>
                            <td colspan="3">
                            </td>
                        </tr>
                    </table>
                    <div>
                        <FTB:FreeTextBox ID="ContextTextBox" Text='<%# Bind("Context") %>' Width="100%" runat="server"
                            ButtonPath="../images/ftb/office2003/" Height="500px" StyleMenuTitle="" ToolbarBackGroundImage="True"
                            ToolbarType="Office2003">
                        </FTB:FreeTextBox>
                    </div>
                </InsertItemTemplate>
                <ItemTemplate>
                    <table id="Table3" class="table" width="100%">
                        <tr>
                            <td class="tools-area" width="16">
                                <img align="absMiddle" src="../images/btn_li.gif" /></td>
                            <td class="tools-area">
                                <asp:Button ID="EditButton" runat="server" CommandName="Edit" CssClass="button" Text=" 修改 " />
                                <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" CssClass="button"
                                    Text=" 删除 " />
                                <input id="btnRequisition" runat="server" class="button"
                                    value=" 提交 " type="button" onserverclick="SubmitButton_Click"/>
                                <asp:Button ID="btnBankOut" runat="server" CssClass="button" OnClick="btnBankOut_Click"
                                    Text=" 作废 " />
                                <input id="btnClose" runat="server" class="button" name="btnClose" onclick="javascript:window.close();"
                                    type="button" value=" 关闭 " />
                            </td>
                        </tr>
                    </table>
                    <table border="0" cellpadding="0" cellspacing="0" class="form" width="100%">
                        <tr>
                            <td class="form-item" style="width: 100px;">
                                会议主题：</td>
                            <td colspan="3">
                                <asp:Label ID="TitleLabel" runat="server" Text='<%# Bind("Title") %>'></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="form-item" style="width: 100px;">
                                质量记录分类号：</td>
                            <td>
                                <asp:Label ID="SortCodeLabel" runat="server" Text='<%# Bind("SortCode") %>'></asp:Label>
                            </td>
                            <td class="form-item" style="width: 100px;">
                                标识序号：</td>
                            <td>
                                <asp:Label ID="CodeRuleLabel" runat="server" Text='<%# Bind("CodeRule") %>'></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="form-item" style="width: 100px;">
                                主持人：</td>
                            <td>
                                <asp:Label ID="CharterMemberLabel" runat="server" Text='<%# RmsPM.BLL.SystemRule.GetUserName((string)Eval("CharterMember")) %> '>
                                </asp:Label>
                            </td>
                            <td class="form-item" style="width: 100px;">
                                会议类型：</td>
                            <td>
                                <asp:Label ID="TypeLabel" runat="server" Text='<%# Bind("Type") %>'></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="form-item" style="width: 100px;">
                                记 &nbsp; &nbsp;&nbsp;录：</td>
                            <td>
                                <asp:Label ID="RecoderLabel" runat="server" Text='<%# RmsPM.BLL.SystemRule.GetUserName((string)Eval("Recoder")) %>'></asp:Label>
                            </td>
                            <td class="form-item" style="width: 100px;">
                                会议部门：</td>
                            <td>
                                <asp:Label ID="DeptLabel" runat="server" Text='<%# RmsPM.BLL.SystemRule.GetUnitName((string)Eval("Dept"))%>'></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="form-item" style="width: 100px;">
                                会议地点：</td>
                            <td>
                                <asp:Label ID="PlaceLabel" runat="server" Text='<%# RmsOA.BFL.ConferenceUserListBFLFacade.GetMeetRoomName((string)Eval("Place")) %>'></asp:Label>
                            </td>
                            <td class="form-item" style="width: 100px;">
                                会议时间：</td>
                            <td>
                                <asp:Label ID="MeetStartTimeLabel" runat="server" Text='<%# Bind("MeetStartTime") %>'></asp:Label>至
                                <asp:Label ID="MeetEndtTimeLabel" runat="server" Text='<%# Bind("MeetEndTime") %>'></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="form-item" style="width: 100px;">
                                出席人员：</td>
                            <td colspan="3">
                            <asp:HiddenField ID="HidAttendPerson" Value='<%# Bind("AttendPersons") %>' runat="server"/>
                                <asp:Label ID="AttendPersonsLabel" runat="server">
                                </asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="form-item" style="width: 100px;">
                                列席人员：</td>
                            <td colspan="3">
                            <asp:HiddenField ID="HidOtherAttend" Value='<%# Bind("OtherPerson") %>' runat="server"/>
                                <asp:Label ID="OtherPersonLabel" runat="server">
                                </asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="form-item" style="width: 100px;">
                                会议内容：
                            </td>
                            <td colspan="3">
                                <asp:Label ID="ContextLabel" runat="server" Text='<%# Bind("Context") %>'></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table border="0" cellpadding="0" cellspacing="0" width="470">
                        <tr id="webtabs">
                            <td width="20">
                            </td>
                            <td id="workflowmsg" runat="server" class="TabDisplay" width="185">
                                相关流程</td>
                        </tr>
                    </table>
                    <table id="tabdiv" border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td>
                                <uc4:WorkFlowList ID="WorkFlowList1" runat="server" />
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:FormView>
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetGK_OA_MeetSummaryListOne"
                TypeName="RmsOA.BFL.GK_OA_MeetSummaryBFL" DataObjectTypeName="RmsOA.MODEL.GK_OA_MeetSummaryModel"
                InsertMethod="Insert" UpdateMethod="Update" OnInserted="ObjectDataSource1_Inserted"
                DeleteMethod="Delete" OldValuesParameterFormatString="original_{0}">
                <SelectParameters>
                    <asp:QueryStringParameter Name="Code" QueryStringField="Code" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="GetConferenceType"
                TypeName="RmsOA.BFL.ConferenceManageBFL"></asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjectDataSource3" runat="server" SelectMethod="GetRoomList"
                TypeName="RmsOA.BFL.ConferenceUserListBFLFacade"></asp:ObjectDataSource>
        </div>
    </form>
</body>
</html>
