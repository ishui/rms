<%@ Register TagPrefix="uc1" TagName="UCBiddingSupplierModify" Src="UCBiddingSupplierModify.ascx" %>
<%@ Page language="c#" Inherits="RmsPM.Web.BiddingManage.BiddingSupplierModify" CodeFile="BiddingSupplierModify.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>BiddingSupplierModify</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<Script language="javascript">
<!--

function openSelectSupplier(){
	var strURL = '../SelectBox/SelectSupplier.aspx?ProjectCode=' + Form1.HideProjectCode.value;
	
	var theWin = OpenMiddleWindow(strURL,'选择供应商' );
	theWin.focus();
}

function DoSelectSupplierReturn(strCode,strName){
	document.all("UCBiddingSupplierModify1_HideSupplierCode").value = strCode;
	document.all("UCBiddingSupplierModify1_txtSupplierName").value = strName;
}

function doToModify(){
	var strURL = './BiddingSupplierModify.aspx?BiddingPrejudicationCode=' + Form1.HideBiddingPrejudicationCode.value;
	
	strURL += '&BiddingSupplierCode=' + Form1.HideBiddingSupplierCode.value;
	
	strURL += '&DoType=SingleModify';
	
	strURL += '&State=<%=""+Request.QueryString["State"]%>';
	
	window.location.href=strURL;
}

//-->
		</Script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">
						<asp:Label id="LabelTitle" runat="server">预审单位</asp:Label></td>
				</tr>
				<tr>
					<td class="tools-area">
						<IMG src="../images/btn_li.gif" align="absMiddle">&nbsp; <INPUT type="submit" value="确定" class="button" id="btnSave" name="btnSave" runat="server" onserverclick="btnSave_ServerClick"><INPUT class="button" id="btnToModify" type="button" value="编辑" name="btnToModify" runat="server" onclick="doToModify();return false;">&nbsp;
						<INPUT type="button" value="删除" class="button" id="btnDelete" name="btnDelete" runat="server" onserverclick="btnDelete_ServerClick">&nbsp;
						<INPUT type="button" value="关闭" class="button" onclick="window.close();">
					</td>
				</tr>
			</table>
			<table height="100%" cellSpacing="0" cellPadding="4" width="100%" border="0">
				<tr>
					<td vAlign="top">
						<uc1:UCBiddingSupplierModify id="UCBiddingSupplierModify1" runat="server"></uc1:UCBiddingSupplierModify>
					</td>
				</tr>
			</table>
			<INPUT type="hidden" id="HideProjectCode" name="HideProjectCode" runat="server"><INPUT id="HideBiddingPrejudicationCode" type="hidden" name="HideBiddingPrejudicationCode"
				runat="server"><INPUT id="HideBiddingSupplierCode" type="hidden" name="HideBiddingSupplierCode" runat="server">
		</form>
	</body>
</HTML>
