<%@ Page language="c#" Inherits="RmsPM.Web.BiddingManage.BiddingReturnModifyPage" CodeFile="BiddingReturnModifyPage.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="BiddingEmitModify" Src="BiddingEmitModify.ascx" %>
<%@ Register TagPrefix="uc1" TagName="BiddingReturnModify" Src="BiddingReturnModify.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>BiddingReturnModify</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td height="25">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="topic" align="center" background="../images/topic_bg.gif" height="25">
									<asp:Label id="Lb_Title" runat="server">回 标</asp:Label></td>
							</tr>
						</table>
						<table class="table" id="tableToolBar" width="100%">
							<tr>
								<td class="tools-area" width="16"><IMG src="../images/btn_li.gif" align="absMiddle"></td>
								<td class="tools-area"><input name="btnSave" id="btnSave" type="button" value=" 保存 " class="button" runat="server" onserverclick="btnSave_ServerClick">&nbsp;
									<input name="btnClose" id="btnClose" type="button" value=" 关闭 " class="button" runat="server"
										onclick="javascript:window.close();"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td class="table" vAlign="top">
						<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td><FONT face="宋体">
										<uc1:BiddingEmitModify id="BiddingEmitModify1" runat="server"></uc1:BiddingEmitModify></FONT></td>
							</tr>
							<tr>
								<td>
									<uc1:BiddingReturnModify id="BiddingReturnModify1" runat="server"></uc1:BiddingReturnModify></td>
							</tr>
						</TABLE>
					</td>
				</tr>
				<tr>
					<td height="12">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td background="../images/corl_bg.gif"><IMG height="12" src="../images/corl.gif" width="12"></td>
								<td width="12"><IMG height="12" src="../images/corr.gif" width="12"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td height="6" bgcolor="#e4eff6"></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
