<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Register TagPrefix="uc1" TagName="AttachMentList" Src="../UserControls/AttachMentList.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AttachMentAdd" Src="../UserControls/AttachMentAdd.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FeedBack" Src="../UserControls/FeedBack.ascx" %>
<%@ Page language="c#" Inherits="RmsPM.Web.Project.WBSMyTask" CodeFile="WBSMyTask.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
<HTML>
	<HEAD>
		<title>当前工作</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<script language="javascript">
		<!--
					
			function OpenTask(WBSCode)
			{
				OpenFullWindow('WBSInfo.aspx?WBSCode=' + WBSCode+"&ProjectCode=<%=Request["ProjectCode"]%>");
			}	
			
			function SelectView(Type)
			{		
				document.all.divHintLoad.style.display = "";		
				window.location.href = "WBSMyTask.aspx?&Type="+Type+"&User=&ProjectCode=<%=Request["ProjectCode"]%>&myUserTask=<%=Request["myUserTask"]%>";
			}
			
			function StatusChange()
			{
				var type = '<%=Request.QueryString["Type"]%>'; 			
				window.location.href = "WBSMyTask.aspx?&Type="+type+"&User=&ProjectCode=<%=Request["ProjectCode"]%>";
			}
			
		-->
		</script>
	</HEAD>
	<body scroll="no">
		<form name="MyTask" runat="server">
			<table width="100%" cellSpacing="0" cellPadding="0"  border="0" height="100%">
				<tr>
					<td bgColor="#e4eff6" height="6"></td>
				</tr>
				<tr>
					<td height="25">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="topic" background="../images/topic_bg.gif"><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle">工作管理
									- 当前工作</td>
								<td width="9"><IMG height="25" src="../images/topic_corr.gif" width="9"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td>
						<table cellSpacing="0" cellPadding="0" width="100%" border="0" id="tbWBSAction" runat="server">
							<tr>
								<td class="tools-area">
									<IMG src="../images/btn_li.gif" align="absMiddle"> &nbsp;&nbsp;<input type="button" id="ThisWeek" class="button" value="本周工作" onclick="SelectView('ThisWeek');return false;"
										runat="server" NAME="ThisWeek">&nbsp;<input type="button" id="ThisMonth" class="button" value="本月工作" onclick="SelectView('ThisMonth');return false;"
										runat="server" NAME="ThisMonth">&nbsp;<input type="button" id="NextWeek" class="button" value="下周工作" onclick="SelectView('NextWeek');return false;"
										runat="server" NAME="NextWeek">&nbsp;<input type="button" id="NextMonth" class="button" value="下月工作" onclick="SelectView('NextMonth');return false;"
										runat="server" NAME="NextMonth">&nbsp;<input type="button" id="ListAll" class="button" value="全部工作" onclick="SelectView('All');return false;"
										runat="server" NAME="NextMonth">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <IMG src="../images/btn_li.gif" align="absMiddle">
									<asp:Label id="lblTime" runat="server"></asp:Label>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr id="trGrid" valign="top" height="100%">
					<td class="table">
						<div style="overflow:auto;width:100%;height:100%">
						<table width="100%" cellpadding="0" cellspacing="0" border="0">
							<tr>
								<td>
									<asp:datagrid id="dgTask" runat="server" AutoGenerateColumns="False" Width="100%" AllowSorting="True"
										CssClass="list">
										<AlternatingItemStyle CssClass="list-i"></AlternatingItemStyle>
										<ItemStyle CssClass=""></ItemStyle>
										<HeaderStyle CssClass="list-title"></HeaderStyle>
										<FooterStyle CssClass="list-title"></FooterStyle>
										<Columns>
											<asp:TemplateColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" HeaderText="工作项">
											    <HeaderStyle Wrap="False"></HeaderStyle>
												<ItemStyle Wrap="False"></ItemStyle>
												<ItemTemplate>
													<a href="#" onclick="OpenTask(this.Code);return false;" Code='<%#  DataBinder.Eval(Container.DataItem, "WBSCode") %>'>
														<%#  DataBinder.Eval(Container.DataItem, "StatusName")%>
													</a>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:BoundColumn DataField="Master" SortExpression="Master" HeaderText="负责人">
												<HeaderStyle Wrap="false"></HeaderStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="CompletePercent" HeaderText="完成进度" DataFormatString="{0}%">
												<HeaderStyle Wrap="false"></HeaderStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="PlannedStartDate" SortExpression="PlannedStartDate" HeaderText="计划开始时间"
												DataFormatString="{0:yyyy-MM-dd}">
												<HeaderStyle Wrap="false"></HeaderStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="PlannedFinishDate" SortExpression="PlannedFinishDate" HeaderText="计划结束时间"
												DataFormatString="{0:yyyy-MM-dd}">
												<HeaderStyle Wrap="false"></HeaderStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="ActualStartDate" SortExpression="ActualStartDate" HeaderText="实际开始时间"
												DataFormatString="{0:yyyy-MM-dd}">
												<HeaderStyle Wrap="false"></HeaderStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="LastModifyDate" SortExpression="LastModifyDate" HeaderText="最后修改时间"
												DataFormatString="{0:yyyy-MM-dd}">
												<HeaderStyle Wrap="false"></HeaderStyle>
											</asp:BoundColumn>
										</Columns>
									</asp:datagrid>
								</td>
							</tr>
							<tr id="trNoTask" runat="server" valign="top">
								<td align="center" valign="top">没有符合条件的工作信息</td>
							</tr>
						</table>
						</div>
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
					<td bgColor="#e4eff6" height="6"></td>
				</tr>
			</table>
			<div id="divHintLoad" style="DISPLAY: none; LEFT: 1px; WIDTH: 100%; POSITION: absolute; TOP: 200px; BACKGROUND-COLOR: transparent">
				<TABLE id="tableHintLoad" height="100" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD vAlign="top" align="center"><iframe id="frameLoad" src="../Cost/LoadingPrepare.htm" frameBorder="no" width="100%" scrolling="auto"
								height="100%"></iframe>
						</TD>
					</TR>
				</TABLE>
			</div>
		</form>
	</body>
</HTML>
