<%@ Page language="c#" Inherits="RmsPM.Web.ConstructProg.GroundWorkChart" CodeFile="GroundWorkChart.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>»ù´¡¹¤³Ì</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<SCRIPT language="javascript" src="../images/JoyBox.js"></SCRIPT>
		<style>.yline { BORDER-RIGHT: #a3afb8 1px solid; FONT-SIZE: 12px; TEXT-ALIGN: right }
	.xline { BORDER-RIGHT: #a3afb8 1px solid; BORDER-TOP: #a3afb8 1px solid; TEXT-ALIGN: center }
	.xlineYear { BORDER-RIGHT: #a3afb8 1px solid; TEXT-ALIGN: center }
	.tdline { BORDER-TOP: #d3dfe8 1px solid; FONT-SIZE: 1px; HEIGHT: 13px }
	.tdline2 { FONT-SIZE: 1px; HEIGHT: 13px }
	.xcolhead { FONT-SIZE: 1px }
		</style>
	</HEAD>
	<body style="BORDER-RIGHT: 0px" scroll="no" onload="winload();" onresize="bodyResize();">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" bgColor="white">
				<tr height="100%">
					<td>
						<div id="divBg" style="Z-INDEX: 99; LEFT: 0px; OVERFLOW: auto; WIDTH: 100%; POSITION: absolute; TOP: 0px; HEIGHT: 100%"
							onscroll="BackgroundScroll(this);"><IMG id="imgBg" src="" name="imgBg" runat="server">
							<asp:repeater id="dgList2" Runat="server">
								<ItemTemplate>
									<div id="divItemName" style='position:absolute; left:<%# DataBinder.Eval(Container.DataItem, "ObjectX") %>px; top:<%# DataBinder.Eval(Container.DataItem, "ObjectY") %>; width:10; z-index:100; background-color: #FFFFFF; layer-background-color: #FFFFFF; border: 1px none #000000;'>
										<table width="100%" border="1" cellpadding="2" cellspacing="0" bordercolor="#6396D6" bgcolor="#F3F5F8"
											class="cursorHand" style="border-collapse: collapse;cursor:hand" onMouseOver="changeBgColor(this,'#D0E8FF');"
											onMouseOut="changeBgColor(this,'#F3F5F8');">
											<tr>
												<td align="center" valign="baseline" nowrap onclick="ShowWBS(this.WBSCode);" bgcolor='yellow' WBSCode='<%# DataBinder.Eval(Container.DataItem, "WBSCode") %>'><%# DataBinder.Eval(Container.DataItem, "TaskName") %>(<%# DataBinder.Eval(Container.DataItem, "CompletePercent") %>%)</td>
											</tr>
										</table>
									</div>
								</ItemTemplate>
							</asp:repeater>
						</div>
						<asp:repeater id="dgList" Runat="server">
							<ItemTemplate>
								<div style="Z-INDEX: 2; OVERFLOW: auto; WIDTH: 100%; POSITION: absolute; HEIGHT: 100%"
									id="divItem">
									<IMG style='filter:chroma(color=#0000ff,enabled=1)' id="imgItem" name="imgItem"
										runat="server"> <input type="hidden" runat="server" id="txtAttachMentCode" name="txtAttachMentCode" value='<%# DataBinder.Eval(Container.DataItem, "AttachMentCode")%>'>
									<input type="hidden" runat="server" id="txtColor" name="txtColor" value='<%# DataBinder.Eval(Container.DataItem, "Color")%>'>
								</div>
							</ItemTemplate>
						</asp:repeater>
					</td>
				</tr>
			</table>
			<div id="myjoybox" style="DISPLAY: none; LEFT: 10px; POSITION: absolute; TOP: 200px; HEIGHT: 110px"
				myDiv="divMain" myOffsetBottom="0" myOffsetRight="40" myOffsetTop="0px">
				<table id="joyboxTable" height="110" cellSpacing="0" cellPadding="0" width="180" border="0">
					<tbody>
						<tr>
							<td width="8%" bgColor="#ffffcc">
							<td width="92%" bgColor="#ffffcc"><label id="linktitle"></label></td>
						</tr>
					</tbody>
				</table>
			</div>
			<div id="legend" style="BORDER-RIGHT: #a3afb8 1px solid; BORDER-TOP: #a3afb8 1px solid; Z-INDEX: 100; LEFT: 5px; BORDER-LEFT: #a3afb8 1px solid; BORDER-BOTTOM: #a3afb8 1px solid; POSITION: absolute; TOP: 105px; BACKGROUND-COLOR: #ffffff; layer-background-color: #6699CC">
				<table cellSpacing="5" cellPadding="0" border="0">
					<tr>
						<td noWrap align="center" colSpan="4">Í¼ Àý</td>
					</tr>
					<asp:repeater id="dgLegend" runat="server">
						<ItemTemplate>
							<tr>
								<td noWrap width="5">&nbsp;</td>
								<td noWrap><span style='BORDER-RIGHT: #a3afb8 1px solid; BORDER-TOP: #a3afb8 1px solid; FONT-SIZE: 1px; BORDER-LEFT: #a3afb8 1px solid; WIDTH: 12px; BORDER-BOTTOM: #a3afb8 1px solid; HEIGHT: 12px; BACKGROUND-COLOR: <%# DataBinder.Eval(Container.DataItem, "Color") %>'></span></td>
								<td noWrap><%# DataBinder.Eval(Container.DataItem, "StateName") %>
									(<font color="black"><b><%# DataBinder.Eval(Container.DataItem, "Count") %></b></font>)</td>
								<td width="5"></td>
							</tr>
						</ItemTemplate>
					</asp:repeater></table>
			</div>
			<div id="divHintLoad" style="DISPLAY:none; LEFT:1px; WIDTH:100%; POSITION:absolute; TOP:200px; BACKGROUND-COLOR:transparent">
				<TABLE id="tableHintLoad" height="100" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD vAlign="top" align="center"><iframe id="frameLoad" src="../Cost/LoadingPrepare.htm" frameBorder="no" width="100%" scrolling="auto"
								height="100%"></iframe>
						</TD>
					</TR>
				</TABLE>
			</div>
			<input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server"> <input id="txtWBSCode" type="hidden" name="txtWBSCode" runat="server">
			<input id="txtGroundWorkCode" type="hidden" name="txtGroundWorkCode" runat="server">
		</form>
		<SCRIPT language="javascript">
<!--

var CurrUrl = window.location.href;

function ShowWBS(WBSCode)
{
	OpenFullWindow("../Project/WBSInfo.aspx?WBSCode="+WBSCode+"&ProjectCode=<%=Request["ProjectCode"]%>","");
}

function ScrollSyn(srcObj, dstObj)
{
	dstObj.scrollTop = srcObj.scrollTop;
	dstObj.scrollLeft = srcObj.scrollLeft;
}

function BackgroundScroll(obj)
{
	div = document.all.divItem;
	if (!div) return;
	
	if (div[0])
	{
		for(var i=0;i<div.length;i++)
		{
			ScrollSyn(obj, div[i]);
		}
	}
	else
	{
		ScrollSyn(obj, div);
	}
}

function changeBgColor(obj,color){
	obj.style.backgroundColor=color;
}

	function winload()
	{
		ChangeLegendLocation();		
	}

	function ChangeLegendLocation()
	{
		var legend = document.all.legend;
		legend.style.left = document.body.offsetWidth - 200 - 5;
		legend.style.top = document.all.divBg.offsetTop + 5;
	}
	
	function bodyResize()
	{
		ChangeLegendLocation();		
	}
	
//-->
		</SCRIPT>
	</body>
</HTML>
