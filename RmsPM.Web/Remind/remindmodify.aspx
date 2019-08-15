<%@ Register TagPrefix="cc2" Namespace="RmsPM.WebControls.ToolsBar" Assembly="RmsPM.WebControls" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Page language="c#" Inherits="RmsPM.Web.Remind.RemindModify" CodeFile="RemindModify.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>事件提醒</title>
		<META http-equiv="Content-Type" content="text/html; charset=gb2312">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript">
			function doCancel(){
				window.close();
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
						else if ((number_chars.indexOf(strDay.charAt(i))==-1)  && (strDay.charAt(i) != "-"))
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
　　		
　　		function TypeChange()
　　		{
　　			// 工作提醒或者变更提醒
　　			if(document.all.lstRemindType.value=='0'||document.all.lstRemindType.value=='3')
　　			{
　　				document.all.lstRemindObject.style.display = "none";
　　				document.all.divTaskObject.style.display = "";
　　			}
　　			else
　　			{
　　				document.all.divTaskObject.style.display = "none";
　　				document.all.lstRemindObject.style.display = "";
　　			}
　　		}
　　		function BodyLoad()
　　		{
　　			TypeChange();
　　		}
　　		function doCheck()
　　		{
　　			if(document.all.lstRemindType.value=='0'||document.all.lstRemindType.value=='3')
　　			{
　　				if(!document.all.chkMaster.checked&&!document.all.chkMonitor.checked&&!document.all.chkExecuter.checked)
　　				{
　　					alert("请选择需要提醒的人员类型");
　　					return false;
　　				}					
　　			}
　　			return true;
　　		}　　		　　		
		</SCRIPT>
	</HEAD>
	<body bottomMargin="0" bgColor="white" leftMargin="0" topMargin="0" scroll="no" rightMargin="0"
		onload="BodyLoad();">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" bgColor="white" border="0">
				<tr vAlign="top">
					<td>
						<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="topic" align="center" background="../images/topic_bg.gif" height="25"><asp:label id="lblTitle" runat="server"></asp:label></td>
							</tr>
						</TABLE>
						<TABLE class="form" id="Table4" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR width="100%">
								<TD class="form-item" align="right" width="20%">事件类型：</TD>
								<TD class="tdBlank"><select class="select" id="lstRemindType" style="WIDTH: 180px" name="lstTaskStatus" runat="server"
										onchange="TypeChange();">
									</select></TD>
							</TR>
							<TR width="100%">
								<TD class="form-item" align="right" width="20%">提醒对象：</TD>
								<TD class="tdBlank"><select class="select" id="lstRemindObject" style="WIDTH: 120px" name="lstRemindObject"
										runat="server"></select>
									<div id="divTaskObject">
										<input type="checkbox" id="chkMaster" runat="server" name="chkMaster" value="2">负责人
										<input type="checkbox" id="chkMonitor" runat="server" NAME="chkMonitor" value="1">监督人
										<input type="checkbox" id="chkExecuter" runat="server" NAME="chkExecuter" value="0">参与人
									</div>
								</TD>
							</TR>
							<TR width="100%">
								<TD class="form-item" align="right">是否生效：</TD>
								<TD class="tdBlank">
									<asp:RadioButtonList id="rblActive" runat="server" RepeatDirection="Horizontal" Width="112px" Height="32px">
										<asp:ListItem Value="1" Selected="True">是</asp:ListItem>
										<asp:ListItem Value="0">否</asp:ListItem>
									</asp:RadioButtonList>
								</TD>
							</TR>
							<TR width="100%">
								<TD class="form-item" align="right">提醒天数：
								</TD>
								<TD class="tdBlank">
									<asp:RadioButtonList id="rblRemindType" runat="server" RepeatDirection="Horizontal">
										<asp:ListItem Value="1" Selected="True">提前</asp:ListItem>
										<asp:ListItem Value="0">滞后</asp:ListItem>
									</asp:RadioButtonList>
									<asp:textbox id="txtRemindDay" runat="server" Width="120px" CssClass="input"></asp:textbox>天<asp:customvalidator id="CVDays" runat="server" ClientValidationFunction="ClientValidate" ControlToValidate="txtRemindDay"
										ErrorMessage="必须输入数字"></asp:customvalidator></TD>
							</TR>
							<TR style="DISPLAY: block" width="100%">
								<TD class="form-item" align="right">备注：</TD>
								<TD class="tdBlank" colSpan="3"><textarea class="textarea" id="taRemark" style="HEIGHT: 60px" name="taRemark" cols="30" runat="server"></textarea></TD>
							</TR>
						</TABLE>
						<table align="center">
							<tr align="center" width="100%">
								<td><input class="submit" id="SaveToolsButton" onclick="if(!doCheck()) return false;" type="button"
										value="确 定" name="SaveToolsButton" runat="server" onserverclick="SaveToolsButton_ServerClick"></td>
								<td><input class="submit" id="CancelToolsButton" onclick="doCancel();return false;" type="button"
										value="取 消" name="CancelToolsButton" runat="server"></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
