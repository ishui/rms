<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Page language="c#" Inherits="RmsPM.Web.Construct.AnnualPlanModify" CodeFile="AnnualPlanModify.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>���̼ƻ��޸�</title>
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
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">���̼ƻ��޸�</td>
				</tr>
				<tr>
					<td vAlign="top">
						<table class="form" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD class="form-item" width="100">��ȣ�</TD>
								<TD colSpan="3"><asp:label id="lblYear" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="form-item" width="100">����ĩ���Ͷ�ʣ�</TD>
								<TD noWrap><input class="input-nember" id="txtInvestBefore" type="text" size="16" name="txtInvestBefore"
										runat="server">��Ԫ</TD>
								<TD class="form-item" width="100">�����ת�����</TD>
								<TD noWrap><input class="input-nember" id="txtLCFArea" type="text" size="16" name="txtLCFArea" runat="server">ƽ��</TD>
							</TR>
							<tr>
								<TD class="form-item">�ƻ�������ȣ�</TD>
								<TD noWrap><SELECT class="select" id="sltVisualProgress" onchange="VisualProgressChange();" name="sltVisualProgress"
										runat="server">
										<OPTION value="" selected>----��ѡ��----</OPTION>
									</SELECT><font color="red">*</font></TD>
								<TD class="form-item">�ƻ�ʩ��������</TD>
								<TD noWrap><input class="input-nember" id="txtCurrentFloor" type="text" size="16" name="txtCurrentFloor"
										runat="server"></TD>
							</tr>
							<TR style="DISPLAY:none">
								<TD class="form-item">����ƻ�Ͷ�ʣ�</TD>
								<TD noWrap><input class="input-nember" id="txtPInvest" type="text" size="16" name="txtPInvest" runat="server">��Ԫ</TD>
							</TR>
						</table>
					</td>
				</tr>
				<tr>
					<td>
						<table cellSpacing="0" cellPadding="0" border="0">
							<tr>
								<td class="intopic">������ȼƻ�</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr height="100%">
					<td class="topic" vAlign="top">
						<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
							<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
								<tr vAlign="top">
									<td><asp:datagrid id="dgList" runat="server" Width="100%" CssClass="list" CellPadding="0" AllowSorting="True"
											AutoGenerateColumns="False" PageSize="15" ShowFooter="False" DataKeyField="ConstructPlanStepCode">
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
												<asp:TemplateColumn HeaderText="�ƻ���ʼ����">
													<ItemTemplate>
														<cc3:calendar id="txtStartDate" runat="server" CalendarResource="../Images/CalendarResource/" ReadOnly="False" Display="True" value='<%# DataBinder.Eval(Container, "DataItem.StartDate", "{0:yyyy-MM-dd}") %>'>
														</cc3:calendar>
													</ItemTemplate>
													<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
													<FooterStyle HorizontalAlign="Left"></FooterStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="�ƻ���������">
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
			<input id="txtIYear" type="hidden" name="txtIYear" runat="server"><input id="txtAnnualPlanCode" type="hidden" name="txtAnnualPlanCode" runat="server">
			<input id="txtAct" type="hidden" name="txtAct" runat="server">
		</form>
		<script language="javascript">
<!--


function VisualProgressChange()
{
//����
return;

	var vg = Form1.sltVisualProgress.value;
	
	if (vg == "�ṹ")
	{
		document.all.trCurrentFloor.style.display="";
	}
	else
	{
		document.all.trCurrentFloor.style.display="none";
	}
}

VisualProgressChange();


//-->
		</script>
	</body>
</HTML>
