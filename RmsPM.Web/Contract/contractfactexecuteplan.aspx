<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Page language="c#" Inherits="RmsPM.Web.Contract.ContractFactExecutePlan" CodeFile="ContractFactExecutePlan.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>合同执行报告</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" bgColor="white" border="0">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">合同执行报告</td>
				</tr>
				<tr>
					<td class="topic" vAlign="top" align="center">
						<table class="form" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD class="form-item">工作：</TD>
								<TD colspan="3">
									<asp:Label id="lblExecuteDetail" runat="server"></asp:Label></TD>
							</TR>
							<TR>
								<TD class="form-item">计划执行人：</TD>
								<TD>
									<asp:Label id="LabelsltPerson" runat="server"></asp:Label></TD>
								<TD class="form-item">计划执行时间：</TD>
								<TD>
									<asp:Label id="LabeldtbExecuteDate" runat="server"></asp:Label></TD>
							</TR>
							<TR>
								<TD class="form-item">实际执行人：</TD>
								<TD><input class="input-lineonly" id="txtFactExecutorName" readOnly type="text" runat="server">
									<input id="txtFactExecutor" readOnly type="hidden" runat="server"> <A href="#" onclick="SelectContractPerson();return false;">
										<IMG src="../images/ToolsItemSearch.gif" border="0"></A> <font color="red">*</font>
								</TD>
								<TD class="form-item">实际执行时间：</TD>
								<TD>
									<cc3:calendar id="FactExecuteDate" runat="server" Display="True" ReadOnly="False" CalendarResource="../Images/CalendarResource/"></cc3:calendar>
									<font color="red">*</font></TD>
							</TR>
							<TR>
								<TD class="form-item">执行情况：</TD>
								<TD colspan="3">
									<asp:textbox id="txtDescription" runat="server" CssClass="textarea" Width="90%" TextMode="MultiLine"
										Rows="4"></asp:textbox>
								</TD>
							</TR>
						</table>
						<table cellSpacing="10" width="100%">
							<tr>
								<td align="center"><input class="submit" id="btnSave" type="button" value="确 定" name="btnSave" runat="server" onserverclick="btnSave_ServerClick">&nbsp;&nbsp;
									<input class="submit" id="btnCancel" onclick="javascript:self.close()" type="button" value="取 消"
										name="btnCancel">
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
		<script language="javascript">
	<!--
		function DoSelectUser(userCode,userName,flag)
		{
			Form1.txtFactExecutor.value = userCode;
			Form1.txtFactExecutorName.value = userName;
		}

		function SelectContractPerson()
		{
			OpenMiddleWindow('../SelectBox/SelectPerson.aspx?Flag=0&Type=Single&ProjectCode=<%=Request["ProjectCode"]%>','选择用户');
		}
	//-->
		</script>
	</body>
</HTML>
