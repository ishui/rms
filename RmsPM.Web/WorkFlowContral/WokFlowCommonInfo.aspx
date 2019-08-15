<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WokFlowCommonInfo.aspx.cs" Inherits="WorkFlowContral_WokFlowCommonInfo" %>
<%@ Register TagPrefix="uc1" TagName="AttachMentList" Src="../UserControls/AttachMentList.ascx" %>
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

	    function doModifyWorkFlow()
	    {

		    OpenFullWindow('WorkFlowCommonModify.aspx?ProjectCode=<%=Request["ProjectCode"]%>&WorkFlowCommonCode=<%=Request["WorkFlowCommonCode"]%>&act=Edit','新增流程');
	    }
	    
	    function doSubmitAudit()
	    {
		    OpenFullWindow('<%=ViewState["_AuditingURLAndParam"]%>','通用流程审核_<%=Request["WorkFlowCommonCode"]%>');
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
				<tr>
					<td class="tools-area" valign="top">
					    <input type="button" class="button" id="btnModify" runat="server" value="修改" onclick="doModifyWorkFlow('');return false;" />&nbsp;
                        <input id="btnDelete" runat="server" class="button" onclick="javascript:if(!window.confirm('确实要删除这条记录吗？')) return false;" type="button" value="删除" onserverclick="btnDelete_ServerClick" />&nbsp;
                        <input class="button" id="btnSubmitAudit" onclick="javascript:doSubmitAudit();return false; " type="button" value="提交申请" name="btnCheck" runat="server" />
                    </td>
				</tr>				
				<tr height="100%">
					<td valign="top">
						<div style="overflow: auto; width: 100%; height: 100%">
							<table class="form" cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tr>

									<td class="form-item" width="120">流程标题：</td>
									<td>
									    <asp:Label ID="lblWorkFlowTitle" runat="server"></asp:Label>&nbsp;
									</td>
									<td class="form-item" width="120">流程状态：</td>
									<td >
									    <asp:Label ID="lblStatus" runat="server"></asp:Label>&nbsp;
									</td>									
									<td class="form-item" width="120">流程编号：</td>
									<td>
									    <asp:Label ID="lblWorkFlowID" runat="server"></asp:Label>&nbsp;
									</td>									
								</tr>
								<tr>
									<td class="form-item">部门：</td>
									<td>
									    <asp:Label ID="lblUnitName" runat="server"></asp:Label>&nbsp;
									</td>
									<td class="form-item" width="120">流程类型：</td>
									<td >
									    <asp:Label ID="lblType" runat="server"></asp:Label>&nbsp;
									</td>
									<td class="form-item">经 办 人：</td>
									<td>
									    <asp:Label ID="lblTransactorName" runat="server"></asp:Label>&nbsp;
									</td>									
								</tr>
								<tr>
									<td class="form-item">附件文档：</td>
									<td colspan="5"><uc1:AttachMentList id="myAttachMentList" runat="server" /></td>
								</tr>
                                <tr>
                                    <td class="form-item" valign="top">主要内容：</td>
                                    <td colspan="5">
                                        <asp:Label ID="lblContent" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr height="5">
									<td colspan="6">
									</td>
								</tr>
							</table>
							
						</div>
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
