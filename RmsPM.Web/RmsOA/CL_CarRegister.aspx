<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Register TagPrefix="cc1" Namespace="Tiannuo.Web.Controls" Assembly="DataGridWebControls" %>
<%@ Page language="c#" Inherits="RmsPM.Web.Contract.CL_CarRegister" CodeFile="CL_CarRegister.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="InputSystemGroup" Src="../UserControls/InputSystemGroup.ascx" %>
<%@ Register TagPrefix="uc1" TagName="InputCostBudgetDtl" Src="../UserControls/InputCostBudgetDtl.ascx" %>
<%@ Register TagPrefix="uc2" TagName="InputUnit" Src="../UserControls/InputUnit.ascx" %>
<%@ Register TagPrefix="uc1" TagName="InputUser" Src="../UserControls/InputUser.ascx" %>
<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics.WebUI.WebDataInput.v5.1, Version=5.1.20051.37, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>�����б�</title>
		<META http-equiv="Content-Type" content="text/html; charset=gb2312">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<LINK href="../Images/infra.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
	</HEAD>
	<body style="BORDER-RIGHT: 0px" scroll="no">
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
									�ҵĹ���>��������>�����б�</td>
								<td width="9"><IMG height="25" src="../images/topic_corr.gif" width="9"></td>
							</tr>
						</table>
					</td>
				</tr>
				<TR>
					<td class="tools-area" vAlign="top">
						<IMG src="../images/btn_li.gif" align="absMiddle"> <input class="button" id="btnNew" onClick="doNewCar('');return false;" type="button"
							value="�����Ǽ�" name="btnNew" runat="server">
					</td>
				</TR>
				<tr height="100%">
					<td class="table" vAlign="top">
						<table width="100%" height="100%">
							<tr>
								<td>
									<TABLE class="search-area" cellSpacing="0" cellPadding="0" width="100%" border="0" onKeyDown="SearchKeyDown();">
										<tr>
											<td>
												<table>
													<TR>
														<td>���ƣ�</td>
														<td><input class="input" id="txtContractName" type="text" name="txtContractName" runat="server"></td>
														<td>״̬��</td>
														<td colspan="3"><asp:checkboxlist id="cblStatus" runat="server" RepeatDirection="Horizontal">
																<asp:ListItem Value="0">����</asp:ListItem>
																<asp:ListItem Value="1">����</asp:ListItem>
																<asp:ListItem Value="7">�����</asp:ListItem>
																<asp:ListItem Value="2">�ѽ�</asp:ListItem>
															</asp:checkboxlist></td>
														<td><INPUT class="submit" id="btnSearch" type="button" value="�� ��" name="btnSearch" runat="server"
																onclick="document.all.divHintLoad.style.display = '';" onserverclick="btnSearch_ServerClick"> &nbsp;<img src="../images/search_more.gif" title="�߼���ѯ" style="CURSOR:hand" id="imgAdvSearch"
																onclick="ShowAdvSearch();">
														</td>
													</TR>
													<tr>
														<td>��ţ�</td>
														<td><input class="input" id="txtContractID" type="text" name="txtContractID" runat="server"></td>													
														<td>���״̬��</td>
														<td colspan="3"><asp:checkboxlist id="cblChangeStatus" runat="server" RepeatDirection="Horizontal">
																<asp:ListItem Value="0">�ޱ��</asp:ListItem>
																<asp:ListItem Value="1">�������</asp:ListItem>
																<asp:ListItem Value="2">�ѱ��</asp:ListItem>
																<asp:ListItem Value="3">��������</asp:ListItem>
															</asp:checkboxlist></td>
													</tr>
													<tr>
														<td>��Ӧ�̣�</td>
														<td><input class="input" id="txtSupplierName" type="text" name="txtSupplierName" runat="server">
															<input id="txtSupplierCode" type="hidden" name="txtSupplierCode" runat="server">
															<A onClick="openSelectSupplier();" href="##"><IMG src="../images/ToolsItemSearch.gif" border="0"></A>
														</td>
														<TD>��ͬ���ͣ�</TD>
														<TD colspan="3">
															<uc1:InputSystemGroup id="inputSystemGroup" runat="server" SelectAllLeaf="True"></uc1:InputSystemGroup>
														</TD>
													</tr>
												</table>
												<table style="DISPLAY:none" id="divAdvSearch">
													<tr>
														<TD>�� �ţ�<uc2:InputUnit id="ucUnit" runat="server"></uc2:InputUnit>
															&nbsp;&nbsp;��Ч���ڣ�<cc3:calendar id="dtContractDate0" runat="server" CalendarResource="../Images/CalendarResource/"
																ReadOnly="False" Display="True" Value=""></cc3:calendar>
															����<cc3:calendar id="dtContractDate1" runat="server" CalendarResource="../Images/CalendarResource/"
																ReadOnly="False" Display="True" Value=""></cc3:calendar>
														</TD>
													</tr>
													<TR>
														<TD>�����ˣ�<uc1:InputUser id="ucContractPerson" runat="server"></uc1:InputUser>
															&nbsp;&nbsp;��ͬ��
															<igtxt:webnumericedit Width="100" id="txtTotalMoney0" runat="server" MinDecimalPlaces="Two" CssClass="infra-input-nember"
																ImageDirectory="../images/infragistics/images/" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js"
																JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js"></igtxt:webnumericedit>
															����<igtxt:webnumericedit Width="100" id="txtTotalMoney1" runat="server" MinDecimalPlaces="Two" CssClass="infra-input-nember"
																ImageDirectory="../images/infragistics/images/" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js"
																JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js"></igtxt:webnumericedit>
														</TD>
													</TR>
													<tr>
														<td>
															Ԥ���
															<select class="select" id="sltCostBudgetSet" name="sltCostBudgetSet" runat="server">
																<option value="" selected>--��ѡ��--</option>
															</select>
															&nbsp;&nbsp;&nbsp;&nbsp;�����<uc1:InputCostBudgetDtl id="ucCostBudgetDtl" runat="server"></uc1:InputCostBudgetDtl>
														</td>
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
										<asp:datagrid id="dgList" runat="server" AutoGenerateColumns="False" PageSize="14" AllowPaging="True"
											GridLines="Horizontal" CellPadding="0" Width="100%" CssClass="list" AllowSorting="true" ShowFooter="True">
											<AlternatingItemStyle CssClass="list-i"></AlternatingItemStyle>
											<HeaderStyle CssClass="list-title"></HeaderStyle>
											<FooterStyle CssClass="list-title"></FooterStyle>
											<Columns>
												<asp:TemplateColumn SortExpression="Car_Id" ItemStyle-Width="160px" HeaderText="����" FooterText="�ϼ�">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<a href="##" onClick="doViewCarInfo(this.code);return false;" code='<%#  DataBinder.Eval(Container.DataItem, "Car_Id") %>'
													title='<%# DataBinder.Eval(Container.DataItem, "Car_Id")%>' >
															<%# DataBinder.Eval(Container.DataItem, "Car_Id")%>
														</a>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="ContractId" ItemStyle-Width="150px" HeaderText="����">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
															<a href="##" onClick="doViewCarInfo(this.code);return false;" code='<%#  DataBinder.Eval(Container.DataItem, "Car_Type") %>'
													title='<%# DataBinder.Eval(Container.DataItem, "Car_Type")%>' >
														<%# DataBinder.Eval(Container.DataItem, "Car_Type")%>
														</a>
													</ItemTemplate>
												</asp:TemplateColumn>
												
												<asp:TemplateColumn SortExpression="Buy_Date" HeaderText="��������">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%# RmsPM.BLL.ContractRule.GetContractStatusName(DataBinder.Eval(Container.DataItem, "Buy_Date").ToString())%>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="Chejia_Id" HeaderText="���ܺ�">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%# RmsPM.BLL.ContractRule.GetChangeStatusNameInContract(DataBinder.Eval(Container.DataItem, "Chejia_Id").ToString())%>
													</ItemTemplate>
												</asp:TemplateColumn>												
												<asp:TemplateColumn SortExpression="Fadongji_Id"  ItemStyle-Width="300px" HeaderText="��������">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														
															<%#  RmsPM.BLL.ProjectRule.GetSupplierName(DataBinder.Eval(Container, "DataItem.Fadongji_Id").ToString())%>
														
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
									<cc1:GridPagination id="GridPagination1" runat="server" ControlSourceUrl="../Images/GridPaginationSource/" DataGridId="dgList" onpageindexchange="GridPagination1_PageIndexChange"></cc1:GridPagination>
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
			
		function DoSelectSupplierReturn ( code,name)
		{
			Form1.txtSupplierCode.value = code;
			Form1.txtSupplierName.value = name;
		}

		function openSelectSupplier()
		{
			OpenMiddleWindow( '../SelectBox/SelectSupplier.aspx?ProjectCode=<%=Request["ProjectCode"]%>','ѡ��Ӧ��' );
		}

		function viewSupplier( supplierCode )
		{
			OpenFullWindow( '../Supplier/SupplierInfo.aspx?SupplierCode=' + supplierCode , '��Ӧ����Ϣ' );
		}
		function doNewCar()
		{
			OpenFullWindow('CarModify.aspx&act=Add','�����Ǽ�');
		}
		
	
		function doViewCarInfo( code )
		{
			OpenFullWindow('ContractInfo.aspx?ProjectCode=<%=Request["ProjectCode"]%>&ContractCode=' + code,'��ͬ��Ϣ');
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

		</script>
	</body>
</HTML>
