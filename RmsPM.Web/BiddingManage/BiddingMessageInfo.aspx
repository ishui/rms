<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BiddingMessageInfo.aspx.cs" Inherits="BiddingManage_BiddingMessageInfo" %>

<%@ Register TagPrefix="uc1" TagName="AttachMentList" Src="../UserControls/AttachMentList.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AttachMentAdd" Src="../UserControls/AttachMentAdd.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Control_BiddingEmitMoney" Src="Control_BiddingEmitMoney.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WorkFlowToolbar" Src="../WorkFlowControl/WorkFlowToolbar.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WorkFlowCaseState" Src="../WorkFlowControl/WorkFlowCaseState.ascx" %>
<%@ Register TagPrefix="uc1" TagName="BiddingMessageModify" Src="BiddingMessageModify.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WorkFlowOpinion" Src="../WorkFlowControl/WorkFlowFormOpinion.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Register Src="../WorkFlowControl/WorkFlowList.ascx" TagName="WorkFlowList" TagPrefix="uc3" %>

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
                                <table class="table" id="tableToolBar" width="100%">
                                        <tr>
                                                
                                                <td class="tools-area" width="5">
                                                    <img src="../images/btn_li.gif" align="absMiddle"/>    
                                                </td>
                                                <td class="tools-area">
                                                    <input name="btnMessage" id="btnMessage" type="button" value=" 中标通知书评审 " class="button"
                                                        runat="server"/>
                                                    <input name="btnClose" id="btnClose" type="button" value=" 关闭 " class="button" runat="server"
                                                        onclick="javascript:window.close();"/></td>
                                       </tr>
                                 </table>
                              </td>
                        </tr>
                        
                        <tr>
                            <td colspan="5">
                                <font face="宋体">
                                  
                                   <table class="form" cellspacing="0" cellpadding="0" width="100%" align="center" >
                                       
                                        <tr>
                                            <td class="form-item">
                                                项目名称</td>
                                            <td  runat="server" id="tdProjectCode">
                                            </td>
                                            
                                             <td class="form-item">
                                                 状态

                                            </td>
                                            <td runat="server" id="tdState">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="form-item">
                                                合同名称</td>
                                            <td  runat="server" id="tdContractName">
                                            </td>
                                            <td class="form-item">
                                                合同类别</td>
                                            <td  runat="server" id="tdContractType">
                                            </td>
                                        </tr>
                                        <tr>
                                              <td class="form-item">
                                                合同编号</td>
                                            <td  runat="server" id="tdContractNember"></td>
                                            <td class="form-item">
                                                签约单位</td>
                                            <td  runat="server" id="tdSupplier" >
                                            </td>
                                        </tr>
                                            <tr>
                                            <td class="form-item">
                                                中标标段</td>
                                            <td  runat="server" id="tdBiddingDtl" colspan="3">
                                            </td>
                                        </tr>
                                            
                                         <tr>
                                          
                                            </td>
                                            <td class="form-item">
                                                预计签约日期</td>
                                            <td  runat="server" id="tdContractDate" colspan="3">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="form-item">
                                                附件</td>
                                            <td colspan="3" >
                                                <uc1:AttachMentList ID="AttachMentList1" runat="server"></uc1:AttachMentList>
                                                <font face="宋体">&nbsp;</font></td>
                                          </tr>
                                          <tr>
                                                <td class="form-item">
                                                    合同概述</td>
                                                <td  colspan="3" runat="server" id="tdRemark">
                                                </td>
                                           </tr>
                                    </table>
                                </font>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5">
                                 <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tr>
                                            <td class="intopic" width="200">
                                                相关流程</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                   <uc3:WorkFlowList ID="WorkFlowList1" runat="server" />
                                            </td>
                                        </tr>
                                    </table>      
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
			
			function BiddingMessage(code)
            {
	            OpenLargeWindow('<%=BiddingMessageManageUrl%>?ApplicationCode='+code,'中标通知书');
            }
    </script>
</body>

</html>
