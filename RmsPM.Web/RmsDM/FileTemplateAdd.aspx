<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FileTemplateAdd.aspx.cs"
    Inherits="RmsDM_FileTemplateAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>���ģ�����</title>
    <link href="../Images/index.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <table class="form" width="100%">
        <tr><td class="form-item" style="width:120px;">�ĵ�������ƣ�</td>
        <td colspan="3"><asp:Label runat="server" ID ="lblSortName"></asp:Label></td>
        </tr>
            <tr>
                <td class="form-item" style="width: 120px">
                    �ĵ�ģ�����ƣ�</td>
                <td>
                    <asp:TextBox ID="tboxName" runat="server" CssClass="input"></asp:TextBox><font color="red">*<asp:RequiredFieldValidator
                        ID="RequiredFieldValidator1" runat="server" ControlToValidate="tboxName" ErrorMessage="����Ϊ��"></asp:RequiredFieldValidator></font></td>
                <td class="form-item" style="width: 120px">
                    ������¼����ţ�</td>
                <td>
                    <asp:TextBox ID="tboxSort" runat="server" CssClass="input"></asp:TextBox><font color="red">*<asp:RequiredFieldValidator
                        ID="RequiredFieldValidator2" runat="server" ControlToValidate="tboxSort" ErrorMessage="����Ϊ��"></asp:RequiredFieldValidator></font></td>
            </tr>
            <tr>
                <td colspan="4" align="Center">
                    <input type="button" value="���" runat="server" id="AddButton" onserverclick="btAdd_Click" class="button" />&nbsp;&nbsp;&nbsp;<input type="button" value="�ر�"
                        onclick="self.close();" class="button" /></td>
            </tr>
        </table>
    </form>
</body>
</html>
