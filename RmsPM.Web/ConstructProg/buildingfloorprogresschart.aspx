<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Page language="c#" Inherits="RmsPM.Web.ConstructProg.BuildingFloorProgressChart" CodeFile="BuildingFloorProgressChart.aspx.cs" %>
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
		<style>.status {
	CURSOR: hand; TEXT-ALIGN: center
}
.status0 {
	BORDER-RIGHT: #a3afb8 0px solid; FONT-SIZE: 12px; BORDER-LEFT: #a3afb8 0px solid; WIDTH: 20px; HEIGHT: 100%; BACKGROUND-COLOR: #ffffff; TEXT-ALIGN: center
}
.status1 {
	BORDER-RIGHT: #a3afb8 0px solid; FONT-SIZE: 12px; BORDER-LEFT: #a3afb8 0px solid; WIDTH: 100%; HEIGHT: 100%; TEXT-ALIGN: center
}
.status1_ {
	BORDER-RIGHT: #a3afb8 0px solid; FONT-SIZE: 12px; BACKGROUND: url(../images/progress_1.gif) repeat-y center top; BORDER-LEFT: #a3afb8 0px solid; WIDTH: 100%; HEIGHT: 100%; TEXT-ALIGN: center
}
.status1_up {
	BORDER-RIGHT: #a3afb8 0px solid; FONT-SIZE: 12px; BACKGROUND: url(../images/progress_1_up.gif) no-repeat center bottom; BORDER-LEFT: #a3afb8 0px solid; WIDTH: 100%; HEIGHT: 100%; TEXT-ALIGN: center
}
.status1_down {
	BORDER-RIGHT: #a3afb8 0px solid; FONT-SIZE: 12px; BACKGROUND: url(../images/progress_1_down.gif) no-repeat center top; BORDER-LEFT: #a3afb8 0px solid; WIDTH: 100%; HEIGHT: 100%; TEXT-ALIGN: center
}
.status3 {
	BORDER-RIGHT: #a3afb8 0px solid; FONT-SIZE: 12px; BORDER-LEFT: #a3afb8 0px solid; WIDTH: 20px; HEIGHT: 100%; TEXT-ALIGN: center
}
.status1_thisweek_ {
	BORDER-RIGHT: #a3afb8 0px solid; FONT-SIZE: 12px; BORDER-LEFT: #a3afb8 0px solid; WIDTH: 20px; HEIGHT: 100%; BACKGROUND-COLOR: #d7d7d7; TEXT-ALIGN: center
}
.status2 {
	BORDER-RIGHT: #a3afb8 0px solid; FONT-SIZE: 12px; BORDER-LEFT: #a3afb8 0px solid; WIDTH: 20px; HEIGHT: 100%; TEXT-ALIGN: center
}
.status2_ {
	BORDER-RIGHT: #a3afb8 0px solid; FONT-SIZE: 12px; BORDER-LEFT: #a3afb8 0px solid; WIDTH: 20px; HEIGHT: 100%; BACKGROUND-COLOR: #272727; TEXT-ALIGN: center
}
.table_border {
	BORDER-RIGHT: #a3afb8 1px solid; PADDING-RIGHT: 9px; BORDER-TOP: #a3afb8 1px solid; PADDING-LEFT: 9px; PADDING-BOTTOM: 9px; BORDER-LEFT: #a3afb8 1px solid; PADDING-TOP: 9px; BORDER-BOTTOM: #a3afb8 1px solid
}
		</style>
	</HEAD>
	<body style="BORDER-RIGHT: 0px" scroll="no">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" bgColor="white">
				<tr>
					<TD class="note"><A href="#" onclick="ShowVisualProgressInfo();return false;"><asp:label id="lblVisualProgressName" onmouseover="init(myjoyboxVg, joyboxTableVg, linktitleVg, hint);"
								onmouseout="mouseend();" Runat="server"></asp:label></A>阶段分项工程图</TD>
				</tr>
				<tr height="100%">
					<td>
						<div id="divMain" style="OVERFLOW: auto; WIDTH: 100%; POSITION: absolute; HEIGHT: 100%">
							<table height="100%" cellSpacing="0" cellPadding="0" width="100%">
								<tr>
									<td vAlign="top">
										<table cellSpacing="0" cellPadding="0" border="0">
											<tr>
												<asp:repeater id="dgBuilding" Runat="server">
													<ItemTemplate>
														<td valign=bottom style="padding-right:9px">
															<input type="hidden" id="txtBuildingCodeDtl" name="txtBuildingCodeDtl" runat="server" value='<%# DataBinder.Eval(Container, "DataItem.BuildingCode") %>'>
															<table id="tbList" cellSpacing="0" cellPadding="0" border="0" class="list">
																<tr class="list-title">
																	<td align="center" colspan='<%# RmsPM.BLL.ConvertRule.ToInt(DataBinder.Eval(Container, "DataItem.ColSpan")) + 1 %>'><%# DataBinder.Eval(Container, "DataItem.BuildingName") %></td>
																</tr>
																<tr class="list-title">
																	<td rowspan="2"></td>
																	<asp:repeater id="dgListTitle" runat="server">
																		<ItemTemplate>
																			<td align="center" nowrap colspan='<%# DataBinder.Eval(Container, "DataItem.ColSpan") %>' rowspan='<%# DataBinder.Eval(Container, "DataItem.RowSpan") %>'><a style="cursor:hand" onclick="ShowWBS(this.val);" val='<%# DataBinder.Eval(Container, "DataItem.WBSCode") %>'  hint='<%# DataBinder.Eval(Container, "DataItem.TaskHintHtml") %>' onMouseOver="init(myjoybox, joyboxTable, linktitle, hint);" onMouseOut="mouseend();"><%# DataBinder.Eval(Container, "DataItem.TaskName") %></a></td>
																		</ItemTemplate>
																	</asp:repeater>
																</tr>
																<tr class="list-title">
																	<asp:repeater id="dgListTitle2" runat="server">
																		<ItemTemplate>
																			<td align="center" nowrap colspan='<%# DataBinder.Eval(Container, "DataItem.ColSpan") %>' rowspan='<%# DataBinder.Eval(Container, "DataItem.RowSpan") %>'><a style="cursor:hand" onclick="ShowWBS(this.val);" val='<%# DataBinder.Eval(Container, "DataItem.WBSCode") %>'  hint='<%# DataBinder.Eval(Container, "DataItem.TaskHintHtml") %>' onMouseOver="init(myjoybox, joyboxTable, linktitle, hint);" onMouseOut="mouseend();"><%# DataBinder.Eval(Container, "DataItem.TaskName") %></a></td>
																		</ItemTemplate>
																	</asp:repeater>
																</tr>
																<asp:repeater id="dgList" runat="server">
																	<ItemTemplate>
																		<tr>
																			<td align="center" nowrap><%# DataBinder.Eval(Container, "DataItem.FloorName") %></td>
																			<%# DataBinder.Eval(Container, "DataItem.Html") %>
																		</tr>
																	</ItemTemplate>
																</asp:repeater>
															</table>
														</td>
													</ItemTemplate>
												</asp:repeater></tr>
										</table>
										<div id="legend" style="BORDER-RIGHT: #a3afb8 1px solid; BORDER-TOP: #a3afb8 1px solid; BORDER-LEFT: #a3afb8 1px solid; WIDTH: 350px; BORDER-BOTTOM: #a3afb8 1px solid; BACKGROUND-COLOR: #ffffff; layer-background-color: #6699CC">
											<table cellSpacing="0" cellPadding="9" border="0">
												<tr align="center">
													<td noWrap>图例：</td>
													<td><IMG src="../images/progress_2.gif" align="absMiddle">&nbsp;&nbsp;已完成&nbsp;&nbsp;&nbsp;&nbsp;</td>
													<td><IMG src="../images/progress_3.gif" align="absMiddle">&nbsp;&nbsp;本周完成 
														&nbsp;&nbsp;&nbsp;&nbsp;</td>
													<td><IMG src="../images/progress_1.gif" align="absMiddle">&nbsp;&nbsp;进行中</td>
												</tr>
											</table>
										</div>
									</td>
								</tr>
							</table>
						</div>
					</td>
				</tr>
			</table>
			<div id="myjoybox" style="DISPLAY: none; LEFT: 10px; POSITION: absolute; TOP: 200px; HEIGHT: 170px"
				myDiv="" myOffsetBottom="0" myOffsetRight="40" myOffsetTop="0">
				<table id="joyboxTable" height="170" cellSpacing="0" cellPadding="0" width="180" border="0">
					<tbody>
						<tr>
							<td width="8%" bgColor="#ffffcc">
							<td width="92%" bgColor="#ffffcc"><label id="linktitle"></label></td>
						</tr>
					</tbody>
				</table>
			</div>
			<div id="myjoyboxVg" style="DISPLAY: none; LEFT: 10px; POSITION: absolute; TOP: 200px; HEIGHT: 150px"
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
			<input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server"> <input id="txtBuildingCode" type="hidden" name="txtBuildingCode" runat="server">
			<input id="txtVisualProgress" type="hidden" name="txtVisualProgress" runat="server"><input id="txtMulti" type="hidden" name="txtMulti" runat="server">
		</form>
		<SCRIPT language="javascript">
<!--

var CurrUrl = window.location.href;

//修改进度
function Modify(BuildingFloorCode, WBSCode, VisualProgressCode)
{
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
	OpenFullWindow("../Project/WBSInfo.aspx?WBSCode="+WBSCode,"");
}

//-->
		</SCRIPT>
	</body>
</HTML>
