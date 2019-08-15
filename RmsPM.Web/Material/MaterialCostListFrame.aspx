<%@ Page language="c#" Inherits="RmsPM.Web.Material.MaterialCostListFrame" CodeFile="MaterialCostListFrame.aspx.cs" %>
<%@ Register TagPrefix="cc1" Namespace="Tiannuo.Web.Controls" Assembly="DataGridWebControls" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>�۸����ݿ�</title>
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
												<asp:TemplateColumn HeaderText="����" SortExpression="GroupCode">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<div title='<%# RmsPM.BLL.SystemGroupRule.GetSystemGroupFullName ( RmsPM.BLL.ConvertRule.ToString(DataBinder.Eval(Container.DataItem, "GroupCode")) ) %>'>
															<%# RmsPM.BLL.SystemGroupRule.GetSystemGroupName(RmsPM.BLL.ConvertRule.ToString(DataBinder.Eval(Container.DataItem, "GroupCode")))%>
														</div>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="����" SortExpression="">
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
												<asp:TemplateColumn HeaderText="��λ<br>unit" SortExpression="Unit">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%# DataBinder.Eval(Container.DataItem, "Unit") %>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="����<br>rate" SortExpression="Price">
												    <HeaderStyle HorizontalAlign="right" />
												    <ItemStyle HorizontalAlign="right" />
													<ItemTemplate>
														<%# DataBinder.Eval(Container.DataItem, "Price", "{0:0.##}") %>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="��Ŀ<br>project" SortExpression="Project">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%#  DataBinder.Eval(Container.DataItem, "Project")%>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="����<br>date" SortExpression="BiddingDate">
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
											<PagerStyle Visible="False" NextPageText="��ҳ&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;��ҳ" HorizontalAlign="Right"
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
												��
												<asp:Label Runat="server" ID="lblRecordCount">0</asp:Label>
												��
											</td>
											<td><input style="" id="btnPrint" onclick="Print()" type="button" value="�� ӡ" name="btnPrint"	runat="server"></td>
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
		OpenLargeWindow('MaterialCostInfo.aspx?MaterialCostCode=' + code,"���ϼ۸���Ϣ");
	}
	
	function doNewMaterialCost(MaterialTypeCode)
	{
		OpenLargeWindow('MaterialCostModify.aspx?MaterialTypeCode=' + MaterialTypeCode ,"�������ϼ۸�")
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
	OpenPrintWindow("../Report/PrintList.aspx?FromControlID=dgList", "��ӡ");
}
	
//-->
		</script>
	</body>
</HTML>
