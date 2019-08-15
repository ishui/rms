<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectDefineGroup.aspx.cs"
    Inherits="RmsPM.Web.SelectBox.SelectDefineGroup" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>选择用户分组</title>
    <link href="../Images/Style.css" type="text/css" rel="stylesheet" />

    <script language="javascript" type="text/javascript" src="../Rms.js"></script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table border="0" width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        分组类别
                    </td>
                </tr>
                <asp:Repeater ID="GroupName" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td>
                                &nbsp; <a href="#" style="vertical-align: middle;" onclick="selectuser('<%# Eval("Code")%>')">
                                    <img alt="" src="../Images/user.gif" style="vertical-align: bottom; border: 0;" />
                                    <%# Eval("GroupName")%>
                                </a>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
    </form>

    <script type="text/javascript">
    function selectuser(code)
    {
        var userdisplay = window.parent.frameUser;
        if(userdisplay)
        {
           window.parent.document.all.frameUser.src='SelectDefineUser.aspx?GroupCode='+code;
        }
        else
        {
            window.alert("加载错误！");
        }
    }
    </script>

</body>
</html>
