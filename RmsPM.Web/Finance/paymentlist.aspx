<%@ Register TagPrefix="uc1" TagName="InputUser" Src="../UserControls/InputUser.ascx" %>
<%@ Register TagPrefix="uc2" TagName="InputUnit" Src="../UserControls/InputUnit.ascx" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Register TagPrefix="cc1" Namespace="Tiannuo.Web.Controls" Assembly="DataGridWebControls" %>
<%@ Page language="c#" Inherits="RmsPM.Web.Finance.PaymentList" CodeFile="PaymentList.aspx.cs" %>
<%@ Register TagPrefix="uc5" TagName="InputSystemGroup" Src="../UserControls/InputSystemGroup.ascx" %>
<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics.WebUI.WebDataInput.v5.1, Version=5.1.20051.37, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>������</title>
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
								<td class="topic" background="../images/topic_bg.gif"><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle">��Ŀ����>�������><span id="spanTitle" runat="server">������</span></td>
								<td width="9"><IMG height="25" src="../images/topic_corr.gif" width="9"></td>
							</tr>
						</table>
					</td>
				</tr>
				<TR>
					<td class="tools-area" vAlign="top">
						<IMG src="../images/btn_li.gif" align="absMiddle"> <input class="button" id="btnAdd" onclick="Add();" type="button" value="�Ǻ�ͬ���" name="btnAdd"
							runat="server"> <input class="button" id="btnAddCostBatch" onclick="AddCostBatch();" type="button" value="�������" name="btnAddCostBatch"
							runat="server"> <input class="button" id="btnPayout" style="DISPLAY:none" onclick="Payout();" type="button"
							value="�� ��" name="btnPayout" runat="server"> <input style="DISPLAY:none" class="button" id="btnAdvSearch" onclick="ShowAdvSearch()"
							type="button" value="�߼���ѯ" name="btnAdvSearch">
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
														<TD noWrap>���ͣ� <input id="chkIsContract" type="checkbox" value="1" name="chkIsContract" runat="server">��ͬ��� 
															&nbsp;<input id="chkIsNotContract" type="checkbox" value="1" name="chkIsNotContract" runat="server">�Ǻ�ͬ��� 
															&nbsp;
														</TD>
														<td>������</td>
														<TD>
															<uc5:InputSystemGroup id="inputSystemGroupPayment" runat="server" SelectAllLeaf="True"></uc5:InputSystemGroup>
														</TD>
														<TD id="tdSearchStatus" noWrap runat="server">״̬�� <input id="chkStatus0" type="checkbox" value="1" name="chkStatus0" runat="server"><label for="chkStatus0">����</label>&nbsp; 
															<input id="chkStatus1" type="checkbox" value="1" name="chkStatus1" runat="server"><label for="chkStatus1">����</label>&nbsp; 
															<input id="chkStatus2" type="checkbox" value="1" name="chkStatus2" runat="server"><label for="chkStatus2">����</label>&nbsp;
															<input id="chkStatus3" type="checkbox" value="1" name="chkStatus3" runat="server"><label for="chkStatus3">�����</label>&nbsp;
													    </TD>
														<TD vAlign="middle" align="right"><input class="submit" id="btnSearch" type="button" value="����" name="btnSearch" runat="server" onclick="document.all.divHintLoad.style.display = '';" onserverclick="btnSearch_ServerClick">
														</TD>
														<td><img src="../images/search_more.gif" title="�߼���ѯ" style="CURSOR:hand" id="imgAdvSearch"
																onclick="ShowAdvSearch();"></td>
													</TR>
												</table>
												<table style="DISPLAY:none" id="divAdvSearch">
													<tr>
														<TD>���ţ�<INPUT class="input" id="txtPaymentID" type="text" size="12" name="txtPaymentID" runat="server">
														    &nbsp;&nbsp;���ҵ��<input class="input" id="txtPaymentTitle" type="text" size="12" name="txtPaymentTitle"
																runat="server">
													    </TD>
													</tr>
													<tr>
														<TD>��ͬ��ţ�<INPUT class="input" id="txtContractID" type="text" size="12" name="txtContractID" runat="server">
														    &nbsp;&nbsp;��ͬ����/�Ǻ�ͬ������ƣ�<INPUT class="input" id="txtContractName" type="text" size="12" name="txtContractName"
																runat="server">
															&nbsp;&nbsp;<input id="chkBatchPayment" type="checkbox" value="1" name="chkBatchPayment" runat="server" /><label for="chkBatchPayment">�ɱ��������</label>
															&nbsp;&nbsp;<input id="chkNotBatchPayment" type="checkbox" value="1" name="chkNotBatchPayment" runat="server" checked /><label for="chkNotBatchPayment">�ǳɱ��������</label>
													    </TD>
													</tr>
													<TR>
														<TD>�ܿ��
															<igtxt:webnumericedit Width="100" id="txtTotalMoney0" runat="server" MinDecimalPlaces="Two" CssClass="infra-input-nember"
																ImageDirectory="../images/infragistics/images/" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js"
																JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js"></igtxt:webnumericedit>
															����<igtxt:webnumericedit Width="100" id="txtTotalMoney1" runat="server" MinDecimalPlaces="Two" CssClass="infra-input-nember"
																ImageDirectory="../images/infragistics/images/" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js"
																JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js"></igtxt:webnumericedit>
																
																&nbsp;&nbsp;�ܿλ��<INPUT class="input" id="txtSupplyName" type="text" size="30" name="txtSupplyName" runat="server"><a href="#" onclick="SelectSupplier();return false;" title="ѡ��Ӧ��">
																<img src="../images/ToolsItemSearch.gif" border="0"></a>
														    &nbsp;&nbsp;�� �� �ˣ�<INPUT class="input" id="txtPayer" type="text" size="12" name="txtPayer" runat="server">
														    
														</TD>
													</TR>
													<tr>
														<TD>���ţ�<uc2:InputUnit id="ucUnit" runat="server"></uc2:InputUnit>
														    &nbsp;&nbsp;��󸶿��գ�<cc3:calendar id="dtbPayDate0" runat="server" CalendarResource="../Images/CalendarResource/" ReadOnly="False"
																Display="True" Value=""></cc3:calendar>
																&nbsp;��&nbsp;��<cc3:calendar id="dtbPayDate1" runat="server" CalendarResource="../Images/CalendarResource/" ReadOnly="False"
																Display="True" Value=""></cc3:calendar>
														</TD>
													</tr>
													<TR>
														<TD>�� �� �ˣ�<uc1:InputUser id="ucApplyPerson" runat="server"></uc1:InputUser>
														    &nbsp;&nbsp;�������ڣ�<cc3:calendar id="dtbApplyDate0" runat="server" CalendarResource="../Images/CalendarResource/"
																ReadOnly="False" Display="True" Value=""></cc3:calendar>
														    &nbsp;��&nbsp;��<cc3:calendar id="dtbApplyDate1" runat="server" CalendarResource="../Images/CalendarResource/"
																ReadOnly="False" Display="True" Value=""></cc3:calendar>
														</TD>
													</TR>
													<TR>
														<TD>�� �� �ˣ�<uc1:InputUser id="ucCheckPerson" runat="server"></uc1:InputUser>
														    &nbsp;&nbsp;������ڣ�<cc3:calendar id="dtbCheckDate0" runat="server" CalendarResource="../Images/CalendarResource/"
																ReadOnly="False" Display="True" Value=""></cc3:calendar>
															&nbsp;��&nbsp;��<cc3:calendar id="dtbCheckDate1" runat="server" CalendarResource="../Images/CalendarResource/"
																ReadOnly="False" Display="True" Value=""></cc3:calendar>
														</TD>
													</TR>
													<tr>
													    <td>ÿҳ��ʾ��<INPUT class="input" id="txtPageSize" type="text" size="4" name="txtPageSize" runat="server">����¼</td>
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
										<asp:datagrid id="dgList" runat="server" Width="100%" AllowSorting="True" AutoGenerateColumns="False"
											PageSize="15" AllowPaging="True" CellPadding="0" CssClass="list" ShowFooter="True">
											<AlternatingItemStyle CssClass=""></AlternatingItemStyle>
											<ItemStyle CssClass=""></ItemStyle>
											<HeaderStyle CssClass="list-title"></HeaderStyle>
											<FooterStyle CssClass="list-title"></FooterStyle>
											<Columns>
												<asp:TemplateColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" Visible="False" HeaderText="&lt;input type='checkbox' name='chkAll' onclick='ChkSelectAll(document.all.chkSelect, document.all.chkAll.checked);' title='ȫѡ��ȫ��ѡ'&gt;">
													<HeaderStyle HorizontalAlign="Center" Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<input id="chkSelect" type="checkbox" name="chkSelect" class="list-checkbox" onclick="ChkSelectRow('<%# Container.ItemIndex + 1%>', this, document.all.dgList, 'list-2', '');" value='<%#DataBinder.Eval(Container, "DataItem.PaymentCode")%>'/>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" HeaderText="����" FooterText="�ϼ�" SortExpression="PaymentID">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<a href="#" onclick="View(this.PaymentCode);return false;" PaymentCode='<%#  DataBinder.Eval(Container.DataItem, "PaymentCode") %>'><asp:Label runat="server" ID="lblPaymentID" Text='<%#  DataBinder.Eval(Container.DataItem, "PaymentID") %>'></asp:Label> </a>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" DataField="StatusName" HeaderText="״̬" SortExpression="Status">
													<HeaderStyle HorizontalAlign="Left" Wrap="False"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left" Wrap="False"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" DataField="PaymentNameEx" HeaderText="��ͬ����/�Ǻ�ͬ�������" SortExpression="PaymentNameEx">
													<HeaderStyle HorizontalAlign="Left" Wrap="False"></HeaderStyle>
												</asp:BoundColumn>
												
												<asp:BoundColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" DataField="UnitName" HeaderText="����" SortExpression="UnitName">
													<HeaderStyle HorizontalAlign="Left" Wrap="False"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" DataField="ApplyPersonName" HeaderText="�����" SortExpression="ApplyPersonName">
													<HeaderStyle HorizontalAlign="Left" Wrap="False"></HeaderStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" HeaderText="�ܿλ" SortExpression="SupplyName">
													<ItemTemplate>
														<%#  RmsPM.BLL.StringRule.TruncateString( DataBinder.Eval(Container.DataItem, "SupplyName"),8) %>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" DataField="Payer" HeaderText="�ܿ���" SortExpression="Payer">
													<HeaderStyle HorizontalAlign="Left" Wrap="False"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" DataField="PayDate" HeaderText="��󸶿���"
													DataFormatString="{0:yyyy-MM-dd}" SortExpression="PayDate">
													<HeaderStyle HorizontalAlign="Left" Wrap="False"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left" Wrap="False"></ItemStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" HeaderText="�����(Ԫ)" SortExpression="Money">
													<HeaderStyle HorizontalAlign="Right" Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<%# DataBinder.Eval(Container, "DataItem.Money", "{0:N}") %>
													</ItemTemplate>
													<FooterTemplate>
														<asp:Label runat="server" ID="lblSumMoney"></asp:Label>
													</FooterTemplate>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" HeaderText="�Ѹ�(δ��/����)" SortExpression="TotalPayout">
													<HeaderStyle HorizontalAlign="Right" Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<%# DataBinder.Eval(Container, "DataItem.TotalPayout", "{0:N}") %>
													</ItemTemplate>
													<FooterTemplate>
														<asp:Label runat="server" ID="lblSumTotalPayoutMoney"></asp:Label>
													</FooterTemplate>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
												</asp:TemplateColumn>
												
												<asp:TemplateColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" HeaderText="����" SortExpression="IsContract">
													<HeaderStyle HorizontalAlign="Left" Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Left"></ItemStyle>
													<ItemTemplate>
														<%# RmsPM.BLL.PaymentRule.GetPaymentIsContractName(RmsPM.BLL.ConvertRule.ToInt(DataBinder.Eval(Container.DataItem, "IsContract")))%>
													</ItemTemplate>
												</asp:TemplateColumn>
												
												<asp:BoundColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" DataField="ApplyDate" HeaderText="��������"
													Visible="False" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn>
													
											</Columns>
											<PagerStyle Visible="False" NextPageText="��ҳ&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;��ҳ" HorizontalAlign="Right"
												CssClass="ListHeadTr"></PagerStyle>
										</asp:datagrid>
									</div>
								</td>
							</tr>
							<tr>
								<td>
									<cc1:GridPagination id="GridPagination1" runat="server" ControlSourceUrl="../Images/GridPaginationSource/" DataGridId="dgList" onpageindexchange="GridPagination1_PageIndexChange" IsPrintList="true"></cc1:GridPagination>
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
			<input id="txtAdvSearch" type="hidden" value="none" name="txtAdvSearch" runat="server">
			<input id="txtSumMoney" type="hidden" value="none" name="txtSumMoney" runat="server"><input id="txtSumTotalPayoutMoney" type="hidden" value="none" name="txtSumTotalPayoutMoney"
				runat="server">
			<script language="javascript">
<!--
var CurrUrl = window.location.href;

//ѡ��Ӧ��
function SelectSupplier()
{
	OpenLargeWindow("../SelectBox/SelectSupplier.aspx?supplierName=" + escape(Form1.txtSupplyName.value) , "ѡ��Ӧ��");
}

//ѡ��Ӧ�̷���
function DoSelectSupplierReturn(code, name)
{
//	Form1.txtSupplyCode.value = code;
	Form1.txtSupplyName.value = name;
}

//�Ǻ�ͬ���
function Add()
{
	OpenCustomWindow("../Finance/PaymentDetailModify.aspx?ProjectCode=" + Form1.txtProjectCode.value, "���޸�", 780, 540);
}

//�ɱ��������
function AddCostBatch()
{
	OpenCustomWindow("../Finance/CostBatchPaymentModify.aspx?ProjectCode=" + Form1.txtProjectCode.value, "���޸�", 780, 540);
}

//�鿴���
function View(PaymentCode)
{
	OpenFullWindow('../Finance/PaymentInfo.aspx?PaymentCode='+ PaymentCode + "&FromUrl=" + escape(CurrUrl),'����Ϣ');
}

//����
function Payout()
{
	var s = ChkGetSelected(document.all.chkSelect);

	if (s == "")
	{
		alert('��ѡ��һ���������¼');
		return false;
	}

	Form1.txtSelect.value = s;
	OpenCustomWindow("../Finance/PayoutDetailModify.aspx?PaymentCode=" + s + "&ProjectCode=" + Form1.txtProjectCode.value, "����޸�", 780, 560);
}

function DoViewVoucherInfo(PaymentCode)
{
	window.navigate('../Finance/VoucherInfo.aspx?PaymentCode='+ PaymentCode );
}

//�߼���ѯ
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
		Form1.imgAdvSearch.title = "�߼���ѯ";
	}
	else
	{
//		Form1.imgAdvSearch.src = "../images/ArrowUp.gif";
		Form1.imgAdvSearch.title = "���ظ߼���ѯ";
	}
}

//�����������س�
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
