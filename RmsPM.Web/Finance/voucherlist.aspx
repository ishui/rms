<%@ Register TagPrefix="cc1" Namespace="Tiannuo.Web.Controls" Assembly="DataGridWebControls" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Page language="c#" Inherits="RmsPM.Web.Finance.VoucherList" CodeFile="VoucherList.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="InputUser" Src="../UserControls/InputUser.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>凭证管理</title>
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
								<td class="topic" background="../images/topic_bg.gif"><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle"><span id="spanTitle" runat="server">项目管理>付款管理>凭证处理</span></td>
								<td width="9"><IMG height="25" src="../images/topic_corr.gif" width="9"></td>
							</tr>
						</table>
					</td>
				</tr>
				<TR>
					<td class="tools-area" vAlign="top">
						<IMG src="../images/btn_li.gif" align="absMiddle"> <input class="button" id="btnAdd" onclick="DoAddNewVoucher()" type="button" value="新 增"
							name="btnAdd" runat="server">&nbsp;<INPUT class="button" id="btnBalanceInput" onclick="BalanceInput()" type="button" value="总帐导入"
							name="btnBalanceInput" runat="server"> <INPUT class="button" id="btnInput" onclick="inputVoucher()" type="button" value="导 入"
							name="btnInput" runat="server" style="display:none"> <input class="button" id="btnCheck" onclick="Check()" type="button" value="审 核" name="btnCheck"
							runat="server"> <input class="button" id="btnDownload" onclick="Download()" type="button" value="导 出" name="btnDownload"
							runat="server"> <input class="button" id="btnAllowPaging" type="button" value="取消分页" name="btnAllowPaging"
							runat="server">
					</td>
				</TR>
				<tr>
					<td class="table" vAlign="top">
						<TABLE class="search-area" cellSpacing="0" cellPadding="0" width="100%" border="0" onkeydown="SearchKeyDown();">
							<tr>
								<td>
									<table>
										<TR>
											<TD>凭证类型：</TD>
											<TD><SELECT id="sltVoucherType" class="select" name="sltVoucherType" runat="server">
													<OPTION value="" selected>------请选择------</OPTION>
												</SELECT></TD>
											<TD>凭证编号：</TD>
											<TD><INPUT class="input" id="txtVoucherID" type="text" name="txtVoucherID" runat="server"></TD>
											<TD>制 单 人：</TD>
											<TD><uc1:InputUser id="ucAccountant" runat="server"></uc1:InputUser>
											</TD>
											<TD><INPUT class="submit" id="btnSearch" type="button" value="搜 索" name="btnSearch" runat="server" onclick="document.all.divHintLoad.style.display = '';"></TD>
										</TR>
										<TR>
											<TD>制单日期：</TD>
											<TD><cc3:calendar id="dtbMakeDate0" runat="server" CalendarResource="../Images/CalendarResource/"
													ReadOnly="False" Display="True" Value=""></cc3:calendar></TD>
											<TD align="center">到：</TD>
											<TD>
												<cc3:calendar id="dtbMakeDate1" runat="server" CalendarResource="../Images/CalendarResource/"
													ReadOnly="False" Display="True" Value=""></cc3:calendar>&nbsp;</TD>
											<td>状态：</td>
											<TD colspan="2"><input id="chkStatus0" type="checkbox" value="1" name="chkStatus0" runat="server">待审&nbsp; 
												&nbsp;<input id="chkStatus1" type="checkbox" value="1" name="chkStatus1" runat="server">已审&nbsp; 
												&nbsp;<input id="chkStatus2" type="checkbox" value="1" name="chkStatus2" runat="server">已导出&nbsp;
											</TD>
										</TR>
									</table>
								</td>
							</tr>
						</TABLE>
					</td>
				</tr>
				<tr height="100%">
					<td class="table" valign="top">
						<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
							<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
								<tr vAlign="top">
									<td>
										<asp:datagrid id="dgList" runat="server" Width="100%" AllowSorting="True" AutoGenerateColumns="False"
											PageSize="15" AllowPaging="True" GridLines="Horizontal" CellPadding="0" CssClass="List" ShowFooter="True">
											<AlternatingItemStyle CssClass=""></AlternatingItemStyle>
											<ItemStyle CssClass=""></ItemStyle>
											<HeaderStyle CssClass="list-title"></HeaderStyle>
											<FooterStyle CssClass="list-title"></FooterStyle>
											<Columns>
												<asp:TemplateColumn HeaderText="&lt;input type='checkbox' name='chkAll' onclick='ChkSelectAll(document.all.chkSelect, document.all.chkAll.checked);' title='全选或全不选'&gt;">
													<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<input type="checkbox" name="chkSelect" class="list-checkbox" onclick="ChkSelectRow('<%# Container.ItemIndex + 1%>', this, document.all.dgList, 'list-2', '');" value='<%#DataBinder.Eval(Container, "DataItem.VoucherCode")%>'>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:HyperLinkColumn DataNavigateUrlField="VoucherCode" DataNavigateUrlFormatString="javascript:DoViewVoucherInfo('{0}');"
													DataTextField="VoucherID" HeaderText="凭证编号" FooterText="合计"></asp:HyperLinkColumn>
												<asp:TemplateColumn HeaderText="凭证类型">
													<ItemTemplate>
														<%# RmsPM.BLL.VoucherRule.GetVoucherTypeName(DataBinder.Eval(Container, "DataItem.VoucherType").ToString())%>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="TotalMoney" HeaderText="金额(元)" DataFormatString="{0:N}">
													<HeaderStyle HorizontalAlign="Right" Wrap="False"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right" Wrap="False"></ItemStyle>
													<FooterStyle HorizontalAlign="Right" Wrap="False"></FooterStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="AccountantName" HeaderText="制单人">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="MakeDate" HeaderText="制单日期" DataFormatString="{0:yyyy-MM-dd}">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="CheckPersonName" HeaderText="审核人">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="CheckDate" HeaderText="审核日期" DataFormatString="{0:yyyy-MM-dd}">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="OutPutDate" HeaderText="导出日期" DataFormatString="{0:yyyy-MM-dd}">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
												</asp:BoundColumn>												<asp:TemplateColumn HeaderText="状态">
													<ItemTemplate>
														<%# DataBinder.Eval(Container,"DataItem.StatusName") %>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle Visible="false" NextPageText="下页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上页" HorizontalAlign="Right"
												CssClass="ListHeadTr"></PagerStyle>
										</asp:datagrid>
									</td>
								</tr>
								<tr>
								    <td>
    									<cc1:GridPagination id="GridPagination1" runat="server" ControlSourceUrl="../Images/GridPaginationSource/" DataGridId="dgList" onpageindexchange="GridPagination1_PageIndexChange"></cc1:GridPagination>
								    </td>
								</tr>
							</table>
						</div>
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
			<input type="hidden" runat="server" id="txtProjectCode" name="txtProjectCode"><input type="hidden" runat="server" id="txtSelect" name="txtSelect">
			<div style="DISPLAY:none"><input type="button" name="btnDownloadHidden" id="btnDownloadHidden" runat="server"></div>
			<script language="javascript">
<!--
var CurrUrl = window.location.href;

function DoAddNewVoucher()
{
	OpenCustomWindow("../Finance/VoucherModify.aspx?ProjectCode=" + Form1.txtProjectCode.value,"凭证修改", 780, 580);
}

function DoViewVoucherInfo(VoucherCode)
{
	window.navigate('../Finance/VoucherInfo.aspx?VoucherCode='+ VoucherCode + "&FromUrl=" + escape(CurrUrl));
}

function ShowAdvanceSearch()
{
	var objDiv = document.all("divAdvanceSearch");
	if ( objDiv.style.display == "none" )
	{
		objDiv.style.display = "";
	}
	else
	{
		objDiv.style.display = "none";
	}
}

//总帐导入
function BalanceInput()
{
	OpenCustomWindow('VoucherBalanceInput.aspx?ProjectCode=<%=Request["ProjectCode"]%>','导入总帐凭证', 430, 300);
}

//导入
function inputVoucher()
{
	OpenCustomWindow('VoucherInput.aspx?ProjectCode=<%=Request["ProjectCode"]%>','导入凭证', 430, 220);
}

//审核
function Check()
{
	var s = ChkGetSelected(document.all.chkSelect);

	if (s == "")
	{
		alert('请选择一条或多条记录');
		return false;
	}

	Form1.txtSelect.value = s;

	OpenCustomWindow("../Finance/VoucherCheck.aspx?VoucherCode=" + s, "凭证审核", 550, 350);

//	document.all.btnDownloadHidden.click();
}

//导出
function Download()
{
	var s = ChkGetSelected(document.all.chkSelect);

	if (s == "")
	{
		alert('请选择一条或多条记录');
		return false;
	}

	Form1.txtSelect.value = s;

	OpenCustomWindow("../Finance/VoucherFileCheck.aspx?VoucherCode=" + s, "凭证导出", 550, 350);

//	document.all.btnDownloadHidden.click();
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

//-->
			</script>
		</form>
	</body>
</HTML>
