<%@ Page language="c#" Inherits="RmsPM.Web.Contract.ContractChangingHistory" CodeFile="ContractChangingHistory.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>合同变更记录</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">合同变更记录</td>
				</tr>
				<tr>
					<td class="table" vAlign="top">
						<asp:datagrid id="dgList" runat="server" AllowSorting="True" AutoGenerateColumns="False" PageSize="15"
							AllowPaging="false" GridLines="Horizontal" CellPadding="0" Width="100%" CssClass="list">
							<AlternatingItemStyle CssClass="list-i"></AlternatingItemStyle>
							<ItemStyle CssClass=""></ItemStyle>
							<HeaderStyle CssClass="list-title"></HeaderStyle>
							<FooterStyle CssClass="list-title"></FooterStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="合同名称">
									<ItemTemplate>
										<a href="##" onclick="doViewContractInfo(this.code);return false;" code='<%#  DataBinder.Eval(Container.DataItem, "ContractCode") %>'>
											<%#  DataBinder.Eval(Container.DataItem, "ContractName") %>
										</a>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="ContractID" HeaderText="合同编号"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="合同类型">
									<ItemTemplate>
										<%# RmsPM.BLL.ContractRule.GetContractTypeName(  DataBinder.Eval(Container.DataItem, "Type").ToString() )%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="当前状态">
									<HeaderStyle Wrap="False"></HeaderStyle>
									<ItemStyle Wrap="False"></ItemStyle>
									<ItemTemplate>
										<%# RmsPM.BLL.ContractRule.GetContractStatusName(  DataBinder.Eval(Container.DataItem, "Status").ToString()) %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn>
									<HeaderTemplate>
										单位
									</HeaderTemplate>
									<ItemTemplate>
										<%# RmsPM.BLL.SystemRule.GetUnitName( DataBinder.Eval( Container,"DataItem.UnitCode" ).ToString() ) %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn>
									<HeaderTemplate>
										供应商
									</HeaderTemplate>
									<ItemTemplate>
										<%# RmsPM.BLL.ProjectRule.GetSupplierName( DataBinder.Eval( Container,"DataItem.SupplierCode" ).ToString() ) %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="TotalMoney" HeaderText="合同总金额（元）" DataFormatString="{0:N}">
									<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<FooterStyle HorizontalAlign="Right"></FooterStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="变更申请人">
									<ItemTemplate>
										<%# RmsPM.BLL.SystemRule.GetUserName(  DataBinder.Eval(Container.DataItem, "ChangePerson").ToString() )%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="ChangeDate" HeaderText="变更日期" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn>
							</Columns>
							<PagerStyle NextPageText="下页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上页" HorizontalAlign="Right"
								CssClass="ListHeadTr"></PagerStyle>
						</asp:datagrid>
					</td>
				</tr>
				<tr>
					<td height="12">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td background="../images/corl_bg.gif"><IMG height="12" src="../images/corl.gif" width="12"></td>
								<td width="12"><IMG height="12" src="../images/corr.gif" width="12"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td bgColor="#e4eff6" height="6"></td>
				</tr>
			</table>
		</form>
		<script language="javascript">

	function doViewContractInfo( code )
	{
		window.navigate('ContractInfo.aspx?ProjectCode=<%=Request["ProjectCode"]%>&ContractCode=' + code,'合同信息');
	}

		</script>
		</TR></TBODY></TABLE>
	</body>
</HTML>
