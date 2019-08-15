<%@ Register TagPrefix="uc1" TagName="UCPicGroup" Src="../PicGroup/UCPicGroupLarge.ascx" %>
<%@ Page language="c#" Inherits="RmsPM.Web.ConstructProg.ProgressPic" CodeFile="ProgressPic.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ProgressPic</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<SCRIPT language="javascript">
		</SCRIPT>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0">
		<form id="Form1" method="post" runat="server">
		    <table border="0" width="100%" height="100%" cellpadding="0" cellspacing="0">
		        <tr>
		            <td><uc1:ucpicgroup id="UCPicGroup1" runat="server"></uc1:ucpicgroup></td>
		        </tr>
		    </table>
			<input type="hidden" id="txtProjectCode" name="txtProjectCode" runat="server">
		</form>
		<script language="javascript">
<!--
//-->
		</script>
	</body>
</HTML>
