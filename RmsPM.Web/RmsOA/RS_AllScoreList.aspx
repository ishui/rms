<%@ Page Language="C#" MasterPageFile="~/RmsOA/MarkerMasterPage.master" AutoEventWireup="true" CodeFile="RS_AllScoreList.aspx.cs" Inherits="RmsOA_RS_AllScoreList" Title="���̨��" %>

<%@ MasterType VirtualPath="~/RmsOA/MarkerMasterPage.master" %>
<%@ Register Assembly="AspWebControl" Namespace="AspWebControl" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" class="search-area">
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td style="width:60px;">
                            ���ڣ�
                        </td>
                        <td>
                            <cc1:calendar id="ScorceMonth" runat="server" calendarmode="Date" calendarresource="../Images/CalendarResource/"
                                value="">
                            </cc1:calendar>
                        </td>
                        <td align="center" rowspan="4" style="height: 45px">
                            <input id="SearchButton" runat="server" class="submit" name="button" onserverclick="SearchButton_ServerClick"
                                type="button" value="����" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
    </table>
        <tr>
        </tr>
            <td>
            </td>
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td width="40">
                            <img alt="" height="35" src="../Images/12-7-1.gif" width="40" /></td>
                        <td background="../Images/12-7-2.gif">
                            <span class="STYLE1"><span class="STYLE2">
                                <% GetYear(); %>
                            </span>��<span class="STYLE2"><% GetMonth(); %></span>�� �߿Ƽ����¶����˷���</span></td>
                        <td width="15">
                            <img height="35" src="../Images/12-7-3.gif" width="15" /></td>
                    </tr>
                </table>
<table border="0" cellpadding="0" cellspacing="0" width="100%" class="list">
<thead class="list-title" style="width:100%;">
 <tr>
 <td align="center">����</td><td align="center">���ŷ���</td><td align="center">Ա��</td><td align="center">Ա������</td><td align="center">Ա��������</td>
 </tr>
</thead>
<tfoot>
<tr>
<td colspan="5" class="list-title"></td>
</tr>
</tfoot>
<tbody style="height:auto;">
<% WriteTBody(); %>
</tbody>
</table>
</asp:Content>

