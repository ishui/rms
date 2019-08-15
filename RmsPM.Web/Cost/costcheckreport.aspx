<%@ Page language="c#" Inherits="RmsPM.Web.Cost.CostCheckReport" CodeFile="CostCheckReport.aspx.cs" %>
<%@ Register TagPrefix="cc1" Namespace="AspWebControl" Assembly="AspWebControl" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>��Ŀ�ɱ���̯��</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<script language="javascript">
<!--

//ѡ��¥��
function SelectBuilding()
{
	OpenCustomWindow("../PBS/SelectBuilding.aspx?CanSelectArea=0&ProjectCode=" + Form1.txtProjectCode.value + "&SelectCode=&ReturnFunc=SelectBuildingReturn", "ѡ��¥��", 400, 540);
}

//ѡ��¥������
function SelectBuildingReturn(code, name)
{
	Form1.txtBuildingCode.value = code;
	Form1.txtBuildingName.value = name;
}

//��ӡ
function Print()
{
	OpenPrintWindow("../Report/PrintList.aspx?FromControlID=divMain", "��ӡ");
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
									��Ŀ�ɱ���̯��</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<TD vAlign="top" align="left">
						<TABLE class="form" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="form-item">¥��������ͣ�</td>
								<td><select runat="server" class="select" id="sltBuildingAreaField" name="sltBuildingAreaField"></select></td>
							</tr>
							<TR>
								<TD class="form-item">ͳ�����ڣ�</TD>
								<td>
									<cc1:calendar id="dtDateBegin" runat="server" Value="" CalendarResource="../Images/CalendarResource/"></cc1:calendar>&nbsp;����
									<cc1:calendar id="dtDateEnd" runat="server" Value="" CalendarResource="../Images/CalendarResource/"></cc1:calendar>
								</td>
							</TR>
							<tr>
								<TD class="form-item">¥����</TD>
								<td><input class="input" id="txtBuildingName" type="text" size="20" name="txtBuildingName"
										runat="server"><A href="javascript:SelectBuilding();"><IMG src="../images/ToolsItemSearch.gif" border="0"></A><input id="txtBuildingCode" type="hidden" name="txtBuildingCode" runat="server">
								</td>
							</tr>
						</TABLE>
						<table border="0" cellpadding="0" cellspacing="10" width="100%">
							<tr>
								<td align="center"><input class="submit" id="btnOk" onclick="document.all.divHintLoad.style.display = '';"
										type="button" value="��ʼͳ��" name="btnOk" runat="server"> <input class="submit" id="btnPrint" onclick="Print();" type="button" value="�� ӡ" name="btnPrint"
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
									<TD>��Ŀ��<asp:Label Runat="server" ID="lblBuildingAreaFieldDesc"></asp:Label>(ƽ��)</TD>
									<TD><asp:label id="lblProjectTotalArea" runat="server"></asp:label></TD>
									<TD>��Ŀ�ܳɱ���</TD>
									<TD><asp:label id="lblProjectTotalCost" runat="server"></asp:label></TD>
								</TR>
							</TABLE>
							<TABLE id="tbReport" style="BORDER-COLLAPSE: collapse" borderColor="blue" cellSpacing="0"
								cellPadding="0" width="100%" border="1" runat="server">
								<tr height="24">
									<td id="tdTableTitle0" width="250"></td>
									<td id="tdTableTitle1" align="center">ƽ̯</td>
									<td id="tdTableTitle2" noWrap align="center">��ͬ��</td>
								</tr>
								<tr>
									<td noWrap width="100" align="center">¥��\������</td>
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
