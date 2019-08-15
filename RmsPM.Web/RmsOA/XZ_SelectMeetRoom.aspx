<%@ Page Language="C#" AutoEventWireup="true" CodeFile="XZ_SelectMeetRoom.aspx.cs"
    Inherits="RmsOA_XZ_SelectMeetRoom" %>
<%@ Register TagPrefix="cc1" Namespace="AspWebControl" Assembly="AspWebControl" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ѡ�������</title>
    <link href="../Images/index.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript" language="javascript" src="../Rms.js"></script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td background="../images/topic_bg.gif" class="topic" style="height: 25px; text-align: center;">
                        �������>�쿴������</td>
                </tr>
            </table>
        </div>
        <div style="width: 100%" class="search-area">
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="text-align: left;">
                        ��ʼʱ�䣺</td>
                    <td>
                        <cc1:Calendar ID="startDate" runat="server" CalendarMode="All" CalendarResource="../Images/CalendarResource/"
                            Value="">
                        </cc1:Calendar>
                    </td>
                    <td style="text-align: left;">
                        ����ʱ�䣺
                    </td>
                    <td>
                        <cc1:Calendar ID="endDate" runat="server" CalendarMode="All" CalendarResource="../Images/CalendarResource/"
                            Value="">
                        </cc1:Calendar>
                    </td>
                    <td style="text-align: right;">
                        <asp:Button ID="btnSearch" CssClass="submit" runat="server" Text="��ѯ" OnClick="btnSearch_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <asp:GridView DataKeyNames="RoomCode" runat="server" ID="successGridView" Width="100%" CssClass="list" AllowPaging="True"
                PageSize="20" AutoGenerateColumns="False" DataSourceID="ObjectDataSource1" AutoGenerateSelectButton="True" OnSelectedIndexChanged="successGridView_SelectedIndexChanged">
                <HeaderStyle CssClass="list-title" HorizontalAlign="Center" Wrap="False" />
                <FooterStyle CssClass="list-title" HorizontalAlign="Center" Wrap="False" />
                <RowStyle HorizontalAlign="Center" />
                <Columns>
                    <asp:TemplateField HeaderText="������">
                        <ItemTemplate>
                            <a href="#" onclick="OpenMiddleWindow('../RmsOA/XZ_MeetRoomEdit.aspx?Code=<%#Eval("RoomCode") %>','')">
                                <%# Eval("RoomName")%>
                            </a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="����������">                  
                    <ItemTemplate>
                    <%# Eval("Message")%>
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="TimeAge" HeaderText="����ʱ��" SortExpression="TimeAge" />
                </Columns>
            </asp:GridView>
            <asp:ObjectDataSource ID="ObjectDataSource1" TypeName="RmsOA.BFL.ConferenceUserListBFLFacade"
                runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetAllRoomUseStatus">
                <SelectParameters>
                    <asp:Parameter Name="begin" Type="String" />
                    <asp:Parameter Name="end" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </div>
    </form>
</body>
</html>
