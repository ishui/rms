<%@ Control Language="c#" Inherits="RmsPM.Web.DeskTopControl.Control_Work_rpAttention" CodeFile="Control_Work_rpAttention.ascx.cs" %>
<LINK href="../images/index.css" type="text/css" rel="stylesheet">
<table cellSpacing="0" cellPadding="0" width="100%" border="0" id="table1">
	<tr>
		<td style="PADDING-RIGHT: 6px" align="right" width="90" height="100">
			<IMG height="31" src="images/desktop/icon_attention.gif" width="58" vspace="5"><br>
			<IMG style="CURSOR: hand" onclick="OpenMoreWBSAttention(this.ProjectCode);return false;"
				height="17" src="../images/desktop/bn_more.gif" width="60" id="imgOpenMoreWBSAttention" name="imgOpenMoreWBSAttention" runat="server"></td>
		<td width="1" bgColor="#cccccc"></td>
		<td style="PADDING-LEFT: 6px; PADDING-BOTTOM: 6px" vAlign="top">
			<table class="txt" id="table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<asp:repeater id="rpAttention" Runat="server">
					<ItemTemplate>
						<tr valign="top"  title='<%# RmsPM.BLL.ProjectRule.GetProjectName(DataBinder.Eval(Container.DataItem, "ProjectCode").ToString()) %> --- <%# DataBinder.Eval(Container.DataItem, "AddTitle") %>'>
							<td width="20"><%# DataBinder.Eval(Container.DataItem, "Img") %></td>
							<td width="120">[<%# DataBinder.Eval(Container.DataItem, "AddModule") %>]</td>
							<td><a href='##' onclick='OpenAttention(code + "&Type=0"); return false;' code='<%# DataBinder.Eval(Container.DataItem,"Url") %>' >
									<%# RmsPM.BLL.StringRule.TruncText(DataBinder.Eval(Container.DataItem, "AddTitle"),15) %>
								</a>
							</td>
						</tr>
					</ItemTemplate>
				</asp:repeater></table>
		</td>
	</tr>
</table>
<script>
</script>
<hr color="#cccccc" SIZE="1">