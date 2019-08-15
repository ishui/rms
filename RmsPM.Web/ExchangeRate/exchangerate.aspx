<%@ Page language="c#" Inherits="RmsPM.Web.ExchangeRate.ExchangeRate" CodeFile="ExchangeRate.aspx.cs" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Register TagPrefix="cc1" Namespace="Tiannuo.Web.Controls" Assembly="DataGridWebControls" %>
<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics.WebUI.WebDataInput.v5.1, Version=5.1.20051.37, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>外币汇率</title>
		<META http-equiv="Content-Type" content="text/html; charset=gb2312">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<LINK href="../Images/infra.css" type="text/css" rel="stylesheet">
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
									外币汇率 - 搜索</td>
								<td width="9"><IMG height="25" src="../images/topic_corr.gif" width="9"></td>
							</tr>
						</table>
					</td>
				</tr>
				<TR>
					<td class="tools-area" vAlign="top">
						<IMG src="../images/btn_li.gif" align="absMiddle"> <input class="button" id="btnNew" onclick="doNewExchangeRate('');return false;" type="button"
							value="新增汇率" name="btnNew" runat="server">
					</td>
				</TR>
				<tr height="100%">
					<td class="table" vAlign="top">
						<table width="100%" height="100%">
							<tr>
								<td>
									<TABLE class="search-area" cellSpacing="0" cellPadding="0" width="100%" border="0" onkeydown="SearchKeyDown();">
										<tr>
											<td>
												<table>
													<TR>
														<td>币种：</td>
														<td width="200">
															<SELECT id="sltMoneyType" style="WIDTH: 136px" name="sltMoneyType" runat="server">
																<OPTION value="" selected>---------请选择---------</OPTION>
															</SELECT>
														</td>
														<td>显示：</td>
														<td width="200">
															<asp:RadioButtonList ID="rblShow" Runat="server" RepeatDirection="Horizontal">
																<asp:ListItem Value="Now" Selected>当前最新汇率</asp:ListItem>
																<asp:ListItem Value="All">所有汇率</asp:ListItem>
															</asp:RadioButtonList>
														</td>
														<td><INPUT class="submit" id="btnSearch" type="button" value="搜 索" name="btnSearch" runat="server"
																onclick="document.all.divHintLoad.style.display = '';" onserverclick="btnSearch_ServerClick"> &nbsp;<img src="../images/search_more.gif" title="高级查询" style="CURSOR:hand" id="imgAdvSearch"
																onclick="ShowAdvSearch();">
														</td>
													</TR>
												</table>
												<table style="DISPLAY:none" id="divAdvSearch">
													<tr>
														<TD>日期：<cc3:calendar id="dtDate0" runat="server" CalendarResource="../Images/CalendarResource/" ReadOnly="False"
																Display="True" Value=""></cc3:calendar>
															——<cc3:calendar id="dtDate1" runat="server" CalendarResource="../Images/CalendarResource/" ReadOnly="False"
																Display="True" Value=""></cc3:calendar>
														</TD>
													</tr>
												</table>
											</td>
										</tr>
									</TABLE>
								</td>
							</tr>
							<tr height="100%">
								<td>
									<div style="OVERFLOW:auto;WIDTH:100%;POSITION:absolute;HEIGHT:100%">
										<p align="right">单位：人民币/100外币&nbsp;</p>
										<asp:datagrid id="dgExchangeRateList" runat="server" AutoGenerateColumns="False" PageSize="15"
											GridLines="Horizontal" CellPadding="0" Width="100%" CssClass="list" ShowFooter="True">
											<AlternatingItemStyle CssClass="list-i"></AlternatingItemStyle>
											<HeaderStyle CssClass="list-title"></HeaderStyle>
											<FooterStyle CssClass="list-title"></FooterStyle>
											<Columns>
											    <asp:TemplateColumn HeaderText="币 种">
													<HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%# RmsPM.BLL.StringRule.TruncateString(  DataBinder.Eval(Container.DataItem, "MoneyType"),8) %>									
													</ItemTemplate>
												</asp:TemplateColumn>	
												<asp:TemplateColumn HeaderText="币种">
													<HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<a href="##" onclick="javascript:doModify('<%#  DataBinder.Eval(Container.DataItem, "ExchangeRateCode") %>');return false;" >
															<%# RmsPM.BLL.StringRule.TruncateString(  DataBinder.Eval(Container.DataItem, "MoneyType"),8) %>
														</a>
													</ItemTemplate>
												</asp:TemplateColumn>												
												<asp:TemplateColumn HeaderText="现汇买入价">
													<HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%# RmsPM.BLL.StringRule.BuildShowNumberString(DataBinder.Eval(Container.DataItem, "RemittanceBuy"),"#.########") %>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="现钞买入价">
													<HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%# RmsPM.BLL.StringRule.BuildShowNumberString(DataBinder.Eval(Container.DataItem, "CashBuy"), "#.########")%>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="现汇卖出价">
													<HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%# RmsPM.BLL.StringRule.BuildShowNumberString(DataBinder.Eval(Container.DataItem, "RemittanceSell"), "#.########")%>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="现钞卖出价">
													<HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%# RmsPM.BLL.StringRule.BuildShowNumberString(DataBinder.Eval(Container.DataItem, "CashSell"), "#.########")%>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="中间价">
													<HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%# RmsPM.BLL.StringRule.BuildShowNumberString(DataBinder.Eval(Container.DataItem, "RemittanceAverage"), "#.########")%>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="日期">
													<HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%# RmsPM.BLL.StringRule.ShowDate((DateTime)DataBinder.Eval(Container.DataItem, "CreateDate")) %>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
										</asp:datagrid>
									</div>
								</td>
							</tr>
							<tr>
								<td>
									<cc1:GridPagination id="GridPagination1" runat="server" ControlSourceUrl="../Images/GridPaginationSource/" DataGridId="dgList"></cc1:GridPagination>
								</td>
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
			</table>
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
			<input id="txtAdvSearch" type="hidden" value="none" name="txtAdvSearch" runat="server">
		</form>
		<script language="javascript">
		
		function doModify( code )
		{
			OpenFullWindow('../ExchangeRate/ExchangeRateModify.aspx?act=Modify&ExchangeRateCode='+code,'新增汇率');
		}

		function doNewExchangeRate()
		{
			OpenFullWindow('../ExchangeRate/ExchangeRateModify.aspx?act=Add','新增汇率');
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
</HTML>
