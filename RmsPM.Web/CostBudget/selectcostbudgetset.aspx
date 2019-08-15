<%@ Page language="c#" Inherits="RmsPM.Web.CostBudget.SelectCostBudgetSet" CodeFile="SelectCostBudgetSet.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>选择预算设置表</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../images/checkbox.js"></SCRIPT>
		<script>

window.returnValue = "";
  		
//单选
function DoSelect(Code,Name)
{
//	var flag = '<%=Request["flag"]%>';
	window.returnValue = Code;
	window.close();
}

		</script>
	</HEAD>
	<body scroll="no">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0" bgcolor="#ffffff">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">选择<span id="spanTitle" runat="server">预算表</span></td>
				</tr>
				<tr style="display:none">
					<td class="table" vAlign="top">
						<table border="0" cellpadding="0" cellspacing="0" class="search-area">
							<tr>
								<td>
									<table>
										<tr>
											<td><span runat="server" id="spanName"></span>：</td>
											<td><input class="input" id="txtSearchValue" size="20" name="txtSearchValue" runat="server"></td>
											<td><input class="submit" id="btnSearch" type="button" value="搜 索" name="btnSearch" runat="server" onserverclick="btnSearch_ServerClick"></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr height="100%">
					<td valign="top">
						<div style="OVERFLOW: auto;WIDTH: 100%;HEIGHT: 100%">
							<asp:datagrid id="dgList" runat="server" CssClass="list" Width="100%" AllowSorting="False" AutoGenerateColumns="False"
								GridLines="Horizontal" CellPadding="0">
								<AlternatingItemStyle CssClass=""></AlternatingItemStyle>
								<ItemStyle CssClass=""></ItemStyle>
								<HeaderStyle CssClass="list-title"></HeaderStyle>
								<FooterStyle CssClass="list-title"></FooterStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="&lt;input type='checkbox' name='chkAll' onclick='ChkSelectAll(document.all.chkSelect, document.all.chkAll.checked);' title='全选或全不选'&gt;"
										Visible="False">
										<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle Wrap="False" HorizontalAlign="Center" Width="30"></ItemStyle>
										<ItemTemplate>
											<input type="checkbox" name="chkSelect" class="list-checkbox" onclick="ChkSelectRow('<%# Container.ItemIndex + 1%>', this, document.all.dgList, 'list-2', '');" value='<%#DataBinder.Eval(Container, "DataItem.CostBudgetSetCode")%>' title='<%#DataBinder.Eval(Container, "DataItem.CostBudgetSetName")%>'>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="预算表名称" SortExpression="CostBudgetSetName" Visible="False">
										<HeaderStyle HorizontalAlign="Left" Wrap="False"></HeaderStyle>
										<ItemTemplate>
											<%# DataBinder.Eval(Container, "DataItem.CostBudgetSetName") %>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="预算表名称" SortExpression="CostBudgetSetName">
										<HeaderStyle HorizontalAlign="Left" Wrap="False"></HeaderStyle>
										<ItemTemplate>
											<a href="#" onclick='DoSelect("<%# DataBinder.Eval(Container, "DataItem.CostBudgetSetCode") %>","<%# DataBinder.Eval(Container, "DataItem.CostBudgetSetName") %>")'>
												<%# DataBinder.Eval(Container, "DataItem.CostBudgetSetName") %>
											</a>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle NextPageText="下页&lt;img src='../images/page_next.gif' width=9 height=9 border=0 &gt;"
									PrevPageText="&lt;img src='../images/page_pre.gif' width=9 height=9 border=0&gt;上页" HorizontalAlign="Right"
									CssClass="ListHeadTr"></PagerStyle>
							</asp:datagrid>
						</div>
					</td>
				</tr>
				<tr id="trSingle1" runat="server">
					<td>
						<table width="100%" cellspacing="10">
							<tr>
								<td align="center">
									<input id="btnClose" name="btnClose" type="button" class="submit" value="取 消" onclick="javascript:self.close()">
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr id="trMulti1" runat="server" style="DISPLAY:none">
					<td>
						<table width="100%" cellspacing="10">
							<tr>
								<td align="center">
									<input id="btnOK" name="btnOK" type="button" class="submit" value="确 定" onclick="btnOKClick();"
										runat="server"> <input id="btnCancel" name="btnCancel" type="button" class="submit" value="取 消" onclick="javascript:self.close()">
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<input type="hidden" id="txtType" name="txtType" runat="server">
			<input type="hidden" id="txtProjectCode" name="txtProjectCode" runat="server">
		</form>
	</body>
</HTML>
