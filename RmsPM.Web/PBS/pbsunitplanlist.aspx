<%@ Reference Control="~/pbs/pbsunithintempty.ascx" %>
<%@ Page language="c#" Inherits="RmsPM.Web.PBS.PBSUnitPlanList" CodeFile="PBSUnitPlanList.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="PBSUnitHintEmpty" Src="PBSUnitHintEmpty.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>PBSUnitPlanList</title>
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
								<td class="topic" background="../images/topic_bg.gif"><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle">��Ʒ���� 
									- ���̼ƻ�</td>
								<td width="9"><img src="../images/topic_corr.gif" width="9" height="25"></td>
							</tr>
						</table>
					</td>
				</tr>
				<TR>
					<TD class="tools-area" valign="top">
						<IMG src="../images/btn_li.gif" align="absMiddle"> <input class="button" type="button" value="�����޸���ȼƻ�" name="btnBatchEditPlan" id="btnBatchEditPlan"
							runat="server" onclick="BatchEditPlan();"> <input class="button" type="button" value="ɾ����ȼƻ�" name="btnDeleteYearPlan" id="btnDeleteYearPlan"
							runat="server" onclick="if (!DeleteYearPlan()) return false;" onserverclick="btnDeleteYearPlan_ServerClick"> <input class="button" id="btnNewYearPlan" onclick="NewYearPlan();" type="button" value="��ת����"
							name="btnNewYearPlan" runat="server" onserverclick="btnNewYearPlan_ServerClick">
					</TD>
				</TR>
				<tr>
					<td class="table" vAlign="top">
						<table>
							<tr>
								<td class="note" align="left" style="PADDING-LEFT:20px"><asp:Label Runat="server" ID="lblIYear"></asp:Label>��ȼƻ�</td>
							</tr>
						</table>
						<table border="0" cellpadding="0" cellspacing="0" class="search-area">
							<tr>
								<td>
									<table>
										<tr>
											<td nowrap>��λ�������ƣ�</td>
											<td nowrap><input id="txtSearchPBSUnitName" class="input" type="text" size="30" name="txtSearchPBSUnitName"
													runat="server"></td>
											<td><input class="submit" id="btnSearch" type="button" value="�� ��" name="btnSearch" runat="server"
													onclick="document.all.divHintLoad.style.display='';" onserverclick="btnSearch_ServerClick"></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<uc1:PBSUnitHintEmpty id="ucPBSUnitHintEmpty" runat="server"></uc1:PBSUnitHintEmpty>
					</td>
				</tr>
				<tr height="100%">
					<td class="table" valign="top">
						<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
							<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0" id="tbPlanList"
								runat="server">
								<tr vAlign="top">
									<td><asp:datagrid id="dgPlanList" runat="server" ShowFooter="True" PageSize="15" AutoGenerateColumns="False"
											AllowSorting="True" CellPadding="0" CssClass="list" Width="100%">
											<ItemStyle CssClass=""></ItemStyle>
											<HeaderStyle CssClass="list-title"></HeaderStyle>
											<FooterStyle CssClass="list-title"></FooterStyle>
											<Columns>
												<asp:TemplateColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" HeaderText="��λ����" FooterText="�ϼ�">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<a href="#" onclick="View(this.Code);return false;" Code='<%#  DataBinder.Eval(Container.DataItem, "PBSUnitCode") %>'><%#  DataBinder.Eval(Container.DataItem, "PBSUnitName") %></a>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="PBSUnitVisualProgress" HeaderText="��ǰ�������" Visible="False"></asp:BoundColumn>
												<asp:BoundColumn DataField="ConstructUnit" HeaderText="ʩ����λ"></asp:BoundColumn>
												<asp:BoundColumn DataField="VisualProgressName" HeaderText="����ƻ��������"></asp:BoundColumn>
												<asp:TemplateColumn HeaderText="�������<br>(ƽ��)">
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<%# RmsPM.BLL.MathRule.GetDecimalShowString(DataBinder.Eval(Container.DataItem, "TotalBuildArea"))%>
													</ItemTemplate>
													<FooterTemplate>
														<asp:Label runat="server" ID="lblSumTotalBuildArea"></asp:Label>
													</FooterTemplate>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="�����ת���<br>(ƽ��)">
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<%# RmsPM.BLL.MathRule.GetDecimalShowString(DataBinder.Eval(Container.DataItem, "LCFArea"))%>
													</ItemTemplate>
													<FooterTemplate>
														<asp:Label runat="server" ID="lblSumLCFArea"></asp:Label>
													</FooterTemplate>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="�ƻ���Ͷ��<br>(��Ԫ)">
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<%# RmsPM.BLL.MathRule.GetDecimalShowString(DataBinder.Eval(Container.DataItem, "PTotalInvest"))%>
													</ItemTemplate>
													<FooterTemplate>
														<asp:Label runat="server" ID="lblSumPTotalInvest"></asp:Label>
													</FooterTemplate>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="�������Ͷ��<br>(��Ԫ)">
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<%# RmsPM.BLL.MathRule.GetDecimalShowString(DataBinder.Eval(Container.DataItem, "InvestBefore"))%>
													</ItemTemplate>
													<FooterTemplate>
														<asp:Label runat="server" ID="lblSumInvestBefore"></asp:Label>
													</FooterTemplate>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="����ƻ�Ͷ��<br>(��Ԫ)">
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<%# RmsPM.BLL.MathRule.GetDecimalShowString(DataBinder.Eval(Container.DataItem, "PInvest"))%>
													</ItemTemplate>
													<FooterTemplate>
														<asp:Label runat="server" ID="lblSumPInvest"></asp:Label>
													</FooterTemplate>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="PStartDate" HeaderText="�ƻ�����" DataFormatString="{0:yyyy-MM}" Visible="False"></asp:BoundColumn>
												<asp:BoundColumn DataField="PEndDate" HeaderText="�ƻ�����" DataFormatString="{0:yyyy-MM}" Visible="False"></asp:BoundColumn>
												<asp:BoundColumn DataField="StartDate" HeaderText="ʵ�ʿ���" DataFormatString="{0:yyyy-MM}" Visible="False"></asp:BoundColumn>
												<asp:BoundColumn DataField="EndDate" HeaderText="ʵ�ʿ���" DataFormatString="{0:yyyy-MM}" Visible="False"></asp:BoundColumn>
												<asp:TemplateColumn HeaderText="����ƻ�ʩ������">
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<%# RmsPM.BLL.MathRule.GetIntShowString(DataBinder.Eval(Container.DataItem, "CurrentFloor"))%>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
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

//�鿴
function View(code)
{
	//�ƻ�
	window.location.href = "../Construct/ConstructPlan.aspx?FromUrl=" + escape(CurrUrl) + "&PBSUnitCode=" + code + "&ProjectCode=" + Form1.txtProjectCode.value;
}

//�����޸���ȼƻ�
function BatchEditPlan()
{
	OpenCustomWindow("../Construct/BatchEditPlan.aspx?ProjectCode=" + Form1.txtProjectCode.value, "��ȼƻ�", 760, 540);
}

//ɾ����ȼƻ�
function DeleteYearPlan()
{
	var y = Form1.txtIYear.value;
	if(!window.confirm('ȷʵҪɾ��' + y + '��ȼƻ���'))
		return false;

	document.all.divHintSave.style.display = '';
	return true;
}

//����ȼƻ�
function NewYearPlan()
{
	var y = Form1.txtIYear.value;
	if (y != "")
	{
		if (!confirm("ȷʵҪ��ת" + y + "��ļƻ���"))
			return;
	}
	
	document.all.divHintSave.style.display = '';
}

//-->
		</SCRIPT>
	</body>
</HTML>
