<%@ Page language="c#" Inherits="RmsPM.Web.ConstructProg.BuildingFloorInfo" CodeFile="BuildingFloorInfo.aspx.cs" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>楼栋工程结构</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
	</HEAD>
	<body style="BORDER-RIGHT: 0px" scroll="no">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="tools-area" vAlign="top"><IMG src="../images/btn_li.gif" align="absMiddle">
						<input class="button" id="btnAdd" onclick="Modify()" type="button" value="新建结构" name="btnAdd"
							runat="server"> <input class="button" id="btnModify" onclick="Modify()" type="button" value="修改结构" name="btnModify"
							runat="server"> <input class="button" id="btnDelete" onclick="if (!Delete()) return;" type="button" value="删除结构"
							name="btnDelete" runat="server" onserverclick="btnDelete_ServerClick"> <IMG src="../images/btn_li.gif" align="absMiddle">
						<input class="button" id="btnGotoProgress" onclick="GotoProgress()" type="button" value="切换到楼栋进度"
							name="btnGotoProgress">
					</td>
				</tr>
				<tr>
					<td class="table">
						<TABLE class="form" cellSpacing="0" cellPadding="0" width="100%">
							<TR>
								<TD class="form-item" noWrap width="120">楼栋名称：</TD>
								<TD><asp:label id="lblBuildingName" Runat="server"></asp:label></TD>
								<TD class="form-item">总 层 数：</TD>
								<TD><asp:label id="lblFloorCount" Runat="server"></asp:label></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
				<tr height="100%">
					<td class="table" vAlign="top">
						<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td vAlign="top">
									<table cellSpacing="0" cellPadding="0" border="0">
										<tr>
											<td class="intopic" width="200">楼层结构</td>
										</tr>
									</table>
								</td>
							</tr>
							<tr vAlign="top" height="100%">
								<td>
									<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
										<asp:datagrid id="dgList" runat="server" AllowPaging="False" Width="100%" CssClass="list" CellPadding="0"
											AllowSorting="True" AutoGenerateColumns="False" PageSize="15" ShowFooter="False">
											<ItemStyle CssClass=""></ItemStyle>
											<HeaderStyle CssClass="list-title"></HeaderStyle>
											<FooterStyle CssClass="list-title"></FooterStyle>
											<Columns>
												<asp:TemplateColumn HeaderText="楼层名称">
													<HeaderStyle HorizontalAlign="Left" Wrap="False"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left" Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%# DataBinder.Eval(Container, "DataItem.FloorName") %>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle NextPageText="下页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上页" HorizontalAlign="Center"
												CssClass="ListHeadTr"></PagerStyle>
										</asp:datagrid>
									</div>
								</td>
							</tr>
						</table>
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
			<input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server"> <input id="txtBuildingCode" type="hidden" name="txtBuildingCode" runat="server">
			<input id="txtDefaultVisualProgress" type="hidden" name="txtDefaultVisualProgress" runat="server">
		</form>
		<SCRIPT language="javascript">
<!--

var CurrUrl = window.location.href;

//修改楼层结构
function Modify()
{
	OpenCustomWindow("../ConstructProg/BuildingFloorModify.aspx?BuildingCode=" + Form1.txtBuildingCode.value, "楼层结构修改", 500, 540);
//	window.location.href = "../Construct/ProgressReportInfo.aspx?ProgressCode=" + ProgressCode + "&FromUrl=" + escape(window.location);
}

function Delete()
{
	if (!confirm("确实要删除吗？"))
		return false;
		
	document.all.divHintSave.style.display = "";
	return true;
}

//切换到楼栋进度
function GotoProgress()
{
	document.all.divHintLoad.style.display='';
	window.parent.navigate("../ConstructProg/BuildingFloorProgressFrame.aspx?ProjectCode=" + Form1.txtProjectCode.value + "&BuildingCode=" + Form1.txtBuildingCode.value + "&DefaultVisualProgress=" + Form1.txtDefaultVisualProgress.value);
}

//-->
		</SCRIPT>
	</body>
</HTML>
