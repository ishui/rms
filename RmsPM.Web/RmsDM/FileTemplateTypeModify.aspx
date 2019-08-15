<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FileTemplateTypeModify.aspx.cs"
    Inherits="RmsDM_FileTemplateTypeModify" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>文档类别修改</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR" />
    <meta content="C#" name="CODE_LANGUAGE" />
    <meta content="JavaScript" name="vs_defaultClientScript" />
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
    <link href="../Images/index.css" type="text/css" rel="stylesheet" />
</head>
<body scroll="no">
    <form id="form1" runat="server">
        <div>
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td background="../images/topic_bg.gif" class="topic" style="height: 25px; text-align: center;">
                        文档类别修改</td>
                </tr>
            </table>
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="RmsDM.MODEL.FileTemplateTypeModel"
                DeleteMethod="Delete" SelectMethod="GetFileTemplateTypeListOne" TypeName="RmsDM.BFL.FileTemplateTypeBFL"
                UpdateMethod="Update">
                <SelectParameters>
                    <asp:QueryStringParameter Name="Code" QueryStringField="code" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
            &nbsp;
            <asp:FormView ID="FormView1" runat="server" DataSourceID="ObjectDataSource1" Width="100%"
                DataKeyNames="Code" OnDataBound="FormView1_DataBound">
                <EditItemTemplate>
                    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="form">
                        <tr>
                            <td style="width: 80px;" class="form-item">
                                类别路径</td>
                            <td>
                                <asp:Label ID="lblPath" Width="140px" runat="server"></asp:Label></td>
                            <td width="100" class="form-item">
                                模板类别名称：</td>
                            <td>
                                <asp:TextBox ID="FileTemplateTypeNameTextBox" runat="server" Font-Size="12px" Text='<%# Bind("FileTemplateTypeName") %>' CssClass="input"></asp:TextBox><font color="red">*<asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                                    runat="server" ErrorMessage="提醒：模版名称不能为空" ControlToValidate="FileTemplateTypeNameTextBox"></asp:RequiredFieldValidator></font></td>
                            <table width="100%" cellspacing="10">
                                <tr>
                                    <td align="center">
                                        <asp:Button ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update"
                                            Text="更新" CssClass="button" OnClick="UpdateButton_Click"></asp:Button>
                                        <asp:Button ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                                            Text="取消" CssClass="button"></asp:Button>
                                    </td>
                                </tr>
                            </table>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="form">
                        <tr>
                            <td style="width: 80px;" class="form-item">
                                类别路径</td>
                            <td>
                                <asp:Label ID="lblPath" runat="server" Width="140px"></asp:Label></td>
                            <td width="100px" class="form-item">
                                模板类别名称：</td>
                            <td>
                                &nbsp;<asp:TextBox ID="FileTemplateTypeNameTextBox" runat="server" Text='<%# Bind("FileTemplateTypeName") %>' CssClass="input"></asp:TextBox><font color="red">*<asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                    runat="server" ControlToValidate="FileTemplateTypeNameTextBox" ErrorMessage="提醒: 模版类别名称不能为空"></asp:RequiredFieldValidator></font></td>
                            <table width="100%" cellspacing="10">
                                <tr>
                                    <td align="center">
                                        <asp:Button ID="AddButton" runat="server" CausesValidation="True"
                                            Text="添加" CssClass="button" OnClick="AddButton_Click"></asp:Button>
                                        <asp:Button ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                                            Text="关闭" CssClass="button" OnClick="UpdateCancelButton_Click"></asp:Button>
                                    </td>
                                </tr>
                            </table>
                </InsertItemTemplate>
                <ItemTemplate>
                    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="form">
                        <tr>
                            <td style="width: 80px;" class="form-item">
                                类别路径</td>
                            <td style="width:200px">
                                <asp:Label ID="lblPath" runat="server"></asp:Label></td>
                            <td width="100" class="form-item">
                                模板类别名称：</td>
                            <td>
                                <asp:Label ID="FileTemplateTypeNameTextBox" runat="server" Text='<%# Bind("FileTemplateTypeName") %>'>
                                </asp:Label></td>
                            <table width="100%" cellspacing="10">
                                <tr>
                                    <td align="center">
                                        <asp:Button ID="EditButton" runat="server" CausesValidation="False" CommandName="Edit"
                                            Text="编辑" CssClass="button"></asp:Button>
                                        <asp:Button ID="DeleteButton" runat="server" CausesValidation="False"
                                            Text="作废" CssClass="button" OnClick="DeleteButton_Click"></asp:Button>
                                        <input type="button" value="关闭" class="button" onclick="self.close();" />
                                    </td>
                                </tr>
                            </table>
                </ItemTemplate>
            </asp:FormView>
        </div>
    </form>
</body>
</html>
