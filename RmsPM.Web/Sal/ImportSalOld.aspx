<%@ Page language="c#" CodeFile="ImportSalOld.aspx.cs" AutoEventWireup="false" Inherits="RmsPM.Web.Sal.ImportSalOld" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ImportSalOld</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Images/Style.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<script language="javascript">
	function ImportSal()
	{
		OpenCustomWindow("ImportSalOldDlg.aspx","������������", 400, 300);
	}

	function ImportUFProject()
	{
		OpenCustomWindow("../Finance/ImportUFProjectDlg.aspx","���������Ŀ", 400, 300);
	}

	function ImportUFUnit()
	{
		OpenCustomWindow("../Finance/ImportUFUnitDlg.aspx","������㲿��", 400, 300);
	}
		</script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="100%" border="0" height="100%">
				<tr height="50">
					<td><input type="button" class="button" id="btnImportSal" onclick="ImportSal();"
							value="������������" runat="server"></td>
				</tr>
				<tr height="50">
					<td><input type="button" class="button" id="btnImportUFProject" onclick="ImportUFProject();"
							value="���������Ŀ" runat="server"></td>
				</tr>
				<tr height="50">
					<td><input type="button" class="button" id="btnImportUFUnit" onclick="ImportUFUnit();"
							value="������㲿��" runat="server"></td>
				</tr>
				<tr>
					<td></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
