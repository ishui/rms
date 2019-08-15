<%@ Page language="c#" Inherits="RmsPM.Web.Remind.NoticeInfo" CodeFile="NoticeInfo.aspx.cs" ValidateRequest="false" %>
<%@ Register TagPrefix="uc1" TagName="FeedBack" Src="../UserControls/FeedBack.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AttachMentList" Src="../UserControls/AttachMentList.ascx" %>
<HTML>
	<HEAD>
		<title>通知</title>
		<META http-equiv="Content-Type" content="text/html; charset=gb2312">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<SCRIPT language="javascript">
			function doCancel(){
				window.close();				
			}
			function doModify(){
				OpenMiddleWindow('Notify.aspx?Action=Modify&Code=<%=Request.QueryString["Code"]%>&ProjectCode=<%=Request["ProjectCode"]%>','');
				window.close();				
			}		
		</SCRIPT>
	</HEAD>
	<body >
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" bgColor="white" border="0">
				<tr>
					<td class="topic" align=center background="../images/topic_bg.gif" colSpan="2" height="25"><FONT face="宋体">通知信息</FONT></td>
				</tr>
				<tr>
					<td class="topic"  colSpan="2" height="25"><input class="submit" visible=false id="SaveToolsButton" type="button" value="修改" name="SaveToolsButton"
										 runat="server"　onserverclick="SaveToolsButton_ServerClick"><INPUT visible=false class="submit" id="btDelete" type="button" value="删除" name="btDelete" runat="server" onserverclick="btDelete_ServerClick"></td>
					
				</tr>
				<tr vAlign="top" width="100%">
					<td width="80%">
						<TABLE class="form" id="Table4" cellSpacing="0" cellPadding="0" width="100%" border="0">
						    <TR width="100%" runat="server" id="trNotice">
								<TD class="form-item" align="right" width="20%" style="height: 29px">类型：</TD>
								　<td class="tdBlank" style="height: 29px">
                                     <b><asp:Label ID="lblNoticeClass" runat="server" Width="300px"></asp:Label></b>
                                  </td>
                             </TR>
							<TR width="100%">
								<TD class="form-item" align="right" width="20%" style="height: 29px">标题：</TD>
								<TD class="tdBlank" style="height: 29px">
									<b>
										<asp:Label id="lblTitle" runat="server" Width="300px"></asp:Label></b></TD>
							</TR>
							<TR style="DISPLAY: block" width="100%">
								<TD class="form-item" align="right"><FONT style="BACKGROUND-COLOR: #ffebcd">发布人：</FONT></TD>
								<TD class="tdBlank" colSpan="3"><asp:Label id="lbSubmitname" runat="server" Width="300px"></asp:Label></TD>
							</TR>
							<TR style="DISPLAY: block" width="100%">
								<TD class="form-item" align="right"><FONT style="BACKGROUND-COLOR: #ffebcd">日期：</FONT></TD>
								<TD class="tdBlank" colSpan="3"><asp:Label id="lblSubmitDate" runat="server" Width="300px"></asp:Label></TD>
							</TR>
							
							<TR style="DISPLAY: block" width="100%">
								<TD class="form-item" align="right">发布范围：</TD>
								<TD class="tdBlank" colSpan="3"><asp:Label ID="lblUser" Runat="server"></asp:Label></TD>
							</TR>
							
							<TR style="DISPLAY: block" width="100%" height=100>
								<TD class="form-item" align="right">内容：</TD>
								<TD class="tdBlank" colSpan="3">
								    <div id="divContent" runat=server></div>
									
								</TD>
							</TR>
							<TR style="DISPLAY: block" width="100%">
								<TD class="form-item" align="right">附件：</TD>
								<TD class="tdBlank" colSpan="3">
									<uc1:AttachMentList id="myAttachMentList" runat="server"></uc1:AttachMentList></TD>
							</TR>
							<TR style="DISPLAY: block" width="100%">
								<TD class="form-item" align="right">最后修改：</TD>
								<TD class="tdBlank" colSpan="3"><asp:Label ID="lbLastUpdate" Runat="server"></asp:Label></TD>
							</TR>
						</TABLE>
						<br>
						<uc1:FeedBack id="FeedBack1" runat="server"></uc1:FeedBack>
					</td>
				</tr>
			</table>
			<input type="hidden" id="hUserFlag" name="hUserFlag" runat="server" value="0"> <input type="hidden" id="hUserCode" name="hUserCode" runat="server">
		</form>
	</body>
</HTML>
