<%@ Page language="c#" Inherits="RmsPM.Web.Material.SupplierMaterialListFrame" CodeFile="SupplierMaterialListFrame.aspx.cs" %>
<%@ Register TagPrefix="cc1" Namespace="Tiannuo.Web.Controls" Assembly="DataGridWebControls" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>厂商材料库</title>
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
									<div style="OVERFLOW:auto;WIDTH:100%;POSITION:absolute;HEIGHT:100%">
										<asp:datagrid id="dgList" runat="server" AllowSorting="True" AutoGenerateColumns="False" PageSize="16"
											AllowPaging="False" GridLines="Horizontal" CellPadding="0" Width="100%" CssClass="list">
											<AlternatingItemStyle CssClass="list-i"></AlternatingItemStyle>
											<ItemStyle CssClass=""></ItemStyle>
											<HeaderStyle CssClass="list-title"></HeaderStyle>
											<FooterStyle CssClass="list-title"></FooterStyle>
											<Columns>
												<asp:TemplateColumn HeaderText="材料类型" SortExpression="GroupCode">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<a href="##" onclick="selectSupplierMaterial(this.code);return false;" 
															code='<%#  DataBinder.Eval(Container.DataItem, "SupplierMaterialCode") %>'
															>
														<div title='<%# RmsPM.BLL.SystemGroupRule.GetSystemGroupFullName ( DataBinder.Eval(Container.DataItem, "GroupCode").ToString() ) %>'>
															<%# RmsPM.BLL.SystemGroupRule.GetSystemGroupName(DataBinder.Eval(Container.DataItem, "GroupCode").ToString())%>
														</div>
														</a>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="厂家" SortExpression="SupplierName">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<a href="##" onclick="doViewSupplierInfo(this.code);return false;" 
															code='<%#  DataBinder.Eval(Container.DataItem, "SupplierCode") %>'
															>
															<%# RmsPM.BLL.StringRule.TruncateString(DataBinder.Eval(Container.DataItem, "SupplierName"), 20)%>
														</a>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="品牌" SortExpression="Brand">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%#  DataBinder.Eval(Container.DataItem, "Brand")%>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="型号" SortExpression="Model">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%#  DataBinder.Eval(Container.DataItem, "Model")%>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="规格" SortExpression="Spec">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%#  DataBinder.Eval(Container.DataItem, "Spec")%>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="进口/国产" SortExpression="Nation">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%# DataBinder.Eval(Container.DataItem, "Nation")%>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="产地" SortExpression="AreaCode">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%# DataBinder.Eval(Container.DataItem, "AreaCode")%>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="样品序号" SortExpression="SampleID">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%# DataBinder.Eval(Container.DataItem, "SampleID")%>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="单位" SortExpression="Unit">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%# DataBinder.Eval(Container.DataItem, "Unit") %>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="单价" SortExpression="Price">
												    <HeaderStyle HorizontalAlign="right" />
												    <ItemStyle HorizontalAlign="right" />
													<ItemTemplate>
														<%# DataBinder.Eval(Container.DataItem, "Price", "{0:0.##}") %>
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
									<table width="100%" class="list">
										<tr class="list-title">
											<td>
												共
												<asp:Label Runat="server" ID="lblRecordCount">0</asp:Label>
												条
											</td>
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
    		<input id="txtRootGroupCode" type="hidden" name="txtRootGroupCode" runat="server">
		</form>
		<script language="javascript">
<!--
	
	var from = '<%=Request["From"]%>' ;
	function doViewSupplierMaterialInfo(code)
	{
		OpenLargeWindow('SupplierMaterialInfo.aspx?SupplierMaterialCode=' + code,"厂商材料信息");
	}
	
	function doNewSupplierMaterial(MaterialTypeCode)
	{
		OpenLargeWindow('SupplierMaterialModify.aspx?MaterialTypeCode=' + MaterialTypeCode ,"新增厂商材料")
	}
	
	function selectSupplierMaterial (code)
	{
	/*
		if ( from == "Select" )
		{
			window.parent.DoSelectMaterial(code);
		}
		else
		*/
			doViewSupplierMaterialInfo(code);
	}
	
	function doViewSupplierInfo(code)
	{
		OpenFullWindow('../Supplier/SupplierInfo.aspx?SupplierCode=' + code,"厂商信息");
	}

//-->
		</script>
	</body>
</HTML>
