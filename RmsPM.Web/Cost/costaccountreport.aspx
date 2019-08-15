<%@ Page language="c#" Inherits="RmsPM.Web.Cost.CostAccountReport" CodeFile="CostAccountReport.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>成本费用对比报表</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD align="center">
						<TABLE cellSpacing="0" cellPadding="0" width="98%" border="0">
							<TR>
								<TD width="20%"></TD>
								<TD width="20%"></TD>
								<TD id="tdTitle" noWrap align="center" width="20%" runat="server">成本费用对比表</TD>
								<TD width="20%"></TD>
								<TD width="20%"></TD>
							</TR>
							<TR>
								<TD width="20%">面积：</TD>
								<TD width="20%">其中住宅：</TD>
								<TD width="20%">车库：</TD>
								<TD width="20%">销售收入：</TD>
								<TD width="20%">单位：元</TD>
							</TR>
							<TR>
								<TD></TD>
								<TD></TD>
								<TD></TD>
								<TD></TD>
								<TD></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD>
						<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0" class="list">
							<tr height="24" class="list-title">
								<td noWrap width="250">费用项名称</td>
								<td noWrap align="center" width="18%">合同数</td>
								<td noWrap align="center" width="18%">单方造价</td>
								<td noWrap align="center" width="18%">实际数</td>
								<td noWrap align="center" width="18%">单方造价</td>
							</tr>
							<asp:repeater id="repeatList" runat="server">
								<ItemTemplate>
									<tr class="list-i">
										<td nowrap><%# DataBinder.Eval(Container.DataItem, "CostName") %></td>
										<td>
											<div align="right"><%# DataBinder.Eval(Container.DataItem, "AHMoney") %></div>
										</td>
										<td>
											<div align="right"><%# DataBinder.Eval(Container.DataItem, "AverageAHMoney") %></div>
										</td>
										<td>
											<div align="right"><%# DataBinder.Eval(Container.DataItem, "APMoney") %></div>
										</td>
										<td>
											<div align="right"><%# DataBinder.Eval(Container.DataItem, "AverageAPMoney") %></div>
										</td>
									</tr>
								</ItemTemplate>
							</asp:repeater>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
