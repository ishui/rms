<%@ Page language="c#" Inherits="RmsPM.Web.Finance.CostCheckList" CodeFile="CostCheckList.aspx.cs" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>�ɱ�����</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<SCRIPT language="javascript" src="../pbs/map.js"></SCRIPT>
		<SCRIPT language="javascript" src="../images/checkbox.js"></SCRIPT>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<div style="DISPLAY: none"><input id="btnMakeVoucherHidden" type="button" name="btnMakeVoucherHidden" runat="server">
			</div>
			<TABLE height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td bgColor="#e4eff6" height="6"></td>
				</tr>
				<tr>
					<td height="25">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="topic" background="../images/topic_bg.gif"><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle">������� 
									- �ɱ�����</td>
								<td width="9"><IMG height="25" src="../images/topic_corr.gif" width="9"></td>
							</tr>
						</table>
					</td>
				</tr>
				<TR>
					<td class="tools-area" vAlign="top">
						<IMG src="../images/btn_li.gif" align="absMiddle"> <input class="button" id="btnApportionAccount" type="button" value="��̯����" name="btnApportionAccount"
							onclick='doPayoutApportionAccount(); return false;' runat="server"> <input class="button" id="btnApportionReport" type="button" value="��Ŀ�ɱ���̯��" name="btnApportionReport"
							onclick='doApportionReport(); return false;' runat="server"> <input class="button" id="btnApportionExcel" type="button" value="��̯��Excel" name="btnApportionExcel"
							onclick='doApportionExcel();' runat="server"> <input class="button" id="btnVoucherCb" onclick="MadeVoucherCB();" type="button" value="�ɱ�ƾ֤"
							name="btnVoucherCb"> <input class="button" id="btnVoucherJt" onclick="MadeVoucherJT();" type="button" value="����˰��ƾ֤"
							name="btnVoucherJt" style="DISPLAY:none">
					</td>
				</TR>
				<tr style="DISPLAY:none">
					<td class="table" vAlign="top">
						<TABLE class="search-area" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td>
									<table>
										<TR>
										</TR>
									</table>
								</td>
							</tr>
						</TABLE>
					</td>
				</tr>
				<tr height="100%">
					<td class="table" vAlign="top">
						<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
							<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
								<tr vAlign="top">
									<td><asp:datagrid id="dgList" runat="server" ShowFooter="True" AllowSorting="True" AutoGenerateColumns="False"
											PageSize="12" GridLines="Horizontal" CellPadding="0" CssClass="list" Width="100%">
											<AlternatingItemStyle CssClass=""></AlternatingItemStyle>
											<ItemStyle CssClass=""></ItemStyle>
											<HeaderStyle CssClass="list-title"></HeaderStyle>
											<FooterStyle CssClass="list-title"></FooterStyle>
											<Columns>
												<asp:TemplateColumn HeaderText="&lt;input type='checkbox' name='chkAll' onclick='ChkSelectAll(document.all.chkSelect, document.all.chkAll.checked);' title='ȫѡ��ȫ��ѡ'&gt;">
													<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<input type="checkbox" name="chkSelect" class="list-checkbox" onclick="ChkSelectRow('<%# Container.ItemIndex + 1%>', this, document.all.dgList, 'list-2', '');" value='<%#DataBinder.Eval(Container, "DataItem.BuildingCode")%>'>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:HyperLinkColumn DataNavigateUrlField="BuildingCode" DataNavigateUrlFormatString="javascript:OpenBuildingInfo('{0}');"
													DataTextField="BuildingName" HeaderText="¥������" FooterText="�ϼ�">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
												</asp:HyperLinkColumn>
												<asp:TemplateColumn HeaderText="�ɱ�(Ԫ)">
													<HeaderStyle HorizontalAlign="Right" Wrap="False"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right" Wrap="False"></ItemStyle>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
													<ItemTemplate>
														<%# RmsPM.BLL.MathRule.GetDecimalShowString(DataBinder.Eval(Container.DataItem, "TotalCost"))%>
													</ItemTemplate>
													<FooterTemplate>
														<asp:Label runat="server" ID="lblSumCost"></asp:Label>
													</FooterTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="�ɱ�����(Ԫ)">
													<HeaderStyle HorizontalAlign="Right" Wrap="False"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right" Wrap="False"></ItemStyle>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
													<ItemTemplate>
														<%# RmsPM.BLL.MathRule.GetDecimalShowString(DataBinder.Eval(Container.DataItem, "CostPrice"))%>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="�������(ƽ��)">
													<HeaderStyle HorizontalAlign="Right" Wrap="False"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right" Wrap="False"></ItemStyle>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
													<ItemTemplate>
														<%# RmsPM.BLL.MathRule.GetDecimalShowString(DataBinder.Eval(Container.DataItem, "Area"))%>
													</ItemTemplate>
													<FooterTemplate>
														<asp:Label runat="server" ID="lblSumArea"></asp:Label>
													</FooterTemplate>
												</asp:TemplateColumn>
												<asp:HyperLinkColumn DataNavigateUrlField="PBSUnitCode" DataNavigateUrlFormatString="javascript:GotoPBSUnitInfo('{0}');"
													DataTextField="PBSUnitName" HeaderText="��λ����">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
												</asp:HyperLinkColumn>
												<asp:TemplateColumn HeaderText="ƾ֤��">
													<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<a href="#" onclick="GotoVoucher(this.val);" val='<%# DataBinder.Eval(Container, "DataItem.VoucherCode") %>'>
															<%# DataBinder.Eval(Container, "DataItem.VoucherCode") %>
														</a>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle NextPageText="��ҳ&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;��ҳ" HorizontalAlign="Right"
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
			<input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server"> <input id="txtTotalCost" type="hidden" name="txtTotalCost" runat="server"><input id="txtTotalPayMoney" type="hidden" name="txtTotalPayMoney" runat="server"><input id="txtBuildingName" type="hidden" name="txtBuildingName" runat="server">
		</form>
		<script language="javascript">
<!--


	//��Ŀ�ɱ���̯��
	function doApportionReport()
	{
		OpenFullWindow( '../Cost/CostCheckReport.aspx?ProjectCode=<%=Request["ProjectCode"]%>','�ɱ���̯����' );
	}

	//��Ŀ�ɱ���̯��Excel
	function doApportionExcel()
	{
		document.all.divHintSave.style.display = "";
		return true;
	}

	//��̯����
	function doPayoutApportionAccount()
	{
		OpenCustomWindow( 'PayoutApportionAccount.aspx?ProjectCode=<%=Request["ProjectCode"]%>','�ɱ���̯����', 300, 200 );
	}


	//�鿴��λ����
	function GotoPBSUnitInfo(PBSUnitCode)
	{
		OpenCustomWindow("../PBS/PBSUnitInfo.aspx?action=view&FromUrl=" + escape(window.location) + "&PBSUnitCode=" + PBSUnitCode + "&ProjectCode=" + Form1.txtProjectCode.value, "��λ����", 700, 500);
	}

	//�鿴ƾ֤
	function GotoVoucher(VoucherCode)
	{
		OpenCustomWindow("../Finance/VoucherInfo.aspx?VoucherCode=" + VoucherCode + "&Open=1", "ƾ֤��Ϣ", 760, 540);
	}
	
	//�ɱ�ƾ֤
	function MadeVoucherCB()
	{
		var s = ChkGetSelected(document.all.chkSelect);
			
		if (s == "")
		{
			alert('��ѡ��һ���������¼');
			return false;
		}

		MakeVoucher("", "BuildingCBAdd", s);
	}
	
	//����˰��ƾ֤
	function MadeVoucherJT()
	{
		var s = ChkGetSelected(document.all.chkSelect);
			
		if (s == "")
		{
			alert('��ѡ��һ���������¼');
			return false;
		}
		
		MakeVoucher("", "BuildingJTAdd", s);
	}
	
	function MakeVoucher(VoucherCode, act, BuildingCodes)
	{
		OpenCustomWindow("../Finance/VoucherModify.aspx?VoucherCode=" + VoucherCode + "&ProjectCode=" + Form1.txtProjectCode.value + "&Act=" + act + "&RelaCode=" + escape(BuildingCodes) + "&RefreshScript=;","ƾ֤�޸�", 780, 580);
	}

//-->
		</script>
	</body>
</HTML>
