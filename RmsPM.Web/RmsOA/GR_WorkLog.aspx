<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GR_WorkLog.aspx.cs" Inherits="RmsOA_GR_WorkLog" %>
<%@ Register Assembly="AspWebControl" Namespace="AspWebControl" TagPrefix="cc1" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../Images/index.css" rel="stylesheet" type="text/css" />
    <title>个人日志</title>

    <script language="javascript" type="text/javascript">
     function OpenLargeWindow(strUrl,strName)
     {
	    return window.open(strUrl,strName,"width=800,height=600,fullscreen=0,top="+(window.screen.height-600)/2+",left="+(window.screen.width-800)/2+",menubar=no,toolbar=no,scrollbars=yes,status=no,titlebar=no,resizable=yes,location=no");
     }
    </script>
</head>
<body style="BORDER-RIGHT: 0px" scroll="no">
    <form id="form1" runat="server">
    <div>
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
          <tr>
            <td height="6" bgcolor="#e4eff6"></td>
          </tr>
        </table>
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td width="986" background="../images/topic_bg.gif" class="topic">
                    <img align="absMiddle" height="25" src="../images/topic_li.jpg" width="35">个人办公>工作日记                </td>
                <td width="9">
                    <img height="25" src="../images/topic_corr.gif" width="9"></td>
            </tr>
            <tr>
                <td colspan="2" valign="top" class="tools-area" style="width: 100%;">
                    <img align="absMiddle" src="../images/btn_li.gif">
                <input class="button" runat="server" id="NewButton" onClick="OpenLargeWindow('GR_WorkLogModify.aspx?Type=Add','WorkLog')"
                        type="button" value="新增" /></td>
            </tr>
        </table>
        <table border="0" cellpadding="0" cellspacing="0" class="search-area" width="100%">
            <tr>
                <td>
                <table width="100%">
                <tr>
                <td>
                    内容</td><td>
                        <asp:TextBox ID="TextBoxContext" runat="server" CssClass="input"></asp:TextBox></td>
                        <td>
                            时间</td>
                    <td>
                        <cc1:Calendar ID="dateBegin" runat="server" CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                            Value="">
                        </cc1:Calendar>
                        &nbsp;到&nbsp;
                        <cc1:Calendar ID="dateEnd" runat="server" CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                            Value="">
                        </cc1:Calendar>
                    </td><td colspan="4" align="center">
                        <input id="btnSearch" runat="server" class="submit" onserverclick="btSearch_Click"
                            type="button" value="搜索" /></td>
                </tr>
                </table>
                </td>
            </tr>
        </table>
        
    </div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="list" Width="100%" AllowPaging="True" PageSize="18">
            <FooterStyle CssClass="list-title" />
            <Columns>
            <asp:TemplateField HeaderText="创建日期">
            <ItemTemplate>
                <a href="#" onClick="OpenLargeWindow('GR_WorkLogModify.aspx?Type=Read&Code=<%#Eval("Code")%>','WorkLog')">
                    <%# Eval("DayWrited")%>
                </a>
            </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
                <HeaderStyle CssClass="list-title" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="内容简介">
            <ItemTemplate>
            <%# Eval("Context") %>
            </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
                <HeaderStyle CssClass="list-title" HorizontalAlign="Center" />
            </asp:TemplateField>
            </Columns>
            <HeaderStyle CssClass="list-title" />
            <PagerSettings FirstPageText="首 页" LastPageText="尾 页" Mode="NextPreviousFirstLast"
                NextPageText="下一页" PageButtonCount="4" Position="Bottom" PreviousPageText="上一页"
                Visible="true" />
        </asp:GridView>
    </form>
</body>
</html>
