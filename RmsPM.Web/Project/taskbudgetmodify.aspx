<%@ Page language="c#" Inherits="RmsPM.Web.Project.TaskBudgetModify" CodeFile="TaskBudgetModify.aspx.cs" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>工作预算维护</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
	</HEAD>
	<body bottomMargin="10" topMargin="10">
		<form id="Form1" method="post" runat="server">
			<div style="DISPLAY: none"><input id="btnPayConditionReturn" type="button" value="btnPayConditionReturn" name="btnPayConditionReturn"
					runat="server" onserverclick="btnPayConditionReturn_ServerClick">
			</div>
			<table id="tableMain" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">工作预算维护-
						<asp:Label id="lblTaskName" runat="server" BackColor="Transparent"></asp:Label></td>
				</tr>
				<tr>
					<td vAlign="top" align="center">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="intopic" width="200">款项分解</td>
								<td><input class="button-small" id="btnNewItem" type="button" value="新增款项" name="btnNewItem"
										runat="server" onserverclick="btnNewItem_ServerClick"></td>
							</tr>
						</table>
						<TABLE cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
							<TR>
								<TD><asp:datagrid id="dgCostList" runat="server" CssClass="list" Width="100%" AllowSorting="True"
										AutoGenerateColumns="False" PageSize="15">
										<AlternatingItemStyle CssClass="list-i"></AlternatingItemStyle>
										<HeaderStyle CssClass="list-title"></HeaderStyle>
										<FooterStyle CssClass="list-title"></FooterStyle>
										<Columns>
											<asp:BoundColumn Visible="False" DataField="TaskBudgetCode"></asp:BoundColumn>
											<asp:TemplateColumn HeaderText="序号">
												<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
												<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
												<ItemTemplate>
													<%# Container.ItemIndex + 1 %>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="款项名称">
												<ItemStyle></ItemStyle>
												<ItemTemplate>
													<input size=30 type=text runat=server id="txtItemName" value='<%#  DataBinder.Eval(Container.DataItem, "ItemName")  %>' class=input NAME="txtItemName">
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="付款时间" HeaderStyle-Width=120>
												<ItemStyle></ItemStyle>
												<ItemTemplate>
													<cc3:calendar id="dtPlanningPayDate" runat="server" Display="True" ReadOnly="False" CalendarResource="../Images/CalendarResource/" value='<%#  DataBinder.Eval(Container.DataItem, "PlanningPayDate")  %>'>
													</cc3:calendar>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="付款条件">
												<ItemStyle></ItemStyle>
												<ItemTemplate>
													<span id="spanPayConditionHtml">
														<%#  DataBinder.Eval(Container.DataItem, "PayConditionHtml") %>
													</span>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="费用项">
												<ItemStyle></ItemStyle>
												<ItemTemplate>
													<input type=text readonly runat=server class=input-readonly size=40 id=txtCostNameShow value='<%#  DataBinder.Eval(Container.DataItem, "CostName")  %>' NAME="txtCostNameShow">
													<input type=hidden runat=server id="txtCostName" value='<%#  DataBinder.Eval(Container.DataItem, "CostName")  %>' class=input NAME="txtCostName">
													<input type=hidden runat=server id="txtCostCode" value='<%#  DataBinder.Eval(Container.DataItem, "CostCode")  %>' class=input NAME="txtCostCode">
													<a  href="javascript:SelectCost(<%#Container.ItemIndex + 2 %>);"><img src="../images/ToolsItemSearch.gif" border="0"></a>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="金额（元）">
												<ItemTemplate>
													<input type=text runat=server id="txtMoney" value='<%# RmsPM.BLL.StringRule.BuildShowNumberString( DataBinder.Eval(Container.DataItem, "Money")) %> ' class=input-nember NAME="txtMoney">
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:ButtonColumn Text="&lt;img src=../images/del.gif width=16 height=16 border=0&gt;" HeaderText="删除"
												CommandName="Delete"></asp:ButtonColumn>
										</Columns>
										<PagerStyle NextPageText="下页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上页" HorizontalAlign="Right"
											CssClass="ListHeadTr"></PagerStyle>
									</asp:datagrid></TD>
							</TR>
						</TABLE>
						<table width="100%" cellspacing="10">
							<tr>
								<td align="center">
									<input id="btnSave" name="btnSave" type="button" class="submit" value="确 定" runat="server"
										onclick="doSave();" onserverclick="btnSave_ServerClick"> <input id="btnCancel" name="btnCancel" type="button" class="submit" value="取 消" onclick="javascript:self.close()">
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<iframe id="iframeSave" style="DISPLAY: none" src="../Cost/SavingWating.htm" frameBorder="no"
				width="100%" scrolling="auto" height="70%"></iframe><input id="txtSelectCostItemIndex" type="hidden" name="txtSelectCostItemIndex" runat="server">
			<input id="txtWBSCode" type="hidden" name="txtWBSCode" runat="server"> <input id="txtTaskBudgetCode" type="hidden" name="txtTaskBudgetCode" runat="server">
			<input id="txtConditionPayDate" type="hidden" name="txtConditionPayDate" runat="server">
			<input id="txtAddTaskCode" type="hidden" name="txtAddTaskCode" runat="server">
		</form>
		<script language="javascript">
<!--

	undoHidden();

	function doSave()
	{
		document.all("iframeSave").style.display = "";
		document.all("tableMain").style.display = "none";
	}

	function undoHidden()
	{
		document.all("iframeSave").style.display = "none";
		document.all("tableMain").style.display = "";
	}
	

	function SelectCost(i)
	{
		document.all("txtSelectCostItemIndex").value=i;
		OpenMiddleWindow('../SelectBox/SelectCost.aspx?Type=Single&ProjectCode=<%=Request["ProjectCode"]%>','选择费用项');
	}
	
	function GetReturnSingleCostCode( costCode, costName )
	{
		var i = Form1.txtSelectCostItemIndex.value;
		//alert (document.all("dgCostList__ctl" + i + "_txtCostCode"));
		document.all("dgCostList__ctl" + i + "_txtCostCode").value = costCode;
		document.all("dgCostList__ctl" + i + "_txtCostNameShow").value = costName;
		document.all("dgCostList__ctl" + i + "_txtCostName").value = costName;
	}
	
	
	//修改付款条件
	function ModifyPayCondition(taskBudgetConditionCode, taskBudgetCode)
	{
		Form1.txtTaskBudgetCode.value = taskBudgetCode;
		OpenCustomWindow('TaskBudgetConditionModify.aspx?InputWBSCode=<%=Request["WBSCode"]%>&TaskBudgetCode=' + taskBudgetCode + "&TaskBudgetConditionCode=" + taskBudgetConditionCode + '&ProjectCode=<%=Request["ProjectCode"]%>', "修改付款条件", 400, 260);
	}
	
	//新增付款条件
	function AddPayCondition(taskBudgetCode)
	{
		Form1.txtTaskBudgetCode.value = taskBudgetCode;
		OpenCustomWindow('TaskBudgetConditionModify.aspx?InputWBSCode=<%=Request["WBSCode"]%>&TaskBudgetCode=' + taskBudgetCode + '&ProjectCode=<%=Request["ProjectCode"]%>', "新增付款条件", 400, 260);
	}
	
	//修改付款条件返回
	function PayConditionReturn(sPayDate)
	{
		Form1.txtConditionPayDate.value = sPayDate;
		Form1.btnPayConditionReturn.click();
	}
		
	//显示工作信息
	function OpenTask(WBSCode)
	{
		OpenFullWindow("WBSInfo.aspx?WBSCode="+WBSCode,"");
	}

	//选择工作项
	function SelectTask()
	{
		OpenMiddleWindow("../SelectBox/SelectTask.aspx?Flag=1&WBSCode=");
	}

	//选择工作项
	function SelectTaskReturn(code, name)
	{
		Form1.txtAddTaskCode.value = code;
		Form1.btnAddTaskReturn.click();;
	}
		
	
//-->
		</script>
	</body>
</HTML>
