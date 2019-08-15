<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ZC_AssetTransferList.aspx.cs" Inherits="RmsOA_ZC_AssetTransferlist" %>

<%@ Register Src="../UserControls/inputunit.ascx" TagName="inputunit" TagPrefix="uc1" %>

<%@ Register Assembly="AspWebControl" Namespace="AspWebControl" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../Images/index.css" rel="stylesheet" type="text/css" />

    <script language="javascript" src="../Rms.js" type="text/javascript"></script>

    <title>固定资产/低耗品转移</title>
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
                <td background="../images/topic_bg.gif" class="topic">
                    <img align="absMiddle" height="25" src="../images/topic_li.jpg" width="35">固定资产/低耗品管理>资产转移
                </td>
                <td width="9">
                    <img height="25" src="../images/topic_corr.gif" width="9"></td>
            </tr>
        </table>
        <table cellpadding="0" cellspacing="0" width="100%" border="0">
            <tr>
                <td class="tools-area" valign="top">
                    <img align="absMiddle" src="../images/btn_li.gif">
                    <input id="NewButton" runat="server" class="button" onClick="OpenLargeWindow('ZC_AssetTransferEdit.aspx?Type=Add','MeetSummaryEdit')"
                        type="button" value="新增" />
                </td>
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
                     编号
                 </td>
                 <td>
                     <asp:TextBox ID="NumberTextBox" runat="server" CssClass="input"></asp:TextBox>
                 </td>
             </tr>
             <tr>
                 <td>
                     单位
                 </td>
                 <td>
                     <uc1:inputunit ID="DeptTextBox" runat="server" />
                 </td>
                 <td>
                     类别
                 </td>
                 <td>
                     <asp:DropDownList ID="SortDropDownList" runat="server" DataSourceID="ObjectDataSource2"
                         Font-Size="9pt">
                     </asp:DropDownList>
                 </td>
             </tr>
             <tr>
                 <td>
                     转移人
                 </td>
                 <td>
                     <asp:TextBox ID="PreUserTextBox" runat="server" CssClass="input" Width="50px"></asp:TextBox>
                 </td>
                 <td>
                     接收人
                 </td>
                 <td>
                     <asp:TextBox ID="PostUserTextBox" runat="server" CssClass="input" Width="50px"></asp:TextBox>
                 </td>
             </tr>
             <tr>
                 <td>
                     时间
                 </td>
                 <td>
                     <cc1:Calendar ID="StartDate" runat="server" CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                         Value="">
                     </cc1:Calendar>
                     &nbsp;到 &nbsp;
                     <cc1:Calendar ID="EndDate" runat="server" CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                         Value="">
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
                 <td colspan="4">
                     <input id="SearchButton" runat="server" class="submit" onserverclick="SearchButton_Click"
                         type="button" value="搜索" />
                 </td>
             </tr>
         </table>
        </td>
        </tr>

        </table>
    </div>
        <asp:GridView ID="GridView1" runat="server" Width="100%" CssClass="list" AutoGenerateColumns="False" DataSourceID="ObjectDataSource1" AllowPaging="True" PageSize="20">
        <HeaderStyle CssClass="list-title" HorizontalAlign="Center" />
            <Columns>
                <asp:TemplateField HeaderText="名称">
                    <ItemTemplate>
                        <a href="#" onClick="javascript:OpenLargeWindow('ZC_AssetTransferEdit.aspx?Code=<%# Eval("Code")%>','EvectionApply');return false;">
                            <%# Eval("Name")%>
                        </a>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:BoundField DataField="Number" HeaderText="编号" SortExpression="Number">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="NumUnit" HeaderText="数量/单位"
                    SortExpression="NumUnit" >
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Sort" HeaderText="类别"
                    SortExpression="Sort" >
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="PreUser" HeaderText="前使用人"
                    SortExpression="PreUser" >
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="PostUser" HeaderText="接受使用人"
                    SortExpression="PostUser" >
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="转移部门" HeaderStyle-HorizontalAlign="center">
                <ItemStyle HorizontalAlign="center" />
                <ItemTemplate>
                    <%# RmsPM.BLL.SystemRule.GetUnitName((string)Eval("PreDept"))%>
                </ItemTemplate>
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
        </asp:GridView>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" MaximumRowsParameterName="MaxRecords"
            SelectMethod="GetGK_OA_AssetTransferList" SortParameterName="SortColumns" StartRowIndexParameterName="StartRecord"
            TypeName="RmsOA.BFL.GK_OA_AssetTransferBFL" EnablePaging="True" OldValuesParameterFormatString="original_{0}">
            <SelectParameters>
                <asp:Parameter Name="SortColumns" Type="String" />
                <asp:Parameter DefaultValue="0" Name="StartRecord" Type="Int32" />
                <asp:Parameter DefaultValue="-1" Name="MaxRecords" Type="Int32" />
                <asp:Parameter DefaultValue="" Name="CodeEqual" Type="Int32" />
                <asp:ControlParameter ControlID="NameTextBox" Name="NameEqual" PropertyName="Text"
                    Type="String" />
                <asp:Parameter Name="SortEqual" Type="String" />
                <asp:ControlParameter ControlID="NumberTextBox" Name="NumberEqual" PropertyName="Text"
                    Type="String" />
                <asp:Parameter Name="NumUnitEqual" Type="String" />
                <asp:Parameter Name="ReasonEqual" Type="String" />
                <asp:ControlParameter ControlID="PreUserTextBox" Name="PreUserEqual" PropertyName="Text"
                    Type="String" />
                <asp:ControlParameter ControlID="DeptTextBox" Name="PreDeptEqual" PropertyName="Value"
                    Type="String" />
                <asp:ControlParameter ControlID="PostUserTextBox" Name="PostUserEqual" PropertyName="Text"
                    Type="String" />
                <asp:Parameter Name="PostDeptEqual" Type="String" />
                <asp:Parameter Name="QualityNOEqual" Type="String" />
                <asp:Parameter Name="SNRuleEqual" Type="String" />
                <asp:Parameter Name="SubmiterEqual" Type="String" />
                <asp:Parameter Name="SubTimeEqual" Type="DateTime" />
                <asp:Parameter Name="StatusEqual" Type="String" />
                <asp:Parameter Name="OriginalPriceEqual" Type="Decimal" />
                <asp:ControlParameter ControlID="StartDate" Name="StartEqual" PropertyName="Value"
                    Type="DateTime" />
                <asp:ControlParameter ControlID="EndDate" Name="EndEqual" PropertyName="Value"
                    Type="DateTime" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="GetSortType" TypeName="RmsOA.BFL.GK_OA_AssetTransferBFL"></asp:ObjectDataSource>
    </form>
</body>
</html>
