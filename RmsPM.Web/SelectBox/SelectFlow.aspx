<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectFlow.aspx.cs" Inherits="RmsPM.Web.SelectBox.SelectFlow" %>

<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Register TagPrefix="uc1" TagName="Workflowmonitorselect" Src="../WorkFlowControl/workflowmonitorselect.ascx" %>
<%@ Register TagPrefix="uc1" TagName="InputUser" Src="../UserControls/InputUser.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>选择流程</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="../Images/index.css" type="text/css" rel="stylesheet">

    <script language="javascript" src="../Rms.js"></script>

    <script language="javascript">
function doSelectFlow(CaseCode,ActCode,ApplicationPath,ApplicationCode,Status,Name)
{
    //alert(ApplicationCode);
    var flag = '<%=Request["Flag"]%>';
    window.parent.opener.<%=_ReturnFunc%>(CaseCode,ActCode,ApplicationPath,ApplicationCode,Status,Name,flag);
    window.parent.close();
}

    </script>

</head>
<body>
    <form runat="server">
        <table height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td bgcolor="#e4eff6" height="6">
                </td>
            </tr>
            <tr>
                <td height="25">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="topic" background="../images/topic_bg.gif">
                                <img height="25" src="../images/topic_li.jpg" width="35" align="absMiddle"><span
                                    id="spanTitle" runat="server"> 所有流程</span></td>
                            <td width="9">
                                <img height="25" src="../images/topic_corr.gif" width="9"></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr height="1">
                <td valign="top">
                    <table class="search-area" cellspacing="0" cellpadding="0" border="0">
                        <tr>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            工作流程：</td>
                                        <td>
                                            <select id="sltProcedure" runat="server" name="sltProcedure">
                                                <option selected value="">---所有流程---</option>
                                            </select>
                                        </td>
                                        <td>
                                            项 &nbsp; &nbsp;&nbsp; 目：</td>
                                        <td colspan="3">
                                            <asp:DropDownList ID="DropDownProject" runat="server">
                                            </asp:DropDownList></td>
                                        <td rowspan="4">
                                            <input class="submit" id="btnSearch" type="button" value="搜 索" name="btnSearch" runat="server"
                                                onserverclick="btnSearch_ServerClick">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            流&nbsp; 水 号：</td>
                                        <td>
                                            <input class="input" type="text" runat="server" id="txtCaseCode" /></td>
                                        <td>
                                            任 &nbsp; &nbsp;&nbsp; 务：</td>
                                        <td>
                                            <input class="input" type="text" runat="server" id="txtTaskName" /></td>
                                        <td>
                                            主 &nbsp; &nbsp;&nbsp; 题：</td>
                                        <td>
                                            <input type="text" class="input" runat="server" id="txtTitle" /></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            发 &nbsp;件 人：</td>
                                        <td>
                                            <uc1:InputUser ID="ucPerson" runat="server"></uc1:InputUser>
                                        </td>
                                        <td>
                                            发件日期：</td>
                                        <td>
                                            <cc3:Calendar ID="DateStart" runat="server" CalendarResource="../Images/CalendarResource/"
                                                ReadOnly="False" Display="True" Value="">
                                            </cc3:Calendar>
                                        </td>
                                        <td align="center">
                                            --></td>
                                        <td>
                                            <cc3:Calendar ID="DateEnd" runat="server" CalendarResource="../Images/CalendarResource/"
                                                ReadOnly="False" Display="True" Value="">
                                            </cc3:Calendar>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            处 &nbsp;理 人：</td>
                                        <td>
                                            <uc1:InputUser ID="ucToPerson" runat="server"></uc1:InputUser>
                                        </td>
                                        <td>
                                            签收日期：</td>
                                        <td>
                                            <cc3:Calendar ID="CalendarStart" runat="server" CalendarResource="../Images/CalendarResource/"
                                                ReadOnly="False" Display="True" Value="">
                                            </cc3:Calendar>
                                        </td>
                                        <td align="center">
                                            --></td>
                                        <td>
                                            <cc3:Calendar ID="CalendarEnd" runat="server" CalendarResource="../Images/CalendarResource/"
                                                ReadOnly="False" Display="True" Value="">
                                            </cc3:Calendar>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr height="100%">
                <td class="table" valign="top">
                    <div style="overflow: auto; width: 100%; height: 100%">
                        <uc1:Workflowmonitorselect ID="WorkFlowMonitor1" runat="server">
                        </uc1:Workflowmonitorselect>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="height: 19px">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td background="../images/corl_bg.gif">
                                <img height="12" src="../images/corl.gif" width="12"></td>
                            <td width="12">
                                <img height="12" src="../images/corr.gif" width="12"></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td bgcolor="#e4eff6" style="height: 6px">
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
