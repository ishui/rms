<%@ Register TagPrefix="uc5" TagName="InputSystemGroup" Src="../UserControls/InputSystemGroup.ascx" %>
<%@ Page language="c#" Inherits="RmsPM.Web.CostBudget.CostTargetList" CodeFile="CostTargetList.aspx.cs" %>
<%@ Register TagPrefix="cc1" Namespace="Tiannuo.Web.Controls" Assembly="DataGridWebControls" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Register TagPrefix="uc2" TagName="InputUnit" Src="../UserControls/InputUnit.ascx" %>
<%@ Register TagPrefix="uc1" TagName="InputUser" Src="../UserControls/InputUser.ascx" %>
<%@ Register TagPrefix="uc1" TagName="InputPBS" Src="../UserControls/InputPBS.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>预算表</title>
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
			<TABLE height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td bgColor="#e4eff6" height="6"></td>
				</tr>
				<tr>
					<td height="25">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="topic" background="../images/topic_bg.gif"><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle">费用管理 
									- 预算表<span id="spanTitle" runat="server">目标费用</span></td>
								<td width="9"><IMG height="25" src="../images/topic_corr.gif" width="9"></td>
							</tr>
						</table>
					</td>
				</tr>
				<TR>
					<td class="tools-area" vAlign="top">
						<IMG src="../images/btn_li.gif" align="absMiddle"> <input class="button" id="btnAddSet" onclick="AddSet();" type="button" value="新增预算表" name="btnAddSet"
							runat="server" style="DISPLAY:none"> <input class="button" id="btnAddTarget" onclick="AddTarget();" type="button" value="新建目标费用"
							name="btnAddTarget" runat="server"> <input class="button" id="btnAddBudget" onclick="AddBudget();" type="button" value="新建动态调整"
							name="btnAddBudget" runat="server"> <input class="button" type="button" value="目标费用汇总" name="btnGotoCostTargetMain" id="btnGotoCostTargetMain"
							runat="server" onclick="GotoCostTargetMain();"> <input class="button" type="button" value="动态费用汇总" name="btnGotoCostBudgetMain" id="btnGotoCostBudgetMain"
							runat="server" onclick="GotoCostBudgetMain();">
					</td>
				</TR>
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
														<td>状态： &nbsp;<input id="chkStatus0" type="checkbox" value="0" name="chkStatus0" runat="server">申请&nbsp; 
															&nbsp;<input id="chkStatus1" type="checkbox" value="1" name="chkStatus1" runat="server" checked>当前&nbsp; 
															&nbsp;<input id="chkStatus2" type="checkbox" value="2" name="chkStatus2" runat="server">历史&nbsp; 
															&nbsp;<input id="chkStatus3" type="checkbox" value="3" name="chkStatus3" runat="server">调整&nbsp; 
															&nbsp;&nbsp;<input class="submit" id="btnSearch" type="button" value="搜索" name="btnSearch" runat="server"
																onclick="document.all.divHintLoad.style.display = '';" onserverclick="btnSearch_ServerClick"> &nbsp;&nbsp;<img src="../images/search_more.gif" title="高级查询" style="CURSOR:hand" id="imgAdvSearch"
																onclick="ShowAdvSearch();">
														</td>
													</TR>
												</table>
												<table style="DISPLAY:none" id="divAdvSearch">
													<tr>
														<TD>类别：<uc5:InputSystemGroup id="ucInputSystemGroup" runat="server" ClassCode="0411" SelectAllLeaf="True"></uc5:InputSystemGroup>
															&nbsp;&nbsp;单位工程：<uc1:InputPBS id="ucPBS" runat="server"></uc1:InputPBS>
															&nbsp;&nbsp;部门：<uc2:InputUnit id="ucUnit" runat="server"></uc2:InputUnit>
														</TD>
													</tr>
													<TR>
														<TD>创建人：<uc1:InputUser id="ucCreatePerson" runat="server"></uc1:InputUser>
															&nbsp;&nbsp;创建日期：<cc3:calendar id="dtCreateDateBegin" runat="server" CalendarResource="../Images/CalendarResource/"
																ReadOnly="False" Display="True" Value=""></cc3:calendar>
															&nbsp;到：<cc3:calendar id="dtCreateDateEnd" runat="server" CalendarResource="../Images/CalendarResource/"
																ReadOnly="False" Display="True" Value=""></cc3:calendar>
														</TD>
													</TR>
													<TR>
														<TD>最后修改人：<uc1:InputUser id="ucModifyPerson" runat="server"></uc1:InputUser>
															&nbsp;&nbsp;最后修改日期：<cc3:calendar id="dtModifyDateBegin" runat="server" CalendarResource="../Images/CalendarResource/"
																ReadOnly="False" Display="True" Value=""></cc3:calendar>
															&nbsp;到：<cc3:calendar id="dtModifyDateEnd" runat="server" CalendarResource="../Images/CalendarResource/"
																ReadOnly="False" Display="True" Value=""></cc3:calendar>
														</TD>
													</TR>
													<TR>
														<TD>审核人：<uc1:InputUser id="ucCheckPerson" runat="server"></uc1:InputUser>
															&nbsp;&nbsp;审核日期：<cc3:calendar id="dtCheckDateBegin" runat="server" CalendarResource="../Images/CalendarResource/"
																ReadOnly="False" Display="True" Value=""></cc3:calendar>
															&nbsp;到：<cc3:calendar id="dtCheckDateEnd" runat="server" CalendarResource="../Images/CalendarResource/"
																ReadOnly="False" Display="True" Value=""></cc3:calendar>
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
										<asp:datagrid id="dgList" runat="server" Width="100%" AllowSorting="False" AutoGenerateColumns="False"
											PageSize="15" AllowPaging="True" CellPadding="0" CssClass="list" ShowFooter="False">
											<AlternatingItemStyle CssClass=""></AlternatingItemStyle>
											<ItemStyle CssClass=""></ItemStyle>
											<HeaderStyle CssClass="list-title"></HeaderStyle>
											<FooterStyle CssClass="list-title"></FooterStyle>
											<Columns>
												<asp:TemplateColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" Visible="False" HeaderText="&lt;input type='checkbox' name='chkAll' onclick='ChkSelectAll(document.all.chkSelect, document.all.chkAll.checked);' title='全选或全不选'&gt;">
													<HeaderStyle HorizontalAlign="Center" Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<input type="checkbox" name="chkSelect" class="list-checkbox" onclick="ChkSelectRow('<%# Container.ItemIndex + 1%>', this, document.all.dgList, 'list-2', '');" value='<%#DataBinder.Eval(Container, "DataItem.CostBudgetCode")%>'>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" HeaderText="名称">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<a href="#" onclick="javascript:View(this.code, this.CostBudgetSetCode); return false;" code='<%# DataBinder.Eval(Container.DataItem, "CostBudgetCode") %>' CostBudgetSetCode='<%# DataBinder.Eval(Container.DataItem, "CostBudgetSetCode") %>'>
															<%#  DataBinder.Eval(Container.DataItem, "CostBudgetSetName") %>
														</a>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" HeaderText="版本号">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%#  DataBinder.Eval(Container.DataItem, "VerID") %>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" HeaderText="版本名称">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%#  DataBinder.Eval(Container.DataItem, "CostBudgetName") %>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" HeaderText="状态">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%#  DataBinder.Eval(Container.DataItem, "StatusName") %>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" HeaderText="类型" Visible="False">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%#  DataBinder.Eval(Container.DataItem, "GroupName") %>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" DataField="UnitName" HeaderText="部门"
													Visible="False">
													<HeaderStyle HorizontalAlign="Left" Wrap="False"></HeaderStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" HeaderText="单位工程" Visible="False">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%#  DataBinder.Eval(Container.DataItem, "PBSName") %>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" HeaderText="创建人">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%#  DataBinder.Eval(Container.DataItem, "CreatePersonName") %>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" HeaderText="创建日期">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%#  DataBinder.Eval(Container.DataItem, "CreateDate", "{0:yyyy-MM-dd}") %>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" HeaderText="最后修改人">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%#  DataBinder.Eval(Container.DataItem, "ModifyPersonName") %>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" HeaderText="最后修改日期">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%#  DataBinder.Eval(Container.DataItem, "ModifyDate", "{0:yyyy-MM-dd}") %>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" HeaderText="审核人" Visible="False">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%#  DataBinder.Eval(Container.DataItem, "CheckPersonName") %>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" HeaderText="审核日期" Visible="False">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%#  DataBinder.Eval(Container.DataItem, "CheckDate", "{0:yyyy-MM-dd}") %>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" HeaderText="总额" SortExpression="TotalBudgetMoney">
													<HeaderStyle HorizontalAlign="Right" Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<%# RmsPM.BLL.CostBudgetPageRule.GetMoneyShowString(DataBinder.Eval(Container, "DataItem.TotalBudgetMoney")) %>
													</ItemTemplate>
													<FooterTemplate>
														<asp:Label runat="server" ID="lblSumMoney"></asp:Label>
													</FooterTemplate>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle Visible="False" NextPageText="下页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上页" HorizontalAlign="Right"
												CssClass="ListHeadTr"></PagerStyle>
										</asp:datagrid>
									</div>
								</td>
							</tr>
							<tr>
								<td><cc1:GridPagination id="GridPagination1" runat="server" ControlSourceUrl="../Images/GridPaginationSource/" DataGridId="dgList" onpageindexchange="GridPagination1_PageIndexChange"></cc1:GridPagination></td>
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
			<input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server"><input id="txtStatus" type="hidden" name="txtStatus" runat="server">
			<input id="txtAct" type="hidden" name="txtAct" runat="server"> <input id="txtSelect" type="hidden" name="txtSelect" runat="server">
			<input id="txtAdvSearch" type="hidden" value="none" name="txtAdvSearch" runat="server"><input id="txtTargetFlag" type="hidden" name="txtTargetFlag" runat="server">
			<script language="javascript">
<!--
var CurrUrl = window.location.href;

//刷新
function Refresh()
{
	Form1.btnSearch.click();
}

//切换到目标费用汇总表
function GotoCostTargetMain()
{
	window.location.href = "CostTargetMain.aspx?ProjectCode=" + Form1.txtProjectCode.value;
}

//切换到动态预算汇总表
function GotoCostBudgetMain()
{
	window.location.href = "CostBudgetMain.aspx?ProjectCode=" + Form1.txtProjectCode.value;
}

//新增预算表
function AddSet()
{
	OpenCustomWindow("CostBudgetSetModify.aspx?ProjectCode=" + Form1.txtProjectCode.value, "预算表设置", 500, 350);
}

//新建目标费用
function AddTarget()
{
	var CostBudgetSetCode = OpenCustomDialog("SelectCostBudgetSet.aspx?ProjectCode=" + Form1.txtProjectCode.value + "&type=NoTarget", "", 25, 20);
	if (!CostBudgetSetCode) return;
	
	OpenFullWindow("CostTargetModify.aspx?CostBudgetSetCode=" + CostBudgetSetCode, "预算表目标费用修改");
}

//新建动态预算
function AddBudget()
{
	var CostBudgetSetCode = OpenCustomDialog("SelectCostBudgetSet.aspx?ProjectCode=" + Form1.txtProjectCode.value + "&type=NoBudget", "", 25, 20);
	if (!CostBudgetSetCode) return;
	
	OpenFullWindow("CostBudgetModify.aspx?CostBudgetSetCode=" + CostBudgetSetCode, "预算表动态预算修改");
}

//查看预算表
function View(CostBudgetCode, CostBudgetSetCode)
{
	if (Form1.txtTargetFlag.value == "1")  //目标费用
	{
		if (CostBudgetCode.substr(0, 5) == "NULL_")
			OpenFullWindow("CostTargetInfo.aspx?CostBudgetSetCode=" + CostBudgetSetCode , "");
		else
			OpenFullWindow("CostTargetInfo.aspx?CostBudgetCode=" + CostBudgetCode + "&CostBudgetSetCode=" + CostBudgetSetCode , "");
	}
	else  //动态预算
	{
		if (CostBudgetCode.substr(0, 5) == "NULL_")
			OpenFullWindow("CostBudgetInfo.aspx?CostBudgetSetCode=" + CostBudgetSetCode , "");
		else
			OpenFullWindow("CostBudgetInfo.aspx?CostBudgetCode=" + CostBudgetCode + "&CostBudgetSetCode=" + CostBudgetSetCode , "");
	}
	
//	window.navigate('CostTargetInfo.aspx?CostBudgetCode='+ CostBudgetCode + "&FromUrl=" + escape(CurrUrl));
}

//高级查询
function ShowAdvSearch()
{
	var display = Form1.txtAdvSearch.value;
	
	if ( display == "none" )
	{
		display = "block";
	}
	else
	{
		display = "none";
	}
	
	Form1.txtAdvSearch.value = display;
	
	SetAdvSearch();;
}

function SetAdvSearch()
{
	document.all("divAdvSearch").style.display = Form1.txtAdvSearch.value;

	if ( Form1.txtAdvSearch.value == "none" )
	{
//		Form1.imgAdvSearch.src = "../images/ArrowDown.gif";
		Form1.imgAdvSearch.title = "高级查询";
	}
	else
	{
//		Form1.imgAdvSearch.src = "../images/ArrowUp.gif";
		Form1.imgAdvSearch.title = "隐藏高级查询";
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

SetAdvSearch();

//-->
			</script>
		</form>
	</body>
</HTML>
