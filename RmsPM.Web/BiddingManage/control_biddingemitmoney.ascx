<%@ Control Language="c#" Inherits="RmsPM.Web.BiddingManage.Control_BiddingEmitMoney" CodeFile="Control_BiddingEmitMoney.ascx.cs" %>
<table cellSpacing="0" cellPadding="0" width="100%" align="center" border="1">
	<tr><asp:label id="Lb_PjMoney" runat="server" Visible="false"></asp:label>
		<td class="blackbordertdcontent" style="WIDTH: 15%">暂定金额:</td>
		<td class="blackbordertd" style="WIDTH: 35%"><FONT face="宋体">&nbsp; </FONT>
			<asp:label id="Lb_ObMoney" runat="server"></asp:label></td>
		<td class="blackbordertdcontent" style="WIDTH: 100px">实际金额:</td>
		<td class="blackbordertd"><FONT face="宋体">&nbsp; </FONT>
			<asp:label id="Lb_FactMoney" runat="server"></asp:label></td>
	</tr>
</table>
