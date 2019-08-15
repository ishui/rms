<%@ Page language="c#" Inherits="RmsPM.Web.Cost.CostContrast" CodeFile="CostContrast.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>对比曲线</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<TABLE WIDTH="90%" ALIGN="center" BORDER="0" CELLSPACING="0" CELLPADDING="0">
				<TR>
					<TD align="center" height="20"></TD>
				</TR>
				<TR>
					<TD align="center">
						<TABLE id="Table1" cellSpacing="1" cellPadding="0" border="0" width="100%">
							<tr>
								<td>
									<asp:label id="lblCostName" runat="server" CssClass="TitleText">费用项名称</asp:label></td>
							</tr>
							<tr>
								<td>
									<asp:Label id="lblDate" runat="server"></asp:Label></td>
							</tr>
							<tr>
								<td align="center">
									<table width="80%">
										<TR>
											<TD id="tdGraphy" runat="server"></TD>
										</TR>
									</table>
								</td>
							</tr>
							<tr>
								<td align="center">
									<table width="60%">
										<tr>
											<td bgColor="black" width="10%"></td>
											<td width="20%">估算费用</td>
											<td width="10%" bgColor="green"></td>
											<td width="20%">预算费用</td>
											<td width="10%" bgColor="red"></td>
											<td>动态费用</td>
										</tr>
									</table>
								</td>
							</tr>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
