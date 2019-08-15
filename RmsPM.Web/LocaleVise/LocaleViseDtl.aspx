<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LocaleViseDtl.aspx.cs" Inherits="LocaleVise_LocaleViseDtl" %>
<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics.WebUI.WebDataInput.v5.1, Version=5.1.20051.37, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="uc1" TagName="InputCostBudgetDtl" Src="../UserControls/InputCostBudgetDtl.ascx" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>签证费用</title>
    <link href="../Images/index.css" type="text/css" rel="stylesheet">
    <link href="../Images/infra.css" rel="stylesheet" type="text/css" />

    <script language="javascript" src="../Rms.js"></script>

</head>
<body>
    <form id="form1" runat="server">
        <table height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td height="25">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="topic" align="center" background="../images/topic_bg.gif" height="25">
                                签证费用</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <asp:FormView ID="FormView1" runat="server" DataSourceID="ObjectDataSource1" OnItemInserting="FormView1_ItemInserting"
                        DefaultMode="Edit" Width="100%" DataKeyNames="ViseCostCode" OnItemDeleted="FormView1_ItemDeleted"
                        OnItemInserted="FormView1_ItemInserted" OnItemUpdated="FormView1_ItemUpdated" OnItemUpdating="FormView1_ItemUpdating">
                        <EditItemTemplate>
                            <table class="form" cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tr>
                                    <td class="form-item" nowrap style="width: 101px">
                                        金 额(元)：</td>
                                    <td>
                                    <igtxt:webnumericedit id="TxtTemMoney" runat="server" MinDecimalPlaces="Two" CssClass="infra-input-nember" 
																ImageDirectory="../images/infragistics/images/" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js"
																JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js" ValueText='<%# Bind("Money") %>'></igtxt:webnumericedit>&nbsp;<font color="red">*</font>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TxtTemMoney"
                                            ErrorMessage="RequiredFieldValidator">必填</asp:RequiredFieldValidator></td>
                                </tr>
                                <tr>
                                    <td class="form-item" nowrap style="width: 101px">
                                        费 用 项：</td>
                                    <td>
                                    <uc1:inputcostbudgetdtl ID="Inputcostbudgetdtl1" runat="server" CostCode='<%# Bind("CostCode") %>' CostBudgetSetCode='<%# Bind("CostBudgetSetCode") %>' PBSType='<%# Bind("PBSType") %>' PBSCode='<%# Bind("PBSCode") %>' ProjectCode='<%# Request["ProjectCode"]+"" %>' /> <font color="red">*</font>
                                    <span runat="server" id="CostMsgSpan"></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item" nowrap style="width: 101px">
                                        签证备注：</td>
                                    <td>
                                        <asp:TextBox ID="RemarkTextBox" Width="300" Height="50" runat="server" Text='<%# Bind("Remark") %>' TextMode="MultiLine"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center" height="50">
                                        <asp:Button ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update" CssClass="submit" 
                                            Text="更新"/>
                                        <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" CssClass="submit" 
                                            Text="删除"/>
                                        <input type="button" class="submit" value="取消" onclick="javascript:window.close();"/></td>
                                </tr>
                            </table>
                        </EditItemTemplate>
                        <InsertItemTemplate>
                            <table class="form" cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tr>
                                    <td class="form-item" nowrap style="width: 101px">
                                        金 额(元)：</td>
                                    <td>
                                    <igtxt:webnumericedit id="TxtTemMoney" runat="server" MinDecimalPlaces="Two" CssClass="infra-input-nember" 
																ImageDirectory="../images/infragistics/images/" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js"
																JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js" ValueText='<%# Bind("Money") %>'></igtxt:webnumericedit>&nbsp;<font color="red">*</font>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtTemMoney"
                                            ErrorMessage="RequiredFieldValidator">必填</asp:RequiredFieldValidator></td>
                                </tr>
                                <tr>
                                    <td class="form-item" nowrap style="width: 101px">
                                        费 用 项：</td>
                                    <td>
                                    <uc1:inputcostbudgetdtl ID="Inputcostbudgetdtl1" runat="server" CostCode='<%# Bind("CostCode") %>' CostBudgetSetCode='<%# Bind("CostBudgetSetCode") %>' PBSType='<%# Bind("PBSType") %>' PBSCode='<%# Bind("PBSCode") %>' ProjectCode='<%# Request["ProjectCode"]+"" %>' /> <font color="red">*</font>
                                    <span runat="server" id="CostMsgSpan"></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item" nowrap style="width: 101px">
                                        签证备注：</td>
                                    <td>
                                        <asp:TextBox ID="RemarkTextBox" Width="300" Height="50" runat="server" Text='<%# Bind("Remark") %>' TextMode="MultiLine"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center" height="50">
                                        <asp:Button ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert" CssClass="submit"
                                            Text="插入"/>
                                        
                                        <input type="button" class="submit" value="取消" onclick="javascript:window.close();"/></td>
                                </tr>
                            </table>
                        </InsertItemTemplate>
                    </asp:FormView>
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetLocalViseCost"
                        TypeName="RmsPM.BFL.LocaleViseBFL" DataObjectTypeName="TiannuoPM.MODEL.LocaleViseCostModel"
                        DeleteMethod="DeleteCost" InsertMethod="InsertCost" UpdateMethod="UpdateCost">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="Code" QueryStringField="ViseCostCode" Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </td>
            </tr>
            <tr>
                <td height="12">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td background="../images/corl_bg.gif">
                                <img height="12" src="../images/corl.gif" width="12"></td>
                            <td width="12">
                                <img height="12" src="../images/corr.gif" width="12"></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td height="6" bgcolor="#e4eff6">
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
