<%@ Page language="c#" Inherits="RmsPM.Web.Project.Document" CodeFile="Document.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>文档管理</title>
		<META http-equiv="Content-Type" content="text/html; charset=gb2312">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Images/SplitPage.js"></SCRIPT>
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<SCRIPT language="javascript">
	function winload()
	{
	    var ShowGroupTree = '<%=Request.QueryString["ShowGroupTree"] %>';
	    if (ShowGroupTree == "0")
	    {
	        document.all.tdFrameLeft.style.display = "none";
	    }
	    
	    GotoList(Form1.txtGroupCode.value);
	}
	
	function GotoList(GroupCode)
	{
        var GroupCodeReadonly = '<%=Request.QueryString["GroupCodeReadonly"]%>';
        var Status = '<%=Request.QueryString["Status"]%>';
        var StatusReadonly = '<%=Request.QueryString["StatusReadonly"]%>';
		document.all.frameMain.src = "../Document/DocumentList.aspx?GroupCode=" + GroupCode + "&GroupCodeReadonly=" + GroupCodeReadonly + "&Status=" + Status + "&StatusReadonly=" + StatusReadonly + "&action=" + Form1.txtAct.value;
	}
		</SCRIPT>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0" onload="winload();">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td bgColor="#e4eff6" height="6"></td>
				</tr>
				<tr>
					<td height="25">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="topic" background="../images/topic_bg.gif"><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle">数据资料>共享文档
								</td>
								<td width="9"><IMG height="25" src="../images/topic_corr.gif" width="9"></td>
							</tr>
						</table>
					</td>
				</tr>
				<TR height="100%">
					<td valign="top">
						<table border="0" cellpadding="0" cellspacing="0" width="100%" height="100%">
							<tr>
								<TD vAlign="top" width="150" id="tdFrameLeft">
									<iframe id="frameLeft" src="../Systems/ShowSystemGroupTree.aspx?ClassCode=1001&MainFunc=GotoList" frameBorder="no" width="100%"
										scrolling="auto" height="100%"></iframe>
								</TD>
								<TD vAlign="top" align="left">
									<iframe id="frameMain" src='../Cost/LoadingPrepare.htm' frameBorder="no" width="100%"
													scrolling="auto" height="100%"></iframe>
								</TD>
							</tr>
						</table>
					</td>
				</TR>
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
					<td bgColor="#e4eff6" height="6"></td>
				</tr>
			</TABLE>
			<input type="hidden" name="txtAct" id="txtAct" runat="server"><input type="hidden" name="txtGroupName" id="txtGroupName" runat="server">
			<input type="hidden" name="txtGroupCode" id="txtGroupCode" runat="server"><input type="hidden" name="txtProjectCode" id="txtProjectCode" runat="server">
		</form>
	</body>
</HTML>
