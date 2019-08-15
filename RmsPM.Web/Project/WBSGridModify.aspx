<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Register TagPrefix="cc2" Namespace="RmsPM.WebControls.ToolsBar" Assembly="RmsPM.WebControls" %>
<%@ Page language="c#" CodeFile="WBSGridModify.aspx.cs" AutoEventWireup="false" Inherits="RmsPM.Web.Project.WBSGridModify" %>
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
					if (i==0)
					{
						if(strDay.charAt(i) == "0")
						{
							arguments.IsValid = false;
							return;
						}
					}
					else
					{ 
						if (number_chars.indexOf(strDay.charAt(i))==-1) 
						{
							arguments.IsValid = false;
							return;
						}
					}
				}
				arguments.IsValid = true;
　　		}
　　		function DoDelete(){	
				//window.opener.refresh();
				window.close();
			}
　　	
		</SCRIPT>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" bgColor="white" border="0">
				<tr vAlign="middle">
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25"><asp:label id="lblTitle" runat="server"></asp:label></td>
				</tr>
				<tr>
					<td align="left"><input class="button" id="SaveToolsButton" type="button" value="保 存" name="Button1" runat="server">
						<input class="button" id="CancelToolsButton" onclick="doCancel();return false;" type="button"
							value="关 闭" name="CancelToolsButton" runat="server"> <INPUT class="button" id="btDelete" type="button" value="删 除" name="btDelete" runat="server"></td>
				</tr>
				<tr>
					<td class="topic" vAlign="top" align="center">
						<table class="form" id="tbModify" cellSpacing="0" cellPadding="0" width="100%" border="0"
							style="VISIBILITY: hidden" runat="server">
							<TR>
								<TD class="form-item" align="right" width="30%">指示人：</TD>
								<TD>
									<asp:label id="Label1" runat="server" Width="120px"></asp:label></TD>
								<TD class="form-item" align="right">指示日期：</TD>
								<TD><cc3:calendar id="dtbExecuteDate" runat="server" CalendarResource="../Images/CalendarResource/"
										ReadOnly="False" Display="True"></cc3:calendar></TD>
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
