<%@ Page language="c#" Inherits="RmsPM.Web.DesignChange.DesignChangeModify" CodeFile="DesignChangeModify.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="DesignChangeControl" Src="Controls/DesignChangeControl.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>DesignChangeModify</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<LINK href="../Images/infra.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TBODY>
					<tr>
						<td valign="top" height="25">
							<table cellSpacing="0" cellPadding="0" width="100%" border="0">
								<tr>
									<td class="topic" align="center" background="../images/topic_bg.gif" height="25">设计变更管理</td>
								</tr>
							</table>
							<table class="table" id="tableToolBar" width="100%">
								<tr>
									<td class="tools-area" width="16"><IMG src="../images/btn_li.gif" align="absMiddle"></td>
									<td class="tools-area"><INPUT class="button" id="btn_Modify" type="button" value=" 修改 " name="btnModify" runat="server" onserverclick="btn_Modify_ServerClick">
										<INPUT class="button" id="btn_Save" type="button" value=" 保存 " name="btnModify" runat="server" onserverclick="btn_Save_ServerClick">
										<INPUT class="button" id="Bt_Cancel" type="button" value=" 取消 " name="btnModify" runat="server" onserverclick="Bt_Cancel_ServerClick">
										<INPUT class="button" id="btn_ChangeAuditing" type="button" value="变更评审" name="btnModify"
											runat="server" onclick="OpenAuditing();return false">&nbsp; <INPUT class="button" id="Bt_Close" type="button" value=" 关闭 " name="btnModify" runat="server"
											onclick="CloseAddRefurbish()">
									</td>
								</tr>
							</table>
							<uc1:DesignChangeControl id="DesignChangeControl1" runat="server"></uc1:DesignChangeControl>
						</td>
					</tr>
				</TBODY>
			</table>
			</form>
		<script>
function CloseAddRefurbish()
{
			window.opener.location.reload(true);
			window.close();
}
function OpenAuditing()
{
	OpenFullWindow('<%=DesignAuditingUrl%>?ProjectCode=<%=Request["ProjectCode"]%>&DesignCode=<%=Request["ApplicationCode"]%>','审批')
}
		</script>
	</body>
</HTML>
