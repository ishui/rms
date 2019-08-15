<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WorkFlowCommonModify.aspx.cs" Inherits="WorkFlowContral_WorkFlowCommonModify" ValidateRequest="false"%>
<%@ Register TagPrefix="uc1" TagName="InputSystemGroup" Src="../UserControls/InputSystemGroup.ascx" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Register TagPrefix="uc1" TagName="AttachMentAdd" Src="../UserControls/AttachMentAdd.ascx" %>
<%@ Register TagPrefix="uc2" TagName="InputUnit" Src="../UserControls/InputUnit.ascx" %>
<%@ Register TagPrefix="ftb" Namespace="FreeTextBoxControls" Assembly="FreeTextBox" %>
<!DOCTYPE HTML PUBLIC "-//W3C//Dtd HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>流程信息</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR" />
		<meta content="C#" name="CODE_LANGUAGE" />
		<meta content="JavaScript" name="vs_defaultClientScript" />
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
		<script language="javascript" src="../images/convert.js"></script>
		<link href="../Images/Index.css" type="text/css" rel="stylesheet" />
        <link href="../Images/infra.css" type="text/css" rel="stylesheet" />
		<script language="javascript" src="../Rms.js"></script>
		<script language="javascript">
<!--


function undoHidden()
{
	document.all("iframeSave").style.display = "none";
	document.all("tableMain").style.display = "";
}

function DoSelectUser(userCode,userName,flag)
{
	// 选经办人
	if ( flag == 0 )
	{
		Form1.txtTransactor.value = userCode;
		Form1.txtTransactorName.value = userName;
	}
}

function SelectTransactor()
{
	OpenMiddleWindow('../SelectBox/SelectPerson.aspx?Flag=0&Type=Single','选择用户');
}

//生成付款计划
function Page_Reload( )
{
	Form1.btnReload.click();
}
//-->
		</script>
	</head>
	<body scroll="no">
		<form id="Form1" method="post" runat="server">
			<table id="tableMain" height="100%" cellspacing="0" cellpadding="0" width="100%" bgColor="#ffffff" border="0">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25"><asp:Label ID="lblProcedureName" runat="server"></asp:Label></td>
				</tr>
				<tr height="100%">
					<td valign="top">
						<div style="overflow: auto; width: 100%; height: 100%">
							<table class="form" cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tr>

									<td class="form-item" width="120">流程标题：</td>
									<td colspan="3">
									    <input class="input" id="txtWorkFlowTitle" type="text" name="txtWorkFlowTitle" runat="server" style="width:400px" />
									    <font color="red">*</font>
									</td>
									<td class="form-item" width="120">流程编号：</td>
									<td><input class="input" id="txtWorkFlowID" type="text" size="32" name="txtWorkFlowID" runat="server" />&nbsp;
									</td>									
								</tr>
								<tr>
									<td class="form-item">部门：</td>
									<td><uc2:inputunit id="ucUnit" runat="server"></uc2:inputunit><font color="red">*</font>
									</td>
									<td class="form-item" width="120">流程类型：</td>
									<td ><uc1:inputsystemgroup id="inputSystemGroup" runat="server"></uc1:inputsystemgroup><font color="red">*</font>
									</td>
									<td class="form-item">经 办 人：</td>
									<td>
									    <input class="input-readonly" id="txtTransactorName" readonly type="text" name="txtTransactorName" runat="server" /> 
									    <input id="txtTransactor"  type="hidden" name="txtTransactor" runat="server" />
										<a onclick="SelectTransactor();return false;" href="#"><img src="../images/ToolsItemSearch.gif" border="0" /></a>
									</td>									
								</tr>
								<tr>
									<td class="form-item">附件文档：</td>
									<td colspan="5"><uc1:attachmentadd id="myAttachMentAdd" runat="server"></uc1:attachmentadd></td>
								</tr>
								<tr height="5">
									<td colspan="6">
									</td>
								</tr>
							</table>
							<br />
                            <ftb:FreeTextBox id="ftbDetail" runat="server" Width="100%" ButtonPath="../images/ftb/office2003/"
								HelperFilesPath="../HelperScripts"></ftb:FreeTextBox>						
						</div>
					</td>
				</tr>
				<tr>
					<td>
						<table cellspacing="0" cellpadding="9" width="100%">
							<tr>
								<td align="center">
								    <input class="submit" id="btnSave" type="button" value="确 定" name="btnSave" runat="server" onserverclick="btnSave_ServerClick">
									&nbsp; <input class="submit" id="btnCancel" onclick="javascript:self.close()" type="button" value="取 消"
										name="btnCancel" runat="server">
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<iframe id="iframeSave" style="display: none" src="../Cost/SavingWating.htm" frameBorder="no"
				width="100%" scrolling="auto" height="70%"></iframe>
			<input id="hidProcedureCode" runat="server" visible="false" />
			<input id="hidProjectCode" runat="server" visible="false" />
			<input id="hidWorkFlowCommonCode" runat="server" visible="false" />
		</form>
		<script language="javascript">
<!--

undoHidden();

//-->
		</script>
	</body>
</HTML>
