<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Page language="c#" Inherits="RmsPM.Web.Finance.SalCB" CodeFile="SalCB.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>SalCB</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
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
									- �ɱ���ת</td>
								<td width="9"><IMG height="25" src="../images/topic_corr.gif" width="9"></td>
							</tr>
						</table>
					</td>
				</tr>
				<TR>
					<td class="tools-area" vAlign="top">
						<IMG src="../images/btn_li.gif" align="absMiddle">
						<input class="button" id="btnExcel" type="button" value="Excel" name="btnExcel" runat="server"
							onclick="document.all.divHintLoad.style.display = 'block';" onserverclick="btnExcel_ServerClick">
						<input class="button" id="btnVoucherCb" onclick="MadeVoucherCB();" type="button" value="�ɱ�ƾ֤"
							name="btnVoucherCb">
						<input class="button" id="btnVoucherJt" onclick="MadeVoucherJT();" type="button" value="����˰��ƾ֤"
							name="btnVoucherJt">
						<input class="button" id="btnModifyCost" onclick="ModifyCost();" type="button" value="�ɱ�¼��"
							name="btnModifyCost">
					</TD>
				</TR>
				<tr>
					<td class="table" vAlign="top">
						<TABLE class="search-area" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td>
									<table>
										<TR>
											<TD>���ţ�</TD>
											<TD><select id="sltSearchBuildingName" name="sltSearchBuildingName" runat="server" onchange="btnSearch.click();">
													<option value="" selected>-------��ѡ��-------</option>
												</select>
											</TD>
											<TD align="right">�ɱ�ƾ֤�ţ�</TD>
											<TD width="100"><a href="#" onclick="GotoVoucher(this.val)" id="aVoucherCodeCB" runat="server"></a></TD>
											<TD align="right">����˰��ƾ֤�ţ�</TD>
											<TD width="100"><a href="#" onclick="GotoVoucher(this.val)" id="aVoucherCodeJT" runat="server"></a></TD>
											<TD vAlign="middle" align="right" style="DISPLAY:none"><input class="submit" id="btnSearch" type="button" value="�� ��" name="btnSearch" runat="server"
													onclick="document.all.divHintLoad.style.display = '';" onkeydown="SearchKeyDown();" onserverclick="btnSearch_ServerClick"></TD>
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
									<td><asp:datagrid id="dgList" runat="server" ShowFooter="True" AllowSorting="True" AutoGenerateColumns="False"
											PageSize="12" GridLines="Horizontal" CellPadding="0" CssClass="list" Width="100%">
											<AlternatingItemStyle CssClass=""></AlternatingItemStyle>
											<ItemStyle CssClass=""></ItemStyle>
											<HeaderStyle CssClass="list-title"></HeaderStyle>
											<FooterStyle CssClass="list-title"></FooterStyle>
											<Columns>
												<asp:TemplateColumn HeaderText="���">
													<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" HeaderText="��ͬ���" FooterText="�ϼ�">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<a href="#" onclick="ViewSalContract(this.Code);return false;" Code='<%#  DataBinder.Eval(Container.DataItem, "ContractCode") %>'><%#  DataBinder.Eval(Container.DataItem, "ContractID") %></a>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="ClientName" HeaderText="�ͻ�����"></asp:BoundColumn>
												<asp:BoundColumn Visible="False" DataField="BofangCode" HeaderText="��������"></asp:BoundColumn>
												<asp:BoundColumn DataField="BuildingName" HeaderText="����" Visible="False"></asp:BoundColumn>
												<asp:BoundColumn DataField="ChamberName" HeaderText="���ƺ�"></asp:BoundColumn>
												<asp:BoundColumn DataField="Room" HeaderText="�Һ�">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="BuildDim" HeaderText="�������<br>(ƽ��)" DataFormatString="{0:n}">
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="UnitPrice" HeaderText="��ͬ����<br>(Ԫ)" DataFormatString="{0:n}">
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="TotalPayMoney" HeaderText="��Ӫ����<br>(Ԫ)" DataFormatString="{0:n}">
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="CostPrice" HeaderText="�ɱ�����<br>(Ԫ)" DataFormatString="{0:n}">
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="Cost" HeaderText="��Ӫ�ɱ�<br>(Ԫ)" DataFormatString="{0:n}">
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
												</asp:BoundColumn>
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
			<input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server"><input id="txtSelect" type="hidden" name="txtSelect" runat="server">
			<input id="txtParam" type="hidden" name="txtParam" runat="server"> <input id="txtVoucherCode" type="hidden" name="txtVoucherCode" runat="server">
			<input id="txtTotalCost" type="hidden" name="txtTotalCost" runat="server"><input id="txtTotalPayMoney" type="hidden" name="txtTotalPayMoney" runat="server"><input id="txtBuildingName" type="hidden" name="txtBuildingName" runat="server">
			<div style="DISPLAY: none"><input id="btnBatchHidden" type="button" name="btnBatchHidden" runat="server">
			</div>
		</form>
		<script language="javascript">
<!--
	//�ɱ�ƾ֤
	function MadeVoucherCB()
	{
		if (Form1.txtTotalCost.value == "")
		{
			alert("���Ȳ�ѯ");
			return false;
		}
		
		var cost = parseFloat(Form1.txtTotalCost.value);
		
		if (cost == 0)
		{
			alert("�ܳɱ�Ϊ0����������ƾ֤");
			return false;
		}
		
		MakeVoucher("", "SalCBAdd", Form1.txtBuildingName.value, Form1.txtTotalCost.value);
	}
	
	//����˰��ƾ֤
	function MadeVoucherJT()
	{
		if (Form1.txtTotalPayMoney.value == "")
		{
			alert("���Ȳ�ѯ");
			return false;
		}
		
		var money = parseFloat(Form1.txtTotalPayMoney.value);
		
		if (money == 0)
		{
			alert("������Ϊ0����������ƾ֤");
			return false;
		}
		
		MakeVoucher("", "SalJTAdd", Form1.txtBuildingName.value, Form1.txtTotalPayMoney.value);
	}
	
	function MakeVoucher(VoucherCode, act, BuildingName, money)
	{
		OpenCustomWindow("../Finance/VoucherModify.aspx?VoucherCode=" + VoucherCode + "&ProjectCode=" + Form1.txtProjectCode.value + "&Act=" + act + "&RelaCode=" + escape(BuildingName) + "&Param=" + escape(money) + "&RefreshScript=DoRefresh();","ƾ֤�޸�", 780, 580);
	}

	function ViewSalContract(ContractCode)
	{
		OpenCustomWindow("../Sal/SalContractView.aspx?Action=view&FromUrl=" + escape(window.location.href) + "&ContractCode=" + ContractCode, "��ͬ��ϸ", 650, 560);
	}
	
	function ModifyCost()
	{
		OpenCustomWindow("../Sal/SalCostModify.aspx?ProjectCode=" + Form1.txtProjectCode.value + "&RefreshScript=DoRefresh();", "�ɱ�¼��", 400, 500);
	}
	
	//�鿴ƾ֤
	function GotoVoucher(VoucherCode)
	{
		OpenCustomWindow("../Finance/VoucherInfo.aspx?VoucherCode=" + VoucherCode + "&Open=1", "ƾ֤��Ϣ", 760, 540);
	}
	
	//ˢ��
	function DoRefresh()
	{
		Form1.btnSearch.click();
	}		

//�����������س�
function SearchKeyDown()
{
	if(event.keyCode==13)
	{
		event.keyCode = 9;
		Form1.btnSearch.click();
	}
}

//-->
		</script>
	</body>
</HTML>
