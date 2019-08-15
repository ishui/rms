<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IncomeBudget.aspx.cs" Inherits="IncomeBudget_IncomeBudget" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>收入计划</title>
    <link href="../Images/index.css" type="text/css" rel="stylesheet" />

    <script language="javascript" type="text/javascript" src="../Rms.js"></script>

</head>
<body>
    <form id="form1" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td background="../images/topic_bg.gif" class="topic" style="height: 25px; text-align: center;">
                    项目管理>财务管理>收入计划</td>
            </tr>
            <tr>
                <td class="tools-area" style="width: 100%;" valign="top">
                    <img align="absMiddle" src="../images/btn_li.gif" />
                    <asp:DropDownList runat="server" ID="ddlYear" Font-Size="9pt" AutoPostBack="True"
                        OnSelectedIndexChanged="ddlYear_SelectedIndexChanged" Width="80px" DataSourceID="SelectBoxObjectDataSource">
                    </asp:DropDownList>
                    <asp:Button runat="server" ID="btnAddYearIncome" CssClass="button" Text="添加新收入计划"
                        OnClick="btnAddYearIncome_Click" />
                </td>
            </tr>
        </table>
        <div>
            <asp:MultiView runat="server" ID="MVContainer">
                <asp:View runat="server" ID="MonthView">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td style="width: 40px;">
                                <img alt="" height="35" src="../Images/12-7-1.gif" width="40" /></td>
                            <td style="background-image: url(../Images/12-7-2.gif); text-align: center; font-size: 12px;
                                font-weight: bold; color: #FAA210;">
                                <%= Year %>
                                年收入预算
                            </td>
                            <td style="width: 15px;">
                                <img height="35" src="../Images/12-7-3.gif" width="15" alt="" /></td>
                        </tr>
                    </table>
                    <asp:GridView DataKeyNames="ID" runat="server" ID="MonthGridView" AutoGenerateColumns="False"
                        Width="100%" CssClass="list" OnRowDataBound="MonthGridView_RowDataBound" DataSourceID="GridViewObjectDataSource">
                        <HeaderStyle CssClass="list-title" HorizontalAlign="Center" />
                        <RowStyle HorizontalAlign="Center" />
                        <Columns>
                            <asp:TemplateField HeaderText="季度">
                                <ItemTemplate>
                                </ItemTemplate>
                                <ItemStyle Width="50px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="月份">
                                <ItemTemplate>
                                    <%#Eval("Month") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="收入金额">
                                <ItemTemplate>
                                    <asp:HiddenField runat="server" ID="hidSystemID" Value='<%#Bind("ID") %>' />
                                    <asp:HiddenField runat="server" ID="hidYear" Value='<%#Bind("Year") %>' />
                                    <asp:HiddenField runat="server" ID="hidMonth" Value='<%#Bind("Month") %>' />
                                    <%#Eval("Money") %>
                                    &nbsp;&nbsp;（万元,人民币）
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:HiddenField runat="server" ID="hidSystemID" Value='<%#Bind("ID") %>' />
                                    <asp:HiddenField runat="server" ID="hidYear" Value='<%#Bind("Year") %>' />
                                    <asp:HiddenField runat="server" ID="hidMonth" Value='<%#Bind("Month") %>' />
                                    <asp:HiddenField runat="server" ID="hidProjectCode" Value='<%#Bind("ProjectCode") %>' />
                                    <asp:TextBox CssClass="input" Font-Size="9pt" ID="tbxMoney" Text='<%# Bind("Money") %>'
                                        runat="server"></asp:TextBox>
                                    <asp:RegularExpressionValidator ControlToValidate="tbxMoney" ID="revtbxMoney" runat="server"
                                        ErrorMessage="输入的金钱格式不对" ValidationExpression="\d*(\.?\d)*"></asp:RegularExpressionValidator>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="操作">
                                <ItemTemplate>
                                    <asp:LinkButton CommandName="Edit" ID="btnEditGroup" runat="server">
                                    </asp:LinkButton>
                                    &nbsp;
                                    <asp:LinkButton ID="btnDelGroup" runat="server" OnClientClick="return window.confirm('确认删除吗？');"
                                        Text="删除" CommandName="Delete"></asp:LinkButton>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:LinkButton ID="btnUpdateGroup" runat="server" Text="保存" CommandName="Update"
                                        OnClientClick="return window.confirm('确认保存吗？');" />
                                    &nbsp;
                                    <asp:LinkButton CommandName="Cancel" ID="btnCancelGroup" runat="server" Text="取消" />
                                </EditItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <span style="width: 100%; text-align: center; font-size: 9pt; font-weight: bold;">对不起，本年度计划收入没有填写！
                            </span>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </asp:View>
                <asp:View runat="server" ID="YearView">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td style="width: 40px;">
                                <img alt="" height="35" src="../Images/12-7-1.gif" width="40" /></td>
                            <td style="background-image: url(../Images/12-7-2.gif); text-align: center; font-size: 12px;
                                font-weight: bold; color: #FAA210;">
                                <%= Year %>
                                年收入预算
                            </td>
                            <td style=" width:100px; background-image: url(../Images/12-7-2.gif);">
                                <asp:Button runat="server" ID="AddYearPlan" Text="保存收入计划" CssClass="button" OnClick="AddYearPlan_Click" />
                            </td>
                            <td style="width: 15px;">
                                <img height="35" src="../Images/12-7-3.gif" width="15" alt="" /></td>
                        </tr>
                    </table>
                    <asp:GridView runat="server" ID="YearGridView" AutoGenerateColumns="False" Width="100%"
                        CssClass="list" DataSourceID="YearGridViewObjectDataSource" OnRowDataBound="YearGridView_RowDataBound">
                        <HeaderStyle CssClass="list-title" HorizontalAlign="Center" />
                        <RowStyle HorizontalAlign="Center" />
                        <Columns>
                            <asp:TemplateField HeaderText="季度">
                                <ItemTemplate>
                                </ItemTemplate>
                                <ItemStyle Width="50px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="月份">
                                <ItemTemplate>
                                <asp:Label runat="server" ID="lblMonth" Font-Size="9pt" Text='<%#Bind("Month") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="收入金额">
                                <ItemTemplate>
                                    <asp:TextBox CssClass="input" Font-Size="9pt" ID="tbxMoney" Text='<%# Bind("Money") %>'
                                        runat="server"></asp:TextBox>&nbsp;&nbsp;（万元,人民币）
                                    <asp:RegularExpressionValidator ControlToValidate="tbxMoney" ID="revtbxMoney" runat="server"
                                        ErrorMessage="输入的金钱格式不对" ValidationExpression="\d*(\.?\d)*"></asp:RegularExpressionValidator>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </asp:View>
            </asp:MultiView>
            <asp:ObjectDataSource ID="GridViewObjectDataSource" TypeName="RmsPM.BLL.IncomeBugetFacade"
                runat="server" DataObjectTypeName="RmsPM.BLL.IncomeBugetModel" OldValuesParameterFormatString="original_{0}"
                SelectMethod="Select" UpdateMethod="Insert" DeleteMethod="Delete">
                <SelectParameters>
                    <asp:Parameter Name="year" Type="Int32" />
                    <asp:Parameter Name="projectCode" Type="String" />
                </SelectParameters>
                <DeleteParameters>
                    <asp:Parameter Name="id" Type="String" />
                </DeleteParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource TypeName="RmsPM.BLL.PageHelpDisplay" SelectMethod="GetYearRangeForSelect"
                ID="SelectBoxObjectDataSource" runat="server" OldValuesParameterFormatString="original_{0}">
                <SelectParameters>
                    <asp:Parameter DefaultValue="2005" Name="startYear" Type="Int32" />
                    <asp:Parameter DefaultValue="2018" Name="endYear" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="YearGridViewObjectDataSource" runat="server" TypeName="RmsPM.BLL.IncomeBugetFacade"
                OldValuesParameterFormatString="original_{0}" SelectMethod="SetForInsert">
                <SelectParameters>
                    <asp:Parameter Name="year" Type="Int32" />
                    <asp:Parameter Name="projectCode" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </div>
    </form>
</body>
</html>
