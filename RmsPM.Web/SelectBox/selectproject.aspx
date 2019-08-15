<%@ Page language="c#" Inherits="RmsPM.Web.SelectBox.SelectProject" CodeFile="SelectProject.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>选择项目</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../images/checkbox.js"></SCRIPT>
		<script>
//单选
function DoSelectProject(projectCode,projectName)
{
    var flag = '<%=Request["Flag"]%>';
	window.opener.<%=ViewState["ReturnFunc"]%>(projectCode,projectName, flag);
	window.close();
}

//多选后确定
function btnOKClick()
{
	var arr;
	
	if (Form1.txtIsAdd.value == "")
	{
		arr = ChkGetSelectedCodeName(document.all.chkSelect);
	}
	else
	{
		arr = new Array();
		arr.push(Form1.txtAddCode.value);
		arr.push(Form1.txtAddName.value);
	}

/* 允许不选，即清空
	if (arr[0] == "")
	{
		alert('请选择一条或多条记录');
		return false;
	}
*/

    var flag = '<%=Request["Flag"]%>';
	window.opener.<%=ViewState["ReturnFunc"]%>(arr[0], arr[1], flag);
	window.close();
}

//多选后添加
function btnAddClick()
{
	var arr = ChkGetSelectedCodeName(document.all.chkSelect);

	if (arr[0] == "")
	{
		alert('请选择一条或多条记录');
		return false;
	}

	Form1.txtIsAdd.value = "1";
	
	Form1.txtAddCode.value = arr[0];
	Form1.txtAddName.value = arr[1];
	
	return true;
}

//多选后移除
function btnDeleteClick()
{
	var arr = ChkGetSelectedCodeName(document.all.chkSelect2);

	if (arr[0] == "")
	{
		alert('请选择一条或多条记录');
		return false;
	}
	
	Form1.txtAddCode.value = arr[0];
	Form1.txtAddName.value = arr[1];
	
	return true;
}
		</script>
	</HEAD>
	<body scroll="no">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" bgColor="#ffffff" border="0">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">选择项目</td>
				</tr>
				<tr>
					<td vAlign="top">
						<table class="search-area" cellSpacing="0" cellPadding="0" border="0">
							<tr>
								<td>
									<table>
										<tr>
											<td width="70">开工年份：</td>
											<td><input class="input" id="txtSearchKgYear" size="4" name="txtSearchKgYear" runat="server"></td>
											<td width="70">竣工年份：</td>
											<td><input class="input" id="txtSearchJgYear" size="4" name="txtSearchJgYear" runat="server"></td>
											<td><input class="submit" id="btnSearch" type="button" value="搜 索" name="btnSearch" runat="server" onserverclick="btnSearch_ServerClick"></td>
										</tr>
										<tr>
											<td>项目名称：</td>
											<td><input class="input" id="txtSearchProjectName" name="txtSearchProjectName" runat="server"></td>
											<td>项目状态：</td>
											<td><SELECT class="select" id="sltSearchStatus" name="sltSearchStatus" runat="server">
													<option value="" selected>--请选择--</option>
												</SELECT></td>
											<td>&nbsp;</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr height="100%">
					<td vAlign="top">
						<table height="100%" cellSpacing="0" cellPadding="0" width="100%">
							<tr>
								<td width="100%">
									<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%"><asp:datagrid id="dgList" runat="server" CellPadding="0" GridLines="Horizontal" AutoGenerateColumns="False"
											AllowSorting="True" Width="100%" CssClass="list">
											<AlternatingItemStyle CssClass="list-i"></AlternatingItemStyle>
											<ItemStyle CssClass=""></ItemStyle>
											<HeaderStyle CssClass="list-title"></HeaderStyle>
											<FooterStyle CssClass="list-title"></FooterStyle>
											<Columns>
												<asp:TemplateColumn HeaderText="&lt;input type='checkbox' name='chkAll' onclick='ChkSelectAll(document.all.chkSelect, document.all.chkAll.checked);' title='全选或全不选'&gt;"
													Visible="False">
													<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<input type="checkbox" name="chkSelect" class="list-checkbox" onclick="ChkSelectRow('<%# Container.ItemIndex + 1%>', this, document.all.dgList, 'list-2', '');" value='<%#DataBinder.Eval(Container, "DataItem.ProjectCode")%>' title='<%#DataBinder.Eval(Container, "DataItem.ProjectName")%>'>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="项目名称" SortExpression="ProjectName" Visible="False">
													<HeaderStyle HorizontalAlign="Left" Wrap="False"></HeaderStyle>
													<ItemTemplate>
														<%# DataBinder.Eval(Container, "DataItem.ProjectName") %>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="项目名称" SortExpression="ProjectName">
													<HeaderStyle HorizontalAlign="Left" Wrap="False"></HeaderStyle>
													<ItemTemplate>
														<a href="#" onclick='DoSelectProject("<%# DataBinder.Eval(Container, "DataItem.ProjectCode") %>","<%# DataBinder.Eval(Container, "DataItem.ProjectName") %>")'>
															<%# DataBinder.Eval(Container, "DataItem.ProjectName") %>
														</a>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="kgDate" HeaderText="开期" DataFormatString="{0:yyyy/MM}" SortExpression="kgDate"></asp:BoundColumn>
												<asp:BoundColumn DataField="jgDate" HeaderText="竣期" DataFormatString="{0:yyyy/MM}" SortExpression="jgDate"></asp:BoundColumn>
											</Columns>
											<PagerStyle NextPageText="下页&lt;img src='../images/page_next.gif' width=9 height=9 border=0 &gt;"
												PrevPageText="&lt;img src='../images/page_pre.gif' width=9 height=9 border=0&gt;上页" HorizontalAlign="Right"
												CssClass="ListHeadTr"></PagerStyle>
										</asp:datagrid></div>
								</td>
								<td id="tdAdd2" style="DISPLAY: none" runat="server">
									<table cellSpacing="0" cellPadding="10">
										<tr>
											<td>
											</td>
										</tr>
									</table>
								</td>
								<td id="tdAdd" style="DISPLAY: none" runat="server">
									<table cellSpacing="0" cellPadding="0" height="100%">
										<tr>
											<td class="intopic" width="200">选择结果</td>
											<td><input class="button-small" id="btnDelete" onclick="if (!btnDeleteClick()) return false;"
													type="button" value="删 除" name="btnDelete" runat="server" onserverclick="btnDelete_ServerClick">&nbsp;
											</td>
										</tr>
										<tr>
											<td colspan="2">
												<div style="OVERFLOW: auto; WIDTH: 250px; HEIGHT: 100%"><asp:datagrid id="dgListAdd" runat="server" CellPadding="0" GridLines="Horizontal" AutoGenerateColumns="False"
														AllowSorting="False" Width="100%" CssClass="list" DataKeyField="Code">
														<AlternatingItemStyle CssClass=""></AlternatingItemStyle>
														<ItemStyle CssClass=""></ItemStyle>
														<HeaderStyle CssClass="list-title"></HeaderStyle>
														<FooterStyle CssClass="list-title"></FooterStyle>
														<Columns>
															<asp:TemplateColumn HeaderText="&lt;input type='checkbox' name='chkAll2' onclick='ChkSelectAll(document.all.chkSelect2, document.all.chkAll2.checked);' title='全选或全不选'&gt;"
																Visible="True">
																<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
																<ItemStyle Wrap="False" HorizontalAlign="Center" Width="30"></ItemStyle>
																<ItemTemplate>
																	<input type="checkbox" name="chkSelect2" class="list-checkbox" onclick="ChkSelectRow('<%# Container.ItemIndex + 1%>', this, document.all.dgListAdd, 'list-2', '');" value='<%#DataBinder.Eval(Container, "DataItem.Code")%>' title='<%#DataBinder.Eval(Container, "DataItem.Name")%>'>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="项目名称" Visible="True">
																<HeaderStyle HorizontalAlign="Left" Wrap="False"></HeaderStyle>
																<ItemTemplate>
																	<%# DataBinder.Eval(Container, "DataItem.Name") %>
																	<input type="hidden" id="txtName" name="txtName" runat="server" value='<%# DataBinder.Eval(Container, "DataItem.Name") %>'>
																</ItemTemplate>
															</asp:TemplateColumn>
														</Columns>
														<PagerStyle NextPageText="下页&lt;img src='../images/page_next.gif' width=9 height=9 border=0 &gt;"
															PrevPageText="&lt;img src='../images/page_pre.gif' width=9 height=9 border=0&gt;上页" HorizontalAlign="Right"
															CssClass="ListHeadTr"></PagerStyle>
													</asp:datagrid></div>
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr id="trSingle1" runat="server">
					<td>
						<table cellSpacing="10" width="100%">
							<tr>
								<td align="center">
										<input type="button" id="btnClear" class="submit" value="清 除" name="btnClear" onclick="DoSelectProject('', '');"
											runat="server" style="DISPLAY:none">
										<input class="submit" id="btnClose" onclick="javascript:self.close()" type="button" value="取 消"
										name="btnClose">
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr id="trMulti1" style="DISPLAY: none" runat="server">
					<td>
						<table cellSpacing="10" width="100%">
							<tr>
								<td align="center">
									 <input style="display:none" id="btnAdd" name="btnAdd" type="button" class="submit" value="添 加" onclick="if (!btnAddClick()) return false;"
										runat="server" onserverclick="btnAdd_ServerClick"> <input id="btnOK" name="btnOK" type="button" class="submit" value="确 定" onclick="btnOKClick();"
										runat="server"> <input id="btnCancel" name="btnCancel" type="button" class="submit" value="取 消" onclick="javascript:self.close()">
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<input type="hidden" id="txtType" name="txtType" runat="server"> <input type="hidden" id="txtAddCode" name="txtAddCode" runat="server"><input type="hidden" id="txtAddName" name="txtAddName" runat="server">
			<input type="hidden" id="txtIsAdd" name="txtIsAdd" runat="server"><input type="hidden" id="txtAllowNull" name="txtAllowNull" runat="server">
			<input type="hidden" id="txtAccess" name="txtAccess" runat="server">
		</form>
	</body>
</HTML>
