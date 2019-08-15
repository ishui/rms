<%@ Register TagPrefix="uc1" TagName="CostBudgetSelectYm" Src="../CostBudget/CostBudgetSelectYm.ascx" %>
<%@ Page language="c#" Inherits="RmsPM.Web.Finance.ContractPaySchedule" CodeFile="ContractPaySchedule.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>���̸���һ����</title>
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

//-->
		</SCRIPT>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" onload="winload();" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<div style="DISPLAY: none">&nbsp;&nbsp;&nbsp; <input id="btnRefresh" onclick="document.all.divHintLoad.style.display = '';" type="button"
					value="btnRefresh" name="btnRefresh" runat="server" onserverclick="btnRefresh_ServerClick">&nbsp;&nbsp;&nbsp; <input id="btnChangeMoneyUnit" onclick="BeforePost();document.all.divHintLoad.style.display = '';"
					type="button" value="btnChangeMoneyUnit" name="btnChangeMoneyUnit" runat="server" onserverclick="btnChangeMoneyUnit_ServerClick">
			    <input type="button" runat="server" id="btnPBSChange" name="btnPBSChange" value="btnPBSChange" onclick="document.all.divHintLoad.style.display = '';" onserverclick="btnPBSChange_ServerClick" />
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
									> <span id="spanTitle" runat="server">����һ��</span></td>
								<td width="9"><IMG height="25" src="../images/topic_corr.gif" width="9"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td class="tools-area" vAlign="top"><IMG src="../images/btn_li.gif" align="absMiddle">
					    ��λ���̣�<select class="select" id="sltPBS" name="sltPBS" runat="server" onchange="sltPBSChange();">
					        <option value="">--��ѡ��--</option>
					    </select>
					    <IMG src="../images/btn_li.gif" align="absMiddle">
						<input class="button" id="btnPrint" onclick="Print()" type="button" value="�� ӡ" name="btnPrint"
							runat="server"> <input class="button" id="btnExcel" onclick="ScheduleExcel()" type="button" value="Excel" name="btnExcel"
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
											<td class="note" height="30"><asp:label Runat="server" ID="lblProjectName"></asp:label><asp:label Runat="server" ID="lblPBSName"></asp:label>���̸���һ����Ԫ
												<select class="select" id="sltMoneyUnit" style="DISPLAY: none" onchange="document.all.btnChangeMoneyUnit.click();"
													name="sltMoneyUnit" runat="server">
													<option value="wan">��Ԫ</option>
													<option value="yuan">Ԫ</option>
													<option value="fen" selected>��</option>
												</select>��</td>
											<td>&nbsp;&nbsp;&nbsp;&nbsp;<uc1:costbudgetselectym id="ucCostBudgetSelectMonth" runat="server" OnClientPost="" ShowMonthVisible="True"
										ShowMonth="False" ShowButton="True" MonthTitle="��ʾ�¶�ʵ��"></uc1:costbudgetselectym>
                                            </td>
										</tr>
									</table>
								</td>
							</tr>
							<tr height="100%">
								<td vAlign="top">
									<div id="tbl-container" onkeydown="return ListKeyDown(this);">
										<table class="tbl-list" id="Tree">
											<thead>
												<tr class="list-title">
													<th noWrap align="center" rowSpan="2">
														������</th>
													<th noWrap align="center" rowSpan="2">
														��ͬ���</th>
													<th noWrap align="center" rowSpan="2">
														��ͬ����</th>
													<th noWrap align="center" rowSpan="2">
														�а���</th>
													<th noWrap align="center" rowSpan="2">
														ԭʼ��ͬ���
													</th>
													<th noWrap align="center" rowSpan="2">
														������
													</th>
													<th noWrap align="center" rowSpan="2">
														�������
													</th>
													<th noWrap align="center" rowSpan="2">
														�ۼ�����
													</th>
													<th noWrap align="center" rowSpan="2">
														�ۼ��Ѹ�
													</th>
													<th noWrap align="center" rowSpan="2">
														����δ��
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
<td nowrap class='<%# DataBinder.Eval(Container, "DataItem.ClassTd") %>'><%# DataBinder.Eval(Container, "DataItem.ContractID") %></td>
<td nowrap class='<%# DataBinder.Eval(Container, "DataItem.ClassTd") %>'><%# RmsPM.BLL.ContractPaySchedule.GetContractHref(DataBinder.Eval(Container, "DataItem.RecordType"), DataBinder.Eval(Container, "DataItem.ContractCode"), DataBinder.Eval(Container, "DataItem.ContractName"))%></td>
<td nowrap class='<%# DataBinder.Eval(Container, "DataItem.ClassTd") %>'><a href="#" onclick="ViewSupplierInfo(this.Code);return false;" Code='<%# DataBinder.Eval(Container, "DataItem.SupplierCode") %>'><%# DataBinder.Eval(Container, "DataItem.SupplierName") %></a></td>
<td nowrap align="right" class='<%# DataBinder.Eval(Container, "DataItem.ClassTd") %>'><%# GetMoneyShowString(DataBinder.Eval(Container, "DataItem.ContractMoney")) %></td>
<td nowrap align="right" class='<%# DataBinder.Eval(Container, "DataItem.ClassTd") %>'><%# GetMoneyShowString(DataBinder.Eval(Container, "DataItem.ContractChangeMoney")) %></td>
<td nowrap align="right" class='<%# DataBinder.Eval(Container, "DataItem.ClassTd") %>'><%# GetMoneyShowString(DataBinder.Eval(Container, "DataItem.ContractTotalMoney")) %></td>
<td nowrap align="right" class='<%# DataBinder.Eval(Container, "DataItem.ClassTd") %>'><%# RmsPM.BLL.ContractPaySchedule.GetContractPayHref(DataBinder.Eval(Container, "DataItem.RecordType"), GetMoneyShowString(DataBinder.Eval(Container, "DataItem.ContractPay")), DataBinder.Eval(Container, "DataItem.CostCode"), DataBinder.Eval(Container, "DataItem.ContractCode"), "", txtPBSType.Value, txtPBSCode.Value)%></td>
<td nowrap align="right" class='<%# DataBinder.Eval(Container, "DataItem.ClassTd") %>'><%# RmsPM.BLL.ContractPaySchedule.GetContractPayRealHref(DataBinder.Eval(Container, "DataItem.RecordType"), GetMoneyShowString(DataBinder.Eval(Container, "DataItem.ContractPayReal")), DataBinder.Eval(Container, "DataItem.CostCode"), DataBinder.Eval(Container, "DataItem.ContractCode"), "", "", "", txtPBSType.Value, txtPBSCode.Value)%></td>
<td nowrap align="right" class='<%# DataBinder.Eval(Container, "DataItem.ClassTd") %>'><%# RmsPM.BLL.ContractPaySchedule.GetContractPayRealBalanceHref(DataBinder.Eval(Container, "DataItem.RecordType"), GetMoneyShowString(DataBinder.Eval(Container, "DataItem.ContractPayRealBalance")), DataBinder.Eval(Container, "DataItem.CostCode"), DataBinder.Eval(Container, "DataItem.ContractCode"), "", txtPBSType.Value, txtPBSCode.Value)%></td>
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
			<INPUT id="txtPBSType" type="hidden" name="txtPBSType" runat="server"><INPUT id="txtPBSCode" type="hidden" name="txtPBSCode" runat="server">
		</form>
		<script language="javascript">
<!--

if (window.opener)
{
//	Form1.btnClose.style.display = "";
//	Form1.btnGoBack.style.display = "none";
}

function sltPBSChange()
{
    document.all.btnPBSChange.click();
}

//������Excel
function ScheduleExcel()
{
    var StartYm = '<%=ViewState["StartYm"]%>';
    var EndYm = '<%=ViewState["EndYm"]%>';
	OpenCustomWindow("ContractPaySchedule.aspx?ProjectCode=" + Form1.txtProjectCode.value + "&StartYm=" + StartYm + "&EndYm=" + EndYm + "&PBSType=" + Form1.txtPBSType.value + "&PBSCode=" + Form1.txtPBSCode.value + "&act=Excel", "ContractPayScheduleExcel", 400, 250);
}

//-->
		</script>
	</body>
</HTML>
