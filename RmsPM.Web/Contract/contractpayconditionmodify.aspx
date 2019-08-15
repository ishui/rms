<%@ Page language="c#" Inherits="RmsPM.Web.Contract.ContractPayConditionModify" CodeFile="ContractPayConditionModify.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>合同付款条件</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
	</HEAD>
	<body scroll="no" onload="winload();">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0" bgcolor="#ffffff">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">合同付款条件</td>
				</tr>
				<tr height="80%">
					<td class="topic" vAlign="top" align="center">
						<table cellpadding="0" cellspacing="0" border="0" width="100%" class="form">
							<TR>
								<TD class="form-item" noWrap width="100">工 作 项：</TD>
								<TD>
									<a href="#" onclick="OpenTask();return false;"><span id="spanTaskName" runat="server"></span></a>
									<input type="hidden" id="txtTaskName" name="txtTaskName" runat="server"> <input type="hidden" id="txtWBSCode" name="txtWBSCode" runat="server">
									<A href="#" onclick="SelectTask();return false;"><IMG src="../images/ToolsItemSearch.gif" border="0"></A>
									<font color="red">*</font>
								</TD>
							</TR>
							<tr>
								<TD class="form-item">完成百分比：</TD>
								<td><input type="text" class="input-nember" size="3" id="txtCompletePercent" name="txtCompletePercent"
										runat="server">%</td>
							</tr>
							<TR>
								<TD class="form-item">时间：</TD>
								<td><select runat="server" id="sltDelayType" name="sltDelayType" class="select" onchange="sltDelayTypeChange();">
										<option value="0" selected>--请选择--</option>
										<option value="-1">提前</option>
										<option value="1">延后</option>
									</select>
									<span id="spanDelayDays"><input type="text" runat="server" id="txtDelayDays" name="txtDelayDays" size="3" class="input-nember">天
										<font color="red">*</font> </span>
								</td>
							</TR>
						</table>
					</td>
				</tr>
				<tr>
					<td>
						<table cellSpacing="10" width="100%">
							<TR>
								<TD align="center"><asp:button id="btnSave" runat="server" Text="确 定" CssClass="submit" onclick="btnSave_Click"></asp:button>&nbsp;
									<input type="button" class="submit" id="btnDelete" name="btnDelete" runat="server" value="删 除"
										onclick="if (!Delete()) return false;" onserverclick="btnDelete_ServerClick">&nbsp; <INPUT class="submit" onclick="window.close();" type="button" value="取 消"></TD>
							</TR>
						</table>
					</td>
				</tr>
			</table>
			<input id="txtAllocateCode" type="hidden" name="txtAllocateCode" runat="server">
			<input id="txtContractCode" type="hidden" name="txtContractCode" runat="server"><input id="txtConditionCode" type="hidden" name="txtConditionCode" runat="server">
			<SCRIPT language="javascript">
<!--

//选择工作项
function SelectTask()
{
	OpenMiddleWindow("../SelectBox/SelectTask.aspx?ProjectCode=<%=Request["ProjectCode"]%>&Flag=1&WBSCode=");
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
	OpenFullWindow("../Project/WBSInfo.aspx?WBSCode="+WBSCode,"");
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
