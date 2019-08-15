<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v5.1, Version=5.1.20051.37, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Page language="c#" CodeFile="SalBudgetListInfra.aspx.cs" AutoEventWireup="false" Inherits="RmsPM.Web.Sal.SalBudgetListInfra" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>SalBudgetList</title>
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
			<div style="DISPLAY: none"><input id="btnHiddenYear" onclick="document.all.divHintLoad.style.display='';" type="button"
					name="btnHiddenYear" runat="server"></div>
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
				<TR id="trToolbarView" runat="server">
					<TD class="tools-area" vAlign="top"><IMG src="../images/btn_li.gif" align="absMiddle">
						<input class="button" id="btnAddBudget" onclick="if (!AddBudget()) return false;" type="button"
							value="新增计划" name="btnAddBudget" runat="server"> <input class="button" id="btnModifyBudget" onclick="if (!ModifyBudget()) return false;"
							type="button" value="修改计划" name="btnModifyBudget" runat="server"> <input class="button" id="btnModifyAct" style="DISPLAY: none" onclick="ModifyAct()" type="button"
							value="修改实际" name="btnModifyAct" runat="server"> <IMG src="../images/btn_li.gif" align="absMiddle">
						年度：<select id="sltYear" onchange="btnHiddenYear.click();" name="sltYear" runat="server"></select>
					</TD>
				</TR>
				<TR id="trToolbarSave" style="DISPLAY: none" runat="server">
					<TD class="tools-area" vAlign="top"><IMG src="../images/btn_li.gif" align="absMiddle">
						<input class="button" id="btnSave" onclick="if (!Save()) return false;" type="button" value="保 存"
							name="btnSave" runat="server"> <input class="button" id="btnCancel" onclick="if (!Cancel()) return false;" type="button"
							value="返 回" name="btnCancel" runat="server">
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
					<td class="table" vAlign="top"><igtbl:ultrawebgrid id="UltraWebGrid1" runat="server" Height="100%" Width="100%">
							<DisplayLayout UseFixedHeaders="True" JavaScriptFileName="../images/infragistics/20051/scripts/ig_WebGrid.js"
								StationaryMargins="HeaderAndFooter" AutoGenerateColumns="False" JavaScriptFileNameCommon="../images/infragistics/20051/Scripts/ig_shared.js"
								RowHeightDefault="23px" Version="4.00" SelectTypeRowDefault="Extended" HeaderClickActionDefault="SortSingle"
								IndentationDefault="20" BorderCollapseDefault="Separate" AllowColSizingDefault="Free" EnableClientSideRenumbering="True"
								RowSelectorsDefault="No" Name="UltraWebGrid1" TableLayout="Fixed" CellClickActionDefault="Edit">
								<HeaderStyleDefault Height="40px" CssClass="grid-title"></HeaderStyleDefault>
								<RowSelectorStyleDefault CssClass="grid-selector"></RowSelectorStyleDefault>
								<FrameStyle Width="100%" TextOverflow="Ellipsis" Font-Names="Tahoma,宋体" Height="100%" CssClass="grid"></FrameStyle>
								<FooterStyleDefault CssClass="grid-footer"></FooterStyleDefault>
								<ClientSideEvents BeforeEnterEditModeHandler="UltraWebGrid1_BeforeEnterEditModeHandler"></ClientSideEvents>
								<FixedHeaderStyleDefault CssClass="grid-title"></FixedHeaderStyleDefault>
								<EditCellStyleDefault CssClass="grid-edit"></EditCellStyleDefault>
								<SelectedRowStyleDefault CssClass="grid-select"></SelectedRowStyleDefault>
								<FixedFooterStyleDefault CssClass="grid-footer"></FixedFooterStyleDefault>
								<RowAlternateStyleDefault CssClass="grid-row-i"></RowAlternateStyleDefault>
								<RowStyleDefault CssClass="grid-row"></RowStyleDefault>
								<ImageUrls ImageDirectory="../images/infragistics/Images/"></ImageUrls>
								<FixedCellStyleDefault CssClass="grid-row"></FixedCellStyleDefault>
							</DisplayLayout>
							<Bands>
								<igtbl:UltraGridBand BaseTableName="SalBudget" Key="SalBudget">
									<Columns>
										<igtbl:UltraGridColumn HeaderText="" Key="IsAct" IsBound="True" Hidden="True" BaseColumnName="IsAct" FooterText="">
											<CellStyle CssClass="grid-row-c"></CellStyle>
											<Footer Formula="" Key="" Caption=""></Footer>
											<Header Fixed="True" Key="" Caption=""></Header>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="" Key="ItemDesc" IsBound="True" MergeCells="True" Width="70px" BaseColumnName="ItemDesc"
											AllowUpdate="No" FooterText="">
											<CellStyle HorizontalAlign="Center" CssClass="grid-row-c"></CellStyle>
											<Footer Formula="" Key="" Caption=""></Footer>
											<Header Fixed="True" Key="" Caption=""></Header>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="" Key="IsActName" IsBound="True" Width="50px" BaseColumnName="IsActName"
											AllowUpdate="No" FooterText="">
											<CellStyle HorizontalAlign="Center" CssClass="grid-row-c"></CellStyle>
											<Footer Formula="" Key="" Caption=""></Footer>
											<Header Fixed="True" Key="" Caption=""></Header>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="" Key="FieldName" IsBound="True" Hidden="True" BaseColumnName="FieldName"
											FooterText="">
											<CellStyle CssClass="grid-row-c"></CellStyle>
											<Footer Formula="" Key="" Caption=""></Footer>
											<Header Fixed="True" Key="" Caption=""></Header>
										</igtbl:UltraGridColumn>
										<igtbl:TemplatedColumn Key="" IsBound="True" Hidden="True" HeaderText="序号" BaseColumnName="sno" FooterText="">
											<Footer Formula="" Key="" Caption=""></Footer>
											<Header Key="" Caption="序号"></Header>
										</igtbl:TemplatedColumn>
										<igtbl:UltraGridColumn HeaderText="期前累计" Key="" IsBound="True" BaseColumnName="y0" AllowUpdate="No" FooterText="">
											<CellStyle HorizontalAlign="Right"></CellStyle>
											<Footer Formula="" Key="" Caption=""></Footer>
											<Header Key="" Caption="期前累计"></Header>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="1" Key="m1" IsBound="True" BaseColumnName="m1" FooterText="">
											<CellStyle HorizontalAlign="Right"></CellStyle>
											<Footer Formula="" Key="" Caption=""></Footer>
											<Header Key="" Caption="1"></Header>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="2" Key="m2" IsBound="True" BaseColumnName="m2" FooterText="">
											<CellStyle HorizontalAlign="Right"></CellStyle>
											<Footer Formula="" Key="" Caption=""></Footer>
											<Header Key="" Caption="2"></Header>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="3" Key="m3" IsBound="True" BaseColumnName="m3" FooterText="">
											<CellStyle HorizontalAlign="Right"></CellStyle>
											<Footer Formula="" Key="" Caption=""></Footer>
											<Header Key="" Caption="3"></Header>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="4" Key="m4" IsBound="True" BaseColumnName="m4" FooterText="">
											<CellStyle HorizontalAlign="Right"></CellStyle>
											<Footer Formula="" Key="" Caption=""></Footer>
											<Header Key="" Caption="4"></Header>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="5" Key="m5" IsBound="True" BaseColumnName="m5" FooterText="">
											<CellStyle HorizontalAlign="Right"></CellStyle>
											<Footer Formula="" Key="" Caption=""></Footer>
											<Header Key="" Caption="5"></Header>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="6" Key="m6" IsBound="True" BaseColumnName="m6" FooterText="">
											<CellStyle HorizontalAlign="Right"></CellStyle>
											<Footer Formula="" Key="" Caption=""></Footer>
											<Header Key="" Caption="6"></Header>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="7" Key="m7" IsBound="True" BaseColumnName="m7" FooterText="">
											<CellStyle HorizontalAlign="Right"></CellStyle>
											<Footer Formula="" Key="" Caption=""></Footer>
											<Header Key="" Caption="7"></Header>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="8" Key="m8" IsBound="True" BaseColumnName="m8" FooterText="">
											<CellStyle HorizontalAlign="Right"></CellStyle>
											<Footer Formula="" Key="" Caption=""></Footer>
											<Header Key="" Caption="8"></Header>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="9" Key="m9" IsBound="True" BaseColumnName="m9" FooterText="">
											<CellStyle HorizontalAlign="Right"></CellStyle>
											<Footer Formula="" Key="" Caption=""></Footer>
											<Header Key="" Caption="9"></Header>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="10" Key="m10" IsBound="True" BaseColumnName="m10" FooterText="">
											<CellStyle HorizontalAlign="Right"></CellStyle>
											<Footer Formula="" Key="" Caption=""></Footer>
											<Header Key="" Caption="10"></Header>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="11" Key="m11" IsBound="True" BaseColumnName="m11" FooterText="">
											<CellStyle HorizontalAlign="Right"></CellStyle>
											<Footer Formula="" Key="" Caption=""></Footer>
											<Header Key="" Caption="11"></Header>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="12" Key="m12" IsBound="True" BaseColumnName="m12" FooterText="">
											<CellStyle HorizontalAlign="Right"></CellStyle>
											<Footer Formula="" Key="" Caption=""></Footer>
											<Header Key="" Caption="12"></Header>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="年度计划" Key="m0" IsBound="True" BaseColumnName="m0" FooterText="">
											<CellStyle HorizontalAlign="Right"></CellStyle>
											<Footer Formula="" Key="" Caption=""></Footer>
											<Header Key="" Caption="年度计划"></Header>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="年度计划" Key="y1" IsBound="True" BaseColumnName="y1" FooterText="">
											<CellStyle HorizontalAlign="Right"></CellStyle>
											<Footer Formula="" Key="" Caption=""></Footer>
											<Header Key="" Caption="年度计划"></Header>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="年度计划" Key="y2" IsBound="True" BaseColumnName="y2" FooterText="">
											<CellStyle HorizontalAlign="Right"></CellStyle>
											<Footer Formula="" Key="" Caption=""></Footer>
											<Header Key="" Caption="年度计划"></Header>
										</igtbl:UltraGridColumn>
									</Columns>
								</igtbl:UltraGridBand>
							</Bands>
						</igtbl:ultrawebgrid></td>
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
			<input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server"> <input id="txtYear" type="hidden" name="txtYear" runat="server">
			<input id="txtProjectName" type="hidden" name="txtProjectName" runat="server">
		</form>
		<SCRIPT language="javascript">
<!--

var CurrUrl = window.location.href;

//新增计划
function AddBudget()
{
	document.all.divHintLoad.style.display = "";
	return true;
//	OpenFullWindow("../Sal/SalBudgetModify.aspx?FromUrl=" + escape(CurrUrl) + "&ProjectCode=" + Form1.txtProjectCode.value + "&Year=" + Form1.sltYear.value + "&RefreshScript=DoRefresh();", "销售计划修改");
}

//修改计划
function ModifyBudget()
{
	document.all.divHintLoad.style.display = "";
	return true;
//	OpenFullWindow("../Sal/SalBudgetModify.aspx?FromUrl=" + escape(CurrUrl) + "&ProjectCode=" + Form1.txtProjectCode.value + "&Year=" + Form1.sltYear.value + "&RefreshScript=DoRefresh();", "销售计划修改");
}

//修改实际
function ModifyAct()
{
	OpenFullWindow("../Sal/SalIncomeModify.aspx?FromUrl=" + escape(CurrUrl) + "&ProjectCode=" + Form1.txtProjectCode.value + "&Year=" + Form1.sltYear.value, "销售实际修改");
}

//显示年份标题
function DisplayTitle()
{
/*
	var year = parseInt(Form1.sltYear.value);
	
	document.all.titleYear.innerText = year;
	document.all.titleYear1.innerText = year + 1;
	document.all.titleYear2.innerText = year + 2;
*/
}

//刷新
function DoRefresh()
{
	Form1.btnHiddenYear.click();
}

//保存
function Save()
{
	document.all.divHintSave.style.display = "";
	return true;
}

//取消保存
function Cancel()
{
	document.all.divHintSave.style.display = "";
	return true;
}

function UltraWebGrid1_BeforeEnterEditModeHandler(gridName, cellId)
{
   	var row = igtbl_getRowById(cellId);
	var cell = row.getCell(0);
	var IsAct = cell.getValue();
	
	if (IsAct == "1") //实际不可修改
		return 1;
	else
		return 0;
}

DisplayTitle();

//-->
		</SCRIPT>
	</body>
</HTML>
