<%@ Page language="c#" Inherits="RmsPM.Web.Systems.SelectVoucher" CodeFile="SelectVoucher.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>选择凭证</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0" bgcolor="#ffffff">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">选择凭证</td>
				</tr>
				<tr>
					<td>
						<TABLE class="search-area" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td>
									<table>
										<tr>
											<TD>凭证类型：</TD>
											<TD><SELECT class="select" id="sltSearchVoucherType" name="sltSearchVoucherType" runat="server">
													<OPTION value="" selected>------请选择------</OPTION>
												</SELECT></TD>
											<td>凭证号：</td>
											<td><input id="txtSearchVoucherID" type="text" size="10" name="txtSearchVoucherID" runat="server"></td>
											<td><input class="submit" id="btnSearch" type="button" value="搜 索" name="btnSearch" runat="server" onserverclick="btnSearch_ServerClick"></td>
										</tr>
									</table>
								</td>
							</tr>
						</TABLE>
					</td>
				</tr>
				<tr height="100%">
					<td vAlign="top">
						<DIV style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
							<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
								<tr>
									<td vAlign="top"><asp:datagrid id="dgList" runat="server" CssClass="list" Width="100%" AllowSorting="True" AutoGenerateColumns="False"
											GridLines="Horizontal" CellPadding="0">
											<AlternatingItemStyle CssClass=""></AlternatingItemStyle>
											<ItemStyle CssClass=""></ItemStyle>
											<HeaderStyle CssClass="list-title"></HeaderStyle>
											<FooterStyle CssClass="list-title"></FooterStyle>
											<Columns>
												<asp:TemplateColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" HeaderText="凭证号">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<a href="#" onclick="Select(this.Code);return false;" Code='<%#  DataBinder.Eval(Container.DataItem, "VoucherCode") %>'><%#  DataBinder.Eval(Container.DataItem, "VoucherID") %></a>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="凭证类型">
													<ItemTemplate>
														<%# RmsPM.BLL.VoucherRule.GetVoucherTypeName(DataBinder.Eval(Container, "DataItem.VoucherType").ToString())%>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="MakeDate" HeaderText="编制时间" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn>
												<asp:BoundColumn DataField="TotalMoney" HeaderText="金额" DataFormatString="{0:N}">
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn HeaderText="状态">
													<ItemTemplate>
														<%# DataBinder.Eval(Container,"DataItem.StatusName") %>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
										</asp:datagrid>&nbsp;
									</td>
								</tr>
							</table>
						</DIV>
					</td>
				</tr>
				<tr>
					<td colSpan="2"><asp:label id="lblMessage" runat="server" ForeColor="Red"></asp:label></td>
				</tr>
				<TR>
					<TD colSpan="2">
						<table cellSpacing="10" width="100%">
							<TR>
								<TD align="center"><INPUT class="submit" onclick="window.close();" type="button" value="取 消"></TD>
							</TR>
						</table>
					</TD>
				</TR>
			</table>
			<INPUT id="txtAct" type="hidden" name="txtAct" runat="server"><INPUT id="txtRefreshScript" type="hidden" name="txtRefreshScript" runat="server">
			<INPUT id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server">
			<script language="javascript">
<!--
	function Select(VoucherCode)
	{
		window.close();
		window.opener.SelectVoucherReturn(VoucherCode);
	}
	
//-->
			</script>
		</form>
	</body>
</HTML>
