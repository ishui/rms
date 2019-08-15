<%@ Page language="c#" Inherits="RmsPM.Web.WorkFlowDefinition.ProcedureManage" CodeFile="ProcedureManage.aspx.cs" %>
<%@ Register TagPrefix="igtab" Namespace="Infragistics.WebUI.UltraWebTab" Assembly="Infragistics.WebUI.UltraWebTab.v5.1, Version=5.1.20051.37, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v5.1, Version=5.1.20051.37, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="uc1" TagName="InputSystemGroup" Src="../UserControls/InputSystemGroup.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>流程管理</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">
						流程信息</td>
				</tr>
				<tr>
					<td vAlign="top">
						<table cellSpacing="7" cellPadding="0" width="100%" border="0">
							<tr>
								<td style="BORDER-RIGHT: #ededed 3px dotted; PADDING-RIGHT: 7px" vAlign="top" width="60%">
									<table class="form" cellSpacing="0" cellPadding="0" width="100%" border="0" id="TABLE2">
										<TR>
											<TD class="form-item" noWrap width="30%">名称：</TD>
                                            <td colspan="3" nowrap="nowrap">
                                                <input class="input" id="txtDescription" style="WIDTH: 448px; HEIGHT: 18px" type="text"
													size="69" name="txtDescription" runat="server"></td>
										</TR>
										<tr>
											<TD class="form-item" noWrap>表单资源路径：</TD>
                                            <td colspan="3" nowrap="nowrap">
                                                <input class="input" id="txtApplicationPath" style="WIDTH: 456px; HEIGHT: 18px" type="text"
													size="70" name="txtApplicationPath" runat="server"></td>
										</tr>
										<tr>
											<TD class="form-item" noWrap>其它资源路径：</TD>
                                            <td colspan="3" nowrap="nowrap">
                                                <input class="input" id="txtApplicationInfoPath" style="WIDTH: 456px; HEIGHT: 18px" type="text"
													size="70" name="txtApplicationInfoPath" runat="server"></td>
										</tr>
										<tr>
											<TD class="form-item" noWrap>流程备注信息：</TD>
                                            <td colspan="3" nowrap="nowrap">
                                                <input class="input" id="txtProcedureRemark" style="WIDTH: 456px; HEIGHT: 18px" type="text"
													size="70" name="txtProcedureRemark" runat="server"></td>
										</tr>
										<tr>
											<TD class="form-item" noWrap>流程版本说明：</TD>
                                            <td colspan="3" nowrap="nowrap">
                                                <input class="input" id="txtVersionDescription" style="WIDTH: 456px; HEIGHT: 18px" type="text"
													size="70" name="txtVersionDescription" runat="server"></td>
										</tr>
										<tr>
											<TD class="form-item" noWrap>索引：</TD>
											<TD noWrap><input class="input" id="txtProcedureName" type="text" name="txtProcedureName" runat="server"></TD>
                                            <td class="form-item" nowrap="nowrap">
                                                版 本 号：</td>
                                            <td nowrap="nowrap">
                                                <input class="input" id="txtVerson" type="text" name="txtProcedureName" runat="server"></td>
										</tr>
										<tr>
											<TD class="form-item" noWrap>代码：</TD>
											<TD noWrap><input class="input" id="txtProcedureNumber" type="text" name="txtProcedureNumber" runat="server"></TD>
                                            <td class="form-item" nowrap="nowrap">
                                                项目：</td>
                                            <td nowrap="nowrap">
                                                <asp:DropDownList ID="DropProject" runat="server">
                                                </asp:DropDownList></td>
										</tr>
										<tr>
											<TD class="form-item" noWrap>
                                                活动：</TD>
                                            <td colspan="3" nowrap="nowrap">
                                                <asp:CheckBox ID="CheckActivity" runat="server" /></td>
										</tr>
										<tr>
											<TD class="form-item" noWrap>类型：</TD>
                                            <td colspan="3" nowrap="nowrap">
                                                <select id="sltType" runat="server">
                                                    <option value="0" selected="selected">--常规类型--</option>
                                                    <option value="1" >通用</option>
                                                </select>&nbsp;
                                                通用流程类别：
                                                <uc1:inputsystemgroup id="inputSystemGroup" runat="server"></uc1:inputsystemgroup>
                                            </td>
										</tr>
									</table>
									<br />

									<igtab:ultrawebtab id="UltraWebTab1" runat="server" JavaScriptFileNameCommon="../Images/infragistics/20051/scripts/ig_shared.js"
										JavaScriptFileName="../Images/infragistics/20051/scripts/ig_webtab.js" ImageDirectory="../Images/infragistics/images/"
										Width="100%" Height="500px" BorderColor="#949878" BorderStyle="Solid" ThreeDEffect="False" BorderWidth="1px">
										<DefaultTabStyle Height="22px" Font-Size="8pt" Font-Names="Microsoft Sans Serif" ForeColor="Black"
											BackColor="#FEFCFD">
											<Padding Top="2px"></Padding>
										</DefaultTabStyle>
										<RoundedImage LeftSideWidth="7" RightSideWidth="6" ShiftOfImages="2" SelectedImage="ig_tab_winXP1.gif"
											NormalImage="ig_tab_winXP3.gif" HoverImage="ig_tab_winXP2.gif" FillStyle="LeftMergedWithCenter"></RoundedImage>
										<SelectedTabStyle>
											<Padding Bottom="2px"></Padding>
										</SelectedTabStyle>
										<Tabs>
											<igtab:Tab Text="- 任 务 -">
												<ContentTemplate>
													<TABLE id="Table1" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
														<TR>
															<TD colSpan="2">
																<igtbl:UltraWebGrid id="UltraWebGrid1" runat="server" Height="100%" Width="100%">
																	<DisplayLayout JavaScriptFileName="../Images/infragistics/20051/scripts/ig_WebGrid.js" StationaryMargins="HeaderAndFooter"
																		AutoGenerateColumns="False" AllowAddNewDefault="Yes" AllowSortingDefault="OnClient" JavaScriptFileNameCommon="../Images/infragistics/20051/Scripts/ig_shared.js"
																		RowHeightDefault="20px" Version="4.00" HeaderClickActionDefault="SortSingle" BorderCollapseDefault="Separate"
																		Name="UltraWebTab1UltraWebGrid1" TableLayout="Fixed" CellClickActionDefault="Edit">
																		<AddNewBox>
																			<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

																			</Style>
																		</AddNewBox>
																		<Pager>
																			<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

																			</Style>
																		</Pager>
																		<HeaderStyleDefault BorderStyle="Solid" BackColor="LightGray">
																			<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
																		</HeaderStyleDefault>
																		<FrameStyle Width="100%" BorderWidth="1px" Font-Size="8pt" Font-Names="Verdana" BorderStyle="Solid"
																			Height="100%"></FrameStyle>
																		<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
																			<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
																		</FooterStyleDefault>
																		<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
																		<RowStyleDefault BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid">
																			<Padding Left="3px"></Padding>
																			<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
																		</RowStyleDefault>
																		<ImageUrls ImageDirectory="../Images/infragistics/Images/"></ImageUrls>
																	</DisplayLayout>
																	<Bands>
																		<igtbl:UltraGridBand>
																			<Columns>
																				<igtbl:UltraGridColumn HeaderText="Code" Key="" Hidden="True" BaseColumnName="TaskCode">
																					<Footer Key=""></Footer>
																					<Header Key="" Caption="Code"></Header>
																				</igtbl:UltraGridColumn>
																				<igtbl:UltraGridColumn HeaderText="任务名称" Key="" Type="HyperLink" DataType="System.Int32" BaseColumnName="TaskName"
																					AllowResize="Free">
																					<Footer Key=""></Footer>
																					<Header Key="" Caption="任务名称"></Header>
																				</igtbl:UltraGridColumn>
																				<igtbl:UltraGridColumn HeaderText="任务代码" Key="" BaseColumnName="TaskID" AllowResize="Free">
																					<Footer Key=""></Footer>
																					<Header Key="" Caption="任务代码"></Header>
																				</igtbl:UltraGridColumn>
																				<igtbl:UltraGridColumn HeaderText="任务类型" Key="" Type="DropDownList" BaseColumnName="TaskType" AllowResize="Free">
																					<Footer Key=""></Footer>
																					<Header Key="" Caption="任务类型"></Header>
																				</igtbl:UltraGridColumn>
																				<igtbl:UltraGridColumn HeaderText="流程角色" Key="" Type="DropDownList" BaseColumnName="TaskRole" AllowResize="Free">
																					<Footer Key=""></Footer>
																					<Header Key="" Caption="流程角色"></Header>
																				</igtbl:UltraGridColumn>
																				<igtbl:UltraGridColumn HeaderText="符合限定" Key="" Type="DropDownList" BaseColumnName="TaskProperty" AllowResize="Free">
																					<Footer Key=""></Footer>
																					<Header Key="" Caption="符合限定"></Header>
																				</igtbl:UltraGridColumn>
																				<igtbl:UltraGridColumn HeaderText="选人方式" Key="" Type="DropDownList" BaseColumnName="WayOfSelectPerson"
																					AllowResize="Free">
																					<Footer Key=""></Footer>
																					<Header Key="" Caption="选人方式"></Header>
																				</igtbl:UltraGridColumn>
																				<igtbl:UltraGridColumn HeaderText="辅助控制" Key="" Width="500px" BaseColumnName="ModuleState" AllowResize="Free">
																					<Footer Key=""></Footer>
																					<Header Key="" Caption="辅助控制"></Header>
																				</igtbl:UltraGridColumn>
																			</Columns>
																		</igtbl:UltraGridBand>
																	</Bands>
																</igtbl:UltraWebGrid></TD>
														</TR>
														<TR bgColor="lightgrey">
															<TD width="30" height="20">&nbsp;新增</TD>
															<TD width="90%"><INPUT onclick="addNewTask();return false;" type="button" value="任务"></TD>
														</TR>
													</TABLE>
												</ContentTemplate>
											</igtab:Tab>
											<igtab:Tab Text="- 路 由 -">
												<ContentTemplate>
													<TABLE height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
														<TR>
															<TD>
																<igtbl:UltraWebGrid id="UltraWebGrid2" runat="server" Height="100%" Width="100%">
																	<DisplayLayout JavaScriptFileName="../Images/infragistics/20051/scripts/ig_WebGrid.js" StationaryMargins="HeaderAndFooter"
																		AutoGenerateColumns="False" AllowAddNewDefault="Yes" AllowSortingDefault="OnClient" JavaScriptFileNameCommon="../Images/infragistics/20051/Scripts/ig_shared.js"
																		RowHeightDefault="20px" Version="4.00" HeaderClickActionDefault="SortSingle" BorderCollapseDefault="Separate"
																		AllowColSizingDefault="Free" Name="UltraWebTab1UltraWebGrid2" TableLayout="Fixed" CellClickActionDefault="Edit"
																		AllowUpdateDefault="Yes">
																		<AddNewBox Hidden="False" Prompt="添加">
																			<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

																			</Style>
																		</AddNewBox>
																		<Pager>
																			<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

																			</Style>
																		</Pager>
																		<HeaderStyleDefault BorderStyle="Solid" BackColor="LightGray">
																			<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
																		</HeaderStyleDefault>
																		<FrameStyle Width="100%" BorderWidth="1px" Font-Size="8pt" Font-Names="Verdana" BorderStyle="Solid"
																			Height="100%"></FrameStyle>
																		<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
																			<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
																		</FooterStyleDefault>
																		<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
																		<RowStyleDefault BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid">
																			<Padding Left="3px"></Padding>
																			<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
																		</RowStyleDefault>
																		<ImageUrls ImageDirectory="../Images/infragistics//Images/"></ImageUrls>
																		<Strings DownLevelCancelPrompt="取消"></Strings>
																	</DisplayLayout>
																	<Bands>
																		<igtbl:UltraGridBand>
																			<Columns>
																				<igtbl:UltraGridColumn HeaderText="Code" Key="" Hidden="True" BaseColumnName="RouterCode">
																					<Footer Key=""></Footer>
																					<Header Key="" Caption="Code"></Header>
																				</igtbl:UltraGridColumn>
																				<igtbl:UltraGridColumn HeaderText="路由名称" Key="" BaseColumnName="Description" AllowResize="Free" AllowUpdate="Yes">
																					<Footer Key=""></Footer>
																					<Header Key="" Caption="路由名称"></Header>
																				</igtbl:UltraGridColumn>
																				<igtbl:UltraGridColumn HeaderText="开始任务节点" Key="" Type="DropDownList" BaseColumnName="FromTaskCode" AllowResize="Free"
																					AllowUpdate="Yes">
																					<Footer Key=""></Footer>
																					<Header Key="" Caption="开始任务节点"></Header>
																				</igtbl:UltraGridColumn>
																				<igtbl:UltraGridColumn HeaderText="结束任务节点" Key="" Type="DropDownList" BaseColumnName="ToTaskCode" AllowResize="Free"
																					AllowUpdate="Yes">
																					<Footer Key=""></Footer>
																					<Header Key="" Caption="结束任务节点"></Header>
																				</igtbl:UltraGridColumn>
																				<igtbl:UltraGridColumn HeaderText="路由代码" Key="" BaseColumnName="SortID" AllowResize="Free" AllowUpdate="Yes">
																					<Footer Key=""></Footer>
																					<Header Key="" Caption="路由代码"></Header>
																				</igtbl:UltraGridColumn>
																				<igtbl:UltraGridColumn HeaderText="路由条件" Key="" Width="500px" BaseColumnName="" AllowResize="Free" AllowUpdate="Yes">
																					<Footer Key=""></Footer>
																					<Header Key="" Caption="路由条件"></Header>
																				</igtbl:UltraGridColumn>
																				<igtbl:UltraGridColumn HeaderText="" Key="" Type="HyperLink" BaseColumnName="" AllowResize="Free" AllowUpdate="No">
																					<Footer Key=""></Footer>
																					<Header Key="" Caption=""></Header>
																				</igtbl:UltraGridColumn>
																			</Columns>
																		</igtbl:UltraGridBand>
																	</Bands>
																</igtbl:UltraWebGrid></TD>
															<TD vAlign="top">
																<asp:datagrid id="dgList" runat="server" Width="200" CssClass="list" CellPadding="0" GridLines="Horizontal"
																	AutoGenerateColumns="False" PageSize="18">
																	<HeaderStyle CssClass="list-title"></HeaderStyle>
																	<FooterStyle CssClass="list-title"></FooterStyle>
																	<Columns>
																		<asp:BoundColumn DataField="ProcedurePropertyName" HeaderText="属性"></asp:BoundColumn>
																	</Columns>
																	<PagerStyle NextPageText="下一页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上一页" HorizontalAlign="Right"
																		CssClass="ListHeadTr"></PagerStyle>
																</asp:datagrid></TD>
														</TR>
													</TABLE>
												</ContentTemplate>
											</igtab:Tab>
											<igtab:Tab Text="- 角 色 -">
												<ContentTemplate>
													<igtbl:UltraWebGrid id="UltraWebGrid3" runat="server" Height="100%" Width="100%">
														<DisplayLayout JavaScriptFileName="../Images/infragistics/20051/scripts/ig_WebGrid.js" StationaryMargins="HeaderAndFooter"
															AutoGenerateColumns="False" AllowAddNewDefault="Yes" AllowSortingDefault="OnClient" JavaScriptFileNameCommon="../Images/infragistics/20051/Scripts/ig_shared.js"
															RowHeightDefault="20px" Version="4.00" HeaderClickActionDefault="SortSingle" BorderCollapseDefault="Separate"
															Name="UltraWebTab1UltraWebGrid3" TableLayout="Fixed" CellClickActionDefault="Edit" AllowUpdateDefault="Yes">
															<AddNewBox Hidden="False" Prompt="添加">
																<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

																</Style>
															</AddNewBox>
															<Pager>
																<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

																</Style>
															</Pager>
															<HeaderStyleDefault BorderStyle="Solid" BackColor="LightGray">
																<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
															</HeaderStyleDefault>
															<FrameStyle Width="100%" BorderWidth="1px" Font-Size="8pt" Font-Names="Verdana" BorderStyle="Solid"
																Height="100%"></FrameStyle>
															<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
																<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
															</FooterStyleDefault>
															<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
															<RowStyleDefault BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid">
																<Padding Left="3px"></Padding>
																<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
															</RowStyleDefault>
															<ImageUrls ImageDirectory="../Images/infragistics//Images/"></ImageUrls>
														</DisplayLayout>
														<Bands>
															<igtbl:UltraGridBand>
																<Columns>
																	<igtbl:UltraGridColumn HeaderText="Code" Key="" Hidden="True" BaseColumnName="WorkFlowRoleCode">
																		<Footer Key=""></Footer>
																		<Header Key="" Caption="Code"></Header>
																	</igtbl:UltraGridColumn>
																	<igtbl:UltraGridColumn HeaderText="角色名称" Key="" BaseColumnName="RoleName" AllowResize="Free" AllowUpdate="Yes">
																		<Footer Key=""></Footer>
																		<Header Key="" Caption="角色名称"></Header>
																	</igtbl:UltraGridColumn>
																	<igtbl:UltraGridColumn HeaderText="全局" Key="" Type="CheckBox" DataType="System.Boolean" BaseColumnName=""
																		AllowResize="Free" AllowUpdate="Yes">
																		<Footer Key=""></Footer>
																		<Header Key="" Caption="全局"></Header>
																	</igtbl:UltraGridColumn>
																	<igtbl:UltraGridColumn HeaderText="备注" Key="" BaseColumnName="Remak" AllowResize="Free" AllowUpdate="Yes">
																		<Footer Key=""></Footer>
																		<Header Key="" Caption="备注"></Header>
																	</igtbl:UltraGridColumn>
																	<igtbl:UltraGridColumn HeaderText="角色成员" Key="" Type="HyperLink" BaseColumnName="" AllowResize="Free" AllowUpdate="No">
																		<Footer Key=""></Footer>
																		<Header Key="" Caption="角色成员"></Header>
																	</igtbl:UltraGridColumn>
																	<igtbl:UltraGridColumn HeaderText="全局" Key="" Hidden="True" BaseColumnName="RoleType" AllowResize="Free"
																		AllowUpdate="Yes">
																		<Footer Key=""></Footer>
																		<Header Key="" Caption="全局"></Header>
																	</igtbl:UltraGridColumn>
																	<igtbl:UltraGridColumn Key="" Type="HyperLink" BaseColumnName="" AllowResize="Free" AllowUpdate="No">
																		<Footer Key=""></Footer>
																		<Header Key=""></Header>
																	</igtbl:UltraGridColumn>
																</Columns>
															</igtbl:UltraGridBand>
														</Bands>
													</igtbl:UltraWebGrid>
												</ContentTemplate>
											</igtab:Tab>
											<igtab:Tab Text="- 属 性 -">
												<ContentTemplate>
													<igtbl:UltraWebGrid id="UltraWebGrid4" runat="server" Width="100%" Height="100%">
														<DisplayLayout JavaScriptFileName="../Images/infragistics/20051/scripts/ig_WebGrid.js" StationaryMargins="HeaderAndFooter"
															AutoGenerateColumns="False" AllowAddNewDefault="Yes" AllowSortingDefault="OnClient" JavaScriptFileNameCommon="../Images/infragistics/20051/Scripts/ig_shared.js"
															RowHeightDefault="20px" Version="4.00" HeaderClickActionDefault="SortSingle" BorderCollapseDefault="Separate"
															Name="UltraWebTab1UltraWebGrid4" TableLayout="Fixed" CellClickActionDefault="Edit">
															<AddNewBox Hidden="False" Prompt="添加">
																<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

																</Style>
															</AddNewBox>
															<Pager>
																<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

																</Style>
															</Pager>
															<HeaderStyleDefault BorderStyle="Solid" BackColor="LightGray">
																<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
															</HeaderStyleDefault>
															<FrameStyle Width="100%" BorderWidth="1px" Font-Size="8pt" Font-Names="Verdana" BorderStyle="Solid"
																Height="100%"></FrameStyle>
															<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
																<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
															</FooterStyleDefault>
															<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
															<RowStyleDefault BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid">
																<Padding Left="3px"></Padding>
																<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
															</RowStyleDefault>
															<ImageUrls ImageDirectory="../Images/infragistics//Images/"></ImageUrls>
														</DisplayLayout>
														<Bands>
															<igtbl:UltraGridBand>
																<Columns>
																	<igtbl:UltraGridColumn HeaderText="Code" Key="" Hidden="True" BaseColumnName="WorkFlowProcedurePropertyCode">
																		<Footer Key=""></Footer>
																		<Header Key="" Caption="Code"></Header>
																	</igtbl:UltraGridColumn>
																	<igtbl:UltraGridColumn HeaderText="属性名称" Key="" BaseColumnName="ProcedurePropertyName" AllowResize="Free"
																		AllowUpdate="Yes">
																		<Footer Key=""></Footer>
																		<Header Key="" Caption="属性名称"></Header>
																	</igtbl:UltraGridColumn>
																	<igtbl:UltraGridColumn HeaderText="类型" Key="" Type="DropDownList" BaseColumnName="ProcedurePropertyType"
																		AllowResize="Free" AllowUpdate="Yes">
																		<Footer Key=""></Footer>
																		<Header Key="" Caption="类型"></Header>
																	</igtbl:UltraGridColumn>
																	<igtbl:UltraGridColumn HeaderText="备注" Key="" BaseColumnName="Remak" AllowResize="Free" AllowUpdate="Yes">
																		<Footer Key=""></Footer>
																		<Header Key="" Caption="备注"></Header>
																	</igtbl:UltraGridColumn>
																	<igtbl:UltraGridColumn Key="" Type="HyperLink" BaseColumnName="" AllowResize="Free" AllowUpdate="No">
																		<Footer Key=""></Footer>
																		<Header Key=""></Header>
																	</igtbl:UltraGridColumn>
																</Columns>
															</igtbl:UltraGridBand>
														</Bands>
													</igtbl:UltraWebGrid>
												</ContentTemplate>
											</igtab:Tab>
										</Tabs>
									</igtab:ultrawebtab><br>
									<table width="100%">
										<tr>
											<td align="center"><input class="submit" id="btnSaveProcedure" type="button" value="保存流程" name="btnSaveProcedure"
													runat="server" onserverclick="btnSaveProcedure_ServerClick"> <input class="submit" id="btnDeleteProcedure" onclick="if(!confirm('是否确定删除 ？')) return false; "
													type="button" value="删除流程" name="btnDeleteProcedure" runat="server" onserverclick="btnDeleteProcedure_ServerClick"> <input class="submit" id="btnClose" onclick="window.close();" type="button" value=" 关闭 "
													name="btnClose" runat="server">
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<input id="btnRefresh" style="DISPLAY: none" type="button" value="刷新" name="btnRefresh"
				runat="server" onserverclick="btnRefresh_ServerClick"> <input type="button" style="DISPLAY:none" value="删除路由" id="btnDeleteRouter" name="btnDeleteRouter"
				runat="server" onserverclick="btnDeleteRouter_ServerClick"> <input type="button" style="DISPLAY:none" value="删除角色" id="btnDeleteRole" name="btnDeleteRole"
				runat="server" onserverclick="btnDeleteRole_ServerClick"> <input type="button" style="DISPLAY:none" value="删除属性" id="btnDeleteProperty" name="btnDeleteProperty"
				runat="server" onserverclick="btnDeleteProperty_ServerClick"> <input id="btnRoleCompriseSave" style="DISPLAY: none" type="button" value="保存角色组成" name="btnRoleCompriseSave"
				runat="server" onserverclick="btnRoleCompriseSave_ServerClick"> <input id="RoleCompriseUserCodes" type="hidden" name="RoleCompriseUserCodes" runat="server">
			<input id="RoleCompriseStationCodes" type="hidden" name="RoleCompriseStationCodes" runat="server">
			<input id="RoleCompriseCode" type="hidden" name="RoleCompriseCode" runat="server">
			<input id="DeleteObjectCode" type="hidden" name="DeleteObjectCode" runat="server">
		</form>
		<script language="javascript">
		/*** 新增任务 ***/
		function addNewTask()
		{
			OpenLargeWindow('TaskManager.aspx','新增任务');
		}
		/*** 修改任务 ***/
		function modifyTask(taskCode)
		{
			OpenLargeWindow('TaskManager.aspx?TaskCode=' + taskCode,'修改任务');
		}
		
		/*** 刷新页面 ***/
		function RefreshGrid()
		{
			Form1.btnRefresh.click();
		}
		
		/*** 打开角色组成页面 ***/
		function SelectRoleComprise(RoleCode,UserCodes,StationCodes)
　　	{
　			OpenMiddleWindow("../SelectBox/SelectSUMain.aspx?UserCodes="+UserCodes+"&StationCodes="+StationCodes+"&Flag="+RoleCode,null);
　　	}
　　	
　　	/*** 接收组成角色页面进行操作 ***/
　　	function getReturnStationUser(userCodes,userNames,stationCodes,stationNames,flag)
		{
			/*if(flag==1)
			{
				window.document.all.txtMaster.value = userCodes;	
				window.document.all.txtMasterStations.value = stationCodes;	
				window.document.all.SelectName1.innerText = getString(userNames,stationNames);
				window.document.all.hSelect1.value = getString(userNames,stationNames);
			}*/
			document.Form1.RoleCompriseCode.value = flag;
			document.Form1.RoleCompriseStationCodes.value = stationCodes;
			document.Form1.RoleCompriseUserCodes.value = userCodes;
			//alert("用户代码："+userCodes+"\n"+"岗位代码："+stationCodes+"\n"+flag);
			document.Form1.btnRoleCompriseSave.click();
		}
		/*** 格式化字符串 ***/
		function getString(str1,str2)
		{
			if(str1.length>0&&str2.length>0)
			{
				return str1+','+str2;
			}
			else
				return str1+str2;
		}
		/*** 重新加载Task内容 ***/
		function LoadTask()
		{
		    document.Form1.btnRefresh.click();
		}
		/*** 删除路由 ***/
		function deleteRouter(RouterCode)
		{
		    document.Form1.DeleteObjectCode.value = RouterCode;
		    document.Form1.btnDeleteRouter.click();
		}
		/*** 删除角色 ***/
		function deleteRole(RoleCode)
		{
		    document.Form1.DeleteObjectCode.value = RoleCode;
		    document.Form1.btnDeleteRole.click();;
		}
		/*** 删除属性 ***/
		function deleteProperty(PropertyCode)
		{
		    document.Form1.DeleteObjectCode.value = PropertyCode;
		    document.Form1.btnDeleteProperty.click();
		    
		}
		
		
		</script>
	</body>
</HTML>
