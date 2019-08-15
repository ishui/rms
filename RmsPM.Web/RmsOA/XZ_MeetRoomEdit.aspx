<%@ Page Language="C#" AutoEventWireup="true" CodeFile="XZ_MeetRoomEdit.aspx.cs"
    Inherits="RmsOA_XZ_MeetRoomEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../Images/index.css" type="text/css" rel="stylesheet" />

    <script type="text/javascript" language="javascript" src="../Rms.js"></script>

    <title>�����ҹ���</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td background="../images/topic_bg.gif" class="topic" style="height: 25px; text-align: center;">
                        �����칫>�����ҹ���</td>
                </tr>
            </table>
        </div>
        <asp:FormView ID="FormView1" runat="server" DataSourceID="ObjectDataSource1" Width="100%"
            DataKeyNames="Code" OnItemDeleted="FormView1_ItemDeleted" OnItemInserted="FormView1_ItemInserted"
            OnItemUpdated="FormView1_ItemUpdated" OnDataBound="FormView1_DataBound">
            <EditItemTemplate>
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td class="tools-area" width="16">
                            <img align="absMiddle" src="../images/btn_li.gif" /></td>
                        <td class="tools-area">
                            <asp:Button ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update"
                                CssClass="button" Text="����" OnClientClick="return window.confirm('ȷ��������');"></asp:Button>
                            <asp:Button ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                                CssClass="button" Text="ȡ��"></asp:Button>
                            <input type="button" class="button" value="�ر�" onclick="opener=null;window.close();" />
                        </td>
                    </tr>
                </table>
                <table border="0" width="100%" cellpadding="0" cellspacing="0" class="form">
                    <tr>
                        <td class="form-item" style="width: 120px;">
                            �����ң�
                        </td>
                        <td style="width: 154px">
                            <asp:TextBox ID="RoomNameTextBox" runat="server" Text='<%# Bind("RoomName") %>' CssClass="input"></asp:TextBox>
                            <span style="color: Red">*</span>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="RoomNameTextBox"
                                ErrorMessage="����Ϊ��"></asp:RequiredFieldValidator>
                        </td>
                        <td class="form-item" style="width: 120px;">
                            �ص㣺
                        </td>
                        <td>
                            <asp:TextBox ID="PlaceTextBox" runat="server" Text='<%# Bind("Place") %>' CssClass="input"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 120px;">
                            ������
                        </td>
                        <td style="width: 154px">
                            <asp:TextBox ID="CapacityTextBox" runat="server" Text='<%# Bind("Capacity") %>' CssClass="input"></asp:TextBox>
                        </td>
                        <td class="form-item" style="width: 120px;">
                            Ӳ���豸��
                        </td>
                        <td>
                            <asp:TextBox ID="HardConditionTextBox" runat="server" Text='<%# Bind("HardCondition") %>'
                                CssClass="input"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 120px;">
                            ��ע��
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="RemarkTextBox" runat="server" Text='<%# Bind("Remark") %>' CssClass="input"
                                Height="60px" TextMode="MultiLine" Width="90%"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </EditItemTemplate>
            <InsertItemTemplate>
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td class="tools-area" width="16">
                            <img align="absMiddle" src="../images/btn_li.gif" /></td>
                        <td class="tools-area">
                            <asp:Button ID="InsertButton" CssClass="button" runat="server" CausesValidation="True"
                                CommandName="Insert" Text="���" OnClientClick="return window.confirm('ȷ�������');">
                            </asp:Button>
                            <input type="button" class="button" value="�ر�" onclick="opener=null;window.close();" />
                        </td>
                    </tr>
                </table>
                <table border="0" width="100%" cellpadding="0" cellspacing="0" class="form">
                    <tr>
                        <td class="form-item" style="width: 120px;">
                            �����ң�
                        </td>
                        <td style="width: 154px">
                            <asp:TextBox ID="RoomNameTextBox" runat="server" Text='<%# Bind("RoomName") %>' CssClass="input"></asp:TextBox>
                            <span style="color: Red">*</span>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="RoomNameTextBox"
                                ErrorMessage="����Ϊ��"></asp:RequiredFieldValidator>
                        </td>
                        <td class="form-item" style="width: 120px;">
                            �ص㣺
                        </td>
                        <td>
                            <asp:TextBox ID="PlaceTextBox" runat="server" Text='<%# Bind("Place") %>' CssClass="input"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 120px;">
                            ������
                        </td>
                        <td style="width: 154px">
                            <asp:TextBox ID="CapacityTextBox" runat="server" Text='<%# Bind("Capacity") %>' CssClass="input"></asp:TextBox>
                        </td>
                        <td class="form-item" style="width: 120px;">
                            Ӳ���豸��
                        </td>
                        <td>
                            <asp:TextBox ID="HardConditionTextBox" runat="server" Text='<%# Bind("HardCondition") %>'
                                CssClass="input"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 120px;">
                            ��ע��
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="RemarkTextBox" runat="server" Text='<%# Bind("Remark") %>' CssClass="input"
                                Height="60px" TextMode="MultiLine" Width="90%"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </InsertItemTemplate>
            <ItemTemplate>
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td class="tools-area" width="16">
                            <img align="absMiddle" src="../images/btn_li.gif" /></td>
                        <td class="tools-area">
                            <asp:Button ID="EditButton" CssClass="button" runat="server" CausesValidation="False"
                                CommandName="Edit" Text="�༭"></asp:Button>
                            <asp:Button ID="DeleteButton" CssClass="button" runat="server" CausesValidation="False"
                                CommandName="Delete" Text="ɾ��"></asp:Button>
                            <input type="button" class="button" value="�ر�" onclick="opener=null;window.close();" />
                        </td>
                    </tr>
                </table>
                <table border="0" width="100%" cellpadding="0" cellspacing="0" class="form">
                    <tr>
                        <td class="form-item" style="width: 120px;">
                            �����ң�
                        </td>
                        <td>
                            <asp:Label ID="RoomNameLabel" runat="server" Text='<%# Bind("RoomName") %>'></asp:Label>
                        </td>
                        <td class="form-item" style="width: 120px;">
                            �ص㣺
                        </td>
                        <td>
                            <asp:Label ID="PlaceLabel" runat="server" Text='<%# Bind("Place") %>'></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 120px;">
                            ������
                        </td>
                        <td>
                            <asp:Label ID="CapacityLabel" runat="server" Text='<%# Bind("Capacity") %>'></asp:Label>
                        </td>
                        <td class="form-item" style="width: 120px;">
                            Ӳ���豸��
                        </td>
                        <td>
                            <asp:Label ID="HardConditionLabel" runat="server" Text='<%# Bind("HardCondition") %>'>
                            </asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 120px;">
                            ��ע��
                        </td>
                        <td colspan="3">
                            <asp:Label ID="RemarkLabel" runat="server" Text='<%# Bind("Remark") %>'></asp:Label>
                        </td>
                    </tr>
                </table>
            </ItemTemplate>
        </asp:FormView>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetMeetRoomListOne"
            TypeName="RmsOA.BFL.MeetRoomBFL" DataObjectTypeName="RmsOA.MODEL.MeetRoomModel"
            DeleteMethod="Delete" InsertMethod="Insert" UpdateMethod="Update" OnInserted="ObjectDataSource1_Inserted">
            <SelectParameters>
                <asp:QueryStringParameter Name="Code" QueryStringField="Code" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </form>
</body>
</html>
