<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectDefineSingleUser.aspx.cs"
    Inherits="SelectBox_SelectDefineSingleUser" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../Images/Style.css" type="text/css" rel="stylesheet" />

    <script language="javascript" type="text/javascript" src="../Rms.js"></script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table height="100%" width="101%" border="0" cellpadding="0" cellspacing="0" bgcolor="#ffffff">
                <tr height="100%">
                    <td valign="top" align="center">
                        <div style="overflow: auto; width: 100%; height: 100%">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%" class="list">
                                <tr class="list-title">
                                    <td style="width: 30px;">
                                    </td>
                                    <td>
                                        »À‘±</td>
                                </tr>
                                <asp:Repeater ID="repeaterSU" runat="server">
                                    <ItemTemplate>
                                        <tr style="width: 100%;">
                                            <td>
                                                <img border="0" width="15" height="15" src='<%# "../Images/" + DataBinder.Eval( Container,"DataItem.ImageFileName" ) %> '
                                                    alt="">
                                            </td>
                                            <td style="width: 100%;">
                                                <a href="##" onclick="doSelectUser('<%# Eval("RelationCode") %>','<%# Eval("Name") %>');">
                                                    <%# Eval("Name") %>
                                                </a>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr style="display: none">
                    <td>
                        <table cellspacing="0" cellpadding="10" width="100%" border="0" id="tableButton"
                            runat="server">
                            <tr align="center">
                                <td>
                                    <input class="submit" id="btnSelect" type="button" value="—° ‘Ò" name="btnSelect" onclick="doSelectSU();" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </form>

    <script type="text/javascript">
        function doSelectUser( userCode,userName )
		{
			window.parent.setuservalue(userCode,userName);
		}
    </script>

</body>
</html>
