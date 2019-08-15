<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Page language="c#" CodeFile="WBSPlan.aspx.cs" AutoEventWireup="false" Inherits="RmsPM.Web.Project.WBSPlan" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
<HTML>
	<HEAD>
		<title></title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<script>
			<!--
				function OpenPlan(Code)
				{
					OpenMiddleWindow("WBSPlanInfo.aspx?Action=Modify&Code=" + Code,"工作计划");
				}
				
				function ViewPlan(Code)
				{
					OpenMiddleWindow("WBSPlanInfo.aspx?Action=View&Code=" + Code,"工作计划");
				}
				
				function CheckPlan(Code)
				{
					OpenMiddleWindow("WBSPlanCheck.aspx?Code=" + Code,"计划审阅");
				}
				
				function Update(Code)
				{
					window.location.reload();
				}
				
				function DoAddNewPlan()
				{
					OpenMiddleWindow("WbsPlanInfo.aspx?Action=Insert","工作计划");
				}
				function SubmitCondition()
				{
					var m_Condition = "";				
					m_Condition = "ExecuteName=" + escape(document.all.txtTitle.value);
					window.SearchExecute(m_Condition);				
				}
				function SearchExecute(Condition)
				{
					window.location.href = "WBSPlan.aspx?" + Condition;
				}
			-->
		</script>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="yes" rightMargin="0">
		<form id="Form1" name="MyTask" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td bgColor="#e4eff6" height="6"></td>
				</tr>
				<tr>
					<td height="25">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr width="100%">
								<td class="topic" background="../images/topic_bg.gif"><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle">
									工作管理 - 工作计划
								</td>
								<td width="79"><IMG height="25" src="../images/btn_back.jpg" width="79"></td>
							</tr>
						</table>
					</td>
				</tr>
				
				<tr valign="top">
					<td class="table">
						<table width="100%" cellpadding="0" cellspacing="0" border="0">
							<tr>
								<td class="search-area">
									<table cellSpacing="10" cellPadding="0" width="50%" border="0">
										<TR width="100%">
											<TD align="right">计划名称：</TD>
											<TD><input style="WIDTH: 120px" type="text" name="txtTitle" id="txtTitle" class="input">
											</TD>
											<td align="center" colSpan="2"><input class="submit" id="btnSearch" onclick="SubmitCondition();return false;" type="button"
													value="搜 索" name="btnSearch">
											</td>
										</TR>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr valign="top">
					<td width="100%" height="100%">
						<table cellpadding="0" cellspacing="0" border="0" width="100%" height="100%">
							<tr id="trOwn" vAlign="top" runat="server">
								<td class="table" vAlign="top" ><br>
									<table cellSpacing="0" cellPadding="0" width="100%">
										<tr width="100%">
											<td width="100%"><asp:datagrid id="dgMyPlan" runat="server" DataKeyField="TaskPlanCode" GridLines="Horizontal"
													CssClass="list" AutoGenerateColumns="False" Width="100%" CellPadding="2">
													<HeaderStyle CssClass="list-title"></HeaderStyle>
													<Columns>
														<asp:HyperLinkColumn DataNavigateUrlField="TaskPlanCode" DataNavigateUrlFormatString="javascript:OpenPlan('{0}')"
															DataTextField="Title" HeaderText="计划名称"></asp:HyperLinkColumn>
														<asp:BoundColumn DataField="UserName" HeaderText="提交人"></asp:BoundColumn>
														<asp:BoundColumn DataField="PlanDate" HeaderText="提交日期" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn>
														<asp:ButtonColumn Text="删除" CommandName="Delete"></asp:ButtonColumn>
													</Columns>
												</asp:datagrid></td>
										</tr>
									</table>
									<table class="form" id="tbOwn" cellSpacing="0" cellPadding="0" width="100%" runat="server">
										<tr align="center" width="100%">
											<td>无工作计划</td>
										</tr>
									</table>
								</td>
							</tr>
							<tr vAlign="top">
								<td class="table" vAlign="top">
									<table border="0">
										<tr>
											<td width="16"><IMG src="../images/btn_li.gif" align="absMiddle"></td>
											<td><input class="button" id="btnAddNewPlan" onclick="DoAddNewPlan(); return false;" type="button"
													value="新增工作计划" name="btnAddNewPlan">
											</td>
											<td width="16"><IMG src="../images/btn_li.gif" align="absMiddle"></td>
										</tr>
									</table>
								</td>
							</tr>
							<tr id="trOther" vAlign="top" runat="server" height="100%">
								<td class="table" vAlign="top" width="100%" height="100%"><br>
									<table cellSpacing="0" cellPadding="0" border="0">
										<tr>
											<td class="intopic">其他工作计划</td>
										</tr>
									</table>
									<table cellSpacing="0" cellPadding="0" width="100%">
										<tr width="100%">
											<td align="center" width="100%"><asp:datagrid id="dgOtherPlan" runat="server" DataKeyField="TaskPlanCode" GridLines="Horizontal"
													CssClass="list" AutoGenerateColumns="False" Width="100%" CellPadding="2">
													<HeaderStyle CssClass="list-title"></HeaderStyle>
													<Columns>
														<asp:HyperLinkColumn DataNavigateUrlField="TaskPlanCode" DataNavigateUrlFormatString="javascript:ViewPlan('{0}')"
															DataTextField="Title" HeaderText="计划名称"></asp:HyperLinkColumn>
														<asp:BoundColumn DataField="UserName" HeaderText="提交人"></asp:BoundColumn>
														<asp:BoundColumn DataField="PlanDate" HeaderText="提交日期" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn>
														<asp:HyperLinkColumn Text="审阅" DataNavigateUrlField="TaskPlanCode" DataNavigateUrlFormatString="javascript:CheckPlan('{0}')"
															HeaderText="操作"></asp:HyperLinkColumn>
													</Columns>
												</asp:datagrid></FONT></td>
										</tr>
									</table>
									<table class="form" id="tbOther" cellSpacing="0" cellPadding="0" width="100%" runat="server">
										<tr align="center" width="100%">
											<td>无工作计划</td>
										</tr>
									</table>
								</td>
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
					<td bgColor="#e4eff6" height="6"><FONT face="宋体"></FONT></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
