<%@ Page language="c#" Inherits="RmsPM.Web.CashFlow.RptSalListFrame" CodeFile="RptSalListFrame.aspx.cs" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>RptSalListFrame</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<script src="../Rms.js"></script>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0" onload="winload();">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0"
				bgcolor="#ffffff">
				<tr>
					<td bgColor="#e4eff6" height="6"></td>
				</tr>
				<tr>
					<td height="25">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="topic" background="../images/topic_bg.gif" nowrap><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle"><span runat="server" id="lblTitle">统计分析</span>
								</td>
								<td width="9"><IMG height="25" src="../images/topic_corr.gif" width="9"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr style="DISPLAY:none">
					<td>
						<table border="0" cellpadding="0" cellspacing="0">
							<tr>
								<td width="200" height="25" valign="bottom" class="note"></td>
							</tr>
						</table>
					</td>
				</tr>
				<TR>
					<TD vAlign="top" align="left" class="table">
						<TABLE class="form" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td width="100%">
									<select class="select" runat="server" id="sltYear" name="sltYear" onchange="DoReport();">
										<option value="2002" selected>2002年</option>
										<option value="2003">2003年</option>
										<option value="2004">2004年</option>
										<option value="2005">2005年</option>
										<option value="2006">2006年</option>
										<option value="2007">2007年</option>
										<option value="2008">2008年</option>
										<option value="2009">2009年</option>
									</select>
									<select class="select" runat="server" id="sltMonth" name="sltMonth" onchange="DoReport();">
										<option value="1" selected>1月</option>
										<option value="2">2月</option>
										<option value="3">3月</option>
										<option value="4">4月</option>
										<option value="5">5月</option>
										<option value="6">6月</option>
										<option value="7">7月</option>
										<option value="8">8月</option>
										<option value="9">9月</option>
										<option value="10">10月</option>
										<option value="11">11月</option>
										<option value="12">12月</option>
									</select>
									<input class="submit" id="btnExcel" type="button" value="开始统计" runat="server" onclick="DoReport(); return false;"
										NAME="btnExcel" style="DISPLAY:none" onserverclick="btnExcel_ServerClick">
								</td>
							</tr>
						</TABLE>
					</TD>
				</TR>
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
			</TABLE>
			<input type="hidden" id="txtProjectCode" name="txtProjectCode" runat="server"> <input type="hidden" id="txtProjectName" name="txtProjectName" runat="server">
			<input type="hidden" id="txtType" name="txtType" runat="server">
		</form>
		<script language="javascript">
		
function winload()
{
	var objMain = document.all("frameMain");
//	if (objMain.src == "")
		DoReport();
}

function DoReport()
{
	var Type = Form1.txtType.value;
	var Year = Form1.sltYear.value;
	var Month = Form1.sltMonth.value;
	
	var objMain = document.all("frameMain");
	if (Type == "2")
	{
		objMain.src = "RptConstructProgList.aspx?ProjectCode=" + Form1.txtProjectCode.value + "&Year=" + Year + "&Month=" + Month;
	}
	else
	if (Type == "1")
	{
		objMain.src = "RptCostList.aspx?ProjectCode=" + Form1.txtProjectCode.value + "&Year=" + Year + "&Month=" + Month;
	}
	else
	{
		objMain.src = "RptSalList.aspx?ProjectCode=" + Form1.txtProjectCode.value + "&Year=" + Year + "&Month=" + Month;
	}
	
//	document.all.tableHint.style.display = "block";
//	return true;
}

		</script>
	</body>
</HTML>
