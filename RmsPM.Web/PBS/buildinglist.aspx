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
								<td class="topic" background="../images/topic_bg.gif"><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle">集团管理 
									- 楼栋查询</td>
								<td width="9"><IMG height="25" src="../images/topic_corr.gif" width="9"></td>
							</tr>
						</table>
					</td>
				</tr>
				<TR id="trToolBar" runat="server">
					<TD class="tools-area" vAlign="top"><IMG src="../images/btn_li.gif" align="absMiddle">
						<input class="button" id="btnPrint" onclick="Print()" type="button" value="打 印" name="btnPrint"
							runat="server"> <input class="button" id="btnPrintAll" onclick="document.all.divHintLoad.style.display = 'block';"
							type="button" value="打印全部" name="btnPrintAll" runat="server" style="DISPLAY: none" onserverclick="btnPrintAll_ServerClick"> <input class="button" id="btnAllowPaging" style="DISPLAY: none" onclick="document.all.divHintLoad.style.display = 'block';"
							type="button" value="取消分页" name="btnAllowPaging" runat="server" onserverclick="btnAllowPaging_ServerClick">
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
															项目状态：
															<SELECT class="select" id="sltSearchProjectStatus" name="sltSearchProjectStatus" runat="server">
																<option value="" selected>--请选择--</option>
															</SELECT>
															&nbsp;&nbsp;项目： <span id="divSearchProjectName" runat="server"></span><INPUT class="button-small" id="btnSelectProject" onclick="doOpenSelectProject();" type="button"
																value="选择项目" name="btnSelectProject"> &nbsp;&nbsp;<input class="submit" id="btnSearch" onclick="document.all.divHintLoad.style.display = 'block';"
																type="button" value="搜 索" name="btnSearch" runat="server" onserverclick="btnSearch_ServerClick"> &nbsp;<IMG id="imgAdvSearch" title="高级查询" style="DISPLAY: none;CURSOR: hand" onclick="ShowAdvSearch();"
																src="../images/search_more.gif">
														</td>
													</TR>
													<tr>
														<td>楼栋：<input class="input" id="txtSearchBuildingName" type="text" size="20" name="txtSearchBuildingName"
																runat="server"> &nbsp;&nbsp;产品类型：<SELECT class="select" id="sltSearchPBSTypeCode" name="sltSearchPBSTypeCode" runat="server">
																<OPTION value="" selected>--请选择--</OPTION>
															</SELECT>
															&nbsp;&nbsp;投资性质：<input class="input" id="txtSearchInvestType" type="text" size="10" name="txtSearchInvestType"
																runat="server"> <a href="#" onclick="SelectDict('投资性质', 'txtSearchInvestType')" title="选择投资性质">
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
															<asp:TemplateColumn Visible="False" HeaderText="&lt;input type='checkbox' name='chkAll' onclick='ChkSelectAll(document.all.chkSelect, document.all.chkAll.checked);' title='全选或全不选'&gt;">
																<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
																<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
																<ItemTemplate>
																	<input type="checkbox" name="chkSelect" class="list-checkbox" onclick="ChkSelectRow('<%# Container.ItemIndex + 1%>', this, document.all.dgList, 'list-2', 'list-i');" value='<%#DataBinder.Eval(Container, "DataItem.BuildingCode")%>'>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:BoundColumn DataField="ProjectName" HeaderText="项目" Visible="False" SortExpression="ProjectName" FooterText="合计">
																<HeaderStyle HorizontalAlign="Left" Wrap="False"></HeaderStyle>
																<ItemStyle HorizontalAlign="Left" Wrap="False"></ItemStyle>
															</asp:BoundColumn>
															<asp:BoundColumn DataField="ProjectStatus" HeaderText="项目状态" Visible="False" SortExpression="ProjectStatus">
																<HeaderStyle HorizontalAlign="Left" Wrap="False"></HeaderStyle>
																<ItemStyle HorizontalAlign="Left" Wrap="False"></ItemStyle>
															</asp:BoundColumn>
															<asp:TemplateColumn SortExpression="BuildingCode" HeaderText="楼栋">
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
															<asp:TemplateColumn SortExpression="IFloorCount" HeaderText="层<br>数">
																<ItemTemplate>
																	<%# DataBinder.Eval(Container, "DataItem.IFloorCount") %>
																</ItemTemplate>
																<HeaderStyle HorizontalAlign="Right" Wrap="False"></HeaderStyle>
																<ItemStyle HorizontalAlign="Right" Wrap="False"></ItemStyle>
																<FooterStyle HorizontalAlign="Right" Wrap="False"></FooterStyle>
															</asp:TemplateColumn>
															<asp:TemplateColumn SortExpression="HouseArea" HeaderText="计划面积<br>(平米)">
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
															<asp:TemplateColumn SortExpression="RoomArea" HeaderText="实测面积<br>(平米)">
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
															<asp:TemplateColumn HeaderText="产品类型" SortExpression="PBSTypeFullName">
																<ItemTemplate>
																	<%# DataBinder.Eval(Container, "DataItem.PBSTypeFullName") %>
																</ItemTemplate>
																<HeaderStyle HorizontalAlign="Left" Wrap="False"></HeaderStyle>
																<ItemStyle HorizontalAlign="Left" Wrap="False"></ItemStyle>
															</asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="当前形象进度" SortExpression="PBSUnitVisualProgress">
																<ItemTemplate>
																	<%# DataBinder.Eval(Container, "DataItem.PBSUnitVisualProgressName") %>
																</ItemTemplate>
																<HeaderStyle HorizontalAlign="Left" Wrap="False"></HeaderStyle>
																<ItemStyle HorizontalAlign="Left" Wrap="False"></ItemStyle>
															</asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="当年计划形象进度">
																<ItemTemplate>
																	<%# RmsPM.BLL.ConstructRule.GetVisualProgressName(RmsPM.BLL.ConstructRule.GetConstructPlanVisualProgress(RmsPM.BLL.ConvertRule.ToString(DataBinder.Eval(Container, "DataItem.PBSUnitCode")), DateTime.Today.Year)) %>
																</ItemTemplate>
																<HeaderStyle HorizontalAlign="Left" Wrap="False"></HeaderStyle>
																<ItemStyle HorizontalAlign="Left" Wrap="False"></ItemStyle>
															</asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="投资性质" SortExpression="InvestType">
																<ItemTemplate>
																	<%# DataBinder.Eval(Container, "DataItem.InvestType") %>
																</ItemTemplate>
																<HeaderStyle HorizontalAlign="Left" Wrap="False"></HeaderStyle>
																<ItemStyle HorizontalAlign="Left" Wrap="False"></ItemStyle>
															</asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="使用性质" SortExpression="UseType">
																<ItemTemplate>
																	<%# DataBinder.Eval(Container, "DataItem.UseType") %>
																</ItemTemplate>
																<HeaderStyle HorizontalAlign="Left" Wrap="False"></HeaderStyle>
																<ItemStyle HorizontalAlign="Left" Wrap="False"></ItemStyle>
															</asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="成本<br>(元)" SortExpression="TotalCost">
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
														<PagerStyle NextPageText="下页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上页" HorizontalAlign="Center"
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

//选择项目
function doOpenSelectProject()
{
	OpenMiddleWindow('../SelectBox/SelectProject.aspx?Type=multi' ,'选择项目');
}

//选择项目返回
function DoSelectProject(projectCode,projectName)
{
	document.all("divSearchProjectName").innerHTML = projectName;
	document.all("txtSearchProjectName").value = projectName;
	document.all("txtSearchProjectCode").value = projectCode;
}

//选择字典
function SelectDict(DictName, flag)
{
	OpenCustomWindow("../SelectBox/SelectDictItem.aspx?DictionaryName=" + escape(DictName) + "&type=multi&flag=" + flag, "选择字典" + DictName, 500, 560);
}

//选择字典返回
function SelectDictItemReturn(code, name, flag)
{
	document.all(flag).value = name;
}

//打印
function Print()
{
	OpenPrintWindow("../Report/PrintList.aspx?FromControlID=tdList", "打印");
}

//高级查询
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
		Form1.imgAdvSearch.title = "高级查询";
	}
	else
	{
//		Form1.imgAdvSearch.src = "../images/ArrowUp.gif";
		Form1.imgAdvSearch.title = "隐藏高级查询";
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
