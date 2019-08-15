<%@ Page language="c#" Inherits="RmsPM.Web.Finance.PayoutApportionAccount" CodeFile="PayoutApportionAccount.aspx.cs" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>成本分摊计算</title>
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
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">成本核算</td>
				</tr>
				<tr>
					<td vAlign="top">
						<TABLE class="form" cellSpacing="0" cellPadding="0" width="100%">
							<TR>
								<TD class="form-item" width="100">成本总额：</TD>
								<TD><asp:label id="lblTotalCost" runat="server"></asp:label></TD>
							</TR>
							<tr>
								<td class="form-item">楼栋面积类型：</td>
								<td><select runat="server" class="select" id="sltBuildingAreaField" name="sltBuildingAreaField"></select></td>
							</tr>
						</TABLE>
					</td>
				</tr>
				<tr style="DISPLAY:none">
					<td vAlign="top">
						<br>
						<table cellSpacing="0" cellPadding="0" border="0">
							<tr>
								<td class="intopic" width="200">分摊情况</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr height="100%" style="DISPLAY:none">
					<td vAlign="top">
						<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
							<asp:datagrid id="dgGridApportion" runat="server" CssClass="List" Width="100%" ShowFooter="True"
								PageSize="15" AutoGenerateColumns="False" GridLines="Both" AllowSorting="True" CellPadding="0">
								<HeaderStyle CssClass="list-title"></HeaderStyle>
								<FooterStyle CssClass="list-title"></FooterStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="序号">
										<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<%# Container.ItemIndex + 1 %>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="分摊方式" FooterText="合计">
										<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle Wrap="False"></ItemStyle>
										<ItemTemplate>
											<%# DataBinder.Eval(Container, "DataItem.AlloTypeName") %>
										</ItemTemplate>
										<FooterStyle HorizontalAlign="Center"></FooterStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="楼栋名称">
										<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle Wrap="False"></ItemStyle>
										<ItemTemplate>
											<%# DataBinder.Eval(Container, "DataItem.BuildingName") %>
										</ItemTemplate>
										<FooterStyle HorizontalAlign="Center"></FooterStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="面 积(平方米)">
										<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
										<FooterStyle HorizontalAlign="Right"></FooterStyle>
										<ItemTemplate>
											<%# DataBinder.Eval(Container, "DataItem.BuildingArea", "{0:N}") %>
										</ItemTemplate>
										<FooterStyle HorizontalAlign="Right"></FooterStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="分摊金额(元)">
										<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
										<FooterStyle HorizontalAlign="Right"></FooterStyle>
										<ItemTemplate>
											<%# DataBinder.Eval(Container, "DataItem.ApportionMoney", "{0:N}") %>
										</ItemTemplate>
										<FooterStyle HorizontalAlign="Right"></FooterStyle>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle NextPageText="下页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上页" HorizontalAlign="Center"
									CssClass="ListHeadTr"></PagerStyle>
							</asp:datagrid></div>
					</td>
				</tr>
				<tr>
					<td>
						<table cellSpacing="10" width="100%">
							<tr>
								<td align="center"><input class="submit" id="btnSave" onclick="if (!Check()) return false;" type="button"
										value="核 算" name="btnSave" runat="server"> <input class="submit" id="btnCancel" onclick="javascript:self.close()" type="button" value="取 消"
										name="btnCancel">
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<div id="divHintSave" style="DISPLAY: none; LEFT: 1px; WIDTH: 100%; POSITION: absolute; TOP: 60px">
				<TABLE id="tableHintSave" height="100" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD vAlign="top" align="center"><iframe id="frameSave" src="../Cost/SavingWating.htm" frameBorder="no" width="100%" scrolling="auto"
								height="100%"></iframe>
						</TD>
					</TR>
				</TABLE>
			</div>
		</form>
		<script language="javascript">

	ShowHintSave(false);

	function ShowHintSave( visibled )
	{
		if ( visibled )
			document.all.divHintSave.style.display = 'block';
		else
			document.all.divHintSave.style.display = 'none';
	}

	function Check()
	{
		if (!confirm("确实要开始核算吗？"))
			return false;

		ShowHintSave(true);
		return true;
	}
		</script>
	</body>
</HTML>
