<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FileTemplateAdd.aspx.cs"
    Inherits="RmsDM_FileTemplateAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>添加模版管理</title>
    <link href="../Images/index.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <table class="form" width="100%">
        <tr><td class="form-item" style="width:120px;">文档类别名称：</td>
        <td colspan="3"><asp:Label runat="server" ID ="lblSortName"></asp:Label></td>
        </tr>
            <tr>
                <td class="form-item" style="width: 120px">
                    文档模版名称：</td>
                <td>
                    <asp:TextBox ID="tboxName" runat="server" CssClass="input"></asp:TextBox><font color="red">*<asp:RequiredFieldValidator
                        ID="RequiredFieldValidator1" runat="server" ControlToValidate="tboxName" ErrorMessage="不能为空"></asp:RequiredFieldValidator></font></td>
                <td class="form-item" style="width: 120px">
                    质量记录分类号：</td>
                <td>
                    <asp:TextBox ID="tboxSort" runat="server" CssClass="input"></asp:TextBox><font color="red">*<asp:RequiredFieldValidator
                        ID="RequiredFieldValidator2" runat="server" ControlToValidate="tboxSort" ErrorMessage="不能为空"></asp:RequiredFieldValidator></font></td>
            </tr>
            <tr>
                <td colspan="4" align="Center">
                    <input type="button" value="添加" runat="server" id="AddButton" onserverclick="btAdd_Click" class="button" />&nbsp;&nbsp;&nbsp;<input type="button" value="关闭"
                        onclick="self.close();" class="button" /></td>
            </tr>
        </table>
    </form>
</body>
</html>
