<%@ Register TagPrefix="cc2" Namespace="RmsPM.WebControls.ToolsBar" Assembly="RmsPM.WebControls" %>
<%@ Register TagPrefix="cc1" Namespace="ZL.WebControls.DateTimeBox" Assembly="ZL.WebControls.DateTimeBox" %>
<%@ Page language="c#" CodeFile="WBSRelatedTask.aspx.cs" AutoEventWireup="false" Inherits="RmsPM.Web.Project.WBSRelatedTask" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>����������</title>
		<META http-equiv="Content-Type" content="text/html; charset=gb2312">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/Style.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD vAlign="top" align="left">
						<TABLE id="Table2" borderColor="#94b6f7" height="23" cellSpacing="0" cellPadding="0" width="100%"
							background="../Images/ToolsBarBg.gif" border="1">
							<TR>
								<TD align="center" class="TableToolBar">�����ع�����</TD>
							</TR>
						</TABLE>
						<asp:datagrid id="dgTaskList" runat="server" CssClass="Table" Width="100%" AllowPaging="True"
							CellPadding="2" GridLines="Horizontal" AllowSorting="True" AutoGenerateColumns="False" PageSize="8"
							DataKeyField="WBSCode">
							<AlternatingItemStyle CssClass="AlterGridTr"></AlternatingItemStyle>
							<ItemStyle CssClass="ListBodyTr"></ItemStyle>
							<HeaderStyle CssClass="ListHeadTr"></HeaderStyle>
							<FooterStyle CssClass="ListHeadTr"></FooterStyle>
							<Columns>
								<asp:HyperLinkColumn DataNavigateUrlField="WBSCode" DataNavigateUrlFormatString="javascript:OpenWBS({0}"
									DataTextField="TaskName" HeaderText="����������"></asp:HyperLinkColumn>
								<asp:BoundColumn DataField="WBSCode" HeaderText="���������"></asp:BoundColumn>
								<asp:BoundColumn DataField="PlannedStartDate" HeaderText="�ƻ���ʼʱ��" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn>
								<asp:BoundColumn DataField="PlannedFinishDate" HeaderText="�ƻ�����ʱ��" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="ѡ��">
									<ItemTemplate>
										<asp:CheckBox ID="checkContract" Runat="server"></asp:CheckBox>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle NextPageText="��ҳ&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;��ҳ" HorizontalAlign="Right"
								CssClass="ListHeadTr"></PagerStyle>
						</asp:datagrid>
					</TD>
				</TR>
			</TABLE>
			</TD></TR></TBODY></TABLE>
			<table align="center">
				<tr align="center" width="100%">
					<td class="ToolBarButton"><cc2:toolsbutton id="SaveToolsButton" runat="server" IsServerEvent="True" ImageUrl="../Images/ToolsItemSave.gif">����&nbsp;</cc2:toolsbutton></td>
					<td class="ToolBarButton"><cc2:toolsbutton id="CancelToolsButton" runat="server" Event="doCancel();">ȡ��&nbsp;</cc2:toolsbutton></td>
				</tr>
			</table>
		</form>
		<table width="100%" background="../Images/ToolsBarBg.gif">
			<tr>
				<td height="30" align="center" class="TableToolBar"><a href="javascript:self.close()" class="close">�رձ�����</a></td>
			</tr>
		</table>
		<script language="javascript">
<!--
	function OpenWBS(WBSCode)
	{
		OpenCustomWindow("WBSInfo.aspx?WBSCode="+WBSCode,"",900,600);
	}
-->

		</script>
	</body>
</HTML>
