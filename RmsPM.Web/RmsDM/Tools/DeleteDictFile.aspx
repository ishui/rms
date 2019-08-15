<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DeleteDictFile.aspx.cs" Inherits="RmsDM_Tools_DeleteDictFile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        父目录代码：<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="删除目录下所有文档" /></div>
    </form>
</body>
</html>
