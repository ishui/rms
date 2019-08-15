<%@ Page language="c#" Inherits="RmsPM.Web.Finance.FinanceInterfaceAnalysisSupplierList" CodeFile="FinanceInterfaceAnalysisSupplierList.aspx.cs" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Register TagPrefix="cc1" Namespace="Tiannuo.Web.Controls" Assembly="DataGridWebControls" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>厂商财务编码</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<SCRIPT language="javascript" src="../images/checkbox.js"></SCRIPT>
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<script language="javascript">
<!--

function Modify(code)
{
    OpenCustomWindow("FinanceInterfaceAnalysisSupplierModify.aspx?SupplierCode=" + code + "&SubjectSetCode=" + Form1.txtSubjectSetCode.value, "修改厂商财务编码", 700, 500);
}

function Refresh()
{
    Form1.btnRefresh.click();
}
	
//-->
		</script>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0" style="BORDER-RIGHT:0px">
		<form id="Form1" method="post" runat="server">
			<TABLE height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
			    <div style="display:none"><input type="button" runat="server" value="btnRefresh" id="btnRefresh" name="btnRefresh" onserverclick="btnRefresh_ServerClick" /></div>
				<TR height="100%">
					<td vAlign="top" class="table">
						<table width="100%" height="100%" border="0">
							<tr>
								<td>
									<table cellSpacing="0" cellPadding="0" border="0">
										<tr>
											<td class="note" width="200">厂商财务编码</td>
											<td>&nbsp;<input style="display:none" class="button-small" onclick="Import():" type="button" value="导入财务编码" name="btnImport"
													id="btnImport" runat="server"></td>
										</tr>
									</table>
								</td>
							</tr>
				            <tr>
					            <td vAlign="top">
						            <table cellSpacing="0" cellPadding="0" width="100%" border="0" class="search-area">
							            <tr>
								            <td>
									            <table>
										            <tr>
											            <td nowrap>厂商名称：<input id="txtSupplierName" type="text" size="20" name="txtSupplierName" runat="server"
													            class="input">
                                                                &nbsp;&nbsp;项目：<SELECT class="select" id="sltProject" name="sltProject" runat="server">
													            <option selected value="">--请选择--</option>
												            </SELECT>
												            &nbsp;&nbsp;财务编码：<input id="txtU8Code" type="text" size="10" name="txtU8Code" runat="server"
													            class="input">
											                &nbsp;&nbsp;<input class="submit" id="btnSearch" type="button" value="搜 索" name="btnSearch" runat="server" onserverclick="btnSearch_ServerClick"></td>
										            </tr>
									            </table>
								            </td>
							            </tr>
						            </table>
					            </td>
				            </tr>
				            <tr height="100%">
								<td>
									<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%"><asp:datagrid id="dgList" runat="server" Width="100%" AutoGenerateColumns="False" PageSize="15"
											CellPadding="0" CssClass="list" ShowFooter="False" AllowPaging="true" AllowSorting="true">
											<HeaderStyle CssClass="list-title"></HeaderStyle>
											<FooterStyle CssClass="list-title"></FooterStyle>
											<Columns>
												<asp:TemplateColumn HeaderText="厂商名称" SortExpression="SupplierName">
													<ItemTemplate>
														<a href="#" onclick="Modify(code); return false;" code='<%# DataBinder.Eval(Container, "DataItem.SupplierCode") %>'>
															<%# DataBinder.Eval(Container, "DataItem.SupplierName") %>
														</a>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="类型" SortExpression="SupplierTypeName">
													<ItemTemplate>
														<%# DataBinder.Eval(Container, "DataItem.SupplierTypeName") %>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="项目名称" SortExpression="ProjectName">
													<ItemTemplate>
														<%# RmsPM.BLL.ConvertRule.ToString(DataBinder.Eval(Container, "DataItem.ProjectCode"))==""?"集团":DataBinder.Eval(Container, "DataItem.ProjectName") %>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="财务编码" SortExpression="U8Code">
													<ItemTemplate>
														<%# DataBinder.Eval(Container, "DataItem.U8Code") %>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle Visible="false" NextPageText="下页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上页" HorizontalAlign="Right"
												CssClass="ListHeadTr"></PagerStyle>
										</asp:datagrid></div>
								</td>
							</tr>
							<tr>
								<td>
									<cc1:GridPagination id="GridPagination1" runat="server" ControlSourceUrl="../Images/GridPaginationSource/" DataGridId="dgList" onpageindexchange="GridPagination1_PageIndexChange"></cc1:GridPagination>
								</td>
							</tr>
						</table>
					</td>
				</TR>
			</TABLE>
			<input type="hidden" runat="server" id="txtSubjectSetCode" name="txtSubjectSetCode">
		</form>
	</body>
</HTML>
