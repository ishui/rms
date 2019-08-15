<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WorkFlowCommon.aspx.cs" Inherits="WorkFlowContral_WorkFlowCommon" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Register TagPrefix="cc1" Namespace="Tiannuo.Web.Controls" Assembly="DataGridWebControls" %>
<%@ Register TagPrefix="uc1" TagName="InputSystemGroup" Src="../UserControls/InputSystemGroup.ascx" %>
<%@ Register TagPrefix="uc1" TagName="InputCostBudgetDtl" Src="../UserControls/InputCostBudgetDtl.ascx" %>
<%@ Register TagPrefix="uc2" TagName="InputUnit" Src="../UserControls/InputUnit.ascx" %>
<%@ Register TagPrefix="uc1" TagName="InputUser" Src="../UserControls/InputUser.ascx" %>
<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics.WebUI.WebDataInput.v5.1, Version=5.1.20051.37, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>合同台帐</title>
		<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR" />
		<meta content="C#" name="CODE_LANGUAGE" />
		<meta content="JavaScript" name="vs_defaultClientScript" />
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
		<link href="../Images/index.css" type="text/css" rel="stylesheet" />
		<link href="../Images/infra.css" type="text/css" rel="stylesheet" />
		<script language="javascript" src="../Rms.js"></script>
	</head>
	<body scroll="no">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
				<tr>
					<td bgColor="#e4eff6" height="6"></td>
				</tr>
				<tr>
					<td height="25">
						<table cellspacing="0" cellpadding="0" width="100%" border="0">
							<tr>
								<td class="topic" background="../images/topic_bg.gif"><img height="25" src="../images/topic_li.jpg" width="35" align="absMiddle">
									通用流程 - 流程搜索</td>
								<td width="9"><img height="25" src="../images/topic_corr.gif" width="9" /></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td class="tools-area" vAlign="top">
						<img src="../images/btn_li.gif" align="absMiddle" /> 
						<asp:DropDownList ID="ddlWorkFlowTypeNew" runat="server"></asp:DropDownList> &nbsp;						
						<input class="button" id="btnNew" onclick="doNewWorkFlow('');return false;" type="button" value="新增流程" name="btnNew" runat="server" />
					</td>
				</tr>
				<tr height="100%">
					<td class="table" valign="top">
						<table width="100%" height="100%">
							<tr>
								<td>
									<table class="search-area" cellspacing="0" cellpadding="0" width="100%" border="0" onkeydown="SearchKeyDown();">
										<tr>
											<td>
												<table>
													<tr>
													    <td>流程名：</td>
													    <td><asp:DropDownList ID="ddlWorkFlowTypeView" runat="server"></asp:DropDownList></td>

														<td>状态：</td>
														<td colspan="3">
														    <asp:checkboxlist id="cblStatus" runat="server" RepeatDirection="Horizontal">
																<asp:ListItem Value="0">申请</asp:ListItem>
																<asp:ListItem Value="1">已审</asp:ListItem>
																<asp:ListItem Value="2">审核中</asp:ListItem>
																<asp:ListItem Value="3">作废</asp:ListItem>
															</asp:checkboxlist></td>
														<td>
														    <input class="submit" id="btnSearch" type="button" value="搜 索" name="btnSearch" runat="server"
																onclick="document.all.divHintLoad.style.display = '';" onserverclick="btnSearch_ServerClick" /> &nbsp;
															<img src="../images/search_more.gif" title="高级查询" style="CURSOR:hand" id="imgAdvSearch"
																onclick="ShowAdvSearch();" />
														</td>
													</tr>
													<tr>
														<td>标题：</td>
														<td><input class="input" id="txtWorkFlowTitle" type="text" name="txtWorkFlowTitle" runat="server" /></td>													
														<td>编号：</td>
														<td><input class="input" id="txtWorkFlowID" type="text" name="txtWorkFlowID" runat="server"></td>													
														<td>类型：</td>
														<td>
															<uc1:InputSystemGroup id="inputSystemGroup" runat="server" SelectAllLeaf="True"></uc1:InputSystemGroup>
														</td>
													</tr>
												</table>
												<table style="DISPLAY:none" id="divAdvSearch">
													<tr>
														<td>
														</td>
													</tr>
												</table>
											</td>
										</tr>
									</table>
								</td>
							</tr>
							<tr height="100%">
								<td>
									<div style="overflow:auto;width:100%;position:absolute;height:100%">
										<asp:datagrid id="dgList" runat="server" AutoGenerateColumns="False" PageSize="14" AllowPaging="True"
											GridLines="Horizontal" CellPadding="0" Width="100%" CssClass="list" AllowSorting="false" ShowFooter="True">
											<AlternatingItemStyle CssClass="list-i"></AlternatingItemStyle>
											<HeaderStyle CssClass="list-title"></HeaderStyle>
											<FooterStyle CssClass="list-title"></FooterStyle>
											<Columns>
												<asp:TemplateColumn SortExpression="WorkFlowTitle" HeaderText="名称">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<a href="##" onclick="doViewInfo(this.code);return false;" code='<%#  DataBinder.Eval(Container.DataItem, "WorkFlowCommonCode") %>'
													title='<%# DataBinder.Eval(Container.DataItem, "WorkFlowTitle")%>' >
															<%# RmsPM.BLL.StringRule.TruncateString(DataBinder.Eval(Container.DataItem, "WorkFlowTitle"), 8)%>
														</a>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="ProcedureName" HeaderText="流程名">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%# DataBinder.Eval(Container.DataItem, "ProcedureName")%>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="WorkFlowID" HeaderText="编号">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%# DataBinder.Eval(Container.DataItem, "WorkFlowID")%>
													</ItemTemplate>
												</asp:TemplateColumn>												
												<asp:TemplateColumn SortExpression="Type" HeaderText="类型">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<div title='<%# RmsPM.BLL.SystemGroupRule.GetSystemGroupFullName ( DataBinder.Eval(Container.DataItem, "Type").ToString() ) %>'>
															<%# RmsPM.DAL.EntityDAO.SystemManageDAO.GetSystemGroupSortID ( DataBinder.Eval(Container.DataItem, "Type").ToString() ) %>
														</div>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="CreateDate" HeaderText="创建时间">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%# DataBinder.Eval(Container.DataItem, "CreateDate")%>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="Status" HeaderText="当前状态">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%# RmsPM.BLL.WorkFlowRule.GetWorkFlowCommonStatusName(  DataBinder.Eval(Container.DataItem, "Status").ToString()) %>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle Visible="False" NextPageText="下一页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上一页" HorizontalAlign="Right"
												CssClass="ListHeadTr"></PagerStyle>
										</asp:datagrid>
									</div>
								</td>
							</tr>
							<tr>
								<td>
									<cc1:GridPagination id="GridPagination1" runat="server" ControlSourceUrl="../Images/GridPaginationSource/" DataGridId="dgList" onpageindexchange="GridPagination1_PageIndexChange"></cc1:GridPagination>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td height="12">
						<table cellspacing="0" cellpadding="0" width="100%" border="0">
							<tr>
								<td background="../images/corl_bg.gif">
								    <img height="12" src="../images/corl.gif" width="12" />
								</td>
								<td width="12">
								    <img height="12" src="../images/corr.gif" width="12" />
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td bgColor="#e4eff6" height="6"></td>
				</tr>
			</table>
			<div id="divHintLoad" style="display: none; left: 1px; width: 100%; position: absolute; TOP: 200px; background-color: transparent">
				<table id="tableHintLoad" height="100" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<tr>
						<td vAlign="top" align="center">
						    <iframe id="frameLoad" src="../Cost/LoadingPrepare.htm" frameBorder="no" width="100%" scrolling="auto"	height="100%"></iframe>
						</td>
					</tr>
				</table>
			</div>
			<div id="divHintSave" style="display: none; left: 1px; width: 100%; position: absolute; top: 200px">
				<table id="tableHintSave" height="100" cellspacing="0" cellpadding="0" width="100%" border="0">
					<tr>
						<td valign="top" align="center">
						    <iframe id="frameSave" src="../Cost/SavingWating.htm" frameborder="no" width="100%" scrolling="auto" height="100%"></iframe>
						</td>
					</tr>
				</table>
			</div>
			<input id="txtAdvSearch" type="hidden" value="none" name="txtAdvSearch" runat="server" />
		</form>
		<script language="javascript">
			

		function doNewWorkFlow()
		{
		    var ProcedureCode = Form1.ddlWorkFlowTypeNew.value;
			OpenFullWindow('WorkFlowCommonModify.aspx?ProjectCode=<%=Request["ProjectCode"]%>&ProcedureCode=' + ProcedureCode + '&act=Add','新增流程');
		}
		
	
		function doViewInfo( code )
		{
			OpenFullWindow('WokFlowCommonInfo.aspx?ProjectCode=<%=Request["ProjectCode"]%>&WorkFlowCommonCode=' + code,'流程信息');
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

		</script>
	</body>
</html>
