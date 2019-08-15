<%@ Page language="c#" Inherits="RmsPM.Web.WorkFlowDefinition.ProcedureManage" CodeFile="ProcedureManage.aspx.cs" %>
<%@ Register TagPrefix="igtab" Namespace="Infragistics.WebUI.UltraWebTab" Assembly="Infragistics.WebUI.UltraWebTab.v5.1, Version=5.1.20051.37, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v5.1, Version=5.1.20051.37, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="uc1" TagName="InputSystemGroup" Src="../UserControls/InputSystemGroup.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>���̹���</title>
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
						������Ϣ</td>
				</tr>
				<tr>
					<td vAlign="top">
						<table cellSpacing="7" cellPadding="0" width="100%" border="0">
							<tr>
								<td style="BORDER-RIGHT: #ededed 3px dotted; PADDING-RIGHT: 7px" vAlign="top" width="60%">
									<table class="form" cellSpacing="0" cellPadding="0" width="100%" border="0" id="TABLE2">
										<TR>
											<TD class="form-item" noWrap width="30%">���ƣ�</TD>
                                            <td colspan="3" nowrap="nowrap">
                                                <input class="input" id="txtDescription" style="WIDTH: 448px; HEIGHT: 18px" type="text"
													size="69" name="txtDescription" runat="server"></td>
										</TR>
										<tr>
											<TD class="form-item" noWrap>����Դ·����</TD>
                                            <td colspan="3" nowrap="nowrap">
                                                <input class="input" id="txtApplicationPath" style="WIDTH: 456px; HEIGHT: 18px" type="text"
													size="70" name="txtApplicationPath" runat="server"></td>
										</tr>
										<tr>
											<TD class="form-item" noWrap>������Դ·����</TD>
                                            <td colspan="3" nowrap="nowrap">
                                                <input class="input" id="txtApplicationInfoPath" style="WIDTH: 456px; HEIGHT: 18px" type="text"
													size="70" name="txtApplicationInfoPath" runat="server"></td>
										</tr>
										<tr>
											<TD class="form-item" noWrap>���̱�ע��Ϣ��</TD>
                                            <td colspan="3" nowrap="nowrap">
                                                <input class="input" id="txtProcedureRemark" style="WIDTH: 456px; HEIGHT: 18px" type="text"
													size="70" name="txtProcedureRemark" runat="server"></td>
										</tr>
										<tr>
											<TD class="form-item" noWrap>���̰汾˵����</TD>
                                            <td colspan="3" nowrap="nowrap">
                                                <input class="input" id="txtVersionDescription" style="WIDTH: 456px; HEIGHT: 18px" type="text"
													size="70" name="txtVersionDescription" runat="server"></td>
										</tr>
										<tr>
											<TD class="form-item" noWrap>������</TD>
											<TD noWrap><input class="input" id="txtProcedureName" type="text" name="txtProcedureName" runat="server"></TD>
                                            <td class="form-item" nowrap="nowrap">
                                                �� �� �ţ�</td>
                                            <td nowrap="nowrap">
                                                <input class="input" id="txtVerson" type="text" name="txtProcedureName" runat="server"></td>
										</tr>
										<tr>
											<TD class="form-item" noWrap>���룺</TD>
											<TD noWrap><input class="input" id="txtProcedureNumber" type="text" name="txtProcedureNumber" runat="server"></TD>
                                            <td class="form-item" nowrap="nowrap">
                                                ��Ŀ��</td>
                                            <td nowrap="nowrap">
                                                <asp:DropDownList ID="DropProject" runat="server">
                                                </asp:DropDownList></td>
										</tr>
										<tr>
											<TD class="form-item" noWrap>
                                                ���</TD>
                                            <td colspan="3" nowrap="nowrap">
                                                <asp:CheckBox ID="CheckActivity" runat="server" /></td>
										</tr>
										<tr>
											<TD class="form-item" noWrap>���ͣ�</TD>
                                            <td colspan="3" nowrap="nowrap">
                                                <select id="sltType" runat="server">
                                                    <option value="0" selected="selected">--��������--</option>
                                                    <option value="1" >ͨ��</option>
                                                </select>&nbsp;
                                                ͨ���������
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
											<igtab:Tab Text="- �� �� -">
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
																				<igtbl:UltraGridColumn HeaderText="��������" Key="" Type="HyperLink" DataType="System.Int32" BaseColumnName="TaskName"
																					AllowResize="Free">
																					<Footer Key=""></Footer>
																					<Header Key="" Caption="��������"></Header>
																				</igtbl:UltraGridColumn>
																				<igtbl:UltraGridColumn HeaderText="�������" Key="" BaseColumnName="TaskID" AllowResize="Free">
																					<Footer Key=""></Footer>
																					<Header Key="" Caption="�������"></Header>
																				</igtbl:UltraGridColumn>
																				<igtbl:UltraGridColumn HeaderText="��������" Key="" Type="DropDownList" BaseColumnName="TaskType" AllowResize="Free">
																					<Footer Key=""></Footer>
																					<Header Key="" Caption="��������"></Header>
																				</igtbl:UltraGridColumn>
																				<igtbl:UltraGridColumn HeaderText="���̽�ɫ" Key="" Type="DropDownList" BaseColumnName="TaskRole" AllowResize="Free">
																					<Footer Key=""></Footer>
																					<Header Key="" Caption="���̽�ɫ"></Header>
																				</igtbl:UltraGridColumn>
																				<igtbl:UltraGridColumn HeaderText="�����޶�" Key="" Type="DropDownList" BaseColumnName="TaskProperty" AllowResize="Free">
																					<Footer Key=""></Footer>
																					<Header Key="" Caption="�����޶�"></Header>
																				</igtbl:UltraGridColumn>
																				<igtbl:UltraGridColumn HeaderText="ѡ�˷�ʽ" Key="" Type="DropDownList" BaseColumnName="WayOfSelectPerson"
																					AllowResize="Free">
																					<Footer Key=""></Footer>
																					<Header Key="" Caption="ѡ�˷�ʽ"></Header>
																				</igtbl:UltraGridColumn>
																				<igtbl:UltraGridColumn HeaderText="��������" Key="" Width="500px" BaseColumnName="ModuleState" AllowResize="Free">
																					<Footer Key=""></Footer>
																					<Header Key="" Caption="��������"></Header>
																				</igtbl:UltraGridColumn>
																			</Columns>
																		</igtbl:UltraGridBand>
																	</Bands>
																</igtbl:UltraWebGrid></TD>
														</TR>
														<TR bgColor="lightgrey">
															<TD width="30" height="20">&nbsp;����</TD>
															<TD width="90%"><INPUT onclick="addNewTask();return false;" type="button" value="����"></TD>
														</TR>
													</TABLE>
												</ContentTemplate>
											</igtab:Tab>
											<igtab:Tab Text="- · �� -">
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
																		<AddNewBox Hidden="False" Prompt="���">
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
																		<Strings DownLevelCancelPrompt="ȡ��"></Strings>
																	</DisplayLayout>
																	<Bands>
																		<igtbl:UltraGridBand>
																			<Columns>
																				<igtbl:UltraGridColumn HeaderText="Code" Key="" Hidden="True" BaseColumnName="RouterCode">
																					<Footer Key=""></Footer>
																					<Header Key="" Caption="Code"></Header>
																				</igtbl:UltraGridColumn>
																				<igtbl:UltraGridColumn HeaderText="·������" Key="" BaseColumnName="Description" AllowResize="Free" AllowUpdate="Yes">
																					<Footer Key=""></Footer>
																					<Header Key="" Caption="·������"></Header>
																				</igtbl:UltraGridColumn>
																				<igtbl:UltraGridColumn HeaderText="��ʼ����ڵ�" Key="" Type="DropDownList" BaseColumnName="FromTaskCode" AllowResize="Free"
																					AllowUpdate="Yes">
																					<Footer Key=""></Footer>
																					<Header Key="" Caption="��ʼ����ڵ�"></Header>
																				</igtbl:UltraGridColumn>
																				<igtbl:UltraGridColumn HeaderText="��������ڵ�" Key="" Type="DropDownList" BaseColumnName="ToTaskCode" AllowResize="Free"
																					AllowUpdate="Yes">
																					<Footer Key=""></Footer>
																					<Header Key="" Caption="��������ڵ�"></Header>
																				</igtbl:UltraGridColumn>
																				<igtbl:UltraGridColumn HeaderText="·�ɴ���" Key="" BaseColumnName="SortID" AllowResize="Free" AllowUpdate="Yes">
																					<Footer Key=""></Footer>
																					<Header Key="" Caption="·�ɴ���"></Header>
																				</igtbl:UltraGridColumn>
																				<igtbl:UltraGridColumn HeaderText="·������" Key="" Width="500px" BaseColumnName="" AllowResize="Free" AllowUpdate="Yes">
																					<Footer Key=""></Footer>
																					<Header Key="" Caption="·������"></Header>
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
																		<asp:BoundColumn DataField="ProcedurePropertyName" HeaderText="����"></asp:BoundColumn>
																	</Columns>
																	<PagerStyle NextPageText="��һҳ&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;��һҳ" HorizontalAlign="Right"
																		CssClass="ListHeadTr"></PagerStyle>
																</asp:datagrid></TD>
														</TR>
													</TABLE>
												</ContentTemplate>
											</igtab:Tab>
											<igtab:Tab Text="- �� ɫ -">
												<ContentTemplate>
													<igtbl:UltraWebGrid id="UltraWebGrid3" runat="server" Height="100%" Width="100%">
														<DisplayLayout JavaScriptFileName="../Images/infragistics/20051/scripts/ig_WebGrid.js" StationaryMargins="HeaderAndFooter"
															AutoGenerateColumns="False" AllowAddNewDefault="Yes" AllowSortingDefault="OnClient" JavaScriptFileNameCommon="../Images/infragistics/20051/Scripts/ig_shared.js"
															RowHeightDefault="20px" Version="4.00" HeaderClickActionDefault="SortSingle" BorderCollapseDefault="Separate"
															Name="UltraWebTab1UltraWebGrid3" TableLayout="Fixed" CellClickActionDefault="Edit" AllowUpdateDefault="Yes">
															<AddNewBox Hidden="False" Prompt="���">
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
																	<igtbl:UltraGridColumn HeaderText="��ɫ����" Key="" BaseColumnName="RoleName" AllowResize="Free" AllowUpdate="Yes">
																		<Footer Key=""></Footer>
																		<Header Key="" Caption="��ɫ����"></Header>
																	</igtbl:UltraGridColumn>
																	<igtbl:UltraGridColumn HeaderText="ȫ��" Key="" Type="CheckBox" DataType="System.Boolean" BaseColumnName=""
																		AllowResize="Free" AllowUpdate="Yes">
																		<Footer Key=""></Footer>
																		<Header Key="" Caption="ȫ��"></Header>
																	</igtbl:UltraGridColumn>
																	<igtbl:UltraGridColumn HeaderText="��ע" Key="" BaseColumnName="Remak" AllowResize="Free" AllowUpdate="Yes">
																		<Footer Key=""></Footer>
																		<Header Key="" Caption="��ע"></Header>
																	</igtbl:UltraGridColumn>
																	<igtbl:UltraGridColumn HeaderText="��ɫ��Ա" Key="" Type="HyperLink" BaseColumnName="" AllowResize="Free" AllowUpdate="No">
																		<Footer Key=""></Footer>
																		<Header Key="" Caption="��ɫ��Ա"></Header>
																	</igtbl:UltraGridColumn>
																	<igtbl:UltraGridColumn HeaderText="ȫ��" Key="" Hidden="True" BaseColumnName="RoleType" AllowResize="Free"
																		AllowUpdate="Yes">
																		<Footer Key=""></Footer>
																		<Header Key="" Caption="ȫ��"></Header>
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
											<igtab:Tab Text="- �� �� -">
												<ContentTemplate>
													<igtbl:UltraWebGrid id="UltraWebGrid4" runat="server" Width="100%" Height="100%">
														<DisplayLayout JavaScriptFileName="../Images/infragistics/20051/scripts/ig_WebGrid.js" StationaryMargins="HeaderAndFooter"
															AutoGenerateColumns="False" AllowAddNewDefault="Yes" AllowSortingDefault="OnClient" JavaScriptFileNameCommon="../Images/infragistics/20051/Scripts/ig_shared.js"
															RowHeightDefault="20px" Version="4.00" HeaderClickActionDefault="SortSingle" BorderCollapseDefault="Separate"
															Name="UltraWebTab1UltraWebGrid4" TableLayout="Fixed" CellClickActionDefault="Edit">
															<AddNewBox Hidden="False" Prompt="���">
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
																	<igtbl:UltraGridColumn HeaderText="��������" Key="" BaseColumnName="ProcedurePropertyName" AllowResize="Free"
																		AllowUpdate="Yes">
																		<Footer Key=""></Footer>
																		<Header Key="" Caption="��������"></Header>
																	</igtbl:UltraGridColumn>
																	<igtbl:UltraGridColumn HeaderText="����" Key="" Type="DropDownList" BaseColumnName="ProcedurePropertyType"
																		AllowResize="Free" AllowUpdate="Yes">
																		<Footer Key=""></Footer>
																		<Header Key="" Caption="����"></Header>
																	</igtbl:UltraGridColumn>
																	<igtbl:UltraGridColumn HeaderText="��ע" Key="" BaseColumnName="Remak" AllowResize="Free" AllowUpdate="Yes">
																		<Footer Key=""></Footer>
																		<Header Key="" Caption="��ע"></Header>
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
											<td align="center"><input class="submit" id="btnSaveProcedure" type="button" value="��������" name="btnSaveProcedure"
													runat="server" onserverclick="btnSaveProcedure_ServerClick"> <input class="submit" id="btnDeleteProcedure" onclick="if(!confirm('�Ƿ�ȷ��ɾ�� ��')) return false; "
													type="button" value="ɾ������" name="btnDeleteProcedure" runat="server" onserverclick="btnDeleteProcedure_ServerClick"> <input class="submit" id="btnClose" onclick="window.close();" type="button" value=" �ر� "
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
			<input id="btnRefresh" style="DISPLAY: none" type="button" value="ˢ��" name="btnRefresh"
				runat="server" onserverclick="btnRefresh_ServerClick"> <input type="button" style="DISPLAY:none" value="ɾ��·��" id="btnDeleteRouter" name="btnDeleteRouter"
				runat="server" onserverclick="btnDeleteRouter_ServerClick"> <input type="button" style="DISPLAY:none" value="ɾ����ɫ" id="btnDeleteRole" name="btnDeleteRole"
				runat="server" onserverclick="btnDeleteRole_ServerClick"> <input type="button" style="DISPLAY:none" value="ɾ������" id="btnDeleteProperty" name="btnDeleteProperty"
				runat="server" onserverclick="btnDeleteProperty_ServerClick"> <input id="btnRoleCompriseSave" style="DISPLAY: none" type="button" value="�����ɫ���" name="btnRoleCompriseSave"
				runat="server" onserverclick="btnRoleCompriseSave_ServerClick"> <input id="RoleCompriseUserCodes" type="hidden" name="RoleCompriseUserCodes" runat="server">
			<input id="RoleCompriseStationCodes" type="hidden" name="RoleCompriseStationCodes" runat="server">
			<input id="RoleCompriseCode" type="hidden" name="RoleCompriseCode" runat="server">
			<input id="DeleteObjectCode" type="hidden" name="DeleteObjectCode" runat="server">
		</form>
		<script language="javascript">
		/*** �������� ***/
		function addNewTask()
		{
			OpenLargeWindow('TaskManager.aspx','��������');
		}
		/*** �޸����� ***/
		function modifyTask(taskCode)
		{
			OpenLargeWindow('TaskManager.aspx?TaskCode=' + taskCode,'�޸�����');
		}
		
		/*** ˢ��ҳ�� ***/
		function RefreshGrid()
		{
			Form1.btnRefresh.click();
		}
		
		/*** �򿪽�ɫ���ҳ�� ***/
		function SelectRoleComprise(RoleCode,UserCodes,StationCodes)
����	{
��			OpenMiddleWindow("../SelectBox/SelectSUMain.aspx?UserCodes="+UserCodes+"&StationCodes="+StationCodes+"&Flag="+RoleCode,null);
����	}
����	
����	/*** ������ɽ�ɫҳ����в��� ***/
����	function getReturnStationUser(userCodes,userNames,stationCodes,stationNames,flag)
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
			//alert("�û����룺"+userCodes+"\n"+"��λ���룺"+stationCodes+"\n"+flag);
			document.Form1.btnRoleCompriseSave.click();
		}
		/*** ��ʽ���ַ��� ***/
		function getString(str1,str2)
		{
			if(str1.length>0&&str2.length>0)
			{
				return str1+','+str2;
			}
			else
				return str1+str2;
		}
		/*** ���¼���Task���� ***/
		function LoadTask()
		{
		    document.Form1.btnRefresh.click();
		}
		/*** ɾ��·�� ***/
		function deleteRouter(RouterCode)
		{
		    document.Form1.DeleteObjectCode.value = RouterCode;
		    document.Form1.btnDeleteRouter.click();
		}
		/*** ɾ����ɫ ***/
		function deleteRole(RoleCode)
		{
		    document.Form1.DeleteObjectCode.value = RoleCode;
		    document.Form1.btnDeleteRole.click();;
		}
		/*** ɾ������ ***/
		function deleteProperty(PropertyCode)
		{
		    document.Form1.DeleteObjectCode.value = PropertyCode;
		    document.Form1.btnDeleteProperty.click();
		    
		}
		
		
		</script>
	</body>
</HTML>
