<%@ Page language="c#" Inherits="RmsPM.Web.ConstructProg.ProjectProgressChartEx" CodeFile="ProjectProgressChartEx.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>楼栋工程进度</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<SCRIPT language="javascript" src="../images/JoyBox.js"></SCRIPT>
		<style>
.yline_arrow { background: url(../images/yline_arrow.gif) no-repeat center bottom; FONT-SIZE: 12px; TEXT-ALIGN: right }
.yline { background: url(../images/yline.gif) repeat-y center top; FONT-SIZE: 12px; TEXT-ALIGN: right; }

.xline_arrow { background: url(../images/xline_arrow.gif) no-repeat top left; FONT-SIZE: 12px; TEXT-ALIGN: right }
.xline { background: url(../images/xline.gif) repeat-x center top; FONT-SIZE: 12px; TEXT-ALIGN: center }
.xline_year {FONT-SIZE: 12px; TEXT-ALIGN: center }

.xy { background: url(../images/xy.gif) no-repeat center top; FONT-SIZE: 12px; TEXT-ALIGN: right; }

.xlineYear { BORDER-RIGHT: #a3afb8 1px solid; TEXT-ALIGN: center }
.tdline { BORDER-TOP: #d3dfe8 1px solid; FONT-SIZE: 1px; HEIGHT: 13px }
.tdline2 { FONT-SIZE: 1px; HEIGHT: 13px }
.xcolhead { FONT-SIZE: 1px }

.ycaption {height:30px;FONT-SIZE: 12px; TEXT-ALIGN: right }
.xcaption {FONT-SIZE: 9px; TEXT-ALIGN: center; BORDER-RIGHT: #a3afb8 1px solid }
		</style>
	</HEAD>
	<body style="BORDER-RIGHT: 0px" scroll="no">
		<form id="Form1" method="post" runat="server">
			<table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0">
				<tr>
					<td>
						<div style="overflow:auto;width:100%;height:100%;position:absolute">
							<table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0">
								<tr>
									<td></td>
									<td class="yline_arrow">&nbsp;&nbsp;</td>
									<td></td>
									<td></td>
								</tr>
								<tr>
									<td nowrap>
										<asp:repeater id="dgList" runat="server">
											<ItemTemplate>
												<div class="ycaption"><%# DataBinder.Eval(Container, "DataItem.TaskName") %></div>
											</ItemTemplate>
										</asp:repeater>
									</td>
									<td class="yline">&nbsp;&nbsp;</td>
									<td width="100%">
									</td>
									<td></td>
								</tr>
								<tr>
									<td></td>
									<td class="xy">&nbsp;</td>
									<td class="xline" nowrap>
										<asp:repeater id="dgX2" runat="server">
											<ItemTemplate>
												<span class="xcaption" style="width:20px"><%# DataBinder.Eval(Container, "DataItem.month") %></span>
											</ItemTemplate>
										</asp:repeater>
									</td>
									<td class="xline_arrow">&nbsp;&nbsp;&nbsp;&nbsp;</td>
								</tr>
								<tr>
									<td></td>
									<td></td>
									<td class="xline_year" nowrap>
										<asp:repeater id="dgXYear" runat="server">
											<ItemTemplate>
												<div class="xcaption" style='position:relative;width:<%# 20 * RmsPM.BLL.ConvertRule.ToInt(DataBinder.Eval(Container, "DataItem.MonthCount")) %>'><%# DataBinder.Eval(Container, "DataItem.Year") %>;<%# 20 * RmsPM.BLL.ConvertRule.ToInt(DataBinder.Eval(Container, "DataItem.MonthCount"))%></div>
											</ItemTemplate>
										</asp:repeater>
									</td>
									<td></td>
								</tr>
								<tr>
									<td></td>
									<td></td>
									<td><div style="position:absolute;width:180;border:#a3afb8 1px solid"></div>
									<div id="div1" style="BORDER-RIGHT: #a3afb8 1px solid; BORDER-TOP: #a3afb8 1px solid; LEFT: 181px; BORDER-LEFT: #a3afb8 1px solid; WIDTH: 180px; BORDER-BOTTOM: #a3afb8 1px solid; POSITION: relative"></div>
									<div id="div2" style="BORDER-RIGHT: #a3afb8 1px solid; BORDER-TOP: #a3afb8 1px solid; LEFT: 181px; BORDER-LEFT: #a3afb8 1px solid; WIDTH: 180px; BORDER-BOTTOM: #a3afb8 1px solid; POSITION: relative"></div>
									</td>
									<td></td>
								</tr>
							</table>
						</div>
					</td>
				</tr>
			</table>
		<div style="display:none">
															<asp:repeater id="dgX" runat="server">
																<ItemTemplate>
																	<td align="center" class="xcolhead"></td>
																</ItemTemplate>
															</asp:repeater>
														<asp:repeater id="dgList1" runat="server">
															<ItemTemplate>
																<tr>
																	<td valign="center" nowrap rowspan="2" class="yline">
																		<a style='display:<%# RmsPM.BLL.ConvertRule.ToInt(DataBinder.Eval(Container, "DataItem.ChildNodesCount"))==0?"none":"" %>' href="#" onclick="GotoTask(this.val);" val='<%# DataBinder.Eval(Container, "DataItem.WBSCode") %>'>
																			<%# DataBinder.Eval(Container, "DataItem.TaskName") %>
																			(<%# DataBinder.Eval(Container, "DataItem.CompletePercent") %>%)</a> <span style='display:<%# RmsPM.BLL.ConvertRule.ToInt(DataBinder.Eval(Container, "DataItem.ChildNodesCount"))==0?"":"none" %>'>
																			<%# DataBinder.Eval(Container, "DataItem.TaskName") %>
																			(<%# DataBinder.Eval(Container, "DataItem.CompletePercent") %>%)</span>
																	</td>
																	<td colspan='<%# DataBinder.Eval(Container, "DataItem.PLeftMargin") %>' class="tdline">&nbsp;</td>
																	<td valign="bottom" colspan='<%# DataBinder.Eval(Container, "DataItem.PMonths") %>' align="center" class="tdline" onclick="ShowWBS(this.val);" val='<%# DataBinder.Eval(Container, "DataItem.WBSCode") %>' style="cursor:hand">
																		<table width="100%" border="0" cellpadding="0" cellspacing="0" style='display:<%# DataBinder.Eval(Container, "DataItem.PBarDisplay") %>'>
																			<tr>
																				<td style="FONT-SIZE:1px;height:5px;BACKGROUND-COLOR:#76d769" hint='<%# DataBinder.Eval(Container, "DataItem.PHint") %>' onMouseOver="init(myjoybox, joyboxTable, linktitle, hint);" onMouseOut="mouseend();">&nbsp;</td>
																			</tr>
																		</table>
																		<img src="../images/point_green.gif" style='display:<%# DataBinder.Eval(Container, "DataItem.PPointDisplay") %>' hint='<%# DataBinder.Eval(Container, "DataItem.PHint") %>' onMouseOver="init(myjoybox, joyboxTable, linktitle, hint);" onMouseOut="mouseend();">
																	</td>
																	<td colspan='<%# DataBinder.Eval(Container, "DataItem.PRightMargin") %>' class="tdline">&nbsp;</td>
																</tr>
																<tr>
																	<td colspan='<%# DataBinder.Eval(Container, "DataItem.LeftMargin") %>' class="tdline2">&nbsp;</td>
																	<td valign="top" colspan='<%# DataBinder.Eval(Container, "DataItem.Months") %>' align="center" class="tdline2" onclick="ShowWBS(this.val);" val='<%# DataBinder.Eval(Container, "DataItem.WBSCode") %>' style="cursor:hand">
																		<table width="100%" border="0" cellpadding="0" cellspacing="0" style='display:<%# DataBinder.Eval(Container, "DataItem.BarDisplay") %>'>
																			<tr>
																				<td style="FONT-SIZE:1px;height:5px;BACKGROUND-COLOR:#ff0000" hint='<%# DataBinder.Eval(Container, "DataItem.Hint") %>' onMouseOver="init(myjoybox, joyboxTable, linktitle, hint);" onMouseOut="mouseend();">&nbsp;</td>
																			</tr>
																		</table>
																		<img src="../images/point_red.gif" style='display:<%# DataBinder.Eval(Container, "DataItem.PointDisplay") %>' hint='<%# DataBinder.Eval(Container, "DataItem.Hint") %>' onMouseOver="init(myjoybox, joyboxTable, linktitle, hint);" onMouseOut="mouseend();">
																	</td>
																	<td colspan='<%# DataBinder.Eval(Container, "DataItem.RightMargin") %>' class="tdline2">&nbsp;</td>
																</tr>
															</ItemTemplate>
														</asp:repeater>
															</div>
			<input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server"> <input id="txtWBSCode" type="hidden" name="txtWBSCode" runat="server">
		</form>
		<SCRIPT language="javascript">
<!--

var CurrUrl = window.location.href;

function ShowWBS(WBSCode)
{
	OpenFullWindow("../Project/WBSInfo.aspx?WBSCode="+WBSCode,"");
}

function GotoTask(WBSCode)
{
	window.parent.GotoTask(WBSCode);
}

//-->
		</SCRIPT>
	</body>
</HTML>
