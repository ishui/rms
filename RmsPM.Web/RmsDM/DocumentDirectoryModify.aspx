<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DocumentDirectoryModify.aspx.cs"
    Inherits="RmsDM_DocumentDirecotoryModify" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>文档修改</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312">
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../Images/index.css" type="text/css" rel="stylesheet">

    <script language="javascript" src="../Rms.js"></script>

    <script language="javascript" type="text/javascript">
		function SelectUnit()
		{
			OpenMiddleWindow("../SelectBox/SelectUnit.aspx?UnitCode=000000");
		}
		function SelectUnitReturn(code, name)
		{
			
			window.document.all("FormView1_txtUnitName").value = name;
			window.document.all("FormView1_txtUnit").value = code;
	
		}
		function SelectTemplate()
		{
			OpenMiddleWindow("SelectDocumentTemplate.aspx?ReturnFunction=ReturnTemplate");
		}	
		function ReturnTemplate(Code,Name)
		{
		   window.document.all("FormView1_txtTemplateName").value = Name;
			window.document.all("FormView1_txtTemplateCode").value = Code;
		}
    </script>

</head>
<body scroll="no">
    <form id="Form1" name="Form1" method="post" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0" height="100%"
            bgcolor="#ffffff">
            <tr>
                <td class="topic" align="center" background="../images/topic_bg.gif" style="height: 25px">
                    文档信息</td>
            </tr>
            <tr>
                <td class="topic" valign="top" align="center">
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="RmsDM.MODEL.DocumentDirectoryModel"
                        DeleteMethod="Delete" InsertMethod="Insert" SelectMethod="GetDocumentDirectoryListOne"
                        TypeName="RmsDM.BFL.DocumentDirectoryBFL" UpdateMethod="Update">
                        <SelectParameters>
                            <asp:QueryStringParameter DefaultValue="" Name="Code" QueryStringField="DocDirCode"
                                Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <asp:FormView ID="FormView1" runat="server" DataSourceID="ObjectDataSource1" Width="100%"
                        DataKeyNames="Code" OnDataBound="FormView1_DataBound" OnItemInserting="FormView1_ItemInserting"
                        OnItemUpdated="FormView1_ItemUpdated" OnItemInserted="FormView1_ItemInserted"
                        OnItemUpdating="FormView1_ItemUpdating" OnItemDeleting="FormView1_ItemDeleting">
                        <EditItemTemplate>
                            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="form">
                                <tr>
                                    <td width="120" class="form-item">
                                        目录名称：</td>
                                    <td>
                                        <asp:TextBox ID="DirectoryNameTextBox" runat="server" Text='<%# Bind("DirectoryName") %>'
                                            CssClass="input">
                                        </asp:TextBox><font color="red">*</font>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="DirectoryNameTextBox"
                                            ErrorMessage="RequiredFieldValidator">提示：请输入目录名称</asp:RequiredFieldValidator></td>
                                    <td width="100" class="form-item">
                                        文档目录编码：</td>
                                    <td>
                                        <asp:TextBox ID="DirectoryNodeCodeTextBox" runat="server" Text='<%# Bind("DirectoryNodeCode") %>'
                                            CssClass="input">
                                        </asp:TextBox>
                                        </td>
                                    &nbsp;</tr>
                                <tr>
                                    <td class="form-item">
                                        上级目录：</td>
                                    <td>
                                        <asp:Label ID="ParentCodeLabel" runat="server"></asp:Label>
                                        <asp:HiddenField ID="ParentCodeHiddenField" runat="server" Value='<%# Bind("ParentCode") %>' />
                                        <br />
                                    </td>
                                    <td class="form-item">
                                        指定模板：</td>
                                    <td>
                                        <input id="txtTemplateCode" runat="server" class="input" name="txtTemplateCode" size="8"
                                            style="width: 72px; height: 18px" type="hidden" value='<%#Bind("FileTemplateCode") %>' />
                                        <input id="txtTemplateName" runat="server" class="input" name="txtTemplateName" style="width: 121px;
                                            height: 18px" type="text" />
                                        <img onclick="SelectTemplate();return false;" src="../images/ToolsItemSearch.gif"
                                            style="cursor: hand" />
                                        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" Visible="false">取消模板</asp:LinkButton></td>
                                </tr>
                                <tr>
                                    <%--<td class="form-item">
                                        质量记录分类号：</td>
                                    <td>
                                        <asp:TextBox ID="SortCodeTextBox" runat="server" Text='<%# Bind("SortCode") %>' CssClass="input">
                                        </asp:TextBox><font color="red">*</font>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="SortCodeTextBox"
                                            ErrorMessage="RequiredFieldValidator">提示：质量记录分类号</asp:RequiredFieldValidator>
                                    </td>--%>
                                    <td class="form-item">
                                        所属部门：</td>
                                    <td colspan="3">
                                        <input id="txtUnit" runat="server" class="input" name="txtUnit" size="8" style="width: 72px;
                                            height: 18px" type="hidden" value='<%#Bind("DepartmentCode") %>' /><input id="txtUnitName"
                                                runat="server" class="input" name="txtUnit" style="width: 121px; height: 18px"
                                                type="text" readonly /><img onclick="SelectUnit();return false;" src="../images/ToolsItemSearch.gif"
                                                    style="cursor: hand" /><font color="red">*</font>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtUnitName"
                                            ErrorMessage="RequiredFieldValidator">提示：请选择所属部门</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                            </table>
                            <table width="100%" cellspacing="10">
                                <tr>
                                    <td align="center">
                                        <asp:Button ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update"
                                            Text="更新" CssClass="button"></asp:Button>
                                        <input id="Button1" type="button" value="关闭" onclick="javascript:window.close();"
                                            class="button" />
                                    </td>
                                </tr>
                            </table>
                        </EditItemTemplate>
                        <InsertItemTemplate>
                            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="form">
                                <tr>
                                    <td width="120" class="form-item">
                                        目录名称：</td>
                                    <td>
                                        <asp:TextBox ID="DirectoryNameTextBox" runat="server" Text='<%# Bind("DirectoryName") %>'
                                            CssClass="input">
                                        </asp:TextBox><font color="red">*</font>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="DirectoryNameTextBox"
                                            ErrorMessage="RequiredFieldValidator">提示：请输入目录名称</asp:RequiredFieldValidator></td>
                                    <td width="100" class="form-item">
                                        文档目录编码：</td>
                                    <td>
                                        <asp:TextBox ID="DirectoryNodeCodeTextBox" runat="server" Text='<%# Bind("DirectoryNodeCode") %>'
                                            CssClass="input">
                                        </asp:TextBox><font color="red">*</font>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="DirectoryNodeCodeTextBox"
                                            ErrorMessage="RequiredFieldValidator">提示：请输入目录编号</asp:RequiredFieldValidator></td>
                                    </tr>
                                <tr>
                                    <td class="form-item">
                                        上级目录：</td>
                                    <td>
                                        <asp:Label ID="ParentCodeLabel" runat="server"></asp:Label>
                                        <asp:HiddenField ID="ParentCodeHiddenField" runat="server" Value='<%# Bind("ParentCode") %>' />
                                    </td>
                                    <td class="form-item">
                                        指定模板：</td>
                                    <td>
                                        <input id="txtTemplateCode" runat="server" class="input" name="txtTemplateCode" size="8"
                                            style="width: 72px; height: 18px" type="hidden" value='<%#Bind("FileTemplateCode") %>'/><input
                                                id="txtTemplateName" runat="server" class="input" name="txtTemplateName" style="width: 121px;
                                                height: 18px" type="text" readonly />
                                        <img onclick="SelectTemplate();return false;" src="../images/ToolsItemSearch.gif"
                                            style="cursor: hand" />
                                    </td>
                                </tr>
                                <tr>
                                    <%--<td class="form-item">
                                        质量记录分类号：</td>
                                    <td>
                                        <asp:TextBox ID="SortCodeTextBox" runat="server" Text='<%# Bind("SortCode") %>' CssClass="input">
                                        </asp:TextBox><font color="red">*</font>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="SortCodeTextBox"
                                            ErrorMessage="RequiredFieldValidator">提示：请输入目录分类号</asp:RequiredFieldValidator>
                                    </td>--%>
                                    <td class="form-item">
                                        所属部门：</td>
                                    <td colspan="3">
                                        <input id="txtUnit" runat="server" class="input" name="txtUnit" size="8" style="width: 72px;
                                            height: 18px" type="hidden" value='<%#Bind("DepartmentCode") %>' /><input id="txtUnitName"
                                                runat="server" class="input" name="txtUnit" style="width: 121px; height: 18px"
                                                type="text" readonly /><img onclick="SelectUnit();return false;" src="../images/ToolsItemSearch.gif"
                                                    style="cursor: hand" /><font color="red">*</font>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtUnitName"
                                            ErrorMessage="RequiredFieldValidator">提示：请选择所属部门</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                            </table>
                            <table width="100%" cellspacing="10">
                                <tr>
                                    <td align="center">
                                        <asp:Button ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                                            Text="插入" CssClass="button"></asp:Button>
                                        <input id="Button1" type="button" value="关闭" onclick="javascript:window.close();"
                                            class="button" />
                                    </td>
                                </tr>
                            </table>
                        </InsertItemTemplate>
                        <ItemTemplate>
                            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="form">
                                <tr>
                                    <td width="120" class="form-item">
                                        目录名称：</td>
                                    <td>
                                        <asp:Label ID="DirectoryNameLabel" runat="server" Text='<%# Bind("DirectoryName") %>'>
                                        </asp:Label></td>
                                    <td width="100" class="form-item">
                                        文档目录编码：</td>
                                    <td>
                                        &nbsp;
                                        <asp:Label ID="DirectoryNodeCodeLabel" runat="server" Text='<%# Bind("DirectoryNodeCode") %>'>
                                        </asp:Label></td>
                                </tr>
                                <tr>
                                    <td class="form-item">
                                        目录路径：</td>
                                    <td>
                                        <asp:Label ID="FullIDLabel" runat="server" Text='<%# WebFunctionRule.GetTreeViewFullPath((Eval("FullID")).ToString()+"/"+(Eval("Code")).ToString()) %>'></asp:Label><br />
                                    </td>
                                    <td class="form-item">
                                        指定模板：</td>
                                    <td>
                                        <asp:Label ID="FileTemplateCodeLabel" runat="server" Text='<%# RmsDM.BFL.FileTemplateBFL.GetTemplateName((Eval("FileTemplateCode")).ToString()) %>'>
                                        </asp:Label></td>
                                </tr>
                                <tr>
                                    <%--<td class="form-item">
                                        质量记录分类号：</td>
                                    <td>
                                        <asp:Label ID="SortCodeLabel" runat="server" Text='<%# Bind("SortCode") %>'></asp:Label><br />
                                    </td>--%>
                                    <td class="form-item">
                                        所属部门：</td>
                                    <td colspan="3">
                                        <asp:Label ID="DepartmentCodeTextBox" runat="server" Text='<%# RmsPM.BLL.SystemRule.GetUnitName(Eval("DepartmentCode").ToString()) %>'>
                                        </asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <table width="100%" cellspacing="10">
                                <tr>
                                    <td align="center">
                                        <asp:Button ID="EditButton" runat="server" CausesValidation="False" CommandName="Edit"
                                            Text="编辑" CssClass="button"></asp:Button>
                                        <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="删除" CssClass="button" />
                                        <input id="Button1" type="button" value="关闭" onclick="javascript:window.close();"
                                            class="button" />
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:FormView>
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
