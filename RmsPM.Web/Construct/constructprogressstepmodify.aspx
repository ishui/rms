<%@ Page language="c#" Inherits="RmsPM.Web.Construct.ConstructProgressStepModify" CodeFile="ConstructProgressStepModify.aspx.cs" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>�����޸ģ��ֲ��ã�</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0" bgcolor="#ffffff">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">�����޸�</td>
				</tr>
				<tr height="100%">
					<td vAlign="top" class="topic">
						<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
							<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
								<tr vAlign="top">
									<td><asp:datagrid id="dgList" runat="server" Width="100%" CssClass="list" CellPadding="0" AllowSorting="True"
											AutoGenerateColumns="False" PageSize="15" ShowFooter="False" DataKeyField="ProgressStepCode">
											<ItemStyle CssClass=""></ItemStyle>
											<HeaderStyle CssClass="list-title"></HeaderStyle>
											<FooterStyle CssClass="list-title"></FooterStyle>
											<Columns>
												<asp:TemplateColumn HeaderText="�������">
													<ItemTemplate>
														<%# DataBinder.Eval(Container, "DataItem.VisualProgressName") %>
														<input type="hidden" name="txtVisualProgress" id="txtVisualProgress" runat="server" value='<%# DataBinder.Eval(Container, "DataItem.VisualProgress") %>'>
														<input type="hidden" name="txtVisualProgressName" id="txtVisualProgressName" runat="server" value='<%# DataBinder.Eval(Container, "DataItem.VisualProgressName") %>'>
														<input type="hidden" id="txtProgressType" name="txtProgressType" runat="server" value='<%# DataBinder.Eval(Container, "DataItem.ProgressType") %>'>
														<input type="hidden" id="txtIsPoint" name="txtIsPoint" runat="server" value='<%# DataBinder.Eval(Container, "DataItem.IsPoint") %>'>
													</ItemTemplate>
													<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
													<FooterStyle HorizontalAlign="Left"></FooterStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="�ƻ���ʼ����" Visible="False">
													<ItemTemplate>
														<%# DataBinder.Eval(Container, "DataItem.PStartDate", "{0:yyyy-MM-dd}") %>
													</ItemTemplate>
													<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
													<FooterStyle HorizontalAlign="Left"></FooterStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="�ƻ���������" Visible="False">
													<ItemTemplate>
														<div style='display: <%# RmsPM.BLL.ConvertRule.ToInt(DataBinder.Eval(Container, "DataItem.IsPoint")) == 1?"none":"block" %>'>
															<%# DataBinder.Eval(Container, "DataItem.PEndDate", "{0:yyyy-MM-dd}") %>
														</div>
													</ItemTemplate>
													<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
													<FooterStyle HorizontalAlign="Left"></FooterStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="ʵ�ʿ�ʼ����">
													<ItemTemplate>
														<cc3:calendar id="txtStartDate" runat="server" CalendarResource="../Images/CalendarResource/" ReadOnly="False" Display="True" value='<%# DataBinder.Eval(Container, "DataItem.StartDate", "{0:yyyy-MM-dd}") %>'>
														</cc3:calendar>
													</ItemTemplate>
													<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
													<FooterStyle HorizontalAlign="Left"></FooterStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="ʵ�ʽ�������">
													<ItemTemplate>
														<div style='display: <%# RmsPM.BLL.ConvertRule.ToInt(DataBinder.Eval(Container, "DataItem.IsPoint")) == 1?"none":"block" %>'>
															<cc3:calendar id="txtEndDate" runat="server" CalendarResource="../Images/CalendarResource/" ReadOnly="False" Display="True" value='<%# DataBinder.Eval(Container, "DataItem.EndDate", "{0:yyyy-MM-dd}") %>'>
															</cc3:calendar>
														</div>
													</ItemTemplate>
													<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
													<FooterStyle HorizontalAlign="Left"></FooterStyle>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle NextPageText="��ҳ&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;��ҳ" HorizontalAlign="Center"
												CssClass="ListHeadTr"></PagerStyle>
										</asp:datagrid></td>
								</tr>
							</table>
						</div>
					</td>
				</tr>
				<tr>
					<td>
						<table cellSpacing="10" width="100%">
							<tr>
								<td align="center"><input class="submit" id="btnSave" type="button" value="ȷ ��" name="btnSave" runat="server" onserverclick="btnSave_ServerClick">
									<input class="submit" id="btnCancel" onclick="javascript:self.close()" type="button" value="ȡ ��"
										name="btnCancel">
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server"> <input id="txtPBSUnitCode" type="hidden" name="txtPBSUnitCode" runat="server">
			<input id="txtAct" type="hidden" name="txtAct" runat="server">
		</form>
	</body>
</HTML>
