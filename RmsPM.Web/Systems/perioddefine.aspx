<%@ Page language="c#" Inherits="RmsPM.Web.Systems.PeriodDefine" CodeFile="PeriodDefine.aspx.cs" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>�ƻ�����</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0" scroll="no">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0" bgcolor="white"
				id="Table1">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">�ƻ�����</td>
				</tr>
				<tr>
					<td vAlign="top" align="center">
						<table width="100%" border="0" cellpadding="0" cellspacing="0" class="form" id="Table2">
							<TR>
								<TD class="form-item" width="20%">��Ŀ�ƻ���ʼ���£�</TD>
								<TD width="30%">
									<SELECT id="sltYear" runat="server">
									</SELECT>��
									<SELECT id="sltMonth" runat="server">
									</SELECT>��
								</TD>
								<TD class="form-item" width="20%"></TD>
								<TD noWrap width="30%"></TD>
							</TR>
							<TR>
								<TD class="form-item">�ƻ����ڣ�</TD>
								<TD><SELECT id="sltPeriodMonth" name="Select1" runat="server">
										<OPTION value="12" selected>���</OPTION>
										<OPTION value="6">����</OPTION>
										<OPTION value="3">����</OPTION>
									</SELECT></TD>
								<TD class="form-item">��������</TD>
								<TD noWrap><select id="sltTotalPeriods" runat="server"></select></TD>
							</TR>
						</table>
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="intopic" width="200">��������</td>
								<td><input type="button" class="button-small" id="btnReSetPeriod" runat="server" value="�����趨" onserverclick="btnReSetPeriod_ServerClick"></td>
							</tr>
						</table>
						<asp:datagrid id="dgYearName" runat="server" CssClass="list" CellPadding="2" GridLines="Horizontal"
							AllowSorting="True" AutoGenerateColumns="False" PageSize="15" Width="100%" AllowPaging="False">
							<AlternatingItemStyle CssClass="list-i"></AlternatingItemStyle>
							<HeaderStyle CssClass="list-title"></HeaderStyle>
							<FooterStyle CssClass="list-title"></FooterStyle>
							<Columns>
								<asp:BoundColumn DataField="PeriodDefineCode" Visible="false"></asp:BoundColumn>
								<asp:BoundColumn DataField="PeriodIndex"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="��������">
									<ItemTemplate>
										<input class=input type=text runat=server id=txtYearName value='<%# DataBinder.Eval( Container,"DataItem.PeriodName" ) %>'>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="StartDate" DataFormatString="{0:yyyy-MM-dd}" HeaderText="��ʼʱ��"></asp:BoundColumn>
								<asp:BoundColumn DataField="EndDate" DataFormatString="{0:yyyy-MM-dd}" HeaderText="����ʱ��"></asp:BoundColumn>
							</Columns>
							<PagerStyle NextPageText="��һҳ&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;��һҳ" HorizontalAlign="Right"
								CssClass="list-title"></PagerStyle>
						</asp:datagrid>
						<table cellspacing="10" width="100%" id="Table3">
							<tr>
								<td align="center">
									<input id="btnSave" name="btnSave" type="button" class="submit" value="ȷ ��" runat="server" onserverclick="btnSave_ServerClick">&nbsp;
									<input id="btnCancel" name="btnCancel" type="button" class="submit" value="ȡ ��" onclick="javascript:self.close()">
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
