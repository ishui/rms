<%@ Page Language="C#" AutoEventWireup="true" CodeFile="albumEntrance.aspx.cs" Inherits="albumEntrance" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0" bgColor="#e4eff6">
    <form id="form1" runat="server">
    <table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="topic" background="../images/topic_bg.gif"><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle">��Ŀ����>���̽���>
									ʵ����Ƭ</td>
								<td width="9"><IMG height="25" src="../images/topic_corr.gif" width="9"></td>
							</tr>
						</table>
    <div>
        ����Ŀ�����Ҫ��ʼ����������һ�����Ŀ¼����<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="�ύ" /><br />
        ��Ŀ¼����ҪΨһ����������Ŀͬ����<br />
        <asp:Label ID="Label1" runat="server" Text="."></asp:Label></div>
    </form>
</body>
</html>
