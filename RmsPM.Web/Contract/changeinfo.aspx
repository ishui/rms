<%@ Page language="c#" Inherits="RmsPM.Web.Contract.ChangeInfo" CodeFile="ChangeInfo.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>合同变更信息</title>
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
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">合同变更信息</td>
				</tr>
				<tr>
					<td class="topic" vAlign="top" align="center">
						<table class="form" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD class="form-item" width=20%>合同编号：</TD>
								<TD width=30%>
									<asp:Label id="lblContractID" runat="server"></asp:Label></TD>
								<TD class="form-item" width=20%>合同名称：</TD>
								<TD width=30%>
									<asp:Label id="lblContractName" runat="server"></asp:Label></TD>
							</TR>
							<TR>
								<TD class="form-item">变更原因：</TD>
								<TD colSpan="3">
									<asp:Label id="lblChangeReason" runat="server"></asp:Label></TD>
							</TR>
							<TR>
								<TD class="form-item">变更内容：</TD>
								<TD colSpan="3">
									<asp:Label id="lblChangeRemark" runat="server"></asp:Label></TD>
							</TR>
							<TR>
								<TD class="form-item">变更审 核 人：</TD>
								<TD>
									<asp:Label id="lblChangePersonName" runat="server"></asp:Label></TD>
								<TD class="form-item">审核时间：</TD>
								<TD>
									<asp:Label id="lblChangeOpinionDate" runat="server"></asp:Label></TD>
							</TR>
						</table>
						<table cellSpacing="10" width="100%">
							<tr>
								<td align="center">&nbsp;&nbsp;&nbsp; <INPUT class="submit" id="btnClose" onclick="javascript:self.close()" type="button" value="关 闭"
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
