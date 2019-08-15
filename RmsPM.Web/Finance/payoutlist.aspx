<%@ Register TagPrefix="uc1" TagName="InputUser" Src="../UserControls/InputUser.ascx" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Register TagPrefix="uc1" TagName="InputSystemGroup" Src="../UserControls/InputSystemGroup.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="Tiannuo.Web.Controls" Assembly="DataGridWebControls" %>
<%@ Register TagPrefix="uc1" TagName="InputSubject" Src="../UserControls/InputSubject.ascx" %>

<%@ Page language="c#" Inherits="RmsPM.Web.Finance.PayoutList" CodeFile="PayoutList.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>�������</title>
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
								<td class="topic" background="../images/topic_bg.gif"><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle">������� 
									- <span id="spanTitle" runat="server">�������</span></td>
								<td width="9"><IMG height="25" src="../images/topic_corr.gif" width="9"></td>
							</tr>
						</table>
					</td>
				</tr>
				<TR>
					<td class="tools-area" vAlign="top"><IMG src="../images/btn_li.gif" align="absMiddle">
						<input class="button" id="btnBuildVoucher" onclick="BuildVoucher(true);" type="button"
							value="����ƾ֤" name="btnBuildVoucher" runat="server"> <input class="button" id="btnSelectVoucher" onclick="BuildVoucher(false);" type="button"
							value="����ƾ֤" name="btnSelectVoucher" runat="server"> <input class="button" id="btnAdvSearch" style="DISPLAY: none" onclick="ShowAdvSearch()"
							type="button" value="�߼���ѯ" name="btnAdvSearch"> <input class="button" id="btnBatchModify" onclick="batchModify();" type="button" value="�޸�"
							name="btnBatchModify" runat="server">
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
														<td>
															������ͣ� <input id="chkIsContract" type="checkbox" value="1" name="chkIsContract" runat="server">��ͬ��� 
															&nbsp;<input id="chkIsNotContract" type="checkbox" value="1" name="chkIsNotContract" runat="server">�Ǻ�ͬ��� 
															&nbsp;&nbsp;�������ͣ�<uc1:inputsystemgroup id="inputSystemGroup" runat="server" SelectAllLeaf="True"></uc1:inputsystemgroup>
															&nbsp;&nbsp;״̬�� &nbsp;<input id="chkStatus0" type="checkbox" value="1" name="chkStatus0" runat="server"><label for="chkStatus0">����</label>&nbsp; 
															&nbsp;<input id="chkStatus1" type="checkbox" value="1" name="chkStatus1" runat="server"><label for="chkStatus1">����</label>&nbsp; 
															&nbsp;&nbsp;<input class="submit" id="btnSearch" type="button" value="����" name="btnSearch" runat="server"
																onclick="document.all.divHintLoad.style.display = '';" onserverclick="btnSearch_ServerClick"> &nbsp;<IMG id="imgAdvSearch" title="�߼���ѯ" style="CURSOR: hand" onclick="ShowAdvSearch();" src="../images/search_more.gif">
														</td>
													</TR>
												</table>
												<table id="divAdvSearch" style="DISPLAY: none">
													<TR>
														<TD>����ţ�<INPUT class="input" id="txtPayoutID" style="WIDTH: 130px" type="text" size="12" name="txtPayoutID"
																runat="server">
															&nbsp;&nbsp;���ţ�<INPUT class="input" id="txtPaymentID" style="WIDTH: 130px" type="text" size="12" name="txtPaymentID"
																runat="server">
															&nbsp;&nbsp;ƾ֤�ţ�<INPUT class="input" id="txtVoucherID" style="WIDTH: 130px" type="text" size="12" name="txtVoucherID"
																runat="server">
														</TD>
													</TR>
													<tr>
														<TD>��ͬ��ţ�<INPUT class="input" id="txtContractID" style="WIDTH: 130px" type="text" size="12" name="txtContractID"
																runat="server">
															&nbsp;&nbsp;��ͬ���ƣ�<INPUT class="input" id="txtContractName" style="WIDTH: 130px" type="text" size="12" name="txtContractName"
																runat="server">
															&nbsp;&nbsp;<input id="chkBatchPayment" type="checkbox" value="1" name="chkBatchPayment" runat="server" /><label for="chkBatchPayment">�ɱ��������</label>
															&nbsp;&nbsp;<input id="chkNotBatchPayment" type="checkbox" value="1" name="chkNotBatchPayment" runat="server" checked /><label for="chkNotBatchPayment">�ǳɱ��������</label>
														</TD>
													</tr>
													<TR>
														<TD>�ܿλ��<INPUT class="input" id="txtSupplyName" type="text" size="30" name="txtSupplyName" runat="server"><A title="ѡ��Ӧ��" href="javascript:SelectSupplier()">
																<IMG src="../images/ToolsItemSearch.gif" border="0"></A>
															&nbsp;&nbsp;�ܿ��<INPUT class="input"  style=" TEXT-ALIGN: right" id="TxtGreateCash"  type="text" size="12" name="TxtGreateCash"
																runat="server">&nbsp;-&nbsp;<INPUT class="input"  style="TEXT-ALIGN: right" id="TxtSmallCash" type="text" size="12" name="TxtSmallCash"
																runat="server">
															&nbsp;&nbsp;�� �� �ˣ�<INPUT class="input" id="txtPayer" style="WIDTH: 130px" type="text" size="12" name="txtPayer"
																runat="server">
														</TD>
													</TR>
													<tr>
														<TD>�������ڣ�<cc3:calendar id="dtbPayoutDate0" runat="server" Value="" Display="True" ReadOnly="False" CalendarResource="../Images/CalendarResource/"></cc3:calendar>
														    &nbsp;��&nbsp;��<cc3:calendar id="dtbPayoutDate1" runat="server" Value="" Display="True" ReadOnly="False" CalendarResource="../Images/CalendarResource/"></cc3:calendar>
														</TD>
													</tr>
                                                    <tr>
                                                        <td>������Ŀ��<uc1:InputSubject id="ucInputSubjectStart" runat="server"></uc1:InputSubject>
                                                            &nbsp;��&nbsp;��<uc1:InputSubject id="ucInputSubjectEnd" runat="server"></uc1:InputSubject>
                                                        </td>
                                                    </tr>
                                                    <TR>
														<TD>�� �� �ˣ�<uc1:inputuser id="ucInputPerson" runat="server"></uc1:inputuser>
														    &nbsp;&nbsp;�������ڣ�<cc3:calendar id="dtbInputDate0" runat="server" Value="" Display="True" ReadOnly="False" CalendarResource="../Images/CalendarResource/"></cc3:calendar>
														    &nbsp;��&nbsp;��<cc3:calendar id="dtbInputDate1" runat="server" Value="" Display="True" ReadOnly="False" CalendarResource="../Images/CalendarResource/"></cc3:calendar>
														</TD>
													</TR>
													<TR>
														<TD>�� �� �ˣ�<uc1:inputuser id="ucCheckPerson" runat="server"></uc1:inputuser>
														    &nbsp;&nbsp;������ڣ�<cc3:calendar id="dtbCheckDate0" runat="server" Value="" Display="True" ReadOnly="False" CalendarResource="../Images/CalendarResource/"></cc3:calendar>
														    &nbsp;��&nbsp;��<cc3:calendar id="dtbCheckDate1" runat="server" Value="" Display="True" ReadOnly="False" CalendarResource="../Images/CalendarResource/"></cc3:calendar>
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
									<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%"><asp:datagrid id="dgList" runat="server" CssClass="list" CellPadding="0" AllowPaging="True" PageSize="15"
											AutoGenerateColumns="False" AllowSorting="True" Width="100%" ShowFooter="True">
											<AlternatingItemStyle CssClass=""></AlternatingItemStyle>
											<ItemStyle CssClass=""></ItemStyle>
											<HeaderStyle CssClass="list-title"></HeaderStyle>
											<FooterStyle CssClass="list-title"></FooterStyle>
											<Columns>
												<asp:TemplateColumn Visible="True" HeaderText="&lt;input type='checkbox' name='chkAll' onclick='ChkSelectAll(document.all.chkSelect, document.all.chkAll.checked);' title='ȫѡ��ȫ��ѡ'&gt;">
													<HeaderStyle HorizontalAlign="Center" Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<input type="checkbox" name="chkSelect" class="list-checkbox" onclick="ChkSelectRow('<%# Container.ItemIndex + 1%>', this, document.all.dgList, 'list-2', '');" value='<%#DataBinder.Eval(Container, "DataItem.PayoutCode")%>'>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" HeaderText="�����"
													SortExpression="PayoutID">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<a href="#" onclick="View(this.Code);return false;" Code='<%#  DataBinder.Eval(Container.DataItem, "PayoutCode") %>'>
															<%#  DataBinder.Eval(Container.DataItem, "PayoutID") %>
														</a>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="StatusName" HeaderText="״̬" SortExpression="Status">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" HeaderText="�������" FooterText="�ϼ�"
													SortExpression="PaymentCodes">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%#  DataBinder.Eval(Container.DataItem, "PaymentCodes") %>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="SupplyName" HeaderText="�ܿλ" SortExpression="SupplyName">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="True"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="Payer" HeaderText="�ܿ���" SortExpression="Payer">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="True"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="PayoutDate" HeaderText="��������" DataFormatString="{0:yyyy-MM-dd}" SortExpression="PayoutDate">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn HeaderText="������(Ԫ)" SortExpression="Money">
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
												<asp:TemplateColumn HeaderText="������Ŀ" SortExpression="SubjectCode">
													<HeaderStyle HorizontalAlign="Center" Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="True"></ItemStyle>
													<ItemTemplate>
														<%# RmsPM.BLL.SubjectRule.GetSubjectFullName(RmsPM.BLL.ConvertRule.ToString(DataBinder.Eval(Container, "DataItem.SubjectCode")), RmsPM.BLL.ConvertRule.ToString(DataBinder.Eval(Container, "DataItem.SubjectSetCode"))) %>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="CheckPersonName" HeaderText="�����" SortExpression="CheckPersonName">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="CheckDate" HeaderText="�������" DataFormatString="{0:yyyy-MM-dd}" SortExpression="CheckDate">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn HeaderText="ƾ֤��">
													<HeaderStyle HorizontalAlign="Center" Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<a href="#" onclick="javascript:GotoVoucher(this.val);" val='<%#DataBinder.Eval(Container, "DataItem.VoucherCode")%>'>
															<%# RmsPM.BLL.PaymentRule.GetVoucherName(DataBinder.Eval(Container, "DataItem.VoucherCode").ToString())%>
														</a>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle Visible="False" NextPageText="��ҳ&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;��ҳ" HorizontalAlign="Right"
												CssClass="ListHeadTr"></PagerStyle>
										</asp:datagrid>
									</div>
								</td>
							</tr>
							<tr>
								<td>
									<cc1:gridpagination id="GridPagination1" runat="server" DataGridId="dgList" ControlSourceUrl="../Images/GridPaginationSource/" onpageindexchange="GridPagination1_PageIndexChange"></cc1:gridpagination>
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
			<input id="txtSelect" type="hidden" name="txtSelect" runat="server"> <input id="txtAdvSearch" type="hidden" value="none" name="txtAdvSearch" runat="server">
			<input id="txtSumMoney" type="hidden" value="none" name="txtSumMoney" runat="server">
			<script language="javascript">
<!--
var CurrUrl = window.location.href;

//ѡ��Ӧ��
function SelectSupplier()
{
	OpenLargeWindow("../SelectBox/SelectSupplier.aspx", "ѡ��Ӧ��");
}

//ѡ��Ӧ�̷���
function DoSelectSupplierReturn(code, name)
{
//	Form1.txtSupplyCode.value = code;
	Form1.txtSupplyName.value = name;
}

//�鿴����
function View(PayoutCode)
{
	window.navigate('../Finance/PayoutInfo.aspx?PayoutCode='+ PayoutCode + "&FromUrl=" + escape(CurrUrl));
}

//����ƾ֤
function BuildVoucher(isNew)
{
	var s = ChkGetSelected(document.all.chkSelect);
		
	if (s == "")
	{
		alert('��ѡ��һ���������¼');
		return false;
	}
	
	Form1.txtSelect.value = s;

	if (isNew)
	{
		OpenCustomWindow("../Finance/VoucherModify.aspx?Act=PayAdd&ProjectCode=" + Form1.txtProjectCode.value + "&RelaCode=" + s,"ƾ֤�޸�", 780, 580);
	}
	else
	{
		OpenMiddleWindow("../Finance/SelectVoucher.aspx?Act=PayAdd&ProjectCode=" + Form1.txtProjectCode.value,"ѡ��ƾ֤");
	}

//		document.all.btnBatchHidden.click();
//		return true;
}

function batchModify()
{
	var s = ChkGetSelected(document.all.chkSelect);
		
	if (s == "")
	{
		alert('��ѡ��һ���������¼');
		return false;
	}
	
	Form1.txtSelect.value = s;

	OpenCustomWindow("../Finance/PayoutBatchModify.aspx?ProjectCode=" + Form1.txtProjectCode.value + "&SelectCode=" + s,"�����޸Ĺ�Ӧ��", 780, 580);
	//OpenMiddleWindow("../Finance/SelectVoucher.aspx?Act=PayAdd&ProjectCode=" + Form1.txtProjectCode.value,"ѡ��ƾ֤");
	

}

	//����ƾ֤
function SelectVoucherReturn(VoucherCode)
{
	var RelaCode = Form1.txtSelect.value;
	OpenCustomWindow("../Finance/VoucherModify.aspx?VoucherCode=" + VoucherCode + "&Act=PayAdd&ProjectCode=" + Form1.txtProjectCode.value + "&RelaCode=" + RelaCode,"ƾ֤�޸�", 780, 580);
}

//�鿴ƾ֤
function GotoVoucher(VoucherCode)
{
	OpenCustomWindow("../Finance/VoucherInfo.aspx?VoucherCode=" + VoucherCode + "&Open=1", "ƾ֤��Ϣ", 760, 540);
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
