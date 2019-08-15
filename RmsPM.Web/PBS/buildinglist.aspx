<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Page language="c#" Inherits="RmsPM.Web.PBS.BuildingList" CodeFile="BuildingList.aspx.cs" %>
<%@ Register TagPrefix="cc1" Namespace="Tiannuo.Web.Controls" Assembly="DataGridWebControls" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>BuildingList</title>
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
								<td class="topic" background="../images/topic_bg.gif"><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle">���Ź��� 
									- ¥����ѯ</td>
								<td width="9"><IMG height="25" src="../images/topic_corr.gif" width="9"></td>
							</tr>
						</table>
					</td>
				</tr>
				<TR id="trToolBar" runat="server">
					<TD class="tools-area" vAlign="top"><IMG src="../images/btn_li.gif" align="absMiddle">
						<input class="button" id="btnPrint" onclick="Print()" type="button" value="�� ӡ" name="btnPrint"
							runat="server"> <input class="button" id="btnPrintAll" onclick="document.all.divHintLoad.style.display = 'block';"
							type="button" value="��ӡȫ��" name="btnPrintAll" runat="server" style="DISPLAY: none" onserverclick="btnPrintAll_ServerClick"> <input class="button" id="btnAllowPaging" style="DISPLAY: none" onclick="document.all.divHintLoad.style.display = 'block';"
							type="button" value="ȡ����ҳ" name="btnAllowPaging" runat="server" onserverclick="btnAllowPaging_ServerClick">
					</TD>
				</TR>
				<tr height="100%">
					<TD class="table" vAlign="top">
						<table width="100%" height="100%">
							<tr>
								<td>
									<TABLE class="search-area" cellSpacing="0" cellPadding="0" width="100%" border="0">
										<tr>
											<td>
												<table>
													<TR>
														<td>
															��Ŀ״̬��
															<SELECT class="select" id="sltSearchProjectStatus" name="sltSearchProjectStatus" runat="server">
																<option value="" selected>--��ѡ��--</option>
															</SELECT>
															&nbsp;&nbsp;��Ŀ�� <span id="divSearchProjectName" runat="server"></span><INPUT class="button-small" id="btnSelectProject" onclick="doOpenSelectProject();" type="button"
																value="ѡ����Ŀ" name="btnSelectProject"> &nbsp;&nbsp;<input class="submit" id="btnSearch" onclick="document.all.divHintLoad.style.display = 'block';"
																type="button" value="�� ��" name="btnSearch" runat="server" onserverclick="btnSearch_ServerClick"> &nbsp;<IMG id="imgAdvSearch" title="�߼���ѯ" style="DISPLAY: none;CURSOR: hand" onclick="ShowAdvSearch();"
																src="../images/search_more.gif">
														</td>
													</TR>
													<tr>
														<td>¥����<input class="input" id="txtSearchBuildingName" type="text" size="20" name="txtSearchBuildingName"
																runat="server"> &nbsp;&nbsp;��Ʒ���ͣ�<SELECT class="select" id="sltSearchPBSTypeCode" name="sltSearchPBSTypeCode" runat="server">
																<OPTION value="" selected>--��ѡ��--</OPTION>
															</SELECT>
															&nbsp;&nbsp;Ͷ�����ʣ�<input class="input" id="txtSearchInvestType" type="text" size="10" name="txtSearchInvestType"
																runat="server"> <a href="#" onclick="SelectDict('Ͷ������', 'txtSearchInvestType')" title="ѡ��Ͷ������">
																<img src="../images/ToolsItemSearch.gif" border="0"></a>
														</td>
													</tr>
												</table>
											</td>
										</tr>
									</TABLE>
								</td>
							</tr>
							<tr height="100%">
								<td vAlign="top">
									<div style="OVERFLOW: auto; WIDTH: 100%; POSITION: absolute; HEIGHT: 100%">
										<table cellSpacing="0" cellPadding="0" width="100%" border="0">
											<tr>
												<td id="tdList"><asp:datagrid id="dgList" runat="server" Width="100%" CssClass="list" CellPadding="0" AllowSorting="True"
														AutoGenerateColumns="False" PageSize="100" ShowFooter="True" DataKeyField="BuildingCode" PagerStyle-Visible="False">
														<ItemStyle CssClass=""></ItemStyle>
														<HeaderStyle CssClass="list-title"></HeaderStyle>
														<FooterStyle CssClass="list-title"></FooterStyle>
														<Columns>
															<asp:TemplateColumn Visible="False" HeaderText="&lt;input type='checkbox' name='chkAll' onclick='ChkSelectAll(document.all.chkSelect, document.all.chkAll.checked);' title='ȫѡ��ȫ��ѡ'&gt;">
																<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
																<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
																<ItemTemplate>
																	<input type="checkbox" name="chkSelect" class="list-checkbox" onclick="ChkSelectRow('<%# Container.ItemIndex + 1%>', this, document.all.dgList, 'list-2', 'list-i');" value='<%#DataBinder.Eval(Container, "DataItem.BuildingCode")%>'>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:BoundColumn DataField="ProjectName" HeaderText="��Ŀ" Visible="False" SortExpression="ProjectName" FooterText="�ϼ�">
																<HeaderStyle HorizontalAlign="Left" Wrap="False"></HeaderStyle>
																<ItemStyle HorizontalAlign="Left" Wrap="False"></ItemStyle>
															</asp:BoundColumn>
															<asp:BoundColumn DataField="ProjectStatus" HeaderText="��Ŀ״̬" Visible="False" SortExpression="ProjectStatus">
																<HeaderStyle HorizontalAlign="Left" Wrap="False"></HeaderStyle>
																<ItemStyle HorizontalAlign="Left" Wrap="False"></ItemStyle>
															</asp:BoundColumn>
															<asp:TemplateColumn SortExpression="BuildingCode" HeaderText="¥��">
																<ItemTemplate>
																	<a href="#" onclick="OpenBuildingInfo(this.BuildingCode);return false;" BuildingCode='<%# DataBinder.Eval(Container, "DataItem.BuildingCode") %>'><%# DataBinder.Eval(Container, "DataItem.BuildingName") %></a>
																</ItemTemplate>
																<HeaderStyle Wrap="False"></HeaderStyle>
																<ItemStyle Wrap="False"></ItemStyle>
																<FooterStyle Wrap="False"></FooterStyle>
																<FooterTemplate>
																	<asp:Label runat="server" ID="lblSumCount"></asp:Label>
																</FooterTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn SortExpression="IFloorCount" HeaderText="��<br>��">
																<ItemTemplate>
																	<%# DataBinder.Eval(Container, "DataItem.IFloorCount") %>
																</ItemTemplate>
																<HeaderStyle HorizontalAlign="Right" Wrap="False"></HeaderStyle>
																<ItemStyle HorizontalAlign="Right" Wrap="False"></ItemStyle>
																<FooterStyle HorizontalAlign="Right" Wrap="False"></FooterStyle>
															</asp:TemplateColumn>
															<asp:TemplateColumn SortExpression="HouseArea" HeaderText="�ƻ����<br>(ƽ��)">
																<ItemTemplate>
																	<%# RmsPM.BLL.MathRule.GetDecimalShowString(DataBinder.Eval(Container, "DataItem.HouseArea")) %>
																</ItemTemplate>
																<FooterTemplate>
																	<asp:Label runat="server" ID="lblSumHouseArea"></asp:Label>
																</FooterTemplate>
																<HeaderStyle HorizontalAlign="Right" Wrap="False"></HeaderStyle>
																<ItemStyle HorizontalAlign="Right" Wrap="False"></ItemStyle>
																<FooterStyle HorizontalAlign="Right" Wrap="False"></FooterStyle>
															</asp:TemplateColumn>
															<asp:TemplateColumn SortExpression="RoomArea" HeaderText="ʵ�����<br>(ƽ��)">
																<ItemTemplate>
																	<%# RmsPM.BLL.MathRule.GetDecimalShowString(DataBinder.Eval(Container, "DataItem.RoomArea")) %>
																</ItemTemplate>
																<FooterTemplate>
																	<asp:Label runat="server" ID="lblSumRoomArea"></asp:Label>
																</FooterTemplate>
																<HeaderStyle HorizontalAlign="Right" Wrap="False"></HeaderStyle>
																<ItemStyle HorizontalAlign="Right" Wrap="False"></ItemStyle>
																<FooterStyle HorizontalAlign="Right" Wrap="False"></FooterStyle>
															</asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="��Ʒ����" SortExpression="PBSTypeFullName">
																<ItemTemplate>
																	<%# DataBinder.Eval(Container, "DataItem.PBSTypeFullName") %>
																</ItemTemplate>
																<HeaderStyle HorizontalAlign="Left" Wrap="False"></HeaderStyle>
																<ItemStyle HorizontalAlign="Left" Wrap="False"></ItemStyle>
															</asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="��ǰ�������" SortExpression="PBSUnitVisualProgress">
																<ItemTemplate>
																	<%# DataBinder.Eval(Container, "DataItem.PBSUnitVisualProgressName") %>
																</ItemTemplate>
																<HeaderStyle HorizontalAlign="Left" Wrap="False"></HeaderStyle>
																<ItemStyle HorizontalAlign="Left" Wrap="False"></ItemStyle>
															</asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="����ƻ��������">
																<ItemTemplate>
																	<%# RmsPM.BLL.ConstructRule.GetVisualProgressName(RmsPM.BLL.ConstructRule.GetConstructPlanVisualProgress(RmsPM.BLL.ConvertRule.ToString(DataBinder.Eval(Container, "DataItem.PBSUnitCode")), DateTime.Today.Year)) %>
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
															<asp:TemplateColumn HeaderText="�ɱ�<br>(Ԫ)" SortExpression="TotalCost">
																<ItemTemplate>
																	<%# RmsPM.BLL.MathRule.GetDecimalShowString(DataBinder.Eval(Container, "DataItem.TotalCost")) %>
																</ItemTemplate>
																<FooterTemplate>
																	<asp:Label runat="server" ID="lblSumTotalCost"></asp:Label>
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
			<input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server"> <input id="txtSelectBuildingCode" type="hidden" name="txtSelectBuildingCode" runat="server">
			<input id="txtAdvSearch" type="hidden" name="txtAdvSearch" runat="server"> <input id="txtSearchProjectCode" type="hidden" name="txtSearchProjectCode" runat="server"><input id="txtSearchProjectName" type="hidden" name="txtSearchProjectName" runat="server">
			<input id="txtIsLoadPrint" type="hidden" name="txtIsLoadPrint" runat="server">
		</form>
		<SCRIPT language="javascript">
<!--

function DoRefresh()
{
	Form1.btnSearch.click();
}

//ѡ����Ŀ
function doOpenSelectProject()
{
	OpenMiddleWindow('../SelectBox/SelectProject.aspx?Type=multi' ,'ѡ����Ŀ');
}

//ѡ����Ŀ����
function DoSelectProject(projectCode,projectName)
{
	document.all("divSearchProjectName").innerHTML = projectName;
	document.all("txtSearchProjectName").value = projectName;
	document.all("txtSearchProjectCode").value = projectCode;
}

//ѡ���ֵ�
function SelectDict(DictName, flag)
{
	OpenCustomWindow("../SelectBox/SelectDictItem.aspx?DictionaryName=" + escape(DictName) + "&type=multi&flag=" + flag, "ѡ���ֵ�" + DictName, 500, 560);
}

//ѡ���ֵ䷵��
function SelectDictItemReturn(code, name, flag)
{
	document.all(flag).value = name;
}

//��ӡ
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

//SetAdvSearch();

if (Form1.txtIsLoadPrint.value == "1")
{
	Form1.txtIsLoadPrint.value = "";
	Print();
}

//-->
		</SCRIPT>
	</body>
</HTML>
