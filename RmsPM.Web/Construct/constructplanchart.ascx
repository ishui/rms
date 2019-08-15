<%@ Control Language="c#" Inherits="RmsPM.Web.Construct.ConstructPlanChart" CodeFile="ConstructPlanChart.ascx.cs" %>
<style>
.yline { BORDER-RIGHT: #a3afb8 1px solid; FONT-SIZE: 12px; TEXT-ALIGN: right }
.xline { BORDER-RIGHT: #a3afb8 1px solid; BORDER-TOP: #a3afb8 1px solid; TEXT-ALIGN: center }
.xlineYear { BORDER-RIGHT: #a3afb8 1px solid; TEXT-ALIGN: center }
.tdline { BORDER-TOP: #d3dfe8 1px solid; FONT-SIZE: 1px; HEIGHT: 13px }
.tdline2 { FONT-SIZE: 1px; HEIGHT: 13px }
.xcolhead { FONT-SIZE: 1px }
</style>
<table height="100%" cellSpacing="0" cellPadding="0" width="100%" bgcolor="white">
	<tr>
		<td class="intopic" width="200">工程形象进度</td>
	</tr>
	<tr height="100%">
		<td>
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" style="BORDER-RIGHT:#a3afb8 1px solid; PADDING-RIGHT:9px; BORDER-TOP:#a3afb8 1px solid; PADDING-LEFT:9px; PADDING-BOTTOM:9px; BORDER-LEFT:#a3afb8 1px solid; PADDING-TOP:9px; BORDER-BOTTOM:#a3afb8 1px solid">
				<tr>
					<td>
						<div style="OVERFLOW: auto; WIDTH: 100%; POSITION: absolute; HEIGHT: 100%" id="divMain">
							<table id="tbList" cellSpacing="0" cellPadding="0" border="0" width="100%">
								<tr>
									<td class="xcolhead" width="1%">&nbsp;</td>
									<td width="1" class="xcolhead">&nbsp;</td>
									<asp:repeater id="dgX" runat="server">
										<ItemTemplate>
											<td align="center" class="xcolhead"></td>
										</ItemTemplate>
									</asp:repeater>
									<td width="1" class="xcolhead">&nbsp;</td>
								</tr>
								<asp:repeater id="dgList" runat="server">
									<ItemTemplate>
										<tr>
											<td valign="center" nowrap rowspan="2" class="yline"><%# DataBinder.Eval(Container, "DataItem.VisualProgressName") %></td>
											<td colspan='<%# DataBinder.Eval(Container, "DataItem.PLeftMargin") %>' class="tdline">&nbsp;</td>
											<td valign="bottom" colspan='<%# DataBinder.Eval(Container, "DataItem.PMonths") %>' align="center" class="tdline">
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
											<td valign="top" colspan='<%# DataBinder.Eval(Container, "DataItem.Months") %>' align="center" class="tdline2">
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
								<tr>
									<td>&nbsp;</td>
									<td width="1" class="xline">&nbsp;</td>
									<asp:repeater id="dgX2" runat="server">
										<ItemTemplate>
											<td class="xline"><%# DataBinder.Eval(Container, "DataItem.month") %></td>
										</ItemTemplate>
									</asp:repeater>
									<td width="1">&nbsp;</td>
								</tr>
								<tr>
									<td>&nbsp;</td>
									<td width="1" class="xlineYear">&nbsp;</td>
									<asp:repeater id="dgXYear" runat="server">
										<ItemTemplate>
											<td class="xlineYear" colspan='<%# DataBinder.Eval(Container, "DataItem.MonthCount") %>'><%# DataBinder.Eval(Container, "DataItem.Year") %></td>
										</ItemTemplate>
									</asp:repeater>
									<td width="1">&nbsp;</td>
								</tr>
							</table>
							<table cellSpacing="0" cellPadding="0" width="100%" border="0">
								<tr align="center">
									<td noWrap><span style="FONT-SIZE: 1px; WIDTH: 50px; HEIGHT: 5px; BACKGROUND-COLOR: #76d769"></span>&nbsp;&nbsp;计划&nbsp;&nbsp;&nbsp;&nbsp;
										<span style="FONT-SIZE: 1px; WIDTH: 50px; HEIGHT: 5px; BACKGROUND-COLOR: red"></span>
										&nbsp;&nbsp;实际
									</td>
								</tr>
							</table>
						</div>
					</td>
				</tr>
			</table>
			<div id="myjoybox" style="DISPLAY: none; LEFT: 10px; POSITION: absolute; TOP: 200px; HEIGHT: 45px"
				myOffsetTop="0px">
				<table id="joyboxTable" height="45" cellSpacing="0" cellPadding="0" width="180" border="0">
					<tbody>
						<tr>
							<td width="8%" bgColor="#ffffcc">
							<td width="92%" bgColor="#ffffcc"><label id="linktitle"></label></td>
						</tr>
					</tbody>
				</table>
			</div>
			<input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server"> <input id="txtPBSUnitCode" type="hidden" name="txtPBSUnitCode" runat="server">
			<input id="txtYear" type="hidden" name="txtYear" runat="server"> <input id="txtAnnualPlanCode" type="hidden" name="txtAnnualPlanCode" runat="server">
			<SCRIPT language="javascript" src="../images/JoyBox.js"></SCRIPT>
	</tr>
</table>
