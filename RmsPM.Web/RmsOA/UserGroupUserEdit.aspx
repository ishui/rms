<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserGroupUserEdit.aspx.cs"
    Inherits="RmsOA_UserGroupUserEdit" %>

<%@ Register Src="../UserControls/inputuser.ascx" TagName="inputuser" TagPrefix="uc1" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>分组用户管理</title>
    <link href="../Images/index.css" type="text/css" rel="stylesheet" />

    <script type="text/javascript" language="javascript" src="../Rms.js"></script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:MultiView ID="MultiView1" runat="server">
            <asp:View ID="View1" runat="server">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td background="../images/topic_bg.gif" class="topic" style="height: 25px; text-align: center;">
                            个人办公>分组名称管理</td>
                    </tr>
                    <tr>
                        <td class="tools-area" style="width: 100%;" valign="top">
                            <img align="absMiddle" src="../images/btn_li.gif" />
                            <asp:Button runat="server" ID="AddGroup" CssClass="submit" Text="创建新分组" OnClick="AddGroup_Click" />
                        </td>
                    </tr>
                </table>
                <asp:GridView ID="GridView2" ShowHeader="true" ShowFooter="true" DataKeyNames="Code"
                    runat="server" Width="100%" AutoGenerateColumns="False" DataSourceID="ObjectDataSource2"
                    OnRowUpdated="GridView2_RowUpdated" OnRowDeleted="GridView2_RowDeleted">
                    <HeaderStyle HorizontalAlign="Center" CssClass="list-title" />
                    <RowStyle HorizontalAlign="Center" />
                    <FooterStyle HorizontalAlign="Center" />
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <%#Eval("GroupName") %>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="tbxGroupName" Text='<%#Bind("GroupName") %>' runat="server" CssClass="input">
                                </asp:TextBox>
                            </EditItemTemplate>
                            <HeaderTemplate>
                                分组名称
                            </HeaderTemplate>
                            <FooterTemplate>
                                <asp:TextBox runat="server" ID="tbxGroupNameAdd" CssClass="input"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <%#string.Format("{0:yyyy年MM月dd日}",Eval("CreateTime")) %>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <%#string.Format("{0:yyyy年MM月dd日}", Eval("CreateTime"))%>
                            </EditItemTemplate>
                            <HeaderTemplate>
                                创建日期
                            </HeaderTemplate>
                            <FooterTemplate>
                                <%#string.Format("{0:yyyy年MM月dd日}",DateTime.Now)%>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                操作
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:LinkButton CommandName="Edit" ID="btnEditGroup" runat="server" Text="编辑">
                                </asp:LinkButton>
                                &nbsp;
                                <asp:LinkButton CommandName="Delete" ID="btnDelGroup" runat="server" OnClientClick="return window.confirm('确认删除吗？');" Text="删除"></asp:LinkButton>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:LinkButton CommandName="UpDate" ID="btnUpdateGroup" runat="server" Text="更新" OnClientClick="return window.confirm('确认保存修改吗？');" />
                                &nbsp;
                                <asp:LinkButton CommandName="Cancel" ID="btnCancelGroup" runat="server" Text="取消" />
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:LinkButton ID="btnAddGroup" runat="server" Text="添加" OnClick="btnAddGroup_Click" />
                                &nbsp;
                                <asp:LinkButton ID="btnCancelAddGroup" runat="server" Text="取消" OnClick="btnCancelAddGroup_Click" />
                            </FooterTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="list">
                            <tr>
                                <td class="list-title" style="text-align:center;">
                                    分组名称</td>
                                <td class="list-title" style="text-align:center;">
                                    创建日期</td>
                                <td class="list-title" style="text-align:center;">
                                    操作</td>
                            </tr>
                            <tr>
                                <td style="text-align:center;">
                                    <asp:TextBox ID="tbxGroupName" runat="server" CssClass="input"></asp:TextBox>
                                </td>
                                <td style="text-align:center;">
                                    <%# string.Format("{0:yyyy年MM月dd日}",DateTime.Now) %>
                                </td>
                                <td style="text-align:center;">
                                    <asp:LinkButton ID="btnAddGroup" runat="server" Text="添加" OnClick="btnAddGroup_Click1" />
                                </td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                </asp:GridView>
                <asp:ObjectDataSource StartRowIndexParameterName="StartRecord" SortParameterName="SortColumns"
                    MaximumRowsParameterName="MaxRecords" TypeName="RmsOA.BFL.UserHelpGroupBFL"
                    ID="ObjectDataSource2" runat="server" OldValuesParameterFormatString="original_{0}"
                    SelectMethod="GetUserHelpGroupList" DataObjectTypeName="RmsOA.MODEL.UserHelpGroupModel"
                    DeleteMethod="Delete" InsertMethod="Insert" UpdateMethod="Update">
                    <SelectParameters>
                        <asp:Parameter Name="SortColumns" Type="String" />
                        <asp:Parameter DefaultValue="0" Name="StartRecord" Type="Int32" />
                        <asp:Parameter DefaultValue="-1" Name="MaxRecords" Type="Int32" />
                        <asp:Parameter Name="CodeEqual" Type="Int32" />
                        <asp:Parameter Name="GroupNameEqual" Type="String" />
                        <asp:Parameter Name="UserCodeEqual" Type="String" />
                        <asp:Parameter Name="CreateTimeEqual" Type="DateTime" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </asp:View>
            <asp:View ID="View2" runat="server">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td background="../images/topic_bg.gif" class="topic" style="height: 25px; text-align: center;">
                            个人办公>人员分组><% WriteGroupName(); %></td>
                    </tr>
                    <tr>
                        <td class="tools-area" style="width: 100%;" valign="top">
                            <img align="absMiddle" src="../images/btn_li.gif" />
                            <asp:Button runat="server" ID="AddUser" CssClass="submit" Text="添加用户" OnClick="AddUser_Click" />
                        </td>
                    </tr>
                </table>
                <asp:GridView ShowFooter="true" ID="GridView1" DataKeyNames="Code" runat="server" Width="100%" AutoGenerateColumns="False"
                    DataSourceID="ObjectDataSource1" OnRowDeleted="GridView1_RowDeleted" OnRowUpdated="GridView1_RowUpdated">
                    <HeaderStyle HorizontalAlign="Center" CssClass="list-title" />
                    <RowStyle HorizontalAlign="Center" />
                    <FooterStyle HorizontalAlign="Center" />
                    <Columns>
                        <asp:TemplateField HeaderText="用户姓名">
                            <ItemTemplate>
                                <%# RmsPM.BLL.SystemRule.GetUserName((string)Eval("UserCode"))%>
                            </ItemTemplate>
                            <EditItemTemplate>
                           <uc1:inputuser ID="Inputuser1" runat="server" Value='<%#Bind("UserCode") %>' />
                            </EditItemTemplate>
                            <FooterTemplate>
                            <uc1:inputuser ID="InputuserAdd" runat="server" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="所属分组">
                            <ItemTemplate>
                                <%WriteGroupName(); %>
                            </ItemTemplate>
                            <EditItemTemplate>
                             <%WriteGroupName(); %>
                            </EditItemTemplate>
                            <FooterTemplate>
                             <%WriteGroupName(); %>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                操作
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:LinkButton CommandName="Edit" ID="btnEditUser" runat="server" Text="编辑">
                                </asp:LinkButton>
                                &nbsp;
                                <asp:LinkButton CommandName="Delete" ID="btnDelUser" runat="server" OnClientClick="return window.confirm('确认删除吗？');" Text="删除"></asp:LinkButton>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:LinkButton CommandName="UpDate" ID="btnUpdateUser" runat="server" Text="更新" OnClientClick="return window.confirm('确认保存修改吗？');" />
                                &nbsp;
                                <asp:LinkButton CommandName="Cancel" ID="btnCancelUser" runat="server" Text="取消" />
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:LinkButton ID="btnAddUser" runat="server" Text="添加" OnClick="btnAddUser_Click" />
                                &nbsp;
                                <asp:LinkButton ID="btnCancelAddGroup" runat="server" Text="取消" OnClick="btnCancelAddUser_Click" />
                            </FooterTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="list" style="text-align:center;">
                            <tr>
                                <td class="list-title">
                                   用户姓名 </td>
                                <td class="list-title">
                                   添加日期 </td>
                                <td class="list-title">
                                    操作</td>
                            </tr>
                            <tr>
                                <td>
                                 <uc1:inputuser ID="InputuserAdd" runat="server" />  
                                </td>
                                <td>
                                    <%# string.Format("{0:yyyy年MM月dd日}",DateTime.Now) %>
                                </td>
                                <td>
                                    <asp:LinkButton ID="btnAddUser" runat="server" Text="添加" OnClick="btnEMPAddUser_Click" />
                                </td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                </asp:GridView>
                <asp:ObjectDataSource StartRowIndexParameterName="StartRecord" SortParameterName="SortColumns"
                    MaximumRowsParameterName="MaxRecords" TypeName="RmsOA.BFL.UserHelpUserBFL"
                    ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}"
                    SelectMethod="GetUserHelpUserList" DataObjectTypeName="RmsOA.MODEL.UserHelpUserModel"
                    DeleteMethod="Delete" InsertMethod="Insert" UpdateMethod="Update">
                    <SelectParameters>
                        <asp:Parameter Name="SortColumns" Type="String" />
                        <asp:Parameter DefaultValue="0" Name="StartRecord" Type="Int32" />
                        <asp:Parameter DefaultValue="-1" Name="MaxRecords" Type="Int32" />
                        <asp:Parameter Name="CodeEqual" Type="Int32" />
                        <asp:QueryStringParameter Name="GroupCodeEqual" QueryStringField="Code" Type="Int32" />
                        <asp:Parameter Name="UserCodeEqual" Type="String" />
                        <asp:Parameter Name="AddDateEqual" Type="DateTime" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </asp:View>
        </asp:MultiView>
    </form>
</body>
</html>
