<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Page language="c#" Inherits="RmsPM.Web.Project.TaskTempletIn" CodeFile="TaskTempletIn.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>��ģ�嵼��</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body scroll="no">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0" bgcolor="white">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">��ģ�嵼��
					</td>
				</tr>
				<tr>
					<td><STRONG><FONT color="red">˵���� 
								��ģ�幤������ӵ�����������</FONT></STRONG></td>
				</tr>
				<tr>
					<td class="topic" vAlign="top" align="center">
						<table width="100%" border="0" cellpadding="0" cellspacing="0" class="form">
							<TR>
								<TD class="form-item" width="30%">�������</TD>
								<TD width="70%"><asp:Label runat="server" ID="lblTaskName"></asp:Label></TD>
							</TR>
							<TR>
								<TD class="form-item">ѡ��ģ��:</TD>
								<TD>
									<select id="sltTemplet" runat="server" NAME="sltTemplet">
									</select>
									<asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" ControlToValidate="sltTemplet" ErrorMessage="*"></asp:RequiredFieldValidator>
								</TD>
							</TR>
							<TR>
								<TD class="form-item">��ʼʱ�䣺</TD>
								<TD>
									<cc3:calendar id="dtbProjectStartDate" runat="server" CalendarResource="../Images/CalendarResource/"
										ReadOnly="False" Display="True"></cc3:calendar></TD>
							</TR>
						</table>
						<table width="100%" cellspacing="10">
							<tr>
								<td align="center">
									<input id="btnSave" name="btnSave" type="button" class="submit" value="ȷ ��" runat="server" onclick="document.all.divHintSave.style.display='';" onserverclick="btnSave_ServerClick">
									<input id="btnCancel" name="btnCancel" type="button" class="submit" value="ȡ ��" onclick="javascript:self.close()">
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<div id="divHintSave" style="DISPLAY: none; LEFT: 1px; WIDTH: 100%; POSITION: absolute; TOP: 200px">
				<TABLE id="tableHintSave" height="100" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD vAlign="top" align="center"><iframe id="frameSave" src="../Cost/SavingWating.htm" frameBorder="no" width="100%" scrolling="auto"
								height="100%"></iframe>
						</TD>
					</TR>
				</TABLE>
			</div>
			<INPUT id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server"> <INPUT id="txtWBSCode" type="hidden" name="txtWBSCode" runat="server">
		</form>
	</body>
</HTML>
