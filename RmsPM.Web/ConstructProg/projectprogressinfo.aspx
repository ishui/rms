<%@ Page language="c#" Inherits="RmsPM.Web.ConstructProg.ProjectProgressInfo" CodeFile="ProjectProgressInfo.aspx.cs" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>进度图</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../Images/ContentMenu.js"></script>
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
	</HEAD>
	<body style="BORDER-RIGHT: 0px" scroll="no" onload="winload();">
		<form id="Form1" method="post" runat="server">
			<div style="DISPLAY: none">
			</div>
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr style="DISPLAY:none">
					<td class="tools-area" vAlign="top"><IMG src="../images/btn_li.gif" align="absMiddle">
					</td>
				</tr>
				<tr>
					<td class="table" vAlign="top">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<!--<TR>
								<TD class="note">当前位置：<asp:label id="lblNavigator" Runat="server"></asp:label></TD>
							</TR>-->
						</table>
					</td>
				</tr>
				<tr height="100%">
					<td class="table">
						<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td><iframe name="frameChart" marginWidth="0" marginHeight="0" src="" frameBorder="no" width="100%"
								scrolling="no" height="100%"></iframe></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server"> <input id="txtFromUrl" type="hidden" name="txtFromUrl" runat="server">
			<input id="txtWBSCode" type="hidden" name="txtWBSCode" runat="server">
		</form>
		<SCRIPT language="javascript">
<!--

var CurrUrl = window.location.href;

function winload()
{
	var type = '<%=Request.QueryString["type"]%>';
	var GanttType = '<%=Request.QueryString["GanttType"]%>';
	
	if (type == "")
	{
		document.all("frameChart").src = "ProjectProgressChartInfraMain.aspx?WBSCode=" + Form1.txtWBSCode.value + "&GanttType=" + GanttType;
	}
	else
	{
		document.all("frameChart").src = "ProjectProgressChart.aspx?WBSCode=" + Form1.txtWBSCode.value;
	}
}

function GoBack()
{
	window.location.href = Form1.txtFromUrl.value;
}

function GotoTask(WBSCode)
{
	var type = '<%=Request.QueryString["type"]%>';
	var GanttType = '<%=Request.QueryString["GanttType"]%>';
	window.location.href = "../ConstructProg/ProjectProgressInfo.aspx?ProjectCode=" + Form1.txtProjectCode.value + "&WBSCode=" + WBSCode + "&type=" + type + "&GanttType=" + GanttType;
}

//-->
		</SCRIPT>
	</body>
</HTML>
