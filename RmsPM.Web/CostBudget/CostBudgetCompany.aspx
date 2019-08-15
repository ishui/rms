<%@ Page language="c#" Inherits="RmsPM.Web.CostBudget.CostBudgetCompany" CodeFile="CostBudgetCompany.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>CostBudgetCompany</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<LINK href="../Images/infra.css" type="text/css" rel="stylesheet">
		<LINK href="../Images/locked-column.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<script language="javascript" src="../Images/locked-column.js"></script>
		<style>.list-t1 { FONT-WEIGHT: bold; BACKGROUND-COLOR: #f0fff0; TEXT-ALIGN: left }
	.list-highlight { BACKGROUND-COLOR: #ffff89 }
		</style>
		<script language="javascript">

var LastSelectedRow;
var LastSelectedRowClass;

//打印
function Print()
{
	OpenPrintWindow("../Report/PrintList.aspx?FromControlID=tdMaster$divList&css=" + escape("../images/locked-column.css"), "打印");
}

//设置行选中
function SetRowSelected(sender)
{
	if (LastSelectedRow)
	{
		LastSelectedRow.className = LastSelectedRowClass;
		
		LastSelectedRowClass = "";
		LastSelectedRow = "";
	}
	
	LastSelectedRow = sender;
	LastSelectedRowClass = LastSelectedRow.className;
	LastSelectedRow.className = "list-highlight";
}

function winload()
{
//	MyLockColumn("tbList", 1);
}

function MyLockColumn(TableID, ColCount)
{
	var table = document.getElementById(TableID);
	var trs = table.getElementsByTagName("TR");
	
	for(var i=0;i<trs.length;i++)
	{
		var tr = trs.item(i);
		
		//特殊
		if ((i == 1) || (i == 2))
		    continue;
		
		for(var j=0;j<ColCount;j++)
		{
			if (tr.cells[j])
			{
				tr.cells[j].className = "locked";
			}
		}
	}
}

function DoReport()
{
    Form1.btnDoReport.click();
}

		</script>
	</HEAD>
	<body onload="winload()">
		<form id="Form1" method="post" runat="server">
			<div style="DISPLAY:none">
			</div>
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" bgColor="white">
				<TR>
					<TD vAlign="top" align="left">
						<TABLE class="form" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td width="100%">
									<input class="submit" id="btnPrint" onclick="Print()" type="button" value="打 印" name="btnPrint"
										runat="server">
								</td>
							</tr>
						</TABLE>
					</TD>
				</TR>
				<tr>
					<td id="tdMaster">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="note" vAlign="bottom" width="200" height="25">集团造价汇总（元）</td>
								<td></td>
								<td noWrap align="right">
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr height="100%">
					<td>
									<div style="OVERFLOW:auto;WIDTH:100%;POSITION:absolute;HEIGHT:100%" id="divList">
										<asp:datagrid id="dgList" runat="server" Width="100%" AllowSorting="False" AutoGenerateColumns="False"
											PageSize="15" AllowPaging="False" CellPadding="0" CssClass="list" ShowFooter="False">
											<AlternatingItemStyle CssClass=""></AlternatingItemStyle>
											<ItemStyle CssClass=""></ItemStyle>
											<HeaderStyle CssClass="list-title"></HeaderStyle>
											<FooterStyle CssClass="list-title"></FooterStyle>
											<Columns>
												<asp:TemplateColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" HeaderText="项目名称" FooterText="合计" SortExpression="ProjectName">
													<HeaderStyle Wrap="False" HorizontalAlign="center"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%#  DataBinder.Eval(Container.DataItem, "ProjectName") %>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" HeaderText="<br>已批预算<br>(A)" SortExpression="BudgetMoney">
													<HeaderStyle Wrap="False" HorizontalAlign="center"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<%# RmsPM.BLL.MathRule.GetDecimalShowString(DataBinder.Eval(Container, "DataItem.BudgetMoney"))%>
													</ItemTemplate>
													<FooterTemplate>
														<asp:Label runat="server" ID="lblSumBudgetMoney"></asp:Label>
													</FooterTemplate>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" HeaderText="<br>已定合同<br>(B)" SortExpression="ContractMoney">
													<HeaderStyle Wrap="False" HorizontalAlign="center"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<%# RmsPM.BLL.MathRule.GetDecimalShowString(DataBinder.Eval(Container, "DataItem.ContractMoney"))%>
													</ItemTemplate>
													<FooterTemplate>
														<asp:Label runat="server" ID="lblSumContractMoney"></asp:Label>
													</FooterTemplate>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" HeaderText="<br>已定变更<br>(C)" SortExpression="ContractChangeMoney">
													<HeaderStyle Wrap="False" HorizontalAlign="center"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<%# RmsPM.BLL.MathRule.GetDecimalShowString(DataBinder.Eval(Container, "DataItem.ContractChangeMoney"))%>
													</ItemTemplate>
													<FooterTemplate>
														<asp:Label runat="server" ID="lblSumContractChangeMoney"></asp:Label>
													</FooterTemplate>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" HeaderText="待定<br>合同/变更<br>(D)" SortExpression="ContractApplyMoney">
													<HeaderStyle Wrap="False" HorizontalAlign="center"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<%# RmsPM.BLL.MathRule.GetDecimalShowString(DataBinder.Eval(Container, "DataItem.ContractApplyMoney"))%>
													</ItemTemplate>
													<FooterTemplate>
														<asp:Label runat="server" ID="lblSumContractApplyMoney"></asp:Label>
													</FooterTemplate>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" HeaderText="<br>估计最终价<br>(E)=B+C+D" SortExpression="ContractTotalMoney">
													<HeaderStyle Wrap="False" HorizontalAlign="center"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<%# RmsPM.BLL.MathRule.GetDecimalShowString(DataBinder.Eval(Container, "DataItem.ContractTotalMoney"))%>
													</ItemTemplate>
													<FooterTemplate>
														<asp:Label runat="server" ID="lblSumContractTotalMoney"></asp:Label>
													</FooterTemplate>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" HeaderText="<br>单方造价<br>(G1)=E/GFA" SortExpression="BuildingPrice">
													<HeaderStyle Wrap="False" HorizontalAlign="center"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<%# RmsPM.BLL.MathRule.GetDecimalShowString(DataBinder.Eval(Container, "DataItem.BuildingPrice"))%>
													</ItemTemplate>
													<FooterTemplate>
														<asp:Label runat="server" ID="lblSumBuildingPrice"></asp:Label>
													</FooterTemplate>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" HeaderText="<br>累计已批<br>(H)" SortExpression="ContractPay">
													<HeaderStyle Wrap="False" HorizontalAlign="center"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<%# RmsPM.BLL.MathRule.GetDecimalShowString(DataBinder.Eval(Container, "DataItem.ContractPay")) %>
													</ItemTemplate>
													<FooterTemplate>
														<asp:Label runat="server" ID="lblSumContractPay"></asp:Label>
													</FooterTemplate>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" HeaderText="<br>已批%<br>(I)=H/E" SortExpression="ContractPayPercent">
													<HeaderStyle Wrap="False" HorizontalAlign="center"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<%# RmsPM.BLL.StringRule.BuildShowPercentString(DataBinder.Eval(Container, "DataItem.ContractPayPercent"))%>
													</ItemTemplate>
													<FooterTemplate>
														<asp:Label runat="server" ID="lblSumContractPayPercent"></asp:Label>
													</FooterTemplate>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" HeaderText="<br>未批款<br>(J)=E-H" SortExpression="ContractPayBalance">
													<HeaderStyle Wrap="False" HorizontalAlign="center"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<%# RmsPM.BLL.MathRule.GetDecimalShowString(DataBinder.Eval(Container, "DataItem.ContractPayBalance"))%>
													</ItemTemplate>
													<FooterTemplate>
														<asp:Label runat="server" ID="lblSumContractPayBalance"></asp:Label>
													</FooterTemplate>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" HeaderText="<br>累计已付<br>(K)" SortExpression="ContractPayReal">
													<HeaderStyle Wrap="False" HorizontalAlign="center"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<%# RmsPM.BLL.MathRule.GetDecimalShowString(DataBinder.Eval(Container, "DataItem.ContractPayReal"))%>
													</ItemTemplate>
													<FooterTemplate>
														<asp:Label runat="server" ID="lblSumContractPayReal"></asp:Label>
													</FooterTemplate>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle Visible="False" NextPageText="下页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上页" HorizontalAlign="Right"
												CssClass="ListHeadTr"></PagerStyle>
										</asp:datagrid>
									</div>
					</td>
				</tr>
			</table>
			<div id="divHintLoad" style="DISPLAY: none; LEFT: 1px; WIDTH: 100%; POSITION: absolute; TOP: 50px">
				<TABLE id="tableHintLoad" height="100" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD vAlign="top" align="center"><iframe id="frameLoad" src="../Cost/LoadingPrepare.htm" frameBorder="no" width="100%" scrolling="auto"
								height="100%"></iframe>
						</TD>
					</TR>
				</TABLE>
			</div>
		</form>
	</body>
</HTML>
