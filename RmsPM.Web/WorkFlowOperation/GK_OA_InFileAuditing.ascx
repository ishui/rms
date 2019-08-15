<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GK_OA_InFileAuditing.ascx.cs"
    Inherits="WorkFlowOperation_GK_OA_InFileAuditing" %>
    
<%@ Register TagPrefix="uc1" TagName="AttachMentList" Src="../UserControls/AttachMentList.ascx" %>
<%@ Register TagPrefix="uc2" TagName="InputUser" Src="../UserControls/InputUser.ascx" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>

<table  cellspacing="0" cellpadding="0" width="100%" border="0">
    <tr>
        <td valign="top">
            <asp:FormView ID="FormView1" runat="server" DataSourceID="ObjectDataSource1" Width="100%"
                OnItemInserting="FormView1_ItemInserting"
                DataKeyNames="Code">
                <EditItemTemplate>
                    <table border="0" cellpadding="0" cellspacing="0" class="form" width="100%">
                        <tr>
                            <td class="blackbordertd" style="width: 20%">
                                文件编号：</td>
                            <td class="blackbordertdpaddingcontent">
                                <asp:TextBox ID="SystemnCodeTextBox" runat="server" CssClass="input" Text='<%# Bind("SystemCode") %>'></asp:TextBox></td>
                            <td class="blackbordertd" style="width: 20%">
                                标识序号：</td>
                            <td class="blackbordertdpaddingcontent">
                                <asp:TextBox ID="FileCodeTextBox" runat="server" CssClass="input" Text='<%# Bind("FileCode") %>'></asp:TextBox></td>
                        </tr>
                    </table>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <table border="0" cellpadding="0" cellspacing="0" class="form" width="100%">
                        <tr>
                            <td class="blackbordertd" style="width: 20%">
                                文件编号：</td>
                            <td class="blackbordertdpaddingcontent">
                                <asp:TextBox ID="SystemnCodeTextBox" runat="server" CssClass="input" Text='<%# Bind("SystemCode") %>'></asp:TextBox></td>
                            <td class="blackbordertd" style="width: 20%">
                                标识序号：</td>
                            <td class="blackbordertdpaddingcontent">
                                <asp:TextBox ID="FileCodeTextBox" runat="server" CssClass="input" Text='<%# Bind("FileCode") %>'></asp:TextBox></td>
                        </tr>
                    </table>
                </InsertItemTemplate>
                <ItemTemplate>
                    <table border="0" cellpadding="0" cellspacing="0" class="form" width="100%">
                        <tr>
                            <td class="blackbordertd" style="width: 20%">
                                文件编号：</td>
                            <td class="blackbordertdpaddingcontent">
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("SystemCode") %>'></asp:Label></td>
                            <td class="blackbordertd" style="width: 20%">
                                标识序号：</td>
                            <td class="blackbordertdpaddingcontent">
                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("FileCode") %>'></asp:Label></td>
                        </tr>
                     </table>
                </ItemTemplate>
            </asp:FormView>
        </td>
    </tr>
</table>
<asp:FormView ID="FormView2" runat="server" DataKeyNames="Code" DataSourceID="ObjectDataSource2"
    Width="100%">
    <EditItemTemplate>
    </EditItemTemplate>
    <InsertItemTemplate>
    </InsertItemTemplate>
    <ItemTemplate>
        <table border="0" cellpadding="0" cellspacing="0" class="form" width="100%">
            <tr>
                <td class="blackbordertd" style="width: 20%">
                    收文编号：</td>
                <td class="blackbordertdpaddingcontent">
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("InFileCode") %>'></asp:Label></td>
                <td class="blackbordertd" style="width: 20%">
                    原文字号：</td>
                <td class="blackbordertdpaddingcontent">
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("OriginalFileCode") %>'></asp:Label></td>
            </tr>
            <tr>
                <td class="blackbordertd" style="width: 20%">
                    文件类别：</td>
                <td class="blackbordertdpaddingcontent">
                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("FileType") %>'></asp:Label></td>
                <td class="blackbordertd" style="width: 20%">
                    登记时间：</td>
                <td class="blackbordertdpaddingcontent">
                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("InFileDate") %>'></asp:Label></td>
            </tr>
            <tr>
                <td class="blackbordertd" style="width: 20%">
                    份数：</td>
                <td class="blackbordertdpaddingcontent">
                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("FileNumber") %>'></asp:Label></td>
                <td style="width: 20%">
                </td>
                <td class="blackbordertdpaddingcontent">
                </td>
            </tr>
            <tr>
                <td class="blackbordertd" style="width: 20%">
                    备注：</td>
                <td colspan="3" class="blackbordertdpaddingcontent">
                    <asp:Label ID="Label7" runat="server" Text='<%# Bind("Remark") %>'></asp:Label></td>
            </tr>
        </table>
    </ItemTemplate>
</asp:FormView>
<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="RmsOA.MODEL.GK_OA_InFileAuditingMainModel"
    DeleteMethod="Delete" InsertMethod="Insert" OnSelected="ObjectDataSource1_Selected"
    SelectMethod="GetGK_OA_InFileAuditingMainListOne" TypeName="RmsOA.BFL.GK_OA_InFileAuditingMainBFL"
    UpdateMethod="Update" OnInserted="ObjectDataSource1_Inserted" >
    <SelectParameters>
        <asp:QueryStringParameter Name="Code" QueryStringField="Code" Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>
<asp:ObjectDataSource ID="ObjectDataSource2" runat="server" DataObjectTypeName="RmsOA.MODEL.GK_OA_InFileRegisterModel"
    DeleteMethod="Delete" InsertMethod="Insert" SelectMethod="GetGK_OA_InFileRegisterListOne"
    TypeName="RmsOA.BFL.GK_OA_InFileRegisterBFL" UpdateMethod="Update" >
    <SelectParameters>
        <asp:QueryStringParameter Name="Code" QueryStringField="InFileRegisterCode" Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>
