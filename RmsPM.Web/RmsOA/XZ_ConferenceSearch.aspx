<%@ Page Language="C#" MasterPageFile="~/RmsOA/XZ_ConferenceMasterPage.master"
    AutoEventWireup="true" CodeFile="XZ_ConferenceSearch.aspx.cs" Inherits="RmsOA_XZ_ConferenceSearch"
    Title="会议查询" %>

<%@ Register Src="../UserControls/inputuser.ascx" TagName="inputuser" TagPrefix="uc2" %>

<%@ Register Src="../UserControls/inputunit.ascx" TagName="inputunit" TagPrefix="uc1" %>
<%@ Register Assembly="AspWebControl" Namespace="AspWebControl" TagPrefix="cc1" %>
<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">

    <script src="../Rms.js" type="text/javascript"></script>

    <table class="search-area" width="100%">
        <tr>
            <td style="width: 120px;">
                会议主题：</td>
            <td style="width: 180px">
                <asp:TextBox ID="TextBoxTopic" runat="server" CssClass="input" Width="90%"></asp:TextBox></td>
            <td>
                会议发起人：</td>
            <td>
                <uc2:inputuser ID="InputuserChaterMember" runat="server" />
            </td>
        </tr>
        <tr>
            <td style="width: 120px;">
                部门：</td>
            <td style="width: 180px">
                <uc1:inputunit ID="InputDept" runat="server" />
            </td>
            <td style="width: 120px;">
                会议地点：</td>
            <td>
                <asp:DropDownList ID="MeetPlace" Font-Size="9pt" runat="server" DataSourceID="ObjectDataSource1" DataTextField="RoomCode" DataValueField="RoomCode">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td style="width: 120px;">
                会议日期：</td>
            <td colspan="3">
                <cc1:Calendar ID="dtStartDate" runat="server" CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                    Value="" ReadOnly="false">
                </cc1:Calendar>
                &nbsp;&nbsp;-->&nbsp;
                <cc1:Calendar ID="dtEndDate" runat="server" CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                    ReadOnly="false" Value="">
                </cc1:Calendar>
                &nbsp;&nbsp;
            </td>
            <td colspan="3">
                <asp:Button ID="ButtonSearch" runat="server" CssClass="submit" OnClick="searchButton_Click"
                    Text=" 搜索 " /></td>
        </tr>
    </table>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="list"
        Width="100%">
        <HeaderStyle CssClass="list-title" HorizontalAlign="center" />
        <FooterStyle CssClass="list-title" />
        <Columns>
            <asp:TemplateField HeaderText="会议主题" SortExpression="Topic">
                <ItemStyle HorizontalAlign="center" />
                <ItemTemplate>
                    <a href="#" onclick="OpenLargeWindow('XZ_Conference.aspx?Type=Read&Code=<%#Eval("Code")%>','FileTemplateAdd')">
                        <%#Eval("Topic")%>
                    </a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="会议发起人" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <%# RmsPM.BLL.SystemRule.GetUserName((string)Eval("ChaterMember"))%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="主办单位" ItemStyle-HorizontalAlign="center">
            <ItemTemplate>
             <%# RmsPM.BLL.SystemRule.GetUnitName((string)Eval("Dept"))%>
            </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Type" HeaderText="会议类型" ItemStyle-HorizontalAlign="center"
                SortExpression="Type" />
             <asp:TemplateField HeaderText="会议地点">
             <ItemStyle HorizontalAlign="center" />
             <ItemTemplate>
            <%# RmsOA.BFL.ConferenceUserListBFLFacade.GetMeetRoomName((string) Eval("Place"))%>
             </ItemTemplate>
             </asp:TemplateField>
            <asp:BoundField DataField="StartTime" HeaderText="会议开始时间" ItemStyle-HorizontalAlign="center"
                SortExpression="StartTime" />
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource runat="server" ID="ObjectDataSource1" TypeName="RmsOA.BFL.ConferenceUserListBFLFacade" SelectMethod="GetRoomList"></asp:ObjectDataSource>
</asp:Content>
