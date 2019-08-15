<%@ Reference Page="~/project/wbsstatus.aspx" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Register TagPrefix="uc1" TagName="AttachMentAdd" Src="../UserControls/AttachMentAdd.ascx" %>
<%@ Register TagPrefix="ftb" Namespace="FreeTextBoxControls" Assembly="FreeTextBox" %>
<%@ Page language="c#" Inherits="RmsPM.Web.Project.WBSExecute" validateRequest=false CodeFile="WBSExecute.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>工作报告</title>
		<META http-equiv="Content-Type" content="text/html; charset=gb2312">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/Index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<SCRIPT language="javascript">
			function doCancel(){	
				//window.opener.refresh();
				window.close();
			}
			
			function DoAddNewAttachMent()
			{
				var ExecuteCode = Form1.txtTaskExecuteCode.value;
				if(ExecuteCode.length<1)
				{
					alert('请先保存工作报告');
					return;
				} 
				OpenSmallWindow("WBSAttachMent.aspx?Action=Insert&ExecuteCode=" + ExecuteCode+"&ProjectCode=<%=Request["ProjectCode"]%>","");
				
			}
			
			function EditAttach(Code)
			{
				OpenSmallWindow("WBSAttachMent.aspx?Action=Modify&AttachMentCode=" + Code+"&ProjectCode=<%=Request["ProjectCode"]%>","");
				
			}
			
			function ViewAttach(Code)
			{
				OpenMiddleWindow("WBSAttachMentView.aspx?Action=View&AttachmentCode=" + Code+"&ProjectCode=<%=Request["ProjectCode"]%>", "");
			}
			
			function refresh()
			{
				document.forms[0].submit();
			}
			
			function ClientValidate(source, arguments)　　
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
　　		}
　　		
　　		function DoDelete(){	
				window.close();
			}
			　　		
			function SelectPerson()
　　		{
　　			OpenMiddleWindow("../SelectBox/SelectSUMain.aspx?UserCodes="+window.document.all.txtUsers.value+"&StationCodes="+window.document.all.txtStations.value+"");
　　		}
　　		/*
　　		function DoSelectUser(userCode,userName,flag)
　　		{
　　			window.document.all.txtUsers.value = userCode;	
				window.document.all.SelectName.innerText = userName;	
				window.document.all.hSelectName.value = userName;	
　　		}
　　		*/
　　		function getReturnStationUser(userCodes,userNames,stationCodes,stationNames,flag)
			{
				window.document.all.txtUsers.value = userCodes;	
				window.document.all.txtStations.value = stationCodes;	
				window.document.all.SelectName.innerText = getString(userNames,stationNames);	
				window.document.all.hSelectName.value = getString(userNames,stationNames);	
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
　　		
		</SCRIPT>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" bgColor="white" border="0">
				<tr vAlign="middle">
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25"><asp:label id="lblTitle" runat="server"></asp:label></td>
				</tr>
				<tr height="100%">
					<td class="topic" vAlign="top">
						<div style="OVERFLOW:auto;WIDTH:100%;HEIGHT:100%">
							<table class="form" id="tbModify" cellSpacing="0" cellPadding="0" width="100%" border="0"
								align="center" runat="server">
								<TR>
									<TD class="form-item" align="right" width="100" nowrap>完成进度：</TD>
									<TD width="50%"><asp:textbox id="txtPercent" runat="server" Width="60px" CssClass="input"></asp:textbox>&nbsp;%
										<asp:customvalidator id="CVPercent" runat="server" ErrorMessage="需要输入数字" ControlToValidate="txtPercent"
											ClientValidationFunction="ClientValidate"></asp:customvalidator></TD>
									<TD class="form-item" align="right" width="100" nowrap>报告日期：</TD>
									<TD width="50%"><cc3:calendar id="dtbExecuteDate" runat="server" CalendarResource="../Images/CalendarResource/"
											ReadOnly="False"></cc3:calendar>
									</TD>
								</TR>
								<TR>
									<TD class="form-item" align="right">附件：</TD>
									<TD colSpan="3"><FONT face="宋体">
											<uc1:AttachMentAdd id="myAttachMentAdd" runat="server"></uc1:AttachMentAdd></FONT>
									</TD>
								</TR>
								<TR>
									<TD class="form-item" align="right">分发范围：</TD>
									<TD colSpan="3">
										<input id="txtUsers" type="hidden" name="txtUsers" runat="server"> <input id="txtStations" type="hidden" name="txtStations" runat="server">
										<input type="button" id="btSelectUser" value="选择分发范围" class="button-small" OnClick="SelectPerson();return false;"
											NAME="btSelectUser"><div id="SelectName" runat="server"></div>
										<input type="hidden" id="hSelectName" name="hSelectName">
									</TD>
								</TR>
							</table>
							<br>
							<TABLE cellSpacing="0" cellPadding="0" border="0">
								<TR align="left">
									<TD class="intopic">执行情况</TD>
									<td></td>
								</TR>
							</TABLE>
							<FTB:FreeTextBox id="ftbDetail" runat="server" Width="100%" ButtonPath="../images/ftb/office2003/"
								HelperFilesPath="../HelperScripts"></FTB:FreeTextBox>
							<br>
						</div>
					</td>
				</tr>
				<tr>
					<td>
						<table cellSpacing="10" width="100%">
							<tr>
								<td align="center"><input class="submit" id="btnSave" type="button" value="确 定" onclick="document.all.divHintSave.style.display='';"
										name="btnSave" runat="server" onserverclick="btnSave_ServerClick"> <input class="submit" id="btnCancel" onclick="javascript:self.close()" type="button" value="取 消"
										name="btnCancel">
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<div id="divHintLoad" style="DISPLAY: none; LEFT: 1px; WIDTH: 100%; POSITION: absolute; TOP: 200px; BACKGROUND-COLOR: transparent">
				<TABLE id="tableHintLoad" height="100" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD vAlign="top" align="center"><iframe id="frameLoad" src="../Cost/LoadingPrepare.htm" frameBorder="no" width="100%" scrolling="auto"
								height="100%"></iframe>
						</TD>
					</TR>
				</TABLE>
			</div>
			<div id="divHintSave" style="DISPLAY: none; LEFT: 1px; WIDTH: 100%; POSITION: absolute; TOP: 200px">
				<TABLE id="tableHintSave" height="100" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD vAlign="top" align="center"><iframe id="frameSave" src="../Cost/SavingWating.htm" frameBorder="no" width="100%" scrolling="auto"
								height="100%"></iframe>
						</TD>
					</TR>
				</TABLE>
			</div>
			<input type="hidden" id="txtTaskExecuteCode" name="txtTaskExecuteCode" runat="server"><input type="hidden" id="txtWBSCode" name="txtWBSCode" runat="server">
			<input type="hidden" id="txtActionState" name="txtActionState" runat="server"><input type="hidden" id="txtRefreshScript" name="txtRefreshScript" runat="server">
			<input type="hidden" id="txtProjectCode" name="txtProjectCode" runat="server">
		</form>
		<script language="javascript">
		var tmp = '<%=Request["hSelectName"]%>';
		if(tmp.length>0)
			window.document.all.SelectName.innerText = tmp;
		window.document.all.hSelectName.value = '<%=Request["hSelectName"]%>';
		</script>
	</body>
</HTML>
