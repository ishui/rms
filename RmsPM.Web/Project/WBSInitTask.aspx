<%@ Register TagPrefix="cc2" Namespace="RmsPM.WebControls.ToolsBar" Assembly="RmsPM.WebControls" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Page language="c#" CodeFile="WBSInitTask.aspx.cs" AutoEventWireup="false" Inherits="RmsPM.Web.Project.WBSInitTask" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>�޸Ĺ�����Ϣ</title>
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
								<td class="topic" align="center" background="../images/topic_bg.gif" height="25"><FONT face="����">���������ʼ��</FONT></td>
							</tr>
						</TABLE>
						<TABLE class="form" id="Table4" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR width="100%">
								<TD class="form-item" align="right" width="20%">�������ƣ�</TD>
								<TD class="tdBlank" id="tdTaskName" width="30%" runat="server">
									<asp:Label id="Label1" runat="server">���Ŵ�¥</asp:Label></TD>
								<TD class="form-item" align="right" width="20%"><FONT face="����">�����ţ�</FONT></TD>
								<TD class="tdBlank" id="tdTaskCode" runat="server"><FONT face="����"><asp:textbox id="txtTaskCode" runat="server" CssClass="input" Width="120px">ȡ�������ı��</asp:textbox></FONT></TD>
							</TR>
							<TR width="100%">
								<TD class="form-item" align="right">����״̬��</TD>
								<TD class="tdBlank" id="tdTaskStatus" runat="server"><select class="select" id="lstTaskStatus" style="WIDTH: 120px" onchange="ShowReason(this);return false;"
										name="lstTaskStatus" runat="server">
										<option value="" selected>������ѡ�񣭣�</option>
										<option value="0">δ��ʼ</option>
										<option value="1">������</option>
										<option value="2">��ͣ</option>
										<option value="3">ȡ��</option>
										<option value="4">�����</option>
									</select></TD>
								<TD class="form-item" align="right">��Ҫ�̶ȣ�</TD>
								<TD class="tdBlank" id="tdImportantLevel" runat="server"><select class="select" id="lstImportantLevel" style="WIDTH: 120px" name="lstImportantLevel"
										runat="server">
										<option value="" selected>������ѡ�񣭣�</option>
										<option value="0">һ��</option>
										<option value="1">��Ҫ</option>
									</select></TD>
							</TR>
							<TR width="100%">
								<TD class="form-item" align="right">Ԥ�ƿ�ʼʱ�䣺</TD>
								<TD class="tdBlank" id="tdPlanStartDate" runat="server"><cc3:calendar id="dtbPlannedStartDate" runat="server" CalendarResource="../Images/CalendarResource/"
										ReadOnly="False" Display="True"></cc3:calendar></TD>
								<TD class="form-item" align="right">Ԥ�ƽ���ʱ�䣺</TD>
								<TD class="tdBlank" id="tdPlanFinishDate" runat="server"><cc3:calendar id="dtbPlannedFinishDate" runat="server" CalendarResource="../Images/CalendarResource/"
										ReadOnly="False" Display="True"></cc3:calendar></TD>
							</TR>
							<TR width="100%">
								<TD class="form-item" align="right">����������</TD>
								<TD class="tdBlank" id="tdTaskDesc" colSpan="3" runat="server"><textarea class="textarea" id="taTaskDesc" style="HEIGHT: 40px" name="taTaskDesc" cols="47"
										runat="server">�������ڵ㣬������,�������׼ģ�壬����ģ��</textarea></TD>
							</TR>
							<TR id="trTemplate" runat="server" width="100%">
								<TD class="form-item" align="right">ģ�������</TD>
								<TD class="tdBlank" colSpan="3"><input class="button" id="btInput" onclick="InputWBS();return false;" type="button" value="ģ�嵼��">
									<input class="button" id="btOutput" onclick="OutputWBS();return false;" type="button" value="ģ�嵼��">
								</TD>
							</TR>
						</TABLE>
						<br>
						<table cellSpacing="20" align="center">
							<tr vAlign="middle" align="center" width="100%">
								<td><input class="submit" id="btSubmit" type="button" value="ȷ ��" name="btSubmit" runat="server">&nbsp;&nbsp;<input class="submit" id="btCancel" onclick="doCancel();return false;" type="button" value="ȡ ��"
										name="btCancel" runat="server"></td>
							</tr>
						</table>
						<P>&nbsp;</P>
						<P><FONT face="����"></FONT>&nbsp;</P>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
