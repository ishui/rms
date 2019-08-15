<%@ Reference Control="~/workflowcontrol/workflowtoolbar.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AttachMentList" Src="../UserControls/AttachMentList.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AttachMentAdd" Src="../UserControls/AttachMentAdd.ascx" %>
<%@ Control Language="c#" Inherits="RmsPM.Web.BiddingManage.BiddingPrejudicationModify" CodeFile="BiddingPrejudicationModify.ascx.cs" %>
<div id="OperableDiv" runat="server">
	<table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
		<tr>
			<td class="blackbordertd" width="25%">&nbsp;&nbsp;&nbsp;项目名称：</td>
			<td class="blackbordertdcontent" id="txtProjectName" width="45%" runat="server">&nbsp;</td>
			<td class="blackbordertd" width="15%">&nbsp;&nbsp;&nbsp;编&nbsp;&nbsp;&nbsp;&nbsp;号：</td>
			<td class="blackbordertdcontent" width="15%"><INPUT class="input-nember" id="txtNumber" name="txtNumber" type="text" runat="server"
					onblur="BiddingPrejudicationNemberCheckMoney(this);" style="WIDTH: 97px" size="10">
				<FONT face="宋体" color="#cc0066">*</FONT></td>
		</tr>
		<tr>
			<td class="blackbordertd">&nbsp;&nbsp;&nbsp;拟招标标段：</td>
			<td class="blackbordertdcontent" id="txtBiddingTitle" runat="server">&nbsp;</td>
			<td class="blackbordertd">&nbsp;&nbsp;&nbsp;计划发标日期：</td>
			<td class="blackbordertdcontent" id="txtEmitDate" runat="server">&nbsp;</td>
		</tr>
		<tr>
			<td class="blackbordertd" colSpan="2">&nbsp;&nbsp;&nbsp;1.拟招标标段之工作范围简述(并附招标范围示意图)
				<uc1:AttachMentAdd id="AttachMentAdd1" runat="server" CtrlPath="../UserControls/"></uc1:AttachMentAdd>
				&nbsp;&nbsp;&nbsp;2.拟招标单位之资格要求/条件简述(建议)
				<uc1:AttachMentAdd id="AttachMentAdd2" runat="server" CtrlPath="../UserControls/"></uc1:AttachMentAdd>
				&nbsp;&nbsp;&nbsp;3.参加资格预审的单位名单及资料详见附件:
				<uc1:AttachMentAdd id="AttachMentAdd3" runat="server" CtrlPath="../UserControls/"></uc1:AttachMentAdd>
			</td>
			<td class="blackbordertd" colSpan="2" valign="bottom">其它：</td>
		</tr>
	</table>
</div>
<div id="EyeableDiv" runat="server">
	<TABLE cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
		<TBODY>
			<TR>
				<TD class="blackbordertd" width="25%">
					&nbsp;&nbsp;&nbsp;项目名称：</TD>
				<TD class="blackbordertdcontent" id="tdProjectName" width="45%" runat="server">&nbsp;</TD>
				<TD class="blackbordertd" width="15%">
					&nbsp;&nbsp;&nbsp;编 号：</TD>
				<TD class="blackbordertdcontent" width="15%" runat="server" id="tdNumber">&nbsp;</TD>
			</TR>
			<TR>
				<TD class="blackbordertd">
					&nbsp;&nbsp;&nbsp;拟招标标段：</TD>
				<TD class="blackbordertdcontent" id="tdBiddingTitle" runat="server">&nbsp;</TD>
				<TD class="blackbordertd">
					&nbsp;&nbsp;&nbsp;计划发标日期：</TD>
				<TD class="blackbordertdcontent" id="tdEmitDate" runat="server">&nbsp;</TD>
			</TR>
			<TR>
				<TD class="blackbordertd" colSpan="2">1.拟招标标段之工作范围简述(并附招标范围示意图)<br>
					2.拟招标单位之资格要求/条件简述(建议)<br>
					3.参加资格预审的单位名单及资料详见附件:<br>
				</TD>
				<TD class="blackbordertd" colSpan="2"><FONT face="宋体"></FONT>
					<uc1:AttachMentList id="AttachMentList1" runat="server" CtrlPath="../UserControls/"></uc1:AttachMentList><BR>
					<uc1:AttachMentList id="AttachMentList2" runat="server" CtrlPath="../UserControls/"></uc1:AttachMentList>
					<BR>
					<uc1:AttachMentList id="AttachMentList3" runat="server" CtrlPath="../UserControls/"></uc1:AttachMentList>其它：</TD>
			</TR>
		</TBODY>
	</TABLE>
</div>
<script language="javascript">
function BiddingPrejudicationCheckSubmit()
{
	//if(document.all("<%=ClientID%>_txtNumber").value == "")
	//{
	//	alert('编号必须填写');
       // return false;
	//}
	return true;
}
function BiddingPrejudicationNemberCheckMoney(obj)
{
	/*if(obj.value.length>0)
	{
		if(obj.value.match("^[0-9]+(\.[0-9]+)?$")==null)
		{
			obj.select();
			obj.focus();
			alert("编号请输入数字");
			obj.select();
			return false;
		}
	}*/
	//return true;				
}
function BiddingPrejudicationOpenSupplierPage(code,state,selectstate)
{
	OpenLargeWindow('BiddingSupplierList.aspx?BiddingPrejudicationCode='+code+'&State='+state+'&Select='+selectstate,'投标单位选择');
}
</script>
