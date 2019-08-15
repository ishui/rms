<%@ Page Language="c#" Inherits="RmsPM.Web.Systems.User" CodeFile="User.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>User</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312">
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../Images/index.css" type="text/css" rel="stylesheet">
</head>
<body scroll="no">

    <script language="javascript" src="../Rms.js"></script>

    <script>
        function SelectUnit() {
            OpenMiddleWindow("../SelectBox/SelectUnit.aspx?UnitCode=100009");
        }
        function SelectUnitReturn(code, name) {
            window.document.all.txtUnitName.value = name;
            window.document.all.txthUnit.value = code;
        }
    </script>

    <form id="Form1" method="post" runat="server">
        <table height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td bgcolor="#e4eff6" height="6"></td>
            </tr>
            <tr>
                <td height="25">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="topic" background="../images/topic_bg.gif">
                                <img height="25" src="../images/topic_li.jpg" width="35" align="absMiddle"><span
                                    id="spanTitle" runat="server"> 系统管理&nbsp;- 用户管理</span></td>
                            <td width="9">
                                <img height="25" src="../images/topic_corr.gif" width="9"></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="tools-area" valign="top">
                    <img src="../images/btn_li.gif" align="absMiddle">
                    <input class="button" id="btnAdd" onclick="doAddNewUser(); return false;" type="button"
                        value="新 增" name="btnAdd">
                    <asp:Button ID="Button1" runat="server" CssClass="button" OnClick="Button1_Click"
                        Text="导 入" /></td>
            </tr>
            <tr>
                <td class="search-area" valign="top">
                    <p>
                        姓&nbsp;&nbsp;&nbsp; &nbsp; 名:
                        <asp:TextBox ID="TB_UserName" runat="server" Height="20px" Width="88px" CssClass="input"></asp:TextBox>&nbsp;登&nbsp;
                        录 名:
                        <asp:TextBox ID="TB_LoginName" runat="server" Height="20px" Width="88px" CssClass="input"></asp:TextBox>&nbsp;工&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;号:
                        <asp:TextBox ID="TB_WorkCode" runat="server" Height="20px" Width="88px" CssClass="input"></asp:TextBox>&nbsp;所属部门:&nbsp;
                        &nbsp;<input class="input" id="txthUnit" style="width: 72px; height: 18px" type="hidden"
                            size="8" name="txthUnit" runat="server"><input class="input" id="txtUnitName" style="width: 72px; height: 18px"
                                type="text" size="4" name="txtUnit" runat="server"><img style="cursor: hand"
                                    onclick="SelectUnit();return false;" src="../images/ToolsItemSearch.gif">&nbsp;&nbsp;<br>
                        办公电话:
                        <asp:TextBox ID="TB_Tel" runat="server" Height="20px" Width="88px" CssClass="input"></asp:TextBox>&nbsp;移动电话:
                        <asp:TextBox ID="TB_Mtel" runat="server" Height="20px" Width="88px" CssClass="input"></asp:TextBox>&nbsp;电子信息箱:
                        <asp:TextBox ID="TB_Email" runat="server" Height="20px" Width="88px" CssClass="input"></asp:TextBox>&nbsp;状&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        态:&nbsp;&nbsp;
                        <asp:DropDownList ID="sltIsAllow" runat="server" Width="72px">
                            <asp:ListItem Value="2">所有状态</asp:ListItem>
                            <asp:ListItem Value="0">启用状态</asp:ListItem>
                            <asp:ListItem Value="1">禁用状态</asp:ListItem>
                        </asp:DropDownList>&nbsp;<input class="submit" id="btnSearch" type="button" value="搜索用户"
                            name="btnSearch" runat="server" style="width: 72px" onserverclick="btnSearch_ServerClick">&nbsp;
                        <input class="button" id="BT_ShFromResult" style="width: 72px; height: 21px" type="button"
                            value="在结果找" name="btnSearch" runat="server" disabled onserverclick="BT_ShFromResult_ServerClick">
                    </p>
                </td>
            </tr>
            <tr height="100%">
                <td class="table" valign="top">
                    <div style="overflow: auto; width: 100%; height: 100%">
                        <asp:DataGrid ID="dgList" runat="server" Width="100%" CssClass="list" CellPadding="0"
                            GridLines="Horizontal" AllowSorting="True" AutoGenerateColumns="False" PageSize="18">
                            <FooterStyle CssClass="list-title"></FooterStyle>
                            <HeaderStyle CssClass="list-title"></HeaderStyle>
                            <Columns>
                                <asp:TemplateColumn SortExpression="UserName" HeaderText="姓 名">
                                    <HeaderStyle Wrap="False"></HeaderStyle>
                                    <ItemStyle Wrap="False"></ItemStyle>
                                    <ItemTemplate>
                                        <a href="#" onclick='OpenUser(<%# DataBinder.Eval(Container.DataItem, "UserCode") %>);return false;' actcode='<%# DataBinder.Eval(Container.DataItem, "UserCode") %>'>
                                            <%# DataBinder.Eval(Container.DataItem, "UserName") %>
                                        </a>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="UserID" SortExpression="UserID" HeaderText="登录名">
                                    <HeaderStyle Wrap="False"></HeaderStyle>
                                    <ItemStyle Wrap="False"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="SortID" SortExpression="SortID" HeaderText="工 号">
                                    <HeaderStyle Wrap="False"></HeaderStyle>
                                    <ItemStyle Wrap="False"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="岗 位">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem, "stationNameHtml") %>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="Phone" SortExpression="Phone" HeaderText="办公电话">
                                    <HeaderStyle Wrap="False"></HeaderStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Mobile" SortExpression="Mobile" HeaderText="移动电话">
                                    <HeaderStyle Wrap="False"></HeaderStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="电子邮箱">
                                    <HeaderStyle Wrap="true"></HeaderStyle>
                                    <ItemStyle Wrap="true"></ItemStyle>
                                    <ItemTemplate>
                                        <%# RmsPM.BLL.PageHelpDisplay.ChangeMessageForDisplay(Eval("MailBox").ToString(),"</br>",';')%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="status" HeaderText="状态">
                                    <HeaderStyle Wrap="False"></HeaderStyle>
                                    <ItemStyle Wrap="False"></ItemStyle>
                                    <ItemTemplate>
                                        <%# RmsPM.BLL.ConvertRule.ToInt(DataBinder.Eval(Container.DataItem, "status"))==1?"<font color=red>禁用</font>":"启用" %>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle NextPageText="下一页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上一页" HorizontalAlign="Right"
                                CssClass="ListHeadTr"></PagerStyle>
                        </asp:DataGrid>
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
                <td bgcolor="#e4eff6" height="6"></td>
            </tr>
        </table>
    </form>

    <script language="javascript" src="../Rms.js"></script>

    <script language="javascript">

        function OpenUser(userCode) {
            OpenLargeWindow('UserInfo.aspx?UserCode=' + userCode, '用户信息');
        }

        function doAddNewUser() {
            OpenLargeWindow('UserModify.aspx', '用户信息');
        }

    </script>

</body>
</html>
