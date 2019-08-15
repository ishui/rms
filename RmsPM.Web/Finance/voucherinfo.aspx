<%@ Page language="c#" Inherits="RmsPM.Web.Finance.VoucherInfo" CodeFile="VoucherInfo.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ƾ֤</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<style>.ItemGridTr1 { COLOR: #000000; BACKGROUND-COLOR: #ffffdd }
	.ItemGridTr2 { COLOR: #000000; BACKGROUND-COLOR: #f5f5f5 }
		</style>
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
								<td class="topic" background="../images/topic_bg.gif"><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle"><span id="spanTitle" runat="server">������� 
										- ƾ֤��Ϣ</span></td>
								<td style="CURSOR: hand" onclick="GoBack(); return false;" width="79" id="tdBack" runat="server"><IMG height="25" src="../images/btn_back.jpg" width="79"></td>
								<td style="DISPLAY: none;CURSOR: hand" onclick="window.close();" id="tdClose" runat="server"
									width="9"><IMG height="25" src="../images/topic_corr.gif" width="9"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<TD class="tools-area" valign="top">
						<IMG src="../images/btn_li.gif" align="absMiddle"> <input class="button" onclick="DoModify();" type="button" value="�� ��" name="btnModify"
							id="btnModify" runat="server"> <input class="button" onclick="DoModify();" type="button" value="��˺��޸�" name="btnModifyEx"
							id="btnModifyEx" style="DISPLAY:none" runat="server"> <input class="button" id="btnDelete" onclick="javascript:if(!window.confirm('ȷʵҪɾ��������¼��')) return false;"
							type="button" value="ɾ ��" name="btnDelete" runat="server" onserverclick="btnDelete_ServerClick"> <input class="button" id="btnCheck" onclick="javascript:DoCheck();" type="button" value="�� ��"
							name="btnCheck" runat="server"> <input class="button" id="btnDownload" type="button" value="�� ��" name="btnDownload" runat="server"
							onclick="Download();">
					</TD>
				</tr>
				<TR>
					<TD class="table" vAlign="top">
						<TABLE class="form" id="Table4" cellSpacing="1" cellPadding="0" width="100%">
							<TR>
								<TD class="form-item">ƾ֤��ţ�</TD>
								<TD><asp:label id="lblVoucherID" runat="server"></asp:label></TD>
								<TD class="form-item">ƾ֤���ͣ�</TD>
								<TD><asp:label id="lblVoucherType" runat="server"></asp:label></TD>
								<TD class="form-item">״̬��</TD>
								<TD><asp:label id="lblStatusName" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="form-item">�� �� �ˣ�</TD>
								<TD><asp:label id="lblAccountantName" runat="server"></asp:label></TD>
								<TD class="form-item">�Ƶ����ڣ�</TD>
								<TD><asp:label id="lblMakeDate" runat="server"></asp:label></TD>
								<TD class="form-item">����������</TD>
								<TD><asp:label id="lblReceiptCount" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="form-item">�� �� �ˣ�</TD>
								<TD><asp:label id="lblCheckPersonName" runat="server"></asp:label></TD>
								<TD class="form-item">������ڣ�</TD>
								<TD><asp:label id="lblCheckDate" runat="server"></asp:label></TD>
								<TD class="form-item">�������ڣ�</TD>
								<TD><asp:label id="lblOutPutDate" runat="server"></asp:label></TD>								
							</TR>
						</TABLE>
					</TD>
				</TR>
				<tr height="100%">
					<td class="table" vAlign="top">
						<table cellSpacing="0" cellPadding="0" border="0" height="100%" width="100%">
							<tr>
								<td class="intopic" width="200">ƾ֤��¼</td>
							</tr>
							<tr height="100%" valign="top">
								<td>
									<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
										<asp:datagrid id="dgList" runat="server" CssClass="list" Width="100%" PageSize="15" AutoGenerateColumns="False"
											AllowSorting="True" GridLines="Horizontal" CellPadding="0" ShowFooter="True">
											<AlternatingItemStyle CssClass=""></AlternatingItemStyle>
											<ItemStyle CssClass=""></ItemStyle>
											<HeaderStyle CssClass="list-title"></HeaderStyle>
											<FooterStyle CssClass="list-title"></FooterStyle>
											<Columns>
												<asp:TemplateColumn HeaderText="���" Visible="True">
													<HeaderStyle HorizontalAlign="Center" Wrap="False"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center" Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%# Container.ItemIndex + 1 %>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="Summary" HeaderText="ժҪ" FooterText="�ϼ�">
													<HeaderStyle Wrap="False"></HeaderStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn HeaderText="��Ŀ">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemTemplate>
														<span title="<%=SubjectSetName%>"><%# DataBinder.Eval(Container, "DataItem.SubjectName") %></span>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="��">
													<HeaderStyle HorizontalAlign="Right" Wrap="False"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right" Wrap="False"></ItemStyle>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
													<ItemTemplate>
														<asp:Label Runat="server" ID="lblDebitMoney" Text='<%# RmsPM.BLL.MathRule.GetDecimalShowString(DataBinder.Eval(Container, "DataItem.DebitMoney")) %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="��">
													<HeaderStyle HorizontalAlign="Right" Wrap="False"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right" Wrap="False"></ItemStyle>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
													<ItemTemplate>
														<asp:Label Runat="server" ID="lblCrebitMoney" Text='<%# RmsPM.BLL.MathRule.GetDecimalShowString(DataBinder.Eval(Container, "DataItem.CrebitMoney")) %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="��ͬ���">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<a style="cursor:hand" onclick="javascript:GotoContract(this.code);" code='<%#  DataBinder.Eval(Container.DataItem, "ContractCode") %>'>
															<%# DataBinder.Eval(Container, "DataItem.ContractID") %>
														</a>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="SupplyName" HeaderText="��Ӧ��">
													<HeaderStyle Wrap="False"></HeaderStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn HeaderText="�ͻ�">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemTemplate>
														<a style="cursor:hand" onclick="javascript:GotoSupplier(this.code);" code='<%#  DataBinder.Eval(Container.DataItem, "CustCode") %>'>
															<%# DataBinder.Eval(Container, "DataItem.CustName") %>
														</a>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="����">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<a style="cursor:hand" onclick="javascript:GotoUnit(this.code);" code='<%#  DataBinder.Eval(Container.DataItem, "UFUnitCode") %>'>
															<%# DataBinder.Eval(Container, "DataItem.UFUnitName") %>
														</a>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="��Ա">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<a style="cursor:hand" onclick="javascript:GotoUser(this.code);" code='<%#  DataBinder.Eval(Container.DataItem, "PaymentCheckPerson") %>'>
															<%# DataBinder.Eval(Container, "DataItem.PaymentCheckPersonName") %>
														</a>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="��λ����">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<a style="cursor:hand" onclick="javascript:GotoPBS(this.PBSType, this.code);" PBSType='<%#  DataBinder.Eval(Container.DataItem, "PBSType") %>' code='<%#  DataBinder.Eval(Container.DataItem, "PBSCode") %>'>
															<%# DataBinder.Eval(Container, "DataItem.PBSName") %>
														</a>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="��Ŀ">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%# DataBinder.Eval(Container, "DataItem.UFProjectName") %>
														<a style="cursor:hand" onclick="javascript:GotoProject(this.code);" code='<%#  DataBinder.Eval(Container.DataItem, "UFProjectCode") %>' style="display:none">
														</a>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="BillNo" HeaderText="Ʊ��">
													<ItemStyle Wrap="False"></ItemStyle>
													<HeaderStyle Wrap="False"></HeaderStyle>
												</asp:BoundColumn>
											</Columns>
											<PagerStyle NextPageText="��ҳ&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;��ҳ" HorizontalAlign="Right"
												CssClass="ListHeadTr"></PagerStyle>
										</asp:datagrid>
									</div>
								</td>
							</tr>
						</table>
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
			<input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server"><INPUT id="txtVoucherCode" type="hidden" name="txtPaymentCode" runat="server">
			<input id="txtAct" type="hidden" name="txtAct" runat="server"> <input id="txtFromUrl" type="hidden" name="txtFromUrl" runat="server">
			<input id="txtOpen" type="hidden" name="txtOpen" runat="server"><input id="txtSubjectSetCode" type="hidden" name="txtSubjectSetCode" runat="server">
			<input id="txtStatus" type="hidden" name="txtStatus" runat="server"><input id="txtSubjectSetName" type="hidden" name="txtSubjectSetName" runat="server">
		</form>
		<SCRIPT language="javascript">
<!--

//�޸�
function DoModify()
{
	var voucherCode = Form1.txtVoucherCode.value;
	OpenFullWindow('../Finance/VoucherModify.aspx?VoucherCode=' + voucherCode,'�޸�ƾ֤');
}
	
function GoBack()
{
	if (Form1.txtFromUrl.value == "")
	{
		window.location.href = "VoucherList.aspx?ProjectCode=" + Form1.txtProjectCode.value;
	}
	else
	{
		window.location.href = Form1.txtFromUrl.value;
	}
}

//���
function DoCheck()
{
	var voucherCode = Form1.txtVoucherCode.value;
	OpenCustomWindow('../Finance/VoucherCheck.aspx?VoucherCode=' + voucherCode,"ƾ֤���",  550, 350);
}

//����
function Download()
{
	OpenCustomWindow("../Finance/VoucherFileCheck.aspx?VoucherCode=" + Form1.txtVoucherCode.value, "ƾ֤����", 550, 350);
}

//�鿴����
function GotoUnit(UnitCode)
{
	OpenCustomWindow("FinanceInterfaceAnalysisUnitModify.aspx?UnitCode=" + UnitCode + "&SubjectSetCode=" + Form1.txtSubjectSetCode.value, "���Ų�������޸�", 300, 180);
//	OpenCustomWindow("../Systems/DepartmentInfo.aspx?UnitCode=" + UnitCode + "&act=view", "����", 550, 350);
}

//�鿴��Ա
function GotoUser(UserCode)
{
	OpenCustomWindow("FinanceInterfaceAnalysisUserModify.aspx?UserCode=" + UserCode + "&SubjectSetCode=" + Form1.txtSubjectSetCode.value, "��Ա��������޸�", 300, 180);
//	OpenCustomWindow("../Systems/DepartmentInfo.aspx?UnitCode=" + UnitCode + "&act=view", "����", 550, 350);
}

//�鿴��Ӧ��
function GotoSupplier(SupplierCode)
{
    OpenCustomWindow("FinanceInterfaceAnalysisSupplierModify.aspx?SupplierCode=" + SupplierCode + "&ProjectCode=" + Form1.txtProjectCode.value + "&SubjectSetCode=" + Form1.txtSubjectSetCode.value, "�޸ĳ��̲������", 500, 300);
//	OpenFullWindow("../Supplier/SupplierInfo.aspx?SupplierCode=" + SupplierCode + "&act=view", "��Ӧ��");
}

//�鿴��λ����
function GotoPBS(PBSType, PBSCode)
{
	if (PBSType == "B")
	{
		OpenCustomWindow("FinanceInterfaceAnalysisBuildingModify.aspx?BuildingCode=" + PBSCode + "&SubjectSetCode=" + Form1.txtSubjectSetCode.value, "¥����������޸�", 300, 180);
//		OpenCustomWindow("../PBS/PBSBuildInfo.aspx?BuildingCode=" + PBSCode + "&OpenModal=open&action=view", "BuildingInfo", 700, 540);
	}
	else
	{
		OpenCustomWindow("FinanceInterfaceAnalysisProjectModify.aspx?ProjectCode=" + Form1.txtProjectCode.value + "&SubjectSetCode=" + Form1.txtSubjectSetCode.value, "��Ŀ��������޸�", 300, 180);
//		OpenCustomWindow("../Project/ProjectInfo.aspx?ProjectCode=" + Form1.txtProjectCode.value, "ProjectInfo", 760, 540);
	}
}

//�鿴��Ŀ
function GotoProject(ProjectCode)
{
	OpenLargeWindow("../Project/ProjectInfo.aspx?ProjectCode=" + ProjectCode + "&act=view", "��Ŀ��Ϣ");
}

//�鿴��ͬ
function GotoContract(ContractCode)
{
	OpenFullWindow("../Contract/ContractInfo.aspx?ProjectCode=" + document.all.txtProjectCode.value + "&ContractCode=" + ContractCode,'��ͬ��Ϣ');
}

//-->
		</SCRIPT>
	</body>
</HTML>
