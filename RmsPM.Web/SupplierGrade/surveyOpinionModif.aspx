<%@ Page Language="C#" AutoEventWireup="true" CodeFile="surveyOpinionModif.aspx.cs" Inherits="SupplierGrade_surveyOpinionModif" %>
<%@ Register TagPrefix="uc1" TagName="WorkFlowToolbar" Src="../WorkFlowControl/WorkFlowToolbar.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WorkFlowCaseState" Src="../WorkFlowControl/WorkFlowCaseState.ascx" %>

    <%@ Register Src="../WorkFlowControl/WorkFlowList.ascx" TagName="WorkFlowList" TagPrefix="uc3" %>
<%@ Register TagPrefix="uc1" TagName="WorkFlowOpinion" Src="../WorkFlowControl/WorkFlowFormOpinion.ascx" %>
<%@ Register TagPrefix="uc1" TagName="OperationControl" Src="control_surveyOpinion.ascx" %>
<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics.WebUI.WebDataInput.v5.1, Version=5.1.20051.37, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>调查意见表</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../Images/index.css" type="text/css" rel="stylesheet">
    <link href="../Images/infra.css" type="text/css" rel="stylesheet">

    <SCRIPT language="javascript" src="../Rms.js"></SCRIPT>

</head>
<body>
    <form id="Form1" runat="server">
        <table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
                <tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">调查意见表</td>
				</tr>
				<tr>
					<td style="height: 25px">
						<table class="table" id="tableToolBar" width="100%">
							<tr>
								<td class="tools-area" width="16"><IMG src="../images/btn_li.gif" align="Middle"></td>
								<td class="tools-area">
								    <input class="button" id="btnDelete" 
							type="button" value="删  除" name="btnDelete" runat="server" onserverclick="btnDelete_Click"> <input class="button" id="btnModify" type="button"
							value="修  改" name="btnModify" runat="server"  onserverclick="btnModify_ServerClick"> <input class="button" id="btnSave" type="button"
							value="保  存" onserverclick="btnSave_ServerClick" name="btnSave" runat="server" >
							<input  class="button" id="btnOldCheck" type="button"	value="审  核" name="btnOldCheck" runat="server" onserverclick="btnOldCheck_ServerClick">
							 <input  class="button" id="btnCheck" type="button"	value="提交审核" name="btnCheck"  runat="server" > 
							 <input name="btnClose" id="btnClose" type="button" value=" 关  闭 " class="button" runat="server" onclick="javascript:window.close();" style="width: 55px">  
								
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr><td align=center colspan=10>
                    <asp:Label ID="lblState" runat="server"></asp:Label></td></tr>
				<tr>
					<td class="table" vAlign="top">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td valign="top" colspan=10>
                                    <uc1:OperationControl ID="ucOperationControl" runat="server"></uc1:OperationControl>
                                </td>
                            </tr>		
                            <tr>
                                <td> 
                                    <table cellspacing="0" cellpadding="0" width="100%" border="0" >
                                       <tr id="webtabs">
                                           <td class="TabShow" id="workflowmsg" runat="server"  nowrap>
                                                            相关流程
                                           </td>                            
                                       </tr>
                                    </table>
                                    <table cellspacing="0" cellpadding="0" width="100%" border="0" id="tabdiv">
                                        <tr id="WorkFlowDiv" runat="server" >
                                           <td>
                                              <uc3:WorkFlowList ID="WorkFlowList1" runat="server" />
                                           </td>
                                        </tr>                    
                                    </table>
                                </td>  
                            </tr>
                            </table>
                        </TD>
				</TR>
		</table>
    </form>

    <script language="javascript">
    
function DoSelectSupplierReturn ( code,name)
		{
			Form1.ucOperationControl_txtSupplierCode.value = code;
			Form1.ucOperationControl_txtSupplierName.value = name;
		}
		function opendoSurvey()
		{
			OpenFullWindow( '<%=SurveyUrl%>?ApplicationCode=<%=ApplicationCodetemp%>&SupplierCode=<%=Request["suppliercode"]+"" %>','选择供应商' );
			
		}

		function viewSupplier( supplierCode )
		{
			OpenFullWindow( '../Supplier/SupplierInfo.aspx?SupplierCode=' + supplierCode , '供应商信息' );
		}
		function DoPrint()
	    {
		var PrintUrl = '<%=ViewState["_PrintURL"] %>';
		
		OpenFullWindow( PrintUrl ,'打印预览');
	    }
	
	function DoAccountPrint()
	{
		var PrintUrl = '<%=ViewState["_AccountPrintURL"] %>';
		OpenFullWindow( PrintUrl ,'打印预览');
	}
	
	//选择工作项

function SelectTaskReturn(code, name)
{
	if (Form1.ucOperationControl_txtSelectTaskFlag.value == "1")
	{
		Form1.ucOperationControl_txtAddTaskCode.value = code;
		
	}
	else
	{
		Form1.ucOperationControl_txtWBSCode.value = code;
		Form1.ucOperationControl_txtTaskName.value = name;
		document.all.ucOperationControl_spanTaskName.innerText = name;
	}
}

////选择工作项

//function SelectTask()
//{
//	Form1.ucOperationControl_txtSelectTaskFlag.value = "";
//	OpenMiddleWindow("../SelectBox/SelectTask.aspx?Flag=1&WBSCode=&ProjectCode=<%=ProjectCode%>");
//}
    </script>

</body>
</html>
