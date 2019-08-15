<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Page language="c#" Inherits="RmsPM.Web.ConstructProg.BuildingFloorProgressChartB" CodeFile="BuildingFloorProgressChartB.aspx.cs" %>
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
.status_null { TEXT-ALIGN: center }
.status { CURSOR: hand; TEXT-ALIGN: center }
.status0 { BORDER-RIGHT: #a3afb8 0px solid; FONT-SIZE: 12px; BORDER-LEFT: #a3afb8 0px solid; WIDTH: 20px; HEIGHT: 100%; BACKGROUND-COLOR: #ffffff; TEXT-ALIGN: center }
.status1 { BORDER-RIGHT: #a3afb8 0px solid; FONT-SIZE: 12px; BACKGROUND: url(../images/progress_1.gif) repeat-y center top; BORDER-LEFT: #a3afb8 0px solid; WIDTH: 100%; HEIGHT: 100%; TEXT-ALIGN: center }
.status1_up { BORDER-RIGHT: #a3afb8 0px solid; FONT-SIZE: 12px; BACKGROUND: url(../images/progress_1_up.gif) no-repeat center bottom; BORDER-LEFT: #a3afb8 0px solid; WIDTH: 100%; HEIGHT: 100%; TEXT-ALIGN: center }
.status1_down { BORDER-RIGHT: #a3afb8 0px solid; FONT-SIZE: 12px; BACKGROUND: url(../images/progress_1_down.gif) no-repeat center top; BORDER-LEFT: #a3afb8 0px solid; WIDTH: 100%; HEIGHT: 100%; TEXT-ALIGN: center }
.status1_thisweek { BORDER-RIGHT: #a3afb8 0px solid; FONT-SIZE: 12px; BORDER-LEFT: #a3afb8 0px solid; WIDTH: 20px; HEIGHT: 100%; BACKGROUND-COLOR: #d7d7d7; TEXT-ALIGN: center }
.status2 { BORDER-RIGHT: #a3afb8 0px solid; FONT-SIZE: 12px; BORDER-LEFT: #a3afb8 0px solid; WIDTH: 20px; HEIGHT: 100%; BACKGROUND-COLOR: #272727; TEXT-ALIGN: center }
.table_border { BORDER-RIGHT: #a3afb8 1px solid; PADDING-RIGHT: 9px; BORDER-TOP: #a3afb8 1px solid; PADDING-LEFT: 9px; PADDING-BOTTOM: 9px; BORDER-LEFT: #a3afb8 1px solid; PADDING-TOP: 9px; BORDER-BOTTOM: #a3afb8 1px solid }
		</style>
	</HEAD>
	<body style="BORDER-RIGHT: 0px" scroll="no" onload="winload();" onresize="bodyResize();">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" bgcolor="white">
				<tr>
					<TD class="note"><A href="#" onclick="ShowVisualProgressInfo();return false;"><asp:label id="lblVisualProgressName" onmouseover="init(myjoyboxVg, joyboxTableVg, linktitleVg, hint);"
								onmouseout="mouseend();" Runat="server"></asp:label></A>阶段当前工程图</TD>
				</tr>
				<tr height="100%">
					<td align="left">
						<div style="OVERFLOW:auto;WIDTH:100%;HEIGHT:100%" id="divMain">
							<table height="100%" cellSpacing="0" cellPadding="0" width="100%">
								<tr>
									<td vAlign="bottom">
										<table cellSpacing="0" cellPadding="0" border="0">
											<tr>
												<asp:repeater id="dgBuilding" Runat="server">
													<ItemTemplate>
														<td valign="bottom" style="padding-right:9px">
															<input type="hidden" id="txtBuildingCodeDtl" name="txtBuildingCodeDtl" runat="server" value='<%# DataBinder.Eval(Container, "DataItem.BuildingCode") %>'>
																<table id="tbList" cellSpacing="0" cellPadding="0" border="0" width="100%" class="list">
																	<tr class="list-title">
																		<td align="center" colspan='2'><%# DataBinder.Eval(Container, "DataItem.BuildingName") %></td>
																	</tr>
																	<tr class="list-title">
																		<td width="120"></td>
																		<td width="120" align="center">当前工程</td>
																	</tr>
																	<asp:repeater id="dgList" runat="server">
																		<ItemTemplate>
																			<tr>
																				<td align="center"><%# DataBinder.Eval(Container, "DataItem.FloorName") %></td>
																				<td class='<%# RmsPM.BLL.ConvertRule.ToString(DataBinder.Eval(Container, "DataItem.WBSCode"))==""?"status_null":"status" %>'
																				BuildingFloorCode='<%# DataBinder.Eval(Container, "DataItem.BuildingFloorCode") %>'
																				WBSCode='<%# DataBinder.Eval(Container, "DataItem.WBSCode") %>'
																				hint='<%# DataBinder.Eval(Container, "DataItem.Hint") %>' 
																				onMouseOver="init(myjoybox, joyboxTable, linktitle, hint);" 
																				onMouseOut="mouseend();" 
																				onclick="Modify(this.BuildingFloorCode, this.WBSCode);"
																				><%# DataBinder.Eval(Container, "DataItem.Html") %>
																				</td>
																			</tr>
																		</ItemTemplate>
																	</asp:repeater>
																</table>
														</td>
													</ItemTemplate>
												</asp:repeater></tr>
										</table>
									</td>
								</tr>
							</table>
						</div>
					</td>
				</tr>
			</table>
			<div id="myjoybox" style="DISPLAY: none; LEFT: 10px; POSITION: absolute; TOP: 200px; HEIGHT: 170px"
				myOffsetTop="0" myOffsetRight="40" myOffsetBottom="0" myDiv="">
				<table id="joyboxTable" height="170" cellSpacing="0" cellPadding="0" width="180" border="0">
					<tbody>
						<tr>
							<td width="8%" bgColor="#ffffcc">
							<td width="92%" bgColor="#ffffcc"><label id="linktitle"></label></td>
						</tr>
					</tbody>
				</table>
			</div>
			<div id="myjoyboxVg" style="DISPLAY: none; Z-INDEX: 2; LEFT: 10px; POSITION: absolute; TOP: 200px; HEIGHT: 150px"
				myDiv="divMain" myOffsetBottom="90" myOffsetRight="40" myOffsetTop="0px">
				<table id="joyboxTableVg" height="150" cellSpacing="0" cellPadding="0" width="180" border="0">
					<tbody>
						<tr>
							<td width="8%" bgColor="#ffffcc">
							<td width="92%" bgColor="#ffffcc"><label id="linktitleVg"></label></td>
						</tr>
					</tbody>
				</table>
			</div>
			<div id="legend" style="BORDER-RIGHT: #a3afb8 1px solid; BORDER-TOP: #a3afb8 1px solid; Z-INDEX: 1; LEFT: 5px; BORDER-LEFT: #a3afb8 1px solid; BORDER-BOTTOM: #a3afb8 1px solid; POSITION: absolute; TOP: 20px; BACKGROUND-COLOR: #ffffff; layer-background-color: #6699CC">
				<table cellSpacing="5" cellPadding="0" border="0" width="120" height="80">
					<tr>
						<td noWrap align="center" colSpan="4">图 例</td>
					</tr>
					<asp:repeater id="dgLegend" runat="server">
						<ItemTemplate>
							<tr>
								<td noWrap width="5">&nbsp;</td>
								<td noWrap><img src='../images/<%# DataBinder.Eval(Container.DataItem, "ImageFileName") %>'></td>
								<td noWrap><a style="cursor:hand" onclick="ShowWBS(this.val);" val='<%# DataBinder.Eval(Container.DataItem, "WBSCode") %>'
									hint='<%# DataBinder.Eval(Container.DataItem, "hint") %>'
									onmouseover="init(myjoyboxVg, joyboxTableVg, linktitleVg, hint);"
									onmouseout="mouseend();"
									>
										<%# DataBinder.Eval(Container.DataItem, "TaskName") %>
									</a>
								</td>
								<td width="5"></td>
							</tr>
						</ItemTemplate>
					</asp:repeater></table>
			</div>
			<input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server"> <input id="txtBuildingCode" type="hidden" name="txtBuildingCode" runat="server">
			<input id="txtVisualProgress" type="hidden" name="txtVisualProgress" runat="server"><input id="txtMulti" type="hidden" name="txtMulti" runat="server">
		</form>
		<SCRIPT language="javascript">
<!--

var CurrUrl = window.location.href;

//修改进度
function Modify(BuildingFloorCode, WBSCode)
{
	if (WBSCode == "") return;
	
	var VisualProgressCode = Form1.txtVisualProgress.value;
	OpenCustomWindow("../ConstructProg/BuildingFloorProgressModify.aspx?BuildingFloorCode=" + BuildingFloorCode + "&WBSCode=" + WBSCode + "&VisualProgressCode=" + VisualProgressCode, "楼栋进度修改", 500, 300);
}

//显示形象进度工作
function ShowVisualProgressInfo()
{
	var WBSCode = Form1.txtVisualProgress.value;
	OpenFullWindow("../Project/WBSInfo.aspx?WBSCode="+WBSCode,"");
}

//显示分项工作
function ShowWBS(WBSCode)
{
	if (WBSCode == "") return;
	OpenFullWindow("../Project/WBSInfo.aspx?WBSCode="+WBSCode,"");
}

function winload()
{
	ChangeLegendLocation();		
}

function ChangeLegendLocation()
{
	var legend = document.all.legend;
	legend.style.left = document.body.offsetWidth - 150 - 5;
	legend.style.top = document.body.offsetTop + 100;
}

function bodyResize()
{
	ChangeLegendLocation();		
}

//-->
		</SCRIPT>
	</body>
</HTML>
