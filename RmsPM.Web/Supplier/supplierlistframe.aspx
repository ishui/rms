<%@ Page language="c#" Inherits="RmsPM.Web.Supplier.SupplierListFrame" CodeFile="SupplierListFrame.aspx.cs" %>
<%@ Register TagPrefix="cc1" Namespace="Tiannuo.Web.Controls" Assembly="DataGridWebControls" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>厂商列表</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
	</HEAD>
	<body bgcolor="#ffffff" style="BORDER-RIGHT:0px">
		<form id="Form1" method="post" runat="server">
			<table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="#ffffff">
				<tr height="100%">
					<td>
						<table width="100%" height="100%">
							<tr height="100%">
								<td>
									<div style="OVERFLOW:auto;WIDTH:100%;POSITION:absolute;HEIGHT:100%" id="tdMaster">
										<asp:datagrid id="dgList" runat="server" AllowSorting="True" AutoGenerateColumns="False" PageSize="16"
											AllowPaging="False" GridLines="Horizontal" CellPadding="0" Width="100%" CssClass="list">
											<AlternatingItemStyle CssClass="list-i"></AlternatingItemStyle>
											<ItemStyle CssClass=""></ItemStyle>
											<HeaderStyle CssClass="list-title"></HeaderStyle>
											<FooterStyle CssClass="list-title"></FooterStyle>
											<Columns>
												<asp:TemplateColumn HeaderText="厂商名称" SortExpression="SupplierName">
												<ItemStyle width="200"></ItemStyle>
													<ItemTemplate>
														<a href="##" onclick="selectSupplier(this.code,this.supplierName,this.ContractPerson,this.OfficePhone,this.WorkAddress,this.OpenBank,this.Account,this.Reciver);return false;" 
															code='<%#  DataBinder.Eval(Container.DataItem, "SupplierCode") %>'
															supplierName='<%#  DataBinder.Eval(Container.DataItem, "SupplierName") %>'
															ContractPerson='<%#  DataBinder.Eval(Container.DataItem, "ContractPerson") %>'
															OfficePhone='<%#  DataBinder.Eval(Container.DataItem, "OfficePhone") %>'
															WorkAddress='<%#  DataBinder.Eval(Container.DataItem, "WorkAddress") %>'
															OpenBank='<%#Eval("OpenBank") %>'
															Account='<%#Eval("Account") %>'
															Reciver='<%#Eval("Reciver") %>'
															>
															<%#  DataBinder.Eval(Container.DataItem, "SupplierName") %>
														</a>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="厂商类型" SortExpression="SupplierTypeCode">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<div title='<%# RmsPM.BLL.SystemGroupRule.GetSystemGroupFullName ( DataBinder.Eval(Container.DataItem, "SupplierTypeCode").ToString() ) %>'>
															<%# RmsPM.BLL.SystemGroupRule.GetSystemGroupName ( DataBinder.Eval(Container.DataItem, "SupplierTypeCode").ToString() ) %>
														</div>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="地区" SortExpression="AreaCode">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%#  DataBinder.Eval(Container.DataItem, "AreaCode") %>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="资质" SortExpression="Quality">
													<ItemTemplate>
														<%#  RmsPM.BLL.StringRule.TruncateString(DataBinder.Eval(Container.DataItem, "Quality").ToString(),30) %>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle Visible="False" NextPageText="下一页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上一页" HorizontalAlign="Right"
												CssClass="ListHeadTr"></PagerStyle>
										</asp:datagrid>
									</div>
								</td>
							</tr>
							<tr>
								<td>
									<table width="100%" class="list">
										<tr class="list-title">
											<td width="100">
												共
												<asp:Label Runat="server" ID="lblRecordCount">0</asp:Label>
												条
											</td>
											<td><input style="" id="btnPrint" onclick="Print()" type="button" value="打 印" name="btnPrint"	runat="server"></td>
										
										</tr>
									</table>
								</td>
							</tr>
							<tr style="DISPLAY:none">
								<td>
									<cc1:GridPagination id="GridPagination1" runat="server" ControlSourceUrl="../Images/GridPaginationSource/" DataGridId="dgList" onpageindexchange="GridPagination1_PageIndexChange"></cc1:GridPagination>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
		<script language="javascript">
//alert('<%=Request["supplierTypeCode"]%>');
<!--
	
	var from = '<%=Request["From"]%>' ;
	function doViewSupplierInfo(code)
	{
		OpenFullWindow('../Supplier/SupplierInfo.aspx?SupplierCode=' + code,"厂商信息");
	}
	
	function doNewSupplier(supplierTypeCode)
	{
		OpenLargeWindow('SupplierModify.aspx?supplierTypeCode=' + supplierTypeCode ,"新增厂商")
	}
	
	function selectSupplier (code,name,ContractPerson,OfficePhone,WorkAddress,bank,account,reciver)
	{
		if ( from == "Select" )
		{
		    if('<%=Request["HelpPage"]%>' == "PayMent")
		    {
//		    window.alert(bank);
		    window.parent.JustForContratPayment(code,name,bank,account,reciver)
		        
		    }
		    else
		    {
			    window.parent.DoSelectSupplier(code,name,ContractPerson,OfficePhone,WorkAddress);
		    }
		}
		else
			doViewSupplierInfo(code);
	}
	function Print()
{
	OpenPrintWindow("../Report/PrintList.aspx?FromControlID=dgList$tdList1$tbl-container&css=" + escape("../CostBudget/CostBudget.css"), "打印");
}
	
//-->
		</script>
	</body>
</HTML>
