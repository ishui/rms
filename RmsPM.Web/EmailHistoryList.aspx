<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmailHistoryList.aspx.cs" Inherits="RmsPM.Web.EmailHistoryList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>邮件发送历史记录</title>
    <link href="./Images/index.css" rel="stylesheet" type="text/css" />
    <link href="./Images/infra.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="frmEmailHistory" runat="server">
    <div id="divEmailHistory" runat="server">
        <asp:GridView id="gvEmailHistoryList" runat="server" PageSize="100" CssClass="list" CellPadding="0" GridLines="Horizontal"
         Width="100%" AllowPaging="True" AutoGenerateColumns="False" DataKeyField="EmailHistoryCode">
        <FooterStyle CssClass="list-title"></FooterStyle>
        <HeaderStyle CssClass="list-title"></HeaderStyle>
        <Columns>
            <asp:TemplateField Visible="False" HeaderText="EmailHistoryCode" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container, "DataItem.EmailHistoryCode") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="邮件类型" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container, "DataItem.EmailTypeCN") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="发件人" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container, "DataItem.Sender") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="收件人" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container, "DataItem.Receiver") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="发件日期" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container, "DataItem.SendDate") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="邮件标题" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container, "DataItem.EmailTitle") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="邮件内容" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container, "DataItem.EmailContent") %>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        
        </asp:GridView>
    </div>
    </form>
</body>
</html>
