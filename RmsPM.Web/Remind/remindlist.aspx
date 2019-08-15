<%@ Page language="c#" Inherits="RmsPM.Web.Remind.RemindList" CodeFile="RemindList.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>RemindList</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<script language="javascript">
			function ModifyRemind(Code)
			{
				OpenMiddleWindow('../Remind/RemindModify.aspx?Action=Modify&Code=' + Code+"&ProjectCode=<%=Request["ProjectCode"]%>",'');
			}
			
			function AddNewRemind()
			{
				OpenMiddleWindow('../Remind/RemindModify.aspx?Action=Insert&ProjectCode=<%=Request["ProjectCode"]%>','');
			}
		</script>
	</HEAD>
	<body  bottomMargin="0" leftMargin="0" topMargin="0" scroll="yes" rightMargin="0">
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
									系统管理-提醒定义
								</td>
								<td><IMG height="25" src="../images/topic_corr.gif"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr vAlign="top">
					<td class="tools-area" valign="middle">
						<IMG src="../images/btn_li.gif"> <input class="button" onclick="AddNewRemind();return false;" type="button" value="新增提醒">
					</td>
				</tr>
				<tr vAlign="top" width="100%">
					<td class="table" vAlign="top"><br>
						<asp:datagrid id="dgRemindList" runat="server" Width="100%" CssClass="list" AutoGenerateColumns="False"
							DataKeyField="RemindStrategyCode">
							<HeaderStyle CssClass="list-title"></HeaderStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="提醒事件">
									<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
									<ItemTemplate>
										<a href="#" onclick="ModifyRemind(this.Code);return false;" Code='<%# DataBinder.Eval(Container, "DataItem.RemindStrategyCode") %>'><%# DataBinder.Eval(Container, "DataItem.TypeName") %></a>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="ObjectName" HeaderText="提醒对象"></asp:BoundColumn>
								<asp:BoundColumn DataField="RemindDayType" HeaderText="提醒时间"></asp:BoundColumn>
								<asp:BoundColumn DataField="ActiveName" HeaderText="是否生效"></asp:BoundColumn>
								<asp:BoundColumn DataField="Remark" HeaderText="注释"></asp:BoundColumn>
								<asp:ButtonColumn Text="删除" HeaderText="操作" CommandName="Delete"></asp:ButtonColumn>
							</Columns>
						</asp:datagrid>
						<table width=100% >
						<tr runat="server" id="trNoRemind" valign=top>
							<td align=center>没有提醒定义</td>
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
		</form>
	</body>
</HTML>
