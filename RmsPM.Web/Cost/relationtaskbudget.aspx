<%@ Page language="c#" Inherits="RmsPM.Web.Cost.RelationTaskBudget" CodeFile="RelationTaskBudget.aspx.cs" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>��ع���Ԥ��</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
	</HEAD>
	<body scroll="no">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0" bgcolor="white">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">��ع���Ԥ��-
						<asp:Label id="lblCostName" runat="server"></asp:Label></td>
				</tr>
				<tr>
					<td class="topic" vAlign="top" align="center">
						<asp:datagrid id="dgTaskBudget" runat="server" Width="100%" AutoGenerateColumns="False" CssClass="list"
							PageSize="1" DataKeyField="WBSCode">
							<AlternatingItemStyle CssClass="AlterGridTr"></AlternatingItemStyle>
							<HeaderStyle CssClass="list-title"></HeaderStyle>
							<FooterStyle CssClass="list-title"></FooterStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="���">
									<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<%# Container.ItemIndex + 1 %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="ItemName" HeaderText="�� ��"></asp:BoundColumn>
								<asp:BoundColumn DataField="PlanningPayDate" HeaderText="����ʱ��" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="������">
									<ItemStyle></ItemStyle>
									<ItemTemplate>
										<%#  DataBinder.Eval(Container.DataItem, "TaskName")  %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="Money" HeaderText="�� ��" DataFormatString="{0:N}">
									<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<FooterStyle HorizontalAlign="Right"></FooterStyle>
								</asp:BoundColumn>
							</Columns>
							<PagerStyle NextPageText="��ҳ&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;��ҳ" HorizontalAlign="Right"
								CssClass="list-title"></PagerStyle>
						</asp:datagrid>
						<table cellspacing="10" width="100%" id="tableButton">
							<tr>
								<td align="center">&nbsp; <input id="btnCancel" name="btnCancel" type="button" class="submit" value="�� ��" onclick="javascript:self.close()">
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
		<script language="javascript">
<!--

		
//-->
		</script>
	</body>
</HTML>
