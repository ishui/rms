<%@ Control Language="c#" Inherits="RmsPM.Web.DeskTopControl.Control_Work_ThisWeek" CodeFile="Control_Work_ThisWeek.ascx.cs" %>
<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
<style type="text/css">TABLE.txt TD {
	COLOR: #aaaaaa; PADDING-TOP: 5px; BORDER-BOTTOM: #e7e7e7 1px dotted
}
.toptxt {
	COLOR: #ff0000
}
</style>
<table cellSpacing="0" cellPadding="0" width="100%" background="../images/desktop/bg.gif"
	border="0" id="table1">
	<tr>
		<td>
		<IMG src="images/desktop/topic_work.gif"></td>
	</tr>
</table>
<table cellSpacing="0" cellPadding="0" width="100%" border="0" id="table2">
	<tr>
		<td style="PADDING-RIGHT: 6px" align="right" width="90" height="100">
		<IMG src="images/desktop/icon_underway.gif" vspace="5"><br>
			<IMG style="CURSOR: hand" onclick="OpenMoreUnderWayTask(this.ProjectCode);return false" src="../images/desktop/bn_more.gif" id="imgOpenMoreUnderWayTask" name="imgOpenMoreUnderWayTask" runat="server"></td>
		<td width="1" bgColor="#cccccc"></td>
		<td style="PADDING-LEFT: 6px; PADDING-BOTTOM: 6px" vAlign="top">
			<table class="txt" id="table3" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<asp:repeater id="rpUnderWay" Runat="server">
					<ItemTemplate>
						<tr title='<%# RmsPM.BLL.ProjectRule.GetProjectName(DataBinder.Eval(Container.DataItem, "ProjectCode").ToString()) %> --- <%# DataBinder.Eval(Container.DataItem, "TaskName") %>'>
							<td width="20"><%# DataBinder.Eval(Container.DataItem, "Img") %></td>
							<td>[<a href='##' onclick='OpenTask(code,ProjectCode);return false;'  code='<%# DataBinder.Eval(Container.DataItem, "WBSCode") %>'  ProjectCode='<%# DataBinder.Eval(Container.DataItem, "ProjectCode") %>'>
									<%# RmsPM.BLL.StringRule.TruncText(DataBinder.Eval(Container.DataItem, "TaskName"),15) %>
								</a>]
							</td>
							<td width="70"><%# DataBinder.Eval(Container.DataItem, "StatusName") %></td>
							<td width="50"><%# DataBinder.Eval(Container.DataItem, "CompletePercent") %>%</td>
							<td width="50"><%# (DataBinder.Eval(Container.DataItem, "Master").ToString().Length>5)?RmsPM.BLL.StringRule.TruncText(DataBinder.Eval(Container.DataItem, "Master"),2):DataBinder.Eval(Container.DataItem, "Master") %></td>
						</tr>
					</ItemTemplate>
				</asp:repeater></table>
		</td>
	</tr>					
</table>
<hr color="#cccccc" SIZE="1">
								