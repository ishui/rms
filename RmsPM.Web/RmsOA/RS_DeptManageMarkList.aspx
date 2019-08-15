<%@ Page Language="C#" MasterPageFile="~/RmsOA/MarkerMasterPage.master" AutoEventWireup="true"
    CodeFile="RS_DeptManageMarkList.aspx.cs" Inherits="RmsOA_RS_DeptManageMark"
    Title="���ž�����" %>
<%@ MasterType VirtualPath="~/RmsOA/MarkerMasterPage.master" %>

<%@ Register Assembly="AspWebControl" Namespace="AspWebControl" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">

    <script language="javascript" src="../Rms.js" type="text/javascript"></script>

    <table id="tableToolBar" class="table" width="100%">
        <tr>
            <td class="tools-area" width="16">
                <img align="absMiddle" src="../images/btn_li.gif"></td>
            <td class="tools-area">
            <asp:Button ID="AddButton" runat="server" Text="����" CssClass="button" Enabled="false" />
            <asp:Button ID="ModifyButton" runat="server" CssClass="button" Text="�޸�" Enabled="false" />
            </td>
        </tr>
    </table>
    <table border="0" cellpadding="0" cellspacing="0" class="search-area">
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td>
                            ���ţ�
                        </td>
                        <td>
                            <asp:DropDownList ID="DeptDropDownList" runat="server" Font-Size="9pt">
                            </asp:DropDownList></td>
                        <td>
                            ���ڣ�
                        </td>
                        <td>
                            <cc1:Calendar ID="ScorceMonth" runat="server" CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                                Value="">
                            </cc1:Calendar>
                        </td>
                        <td align="center" rowspan="4">
                            <input id="SearchButton" runat="server" class="submit" name="button" onserverclick="Search_ServerClick"
                                type="button" value="����" /></td>
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
                            <img height="35" src="../Images/12-7-1.gif" width="40" alt="" /></td>
                        <td background="../Images/12-7-2.gif">
                            <span class="STYLE1"><span class="STYLE2">
                                <% GetYear(); %>
                            </span>��<span class="STYLE2"><% GetMonth(); %></span>�� �߿Ƽ��� <span class="STYLE2">
                                <% GetDept(); %>
                            </span>����Ա������</span></td>
                        <td width="15">
                            <img height="35" src="../Images/12-7-3.gif" width="15" alt="" /></td>
                    </tr>
                </table>
                <asp:GridView AutoGenerateColumns="false" ID="GridView1" runat="server" CssClass="list" OnRowDataBound="GridView1_RowDataBound"
                    Width="100%" EnableViewState="False">
                    <HeaderStyle CssClass="list-title" HorizontalAlign="Center" />
                    <RowStyle HorizontalAlign="Center" />
                    <FooterStyle CssClass="list-title" />
                    <Columns>
                        <asp:BoundField DataField="MarkerCode" HeaderText="MarkerCode" SortExpression="MarkerCode" />
                        <asp:BoundField DataField="DeptName" HeaderText="DeptName" SortExpression="DeptName" HeaderStyle-Width="40px" />
                        <asp:BoundField DataField="UserName" HeaderText="UserName" SortExpression="UserName" />
                        <asp:BoundField DataField="Score" HeaderText="Score" SortExpression="Score" />
                        <asp:BoundField DataField="Marker" HeaderStyle-Width="40px" HeaderText="Marker" SortExpression="Marker" />
                    </Columns>
                    <EmptyDataTemplate>
                    <span style=" font-weight:bolder;">�Բ���û�з�����Ҫ��ļ�¼��</span>
                    </EmptyDataTemplate>
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>
