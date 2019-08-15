<%@ Page language="c#" Inherits="RmsPM.Web.Systems.TaskAccessInfo" CodeFile="TaskAccessInfo.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title></title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Images/XmlTree.js"></SCRIPT>
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<SCRIPT language="javascript">
	
	//刷新
	function Refresh(act)
	{
		if (act.toLowerCase() == "insert")
		{
			window.parent.frameMain.MyRefreshChild();
			window.location = window.location;
		}
		else
		{
			if (act.toLowerCase() == "move")
			{
				window.parent.frameMain.MyRefreshChild();
				window.parent.frameMain.MyDeleteSrc();

				ClearSrc();
				window.location = window.location;
			}
			else
			{
				window.parent.frameMain.MyRefreshNode();
				window.location = window.location;
			}
		}
	}
	
		</SCRIPT>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="table" vAlign="top" align="center">
						<table class="form" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD class="form-item" width="80">工作项：</TD>
								<TD><asp:label id="lblProjectName" Runat="server"></asp:label>&nbsp;->&nbsp;<asp:label id="lblTaskName" Runat="server"></asp:label></TD>
							</TR>
						</table>
					</td>
				</tr>
				<tr height="100%">
					<td class="table" vAlign="top" align="center">
						<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
							<table cellSpacing="0" cellPadding="0" width="100%" border="0">
								<tr>
									<td>
										<table cellSpacing="0" cellPadding="0" border="0">
											<tr>
												<td class="intopic">权限</td>
												<td><input class="button-small" id="btnAddAccess" onclick="AddAccess();" type="button" value="配置权限"
														name="btnAddAccess" runat="server"></td>
											</tr>
										</table>
									</td>
								</tr>
								<tr>
									<td><asp:datagrid id="dgList" runat="server" AllowPaging="false" Width="100%" CssClass="list" CellPadding="0"
											AllowSorting="True" AutoGenerateColumns="False" PageSize="15" ShowFooter="False">
											<ItemStyle CssClass=""></ItemStyle>
											<HeaderStyle CssClass="list-title"></HeaderStyle>
											<FooterStyle CssClass="list-title"></FooterStyle>
											<Columns>
												<asp:TemplateColumn HeaderText="类型">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%# DataBinder.Eval(Container.DataItem, "TypeName")%>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="岗位人员">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="True"></ItemStyle>
													<ItemTemplate>
														<%# DataBinder.Eval(Container.DataItem, "UserNames")%>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle NextPageText="下一页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上一页" HorizontalAlign="Center"
												CssClass="ListHeadTr"></PagerStyle>
										</asp:datagrid></td>
								</tr>
							</table>
						</div>
					</td>
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
			<div id="divHintSave" style="DISPLAY: none; LEFT: 1px; WIDTH: 100%; POSITION: absolute; TOP: 200px">
				<TABLE id="tableHintSave" height="100" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD vAlign="top" align="center"><iframe id="frameSave" src="../Cost/SavingWating.htm" frameBorder="no" width="100%" scrolling="auto"
								height="100%"></iframe>
						</TD>
					</TR>
				</TABLE>
			</div>
			<input id="txtWBSCode" type="hidden" name="txtWBSCode" runat="server"> <input id="txtClassCode" type="hidden" name="txtClassCode" runat="server">
			<input id="txtAct" type="hidden" name="txtAct" runat="server"> <input id="txtParentCode" type="hidden" name="txtParentCode" runat="server"><input id="txtFromUrl" type="hidden" name="txtFromUrl" runat="server">
			<input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server">
		</form>
		<SCRIPT language="javascript">
<!--

	function AddAccess()
	{
		OpenCustomWindow("TaskAccessModify.aspx?WBSCode=" + Form1.txtWBSCode.value, "工作项访问权限修改", 760, 540);
	}
	
//-->	
		</SCRIPT>
	</body>
</HTML>
