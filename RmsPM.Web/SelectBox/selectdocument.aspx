<%@ Page language="c#" Inherits="RmsPM.Web.SelectBox.SelectDocument" CodeFile="SelectDocument.aspx.cs" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ѡ���ĵ�</title>
		<META http-equiv="Content-Type" content="text/html; charset=gb2312">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<script language="javascript">
			function doCancel()
			{
				window.close();
			}
		</script>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" bgColor="white" border="0">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">ѡ���ĵ�</td>
				</tr>
				<TR>
					<TD vAlign="top" align="center">
						<table class="search-area" border="0">
							<tr><td width=30></td>
								<TD  align="left" width=60%>����ʱ��ӣ�
								<cc3:calendar id="dtbStartDate" runat="server" CalendarResource="../Images/CalendarResource/"
										ReadOnly="False" Display="True"></cc3:calendar>
								����<cc3:calendar id="dtbEndDate" runat="server" CalendarResource="../Images/CalendarResource/" ReadOnly="False"
										Display="True"></cc3:calendar></td>
								<td align="center" rowspan="2"><input class="submit" id="btnSearch" type="button" value="�� ��" name="btnSearch" runat="server" onserverclick="btnSearch_ServerClick"></td>
							</tr>
							<tr><td width=30></td>
								<td noWrap align="left" >�ĵ����ƣ�
								<input id="txtDocumentName" style="WIDTH: 120px;HEIGHT: 22px" type="text" maxLength="25"
										name="txtDocumentName" runat="server" class="input">
								���ߣ�
								<input id="txtCreatePerson" style="WIDTH: 120px;HEIGHT: 22px" type="text" maxLength="25"
										name="txtCreatePerson" runat="server" class="input">
										�ĵ����ͣ�<SELECT class="Select" id="SelectType" style="WIDTH: 110px" name="sltType" runat="server"></SELECT></td>
							</tr>
						</table>
						<asp:datagrid id="dgDocumentList" runat="server" DataKeyField="DocumentCode" AutoGenerateColumns="False"
							AllowSorting="True" GridLines="Horizontal" CellPadding="2" Width="100%" CssClass="list">
							<FooterStyle CssClass="list-title"></FooterStyle>
							<AlternatingItemStyle CssClass="list-i"></AlternatingItemStyle>
							<HeaderStyle CssClass="list-title"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="Title" HeaderText="�ĵ�����"></asp:BoundColumn>
								<asp:BoundColumn DataField="CreatePerson" HeaderText="ǩ����"></asp:BoundColumn>
								<asp:BoundColumn DataField="CreateDate" HeaderText="ǩ��ʱ��" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="ѡ��">
									<ItemTemplate>
										<asp:CheckBox ID="chkContract" Runat="server"></asp:CheckBox>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
						</asp:datagrid>
						<table id="tbButton" align="center" runat="server" cellspacing="10">
							<tr align="center" width="100%" valign="bottom">
								<td rowSpan="2"><input class="submit" id="SaveToolsButton" type="button" value="ȷ ��" name="SaveToolsButton"
										runat="server" onserverclick="SaveToolsButton_ServerClick"> <input class="submit" id="CancelToolsButton" type="button" value="ȡ ��" name="CancelToolsButton"
										onclick="doCancel();return false;">
								</td>
							</tr>
						</table>
					</TD>
				</TR>
			</table>
			<br>
			<br>
			</TD></TR></TABLE form <></form>
	</body>
</HTML>
