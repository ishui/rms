<%@ Page language="c#" Inherits="RmsPM.Web.Sal.SalSuplList" CodeFile="SalSuplList.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>销售供应商列表</title>
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
								<td class="topic" background="../images/topic_bg.gif"><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle"><span id="spanTitle" runat="server">财务管理 
										- 销售供应商</span></td>
								<td width="9"><IMG height="25" src="../images/topic_corr.gif" width="9"></td>
							</tr>
						</table>
					</td>
				</tr>
				<TR>
					<td class="tools-area" vAlign="top">
						<IMG src="../images/btn_li.gif" align="absMiddle">
						<input class="button" id="btnAdd" onclick="Add();" type="button" value="新 增" name="btnAdd">
						<input class="button" id="btnImport" onclick="Import();" type="button" value="导 入" name="btnImport">
					</TD>
				</TR>
				<tr>
					<td class="table" vAlign="top">
						<TABLE class="search-area" cellSpacing="0" cellPadding="0" width="100%" border="0" onkeydown="SearchKeyDown();">
							<tr>
								<td>
									<table>
										<TR>
											<TD>供应商代码：</TD>
											<TD><input id="txtSearchSuplCode" type="text" name="txtSearchSuplCode" size="12" class="input" runat="server"></TD>
											<TD>供应商名称：</TD>
											<TD><input id="txtSearchSuplName" type="text" name="txtSearchSuplName" size="20" class="input" runat="server"></TD>
											<TD>项目：</TD>
											<TD><select id="sltSearchProject" name="sltSearchProject" runat="server" class="select">
													<option value=" " selected>-----------请选择------------</option>
												</select>
											</TD>
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
											PageSize="100" Width="100%" CssClass="list" DataKeyField="SystemID" AllowPaging="True">
											<AlternatingItemStyle CssClass=""></AlternatingItemStyle>
											<ItemStyle CssClass=""></ItemStyle>
											<HeaderStyle CssClass="list-title"></HeaderStyle>
											<FooterStyle CssClass="list-title"></FooterStyle>
											<Columns>
												<asp:TemplateColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" HeaderText="供应商代码">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<a href="#" onclick="Edit(this.Code);return false;" Code='<%#  DataBinder.Eval(Container.DataItem, "SystemID") %>'><%#  DataBinder.Eval(Container.DataItem, "SuplCode") %></a>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="SuplName" HeaderText="供应商名称"></asp:BoundColumn>
												<asp:TemplateColumn HeaderText="项目">
													<ItemTemplate>
														<%# RmsPM.BLL.ProjectRule.GetProjectName(DataBinder.Eval(Container.DataItem, "ProjectCode").ToString())%>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn>
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
		OpenCustomWindow("SalSuplModify.aspx?ProjectCode=" + Form1.txtProjectCode.value,"销售供应商修改", 300, 200);
	}

	function Edit(id)
	{
		OpenCustomWindow("SalSuplModify.aspx?SystemID=" + id,"销售供应商修改", 300, 200);
	}

	function Import()
	{
		OpenCustomWindow("SalSuplImport.aspx?ProjectCode=" + Form1.txtProjectCode.value,"导入销售供应商", 400, 300);
	}

//搜索条件按回车
function SearchKeyDown()
{
	if(event.keyCode==13)
	{
		event.keyCode = 9;
		Form1.btnSearch.click();
	}
}
		</script>
	</body>
</HTML>
