<%@ Page language="c#" CodeFile="TestDesktop.aspx.cs" AutoEventWireup="false" Inherits="RmsPM.Web.Remind.TestDesktop" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>TestDesktop</title>
		<META http-equiv="Content-Type" content="text/html; charset=gb2312">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<script language=javascript src="../Rms.js"></script>
		<script language=javascript>
			function OpenNotice(Code)
			{
				OpenMiddleWindow("../Remind/NoticeInfo.aspx?Code=" + Code,"");
			}
			
			function OpenTask(HREF)
			{
				OpenFullWindow(HREF);
			}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table cellpadding="0" cellspacing="0" border="0" id="tbNotice" runat="server">
			</table>
			<table cellpadding="0" cellspacing="0" border="0" id="tbTaskRole" runat="server">
			</table>
			<table cellpadding="0" cellspacing="0" border="0" id="tbTaskStatus" runat="server">
			</table>
			<table cellpadding="0" cellspacing="0" border="0" id="tbTaskExceed" runat="server">
			</table>
		</form>
	</body>
</HTML>
