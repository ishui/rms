<%@ Register TagPrefix="uc1" TagName="InputSubjectSetWithProject" Src="../UserControls/InputSubjectSetWithProject.ascx" %>
<%@ Page language="c#" Inherits="RmsPM.Web.Finance.FinanceInterfaceAnalysisSupplierModify" CodeFile="FinanceInterfaceAnalysisSupplierModify.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>厂商财务编码</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<LINK href="../Images/infra.css" type="text/css" rel="stylesheet">
		<script language="javascript">
<!--

//-->
		</script>
	</HEAD>
	<body scroll="no">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0" bgcolor="#ffffff">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">厂商财务编码</td>
				</tr>
				<tr>
					<td class="topic" vAlign="top" align="center">
						<table width="100%" border="0" cellpadding="0" cellspacing="0" class="form">
							<TR>
								<TD width="80" class="form-item">厂商：</TD>
								<TD><asp:Label Runat="server" ID="lblSupplierName"></asp:Label></TD>
							</TR>
						</table>
					</td>
				</tr>
				<tr>
				    <td>
							<table cellSpacing="0" cellPadding="0" width="100%" border="0">
								<tr>
									<td class="intopic" width="200">财务编码</td>
								</tr>
							</table>
				    </td>
				</tr>
				<tr height="100%">
				    <td>
				        <div style="overflow:auto;width:100%;height:100%;">
							<uc1:InputSubjectSetWithProject id="ucInputSubjectSet" runat="server" TableName="SupplierSubjectSet" KeyFieldName="SupplierSubjectSetCode" CodeFieldName="SupplierCode"></uc1:InputSubjectSetWithProject>
					    </div>
				    </td>
				</tr>
				<tr>
					<td>
						<table width="100%" cellspacing="10">
							<tr>
								<td align="center">
									<input id="btnSave" name="btnSave" type="button" class="submit" value="确 定" runat="server" onserverclick="btnSave_ServerClick">
									<input id="btnCancel" name="btnCancel" type="button" class="submit" value="取 消" onclick="javascript:self.close()">
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<input id="txtAct" type="hidden" name="txtAct" runat="server"> <input id="txtSupplierCode" type="hidden" name="txtSupplierCode" runat="server">
			<input type="hidden" name="txtSubjectSetCode" id="txtSubjectSetCode" runat="server"><input type="hidden" name="txtProjectCode" id="txtProjectCode" runat="server">
			<input type="hidden" name="txtIsGroup" id="txtIsGroup" runat="server">
		</form>
	</body>
</HTML>
