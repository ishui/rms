<%@ Page language="c#" Inherits="RmsPM.Web.PBS.BuildingFunctionModify" CodeFile="BuildingFunctionModify.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="UCBuildingFunction" Src="UCBuildingFunction.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>¥������</title>
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
	var strBuildingFunctionCode = Form1.HideBuildingFunctionCode.value;
	
	var NowTime = new Date();
	
	var strURL = './BuildingFunctionModify.aspx?BuildingCode=' + strBuildingCode;
	
	strURL += '&CellCode=' + strBuildingFunctionCode;
	
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
						<asp:Label id="LabelTitle" runat="server">¥������</asp:Label></td>
				</tr>
				<tr>
					<td class="tools-area">
						<INPUT class="button" id="btnSave" type="submit" value="ȷ ��" name="btnSave" runat="server" onserverclick="btnSave_ServerClick"><INPUT class="button" id="btnToModify" type="button" value="�� ��" name="btnToModify" runat="server" onclick="toModify('SingleModify');return false;">
						&nbsp;&nbsp; <input id="btnDelete" name="btnDelete" type="button" class="button" value="ɾ ��" onclick="javascript:if(!window.confirm('ȷʵҪɾ��������¼��')) return false;"
							runat="server" onserverclick="btnDelete_ServerClick"> &nbsp; <input id="btnCancel" name="btnCancel" type="button" class="button" value="ȡ ��" onclick="javascript:self.close()">
					</td>
				</tr>
			</table>
			<table height="100%" cellSpacing="0" cellPadding="4" width="100%" border="0">
				<tr>
					<td vAlign="top">
						<uc1:UCBuildingFunction id="UCBuildingFunction1" runat="server"></uc1:UCBuildingFunction>
					</td>
				</tr>
			</table>
			<INPUT type="hidden" id="HideBuildingCode" name="HideBuildingCode" runat="server"><INPUT type="hidden" id="HideBuildingFunctionCode" name="HideBuildingFunctionCode" runat="server">
		</form>
	</body>
</HTML>
