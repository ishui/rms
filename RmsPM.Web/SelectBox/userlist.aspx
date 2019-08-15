<%@ Page language="c#" Inherits="RmsPM.Web.SelectBox.UserList" CodeFile="UserList.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title></title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<script>
			function doSelectUser( userCode,userName,flag )
			{
				var flag = '<%=Request["Flag"]%>';
				window.parent.opener.<%=ViewState["ReturnFunc"]%>(userCode,userName,flag);
				window.parent.close();
			}
		</script>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<table height="100%" width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="#ffffff">
				<tr height="100%">
					<td valign="top">
						<div style="OVERFLOW:auto; WIDTH:100%; HEIGHT:100%">
							<asp:datagrid id="dgList" runat="server" CssClass="list" CellPadding="0" GridLines="Horizontal"
								AutoGenerateColumns="False" PageSize="15" Width="100%" AllowPaging="False">
								<HeaderStyle CssClass="list-title"></HeaderStyle>
								<FooterStyle CssClass="list-title"></FooterStyle>
								<Columns>
									<asp:BoundColumn DataField="UserCode" HeaderText="" Visible="False"></asp:BoundColumn>
									<asp:TemplateColumn>
										<HeaderTemplate>
											姓 名
										</HeaderTemplate>
										<ItemTemplate>
											<a href="##" onclick="doSelectUser(userCode,userName)" 
										userCode='<%# DataBinder.Eval( Container,"DataItem.UserCode" )%>'
										userName='<%# DataBinder.Eval( Container,"DataItem.UserName" )%>'
										>
												<%# DataBinder.Eval( Container,"DataItem.UserName" )%>
											</a>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="UserName" HeaderText="姓 名"></asp:BoundColumn>
									<asp:BoundColumn DataField="UserID" HeaderText="登录名"></asp:BoundColumn>
									<asp:BoundColumn DataField="SortID" HeaderText="工 号"></asp:BoundColumn>
									<asp:BoundColumn DataField="Phone" HeaderText="办公电话"></asp:BoundColumn>
									<asp:TemplateColumn HeaderText="选择">
										<ItemTemplate>
											<asp:CheckBox id="chkPerson" runat="server"></asp:CheckBox>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle NextPageText="下一页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上一页" HorizontalAlign="Right"
									CssClass="ListHeadTr"></PagerStyle>
							</asp:datagrid>
						</div>
					</td>
				</tr>
				<tr>
					<td>
						<table cellSpacing="0" cellPadding="10" width="100%" border="0" id="tableButton" runat="server">
							<tr align="center">
								<td><input class="submit" id="btnSave" type="button" value="确 定" name="btnSave" runat="server" onserverclick="btnSave_ServerClick">&nbsp;&nbsp;&nbsp;
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
