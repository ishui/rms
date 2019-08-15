<%@ Page language="c#" Inherits="RmsPM.Web.Finance.SelectSuplList" CodeFile="SelectSuplList.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ѡ��Ӧ��</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body scroll="no">
		<form id="Form1" method="post" runat="server">
			<TABLE height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0" bgcolor="#ffffff">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">ѡ��Ӧ��</td>
				</tr>
				<tr style="display:none">
					<td>
						<TABLE height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td>
									<asp:RadioButtonList id="rbType" runat="server" RepeatColumns="2">
										<asp:ListItem Value="0" Selected="True">��Ӧ��</asp:ListItem>
										<asp:ListItem Value="1">���۹�Ӧ��</asp:ListItem>
									</asp:RadioButtonList></td>
							</tr>
						</TABLE>
					</td>
				</tr>
				<tr>
					<td>
						<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0" class="search-area">
							<tr>
								<td>
									<table>
										<tr>
											<td>��Ӧ�����ƣ�</td>
											<td><input type="text" class="input" id="txtSearchSuplName" name="txtSearchSuplName" runat="server"></td>
											<td><input type="button" class="submit" id="btnSearch" name="btnSearch" runat="server" value="�� ��" onserverclick="btnSearch_ServerClick"></td>
										</tr>
									</table>
								</td>
							</tr>
						</TABLE>
					</td>
				</tr>
				<tr height="100%">
					<td valign="top">
						<TABLE height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr valign="top">
								<td>
									<asp:DataGrid id="dgList" runat="server" AllowPaging="True" CellPadding="2" GridLines="Horizontal"
										AllowSorting="True" AutoGenerateColumns="False" PageSize="15" Width="100%" CssClass="list"
										DataKeyField="SystemID">
										<AlternatingItemStyle CssClass=""></AlternatingItemStyle>
										<ItemStyle CssClass=""></ItemStyle>
										<HeaderStyle CssClass="list-title"></HeaderStyle>
										<FooterStyle CssClass="list-title"></FooterStyle>
										<Columns>
											<asp:TemplateColumn HeaderText="��Ӧ������">
												<ItemTemplate>
													<a href="#" onclick="Select('<%# DataBinder.Eval(Container.DataItem,"SuplCode").ToString() %>', '<%# DataBinder.Eval(Container.DataItem,"SuplName").ToString() %>');return false;" id='a_<%# DataBinder.Eval(Container.DataItem,"SuplCode").ToString() %>'><%# DataBinder.Eval(Container.DataItem,"SuplName").ToString() %></a>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:BoundColumn DataField="SuplCode" HeaderText="��Ӧ�̴���"></asp:BoundColumn>
										</Columns>
										<PagerStyle NextPageText="��ҳ&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;��ҳ" HorizontalAlign="Right"
											CssClass="ListHeadTr"></PagerStyle>
									</asp:DataGrid>
								</td>
							</tr>
						</TABLE>
					</td>
				</tr>
				<tr>
					<td>
						<table height="100%" cellSpacing="10" cellPadding="0" width="100%" border="0">
							<tr align="center">
								<td><input type="button" class="submit" value="�� ��" name="btnClear" onclick="Select('', '');">
									<input type="button" class="submit" value="ȡ ��" name="btnClose" onclick="window.close();">
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</TABLE>
			<input type="hidden" id="txtSelect" name="txtSelect" runat="server">
			<Script language="javascript">
	function Select(code, name)
	{
		window.opener.SelectSuplReturn(code, name);
		window.close();
//		Form1.txtSelect.value = SuplCode;
//		Form1.btnSelectHidden.click();
	}
			</Script>
		</form>
	</body>
</HTML>
