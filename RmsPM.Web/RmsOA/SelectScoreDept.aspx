<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectScoreDept.aspx.cs" Inherits="RmsOA_SelectScoreDept" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../Images/index.css" rel="stylesheet" type="text/css" />

    <script language="javascript" src="../Rms.js" type="text/javascript"></script>
<%--    <script language="javascript" type="text/javascript">
    </script>--%>

    <title>����ѡ��</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td background="../images/topic_bg.gif" class="topic" style="height: 25px; text-align: center;">
                        ѡ����Ҫ��ֵĲ���</td>
                </tr>
            </table>
        </div>
        <div style="width:100%; text-align:center;" class="list">
            <asp:RadioButtonList ID="DeptRadioButtonList" runat="server">
            </asp:RadioButtonList>
            <br />
            <input id="test" runat="server" class="button" onserverclick="ScoreButton_Click" type="button"
                value="���" />
           <%-- <asp:Button ID="ScoreButton" runat="server" CssClass="button" Text="���" OnClick="ScoreButton_Click" />--%>
            <input type="button" class="button" value="ȡ��" onclick="window.close();" />
            </div>
    </form>
</body>
</html>
