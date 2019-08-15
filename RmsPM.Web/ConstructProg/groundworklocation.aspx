<%@ Page language="c#" Inherits="RmsPM.Web.ConstructProg.GroundWorkLocation" CodeFile="GroundWorkLocation.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>工程剖面图</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
	</HEAD>
		<script language="javascript">
		
function GoBack()
{
	window.location = "GroundWorkInfo.aspx?ProjectCode=" + Form1.txtProjectCode.value + "&GroundWorkCode=" + Form1.txtGroundWorkCode.value;
}

		</script>
	<body topmargin="0" leftmargin="0" scroll="no" onload="winload();">
		<form id="Form1" method="post" runat="server" action="asdasd">
			<table border="0" cellspacing="0" cellpadding="0" width="100%" height="100%">
				<tr>
					<td class="tools-area" vAlign="top">
						<IMG src="../images/btn_li.gif" align="absMiddle"> 
						<input class="button" id="btnSave" type="button" value="保存位置" name="btnSave" onclick="if (!SaveLocation()) return false;" runat="server" onserverclick="btnSave_ServerClick">
						<input class="button" id="btnBack" type="button" value="返 回" name="btnBack" onclick="GoBack();">
					</td>
				</tr>
				<tr>
					<td class="table">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="note">提示：点击区域名称，拖到指定位置后再点击</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr height="100%">
					<td valign="top" id="group_map">
						<div id="divBg" style="Z-INDEX: 99; OVERFLOW: auto; WIDTH: 100%; POSITION: absolute; HEIGHT: 100%"
							onscroll="BackgroundScroll(this);" runat="server">
							<IMG id="imgBg" src="" name="imgBg" runat="server">
							<asp:repeater id="dgList2" Runat="server">
								<ItemTemplate>
									<div id="divItemName" style='position:absolute; left:<%# DataBinder.Eval(Container.DataItem, "ObjectX") %>px; top:<%# DataBinder.Eval(Container.DataItem, "ObjectY") %>; width:10; z-index:100; background-color: #FFFFFF; layer-background-color: #FFFFFF; border: 1px none #000000;' onMouseDown="doMoves(this);" code='<%# DataBinder.Eval(Container.DataItem, "WBSCode") %>'>
										<table width="100%" border="1" cellpadding="2" cellspacing="0" bordercolor="#6396D6" bgcolor="#F3F5F8"
											class="cursorHand" style="border-collapse: collapse;cursor:hand" onMouseOver="changeBgColor(this,'#D0E8FF');"
											onMouseOut="changeBgColor(this,'#F3F5F8');">
											<tr>
												<td align="center" valign="baseline" nowrap onClick="return false;" bgcolor='yellow'><%# DataBinder.Eval(Container.DataItem, "TaskName") %></td>
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
									<IMG style='filter:chroma(color=#0000ff,enabled=1);background-color:#ffffff' id="imgItem" name="imgItem"
										runat="server"> <input type="hidden" runat="server" id="txtAttachMentCode" name="txtAttachMentCode" value='<%# DataBinder.Eval(Container.DataItem, "AttachMentCode")%>'>
									<input type="hidden" runat="server" id="txtColor" name="txtColor" value='<%# DataBinder.Eval(Container.DataItem, "Color")%>'>
								</div>
							</ItemTemplate>
						</asp:repeater>
					</td>
				</tr>
			</table>
			<input id="txtFromUrl" type="hidden" name="txtFromUrl" runat="server"> <input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server">
			<input id="txtWBSCode" type="hidden" name="txtWBSCode" runat="server"> <input id="txtGroundWorkCode" type="hidden" name="txtGroundWorkCode" runat="server">
			<input id="txtSaveLocation" type="hidden" name="txtSaveLocation" runat="server">
		</form>
		<script language="javascript">
var moveObj;
var canMove=false;

var MarginX = 5;
var MarginY = 5;

function doMoves(obj){
	canMove=!canMove;
	moveObj=obj;
}

function goMoves(){
	if(canMove){
		var obj0=document.body;
		moveObj.style.left=obj0.scrollLeft + event.clientX - MarginX;
		moveObj.style.top=obj0.scrollTop + event.clientY - MarginY;
	}
}

function changeBgColor(obj,color){
	obj.style.backgroundColor=color;
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

function winload()
{
	//重定义map.js中的左边距、上边距
	MarginX = document.all.divBg.offsetLeft + 5;
	MarginY = document.all.divBg.offsetTop + 5;

	if(document.all("group_map"))
	{
//		document.all("group_map").oncontextmenu=doMenu;
//		document.body.onmousedown=doMenuHidden;
		
		document.all("group_map").onmousemove = goMoves;
	}
}

//保存位置
function SaveLocation()
{
	var obj = document.all("divItemName");
	if (!obj) return true;
	
	var str="";
	if(obj[0])
	{
		for(var i=0;i<obj.length;i++){
			str+=""+obj[i].code+"|"+obj[i].style.pixelLeft.toString()+"|"+obj[i].style.pixelTop.toString()+"$";
		}
	}
	else
	{
		if(obj)
		{
			str=""+obj.code+"|"+obj.style.pixelLeft.toString()+"|"+obj.style.pixelTop.toString()+"$";
		}
	}
	
	Form1.txtSaveLocation.value = str;
	
	return true;
}

		</script>
	</body>
</HTML>
