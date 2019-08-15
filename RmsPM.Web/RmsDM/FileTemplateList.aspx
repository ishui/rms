<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FileTemplateList.aspx.cs"
    Inherits="RmsDM_FileTemplateList" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <link href="../Images/index.css" rel="stylesheet" type="text/css" />
    <title>模板类别列表</title>

    <script language="javascript" type="text/javascript">
    function OpenLargeWindow(strUrl,strName)
    {
       return window.open(strUrl,strName,"width=800,height=400,fullscreen=0,top="+(window.screen.height-600)/2+",left="+(window.screen.width-800)/2+",menubar=no,toolbar=no,scrollbars=yes,status=no,titlebar=no,resizable=yes,location=no");
	}
    </script>

</head>
<body bottommargin="0">
    <form id="form1" runat="server">
        <div>
            <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0" style="background: #ffffff">
                <tr>
                    <td class="topic" colspan="3" align="center" background="../images/topic_bg.gif"
                        style="height: 25px">
                        文档模板信息</td>
                </tr>
                <tr>
                    <td class="tools-area" valign="top">
                        <img align="absMiddle" src="../images/btn_li.gif">
                        <input id="btAdd" runat="server" type="button" value="新增" class="button" name="btAdd" />
                    </td>
                </tr>
            </table>
        </div>
        <div style="overflow: auto; position: absolute; width: 100%; height: 100%;">
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetFileTemplateList"
                SortParameterName="SortColumns" StartRowIndexParameterName="StartRecord" MaximumRowsParameterName="MaxRecords"
                TypeName="RmsDM.BFL.FileTemplateBFL">
                <SelectParameters>
                    <asp:Parameter Name="SortColumns" Type="String" />
                    <asp:Parameter DefaultValue="0" Name="StartRecord" Type="Int32" />
                    <asp:Parameter DefaultValue="-1" Name="MaxRecords" Type="Int32" />
                    <asp:Parameter Name="CodeEqual" Type="Int32" DefaultValue="" />
                    <asp:QueryStringParameter Name="FileTemplateTypeCodeEqual" QueryStringField="NodeValue"
                        Type="Int32" />
                    <asp:Parameter Name="FileTemplateNameEqual" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                DataSourceID="ObjectDataSource1" Width="100%" PageSize="18">
                <HeaderStyle CssClass="list-title" />
                <FooterStyle CssClass="list-title" HorizontalAlign="center" />
                <Columns>
                    <asp:TemplateField HeaderText="文档模板名称" SortExpression="FileTemplateName">
                        <HeaderStyle Width="100%" HorizontalAlign="center" Wrap="False" />
                        <ItemStyle Width="100%" HorizontalAlign="center" />
                        <ItemTemplate>
                            <a href="#" onclick="OpenLargeWindow('FileTemplateVersionList.aspx?code=<%# Eval("Code") %>','FileTemplateVersionList')">
                                <%#Eval("FileTemplateName")%>
                            </a>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerStyle CssClass="ListHeadTr" />
                <PagerSettings Visible="true" FirstPageText="首 页" NextPageText="下一页" LastPageText="尾 页" PreviousPageText="上一页" Mode="NextPreviousFirstLast" Position="Bottom" PageButtonCount="4" />
            </asp:GridView>
        </div>
    </form>
</body>
</html>
