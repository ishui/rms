<%@ Register TagPrefix="cc2" Namespace="RmsPM.WebControls.ToolsBar" Assembly="RmsPM.WebControls" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Page language="c#" CodeFile="WBSInitTask.aspx.cs" AutoEventWireup="false" Inherits="RmsPM.Web.Project.WBSInitTask" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>修改工作信息</title>
		<META http-equiv="Content-Type" content="text/html; charset=gb2312">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript">
			function doCancel(){
				window.close();
			}
			
			function ShowReason(obj)
			{
			/*
				var tmp = '<%=strState%>';
				if(tmp=="Insert")
				{
					document.all.trActual.style.display = (obj.selectedIndex > 1)?"":"none";
					document.all.tdActualStartDate.style.display = (obj.selectedIndex > 1)?"":"none";
					document.all.tdActualStartTemp.style.display = (obj.selectedIndex > 1)?"none":"";
					document.all.tdActualFinishDate.style.display = (obj.selectedIndex == 5)?"":"none";
					document.all.tdActualFinishTemp.style.display = (obj.selectedIndex == 5)?"none":"";
					document.all.trPause.style.display = (obj.selectedIndex == 3)?"":"none";
					document.all.trCancel.style.display = (obj.selectedIndex == 4)?"":"none";
				}
				if (document.all.tdActualStartDate.style.display == "none")
				{
					document.all.dtbActualStartDate.value = "";
				}
				else
				{
					document.all.dtbActualStartDate.value = document.all.dtbActualStartDate_year.value + "-";
					document.all.dtbActualStartDate.value += document.all.dtbActualStartDate_month.value + "-"; 
					document.all.dtbActualStartDate.value += document.all.dtbActualStartDate_day.value + " 00:00:00"; 
				}
				if (document.all.tdActualFinishDate.style.display == "none")
				{
					document.all.dtbActualFinishDate.value = "";
				}
				else
				{
					document.all.dtbActualFinishDate.value = document.all.dtbActualFinishDate_year.value + "-";
					document.all.dtbActualFinishDate.value += document.all.dtbActualFinishDate_month.value + "-"; 
					document.all.dtbActualFinishDate.value += document.all.dtbActualFinishDate_day.value + " 00:00:00"; 
				}
				document.all.dtbActualStartDate.value = (document.all.tdActualStartDate.style.display == "none")?"":document.all.dtbActualStartDate.value;
				document.all.dtbActualFinishDate.value = (document.all.tdActualFinishDate.style.display == "none")?"":document.all.dtbActualFinishDate.value;
				document.all.taPauseReason.innerText = (document.all.trPause.style.display == "none")?"":document.all.taPauseReason.innerText;
				document.all.taCancelReason.innerText = (document.all.trCancel.style.display == "none")?"":document.all.taCancelReason.innerText;
				*/
			}
			
			function BodyLoad()
			{	
				ShowReason(document.all.lstTaskStatus);
			}
			
			function InputWBS()
			{
				OpenSmallWindow("WBSTempletIn.aspx","" );
			}
			
			function OutputWBS ()
			{
				OpenSmallWindow("WBSTempletOut.aspx","" );
			}
		</SCRIPT>
	</HEAD>
	<body bottomMargin="0" bgColor="white" leftMargin="0" topMargin="0" scroll="no" onload="BodyLoad();"
		rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" bgColor="white" border="0">
				<tr vAlign="top">
					<td>
						<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="topic" align="center" background="../images/topic_bg.gif" height="25"><FONT face="宋体">工作任务初始化</FONT></td>
							</tr>
						</TABLE>
						<TABLE class="form" id="Table4" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR width="100%">
								<TD class="form-item" align="right" width="20%">任务名称：</TD>
								<TD class="tdBlank" id="tdTaskName" width="30%" runat="server">
									<asp:Label id="Label1" runat="server">电信大楼</asp:Label></TD>
								<TD class="form-item" align="right" width="20%"><FONT face="宋体">任务编号：</FONT></TD>
								<TD class="tdBlank" id="tdTaskCode" runat="server"><FONT face="宋体"><asp:textbox id="txtTaskCode" runat="server" CssClass="input" Width="120px">取得新增的编号</asp:textbox></FONT></TD>
							</TR>
							<TR width="100%">
								<TD class="form-item" align="right">工作状态：</TD>
								<TD class="tdBlank" id="tdTaskStatus" runat="server"><select class="select" id="lstTaskStatus" style="WIDTH: 120px" onchange="ShowReason(this);return false;"
										name="lstTaskStatus" runat="server">
										<option value="" selected>－－请选择－－</option>
										<option value="0">未开始</option>
										<option value="1">进行中</option>
										<option value="2">暂停</option>
										<option value="3">取消</option>
										<option value="4">已完成</option>
									</select></TD>
								<TD class="form-item" align="right">重要程度：</TD>
								<TD class="tdBlank" id="tdImportantLevel" runat="server"><select class="select" id="lstImportantLevel" style="WIDTH: 120px" name="lstImportantLevel"
										runat="server">
										<option value="" selected>－－请选择－－</option>
										<option value="0">一般</option>
										<option value="1">重要</option>
									</select></TD>
							</TR>
							<TR width="100%">
								<TD class="form-item" align="right">预计开始时间：</TD>
								<TD class="tdBlank" id="tdPlanStartDate" runat="server"><cc3:calendar id="dtbPlannedStartDate" runat="server" CalendarResource="../Images/CalendarResource/"
										ReadOnly="False" Display="True"></cc3:calendar></TD>
								<TD class="form-item" align="right">预计结束时间：</TD>
								<TD class="tdBlank" id="tdPlanFinishDate" runat="server"><cc3:calendar id="dtbPlannedFinishDate" runat="server" CalendarResource="../Images/CalendarResource/"
										ReadOnly="False" Display="True"></cc3:calendar></TD>
							</TR>
							<TR width="100%">
								<TD class="form-item" align="right">工作描述：</TD>
								<TD class="tdBlank" id="tdTaskDesc" colSpan="3" runat="server"><textarea class="textarea" id="taTaskDesc" style="HEIGHT: 40px" name="taTaskDesc" cols="47"
										runat="server">新增根节点，负责人,可引入标准模板，其他模板</textarea></TD>
							</TR>
							<TR id="trTemplate" runat="server" width="100%">
								<TD class="form-item" align="right">模板操作：</TD>
								<TD class="tdBlank" colSpan="3"><input class="button" id="btInput" onclick="InputWBS();return false;" type="button" value="模板导入">
									<input class="button" id="btOutput" onclick="OutputWBS();return false;" type="button" value="模板导出">
								</TD>
							</TR>
						</TABLE>
						<br>
						<table cellSpacing="20" align="center">
							<tr vAlign="middle" align="center" width="100%">
								<td><input class="submit" id="btSubmit" type="button" value="确 定" name="btSubmit" runat="server">&nbsp;&nbsp;<input class="submit" id="btCancel" onclick="doCancel();return false;" type="button" value="取 消"
										name="btCancel" runat="server"></td>
							</tr>
						</table>
						<P>&nbsp;</P>
						<P><FONT face="宋体"></FONT>&nbsp;</P>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
