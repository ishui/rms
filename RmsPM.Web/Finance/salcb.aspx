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
								<td class="topic" background="../images/topic_bg.gif"><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle">财务管理 
									- 成本结转</td>
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
						<input class="button" id="btnVoucherCb" onclick="MadeVoucherCB();" type="button" value="成本凭证"
							name="btnVoucherCb">
						<input class="button" id="btnVoucherJt" onclick="MadeVoucherJT();" type="button" value="计提税金凭证"
							name="btnVoucherJt">
						<input class="button" id="btnModifyCost" onclick="ModifyCost();" type="button" value="成本录入"
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
											<TD>幢号：</TD>
											<TD><select id="sltSearchBuildingName" name="sltSearchBuildingName" runat="server" onchange="btnSearch.click();">
													<option value="" selected>-------请选择-------</option>
												</select>
											</TD>
											<TD align="right">成本凭证号：</TD>
											<TD width="100"><a href="#" onclick="GotoVoucher(this.val)" id="aVoucherCodeCB" runat="server"></a></TD>
											<TD align="right">计提税金凭证号：</TD>
											<TD width="100"><a href="#" onclick="GotoVoucher(this.val)" id="aVoucherCodeJT" runat="server"></a></TD>
											<TD vAlign="middle" align="right" style="DISPLAY:none"><input class="submit" id="btnSearch" type="button" value="搜 索" name="btnSearch" runat="server"
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
												<asp:TemplateColumn HeaderText="序号">
													<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" HeaderText="合同编号" FooterText="合计">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<a href="#" onclick="ViewSalContract(this.Code);return false;" Code='<%#  DataBinder.Eval(Container.DataItem, "ContractCode") %>'><%#  DataBinder.Eval(Container.DataItem, "ContractID") %></a>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="ClientName" HeaderText="客户姓名"></asp:BoundColumn>
												<asp:BoundColumn Visible="False" DataField="BofangCode" HeaderText="拨房单号"></asp:BoundColumn>
												<asp:BoundColumn DataField="BuildingName" HeaderText="幢号" Visible="False"></asp:BoundColumn>
												<asp:BoundColumn DataField="ChamberName" HeaderText="门牌号"></asp:BoundColumn>
												<asp:BoundColumn DataField="Room" HeaderText="室号">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="BuildDim" HeaderText="建筑面积<br>(平米)" DataFormatString="{0:n}">
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="UnitPrice" HeaderText="合同单价<br>(元)" DataFormatString="{0:n}">
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="TotalPayMoney" HeaderText="经营收入<br>(元)" DataFormatString="{0:n}">
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="CostPrice" HeaderText="成本单价<br>(元)" DataFormatString="{0:n}">
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="Cost" HeaderText="经营成本<br>(元)" DataFormatString="{0:n}">
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
												</asp:BoundColumn>
											</Columns>
											<PagerStyle NextPageText="下页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上页" HorizontalAlign="Right"
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
	//成本凭证
	function MadeVoucherCB()
	{
		if (Form1.txtTotalCost.value == "")
		{
			alert("请先查询");
			return false;
		}
		
		var cost = parseFloat(Form1.txtTotalCost.value);
		
		if (cost == 0)
		{
			alert("总成本为0，不能制作凭证");
			return false;
		}
		
		MakeVoucher("", "SalCBAdd", Form1.txtBuildingName.value, Form1.txtTotalCost.value);
	}
	
	//计提税金凭证
	function MadeVoucherJT()
	{
		if (Form1.txtTotalPayMoney.value == "")
		{
			alert("请先查询");
			return false;
		}
		
		var money = parseFloat(Form1.txtTotalPayMoney.value);
		
		if (money == 0)
		{
			alert("总收入为0，不能制作凭证");
			return false;
		}
		
		MakeVoucher("", "SalJTAdd", Form1.txtBuildingName.value, Form1.txtTotalPayMoney.value);
	}
	
	function MakeVoucher(VoucherCode, act, BuildingName, money)
	{
		OpenCustomWindow("../Finance/VoucherModify.aspx?VoucherCode=" + VoucherCode + "&ProjectCode=" + Form1.txtProjectCode.value + "&Act=" + act + "&RelaCode=" + escape(BuildingName) + "&Param=" + escape(money) + "&RefreshScript=DoRefresh();","凭证修改", 780, 580);
	}

	function ViewSalContract(ContractCode)
	{
		OpenCustomWindow("../Sal/SalContractView.aspx?Action=view&FromUrl=" + escape(window.location.href) + "&ContractCode=" + ContractCode, "合同详细", 650, 560);
	}
	
	function ModifyCost()
	{
		OpenCustomWindow("../Sal/SalCostModify.aspx?ProjectCode=" + Form1.txtProjectCode.value + "&RefreshScript=DoRefresh();", "成本录入", 400, 500);
	}
	
	//查看凭证
	function GotoVoucher(VoucherCode)
	{
		OpenCustomWindow("../Finance/VoucherInfo.aspx?VoucherCode=" + VoucherCode + "&Open=1", "凭证信息", 760, 540);
	}
	
	//刷新
	function DoRefresh()
	{
		Form1.btnSearch.click();
	}		

//搜索条件按回车
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
