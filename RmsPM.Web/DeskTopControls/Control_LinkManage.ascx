<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Control_LinkManage.ascx.cs" Inherits="DeskTopControls_Control_LinkManage" %>
<head>
<title></title>
</head>
<link href="../images/index.css" type="text/css" rel="stylesheet"/>
<style type="text/css">
.toptxt { color: #ff0000 }
</style>

<table cellspacing="0" cellpadding="0" width="100%" background="images/desktop/bg.gif"	border="0" id="table1">
	<tr>
		
			<td><IMG height="42" src="Images/desktop/topic_LinkManage.gif" width="126"></td>
		<td style="PADDING-TOP: 18px" vAlign="top" align="right"><!-- <A href="#"><IMG height="17" src="images/desktop/bn_tailor.gif" width="60" border="0"></A> --></td>
		<td vAlign="top" width="25"><!--<IMG height="37" src="images/desktop/bn_blank.gif" width="25">--></td>
		<td style="PADDING-TOP: 18px" vAlign="top" width="70"><!--<A href="#"><IMG height="17" src="images/desktop/bn_more.gif" width="60" border="0"></A>--></td>
		
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
							<td>
							    <a href="##" class="toptxt" onclick='javascript:OpenGoLink(url)' url='<%# DataBinder.Eval(Container.DataItem, "LinkUrl") %>' >	<%# DataBinder.Eval(Container.DataItem, "Linkname") %></a>
							</td>
							
						</tr>
					</ItemTemplate>
				</asp:repeater></table>
		</td>
	</tr>
</table>