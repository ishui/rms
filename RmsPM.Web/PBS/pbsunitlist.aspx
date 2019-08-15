<%@ Reference Control="~/pbs/pbsunithintempty.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PBSUnitHintEmpty" Src="PBSUnitHintEmpty.ascx" %>
<%@ Page language="c#" Inherits="RmsPM.Web.PBS.PBSUnitList" CodeFile="PBSUnitList.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>PBSUnitList</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
	</HEAD>
	<body scroll="no">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td bgColor="#e4eff6" height="6"></td>
				</tr>
				<tr>
					<td height="25">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="topic" background="../images/topic_bg.gif"><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle">产品管理 
									- 单位工程</td>
								<td width="9"><img src="../images/topic_corr.gif" width="9" height="25"></td>
							</tr>
						</table>
					</td>
				</tr>
				<TR>
					<TD class="tools-area" valign="top">
						<IMG src="../images/btn_li.gif" align="absMiddle"> <input class="button" id="btnAdd" onclick="Add()" type="button" value="新 增" name="btnAdd"
							runat="server">
					</TD>
				</TR>
				<tr style="DISPLAY:none">
					<td class="table" vAlign="top">
						<table border="0" cellpadding="0" cellspacing="0" class="search-area">
							<tr>
								<td>
									<table>
										<tr>
											<td nowrap>单位工程名称：</td>
											<td nowrap><input id="txtSearchPBSUnitName" class="input" type="text" size="30" name="txtSearchPBSUnitName"
													runat="server"></td>
											<td><input class="submit" id="btnSearch" type="button" value="搜 索" name="btnSearch" runat="server"
													onclick="document.all.divHintLoad.style.display='';" onserverclick="btnSearch_ServerClick"></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td valign="top" class="table" bgcolor="#ffffff">
						<uc1:PBSUnitHintEmpty id="ucPBSUnitHintEmpty" runat="server"></uc1:PBSUnitHintEmpty>
					</td>
				</tr>
				<tr height="100%">
					<td class="table" valign="top">
						<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
							<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0" id="tbList"
								runat="server">
								<tr vAlign="top">
									<td><asp:datagrid id="dgList" runat="server" ShowFooter="True" PageSize="15" AutoGenerateColumns="False"
											AllowSorting="True" CellPadding="0" CssClass="list" Width="100%">
											<ItemStyle CssClass=""></ItemStyle>
											<HeaderStyle CssClass="list-title"></HeaderStyle>
											<FooterStyle CssClass="list-title"></FooterStyle>
											<Columns>
												<asp:TemplateColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" HeaderText="单位工程">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<a href="#" onclick="View(this.Code);return false;" Code='<%#  DataBinder.Eval(Container.DataItem, "PBSUnitCode") %>'><%#  DataBinder.Eval(Container.DataItem, "PBSUnitName") %></a>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="VisualProgressName" HeaderText="形象进度">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="ConstructUnit" HeaderText="施工单位">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn HeaderText="建筑面积<br>(平米)">
													<HeaderStyle HorizontalAlign="Right" Wrap="False"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right" Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%# RmsPM.BLL.MathRule.GetDecimalShowString(DataBinder.Eval(Container.DataItem, "TotalBuildArea"))%>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="计划投资<br>(万元)">
													<HeaderStyle HorizontalAlign="Right" Wrap="False"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right" Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%# RmsPM.BLL.MathRule.GetDecimalShowString(DataBinder.Eval(Container.DataItem, "PInvest"))%>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="实际投资<br>(万元)">
													<HeaderStyle HorizontalAlign="Right" Wrap="False"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right" Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%# RmsPM.BLL.MathRule.GetDecimalShowString(DataBinder.Eval(Container.DataItem, "Invest"))%>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="PStartDate" HeaderText="计划开工" DataFormatString="{0:yyyy-MM}">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="PEndDate" HeaderText="计划竣工" DataFormatString="{0:yyyy-MM}">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="StartDate" HeaderText="实际开工" DataFormatString="{0:yyyy-MM}">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="EndDate" HeaderText="实际竣工" DataFormatString="{0:yyyy-MM}">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn HeaderText="包含楼栋">
													<HeaderStyle HorizontalAlign="Left" Wrap="False"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
													<ItemTemplate>
														<%# RmsPM.BLL.ProductRule.GetBuildingNameByPBSUnit(DataBinder.Eval(Container.DataItem, "PBSUnitCode").ToString())%>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Left"></FooterStyle>
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
			<input type="hidden" id="txtProjectCode" name="txtProjectCode" runat="server"> <input type="hidden" id="txtParam" name="txtParam" runat="server">
			<input type="hidden" id="txtIYear" name="txtIYear" runat="server"> <input id="txtSumTotalBuildArea" name="txtSumTotalBuildArea" runat="server"><input id="txtSumPTotalInvest" name="txtSumPTotalInvest" runat="server">
			<input id="txtSumInvestBefore" name="txtSumInvestBefore" runat="server"><input id="txtSumLCFArea" name="txtSumLCFArea" runat="server">
			<input id="txtSumPInvest" name="txtSumPInvest" runat="server">
		</form>
		<SCRIPT language="javascript">
<!--

var CurrUrl = window.location.href;

//新增
function Add()
{
	OpenCustomWindow("PBSUnitModify.aspx?FromUrl=" + escape(CurrUrl) + "&ProjectCode=" + Form1.txtProjectCode.value, "修改单位工程", 760, 500);
//	window.location.href = "PBSUnitModify.aspx?FromUrl=" + escape(CurrUrl) + "&ProjectCode=" + Form1.txtProjectCode.value;
}

//查看
function View(code)
{
	//单位工程基本信息
	window.location.href = "../PBS/PBSUnitFrame.aspx?FromUrl=" + escape(CurrUrl) + "&PBSUnitCode=" + code + "&ProjectCode=" + Form1.txtProjectCode.value;
}

//-->
		</SCRIPT>
	</body>
</HTML>
