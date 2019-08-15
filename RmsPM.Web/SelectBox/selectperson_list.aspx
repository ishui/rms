<%@ Page language="c#" Inherits="RmsPM.Web.SelectBox.SelectPerson_List" CodeFile="SelectPerson_List.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ѡ����Ŀ</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<script>
        
        function SelectSingleUser(userCode,userName)
        {
			var flag = '<%=Request["Flag"]%>';
			window.opener.<%=ViewState["ReturnFunc"]%>(userCode,userName,flag);
			
        }
        function DoCancel()
        {
			window.close();
        }
		</script>
	</HEAD>
	<body >
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">ѡ���û�</td>
				</tr>
				<TR>
					<TD vAlign="top" align="center">
						<table class="search-area" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td>&nbsp;������ &nbsp;<asp:textbox id="txtUserName" runat="server" CssClass="input"></asp:textbox>
									&nbsp;<input class="submit" id="btnSearch" type="button" value="�� ��" name="btnSearch" runat="server" onserverclick="btnSearch_ServerClick"></td>
							</tr>
						</table>
						<asp:datagrid id="dgPersonList" runat="server" CssClass="list" AutoGenerateColumns="False" GridLines="Horizontal"
							CellPadding="2" PageSize="8" DataKeyField="UserCode" Width="100%">
							<AlternatingItemStyle CssClass="list-i"></AlternatingItemStyle>
							<HeaderStyle CssClass="list-title"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="UserCode" HeaderText="" Visible="False"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="����">
									<ItemTemplate>
										<a href=## userCode='<%#  DataBinder.Eval(Container.DataItem, "UserCode")  %>'
										userName='<%#  DataBinder.Eval(Container.DataItem, "UserName")  %>'
										 onclick='doSelectSingleUser(userCode,userName);'>
											<%#  DataBinder.Eval(Container.DataItem, "UserName")  %>
										</a>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="����">
									<ItemTemplate>
										<%#  DataBinder.Eval(Container.DataItem, "UserName")  %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="ѡ��">
									<ItemTemplate>
										<asp:CheckBox id="chkPerson" runat="server"></asp:CheckBox>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle NextPageText="��ҳ&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;��ҳ" HorizontalAlign="Right"
								CssClass="ListHeadTr"></PagerStyle>
						</asp:datagrid>
						<table id="tbButton" align="center" runat="server">
							<tr align="center" width="100%">
								<td rowSpan="2"><input class="submit" id="SaveToolsButton" type="button" value="ȷ ��" name="SaveToolsButton"
										runat="server" onserverclick="SaveToolsButton_ServerClick"> <input class="submit" id="CancelToolsButton" onclick="DoCancel();return false;" type="button"
										value="ȡ ��" name="CancelToolsButton">
								</td>
							</tr>
						</table>
					</TD>
				</TR>
			</table>
		</form>
	</body>
</HTML>
