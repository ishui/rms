<%@ Register TagPrefix="uc1" TagName="AttachMentAdd" Src="../UserControls/AttachMentAdd.ascx" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Page language="c#" Inherits="RmsPM.Web.Construct.ConstructProgressModify" CodeFile="ConstructProgressModify.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>���ȱ����޸�</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" bgColor="#ffffff" border="0">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">���ȱ���</td>
				</tr>
				<tr>
					<td class="table" vAlign="top" align="center">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="note">��λ���̣�<asp:label id="lblPBSUnitName" Runat="server"></asp:label></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr height="100%">
					<td vAlign="top">
						<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
							<table class="form" cellSpacing="0" cellPadding="0" width="100%" border="0">
								<TR>
									<TD class="form-item" width="100">�������ڣ�</TD>
									<TD><cc3:calendar id="txtReportDate" runat="server" Value="" Display="True" ReadOnly="False" CalendarResource="../Images/CalendarResource/"></cc3:calendar><font color="red">*</font></TD>
									<TD class="form-item" width="100">�����ˣ�</TD>
									<TD><asp:label id="lblReportPersonName" Runat="server"></asp:label></TD>
								</TR>
							</table>
							<br>
							<table cellSpacing="0" cellPadding="0" border="0">
								<tr>
									<td class="intopic">���ȱ���</td>
								</tr>
							</table>
							<table class="form" cellSpacing="0" cellPadding="0" width="100%" border="0">
								<tr>
									<TD class="form-item" width="100">��ǰ������ȣ�</TD>
									<TD><SELECT class="select" id="sltVisualProgress" name="sltVisualProgress" runat="server">
											<OPTION value="" selected>----��ѡ��----</OPTION>
										</SELECT><font color="red">*</font></TD>
									<td class="form-item">Ŀǰʩ��������</td>
									<td><input class="input-nember" id="txtCurrentLayer" type="text" size="14" name="txtCurrentLayer"
											runat="server"></td>
								</tr>
								<TR>
									<TD class="form-item" align="right">������</TD>
									<TD colSpan="3"><uc1:attachmentadd id="myAttachMentAdd" runat="server"></uc1:attachmentadd></TD>
								</TR>
								<TR>
									<TD class="form-item">���ݣ�</TD>
									<TD noWrap colSpan="3"><textarea class="textarea" id="txtContent" style="WIDTH: 100%" name="txtContent" rows="4"
											runat="server"></textarea></TD>
								</TR>
							</table>
							<br>
							<table cellSpacing="0" cellPadding="0" border="0">
								<tr>
									<td class="intopic">���ձ���</td>
								</tr>
							</table>
							<table cellSpacing="0" cellPadding="0" width="100%" border="0">
								<tr vAlign="top">
									<td><asp:datagrid id="dgRisk" runat="server" DataKeyField="ProgressRiskCode" ShowFooter="False" PageSize="15"
											AutoGenerateColumns="False" AllowSorting="True" CellPadding="0" CssClass="list" Width="100%">
											<ItemStyle CssClass=""></ItemStyle>
											<HeaderStyle CssClass="list-title"></HeaderStyle>
											<FooterStyle CssClass="list-title"></FooterStyle>
											<Columns>
												<asp:TemplateColumn HeaderText="��������">
													<ItemTemplate>
														<%# DataBinder.Eval(Container, "DataItem.RiskTypeName") %>
														<input type="hidden" name="txtRiskTypeName" id="txtRiskTypeName" runat="server" value='<%# DataBinder.Eval(Container, "DataItem.RiskTypeName") %>'>
													</ItemTemplate>
													<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
													<FooterStyle HorizontalAlign="Left"></FooterStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="����ָ��">
													<ItemTemplate>
														<asp:RadioButtonList Runat="server" ID="rdoRiskIndexCode" DataSource="<%# GetRiskIndexDataSource() %>" DataTextField="IndexName" DataValueField="IndexCode" RepeatColumns="20">
														</asp:RadioButtonList>
														<input type="hidden" name="txtRiskIndexCode" id="txtRiskIndexCode" runat="server" value='<%# DataBinder.Eval(Container, "DataItem.RiskIndexCode") %>'>
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
							<table class="form" cellSpacing="0" cellPadding="0" width="100%" border="0">
								<TR>
									<TD class="form-item" width="100">����������</TD>
									<TD noWrap><textarea class="textarea" id="txtRiskRemark" style="WIDTH: 100%" name="txtContent" rows="4"
											runat="server"></textarea></TD>
								</TR>
							</table>
							<br>
							<table cellSpacing="0" cellPadding="0" border="0">
								<tr>
									<td class="intopic">�������</td>
								</tr>
							</table>
							<table cellSpacing="0" cellPadding="0" width="100%" border="0">
								<tr vAlign="top">
									<td><asp:datagrid id="dgList" runat="server" DataKeyField="ProgressStepCode" ShowFooter="False" PageSize="15"
											AutoGenerateColumns="False" AllowSorting="True" CellPadding="0" CssClass="list" Width="100%">
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
												<asp:TemplateColumn HeaderText="�ƻ���ʼ����">
													<ItemTemplate>
														<%# DataBinder.Eval(Container, "DataItem.PStartDate", "{0:yyyy-MM-dd}") %>
													</ItemTemplate>
													<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
													<FooterStyle HorizontalAlign="Left"></FooterStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="�ƻ���������">
													<ItemTemplate>
														<div style='display: <%# RmsPM.BLL.ConvertRule.ToInt(DataBinder.Eval(Container, "DataItem.IsPoint")) == 1?"none":"block" %>'>
															<%# DataBinder.Eval(Container, "DataItem.PEndDate", "{0:yyyy-MM-dd}") %>
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
			<input id="txtProgressCode" type="hidden" name="txtProgressCode" runat="server"><input id="txtReportPersonCode" type="hidden" name="txtReportPersonCode" runat="server">
			<input id="txtAct" type="hidden" name="txtAct" runat="server">
		</form>
		<SCRIPT language="javascript">
<!--
//-->
		</SCRIPT>
	</body>
</HTML>
