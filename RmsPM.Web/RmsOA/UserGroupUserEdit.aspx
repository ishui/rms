<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserGroupUserEdit.aspx.cs"
    Inherits="RmsOA_UserGroupUserEdit" %>

<%@ Register Src="../UserControls/inputuser.ascx" TagName="inputuser" TagPrefix="uc1" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>�����û�����</title>
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
                            ���˰칫>�������ƹ���</td>
                    </tr>
                    <tr>
                        <td class="tools-area" style="width: 100%;" valign="top">
                            <img align="absMiddle" src="../images/btn_li.gif" />
                            <asp:Button runat="server" ID="AddGroup" CssClass="submit" Text="�����·���" OnClick="AddGroup_Click" />
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
                                ��������
                            </HeaderTemplate>
                            <FooterTemplate>
                                <asp:TextBox runat="server" ID="tbxGroupNameAdd" CssClass="input"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <%#string.Format("{0:yyyy��MM��dd��}",Eval("CreateTime")) %>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <%#string.Format("{0:yyyy��MM��dd��}", Eval("CreateTime"))%>
                            </EditItemTemplate>
                            <HeaderTemplate>
                                ��������
                            </HeaderTemplate>
                            <FooterTemplate>
                                <%#string.Format("{0:yyyy��MM��dd��}",DateTime.Now)%>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                ����
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:LinkButton CommandName="Edit" ID="btnEditGroup" runat="server" Text="�༭">
                                </asp:LinkButton>
                                &nbsp;
                                <asp:LinkButton CommandName="Delete" ID="btnDelGroup" runat="server" OnClientClick="return window.confirm('ȷ��ɾ����');" Text="ɾ��"></asp:LinkButton>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:LinkButton CommandName="UpDate" ID="btnUpdateGroup" runat="server" Text="����" OnClientClick="return window.confirm('ȷ�ϱ����޸���');" />
                                &nbsp;
                                <asp:LinkButton CommandName="Cancel" ID="btnCancelGroup" runat="server" Text="ȡ��" />
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:LinkButton ID="btnAddGroup" runat="server" Text="���" OnClick="btnAddGroup_Click" />
                                &nbsp;
                                <asp:LinkButton ID="btnCancelAddGroup" runat="server" Text="ȡ��" OnClick="btnCancelAddGroup_Click" />
                            </FooterTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="list">
                            <tr>
                                <td class="list-title" style="text-align:center;">
                                    ��������</td>
                                <td class="list-title" style="text-align:center;">
                                    ��������</td>
                                <td class="list-title" style="text-align:center;">
                                    ����</td>
                            </tr>
                            <tr>
                                <td style="text-align:center;">
                                    <asp:TextBox ID="tbxGroupName" runat="server" CssClass="input"></asp:TextBox>
                                </td>
                                <td style="text-align:center;">
                                    <%# string.Format("{0:yyyy��MM��dd��}",DateTime.Now) %>
                                </td>
                                <td style="text-align:center;">
                                    <asp:LinkButton ID="btnAddGroup" runat="server" Text="���" OnClick="btnAddGroup_Click1" />
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
                            ���˰칫>��Ա����><% WriteGroupName(); %></td>
                    </tr>
                    <tr>
                        <td class="tools-area" style="width: 100%;" valign="top">
                            <img align="absMiddle" src="../images/btn_li.gif" />
                            <asp:Button runat="server" ID="AddUser" CssClass="submit" Text="����û�" OnClick="AddUser_Click" />
                        </td>
                    </tr>
                </table>
                <asp:GridView ShowFooter="true" ID="GridView1" DataKeyNames="Code" runat="server" Width="100%" AutoGenerateColumns="False"
                    DataSourceID="ObjectDataSource1" OnRowDeleted="GridView1_RowDeleted" OnRowUpdated="GridView1_RowUpdated">
                    <HeaderStyle HorizontalAlign="Center" CssClass="list-title" />
                    <RowStyle HorizontalAlign="Center" />
                    <FooterStyle HorizontalAlign="Center" />
                    <Columns>
                        <asp:TemplateField HeaderText="�û�����">
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
                        <asp:TemplateField HeaderText="��������">
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
                                ����
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:LinkButton CommandName="Edit" ID="btnEditUser" runat="server" Text="�༭">
                                </asp:LinkButton>
                                &nbsp;
                                <asp:LinkButton CommandName="Delete" ID="btnDelUser" runat="server" OnClientClick="return window.confirm('ȷ��ɾ����');" Text="ɾ��"></asp:LinkButton>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:LinkButton CommandName="UpDate" ID="btnUpdateUser" runat="server" Text="����" OnClientClick="return window.confirm('ȷ�ϱ����޸���');" />
                                &nbsp;
                                <asp:LinkButton CommandName="Cancel" ID="btnCancelUser" runat="server" Text="ȡ��" />
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:LinkButton ID="btnAddUser" runat="server" Text="���" OnClick="btnAddUser_Click" />
                                &nbsp;
                                <asp:LinkButton ID="btnCancelAddGroup" runat="server" Text="ȡ��" OnClick="btnCancelAddUser_Click" />
                            </FooterTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="list" style="text-align:center;">
                            <tr>
                                <td class="list-title">
                                   �û����� </td>
                                <td class="list-title">
                                   ������� </td>
                                <td class="list-title">
                                    ����</td>
                            </tr>
                            <tr>
                                <td>
                                 <uc1:inputuser ID="InputuserAdd" runat="server" />  
                                </td>
                                <td>
                                    <%# string.Format("{0:yyyy��MM��dd��}",DateTime.Now) %>
                                </td>
                                <td>
                                    <asp:LinkButton ID="btnAddUser" runat="server" Text="���" OnClick="btnEMPAddUser_Click" />
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
