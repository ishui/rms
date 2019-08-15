<%@ Reference Control="~/usercontrols/inputsystemgroup.ascx" %>
<%@ Reference Control="~/usercontrols/inputunit.ascx" %>
<%@ Reference Control="~/usercontrols/inputuser.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="Tiannuo.Web.Controls" Assembly="DataGridWebControls" %>
<%@ Page language="c#" Inherits="RmsPM.Web.Finance.ShowPaymentItemList" CodeFile="ShowPaymentItemList.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>��</title>
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
			<TABLE height="100%" cellSpacing="0" cellPadding="0" width="100%" bgColor="#ffffff" border="0">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">��</td>
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
						<table height="100%" width="100%">
							<tr height="100%">
								<td>
									<div style="OVERFLOW: auto; WIDTH: 100%; POSITION: absolute; HEIGHT: 100%"><asp:datagrid id="dgList" runat="server" ShowFooter="True" CssClass="list" CellPadding="0" AllowPaging="True"
											PageSize="15" AutoGenerateColumns="False" AllowSorting="True" Width="100%">
											<AlternatingItemStyle CssClass=""></AlternatingItemStyle>
											<ItemStyle CssClass=""></ItemStyle>
											<HeaderStyle CssClass="list-title"></HeaderStyle>
											<FooterStyle CssClass="list-title"></FooterStyle>
											<Columns>
												<asp:TemplateColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" Visible="False" HeaderText="&lt;input type='checkbox' name='chkAll' onclick='ChkSelectAll(document.all.chkSelect, document.all.chkAll.checked);' title='ȫѡ��ȫ��ѡ'&gt;">
													<HeaderStyle HorizontalAlign="Center" Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<input type="checkbox" name="chkSelect" class="list-checkbox" onclick="ChkSelectRow('<%# Container.ItemIndex + 1%>', this, document.all.dgList, 'list-2', '');" value='<%#DataBinder.Eval(Container, "DataItem.PaymentCode")%>'>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" HeaderText="����" FooterText="�ϼ�"
													SortExpression="PaymentID">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<a href="#" onclick="View(this.PaymentCode);return false;" PaymentCode='<%#  DataBinder.Eval(Container.DataItem, "PaymentCode") %>'>
															<%#  DataBinder.Eval(Container.DataItem, "PaymentID") %>
														</a>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" DataField="CheckDate" HeaderText="�������"
													DataFormatString="{0:yyyy-MM-dd}" SortExpression="CheckDate"></asp:BoundColumn>
												<asp:BoundColumn Visible="False" ItemStyle-Wrap="False" HeaderStyle-Wrap="False" DataField="StatusName" HeaderText="״̬"
													SortExpression="Status">
													<HeaderStyle HorizontalAlign="Left" Wrap="False"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left" Wrap="False"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" DataField="ContractName" HeaderText="��ͬ����"
													SortExpression="ContractName">
													<HeaderStyle HorizontalAlign="Left" Wrap="False"></HeaderStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" HeaderText="����" SortExpression="GroupName">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<div title='<%#  DataBinder.Eval(Container.DataItem, "GroupName") %>'><%#  DataBinder.Eval(Container.DataItem, "GroupSortID") %></div>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" DataField="UnitName" HeaderText="����"
													SortExpression="UnitName">
													<HeaderStyle HorizontalAlign="Left" Wrap="False"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" DataField="ApplyPersonName" HeaderText="�����"
													SortExpression="ApplyPersonName">
													<HeaderStyle HorizontalAlign="Left" Wrap="False"></HeaderStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" HeaderText="�ܿλ" SortExpression="SupplyName">
													<ItemTemplate>
														<%#  RmsPM.BLL.StringRule.TruncateString( DataBinder.Eval(Container.DataItem, "SupplyName"),8) %>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" DataField="Payer" HeaderText="�ܿ���"
													SortExpression="Payer">
													<HeaderStyle HorizontalAlign="Left" Wrap="False"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" DataField="PayDate" HeaderText="��󸶿���"
													DataFormatString="{0:yyyy-MM-dd}" SortExpression="PayDate">
													<HeaderStyle HorizontalAlign="Left" Wrap="False"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left" Wrap="False"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" DataField="Summary" HeaderText="����"
													SortExpression="Summary">
													<HeaderStyle HorizontalAlign="Left" Wrap="False"></HeaderStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" HeaderText="�����(Ԫ)" SortExpression="Money">
													<HeaderStyle HorizontalAlign="Right" Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<%# RmsPM.BLL.StringRule.BuildShowNumberString(DataBinder.Eval(Container, "DataItem.ItemMoney")) %>
													</ItemTemplate>
													<FooterTemplate>
														<asp:Label runat="server" ID="lblSumMoney"></asp:Label>
													</FooterTemplate>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" HeaderText="�Ѹ����(Ԫ)">
													<HeaderStyle HorizontalAlign="Right" Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<%# RmsPM.BLL.StringRule.BuildShowNumberString(DataBinder.Eval(Container, "DataItem.ItemPayoutMoney")) %>
													</ItemTemplate>
													<FooterTemplate>
														<asp:Label runat="server" ID="lblSumTotalPayoutMoney"></asp:Label>
													</FooterTemplate>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" HeaderText="δ�����(Ԫ)">
													<HeaderStyle HorizontalAlign="Right" Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<%# RmsPM.BLL.StringRule.BuildShowNumberString(RmsPM.BLL.ConvertRule.ToDecimal(DataBinder.Eval(Container, "DataItem.ItemMoney")) - RmsPM.BLL.ConvertRule.ToDecimal(DataBinder.Eval(Container, "DataItem.ItemPayoutMoney")))%>
													</ItemTemplate>
													<FooterTemplate>
														<asp:Label runat="server" ID="lblSumTotalPayoutBalance"></asp:Label>
													</FooterTemplate>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
												</asp:TemplateColumn>
												<asp:BoundColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" DataField="ApplyDate" HeaderText="��������"
													Visible="False" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn>
											</Columns>
											<PagerStyle Visible="False" NextPageText="��ҳ&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;��ҳ" HorizontalAlign="Right"
												CssClass="ListHeadTr"></PagerStyle>
										</asp:datagrid></div>
								</td>
							</tr>
							<tr>
								<td><cc1:gridpagination id="GridPagination1" runat="server" DataGridId="dgList" ControlSourceUrl="../Images/GridPaginationSource/" onpageindexchange="GridPagination1_PageIndexChange"></cc1:gridpagination></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td>
						<table width="100%">
							<tr>
								<td align="center"><input class="submit" onclick="window.close();" type="button" value="�� ��"></td>
							</tr>
						</table>
					</td>
				</tr>
			</TABLE>
			<input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server"> <input id="txtAct" type="hidden" name="txtAct" runat="server">
			<script language="javascript">
<!--

var CurrUrl = window.location.href;

//�鿴���
function View(PaymentCode)
{
	OpenCustomWindow('../Finance/PaymentInfo.aspx?PaymentCode='+ PaymentCode + "&FromUrl=" + escape(CurrUrl), "PaymentInfo", 760, 540);
}

//-->
			</script>
		</form>
	</body>
</HTML>
