<%@ Page Language="C#" AutoEventWireup="true" CodeFile="XZ_WeekMeetShow.aspx.cs"
    Inherits="RmsOA_XZ_WeekMeetShow" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>���ܻ���</title>
    <link href="../Images/index.css" type="text/css" rel="stylesheet" />

    <script type="text/javascript" language="javascript" src="../Rms.js"></script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td background="../images/topic_bg.gif" class="topic" style="height: 25px; text-align: center;">
                        �������>���ܻ����б�</td>
                </tr>
            </table>
        </div>
        <div>
            <div style="text-align: center; font-size: 9pt;">
                <span style="color: #FAA210; font-weight: bolder;">
                    <% WirteYear(); %>
                </span>���<span style="color: #FAA210; font-weight: bolder;"><% WriteWeekIndex(); %></span>�ܻ��鰲�ţ�<span
                    style="color: #FAA210; font-weight: bolder;"><%WriteWeekTime(); %></span>��
            </div>
            <table class="table" width="100%">
                <tr>
                    <td class="tools-area" style="width: 16px;">
                        <img align="absMiddle" src="../images/btn_li.gif" /></td>
                    <td class="tools-area">
                        <asp:Button ID="WeekButton" runat="server" CssClass="button" Text="����" OnClick="WeekButton_Click" />
                        <asp:Button ID="PreButton" runat="server" CssClass="button" Text="����" OnClick="PreButton_Click"/>
                        <asp:Button ID="NextWeekButtom" runat="server" CssClass="button" Text="����" OnClick="NextWeekButtom_Click" />
                    </td>
                </tr>
            </table>
            <table width="100%" class="list">
                <thead>
                    <tr class="list-title">
                        <td>
                            ����</td>
                        <td>
                            ��������</td>
                        <td>
                            ���쵥λ</td>
                        <td>
                            ��������</td>
                        <td>
                            ����ص�</td>
                        <td>
                            ����ʱ��</td>
                    </tr>
                </thead>
                <tbody>
                    <% WriteMeetContent(); %>
                </tbody>
            </table>
        </div>
    </form>
</body>
</html>
