<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GK_OA_CapitalAssertAcountList.aspx.cs"
    Inherits="RmsOA_GK_OA_CapitalAssertAcountList" %>

<%@ Register Src="../UserControls/inputunit.ascx" TagName="inputunit" TagPrefix="uc1" %>
<%@ Register Assembly="AspWebControl" Namespace="AspWebControl" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <link href="../Images/index.css" rel="stylesheet" type="text/css" />

    <script language="javascript" src="../Rms.js" type="text/javascript"></script>

    <title>固定资产台帐列表</title>
</head>
<body style="BORDER-RIGHT: 0px" scroll="no">
    <form id="form1" runat="server">
        <div>
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
              <tr>
                <td height="6" bgcolor="#e4eff6"></td>
              </tr>
            </table>
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td width="986" background="../images/topic_bg.gif" class="topic">
                        <img align="absMiddle" height="25" src="../images/topic_li.jpg" width="35">固定资产/低耗品管理>固定资产台帐                    </td>
                    <td width="9">
                        <img height="25" src="../images/topic_corr.gif" width="9"></td>
                </tr>
                <tr>
                    <td colspan="2" valign="top" class="tools-area" style="width: 100%;">
                        <img align="absMiddle" src="../images/btn_li.gif">
                        <input class="button" onClick="OpenLargeWindow('GK_OA_CapitalAssertAcountEdit.aspx?Type=Add','CapitalAssert')"
                            runat="server" id="NewButton" type="button" value="新增" />
                            <input class="button" onClick="OpenMiddleWindow('ZC_ImportAccout.aspx','')" type="button" value="导入资产台帐" />                    </td>
                </tr>
            </table>
            <table border="0" cellpadding="0" cellspacing="0" class="search-area" width="100%">
                <tr>
                    <td>
                        <table width="100%">
                            <tr>
                                <td>
                                    名称
                                </td>
                                <td>
                                    <asp:TextBox ID="NameTextBox" runat="server" CssClass="input"></asp:TextBox>
                                </td>
                                <td>
                                    类别
                                </td>
                                <td>
                                    <asp:DropDownList ID="TypeDropDownList" runat="server" DataSourceID="DropDownListObjectDataSource"
                                        Font-Size="9pt">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    使用部门
                                </td>
                                <td>
                                    <uc1:inputunit ID="Inputunit1" runat="server" />
                                </td>
                                <td>
                                    使用人
                                </td>
                                <td>
                                    <asp:TextBox ID="UserTextBox" runat="server" CssClass="input" Width="50px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    购买日期
                                </td>
                                <td>
                                    <cc1:Calendar ID="StartDate" runat="server" CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                                        Display="True" ReadOnly="False" Value="">
                                    </cc1:Calendar>
                                    到
                                    <cc1:Calendar ID="EndDate" runat="server" CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                                        Display="True" ReadOnly="False" Value="">
                                    </cc1:Calendar>
                                </td>
                                <td>
                                    状态</td>
                                <td>
                                    <asp:CheckBoxList ID="cblStatus" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Selected="True" Value="0">申请</asp:ListItem>
                                        <asp:ListItem Value="1">审核中</asp:ListItem>
                                        <asp:ListItem Value="2">以审</asp:ListItem>
                                        <asp:ListItem Value="3">未通过</asp:ListItem>
                                        <asp:ListItem Value="4">作废</asp:ListItem>
                                    </asp:CheckBoxList></td>
                                <td colspan="4" align="center">
                                    <input id="btnSearch" runat="server" class="submit" onserverclick="btSearch_Click"
                                        type="button" value="搜索" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <asp:GridView ID="AccountGridView" runat="server" AllowPaging="True" CssClass="list"
                HorizontalAlign="Center" Width="100%" AutoGenerateColumns="False" DataSourceID="GridViewObjectDataSource" PageSize="18">
                <HeaderStyle CssClass="list-title" />
                <Columns>
                    <asp:TemplateField HeaderText="名称">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <a href="#" onClick="javascript:OpenLargeWindow('GK_OA_CapitalAssertAcountEdit.aspx?Type=Read&Code=<%# Eval("Code")%>','EvectionApply');return false;">
                                <%# Eval("Name")%>
                            </a>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="Number" HeaderText="编号" SortExpression="Number" >
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Type" HeaderText="类别"
                        SortExpression="Type" >
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Buydate" HeaderText="购买日期" SortExpression="Buydate" >
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="数量/单位">
                        <ItemTemplate>
                            <%# Eval("BuyCount")%>
                            /
                            <%# Eval("Unit")%>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="UserName" HeaderText="使用者" SortExpression="UserName" >
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderStyle-HorizontalAlign="center" HeaderText="部门">
                    <ItemTemplate>
                        <%# RmsPM.BLL.SystemRule.GetUnitName((string)Eval("Dept"))%>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="状态" SortExpression="Status">
                        <ItemTemplate>
                            <%# RmsOA.BFL.GK_OA_FileChangeBFL.GetManpowerNeedStatusName((string)Eval("Status")) %>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="center" />
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    无匹配数据
                </EmptyDataTemplate>
                <PagerSettings FirstPageText="首 页" LastPageText="尾 页" Mode="NextPreviousFirstLast"
                    NextPageText="下一页" PageButtonCount="4" Position="Bottom" PreviousPageText="上一页"
                    Visible="true" />
            </asp:GridView>
            <asp:ObjectDataSource ID="GridViewObjectDataSource" runat="server" MaximumRowsParameterName="MaxRecords"
                OldValuesParameterFormatString="original_{0}" SelectMethod="GetGK_OA_CapitalAssertAcountList"
                SortParameterName="SortColumns" StartRowIndexParameterName="StartRecord" TypeName="RmsOA.BFL.GK_OA_CapitalAssertAcountBFL"
                EnablePaging="True">
                <SelectParameters>
                    <asp:Parameter Name="SortColumns" Type="String" />
                    <asp:Parameter DefaultValue="0" Name="StartRecord" Type="Int32" />
                    <asp:Parameter DefaultValue="-1" Name="MaxRecords" Type="Int32" />
                    <asp:Parameter Name="CodeEqual" Type="Int32" />
                    <asp:ControlParameter ControlID="NameTextBox" Name="NameEqual" PropertyName="Text"
                        Type="String" />
                    <asp:Parameter Name="TypeEqual" Type="String" />
                    <asp:Parameter Name="NumberEqual" Type="String" />
                    <asp:Parameter Name="BuydateEqual" Type="DateTime" />
                    <asp:Parameter Name="BuyCountEqual" Type="Int32" />
                    <asp:Parameter Name="PriceEqual" Type="Decimal" />
                    <asp:Parameter Name="UnitEqual" Type="String" />
                    <asp:ControlParameter ControlID="Inputunit1" Name="DeptEqual" PropertyName="Value"
                        Type="String" />
                    <asp:Parameter Name="TransferRecordEqual" Type="String" />
                    <asp:ControlParameter ControlID="UserTextBox" Name="UserNameEqual" PropertyName="Text"
                        Type="String" />
                    <asp:Parameter Name="PlaceEqual" Type="String" />
                    <asp:Parameter Name="RemarkEqual" Type="String" />
                    <asp:Parameter Name="QualityNOEqual" Type="String" />
                    <asp:Parameter Name="SNRuleEqual" Type="String" />
                    <asp:Parameter Name="StatusEqual" Type="String" />
                    <asp:ControlParameter ControlID="StartDate" Name="StartDay" PropertyName="Value"
                        Type="DateTime" />
                    <asp:ControlParameter ControlID="EndDate" Name="EndDay" PropertyName="Value" Type="DateTime" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="DropDownListObjectDataSource" runat="server" SelectMethod="GetMaterialType"
                TypeName="RmsOA.BFL.GK_OA_CapitalAssertAcountBFL"></asp:ObjectDataSource>
      </div>
    </form>
</body>
</html>
