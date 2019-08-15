<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GK_BiddingPrejudicationManage.ascx.cs" Inherits="WorkFlowOperation_GK_BiddingPrejudicationManage" %>
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
	<table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0" id="TABLE1" onclick="return TABLE1_onclick()">
		<tr>
		    <td class="blackbordertd">&nbsp;&nbsp;&nbsp;项目名称：</td>
			<td colspan=3 class="blackbordertdcontent" style="text-align:left;" id="txtProjectName" runat="server">&nbsp;</td>
		   
			<td align="center" class="blackbordertd" >&nbsp;&nbsp;&nbsp;记录编号：</td>
			<td class="blackbordertdcontent" style="text-align:left;" >&nbsp;&nbsp;<INPUT class="input-nember" id="txtNumber" name="txtNumber" type="text" runat="server"
					onblur="BiddingPrejudicationNemberCheckMoney(this);"  size="10">
				<FONT face="宋体" color="#cc0066">*</FONT></td>
		</tr>
		<tr>
			 <td class="blackbordertd" style="width:10%;">&nbsp;&nbsp;&nbsp;招标项目：</td>
			<td class="blackbordertdcontent"  id="txtBiddingTitle" style="width:20%;text-align:left;" runat="server">&nbsp;</td>	
			<td class="blackbordertd" align=center style="width:10%;">预估费用：</td>
			<TD class="blackbordertdcontent" id="TxtMoney" style="width:20%;text-align:left;"  runat="server">&nbsp;</TD>
            <td align=center class="blackbordertd">计划发标日期：</td>
            <td class="blackbordertdcontent" style="text-align:left;">
                &nbsp;&nbsp;<cc2:Calendar ID="TxtEmitDate" runat="server" ReadOnly=false
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
			    <TD class="blackbordertd" >
					&nbsp;&nbsp;&nbsp;项目名称：</TD>
				<TD colspan=3 class="blackbordertdcontent" style="text-align:left;" id="tdProjectName" runat="server" >&nbsp;</TD>
				 <TD align="center" class="blackbordertd" >
					&nbsp;&nbsp;&nbsp;记录编号：</TD>
				<TD class="blackbordertdcontent" style="text-align:left;"  runat="server" id="tdNumber">&nbsp;</TD>
			</TR>
			<TR>
				
				<TD class="blackbordertd" style="width:10%;">
					&nbsp;&nbsp;&nbsp;招标项目：</TD>
				<TD class="blackbordertdcontent" id="tdBiddingTitle" runat="server" style="width:20%;text-align:left;">&nbsp;</TD>
				
				<TD align="center" class="blackbordertd" style="width:10%;">
					预估费用：</TD>
				<TD class="blackbordertdcontent" id="TDMoney" runat="server" style="width:20%;text-align:left;">&nbsp;</TD>
				 <td align="center" class="blackbordertd" nowrap>计划发标日期：</td>
                <td class="blackbordertdcontent" style="text-align:left;" runat="server" id="tdEmitDate">
                    &nbsp;
                </td>
			</TR>
			
		</TBODY>
	</TABLE>
</div></TD>
</TR>
        <tr>
			<td class="blackbordertd" style="width:60%;"  colSpan="4">&nbsp;&nbsp;&nbsp;1.拟招标标段之工作范围简述(并附招标范围示意图)：<br>
                <uc1:AttachMentList id="AttachMentList1" runat="server" CtrlPath="../UserControls/"></uc1:AttachMentList>
				<uc1:AttachMentAdd id="AttachMentAdd1" runat="server" CtrlPath="../UserControls/"></uc1:AttachMentAdd>
				&nbsp;&nbsp;&nbsp;2.拟招标单位之资格要求/条件简述(建议)：<br>
                <uc1:AttachMentList id="AttachMentList2" runat="server" CtrlPath="../UserControls/"></uc1:AttachMentList>
				<uc1:AttachMentAdd id="AttachMentAdd2" runat="server" CtrlPath="../UserControls/"></uc1:AttachMentAdd>
				&nbsp;&nbsp;&nbsp;3.参加资格预审的单位名单及资料详见附件：<br>
                <uc1:AttachMentList id="AttachMentList3" runat="server" CtrlPath="../UserControls/"></uc1:AttachMentList>
				<uc1:AttachMentAdd id="AttachMentAdd3" runat="server" CtrlPath="../UserControls/"></uc1:AttachMentAdd>
				
			</td>
			
		    <td class="blackbordertdcontent"   valign="top" align="left">
		   
		    备注：<br>
			    <textarea id="TxtRemark" visible="false" style="width:95%; height:80%;" rows="5" runat="server"></textarea>
		        <asp:label id="lblRemark" Visible="false" Runat="server"></asp:label>&nbsp;
			</td>
		</tr>
		
		
<tr>
    <td colSpan="5"><uc1:ucbiddingsuppliermodify id="UCBiddingSupplierModify1" runat="server"></uc1:ucbiddingsuppliermodify><uc1:ucbiddingsupplierlist id="UCBiddingSupplierList1" runat="server"></uc1:ucbiddingsupplierlist></td>
</tr>
</TABLE>