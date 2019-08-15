<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Page language="c#" Inherits="RmsPM.Web.Finance.FinanceInterfaceFrame" CodeFile="FinanceInterfaceFrame.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>财务编码管理</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<SCRIPT language="javascript" src="../images/checkbox.js"></SCRIPT>
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<script language="javascript">
<!--

function winload()
{
	if ((Form1.sltSubjectSet.value == "") && (Form1.sltSubjectSet.options.length > 1))
		Form1.sltSubjectSet.selectedIndex = 1;

	if (Form1.sltAnalysisType.options.length > 1)
		Form1.sltAnalysisType.selectedIndex = 1;
		
	GotoAnalysis();
}

function FinanceInterfaceSetup()
{
	OpenCustomWindow("FinanceInterfaceSetup.aspx", "FinanceInterfaceSetup", 350, 200);
}

//显示该核算项目的列表
function GotoAnalysis()
{
	var AnalysisType = Form1.sltAnalysisType.value;
	var SubjectSetCode = Form1.sltSubjectSet.value;
	
	if ((AnalysisType == "") || (SubjectSetCode == "")) return;
	
	var href = "";
	
	if (AnalysisType == "Unit")
		href = "FinanceInterfaceAnalysisUnitList.aspx";
	else if (AnalysisType == "User")
		href = "FinanceInterfaceAnalysisUserList.aspx";
	else if (AnalysisType == "Building")
		href = "FinanceInterfaceAnalysisBuildingList.aspx";
	else if (AnalysisType == "Project")
		href = "FinanceInterfaceAnalysisProjectList.aspx";
	else if (AnalysisType == "Supplier")
		href = "FinanceInterfaceAnalysisSupplierList.aspx";
		
	var objMain = document.all("frameMain");
	objMain.src = href + "?SubjectSetCode=" + SubjectSetCode;
}
	
//-->
		</script>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0" onload="winload()">
		<form id="Form1" method="post" runat="server">
			<TABLE height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td bgColor="#e4eff6" height="6"></td>
				</tr>
				<tr>
					<td height="25">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="topic" background="../images/topic_bg.gif"><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle">系统管理 
									- 财务编码管理</td>
								<td width="9"><IMG height="25" src="../images/topic_corr.gif" width="9"></td>
							</tr>
						</table>
					</td>
				</tr>
				<TR style="DISPLAY:none">
					<td class="tools-area" vAlign="top">
						<IMG src="../images/btn_li.gif" align="absMiddle"> <input class="button" id="btnSetup" onclick="FinanceInterfaceSetup();" type="button" value="财务接口设置"
							name="btnSetup" runat="server">
					</td>
				</TR>
				<tr>
					<td class="table">
						<table cellSpacing="0" cellPadding="0" border="0">
							<tr>
								<td style="display:none" class="note" height="20" nowrap valign="bottom" style="PADDING-RIGHT:20px">导入到财务系统：<a href="#" onclick="FinanceInterfaceSetup();return false;" runat="server" id="hrefSetup"
										name="hrefSetup"><span id="spanFinanceInterface" runat="server"></span></a></td>
								<td width="100%" valign="bottom">
									<select runat="server" id="sltAnalysisType" name="sltAnalysisType" class="select" onchange="GotoAnalysis()">
										<option selected value="">--请选择编码项--</option>
									</select>
									<select runat="server" id="sltSubjectSet" name="sltSubjectSet" class="select" onchange="GotoAnalysis()">
										<option selected value="">--请选择帐套--</option>
									</select>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<TR height="100%">
					<td class="" vAlign="top">
						<table border="0" cellpadding="0" cellspacing="0" width="100%" height="100%">
							<tr>
								<TD vAlign="top" align="left">
									<iframe name="frameMain" src='' frameBorder="no" width="100%" scrolling="no" height="100%"
										marginwidth="0" marginheight="0"></iframe>
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
			<input type="hidden" runat="server" id="txtSubjectSetCode" name="txtSubjectSetCode">
		</form>
	</body>
</HTML>
