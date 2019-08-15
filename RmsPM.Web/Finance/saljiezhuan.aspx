<%@ Page language="c#" Inherits="RmsPM.Web.Finance.SalJieZhuan" CodeFile="SalJieZhuan.aspx.cs" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>SalJieZhuan</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<SCRIPT language="javascript" src="../images/checkbox.js"></SCRIPT>
		<script language="javascript">
	function MakeVoucher(VoucherCode, param)
	{
//		var param = Form1.txtParam.value;
//		var RelaCode = Form1.txtSelect.value;
		OpenCustomWindow("../Finance/VoucherModify.aspx?VoucherCode=" + VoucherCode + "&ProjectCode=" + Form1.txtProjectCode.value + "&Act=SalJZAdd&Param=" + param + "&RelaCodeSession=RelaCode" + "&RefreshScript=DoRefresh();","凭证修改", 780, 580);
	}
	
		</script>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<div style="DISPLAY:none">
				<input id="btnMakeVoucherHidden" type="button" name="btnMakeVoucherHidden" onclick="document.all.divHintSave.style.display='';" runat="server" onserverclick="btnMakeVoucherHidden_ServerClick">
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
									- 收入结转</td>
								<td width="9"><IMG height="25" src="../images/topic_corr.gif" width="9"></td>
							</tr>
						</table>
					</td>
				</tr>
				<TR>
					<td class="tools-area" vAlign="top">
						<IMG src="../images/btn_li.gif" align="absMiddle">
						<input class="button" id="btnBatch" onclick="Batch(true);" type="button" value="生成凭证" name="btnBatch">
						<input class="button" id="btnBatchSelectVoucher" onclick="Batch(false);" type="button"
							value="加入凭证" name="btnBatchSelectVoucher">
					</TD>
				</TR>
				<tr>
					<td class="table" vAlign="top">
						<TABLE class="search-area" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td>
									<table>
										<TR>
											<TD>合同编号：</TD>
											<TD><input class="input" id="txtSearchContractID" type="text" name="txtSearchContractID" runat="server"></TD>
											<TD>状态：</TD>
											<TD><select class="select" id="sltSearchStatus" name="sltSearchStatus" runat="server">
													<option value=" " selected>全部</option>
													<option value="0">未处理</option>
													<option value="2">已入凭证</option>
												</select>
											</TD>
											<TD>幢号：</TD>
											<TD><select class="select" id="sltSearchBuildingName" name="sltSearchBuildingName" runat="server">
													<option value="" selected>-------请选择-------</option>
												</select>
											</TD>
											<TD><input class="submit" id="btnSearch" type="button" value="搜 索" name="btnSearch" runat="server" onclick="document.all.divHintLoad.style.display = '';" onkeydown="SearchKeyDown();" onserverclick="btnSearch_ServerClick"></TD>
										</TR>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr height="100%">
					<td class="table" valign="top">
						<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
							<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
								<tr vAlign="top">
									<td>
										<asp:datagrid id="dgList" runat="server" ShowFooter="True" AllowSorting="True" AutoGenerateColumns="False"
											PageSize="12" GridLines="Horizontal" CellPadding="0" CssClass="list" Width="100%">
											<AlternatingItemStyle CssClass=""></AlternatingItemStyle>
											<ItemStyle CssClass=""></ItemStyle>
											<HeaderStyle CssClass="list-title"></HeaderStyle>
											<FooterStyle CssClass="list-title"></FooterStyle>
											<Columns>
												<asp:TemplateColumn HeaderText="&lt;input type='checkbox' name='chkAll' onclick='ChkSelectAll(document.all.chkSelect, document.all.chkAll.checked);' title='全选或全不选'&gt;">
													<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<input type="checkbox" name="chkSelect" class="list-checkbox" onclick="ChkSelectRow('<%# Container.ItemIndex + 1%>', this, document.all.dgList, 'list-2', '');" value='<%#DataBinder.Eval(Container, "DataItem.ContractCode")%>'>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="序号" HeaderStyle-HorizontalAlign="Center">
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<%# Container.ItemIndex + 1%>
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
												<asp:BoundColumn DataField="BofangCode" HeaderText="拨房单号" Visible="False"></asp:BoundColumn>
												<asp:BoundColumn DataField="BuildingName" HeaderText="幢号"></asp:BoundColumn>
												<asp:BoundColumn DataField="ChamberName" HeaderText="门牌号"></asp:BoundColumn>
												<asp:BoundColumn DataField="Room" HeaderText="室号"></asp:BoundColumn>
												<asp:BoundColumn DataField="BuildDim" HeaderText="建筑面积<br>(平米)" DataFormatString="{0:n}">
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="TotalPrice" HeaderText="合同总价<br>(元)" DataFormatString="{0:n}">
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="FactPrice" HeaderText="补差后总价<br>(元)" DataFormatString="{0:n}">
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="TotalPayMoney" HeaderText="经营收入<br>(元)" DataFormatString="{0:n}">
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn HeaderText="凭证号">
													<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<a href="#" onclick="GotoVoucher(this.val);" val='<%# RmsPM.BLL.SalRule.GetSalContractVoucherCode(DataBinder.Eval(Container, "DataItem.ContractCode").ToString()) %>'><%# RmsPM.BLL.PaymentRule.GetVoucherName(RmsPM.BLL.SalRule.GetSalContractVoucherCode(DataBinder.Eval(Container, "DataItem.ContractCode").ToString()))%></a>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="状态">
													<HeaderStyle HorizontalAlign="Left" Wrap="False"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left" Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%# RmsPM.BLL.SalRule.GetSalContractVoucherCode(DataBinder.Eval(Container, "DataItem.ContractCode").ToString())==""?"未处理":"已入凭证"%>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle NextPageText="下页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上页" HorizontalAlign="Right"
												CssClass="ListHeadTr"></PagerStyle>
										</asp:datagrid>
									</td>
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
			<div style="DISPLAY: none"><input id="btnBatchHidden" type="button" name="btnBatchHidden" runat="server">
			</div>
		</form>
		<script language="javascript">
<!--
	function SelectAll(){
		var checked;

		chk = document.all.chkAll;
		checked = chk.checked;
		
		var obj=document.all("chkSelect");

		if (obj == null)
			return false;
			
		
		if(obj[0]){
			for(var i=0;i<obj.length;i++){
				obj[i].checked=checked;
			}
		}else{
			if(obj){
				obj.checked=checked;
			}
		}
	}
	
	function Batch(isNew)
	{
		var checked = false;
		var s = "";
		var obj=document.all("chkSelect");
		
		if (obj == null)
			return false;
			
		if(obj[0]){
			for(var i=0;i<obj.length;i++){
				if (obj[i].checked)
				{
					if (s != "")
					{
						s = s + ",";
					}
					s = s + obj[i].value;
					
//					checked = true;
//					break;
				}
			}
		}else{
			if(obj){
				if (obj.checked)
				{
					s = obj.value;
//					checked = true;
				}
			}
		}

//		if (!checked)
		if (s == "")
		{
			alert('请选择一条或多条记录');
			return false;
		}
		
		Form1.txtSelect.value = s;

		if (isNew)
		{
			Form1.txtVoucherCode.value = "";
			Form1.btnMakeVoucherHidden.click();
		}
		else
		{
			OpenMiddleWindow("../Finance/SelectVoucher.aspx?Act=SalJZAdd&ProjectCode=" + Form1.txtProjectCode.value,"选择凭证");
		}

//		document.all.btnBatchHidden.click();
//		return true;
	}
	
	function SelectVoucherReturn(VoucherCode)
	{
		Form1.txtVoucherCode.value = VoucherCode;
		Form1.btnMakeVoucherHidden.click();
//		MakeVoucher(VoucherCode);
	}
	
	function ViewSalContract(ContractCode)
	{
		OpenCustomWindow("../Sal/SalContractView.aspx?Action=view&FromUrl=" + escape(window.location.href) + "&ContractCode=" + ContractCode, "合同详细", 650, 560);
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
