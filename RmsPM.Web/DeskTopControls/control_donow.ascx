<%@ Control Language="c#" Inherits="RmsPM.Web.DeskTopControl.Control_DoNow" CodeFile="Control_DoNow.ascx.cs" %>
<LINK href="../images/index.css" type="text/css" rel="stylesheet">
<style>
<!--
	.toptxt { COLOR: #ff0000 }
-->
</style>
<table cellSpacing="0" cellPadding="0" width="100%" background="images/desktop/bg.gif"
	border="0" id="table1">
	<tr>
		<td>
		<IMG src="images/topic_operate.GIF" width="150" height="43"></td>
		<td style="PADDING-TOP: 18px" vAlign="top" align="right"><!--<A href="#"><IMG height="17" src="images/desktop/bn_tailor.gif" width="60" border="0"></A>--></td>
		<td vAlign="middle" width="35"><a href="./WorkFlowContral/WorkFlowInBox.aspx">[È«²¿]</a></td>
		<td style="PADDING-TOP: 18px" vAlign="top" width="60"><!--<A href="#"><IMG height="17" src="images/desktop/bn_more.gif" width="60" border="0"></A>--></td>
	</tr>
</table>
<table height="114" border="0" id="table2">
	<tr>
		<td vAlign="top">
			<table class="txt" cellSpacing="0" cellPadding="0" width="300" align="center" border="0" id="table3">
				<asp:repeater id="Repeater1" Runat="server">
					<ItemTemplate>
						<tr>
							<td width="20"></td>
							<td>[<%# DataBinder.Eval(Container.DataItem, "Type") %>]</td>
							<td width="50%"><a href='##' onclick='OpenAudit(code); return false;' code='<%# DataBinder.Eval(Container.DataItem,"Url") %>' >
									<%# DataBinder.Eval(Container.DataItem, "Title") %>¸ö
								</a>
							</td>
						</tr>
					</ItemTemplate>
				</asp:repeater></table>
		</td>
	</tr>
</table>