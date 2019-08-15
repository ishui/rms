<%@ Control Language="c#" Inherits="RmsPM.Web.DeskTopControl.Control_rpRemind" CodeFile="Control_rpRemind.ascx.cs" %>
<LINK href="../images/index.css" type="text/css" rel="stylesheet">
<table id="table1" cellSpacing="0" cellPadding="0" width="100%" background="images/desktop/bg.gif"
	border="0">
	<tr>
		<td><IMG height="42" src="images/desktop/topic_awoke.gif" width="126"></td>
		<td style="PADDING-TOP: 18px" vAlign="top" align="right"><!-- <A href="#"><IMG height="17" src="images/desktop/bn_tailor.gif" width="60" border="0"></A> --></td>
		<td vAlign="top" width="25"><!--<IMG height="37" src="images/desktop/bn_blank.gif" width="25">--></td>
		<td style="PADDING-TOP: 18px" vAlign="top" width="70"><!--<A href="#"><IMG height="17" src="images/desktop/bn_more.gif" width="60" border="0"></A>--></td>
	</tr>
</table>
<table id="table2" height="100">
	<tr>
		<td vAlign="top" width="100%">
			<table class="txt" id="table3" cellSpacing="0" cellPadding="0" width="300" align="center"
				border="0" valign="top">
				<asp:repeater id="rpRemind" Runat="server">
					<ItemTemplate>
						<tr valign="top">
							<td width="60%" nowrap><%# DataBinder.Eval(Container.DataItem, "Type") %></td>
							<td width="40%" nowrap><a href='##' onclick='OpenRemind(Url); return false;' Url='<%# DataBinder.Eval(Container.DataItem, "Url") %>' >
									<%# DataBinder.Eval(Container.DataItem, "Title") %>
								</a>
							</td>
						</tr>
					</ItemTemplate>
				</asp:repeater></table>
		</td>
	</tr>
</table>

