<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Page language="c#" Inherits="RmsPM.Web.ConstructProg.BuildingFloorModify" CodeFile="BuildingFloorModify.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>楼栋工程结构</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<SCRIPT language="javascript" src="../images/convert.js"></SCRIPT>
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0" bgcolor="white">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">楼栋工程结构</td>
				</tr>
				<tr>
					<td>
						<TABLE class="form" cellSpacing="0" cellPadding="0" width="100%">
							<TR>
								<TD class="form-item" noWrap width="80">楼栋名称：</TD>
								<TD><asp:label id="lblBuildingName" Runat="server"></asp:label></TD>
								<TD class="form-item" width="80">总 层 数：</TD>
								<TD><asp:label id="lblFloorCount" Runat="server"></asp:label></TD>
							</TR>
						</TABLE>
						<br>
					</td>
				</tr>
				<tr>
					<td vAlign="top">
						<table cellSpacing="0" cellPadding="0" border="0">
							<tr>
								<td class="intopic" width="200">楼层结构</td>
								<td><input class="button-small" id="btnAddDtl" type="button" value="新增底层" name="btnAddDtl"
										runat="server" onserverclick="btnAddDtl_ServerClick"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr height="100%">
					<td vAlign="top" class="topic">
						<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
							<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
								<tr vAlign="top">
									<td><asp:datagrid id="dgList" runat="server" Width="100%" CssClass="list" CellPadding="0" AllowSorting="True"
											AutoGenerateColumns="False" PageSize="15" ShowFooter="True" DataKeyField="BuildingFloorCode">
											<ItemStyle CssClass=""></ItemStyle>
											<HeaderStyle CssClass="list-title"></HeaderStyle>
											<FooterStyle CssClass="list-title"></FooterStyle>
											<Columns>
												<asp:TemplateColumn HeaderText="楼层名称">
													<ItemTemplate>
														<input type="text" class="input" size="30" id="txtFloorName" name="txtFloorName" runat="server" value='<%# DataBinder.Eval(Container, "DataItem.FloorName") %>'>
														<input type="hidden" name="txtBuildingFloorCode" id="txtBuildingFloorCode" runat="server" value='<%# DataBinder.Eval(Container, "DataItem.BuildingFloorCode") %>'>
													</ItemTemplate>
													<FooterTemplate>
														<asp:Label runat="server" ID="lblSumPTotalInvest"></asp:Label>
													</FooterTemplate>
													<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<FooterStyle HorizontalAlign="Center"></FooterStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="插入">
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
													<ItemTemplate>
														<asp:LinkButton runat="server" Text="&lt;img src=../images/DtlInsert.gif width=16 height=16 border=0&gt;"
															CommandName="Edit" CausesValidation="false" ID="btnInsertDtl"></asp:LinkButton>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="删除">
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
													<ItemTemplate>
														<asp:LinkButton runat="server" Text="&lt;img src=../images/del.gif width=16 height=16 border=0&gt;"
															CommandName="Delete" CausesValidation="false" ID="btnDeleteDtl"></asp:LinkButton>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle NextPageText="下页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上页" HorizontalAlign="Center"
												CssClass="ListHeadTr"></PagerStyle>
										</asp:datagrid></td>
								</tr>
							</table>
						</div>
					</td>
				</tr>
				<tr>
					<td>
						<table cellSpacing="10" width="100%">
							<tr>
								<td align="center"><input class="submit" id="btnSave" type="button" value="确 定" onclick="document.all.divHintSave.style.display='';"
										name="btnSave" runat="server" onserverclick="btnSave_ServerClick"> <input class="submit" id="btnCancel" onclick="javascript:self.close()" type="button" value="取 消"
										name="btnCancel">
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<div id="divHintLoad" style="DISPLAY: none; LEFT: 1px; WIDTH: 100%; POSITION: absolute; TOP: 150px; BACKGROUND-COLOR: transparent">
				<TABLE id="tableHintLoad" height="100" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD vAlign="top" align="center"><iframe id="frameLoad" src="../Cost/LoadingPrepare.htm" frameBorder="no" width="100%" scrolling="auto"
								height="100%"></iframe>
						</TD>
					</TR>
				</TABLE>
			</div>
			<div id="divHintSave" style="DISPLAY: none; LEFT: 1px; WIDTH: 100%; POSITION: absolute; TOP: 150px">
				<TABLE id="tableHintSave" height="100" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD vAlign="top" align="center"><iframe id="frameSave" src="../Cost/SavingWating.htm" frameBorder="no" width="100%" scrolling="auto"
								height="100%"></iframe>
						</TD>
					</TR>
				</TABLE>
			</div>
			<input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server"> <input id="txtAct" type="hidden" name="txtAct" runat="server">
			<input id="txtBuildingCode" type="hidden" name="txtBuildingCode" runat="server"><input id="txtDetailSno" name="txDetailSno" runat="server">
		</form>
		<script language="javascript">
		</script>
	</body>
</HTML>
