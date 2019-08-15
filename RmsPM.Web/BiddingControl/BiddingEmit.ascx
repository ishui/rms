<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BiddingEmit.ascx.cs" Inherits="BiddingControl_BiddingEmit" %>
<%@ Register TagPrefix="cc1" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Register Src="../UserControls/attachmentadd.ascx"  TagPrefix="uc1" TagName="attachmentadd"%>
<%@ Register Src="../UserControls/AttachMentList.ascx" TagPrefix="uc1" TagName="AttachMentList" %>

<div id="OperableDiv" runat="server">
	<table class="form" cellSpacing="0" cellPadding="0" width="100%" align="center" border="1">
		<tr>
			<td class="form-item">编&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;号：</td>
			<td><INPUT id="txtEmitNumber" type="text" name="txtEmitNumber" runat="server" class="input-nember"
					onblur="BiddingEmitCheckMoney(this);" style="WIDTH: 104px" size="12" readOnly><FONT face="宋体">&nbsp;<FONT color="#cc0066">*</FONT></FONT></td>
			<td class="form-item">发标日期：</td>
			<td>
				<cc1:calendar id="txtEmitDate" runat="server" CalendarResource="../Images/CalendarResource/" CalendarMode="All"></cc1:calendar></td>
		</tr>
		<tr>
			<td class="form-item">截止日期：</td>
			<td>
				<cc1:calendar id="txtEndDate" runat="server" CalendarResource="../Images/CalendarResource/" CalendarMode="All"></cc1:calendar><FONT face="宋体"></FONT></td>
			<td class="form-item">开标日期：</td>
			<td>
				<cc1:calendar id="txtPrejudicationDate" runat="server" CalendarResource="../Images/CalendarResource/"
					CalendarMode="All"></cc1:calendar></td>
		</tr>
	</table>
    <table class="tree" width="100%">
        <tr style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
            border-bottom: 1px solid">
            <td align="left">
                招标内容要求:
            </td>
            <td>
                <asp:TextBox ID="txtTotalRemark" runat="server" Width="100%" Height="80px" TextMode="MultiLine"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 81px" align="left">
                相关附件:
            </td>
            <td>
                <uc1:attachmentadd ID="AttachMentAdd1" runat="server"></uc1:attachmentadd>
            </td>
        </tr>
    </table>
    &nbsp;
</div>
