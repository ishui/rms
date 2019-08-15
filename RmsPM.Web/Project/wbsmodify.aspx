<%@ Reference Page="~/project/wbs.aspx" %>
<%@ Reference Page="~/project/wbsstatus.aspx" %>
<%@ Register TagPrefix="uc1" TagName="InputUnit" Src="../UserControls/InputUnit.ascx" %>
<%@ Page language="c#" Inherits="RmsPM.Web.Project.WBSModify" validateRequest=false CodeFile="WBSModify.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="InputTask" Src="../UserControls/InputTask.ascx" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Register TagPrefix="cc2" Namespace="RmsPM.WebControls.ToolsBar" Assembly="RmsPM.WebControls" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>������Ϣ</title>
		<META http-equiv="Content-Type" content="text/html; charset=gb2312">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<SCRIPT language="javascript">
			function doCancel(){
				window.close();
			}
			
			function ShowReason(obj)
			{
				document.all.trEarly.style.display = (obj.selectedIndex > 0)?"":"none";
				document.all.trActual.style.display = (obj.selectedIndex > 0)?"":"none";
				document.all.tdActualStartDate.style.display = (obj.selectedIndex > 0)?"":"none";
				document.all.tdActualStartTemp.style.display = (obj.selectedIndex > 0)?"none":"";
				document.all.tdActualFinishDate.style.display = (obj.selectedIndex == 4)?"":"none";
				document.all.tdActualFinishTemp.style.display = (obj.selectedIndex == 4)?"none":"";
				document.all.trPause.style.display = (obj.selectedIndex == 2)?"":"none";
				document.all.trCancel.style.display = (obj.selectedIndex == 3)?"":"none";
				
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
			}
			
			function BodyLoad()
			{	
				ShowReason(document.all.lstTaskStatus);
				
				var tmp0 = '<%=Request["hSelect0"]%>';
				var tmp1 = '<%=Request["hSelect1"]%>';
				var tmp2 = '<%=Request["hSelect2"]%>';
				var tmp9 = '<%=Request["hSelect9"]%>';
				
				if(tmp0.length>0)
					window.document.all.SelectName0.innerText = '<%=Request["hSelect0"]%>';
				window.document.all.hSelect0.value = '<%=Request["hSelect0"]%>';

				if(tmp1.length>0)
					window.document.all.SelectName1.innerText = '<%=Request["hSelect1"]%>';
				window.document.all.hSelect1.value = '<%=Request["hSelect1"]%>';

				if(tmp2.length>0)
					window.document.all.SelectName2.innerText = '<%=Request["hSelect2"]%>';
				window.document.all.hSelect2.value = '<%=Request["hSelect2"]%>';
				
				if(tmp9.length>0)
					window.document.all.SelectName9.innerText = '<%=Request["hSelect9"]%>';
				window.document.all.hSelect9.value = '<%=Request["hSelect9"]%>';
				
				if(window.document.all.SelectName9.innerText.length>0)
					window.document.all.hSelect9.value = window.document.all.SelectName9.innerText;
					
				//alert(<%=Request["ProjectCode"]%>);
			}
			
			function ClientValidate(source, arguments)����
			{ 								
				var strDay = arguments.Value;
				var number_chars = "1234567890";
				var i;
				for (i=0;i<strDay.length;i++)
				{
					if (number_chars.indexOf(strDay.charAt(i))==-1) 
					{
						arguments.IsValid = false;
						return;
					}					
				}
				arguments.IsValid = true;
����		}
����		
����				
			function SelectPerson(type)
����		{
����			if(type==9) //����
����			{
����				OpenMiddleWindow("../SelectBox/SelectSUMain.aspx?UserCodes="+window.document.all.txtMaster.value+"&StationCodes="+window.document.all.txtMasterStations.value+"&Flag="+type,null);
����			}����			
����			if(type==2) //¼��
����			{
����				OpenMiddleWindow("../SelectBox/SelectSUMain.aspx?UserCodes="+window.document.all.txtInputor.value+"&StationCodes="+window.document.all.txtInputorStations.value+"&Flag="+type,null);
����			}
����			if(type==1) //�ල
����			{
����				OpenMiddleWindow("../SelectBox/SelectSUMain.aspx?UserCodes="+window.document.all.txtMonitor.value+"&StationCodes="+window.document.all.txtMonitorStations.value+"&Flag="+type,null);
����			}
����			if(type==0) //����
����			{
����				OpenMiddleWindow("../SelectBox/SelectSUMain.aspx?UserCodes="+window.document.all.txtExecuter.value+"&StationCodes="+window.document.all.txtExecuterStations.value+"&Flag="+type,null);
����			}
����		}
			
			function getReturnStationUser(userCodes,userNames,stationCodes,stationNames,flag)
			{
				if(flag==9) //����
				{
					window.document.all.txtMaster.value = userCodes;	
					window.document.all.txtMasterStations.value = stationCodes;	
					window.document.all.SelectName9.innerText = getString(userNames,stationNames);
					window.document.all.hSelect9.value = getString(userNames,stationNames);
				}
				if(flag==2) //����
				{
					window.document.all.txtInputor.value = userCodes;	
					window.document.all.txtInputorStations.value = stationCodes;	
					window.document.all.SelectName2.innerText = getString(userNames,stationNames);
					window.document.all.hSelect2.value = getString(userNames,stationNames);
				}
				if(flag==1) //�ල
				{
					window.document.all.txtMonitor.value = userCodes;	
					window.document.all.txtMonitorStations.value = stationCodes;	
					window.document.all.SelectName1.innerText = getString(userNames,stationNames);
					window.document.all.hSelect1.value = getString(userNames,stationNames);
				}
				if(flag==0) //����
				{
					window.document.all.txtExecuter.value = userCodes;
					window.document.all.txtExecuterStations.value = stationCodes;	
					window.document.all.SelectName0.innerText = getString(userNames,stationNames);
					window.document.all.hSelect0.value = getString(userNames,stationNames);
				}
			}
			function getString(str1,str2)
			{
				if(str1.length>0&&str2.length>0)
				{
					return str1+','+str2;
				}
				else
					return str1+str2;
			}

			
			function DoSelectUser(userCode,userName,flag)
			{
				if(flag==9) //����
				{
					window.document.all.txtMaster.value = userCode;	
					window.document.all.SelectName9.innerText = userName;
					window.document.all.hSelect9.value = userName;
				}
				if(flag==2) //¼��
				{
					window.document.all.txtInputor.value = userCode;	
					window.document.all.SelectName2.innerText = userName;
					window.document.all.hSelect2.value = userName;
				}
				if(flag==1) //�ල
				{
					window.document.all.txtMonitor.value = userCode;	
					window.document.all.SelectName1.innerText = userName;
					window.document.all.hSelect1.value = userName;
				}
				if(flag==0) //����
				{
					window.document.all.txtExecuter.value = userCode;
					window.document.all.SelectName0.innerText = userName;
					window.document.all.hSelect0.value = userName;
				}
			}
//alert(window.document.all.txtMaster.value);
			
			function doCheck()
			{
//alert(window.document.all.txtMaster.value);			
				if(window.document.all.txtTaskName.value.length<1)
				{
					alert("����������������");
					return false;
				}
/*				if(window.document.all.txtMaster.value.length<1)
				{
					alert("���������������");
					return false;
				}	*/
				if(document.all.txtCompletePercent.value!='100')
				{
				//	if(document.all.txtCompletePercent.length>2
				//	{
				//		alert("���Ȳ���");
				//		return false;
				//	}					
				}	
				return true;
			}
����		
//ѡ����������
function SelectRelaType(i)
{
	var RelaType = Form1.txtRelaType.value;
	var RelaCode = Form1.txtRelaCode.value;;
	OpenCustomWindow("../SelectBox/SelectTaskRelaType.aspx?ProjectCode=" + Form1.txtProjectCode.value + "&RelaType=" + RelaType + "&SelectCode=" + escape(RelaCode) + "&ReturnFunc=SelectRelaTypeReturn","ѡ����������", 400, 540);
}

//ѡ���������ͷ���
function SelectRelaTypeReturn(RelaType, code, name, RelaName)
{
	Form1.txtRelaType.value = RelaType;
	Form1.txtRelaCode.value = code;
	Form1.txtRelaName.value = RelaName;
	document.all.spanRelaName.innerText = RelaName;

/*	
	if (name != "")
	{
		//ȱʡ����������
		Form1.txtTaskName.value = name;
	}
*/
}
/*
function SelectTask()
{
	OpenMiddleWindow("../SelectBox/SelectTask.aspx?Flag=2&WBSCode=&ProjectCode=<%=Request["ProjectCode"]%>");
}
//ѡ������
function SelectTaskReturn(code, name)
{
	//alert(code+"-"+name);
	document.all.txtPreTask.value = code;
	document.all.spPreTask.innerText = name;
	
}*/
function OpenTask(WBSCode) 
{
	OpenFullWindow("WBSInfo.aspx?WBSCode=" + WBSCode+"&ProjectCode=<%=Request["ProjectCode"]%>","");
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
								<td class="topic" align="center" background="../images/topic_bg.gif" height="25"><asp:label id="lblName" runat="server">Label</asp:label></td>
							</tr>
						</TABLE>
						<TABLE class="form" id="Table4" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR width="100%">
								<TD class="form-item" align="right" width="20%">������ţ�</TD>
								<TD class="tdBlank" width="30%"><asp:textbox id="txtSortID" runat="server" Width="100" CssClass="input"></asp:textbox><font color="red">*</font><asp:customvalidator id="Customvalidator2" runat="server" ErrorMessage="�����Ҫ��������" ClientValidationFunction="ClientValidate"
										ControlToValidate="txtSortID"></asp:customvalidator>
								<TD class="form-item" align="right" width="20%">�������ƣ�</TD>
								<TD class="tdBlank"><asp:textbox id="txtTaskName" runat="server" Width="200" CssClass="input"></asp:textbox><font color="red">*</font></TD>
							</TR>
							<TR width="100%">
								<TD class="form-item" align="right" width="20%">�������</TD>
								<TD class="tdBlank" width="30%"><asp:label id="lblFather" runat="server"></asp:label></TD>
								<TD class="form-item" align="right" width="20%">ǰ�ù����</TD>
								<TD class="tdBlank" width="30%"><uc1:inputtask id="ucTask" runat="server"></uc1:inputtask></TD>
							</TR>
							<TR width="100%">
								<TD class="form-item" align="right">����״̬��</TD>
								<TD class="tdBlank"><select class="select" id="lstTaskStatus" style="WIDTH: 120px" onchange="ShowReason(this);return false;"
										name="lstTaskStatus" runat="server">
										<option value="0" selected>δ��ʼ</option>
										<option value="1">������</option>
										<option value="2">��ͣ</option>
										<option value="3">ȡ��</option>
										<option value="4">�����</option>
									</select></TD>
								<TD class="form-item" align="right">��Ҫ�̶ȣ�</TD>
								<TD class="tdBlank"><asp:radiobuttonlist id="rblImportLevel" runat="server" RepeatDirection="Horizontal">
										<asp:ListItem Value="0" Selected="True">һ��</asp:ListItem>
										<asp:ListItem Value="1">��Ҫ</asp:ListItem>
									</asp:radiobuttonlist></TD>
							</TR>
							<TR width="100%">
								<TD class="form-item" align="right" width="20%">��������(%)��</TD>
								<TD class="tdBlank"><asp:textbox id="txtCompletePercent" runat="server" Width="60" CssClass="input"></asp:textbox><asp:customvalidator id="CustomValidator" runat="server" ErrorMessage="������Ҫ��������" ClientValidationFunction="ClientValidate"
										ControlToValidate="txtCompletePercent"></asp:customvalidator></TD>
								<td class="form-item">������ţ�</td>
								<td><uc1:inputunit id="ucUnit" runat="server"></uc1:inputunit></td>
							</TR>
							<tr>
								<td class="form-item">���ͣ�</td>
								<td><span id="spanRelaName" runat="server"></span><A id="hrefSelectRelaType" title="ѡ������" onclick="SelectRelaType()" href="#" runat="server"><IMG src="../images/ToolsItemSearch.gif" border="0"></A></td>
								<td class="form-item">ͼ���ļ���</td>
								<td><input class="input" id="txtImageFileName" type="text" name="txtImageFileName" runat="server"></td>
							</tr>
							<TR width="100%">
								<TD class="form-item" align="right">�ƻ���ʼʱ�䣺</TD>
								<TD class="tdBlank"><cc3:calendar id="dtbPlannedStartDate" runat="server" CalendarResource="../Images/CalendarResource/"
										ReadOnly="False" Display="True"></cc3:calendar><font color="red">*</font></TD>
								<TD class="form-item" align="right">�ƻ�����ʱ�䣺</TD>
								<TD class="tdBlank"><cc3:calendar id="dtbPlannedFinishDate" runat="server" CalendarResource="../Images/CalendarResource/"
										ReadOnly="False" Display="True"></cc3:calendar><font color="red">*</font></TD>
							</TR>
							<TR id="trActual" width="100%">
								<TD class="form-item" id="tdActualStartTitle" align="right">ʵ�ʿ�ʼʱ�䣺</TD>
								<td class="tdBlank" id="tdActualStartTemp" style="DISPLAY: none">&nbsp;</td>
								<TD class="tdBlank" id="tdActualStartDate"><cc3:calendar id="dtbActualStartDate" runat="server" CalendarResource="../Images/CalendarResource/"
										ReadOnly="False" Display="True"></cc3:calendar></TD>
								<TD class="form-item" id="tdActualFinishTitle" align="right">ʵ�ʽ���ʱ�䣺</TD>
								<td class="tdBlank" id="tdActualFinishTemp" style="DISPLAY: none">&nbsp;</td>
								<TD class="tdBlank" id="tdActualFinishDate"><cc3:calendar id="dtbActualFinishDate" runat="server" CalendarResource="../Images/CalendarResource/"
										ReadOnly="False" Display="True"></cc3:calendar></TD>
							</TR>
							<TR id="trEarly" width="100%">
								<TD class="form-item" align="right"></TD>
								<td></td>
								<TD class="form-item" align="right">Ԥ�����ʱ�䣺</TD>
								<TD><cc3:calendar id="dtbEarlyFinishDate" runat="server" CalendarResource="../Images/CalendarResource/"
										ReadOnly="False" Display="True"></cc3:calendar></TD>
							</TR>
							<TR width="100%">
								<TD class="form-item" align="right">�����ˣ�</TD>
								<TD class="tdBlank" id="tdPreProportion" runat="server"><input id="txtMaster" type="hidden" name="txtMaster" runat="server">
									<input id="txtMasterStations" type="hidden" name="txtMasterStations" runat="server">
									<input class="button-small" id="btSelectMaster" onclick="SelectPerson(9);return false;"
										type="button" value="ѡ������">
									<div id="SelectName9" runat="server"></div>
									<input id="hSelect9" type="hidden" name="hSelect9">
								</TD>
								<td class="form-item" align="right" id="tdProportionText" runat="server">������Ȩ�أ�</td>
								<td id="tdProportionValue" runat="server"><input class="input" id="txtProportion" type="text" style="WIDTH:60px" name="txtProportion"
										runat="server"></td>
							</TR>
							<TR width="100%">
								<TD class="form-item" align="right">�ල�ˣ�</TD>
								<TD class="tdBlank" colSpan="3"><input id="txtMonitor" type="hidden" name="txtMonitor" runat="server">
									<input id="txtMonitorStations" type="hidden" name="txtMonitorStations" runat="server">
									<input class="button-small" id="btSelectMonitor" onclick="SelectPerson(1);return false;"
										type="button" value="ѡ��ල��">
									<div id="SelectName1" runat="server"></div>
									<input id="hSelect1" type="hidden" name="hSelect1">
								</TD>
							</TR>
							<TR width="100%">
								<TD class="form-item" align="right">�����ˣ�</TD>
								<TD class="tdBlank" colSpan="3"><input id="txtExecuter" type="hidden" name="txtExecuter" runat="server">
									<input id="txtExecuterStations" type="hidden" name="txtExecuterStations" runat="server">
									<input class="button-small" id="btSelectExecuter" onclick="SelectPerson(0);return false;"
										type="button" value="ѡ�������">
									<div id="SelectName0" runat="server"></div>
									<input id="hSelect0" type="hidden" name="hSelect0">
								</TD>
							</TR>
							<TR width="100%">
								<TD class="form-item" align="right">¼���ˣ�</TD>
								<TD class="tdBlank" colSpan="3"><input id="txtInputor" type="hidden" name="txtInputor" runat="server">
									<input id="txtInputorStations" type="hidden" name="txtInputorStations" runat="server">
									<input class="button-small" id="btSelectInputor" onclick="SelectPerson(2);return false;"
										type="button" value="ѡ��¼����">
									<div id="SelectName2" runat="server"></div>
									<input id="hSelect2" type="hidden" name="hSelect2">
								</TD>
							</TR>
							<TR width="100%">
								<TD class="form-item" align="right">����������</TD>
								<TD class="tdBlank" colSpan="3"><textarea class="textarea" id="taTaskDesc" style="HEIGHT: 40px" name="taTaskDesc" cols="47"
										runat="server"></textarea></TD>
							</TR>
							<TR id="trPause" style="DISPLAY: none" width="100%">
								<TD class="form-item" align="right">��ͣԭ��</TD>
								<TD class="tdBlank" colSpan="3"><textarea class="textarea" id="taPauseReason" style="HEIGHT: 40px" name="taPauseReason" cols="47"
										runat="server"></textarea>
									<br>
									<input id="chkPauseRemind" type="checkbox" CHECKED runat="server">���ѹ���������Ա
								</TD>
							</TR>
							<TR id="trCancel" style="DISPLAY: none" width="100%">
								<TD class="form-item" align="right">ȡ��ԭ��</TD>
								<TD class="tdBlank" colSpan="3"><textarea class="textarea" id="taCancelReason" style="HEIGHT: 40px" name="taCancelReason"
										cols="47" runat="server"></textarea>
									<br>
									<input id="chkCancelRemind" type="checkbox" CHECKED runat="server">���ѹ���������Ա
								</TD>
							</TR>
						</TABLE>
						<br>
						<table cellSpacing="20" align="center">
							<tr vAlign="middle" align="center" width="100%">
								<td><input class="submit" id="SaveToolsButton" onclick="if(!doCheck()) return false;" type="button"
										value="����" name="SaveToolsButton" runat="server" onserverclick="SaveToolsButton_ServerClick">&nbsp;&nbsp; <input class="submit" id="SaveToolsButtonNext" onclick="if(!doCheck()) return false;" type="button"
										value="���沢������һ��" name="SaveToolsButtonNext" runat="server" onserverclick="SaveToolsButtonNext_ServerClick">&nbsp;&nbsp; <input class="submit" id="CancelToolsButton" onclick="doCancel();return false;" type="button"
										value="ȡ��" name="CancelToolsButton" runat="server"></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<INPUT id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server"> <INPUT id="txtRelaType" type="hidden" name="txtRelaType" runat="server"><INPUT id="txtRelaName" type="hidden" name="txtRelaName" runat="server">
			<INPUT id="txtRelaCode" type="hidden" name="txtRelaCode" runat="server">
		</form>
	</body>
</HTML>
