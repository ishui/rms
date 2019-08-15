<%@ Page language="c#" Inherits="RmsPM.Web.Cost.DynamicCostApplyList" CodeFile="DynamicCostApplyList.aspx.cs" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>��̬��������</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
	</HEAD>
	<body scroll="no">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td bgColor="#e4eff6" height="6"></td>
				</tr>
				<tr>
					<td height="25">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="topic" background="../images/topic_bg.gif"><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle">
									���ù���-&nbsp;��̬���õ�������&nbsp;&nbsp;
								</td>
								<td width="9"><IMG height="25" src="../images/topic_corr.gif" width="9"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td class="tools-area" vAlign="top"><input class="button" id="btnNewApply" onclick="doNewDynamicApply('');return false;" type="button"
							value="������������" name="btnNewApply" runat="server"></td>
				</tr>
				<tr>
					<td class="table" vAlign="top">
						<table class="search-area" cellSpacing="0" cellPadding="0" border="0">
							<tr>
								<td>
									<table>
										<tr>
											<td>����ʱ�䣺</td>
											<td nowrap><cc3:calendar id="dtMakeDate0" runat="server" CalendarResource="../Images/CalendarResource/" ReadOnly="False"
													Display="True"></cc3:calendar>
												&nbsp;����<cc3:calendar id="dtMakeDate1" runat="server" CalendarResource="../Images/CalendarResource/" ReadOnly="False"
													Display="True"></cc3:calendar></td>
											<td>���ʱ�䣺</td>
											<td nowrap><cc3:calendar id="dtCheckDate0" runat="server" CalendarResource="../Images/CalendarResource/"
													ReadOnly="False" Display="True"></cc3:calendar>
												&nbsp;����<cc3:calendar id="dtCheckDate1" runat="server" CalendarResource="../Images/CalendarResource/"
													ReadOnly="False" Display="True"></cc3:calendar></td>
											<td width="10%"><INPUT class="submit" id="btnSearch" type="button" value="�� ��" name="Button1" runat="server" onserverclick="btnSearch_ServerClick"></td>
										</tr>
										<tr>
											<td>����״̬��</td>
											<td><select id="sltFlag" runat="server" NAME="sltFlag">
													<option value="">-----��ѡ��-----</option>
													<option selected value="1">δ���</option>
													<option value="2">�����</option>
													<option value="3">����</option>
												</select></td>
											<td></td>
											<td></td>
											<td></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<asp:datagrid id="dgList" runat="server" CssClass="list" AutoGenerateColumns="False" CellPadding="2"
							Width="100%" ShowFooter="True">
							<AlternatingItemStyle CssClass="list-i"></AlternatingItemStyle>
							<HeaderStyle CssClass="list-title"></HeaderStyle>
							<FooterStyle CssClass="list-title"></FooterStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="����ʱ��">
									<ItemTemplate>
										<a href="##" onclick="doViewApply(this.code)" code='<%# DataBinder.Eval(Container.DataItem, "BudgetCode") %>'>
											<%# DataBinder.Eval(Container.DataItem, "MakeDate","{0:yyyy-MM-dd}") %>
										</a>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="������">
									<ItemTemplate>
										<%# RmsPM.BLL.SystemRule.GetUserName( DataBinder.Eval(Container.DataItem, "MakePerson").ToString() ) %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="����״̬">
									<ItemTemplate>
										<%# DataBinder.Eval(Container.DataItem, "FlagName") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="�����">
									<ItemTemplate>
										<%# RmsPM.BLL.SystemRule.GetUserName( DataBinder.Eval(Container.DataItem, "CheckPerson").ToString() ) %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="���ʱ��">
									<ItemTemplate>
										<%# DataBinder.Eval(Container.DataItem, "CheckDate","{0:yyyy-MM-dd}") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="����ԭ��">
									<ItemTemplate>
										<%# RmsPM.BLL.StringRule.TruncateString( DataBinder.Eval(Container.DataItem, "Reason"),20) %>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
						</asp:datagrid></td>
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
		function doViewApply(code)
		{
			OpenLargeWindow( '../Cost/DynamicApplyInfo.aspx?ProjectCode=<%=Request["ProjectCode"]%>&BudgetCode='+code,"��̬����������Ϣ" );
			
		}
			
		function doNewDynamicApply()
		{
			OpenLargeWindow( '../Cost/DynamicApplyModify.aspx?Action=AddNew&ProjectCode=<%=Request["ProjectCode"]%>','��̬����' );
		}
		
//-->
		</script>
	</body>
</HTML>
