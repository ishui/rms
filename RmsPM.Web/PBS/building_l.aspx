<%@ Page language="c#" Inherits="RmsPM.Web.PBS.Building_l" CodeFile="Building_l.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Building_l</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="map.js"></SCRIPT>
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
	</HEAD>
	<body scroll="no" leftmargin="0" rightmargin="0" topmargin="0"
		bottommargin="0" onload="winload();" onresize="bodyResize();">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td bgColor="#e4eff6" height="6"></td>
				</tr>
				<tr>
					<td height="25">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="topic" background="../images/topic_bg.gif"><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle">项目管理>项目信息>楼栋列表>分布图
								</td>
								<td width="9"><IMG height="25" src="../images/topic_corr.gif" width="9"></td>
							</tr>
						</table>
					</td>
				</tr>
				<TR>
					<TD class="tools-area" valign="top">
						<IMG src="../images/btn_li.gif" align="absMiddle"> <input class="button" id="btnCreateArea" onclick="doCreateArea(Form1.txtProjectCode.value,Form1.txtParentCode.value);return false;"
							type="button" runat=server value="新增区域" name="btnCreateArea"> <input class="button" id="btnCreateBuilding" onclick="doCreateBuilding(Form1.txtProjectCode.value,Form1.txtParentCode.value);return false;"
							type="button" runat=server value="新增楼栋" name="btnCreateBuilding"> <input class="button" id="btnChangeLocation" onclick="doChangeLocation(Form1.txtProjectCode.value,Form1.txtParentCode.value);return false;"
							type="button" runat=server value="修改位置" name="btnChangeLocation"> <input class="button" id="btnUpload" onclick="doUploadMap(Form1.txtProjectCode.value,Form1.txtParentCode.value);return false;"
							type="button" value="上传分布图" name="btnUpload" runat="server"> <IMG src="../images/btn_li.gif" align="absMiddle">
						<input class="button" id="btnView" onclick="GotoArea(Form1.txtParentCode.value);return false;"
							type="button" value="切换到区域信息" name="btnView"> <input class="button" type="button" value="楼栋列表" name="btnGotoBuildingList" onclick="GotoBuildingList(Form1.txtProjectCode.value);"></TD>
					</TD>
				</TR>
				<tr id="trArea" runat="server" style="DISPLAY:none">
					<td class="table" vAlign="top">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD class="note">区域名称：<asp:label id="lblAreaName" Runat="server"></asp:label></TD>
							</TR>
						</table>
					</td>
				</tr>
				<tr height="100%">
					<td valign="top" id="group_map">
						<div class="table" style="Z-INDEX:1; OVERFLOW:auto; WIDTH:100%; POSITION:absolute; HEIGHT:100%;"
							id="divPhoto">
							<IMG src="" id="imgMain" name="imgMain" runat="server">
							<asp:DataList id="dlBuild" runat="server">
								<ItemTemplate>
									<div id="DivBuilding" style='position:absolute; left:<%# DataBinder.Eval(Container.DataItem, "ObjLeft") %>px; top:<%# DataBinder.Eval(Container.DataItem, "ObjTop") %>; width:10; z-index:2; background-color: #FFFFFF; layer-background-color: #FFFFFF; border: 1px none #000000;'>
										<table width="100%" border="1" cellpadding="2" cellspacing="0" bordercolor="#6396D6" bgcolor="#F3F5F8"
											class="cursorHand" style="border-collapse: collapse;cursor:hand" onMouseOver="changeBgColor(this,'#D0E8FF');"
											onMouseOut="changeBgColor(this,'#F3F5F8');">
											<tr>
												<td align="center" valign="baseline" nowrap onClick='<%# DataBinder.Eval(Container.DataItem, "Events") %>;return false;' bgcolor='<%# DataBinder.Eval(Container.DataItem, "Color") %>'><%# DataBinder.Eval(Container.DataItem, "Desc") %></td>
											</tr>
										</table>
									</div>
								</ItemTemplate>
							</asp:DataList>
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
			<div id="legend" style="BORDER-RIGHT: #a3afb8 1px solid; BORDER-TOP: #a3afb8 1px solid; Z-INDEX: 1;BORDER-LEFT: #a3afb8 1px solid; BORDER-BOTTOM: #a3afb8 1px solid; POSITION: absolute; BACKGROUND-COLOR: #ffffff; layer-background-color: #6699CC">
				<!--<table cellSpacing="5" cellPadding="0" border="0">
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
					</asp:repeater></table>-->
			</div>
			<input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server" style="WIDTH: 2px; HEIGHT: 22px"
				size="1"> <input id="txtParentCode" type="hidden" name="txtParentCode" runat="server" style="WIDTH: 4px; HEIGHT: 22px"
				size="1">
		</form>
		<Script language="javascript">

	//查看区域信息
	function GotoArea(code){
		window.location.href = "../PBS/PBSBuildInfo.aspx?BuildingCode="+code + "&FromUrl=" + window.location.href;
	}

	function winload()
	{
		if (Form1.txtParentCode.value == "")
		{
			Form1.btnView.style.display = "none";
		}
		else
		{
			Form1.btnView.style.display = "";
		}

		ChangeLegendLocation();		
	}

	function ChangeLegendLocation()
	{
		var legend = document.all.legend;
		legend.style.left = document.body.offsetWidth - 120 - 5;
		legend.style.top = document.all.divPhoto.offsetTop + 5;
	}
	
	function bodyResize()
	{
		ChangeLegendLocation();		
	}
	
		</Script>
	</body>
</HTML>
