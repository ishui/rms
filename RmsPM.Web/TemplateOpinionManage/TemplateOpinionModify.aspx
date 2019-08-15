<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TemplateOpinionModify.aspx.cs"
    Inherits="TemplateOpinionManage_TemplateOpinionModify" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Images/index.css" type="text/css" rel="stylesheet">

    <script language="javascript" src="../Rms.js"></script>

</head>
<body style="BORDER-RIGHT: 0px" scroll="no">
    <form id="form1" runat="server">
        <table height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td height="25">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="topic" align="center" background="../images/topic_bg.gif" height="25">
                                意见维护</td>
                        </tr>
                    </table>
                    <table class="table" id="tableToolBar" width="100%">
                        <tr>
                            <td class="tools-area" width="16">
                                <img src="../images/btn_li.gif" align="absMiddle"></td>
                            <td class="tools-area">
                                <input name="btnModify" id="btnModify" type="button" value=" 修改 " class="button"
                                    runat="server" onserverclick="btnModify_ServerClick">
                                <input name="btnDel" id="btnDel" type="button" value=" 删除 " class="button" runat="server" onserverclick="btnDel_ServerClick">
                                <input name="btnSave" id="btnSave" type="button" value=" 保存 " class="button" runat="server" onserverclick="btnSave_ServerClick">
                                <input name="btnClose" id="btnClose" type="button" value=" 关闭 " class="button" runat="server"
                                    onclick="javascript:window.close();"></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="table" valign="top">
                    <div id="OperableDiv" runat="server">
                        <table class="form" cellspacing="0" cellpadding="0" width="100%" align="center" border="1">
                            <tr>
                                <td class="form-item">
                                    标 &nbsp; &nbsp;&nbsp; 题：</td>
                                <td>
                                    <input id="txtName" type="text" runat="server" class="input" style="width: 542px">&nbsp;&nbsp;<FONT face="宋体" color="#cc0066">*</FONT>
                            </tr>
                            <tr>
                                <td class="form-item" style="height: 105px">
                                    意见内容：</td>
                                <td style="height: 105px" >
                                    <textarea id="txtCenter" runat="server" style="width: 547px; height: 87px"></textarea>
                            </tr>
                        </table>
                    </div>
                    <div id="EyeableDiv" runat="server">
                        <table class="form" cellspacing="0" cellpadding="0" width="100%" align="center" border="1">
                            <tr>
                                <td class="form-item" style="width: 99px">
                                    标 &nbsp; &nbsp;&nbsp; 题：</td>
                                <td runat="server" id="tdName">
                                </td>
                            </tr>
                            <tr>
                                <td class="form-item" style="width: 99px; height: 120px">
                                    意见内容：</td>
                                <td runat="server" id="tdCenter" style="height: 120px">
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td height="12">
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
                <td height="6" bgcolor="#e4eff6">
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
