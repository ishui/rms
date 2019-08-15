<%@ Reference Control="~/usercontrols/inputpbs.ascx" %>
<%@ Reference Control="~/usercontrols/inputsystemgroup.ascx" %>
<%@ Reference Control="~/usercontrols/inputunit.ascx" %>
<%@ Reference Control="~/usercontrols/inputuser.ascx" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Page language="c#" Inherits="RmsPM.Web.CostBudget.CostBudgetBackupList" CodeFile="CostBudgetBackupList.aspx.cs" %>
<%@ Register TagPrefix="cc1" Namespace="Tiannuo.Web.Controls" Assembly="DataGridWebControls" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>项目费用存档列表</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<SCRIPT language="javascript" src="../images/checkbox.js"></SCRIPT>
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<div style="DISPLAY:none">
				<input type="button" value="delete" id="btnDelete" name="btnDelete" runat="server" onserverclick="btnDelete_ServerClick">
			</div>
			<TABLE height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td bgColor="#e4eff6" height="6"></td>
				</tr>
				<tr>
					<td height="25">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="topic" background="../images/topic_bg.gif"><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle">费用管理 
									- <span id="spanTitle" runat="server">项目费用存档列表</span></td>
								<td width="9"><IMG height="25" src="../images/topic_corr.gif" width="9"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td class="table" vAlign="top">
						<table width="100%" height="100%">
							<tr>
								<td>
									<TABLE class="search-area" cellSpacing="0" cellPadding="0" width="100%" border="0" onkeydown="SearchKeyDown();">
										<tr>
											<td>
												<table>
													<TR>
														<TD>存档日期：<cc3:calendar id="dtBackupDateBegin" runat="server" CalendarResource="../Images/CalendarResource/"
																ReadOnly="False" Display="True" Value=""></cc3:calendar>
															&nbsp;到：<cc3:calendar id="dtBackupDateEnd" runat="server" CalendarResource="../Images/CalendarResource/"
																ReadOnly="False" Display="True" Value=""></cc3:calendar>
															&nbsp;<input class="submit" id="btnSearch" type="button" value="搜 索" name="btnSearch" runat="server"
																onclick="document.all.divHintLoad.style.display = '';" onserverclick="btnSearch_ServerClick">
														</TD>
													</TR>
												</table>
											</td>
										</tr>
									</TABLE>
								</td>
							</tr>
							<tr height="100%">
								<td>
									<div style="OVERFLOW:auto;WIDTH:100%;POSITION:absolute;HEIGHT:100%">
										<asp:datagrid id="dgList" runat="server" Width="100%" AllowSorting="True" AutoGenerateColumns="False"
											PageSize="15" AllowPaging="True" CellPadding="0" CssClass="list" ShowFooter="False">
											<AlternatingItemStyle CssClass=""></AlternatingItemStyle>
											<ItemStyle CssClass=""></ItemStyle>
											<HeaderStyle CssClass="list-title"></HeaderStyle>
											<FooterStyle CssClass="list-title"></FooterStyle>
											<Columns>
												<asp:TemplateColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" HeaderText="存档日期" SortExpression="BackupDate">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<a href="#" onclick="View(this.CostBudgetBackupCode);return false;" CostBudgetBackupCode='<%#  DataBinder.Eval(Container.DataItem, "CostBudgetBackupCode") %>'>
															<%#  DataBinder.Eval(Container.DataItem, "BackupDate", "{0:yyyy-MM-dd HH:mm:ss}") %>
														</a>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" HeaderText="描述" SortExpression="BackupDescription">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%#  DataBinder.Eval(Container.DataItem, "BackupDescription") %>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" HeaderText="删除">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<img src="../images/del.gif" style="cursor:hand" border="0" onclick="Delete(this.code, this.desc);" code='<%#  DataBinder.Eval(Container.DataItem, "CostBudgetBackupCode") %>' desc='<%#  DataBinder.Eval(Container.DataItem, "BackupDate", "{0:yyyy-MM-dd HH:mm:ss}") %>'>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle Visible="False" NextPageText="下页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上页" HorizontalAlign="Right"
												CssClass="ListHeadTr"></PagerStyle>
										</asp:datagrid>
									</div>
								</td>
							</tr>
							<tr style="DISPLAY:none">
								<td><cc1:GridPagination id="GridPagination1" runat="server" ControlSourceUrl="../Images/GridPaginationSource/" DataGridId=""></cc1:GridPagination></td>
							</tr>
						</table>
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
			</TABLE>
			<div id="divHintLoad" style="DISPLAY: none; LEFT: 1px; WIDTH: 100%; POSITION: absolute; TOP: 200px; BACKGROUND-COLOR: transparent">
				<TABLE id="tableHintLoad" height="100" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD vAlign="top" align="center"><iframe id="frameLoad" src="../Cost/LoadingPrepare.htm" frameBorder="no" width="100%" scrolling="auto"
								height="100%"></iframe>
						</TD>
					</TR>
				</TABLE>
			</div>
			<div id="divHintSave" style="DISPLAY: none; LEFT: 1px; WIDTH: 100%; POSITION: absolute; TOP: 200px">
				<TABLE id="tableHintSave" height="100" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD vAlign="top" align="center"><iframe id="frameSave" src="../Cost/SavingWating.htm" frameBorder="no" width="100%" scrolling="auto"
								height="100%"></iframe>
						</TD>
					</TR>
				</TABLE>
			</div>
			<input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server"><input id="txtCostBudgetSetCode" type="hidden" name="txtCostBudgetSetCode" runat="server">
			<input id="txtGroupCode" type="hidden" name="txtGroupCode" runat="server"> <input id="txtAct" type="hidden" name="txtAct" runat="server"><input id="txtDeleteCode" type="hidden" name="txtDeleteCode" runat="server">
			<script language="javascript">
<!--
var CurrUrl = window.location.href;

//查看项目费用表
function View(CostBudgetBackupCode)
{
	if (Form1.txtGroupCode.value != "") //预算表(按预算类别)
	{
		window.opener.location.href = "CostBudgetGroupInfo.aspx?CostBudgetBackupCode=" + CostBudgetBackupCode + "&GroupCode=" + Form1.txtGroupCode.value;
		window.close();
	}
	else if (Form1.txtCostBudgetSetCode.value != "") //预算表
	{
		window.opener.location.href = "CostBudgetInfo.aspx?CostBudgetBackupCode=" + CostBudgetBackupCode + "&CostBudgetSetCode=" + Form1.txtCostBudgetSetCode.value;
		window.close();

//		OpenFullWindow("CostBudgetInfo.aspx?CostBudgetBackupCode=" + CostBudgetBackupCode + "&CostBudgetSetCode=" + Form1.txtCostBudgetSetCode.value, "CostBudgetBackupInfo_" + CostBudgetBackupCode);
	}
	else //项目费用汇总
	{
		window.opener.location.href = "CostBudgetFrame.aspx?CostBudgetBackupCode=" + CostBudgetBackupCode + "&ProjectCode=" + Form1.txtProjectCode.value;
		window.close();
	}
}

//搜索条件按回车
function SearchKeyDown()
{
	if(event.keyCode==13)
	{
		event.keyCode = 9;
		Form1.btnSearch.click();
	}
}

function Delete(code, desc)
{
	if (!confirm("确实要删除 " + desc + " 的存档吗？")) return false;
	
	document.all.txtDeleteCode.value = code;
	document.all.btnDelete.click();
}

//-->
			</script>
		</form>
	</body>
</HTML>
