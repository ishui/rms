<%@ Register TagPrefix="uc1" TagName="UCBuildingStation" Src="UCBuildingStation.ascx" %>
<%@ Page language="c#" Inherits="RmsPM.Web.PBS.BuildingStationModify" CodeFile="BuildingStationModify.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>楼栋位置</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<script language="javascript">
<!--

function toModify(dotype){
	var strBuildingCode = Form1.HideBuildingCode.value;
	var strBuildingStationCode = Form1.HideBuildingStationCode.value;
	
	var NowTime = new Date();
	
	var strURL = './BuildingStationModify.aspx?BuildingCode=' + strBuildingCode;
	
	strURL += '&CellCode=' + strBuildingStationCode;
	
	strURL += '&DoType=' + dotype;
	
	strURL += '&ct_'+ NowTime.getFullYear().toString() + '_' + NowTime.getMonth().toString() + '_' + NowTime.getDay().toString() + '_' + NowTime.getHours().toString() + '_' + NowTime.getMinutes().toString() + '_' + NowTime.getSeconds().toString() + '_' + NowTime.getMilliseconds().toString();
	
	window.location.href = strURL;
}

//-->
		</script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">
						<asp:Label id="LabelTitle" runat="server">楼栋位置</asp:Label></td>
				</tr>
				<tr id="trToolBar" runat="server">
					<td class="tools-area">
						<INPUT class="button" id="btnSave" type="submit" value="确 定" name="btnSave" runat="server" onserverclick="btnSave_ServerClick"><INPUT class="button" id="btnToModify" type="button" value="修 改" name="btnToModify" runat="server" onclick="toModify('SingleModify');return false;">
						&nbsp;&nbsp; <input class="button" id="btnDelete" onclick="javascript:if(!window.confirm('确实要删除这条记录吗？')) return false;"
							type="button" value="删 除" name="btnDelete" runat="server" onserverclick="btnDelete_ServerClick"> &nbsp; <input class="button" id="btnCancel" onclick="javascript:self.close()" type="button" value="取 消"
							name="btnCancel">
					</td>
				</tr>
			</table>
			<table height="100%" cellSpacing="0" cellPadding="4" width="100%" border="0">
				<tr>
					<td vAlign="top">
						<uc1:UCBuildingStation id="UCBuildingStation1" runat="server"></uc1:UCBuildingStation>
					</td>
				</tr>
			</table>
			<INPUT id="HideBuildingCode" type="hidden" name="HideBuildingCode" runat="server"><INPUT type="hidden" id="HideBuildingStationCode" name="HideBuildingStationCode" runat="server">
		</form>
	</body>
</HTML>
