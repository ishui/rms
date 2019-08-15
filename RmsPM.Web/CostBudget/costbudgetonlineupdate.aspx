<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Page language="c#" Inherits="RmsPM.Web.CostBudget.CostBudgetOnlineUpdate" CodeFile="CostBudgetOnlineUpdate.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>项目费用即时更新</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">

<script language="javascript">

function winload()
{
	if (Form1.txtAct.value == "")
	{
		window.location.href = "CostBudgetOnlineUpdate.aspx?ProjectCode=" + Form1.txtProjectCode.value + "&Act=OnlineUpdate";
	}
}
		
</script>

	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0" onload="winload()">
		<form id="Form1" method="post" runat="server">
			<div style="position:absolute;z-index:9999;width:100%;height:100%">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" bgColor="#ffffff" border="0">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">项目费用即时更新</td>
				</tr>
				<tr height="100%">
					<td class="topic" vAlign="top" align="center">
						<table class="form" cellSpacing="0" cellPadding="0" width="100%" border="0" height="100%">
							<tr>
								<td align="center" valign="middle">正在更新项目费用，请稍侯...</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr style="DISPLAY:none">
					<td>
						<table cellSpacing="10" width="100%">
							<tr>
								<td align="center"><input class="submit" id="btnSave" type="button" value="确 定" name="btnSave" runat="server">
									<input class="submit" id="btnCancel" onclick="javascript:self.close()" type="button" value="取 消"
										name="btnCancel">
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			</div>
			<input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server"><input id="txtAct" type="hidden" name="txtAct" runat="server">
		</form>
	</body>
</HTML>
