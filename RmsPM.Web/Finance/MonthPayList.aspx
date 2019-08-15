<%@ Register TagPrefix="uc1" TagName="CostBudgetSelectMonth" Src="../CostBudget/CostBudgetSelectYm.ascx" %>
<%@ Page language="c#" Inherits="RmsPM.Web.Finance.MonthPayList" CodeFile="MonthPayList.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ÿ�¸�����ܱ�</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<LINK href="../Images/locked-column.css" type="text/css" rel="stylesheet">
		<LINK href="../CostBudget/CostBudget.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<SCRIPT language="javascript" src="../CostBudget/CostBudgetDtlTree.js" charset="gb2312"></SCRIPT>
		<SCRIPT language="javascript" src="../CostBudget/CostBudgetInfo.js" charset="gb2312"></SCRIPT>
		<script language="javascript" src="../Images/ContentMenu.js"></script>
		<SCRIPT language="javascript">
<!--

//�л�������
function sltTypeChange()
{
	var Type = document.all("sltType").value;

    if (Type != "")
    {
        Form1.btnCostBudgetSetChange.click();
    }	
}

//�л�Ԥ���
function sltCostBudgetSetChange()
{
	var CostBudgetSetCode = document.all("sltCostBudgetSet").value;

    if (CostBudgetSetCode != "")
    {
        Form1.btnCostBudgetSetChange.click();
    }	
}

//�鿴�Ѹ���ϸ
function ViewPayout(CostCode, PayoutDateBegin, PayoutDateEnd)
{
	var Type = document.all("sltType").value;
	
	if (Type == "payout") //�Ѹ�
        OpenFullWindow("../Finance/ShowPayoutItemList.aspx?ProjectCode=" + document.all.txtProjectCode.value + "&CostBudgetSetCode=" + Form1.txtCostBudgetSetCode.value + "&CostCode=" + CostCode + "&PayoutDateBegin=" + PayoutDateBegin + "&PayoutDateEnd=" + PayoutDateEnd, 'ShowPayoutItemList');
    else //����
        OpenFullWindow("../Finance/ShowPaymentItemList.aspx?ProjectCode=" + document.all.txtProjectCode.value + "&CostBudgetSetCode=" + Form1.txtCostBudgetSetCode.value + "&CostCode=" + CostCode + "&PayDateBegin=" + PayoutDateBegin + "&PayDateEnd=" + PayoutDateEnd, 'ShowPaymentItemList');
}

//-->
		</SCRIPT>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" onload="winload();" rightMargin="0" onload="winload()">
		<form id="Form1" method="post" runat="server">
			<div style="DISPLAY: none">&nbsp;&nbsp;&nbsp; <input id="btnRefresh" onclick="document.all.divHintLoad.style.display = '';" type="button"
					value="btnRefresh" name="btnRefresh" runat="server" onserverclick="btnRefresh_ServerClick">&nbsp;&nbsp;&nbsp; <input id="btnChangeMoneyUnit" onclick="BeforePost();document.all.divHintLoad.style.display = '';"
					type="button" value="btnChangeMoneyUnit" name="btnChangeMoneyUnit" runat="server" onserverclick="btnChangeMoneyUnit_ServerClick">
			    <input type="button" runat="server" id="btnCostBudgetSetChange" name="btnCostBudgetSetChange" value="btnCostBudgetSetChange" onclick="document.all.divHintLoad.style.display = '';" onserverclick="btnCostBudgetSetChange_ServerClick" />
			</div>
			<TABLE height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td bgColor="#e4eff6" height="6"></td>
				</tr>
				<tr>
					<td height="25">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="topic" background="../images/topic_bg.gif"><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle">��Ŀ����>������� 
									> <span id="spanTitle" runat="server">ÿ�¸���</span></td>
								<td width="9"><IMG height="25" src="../images/topic_corr.gif" width="9"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td class="tools-area" vAlign="top"><IMG src="../images/btn_li.gif" align="absMiddle">
					    <select class="select" id="sltType" name="sltType"
										runat="server" onchange="sltTypeChange();">
							<option value="payment" selected>����</option>
							<option value="payout">�Ѹ�</option>
						</select>
					    Ԥ���<select class="select" id="sltCostBudgetSet" name="sltCostBudgetSet"
										runat="server" onchange="sltCostBudgetSetChange();">
									</select>
					    <IMG src="../images/btn_li.gif" align="absMiddle">
						<input class="button" id="btnPrint" onclick="Print()" type="button" value="�� ӡ" name="btnPrint"
							runat="server"> <input style="display:none" class="button" id="btnExcel" onclick="ScheduleExcel()" type="button" value="Excel" name="btnExcel"
							runat="server"> <input class="button" id="btnGoBack" style="DISPLAY: none" onclick="GoBack();" type="button"
							value="�� ��" name="btnGoBack"> <input class="button" id="btnClose" style="DISPLAY: none" onclick="window.close();" type="button"
							value="�� ��" name="btnClose">
					</td>
				</tr>
				<tr height="100%">
					<td class="table" id="tdList" vAlign="top">
						<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td id="tdList1">
									<table cellSpacing="0" cellPadding="0" border="0">
										<tr>
											<td class="note" height="30"><asp:label Runat="server" ID="lblProjectName"></asp:label><asp:label Runat="server" ID="lblCostBudgetSetName"></asp:label>ÿ��<asp:label runat="server" ID="lblTypeTitle"></asp:label>����ܱ�Ԫ
												<select class="select" id="sltMoneyUnit" style="DISPLAY: none" onchange="document.all.btnChangeMoneyUnit.click();"
													name="sltMoneyUnit" runat="server">
													<option value="wan">��Ԫ</option>
													<option value="yuan">Ԫ</option>
													<option value="fen" selected>��</option>
												</select>��</td>
											<td>&nbsp;&nbsp;&nbsp;&nbsp;<uc1:costbudgetselectmonth id="ucCostBudgetSelectMonth" runat="server" OnClientPost="BeforePost()" ShowMonthVisible="False"
													ShowMonth="True" MaxYearsBetween=20 MonthTitle="��ʾ���ʵ��"></uc1:costbudgetselectmonth></td>
										</tr>
									</table>
								</td>
							</tr>
							<tr height="100%">
								<td vAlign="top">
									<div id="tbl-container" onkeydown="return ListKeyDown(this);">
										<table class="tbl-list" id="Tree">
											<thead runat="server" id="threadList">
												<tr class="list-title">
													<th noWrap align="center" rowSpan="2">
														������</th>
													<th noWrap align="center" rowSpan="2">
														�ۼ���<asp:label runat="server" ID="lblTypeTitle2"></asp:label>
													</th>
													<%=RmsPM.BLL.ConvertRule.ToString(ViewState["html_title1"])%>
												</tr>
												<tr class="list-title">
													<%=RmsPM.BLL.ConvertRule.ToString(ViewState["html_title2"])%>
												</tr>
											</thead>
											<tbody>
												<asp:repeater id="dgList" Runat="server" EnableViewState="false">
													<ItemTemplate>
<tr style="display:none" id='TreeNode_<%# DataBinder.Eval(Container, "DataItem.DtlCode") %>' Deep='<%# DataBinder.Eval(Container, "DataItem.Deep") %>' ParentCode='<%# DataBinder.Eval(Container, "DataItem.ParentCode") %>' ChildCount='<%# DataBinder.Eval(Container, "DataItem.ChildCount") %>' RowIndex='<%# Container.ItemIndex %>' DefaultExpand='<%# DataBinder.Eval(Container, "DataItem.IsExpand") %>' onclick="RowClick(this);">
<td nowrap title='<%# DataBinder.Eval(Container, "DataItem.SortID") %>'><span id='TreeNodeSpan_<%# DataBinder.Eval(Container, "DataItem.DtlCode") %>'></span><span style='width:15px'><img id='TreeNodeImg_<%# DataBinder.Eval(Container, "DataItem.DtlCode") %>' style="display:none;cursor:hand" onclick="CBTree_ImgExpandClick(this);"></span><%# DataBinder.Eval(Container, "DataItem.CostName") %></td>
<td nowrap align="right" class='<%# DataBinder.Eval(Container, "DataItem.ClassTd") %>'><%# RmsPM.BLL.MonthPayList.GetPayoutHref(GetMoneyShowString(DataBinder.Eval(Container, "DataItem.PayoutMoney")), DataBinder.Eval(Container, "DataItem.CostCode"), "", "")%></td>
<%# DataBinder.Eval(Container, "DataItem.PlanDataHtml") %>
</tr>
													</ItemTemplate>
												</asp:repeater></tbody></table>
									</div>
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
			</TABLE>
			<div id="divHintLoad" style="DISPLAY: none; LEFT: 1px; WIDTH: 100%; POSITION: absolute; TOP: 200px; TEXT-ALIGN: center">
				<TABLE id="tableHintLoad" height="80" cellSpacing="0" cellPadding="0" border="0">
					<TR>
						<TD vAlign="top" align="center"><iframe id="frameLoad" src="../Cost/LoadingPrepare.htm" frameBorder="no" scrolling="no"
								height="100%"></iframe>
						</TD>
					</TR>
				</TABLE>
			</div>
			<div id="divHintSave" style="DISPLAY: none; LEFT: 1px; WIDTH: 100%; POSITION: absolute; TOP: 200px; TEXT-ALIGN: center">
				<TABLE id="tableHintSave" height="100" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD vAlign="top" align="center"><iframe id="frameSave" src="../Cost/SavingWating.htm" frameBorder="no" scrolling="auto"
								height="100%"></iframe>
						</TD>
					</TR>
				</TABLE>
			</div>
			<input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server"><input id="txtFromUrl" type="hidden" name="txtFromUrl" runat="server">
			<input id="txtCostBudgetSetCode" type="hidden" name="txtCostBudgetSetCode" runat="server">
			<INPUT id="txtListScrollTop" type="hidden" name="txtListScrollTop" runat="server"><INPUT id="txtListScrollLeft" type="hidden" name="txtListScrollLeft" runat="server">
			<INPUT id="txtLastSelectedRowID" type="hidden" name="txtLastSelectedRowID" runat="server">
			<INPUT id="txtLastSelectedRowClass" type="hidden" name="txtLastSelectedRowClass" runat="server"><INPUT id="Hidden3" type="hidden" name="txtListScrollLeft" runat="server">
			<input id="txtStartYm" type="hidden" name="txtStartYm" runat="server"><input id="txtEndYm" type="hidden" name="txtEndYm" runat="server">
		</form>
		<script language="javascript">
<!--

/*
//������Excel
function ScheduleExcel()
{
    var StartY = '<%=ViewState["StartY"]%>';
    var EndY = '<%=ViewState["EndY"]%>';
	OpenCustomWindow("ContractPaySchedule.aspx?ProjectCode=" + Form1.txtProjectCode.value + "&StartY=" + StartY + "&EndY=" + EndY + "&PBSType=" + Form1.txtPBSType.value + "&PBSCode=" + Form1.txtPBSCode.value + "&act=Excel", "ContractPayScheduleExcel", 400, 250);
}
*/

//-->
		</script>
	</body>
</HTML>
