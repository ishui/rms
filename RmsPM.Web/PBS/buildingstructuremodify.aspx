<%@ Page language="c#" Inherits="RmsPM.Web.PBS.BuildingStructureModify" CodeFile="BuildingStructureModify.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>BuildingStructureModify</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="map.js"></SCRIPT>
		<script>
			function iniBody()
			{
				if (Form1.txtAct.value=="structure")
				{
					document.all("trSubmit").style.display="none";
				}
			}
		</script>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0" onload="iniBody();"
		scroll="no">
		<form id="Form1" name="Form1" action='CreateBuildingAction.aspx?ACT=building_modify_<%=Request["action"]%>&amp;BuildingCode=<%=Request["BuildingCode"]%>' method="post" >
			<table cellSpacing="0" cellPadding="0" width="100%" border="0" height="100%">
				<tr>
					<td bgColor="#e4eff6" height="6"></td>
				</tr>
				<tr>
					<td height="25">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="topic" background="../images/topic_bg.gif"><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle">产品管理 
									- <span id="tdTitle" runat="server"></span>
								</td>
								<td style="CURSOR: hand" onclick="GotoBuildingPart(Form1.txtBuildingCode.value);" width="79"><IMG height="25" src="../images/btn_back.jpg" width="79"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td class="table" align="center">
						<table border="0" cellpadding="0" cellspacing="0" class="form" width="100%">
							<tr>
								<td align="center" class="note"><asp:label id="lblBuildingName" Runat="server"></asp:label>(<asp:label id="lblPBSTypeName" Runat="server"></asp:label>)</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr height="100%">
					<td class="table" align="center">
						<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
						<table border="1" cellpadding="3" cellspacing="0" bordercolor="#6396d6" bgcolor="#f3f5f8"
							style="BORDER-COLLAPSE: collapse">
							<tr>
								<td width="70" align="center" rowspan="2">
									<table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0">
										<tr align="center">
											<td rowspan="2" nowrap>楼层</td>
											<td height="500%" nowrap>门牌号</td>
										</tr>
										<tr>
											<td height="500%" align="center">室号</td>
										</tr>
									</table>
								</td>
								<asp:Repeater id="repeat1" runat="server">
									<ItemTemplate>
										<td align="center" colspan='<%# DataBinder.Eval(Container.DataItem, "No3") %>'>
											<%# DataBinder.Eval(Container.DataItem, "No4") %>
										</td>
									</ItemTemplate>
								</asp:Repeater>
							</tr>
							<tr>
								<asp:Repeater id="dlBuild3" runat="server">
									<ItemTemplate>
										<td>
											<%# DataBinder.Eval(Container.DataItem, "No3") %>
										</td>
									</ItemTemplate>
								</asp:Repeater>
							</tr>
							<asp:Repeater id="dlBuild1" runat="server">
								<ItemTemplate>
									<tr>
										<td align="center"><%# DataBinder.Eval(Container.DataItem, "No3") %></td>
										<%# DataBinder.Eval(Container.DataItem, "No4") %>
									</tr>
								</ItemTemplate>
							</asp:Repeater>
						</table>
						</div>
					</td>
				</tr>
				<tr id="trSubmit" align="center">
					<td class="table" vAlign="top">
						<table border="0">
							<tr>
								<td>
									<input name="building_modify_submit" class="submit" type="submit" id="building_modify_submit"
										value="保 存">
									<input class="submit" onclick="GotoBuildingPart(Form1.txtBuildingCode.value);" type="button"
										value="取 消">
								</td>
							</tr>
						</table>
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
			</TABLE>
			<div id="MenuDiv" style="BORDER-RIGHT:#000000 1px; BORDER-TOP:#000000 1px; DISPLAY:none; Z-INDEX:9; BORDER-LEFT:#000000 1px; WIDTH:110px; BORDER-BOTTOM:#000000 1px; POSITION:absolute; BACKGROUND-COLOR:#ffffff; layer-background-color:#FFFFFF"
				onMouseOver="doCanHiddenFalse();" onMouseOut="doCanHiddenTrue();">
				<table width="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#6396d6" bgcolor="#f3f5f8"
					style="BORDER-COLLAPSE: collapse">
					<tr>
						<td><table width="100%" border="0" cellspacing="3" cellpadding="0">
								<tr id="UniteTD" style="DISPLAY:none" onClick="doRoomUnite(Form1.txtBuildingCode.value);  return false;">
									<td height="30" align="center" nowrap class="cursorHand">
										<table width="100%" height="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#f3f5f8"
											class="cursorHand" id="RightMenuTable" onMouseOver="changeBgColor(this,'#D0E8FF');this.border=1;changeBorderColor(this,'#6396D6');"
											onMouseOut="changeBgColor(this,'#F3F5F8');this.border=0;changeBorderColor(this,'#F3F5F8');"
											onMouseDown="changeBgColor(this,'#D0E8FF');changeBorderColor(this,'#F3F5F8');" onMouseUp="changeBgColor(this,'#D0E8FF');changeBorderColor(this,'#6396D6');"
											style="BORDER-COLLAPSE: collapse">
											<tr>
												<td height="15" align="center">
													合并</td>
											</tr>
										</table>
									</td>
								</tr>
								<tr id="SplitTD" style="DISPLAY:none" onClick="doRoomSplit(Form1.txtBuildingCode.value); return false;">
									<td height="30" align="center" nowrap class="cursorHand"><table width="100%" height="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#f3f5f8"
											class="cursorHand" id="RightMenuTable" onMouseOver="changeBgColor(this,'#D0E8FF');this.border=1;changeBorderColor(this,'#6396D6');" onMouseOut="changeBgColor(this,'#F3F5F8');this.border=0;changeBorderColor(this,'#F3F5F8');"
											onMouseDown="changeBgColor(this,'#D0E8FF');changeBorderColor(this,'#F3F5F8');" onMouseUp="changeBgColor(this,'#D0E8FF');changeBorderColor(this,'#6396D6');"
											style="BORDER-COLLAPSE: collapse">
											<tr>
												<td height="15" align="center">
													拆分</td>
											</tr>
										</table>
									</td>
								</tr>
								<tr id="DelTD" style="DISPLAY:none" onClick="doRoomDel(Form1.txtBuildingCode.value); return false;">
									<td height="30" align="center" nowrap class="cursorHand"><table width="100%" height="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#f3f5f8"
											class="cursorHand" id="RightMenuTable" onMouseOver="changeBgColor(this,'#D0E8FF');this.border=1;changeBorderColor(this,'#6396D6');" onMouseOut="changeBgColor(this,'#F3F5F8');this.border=0;changeBorderColor(this,'#F3F5F8');"
											onMouseDown="changeBgColor(this,'#D0E8FF');changeBorderColor(this,'#F3F5F8');" onMouseUp="changeBgColor(this,'#D0E8FF');changeBorderColor(this,'#6396D6');"
											style="BORDER-COLLAPSE: collapse">
											<tr>
												<td height="15" align="center">
													删除</td>
											</tr>
										</table>
									</td>
								</tr>
								<tr id="NewTD" style="DISPLAY:none" onClick="doRoomNew(Form1.txtBuildingCode.value); return false;">
									<td height="30" align="center" nowrap class="cursorHand"><table width="100%" height="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#f3f5f8"
											class="cursorHand" id="RightMenuTable" onMouseOver="changeBgColor(this,'#D0E8FF');this.border=1;changeBorderColor(this,'#6396D6');" onMouseOut="changeBgColor(this,'#F3F5F8');this.border=0;changeBorderColor(this,'#F3F5F8');"
											onMouseDown="changeBgColor(this,'#D0E8FF');changeBorderColor(this,'#F3F5F8');" onMouseUp="changeBgColor(this,'#D0E8FF');changeBorderColor(this,'#6396D6');"
											style="BORDER-COLLAPSE: collapse">
											<tr>
												<td height="15" align="center">
													新增</td>
											</tr>
										</table>
									</td>
								</tr>
								<tr id="UniteXTDLeft" style="DISPLAY:none" onClick="doRoomUniteXLeft(Form1.txtBuildingCode.value); return false;">
									<td height="30" align="center" nowrap class="cursorHand"><table width="100%" height="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#f3f5f8"
											class="cursorHand" id="RightMenuTable" onMouseOver="changeBgColor(this,'#D0E8FF');this.border=1;changeBorderColor(this,'#6396D6');" onMouseOut="changeBgColor(this,'#F3F5F8');this.border=0;changeBorderColor(this,'#F3F5F8');"
											onMouseDown="changeBgColor(this,'#D0E8FF');changeBorderColor(this,'#F3F5F8');" onMouseUp="changeBgColor(this,'#D0E8FF');changeBorderColor(this,'#6396D6');"
											style="BORDER-COLLAPSE: collapse">
											<tr>
												<td height="15" align="center">
													向左合并</td>
											</tr>
										</table>
									</td>
								</tr>
								<tr id="UniteXTD" style="DISPLAY:none" onClick="doRoomUniteX(Form1.txtBuildingCode.value); return false;">
									<td height="30" align="center" nowrap class="cursorHand"><table width="100%" height="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#f3f5f8"
											class="cursorHand" id="RightMenuTable" onMouseOver="changeBgColor(this,'#D0E8FF');this.border=1;changeBorderColor(this,'#6396D6');" onMouseOut="changeBgColor(this,'#F3F5F8');this.border=0;changeBorderColor(this,'#F3F5F8');"
											onMouseDown="changeBgColor(this,'#D0E8FF');changeBorderColor(this,'#F3F5F8');" onMouseUp="changeBgColor(this,'#D0E8FF');changeBorderColor(this,'#6396D6');"
											style="BORDER-COLLAPSE: collapse">
											<tr>
												<td height="15" align="center">
													向右合并</td>
											</tr>
										</table>
									</td>
								</tr>
								<tr id="UniteYTD" style="DISPLAY:none" onClick="doRoomUniteY(Form1.txtBuildingCode.value); return false;">
									<td height="30" align="center" nowrap class="cursorHand"><table width="100%" height="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#f3f5f8"
											class="cursorHand" id="RightMenuTable" onMouseOver="changeBgColor(this,'#D0E8FF');this.border=1;changeBorderColor(this,'#6396D6');" onMouseOut="changeBgColor(this,'#F3F5F8');this.border=0;changeBorderColor(this,'#F3F5F8');"
											onMouseDown="changeBgColor(this,'#D0E8FF');changeBorderColor(this,'#F3F5F8');" onMouseUp="changeBgColor(this,'#D0E8FF');changeBorderColor(this,'#6396D6');"
											style="BORDER-COLLAPSE: collapse">
											<tr>
												<td height="15" align="center">
													向下合并</td>
											</tr>
										</table>
									</td>
								</tr>
								<tr id="UniteYTDUp" style="DISPLAY:none" onClick="doRoomUniteYUp(Form1.txtBuildingCode.value); return false;">
									<td height="30" align="center" nowrap class="cursorHand"><table width="100%" height="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#f3f5f8"
											class="cursorHand" id="RightMenuTable" onMouseOver="changeBgColor(this,'#D0E8FF');this.border=1;changeBorderColor(this,'#6396D6');" onMouseOut="changeBgColor(this,'#F3F5F8');this.border=0;changeBorderColor(this,'#F3F5F8');"
											onMouseDown="changeBgColor(this,'#D0E8FF');changeBorderColor(this,'#F3F5F8');" onMouseUp="changeBgColor(this,'#D0E8FF');changeBorderColor(this,'#6396D6');"
											style="BORDER-COLLAPSE: collapse">
											<tr>
												<td height="15" align="center">
													向上合并</td>
											</tr>
										</table>
									</td>
								</tr>
								<tr id="CancelUniteTD" style="DISPLAY:none" onClick="doCancelUnite(Form1.txtBuildingCode.value); return false;">
									<td height="30" align="center" nowrap class="cursorHand"><table width="100%" height="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#f3f5f8"
											class="cursorHand" id="RightMenuTable" onMouseOver="changeBgColor(this,'#D0E8FF');this.border=1;changeBorderColor(this,'#6396D6');" onMouseOut="changeBgColor(this,'#F3F5F8');this.border=0;changeBorderColor(this,'#F3F5F8');"
											onMouseDown="changeBgColor(this,'#D0E8FF');changeBorderColor(this,'#F3F5F8');" onMouseUp="changeBgColor(this,'#D0E8FF');changeBorderColor(this,'#6396D6');"
											style="BORDER-COLLAPSE: collapse">
											<tr>
												<td height="15" align="center">
													取消</td>
											</tr>
										</table>
									</td>
								</tr>
								<tr id="SplitXTD" style="DISPLAY:none" onClick="doRoomSplitX(Form1.txtBuildingCode.value); return false;">
									<td height="30" align="center" nowrap class="cursorHand"><table width="100%" height="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#f3f5f8"
											class="cursorHand" id="RightMenuTable" onMouseOver="changeBgColor(this,'#D0E8FF');this.border=1;changeBorderColor(this,'#6396D6');" onMouseOut="changeBgColor(this,'#F3F5F8');this.border=0;changeBorderColor(this,'#F3F5F8');"
											onMouseDown="changeBgColor(this,'#D0E8FF');changeBorderColor(this,'#F3F5F8');" onMouseUp="changeBgColor(this,'#D0E8FF');changeBorderColor(this,'#6396D6');"
											style="BORDER-COLLAPSE: collapse">
											<tr>
												<td height="15" align="center">
													横向拆分</td>
											</tr>
										</table>
									</td>
								</tr>
								<tr id="SplitYTD" style="DISPLAY:none" onClick="doRoomSplitY(Form1.txtBuildingCode.value); return false;">
									<td height="30" align="center" nowrap class="cursorHand"><table width="100%" height="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#f3f5f8"
											class="cursorHand" id="RightMenuTable" onMouseOver="changeBgColor(this,'#D0E8FF');this.border=1;changeBorderColor(this,'#6396D6');" onMouseOut="changeBgColor(this,'#F3F5F8');this.border=0;changeBorderColor(this,'#F3F5F8');"
											onMouseDown="changeBgColor(this,'#D0E8FF');changeBorderColor(this,'#F3F5F8');" onMouseUp="changeBgColor(this,'#D0E8FF');changeBorderColor(this,'#6396D6');"
											style="BORDER-COLLAPSE: collapse">
											<tr>
												<td height="15" align="center">
													纵向拆分</td>
											</tr>
										</table>
									</td>
								</tr>
								<tr id="CancelSplitTD" style="DISPLAY:none" onClick="doCancelSplit(Form1.txtBuildingCode.value); return false;">
									<td height="30" align="center" nowrap class="cursorHand"><table width="100%" height="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#f3f5f8"
											class="cursorHand" id="RightMenuTable" onMouseOver="changeBgColor(this,'#D0E8FF');this.border=1;changeBorderColor(this,'#6396D6');" onMouseOut="changeBgColor(this,'#F3F5F8');this.border=0;changeBorderColor(this,'#F3F5F8');"
											onMouseDown="changeBgColor(this,'#D0E8FF');changeBorderColor(this,'#F3F5F8');" onMouseUp="changeBgColor(this,'#D0E8FF');changeBorderColor(this,'#6396D6');"
											style="BORDER-COLLAPSE: collapse">
											<tr>
												<td align="center">
													取消拆分</td>
											</tr>
										</table>
									</td>
								</tr>
							</table>
						</td>
					</tr>
				</table>
			</div>
			<input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server">
			<input id="txtParentCode" type="hidden" name="txtParentCode" runat="server"> <input id="txtBuildingCode" type="hidden" name="txtBuildingCode" runat="server"><input id="txtAct" type="hidden" name="txtAct" runat="server">
			<input id="txtIsArea" type="hidden" name="txtIsArea" runat="server"> <input id="txtBuildingName" type="hidden" size="1" name="txtBuildingName" runat="server">
		</form>
		<script>
	function doRoomChoose(str1,str2,str3,str4,str5){
			var obj1=window.opener.document.all(str1);
			var obj2=window.opener.document.all(str2);
			if(obj1){
				obj1.innerHTML=str3;
			}
			if(obj2){
				obj2.value=str4;
			}
			if(str5!=""){
				window.opener.eval(str5);
			}
			WinClose();
		}

		var nowRoom=null;

		function doRoomSplitX(id){
			if(window.confirm("您确认要进行横向拆分吗？")){
//			alert("CreateBuildingAction.aspx?action=room_splitX&BuildingCode="+id+"&room_id="+room_id+"&room_x="+room_x+"&room_y="+room_y)
				PageTo("CreateBuildingAction.aspx?ACT=room_splitX&BuildingCode="+id+"&room_id="+room_id+"&room_x="+room_x+"&room_y="+room_y);
			}
		}
		function doRoomSplitY(id){
			if(window.confirm("您确认要进行纵向拆分吗？")){
				PageTo("CreateBuildingAction.aspx?ACT=room_splitY&BuildingCode="+id+"&room_id="+room_id+"&room_x="+room_x+"&room_y="+room_y);
			}
		}
		function doRoomUniteXLeft(id){
			if(window.confirm("您确认要进行合并吗？")){
				PageTo("CreateBuildingAction.aspx?ACT=room_uniteXLeft&BuildingCode="+id+"&room_id="+room_id+"&room_x="+room_x+"&room_y="+room_y);
			}
		}
		function doRoomUniteX(id){
			if(window.confirm("您确认要进行合并吗？")){
				PageTo("CreateBuildingAction.aspx?ACT=room_uniteX&BuildingCode="+id+"&room_id="+room_id+"&room_x="+room_x+"&room_y="+room_y);
			}
		}
		function doRoomUniteY(id){
			if(window.confirm("您确认要进行合并吗？")){
				PageTo("CreateBuildingAction.aspx?ACT=room_uniteY&BuildingCode="+id+"&room_id="+room_id+"&room_x="+room_x+"&room_y="+room_y);
			}
		}
		function doRoomUniteYUp(id){
			if(window.confirm("您确认要进行合并吗？")){
				PageTo("CreateBuildingAction.aspx?ACT=room_uniteYUp&BuildingCode="+id+"&room_id="+room_id+"&room_x="+room_x+"&room_y="+room_y);
			}
		}
		function doRoomSplit(id){
			document.all("UniteTD").style.display="none";
			document.all("SplitTD").style.display="none";
			document.all("DelTD").style.display="none";
			document.all("NewTD").style.display="none";
			document.all("UniteXTDLeft").style.display="none";
			document.all("UniteXTD").style.display="none";
			document.all("UniteYTD").style.display="none";
			document.all("UniteYTDUp").style.display="none";
			document.all("CancelUniteTD").style.display="none";
			document.all("SplitXTD").style.display="none";
			document.all("SplitYTD").style.display="none";
			document.all("CancelSplitTD").style.display="";
			if(parseInt(cs)>1){
				document.all("SplitXTD").style.display="";
			}
			if(parseInt(rs)>1){
				document.all("SplitYTD").style.display="";
			}
		}
		function doCancelSplit(id){
			document.all("UniteTD").style.display="";
			document.all("SplitTD").style.display="none";
			document.all("DelTD").style.display="";
			document.all("NewTD").style.display="none";
			document.all("UniteXTDLeft").style.display="none";
			document.all("UniteXTD").style.display="none";
			document.all("UniteYTD").style.display="none";
			document.all("UniteYTDUp").style.display="none";
			document.all("CancelUniteTD").style.display="none";
			document.all("SplitXTD").style.display="none";
			document.all("SplitYTD").style.display="none";
			document.all("CancelSplitTD").style.display="none";
			if(parseInt(rs)>1||parseInt(cs)>1){
				document.all("SplitTD").style.display="";
			}
		}
		function doRoomUnite(id){
			document.all("UniteTD").style.display="none";
			document.all("SplitTD").style.display="none";
			document.all("DelTD").style.display="none";
			document.all("NewTD").style.display="none";
			document.all("UniteXTDLeft").style.display="";
			document.all("UniteXTD").style.display="";
			document.all("UniteYTD").style.display="";
			document.all("UniteYTDUp").style.display="";
			document.all("CancelUniteTD").style.display="";
			document.all("SplitXTD").style.display="none";
			document.all("SplitYTD").style.display="none";
			document.all("CancelSplitTD").style.display="none";
		}
		function doCancelUnite(id){
			document.all("UniteTD").style.display="";
			document.all("SplitTD").style.display="none";
			document.all("DelTD").style.display="";
			document.all("NewTD").style.display="none";
			document.all("UniteXTDLeft").style.display="none";
			document.all("UniteXTD").style.display="none";
			document.all("UniteYTD").style.display="none";
			document.all("UniteYTDUp").style.display="none";
			document.all("CancelUniteTD").style.display="none";
			document.all("SplitXTD").style.display="none";
			document.all("SplitYTD").style.display="none";
			document.all("CancelSplitTD").style.display="none";
			if(parseInt(rs)>1||parseInt(cs)>1){
				document.all("SplitTD").style.display="";
			}
		}
		function doRoomNew(id){
			if(window.confirm("您确认要在这里新增一个房间吗？")){
				PageTo("CreateBuildingAction.aspx?ACT=room_new&BuildingCode="+id+"&room_x="+room_x+"&room_y="+room_y);
			}
		}
		function doRoomDel(id){
			if(window.confirm("您确认要删除这个房间吗？")){
				PageTo("CreateBuildingAction.aspx?ACT=room_del&BuildingCode="+id+"&room_id="+room_id);
			}
		}
		document.body.onmousedown=doMenuHidden;
		var canHidden=true;
		var MenuObj=document.all("MenuDiv");
		var rs,cs,room_id,room_x,room_y;
		function doCanHiddenTrue(){
			canHidden=true;
		}
		function doCanHiddenFalse(){
			canHidden=false;
		}
		function doModifyBuilding(obj){
			if(MenuObj){
				MenuObj.style.left=document.body.scrollLeft+event.clientX-5;
				MenuObj.style.top=document.body.scrollTop+event.clientY-85;
				MenuObj.style.display="";
				room_id=obj.room_id;
				rs=obj.rs;
				cs=obj.cs;
				room_x=obj.x;
				room_y=obj.y;
				if(room_id!=""){
					document.all("UniteTD").style.display="";
					document.all("SplitTD").style.display="none";
					document.all("DelTD").style.display="";
					document.all("NewTD").style.display="none";
					document.all("UniteXTDLeft").style.display="none";
					document.all("UniteXTD").style.display="none";
					document.all("UniteYTD").style.display="none";
					document.all("UniteYTDUp").style.display="none";
					document.all("CancelUniteTD").style.display="none";
					document.all("SplitXTD").style.display="none";
					document.all("SplitYTD").style.display="none";
					document.all("CancelSplitTD").style.display="none";
					if(parseInt(rs)>1||parseInt(cs)>1){
						document.all("SplitTD").style.display="";
					}
				}else{
					document.all("UniteTD").style.display="none";
					document.all("SplitTD").style.display="none";
					document.all("DelTD").style.display="none";
					document.all("NewTD").style.display="";
					document.all("UniteXTDLeft").style.display="none";
					document.all("UniteXTD").style.display="none";
					document.all("UniteYTD").style.display="none";
					document.all("UniteYTDUp").style.display="none";
					document.all("CancelUniteTD").style.display="none";
					document.all("SplitXTD").style.display="none";
					document.all("SplitYTD").style.display="none";
					document.all("CancelSplitTD").style.display="none";
				}
			}
			return false;
		}
		function doMenuHidden(){
			if(canHidden&&MenuObj){
				MenuObj.style.display="none";
				room_id="";
				rs="";
				cs="";
				room_x="";
				room_y="";
			}
		}
		function doRoomDimChange0(obj){
			if(isNumerics(obj.value)){
				var objs,x;
				x=obj.x.toString();
				for(var i=1;i<parseInt(obj.floor_count)+1;i++){
					objs=document.all("room_dim_"+i+"_"+x);
					if(objs){
						objs.value=obj.value;
					}
				}
			}else{
				alert("请输入数字！");
				obj.focus();
			}
		}
		function doRoomDimChange1(obj){
			if(isNumerics(obj.value)){
				var objs,y;
				y=obj.y.toString();
				for(var i=1;i<parseInt(obj.room_count)+1;i++){
					objs=document.all("room_dim_"+y+"_"+i);
					if(objs){
						objs.value=obj.value;
					}
				}
			}else{
				alert("请输入数字！");
				obj.focus();
			}
		}
		function doBuildingDimChange0(obj){
			if(isNumerics(obj.value)){
				var objs,x;
				x=obj.x.toString();
				for(var i=1;i<parseInt(obj.floor_count)+1;i++){
					objs=document.all("building_dim_"+i+"_"+x);
					if(objs){
						objs.value=obj.value;
					}
				}
			}else{
				alert("请输入数字！");
				obj.focus();
			}
		}
		function doBuildingDimChange1(obj){
			if(isNumerics(obj.value)){
				var objs,y;
				y=obj.y.toString();
				for(var i=1;i<parseInt(obj.room_count)+1;i++){
					objs=document.all("building_dim_"+y+"_"+i);
					if(objs){
						objs.value=obj.value;
					}
				}
			}else{
				alert("请输入数字！");
				obj.focus();
			}
		}
		function doBuildingDimChanges(obj){
			if(!isNumerics(obj.value)){
				obj.value=0;
			}
		}
		function doRoomDimChanges(obj){
			if(!isNumerics(obj.value)){
				obj.value=0;
			}
		}
		function doRoomModelChange(obj){
			var objs,x;
			x=obj.x.toString();
			for(var i=1;i<parseInt(obj.floor_count)+1;i++){
				objs=document.all("room_model_"+i+"_"+x);
				if(objs){
					objs.options[obj.selectedIndex].selected=true;
				}
			}
		}
		function doWGFChange0(obj){
			var objs,x;
			x=obj.x.toString();
			for(var i=1;i<parseInt(obj.floor_count)+1;i++){
				objs=document.all("wy_price_type_"+i+"_"+x);
				if(objs){
					objs.options[obj.selectedIndex].selected=true;
				}
			}
		}
		function doWGFChange1(obj){
			var objs,y;
			y=obj.y.toString();
			for(var i=1;i<parseInt(obj.floor_count)+1;i++){
				objs=document.all("wy_price_type_"+y+"_"+i);
				if(objs){
					objs.options[obj.selectedIndex].selected=true;
				}
			}
		}
		function doFloorModelChange(obj){
			var objs,y;
			y=obj.y.toString();
			for(var i=1;i<parseInt(obj.room_count)+1;i++){
				objs=document.all("room_model_"+y+"_"+i);
				if(objs){
					objs.options[obj.selectedIndex].selected=true;
				}
			}
		}
		function doBaseSubmit(obj,floor_count){
			document.all("building_modify_submit").disabled=true;
			var i;
			var room_count=parseInt(document.all("room_counts").value);
			var itemArray=new Array(1+floor_count);
			itemArray[0]=new Array("building_name",0,"text","请输入楼栋名称！");
			for(i=0;i<floor_count;i++){
				itemArray[i+1]=new Array("floor_list",i,"text","请输入楼层名称！");
			}
			for(i=0;i<room_count;i++){
				itemArray[i+floor_count+1]=new Array("room_list",i,"text","请输入室号！");
			}
			if(chkForm(itemArray)){
				obj.submit();
			}else{
				document.all("building_modify_submit").disabled=false;
			}
		}
		function doBaseRoomNameChange(obj){
			var objs,objy,x,y,j;
			objy=document.all("floor_list");
			x=parseInt(obj.x.toString()) + 1;
			val=obj.value.toString();
			y="";
			for(var i=1;i<parseInt(obj.floor_count)+1;i++){
				if(objy[0]){
					for(j=0;j<objy.length;j++){
						if(objy[j].y.toString()==i.toString()){
							y=objy[j].value;
							break;
						}else{
							y="";
						}
					}
				}else{
					if(objy.y.toString()==i.toString()){
						y=objy.value
					}else{
						y=""
					}
				}
				
				objs=document.all("room_name_"+i+"_"+x);
				if(objs){
					objs.value=y.toString()+""+val.toString();
				}
			}
		}
		
		function doBaseFloorNameChange(obj){
			var objs,objx,y,x,j;
			objx=document.all("room_list");
			y=obj.y.toString();
			val=obj.value.toString();
			x="";
			for(var i=1;i<parseInt(obj.room_count)+1;i++){
				if(objx[0]){
					for(j=0;j<objx.length;j++){
						if(objx[j].x.toString()==(i-1).toString()){
							x=objx[j].value;
							break;
						}else{
							x="";
						}
					}
				}else{
					if(objx.x.toString()==(i-1).toString()){
						x=objx.value
					}else{
						x=""
					}
				}
				
				objs=document.all("room_name_"+y+"_"+i);
				if(objs){
					objs.value=val.toString()+""+x.toString();
				}
			}
		}
		
		var roomDiv=document.all("room_info");
		function document.body.onscroll(){
			if(document.all("room_info")){
				if(document.body.scrollTop<20){
					roomDiv.style.pixelTop=document.body.scrollTop+(20-document.body.scrollTop);
				}else{
					roomDiv.style.pixelTop=document.body.scrollTop;
				}
			}
		}

		function changeBgColor(obj,color){
			obj.style.backgroundColor=color;
		}
		function changeBorderColor(obj,color){
			obj.style.borderColor=color;
		}
		function changeBackGroud(obj,str){
			obj.background=str;
}
function isNumerics(strs){
		var x=true;
		var dot=0;
		var z=strs.toString();
			if(z.substring(0,1)=="+"||z.substring(0,1)=="-"){
				z=z.substring(1,z.length);
			}
			for(var i=0;i<z.length;i++){
				if(!((parseInt(z.substring(i,i+1))>=0&&parseInt(z.substring(i,i+1))<=9)||(z.substring(i,i+1)=="."&&dot==0))){
					x=false;
				}else if(z.substring(i,i+1)=="."&&dot==0){
					dot=1;
				}
			}
		return x;
	}
	//取小数位数
	function formatNumeric(str,o){
		var z=str;
		var x=isNumerics(z);
		var y=isNumerics(o);
		var v=10;
		for(j=0;j<parseInt(o)-1;j++){
			v*=10;
		}
		if(x&&y){
			z=parseFloat(z)*v;
			if(z.toString().indexOf(".")>0)
				z=(z.toString()).substring(0,(z.toString()).indexOf(".")).toString();
			else
				z=z.toString();
			z=z.substring(0,z.length-o)+"."+z.substring(z.length-o,z.length);
		}
		return z;
	}
	//日期加减法
	function doDateAdd(str,ymd,num){
		var i=0;
		var MonthDays=new Array(31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31);
		var date=str.split("-");
		if(ymd=="d"){
			if(num>0){
				date[2]=parseInt(date[2])+num;
				for(;;){
					if(parseInt(date[2]/MonthDays[parseInt(date[1])])>0){
						date[1]=parseInt(date[1])+1;
						if(parseInt(date[1])>12){
							date[0]=parseInt(date[0])+1;
							date[1]=parseInt(date[1])-12;
						}
						date[2]=parseInt(date[2])-MonthDays[parseInt(date[1])];
					}else{
						break;
					}
				}
			}
			if(num<0){
				date[2]=parseInt(date[2])+num;
				for(;;){
					if(parseInt(date[2]/MonthDays[parseInt(date[1])])<0){
						date[1]=parseInt(date[1])-1;
						if(parseInt(date[1])<1){
							date[0]=parseInt(date[0])-1;
							date[1]=parseInt(date[1])+12;
						}
						date[2]=parseInt(date[2])+MonthDays[parseInt(date[1])];
					}else{
						break;
					}
				}
			}
		}
		if(ymd=="y"){
			if(num>0){
				date[0]=date[0]+num;
			}
			if(num<0){
				date[0]=date[0]+num;
			}
		}
		if(ymd=="m"){
			if(num>0){
				date[1]=parseInt(date[1])+num;
				for(;;){
					if(parseInt(date[1])>12){
						date[0]=parseInt(date[0])+1
						date[1]=parseInt(date[1])-12;
					}else{
						break;
					}
				}
			}
			if(num<0){
				date[1]=parseInt(date[1])+num;
				for(;;){
					if(parseInt(date[1])/(12)<0){
						date[0]=parseInt(date[0])-1
						date[1]=parseInt(date[1])+12;
					}else{
						break;
					}
				}
			}
		}
		var str1=date[0].toString();
		if(date[1]<0){
			var str2=(0-parseInt(date[1])).toString();
		}else{
			var str2=date[1].toString();
		}
		if(date[2]<0){
			var str3=(0-parseInt(date[2])).toString();
		}else{
			var str3=date[2].toString();
		}
		return str1+"-"+str2+"-"+str3;
	}
	function PageTo(url){
		window.location.href=url;
	}

		</script>
	</body>
</HTML>
