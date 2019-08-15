<%@ Page language="c#" Inherits="RmsPM.Web.Systems.SystemGroupFrame" CodeFile="SystemGroupFrame.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ProjectProgressFrame</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<SCRIPT language="javascript">
function winload()
{
	var FunctionStructureCode = '<%=Request["FunctionStructureCode"]%>';
	var objMain = document.all("frameMain");
	objMain.src = '../Systems/SystemGroupTree.aspx?FunctionStructureCode=' + FunctionStructureCode;
}
		</SCRIPT>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0" onload="winload();">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td bgColor="#e4eff6" height="6"></td>
				</tr>
				<tr>
					<td height="25">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="topic" background="../images/topic_bg.gif" nowrap><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle">系统管理 
									- 系统类别
								</td>
								<td width="9"><IMG height="25" src="../images/topic_corr.gif" width="9"></td>
							</tr>
						</table>
					</td>
				</tr>
				<TR height="100%">
					<td valign="top">
						<table border="0" cellpadding="0" cellspacing="0" width="100%" height="100%">
							<tr>
								<TD vAlign="top" align="left" width="250">
									<iframe name="frameMain" src='../Cost/LoadingPrepare.htm' frameBorder="no" width="100%"
										scrolling="no" height="100%" marginwidth="0" marginheight="0"></iframe>
								</TD>
								<td vAlign="top" align="left">
									<iframe name="frameRight" src='' frameBorder="no" width="100%" scrolling="no" height="100%"
										marginwidth="0" marginheight="0"></iframe>
								</td>
							</tr>
						</table>
					</td>
				</TR>
				<tr>
					<td height="12">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td background="../images/corl_bg.gif"><IMG height="12" src="../images/corl.gif" width="12"></td>
								<td width="12"><IMG height="12" src="../images/corr.gif" width="12"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td bgColor="#e4eff6" height="6"></td>
				</tr>
			</table>
			<input type="hidden" id="txtProjectCode" name="txtProjectCode" runat="server"><input type="hidden" id="txtFromUrl" name="txtFromUrl" runat="server">
			<input type="hidden" id="txtSrcGroupCode" name="txtSrcGroupCode" runat="server"><input type="hidden" id="txtSrcDesc" name="txtSrcDesc" runat="server">
			<input type="hidden" id="txtSrcClassCode" name="txtSrcClassCode" runat="server">
			<input type="hidden" id="txtIsCut" name="txtIsCut" runat="server">
		</form>
		<script language="javascript">
<!--

function Paste(dstClassCode, dstGroupCode)
{
	var src = document.all("txtSrcGroupCode").value;
}

//-->
		</script>
	</body>
</HTML>
