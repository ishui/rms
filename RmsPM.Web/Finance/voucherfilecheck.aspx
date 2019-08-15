<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Page language="c#" Inherits="RmsPM.Web.Finance.VoucherFileCheck" CodeFile="VoucherFileCheck.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>凭证导出检查</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" bgColor="#ffffff" border="0">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">凭证导出检查</td>
				</tr>
				<tr id="trErr" height="100%" runat="server">
					<td vAlign="top">
						<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
							<div id="divWarn" style="DISPLAY: none" runat="server"><br>
								<table id="tbErr3" cellSpacing="0" cellPadding="0" border="0" runat="server">
									<tr>
										<td class="intopic" width="200">警告信息列表</td>
									</tr>
								</table>
								<TABLE id="tbErr4" cellSpacing="0" cellPadding="0" width="100%" align="center" border="0"
									runat="server">
									<TR>
										<TD vAlign="top"><asp:datagrid id="dgWarn" runat="server" CssClass="List" Width="100%" PageSize="15" AutoGenerateColumns="False"
												GridLines="Both" AllowSorting="True" CellPadding="0" DataKeyField="">
												<HeaderStyle CssClass="list-title"></HeaderStyle>
												<FooterStyle CssClass="list-title"></FooterStyle>
												<Columns>
													<asp:TemplateColumn HeaderText="序号">
														<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
														<ItemStyle Wrap="False" HorizontalAlign="Center" Width="30px"></ItemStyle>
														<ItemTemplate>
															<%# Container.ItemIndex + 1 %>
														</ItemTemplate>
													</asp:TemplateColumn>
													<asp:TemplateColumn HeaderText="警告信息">
														<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
														<ItemStyle></ItemStyle>
														<ItemTemplate>
															<%# DataBinder.Eval(Container, "DataItem.Desc") %>
														</ItemTemplate>
														<FooterStyle HorizontalAlign="Center"></FooterStyle>
													</asp:TemplateColumn>
												</Columns>
												<PagerStyle NextPageText="下页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上页" HorizontalAlign="Center"
													CssClass="ListHeadTr"></PagerStyle>
											</asp:datagrid></TD>
									</TR>
								</TABLE>
							</div>
							<div id="divErr" style="DISPLAY: none" runat="server"><br>
								<table id="tbErr1" cellSpacing="0" cellPadding="0" border="0" runat="server">
									<tr>
										<td class="intopic" width="200">错误信息列表</td>
									</tr>
								</table>
								<TABLE id="tbErr2" cellSpacing="0" cellPadding="0" width="100%" align="center" border="0"
									runat="server">
									<TR>
										<TD vAlign="top"><asp:datagrid id="dgList" runat="server" CssClass="List" Width="100%" PageSize="15" AutoGenerateColumns="False"
												GridLines="Both" AllowSorting="True" CellPadding="0" DataKeyField="">
												<HeaderStyle CssClass="list-title"></HeaderStyle>
												<FooterStyle CssClass="list-title"></FooterStyle>
												<Columns>
													<asp:TemplateColumn HeaderText="序号">
														<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
														<ItemStyle Wrap="False" HorizontalAlign="Center" Width="30px"></ItemStyle>
														<ItemTemplate>
															<%# Container.ItemIndex + 1 %>
														</ItemTemplate>
													</asp:TemplateColumn>
													<asp:TemplateColumn HeaderText="错误信息">
														<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
														<ItemStyle></ItemStyle>
														<ItemTemplate>
															<%# DataBinder.Eval(Container, "DataItem.Desc") %>
														</ItemTemplate>
														<FooterStyle HorizontalAlign="Center"></FooterStyle>
													</asp:TemplateColumn>
												</Columns>
												<PagerStyle NextPageText="下页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上页" HorizontalAlign="Center"
													CssClass="ListHeadTr"></PagerStyle>
											</asp:datagrid></TD>
									</TR>
								</TABLE>
							</div>
						</div>
					</td>
				</tr>
				<tr>
					<td>
						<table cellPadding="0" width="100%">
							<tr>
								<TD class="note"><asp:label id="lblResultWarn" Runat="server" Visible="False">有警告，是否继续导出？</asp:label><asp:label id="lblResultErr" Runat="server" Visible="False">检查不通过，不能导出</asp:label></TD>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td>
						<table cellSpacing="10" width="100%">
							<tr>
								<td align="center"><input class="submit" id="btnSave" style="DISPLAY: none" onclick="if (!Check()) return false;"
										type="button" value="确 定" name="btnSave" runat="server" onserverclick="btnSave_ServerClick"> <input class="submit" id="btnCancel" onclick="javascript:self.close()" type="button" value="取 消"
										name="btnCancel">
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server"> <INPUT id="txtIsNew" type="hidden" name="txtIsNew" runat="server">
			<INPUT id="txtContractCode" type="hidden" name="txtContractCode" runat="server"><INPUT id="txtStatus" type="hidden" name="txtStatus" runat="server">
			<INPUT id="txtIsContract" type="hidden" name="txtCode" runat="server"> <INPUT id="txtVoucherCode" type="hidden" name="txtPaymentCode" runat="server">
			<INPUT id="txtSupplyCode" type="hidden" name="txtSupplyCode" runat="server">
			<INPUT id="txtSubjectSetCode" type="hidden" name="txtSubjectSetCode" runat="server">
		</form>
		<script language="javascript">
//审核
function Check()
{
	if (document.all.divWarn.style.display != "none")
	{
		if (!confirm("有警告，确实要导出吗？"))
		{
			return false;
		}
	}
	
	return true;
}
		</script>
	</body>
</HTML>
