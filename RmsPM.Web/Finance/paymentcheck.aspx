<%@ Page language="c#" Inherits="RmsPM.Web.Finance.PaymentCheck" CodeFile="PaymentCheck.aspx.cs" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>请款单审核</title>
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
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">请款单审核</td>
				</tr>
				<tr >
					<td class="topic" vAlign="top" align="center">
						<table class="form" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr id="trOpinion" style="DISPLAY: none"  runat="server">
								<TD class="form-item" width="100">审核意见：</TD>
								<TD><textarea class="textarea" id="txtCheckOpinion" style="WIDTH: 100%" name="txtCheckOpinion"
										rows="5" runat="server"></textarea>
								</TD>
							</tr>
						</table>
					</td>
				</tr>
				<tr height="100%" id="trErr" runat="server">
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
										<TD vAlign="top"><asp:datagrid id="dgWarn" runat="server" DataKeyField="" CellPadding="0" AllowSorting="True" GridLines="Both"
												AutoGenerateColumns="False" PageSize="15" Width="100%" CssClass="List">
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
													<asp:TemplateColumn HeaderText="警告类别">
														<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
														<ItemStyle Wrap="False"></ItemStyle>
														<ItemTemplate>
															<%# DataBinder.Eval(Container, "DataItem.Title") %>
														</ItemTemplate>
														<FooterStyle HorizontalAlign="Center"></FooterStyle>
													</asp:TemplateColumn>
													<asp:TemplateColumn HeaderText="详细信息">
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
										<TD vAlign="top"><asp:datagrid id="dgList" runat="server" DataKeyField="" CellPadding="0" AllowSorting="True" GridLines="Both"
												AutoGenerateColumns="False" PageSize="15" Width="100%" CssClass="List">
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
													<asp:TemplateColumn HeaderText="错误类别">
														<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
														<ItemStyle Wrap="False"></ItemStyle>
														<ItemTemplate>
															<%# DataBinder.Eval(Container, "DataItem.Title") %>
														</ItemTemplate>
														<FooterStyle HorizontalAlign="Center"></FooterStyle>
													</asp:TemplateColumn>
													<asp:TemplateColumn HeaderText="详细信息">
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
								<TD class="note"><asp:label id="lblResultOk" Visible="False" Runat="server">确定审核通过吗？</asp:label><asp:label id="lblResultWarn" Visible="False" Runat="server">有警告，是否审核通过？</asp:label><asp:label id="lblResultErr" Visible="False" Runat="server">校验不通过，不能审核</asp:label></TD>
							</tr>
						</table>
					</td>
				</tr>
				<tr id="trAutoCreatePayout" runat="server" visible="false">
					<td>
						<table cellPadding="0" width="100%">
							<tr>
								<TD class="note">注：成本批量请款审核后将自动付款</TD>
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
			<INPUT id="txtIsContract" type="hidden" name="txtCode" runat="server"> <INPUT id="txtPaymentCode" type="hidden" name="txtPaymentCode" runat="server">
			<INPUT id="txtSupplyCode" type="hidden" name="txtSupplyCode" runat="server">
			<INPUT id="txtAutoCreatePayout" type="hidden" name="txtAutoCreatePayout" runat="server">
		</form>
		<script language="javascript">
//审核
function Check()
{
	if (document.all.divWarn.style.display != "none")
	{
		if (!confirm("有警告，确实要审核吗？"))
		{
			return false;
		}
	}
	
	return true;
}
		</script>
	</body>
</HTML>
