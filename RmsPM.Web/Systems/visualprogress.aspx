<%@ Register TagPrefix="cc2" Namespace="RmsPM.WebControls.ToolsBar" Assembly="RmsPM.WebControls" %>
<%@ Page language="c#" Inherits="RmsPM.Web.Systems.VisualProgress" CodeFile="VisualProgress.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>形象进度</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0" scroll="no">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td bgColor="#e4eff6" height="6"></td>
				</tr>
				<tr>
					<td height="25">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="topic" background="../images/topic_bg.gif"><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle">系统管理 
									- 形象进度</td>
								<td width="9"><img src="../images/topic_corr.gif" width="9" height="25"></td>
							</tr>
						</table>
					</td>
				</tr>
				<TR>
					<TD class="tools-area" valign="top">
						<IMG src="../images/btn_li.gif" align="absMiddle"> <input class="button" id="btnAdd" onclick="Add();" type="button" value="新增形象进度" name="btnAdd"
							runat="server">
					</TD>
				</TR>
				<tr height="100%">
					<td class="table" valign="top">
						<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
							<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0" id="tbList"
								runat="server">
								<tr vAlign="top">
									<td><asp:datagrid id="dgList" runat="server" PageSize="15" AutoGenerateColumns="False" AllowSorting="True"
											CellPadding="0" CssClass="list" Width="100%">
											<ItemStyle CssClass=""></ItemStyle>
											<HeaderStyle CssClass="list-title"></HeaderStyle>
											<FooterStyle CssClass="list-title"></FooterStyle>
											<Columns>
												<asp:TemplateColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" HeaderText="形象进度">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<a href="#" onclick="Modify(this.Code);return false;" Code='<%#  DataBinder.Eval(Container.DataItem, "SystemID") %>'><%#  DataBinder.Eval(Container.DataItem, "VisualProgress") %></a>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="进度类型">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%# DataBinder.Eval(Container.DataItem, "ProgressTypeName") %>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="时间类型">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%# DataBinder.Eval(Container.DataItem, "IsPointName") %>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="投资比例">
													<HeaderStyle Wrap="False" HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<%# RmsPM.BLL.StringRule.AddUnit(RmsPM.BLL.MathRule.GetDecimalShowString(DataBinder.Eval(Container.DataItem, "InvestPercent")), "%") %>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="排序号">
													<HeaderStyle Wrap="False" HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<%# DataBinder.Eval(Container.DataItem, "SortID") %>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle NextPageText="下一页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上一页" HorizontalAlign="Center"
												CssClass="ListHeadTr"></PagerStyle>
										</asp:datagrid></td>
								</tr>
							</table>
						</div>
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
<!--
		function Add()
		{
			OpenCustomWindow("VisualProgressModify.aspx", "形象进度", 360, 250);
		}
	
		function Modify( code )
		{
			OpenCustomWindow("VisualProgressModify.aspx?SystemID=" + escape(code), "形象进度", 360, 250);
		}

	
//-->
		</script>
	</body>
</HTML>
