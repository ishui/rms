<%@ Page Language="c#" Inherits="RmsPM.Web.Systems.ChgPwd" CodeFile="ChgPwd.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>更改密码</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="../Images/index.css" type="text/css" rel="stylesheet">
</head>
<body scroll="no">
    <form id="Form1" method="post" runat="server">
        <table cellspacing="0" cellpadding="0" width="100%" border="0" height="100%" align="center"
            bgcolor="#ffffff">
            <tr>
                <td>
                    <table class="form" cellspacing="0" cellpadding="0" width="100%" border="0" height="50%"
                        align="center" runat="server" id="pwdtable">
                        <tr>
                            <td width="40%" class="form-item">
                                原密码：</td>
                            <td>
                                <input type="password" id="txtOldPwd" name="txtOldPwd" runat="server" class="input"></td>
                        </tr>
                        <tr>
                            <td class="form-item">
                                新密码：</td>
                            <td>
                                <input type="password" id="txtNewPwd" name="txtNewPwd" runat="server" class="input"></td>
                        </tr>
                        <tr>
                            <td class="form-item">
                                确认新密码：</td>
                            <td>
                                <input type="password" id="txtConfirmPwd" name="txtConfirmPwd" runat="server" class="input"></td>
                        </tr>
                    </table>
                    <table class="form" cellspacing="0" cellpadding="0" width="100%" border="0" height="50%"
                        align="center" runat="server" id="owntable">
                        <tr>
                            <td width="40%" class="form-item">
                                旧辅助密码：</td>
                            <td>
                                <input type="password" id="txtOldOwn" name="txtOldOwn" runat="server" class="input"></td>
                        </tr>
                        <tr>
                            <td class="form-item">
                                新辅助密码：</td>
                            <td>
                                <input type="password" id="txtNewOwn" name="txtNewOwn" runat="server" class="input"></td>
                        </tr>
                        <tr>
                            <td class="form-item">
                                确认新辅助密码：</td>
                            <td>
                                <input type="password" id="txtConfirmOwn" name="txtConfirmOwn" runat="server" class="input"></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table cellspacing="0" cellpadding="10" width="100%" border="0" height="100%">
                        <tr align="center">
                            <td>
                                <input type="button" name="btnOK" id="btnOK" class="submit" value="确 定" runat="server"
                                    onserverclick="btnOK_ServerClick">
                                <!--<input type="button" name="btnCalcen" id="btnCancel" class="submit" value="取 消" onclick="window.close();">--></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
