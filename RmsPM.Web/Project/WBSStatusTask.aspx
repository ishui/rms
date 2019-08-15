<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Page language="c#" CodeFile="WBSStatusTask.aspx.cs" AutoEventWireup="false" Inherits="RmsPM.Web.Project.WBSStatusTask" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
<HTML>
	<HEAD>
		<title>WBSStatusTask</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<script>
				<!--
					function Sort(SortField)
					{
						document.Form1.txtSortField.value = SortField;
						document.forms[0].submit();
					}
					
					function SelectTask(Condition)
					{
//						window.location.href = "WBSStatusTask.aspx?" + Condition +"&TaskStatus=<%=Request["TaskStatus"]%>&TaskExceed=" + document.all.TaskExceed.value;
					}
					
					function OpenTask(WBSCode)
					{
						OpenFullWindow('WBSInfo.aspx?WBSCode=' + WBSCode,900,600);
					}
					
					function SubmitCondition()
					{
						var m_Condition = "";
						
						m_Condition = "TaskName=" + escape(document.all.txtTaskName.value);
						m_Condition += "&Master=" + escape(document.all.txtMaster.value);
						m_Condition += "&ImportantLevel=" + document.all.lstImportantLevel.value;
						m_Condition += "&Exceed=" + document.all.lstExceed.value;
						m_Condition += "&StartDate=" + document.all.dtbStartDate.value;
						m_Condition += "&EndDate=" + document.all.dtbEndDate.value;
						
						window.SelectTask(m_Condition);
						
					}
				-->
		</script>
	</HEAD>
	<body scroll="no">
		<form id="Form1" name="MyTask" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td bgColor="#e4eff6" height="6"></td>
				</tr>
				<tr>
					<td height="25">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="topic" background="../images/topic_bg.gif" width=100%><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle">
									工作管理 -<asp:Label id="lblTitle" runat="server">Label</asp:Label>
								</td>
								<td><IMG height="25" src="../images/topic_corr.gif"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr valign="top">
					<td class="table" valign=top>						
						<table width="100%" cellpadding="0" cellspacing="0" border="0">
							<tr>
								<td class="search-area">
									<table cellSpacing="10" cellPadding="0" width="100%" border="0">
										<TR width="100%">
											<TD align="right">工作名称/编码：</TD>
											<TD><input style="WIDTH: 120px" type="text" name="txtTaskName" id="txtTaskName" runat="server"
													class="input">
											</TD>
											<TD align="right">负责人：</TD>
											<TD><input style="WIDTH: 120px" type="text" name="txtMaster" id="txtMaster" runat="server"
													class="input">
											</TD>
											<TD align="right">是否超期：</TD>
											<TD><select style="WIDTH: 120px" name="lstExceed" id="lstExceed" runat="server">
													<OPTION value="" selected>－－请选择－－</OPTION>
													<option value="0">否</option>
													<OPTION value="1">是</OPTION>													
												</select></TD>
											<td align="center" colSpan="2"><input class="submit" id="btnSearch" onclick="SubmitCondition();return false;" type="button"
													value="搜 索" name="btnSearch">
											</td>
										</TR>
										<TR>
											<TD align="right">开始时间：</TD>
											<TD><cc3:calendar id="dtbStartDate" runat="server" CalendarResource="../Images/CalendarResource/"
													ReadOnly="False" Display="True"></cc3:calendar></TD>
											<TD align="right">结束时间：</TD>
											<TD><cc3:calendar id="dtbEndDate" runat="server" CalendarResource="../Images/CalendarResource/" ReadOnly="False"
													Display="True"></cc3:calendar></TD>
											<TD align="right">重要程度：</TD>
											<TD><select style="WIDTH: 120px" name="lstImportantLevel" id="lstImportantLevel" runat="server">
													<OPTION value="" selected>－－请选择－－</OPTION>
													<OPTION value="0">一般</OPTION>
													<option value="1">重要</option>
												</select></TD>
											
										</TR>
									</table>
								</td>
							</tr>
						</table>

						<asp:datagrid id="dgTaskList" runat="server" DataKeyField="WBSCode" AutoGenerateColumns="False"
							Width="100%" AllowSorting="True" CssClass="list">
							<HeaderStyle CssClass="list-title"></HeaderStyle>
							<Columns>
								<asp:HyperLinkColumn DataNavigateUrlField="WBSCode" DataNavigateUrlFormatString="javascript:OpenTask('{0}');"
									DataTextField="PicName" HeaderText="工作项"></asp:HyperLinkColumn>
								<asp:BoundColumn DataField="WBSCode" SortExpression="TaskCode" HeaderText="编号"></asp:BoundColumn>
								<asp:BoundColumn DataField="Master" HeaderText="负责人"></asp:BoundColumn>
								<asp:BoundColumn DataField="StatusName" SortExpression="Status" HeaderText="状态"></asp:BoundColumn>
								<asp:BoundColumn DataField="ImportantName" SortExpression="ImportantLevel" HeaderText="重要程度"></asp:BoundColumn>
								<asp:BoundColumn DataField="PlannedStartDate" SortExpression="PlannedStartDate" HeaderText="计划开始时间"
									DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn>
								<asp:BoundColumn DataField="PlannedFinishDate" SortExpression="PlannedFinishDate" HeaderText="计划结束时间"
									DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn>
							</Columns>
							<PagerStyle NextPageText="下一页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上一页" HorizontalAlign="Right"
								CssClass="ListHeadTr"></PagerStyle>
						</asp:datagrid>
						<table width=100%>
							<tr id="trNoTask" runat="server" >
								<td width="100%" align="center">没有符合条件的工作信息</td>
							</tr>
						</table>
					</td>
				</tr>				
				<tr>
					<td height="12">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td background="../images/corl_bg.gif"><IMG height="12" src="../images/corl.gif" width="12"></td>
								<td width="12"><IMG height="12" src="../images/corr.gif" width="12"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td bgColor="#e4eff6" height="6"><FONT face="宋体">&nbsp;</FONT></td>
				</tr>
			</table>
			<input id="txtSortField" type="hidden" name="txtSortField" runat="server"> <input id="TaskStatus" type="hidden" name="TaskStatus" runat="server">
			<input id="TaskExceed" type="hidden" name="TaskExceed" runat="server">
		</form>
	</body>
</HTML>
