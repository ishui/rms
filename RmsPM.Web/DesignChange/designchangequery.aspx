<%@ Page language="c#" Inherits="RmsPM.Web.DesignChange.DesignChangeQuery" CodeFile="DesignChangeQuery.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="DesignListControl" Src="Controls/DesignListControl.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>DesignChangeQuery</title>
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
					<td bgColor="#e4eff6" height="6"></td>
				</tr>
				<tr>
					<td height="25">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="topic" background="../images/topic_bg.gif"><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle"><span id="spanTitle">
										签证计划</span></td>
								<td width="9"><IMG height="25" src="../images/topic_corr.gif" width="9"></td>
							</tr>
						</table>
						<table class="table" id="tableToolBar" width="100%">
							<tr>
								<td class="tools-area" width="16"><IMG src="../images/btn_li.gif" align="absMiddle"></td>
								<td class="tools-area"><input name="btnNew" id="Button1" type="button" value=" 新增 " class="button" runat="server"
										onclick="javascript:OpenNewDesign();">
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td valign=top><uc1:DesignListControl id="DesignListControl1" runat="server"></uc1:DesignListControl></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
<script>
function OpenNewDesign()
{
	OpenFullWindow('DesignChangeModify.aspx?OperateState=Add&ProjectCode=<%=Request["ProjectCode"]%>','新增变更');
	
}
</script>