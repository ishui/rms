<%@ Page language="c#" Inherits="RmsPM.Web.SelectBox.SelectTask" CodeFile="SelectTask.aspx.cs" %>
<%@ Register TagPrefix="cc2" Namespace="RmsPM.WebControls.ToolsBar" Assembly="RmsPM.WebControls" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ѡ������</title>
		<META http-equiv="Content-Type" content="text/html; charset=gb2312">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../Rms.js"></script>
		<script language="javascript">

	function doCancel()
	{
		window.close();
	}

	function Select(WBSCode, TaskName)
	{
		var Define1 = '<%=Request["Define1"]%>';
		window.opener.<%=ViewState["ReturnFunc"]%>(WBSCode, TaskName, Define1);
		window.close();
	}
	
		</script>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="100%" bgColor="white" border="0" height="100%">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">ѡ������</td>
				</tr>
				<TR>
					<TD valign="top">
						<table class="search-area" cellSpacing="0" cellPadding="0" border="0">
							<tr>
								<td>
									<table>
										<tr>
											<TD colSpan="6">�ƻ���ʼʱ��ӣ�<cc3:calendar id="dtbPlannedStartFromDate" runat="server" Display="True" ReadOnly="False" CalendarResource="../Images/CalendarResource/"
													Value=''></cc3:calendar>
												&nbsp;&nbsp;����<cc3:calendar id="dtbPlannedStartToDate" runat="server" Display="True" ReadOnly="False" CalendarResource="../Images/CalendarResource/"
													Value=''></cc3:calendar></SELECT></TD>
											<td align="center" rowSpan="2"><input class="submit" id="btnSearch" type="button" value="�� ��" name="btnSearch" runat="server" onserverclick="btnSearch_ServerClick"></td>
										</tr>
										<tr>
											<TD colSpan="6">�ƻ�����ʱ��ӣ�<cc3:calendar id="dtbPlannedFinishFromDate" runat="server" Display="True" ReadOnly="False" CalendarResource="../Images/CalendarResource/"
													Value=''></cc3:calendar>
												&nbsp;&nbsp;����<cc3:calendar id="dtbPlannedFinishToDate" runat="server" Display="True" ReadOnly="False" CalendarResource="../Images/CalendarResource/"
													Value=''></cc3:calendar></SELECT>
											</TD>
										</tr>
										<tr>
											<td noWrap align="right">�����</td>
											<td><input class="input" id="txtTaskName" name="txtKgYear" runat="server"></td>
											<td noWrap align="right">�����ˣ�</td>
											<td><input class="input" id="txtMaster" name="txtJgYear" runat="server"></td>
											<td></td>
											<td noWrap align="right">������״̬��</td>
											<td><SELECT class="Select" id="SelectStatus" name="sltSex" runat="server">
													<option value="" selected>--��ѡ��--</option>
												</SELECT></td>
											<td></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</TD>
				</TR>
				<tr height="100%">
					<td valign="top"><asp:datagrid id="dgTaskList" runat="server" DataKeyField="WBSCode" PageSize="10" AutoGenerateColumns="False"
							AllowSorting="True" GridLines="Horizontal" CellPadding="2" AllowPaging="true" Width="100%" CssClass="list">
							<AlternatingItemStyle CssClass="list-i"></AlternatingItemStyle>
							<ItemStyle></ItemStyle>
							<HeaderStyle CssClass="list-title"></HeaderStyle>
							<FooterStyle CssClass="list-title"></FooterStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="����������">
									<HeaderStyle HorizontalAlign="Left" Wrap="False"></HeaderStyle>
									<ItemTemplate>
										<a href="#" id=taskName runat=server onclick='Select(this.code, this.name)' code='<%# DataBinder.Eval(Container, "DataItem.WBSCode") %>' name='<%# DataBinder.Eval(Container, "DataItem.TaskName") %>' >
											<%# DataBinder.Eval(Container, "DataItem.myTaskName") %>
										</a>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="Master" HeaderText="������"></asp:BoundColumn>
								<asp:BoundColumn DataField="StatusName" HeaderText="��ǰ״̬"></asp:BoundColumn>
								<asp:BoundColumn DataField="CompletePercent" HeaderText="��ǰ����" DataFormatString="{0}%"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="ѡ��">
									<ItemTemplate>
										<asp:CheckBox ID="checkTask" Runat="server"></asp:CheckBox>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle NextPageText="��һҳ&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;��һҳ" HorizontalAlign="Right"
								CssClass="ListHeadTr"></PagerStyle>
						</asp:datagrid>
						<table id="tbButton" align="center" runat="server">
							<tr align="center" width="100%">
								<td rowSpan="2">
									<input class="submit" id="SaveToolsButton" type="button" value="ȷ ��" name="SaveToolsButton"
										runat="server" onserverclick="SaveToolsButton_ServerClick"> <input class="submit" id="CancelToolsButton" onclick="doCancel();return false;" type="button"
										value="ȡ ��" name="CancelToolsButton">
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<a href="#" id="a" runat="server">as</a>
		</form>
	</body>
</HTML>
