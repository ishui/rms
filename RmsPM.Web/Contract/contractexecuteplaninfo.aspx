<%@ Page language="c#" Inherits="RmsPM.Web.Contract.ContractExecutePlanInfo" CodeFile="ContractExecutePlanInfo.aspx.cs" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>��ִͬ�б���</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" bgColor="white" border="0">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">��ִͬ�б���</td>
				</tr>
				<tr>
					<td class="topic" vAlign="top" align="center">
						<table class="form" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD class="form-item">������</TD>
								<TD colspan="3">
									<asp:Label id="lblExecuteDetail" runat="server"></asp:Label></TD>
							</TR>
							<TR>
								<TD class="form-item">�ƻ�ִ���ˣ�</TD>
								<TD>
									<asp:Label id="LabelsltPerson" runat="server"></asp:Label></TD>
								<TD class="form-item">�ƻ�ִ��ʱ�䣺</TD>
								<TD>
									<asp:Label id="LabeldtbExecuteDate" runat="server"></asp:Label></TD>
							</TR>
							<TR>
								<TD class="form-item">ʵ��ִ���ˣ�</TD>
								<TD>
									<asp:Label id="lblFactPerson" runat="server"></asp:Label>
								</TD>
								<TD class="form-item">ʵ��ִ��ʱ�䣺</TD>
								<TD>
									<asp:Label id="lblFactExecuteDate" runat="server"></asp:Label></TD>
							</TR>
							<TR>
								<TD class="form-item">ִ�������</TD>
								<TD colspan="3">
									<asp:Label id="lblDescription" runat="server"></asp:Label>
								</TD>
							</TR>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
