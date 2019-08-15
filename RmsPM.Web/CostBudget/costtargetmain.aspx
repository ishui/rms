<%@ Page language="c#" Inherits="RmsPM.Web.CostBudget.CostTargetMain" CodeFile="CostTargetMain.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>CostTargetMain</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<SCRIPT language="javascript">
function winload()
{
	var objMain = document.all("frameMain");
	
	var type = '<%=Request.QueryString["type"]%>';
	objMain.src = 'CostTargetSumTree.aspx?ProjectCode=' + Form1.txtProjectCode.value + "&type=" + type + "&TargetFlag=1";
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
								<td class="topic" background="../images/topic_bg.gif" nowrap><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle">项目管理>成本控制>目标费用汇总
								</td>
								<td width="9"><IMG height="25" src="../images/topic_corr.gif" width="9"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td class="tools-area" valign="top">
						<IMG src="../images/btn_li.gif" align="absMiddle"> <input class="button" type="button" value="切换到目标费用" name="btnGotoCostTargetList" onclick="GotoCostTargetList();">
					</td>
				</tr>
				<TR height="100%">
					<td valign="top" class="table">
						<table border="0" cellpadding="0" cellspacing="0" width="100%" height="100%">
							<tr>
								<TD vAlign="top" align="left">
									<iframe name="frameMain" src='../Cost/LoadingPrepare.htm' frameBorder="no" width="100%"
										scrolling="no" height="100%" marginwidth="0" marginheight="0"></iframe>
								</TD>
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
		</form>
		<script language="javascript">
<!--

//切换到预算表
function GotoCostTargetList()
{
	window.location.href = "CostTargetFrame.aspx?ProjectCode=" + Form1.txtProjectCode.value + "&TargetFlag=1";
//	window.location.href = "CostTargetList.aspx?ProjectCode=" + Form1.txtProjectCode.value + "&TargetFlag=1";
}

//-->
		</script>
	</body>
</HTML>
