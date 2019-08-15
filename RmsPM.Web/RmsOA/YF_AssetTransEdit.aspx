<%@ Page Language="C#" AutoEventWireup="true" CodeFile="YF_AssetTransEdit.aspx.cs"
    Inherits="RmsOA_YF_AssetTransEdit" %>
    

<%@ Register Assembly="AspWebControl" Namespace="AspWebControl" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head  runat="server">
    <link href="../Images/index.css" rel="stylesheet" type="text/css" />

    <script language="javascript" src="../Rms.js" type="text/javascript"></script>

    <title>�ʲ���Ϣ�鿴</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div>
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td background="../images/topic_bg.gif" class="topic" style="height: 25px; text-align: center;">
                            �̶��ʲ�/�ʲ�����</td>
                    </tr>
                </table>
            </div>
        </div>
        <asp:FormView ID="FormView1" runat="server" DataSourceID="ObjectDataSource1" Width="100%" DataKeyNames="Code" OnDataBound="FormView1_DataBound" OnItemDeleted="FormView1_ItemDeleted" OnItemInserted="FormView1_ItemInserted" OnItemInserting="FormView1_ItemInserting" OnItemUpdated="FormView1_ItemUpdated">
            <EditItemTemplate>
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td class="tools-area" style="width: 100%; height: 25px;" valign="top">
                            <img align="absMiddle" alt="" src="../images/btn_li.gif">
                            <asp:Button ID="UpdateButton" runat="server" CausesValidation="True" CommandName="UpDate"
                                CssClass="button" Text="����" />
                            <asp:Button ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                                CssClass="button" Text="ȡ��" />
                            <input class="button" onclick="self.close()" type="button" value="�ر�" />
                        </td>
                    </tr>
                </table>
                <table border="0" cellpadding="0" cellspacing="0" class="form" width="100%">
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            �豸���
                        </td>
                        <td>
                            <asp:Label ID="CodeNOLabel" runat="server"></asp:Label>
                        </td>
                        <td class="form-item" style="width: 100px;">
                            ԭ���ڵ�
                        </td>
                        <td>
                            <asp:TextBox ID="PreDeptTextBox" runat="server" CssClass="input" Text='<%# Bind("PreDept") %>'>
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            �����ڵ�
                        </td>
                        <td>
                            <asp:TextBox ID="PostDeptTextBox" runat="server" CssClass="input" Text='<%# Bind("PostDept") %>'>
                            </asp:TextBox>
                        </td>
                        <td class="form-item" style="width: 100px;">
                            ʱ��
                        </td>
                        <td>
                            <cc1:Calendar ID="TransTime" runat="server" CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                                Value='<%# Bind("TransferTime") %>'>
                            </cc1:Calendar>
                        </td>
                    </tr>
                </table>
            </EditItemTemplate>
            <InsertItemTemplate>
                <table id="Table1" class="table" width="100%">
                    <tr>
                        <td class="tools-area" width="16">
                            <img align="absMiddle" src="../images/btn_li.gif" /></td>
                        <td class="tools-area">
                            <asp:Button ID="btnSave" runat="server" CommandName="Insert" CssClass="button" Text=" ���� " />
                            <input id="btnClose" runat="server" class="button" name="btnClose" onclick="javascript:window.close();"
                                type="button" value=" �ر� " />
                        </td>
                    </tr>
                </table>
                <table border="0" cellpadding="0" cellspacing="0" class="form" width="100%">
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            �豸���
                        </td>
                        <td>
                            <asp:Label ID="CodeNOLabel" runat="server"></asp:Label>
                        </td>
                        <td class="form-item" style="width: 100px;">
                            ԭ���ڵ�
                        </td>
                        <td>
                            <asp:TextBox ID="PreDeptTextBox" CssClass="input" runat="server" Text='<%# Bind("PreDept") %>'>
                            </asp:TextBox> 
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            �����ڵ�
                        </td>
                        <td>
                            <asp:TextBox CssClass="input" ID="PostDeptTextBox" runat="server" Text='<%# Bind("PostDept") %>'>
                            </asp:TextBox>   
                        </td>
                        <td class="form-item" style="width: 100px;">
                            ʱ��
                        </td>
                        <td>
                            <cc1:Calendar ID="TransTime" runat="server" CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                                Value='<%# Bind("TransferTime") %>'>
                            </cc1:Calendar>
                        </td>
                    </tr>
                </table>                 
            </InsertItemTemplate>
            <ItemTemplate>
                <table id="Table3" class="table" width="100%">
                    <tr>
                        <td class="tools-area" width="16">
                            <img align="absMiddle" src="../images/btn_li.gif" /></td>
                        <td class="tools-area">
                            <asp:Button ID="EditButton" runat="server" CommandName="Edit" CssClass="button" Text=" �޸� " />
                            <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" CssClass="button"
                                Text=" ɾ�� " />
                            <input id="btnClose" runat="server" class="button" name="btnClose" onclick="javascript:window.close();"
                                type="button" value=" �ر� " />
                        </td>
                    </tr>
                </table>
                <table border="0" cellpadding="0" cellspacing="0" width="100%" class="form">
                    <tr>
                        <td style="width: 100px;" class="form-item"> �豸���
                        </td>
                        <td>
                        <asp:Label runat="server" ID="CodeNOLabel"></asp:Label>
                        </td>
                        <td class="form-item" style="width: 100px;">  ԭ���ڵ�
                        </td>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("PreDept") %>'></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">�����ڵ�
                        </td>
                        <td>
                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("PostDept") %>'></asp:Label>
                        </td>
                        <td class="form-item" style="width: 100px;"> ʱ��
                        </td>
                        <td>
                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("TransferTime") %>'>
                            </asp:Label>
                        </td>
                    </tr>
                </table>
                </ItemTemplate>
        </asp:FormView>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" TypeName="RmsOA.BFL.YF_AssetTransferBFL"
            OldValuesParameterFormatString="original_{0}" SelectMethod="GetYF_AssetTransferListOne" DataObjectTypeName="RmsOA.MODEL.YF_AssetTransferModel" DeleteMethod="Delete" InsertMethod="Insert" UpdateMethod="Update" OnInserted="ObjectDataSource1_Inserted">
            <SelectParameters>
                <asp:QueryStringParameter Name="Code" QueryStringField="Code" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </form>
</body>
</html>
