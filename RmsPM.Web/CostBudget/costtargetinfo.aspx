<%@ Page language="c#" Inherits="RmsPM.Web.CostBudget.CostTargetInfo" CodeFile="CostTargetInfo.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="CostBudgetSelectMonth" Src="../CostBudget/CostBudgetSelectMonth.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Ԥ���Ŀ�����</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<LINK href="../CostBudget/CostBudget.css" type="text/css" rel="stylesheet">
		<LINK href="../Images/locked-column.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<SCRIPT language="javascript" src="CostBudgetDtlTree.js" charset="gb2312"></SCRIPT>
		<script language="javascript" src="../Images/ContentMenu.js"></script>
		<SCRIPT language="javascript">
<!--

var headCount = 2;

//����
function GoBack()
{
	if (Form1.txtFromUrl.value == "")
	{
		window.history.go(-1);
//		window.location.href = "PaymentList.aspx?ProjectCode=" + Form1.txtProjectCode.value;
	}
	else
	{
		window.history.go(-1);
//		window.location.href = Form1.txtFromUrl.value;
	}
}

//�޸�
function Modify()
{
	window.location.href = "CostTargetModify.aspx?CostBudgetSetCode=" + Form1.txtCostBudgetSetCode.value;
//	OpenFullWindow("CostTargetModify.aspx?CostBudgetSetCode=" + Form1.txtCostBudgetSetCode.value, "Ԥ���Ŀ������޸�");
}
	
//�޸ĵ���Ŀ�����
function ModifyTargetMoney(CostCode)
{
	window.location.href = "CostTargetModify.aspx?CostBudgetSetCode=" + Form1.txtCostBudgetSetCode.value + "&CostCode=" + CostCode;
//	OpenFullWindow("CostTargetModify.aspx?CostBudgetSetCode=" + Form1.txtCostBudgetSetCode.value, "Ԥ���Ŀ������޸�");
}
	
//���
function DoCheck()
{
	if (!confirm("ȷʵ���ͨ����")) return false;
	
	document.all.divHintSave.style.display = '';
	return true;
	
//	OpenCustomWindow("CostTargetCheck.aspx?PaymentCode=' + paymentCode,"Ԥ���Ŀ��������", 600, 400);
}

//�鿴��ʷ
function ViewHistory()
{
	OpenCustomWindow("CostBudgetHistoryList.aspx?TargetFlag=1&CostBudgetCode=" + Form1.txtCostBudgetCode.value, "Ŀ�������ʷ", 780, 560);
}

//Ԥ�������
function ModifySet()
{
	OpenCustomWindow("CostBudgetSetModify.aspx?CostBudgetSetCode=" + Form1.txtCostBudgetSetCode.value, "Ԥ�������", 500, 350);
}

//��������Ϣ
function ViewCostCode(code)
{
	OpenFullWindow('../Cost/DynamicCostInfo.aspx?ProjectCode=' + Form1.txtProjectCode.value + '&CostCode=' + code ,'��̬��������Ϣ');
}

//��ʾ��ʷԤ����
function ShowTargetMoneyHis()
{
	Form1.btnShowBudgetMoneyHis.click();
}

//������ʷԤ����
function HideTargetMoneyHis()
{
	Form1.btnHideBudgetMoneyHis.click();
}

//��ӡ
function Print()
{
	OpenPrintWindow("../Report/PrintList.aspx?FromControlID=tdMaster$tdList", "��ӡ");
}

//���Ҽ�
function RowMouseClick(sender)
{
	if (event.button==2)
	{
		ShowEditMenu(sender);
		CBTree_SetRowSelected(sender);
	}
}

//�����˵�
function ShowEditMenu(sender)
{
return;
	var cssFile="../Images/ContentMenu.css";

	var rowCBS = sender;
		
	//�ϼ���
	if (rowCBS.key.substr(0, 2) == "R_") return;

	var Items = new Array();

	var i = -1;

/*	
	//�ӽڵ���
	var ChildCount = 0;
	if (rowCBS.ChildCount)
	{
		ChildCount = parseInt(rowCBS.ChildCount);
	}
*/
	
	var status = Form1.txtStatus.value;
	
	if (status != "2")  //����ʷ
	{
		i++;
		Items[i] = new Array(3);
		Items[i][0] = "�޸ĵ���Ŀ�����";
		Items[i][1] = "";
		Items[i][2] = "ModifyTargetMoney('" + rowCBS.key + "');";
	}

	CreateContentMenu(Items,cssFile,event.x-1,event.y-1);
}

//Ŀ����ñ��ⵯ���˵�
function ShowTargetHeadMenu(sender)
{
	var cssFile="../Images/ContentMenu.css";

	var Items = new Array();

	var i = -1;
	
	if (document.all.txtShowTargetHis.value == "1")
	{
		i++;
		Items[i] = new Array(3);
		Items[i][0] = "������ʷԤ��";
		Items[i][1] = "";
		Items[i][2] = "HideTargetMoneyHis();";
	}
	else
	{
//		if (document.all.txtHasTargetHis.value == "1")
//		{
			i++;
			Items[i] = new Array(3);
			Items[i][0] = "��ʾ������ʷԤ��";
			Items[i][1] = "";
			Items[i][2] = "ShowTargetMoneyHis('');";
//		}
	}

	if (i >= 0)
	{
		var offsetTop = 0;
		var offsetLeft = 0;
		
//		if (document.all(sender.id)[0])
//		{
			offsetTop = document.all("tbl-container").offsetTop + 2;
			offsetLeft = document.all("tbl-container").offsetLeft + 2 + document.all("tdTargetHead").offsetLeft;
//		}
		CreateContentMenu(Items,cssFile, event.x + offsetLeft, event.y + offsetTop);
	}
}

function winload()
{
	if (document.all("tbl-container"))	document.all("tbl-container").oncontextmenu=Function("return false;");

	CBTree_InitTree("Tree", "../images/plus.gif", "../images/minus.gif", headCount);

	CBTree_ExpandTreeByNodeDefaultExpand();
}

function Flowcheck()
{
 OpenFullWindow('<%=ViewState["_ObjectCostConfirmURL"]%>?ProjectCode=<%=ViewState["ProjectCode"]%>&CostBudgetCode=<%=Request["CostBudgetCode"]%>&CostBudgetSetCode=<%=Request["CostBudgetSetCode"]%>','Ŀ��ɱ����_<%=Request["CostBudgetCode"]%>');	
}

//-->
		</SCRIPT>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0" onload="winload();">
		<form id="Form1" method="post" runat="server">
			<div style="DISPLAY:none">
				<input type="button" runat="server" id="btnShowBudgetMoneyHis" name="btnShowBudgetMoneyHis"
					value="btnShowBudgetMoneyHis" onclick="document.all.divHintLoad.style.display = '';" onserverclick="btnShowBudgetMoneyHis_ServerClick">
				<input type="button" runat="server" id="btnHideBudgetMoneyHis" name="btnHideBudgetMoneyHis"
					value="btnHideBudgetMoneyHis" onclick="document.all.divHintLoad.style.display = '';" onserverclick="btnHideBudgetMoneyHis_ServerClick">
<input class="button" id="btnViewHistory" onclick="javascript:ViewHistory();" type="button"
							value="�� ʷ" name="btnViewHistory" runat="server">
			</div>
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" bgColor="#ffffff" border="0">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">Ԥ���Ŀ�����</td>
				</tr>
				<tr>
					<td class="tools-area" vAlign="top"><IMG src="../images/btn_li.gif" align="absMiddle">
						<input class="button" id="btnPrint" onclick="Print()" type="button" value="�� ӡ" name="Print"
							runat="server"> <input class="button" id="btnModify" onclick="Modify();" type="button" value="�� ��" name="btnModify"
							runat="server"> <input class="button" id="btnDelete" onclick="javascript:if(!window.confirm('ȷʵҪɾ��������¼��')) return false; document.all.divHintSave.style.display = '';"
							type="button" value="ɾ ��" name="btnDelete" runat="server" onserverclick="btnDelete_ServerClick"> <input class="button" id="btnCheck" onclick="javascript:if (!DoCheck()) return false;"
							type="button" value="�� ��" name="btnCheck" runat="server" onserverclick="btnCheck_ServerClick"> <input class="button" id="btnModifySet" onclick="javascript:ModifySet();" type="button"
							value="Ԥ�������" name="btnModifySet" runat="server"> 
							<input class="button" id="btnflowcheck" onclick="javascript:Flowcheck();" type="button"
							value="�ύĿ��ɱ��������" name="btnflowcheck" runat="server"> 
							<input class="button" id="btnGoBack" style="DISPLAY: none" onclick="GoBack();" type="button"
							value="�� ��" name="btnGoBack"> <input class="button" id="btnClose" onclick="window.close();" type="button" value="�� ��"
							name="btnClose">
					</td>
				</tr>
				<tr>
					<td class="table" id="tdMaster">
						<TABLE class="form" id="Table4" cellSpacing="0" cellPadding="0" width="100%">
							<TR>
								<TD class="form-item">Ԥ������ƣ�</TD>
								<TD><asp:label id="lblCostBudgetSetName" runat="server"></asp:label>
									<asp:label id="lblStatusName" Runat="server" ForeColor="red"></asp:label></TD>
								<TD class="form-item">�汾�ţ�</TD>
								<TD><asp:label id="lblVerID" Runat="server"></asp:label></TD>
								<TD class="form-item">�汾���ƣ�</TD>
								<TD><asp:label id="lblCostBudgetName" Runat="server"></asp:label></TD>
								<TD class="form-item">���</TD>
								<TD><asp:label id="lblGroupName" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="form-item">���ţ�</TD>
								<TD><asp:label id="lblUnitName" runat="server"></asp:label></TD>
								<TD class="form-item">��λ���̣�</TD>
								<TD><asp:label id="lblPBSName" runat="server"></asp:label></TD>
								<TD class="form-item">�� �� �ˣ�</TD>
								<TD><asp:label id="lblCreatePersonName" runat="server"></asp:label></TD>
								<TD class="form-item">�������ڣ�</TD>
								<TD><asp:label id="lblCreateDate" runat="server"></asp:label></TD>
								<!--
								<TD class="form-item">�����</TD>
								<TD colSpan="3"><asp:label id="lblCostSortID" runat="server"></asp:label><asp:label id="lblCostName" runat="server"></asp:label></TD>
								-->
							</TR>
							<TR>
								<TD class="form-item">����޸��ˣ�</TD>
								<TD><asp:label id="lblModifyPersonName" runat="server"></asp:label></TD>
								<TD class="form-item">����޸����ڣ�</TD>
								<TD><asp:label id="lblModifyDate" runat="server"></asp:label></TD>
								<TD class="form-item">�� �� �ˣ�</TD>
								<TD><asp:label id="lblCheckPersonName" runat="server"></asp:label></TD>
								<TD class="form-item">������ڣ�</TD>
								<TD><asp:label id="lblCheckDate" runat="server"></asp:label></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
				<tr height="100%">
					<td class="table" vAlign="top" id="tdList">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0" height="100%">
							<tr>
								<td>
									<table cellSpacing="0" cellPadding="0" border="0">
										<tr>
											<td class="intopic" width="200">Ŀ�������ϸ��Ԫ��</td>
											<td><uc1:CostBudgetSelectMonth id="ucCostBudgetSelectMonth" runat="server" MaxYearsBetween=20></uc1:CostBudgetSelectMonth></td>
										</tr>
									</table>
								</td>
							</tr>
							<tr height="100%">
								<td valign="top">
									<div id="tbl-container" style="OVERFLOW: auto; WIDTH: 100%; POSITION: absolute; HEIGHT: 100%">
										<table id="Tree" class="tbl-list" width="100%" cellpadding="0" cellspacing="0">
											<thead>
												<tr class="list-title">
													<th noWrap align="center" rowSpan="2">������</th>
													<th noWrap align="center" rowSpan="2" id="tdTargetHead"><A id="hrefTargetVerID" onclick="ShowTargetHeadMenu(this);return false;" href="#" runat="server">Ŀ�����</A></th>
													<%=RmsPM.BLL.ConvertRule.ToString(ViewState["TargetHisHead1"])%>
													<th noWrap align="center" rowSpan="2">˵��</th>
													<%=RmsPM.BLL.ConvertRule.ToString(ViewState["html_title1"])%>
												</tr>
												<tr class="list-title">
													<%=RmsPM.BLL.ConvertRule.ToString(ViewState["TargetHisHead2"])%>
													<%=RmsPM.BLL.ConvertRule.ToString(ViewState["html_title2"])%>
												</tr>
											</thead>
											<tbody>
												<asp:Repeater Runat="server" ID="dgList">
													<ItemTemplate>
														<tr style="display:none" id='TreeNode_<%# DataBinder.Eval(Container, "DataItem.CostCode") %>' key='<%# DataBinder.Eval(Container, "DataItem.CostCode") %>' Deep='<%# DataBinder.Eval(Container, "DataItem.Deep") %>' ParentCode='<%# DataBinder.Eval(Container, "DataItem.ParentCode") %>' ChildCount='<%# DataBinder.Eval(Container, "DataItem.ChildCount") %>' RowIndex='<%# Container.ItemIndex %>' DefaultExpand='<%# DataBinder.Eval(Container, "DataItem.IsExpand") %>' onclick="CBTree_SetRowSelected(this);" onmouseup="RowMouseClick(this);">
															<td nowrap title='<%# DataBinder.Eval(Container, "DataItem.SortID") %>'><span id='TreeNodeSpan_<%# DataBinder.Eval(Container, "DataItem.CostCode") %>'></span><span style='width:15px'><img id='TreeNodeImg_<%# DataBinder.Eval(Container, "DataItem.CostCode") %>' key='<%# DataBinder.Eval(Container, "DataItem.CostCode") %>' style="display:none;cursor:hand" onclick="CBTree_ImgExpandClick(this);"></span>
																<%# DataBinder.Eval(Container, "DataItem.CostName") %>
																<input type="hidden" runat="server" id="txtCostCode" name="txtCostCode" value='<%# DataBinder.Eval(Container, "DataItem.CostCode") %>'>
																<input type="hidden" runat="server" id="txtCostBudgetDtlCode" name="txtCostBudgetDtlCode" value='<%# DataBinder.Eval(Container, "DataItem.CostBudgetDtlCode") %>'>
																<input type="hidden" runat="server" id="txtIsExpand" name="txtIsExpand" value='<%# DataBinder.Eval(Container, "DataItem.IsExpand") %>'>
															</td>
															<td nowrap align="right"><%# RmsPM.BLL.CostBudgetPageRule.GetMoneyShowString(DataBinder.Eval(Container, "DataItem.BudgetMoney")) %></td>
															<%# DataBinder.Eval(Container, "DataItem.BudgetMoneyHisHtml") %>
															<td nowrap align="left"><%# DataBinder.Eval(Container, "DataItem.Description") %></td>
															<%# DataBinder.Eval(Container, "DataItem.PlanDataHtml") %>
														</tr>
													</ItemTemplate>
												</asp:Repeater>
											</tbody>
										</table>
									</div>
								</td>
							</tr>
						</table>
					</td>
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
			<input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server"><input id="txtFromUrl" type="hidden" name="txtFromUrl" runat="server">
			<INPUT id="txtCostBudgetCode" type="hidden" name="txtCostBudgetCode" runat="server">
			<INPUT id="txtCostBudgetSetCode" type="hidden" name="txtContractCode" runat="server">
			<INPUT id="txtFirstCostBudgetCode" type="hidden" name="txtFirstCostBudgetCode" runat="server">
			<INPUT id="txtStatus" type="hidden" name="txtStatus" runat="server"><INPUT id="txtShowTargetHis" type="hidden" name="txtShowTargetHis" runat="server">
		</form>
		<script language="javascript">
<!--

if (window.opener)
{
//	Form1.btnClose.style.display = "";
//	Form1.btnGoBack.style.display = "none";
}
	
//-->
		</script>
	</body>
</HTML>
