<%@ Reference Control="~/usercontrols/inputsystemgroup.ascx" %>
<%@ Reference Control="~/usercontrols/inputuser.ascx" %>
<%@ Page language="c#" Inherits="RmsPM.Web.Finance.ShowPayoutItemList" CodeFile="ShowPayoutItemList.aspx.cs" %>
<%@ Register TagPrefix="cc1" Namespace="Tiannuo.Web.Controls" Assembly="DataGridWebControls" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>付款单</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<SCRIPT language="javascript" src="../images/checkbox.js"></SCRIPT>
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<TABLE height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0" bgcolor="#ffffff">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">付款单</td>
				</tr>
				<tr>
					<td>
						<table class="note" height="25">
							<tr>
								<td vAlign="bottom"><asp:label id="lblParamDesc" Runat="server"></asp:label></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr height="100%">
					<td vAlign="top">
						<table width="100%" height="100%">
							<tr height="100%">
								<td>
									<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%"><asp:datagrid id="dgList" runat="server" CssClass="list" CellPadding="0" AllowPaging="True" PageSize="15"
											AutoGenerateColumns="False" AllowSorting="True" Width="100%" ShowFooter="True">
											<AlternatingItemStyle CssClass=""></AlternatingItemStyle>
											<ItemStyle CssClass=""></ItemStyle>
											<HeaderStyle CssClass="list-title"></HeaderStyle>
											<FooterStyle CssClass="list-title"></FooterStyle>
											<Columns>
												<asp:TemplateColumn Visible="False" HeaderText="&lt;input type='checkbox' name='chkAll' onclick='ChkSelectAll(document.all.chkSelect, document.all.chkAll.checked);' title='全选或全不选'&gt;">
													<HeaderStyle HorizontalAlign="Center" Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<input type="checkbox" name="chkSelect" class="list-checkbox" onclick="ChkSelectRow('<%# Container.ItemIndex + 1%>', this, document.all.dgList, 'list-2', '');" value='<%#DataBinder.Eval(Container, "DataItem.PayoutCode")%>'>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" HeaderText="付款单号" FooterText="合计"
													SortExpression="PayoutID">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<a href="#" onclick="View(this.Code);return false;" Code='<%#  DataBinder.Eval(Container.DataItem, "PayoutCode") %>'>
															<%#  DataBinder.Eval(Container.DataItem, "PayoutID") %>
														</a>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="StatusName" HeaderText="状态" SortExpression="Status">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" DataField="ContractName" HeaderText="合同名称"
													SortExpression="ContractName">
													<HeaderStyle HorizontalAlign="Left" Wrap="False"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="SupplyName" HeaderText="受款单位" SortExpression="SupplyName">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="True"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="Payer" HeaderText="受款人" SortExpression="Payer">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="True"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="PayoutDate" HeaderText="付款日期" DataFormatString="{0:yyyy-MM-dd}" SortExpression="PayoutDate">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" DataField="Summary" HeaderText="款项"
													SortExpression="Summary">
													<HeaderStyle HorizontalAlign="Left" Wrap="False"></HeaderStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn HeaderText="付款金额(元)" SortExpression="PayoutMoney">
													<HeaderStyle HorizontalAlign="Right" Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<%# DataBinder.Eval(Container, "DataItem.PayoutMoney", "{0:N}") %>
													</ItemTemplate>
													<FooterTemplate>
														<asp:Label runat="server" ID="lblSumMoney"></asp:Label>
													</FooterTemplate>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="CheckPersonName" HeaderText="审核人" SortExpression="CheckPersonName">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="CheckDate" HeaderText="审核日期" DataFormatString="{0:yyyy-MM-dd}" SortExpression="CheckDate">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn HeaderText="凭证号">
													<HeaderStyle HorizontalAlign="Center" Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<a href="#" onclick="javascript:GotoVoucher(this.val);" val='<%#DataBinder.Eval(Container, "DataItem.VoucherCode")%>'>
															<%# RmsPM.BLL.PaymentRule.GetVoucherName(DataBinder.Eval(Container, "DataItem.VoucherCode").ToString())%>
														</a>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle Visible="False" NextPageText="下页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上页" HorizontalAlign="Right"
												CssClass="ListHeadTr"></PagerStyle>
										</asp:datagrid>
									</div>
								</td>
							</tr>
							<tr>
								<td>
									<cc1:gridpagination id="GridPagination1" runat="server" DataGridId="dgList" ControlSourceUrl="../Images/GridPaginationSource/" onpageindexchange="GridPagination1_PageIndexChange"></cc1:gridpagination>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td>
						<table width="100%">
							<tr>
								<td align="center"><input type="button" value="关 闭" class="submit" onclick="window.close();"></td>
							</tr>
						</table>
					</td>
				</tr>
			</TABLE>
			<input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server">
			<script language="javascript">
<!--

var CurrUrl = window.location.href;

//查看付款
function View(PayoutCode)
{
	OpenCustomWindow('../Finance/PayoutInfo.aspx?PayoutCode='+ PayoutCode + "&FromUrl=" + escape(CurrUrl), "PayoutInfo", 760, 540);
}

//查看凭证
function GotoVoucher(VoucherCode)
{
	OpenCustomWindow("../Finance/VoucherInfo.aspx?VoucherCode=" + VoucherCode + "&Open=1", "凭证信息", 760, 540);
}

//-->
			</script>
		</form>
	</body>
</HTML>
