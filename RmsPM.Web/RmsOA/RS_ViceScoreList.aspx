<%@ Page Language="C#" MasterPageFile="~/RmsOA/MarkerMasterPage.master" AutoEventWireup="true"
    CodeFile="RS_ViceScoreList.aspx.cs" Inherits="RmsOA_RS_ViceScoreList" Title="副总经理打分" %>
 <%@ MasterType VirtualPath="~/RmsOA/MarkerMasterPage.master" %>
<%@ Register Assembly="AspWebControl" Namespace="AspWebControl" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="Server" ContentPlaceHolderID="ContentPlaceHolder">

    <script language="javascript" src="../Rms.js" type="text/javascript"></script>

    <table id="tableToolBar" class="table" width="100%">
        <tr>
            <td class="tools-area" width="16">
                <img align="absMiddle" src="../images/btn_li.gif"></td>
            <td class="tools-area">
                <asp:Button ID="AddButton" runat="server" CssClass="button" Text="新增"  />
                <asp:Button ID="ModifyButton" runat="server" CssClass="button" Text="修改"  />
            </td>
        </tr>
    </table>
    <table border="0" cellpadding="0" cellspacing="0" class="search-area">
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td>
                            日期：
                        </td>
                        <td >
                            <cc1:Calendar ID="ScorceMonth" runat="server" CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                                Value="">
                            </cc1:Calendar>
                        </td>
                        <td align="center" rowspan="4">
                            <input id="SearchButton" runat="server" class="submit" name="button" onserverclick="SearchButton_ServerClick"
                                type="button" value="搜索" /></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td width="40">
                            <img alt="" height="35" src="../Images/12-7-1.gif" width="40" /></td>
                        <td background="../Images/12-7-2.gif">
                            <span class="STYLE1"><span class="STYLE2">
                                <% GetYear(); %>
                            </span>年<span class="STYLE2"><% GetMonth(); %></span>月 高科集团部门经理分数</span></td>
                        <td width="15">
                            <img height="35" src="../Images/12-7-3.gif" width="15" /></td>
                    </tr>
                </table>
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="list"
                    OnRowDataBound="GridView1_RowDataBound" Width="100%">
                    <HeaderStyle CssClass="list-title" HorizontalAlign="Center" />
                    <RowStyle HorizontalAlign="Center" />
                    <FooterStyle CssClass="list-title" />
                    <Columns>
                        <asp:BoundField HeaderText="序号"/>
                        <asp:BoundField DataField="UserCode" HeaderText="姓名" SortExpression="UserCode" />
                        <asp:BoundField DataField="Score" HeaderText="考核分数" SortExpression="Score" />
                        <asp:BoundField DataField="MarkTime" HeaderText="MarkTime" SortExpression="MarkTime" Visible="false" />
                   </Columns>
                    <EmptyDataTemplate>
                        <span style="font-weight: bolder;">对不起，没有符合你要求的记录！</span>
                    </EmptyDataTemplate>
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>
