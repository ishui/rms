<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Page language="c#" Inherits="RmsPM.Web.Construct.ProgressReportList" CodeFile="ProgressReportList.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>���ȱ����б�</title>
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
								<td class="topic" noWrap background="../images/topic_bg.gif"><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle">�ƻ����� 
									- ���ȱ���
								</td>
								<td width="9"><IMG height="25" src="../images/topic_corr.gif" width="9"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr style="DISPLAY: none">
					<td class="tools-area" vAlign="top"><IMG src="../images/btn_li.gif" align="absMiddle">
						<input class="button" id="btnShowPBSUnitWindow" style="DISPLAY: none" onclick="parent.OpenPBSUnitWindow();"
							type="button" value="��ʾ��λ����" name="btnShowPBSUnitWindow" runat="server"> <input class="button" id="btnAdd" onclick="Add()" type="button" value="�� ��" name="btnAdd"
							runat="server"> <input class="button" id="btnAllowPaging" style="DISPLAY: none" type="button" value="ȡ����ҳ"
							name="btnAllowPaging" runat="server" onserverclick="btnAllowPaging_ServerClick"> <IMG src="../images/btn_li.gif" align="absMiddle">
						<input class="button" id="btnGotoPlan" style="DISPLAY: none" onclick="GotoPlan()" type="button"
							value="�л������̼ƻ�" name="btnGotoPlan">
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
											<td noWrap>��λ���̣�</td>
											<td><select class="select" id="sltSearchPBSUnitCode" name="sltSearchPBSUnitCode" runat="server">
													<option value="" selected>--��ѡ��--</option>
												</select>
											</td>
											<TD noWrap>�������ڣ�</TD>
											<TD><cc3:calendar id="dtSearchReportDateBegin" runat="server" Value="" Display="True" ReadOnly="False"
													CalendarResource="../Images/CalendarResource/"></cc3:calendar></TD>
											<TD noWrap>����</TD>
											<TD><cc3:calendar id="dtSearchReportDateEnd" runat="server" Value="" Display="True" ReadOnly="False"
													CalendarResource="../Images/CalendarResource/"></cc3:calendar></TD>
											<TD noWrap>�����ˣ�</TD>
											<TD><span id="spanReportPerson" runat="server"></span><A id="hrefSelectPerson" title="ѡ�񱨸���" href="javascript:SelectPerson()" runat="server"><IMG src="../images/ToolsItemSearch.gif" border="0"></A>
											</TD>
											<td><input class="submit" id="btnSearch" type="button" value="�� ��" name="btnSearch" runat="server" onserverclick="btnSearch_ServerClick"></td>
										</tr>
										<tr>
											<td noWrap>������ȣ�</td>
											<td><select class="select" id="sltSearchVisualProgress" name="sltSearchVisualProgress" runat="server">
													<option value="" selected>--��ѡ��--</option>
												</select>
											</td>
											<td noWrap>���ȱ������ݣ�</td>
											<td><input class="input" id="txtSearchContent" type="text" name="txtSearchContent" runat="server"></td>
											<td noWrap>����������</td>
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
								<td class="note" noWrap>��λ���̣�<asp:label id="lblPBSUnitName" Runat="server"></asp:label></td>
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
												<asp:TemplateColumn HeaderText="��������" SortExpression="ReportDate">
													<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
													<ItemTemplate>
														<a href="#" onclick="View(this.Code);return false;" Code='<%# DataBinder.Eval(Container, "DataItem.ProgressCode") %>'><%# DataBinder.Eval(Container, "DataItem.ReportDate", "{0:yyyy-MM-dd}") %></a>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="������" SortExpression="ReportPerson">
													<HeaderStyle HorizontalAlign="Left" Wrap="False"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left" Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%# RmsPM.BLL.SystemRule.GetUserName(RmsPM.BLL.ConvertRule.ToString(DataBinder.Eval(Container, "DataItem.ReportPerson")) ) %>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="��λ����" SortExpression="PBSUnitName">
													<HeaderStyle HorizontalAlign="Left" Wrap="False"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left" Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%# DataBinder.Eval(Container, "DataItem.PBSUnitName") %>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="VisualProgressName" HeaderText="�������" SortExpression="VisualProgressSortID">
													<HeaderStyle HorizontalAlign="Left" Wrap="False"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left" Wrap="False"></ItemStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn HeaderText="Ŀǰʩ������" SortExpression="CurrentLayer">
													<HeaderStyle HorizontalAlign="Right" Wrap="False"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right" Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%# DataBinder.Eval(Container, "DataItem.CurrentLayer") %>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="���ȱ�������">
													<HeaderStyle HorizontalAlign="Left" Wrap="False"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
													<ItemTemplate>
														<%# RmsPM.BLL.StringRule.TruncText(DataBinder.Eval(Container, "DataItem.Content"), 20) %>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="��������">
													<HeaderStyle HorizontalAlign="Left" Wrap="False"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
													<ItemTemplate>
														<%# RmsPM.BLL.StringRule.TruncText(DataBinder.Eval(Container, "DataItem.RiskRemark"), 20) %>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle NextPageText="��ҳ&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;��ҳ" HorizontalAlign="Center"
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

//�������ȱ���
function Add()
{
	OpenFullWindow("../Construct/ConstructProgressModify.aspx?PBSUnitCode=" + Form1.txtPBSUnitCode.value + "&action=insert", "���ȱ����޸�", 760, 540);
}

//�鿴���ȱ���
function View(ProgressCode)
{
	OpenCustomWindow("../Construct/ProgressReportInfo.aspx?ProgressCode=" + ProgressCode, "���ȱ���", 750, 540);
//	window.location.href = "../Construct/ProgressReportInfo.aspx?ProgressCode=" + ProgressCode + "&FromUrl=" + escape(window.location);
}

//�л������̼ƻ�
function GotoPlan()
{
	window.parent.location.href = "../Construct/ConstructPlan.aspx?PBSUnitCode=" + Form1.txtPBSUnitCode.value + "&ProjectCode=" + Form1.txtProjectCode.value;
}

//ѡ�񱨸���
function SelectPerson()
{
	OpenCustomWindow("../SelectBox/SelectPerson.aspx?Flag=1&Type=Single", "ѡ������", 500, 580);
//	OpenCustomWindow("../SelectBox/SelectPerson.aspx?Flag=1&Type=Single&ProjectCode=" + Form1.txtProjectCode.value, "ѡ������", 500, 580);
}

//ѡ�񱨸��˷���
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
