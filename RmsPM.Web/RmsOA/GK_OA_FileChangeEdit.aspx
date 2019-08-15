<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GK_OA_FileChangeEdit.aspx.cs"
    Inherits="RmsOA_GK_OA_FileChangeEdit" %>

<%@ Register Src="../WorkFlowControl/workflowtoolbar.ascx" TagName="workflowtoolbar"
    TagPrefix="uc5" %>
<%@ Register Src="../WorkFlowControl/WorkFlowList.ascx" TagName="WorkFlowList" TagPrefix="uc4" %>
<%@ Register Src="../UserControls/inputcontract.ascx" TagName="inputcontract" TagPrefix="uc3" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Register TagPrefix="uc2" TagName="InputUnit" Src="../UserControls/InputUnit.ascx" %>
<%@ Register TagPrefix="uc1" TagName="InputUser" Src="../UserControls/InputUser.ascx" %>
<%@ Register TagPrefix="uc1" TagName="InputSupplier" Src="../UserControls/InputSupplier.ascx" %>
<%@ Register TagPrefix="cc4" Namespace="Rms.ControlLb" Assembly="RmsPM.BLL" %>
<%@ Register TagPrefix="uc1" TagName="AttachMentAdd" Src="../UserControls/AttachMentAdd.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AttachMentList" Src="../UserControls/AttachMentList.ascx" %>
<%@ Register TagPrefix="uc1" TagName="InputSystemGroup" Src="../UserControls/InputSystemGroup.ascx" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>文件更改</title>
    <link href="../Images/index.css" type="text/css" rel="stylesheet" />

    <script type="text/javascript" language="javascript" src="../Rms.js"></script>

    <script type="text/javascript" language="javascript">
        function SelectUnit()
		{
			OpenSmallWindow("../SelectBox/SelectUnit.aspx?UnitCode=000000");
		}
		function SelectUnitReturn(code, name)
		{
			window.document.all.FormView1_txtUnitName.value = name;
			window.document.all.FormView1_txtUnit.value = code;
		}	
		
		function OpenRequisition()
        {
		    OpenFullWindow('<%= RmsPM.BLL.WorkFlowRule.GetProcedureURLByName("文件更改审批")%>?FileChangeCode=<%= FormView1.DataKey.Value %>&ProjectCode=<%= Request["ProjectCode"] + ""%>','文件更改审批');
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <table height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td height="25">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="topic" align="center" background="../images/topic_bg.gif" height="25">
                                文件更改</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <asp:FormView ID="FormView1" runat="server" DataSourceID="ObjectDataSource1" Width="100%"
                        OnItemInserted="FormView1_ItemInserted" OnItemDeleted="FormView1_ItemDeleted"
                        OnItemUpdated="FormView1_ItemUpdated" DataKeyNames="Code" OnItemInserting="FormView1_ItemInserting" OnDataBound="FormView1_DataBound">
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
                                    <td class="form-item" style="width: 20%">
                                        质量记录编号：</td>
                                    <td>
                                    
                                        <asp:Label ID="Label1" runat="server" Text='GKFC-JL-CX-420105'></asp:Label></td>
                                    <td class="form-item" style="width: 20%">
                                        标识序号：</td>
                                    <td>
                                        <asp:TextBox ID="FileSystemCodeTextBox" runat="server" CssClass="input" Text='<%# Bind("FileSystemCode") %>'></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        文件名称：</td>
                                    <td>
                                        <asp:TextBox ID="FileNameTextBox" runat="server" CssClass="input" Text='<%# Bind("FileName") %>'></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="FileNameTextBox"
                                            ErrorMessage="*"></asp:RequiredFieldValidator></td>
                                    <td class="form-item" style="width: 20%">
                                        文件编号：</td>
                                    <td>
                                        <asp:TextBox ID="FileCodeTextBox" runat="server" CssClass="input" Text='<%# Bind("FileCode") %>'></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="FileCodeTextBox"
                                            ErrorMessage="*"></asp:RequiredFieldValidator></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        原版本号：</td>
                                    <td>
                                        <asp:TextBox ID="OldVersionNumberTextBox" runat="server" CssClass="input" Text='<%# Bind("OldVersionNumber") %>'></asp:TextBox></td>
                                    <td class="form-item" style="width: 20%">
                                        现版本号：</td>
                                    <td>
                                        <asp:TextBox ID="NewVersionNumberTextBox" runat="server" CssClass="input" Text='<%# Bind("NewVersionNumber") %>'></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        申请部门：</td>
                                    <td>
                                        <input id="txtUnit" runat="server" class="input" name="txtUnit" size="8" style="width: 72px;
                                            height: 18px" type="hidden" value='<%# Bind("UnitCode") %>' /><input id="txtUnitName"
                                                runat="server" class="input" name="txtUnit" style="width: 121px; height: 18px"
                                                type="text" /><img onclick="SelectUnit();return false;" src="../images/ToolsItemSearch.gif"
                                                    style="cursor: hand" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtUnitName"
                                            ErrorMessage="*"></asp:RequiredFieldValidator>
                                    </td>
                                    <td class="form-item" style="width: 20%">
                                        提交日期：</td>
                                    <td>
                                        <cc3:Calendar ID="SubmitDateCalendar" runat="server" CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                                            Display="True" ReadOnly="False" Value='<%# Bind("SubmitDate") %>'>
                                        </cc3:Calendar>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        更改原因：</td>
                                    <td colspan="3">
                                        <asp:TextBox ID="ReasionTextBox" runat="server" TextMode="MultiLine" Width="90%"
                                            Text='<%# Bind("ChangeReason") %>'></asp:TextBox>
                                        </td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        原文件内容：</td>
                                    <td colspan="3">
                                        <asp:TextBox ID="OldContextTextBox" runat="server" TextMode="MultiLine" Width="90%"
                                            Text='<%# Bind("OldContext") %>'></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="OldContextTextBox"
                                            ErrorMessage="*"></asp:RequiredFieldValidator></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        更改后文件内容：</td>
                                    <td colspan="3">
                                        <asp:TextBox ID="NewContextTextBox" runat="server" TextMode="MultiLine" Width="90%"
                                            Text='<%# Bind("NewContext") %>'></asp:TextBox>&nbsp;
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="NewContextTextBox"
                                            ErrorMessage="*"></asp:RequiredFieldValidator></td>
                                </tr>
                            </table>
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
                                    <td class="form-item" style="width: 20%">
                                        质量记录编号：</td>
                                    <td>
                                     <asp:Label ID="Label1" runat="server" Text='GKFC-JL-CX-420105'></asp:Label></td>
                                        
                                    <td class="form-item" style="width: 20%">
                                        标识序号：</td>
                                    <td>
                                        <asp:TextBox ID="FileSystemCodeTextBox" runat="server" CssClass="input" Text='<%# Bind("FileSystemCode") %>'></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        文件名称：</td>
                                    <td>
                                        <asp:TextBox ID="FileNameTextBox" runat="server" CssClass="input" Text='<%# Bind("FileName") %>'></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="FileNameTextBox"
                                            ErrorMessage="*"></asp:RequiredFieldValidator></td>
                                    <td class="form-item" style="width: 20%">
                                        文件编号：</td>
                                    <td>
                                        <asp:TextBox ID="FileCodeTextBox" runat="server" CssClass="input" Text='<%# Bind("FileCode") %>'></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="FileCodeTextBox"
                                            ErrorMessage="*"></asp:RequiredFieldValidator></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        原版本号：</td>
                                    <td>
                                        <asp:TextBox ID="OldVersionNumberTextBox" runat="server" CssClass="input" Text='<%# Bind("OldVersionNumber") %>'></asp:TextBox></td>
                                    <td class="form-item" style="width: 20%">
                                        现版本号：</td>
                                    <td>
                                        <asp:TextBox ID="NewVersionNumberTextBox" runat="server" CssClass="input" Text='<%# Bind("NewVersionNumber") %>'></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        申请部门：</td>
                                    <td>
                                        <input id="txtUnit" runat="server" class="input" name="txtUnit" size="8" style="width: 72px;
                                            height: 18px" type="hidden" value='<%# Bind("UnitCode") %>' /><input id="txtUnitName"
                                                runat="server" class="input" name="txtUnit" style="width: 121px; height: 18px"
                                                type="text" /><img onclick="SelectUnit();return false;" src="../images/ToolsItemSearch.gif"
                                                    style="cursor: hand" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtUnitName"
                                            ErrorMessage="*"></asp:RequiredFieldValidator>
                                    </td>
                                    <td class="form-item" style="width: 20%">
                                        提交日期：</td>
                                    <td>
                                        <cc3:Calendar ID="SubmitDateCalendar" runat="server" CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                                            Display="True" ReadOnly="False" Value='<%# Bind("SubmitDate") %>'>
                                        </cc3:Calendar>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        更改原因：</td>
                                    <td colspan="3">
                                        <asp:TextBox ID="ReasionTextBox" runat="server" TextMode="MultiLine" Width="90%"
                                            Text='<%# Bind("ChangeReason") %>'></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        原文件内容：</td>
                                    <td colspan="3">
                                        <asp:TextBox ID="OldContextTextBox" runat="server" TextMode="MultiLine" Width="90%"
                                            Text='<%# Bind("OldContext") %>'></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="OldContextTextBox"
                                            ErrorMessage="*"></asp:RequiredFieldValidator></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        更改后文件内容：</td>
                                    <td colspan="3">
                                        <asp:TextBox ID="NewContextTextBox" runat="server" TextMode="MultiLine" Width="90%"
                                            Text='<%# Bind("NewContext") %>'></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="NewContextTextBox"
                                            ErrorMessage="*"></asp:RequiredFieldValidator></td>
                                </tr>
                            </table>
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
                                        <input name="btnRequisition" id="btnRequisition" type="button" value=" 提交 " class="button"
                                            runat="server" onclick="javascript:OpenRequisition();return false;">
                                        <asp:Button ID="btnBankOut" runat="server" CssClass="button" Text=" 作废 " OnClick="btnBankOut_Click" />
                                        <asp:Button ID="btnArchives" runat="server" CssClass="button" Text="归档" Visible="False"  />
                                        <input id="btnClose" runat="server" class="button" name="btnClose" onclick="javascript:window.close();"
                                            type="button" value=" 关闭 " />
                                    </td>
                                </tr>
                            </table>
                            <table border="0" cellpadding="0" cellspacing="0" class="form" width="100%">
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        质量记录编号：</td>
                                    <td>
                                    <asp:Label ID="Label7" runat="server" Text='GKFC-JL-CX-420105'></asp:Label></td>
                                        
                                    <td class="form-item" style="width: 20%">
                                        标识序号：</td>
                                    <td>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("FileSystemCode") %>'></asp:Label></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        文件名称：</td>
                                    <td>
                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("FileName") %>'></asp:Label></td>
                                    <td class="form-item" style="width: 20%">
                                        文件编号：</td>
                                    <td>
                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("FileCode") %>'></asp:Label></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        原版本号：</td>
                                    <td>
                                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("OldVersionNumber") %>'></asp:Label></td>
                                    <td class="form-item" style="width: 20%">
                                        现版本号：</td>
                                    <td>
                                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("NewVersionNumber") %>'></asp:Label></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        申请部门：</td>
                                    <td>
                                        <asp:Label ID="UnitLabel" runat="server" Text='<%# Bind("UnitCode") %>'></asp:Label></td>
                                    <td class="form-item" style="width: 20%">
                                        提交日期：</td>
                                    <td>
                                        <asp:Label ID="Label8" runat="server" Text='<%# Bind("SubmitDate") %>'></asp:Label></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        更改原因：</td>
                                    <td colspan="3">
                                        <asp:Label ID="Label11" runat="server" Text='<%# Bind("ChangeReason") %>'></asp:Label></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        原文件内容：</td>
                                    <td colspan="3">
                                        <asp:Label ID="Label9" runat="server" Text='<%# Bind("OldContext") %>'></asp:Label></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        更改后文件内容：</td>
                                    <td colspan="3">
                                        <asp:Label ID="Label10" runat="server" Text='<%# Bind("NewContext") %>'></asp:Label></td>
                                </tr>
                            </table>
                            <table cellspacing="0" cellpadding="0" width="470" border="0">
                                <tr id="webtabs">
                                    <td width="20">
                                    </td>
                                    <td class="TabDisplay" id="workflowmsg" runat="server" width="185">
                                        相关流程</td>
                                </tr>
                            </table>
                            <table cellspacing="0" cellpadding="0" width="100%" border="0" id="tabdiv">
                                <tr>
                                    <td>
                                        <uc4:WorkFlowList ID="WorkFlowList1" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:FormView>
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="RmsOA.MODEL.GK_OA_FileChangeModel"
                        DeleteMethod="Delete" InsertMethod="Insert" SelectMethod="GetGK_OA_FileChangeListOne"
                        TypeName="RmsOA.BFL.GK_OA_FileChangeBFL" UpdateMethod="Update" OnInserted="ObjectDataSource1_Inserted">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="Code" QueryStringField="Code" Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
