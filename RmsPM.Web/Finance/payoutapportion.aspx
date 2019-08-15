<%@ Register TagPrefix="cc5" Namespace="Tiannuo.Web.Controls" Assembly="DataGridWebControls" %>
<%@ Register TagPrefix="uc1" TagName="InputUser" Src="../UserControls/InputUser.ascx" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Register TagPrefix="uc1" TagName="InputSystemGroup" Src="../UserControls/InputSystemGroup.ascx" %>
<%@ Page language="c#" Inherits="RmsPM.Web.Finance.PayoutApportion" CodeFile="PayoutApportion.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>�ɱ���̯</title>
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
			<TABLE height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR height="100%">
					<td>
						<table width="100%" height="100%">
							<tr height="100%">
								<td>
									<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%"><asp:datagrid id="dgList" runat="server" ShowFooter="True" Width="100%" AllowSorting="True" AutoGenerateColumns="False"
											PageSize="15" AllowPaging="True" CellPadding="0" CssClass="list">
											<HeaderStyle CssClass="list-title"></HeaderStyle>
											<FooterStyle CssClass="list-title"></FooterStyle>
											<Columns>
												<asp:TemplateColumn HeaderText="&lt;input type='checkbox' name='chkAll' onclick='ChkSelectAll(document.all.chkSelect, document.all.chkAll.checked);' title='ȫѡ��ȫ��ѡ'&gt;">
													<HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<input type="checkbox" name="chkSelect" class="list-checkbox" onclick="ChkSelectRow('<%# Container.ItemIndex + 1%>', this, document.all.dgList, 'list-2', '');" value='<%#DataBinder.Eval(Container, "DataItem.PayoutCode")%>'>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" HeaderText="�����" FooterText="�ϼ�">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<a href="#" onclick="View(this.Code);return false;" Code='<%#  DataBinder.Eval(Container.DataItem, "PayoutCode") %>'><%#  DataBinder.Eval(Container.DataItem, "PayoutID") %></a>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="ApportionName" SortExpression="StatusName" HeaderText="״̬">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="SupplyName" SortExpression="SupplyName" HeaderText="�ܿλ">
													<HeaderStyle Wrap="False"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="Payer" HeaderText="�ܿ���">
													<HeaderStyle Wrap="False"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="PayoutDate" SortExpression="PayoutDate" HeaderText="��������" DataFormatString="{0:yyyy-MM-dd}">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn SortExpression="Money" HeaderText="������(Ԫ)">
													<HeaderStyle Wrap="False" HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<%# DataBinder.Eval(Container, "DataItem.Money", "{0:N}") %>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
													<FooterTemplate>
														<asp:Label runat="server" ID="lblSumMoney"></asp:Label>
													</FooterTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="���п�Ŀ">
													<HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
													<ItemTemplate>
														<%# RmsPM.BLL.SubjectRule.GetSubjectFullName(RmsPM.BLL.ConvertRule.ToString(DataBinder.Eval(Container, "DataItem.SubjectCode")), RmsPM.BLL.ConvertRule.ToString(DataBinder.Eval(Container, "DataItem.SubjectSetCode"))) %>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="ƾ֤��">
													<HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<a href="#" onclick="javascript:GotoVoucher(this.val);" val='<%#DataBinder.Eval(Container, "DataItem.VoucherCode")%>'>
															<%# RmsPM.BLL.PaymentRule.GetVoucherName(DataBinder.Eval(Container, "DataItem.VoucherCode").ToString())%>
														</a>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle Visible="False" NextPageText="��ҳ&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;��ҳ" HorizontalAlign="Right"
												CssClass="ListHeadTr"></PagerStyle>
										</asp:datagrid>
									</div>
								</td>
							</tr>
							<tr>
								<td>
									<cc5:GridPagination id="gpControl" runat="server" DataGridId="dgList" ControlSourceUrl="../Images/GridPaginationSource/" onpageindexchange="gpControl_PageIndexChange"></cc5:GridPagination>
								</td>
							</tr>
						</table>
					</td>
				</TR>
			</TABLE>
			<input id="txtSumMoney" type="text" runat="server">
		</form>
		<script language="javascript">
<!--
var CurrUrl = window.location.href;

//�鿴����
function View(PayoutCode)
{
	OpenFullWindow('../Finance/PayoutInfo.aspx?PayoutCode='+ PayoutCode + "&FromUrl=" + escape(CurrUrl),'���');
}

//��̯
function doPayoutApportion()
{
	var s = ChkGetSelected(document.all.chkSelect);
	if (s == "")
	{
		alert('��ѡ��һ���������¼');
		return false;
	}

	OpenLargeWindow("../Finance/PayoutDetailApportion.aspx?&PayoutCodes=" + s,"�����̯");

}


//�鿴ƾ֤
function GotoVoucher(VoucherCode)
{
	OpenCustomWindow("../Finance/VoucherInfo.aspx?VoucherCode=" + VoucherCode + "&Open=1", "ƾ֤��Ϣ", 760, 540);
}



//-->
		</script>
		</FORM>
	</body>
</HTML>
