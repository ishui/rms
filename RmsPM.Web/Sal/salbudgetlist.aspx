<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Page language="c#" Inherits="RmsPM.Web.Sal.SalBudgetList" CodeFile="SalBudgetList.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>SalBudgetList</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<SCRIPT language="javascript" src="SalBudget.js" charset="gb2312"></SCRIPT>
	</HEAD>
	<body scroll="no">
		<form id="Form1" method="post" runat="server">
			<div style="DISPLAY: none"><input id="btnHiddenYear" onclick="document.all.divHintLoad.style.display='';" type="button"
					name="btnHiddenYear" runat="server" onserverclick="btnHiddenYear_ServerClick"></div>
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td bgColor="#e4eff6" height="6"></td>
				</tr>
				<tr>
					<td height="25">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="topic" background="../images/topic_bg.gif"><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle"><span id="spanTitle" runat="server">计划管理 
										- 销售计划</span></td>
								<td width="9"><IMG height="25" src="../images/topic_corr.gif" width="9"></td>
							</tr>
						</table>
					</td>
				</tr>
				<TR>
					<TD class="tools-area" vAlign="top"><IMG src="../images/btn_li.gif" align="absMiddle">
						<input class="button" id="btnAddBudget" onclick="AddBudget()" type="button" value="新增计划"
							name="btnAddBudget" runat="server"> <input class="button" id="btnModifyBudget" onclick="ModifyBudget()" type="button" value="修改计划"
							name="btnModifyBudget" runat="server"> <input class="button" id="btnModifyAct" style="DISPLAY: none" onclick="ModifyAct()" type="button"
							value="修改实际" name="btnModifyAct" runat="server"> <IMG src="../images/btn_li.gif" align="absMiddle">
						年度：<select id="sltYear" onchange="btnHiddenYear.click();" name="sltYear" runat="server"></select>
					</TD>
				</TR>
				<tr>
					<td class="table">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="note"><asp:label id="lblBudgetName" runat="server"></asp:label></td>
							</tr>
						</table>
						<TABLE class="form" cellSpacing="0" cellPadding="0" width="100%">
							<TR>
								<TD class="form-item" width="100">计划周期：</TD>
								<TD><asp:label id="lblPeriodMonthDesc" runat="server"></asp:label></TD>
								<TD class="form-item" width="100">后续计划：</TD>
								<TD><asp:label id="lblAfterPeriodDesc" Runat="server"></asp:label></TD>
							</TR>
							<tr>
								<TD class="form-item">填报人：</TD>
								<TD><asp:label id="lblModiPersonName" Runat="server"></asp:label></TD>
								<TD class="form-item">填报日期：</TD>
								<TD><asp:label id="lblModiDate" Runat="server"></asp:label></TD>
							</tr>
						</TABLE>
					</td>
				</tr>
				<tr height="100%">
					<td class="table" vAlign="top">
						<TABLE height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td>
									<div style="OVERFLOW: auto; WIDTH: 100%; POSITION: absolute; HEIGHT: 100%">
										<table class="list" cellSpacing="0" cellPadding="0" id="tableList">
											<tr class="list-title" align="center">
												<td colSpan="2" rowSpan="2" width="80">&nbsp;</td>
												<td rowSpan="2" nowrap>产品类型</td>
												<td id="titleYear0" rowSpan="2" nowrap>期前累计</td>
												<td id="titleYear" colSpan="13"></td>
												<td id="titleYear1"></td>
												<td id="titleYear2"></td>
											</tr>
											<tr class="list-title" align="center">
												<td width="80">1</td>
												<td width="80">2</td>
												<td width="80">3</td>
												<td width="80">4</td>
												<td width="80">5</td>
												<td width="80">6</td>
												<td width="80">7</td>
												<td width="80">8</td>
												<td width="80">9</td>
												<td width="80">10</td>
												<td width="80">11</td>
												<td width="80">12</td>
												<td width="80" nowrap>年度计划</td>
												<td nowrap>年度计划</td>
												<td nowrap>年度计划</td>
											</tr>
											<asp:repeater id="dgList" Runat="server">
												<ItemTemplate>
													<%# DataBinder.Eval(Container.DataItem, "TrHtml")%>
												</ItemTemplate>
											</asp:repeater>
											<tr>
											</tr>
											<asp:repeater id="dgListAct" Runat="server">
												<ItemTemplate>
													<%# DataBinder.Eval(Container.DataItem, "TrHtml")%>
												</ItemTemplate>
											</asp:repeater>
											<tr>
											</tr>
											<asp:repeater id="dgListArea" Runat="server">
												<ItemTemplate>
													<%# DataBinder.Eval(Container.DataItem, "TrHtml")%>
												</ItemTemplate>
											</asp:repeater>
											<tr>
											</tr>
											<asp:repeater id="dgListAreaAct" Runat="server">
												<ItemTemplate>
													<%# DataBinder.Eval(Container.DataItem, "TrHtml")%>
												</ItemTemplate>
											</asp:repeater>
											<tr>
											</tr>
											<asp:repeater id="dgListPrice" Runat="server">
												<ItemTemplate>
													<%# DataBinder.Eval(Container.DataItem, "TrHtml")%>
												</ItemTemplate>
											</asp:repeater>
											<tr>
											</tr>
											<asp:repeater id="dgListPriceAct" Runat="server">
												<ItemTemplate>
													<%# DataBinder.Eval(Container.DataItem, "TrHtml")%>
												</ItemTemplate>
											</asp:repeater>
											<tr>
											</tr>
											<asp:repeater id="dgListMoney" Runat="server">
												<ItemTemplate>
													<%# DataBinder.Eval(Container.DataItem, "TrHtml")%>
												</ItemTemplate>
											</asp:repeater>
											<tr>
											</tr>
											<asp:repeater id="dgListMoneyAct" Runat="server">
												<ItemTemplate>
													<%# DataBinder.Eval(Container.DataItem, "TrHtml")%>
												</ItemTemplate>
											</asp:repeater>
											<tr>
											</tr>
											<asp:repeater id="dgListRcvMoney" Runat="server">
												<ItemTemplate>
													<%# DataBinder.Eval(Container.DataItem, "TrHtml")%>
												</ItemTemplate>
											</asp:repeater>
											<tr>
											</tr>
											<asp:repeater id="dgListRcvMoneyAct" Runat="server">
												<ItemTemplate>
													<%# DataBinder.Eval(Container.DataItem, "TrHtml")%>
												</ItemTemplate>
											</asp:repeater>
											<tr>
											</tr>
										</table>
									</DIV>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
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
			<input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server"> <input id="txtYear" type="hidden" name="txtYear" runat="server">
			<input id="txtProjectName" type="hidden" name="txtProjectName" runat="server">
		</form>
		<SCRIPT language="javascript">
<!--

var CurrUrl = window.location.href;

//新增计划
function AddBudget()
{
	OpenFullWindow("../Sal/SalBudgetModify.aspx?FromUrl=" + escape(CurrUrl) + "&ProjectCode=" + Form1.txtProjectCode.value + "&Year=" + Form1.sltYear.value + "&RefreshScript=DoRefresh();", "销售计划修改");
//	OpenCustomWindow("../Sal/SalBudgetModify.aspx?FromUrl=" + escape(CurrUrl) + "&ProjectCode=" + Form1.txtProjectCode.Value + "&Year=" + Form1.sltYear.value, "销售计划修改", 700, 400);
}

//修改计划
function ModifyBudget()
{
	OpenFullWindow("../Sal/SalBudgetModify.aspx?FromUrl=" + escape(CurrUrl) + "&ProjectCode=" + Form1.txtProjectCode.value + "&Year=" + Form1.sltYear.value + "&RefreshScript=DoRefresh();", "销售计划修改");
//	OpenCustomWindow("../Sal/SalBudgetModify.aspx?FromUrl=" + escape(CurrUrl) + "&ProjectCode=" + Form1.txtProjectCode.Value + "&Year=" + Form1.sltYear.value, "销售计划修改", 700, 400);
}

//修改实际
function ModifyAct()
{
	OpenFullWindow("../Sal/SalIncomeModify.aspx?FromUrl=" + escape(CurrUrl) + "&ProjectCode=" + Form1.txtProjectCode.value + "&Year=" + Form1.sltYear.value, "销售实际修改");
//	OpenCustomWindow("../Sal/SalBudgetModify.aspx?FromUrl=" + escape(CurrUrl) + "&ProjectCode=" + Form1.txtProjectCode.Value + "&Year=" + Form1.sltYear.value, "销售计划修改", 700, 400);
}

//显示年份标题
function DisplayTitle()
{
	var year = parseInt(Form1.sltYear.value);
	
	document.all.titleYear.innerText = year;
	document.all.titleYear1.innerText = year + 1;
	document.all.titleYear2.innerText = year + 2;
}

//刷新
function DoRefresh()
{
	Form1.btnHiddenYear.click();
}

DisplayTitle();

var HasLoadGrid = '<%=ViewState["HasLoadGrid"]%>';
if (HasLoadGrid == "1")
{
	CollapseAllRoot();
}

//-->
		</SCRIPT>
	</body>
</HTML>
