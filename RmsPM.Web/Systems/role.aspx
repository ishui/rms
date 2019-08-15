<%@ Page language="c#" Inherits="RmsPM.Web.Systems.Role" CodeFile="Role.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>角色</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
	</HEAD>
	<body scroll="no">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td bgColor="#e4eff6" height="6"></td>
				</tr>
				<tr>
					<td height="25">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="topic" background="../images/topic_bg.gif"><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle"><span id="spanTitle" runat="server">
										系统管理&nbsp;- 角色管理</span></td>
								<td width="9"><IMG height="25" src="../images/topic_corr.gif" width="9"></td>
							</tr>
						</table>
					</td>
				</tr>
				<TR>
					<td class="tools-area" vAlign="top">
						<IMG src="../images/btn_li.gif" align="absMiddle">
						<input class="button" id="btnAdd" onclick="DoAddNew();return false;" type="button" value="新 增"
							name="btnAdd"></TD>
				</TR>
				<tr><td>
                        &nbsp; &nbsp; 角色<asp:TextBox ID="RoleName" runat="server" CssClass="input" Width="100px"></asp:TextBox>
                        序号
                        <asp:TextBox ID="SortID" runat="server" CssClass="input" Width="100px"></asp:TextBox>&nbsp;
                        <asp:Button ID="bt" runat="server" Text="搜 索" CssClass="button" OnClick="Button1_Click" /></td></tr>
				<tr height="100%">
					<td class="table" vAlign="top">
						<div style="overflow:auto;width:100%;height:100%">
						<asp:datagrid id="dgList" runat="server" CssClass="list" CellPadding="0" GridLines="Horizontal"
							AllowSorting="True" AutoGenerateColumns="False" PageSize="15" Width="100%">
							<HeaderStyle CssClass="list-title"></HeaderStyle>
							<FooterStyle CssClass="list-title"></FooterStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="角色">
									<HeaderStyle Wrap="False"></HeaderStyle>
									<ItemStyle Wrap="False"></ItemStyle>
									<ItemTemplate>
										<a href="#" onclick="DoModify('<%#  DataBinder.Eval(Container.DataItem, "RoleCode") %>');return false;" Code='<%#  DataBinder.Eval(Container.DataItem, "RoleCode") %>'><%#  DataBinder.Eval(Container.DataItem, "RoleName") %></a>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="Description" HeaderText="说明"></asp:BoundColumn>
                                <asp:BoundColumn DataField="SortID" HeaderText="排序"></asp:BoundColumn>
							</Columns>
							<PagerStyle NextPageText="下一页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上一页" HorizontalAlign="Right"
								CssClass="ListHeadTr"></PagerStyle>
						</asp:datagrid>
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
		</form>
		<script language="javascript">
<!--
		function DoAddNew()
		{
			OpenLargeWindow('RoleModify.aspx?ProjectCode=<%=Request["ProjectCode"]%>','角色定义');
		}
	
		function DoModify( RoleCode )
		{
			OpenLargeWindow('RoleInfo.aspx?ProjectCode=<%=Request["ProjectCode"]%>&RoleCode=' + RoleCode ,'角色定义');
		}
		
	
//-->
		</script>
	</body>
</HTML>
