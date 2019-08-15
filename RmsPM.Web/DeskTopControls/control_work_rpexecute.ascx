<%@ Control Language="c#" Inherits="RmsPM.Web.DeskTopControl.Control_Work_rpExecute" CodeFile="Control_Work_rpExecute.ascx.cs" %>
<LINK href="../images/index.css" type="text/css" rel="stylesheet">
<table cellSpacing="0" cellPadding="0" width="100%" border="0" id="table1">
	<tr>
		<td style="PADDING-RIGHT: 6px" align="right" width="90" height="100">
			<IMG height="31" src="images/desktop/icon_report.gif" width="58" vspace="5"><br>
			<IMG style="CURSOR: hand" onclick="OpenMoreTaskExecute(this.ProjectCode);return false" height="17" src="../images/desktop/bn_more.gif"
				width="60" id="imgOpenMoreTaskExecute" name="imgOpenMoreTaskExecute" runat="server"></td>
		<td width="1" bgColor="#cccccc"></td>
		<td style="PADDING-LEFT: 6px; PADDING-BOTTOM: 6px" vAlign="top">
			<table class="txt" id="table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<asp:repeater id="rpExecute" Runat="server">
					<ItemTemplate>
						<tr  title='<%# RmsPM.BLL.ProjectRule.GetProjectName(DataBinder.Eval(Container.DataItem, "ProjectCode").ToString()) %> --- <%# DataBinder.Eval(Container.DataItem, "TaskName") %>'>
							<td width="20"><%# DataBinder.Eval(Container.DataItem, "Img") %></td>
							<td>[<%# RmsPM.BLL.StringRule.TruncText(DataBinder.Eval(Container.DataItem, "TaskName"),5) %>]</td>
							<td><a href='##' onclick='OpenExecute(WBSCode,TaskExecuteCode,ProjectCode);return false;'  WBSCode='<%# DataBinder.Eval(Container.DataItem, "WBSCode") %>' TaskExecuteCode='<%# DataBinder.Eval(Container.DataItem, "TaskExecuteCode") %>' ProjectCode='<%# DataBinder.Eval(Container.DataItem, "ProjectCode") %>'>
									<%# DataBinder.Eval(Container.DataItem, "Detail") %>
								</a>
							</td>
							<td><%# DataBinder.Eval(Container.DataItem, "ExecuteDate","{0:yyyy-MM-dd}") %></td>
							<td width="50"><%# RmsPM.BLL.SystemRule.GetUserName(DataBinder.Eval(Container.DataItem, "ExecutePerson").ToString()) %></td>
						</tr>
					</ItemTemplate>
				</asp:repeater></table>
		</td>
	</tr>
</table>
<hr color="#cccccc" SIZE="1">