<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GK_OA_EquipmentMaintainApplyEdit.aspx.cs"
    Inherits="RmsOA_GK_OA_EquipmentMaintainApplyEdit" %>

<%@ Register Src="../WorkFlowControl/workflowtoolbar.ascx" TagName="workflowtoolbar"
    TagPrefix="uc5" %>
<%@ Register Src="../UserControls/inputunit.ascx" TagName="inputunit" TagPrefix="uc1" %>
<%@ Register Assembly="RmsPM.BLL" Namespace="RmsPM.BLL.ControlsLB" TagPrefix="cc2" %>
<%@ Register Src="../WorkFlowControl/WorkFlowList.ascx" TagName="WorkFlowList" TagPrefix="uc4" %>
<%@ Register Assembly="AspWebControl" Namespace="AspWebControl" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../Images/index.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript" src="../Rms.js"></script>

    <title>维修申请表</title>
</head>
<body>

    <script language="javascript" type="text/javascript">		
		function OpenRequisition()
        {
		    OpenFullWindow('<%= RmsPM.BLL.WorkFlowRule.GetProcedureURLByName("设备维护申请")%>?Code=<%= EquipmentFormView.DataKey.Value %>','设备维护申请');
        }
    </script>

    <form id="form1" runat="server">
        <div>
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td background="../images/topic_bg.gif" class="topic" style="height: 25px; text-align: center;">
                        维修申请表</td>
                </tr>
            </table>
        </div>
        <asp:FormView ID="EquipmentFormView" runat="server" Width="100%" DataSourceID="FormViewObjectDataSource"
            DataKeyNames="Code" OnItemDeleted="EquipmentFormView_ItemDeleted" OnItemInserted="EquipmentFormView_ItemInserted"
            OnItemInserting="EquipmentFormView_ItemInserting" OnItemUpdated="EquipmentFormView_ItemUpdated"
            OnDataBound="EquipmentFormView_DataBound" OnItemUpdating="EquipmentFormView_ItemUpdating">
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
                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="form">
                    <tr>
                        <td class="form-item" style="width: 120px;">
                            设备/设施名称：
                        </td>
                        <td>
                            <asp:TextBox ID="NameTextBox" runat="server" Text='<%# Bind("Name") %>' CssClass="input"></asp:TextBox><span
                                style="color: #ff0000">*<asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                                    runat="server" ControlToValidate="NameTextBox" ErrorMessage="不能为空"></asp:RequiredFieldValidator></span>
                        </td>
                        <td class="form-item" style="width: 100px;">
                            所属部门：
                        </td>
                        <td>
                            <uc1:inputunit ID="DeptTextBox" runat="server" Value='<%# Bind("Dept") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 120px;">
                            设备/设施型号：
                        </td>
                        <td>
                            <asp:TextBox ID="ModelNOTextBox" runat="server" Text='<%# Bind("ModelNO") %>' CssClass="input"></asp:TextBox>
                        </td>
                        <td class="form-item" style="width: 100px;">
                            类别：
                        </td>
                        <td>
                            <asp:DropDownList ID="TypeDropDownList" runat="server" DataSourceID="TypeObjectDataSource"
                                SelectedValue='<%# Bind("Type") %>' Font-Size="9pt">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 120px;">
                            设备/设施编号：
                        </td>
                        <td>
                            <asp:TextBox ID="NumberTextBox" runat="server" Text='<%# Bind("Number") %>' CssClass="input"></asp:TextBox>
                        </td>
                        <td class="form-item" style="width: 100px;">
                            预算金额(元)：
                        </td>
                        <td>
                            <asp:TextBox ID="BudgetMoneyTextBox" runat="server" Text='<%# Bind("BudgetMoney") %>'
                                CssClass="input"></asp:TextBox>
                            <span style="color: Red;">*</span>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="BudgetMoneyTextBox"
                                ErrorMessage="输入格式有误" ValidationExpression="^[0-9]\d+[.]?\d*$"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="BudgetMoneyTextBox"
                                ErrorMessage="不能为空"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 120px;">
                            经办人/申请人：
                        </td>
                        <td>
                            <asp:Label ID="UserCodeLabel" runat="server"></asp:Label>
                            <asp:HiddenField ID="UserHiddenField" runat="server" Value='<%# Bind("UserName") %>' />
                        </td>
                        <td class="form-item" style="width: 100px;">
                            申请日期：
                        </td>
                        <td>
                            <cc1:Calendar ID="ApplyDate" runat="server" CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                                Value='<%# Bind("ApplyDate") %>'>
                            </cc1:Calendar>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 120px;">
                            维修原因：
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="ReasonTextBox" runat="server" Text='<%# Bind("Reason") %>' CssClass="input"
                                Height="50px" TextMode="MultiLine" Width="80%"></asp:TextBox>
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
                        <td class="form-item" style="width: 120px;">
                            设备/设施名称：
                        </td>
                        <td style="width: 154px">
                            <asp:TextBox ID="NameTextBox" runat="server" CssClass="input" Text='<%# Bind("Name") %>'></asp:TextBox><span
                                style="color: #ff0000">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                                    runat="server" ControlToValidate="NameTextBox" ErrorMessage="不能为空"></asp:RequiredFieldValidator>&nbsp;
                        </td>
                        <td class="form-item" style="width: 100px;">
                            所属部门：
                        </td>
                        <td>
                            <uc1:inputunit ID="DeptTextBox" runat="server" Value='<%# Bind("Dept") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 120px;">
                            设备/设施型号：
                        </td>
                        <td style="width: 154px">
                            <asp:TextBox ID="ModelNOTextBox" runat="server" CssClass="input" Text='<%# Bind("ModelNO") %>'></asp:TextBox>
                        </td>
                        <td class="form-item" style="width: 100px;">
                            类别：
                        </td>
                        <td>
                            <asp:DropDownList ID="TypeDropDownList" runat="server" DataSourceID="TypeObjectDataSource" SelectedValue='<%# Bind("Type") %>'
                                Font-Size="9pt">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 120px;">
                            设备/设施编号：
                        </td>
                        <td style="width: 154px">
                            <asp:TextBox ID="NumberTextBox" runat="server" CssClass="input" Text='<%# Bind("Number") %>'></asp:TextBox>
                        </td>
                        <td class="form-item" style="width: 100px;">
                            预算金额(元)：
                        </td>
                        <td>
                            <asp:TextBox ID="BudgetMoneyTextBox" runat="server" CssClass="input" Text='<%# Bind("BudgetMoney") %>'></asp:TextBox>
                            <span style="color: Red;">*</span>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="BudgetMoneyTextBox"
                                ErrorMessage="输入格式有误" ValidationExpression="^[0-9]\d+[.]?\d*$"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="BudgetMoneyTextBox"
                                ErrorMessage="不能为空"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 120px;">
                            经办人/申请人：
                        </td>
                        <td style="width: 154px">
                            <asp:Label ID="UserCodeLabel" runat="server"></asp:Label>
                            <asp:HiddenField ID="UserHiddenField" runat="server" Value='<%# Bind("UserName") %>' />
                        </td>
                        <td class="form-item" style="width: 100px;">
                            申请日期：
                        </td>
                        <td>
                            <cc1:Calendar ID="ApplyDate" runat="server" CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                                Value='<%# Bind("ApplyDate") %>'>
                            </cc1:Calendar>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 120px;">
                            维修原因：
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="ReasonTextBox" runat="server" CssClass="input" Height="50px" Text='<%# Bind("Reason") %>'
                                TextMode="MultiLine" Width="80%"></asp:TextBox>
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
                            <input id="btnRequisition" runat="server" class="button" name="btnRequisition" onclick="javascript:OpenRequisition();"
                                type="button" value=" 提交 " />
                            <asp:Button ID="btnBankOut" runat="server" CssClass="button" OnClick="btnBankOut_Click"
                                Text=" 作废 " />
                            <input id="btnClose" runat="server" class="button" name="btnClose" onclick="javascript:window.close();"
                                type="button" value=" 关闭 " />
                        </td>
                    </tr>
                </table>
                <table border="0" cellpadding="0" cellspacing="0" class="form" width="100%">
                    <tr>
                        <td class="form-item" style="width: 120px;">
                            设备/设施名称：
                        </td>
                        <td>
                            <asp:Label ID="NameLabel" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                        </td>
                        <td class="form-item" style="width: 100px;">
                            所属部门：
                        </td>
                        <td>
                            <asp:Label ID="DeptLabel" runat="server" Text='<%# RmsPM.BLL.SystemRule.GetUnitName((string)Eval("Dept"))%>'></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 120px;">
                            设备/设施型号：
                        </td>
                        <td>
                            <asp:Label ID="ModelNOLabel" runat="server" Text='<%# Bind("ModelNO") %>'></asp:Label>
                        </td>
                        <td class="form-item" style="width: 100px;">
                            类别：
                        </td>
                        <td>
                            <asp:Label ID="TypeLabel" runat="server" Text='<%# Bind("Type") %>'></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 120px;">
                            设备/设施编号：
                        </td>
                        <td>
                            <asp:Label ID="NumberLabel" runat="server" Text='<%# Bind("Number") %>'></asp:Label>
                        </td>
                        <td class="form-item" style="width: 100px;">
                            预算金额(元)：
                        </td>
                        <td>
                            <asp:Label ID="BudgetMoneyLabel" runat="server" Text='<%# Bind("BudgetMoney") %>'>
                            </asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 120px;">
                            经办人/申请人：
                        </td>
                        <td>
                            <asp:Label ID="UserCodeLabel" runat="server"></asp:Label>
                            <asp:HiddenField ID="UserHiddenField" runat="server" Value='<%# Bind("UserName") %>' />
                        </td>
                        <td class="form-item" style="width: 100px;">
                            申请日期：
                        </td>
                        <td>
                            <asp:Label ID="ApplyDateLabel" runat="server" Text='<%# Bind("ApplyDate") %>'></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 120px;">
                            维修原因：
                        </td>
                        <td colspan="3">
                            <asp:Label ID="ReasonLabel" runat="server" Text='<%# Bind("Reason") %>'></asp:Label>
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
        <asp:ObjectDataSource ID="FormViewObjectDataSource" runat="server" SelectMethod="GetGK_OA_EquipmentMaintainApplyListOne"
            TypeName="RmsOA.BFL.GK_OA_EquipmentMaintainApplyBFL" OnInserted="FormViewObjectDataSource_Inserted"
            DataObjectTypeName="RmsOA.MODEL.GK_OA_EquipmentMaintainApplyModel" DeleteMethod="Delete"
            InsertMethod="Insert" UpdateMethod="Update">
            <SelectParameters>
                <asp:QueryStringParameter Name="Code" QueryStringField="Code" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="TypeObjectDataSource" runat="server" SelectMethod="GetEquipmentType"
            TypeName="RmsOA.BFL.GK_OA_EquipmentMaintainApplyBFL"></asp:ObjectDataSource>
    </form>
</body>
</html>
