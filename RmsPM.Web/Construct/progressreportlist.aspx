<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Page language="c#" Inherits="RmsPM.Web.Construct.ProgressReportList" CodeFile="ProgressReportList.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>进度报告列表</title>
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
					<td bgColor="#e4eff6" height="6"></td>
				</tr>
				<tr>
					<td height="25">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="topic" noWrap background="../images/topic_bg.gif"><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle">计划管理 
									- 进度报告
								</td>
								<td width="9"><IMG height="25" src="../images/topic_corr.gif" width="9"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr style="DISPLAY: none">
					<td class="tools-area" vAlign="top"><IMG src="../images/btn_li.gif" align="absMiddle">
						<input class="button" id="btnShowPBSUnitWindow" style="DISPLAY: none" onclick="parent.OpenPBSUnitWindow();"
							type="button" value="显示单位工程" name="btnShowPBSUnitWindow" runat="server"> <input class="button" id="btnAdd" onclick="Add()" type="button" value="新 增" name="btnAdd"
							runat="server"> <input class="button" id="btnAllowPaging" style="DISPLAY: none" type="button" value="取消分页"
							name="btnAllowPaging" runat="server" onserverclick="btnAllowPaging_ServerClick"> <IMG src="../images/btn_li.gif" align="absMiddle">
						<input class="button" id="btnGotoPlan" style="DISPLAY: none" onclick="GotoPlan()" type="button"
							value="切换到工程计划" name="btnGotoPlan">
					</td>
				</tr>
				<tr>
					<td class="table" vAlign="top">
						<table class="search-area" id="tableSearch" cellSpacing="0" cellPadding="0" border="0"
							runat="server">
							<tr>
								<td>
									<table>
										<tr>
											<td noWrap>单位工程：</td>
											<td><select class="select" id="sltSearchPBSUnitCode" name="sltSearchPBSUnitCode" runat="server">
													<option value="" selected>--请选择--</option>
												</select>
											</td>
											<TD noWrap>报告日期：</TD>
											<TD><cc3:calendar id="dtSearchReportDateBegin" runat="server" Value="" Display="True" ReadOnly="False"
													CalendarResource="../Images/CalendarResource/"></cc3:calendar></TD>
											<TD noWrap>到：</TD>
											<TD><cc3:calendar id="dtSearchReportDateEnd" runat="server" Value="" Display="True" ReadOnly="False"
													CalendarResource="../Images/CalendarResource/"></cc3:calendar></TD>
											<TD noWrap>报告人：</TD>
											<TD><span id="spanReportPerson" runat="server"></span><A id="hrefSelectPerson" title="选择报告人" href="javascript:SelectPerson()" runat="server"><IMG src="../images/ToolsItemSearch.gif" border="0"></A>
											</TD>
											<td><input class="submit" id="btnSearch" type="button" value="搜 索" name="btnSearch" runat="server" onserverclick="btnSearch_ServerClick"></td>
										</tr>
										<tr>
											<td noWrap>形象进度：</td>
											<td><select class="select" id="sltSearchVisualProgress" name="sltSearchVisualProgress" runat="server">
													<option value="" selected>--请选择--</option>
												</select>
											</td>
											<td noWrap>进度报告内容：</td>
											<td><input class="input" id="txtSearchContent" type="text" name="txtSearchContent" runat="server"></td>
											<td noWrap>风险描述：</td>
											<td colSpan="4"><input class="input" id="txtSearchRiskRemark" type="text" name="txtSearchRiskRemark" runat="server"></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr style="DISPLAY: none" id="trPBSUnitName" runat="server">
					<td class="table" vAlign="bottom" align="center" height="25">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="note" noWrap>单位工程：<asp:label id="lblPBSUnitName" Runat="server"></asp:label></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr height="100%">
					<td class="table" vAlign="top">
						<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
							<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
								<tr vAlign="top">
									<td><asp:datagrid id="dgList" runat="server" AllowPaging="True" Width="100%" CssClass="list" CellPadding="0"
											AllowSorting="True" AutoGenerateColumns="False" PageSize="15" ShowFooter="False">
											<ItemStyle CssClass=""></ItemStyle>
											<HeaderStyle CssClass="list-title"></HeaderStyle>
											<FooterStyle CssClass="list-title"></FooterStyle>
											<Columns>
												<asp:TemplateColumn HeaderText="报告日期" SortExpression="ReportDate">
													<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
													<ItemTemplate>
														<a href="#" onclick="View(this.Code);return false;" Code='<%# DataBinder.Eval(Container, "DataItem.ProgressCode") %>'><%# DataBinder.Eval(Container, "DataItem.ReportDate", "{0:yyyy-MM-dd}") %></a>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="报告人" SortExpression="ReportPerson">
													<HeaderStyle HorizontalAlign="Left" Wrap="False"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left" Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%# RmsPM.BLL.SystemRule.GetUserName(RmsPM.BLL.ConvertRule.ToString(DataBinder.Eval(Container, "DataItem.ReportPerson")) ) %>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="单位工程" SortExpression="PBSUnitName">
													<HeaderStyle HorizontalAlign="Left" Wrap="False"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left" Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%# DataBinder.Eval(Container, "DataItem.PBSUnitName") %>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="VisualProgressName" HeaderText="形象进度" SortExpression="VisualProgressSortID">
													<HeaderStyle HorizontalAlign="Left" Wrap="False"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left" Wrap="False"></ItemStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn HeaderText="目前施工层数" SortExpression="CurrentLayer">
													<HeaderStyle HorizontalAlign="Right" Wrap="False"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right" Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%# DataBinder.Eval(Container, "DataItem.CurrentLayer") %>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="进度报告内容">
													<HeaderStyle HorizontalAlign="Left" Wrap="False"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
													<ItemTemplate>
														<%# RmsPM.BLL.StringRule.TruncText(DataBinder.Eval(Container, "DataItem.Content"), 20) %>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="风险描述">
													<HeaderStyle HorizontalAlign="Left" Wrap="False"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
													<ItemTemplate>
														<%# RmsPM.BLL.StringRule.TruncText(DataBinder.Eval(Container, "DataItem.RiskRemark"), 20) %>
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
			<input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server"> <input id="txtPBSUnitCode" type="hidden" name="txtPBSUnitCode" runat="server">
			<input id="txtReportPerson" type="hidden" name="txtReportPerson" runat="server"><input id="txtReportPersonName" type="hidden" name="txtReportPersonName" runat="server">
		</form>
		<SCRIPT language="javascript">
<!--

var CurrUrl = window.location.href;

//新增进度报告
function Add()
{
	OpenFullWindow("../Construct/ConstructProgressModify.aspx?PBSUnitCode=" + Form1.txtPBSUnitCode.value + "&action=insert", "进度报告修改", 760, 540);
}

//查看进度报告
function View(ProgressCode)
{
	OpenCustomWindow("../Construct/ProgressReportInfo.aspx?ProgressCode=" + ProgressCode, "进度报告", 750, 540);
//	window.location.href = "../Construct/ProgressReportInfo.aspx?ProgressCode=" + ProgressCode + "&FromUrl=" + escape(window.location);
}

//切换到工程计划
function GotoPlan()
{
	window.parent.location.href = "../Construct/ConstructPlan.aspx?PBSUnitCode=" + Form1.txtPBSUnitCode.value + "&ProjectCode=" + Form1.txtProjectCode.value;
}

//选择报告人
function SelectPerson()
{
	OpenCustomWindow("../SelectBox/SelectPerson.aspx?Flag=1&Type=Single", "选择负责人", 500, 580);
//	OpenCustomWindow("../SelectBox/SelectPerson.aspx?Flag=1&Type=Single&ProjectCode=" + Form1.txtProjectCode.value, "选择负责人", 500, 580);
}

//选择报告人返回
function DoSelectUser(code, name, flag)
{
	Form1.txtReportPerson.value = code;
	Form1.txtReportPersonName.value = name;
	document.all.spanReportPerson.innerText = name;
}

//-->
		</SCRIPT>
	</body>
</HTML>
