<%@ Reference Page="~/sendmsg/sendmsgmodify.aspx" %>
<%@ Page language="c#" Inherits="RmsPM.Web.SendMsg.SendMsgView" CodeFile="SendMsgView.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="AttachMentList" Src="../UserControls/AttachMentList.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AttachMentAdd" Src="../UserControls/AttachMentAdd.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>SendMsgView</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<script type="text/javascript" >
		function MyClose()
		{   
		    window.opener.location.reload();
		}
		</script>
	</HEAD>
	<body onunload="MyClose()">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" colSpan="2" height="25">消息查看</td>
				</tr>
				<tr>
					<td class="tools-area" style="WIDTH: 0px" width="0"><IMG src="../images/btn_li.gif" align="absMiddle"></td>
					<td class="tools-area" width="100%"><input class="submit" id="btnRevert" onclick="javascript:Revert();" type="button" value="回 复"
							name="btnRevert" runat="server"> <input class="submit" id="Button1" type="button" value="删 除" name="btnSend" runat="server" onserverclick="Button1_ServerClick">
						<input class="submit" id="Button2" onclick="window.close();" type="button" value="关 闭"
							name="btnCancel" runat="server">					    <input class="submit" id="btnTransmit" type="button" value="转 发" name="btnTransmit" runat ="server" onserverclick="btnTransmit_ServerClick" visible="true" /></td>
				</tr>
				<tr>
					<td vAlign="top" colSpan="2">
						<table cellSpacing="7" cellPadding="0" width="100%" border="0">
							<tr>
								<td style="BORDER-RIGHT: #ededed 3px dotted; PADDING-RIGHT: 7px" vAlign="top" width="60%">
									<table cellSpacing="0" cellPadding="0" border="0">
										<tr>
											<td class="intopic">消息内容</td>
										</tr>
									</table>
									<table class="input" width="100%">
										<tr>
											<td valign="top" id="txtContent" width="100%" runat="server" height="200"></td>
										</tr>
										<tr>
											<td valign="top" id="Td1" width="100%" nowrap runat="server" >附件：&nbsp;&nbsp;<uc1:AttachMentList id="AttachMentList1" runat="server" CtrlPath="../UserControls/"></uc1:AttachMentList></td>
										</tr>
										
									</table>
									<table cellSpacing="0" cellPadding="0" width="100%" border="0">
										<tr>
											<td id="tdusername" align="center" runat="server"></td>
											<td id="tdsendtime" align="center" runat="server"></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<span runat="server" id="spanscript"></span>
		</form>
	</body>
</HTML>
