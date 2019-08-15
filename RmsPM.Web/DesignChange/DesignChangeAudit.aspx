<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DesignChangeAudit.aspx.cs" Inherits="DesignChange_DesignChangeAudit" %>
<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics.WebUI.WebDataInput.v5.1, Version=5.1.20051.37, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>设计变更审批</title>
    <link href="../Images/index.css" type="text/css" rel="stylesheet">

    <script language="javascript" src="../Rms.js"></script>
</head>
<body>
    <form id="form1" runat="server">
   <table height="100%" cellspacing="0" cellpadding="0" width="100%" border="0" bgcolor="white">
            <tr>
                <td style="height: 1px">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="topic" align="center" background="../images/topic_bg.gif" height="25">
                                设计变更审批</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="middle" height="75" >
                         <table class="form" cellspacing="0" cellpadding="0" width="100%" border="0">
                           <tr>
                                    <td class="form-item" width="15%" nowrap>
                                        变更费用：</td>
                                    <td colspan="5">
                                        <asp:TextBox ID="txtChangeMoney" runat="server" 
                                            Height="80px" TextMode="MultiLine" Width="100%"></asp:TextBox>&nbsp;
                                        合计（估计结算金额）：<igtxt:webnumericedit id="TxtTotalMoney" runat="server" MinDecimalPlaces="Two" CssClass="infra-input-nember" Width="100"
																ImageDirectory="../images/infragistics/images/" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js"
																JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js"></igtxt:webnumericedit>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtTotalMoney"
                                            ErrorMessage="必填">*</asp:RequiredFieldValidator>
                                        </td>
                                </tr>
                                </table>
                    请审核设计变更“<%= RmsPM.BFL.DesignChangeBFL.GetDesignChangeName(int.Parse(Request["DesignChangeCode"].ToString())) %>”！
                    <br />
                </td>
            </tr>
            <tr>
                <td height="50" align="center">
                    <asp:Button ID="btnPassAudit" runat="server" CssClass="submit" 
                        Text=" 通过 " OnClick="btnPassAudit_Click" />
                    <asp:Button ID="btnNoPassAudit" runat="server" CssClass="submit"
                        Text=" 作废 " OnClick="btnNoPassAudit_Click" /></td>
            </tr>
        </table>
    </form>
</body>
</html>
