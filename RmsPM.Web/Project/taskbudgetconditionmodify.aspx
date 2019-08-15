<%@ Page language="c#" Inherits="RmsPM.Web.Project.TaskBudgetConditionModify" CodeFile="TaskBudgetConditionModify.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>工作预算付款条件</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
	</HEAD>
	<body scroll="no" onload="winload();">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" bgColor="#ffffff" border="0">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">工作预算付款条件</td>
				</tr>
				<tr height="80%">
					<td class="topic" vAlign="top" align="center">
						<table class="form" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD class="form-item" noWrap width="100">工 作 项：</TD>
								<TD><A href="#" onclick="OpenTask();return false;"><span id="spanTaskName" runat="server"></span></A><input id="txtTaskName" type="hidden" name="txtTaskName" runat="server">
									<input id="txtWBSCode" type="hidden" name="txtWBSCode" runat="server"> <A href="#" onclick="SelectTask();return false;">
										<IMG src="../images/ToolsItemSearch.gif" border="0"></A> <font color="red">*</font>
								</TD>
							</TR>
							<tr>
								<TD class="form-item">完成百分比：</TD>
								<td><input class="input-nember" id="txtCompletePercent" type="text" size="3" name="txtCompletePercent"
										runat="server">%</td>
							</tr>
							<TR>
								<TD class="form-item">时间：</TD>
								<td><select class="select" id="sltDelayType" onchange="sltDelayTypeChange();" name="sltDelayType"
										runat="server">
										<option value="0" selected>--请选择--</option>
										<option value="-1">提前</option>
										<option value="1">延后</option>
									</select>
									<span id="spanDelayDays"><input class="input-nember" id="txtDelayDays" type="text" size="3" name="txtDelayDays"
											runat="server">天 <font color="red">*</font> </span>
								</td>
							</TR>
						</table>
					</td>
				</tr>
				<tr>
					<td>
						<table cellSpacing="10" width="100%">
							<TR>
								<TD align="center"><asp:button id="btnSave" runat="server" CssClass="submit" Text="确 定" onclick="btnSave_Click"></asp:button>&nbsp;
									<input class="submit" id="btnDelete" onclick="if (!Delete()) return false;" type="button"
										value="删 除" name="btnDelete" runat="server" onserverclick="btnDelete_ServerClick">&nbsp; <INPUT class="submit" onclick="window.close();" type="button" value="取 消"></TD>
							</TR>
						</table>
					</td>
				</tr>
			</table>
			<input id="txtTaskBudgetCode" type="hidden" name="txtTaskBudgetCode" runat="server">
			<input id="txtInputWBSCode" type="hidden" name="txtInputWBSCode" runat="server">
			<input id="txtTaskBudgetConditionCode" type="hidden" name="txtTaskBudgetConditionCode"
				runat="server">
			<SCRIPT language="javascript">
<!--

//选择工作项
function SelectTask()
{
	OpenMiddleWindow("../SelectBox/SelectTask.aspx?Flag=1&WBSCode=<%=Request["InputWBSCode"]%>&ProjectCode=<%=Request["ProjectCode"]%>");
}

//选择工作项
function SelectTaskReturn(code, name)
{
	Form1.txtWBSCode.value = code;
	Form1.txtTaskName.value = name;
	document.all.spanTaskName.innerText = name;
}

function winload()
{
	sltDelayTypeChange();
}

function sltDelayTypeChange()
{
	var type = Form1.sltDelayType.value;
	
	if (type == "0")
	{
		document.all.spanDelayDays.style.display = "none";
	}
	else
	{
		//提前或延后
		document.all.spanDelayDays.style.display = "";
	}
}

//显示工作信息
function OpenTask()
{
	var WBSCode = Form1.txtWBSCode.value;
	OpenFullWindow("WBSInfo.aspx?WBSCode="+WBSCode,"");
}

function Delete()
{
	if (!confirm("确实要删除吗？")) return false;
	
	return true;
}

//-->
			</SCRIPT>
		</form>
	</body>
</HTML>
