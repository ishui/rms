<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ZC_AssetTransfer.aspx.cs" Inherits="WorkFlowPage_ZC_AssetTransfer" %>

<%@ Register Src="../WorkFlowOperation/ZC_AssetTransfer.ascx" TagName="OperationControl"
    TagPrefix="uc1" %>
<%@ Register Src="../WorkFlowControl/WorkFlowFormOpinion.ascx" TagName="WorkFlowOpinion"
    TagPrefix="uc1" %>
<%@ Register Src="../WorkFlowControl/WorkFlowToolbar.ascx" TagName="WorkFlowToolbar"
    TagPrefix="uc1" %>
<%@ Register Src="../WorkFlowControl/localworkflowcasestate.ascx" TagName="WorkFlowCaseState"
    TagPrefix="uc1" %>
<!DOCTYPE HTML PUBLIC "-//W3C//Dtd HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>固定资产转移</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR" />
    <meta content="C#" name="CODE_LANGUAGE" />
    <meta content="JavaScript" name="vs_defaultClientScript" />
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
    <link href="../Images/index.css" rel="stylesheet" type="text/css" />

    <script language="javascript" src="../Rms.js" type="text/javascript"></script>

    <script type="text/javascript">
		    function SelectUnit()
		    {
			    OpenSmallWindow("../SelectBox/SelectUnit.aspx?UnitCode=000000");
		    }
		    function SelectUnitReturn(code, name)
		    {
			    window.document.all.ucOperationControl_FormView1_txtUnitName.value = name;
			    window.document.all.ucOperationControl_FormView1_txtUnit.value = code;
		    }	
    </script>

</head>
<body>
    <form id="Form1" runat="server" method="post">
        <table border="0" cellpadding="0" cellspacing="0" height="100%" width="100%">
            <tr>
                <td height="25">
                    <table id="tableToolBar" class="table" width="100%">
                        <tr>
                            <td class="tools-area" width="16">
                                <img align="absMiddle" src="../images/btn_li.gif" />
                            </td>
                            <td class="tools-area">
                                <uc1:WorkFlowToolbar ID="wftToolbar" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td id="td_Print" runat="server" class="table" valign="top">
                    <table border="0" cellpadding="0" cellspacing="0" class="blackbordertable" width="100%">
                        <tr>
                            <td align="center" class="blackbordertd" colspan="2">
                                <br />
                                <font size="3"><strong>
                                    <asp:Label ID="lblWorkFlowName" runat="server"></asp:Label>
                                </strong></font>
                                <br />
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td class="blackbordertd" colspan="2">
                                <uc1:OperationControl ID="ucOperationControl" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <uc1:WorkFlowCaseState ID="wfcCaseState" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td height="12">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td background="../images/corl_bg.gif">
                                <img height="12" src="../images/corl.gif" width="12" />
                            </td>
                            <td width="12">
                                <img height="12" src="../images/corr.gif" width="12" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td bgcolor="#e4eff6" height="6">
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
