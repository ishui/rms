<%@ Register TagPrefix="uc1" TagName="AttachMentList" Src="../UserControls/AttachMentList.ascx" %>
<%@ Page language="c#" Inherits="RmsPM.Web.WorkFlowControl.WorkFlowOpinionView" CodeFile="WorkFlowOpinionView.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title></title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">流程意见</td>
				</tr>
				<tr>
					<td vAlign="top" colSpan="1" rowSpan="1">
						<table cellSpacing="7" cellPadding="0" width="100%" border="0">
							<tr>
								<td style="BORDER-RIGHT: #ededed 3px dotted; PADDING-RIGHT: 7px" vAlign="top" width="60%">
									<table id="OpinionTitle" cellSpacing="0" cellPadding="0" border="0" runat="server">
										<tr>
											<td class="intopic">处理意见</td>
										</tr>
									</table>
									<table class="input" id="OpinionTable" width="100%" runat="server" name="OpinionTable">
										<tr>
											<td align="center"><br>
												<div align="left" style="OVERFLOW: auto; WIDTH: 90%" runat="server" id="OpinionDiv"></div>
												<br>
												<br>
											</td>
										</tr>
									</table>
									<br>
									<table class="input" width="100%">
										<tr>
											<td align="center">
												<div align="left" style="OVERFLOW: auto; WIDTH: 90%">
													附件：<uc1:attachmentlist id="AttachMentList1" runat="server" CtrlPath="../UserControls/"></uc1:attachmentlist></div>
											</td>
										</tr>
									</table>
									<table width="100%">
										<tr>
											<td align="center" runat="server" id="UserName">
											</td>
											<td align="center" runat="server" id="OpininoDate">
											</td>
										</tr>
									</table>
									<br>
									<table width="100%">
										<tr>
											<td align="center"><input class="submit" id="btnCancel" onclick="window.close();" type="button" value=" 关 闭 "
													name="btnCancel" runat="server">
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
