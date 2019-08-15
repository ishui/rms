<%@ Page Language="C#" AutoEventWireup="true" CodeFile="XZ_MeetRoomManageList.aspx.cs"
    Inherits="RmsOA_XZ_MeetRoomManageList" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../Images/index.css" type="text/css" rel="stylesheet" />

    <script type="text/javascript" language="javascript" src="../Rms.js"></script>

    <title>�����ҹ���</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td background="../images/topic_bg.gif" class="topic" style="height: 25px; text-align: center;">
                        �����칫>�����ҹ���</td>
                </tr>
                <tr>
                    <td class="tools-area" style="width: 100%;" valign="top">
                        <img align="absMiddle" src="../images/btn_li.gif">
                        <input class="button" runat="server" onclick="OpenMiddleWindow('XZ_MeetRoomEdit.aspx?Type=Add','')"
                            id="NewButton" type="button" value="����" />
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <asp:GridView runat="server" ID="successGridView" Width="100%" CssClass="list" ShowFooter="false"
                AutoGenerateColumns="False" DataSourceID="ObjectDataSource1">
                <HeaderStyle CssClass="list-title" HorizontalAlign="Center" Wrap="False" />
                <FooterStyle CssClass="list-title" HorizontalAlign="Center" Wrap="False" />
                <RowStyle HorizontalAlign="Center" />
                <Columns>
                    <asp:TemplateField HeaderText="������">
                        <ItemTemplate>
                            <a href="#" onclick="OpenMiddleWindow('XZ_MeetRoomEdit.aspx?Code=<%#Eval("Code")%>','')">
                                <%# Eval("RoomName")%>
                            </a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Place" HeaderText="�ص�" SortExpression="Place" />
                    <asp:BoundField DataField="HardCondition" HeaderText="Ӳ����ʩ" SortExpression="HardCondition" />
                    <asp:BoundField DataField="Capacity" HeaderText="����" SortExpression="Capacity" />
                    <asp:BoundField DataField="Remark" HeaderText="��ע" SortExpression="Remark" />
                </Columns>
            </asp:GridView>
        </div>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetMeetRoomList"
            TypeName="RmsOA.BFL.MeetRoomBFL">
            <SelectParameters>
                <asp:Parameter DefaultValue="Code" Name="SortColumns" Type="String" />
                <asp:Parameter DefaultValue="0" Name="StartRecord" Type="Int32" />
                <asp:Parameter DefaultValue="-1" Name="MaxRecords" Type="Int32" />
                <asp:Parameter Name="CodeEqual" Type="Int32" />
                <asp:Parameter Name="RoomNameEqual" Type="String" />
                <asp:Parameter Name="CapacityEqual" Type="String" />
                <asp:Parameter Name="PlaceEqual" Type="String" />
                <asp:Parameter Name="HardConditionEqual" Type="String" />
                <asp:Parameter Name="RemarkEqual" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </form>
</body>
</html>
