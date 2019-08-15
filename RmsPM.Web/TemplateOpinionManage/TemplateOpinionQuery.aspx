<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TemplateOpinionQuery.aspx.cs"
    Inherits="TemplateOpinionManage_TemplateOpinionQuery" %>

<%@ Register TagPrefix="cc1" Namespace="Tiannuo.Web.Controls" Assembly="DataGridWebControls" %>
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
                <td bgcolor="#e4eff6" height="6">
                </td>
            </tr>
            <tr>
                <td height="25">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="topic" background="../images/topic_bg.gif">
                                <img height="25" src="../images/topic_li.jpg" width="35" align="absMiddle"><span
                                    id="spanTitle"> 常用意见</span></td>
                            <td width="9">
                                <img height="25" src="../images/topic_corr.gif" width="9"></td>
                        </tr>
                    </table>
                    <table class="table" id="table1" width="100%">
                        <tr>
                            <td class="tools-area" width="16">
                                <img src="../images/btn_li.gif" align="absMiddle"></td>
                            <td class="tools-area">
                                <input name="btnAdd" id="btnAdd" type="button" value=" 新增 " class="button" runat="server"
                                    onclick="javascript:OpenTemplateModify('');return false;">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr height="100%">
                <td class="table" valign="top">
                    <asp:DataGrid ID="dgList" CssClass="list" CellPadding="0" GridLines="Horizontal"
                        PageSize="15" Width="100%" AllowPaging="True" runat="server" AutoGenerateColumns="False">
                        <HeaderStyle CssClass="list-title"></HeaderStyle>
                        <FooterStyle CssClass="list-title"></FooterStyle>
                        <Columns>
                            <asp:BoundColumn DataField="TemplateOpinionCode" HeaderText="TemplateOpinionCode"
                                Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="标题">
                                <ItemTemplate>
                                    <a href="#" onClick="javascript:OpenTemplateModify('<%# DataBinder.Eval(Container, "DataItem.TemplateOpinionCode") %>');return false;">
                                        <%# DataBinder.Eval(Container, "DataItem.name") %>
                                    </a>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="意见内容">
                                <ItemTemplate>
                                        <%# DataBinder.Eval(Container, "DataItem.Center") %>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <PagerStyle Visible="False" NextPageText="下一页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上一页"
                            HorizontalAlign="Right" CssClass="ListHeadTr"></PagerStyle>
                    </asp:DataGrid>
                    <cc1:GridPagination ID="gpControl" runat="server" DataGridId="dgList" ControlSourceUrl="../Images/GridPaginationSource/"
                        OnPageIndexChange="gpControl_PageIndexChange"></cc1:GridPagination>
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
                <td bgcolor="#e4eff6" height="6">
                </td>
            </tr>
        </table>
    </form>

    <script>
function OpenTemplateModify(code)
{
	OpenLargeWindow('TemplateOpinionModify.aspx?TemplateOpinionCode='+code,'常用意见维护');
}
    </script>

</body>
</html>
