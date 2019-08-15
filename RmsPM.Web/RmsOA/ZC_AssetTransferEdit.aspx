<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ZC_AssetTransferEdit.aspx.cs"
    Inherits="RmsOA_ZC_AssetTransfer" %>

<%@ Register Src="../WorkFlowControl/workflowtoolbar.ascx" TagName="workflowtoolbar"
    TagPrefix="uc5" %>
<%@ Register Src="../WorkFlowControl/WorkFlowList.ascx" TagName="WorkFlowList" TagPrefix="uc4" %>
<%@ Register Src="../UserControls/inputunit.ascx" TagName="inputunit" TagPrefix="uc1" %>
<%@ Register Assembly="AspWebControl" Namespace="AspWebControl" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../Images/index.css" rel="stylesheet" type="text/css" />

    <script language="javascript" src="../Rms.js" type="text/javascript"></script>

    <title>固定资产/低耗品转移清单</title>
</head>
<body>

    <script language="javascript" type="text/javascript">		
		function OpenRequisition()
        {
		    OpenFullWindow('<%= RmsPM.BLL.WorkFlowRule.GetProcedureURLByName("固定资产转移")%>?Code=<%= FormView1.DataKey.Value %>&ProjectCode=<%= Request["ProjectCode"] + ""%>','固定资产转移');
        }
    </script>

    <form id="form1" runat="server">
        <div>
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td background="../images/topic_bg.gif" class="topic" style="height: 25px; text-align: center;">
                        固定资产/低耗品转移</td>
                </tr>
            </table>
        </div>
        <asp:FormView ID="FormView1" runat="server" DataSourceID="ObjectDataSource1" Width="100%"
            OnDataBound="FormView1_DataBound" DataKeyNames="Code" OnItemDeleted="FormView1_ItemDeleted" OnItemInserted="FormView1_ItemInserted" OnItemInserting="FormView1_ItemInserting" OnItemUpdated="FormView1_ItemUpdated" OnItemUpdating="FormView1_ItemUpdating">
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
                            名称：
                        </td>
                        <td>
                            <asp:TextBox ID="NameTextBox" runat="server" CssClass="input" Text='<%# Bind("Name") %>'></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="NameTextBox"
                                ErrorMessage="*"></asp:RequiredFieldValidator></td>
                        <td class="form-item" style="width: 100px;">
                            类别：
                        </td>
                        <td>
                            <asp:DropDownList ID="TypeDropDownList" runat="server" DataSourceID="DropDownListObjectDataSource"
                                Font-Size="9pt" SelectedValue='<%# Bind("Sort") %>'>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="TypeDropDownList"
                                ErrorMessage="*"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            编号：
                        </td>
                        <td>
                            <asp:TextBox ID="NumberTextBox" runat="server" CssClass="input" Text='<%# Bind("Number") %>'></asp:TextBox>
                        </td>
                        <td class="form-item" style="width: 100px;">
                            数量/单位 ：
                        </td>
                        <td>
                            <asp:TextBox ID="NumUnitTextBox" runat="server" CssClass="input" Text='<%# Bind("NumUnit") %>'></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            原始单价：
                        </td>
                        <td>
                            <asp:TextBox ID="OriginalPriceTextBox" runat="server" CssClass="input" Text='<%# Bind("OriginalPrice") %>'></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="OriginalPriceTextBox"
                                ErrorMessage="输入格式有误" ValidationExpression="^[0-9]\d*[.]?\d*$"></asp:RegularExpressionValidator></td>
                        <td class="form-item" style="width: 100px;">
                            使用部门
                        </td>
                        <td>
                            <uc1:inputunit ID="Inputunit1" runat="server" Value='<%# Bind("PreDept") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            前使用人：
                        </td>
                        <td>
                            <asp:TextBox ID="PreUserTextBox" runat="server" CssClass="input" Text='<%# Bind("PreUser") %>'></asp:TextBox>
                        </td>
                        <td class="form-item" style="width: 100px;">
                            后使用人：
                        </td>
                        <td>
                            <asp:TextBox ID="LaterUserTextBox" runat="server" CssClass="input" Text='<%# Bind("PostUser") %>'></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="LaterUserTextBox"
                                ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            变更原因：
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="ReasonTextBox" runat="server" CssClass="input" Height="50px" Text='<%# Bind("Reason") %>'
                                TextMode="MultiLine" Width="80%"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ReasonTextBox"
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
                        <td class="form-item" style="width: 100px;">
                            名称：
                        </td>
                        <td>
                            <asp:TextBox ID="NameTextBox" runat="server" CssClass="input" Text='<%# Bind("Name") %>'></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="NameTextBox"
                                ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                        <td class="form-item" style="width: 100px;">
                            类别：
                        </td>
                        <td>
                            <asp:DropDownList ID="TypeDropDownList" runat="server" DataSourceID="DropDownListObjectDataSource"
                                Font-Size="9pt" SelectedValue='<%# Bind("Sort") %>'>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TypeDropDownList"
                                ErrorMessage="*"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            编号：
                        </td>
                        <td>
                            <asp:TextBox ID="NumberTextBox" runat="server" CssClass="input" Text='<%# Bind("Number") %>'></asp:TextBox>
                        </td>
                        <td class="form-item" style="width: 100px;">
                            数量/单位 ：
                        </td>
                        <td>
                            <asp:TextBox ID="NumUnitTextBox" runat="server" CssClass="input" Text='<%# Bind("NumUnit") %>'></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            原始单价：
                        </td>
                        <td>
                            <asp:TextBox ID="OriginalPriceTextBox" runat="server" CssClass="input" Text='<%# Bind("OriginalPrice") %>'></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="OriginalPriceTextBox"
                                ErrorMessage="输入格式有误" ValidationExpression="^[0-9]\d*[.]?\d*$"></asp:RegularExpressionValidator></td>
                        <td class="form-item" style="width: 100px;">
                            使用部门
                        </td>
                        <td>
                            <uc1:inputunit ID="Inputunit1" runat="server" Value='<%# Bind("PreDept") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            前使用人：
                        </td>
                        <td>
                            <asp:TextBox ID="PreUserTextBox" runat="server" CssClass="input" Text='<%# Bind("PreUser") %>'></asp:TextBox>
                        </td>
                        <td class="form-item" style="width: 100px;">
                            后使用人：
                        </td>
                        <td>
                            <asp:TextBox ID="LaterUserTextBox" runat="server" CssClass="input" Text='<%# Bind("PostUser") %>'></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="LaterUserTextBox"
                                ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            变更原因：
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="ReasonTextBox" runat="server" CssClass="input" Height="50px" Text='<%# Bind("Reason") %>'
                                TextMode="MultiLine" Width="80%"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ReasonTextBox"
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
                            <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" CssClass="button" Text=" 删除 " />
                            <input id="btnRequisition" runat="server" class="button" name="btnRequisition" onclick="javascript:OpenRequisition();return false;"
                                type="button" value=" 提交 ">
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
                            名称：
                        </td>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                        </td>
                        <td class="form-item" style="width: 100px;">
                            类别：
                        </td>
                        <td>
                            <asp:Label ID="TypeLabel" runat="server" Text='<%# Bind("Sort") %>'></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            编号：
                        </td>
                        <td>
                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("Number") %>'></asp:Label>
                        </td>
                        <td class="form-item" style="width: 100px;">
                            数量/单位 ：
                        </td>
                        <td>
                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("NumUnit") %>'></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            原始单价：
                        </td>
                        <td>
                            <asp:Label ID="OriginalPriceLabel" runat="server" Text='<%# Bind("OriginalPrice") %>'>
                            </asp:Label><br />
                        </td>
                        <td class="form-item" style="width: 100px;">
                            使用部门：</td>
                        <td>
                            <asp:Label ID="Label12" runat="server" Text='<%# RmsPM.BLL.SystemRule.GetUnitName((string)Eval("PreDept"))%>'></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            前使用人：
                        </td>
                        <td>
                            <asp:Label ID="Label6" runat="server" Text='<%# Bind("PreUser") %>'></asp:Label>
                        </td>
                        <td class="form-item" style="width: 100px;">
                            后使用人：
                        </td>
                        <td>
                            <asp:Label ID="LaterUserLabel" runat="server" Text='<%# Bind("PostUser") %>'></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            变更原因：
                        </td>
                        <td colspan="3">
                            <asp:Label ID="Label5" runat="server" Text='<%# Bind("Reason") %>'></asp:Label>
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
                            <uc4:WorkFlowList ID="WorkFlowList1" runat="server"></uc4:WorkFlowList>
                        </td>
                    </tr>
                </table>
            </ItemTemplate>
        </asp:FormView>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetGK_OA_AssetTransferListOne"
            TypeName="RmsOA.BFL.GK_OA_AssetTransferBFL" OldValuesParameterFormatString="original_{0}"
            DataObjectTypeName="RmsOA.MODEL.GK_OA_AssetTransferModel" DeleteMethod="Delete"
            InsertMethod="Insert" UpdateMethod="Update" OnInserted="ObjectDataSource1_Inserted">
            <SelectParameters>
                <asp:QueryStringParameter Name="Code" QueryStringField="Code" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="DropDownListObjectDataSource" runat="server" SelectMethod="GetSortType"
            TypeName="RmsOA.BFL.GK_OA_AssetTransferBFL"></asp:ObjectDataSource>
    </form>
</body>
</html>
