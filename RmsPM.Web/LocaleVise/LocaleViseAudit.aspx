<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LocaleViseAudit.aspx.cs" Inherits="LocaleVise_LocaleViseAudit" %>
<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics.WebUI.WebDataInput.v5.1, Version=5.1.20051.37, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>签证审核</title>
    <link href="../Images/index.css" type="text/css" rel="stylesheet">

    <script language="javascript" src="../Rms.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <table height="100%" cellspacing="0" cellpadding="0" width="100%" border="0" bgcolor="white">
            <tr>
                <td height="25">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="topic" align="center" background="../images/topic_bg.gif" height="25">
                                签证审核</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="middle" height="75" >
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="List"
                        DataSourceID="ObjectDataSource2" ShowFooter="True" Width="100%" DataKeyNames="ViseCostCode">
                        <Columns>
                            <asp:BoundField DataField="ViseCostCode" HeaderText="ViseCostCode" SortExpression="ViseCostCode"
                                Visible="False" />
                            <asp:TemplateField HeaderText="费用项" SortExpression="CostCode">
                                <ItemTemplate>
                                    <%# RmsPM.BLL.CostBudgetRule.GetCostBudgetSetName((string)Eval("CostBudgetSetCode")) %>
                                    &nbsp;
                                    <%# RmsPM.BLL.CBSRule.GetCostFullName((string)Eval("CostCode"))%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="金额(元)" SortExpression="Money">
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <%# RmsPM.BLL.MathRule.GetDecimalShowString(Eval("Money")) %>
                                </ItemTemplate>
                                <FooterStyle HorizontalAlign="Center" />
                                <FooterTemplate>
                                    合计：<%# RmsPM.BLL.MathRule.GetDecimalShowString(RmsPM.BFL.LocaleViseBFL.GetViseSumMoney(int.Parse(Request["ViseCode"].ToString()))) %>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="审批金额(元)">
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    
                                    <igtxt:webnumericedit id="TxtCheckMoney" runat="server" MinDecimalPlaces="Two" CssClass="input" 
																ImageDirectory="../images/infragistics/images/" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js"
																JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js" ValueText='0'></igtxt:webnumericedit>&nbsp;<font color="red">*</font>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TxtCheckMoney"
                                            ErrorMessage="RequiredFieldValidator">必填</asp:RequiredFieldValidator>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="备注" SortExpression="Remark">
                                <ItemTemplate>
                                    <%# Eval("Remark").ToString().Replace("\n","<br>") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerStyle CssClass="list-title" />
                        <HeaderStyle CssClass="list-title" />
                        <FooterStyle CssClass="list-title" />
                        <EmptyDataTemplate>
                            无

                        </EmptyDataTemplate>
                    </asp:GridView>
                    <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="GetLocalViseCosts"
                        TypeName="RmsPM.BFL.LocaleViseBFL">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="Code" QueryStringField="ViseCode" Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    &nbsp; &nbsp; &nbsp;
                    请审核签证“<%= RmsPM.BFL.LocaleViseBFL.GetViseName(int.Parse(Request["ViseCode"].ToString())) %>”！
                    <br />
                </td>
            </tr>
            <tr>
                <td height="50" align="center">
                    <asp:Button ID="btnPassAudit" runat="server" CssClass="submit" 
                        Text=" 通过 " OnClick="btnPassAudit_Click" />
                    <asp:Button ID="btnNoPassAudit" runat="server" CssClass="submit"
                        Text=" 作废 " OnClick="btnNoPassAudit_Click" /></td>
            </tr>
        </table>
    </form>
</body>
</html>
