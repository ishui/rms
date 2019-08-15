<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Page language="c#" Inherits="RmsPM.Web.Project.WBSGuidModify" CodeFile="WBSGuidModify.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>工作批示</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Images/Index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<script language="javascript">
			function doCancel(){	
				window.close();
			}
			function doSave()
			{
				if(document.all.arDetail.value.length<1)
				{
					alert('请输入内容');
					return false;
				}
				return true;
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
		</script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" bgColor="white" border="0">
				<tr vAlign="middle">
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25"><asp:label id="lblTitle" runat="server"></asp:label></td>
				</tr>
				<tr>
					<td class="topic" vAlign="top" align="left">
						<input class="button" id="SaveToolsButton" type="button" value="保 存" onclick="if(!doSave()) return false;"
							name="Button1" runat="server" onserverclick="SaveToolsButton_ServerClick"> <input class="button" id="CancelToolsButton" onclick="doCancel();return false;" type="button"
							value="关 闭" name="CancelToolsButton" runat="server"> <INPUT class="button" id="btDelete" type="button" value="删 除" name="btDelete" runat="server"
							style="VISIBILITY: hidden">
						<table class="form" id="tbModify" cellSpacing="0" cellPadding="0" width="100%" border="0"
							runat="server">
							<TR>
								<TD class="form-item" align="right" width="100">指示人：</TD>
								<TD>
									<asp:label id="lblUser" runat="server" Width="100"></asp:label></TD>
								<TD class="form-item" align="right" width="100">指示日期：</TD>
								<TD><cc3:calendar id="dtbExecuteDate" runat="server" CalendarResource="../Images/CalendarResource/"
										ReadOnly="False" Display="True"></cc3:calendar></TD>
							</TR>
							<TR>
								<TD class="form-item" align="right">分发范围：</TD>
								<TD colSpan="3">
									<input id="txtUsers" type="hidden" name="txtUsers" runat="server"> <input id="txtStations" type="hidden" name="txtStations" runat="server">
									<input type="button" id="btSelectUser" value="选择分发范围" class="button-small" OnClick="SelectPerson();return false;"
										NAME="btSelectUser"><font color=red>(无选择则默认发送给责任人)</font><div id="SelectName" runat="server"></div>
									<input type="hidden" id="hSelectName" name="hSelectName">
								</TD>
							</TR>
							<TR>
								<TD class="form-item" align="right">指示内容：</TD>
								<TD colSpan="3"><textarea class="textarea" id="arDetail" name="arDetail" rows="5" cols="40" runat="server"></textarea>
								</TD>
							</TR>
						</table>
						<br>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
