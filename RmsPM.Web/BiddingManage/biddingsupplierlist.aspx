<%@ Register TagPrefix="uc1" TagName="UCBiddingSupplierList" Src="UCBiddingSupplierList.ascx" %>
<%@ Page language="c#" Inherits="RmsPM.Web.BiddingManage.BiddingSupplierList" CodeFile="BiddingSupplierList.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="UCBiddingSupplierModify" Src="UCBiddingSupplierModify.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>BiddingSupplierList</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<SCRIPT language="javascript" src="../Images/checkbox.js"></SCRIPT>
		<Script language="javascript">
<!--

function doBiddingSupplierModify(strCellCode,strType,strSupplierCode){
	var strState = '<%=""+Request.QueryString["State"]%>';
	var strURL = '';
	
//	if( 'edit'==strState ){
		
//		strURL = './BiddingSupplierModify.aspx?BiddingPrejudicationCode=' + Form1.HideBiddingPrejudicationCode.value;
		
//		strURL += '&BiddingSupplierCode=' + strCellCode;
		
//		strURL += '&DoType=' + strType;
		
//		strURL += '&State=' + strState;
		
//		var theWin = OpenMiddleWindow(strURL,'doBiddingSupplierModify');
//		theWin.focus();
	
//	}else{
		strURL = '../Supplier/SupplierInfo.aspx?SupplierCode=' + strSupplierCode;
		
		var theWin = OpenFullWindow(strURL,'供应商信息');
		theWin.focus();
//	}

}

function openSelectSupplier(){
	var strURL = '../SelectBox/SelectSupplier.aspx?ProjectCode=<%=project.ProjectCode%>';
	
	var theWin = OpenMiddleWindow(strURL,'选择供应商' );
	theWin.focus();
}

function DoSelectSupplierReturn(strCode,strName){
	document.all("UCBiddingSupplierModify1_HideSupplierCode").value = strCode;
	document.all("UCBiddingSupplierModify1_txtSupplierName").value = strName;
}

//-->
		</Script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">
						<asp:Label id="LabelTitle" runat="server">预审单位名单</asp:Label></td>
				</tr>
				<tr>
					<td class="tools-area">
						<IMG src="../images/btn_li.gif" align="absMiddle">&nbsp;<INPUT class="button" id="btnModify" type="button" value=" 编辑 " name="btnModify" runat="server" onserverclick="btnModify_ServerClick">&nbsp;
						<INPUT class="button" id="btnSave" type="button" value="预审通过" name="btnSave" runat="server" onserverclick="btnSave_ServerClick">&nbsp;
						<INPUT type="button" value="关闭" class="button" onclick="window.close();return false;">
					</td>
				</tr>
			</table>
			<table height="100%" cellSpacing="0" cellPadding="4" width="100%" border="0">
				<tr>
					<td vAlign="top">
						<table id="TabAdd" runat="server" cellSpacing="0" cellPadding="4" border="0">
							<tr>
								<td>
									<uc1:UCBiddingSupplierModify id="UCBiddingSupplierModify1" runat="server"></uc1:UCBiddingSupplierModify>
								</td>
								<td><INPUT type="button" value=" 添 加 " class="button" id="btnAdd" name="btnAdd" runat="server" onserverclick="btnAdd_ServerClick"></td>
							</tr>
						</table>
						<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
							<uc1:UCBiddingSupplierList id="UCBiddingSupplierList1" runat="server"></uc1:UCBiddingSupplierList>
						</div>
					</td>
				</tr>
			</table>
			<INPUT type="hidden" id="HideBiddingPrejudicationCode" name="HideBiddingPrejudicationCode"
				runat="server">
		</form>
	</body>
</HTML>
