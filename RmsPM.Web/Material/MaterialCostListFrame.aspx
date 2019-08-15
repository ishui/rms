<%@ Page language="c#" Inherits="RmsPM.Web.Material.MaterialCostListFrame" CodeFile="MaterialCostListFrame.aspx.cs" %>
<%@ Register TagPrefix="cc1" Namespace="Tiannuo.Web.Controls" Assembly="DataGridWebControls" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>价格数据库</title>
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
								<td class="table" >
									<div style="OVERFLOW:auto;WIDTH:100%;POSITION:absolute;HEIGHT:100%" id="tdMaster">
										<asp:datagrid id="dgList" runat="server" AllowSorting="True" AutoGenerateColumns="False" PageSize="16"
											AllowPaging="False" GridLines="Horizontal" CellPadding="0" Width="100%" CssClass="list">
											<AlternatingItemStyle CssClass="list-i"></AlternatingItemStyle>
											<ItemStyle CssClass=""></ItemStyle>
											<HeaderStyle CssClass="list-title"></HeaderStyle>
											<FooterStyle CssClass="list-title"></FooterStyle>
											<Columns>
												<asp:TemplateColumn HeaderText="分类" SortExpression="GroupCode">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<div title='<%# RmsPM.BLL.SystemGroupRule.GetSystemGroupFullName ( RmsPM.BLL.ConvertRule.ToString(DataBinder.Eval(Container.DataItem, "GroupCode")) ) %>'>
															<%# RmsPM.BLL.SystemGroupRule.GetSystemGroupName(RmsPM.BLL.ConvertRule.ToString(DataBinder.Eval(Container.DataItem, "GroupCode")))%>
														</div>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="描述" SortExpression="">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Width="200"></ItemStyle>
													<ItemTemplate>
														<a href="##" onclick="selectMaterialCost(this.code);return false;" 
															code='<%#  DataBinder.Eval(Container.DataItem, "MaterialCostCode") %>'
															>
															<%# DataBinder.Eval(Container.DataItem, "Description")%>
														</a>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="单位<br>unit" SortExpression="Unit">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%# DataBinder.Eval(Container.DataItem, "Unit") %>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="单价<br>rate" SortExpression="Price">
												    <HeaderStyle HorizontalAlign="right" />
												    <ItemStyle HorizontalAlign="right" />
													<ItemTemplate>
														<%# DataBinder.Eval(Container.DataItem, "Price", "{0:0.##}") %>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="项目<br>project" SortExpression="Project">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%#  DataBinder.Eval(Container.DataItem, "Project")%>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="日期<br>date" SortExpression="BiddingDate">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%#  DataBinder.Eval(Container.DataItem, "BiddingDate", "{0:yyyy-MM-dd}")%>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="category" SortExpression="Category">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%# DataBinder.Eval(Container.DataItem, "Category")%>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="description" SortExpression="">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="True"></ItemStyle>
													<ItemTemplate>
														<%# DataBinder.Eval(Container.DataItem, "DescriptionEn")%>
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
									<cc1:GridPagination id="GridPagination1" runat="server" ControlSourceUrl="../Images/GridPaginationSource/" DataGridId="dgList" onpageindexchange="GridPagination1_PageIndexChange" IsPrintList="true"></cc1:GridPagination>
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
	function doViewMaterialCostInfo(code)
	{
		OpenLargeWindow('MaterialCostInfo.aspx?MaterialCostCode=' + code,"材料价格信息");
	}
	
	function doNewMaterialCost(MaterialTypeCode)
	{
		OpenLargeWindow('MaterialCostModify.aspx?MaterialTypeCode=' + MaterialTypeCode ,"新增材料价格")
	}
	
	function selectMaterialCost (code)
	{
	/*
		if ( from == "Select" )
		{
			window.parent.DoSelectMaterial(code);
		}
		else
		*/
			doViewMaterialCostInfo(code);
	}
	function Print()
{
	OpenPrintWindow("../Report/PrintList.aspx?FromControlID=dgList", "打印");
}
	
//-->
		</script>
	</body>
</HTML>
