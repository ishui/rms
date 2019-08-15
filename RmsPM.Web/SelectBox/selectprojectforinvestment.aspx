<%@ Page language="c#" Inherits="RmsPM.Web.SelectBox.SelectProjectForInvestment" CodeFile="SelectProjectForInvestment.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ѡ����Ŀ</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../images/checkbox.js"></SCRIPT>
		<script>
//��ѡ
function DoSelectProject(projectCode,projectName)
{
	window.opener.DoSelectProject(projectCode,projectName);
	window.close();
}

//��ѡ��ȷ��
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

/* ����ѡ�������
	if (arr[0] == "")
	{
		alert('��ѡ��һ���������¼');
		return false;
	}
*/
	window.opener.DoSelectProject(arr[0], arr[1]);
	window.close();
}

//��ѡ�����
function btnAddClick()
{
	var arr = ChkGetSelectedCodeName(document.all.chkSelect);

	if (arr[0] == "")
	{
		alert('��ѡ��һ���������¼');
		return false;
	}

	Form1.txtIsAdd.value = "1";
	
	Form1.txtAddCode.value = arr[0];
	Form1.txtAddName.value = arr[1];
	
	return true;
}

//��ѡ���Ƴ�
function btnDeleteClick()
{
	var arr = ChkGetSelectedCodeName(document.all.chkSelect2);

	if (arr[0] == "")
	{
		alert('��ѡ��һ���������¼');
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
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">ѡ����Ŀ</td>
				</tr>
				<tr>
					<td vAlign="top">
						<table class="search-area" cellSpacing="0" cellPadding="0" border="0">
							<tr>
								<td>
									<table>
										<TR>
											<td>��Ŀ���ƣ�</td>
											<td><input class="input" id="txtSearchProjectName" name="txtSearchProjectName" runat="server"></td>
											<td>���У�</td>
											<td><SELECT class="select" style="DISPLAY: none" id="sltSearchStatus" name="sltSearchStatus"
													runat="server">
													<option value="" selected>--��ѡ��--</option>
												</SELECT>
												<SELECT class="select" id="Select1" name="sltSearchStatus" runat="server">
													<option value="" selected>--��ѡ��--</option>
													<OPTION value="">����</OPTION>
													<OPTION value="">�Ϻ�</OPTION>
													<OPTION value="">����</OPTION>
												</SELECT>
											</td>
											<td>&nbsp;<INPUT class="submit" id="btnSearch" type="button" value="�� ��" name="btnSearch" runat="server" onserverclick="btnSearch_ServerClick"></td>
											<td>&nbsp;<INPUT class="submit" id="Button1" type="button" value="�� ��" name="btnSearch" runat="server"></td>
										</TR>
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
									<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%"><asp:datagrid id="dgList" runat="server" CssClass="list" Width="100%" AllowSorting="True" AutoGenerateColumns="False"
											GridLines="Horizontal" CellPadding="0">
											<AlternatingItemStyle CssClass="list-i"></AlternatingItemStyle>
											<HeaderStyle CssClass="list-title"></HeaderStyle>
											<FooterStyle CssClass="list-title"></FooterStyle>
											<Columns>
												<asp:TemplateColumn Visible="False" HeaderText="&lt;input type='checkbox' name='chkAll' onclick='ChkSelectAll(document.all.chkSelect, document.all.chkAll.checked);' title='ȫѡ��ȫ��ѡ'&gt;">
													<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<input type="checkbox" name="chkSelect" class="list-checkbox" onclick="ChkSelectRow('<%# Container.ItemIndex + 1%>', this, document.all.dgList, 'list-2', '');" value='<%#DataBinder.Eval(Container, "DataItem.ProjectCode")%>' title='<%#DataBinder.Eval(Container, "DataItem.ProjectName")%>'>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn Visible="False" SortExpression="ProjectName" HeaderText="��Ŀ����">
													<HeaderStyle Wrap="False" HorizontalAlign="Left"></HeaderStyle>
													<ItemTemplate>
														<%# DataBinder.Eval(Container, "DataItem.ProjectName") %>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="ProjectName" HeaderText="��Ŀ����">
													<HeaderStyle Wrap="False" HorizontalAlign="Left"></HeaderStyle>
													<ItemTemplate>
														<a href="#" onclick='DoSelectProject("<%# DataBinder.Eval(Container, "DataItem.ProjectCode") %>","<%# DataBinder.Eval(Container, "DataItem.ProjectName") %>")'>
															<%# DataBinder.Eval(Container, "DataItem.ProjectName") %>
														</a>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn Visible="False" DataField="kgDate" SortExpression="kgDate" HeaderText="����" DataFormatString="{0:yyyy/MM}"></asp:BoundColumn>
												<asp:BoundColumn Visible="False" DataField="jgDate" SortExpression="jgDate" HeaderText="����" DataFormatString="{0:yyyy/MM}"></asp:BoundColumn>
												<asp:BoundColumn HeaderText="����"></asp:BoundColumn>
											</Columns>
											<PagerStyle NextPageText="��ҳ&lt;img src='../images/page_next.gif' width=9 height=9 border=0 &gt;"
												PrevPageText="&lt;img src='../images/page_pre.gif' width=9 height=9 border=0&gt;��ҳ" HorizontalAlign="Right"
												CssClass="ListHeadTr"></PagerStyle>
										</asp:datagrid></div>
								</td>
								<td id="tdAdd2" style="DISPLAY: none" runat="server">
									<table cellSpacing="0" cellPadding="10">
										<tr>
											<td></td>
										</tr>
									</table>
								</td>
								<td id="tdAdd" style="DISPLAY: none" runat="server">
									<table height="100%" cellSpacing="0" cellPadding="0">
										<tr>
											<td class="intopic" width="200">ѡ����</td>
											<td><input class="button-small" id="btnDelete" onclick="if (!btnDeleteClick()) return false;"
													type="button" value="ɾ ��" name="btnDelete" runat="server" onserverclick="btnDelete_ServerClick">&nbsp;
											</td>
										</tr>
										<tr>
											<td colSpan="2">
												<div style="OVERFLOW: auto; WIDTH: 250px; HEIGHT: 100%"><asp:datagrid id="dgListAdd" runat="server" CssClass="list" Width="100%" AllowSorting="False"
														AutoGenerateColumns="False" GridLines="Horizontal" CellPadding="0" DataKeyField="Code">
														<AlternatingItemStyle CssClass=""></AlternatingItemStyle>
														<ItemStyle CssClass=""></ItemStyle>
														<HeaderStyle CssClass="list-title"></HeaderStyle>
														<FooterStyle CssClass="list-title"></FooterStyle>
														<Columns>
															<asp:TemplateColumn HeaderText="&lt;input type='checkbox' name='chkAll2' onclick='ChkSelectAll(document.all.chkSelect2, document.all.chkAll2.checked);' title='ȫѡ��ȫ��ѡ'&gt;"
																Visible="True">
																<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
																<ItemStyle Wrap="False" HorizontalAlign="Center" Width="30"></ItemStyle>
																<ItemTemplate>
																	<input type="checkbox" name="chkSelect2" class="list-checkbox" onclick="ChkSelectRow('<%# Container.ItemIndex + 1%>', this, document.all.dgListAdd, 'list-2', '');" value='<%#DataBinder.Eval(Container, "DataItem.Code")%>' title='<%#DataBinder.Eval(Container, "DataItem.Name")%>'>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="��Ŀ����" Visible="True">
																<HeaderStyle HorizontalAlign="Left" Wrap="False"></HeaderStyle>
																<ItemTemplate>
																	<%# DataBinder.Eval(Container, "DataItem.Name") %>
																	<input type="hidden" id="txtName" name="txtName" runat="server" value='<%# DataBinder.Eval(Container, "DataItem.Name") %>'>
																</ItemTemplate>
															</asp:TemplateColumn>
														</Columns>
														<PagerStyle NextPageText="��һҳ&lt;img src='../images/page_next.gif' width=9 height=9 border=0 &gt;"
															PrevPageText="&lt;img src='../images/page_pre.gif' width=9 height=9 border=0&gt;��һҳ" HorizontalAlign="Right"
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
								<td align="center"><input class="submit" id="btnClear" style="DISPLAY: none" onclick="DoSelectProject('', '');"
										type="button" value="�� ��" name="btnClear" runat="server"> <input class="submit" id="btnClose" onclick="javascript:self.close()" type="button" value="ȡ ��"
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
								<td align="center"><input class="submit" id="btnAdd" onclick="if (!btnAddClick()) return false;" type="button"
										value="�� ��" name="btnAdd" runat="server" onserverclick="btnAdd_ServerClick"> <input class="submit" id="btnOK" onclick="btnOKClick();" type="button" value="ȷ ��" name="btnOK"
										runat="server"> <input class="submit" id="btnCancel" onclick="javascript:self.close()" type="button" value="ȡ ��"
										name="btnCancel">
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<input id="txtType" type="hidden" name="txtType" runat="server"> <input id="txtAddCode" type="hidden" name="txtAddCode" runat="server"><input id="txtAddName" type="hidden" name="txtAddName" runat="server">
			<input id="txtIsAdd" type="hidden" name="txtIsAdd" runat="server"><input id="txtAllowNull" type="hidden" name="txtAllowNull" runat="server">
		</form>
	</body>
</HTML>
