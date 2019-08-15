<%@ Page language="c#" Inherits="RmsPM.Web.Cost.CostCheckReport" CodeFile="CostCheckReport.aspx.cs" %>
<%@ Register TagPrefix="cc1" Namespace="AspWebControl" Assembly="AspWebControl" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>项目成本分摊表</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<script language="javascript">
<!--

//选择楼栋
function SelectBuilding()
{
	OpenCustomWindow("../PBS/SelectBuilding.aspx?CanSelectArea=0&ProjectCode=" + Form1.txtProjectCode.value + "&SelectCode=&ReturnFunc=SelectBuildingReturn", "选择楼栋", 400, 540);
}

//选择楼栋返回
function SelectBuildingReturn(code, name)
{
	Form1.txtBuildingCode.value = code;
	Form1.txtBuildingName.value = name;
}

//打印
function Print()
{
	OpenPrintWindow("../Report/PrintList.aspx?FromControlID=divMain", "打印");
}

//-->
		</script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0" height="100%">
				<tr>
					<td>
						<table border="0" cellpadding="0" cellspacing="0" id="Table2">
							<tr>
								<td height="25" valign="bottom" class="note"><asp:Label ID="lblProjectName" Runat="server"></asp:Label>
									项目成本分摊表</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<TD vAlign="top" align="left">
						<TABLE class="form" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="form-item">楼栋面积类型：</td>
								<td><select runat="server" class="select" id="sltBuildingAreaField" name="sltBuildingAreaField"></select></td>
							</tr>
							<TR>
								<TD class="form-item">统计日期：</TD>
								<td>
									<cc1:calendar id="dtDateBegin" runat="server" Value="" CalendarResource="../Images/CalendarResource/"></cc1:calendar>&nbsp;——
									<cc1:calendar id="dtDateEnd" runat="server" Value="" CalendarResource="../Images/CalendarResource/"></cc1:calendar>
								</td>
							</TR>
							<tr>
								<TD class="form-item">楼栋：</TD>
								<td><input class="input" id="txtBuildingName" type="text" size="20" name="txtBuildingName"
										runat="server"><A href="javascript:SelectBuilding();"><IMG src="../images/ToolsItemSearch.gif" border="0"></A><input id="txtBuildingCode" type="hidden" name="txtBuildingCode" runat="server">
								</td>
							</tr>
						</TABLE>
						<table border="0" cellpadding="0" cellspacing="10" width="100%">
							<tr>
								<td align="center"><input class="submit" id="btnOk" onclick="document.all.divHintLoad.style.display = '';"
										type="button" value="开始统计" name="btnOk" runat="server"> <input class="submit" id="btnPrint" onclick="Print();" type="button" value="打 印" name="btnPrint"
										runat="server">
								</td>
							</tr>
						</table>
					</TD>
				</tr>
				<TR height="100%">
					<TD vAlign="top" align="left">
						<div style="OVERFLOW:auto;WIDTH:100%;POSITION:absolute;HEIGHT:100%" id="divMain">
							<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
								<TR>
									<TD>项目总<asp:Label Runat="server" ID="lblBuildingAreaFieldDesc"></asp:Label>(平米)</TD>
									<TD><asp:label id="lblProjectTotalArea" runat="server"></asp:label></TD>
									<TD>项目总成本：</TD>
									<TD><asp:label id="lblProjectTotalCost" runat="server"></asp:label></TD>
								</TR>
							</TABLE>
							<TABLE id="tbReport" style="BORDER-COLLAPSE: collapse" borderColor="blue" cellSpacing="0"
								cellPadding="0" width="100%" border="1" runat="server">
								<tr height="24">
									<td id="tdTableTitle0" width="250"></td>
									<td id="tdTableTitle1" align="center">平摊</td>
									<td id="tdTableTitle2" noWrap align="center">合同数</td>
								</tr>
								<tr>
									<td noWrap width="100" align="center">楼栋\费用项</td>
								</tr>
							</TABLE>
						</div>
					</TD>
				</TR>
			</TABLE>
			<div id="divHintLoad" style="DISPLAY: none; LEFT: 1px; WIDTH: 100%; POSITION: absolute; TOP: 200px; BACKGROUND-COLOR: transparent">
				<TABLE id="tableHintLoad" height="100" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD vAlign="top" align="center"><iframe id="frameLoad" src="../Cost/LoadingPrepare.htm" frameBorder="no" width="100%" scrolling="auto"
								height="100%"></iframe>
						</TD>
					</TR>
				</TABLE>
			</div>
			<input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server">
		</form>
	</body>
</HTML>
