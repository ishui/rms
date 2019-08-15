<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Page language="c#" Inherits="RmsPM.Web.Construct.BatchEditPlan" CodeFile="BatchEditPlan.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>批量修改年度计划</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<SCRIPT language="javascript" src="../images/convert.js"></SCRIPT>
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0" bgcolor="white">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25"><span runat="server" id="spanYear"></span>年度计划</td>
				</tr>
				<tr>
					<td>
						<table cellpadding="0" cellspacing="0" class="form" width="100%">
							<tr>
								<td class="form-item" nowrap>项目计划总投资：</td>
								<td nowrap><input type="text" class="input-nember" id="txtProjectPInvest" name="txtProjectPInvest"
										runat="server">万元</td>
								<td nowrap width="100%"><input type="button" id="btnApport" runat="server" class="button-small" value="分摊到单位工程"
										onclick="document.all.divHintSave.style.display='';" onserverclick="btnApport_ServerClick"></td>
							</tr>
						</table>
						<br>
					</td>
				</tr>
				<tr height="100%">
					<td vAlign="top" class="topic">
						<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
							<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
								<tr vAlign="top">
									<td><asp:datagrid id="dgList" runat="server" Width="100%" CssClass="list" CellPadding="0" AllowSorting="True"
											AutoGenerateColumns="False" PageSize="15" ShowFooter="True" DataKeyField="PBSUnitCode">
											<ItemStyle CssClass=""></ItemStyle>
											<HeaderStyle CssClass="list-title"></HeaderStyle>
											<FooterStyle CssClass="list-title"></FooterStyle>
											<Columns>
												<asp:TemplateColumn HeaderText="单位工程" FooterText="合计">
													<ItemTemplate>
														<%# DataBinder.Eval(Container, "DataItem.PBSUnitName") %>
														<input type="hidden" name="txtPBSUnitName" id="txtPBSUnitName" runat="server" value='<%# DataBinder.Eval(Container, "DataItem.PBSUnitName") %>'>
														<input type="hidden" name="txtPBSUnitCode" id="txtPBSUnitCode" runat="server" value='<%# DataBinder.Eval(Container, "DataItem.PBSUnitCode") %>'>
														<input type="hidden" name="txtAnnualPlanCode" id="txtAnnualPlanCode" runat="server" value='<%# DataBinder.Eval(Container, "DataItem.AnnualPlanCode") %>'>
													</ItemTemplate>
													<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
													<FooterStyle HorizontalAlign="Left"></FooterStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="建筑面积(平米)">
													<ItemTemplate>
														<%# RmsPM.BLL.MathRule.GetDecimalShowString(DataBinder.Eval(Container, "DataItem.TotalBuildArea")) %>
														<input type="hidden" size="10" name="txtTotalBuildArea" id="txtTotalBuildArea" runat="server" value='<%# DataBinder.Eval(Container, "DataItem.TotalBuildArea") %>'>
													</ItemTemplate>
													<FooterTemplate>
														<asp:Label runat="server" ID="lblSumTotalBuildArea"></asp:Label>
													</FooterTemplate>
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="计划总投资(万元)">
													<ItemTemplate>
														<input type="text" class="input-nember" size="10" id="txtPTotalInvest" name="txtPTotalInvest" onfocus="InputFocus(this);" onblur="InputBlur(this);" runat="server" value='<%# RmsPM.BLL.MathRule.GetDecimalShowString(DataBinder.Eval(Container, "DataItem.PTotalInvest")) %>'>
													</ItemTemplate>
													<FooterTemplate>
														<asp:Label runat="server" ID="lblSumPTotalInvest"></asp:Label>
													</FooterTemplate>
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="计划形象进度">
													<ItemTemplate>
														<select class="select" runat="server" id="sltVisualProgress" name="sltVisualProgress" DataSource="<%# GetVisualProgressDataSource() %>" DataTextField="VisualProgress" DataValueField="SystemID">
														</select>
														<input type="hidden" name="txtVisualProgress" id="txtVisualProgress" runat="server" value='<%# DataBinder.Eval(Container, "DataItem.VisualProgress") %>'>
													</ItemTemplate>
													<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
													<FooterStyle HorizontalAlign="Left"></FooterStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="计划施工层数">
													<ItemTemplate>
														<input type="text" class="input-nember" size="10" id="txtCurrentFloor" name="txtCurrentFloor" runat="server" value='<%# RmsPM.BLL.MathRule.GetIntShowString(DataBinder.Eval(Container, "DataItem.CurrentFloor")) %>'>
													</ItemTemplate>
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="上年末完成投资(万元)">
													<ItemTemplate>
														<input type="text" class="input-nember" size="10" id="txtInvestBefore" name="txtInvestBefore" onfocus="InputFocus(this);" onblur="InputBlur(this);" runat="server" value='<%# RmsPM.BLL.MathRule.GetDecimalShowString(DataBinder.Eval(Container, "DataItem.InvestBefore")) %>'>
													</ItemTemplate>
													<FooterTemplate>
														<asp:Label runat="server" ID="lblSumInvestBefore"></asp:Label>
													</FooterTemplate>
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="上年结转面积(平米)">
													<ItemTemplate>
														<input type="text" class="input-nember" size="10" id="txtLCFArea" name="txtLCFArea" onfocus="InputFocus(this);" onblur="InputBlur(this);" runat="server" value='<%# RmsPM.BLL.MathRule.GetDecimalShowString(DataBinder.Eval(Container, "DataItem.LCFArea")) %>'>
													</ItemTemplate>
													<FooterTemplate>
														<asp:Label runat="server" ID="lblSumLCFArea"></asp:Label>
													</FooterTemplate>
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
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
					<td>
						<table cellSpacing="10" width="100%">
							<tr>
								<td align="center"><input class="submit" id="btnSave" type="button" value="确 定" onclick="document.all.divHintSave.style.display='';"
										name="btnSave" runat="server" onserverclick="btnSave_ServerClick"> <input class="submit" id="btnCancel" onclick="javascript:self.close()" type="button" value="取 消"
										name="btnCancel">
								</td>
							</tr>
						</table>
					</td>
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
			<input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server"> <input id="txtAct" type="hidden" name="txtAct" runat="server">
			<input id="txtIYear" type="hidden" name="txtIYear" runat="server"> <input id="txtDetailCount" name="txDetailCount" runat="server">
			<input id="txtSumTotalBuildArea" name="txtSumTotalBuildArea" runat="server"><input id="txtSumPTotalInvest" name="txtSumPTotalInvest" runat="server">
			<input id="txtSumInvestBefore" name="txtSumInvestBefore" runat="server"><input id="txtSumLCFArea" name="txtSumLCFArea" runat="server">
		</form>
		<script language="javascript">
var oldvalue;

function InputFocus(obj)
{
	oldvalue = obj.value;
}

function InputBlur(obj)
{
	if (obj.value != oldvalue)
	  CalcSum();
}

//计算合计
function CalcSum()
{
	var c = parseInt(Form1.txtDetailCount.value);
	var tempVal = 0;
	var sum = 0;
	var sum2 = 0;
	var sum3 = 0;
	
	for(i=0;i<c;i++)
	{
		tempVal = ConvertFloat(document.all("dgList__ctl" + (i + 2) + "_txtPTotalInvest").value);
		sum = sum + tempVal;

		tempVal = ConvertFloat(document.all("dgList__ctl" + (i + 2) + "_txtInvestBefore").value);
		sum2 = sum2 + tempVal;

		tempVal = ConvertFloat(document.all("dgList__ctl" + (i + 2) + "_txtLCFArea").value);
		sum3 = sum3 + tempVal;
	}

	//四舍五入到2位小数
	sum = Math.round(sum * 100) / 100;
	sum2 = Math.round(sum2 * 100) / 100;
	sum3 = Math.round(sum3 * 100) / 100;
	
	//格式化
	sum = FormatNumber(sum, 2);
	sum2 = FormatNumber(sum2, 2);
	sum3 = FormatNumber(sum3, 2);
	
	document.all.txtSumPTotalInvest.value = sum;
	document.all.txtSumInvestBefore.value = sum2;
	document.all.txtSumLCFArea.value = sum3;

	document.all("dgList__ctl" + (c + 2) + "_lblSumPTotalInvest").innerText = sum;
	document.all("dgList__ctl" + (c + 2) + "_lblSumInvestBefore").innerText = sum2;
	document.all("dgList__ctl" + (c + 2) + "_lblSumLCFArea").innerText = sum3;
	
	Form1.txtProjectPInvest.value = sum;
}
		</script>
	</body>
</HTML>
