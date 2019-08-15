<%@ Page language="c#" Inherits="RmsPM.Web.SendMsg.SendMsgModify" CodeFile="SendMsgModify.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="AttachMentList" Src="../UserControls/AttachMentList.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AttachMentAdd" Src="../UserControls/AttachMentAdd.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>SendMsgModfy</title>
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
					<td class="topic" colspan="2" align="center" background="../images/topic_bg.gif" height="25">消息发送</td>
				</tr>
				<tr>
					<td class="tools-area" width="0" style="WIDTH: 0px"><IMG src="../images/btn_li.gif" align="absMiddle"></td>
					<td class="tools-area" width="100%"><input class="submit" id="Button1" type="button" value="发 送" runat="server" NAME="btnSend" onserverclick="Button1_ServerClick">
						<input class="submit" id="Button2" onclick="window.close();" type="button" value="关 闭"
							name="btnCancel" runat="server"></td>
				</tr>
				<tr>
					<td vAlign="top" colspan="2">
						<table cellSpacing="7" cellPadding="0" width="100%" border="0">
							<tr>
								<td style="BORDER-RIGHT: #ededed 3px dotted; PADDING-RIGHT: 7px" vAlign="top" width="60%">
									<table cellSpacing="0" cellPadding="0" border="0">
										<tr>
											<td>
												<table cellSpacing="0" cellPadding="0" border="0">
													<tr>
														<td class="intopic">消息内容</td>
													</tr>
												</table>
											</td>
										</tr>
									</table>
									<table width="100%" class="input">
										<tr>
											<td width="100%">
												<TEXTAREA class="input" runat="server" name="txtContent" id="txtContent" style="WIDTH: 100%; HEIGHT: 56px"
													size="15"></TEXTAREA>
											</td>
										</tr>
										<tr>
											<td width="100%">
											
		                                        <uc1:AttachMentAdd id="AttachMentAdd1" runat="server" CtrlPath="../UserControls/"></uc1:AttachMentAdd>
											</td>
										</tr>
										
									</table>
									<br>
									<table cellSpacing="0" cellPadding="0" border="0">
										<tr>
											<td>
												<table cellSpacing="0" cellPadding="0" border="0">
													<tr>
														<td class="intopic">接收人</td>
													</tr>
												</table>
											</td>
											<td><input type="button" class="button-small" id="btnCopy" onclick="addNewTaskActor();" value=" 选择 "
													runat="server" NAME="btnCopy"></td>
										</tr>
									</table>
									<table width="100%" class="input">
										<tr>
											<td width="100%">
												<input type="text" class="input" runat="server" name="txtUserName" id="txtUserName" style="WIDTH: 100%; HEIGHT: 56px"
													size="15" disabled>
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<input type="hidden" class="input" runat="server" name="txtUserCode" id="txtUserCode">
		</form>
		<script>

	function addNewTaskActor()
	{
		OpenLargeWindow('../SelectBox/SelectSUMain.aspx?ReturnFunc=SelectReturn','选择接收人');
	}
	function SelectReturn(userCodes,userNames,stationCodes,stationNames)
    {
    	
    	document.all("txtUserName").value = userNames;
		document.all("txtUserCode").value = userCodes;
	}

		</script>
	</body>
</HTML>
