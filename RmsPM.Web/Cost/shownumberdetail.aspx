<%@ Page language="c#" Inherits="RmsPM.Web.Cost.ShowNumberDetail" CodeFile="ShowNumberDetail.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>������ϸ</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0" bgcolor="white">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">Ԥ����ϸ-
						<asp:Label id="lblCostName" runat="server"></asp:Label></td>
				</tr>
				<tr>
					<td class="topic" vAlign="top" align="center">
						<table border="0" cellpadding="0" cellspacing="0" id="tableUse" width="100%" runat="server">
							<tr>
								<td>
									<table cellSpacing="0" cellPadding="0" border="0" width="100%">
										<tr>
											<td class="intopic" width="200">��ͬռ��</td>
											<td></td>
										</tr>
									</table>
									<asp:datagrid id="dgUse" runat="server" Width="100%" AutoGenerateColumns="False" CssClass="list"
										PageSize="1" >
										<AlternatingItemStyle CssClass="AlterGridTr"></AlternatingItemStyle>
										<HeaderStyle CssClass="list-title"></HeaderStyle>
										<FooterStyle CssClass="list-title"></FooterStyle>
										<Columns>
											<asp:TemplateColumn HeaderText="���">
												<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
												<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
												<ItemTemplate>
													<%# Container.ItemIndex + 1 %>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="��ͬ����">
												<ItemTemplate>
													<a href=## contractCode='<%# DataBinder.Eval(Container.DataItem, "ContractCode")  %>' onclick='showContractInfo(this.contractCode);'><%# DataBinder.Eval(Container.DataItem, "ContractName")  %></a>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:BoundColumn DataField="ItemName" HeaderText="�� ��"></asp:BoundColumn>
											<asp:BoundColumn DataField="PlanningPayDate" HeaderText="Ԥ�Ƹ���ʱ��" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn>
											<asp:BoundColumn DataField="Money" HeaderText="�� ��" DataFormatString="{0:N}">
												<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
												<ItemStyle HorizontalAlign="Right"></ItemStyle>
												<FooterStyle HorizontalAlign="Right"></FooterStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="PayedMoney" HeaderText="�Ѹ����" DataFormatString="{0:N}">
												<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
												<ItemStyle HorizontalAlign="Right"></ItemStyle>
												<FooterStyle HorizontalAlign="Right"></FooterStyle>
											</asp:BoundColumn>
										</Columns>
										<PagerStyle NextPageText="��ҳ&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;��ҳ" HorizontalAlign="Right"
											CssClass="list-title"></PagerStyle>
									</asp:datagrid>
								</td>
							</tr>
						</table>
						<table border="0" cellpadding="0" cellspacing="0" id="tableAH" width="100%" runat="server">
							<tr>
								<td>
									<table cellSpacing="0" cellPadding="0" border="0" width="100%">
										<tr>
											<td class="intopic" width="200">�ѷ������</td>
											<td></td>
										</tr>
									</table>
									<asp:datagrid id="dgAH" runat="server" Width="100%" AutoGenerateColumns="False" CssClass="list"
										PageSize="1" >
										<AlternatingItemStyle CssClass="AlterGridTr"></AlternatingItemStyle>
										<HeaderStyle CssClass="list-title"></HeaderStyle>
										<FooterStyle CssClass="list-title"></FooterStyle>
										<Columns>
											<asp:TemplateColumn HeaderText="���">
												<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
												<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
												<ItemTemplate>
													<%# Container.ItemIndex + 1 %>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:BoundColumn DataField="Summary" HeaderText="�� ��"></asp:BoundColumn>

											<asp:TemplateColumn HeaderText="��ͬ����">
												<ItemTemplate>
													<a href=## contractCode='<%# DataBinder.Eval(Container.DataItem, "ContractCode")  %>' onclick='showContractInfo(this.contractCode);'><%# DataBinder.Eval(Container.DataItem, "ContractName")  %></a>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="���">
												<ItemTemplate>
													<a href=## paymentCode='<%# DataBinder.Eval(Container.DataItem, "PaymentCode")  %>' onclick='showPaymentInfo(this.paymentCode);'><%# DataBinder.Eval(Container.DataItem, "PaymentID")  %></a>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:BoundColumn DataField="PayDate" HeaderText="����ʱ��" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn>
											<asp:BoundColumn DataField="Money" HeaderText="�� ��" DataFormatString="{0:N}">
												<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
												<ItemStyle HorizontalAlign="Right"></ItemStyle>
												<FooterStyle HorizontalAlign="Right"></FooterStyle>
											</asp:BoundColumn>
										</Columns>
										<PagerStyle NextPageText="��ҳ&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;��ҳ" HorizontalAlign="Right"
											CssClass="list-title"></PagerStyle>
									</asp:datagrid>
								</td>
							</tr>
						</table>
						<table border="0" cellpadding="0" cellspacing="0" id="tableApply" width="100%" runat="server">
							<tr>
								<td>
									<table cellSpacing="0" cellPadding="0" border="0" width="100%">
										<tr>
											<td class="intopic" width="200">��ͬ����</td>
											<td></td>
										</tr>
									</table>
									<asp:datagrid id="dgApply" runat="server" Width="100%" AutoGenerateColumns="False" CssClass="list"
										PageSize="1" >
										<AlternatingItemStyle CssClass="AlterGridTr"></AlternatingItemStyle>
										<HeaderStyle CssClass="list-title"></HeaderStyle>
										<FooterStyle CssClass="list-title"></FooterStyle>
										<Columns>
											<asp:TemplateColumn HeaderText="���">
												<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
												<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
												<ItemTemplate>
													<%# Container.ItemIndex + 1 %>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="��ͬ����">
												<ItemTemplate>
													<a href=## contractCode='<%# DataBinder.Eval(Container.DataItem, "ContractCode")  %>' onclick='showContractInfo(contractCode);'><%# DataBinder.Eval(Container.DataItem, "ContractName")  %></a>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:BoundColumn DataField="ItemName" HeaderText="�� ��"></asp:BoundColumn>
											<asp:BoundColumn DataField="PlanningPayDate" HeaderText="Ԥ�Ƹ���ʱ��" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn>
											<asp:BoundColumn DataField="Money" HeaderText="�� ��" DataFormatString="{0:N}">
												<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
												<ItemStyle HorizontalAlign="Right"></ItemStyle>
												<FooterStyle HorizontalAlign="Right"></FooterStyle>
											</asp:BoundColumn>
										</Columns>
										<PagerStyle NextPageText="��ҳ&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;��ҳ" HorizontalAlign="Right"
											CssClass="list-title"></PagerStyle>
									</asp:datagrid>
								</td>
							</tr>
						</table>
						<table cellspacing="10" width="100%" id="tableButton">
							<tr>
								<td align="center">&nbsp; <input id="btnCancel" name="btnCancel" type="button" class="submit" value="�� ��" onclick="javascript:self.close()">
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
<script language=javascript>
<!--
	function showContractInfo( contractCode )
	{
		OpenFullWindow( '../Contract/ContractInfo.aspx?ContractCode=' + contractCode,'��ͬ��Ϣ' ) ;
	}

	function showPaymentInfo ( paymentCode )
	{
		OpenFullWindow( '../Finance/PaymentInfo.aspx?PaymentCode=' + paymentCode,'�����Ϣ' );
	}


//-->
</script>

</HTML>
