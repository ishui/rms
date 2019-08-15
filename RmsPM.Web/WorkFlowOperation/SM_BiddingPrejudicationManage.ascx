<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SM_BiddingPrejudicationManage.ascx.cs" Inherits="WorkFlowOperation_SM_BiddingPrejudicationManage" %>
<%@ Register TagPrefix="uc1" TagName="UCBiddingSupplierModify" Src="../BiddingManage/UCBiddingSupplierModify.ascx" %>
<%@ Register TagPrefix="uc1" TagName="UCBiddingSupplierList" Src="../BiddingManage/UCBiddingSupplierList.ascx" %>
<%@ Register TagPrefix="uc1" TagName="BiddingPrejudicationModify" Src="../BiddingManage/BiddingPrejudicationModify.ascx" %>
<%@ Reference Control="~/workflowcontrol/workflowtoolbar.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AttachMentList" Src="../UserControls/AttachMentList.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AttachMentAdd" Src="../UserControls/AttachMentAdd.ascx" %>
<%@ Register TagPrefix="cc2" Namespace="AspWebControl" Assembly="AspWebControl" %>


<TABLE class="blackbordertable" cellSpacing="0" cellPadding="0" width="100%" border="0">

<TR>
    <TD colSpan="5"><div id="OperableDiv" runat="server">
	<table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0" id="TABLE1">
		<tr>
			<td class="blackbordertd" width="25%">&nbsp;&nbsp;&nbsp;项目名称：</td>
			<td class="blackbordertdcontent" id="txtProjectName" width="45%" runat="server">&nbsp;</td>
			<td class="blackbordertd" width="15%">&nbsp;&nbsp;&nbsp;编&nbsp;&nbsp;&nbsp;&nbsp;号：</td>
			<td class="blackbordertdcontent" width="15%"><INPUT class="input-nember" id="txtNumber" name="txtNumber" type="text" runat="server"
					style="WIDTH: 97px" size="10">
				<FONT face="宋体" color="#cc0066">*</FONT></td>
		</tr>
		<tr>
			<td class="blackbordertd">&nbsp;&nbsp;&nbsp;拟招标标段：</td>
			<td class="blackbordertdcontent" id="txtBiddingTitle" runat="server">&nbsp;</td>
			<td class="blackbordertd">&nbsp;&nbsp;&nbsp;计划发标日期：</td>
			<td class="blackbordertdcontent"> 
			    <cc2:Calendar ID="TxtEmitDate" runat="server" ReadOnly=false
                        CalendarMode="All" CalendarResource="../Images/CalendarResource/">
                </cc2:Calendar>&nbsp;
            </td>
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
			
		</TBODY>
	</TABLE>
</div></TD>
</TR>
<tr>
			<td class="blackbordertd" colSpan="3" style="height: 141px">&nbsp;&nbsp;&nbsp;1.拟招标标段之工作范围简述(并附招标范围示意图)：<br>
                <uc1:AttachMentList id="AttachMentList1" runat="server" CtrlPath="../UserControls/"></uc1:AttachMentList>
				<uc1:AttachMentAdd id="AttachMentAdd1" runat="server" CtrlPath="../UserControls/"></uc1:AttachMentAdd>
				&nbsp;&nbsp;&nbsp;2.拟招标单位之资格要求/条件简述(建议)：<br>
                <uc1:AttachMentList id="AttachMentList2" runat="server" CtrlPath="../UserControls/"></uc1:AttachMentList>
				<uc1:AttachMentAdd id="AttachMentAdd2" runat="server" CtrlPath="../UserControls/"></uc1:AttachMentAdd>
				&nbsp;&nbsp;&nbsp;3.参加资格预审的单位名单及资料详见附件：<br>
                <uc1:AttachMentList id="AttachMentList3" runat="server" CtrlPath="../UserControls/"></uc1:AttachMentList>
				<uc1:AttachMentAdd id="AttachMentAdd3" runat="server" CtrlPath="../UserControls/"></uc1:AttachMentAdd>
			</td>
			<td class="blackbordertd" colSpan="3" valign=top>其它：</td>
		</tr>
<tr>
    <td colSpan="5"><input  class="button" id="btnAddPrice" type="button" value=" 明细查看 " visible=false name="btnDel" runat="server"><span id="spMoney" visible="false" runat="server"></span><uc1:ucbiddingsuppliermodify id="UCBiddingSupplierModify1" runat="server"></uc1:ucbiddingsuppliermodify><uc1:ucbiddingsupplierlist id="UCBiddingSupplierList1" runat="server"></uc1:ucbiddingsupplierlist></td>
</tr>
</TABLE>