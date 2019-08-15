<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FileTemplateTypeList.aspx.cs" Inherits="RmsDM_FileTemplateTypeList" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
	<link href="../Images/Style.css" rel="stylesheet" type="text/css" />
    <link href="../Images/index.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="..\Rms.js"></script>
    <script type="text/javascript" language="javascript">
    function OpenLargeWindow(strUrl,strName){
	return window.open(strUrl,strName,"width=800,height=150,fullscreen=0,top="+(window.screen.height-600)/2+",left="+(window.screen.width-800)/2+",menubar=no,toolbar=no,scrollbars=yes,status=no,titlebar=no,resizable=yes,location=no");
}
    function AddDocument()
    {
	    OpenLargeWindow("FileTemplateTypeModify.aspx?FormType=Add&Parentcode="+'<%=Request["NodeValue"]%>'+"&FullPath="+'<%=Request["FullID"] %>'+"&Deep="+'<%=Request["Deep"] %>', "FileTemplateTypeModify");
    }
	
    </script>
    <title>模板类别</title>
</head>
<body topmargin="0" scroll="no">
    <form id="form1" runat="server">
    <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0"
      bgcolor="#ffffff">
            <tr>
                <td class="topic" colspan="2" align="center" background="../images/topic_bg.gif" height="25">
                    模板类别信息</td>
            </tr>
                <tr>
                    <td class="tools-area" valign="top">
                        <img src="../images/btn_li.gif" align="absMiddle" />
                        <input class="button" id="btnAdd" runat="server" onclick="AddDocument('')" type="button" value="新 增"
                            name="btnAdd">
                    </td>
                </tr>
     </table>
     <div style="overflow: auto; position: absolute; width: 100%; height: 100%;">
        <asp:GridView  Width="100%" ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataSource1" AllowPaging="True" CssClass="list" PageSize="18">
            <HeaderStyle CssClass="list-title" />
            <FooterStyle CssClass="list-title" />
            <Columns>
            <asp:TemplateField HeaderText ="子模板类别名称" SortExpression="FileTemplateTypeName">
            <ItemTemplate>
            <a href="#" onclick="OpenLargeWindow('FileTemplateTypeModify.aspx?code=<%# Eval("Code") %>&NodeValue=<%=Request["NodeValue"]%>','FileTemplateTypeModify')"><%# Eval("FileTemplateTypeName")%></a>
            </ItemTemplate>
                <HeaderStyle Wrap="False" />
                <ItemStyle  HorizontalAlign ="Center" />
            </asp:TemplateField>
            </Columns>
            <PagerStyle CssClass="ListHeadTr"/>
            <PagerSettings FirstPageText="首 页" LastPageText="尾 页" Mode="NextPreviousFirstLast"
                NextPageText="下一页" PageButtonCount="4" Position="Bottom" PreviousPageText="上一页"
                Visible="true" />
        </asp:GridView>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetFileTemplateTypeList"
            TypeName="RmsDM.BFL.FileTemplateTypeBFL" SortParameterName="SortColumns" StartRowIndexParameterName="StartRecord" MaximumRowsParameterName="MaxRecords">
            <SelectParameters>
                <asp:Parameter Name="SortColumns" Type="String" />
                <asp:Parameter DefaultValue="0" Name="StartRecord" Type="Int32" />
                <asp:Parameter DefaultValue="-1" Name="MaxRecords" Type="Int32" />
                <asp:Parameter Name="CodeEqual" Type="Int32" DefaultValue="" />
                <asp:Parameter Name="FileTemplateTypeNameEqual" Type="String" />
                <asp:QueryStringParameter Name="ParentCodeEqual" QueryStringField="NodeValue" Type="Int32" />
                <asp:Parameter Name="FullIDEqual" Type="String" />
                <asp:Parameter Name="DeepEqual" Type="Int32" />
                <asp:Parameter Name="OrderByIDEqual" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        </div>
    </form>
</body>
</html>
