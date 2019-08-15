<%@ Page language="c#" CodeFile="WBSDocument.aspx.cs" AutoEventWireup="false" Inherits="RmsPM.Web.Project.WBSDocument" %>
<%@ Register TagPrefix="cc1" Namespace="ZL.WebControls.DateTimeBox" Assembly="ZL.WebControls.DateTimeBox" %>
<%@ Register TagPrefix="cc2" Namespace="RmsPM.WebControls.ToolsBar" Assembly="RmsPM.WebControls" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>添加附件</title>
		<META http-equiv="Content-Type" content="text/html; charset=gb2312">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/Style.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript">
			function doCancel(){
				window.parent.frames.cancelShow();
			}
			

		</SCRIPT>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD vAlign="top" align="left">
						<TABLE id="Table2" borderColor="#94b6f7" height="23" cellSpacing="0" cellPadding="0" width="100%"
							background="../Images/ToolsBarBg.gif" border="1">
							<TR>
								<TD align="center" class="TableToolBar">添加附件</TD>
							</TR>
						</TABLE>
						<asp:datagrid id="dgDocumentList" runat="server" CssClass="Table" Width="100%" AllowPaging="True"
							CellPadding="2" GridLines="Horizontal" AllowSorting="True" AutoGenerateColumns="False" PageSize="8">
							<AlternatingItemStyle CssClass="AlterGridTr"></AlternatingItemStyle>
							<ItemStyle CssClass="ListBodyTr"></ItemStyle>
							<HeaderStyle CssClass="ListHeadTr"></HeaderStyle>
							<FooterStyle CssClass="ListHeadTr"></FooterStyle>
							<Columns>
								<asp:BoundColumn DataField="Title" HeaderText="文档标题"></asp:BoundColumn>
								<asp:BoundColumn DataField="Author" HeaderText="作者"></asp:BoundColumn>
								<asp:BoundColumn DataField="CreateDate" HeaderText="创建时间"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="选择">
									<ItemTemplate>
										<asp:CheckBox id="checkDocument" Runat="server"></asp:CheckBox>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle NextPageText="下页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上页" HorizontalAlign="Right"
								CssClass="ListHeadTr"></PagerStyle>
						</asp:datagrid>
					</TD>
				</TR>
			</TABLE>

			<table align="center">
				<tr align="center" width="100%">
					<td class="ToolBarButton"><cc2:toolsbutton id="SaveToolsButton" runat="server" IsServerEvent="True" ImageUrl="../Images/ToolsItemSave.gif">保存&nbsp;</cc2:toolsbutton></td>
					<td class="ToolBarButton"><cc2:toolsbutton id="CancelToolsButton" runat="server" Event="doCancel();">取消&nbsp;</cc2:toolsbutton></td>
				</tr>
			</table>
			<table width="100%" background="../Images/ToolsBarBg.gif">
				<tr>
					<td height="30" align="center" class="TableToolBar"><a href="javascript:self.close()" class="close">关闭本窗口</a></td>
				</tr>
			</table>
		</form>
		&nbsp;
	</body>
</HTML>
