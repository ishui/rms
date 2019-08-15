<%@ Page language="c#" Inherits="RmsPM.Web.Finance.FinanceInterfaceAnalysisUserList" CodeFile="FinanceInterfaceAnalysisUserList.aspx.cs" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>人员财务编码</title>
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
	OpenCustomWindow("FinanceInterfaceAnalysisUserModify.aspx?UserCode=" + code + "&SubjectSetCode=" + Form1.txtSubjectSetCode.value, "人员财务编码修改", 300, 180);
}

function Import()
{
	OpenCustomWindow("FinanceInterfaceAnalysisUserImport.aspx?SubjectSetCode=" + Form1.txtSubjectSetCode.value, "人员财务编码导入", 400, 280);
}
	
//-->
		</script>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0" style="BORDER-RIGHT:0px">
		<form id="Form1" method="post" runat="server">
			<TABLE height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR height="100%">
					<td vAlign="top" class="table">
						<table width="100%" height="100%" border="0">
							<tr>
								<td>
									<table cellSpacing="0" cellPadding="0" border="0">
										<tr>
											<td class="intopic" width="200">人员财务编码</td>
											<td>&nbsp;<input class="button-small" onclick="Import()" type="button" value="导入财务编码"
													name="btnImport" id="btnImport" runat="server"></td>
										</tr>
									</table>
								</td>
							</tr>
							<tr height="100%">
								<td>
									<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%"><asp:datagrid id="dgList" runat="server" Width="100%" AutoGenerateColumns="False" PageSize="15"
											CellPadding="0" CssClass="list" ShowFooter="False">
											<HeaderStyle CssClass="list-title"></HeaderStyle>
											<FooterStyle CssClass="list-title"></FooterStyle>
											<Columns>
												<asp:TemplateColumn HeaderText="人员">
													<ItemTemplate>
														<a href="#" onclick="Modify(code); return false;" code='<%# DataBinder.Eval(Container, "DataItem.UserCode") %>'>
															<%# DataBinder.Eval(Container, "DataItem.UserName") %>
														</a>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="财务编码">
													<ItemTemplate>
														<%# DataBinder.Eval(Container, "DataItem.U8Code") %>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle NextPageText="下页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上页" HorizontalAlign="Right"
												CssClass="ListHeadTr"></PagerStyle>
										</asp:datagrid></div>
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
