<%@ Page language="c#" Inherits="RmsPM.Web.PBS.PBSUnitInfo" CodeFile="PBSUnitInfo.aspx.cs" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>单位工程</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../PBS/PBSUnitNav.js"></SCRIPT>
		<SCRIPT language="javascript" src="map.js"></SCRIPT>
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
	</HEAD>
	<body scroll="no" style="BORDER-RIGHT:0px">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TBODY>
					<TR id="trToolBar" runat="server">
						<TD class="tools-area" valign="top">
							<IMG src="../images/btn_li.gif" align="absMiddle"> <input class="button" type="button" value="修 改" name="btnModify" id="btnModify" runat="server"
								onclick="Modify();"> <input class="button" type="button" value="删 除" name="btnDelete" id="btnDelete" runat="server"
								onclick="javascript:if(!window.confirm('确实要删除这条记录吗？')) return false;" onserverclick="btnDelete_ServerClick"> <input class="button" type="button" value="显示单位工程" name="btnShowPBSUnitWindow" id="btnShowPBSUnitWindow"
								runat="server" onclick="parent.OpenPBSUnitWindow();" style="DISPLAY:none"> <IMG src="../images/btn_li.gif" align="absMiddle">
							<input class="button" type="button" value="切换到工程计划" name="btnGotoPlan" id="btnGotoPlan"
								runat="server" onclick="GotoPlan();">
						</TD>
					</TR>
					<tr height="100%">
						<td class="table" valign="top">
							<table width="100%" border="0" cellpadding="0" cellspacing="0" class="tab-aera">
								<tr>
									<td>
										<table width="100%" border="0" cellpadding="0" cellspacing="6">
											<tr>
												<td style="BORDER-RIGHT: #ededed 3px dotted; PADDING-RIGHT: 7px" vAlign="top">
													<TABLE class="form" id="Table4" cellSpacing="0" cellPadding="0" width="100%">
														<TBODY>
															<TR>
																<TD class="form-item" width="15%">单位工程名称：</TD>
																<TD colspan="3"><asp:Label Runat="server" ID="lblPBSUnitName"></asp:Label></TD>
															</TR>
															<tr>
																<TD class="form-item">形象进度：</TD>
																<TD colspan="3"><asp:Label Runat="server" ID="lblVisualProgress"></asp:Label></TD>
															</tr>
															<TR>
																<!--																<TD class="form-item">建设单位：</TD>
																<TD><asp:Label Runat="server" ID="lblDevelopUnit"></asp:Label></TD>-->
																<TD class="form-item">施工单位：</TD>
																<TD colspan="3"><asp:Label Runat="server" ID="lblConstructUnit"></asp:Label></TD>
															</TR>
															<tr>
																<TD class="form-item">负 责 人：</TD>
																<TD colspan="3"><asp:Label Runat="server" ID="lblManagerName"></asp:Label></TD>
															</tr>
															<TR>
																<TD class="form-item" nowrap>计划投资总额：</TD>
																<TD width="30%"><asp:Label Runat="server" ID="lblPInvest"></asp:Label></TD>
																<TD class="form-item" nowrap>实际投资总额：</TD>
																<TD width="30%"><asp:Label Runat="server" ID="lblInvest"></asp:Label></TD>
															</TR>
															<TR>
																<TD class="form-item">计划开工时间：</TD>
																<TD><asp:Label Runat="server" ID="lblPStartDate"></asp:Label></TD>
																<TD class="form-item">计划竣工时间：</TD>
																<TD><asp:Label Runat="server" ID="lblPEndDate"></asp:Label></TD>
															</TR>
															<TR>
																<TD class="form-item">实际开工时间：</TD>
																<TD><asp:Label Runat="server" ID="lblStartDate"></asp:Label></TD>
																<TD class="form-item">实际竣工时间：</TD>
																<TD><asp:Label Runat="server" ID="lblEndDate"></asp:Label></TD>
															</TR>
															<TR>
																<TD class="form-item">财务代码：</TD>
																<TD colspan="3"><asp:Label Runat="server" ID="lblUFCode"></asp:Label></TD>
															</TR>
															<TR>
																<TD class="form-item">备注：</TD>
																<TD colSpan="3"><asp:Label Runat="server" ID="lblRemark"></asp:Label></TD>
															</TR>
														</TBODY>
													</TABLE>
												</td>
												<td>
													<table border="0" cellpadding="0" cellspacing="0">
														<tr>
															<td class="intopic">楼栋列表</td>
														</tr>
													</table>
													<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
														<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
															<tr vAlign="top">
																<td><asp:datagrid id="dgList" runat="server" DataKeyField="BuildingCode" Width="100%" CssClass="list"
																		CellPadding="0" AllowSorting="True" AutoGenerateColumns="False" PageSize="15" ShowFooter="True">
																		<HeaderStyle CssClass="list-title"></HeaderStyle>
																		<FooterStyle CssClass="list-title"></FooterStyle>
																		<Columns>
																			<asp:TemplateColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" HeaderText="楼栋名称" FooterText="合计">
																				<HeaderStyle Wrap="False"></HeaderStyle>
																				<ItemStyle Wrap="False"></ItemStyle>
																				<ItemTemplate>
																					<a href="#" onclick="OpenBuildingInfo(this.Code);return false;" Code='<%#  DataBinder.Eval(Container.DataItem, "BuildingCode") %>'><%#  DataBinder.Eval(Container.DataItem, "BuildingName") %></a>
																				</ItemTemplate>
																			</asp:TemplateColumn>
																			<asp:TemplateColumn HeaderText="建筑面积(平米)">
																				<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
																				<ItemStyle HorizontalAlign="Right"></ItemStyle>
																				<FooterStyle HorizontalAlign="Right"></FooterStyle>
																				<ItemTemplate>
																					<%# RmsPM.BLL.StringRule.BuildShowNumberString(DataBinder.Eval(Container.DataItem, "Area")) %>
																				</ItemTemplate>
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
										</table>
									</td>
								</tr>
							</table>
						</td>
					</tr>
				</TBODY>
			</table>
			<input type="hidden" id="txtPBSUnitCode" name="txtPBSUnitCode" runat="server"> <input type="hidden" id="txtFromUrl" name="txtFromUrl" runat="server">
			<input type="hidden" id="txtProjectCode" name="txtProjectCode" runat="server"><input type="hidden" id="txtAct" name="txtAct" runat="server">
		</form>
		<SCRIPT language="javascript">
<!--

var CurrUrl = window.location.href;

function GoBack()
{
	window.location.href = Form1.txtFromUrl.value;
}

function Modify()
{
	OpenCustomWindow("PBSUnitModify.aspx?FromUrl=" + escape(CurrUrl) + "&PBSUnitCode=" + Form1.txtPBSUnitCode.value, "修改单位工程", 760, 500);
//	window.location.href = "PBSUnitModify.aspx?FromUrl=" + escape(CurrUrl) + "&PBSUnitCode=" + Form1.txtPBSUnitCode.value;
}

//查看工程计划
function GotoPlan()
{
	window.parent.location.href = "../Construct/ConstructPlan.aspx?FromUrl=" + escape(CurrUrl) + "&PBSUnitCode=" + Form1.txtPBSUnitCode.value + "&ProjectCode=" + Form1.txtProjectCode.value;
}
//-->
		</SCRIPT>
	</body>
</HTML>
