<%@ Page language="c#" Inherits="RmsPM.Web.Contract.ChangeInfo" CodeFile="ChangeInfo.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>��ͬ�����Ϣ</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" bgColor="white" border="0">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">��ͬ�����Ϣ</td>
				</tr>
				<tr>
					<td class="topic" vAlign="top" align="center">
						<table class="form" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD class="form-item" width=20%>��ͬ��ţ�</TD>
								<TD width=30%>
									<asp:Label id="lblContractID" runat="server"></asp:Label></TD>
								<TD class="form-item" width=20%>��ͬ���ƣ�</TD>
								<TD width=30%>
									<asp:Label id="lblContractName" runat="server"></asp:Label></TD>
							</TR>
							<TR>
								<TD class="form-item">���ԭ��</TD>
								<TD colSpan="3">
									<asp:Label id="lblChangeReason" runat="server"></asp:Label></TD>
							</TR>
							<TR>
								<TD class="form-item">������ݣ�</TD>
								<TD colSpan="3">
									<asp:Label id="lblChangeRemark" runat="server"></asp:Label></TD>
							</TR>
							<TR>
								<TD class="form-item">����� �� �ˣ�</TD>
								<TD>
									<asp:Label id="lblChangePersonName" runat="server"></asp:Label></TD>
								<TD class="form-item">���ʱ�䣺</TD>
								<TD>
									<asp:Label id="lblChangeOpinionDate" runat="server"></asp:Label></TD>
							</TR>
						</table>
						<table cellSpacing="10" width="100%">
							<tr>
								<td align="center">&nbsp;&nbsp;&nbsp; <INPUT class="submit" id="btnClose" onclick="javascript:self.close()" type="button" value="�� ��"
										name="btnCancel" runat="server">
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			</TD></TR></TABLE>
		</form>
	</body>
</HTML>
