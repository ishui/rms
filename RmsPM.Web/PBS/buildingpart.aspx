<%@ Page language="c#" Inherits="RmsPM.Web.PBS.BuildingPart" CodeFile="BuildingPart.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>BuildingPart</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="map.js"></SCRIPT>
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<SCRIPT language="javascript" src="../images/JoyBox.js"></SCRIPT>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0">
		<form id="Form1" name="Form1" onsubmit="" action="" method="post" runat="server">
			<div style="DISPLAY: none"><input id="btnHidLoadData" type="button" name="btnHidLoadData" runat="server"></div>
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td bgColor="#e4eff6" height="6"></td>
				</tr>
				<tr>
					<td height="25">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="topic" background="../images/topic_bg.gif"><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle">产品管理 
									- 房控图
								</td>
								<td style="CURSOR: hand" onclick="window.history.go(-1);" width="79"><IMG height="25" src="../images/btn_back.jpg" width="79"></td>
							</tr>
						</table>
					</td>
				</tr>
				<TR>
					<TD class="tools-area" valign="top">
						<IMG src="../images/btn_li.gif" align="absMiddle"> <input class="button" id="btnAddWizard" onclick="doModifyBuildingWizard(Form1.txtBuildingCode.value);"
							type="button" value="新建结构向导" name="btnAddWizard" runat="server"> <input class="button" id="btnRebuild" onclick="if (!delConfirm()) return; document.all.divHintSave.style.display='';"
							type="button" value="删除结构" name="btnRebuild" runat="server" onserverclick="btnRebuild_ServerClick"> <input class="button" id="btnModifyBuildingStructure" onclick="document.all.divHintLoad.style.display=''; doModifyBuildingStructure(Form1.txtBuildingCode.value);"
							type="button" value="修改结构" name="btnModifyBuildingStructure" runat="server">
						<input class="button" id="btnModifyRoomName" onclick="document.all.divHintLoad.style.display=''; doModifyRoomName(Form1.txtBuildingCode.value);"
							type="button" value="修改室号" name="btnModifyRoomName" runat="server"> <input class="button" id="btnModifyRoomModel" onclick="document.all.divHintLoad.style.display=''; doModifyRoomModel(Form1.txtBuildingCode.value);"
							type="button" value="修改户型" name="btnModifyRoomModel" runat="server"> <input class="button" id="btnModifyRoomArea" onclick="doModifyRoomArea(Form1.txtBuildingCode.value);"
							type="button" value="修改房源面积" name="btnModifyRoomArea" runat="server"> <IMG src="../images/btn_li.gif" align="absMiddle">
						<input class="button" onclick="document.all.divHintLoad.style.display=''; GotoBuildingInfo(Form1.txtBuildingCode.value);"
							type="button" value="切换到楼栋信息" name="btnGotoBuildingInfo"> 转到楼栋：<select runat="server" id="sltBuilding" name="sltBuilding" class="select" onchange="document.all.divHintLoad.style.display='';GotoBuildingPart(this.value);"></select>
					</TD>
				</TR>
				<tr>
					<td class="table" align="center">
						<table class="form" id="tbMain2" cellSpacing="0" cellPadding="0" width="100%" border="0"
							runat="server">
							<tr>
								<td class="note" vAlign="bottom" align="center" height="25"><asp:label id="lblBuildingName" Runat="server"></asp:label>(<asp:label id="lblPBSTypeName" Runat="server"></asp:label>)</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr height="100%">
					<td class="table" vAlign="top" align="center">
						<div id="divMain" style="LEFT: 0px; OVERFLOW: auto; WIDTH: 100%; POSITION: static; HEIGHT: 100%"
							runat="server">
							<table class="list" cellSpacing="0" cellPadding="0" width="300" border="0">
								<tr>
									<td class="list-a" align="center" width="70" rowSpan="2"><table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
											<tr align="center">
												<td noWrap rowSpan="2">楼层</td>
												<td noWrap>门牌号</td>
											</tr>
											<tr>
												<td noWrap align="center">室号</td>
											</tr>
										</table>
									</td>
									<asp:repeater id="repeat1" runat="server">
										<ItemTemplate>
											<td colspan='<%# DataBinder.Eval(Container.DataItem, "No3") %>' align="center" class="list-c">
												<%# DataBinder.Eval(Container.DataItem, "No4") %>
											</td>
										</ItemTemplate>
									</asp:repeater></tr>
								<tr class="list-c">
									<asp:repeater id="dlBuild3" runat="server">
										<ItemTemplate>
											<td align="center">
												<%# DataBinder.Eval(Container.DataItem, "No3") %>
											</td>
										</ItemTemplate>
									</asp:repeater></tr>
								<asp:repeater id="dlBuild1" runat="server">
									<ItemTemplate>
										<tr>
											<td align="center" class="list-c"><%# DataBinder.Eval(Container.DataItem, "FloorName") %></td>
											<%# DataBinder.Eval(Container.DataItem, "No4") %>
										</tr>
									</ItemTemplate>
								</asp:repeater></table>
						</div>
					</td>
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
			<div id="myjoybox" style="DISPLAY: none; LEFT: 10px; POSITION: absolute; TOP: 200px; HEIGHT: 50px"
				myOffsetTop="0px" myOffsetRight="40" myOffsetBottom="20" myDiv="">
				<table id="joyboxTable" height="50" cellSpacing="0" cellPadding="0" width="180" border="0">
					<tbody>
						<tr>
							<td width="8%" bgColor="#ffffcc">
							<td width="92%" bgColor="#ffffcc"><label id="linktitle"></label></td>
						</tr>
					</tbody>
				</table>
			</div>
			<div id="legend" style="display:none;BORDER-RIGHT: #a3afb8 1px solid; BORDER-TOP: #a3afb8 1px solid; Z-INDEX: 1; LEFT: 5px; BORDER-LEFT: #a3afb8 1px solid; BORDER-BOTTOM: #a3afb8 1px solid; POSITION: absolute; TOP: 105px; BACKGROUND-COLOR: #ffffff; layer-background-color: #6699CC">
				<table cellSpacing="5" cellPadding="0" border="0">
					<tr>
						<td noWrap align="center" colSpan="4">图 例</td>
					</tr>
					<asp:repeater id="repLegend" runat="server">
						<ItemTemplate>
							<tr>
								<td noWrap width="5">&nbsp;</td>
								<td noWrap><span style='BORDER-RIGHT: #a3afb8 1px solid; BORDER-TOP: #a3afb8 1px solid; FONT-SIZE: 1px; BORDER-LEFT: #a3afb8 1px solid; WIDTH: 12px; BORDER-BOTTOM: #a3afb8 1px solid; HEIGHT: 12px; BACKGROUND-COLOR: <%# DataBinder.Eval(Container.DataItem, "Color") %>'></span></td>
								<td noWrap><%# DataBinder.Eval(Container.DataItem, "State") %>
									(<font color="black"><b><%# DataBinder.Eval(Container.DataItem, "Count") %></b></font>)</td>
								<td width="5"></td>
							</tr>
						</ItemTemplate>
					</asp:repeater></table>
			</div>
			<input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server"> <input id="txtParentCode" type="hidden" name="txtParentCode" runat="server">
			<input id="txtBuildingCode" type="hidden" name="txtBuildingCode" runat="server"><input id="txtAct" type="hidden" name="txtAct" runat="server">
			<input id="txtIsArea" type="hidden" name="txtIsArea" runat="server"> <input id="txtBuildingName" type="hidden" size="1" name="txtBuildingName" runat="server">
			<input id="txtHasLoad" type="hidden" name="txtHasLoad" runat="server">
		</form>
		<script>  

		    function delConfirm()
		    {
		       return confirm("请确认是否重新建立楼东结构？注意：将要删除原有结构！") ;
		    }
		    
		/*
			var nowRoom=null;
			function doRoomDetails(id,obj){
				if(nowRoom!=null){
					changeBackGroud(nowRoom,"");
					
				}
				nowRoom=obj;
				changeBackGroud(nowRoom,"../images/bgs.gif");
				document.all("room_details").src="RoomInfo.aspx?RoomCode="+id;
			}
		*/
		
		</script>
		<script language="javascript">

function  window_onscroll(){
return;

	if (document.all('legend')){
		legend.style.top= basetb.offsetTop +document.body.scrollTop+30;
		if (basetb.offsetLeft+ document.body.scrollLeft-80>=0)
			legend.style.left= basetb.offsetLeft+ document.body.scrollLeft-150 
		else
			legend.style.left= 0;
		
	}
}

window.onresize =window_onscroll;
window.onload = window_onscroll;
window.onscroll = window_onscroll;

if (Form1.txtHasLoad.value == "")
{
//	Form1.btnHidLoadData.click();
}

function RoomMouseOver(obj)
{
}

function RoomMouseOut(obj)
{
}

//查看房间
function OpenRoomInfo(code)
{
	OpenCustomWindow("RoomInfo.aspx?RoomCode="+code, "房间信息" , 760, 540);
}

		</script>
	</body>
</HTML>
