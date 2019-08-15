<%@ Page language="c#" CodeFile="WBSPlanInfo.aspx.cs" AutoEventWireup="false" Inherits="RmsPM.Web.Project.WBSPlanInfo" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>工作计划</title>
		<META http-equiv="Content-Type" content="text/html; charset=gb2312">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript">
			function doCancel(){
				window.close();
			}

		</SCRIPT>
	</HEAD>
	<body bottomMargin="0" bgColor="white" leftMargin="0" topMargin="0" scroll="no" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" bgColor="white" border="0">
				<tr vAlign="top">
					<td>
						<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="topic" align="center" background="../images/topic_bg.gif" height="25"><asp:label id="lblTitle" runat="server">Label</asp:label></td>
							</tr>
						</TABLE>
						<table class="form" id="tbInfo" cellSpacing="0" cellPadding="0" width="100%" border="0"
							runat="server">
							<TR width="100%">
								<TD class="form-item" align="right" width="20%">计划名称：</TD>
								<TD class="tdBlank"><asp:label id="lblPlanTitle" runat="server"></asp:label></TD>
								<TD class="form-item" align="right" width="20%">制定时间：</TD>
								<TD class="tdBlank"><asp:label id="lblPlanDate" runat="server"></asp:label></TD>
							</TR>
							<TR width="100%">
								<TD class="form-item" align="right" width="20%">计划名称：</TD>
								<TD class="tdBlank" id="tdPlanContent" colSpan="3" height="100" runat="server"></TD>
							</TR>
						</table>
						<TABLE class="form" id="tbAdd" cellSpacing="0" cellPadding="0" width="100%" border="0"
							runat="server">
							<tr style="DISPLAY: none" width="100%">
								<td colSpan="2"><asp:datagrid id="dgTaskList" runat="server" AutoGenerateColumns="False" Width="100%">
										<Columns>
											<asp:HyperLinkColumn DataNavigateUrlField="WBSCode" DataNavigateUrlFormatString="javascript:OpenTask('{0}');"
												DataTextField="TaskName" HeaderText="工作名称"></asp:HyperLinkColumn>
											<asp:BoundColumn DataField="StatusName" HeaderText="工作状态"></asp:BoundColumn>
											<asp:BoundColumn DataField="Master" HeaderText="负责人"></asp:BoundColumn>
											<asp:BoundColumn DataField="PlannedStartDate" HeaderText="预计开始时间" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn>
											<asp:BoundColumn DataField="PlannedFinishDate" HeaderText="预计完成时间" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn>
											<asp:TemplateColumn>
												<ItemTemplate>
													<cc3:Calendar id="MyPlanStartDate" Display="True" ReadOnly="False" CalendarResource="../Images/CalendarResource/"
														runat="server"></cc3:Calendar>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn>
												<ItemTemplate>
													<cc3:Calendar id="MyPlanFinishDate" Display="True" ReadOnly="False" CalendarResource="../Images/CalendarResource/"
														runat="server"></cc3:Calendar>
												</ItemTemplate>
											</asp:TemplateColumn>
										</Columns>
									</asp:datagrid></td>
							</tr>
						</TABLE>
						<table class="form" id="tbModify" cellSpacing="0" cellPadding="0" border="0" runat="server">
							<TR width="100%">
								<TD class="form-item" align="right" width="20%">计划名称：</TD>
								<TD class="tdBlank" width="30%"><asp:textbox id="txtPlanTitle" runat="server" CssClass="input" Width="120px"></asp:textbox></TD>
							</TR>
							<TR width="100%">
								<TD class="form-item" align="right" width="20%">计划内容：</TD>
								<TD class="tdBlank" width="30%"><textarea id="arPlanContent" name="arPlanContent" rows="5" cols="50" runat="server"></textarea></TD>
							</TR>
						</table>
						<table align="center">
							<tr align="center" width="100%">
								<td><input class="submit" id="SaveToolsButton" type="button" value="确 定" name="SaveToolsButton"
										runat="server"></td>
								<td><input class="submit" id="CancelToolsButton" onclick="doCancel();return false;" type="button"
										value="取 消" name="CancelToolsButton" runat="server"></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<input id="hAction" type="hidden" runat="server"> <input id="hCode" type="hidden" runat="server">
		</form>
	</body>
</HTML>
