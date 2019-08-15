<%@ Page language="c#" Inherits="RmsPM.Web.Construct.ProgressReport" CodeFile="ProgressReport.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ProgressReport</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<SCRIPT language="javascript">
function winload()
{
	var objLeft = document.all("frameLeft");
	objLeft.src = '../PBS/SelectPBSUnitWindow.aspx?ProjectCode=' + Form1.txtProjectCode.value + '&PBSUnitCode=' + Form1.txtPBSUnitCode.value;

//	GotoPBSUnit(Form1.txtPBSUnitCode.value);
}

function GotoPBSUnit(PBSUnitCode)
{
	var objFrame = document.all("frameMain");
	if (PBSUnitCode != "")
	{
		objFrame.src = '../Construct/ProgressReportList.aspx?PBSUnitCode=' + PBSUnitCode;
	}
	else
	{
		objFrame.src = '';
	}
}
	
function ClosePBSUnitWindow()
{
	document.all("tdLeft").style.display = "none";
	
//	var objBtn = frameMain.Form1.btnShowPBSUnitWindow;
	var objBtn = document.all.divShowWindow;
	objBtn.style.display = "";
}

function OpenPBSUnitWindow()
{
	document.all("tdLeft").style.display = "block";
	
//	var objBtn = frameMain.Form1.btnShowPBSUnitWindow;
	var objBtn = document.all.divShowWindow;
	objBtn.style.display = "none";
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
								<td class="topic" background="../images/topic_bg.gif" nowrap><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle">计划管理 
									- 进度报告
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
								<TD vAlign="top" align="center" width="120" bgColor="#ffffff" id="tdLeft">
									<iframe name="frameLeft" src="" frameBorder="no" width="100%" scrolling="no" height="100%"
										marginwidth="0" marginheight="0"></iframe>
								</TD>
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
	<div id="divShowWindow" style="DISPLAY: none; LEFT: 1px; POSITION: absolute; TOP: 32px; BACKGROUND-COLOR: transparent">
    <TABLE id="tableShowWindow" cellSpacing="0" cellPadding="0" border="0">
     <TR>
      <TD vAlign="top" align="center" onclick="OpenPBSUnitWindow();" style="cursor:hand"><img src="../images/btn_more_l.gif" onmouseover="this.src='../images/btn_more_lo.gif';" onmouseout="this.src='../images/btn_more_l.gif';" title="显示单位工程列表">
      </TD>
     </TR>
    </TABLE>
   </div>
			<input type="hidden" id="txtProjectCode" name="txtProjectCode" runat="server"><input type="hidden" id="txtFromUrl" name="txtFromUrl" runat="server">
			<input type="hidden" id="txtPBSUnitCode" name="txtPBSUnitCode" runat="server">
		</form>
		<script language="javascript">
<!--
//-->
		</script>
	</body>
</HTML>
