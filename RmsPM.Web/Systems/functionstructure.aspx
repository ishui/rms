<%@ Page language="c#" Inherits="RmsPM.Web.Systems.FunctionStructure" CodeFile="FunctionStructure.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>功能结构</title>
		<META http-equiv="Content-Type" content="text/html; charset=gb2312">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Images/SplitPage.js"></SCRIPT>
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		
<SCRIPT language="javascript">

	// 当前选中的功能点编号
	var currentCode = "";

	
	function InsertFunctionStructure()
	{
		window.open("FunctionStructureModify.aspx?Action=AddChild&FunctionStructureCode=");
	}
	
	// 选择一个功能点,显示明细信息
	function SelectFunctionStructure(functionStructureCode,functionStrucureName)
	{
		currentCode = functionStructureCode;
		window.open("FunctionStructureModify.aspx?Action=Modify&FunctionStructureCode="+currentCode);
	}

	function DoBodyLoad()
	{
		var objFrame = document.all("TreeSplitTop");
		objFrame.src = "../Systems/FunctionStructureTree.aspx";
	}
	
</SCRIPT>
	</HEAD>
	<body scroll="no" onload="DoBodyLoad();" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td bgColor="#e4eff6" height="6"></td>
				</tr>
				<tr>
					<td height="25">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0" background="../images/topic_bg.gif">
							<tr>
								<td class="topic" background="../images/topic_bg.gif" width="100%"><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle">
									系统管理 - 功能结构定义
								</td>
								<td><IMG height="25" src="../images/topic_corr.gif"></td>
							</tr>
						</table>
					</td>
				</tr>
				<TR>
					<td class="tools-area" vAlign="top">
						<IMG src="../images/btn_li.gif" align="absMiddle"> <input class="button" id="btnAddChild" onclick="InsertFunctionStructure(); return false;" type="button"
										value="新增功能点" name="btnCancel" runat="server">
							</td>
				</tr>
				<tr height="100%">
					<td class="table">
						<TABLE height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD vAlign="top" align="left"><iframe id="TreeSplitTop" src="../Cost/LoadingPrepare.htm" frameBorder="no" width="100%"
										scrolling="auto" height="100%"></iframe>
								</TD>
							</TR>
						</TABLE>
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
