<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AttachmentConvert.aspx.cs" Inherits="Document_AttachmentConvert" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    警告：转换不可撤销，请勿随意使用！<br />
    <div>
        &nbsp;<asp:Button ID="Button1" runat="server" Text="数据库=〉硬盘" OnClick="Button1_Click" />
        &nbsp;<asp:Button ID="Button2" runat="server" Text="硬盘=〉数据库" OnClick="Button2_Click" />
       
        &nbsp;&nbsp;&nbsp;<br />每次转换附件数量：
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox><br />
        <br />
        <br />
        <asp:Button ID="Button3" runat="server" Text="移动文件到时间对应子目录下" OnClick="Button3_Click" />
        需要先配置savepahtmode为需要转换的格式。<br />
        <br />
        <asp:Label ID="Label1" runat="server" Text="等待转换工作开始" Width="328px"></asp:Label><br />
        <br />
        说明：
        <br />
        1 请转换前做好系统配置，将硬盘存储目录的读写权限分配给 aspnet用户<br />
        2 转换前请备份数据库<br />
        3 因转换需要耗费大量的系统资源，请不要在工作时间做此操作以免影响其它用户的使用<br />
        4 为避免服务器资源不足，可设定每次转换的数量，本页面会自动刷新直至转换完成。
        </div>
    </form>
</body>
</html>
