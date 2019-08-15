<%@ Page language="c#" Inherits="RmsPM.Web.Finance.VoucherInfo" CodeFile="VoucherInfo.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>凭证</title>
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
								<td class="topic" background="../images/topic_bg.gif"><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle"><span id="spanTitle" runat="server">财务管理 
										- 凭证信息</span></td>
								<td style="CURSOR: hand" onclick="GoBack(); return false;" width="79" id="tdBack" runat="server"><IMG height="25" src="../images/btn_back.jpg" width="79"></td>
								<td style="DISPLAY: none;CURSOR: hand" onclick="window.close();" id="tdClose" runat="server"
									width="9"><IMG height="25" src="../images/topic_corr.gif" width="9"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<TD class="tools-area" valign="top">
						<IMG src="../images/btn_li.gif" align="absMiddle"> <input class="button" onclick="DoModify();" type="button" value="修 改" name="btnModify"
							id="btnModify" runat="server"> <input class="button" onclick="DoModify();" type="button" value="审核后修改" name="btnModifyEx"
							id="btnModifyEx" style="DISPLAY:none" runat="server"> <input class="button" id="btnDelete" onclick="javascript:if(!window.confirm('确实要删除这条记录吗？')) return false;"
							type="button" value="删 除" name="btnDelete" runat="server" onserverclick="btnDelete_ServerClick"> <input class="button" id="btnCheck" onclick="javascript:DoCheck();" type="button" value="审 核"
							name="btnCheck" runat="server"> <input class="button" id="btnDownload" type="button" value="导 出" name="btnDownload" runat="server"
							onclick="Download();">
					</TD>
				</tr>
				<TR>
					<TD class="table" vAlign="top">
						<TABLE class="form" id="Table4" cellSpacing="1" cellPadding="0" width="100%">
							<TR>
								<TD class="form-item">凭证编号：</TD>
								<TD><asp:label id="lblVoucherID" runat="server"></asp:label></TD>
								<TD class="form-item">凭证类型：</TD>
								<TD><asp:label id="lblVoucherType" runat="server"></asp:label></TD>
								<TD class="form-item">状态：</TD>
								<TD><asp:label id="lblStatusName" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="form-item">制 单 人：</TD>
								<TD><asp:label id="lblAccountantName" runat="server"></asp:label></TD>
								<TD class="form-item">制单日期：</TD>
								<TD><asp:label id="lblMakeDate" runat="server"></asp:label></TD>
								<TD class="form-item">单据张数：</TD>
								<TD><asp:label id="lblReceiptCount" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="form-item">审 核 人：</TD>
								<TD><asp:label id="lblCheckPersonName" runat="server"></asp:label></TD>
								<TD class="form-item">审核日期：</TD>
								<TD><asp:label id="lblCheckDate" runat="server"></asp:label></TD>
								<TD class="form-item">导出日期：</TD>
								<TD><asp:label id="lblOutPutDate" runat="server"></asp:label></TD>								
							</TR>
						</TABLE>
					</TD>
				</TR>
				<tr height="100%">
					<td class="table" vAlign="top">
						<table cellSpacing="0" cellPadding="0" border="0" height="100%" width="100%">
							<tr>
								<td class="intopic" width="200">凭证分录</td>
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
												<asp:TemplateColumn HeaderText="序号" Visible="True">
													<HeaderStyle HorizontalAlign="Center" Wrap="False"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center" Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%# Container.ItemIndex + 1 %>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="Summary" HeaderText="摘要" FooterText="合计">
													<HeaderStyle Wrap="False"></HeaderStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn HeaderText="科目">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemTemplate>
														<span title="<%=SubjectSetName%>"><%# DataBinder.Eval(Container, "DataItem.SubjectName") %></span>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="借">
													<HeaderStyle HorizontalAlign="Right" Wrap="False"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right" Wrap="False"></ItemStyle>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
													<ItemTemplate>
														<asp:Label Runat="server" ID="lblDebitMoney" Text='<%# RmsPM.BLL.MathRule.GetDecimalShowString(DataBinder.Eval(Container, "DataItem.DebitMoney")) %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="贷">
													<HeaderStyle HorizontalAlign="Right" Wrap="False"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right" Wrap="False"></ItemStyle>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
													<ItemTemplate>
														<asp:Label Runat="server" ID="lblCrebitMoney" Text='<%# RmsPM.BLL.MathRule.GetDecimalShowString(DataBinder.Eval(Container, "DataItem.CrebitMoney")) %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="合同编号">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<a style="cursor:hand" onclick="javascript:GotoContract(this.code);" code='<%#  DataBinder.Eval(Container.DataItem, "ContractCode") %>'>
															<%# DataBinder.Eval(Container, "DataItem.ContractID") %>
														</a>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="SupplyName" HeaderText="供应商">
													<HeaderStyle Wrap="False"></HeaderStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn HeaderText="客户">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemTemplate>
														<a style="cursor:hand" onclick="javascript:GotoSupplier(this.code);" code='<%#  DataBinder.Eval(Container.DataItem, "CustCode") %>'>
															<%# DataBinder.Eval(Container, "DataItem.CustName") %>
														</a>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="部门">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<a style="cursor:hand" onclick="javascript:GotoUnit(this.code);" code='<%#  DataBinder.Eval(Container.DataItem, "UFUnitCode") %>'>
															<%# DataBinder.Eval(Container, "DataItem.UFUnitName") %>
														</a>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="人员">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<a style="cursor:hand" onclick="javascript:GotoUser(this.code);" code='<%#  DataBinder.Eval(Container.DataItem, "PaymentCheckPerson") %>'>
															<%# DataBinder.Eval(Container, "DataItem.PaymentCheckPersonName") %>
														</a>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="单位工程">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<a style="cursor:hand" onclick="javascript:GotoPBS(this.PBSType, this.code);" PBSType='<%#  DataBinder.Eval(Container.DataItem, "PBSType") %>' code='<%#  DataBinder.Eval(Container.DataItem, "PBSCode") %>'>
															<%# DataBinder.Eval(Container, "DataItem.PBSName") %>
														</a>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="项目">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%# DataBinder.Eval(Container, "DataItem.UFProjectName") %>
														<a style="cursor:hand" onclick="javascript:GotoProject(this.code);" code='<%#  DataBinder.Eval(Container.DataItem, "UFProjectCode") %>' style="display:none">
														</a>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="BillNo" HeaderText="票号">
													<ItemStyle Wrap="False"></ItemStyle>
													<HeaderStyle Wrap="False"></HeaderStyle>
												</asp:BoundColumn>
											</Columns>
											<PagerStyle NextPageText="下页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上页" HorizontalAlign="Right"
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

//修改
function DoModify()
{
	var voucherCode = Form1.txtVoucherCode.value;
	OpenFullWindow('../Finance/VoucherModify.aspx?VoucherCode=' + voucherCode,'修改凭证');
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

//审核
function DoCheck()
{
	var voucherCode = Form1.txtVoucherCode.value;
	OpenCustomWindow('../Finance/VoucherCheck.aspx?VoucherCode=' + voucherCode,"凭证审核",  550, 350);
}

//导出
function Download()
{
	OpenCustomWindow("../Finance/VoucherFileCheck.aspx?VoucherCode=" + Form1.txtVoucherCode.value, "凭证导出", 550, 350);
}

//查看部门
function GotoUnit(UnitCode)
{
	OpenCustomWindow("FinanceInterfaceAnalysisUnitModify.aspx?UnitCode=" + UnitCode + "&SubjectSetCode=" + Form1.txtSubjectSetCode.value, "部门财务编码修改", 300, 180);
//	OpenCustomWindow("../Systems/DepartmentInfo.aspx?UnitCode=" + UnitCode + "&act=view", "部门", 550, 350);
}

//查看人员
function GotoUser(UserCode)
{
	OpenCustomWindow("FinanceInterfaceAnalysisUserModify.aspx?UserCode=" + UserCode + "&SubjectSetCode=" + Form1.txtSubjectSetCode.value, "人员财务编码修改", 300, 180);
//	OpenCustomWindow("../Systems/DepartmentInfo.aspx?UnitCode=" + UnitCode + "&act=view", "部门", 550, 350);
}

//查看供应商
function GotoSupplier(SupplierCode)
{
    OpenCustomWindow("FinanceInterfaceAnalysisSupplierModify.aspx?SupplierCode=" + SupplierCode + "&ProjectCode=" + Form1.txtProjectCode.value + "&SubjectSetCode=" + Form1.txtSubjectSetCode.value, "修改厂商财务编码", 500, 300);
//	OpenFullWindow("../Supplier/SupplierInfo.aspx?SupplierCode=" + SupplierCode + "&act=view", "供应商");
}

//查看单位工程
function GotoPBS(PBSType, PBSCode)
{
	if (PBSType == "B")
	{
		OpenCustomWindow("FinanceInterfaceAnalysisBuildingModify.aspx?BuildingCode=" + PBSCode + "&SubjectSetCode=" + Form1.txtSubjectSetCode.value, "楼栋财务编码修改", 300, 180);
//		OpenCustomWindow("../PBS/PBSBuildInfo.aspx?BuildingCode=" + PBSCode + "&OpenModal=open&action=view", "BuildingInfo", 700, 540);
	}
	else
	{
		OpenCustomWindow("FinanceInterfaceAnalysisProjectModify.aspx?ProjectCode=" + Form1.txtProjectCode.value + "&SubjectSetCode=" + Form1.txtSubjectSetCode.value, "项目财务编码修改", 300, 180);
//		OpenCustomWindow("../Project/ProjectInfo.aspx?ProjectCode=" + Form1.txtProjectCode.value, "ProjectInfo", 760, 540);
	}
}

//查看项目
function GotoProject(ProjectCode)
{
	OpenLargeWindow("../Project/ProjectInfo.aspx?ProjectCode=" + ProjectCode + "&act=view", "项目信息");
}

//查看合同
function GotoContract(ContractCode)
{
	OpenFullWindow("../Contract/ContractInfo.aspx?ProjectCode=" + document.all.txtProjectCode.value + "&ContractCode=" + ContractCode,'合同信息');
}

//-->
		</SCRIPT>
	</body>
</HTML>
