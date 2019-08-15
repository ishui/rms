<%@ Register TagPrefix="cc2" Namespace="RmsPM.WebControls.ToolsBar" Assembly="RmsPM.WebControls" %>
<%@ Page language="c#" Inherits="RmsPM.Web.Sal.SalCostModify" CodeFile="SalCostModify.aspx.cs" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>成本录入</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<style>.ItemGridTr1 { COLOR: #000000; BACKGROUND-COLOR: #ffffcc }
	.ItemGridTr2 { COLOR: #000000; BACKGROUND-COLOR: #f5f5f5 }
		</style>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0" bgcolor="#ffffff">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">成本录入</td>
				</tr>
				<tr>
					<td class="note">单位：元</td>
				</tr>
				<tr height="100%">
					<td class="topic" vAlign="top" align="center">
						<table width="100%" border="0" cellpadding="0" cellspacing="0">
							<TR>
								<TD>
									<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%"><asp:datagrid id="dgList" runat="server" Width="100%" CssClass="list" CellPadding="0" AllowSorting="True"
											AutoGenerateColumns="False" PageSize="15" ShowFooter="False">
											<ItemStyle CssClass=""></ItemStyle>
											<HeaderStyle CssClass="list-title"></HeaderStyle>
											<FooterStyle CssClass="list-title"></FooterStyle>
											<Columns>
												<asp:TemplateColumn HeaderText="序号" HeaderStyle-HorizontalAlign="Left" Visible="False">
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
													<ItemTemplate>
														<%# Container.ItemIndex + 1 %>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="BuildingName" HeaderText="幢号" HeaderStyle-HorizontalAlign="Left"></asp:BoundColumn>
												<asp:TemplateColumn HeaderText="总成本" HeaderStyle-HorizontalAlign="Left">
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
													<ItemTemplate>
														<asp:TextBox CssClass="input-nember" ID="txtTotalCost" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TotalCost", "{0:N}") %>'>
														</asp:TextBox>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle NextPageText="下页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上页" HorizontalAlign="Center"
												CssClass="ListHeadTr"></PagerStyle>
										</asp:datagrid></div>
								</TD>
							</TR>
						</table>
					</td>
				</tr>
				<tr>
					<td>
						<table width="100%" cellspacing="10">
							<tr>
								<td align="center">
									<input id="btnSave" name="btnSave" type="button" class="submit" value="确 定" runat="server" onserverclick="btnSave_ServerClick">
									<input id="btnCancel" name="btnCancel" type="button" class="submit" value="取 消" onclick="javascript:self.close()">
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<input type="hidden" runat="server" id="txtProjectCode" name="txtProjectCode"><INPUT id="txtRefreshScript" type="hidden" name="txtRefreshScript" runat="server">
			<SCRIPT language="javascript">
<!--

//-->
			</SCRIPT>
		</form>
	</body>
</HTML>
