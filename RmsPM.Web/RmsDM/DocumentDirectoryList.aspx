<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DocumentDirectoryList.aspx.cs"
    Inherits="RmsDM_DocumentDirectoryList" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>文档目录列表</title>
    <link href="../Images/index.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../Rms.js"></script>
    <script type="text/javascript" language="javascript">
    function AddDocument()
    {
	    OpenCustomWindow("DocumentDirectoryModify.aspx?FormType=Add&Parentcode="+'<%=Request["NodeValue"]%>'+"&FullPath="+'<%=Request["FullID"] %>'+"&Deep="+'<%=Request["Deep"] %>', "DocumentDirectoryModify","800","300");
    }
    function CopyFileTemplateType()
    {
	    OpenMiddleWindow("SelectDocumentTemplate.aspx?ChooseType=TemplateType&ParentCode="+'<%=Request["NodeValue"]%>');
    }
	function AddFileTemplate()
    {
	    OpenMiddleWindow("SelectDocumentTemplate.aspx?AddSignleType=addSignleType&ParentCode="+'<%=Request["NodeValue"]%>');
    }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table id="Table1" height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                    <td class="tools-area" valign="top">
                        <img src="../images/btn_li.gif" align="absMiddle">
                        <input class="button" id="btnAdd" onclick="AddDocument()" type="button" value="新 增"
                            name="btnAdd" runat="server"/>
                        <input class="button" id="btnCopy" onclick="CopyFileTemplateType()" type="button" value="复制模板节点"
                            name="btnAdd" runat="server" />
                        <input class="button" id="btnAddTemplate" onclick="AddFileTemplate()" type="button" value="多选模板节点"
                            name="btnAdd" runat="server" />   
                    </td>
                </tr>
                <tr>
                    <td class="table" valign="top">
                        <table width="100%" height="100%">
                            <tr height="100%">
                                <td valign="top" align="left" style="height: 100%">
                                    <div style="overflow: auto; position: absolute; width: 100%; height: 100%">
                                        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetChildrenDocumentDirectory"
                                            TypeName="RmsDM.BFL.DocumentDirectoryBFL">
                                            <SelectParameters>
                                                <asp:QueryStringParameter Name="ParentCode" QueryStringField="NodeValue" Type="Int32" />
                                            </SelectParameters>
                                        </asp:ObjectDataSource>
                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataSource1" Width="100%" CssClass="list">
                                            <HeaderStyle CssClass="list-title"></HeaderStyle>
                                            <FooterStyle CssClass="list-title"></FooterStyle>
                                            <Columns>
                                                <asp:TemplateField HeaderText="目录名称" SortExpression="DicertoryName">
                                                    <ItemTemplate>
                                                     <a href="#" onclick="OpenCustomWindow('DocumentDirectoryModify.aspx?DocDirCode=<%# Eval("Code") %>','DocumentDirectoryModify','800','300')">
                                                         <%# Eval("DirectoryName") %>
                                                      </a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>                                               
                                                <asp:TemplateField HeaderText="所属部门" SortExpression="DepartmentCode">
                                                    <ItemTemplate>
                                                        <%# RmsPM.BLL.SystemRule.GetUnitFullName((Eval("DepartmentCode")).ToString()) %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="DirectoryNodeCode" HeaderText="编码" SortExpression="DirectoryNodeCode" /> 
                                                <asp:TemplateField HeaderText="完整路径" SortExpression="FullID">
                                                    <ItemTemplate>
                                                        <%#WebFunctionRule.GetTreeViewFullPath((Eval("FullID")).ToString())%>
                                                    </ItemTemplate>
                                                </asp:TemplateField> 
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
