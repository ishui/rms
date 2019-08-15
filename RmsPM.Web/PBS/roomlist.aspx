<%@ Reference Control="~/pbs/searchroomall.ascx" %>
<%@ Reference Control="~/pbs/searchroom.ascx" %>
<%@ Register TagPrefix="uc2" TagName="SearchRoomAll" Src="SearchRoomAll.ascx" %>
<%@ Register TagPrefix="uc1" TagName="SearchRoom" Src="SearchRoom.ascx" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Page language="c#" Inherits="RmsPM.Web.PBS.RoomList" CodeFile="RoomList.aspx.cs" %>
<%@ Register TagPrefix="cc1" Namespace="Tiannuo.Web.Controls" Assembly="DataGridWebControls" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>PBSUnitList</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<LINK href="../Images/infra.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<SCRIPT language="javascript" src="../images/checkbox.js"></SCRIPT>
		<SCRIPT language="javascript" src="map.js"></SCRIPT>
	</HEAD>
	<body scroll="no">
		<form id="Form1" method="post" runat="server">
			<table id="tbMain" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td bgColor="#e4eff6" height="6"></td>
				</tr>
				<tr>
					<td height="25">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="topic" background="../images/topic_bg.gif"><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle">��Ʒ���� 
									- ��Դ��ѯ</td>
								<td width="9"><IMG height="25" src="../images/topic_corr.gif" width="9"></td>
							</tr>
						</table>
					</td>
				</tr>
				<TR id="trToolBar" runat="server">
					<TD class="tools-area" vAlign="top"><IMG src="../images/btn_li.gif" align="absMiddle">
						<input class="button" id="btnModifyArea" onclick="ModifyArea()" type="button" value="�޸����"
							name="btnModifyArea" runat="server"> <input class="button" id="btnPrint" onclick="Print()" type="button" value="�� ӡ" name="Print"
							runat="server"> <input class="button" id="btnPrintAll" onclick="document.all.divHintLoad.style.display = 'block';"
							type="button" value="��ӡȫ��" name="btnPrintAll" runat="server" style="DISPLAY: none" onserverclick="btnPrintAll_ServerClick"> <input class="button" id="btnAllowPaging" style="DISPLAY: none" onclick="document.all.divHintLoad.style.display = 'block';"
							type="button" value="ȡ����ҳ" name="btnAllowPaging" runat="server" onserverclick="btnAllowPaging_ServerClick">
					</TD>
				</TR>
				<tr height="100%">
					<TD class="table" vAlign="top">
						<table height="100%" cellSpacing="0" cellPadding="0" width="100%">
							<tr>
								<td valign="top">
									<TABLE class="search-area" cellSpacing="0" cellPadding="0" width="100%" border="0">
										<tr>
											<td>
												<table>
													<tr>
														<td width="100%"><uc1:searchroom id="tbSearchRoom" runat="server"></uc1:searchroom><uc2:searchroomall id="tbSearchRoomAll" runat="server"></uc2:searchroomall></td>
														<td vAlign="top" width="10"><input class="submit" id="btnSearch" onclick="document.all.divHintLoad.style.display = 'block';"
																type="button" value="�� ��" name="btnSearch" runat="server" onserverclick="btnSearch_ServerClick">
															&nbsp;<IMG id="imgAdvSearch" title="�߼���ѯ" style="CURSOR: hand" onclick="ShowAdvSearch();" src="../images/search_more.gif">
														</td>
													</tr>
												</table>
											</td>
										</tr>
									</table>
								</td>
							</tr>
							<tr height="100%">
								<td vAlign="top">
									<div style="OVERFLOW: auto; WIDTH: 100%; POSITION: absolute; HEIGHT: 100%">
										<table cellSpacing="0" cellPadding="0" width="100%" border="0">
											<tr>
												<td id="tdList"><asp:datagrid id="dgList" runat="server" Width="100%" CssClass="list" CellPadding="0" AllowSorting="True"
														AutoGenerateColumns="False" PageSize="100" ShowFooter="True" DataKeyField="RoomCode" PagerStyle-Visible="False">
														<ItemStyle CssClass=""></ItemStyle>
														<HeaderStyle CssClass="list-title"></HeaderStyle>
														<FooterStyle CssClass="list-title"></FooterStyle>
														<Columns>
															<asp:TemplateColumn Visible="False" HeaderText="&lt;input type='checkbox' name='chkAll' onclick='ChkSelectAll(document.all.chkSelect, document.all.chkAll.checked);' title='ȫѡ��ȫ��ѡ'&gt;">
																<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
																<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
																<ItemTemplate>
																	<input type="checkbox" name="chkSelect" class="list-checkbox" onclick="ChkSelectRow('<%# Container.ItemIndex + 1%>', this, document.all.dgList, 'list-2', 'list-i');" value='<%#DataBinder.Eval(Container, "DataItem.RoomCode")%>'>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn SortExpression="JgDate" HeaderText="�������" Visible="False">
																<ItemTemplate>
																	<%# DataBinder.Eval(Container, "DataItem.JgDate", "{0:yyyy}") %>
																</ItemTemplate>
																<HeaderStyle HorizontalAlign="Left" Wrap="False"></HeaderStyle>
																<ItemStyle HorizontalAlign="Left" Wrap="False"></ItemStyle>
															</asp:TemplateColumn>
															<asp:BoundColumn DataField="ProjectName" HeaderText="��Ŀ" Visible="False" SortExpression="ProjectName">
																<HeaderStyle HorizontalAlign="Left" Wrap="False"></HeaderStyle>
																<ItemStyle HorizontalAlign="Left" Wrap="False"></ItemStyle>
															</asp:BoundColumn>
															<asp:TemplateColumn SortExpression="BuildingCode" HeaderText="¥��" FooterText="�ϼ�">
																<ItemTemplate>
																	<a href="#" onclick="OpenBuildingInfo(this.BuildingCode);return false;" BuildingCode='<%# DataBinder.Eval(Container, "DataItem.BuildingCode") %>'><%# DataBinder.Eval(Container, "DataItem.BuildingName") %></a>
																</ItemTemplate>
																<HeaderStyle Wrap="False"></HeaderStyle>
																<ItemStyle Wrap="False"></ItemStyle>
																<FooterStyle Wrap="False"></FooterStyle>
															</asp:TemplateColumn>
															<asp:TemplateColumn SortExpression="IFloorCount" HeaderText="��<br>��">
																<ItemTemplate>
																	<%# DataBinder.Eval(Container, "DataItem.IFloorCount") %>
																</ItemTemplate>
																<HeaderStyle HorizontalAlign="Right" Wrap="False"></HeaderStyle>
																<ItemStyle HorizontalAlign="Right" Wrap="False"></ItemStyle>
															</asp:TemplateColumn>
															<asp:BoundColumn DataField="ChamberName" SortExpression="ChamberName" HeaderText="���ƺ�">
																<HeaderStyle HorizontalAlign="Left" Wrap="False"></HeaderStyle>
																<ItemStyle HorizontalAlign="Left" Wrap="False"></ItemStyle>
																<FooterStyle Wrap="False"></FooterStyle>
															</asp:BoundColumn>
															<asp:TemplateColumn SortExpression="RoomName" HeaderText="�Һ�">
																<ItemTemplate>
																	<a style="cursor:hand" onclick="javascript:OpenRoomInfo(this.code);" code='<%#  DataBinder.Eval(Container.DataItem, "RoomCode") %>'>
																		<%# DataBinder.Eval(Container, "DataItem.RoomName") %>
																	</a>
																</ItemTemplate>
																<FooterTemplate>
																	<asp:Label runat="server" ID="lblSumCount"></asp:Label>
																</FooterTemplate>
																<HeaderStyle HorizontalAlign="Left" Wrap="False"></HeaderStyle>
																<ItemStyle HorizontalAlign="Left" Wrap="False"></ItemStyle>
																<FooterStyle Wrap="False"></FooterStyle>
															</asp:TemplateColumn>
															<asp:TemplateColumn SortExpression="BuildArea" HeaderText="�������<br>(ƽ��)">
																<ItemTemplate>
																	<%# RmsPM.BLL.MathRule.GetDecimalShowString(DataBinder.Eval(Container, "DataItem.BuildArea")) %>
																</ItemTemplate>
																<FooterTemplate>
																	<asp:Label runat="server" ID="lblSumBuildArea"></asp:Label>
																</FooterTemplate>
																<HeaderStyle HorizontalAlign="Right" Wrap="False"></HeaderStyle>
																<ItemStyle HorizontalAlign="Right" Wrap="False"></ItemStyle>
																<FooterStyle HorizontalAlign="Right" Wrap="False"></FooterStyle>
															</asp:TemplateColumn>
															<asp:BoundColumn DataField="InvState" SortExpression="InvState" HeaderText="���<br>״̬">
																<HeaderStyle HorizontalAlign="Left" Wrap="False"></HeaderStyle>
																<ItemStyle HorizontalAlign="Left" Wrap="False"></ItemStyle>
															</asp:BoundColumn>
															<asp:BoundColumn DataField="InDate" SortExpression="InDate" HeaderText="�������" DataFormatString="{0:yyyy-MM-dd}">
																<HeaderStyle HorizontalAlign="Left" Wrap="False"></HeaderStyle>
																<ItemStyle HorizontalAlign="Left" Wrap="False"></ItemStyle>
															</asp:BoundColumn>
															<asp:BoundColumn DataField="OutDate" SortExpression="OutDate" HeaderText="��������" DataFormatString="{0:yyyy-MM-dd}">
																<HeaderStyle HorizontalAlign="Left" Wrap="False"></HeaderStyle>
																<ItemStyle HorizontalAlign="Left" Wrap="False"></ItemStyle>
															</asp:BoundColumn>
															<asp:BoundColumn DataField="OutState" SortExpression="OutState" HeaderText="����<br>״̬">
																<HeaderStyle HorizontalAlign="Left" Wrap="False"></HeaderStyle>
																<ItemStyle HorizontalAlign="Left" Wrap="False"></ItemStyle>
															</asp:BoundColumn>
															<asp:BoundColumn DataField="BofangName" SortExpression="BofangName" HeaderText="��������">
																<HeaderStyle HorizontalAlign="Left" Wrap="False"></HeaderStyle>
																<ItemStyle HorizontalAlign="Left" Wrap="False"></ItemStyle>
															</asp:BoundColumn>
															<asp:BoundColumn DataField="OutAspect" SortExpression="OutAspect" HeaderText="ȥ��">
																<HeaderStyle HorizontalAlign="Left" Wrap="False"></HeaderStyle>
																<ItemStyle HorizontalAlign="Left" Wrap="False"></ItemStyle>
															</asp:BoundColumn>
															<asp:BoundColumn DataField="SalState" SortExpression="SalState" HeaderText="����<br>״̬">
																<HeaderStyle HorizontalAlign="Left" Wrap="False"></HeaderStyle>
																<ItemStyle HorizontalAlign="Left" Wrap="False"></ItemStyle>
															</asp:BoundColumn>
															<asp:TemplateColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" HeaderText="����">
																<HeaderStyle Wrap="False"></HeaderStyle>
																<ItemStyle Wrap="False"></ItemStyle>
																<ItemTemplate>
																	<a href="#" onclick="ViewModel(this.Code);return false;" Code='<%#  DataBinder.Eval(Container.DataItem, "ModelCode") %>'><%#  DataBinder.Eval(Container.DataItem, "ModelName") %></a>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="��Ʒ����" SortExpression="PBSTypeFullName">
																<ItemTemplate>
																	<%# DataBinder.Eval(Container, "DataItem.PBSTypeFullName") %>
																</ItemTemplate>
																<HeaderStyle HorizontalAlign="Left" Wrap="False"></HeaderStyle>
																<ItemStyle HorizontalAlign="Left" Wrap="False"></ItemStyle>
															</asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="Ͷ������" SortExpression="InvestType">
																<ItemTemplate>
																	<%# DataBinder.Eval(Container, "DataItem.InvestType") %>
																</ItemTemplate>
																<HeaderStyle HorizontalAlign="Left" Wrap="False"></HeaderStyle>
																<ItemStyle HorizontalAlign="Left" Wrap="False"></ItemStyle>
															</asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="ʹ������" SortExpression="UseType">
																<ItemTemplate>
																	<%# DataBinder.Eval(Container, "DataItem.UseType") %>
																</ItemTemplate>
																<HeaderStyle HorizontalAlign="Left" Wrap="False"></HeaderStyle>
																<ItemStyle HorizontalAlign="Left" Wrap="False"></ItemStyle>
															</asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="�ɱ�<br>(Ԫ)" SortExpression="Cost">
																<ItemTemplate>
																	<%# RmsPM.BLL.MathRule.GetDecimalShowString(DataBinder.Eval(Container, "DataItem.Cost")) %>
																</ItemTemplate>
																<FooterTemplate>
																	<asp:Label runat="server" ID="lblSumCost"></asp:Label>
																</FooterTemplate>
																<HeaderStyle HorizontalAlign="Right" Wrap="False"></HeaderStyle>
																<ItemStyle HorizontalAlign="Right" Wrap="False"></ItemStyle>
																<FooterStyle HorizontalAlign="Right" Wrap="False"></FooterStyle>
															</asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="��������<br>(Ԫ)" SortExpression="TotalPayMoney">
																<ItemTemplate>
																	<a style="cursor:hand" onclick="javascript:ViewSalContract(this.code);" code='<%#  DataBinder.Eval(Container.DataItem, "ContractCode") %>'>
																		<%# RmsPM.BLL.MathRule.GetDecimalShowString(DataBinder.Eval(Container, "DataItem.TotalPayMoney")) %>
																	</a>
																</ItemTemplate>
																<FooterTemplate>
																	<asp:Label runat="server" ID="lblSumTotalPayMoney"></asp:Label>
																</FooterTemplate>
																<HeaderStyle HorizontalAlign="Right" Wrap="False"></HeaderStyle>
																<ItemStyle HorizontalAlign="Right" Wrap="False"></ItemStyle>
																<FooterStyle HorizontalAlign="Right" Wrap="False"></FooterStyle>
															</asp:TemplateColumn>
														</Columns>
														<PagerStyle NextPageText="��ҳ&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;��ҳ" HorizontalAlign="Center"
															CssClass="ListHeadTr"></PagerStyle>
													</asp:datagrid></td>
											</tr>
										</table>
									</div>
								</td>
							</tr>
							<tr>
								<td><cc1:gridpagination id="GridPagination1" runat="server" ControlSourceUrl="../Images/GridPaginationSource/" DataGridId="dgList" onpageindexchange="GridPagination1_PageIndexChange"></cc1:gridpagination></td>
							</tr>
						</table>
					</TD>
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
			<input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server"> <input id="txtSelectRoomCode" type="hidden" name="txtSelectRoomCode" runat="server">
			<input id="txtAdvSearch" type="hidden" value="" name="txtAdvSearch" runat="server">
			<input id="txtIsLoadPrint" type="hidden" name="txtIsLoadPrint" runat="server">
		</form>
		<SCRIPT language="javascript">
<!--

function DoRefresh()
{
	Form1.btnSearch.click();
}

//�޸����
function ModifyArea()
{
/*
	var s = ChkGetSelected(document.all.chkSelect);

	if (s == "")
	{
		alert('��ѡ��һ���������¼');
		return false;
	}

	
	Form1.txtSelectRoomCode.value = s;
*/

	if (Form1.txtSelectRoomCode.value == "")
	{
		alert('���Ȳ�ѯ');
		return false;
	}
	
	OpenCustomWindow("RoomModifyArea.aspx?action=parent&ReturnScript=DoRefresh();", "�޸����" , 760, 540);
}

//�鿴����
function OpenRoomInfo(code)
{
	OpenCustomWindow("RoomInfo.aspx?RoomCode="+code, "������Ϣ" , 760, 540);
}

//�鿴����
function ViewModel(code)
{
	OpenCustomWindow("RoomModel.aspx?ModelCode=" + code + "&act=view", "������Ϣ", 760, 540);
}

//�鿴��������
function ViewSalContract(ContractCode)
{
	OpenCustomWindow("../Sal/SalContractView.aspx?Action=view&FromUrl=" + escape(window.location.href) + "&ContractCode=" + ContractCode, "��ͬ��ϸ", 650, 560);
}

function Print()
{
	OpenPrintWindow("../Report/PrintList.aspx?FromControlID=tdList", "��ӡ");
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

SetAdvSearch();

if (Form1.txtIsLoadPrint.value == "1")
{
	Form1.txtIsLoadPrint.value = "";
	Print();
}

//-->
		</SCRIPT>
	</body>
</HTML>
