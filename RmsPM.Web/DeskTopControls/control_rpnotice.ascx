<%@ Control Language="c#" Inherits="RmsPM.Web.DeskTopControl.Control_rpNotice" CodeFile="Control_rpNotice.ascx.cs" %>
<head>
<title></title>
</head>
<link href="../images/index.css" type="text/css" rel="stylesheet"/>
<style type="text/css">
.toptxt { color: #ff0000 }
</style>

<table cellspacing="0" cellpadding="0" width="100%" background="images/desktop/bg.gif"	border="0" id="table1">
	<tr>
		<td>
			<IMG  src="Images/desktop/topic_message<%=(IsOther?"2":"")%>.gif"></td>
		<td class="bn" style="PADDING-TOP: 18px" vAlign="top" align="right"><A href="#"></A></td>
		<td vAlign="top" width="25">
			<IMG src="images/desktop/bn_blank.gif"></td>
		<td style="PADDING-TOP: 18px" vAlign="top" width="140"><span id="hylNewNotice" runat="server"> 
		        <a onclick="DoNewNotice('Remind/Notify.aspx?Action=Insert&DocType=<%=(IsOther?"99":"1")%>');return false;"
				href="#"> </span> 
		<IMG src="images/desktop/bn_issuance.gif" border="0"></a>
			<A onclick='GoMore("Remind/NoticeList.aspx?DocType=<%=(IsOther?"99":"1")%>");return false;' href="#">
		<img src="images/desktop/bn_more.gif" border="0"></A>
		</td>
	</tr>
</table>
<table height="100" width="100%" border="0" id="table2">
	<tr vAlign="top">
		<td align="left">
			<table class="txt" cellSpacing="0" cellPadding="0" width="100%" border="0" id="table3">
				<asp:repeater id="rpNotice" Runat="server">
					<ItemTemplate>
						<tr valign="top">
							<td width="20" align="right">-</td>
							<td width="250"><a href="#" class="toptxt" onclick="ModifyNotice(this.code);return false;" code='<%# DataBinder.Eval(Container.DataItem, "NoticeCode") %>' >
									<%# DataBinder.Eval(Container.DataItem, "NoticeClassTitle") %>
								</a>
							</td>
							<td><%# DataBinder.Eval(Container.DataItem, "UpdateDate", "{0:yyyy-MM-dd}")%>&nbsp;&nbsp;<%# RmsPM.BLL.SystemRule.GetUserName(DataBinder.Eval(Container.DataItem, "SubmitPerson").ToString())%></td>
						    <td width="20" class ="toptxt">
						        <%# (UserHasReadThisNotice(DataBinder.Eval(Container.DataItem, "NoticeCode").ToString())) ? "" : "<img src='images/Noticenew.gif'>"%>
							</td>
						</tr>
					</ItemTemplate>
				</asp:repeater></table>
		</td>
	</tr>
</table>