<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AttachmentConvert.aspx.cs" Inherits="Document_AttachmentConvert" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    ���棺ת�����ɳ�������������ʹ�ã�<br />
    <div>
        &nbsp;<asp:Button ID="Button1" runat="server" Text="���ݿ�=��Ӳ��" OnClick="Button1_Click" />
        &nbsp;<asp:Button ID="Button2" runat="server" Text="Ӳ��=�����ݿ�" OnClick="Button2_Click" />
       
        &nbsp;&nbsp;&nbsp;<br />ÿ��ת������������
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox><br />
        <br />
        <br />
        <asp:Button ID="Button3" runat="server" Text="�ƶ��ļ���ʱ���Ӧ��Ŀ¼��" OnClick="Button3_Click" />
        ��Ҫ������savepahtmodeΪ��Ҫת���ĸ�ʽ��<br />
        <br />
        <asp:Label ID="Label1" runat="server" Text="�ȴ�ת��������ʼ" Width="328px"></asp:Label><br />
        <br />
        ˵����
        <br />
        1 ��ת��ǰ����ϵͳ���ã���Ӳ�̴洢Ŀ¼�Ķ�дȨ�޷���� aspnet�û�<br />
        2 ת��ǰ�뱸�����ݿ�<br />
        3 ��ת����Ҫ�ķѴ�����ϵͳ��Դ���벻Ҫ�ڹ���ʱ�����˲�������Ӱ�������û���ʹ��<br />
        4 Ϊ�����������Դ���㣬���趨ÿ��ת������������ҳ����Զ�ˢ��ֱ��ת����ɡ�
        </div>
    </form>
</body>
</html>
