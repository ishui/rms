<%@ Page language="c#" Inherits="RmsPM.Web.Cost.DynamicCostApplyList" CodeFile="DynamicCostApplyList.aspx.cs" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>动态调整申请</title>
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
									费用管理-&nbsp;动态费用调整申请&nbsp;&nbsp;
								</td>
								<td width="9"><IMG height="25" src="../images/topic_corr.gif" width="9"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td class="tools-area" vAlign="top"><input class="button" id="btnNewApply" onclick="doNewDynamicApply('');return false;" type="button"
							value="新增调整申请" name="btnNewApply" runat="server"></td>
				</tr>
				<tr>
					<td class="table" vAlign="top">
						<table class="search-area" cellSpacing="0" cellPadding="0" border="0">
							<tr>
								<td>
									<table>
										<tr>
											<td>申请时间：</td>
											<td nowrap><cc3:calendar id="dtMakeDate0" runat="server" CalendarResource="../Images/CalendarResource/" ReadOnly="False"
													Display="True"></cc3:calendar>
												&nbsp;到：<cc3:calendar id="dtMakeDate1" runat="server" CalendarResource="../Images/CalendarResource/" ReadOnly="False"
													Display="True"></cc3:calendar></td>
											<td>审核时间：</td>
											<td nowrap><cc3:calendar id="dtCheckDate0" runat="server" CalendarResource="../Images/CalendarResource/"
													ReadOnly="False" Display="True"></cc3:calendar>
												&nbsp;到：<cc3:calendar id="dtCheckDate1" runat="server" CalendarResource="../Images/CalendarResource/"
													ReadOnly="False" Display="True"></cc3:calendar></td>
											<td width="10%"><INPUT class="submit" id="btnSearch" type="button" value="搜 索" name="Button1" runat="server" onserverclick="btnSearch_ServerClick"></td>
										</tr>
										<tr>
											<td>调整状态：</td>
											<td><select id="sltFlag" runat="server" NAME="sltFlag">
													<option value="">-----请选择-----</option>
													<option selected value="1">未审核</option>
													<option value="2">已审核</option>
													<option value="3">作废</option>
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
								<asp:TemplateColumn HeaderText="申请时间">
									<ItemTemplate>
										<a href="##" onclick="doViewApply(this.code)" code='<%# DataBinder.Eval(Container.DataItem, "BudgetCode") %>'>
											<%# DataBinder.Eval(Container.DataItem, "MakeDate","{0:yyyy-MM-dd}") %>
										</a>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="申请人">
									<ItemTemplate>
										<%# RmsPM.BLL.SystemRule.GetUserName( DataBinder.Eval(Container.DataItem, "MakePerson").ToString() ) %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="申请状态">
									<ItemTemplate>
										<%# DataBinder.Eval(Container.DataItem, "FlagName") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="审核人">
									<ItemTemplate>
										<%# RmsPM.BLL.SystemRule.GetUserName( DataBinder.Eval(Container.DataItem, "CheckPerson").ToString() ) %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="审核时间">
									<ItemTemplate>
										<%# DataBinder.Eval(Container.DataItem, "CheckDate","{0:yyyy-MM-dd}") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="调整原因">
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
			OpenLargeWindow( '../Cost/DynamicApplyInfo.aspx?ProjectCode=<%=Request["ProjectCode"]%>&BudgetCode='+code,"动态费用申请信息" );
			
		}
			
		function doNewDynamicApply()
		{
			OpenLargeWindow( '../Cost/DynamicApplyModify.aspx?Action=AddNew&ProjectCode=<%=Request["ProjectCode"]%>','动态调整' );
		}
		
//-->
		</script>
	</body>
</HTML>
