<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ContractBill.aspx.cs" Inherits="Contract_ContractBill" %>

<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics.WebUI.WebDataInput.v5.1, Version=5.1.20051.37, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<link href="../Images/infra.css" type="text/css" rel="stylesheet" />

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>合同发票</title>
    <link href="../Images/index.css" type="text/css" rel="stylesheet" />

    <script type="text/javascript" language="javascript" src="../Rms.js"></script>

    <script type="text/javascript">
		
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="RmsPM.DAL.CommonWorkFlowDAL.ContractBillModel"
                DeleteMethod="Delete" InsertMethod="Insert" SelectMethod="GetContractBillListOne"
                TypeName="RmsPM.BLL.CommomWorkFlowBLL.ContractBillBFL" UpdateMethod="Update">
                <SelectParameters>
                    <asp:QueryStringParameter Name="Code" QueryStringField="Code" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </div>
        <asp:FormView ID="FormView1" runat="server" DataSourceID="ObjectDataSource1" Width="100%"
            OnItemDeleted="FormView1_ItemDeleted" OnItemInserted="FormView1_ItemInserted"
            OnItemUpdated="FormView1_ItemUpdated" DataKeyNames="Code" 
            OnItemInserting="FormView1_ItemInserting">
            <EditItemTemplate>
                <table align="center" border="0" cellpadding="0" cellspacing="0" class="form" width="100%">
                    <tr>
                        <td class="form-item" >
                            发票号</td>
                        <td >
                           <asp:TextBox ID="BillNoTextBox" runat="server" Text='<%# Bind("BillNo") %>' CssClass="input">
                            </asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                ControlToValidate="BillNoTextBox" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator></td>
                        <td class="form-item" >
                            金额</td>
                        <td >
                            <igtxt:WebNumericEdit ID="BillMoneyTextBox" runat="server" CssClass="infra-input-nember"
                                ImageDirectory="../images/infragistics/images/" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js"
                                JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js"
                                MinDecimalPlaces="Two" Width="100" ValueText='<%# Bind("BillMoney") %>' >
                            </igtxt:WebNumericEdit>
                        </td>
                    </tr>
                </table>
                <table align="center" border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr align="center">
                        <td>
                            <asp:Button ID="Button4" runat="server" CausesValidation="True" class="button" CommandName="Update"
                                Text="保存" />&nbsp;
                            <input id="Button5" class="button" onclick="javascript:window.close();" type="button"
                                value="关闭" /></td>
                    </tr>
                </table>
            </EditItemTemplate>
            <InsertItemTemplate>
                <table align="center" border="0" cellpadding="0" cellspacing="0" class="form" width="100%">
                    <tr>
                        <td class="form-item" >
                            发票号</td>
                        <td >
                           <asp:TextBox ID="BillNoTextBox" runat="server" Text='<%# Bind("BillNo") %>' CssClass="input">
                            </asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                ControlToValidate="BillNoTextBox" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                         </td>
                        <td class="form-item" >
                            金额</td>
                        <td >
                            <igtxt:WebNumericEdit ID="BillMoneyTextBox" runat="server" CssClass="infra-input-nember"
                                ImageDirectory="../images/infragistics/images/" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js"
                                JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js"
                                MinDecimalPlaces="Two"  Width="100" ValueText='<%# Bind("BillMoney") %>' >
                            </igtxt:WebNumericEdit>
                        </td>
                    </tr>
                </table>
                <table align="center" border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr align="center">
                        <td>
                            <asp:Button ID="Button6" runat="server" CausesValidation="True" class="button" CommandName="Insert"
                                Text="保存" />&nbsp;
                            <input id="Button7" class="button" onclick="javascript:window.close();" type="button"
                                value="关闭" /></td>
                    </tr>
                </table>
            </InsertItemTemplate>
            <ItemTemplate>
                <table align="center" border="0" cellpadding="0" cellspacing="0" class="form" width="100%">
                    <tr>
                        <td class="form-item" >
                            发票号</td>
                        <td >
                            <asp:Label ID="BillNoLabel" runat="server" Text='<%# Bind("BillNo") %>'></asp:Label></td>
                        <td class="form-item" >
                            金额</td>
                        <td >
                            <asp:Label ID="BillMoneyLabel" runat="server" Text='<%# Bind("BillMoney") %>'></asp:Label></td>
                    </tr>
                </table>
                <table align="center" border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr align="center">
                        <td>
                            <asp:Button ID="EditButton" runat="server" CausesValidation="False" class="button"
                                CommandName="Edit" Text="编辑" />&nbsp;
                            <asp:Button ID="DeleteButton" runat="server" CausesValidation="False" class="button"
                                CommandName="Delete" Text="删除" />&nbsp;
                            <input id="Button1" type="button" value="关闭" onclick="javascript:window.close();"
                                class="button" /></td>
                    </tr>
                </table>
            </ItemTemplate>
        </asp:FormView>
    </form>
</body>
</html>
