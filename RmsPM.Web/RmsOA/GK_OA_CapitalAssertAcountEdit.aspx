<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GK_OA_CapitalAssertAcountEdit.aspx.cs"
    Inherits="RmsOA_GK_OA_CapitalAssertAcount" %>
<%@ Register Src="../UserControls/inputunit.ascx" TagName="inputunit" TagPrefix="uc1" %>
<%@ Register Src="../WorkFlowControl/WorkFlowList.ascx" TagName="WorkFlowList" TagPrefix="uc4" %>
<%@ Register Assembly="AspWebControl" Namespace="AspWebControl" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <link href="../Images/index.css" rel="stylesheet" type="text/css" />
    <script language="javascript" src="../Rms.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">		
		function OpenRequisition()
        {
		    OpenFullWindow('<%= RmsPM.BLL.WorkFlowRule.GetProcedureURLByName("固定资产台帐")%>?FileChangeCode=<%= AccountFormView.DataKey.Value %>&ProjectCode=<%= Request["Code"] + ""%>','设备维护申请');
        }
    </script>
    <title>固定资产台帐表</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td background="../images/topic_bg.gif" class="topic" style="height: 25px; text-align: center;">
                        固定资产台帐</td>
                </tr>
            </table>
        </div>
        <asp:FormView ID="AccountFormView" runat="server" Width="100%" DataSourceID="FormViewObjectDataSource"
            OnItemDeleted="AccountFormView_ItemDeleted" OnItemInserted="AccountFormView_ItemInserted"
            OnItemInserting="AccountFormView_ItemInserting" OnItemUpdated="AccountFormView_ItemUpdated"
            OnDataBound="AccountFormView_DataBound" DataKeyNames="Code" OnItemUpdating="AccountFormView_ItemUpdating">
            <EditItemTemplate>
                <table id="Table2" class="table" width="100%">
                    <tr>
                        <td class="tools-area" width="16">
                            <img align="absMiddle" src="../images/btn_li.gif" alt="" /></td>
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
                            <asp:TextBox ID="NameTextBox" runat="server" Text='<%# Bind("Name") %>' CssClass="input"></asp:TextBox><span
                                style="color: Red;">*<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                    ControlToValidate="NameTextBox" ErrorMessage="不能为空"></asp:RequiredFieldValidator></span>
                        </td>
                        <td class="form-item" style="width: 100px;">
                            类别：
                        </td>
                        <td>
                            <asp:DropDownList ID="TypeDropDownList" runat="server" DataSourceID="DropDownListObjectDataSource"
                                SelectedValue='<%# Bind("Type") %>'>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TypeDropDownList"
                                ErrorMessage="*"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            编号：
                        </td>
                        <td>
                            <asp:TextBox ID="NumberTextBox" runat="server" Text='<%# Bind("Number") %>' CssClass="input"></asp:TextBox>
                        </td>
                        <td class="form-item" style="width: 100px;">
                            购买日期：
                        </td>
                        <td>
                            <cc1:Calendar ID="StartDate" runat="server" CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                                Display="True" ReadOnly="False" Value='<%# Bind("Buydate") %>'>
                            </cc1:Calendar>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            购买数量：
                        </td>
                        <td>
                            <asp:TextBox ID="BuyCountTextBox" runat="server" Text='<%# Bind("BuyCount") %>' CssClass="input"
                                Width="50px"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="BuyCountTextBox"
                                ErrorMessage="输入必须是数字格式" ValidationExpression="^\d*"></asp:RegularExpressionValidator></td>
                        <td class="form-item" style="width: 100px;">
                            单位：
                        </td>
                        <td>
                            <asp:TextBox ID="UnitTextBox" runat="server" Text='<%# Bind("Unit") %>' CssClass="input"
                                Width="30px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            金额：
                        </td>
                        <td>
                            <asp:TextBox ID="PriceTextBox" runat="server" Text='<%# Bind("Price") %>' CssClass="input"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="PriceTextBox"
                                ErrorMessage="输入格式不合法" ValidationExpression="^[0-9]\d*[.]?\d*$"></asp:RegularExpressionValidator></td>
                        <td class="form-item" style="width: 100px;">
                            使用部门：
                        </td>
                        <td>
                            <uc1:inputunit ID="DeptTextBox" runat="server" Value='<%# Bind("Dept") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            使用人：
                        </td>
                        <td>
                            <asp:TextBox ID="UserNameTextBox" runat="server" Text='<%# Bind("UserName") %>' CssClass="input"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="UserNameTextBox"
                                ErrorMessage="*"></asp:RequiredFieldValidator></td>
                        <td class="form-item" style="width: 100px;">
                            存放地点：
                        </td>
                        <td>
                            <asp:TextBox ID="PlaceTextBox" runat="server" Text='<%# Bind("Place") %>' CssClass="input"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="PlaceTextBox"
                                ErrorMessage="*"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            变更情况：</td>
                        <td colspan="3">
                            <asp:TextBox ID="TransferRecordTextBox" runat="server" Text='<%# Bind("TransferRecord") %>'
                                CssClass="input" Height="50px" TextMode="MultiLine" Width="78%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            备注：</td>
                        <td colspan="3">
                            <asp:TextBox ID="RemarkTextBox" runat="server" Text='<%# Bind("Remark") %>' CssClass="input"
                                Height="50px" Width="78%" TextMode="MultiLine"></asp:TextBox>
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
                        <td class="form-item" style="width: 100px;">
                            名称：
                        </td>
                        <td>
                            <asp:TextBox ID="NameTextBox" runat="server" CssClass="input" Text='<%# Bind("Name") %>'></asp:TextBox><span
                                style="color: Red;"><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                    ControlToValidate="NameTextBox" ErrorMessage="*"></asp:RequiredFieldValidator></span>
                        </td>
                        <td class="form-item" style="width: 100px;">
                            类别：
                        </td>
                        <td>
                            <asp:DropDownList ID="TypeDropDownList" runat="server" DataSourceID="DropDownListObjectDataSource"
                                SelectedValue='<%# Bind("Type") %>'>
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
                            购买日期：
                        </td>
                        <td>
                            <cc1:Calendar ID="StartDate" runat="server" CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                                Display="True" ReadOnly="False" Value='<%# Bind("Buydate") %>'>
                            </cc1:Calendar>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            购买数量：
                        </td>
                        <td>
                            <asp:TextBox ID="BuyCountTextBox" runat="server" CssClass="input" Text='<%# Bind("BuyCount") %>'
                                Width="50px"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="BuyCountTextBox"
                                ErrorMessage="输入必须是数字格式" ValidationExpression="^\d*"></asp:RegularExpressionValidator></td>
                        <td class="form-item" style="width: 100px;">
                            单位：
                        </td>
                        <td>
                            <asp:TextBox ID="UnitTextBox" runat="server" CssClass="input" Text='<%# Bind("Unit") %>'
                                Width="30px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            金额：
                        </td>
                        <td>
                            <asp:TextBox ID="PriceTextBox" runat="server" CssClass="input" Text='<%# Bind("Price") %>'></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="PriceTextBox"
                                ErrorMessage="输入格式不合法" ValidationExpression="^[0-9]\d*[.]?\d*$"></asp:RegularExpressionValidator></td>
                        <td class="form-item" style="width: 100px;">
                            使用部门：
                        </td>
                        <td>
                            <uc1:inputunit ID="DeptTextBox" runat="server" Value='<%# Bind("Dept") %>' />
                            </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            使用人：
                        </td>
                        <td>
                            <asp:TextBox ID="UserNameTextBox" runat="server" CssClass="input" Text='<%# Bind("UserName") %>'></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="UserNameTextBox"
                                ErrorMessage="*"></asp:RequiredFieldValidator></td>
                        <td class="form-item" style="width: 100px;">
                            存放地点：
                        </td>
                        <td>
                            <asp:TextBox ID="PlaceTextBox" runat="server" CssClass="input" Text='<%# Bind("Place") %>'></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="PlaceTextBox"
                                ErrorMessage="*"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            变更情况：</td>
                        <td colspan="3">
                            <asp:TextBox ID="TransferRecordTextBox" runat="server" CssClass="input" Height="50px"
                                Text='<%# Bind("TransferRecord") %>' TextMode="MultiLine" Width="78%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            备注：</td>
                        <td colspan="3">
                            <asp:TextBox ID="RemarkTextBox" runat="server" CssClass="input" Height="50px" Text='<%# Bind("Remark") %>'
                                TextMode="MultiLine" Width="78%"></asp:TextBox>
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
                            <input id="btnRequisition" runat="server" class="button" name="btnRequisition" onclick="javascript:OpenRequisition();return false;"
                                type="button" value=" 提交 " />
                            <asp:Button ID="btnBankOut" runat="server" CssClass="button" OnClick="btnBankOut_Click"
                                Text=" 作废 " />
                            <input id="btnClose" runat="server" class="button" name="btnClose" onclick="javascript:window.close();"
                                type="button" value=" 关闭 " />
                        </td>
                    </tr>
                </table>
                <table border="0" width="100%" cellpadding="0" cellspacing="0" class="form">
                    <tr>
                        <td style="width: 100px;" class="form-item">
                            名称：
                        </td>
                        <td>
                            <asp:Label ID="NameLabel" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                        </td>
                        <td class="form-item" style="width: 100px;">
                            类别：
                        </td>
                        <td>
                            <asp:Label ID="TypeLabel" runat="server" Text='<%# Bind("Type") %>'></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            编号：
                        </td>
                        <td>
                            <asp:Label ID="NumberLabel" runat="server" Text='<%# Bind("Number") %>'></asp:Label>
                        </td>
                        <td class="form-item" style="width: 100px;">
                            购买日期：
                        </td>
                        <td>
                            <asp:Label ID="BuydateLabel" runat="server" Text='<%# Bind("Buydate") %>'></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            购买数量：
                        </td>
                        <td>
                            <asp:Label ID="BuyCountLabel" runat="server" Text='<%# Bind("BuyCount") %>'></asp:Label>
                        </td>
                        <td class="form-item" style="width: 100px;">
                            单位：
                        </td>
                        <td>
                            <asp:Label ID="UnitLabel" runat="server" Text='<%# Bind("Unit") %>'></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            金额：
                        </td>
                        <td>
                            <asp:Label ID="PriceLabel" runat="server" Text='<%# Bind("Price") %>'></asp:Label>
                        </td>
                        <td class="form-item" style="width: 100px;">
                            使用部门：
                        </td>
                        <td>
                            <asp:Label ID="DeptLabel" runat="server" Text='<%# RmsPM.BLL.SystemRule.GetUnitName((string)Eval("Dept"))%>'></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            使用人：
                        </td>
                        <td>
                            <asp:Label ID="UserNameLabel" runat="server" Text='<%# Bind("UserName") %>'></asp:Label>
                        </td>
                        <td class="form-item" style="width: 100px;">
                            存放地点：
                        </td>
                        <td>
                            <asp:Label ID="PlaceLabel" runat="server" Text='<%# Bind("Place") %>'></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            变更情况：</td>
                        <td colspan="3">
                            <asp:Label ID="TransferRecordLabel" runat="server" Text='<%# Bind("TransferRecord") %>'>
                            </asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            备注：</td>
                        <td colspan="3">
                            <asp:Label ID="RemarkLabel" runat="server" Text='<%# Bind("Remark") %>'></asp:Label>
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
                            <uc4:workflowlist id="WorkFlowList1" runat="server"></uc4:workflowlist>
                        </td>
                    </tr>
                </table>
            </ItemTemplate>
        </asp:FormView>
        <asp:ObjectDataSource ID="FormViewObjectDataSource" runat="server" SelectMethod="GetGK_OA_CapitalAssertAcountListOne"
            TypeName="RmsOA.BFL.GK_OA_CapitalAssertAcountBFL" OnInserted="FormViewObjectDataSource_Inserted"
            DataObjectTypeName="RmsOA.MODEL.GK_OA_CapitalAssertAcountModel" DeleteMethod="Delete"
            InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" UpdateMethod="Update">
            <SelectParameters>
                <asp:QueryStringParameter Name="Code" QueryStringField="Code" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="DropDownListObjectDataSource" runat="server" SelectMethod="GetMaterialType"
            TypeName="RmsOA.BFL.GK_OA_CapitalAssertAcountBFL"></asp:ObjectDataSource>
        &nbsp;&nbsp;
        
    </form>
</body>
</html>
