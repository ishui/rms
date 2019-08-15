<%@ Reference Control="~/workflowcontrol/workflowtoolbar.ascx" %>
<%@ Control Language="c#" Inherits="RmsPM.Web.BiddingManage.UCBiddingSupplierModify" CodeFile="UCBiddingSupplierModify.ascx.cs" %>
<div id="OperableDiv" runat="server">
	<table cellSpacing="0" cellPadding="4" width="100%" align="center" border="1">
		<TR>
			<TD class="blackbordertdcontent" align="right" width="100">供应商：</TD>
			<TD class="blackbordertd"><INPUT id="txtSupplierName" type="text" name="txtSupplierName" class="input" readonly size="35"
					runat="server"><FONT face="宋体" color="red">&nbsp;*</FONT><INPUT id="HideSupplierCode" type="hidden" name="HideSupplierCode" runat="server"><FONT face="宋体">&nbsp;</FONT><IMG style="CURSOR: hand" onclick="openSelectSupplier();return false;" src="../images/ToolsItemSearch.gif"></TD>
			<td class="blackbordertdcontent" width="100" align="right" id="txtPerson" runat="server">提名人：</td>
			<td class="blackbordertd"><INPUT id="txtNominateUser" type="text" runat="server" class="input" NAME="txtNominateUser"></td>
			<td class="blackbordertd"><FONT face="宋体"><INPUT class="button" id="btnAdd" type="button" value=" 添 加 " name="btnAdd" runat="server" onserverclick="btnAdd_ServerClick"></FONT></td>
		</TR>
	</table>
</div>
<div id="EyeableDiv" runat="server">
	<table class="blackbordertable" cellSpacing="0" cellPadding="4" width="100%" align="center"
		border="1">
		<TR>
			<TD class="blackbordertdcontent" align="right" width="100">供应商：</TD>
			<TD id="tdSupplierName" runat="server"><FONT face="宋体"></FONT></TD>
			<td class="blackbordertdcontent" width="100" align="right" id="tdPerson" runat="server">提名人：</td>
			<td runat="server" id="tdNominateUser"><FONT face="宋体"></FONT></td>
		</TR>
	</table>
</div>
<INPUT id="HideBiddingPrejudicationCode" type="hidden"  name="HideBiddingPrejudicationCode"
	runat="server"><INPUT id="HideBiddingSupplierCode" type="hidden" name="HideBiddingSupplierCode" runat="server">
	<script>
		function openSelectSupplier(){
	var strURL = '../SelectBox/SelectSupplier.aspx?ProjectCode=<%=((RmsPM.Web.ProjectInfo)Session["Project"]).ProjectCode%>';
	
	var theWin = OpenMiddleWindow(strURL,'选择供应商' );
	theWin.focus();
}
function doBiddingSupplierModify(strCellCode,strType,strSupplierCode){

	var strURL = '';
	
	strURL = '../Supplier/SupplierInfo.aspx?SupplierCode=' + strSupplierCode;
		
	var theWin = OpenFullWindow(strURL,'供应商信息');
	theWin.focus();

}

function DoSelectSupplierReturn(strCode,strName){
	document.all('<%=SupplierCode %>').value = strCode;
	document.all('<%=SupplierName %>').value = strName;
}
	</script>
