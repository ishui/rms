<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SupplierGradeInfo.aspx.cs" Inherits="SupplierGrade_SupplierGradeInfo" %>
<%@ Register TagPrefix="uc1" TagName="WorkFlowToolbar" Src="../WorkFlowControl/WorkFlowToolbar.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WorkFlowCaseState" Src="../WorkFlowControl/WorkFlowCaseState.ascx" %>

<%@ Register TagPrefix="uc1" TagName="WorkFlowOpinion" Src="../WorkFlowControl/WorkFlowFormOpinion.ascx" %>
<%@ Register TagPrefix="uc1" TagName="OperationControl" Src="../WorkFlowOperation/sm_SupplierGradeOpinion.ascx" %>
<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics.WebUI.WebDataInput.v5.1, Version=5.1.20051.37, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>承包商评分</title>
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
					<td style="height: 25px">
						<table class="table" id="tableToolBar" width="100%">
							<tr>
								<td class="tools-area" width="16"><IMG src="../images/btn_li.gif" align="Middle"></td>
								<td class="tools-area">
								    <input class="button" id="btnDelete" visible=false
							type="button" value="删除" name="btnDelete" runat="server" onserverclick="btnDelete_Click"> <input class="button" id="btnModify" type="button"
							value="修改" name="btnModify" runat="server" visible=false onserverclick="btnModify_ServerClick"> <input class="button" id="btnSave" type="button"
							value="保存" onserverclick="btnSave_ServerClick" name="btnSave" runat="server" visible=false > <input  class="button" id="btnCheck" type="button"
							value="提交审核" name="btnCheck" visible=false runat="server" onclick="doGrade();return false;"> <input name="btnClose" id="btnClose" type="button" value=" 关闭 " class="button" runat="server"
                                    onclick="javascript:window.close();" style="width: 64px">  
									<uc1:WorkFlowToolbar Visible="false" id="wftToolbar" runat="server"></uc1:WorkFlowToolbar>
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
                                <td colspan=10><uc1:WorkFlowCaseState Visible=false  id="wfcCaseState" runat="server"></uc1:WorkFlowCaseState></td>
                            </tr>
                        </table>
                    </TD>
				</TR>
		</table>
    </form>

    <script language="javascript">
    function doGrade()
		{
			OpenFullWindow('../WorkFlowPage/sm_SupplierGrade.aspx?ProjectCode=<%=ProjectCode%>&gradeMessageCode=<%=Request["gradeMessageCode"]%>&SupplierCode=<%=SupplierCode%>','评分');
		}
function DoSelectSupplierReturn ( code,name)
		{
			Form1.ucOperationControl_txtSupplierCode.value = code;
			Form1.ucOperationControl_txtSupplierName.value = name;
		}
		function openSelectSupplier()
		{
			OpenMiddleWindow( '../SelectBox/SelectSupplier.aspx?ProjectCode=<%=Request["ProjectCode"]%>','选择供应商' );
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
    </script>

</body>
</html>
