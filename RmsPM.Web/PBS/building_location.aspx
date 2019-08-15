<%@ Page language="c#" Inherits="RmsPM.Web.PBS.Building_Location" CodeFile="Building_Location.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Building_Location</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body topmargin="0" leftmargin="0" scroll="no" onscroll="window_onscroll();">
		<form id="Form1" method="post" runat="server" action="asdasd">
		<table border="0" cellspacing="0" cellpadding="0" width="100%" height="100%">
				<tr>
					<td bgColor="#e4eff6" height="6"></td>
				</tr>
				<tr>
					<td height="25">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="topic" background="../images/topic_bg.gif"><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle">产品管理 
									- 项目导图 
								</td>
								<td width="9"><IMG height="25" src="../images/topic_corr.gif" width="9"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<TD class="tools-area" valign="top">
						<IMG src="../images/btn_li.gif" align="absMiddle">
						<input class="button" id="btnSave" type="button" value="保存位置" name="btnSave" runat="server" onclick="doSaveLocation(Form1.hidPprojectCode.value,Form1.hidParentCode.value);return false;">
						<input class="button" id="btnUpload" onclick="doUploadMap(Form1.hidPprojectCode.value,Form1.hidParentCode.value);return false;"
							type="button" value="上传平面图" name="btnUpload" runat="server">
					</td>
				</tr>
				<tr>
					<td class="table">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="note">提示：点击楼栋名称，拖到指定位置后再点击</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr height="100%">
					<td valign="top" id="group_map">
						<div style="Z-INDEX:1; OVERFLOW: auto; POSITION: absolute;WIDTH: 100%; HEIGHT: 100%" id="divPhoto">
							<IMG src="" id="imgMain" name="imgMain" runat="server">
							<asp:DataList id="dlBuild" runat="server">
								<HeaderTemplate>
								</HeaderTemplate>
								<FooterTemplate>
								</FooterTemplate>
								<ItemTemplate>
								<div id="DivBuilding" style="position:absolute; left:<%# DataBinder.Eval(Container.DataItem, "ObjLeft") %>px; top:<%# DataBinder.Eval(Container.DataItem, "ObjTop") %>; width:10; z-index:2; background-color: #FFFFFF; layer-background-color: #FFFFFF; border: 1px none #000000;" onMouseDown="doMoves(this, document.all.divPhoto);" building_id="<%# DataBinder.Eval(Container.DataItem, "BuildingCode") %>" building_type="building">
									<table width="100%" border="1" cellpadding="2" cellspacing="0" bordercolor="#6396D6" bgcolor="#F3F5F8"
										class="cursorHand" style="border-collapse: collapse;cursor:hand" onMouseOver="changeBgColor(this,'#D0E8FF');"
										onMouseOut="changeBgColor(this,'#F3F5F8');">
										<tr>
											<td align="center" valign="baseline" nowrap onClick="<%# DataBinder.Eval(Container.DataItem, "Events") %>;return false;" bgcolor='<%# DataBinder.Eval(Container.DataItem, "Color") %>'><%# DataBinder.Eval(Container.DataItem, "BuildingName") %></td>
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
			<input id="hidPprojectCode" type="hidden" name="hidPprojectCode" runat="server" style="WIDTH: 2px; HEIGHT: 22px"
										size="1" value=""> <input id="hidParentCode" type="hidden" name="hidParentCode" runat="server" style="WIDTH: 4px; HEIGHT: 22px"
										size="1" value="">
		</form>
		<SCRIPT language="javascript" src="map.js"></SCRIPT>
		<script language="javascript">
			//重定义map.js中的左边距、上边距
			MarginX = document.all.divPhoto.offsetLeft + 5;
			MarginY = document.all.divPhoto.offsetTop + 5;

function  window_onscroll()
{
	if (document.all('legend')){
		legend.style.top= basetb.offsetTop +document.body.scrollTop+30;
		if (basetb.offsetLeft+ document.body.scrollLeft-80>=0)
			legend.style.left= basetb.offsetLeft+ document.body.scrollLeft-150 
		else
			legend.style.left= 0;
		
	}
}
		</script>
	</body>
</HTML>
