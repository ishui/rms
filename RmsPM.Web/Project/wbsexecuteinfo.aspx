<%@ Register TagPrefix="cc2" Namespace="RmsPM.WebControls.ToolsBar" Assembly="RmsPM.WebControls" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Register TagPrefix="uc1" TagName="FeedBack" Src="../UserControls/FeedBack.ascx" %>
<%@ Page language="c#" Inherits="RmsPM.Web.Project.WBSExecuteInfo" CodeFile="WBSExecuteInfo.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="AttachMentList" Src="../UserControls/AttachMentList.ascx" %>
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
			
			
　　		function DoDelete(){	
				window.opener.SelectTask();
				window.close();
			}
　　		
//修改进度报告
function Modify()
{
	OpenFullWindow("WBSExecute.aspx?TaskExecuteCode=" + Form1.txtTaskExecuteCode.value + "&WBSCode=" + Form1.txtWBSCode.value + "&ActionState=Modify&ProjectCode=<%=Request["ProjectCode"]%>", "");
}

function OpenTask()
{
	OpenFullWindow("WBSInfo.aspx?WBSCode=" + Form1.txtWBSCode.value +"&ProjectCode=<%=Request["ProjectCode"]%>","");
}

		</SCRIPT>
	</HEAD>
	<body scroll="no">
		<form id="Form1" method="post" runat="server">
			<table width="100%" border="0" cellpadding="0" cellspacing="0" height="100%" bgcolor="#ffffff">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">工作报告</td>
				</tr>
				<tr>
					<td class="tools-area" vAlign="top">
						<IMG src="../images/btn_li.gif" align="absMiddle"> <input class="button" id="btnModify" onclick="Modify();" type="button" value="修 改" name="btnModify"
							runat="server"> <input class="button" id="btnDelete" onclick="javascript:if(!window.confirm('确实要删除这条记录吗？')) return false;"
							type="button" value="删 除" name="btnDelete" runat="server" onserverclick="btnDelete_ServerClick"> <input class="button" id="btnClose" onclick="javascript:window.close();" type="button"
							value="关 闭" name="btnClose" runat="server">
					</td>
				</tr>
				<TR height="100%">
					<td valign="top">
						<div style="OVERFLOW:auto;WIDTH:100%;HEIGHT:100%">
							<table width="100%">
								<tr>
									<td vAlign="bottom" align="center" height="25">
										<table cellSpacing="0" cellPadding="0" width="100%" border="0">
											<tr>
												<td class="note">所属工作：<a href="#" onclick="OpenTask();return false;"><asp:label id="lblTaskName" Runat="server"></asp:label>
													(<asp:label id="lblCompletePercent" runat="server"></asp:label>%)</a>
												</td>
											</tr>
										</table>
									</td>
								</tr>
								<tr>
									<td>
										<table class="form" width="100%">
											<TR>
												<TD class="form-item" nowrap width="80">报 告 人：</TD>
												<TD width="25%"><asp:label id="lblExecutePerson" runat="server"></asp:label></TD>
												<TD class="form-item" nowrap width="80">报告日期：</TD>
												<TD width="25%"><asp:label id="lblExecuteDate" runat="server"></asp:label></TD>
												<TD class="form-item" nowrap width="80">录入日期：</TD>
												<TD width="25%"><asp:label id="lblInputDate" runat="server"></asp:label></TD>
											</TR>
											<TR>
												<TD class="form-item" nowrap>分发范围：</TD>
												<TD colSpan="5">
													<asp:Label id="lblUser" runat="server"></asp:Label></TD>
											</TR>
											<TR>
												<TD class="form-item">附件：</TD>
												<TD colSpan="53">
													<uc1:AttachMentList id="myAttachMentList" runat="server"></uc1:AttachMentList>
												</TD>
											</TR>
											<TR>
												<TD class="form-item" nowrap>执行情况：</TD>
												<TD id="tdDetail" colSpan="5" runat="server" valign="top"></TD>
											</TR>
										</table>
										<br>
										<uc1:FeedBack id="FeedBack1" runat="server"></uc1:FeedBack>
									</td>
								</tr>
							</table>
						</div>
					</td>
				</TR>
			</table>
			<input type="hidden" id="txtTaskExecuteCode" name="txtTaskExecuteCode" runat="server"><input type="hidden" id="txtWBSCode" name="txtWBSCode" runat="server">
			<input type="hidden" id="txtActionState" name="txtActionState" runat="server"><input type="hidden" id="txtRefreshScript" name="txtRefreshScript" runat="server">
		</form>
	</body>
</HTML>
