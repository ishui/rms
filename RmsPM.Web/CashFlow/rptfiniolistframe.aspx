<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Page language="c#" Inherits="RmsPM.Web.CashFlow.RptFinIOListFrame" CodeFile="RptFinIOListFrame.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>RptFinIOListFrame</title>
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
								<td class="topic" background="../images/topic_bg.gif" nowrap><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle">项目管理>财务管理>现金流量
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
								<td width="200" height="25" valign="bottom" class="note">现金流量表</td>
							</tr>
						</table>
					</td>
				</tr>
				<TR>
					<TD vAlign="top" align="left" class="table">
						<TABLE class="form" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td width="100%">
									<input class="button" id="btnPrint" onclick="Print()" type="button" value="打 印" name="Print"
										runat="server">
									<select class="select" runat="server" id="sltSource" name="sltSource" onchange="DoReport();">
										<option value="Fact" selected>实际已批</option>
										<option value="FactPay">实际已付</option>
										<option value="Plan">计划基准</option>
									</select>
									<select class="select" runat="server" id="sltMonthType" name="sltMonthType" onchange="DoReport();">
										<option value="q" selected>季度</option>
										<option value="m">月度</option>
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
	var Source = Form1.sltSource.value;
	var MonthType = Form1.sltMonthType.value;
	
	var objMain = document.all("frameMain");
	objMain.src = "RptFinIOList.aspx?ProjectCode=" + Form1.txtProjectCode.value + "&Source=" + Source + "&MonthType=" + MonthType;
	
//	document.all.tableHint.style.display = "block";
//	return true;
}

//打印
function Print()
{
	var objMain = document.all("frameMain");
	frameMain.document.all.btnPrint.click();
}

		</script>
	</body>
</HTML>
