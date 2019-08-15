<%@ Page language="c#" Inherits="RmsPM.Web.Finance.UFProjectList" CodeFile="UFProjectList.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>核算项目列表</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<LINK href="../Images/TreeView.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<TABLE height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td bgColor="#e4eff6" height="6"></td>
				</tr>
				<tr>
					<td height="25">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="topic" background="../images/topic_bg.gif"><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle"><span id="spanTitle" runat="server">系统管理 
										- 核算项目</span></td>
								<td width="9"><IMG height="25" src="../images/topic_corr.gif" width="9"></td>
							</tr>
						</table>
					</td>
				</tr>
				<TR>
					<td class="tools-area" vAlign="top">
						<IMG src="../images/btn_li.gif" align="absMiddle">
						<input style="display:none" class="button" id="btnAdd" onclick="Add();" type="button" value="新 增" name="btnAdd">
						<input class="button" id="btnImport" onclick="Import();" type="button" value="导 入" name="btnImport">
					</TD>
				</TR>
				<tr>
					<td class="table" vAlign="top">
						<TABLE class="search-area" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td>
									<table>
										<TR>
											<TD>核算项目名称：</TD>
											<TD><input id="txtSearchUFProjectName" type="text" name="txtSearchUFProjectName" size="20" class="input" runat="server"></TD>
											<TD><input class="submit" id="btnSearch" type="button" value="搜 索" name="btnSearch" runat="server" onclick="document.all.divHintLoad.style.display='';" onserverclick="btnSearch_ServerClick"></TD>
										</TR>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr height="100%">
					<td class="table" vAlign="top">
						<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
							<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
								<tr vAlign="top">
									<td>
										<asp:DataGrid id="dgList" runat="server" CellPadding="0" AllowSorting="True" AutoGenerateColumns="False"
											PageSize="100" Width="100%" CssClass="list" DataKeyField="UFProjectCode" AllowPaging="True">
											<AlternatingItemStyle CssClass=""></AlternatingItemStyle>
											<ItemStyle CssClass=""></ItemStyle>
											<HeaderStyle CssClass="list-title"></HeaderStyle>
											<FooterStyle CssClass="list-title"></FooterStyle>
											<Columns>
												<asp:TemplateColumn HeaderText="核算项目代码">
													<ItemTemplate>
														<%# DataBinder.Eval(Container.DataItem, "UFProjectCode")%>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="UFProjectName" HeaderText="核算项目名称"></asp:BoundColumn>
												<asp:TemplateColumn Visible="False">
													<ItemTemplate>
														<asp:LinkButton runat="server" Text="删除" CommandName="Delete" CausesValidation="false" ID="btnDelete"></asp:LinkButton>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle NextPageText="下页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上页" HorizontalAlign="Center"
												CssClass="ListHeadTr"></PagerStyle>
										</asp:DataGrid>
									</td>
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
			</TABLE>
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
			<input type="hidden" id="txtProjectCode" name="txtProjectCode" runat="server">
		</form>
		<script language="javascript">
	function Add()
	{
		OpenCustomWindow("UFProjectModify.aspx?ProjectCode=" + Form1.txtProjectCode.value,"核算项目修改", 300, 200);
	}

	function Edit(id)
	{
		OpenCustomWindow("UFProjectModify.aspx?UFProjectCode=" + id,"核算项目修改", 300, 200);
	}

	function Import()
	{
		OpenCustomWindow("ImportUFProjectDlg.aspx?ProjectCode=" + Form1.txtProjectCode.value,"导入核算项目", 400, 300);
	}
		</script>
	</body>
</HTML>
