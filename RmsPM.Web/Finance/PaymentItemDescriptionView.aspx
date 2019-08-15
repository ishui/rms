<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PaymentItemDescriptionView.aspx.cs" Inherits="Finance_PaymentItemDescriptionView" %>
<%@ Register TagPrefix="uc1" TagName="AttachMentList" Src="../UserControls/AttachMentList.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title></title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR" />
		<meta content="C#" name="CODE_LANGUAGE" />
		<meta content="JavaScript" name="vs_defaultClientScript" />
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
		<link href="../Images/index.css" type="text/css" rel="stylesheet" />
		<script language="javascript" src="../Rms.js"></script>
	</head>
	<body>
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">备注</td>
				</tr>
				<tr>
					<td valign="top" colspan="1" rowspan="1">
						<table cellspacing="7" cellpadding="0" width="100%" border="0">
							<tr>
								<td style="BORDER-RIGHT: #ededed 3px dotted; PADDING-RIGHT: 7px" valign="top" width="60%">
									<table id="OpinionTitle" cellspacing="0" cellpadding="0" border="0" runat="server">
										<tr>
											<td class="intopic">备注</td>
										</tr>
									</table>
									<table class="input" id="OpinionTable" width="100%" runat="server" name="OpinionTable">
										<tr>
											<td align="center"><br />
												<div align="left" style="OVERFLOW: auto; WIDTH: 90%" runat="server" id="OpinionDiv"></div>
												<br />
												<br />
											</td>
										</tr>
									</table>
									<br />
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
</html>
