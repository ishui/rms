<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Page language="c#" Inherits="RmsPM.Web.Project.WBS" CodeFile="WBS.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>WBS</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Images/SplitPage.js"></SCRIPT>
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<SCRIPT language="javascript">
			
	function ShowWBS(WBSCode){
		OpenFullWindow("WBSInfo.aspx?WBSCode="+WBSCode+"&ProjectCode=<%=Request["ProjectCode"]%>","");
	}
	
	function DoBodyLoad()
	{
		var objFrame = document.all("TreeSplitTop");
		objFrame.src = "WBSTree.aspx?ProjectCode=<%=Request["ProjectCode"]%>&ParentCode=<%=Request["ParentCode"]%>";
	}
	
	function SelectTask(Condition)
	{
		var objFrame = document.all("TreeSplitTop");
		objFrame.src = "WBSTree.aspx?" + Condition;	
	}
	
	function InputWBS()
	{
		OpenSmallWindow( 'WBSTempletIn.aspx?ProjectCode=<%=Request["ProjectCode"]%>',"模板导入" );
	}
			
	function OutputWBS ()
	{
		OpenSmallWindow( 'WBSTempletOut.aspx?ProjectCode=<%=Request["ProjectCode"]%>',"模板导出" );
	}
	function InputExcelWBS()
	{
		OpenSmallWindow( 'WBSExcelIn.aspx?ProjectCode=<%=Request["ProjectCode"]%>',"模板导入" );
	}
			
	function OutputExcelWBS ()
	{
    	return window.open('WBSExcelOut.aspx?ProjectCode=<%=Request["ProjectCode"]%>',"模板导出","width=400,height=300,fullscreen=0,top="+(window.screen.height-300)/2+",left="+(window.screen.width-400)/2+",menubar=yes,toolbar=no,scrollbars=yes,status=no,titlebar=no,resizable=yes,location=no");
//		OpenSmallWindow( '../Project/WBSExcelOut.aspx?ProjectCode=<%=Request["ProjectCode"]%>',"模板导出" );
	}
	function SelModify()
	{
		document.all("TreeSplitTop").contentWindow.SelectModifyCode();		
	}
		</SCRIPT>
</HEAD>
	<body scroll="no" onload="DoBodyLoad();">
		<form id="Form1" runat="server">
			<table width="100%" cellSpacing="0" cellPadding="0"  border="0" height="100%">
				<tr>
					<td bgColor="#e4eff6" height="6"></td>
				</tr>
				<tr>
					<td height="25">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="topic" background="../images/topic_bg.gif"><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle">项目管理>工作进度>进度计划</td>
								<td width="9"><IMG height="25" src="../images/topic_corr.gif" width="9"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td>
						<table cellSpacing="0" cellPadding="0" width="100%" border="0" id=tbWBSAction runat=server>
							<tr>
								<td  class="tools-area">
									<IMG src="../images/btn_li.gif" align="absMiddle"> 
									<span id="spanIOButton" runat="server">
									<input class="button" id="btInput" onclick="InputWBS();return false;" type="button" value="WBS导入">
									<input class="button" id="btOutput" onclick="OutputWBS();return false;" type="button" value="WBS导出">
									<input class="button" id="btExcelInput" onclick="InputExcelWBS();return false;" type="button" value="Excel导入">
									<input class="button" id="btExcelOutput" onclick="OutputExcelWBS();return false;" type="button" value="Excel导出">
									</span>
									&nbsp;&nbsp;&nbsp;&nbsp;
									<img src="../Images/icon_unbegin.gif" >未开始
									<img src="../Images/icon_going.gif">进行中
									<img src="../Images/icon_pause.gif" >已暂停
									<img src="../Images/icon_cancel.gif" >已取消
									<img src="../Images/icon_over.gif">已完成
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr height="100%">
					<td class="table">
						<IFRAME id="TreeSplitTop" src="../Cost/LoadingPrepare.htm" frameBorder="no" width="100%"
							scrolling="no" height="100%"></IFRAME>
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
					<td bgColor="#e4eff6" height="6"></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
