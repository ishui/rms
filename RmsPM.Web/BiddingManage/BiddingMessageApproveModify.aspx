<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BiddingMessageApproveModify.aspx.cs" Inherits="BiddingManage_BiddingMessageApproveModify" %>

<%@ Register TagPrefix="uc1" TagName="AttachMentList" Src="../UserControls/AttachMentList.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AttachMentAdd" Src="../UserControls/AttachMentAdd.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Control_BiddingEmitMoney" Src="Control_BiddingEmitMoney.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WorkFlowToolbar" Src="../WorkFlowControl/WorkFlowToolbar.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WorkFlowCaseState" Src="../WorkFlowControl/WorkFlowCaseState.ascx" %>
<%@ Register TagPrefix="uc1" TagName="BiddingMessageModify" Src="BiddingMessageModify.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WorkFlowOpinion" Src="../WorkFlowControl/WorkFlowFormOpinion.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="AspWebControl" Assembly="AspWebControl" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>中标通知单</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="../Images/index.css" type="text/css" rel="stylesheet">

    <script language="javascript" src="../Rms.js"></script>

</head>
<body>
    <form id="Form1" method="post" runat="server">
        <table height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="table" valign="top" colspan="5">
                    <table class="blackbordertable" cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td  align="center" colspan="5">
                                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                    <tr>
                                        <td class="topic" align="center" background="../images/topic_bg.gif" height="25">
                                            中标通知单</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="tools-area" width="16">
                                &nbsp</td>
                       </tr>
                        
                        <tr>
                            <td colspan="5">
                                <font face="宋体">
                                  
                                    <table class="form"  cellspacing="0" cellpadding="0" width="100%" align="center" border="1">
                                        <tr>
                                            <td style="width: 15%" class="form-item">
                                                项目名称</td>
                                            <td style="width: 35%"  runat="server" id="txtProjectCode">
                                            &nbsp;
                                            </td>
                                            <td style="width: 15%" class="form-item" >
                                                合同编号</td>
                                            <td style="width: 35%" >
                                                &nbsp;&nbsp;&nbsp;&nbsp;<input id="txtContractNember" type="text" runat="server"
                                                    class="input" name="txtContractNember"></td>
                                        </tr>
                                        <tr>
                                            <td class="form-item">
                                                合同名称</td>
                                            <td>
                                                &nbsp;&nbsp;&nbsp;&nbsp;<input id="txtContractName" type="text" runat="server" class="input"
                                                    name="txtContractName"><font face="宋体" color="#cc0066">*</font></td>
                                            <td class="form-item">
                                                合同类别</td>
                                            <td runat="server" id="txtContractType">
                                                <font face="宋体"></font>
                                            </td>
                                        </tr>
                                            <tr>
                                                <td class="form-item">
                                                    签约单位</td>
                                                <td runat="server" id="txtSupplier" colspan="3">
                                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:DropDownList ID="DropSupplier" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropSupplier_SelectedIndexChanged">
                                                    </asp:DropDownList><br/></td>
                                            </tr>
                                            <tr>
                                                <td class="form-item">
                                                    中标标段</td>
                                                <td colspan="3">
                                                    <asp:CheckBoxList ID="CheckBoxList1" runat="server">
                                                    </asp:CheckBoxList></td>
                                            </tr>
                                        <tr>
                                            <td style="height: 35px" class="form-item">
                                                预计签约日期</td>
                                            <td colspan="3">
                                                &nbsp;&nbsp;&nbsp;&nbsp;<cc1:Calendar ID="txtContractDate" runat="server" CalendarResource="../Images/CalendarResource/"
                                                    CalendarMode="Date">
                                                </cc1:Calendar>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="form-item">
                                                附件</td>
                                            <td colspan="3">
                                                <uc1:AttachMentAdd ID="AttachMentAdd1" runat="server"></uc1:AttachMentAdd>
                                                <font face="宋体">&nbsp;</font></td>
                                        </tr>
                                        <tr>
                                            <td class="form-item" nowrap>
                                                &nbsp;&nbsp;合同概述&nbsp;&nbsp;</td>
                                            <td colspan="3" align="center" valign="middle">
                                                <textarea id="txtRemark" style="width: 98%; height: 56px" name="Reason" rows="3"
                                                    runat="server"></textarea></td>
                                        </tr>
                                    </table>
                                    <table  align="center">
                                        <tr>
                                            <td width="100%" colspan="2">&nbsp;</td>
                                        </tr>
					                    <tr align="center" width="100%">
						                    <td><input class="submit" id="SaveToolsButton"  type="button"
								                    value="确 定" name="SaveToolsButton" runat="server" onserverclick="SaveToolsButton_ServerClick" ></td>
						                    <td><input class="submit" id="CancelToolsButton" onclick="doCancel();return false;" type="button"
								                    value="取 消" name="CancelToolsButton" runat="server"></td>
					                    </tr>
				                    </table>
                                      
                                </font>
                            </td>
                        </tr>
                        
                    </table>
                </td>
            </tr>
        </table>
    </form>
    <script>
            function doCancel()
            {
				window.close();
			}
    </script>
</body>

</html>
