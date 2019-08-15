<%@ Page language="c#" Inherits="RmsPM.Web.Project.TaskBudgetConditionModify" CodeFile="TaskBudgetConditionModify.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>����Ԥ�㸶������</title>
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
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">����Ԥ�㸶������</td>
				</tr>
				<tr height="80%">
					<td class="topic" vAlign="top" align="center">
						<table class="form" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD class="form-item" noWrap width="100">�� �� �</TD>
								<TD><A href="#" onclick="OpenTask();return false;"><span id="spanTaskName" runat="server"></span></A><input id="txtTaskName" type="hidden" name="txtTaskName" runat="server">
									<input id="txtWBSCode" type="hidden" name="txtWBSCode" runat="server"> <A href="#" onclick="SelectTask();return false;">
										<IMG src="../images/ToolsItemSearch.gif" border="0"></A> <font color="red">*</font>
								</TD>
							</TR>
							<tr>
								<TD class="form-item">��ɰٷֱȣ�</TD>
								<td><input class="input-nember" id="txtCompletePercent" type="text" size="3" name="txtCompletePercent"
										runat="server">%</td>
							</tr>
							<TR>
								<TD class="form-item">ʱ�䣺</TD>
								<td><select class="select" id="sltDelayType" onchange="sltDelayTypeChange();" name="sltDelayType"
										runat="server">
										<option value="0" selected>--��ѡ��--</option>
										<option value="-1">��ǰ</option>
										<option value="1">�Ӻ�</option>
									</select>
									<span id="spanDelayDays"><input class="input-nember" id="txtDelayDays" type="text" size="3" name="txtDelayDays"
											runat="server">�� <font color="red">*</font> </span>
								</td>
							</TR>
						</table>
					</td>
				</tr>
				<tr>
					<td>
						<table cellSpacing="10" width="100%">
							<TR>
								<TD align="center"><asp:button id="btnSave" runat="server" CssClass="submit" Text="ȷ ��" onclick="btnSave_Click"></asp:button>&nbsp;
									<input class="submit" id="btnDelete" onclick="if (!Delete()) return false;" type="button"
										value="ɾ ��" name="btnDelete" runat="server" onserverclick="btnDelete_ServerClick">&nbsp; <INPUT class="submit" onclick="window.close();" type="button" value="ȡ ��"></TD>
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

//ѡ������
function SelectTask()
{
	OpenMiddleWindow("../SelectBox/SelectTask.aspx?Flag=1&WBSCode=<%=Request["InputWBSCode"]%>&ProjectCode=<%=Request["ProjectCode"]%>");
}

//ѡ������
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
		//��ǰ���Ӻ�
		document.all.spanDelayDays.style.display = "";
	}
}

//��ʾ������Ϣ
function OpenTask()
{
	var WBSCode = Form1.txtWBSCode.value;
	OpenFullWindow("WBSInfo.aspx?WBSCode="+WBSCode,"");
}

function Delete()
{
	if (!confirm("ȷʵҪɾ����")) return false;
	
	return true;
}

//-->
			</SCRIPT>
		</form>
	</body>
</HTML>
