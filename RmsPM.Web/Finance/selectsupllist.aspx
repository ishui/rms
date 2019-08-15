<%@ Page language="c#" Inherits="RmsPM.Web.Finance.SelectSuplList" CodeFile="SelectSuplList.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>选择供应商</title>
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
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">选择供应商</td>
				</tr>
				<tr style="display:none">
					<td>
						<TABLE height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td>
									<asp:RadioButtonList id="rbType" runat="server" RepeatColumns="2">
										<asp:ListItem Value="0" Selected="True">供应商</asp:ListItem>
										<asp:ListItem Value="1">销售供应商</asp:ListItem>
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
											<td>供应商名称：</td>
											<td><input type="text" class="input" id="txtSearchSuplName" name="txtSearchSuplName" runat="server"></td>
											<td><input type="button" class="submit" id="btnSearch" name="btnSearch" runat="server" value="搜 索" onserverclick="btnSearch_ServerClick"></td>
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
											<asp:TemplateColumn HeaderText="供应商名称">
												<ItemTemplate>
													<a href="#" onclick="Select('<%# DataBinder.Eval(Container.DataItem,"SuplCode").ToString() %>', '<%# DataBinder.Eval(Container.DataItem,"SuplName").ToString() %>');return false;" id='a_<%# DataBinder.Eval(Container.DataItem,"SuplCode").ToString() %>'><%# DataBinder.Eval(Container.DataItem,"SuplName").ToString() %></a>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:BoundColumn DataField="SuplCode" HeaderText="供应商代码"></asp:BoundColumn>
										</Columns>
										<PagerStyle NextPageText="下页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上页" HorizontalAlign="Right"
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
								<td><input type="button" class="submit" value="清 除" name="btnClear" onclick="Select('', '');">
									<input type="button" class="submit" value="取 消" name="btnClose" onclick="window.close();">
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
