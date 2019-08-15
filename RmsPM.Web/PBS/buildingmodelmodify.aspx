<%@ Page language="c#" Inherits="RmsPM.Web.PBS.BuildingModelModify" CodeFile="BuildingModelModify.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="UCBuildingModel" Src="UCBuildingModel.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>楼栋户型</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<script language="javascript">
<!--

function toModify(dotype){
	var strBuildingCode = Form1.HideBuildingCode.value;
	var strBuildingModelCode = Form1.HideBuildingModelCode.value;
	var strProjectCode= Form1.HideProjectCode.value;
	
	var NowTime = new Date();
	
	var strURL = './BuildingModelModify.aspx?BuildingCode=' + strBuildingCode;
	
	strURL += '&ProjectCode=' + strProjectCode;
	
	strURL += '&CellCode=' + strBuildingModelCode;
	
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
						<asp:Label id="LabelTitle" runat="server">楼栋户型</asp:Label></td>
				</tr>
				<tr>
					<td class="tools-area">
						<INPUT class="button" id="btnSave" type="submit" value="确 定" name="btnSave" runat="server" onserverclick="btnSave_ServerClick"><INPUT class="button" id="btnToModify" type="button" value="修 改" name="btnToModify" runat="server"
							onclick="toModify('SingleModify');return false;"> &nbsp;&nbsp; <input id="btnDelete" name="btnDelete" type="button" class="button" value="删 除" onclick="javascript:if(!window.confirm('确实要删除这条记录吗？')) return false;"
							runat="server" onserverclick="btnDelete_ServerClick"> &nbsp; <input id="btnCancel" name="btnCancel" type="button" class="button" value="取 消" onclick="javascript:self.close()">
					</td>
				</tr>
			</table>
			<table height="100%" cellSpacing="0" cellPadding="4" width="100%" border="0">
				<tr>
					<td vAlign="top">
						<uc1:UCBuildingModel id="UCBuildingModel1" runat="server"></uc1:UCBuildingModel>
					</td>
				</tr>
			</table>
			<INPUT type="hidden" id="HideProjectCode" name="HideProjectCode" runat="server"><INPUT type="hidden" id="HideBuildingCode" name="HideBuildingCode" runat="server"><INPUT type="hidden" id="HideBuildingModelCode" name="HideBuildingModelCode" runat="server">
		</form>
	</body>
</HTML>
