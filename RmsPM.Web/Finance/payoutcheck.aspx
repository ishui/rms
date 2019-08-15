<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Page language="c#" Inherits="RmsPM.Web.Finance.PayoutCheck" CodeFile="PayoutCheck.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>付款单审核</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0" bgcolor="#ffffff">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">付款单审核</td>
				</tr>
				<tr height="100%" style="display:none">
					<td class="topic" vAlign="top" align="center">
						<table class="form" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<TD class="form-item" width="100">审核意见：</TD>
								<TD><textarea runat="server" class="textarea" id="txtCheckOpinion" name="txtCheckOpinion" rows="5"
										style="WIDTH:100%"></textarea>
								</TD>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td>
						<table cellpadding="0" width="100%">
							<tr>
								<TD class="note">确实要审核吗？</TD>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td>
						<table cellSpacing="10" width="100%">
							<tr>
								<td align="center"><input class="submit" id="btnSave" type="button" value="确 定" name="btnSave" runat="server" onserverclick="btnSave_ServerClick">
									<input class="submit" id="btnCancel" onclick="javascript:self.close()" type="button" value="取 消"
										name="btnCancel">
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server"> <INPUT id="txtIsNew" type="hidden" name="txtIsNew" runat="server">
			<INPUT id="txtContractCode" type="hidden" name="txtContractCode" runat="server">
			<INPUT id="txtIsContract" type="hidden" name="txtCode" runat="server"> <INPUT id="txtPayoutCode" type="hidden" name="txtPayoutCode" runat="server">
			<INPUT id="txtSupplyCode" type="hidden" name="txtSupplyCode" runat="server">
		</form>
		<script language="javascript">
		</script>
	</body>
</HTML>
