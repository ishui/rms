<%@ Page language="c#" Inherits="RmsPM.Web.Cost.BudgetModifyCheck" CodeFile="BudgetModifyCheck.aspx.cs" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Ԥ��������</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body scroll="no">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0" id="tableMain">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">�ɱ�Ԥ��������</td>
				</tr>
				<tr id="trModify" runat="server">
					<td class="topic" vAlign="top" align="center">
						<table width="100%" border="0" cellpadding="0" cellspacing="0" class="form">
							<TR>
								<td style="WIDTH: 100px" valign="middle" align="center">��<br>
									��<br>
									��<br>
									��</td>
								<TD colspan="3"><TEXTAREA style="WIDTH: 98%" rows="5" cols="20" id="txtOpinion" name="TEXTAREA1" runat="server"
										class="textarea"></TEXTAREA></TD>
							</TR>
							<TR>
								<TD class="form-item" noWrap width="20%">��Чʱ�䣺</TD>
								<TD width="30%">
									<cc3:calendar id="dtInsureDate" runat="server" Value="" Display="True" ReadOnly="False" CalendarResource="../Images/CalendarResource/"></cc3:calendar></TD>
								<TD width="20%" class="form-item" noWrap></TD>
								<TD width="30%" noWrap></TD>
							</TR>
							<TR>
								<TD class="form-item" noWrap width="20%">�� �� �ˣ�</TD>
								<TD width="30%">
									<asp:label id="lblCheckPerson" runat="server"></asp:label></TD>
								<TD width="20%" class="form-item" noWrap>���ʱ�䣺</TD>
								<TD width="30%" noWrap>
									<asp:label id="lblCheckDate" runat="server"></asp:label></TD>
							</TR>
						</table>
						<table width="100%" cellspacing="10">
							<tr>
								<td align="center">
									<input id="btnSave" name="btnSave" type="button" class="submit" value="ȷ ��" runat="server"
										onclick=" if ( !doSave()) return false;" onserverclick="btnSave_ServerClick">&nbsp;<INPUT class="submit" id="btnBlankout" onclick="if( !confirm('�Ƿ�ȷ������ ��')) return false;" type="button" value="�� ��" name="btnSave"
										runat="server" onserverclick="btnBlankout_ServerClick">&nbsp; <input id="btnCancel" name="btnCancel" type="button" class="submit" value="ȡ ��" onclick="javascript:self.close()">
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr id="trView" runat="server">
					<td class="topic" vAlign="top" align="center">
						<table width="100%" border="0" cellpadding="0" cellspacing="0" class="form">
							<TR>
								<td style="WIDTH: 100px" valign="middle" align="center">������</td>
								<TD colspan="3">
									<asp:label id="lblOpinion" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="form-item" noWrap width="20%">��Чʱ�䣺</TD>
								<TD width="30%"><asp:Label ID="lblInsureDate" Runat="server"></asp:Label></TD>
								<TD width="20%" class="form-item" noWrap></TD>
								<TD width="30%" noWrap></TD>
							</TR>
							<TR>
								<TD class="form-item" noWrap width="20%">�� �� �ˣ�</TD>
								<TD width="30%">
									<asp:label id="lblCheckPerson0" runat="server"></asp:label></TD>
								<TD width="20%" class="form-item" noWrap>���ʱ�䣺</TD>
								<TD width="30%" noWrap>
									<asp:label id="lblCheckDate0" runat="server"></asp:label></TD>
							</TR>
						</table>
						<table width="100%" cellspacing="10">
							<tr>
								<td align="center">
									<input id="btnCancel0" name="btnCancel" type="button" class="submit" value="�� ��" onclick="javascript:self.close()">
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<iframe id="iframeSave" style="DISPLAY: none" src="../Cost/SavingWating.htm" frameBorder="no"
				width="100%" scrolling="auto" height="70%"></iframe>
		</form>
		<script language="javascript">
<!--

	undoHidden();

	function doSave()
	{
		if ( !confirm('�Ƿ�ȷ�����ͨ�� ��'))
			return false;
		document.all("iframeSave").style.display = "";
		document.all("tableMain").style.display = "none";
		return true;
	}

	function undoHidden()
	{
		document.all("iframeSave").style.display = "none";
		document.all("tableMain").style.display = "";
	}
//-->
		</script>
	</body>
</HTML>
