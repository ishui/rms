<%@ Register TagPrefix="uc1" TagName="WorkFlowMonitor" Src="WorkFlowMonitor.ascx" %>

<%@ Page Language="c#" Inherits="RmsPM.Web.WorkFlowControl.WorkFlowQuery" CodeFile="WorkFlowQuery.aspx.cs" %>

<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Register TagPrefix="uc1" TagName="InputUser" Src="../UserControls/InputUser.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>�Ѱ�����</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="../Images/index.css" type="text/css" rel="stylesheet">

    <script language="javascript" src="../Rms.js"></script>

</head>
<body style="BORDER-RIGHT: 0px" scroll="no">
    <form id="Form1" method="post" runat="server">
        <table height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td bgcolor="#e4eff6" height="6">
                </td>
            </tr>
            <tr>
                <td height="25">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="topic" background="../images/topic_bg.gif">
                                <img height="25" src="../images/topic_li.jpg" width="35" align="absMiddle"><span
                                    id="spanTitle" runat="server"> ����&nbsp;�Ѱ�����</span></td>
                            <td width="9">
                                <img height="25" src="../images/topic_corr.gif" width="9"></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr height="1">
                <td valign="top">
                    <table class="search-area" cellspacing="0" cellpadding="0" border="0">
                        <tr>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            �������̣�</td>
                                        <td>
                                            <select id="sltProcedure" runat="server" name="sltProcedure">
                                                <option selected value="">---��������---</option>
                                            </select>
                                        </td>
                                        <td>
                                            �� &nbsp; &nbsp;&nbsp; Ŀ��</td>
                                        <td>
                                            <asp:DropDownList ID="DropDownProject" runat="server">
                                            </asp:DropDownList></td>
                                        <td align="center" colspan="2">
                                            <asp:CheckBox ID="CheckBox1" runat="server" Text="���̸���" Checked="True" />
                                            <asp:CheckBox ID="CheckBox2" runat="server" Text="���̼��" Checked="True" /></td>
                                        <td rowspan="4">
                                            <input class="submit" id="btnSearch" type="button" value="�� ��" name="btnSearch" runat="server"
                                                onserverclick="btnSearch_ServerClick">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            ��&nbsp; ˮ �ţ�</td>
                                        <td>
                                            <input class="input" type="text" runat="server" id="txtCaseCode" /></td>
                                        <td>
                                            �� &nbsp; &nbsp;&nbsp; ��</td>
                                        <td>
                                            <input class="input" type="text" runat="server" id="txtTaskName" /></td>
                                        <td>
                                            �� &nbsp; &nbsp;&nbsp; �⣺</td>
                                        <td>
                                            <input type="text" class="input" runat="server" id="txtTitle" /></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            �� &nbsp;�� �ˣ�</td>
                                        <td>
                                            <uc1:InputUser ID="ucPerson" runat="server"></uc1:InputUser>
                                        </td>
                                        <td>
                                            �������ڣ�</td>
                                        <td>
                                            <cc3:Calendar ID="DateStart" runat="server" CalendarResource="../Images/CalendarResource/"
                                                ReadOnly="False" Display="True" Value="">
                                            </cc3:Calendar>
                                        </td>
                                        <td align="center">
                                            --></td>
                                        <td>
                                            <cc3:Calendar ID="DateEnd" runat="server" CalendarResource="../Images/CalendarResource/"
                                                ReadOnly="False" Display="True" Value="">
                                            </cc3:Calendar>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            �� &nbsp;�� �ˣ�</td>
                                        <td>
                                            <uc1:InputUser ID="ucToPerson" runat="server"></uc1:InputUser>
                                        </td>
                                        <td>
                                            ǩ�����ڣ�</td>
                                        <td>
                                            <cc3:Calendar ID="CalendarStart" runat="server" CalendarResource="../Images/CalendarResource/"
                                                ReadOnly="False" Display="True" Value="">
                                            </cc3:Calendar>
                                        </td>
                                        <td align="center">
                                            --></td>
                                        <td>
                                            <cc3:Calendar ID="CalendarEnd" runat="server" CalendarResource="../Images/CalendarResource/"
                                                ReadOnly="False" Display="True" Value="">
                                            </cc3:Calendar>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr height="100%">
                <td class="table" valign="top">
                    <div style="overflow: auto; width: 100%; height: 100%">
                        <uc1:WorkFlowMonitor ID="WorkFlowMonitor1" runat="server"></uc1:WorkFlowMonitor>
                    </div>
                </td>
            </tr>
            <tr>
                <td height="12">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td background="../images/corl_bg.gif">
                                <img height="12" src="../images/corl.gif" width="12"></td>
                            <td width="12">
                                <img height="12" src="../images/corr.gif" width="12"></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td bgcolor="#e4eff6" style="height: 6px">
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
