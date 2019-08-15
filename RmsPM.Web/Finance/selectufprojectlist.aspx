<%@ Page language="c#" Inherits="RmsPM.Web.Finance.SelectUFProjectList" CodeFile="SelectUFProjectList.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>选择项目</title>
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
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">选择项目</td>
				</tr>
				<tr>
					<td>
						<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0" class="search-area">
							<tr>
								<td>
									<table>
										<tr>
											<td>项目名称：</td>
											<td><input type="text" class="input" id="txtSearchUFProjectName" name="txtSearchUFProjectName" runat="server"></td>
											<td><input type="button" class="submit" id="btnSearch" name="btnSearch" runat="server" value="搜 索" onserverclick="btnSearch_ServerClick"></td>
										</tr>
									</table>
								</td>
							</tr>
						</TABLE>
					</td>
				</tr>
				<tr height="100%">
					<td>
						<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
						<TABLE height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr valign="top">
								<td>
									<asp:DataGrid id="dgList" runat="server" AllowPaging="False" CellPadding="0" GridLines="Horizontal"
										AllowSorting="True" AutoGenerateColumns="False" PageSize="15" Width="100%" CssClass="list"
										DataKeyField="UFProjectCode">
										<AlternatingItemStyle CssClass=""></AlternatingItemStyle>
										<ItemStyle CssClass=""></ItemStyle>
										<HeaderStyle CssClass="list-title"></HeaderStyle>
										<FooterStyle CssClass="list-title"></FooterStyle>
										<Columns>
											<asp:TemplateColumn HeaderText="项目名称">
												<ItemTemplate>
													<a href="#" onclick="Select('<%# DataBinder.Eval(Container.DataItem,"UFProjectCode").ToString() %>', '<%# DataBinder.Eval(Container.DataItem,"UFProjectName").ToString() %>');return false;" id='a_<%# DataBinder.Eval(Container.DataItem,"UFProjectCode").ToString() %>'><%# DataBinder.Eval(Container.DataItem,"UFProjectName").ToString() %></a>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:BoundColumn DataField="UFProjectCode" HeaderText="项目代码"></asp:BoundColumn>
										</Columns>
										<PagerStyle NextPageText="下页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上页" HorizontalAlign="Right"
											CssClass="ListHeadTr"></PagerStyle>
									</asp:DataGrid>
								</td>
							</tr>
						</TABLE>
						</div>
					</td>
				</tr>
				<tr>
					<td>
						<table height="100%" cellSpacing="10" cellPadding="0" width="100%" border="0">
							<tr align="center">
								<td><input type="button" class="submit" value="清 除" name="btnClear" onclick="Select('', '');">
									<input type="button" class="submit" value="关 闭" name="btnClose" onclick="window.close();"></td>
							</tr>
						</table>
					</td>
				</tr>
			</TABLE>
			<input type="hidden" id="txtSelect" name="txtSelect" runat="server">
			<Script language="javascript">
	function Select(code, name)
	{
		window.opener.SelectUFProjectReturn(code, name);
		window.close();
	}
			</Script>
		</form>
	</body>
</HTML>
