<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GK_OA_ComBookEdit.aspx.cs"
    Inherits="RmsOA_GK_OA_ComBookEdit" %>

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
    <title>通讯录</title>
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
		    OpenFullWindow('<%= RmsPM.BLL.WorkFlowRule.GetProcedureURLByName("通讯录")%>?ArchivesCopyCode=<%= FormView1.DataKey.Value %>&ProjectCode=<%= Request["ProjectCode"] + ""%>','ArchivesCopyDetailq');
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
                                通讯录</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <asp:FormView ID="FormView1" runat="server" DataSourceID="ObjectDataSource1" Width="100%"
                        OnItemInserted="FormView1_ItemInserted" OnItemDeleted="FormView1_ItemDeleted"
                        OnItemUpdated="FormView1_ItemUpdated" DataKeyNames="Code" 
                        OnDataBound="FormView1_DataBound">
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
                                        用户名：</td>
                                    <td>
                                        <asp:TextBox ID="FileNameTextBox" runat="server" CssClass="input" Text='<%# Bind("UserCode") %>'></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="FileNameTextBox"
                                            ErrorMessage="*"></asp:RequiredFieldValidator></td>
                                             <td class="form-item" style="width: 20%">
                                        部门：</td>
                                    <td>
                                        <input id="txtUnit" runat="server" class="input" name="txtUnit" size="8" style="width: 72px;
                                            height: 18px" type="hidden" value='<%# Bind("UnitCode") %>' /><input id="txtUnitName"
                                                runat="server" class="input" name="txtUnit" style="width: 121px; height: 18px"
                                                type="text" /><img onclick="SelectUnit();return false;" src="../images/ToolsItemSearch.gif"
                                                    style="cursor: hand" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtUnitName"
                                            ErrorMessage="*"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                   
                                    <td class="form-item" style="width: 20%">
                                        电话：</td>
                                    <td>
                                        <asp:TextBox ID="TextBox5" runat="server" CssClass="input" Text='<%# Bind("Telephone") %>'></asp:TextBox>
                                    </td>
                                    <td class="form-item" style="width: 20%">
                                        手机：</td>
                                    <td>
                                        <asp:TextBox ID="TextBox1" runat="server" CssClass="input" Text='<%# Bind("HandleTelephone") %>'></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    
                                    <td class="form-item" style="width: 20%">
                                        MSN：</td>
                                    <td>
                                        <asp:TextBox ID="TextBox2" runat="server" CssClass="input" Text='<%# Bind("MSN") %>'></asp:TextBox>
                                    </td>
                                     <td class="form-item" style="width: 20%">
                                        QQ：</td>
                                    <td>
                                        <asp:TextBox ID="TextBox3" runat="server" CssClass="input" Text='<%# Bind("QQ") %>'></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                   
                                    <td class="form-item" style="width: 20%">
                                        Email：</td>
                                    <td>
                                        <asp:TextBox ID="TextBox4" runat="server" CssClass="input" Text='<%# Bind("Email") %>'></asp:TextBox>
                                    </td>
                                    <td class="form-item" style="width: 20%">
                                        紧急联系电话：</td>
                                    <td>
                                        <asp:TextBox ID="UrgePhone" runat="server" CssClass="input" Text='<%# Bind("UrgePhone") %>'></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                <td class="form-item" style="width: 20%">
                                        家庭住址：</td>
                                    <td colspan="3">
                                        <asp:TextBox ID="HomeTextBox" runat="server" CssClass="input" Text='<%# Bind("PrepField1") %>'></asp:TextBox>
                                    </td>
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
                                        用户名：</td>
                                    <td>
                                        <asp:TextBox ID="FileNameTextBox" runat="server" CssClass="input" Text='<%# Bind("UserCode") %>'></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="FileNameTextBox"
                                            ErrorMessage="*"></asp:RequiredFieldValidator></td>
                                             <td class="form-item" style="width: 20%">
                                        部门：</td>
                                    <td>
                                        <input id="txtUnit" runat="server" class="input" name="txtUnit" size="8" style="width: 72px;
                                            height: 18px" type="hidden" value='<%# Bind("UnitCode") %>' /><input id="txtUnitName"
                                                runat="server" class="input" name="txtUnit" style="width: 121px; height: 18px"
                                                type="text" /><img onclick="SelectUnit();return false;" src="../images/ToolsItemSearch.gif"
                                                    style="cursor: hand" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtUnitName"
                                            ErrorMessage="*"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                   
                                    <td class="form-item" style="width: 20%">
                                        电话：</td>
                                    <td>
                                        <asp:TextBox ID="TextBox5" runat="server" CssClass="input" Text='<%# Bind("Telephone") %>'></asp:TextBox>
                                    </td>
                                    <td class="form-item" style="width: 20%">
                                        手机：</td>
                                    <td>
                                        <asp:TextBox ID="TextBox1" runat="server" CssClass="input" Text='<%# Bind("HandleTelephone") %>'></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    
                                    <td class="form-item" style="width: 20%">
                                        MSN：</td>
                                    <td>
                                        <asp:TextBox ID="TextBox2" runat="server" CssClass="input" Text='<%# Bind("MSN") %>'></asp:TextBox>
                                    </td>
                                     <td class="form-item" style="width: 20%">
                                        QQ：</td>
                                    <td>
                                        <asp:TextBox ID="TextBox3" runat="server" CssClass="input" Text='<%# Bind("QQ") %>'></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                   
                                    <td class="form-item" style="width: 20%">
                                        Email：</td>
                                    <td>
                                        <asp:TextBox ID="TextBox4" runat="server" CssClass="input" Text='<%# Bind("Email") %>'></asp:TextBox>
                                    </td>
                                    <td class="form-item" style="width: 20%">
                                        紧急联系电话：</td>
                                    <td>
                                        <asp:TextBox ID="UrgePhone" runat="server" CssClass="input" Text='<%# Bind("UrgePhone") %>'></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                <td class="form-item" style="width: 20%">
                                        家庭住址：</td>
                                    <td colspan="3">
                                        <asp:TextBox ID="HomeTextBox" runat="server" CssClass="input" Text='<%# Bind("PrepField1") %>'></asp:TextBox>
                                    </td>
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
                                        <input id="btnClose" runat="server" class="button" name="btnClose" onclick="javascript:window.close();"
                                            type="button" value=" 关闭 " />
                                    </td>
                                </tr>
                            </table>
                            <table border="0" cellpadding="0" cellspacing="0" class="form" width="100%">
                                <tr>
                                 <td class="form-item" style="width: 20%">
                                        用户名：</td>
                                    <td>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("UserCode") %>'></asp:Label></td>
                                       <td class="form-item" style="width: 20%">
                                        部门：</td>
                                    <td>
                                        <asp:Label ID="UnitLabel" runat="server" Text='<%# Bind("UnitCode") %>'></asp:Label></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        电话：</td>
                                    <td>
                                        <asp:Label ID="CharterMemberLabel" runat="server" Text='<%# Bind("Telephone") %>'> '>
                                        </asp:Label>
                                    </td>
                                    <td class="form-item" style="width: 20%">
                                        手机：</td>
                                    <td>
                                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("HandleTelephone") %>'></asp:Label></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        MSN：</td>
                                    <td>
                                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("MSN") %>'></asp:Label></td>
                                                                            <td class="form-item" style="width: 20%">
                                        QQ：</td>
                                    <td>
                                        <asp:Label ID="ArchivesTypeLabel" runat="server" Text='<%# Bind("QQ") %>'></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        Email：</td>
                                    <td >
                                        <asp:Label ID="EmailLabel" runat="server" Text='<%# Bind("Email") %>'></asp:Label></td>
                                    <td class="form-item" style="width:20%">紧急联系电话：</td>
                                    <td>
                                    <asp:Label ID="UrgePhoneLabel" runat="server" Text='<%# Bind("UrgePhone") %>'></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                <td class="form-item">家庭住址：</td>
                                <td colspan="3">
                                <asp:Label runat="server" ID="HomeLabel" Text='<%# Bind("PrepField1") %>'></asp:Label>
                                </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:FormView>
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="RmsOA.MODEL.GK_OA_ComBookModel"
                        DeleteMethod="Delete" InsertMethod="Insert" SelectMethod="GetGK_OA_ComBookListOne"
                        TypeName="RmsOA.BFL.GK_OA_ComBookBFL" UpdateMethod="Update" OnInserted="ObjectDataSource1_Inserted">
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
