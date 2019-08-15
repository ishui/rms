<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GK_OA_ArchivesCopy.ascx.cs"
    Inherits="WorkFlowOperation_GK_OA_ArchivesCopy" %>
<%@ Register TagPrefix="uc1" TagName="AttachMentList" Src="../UserControls/AttachMentList.ascx" %>
<%@ Register TagPrefix="uc2" TagName="InputUser" Src="../UserControls/InputUser.ascx" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<asp:FormView ID="FormView1" runat="server" DataKeyNames="Code" DataSourceID="ObjectDataSource1"
    Width="100%" OnDataBound="FormView1_DataBound">
    <EditItemTemplate>
    </EditItemTemplate>
    <InsertItemTemplate>
    </InsertItemTemplate>
    <ItemTemplate>
        <table border="0" cellpadding="0" cellspacing="0"  width="100%">
            <tr>
                <td class="blackbordertd" style="width: 20%">
                    文件编号：</td>
                <td class="blackbordertdpaddingcontent">
                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("FileCode") %>'></asp:Label></td>
                <td class="blackbordertd" style="width: 20%">
                    文件名称：</td>
                <td class="blackbordertdpaddingcontent">
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("FileName") %>'></asp:Label></td>
            </tr>
            <tr>
                <td class="blackbordertd" style="width: 20%">
                    部门：</td>
                <td class="blackbordertdpaddingcontent">
                    <asp:Label ID="UnitLabel" runat="server" Text='<%# Bind("UnitCode") %>'></asp:Label></td>
                <td class="blackbordertd" style="width: 20%">
                    复印/借用人：</td>
                <td class="blackbordertdpaddingcontent">
                    <asp:Label ID="Label4" runat="server" Text='<%# RmsPM.BLL.SystemRule.GetUserName((string)Eval("UsePerson")) %> '></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="blackbordertd" style="width: 20%">
                    登记日期：</td>
                <td class="blackbordertdpaddingcontent">
                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("RegiesterDate") %>'></asp:Label></td>
                <td class="blackbordertd" style="width: 20%">
                    预计归还日期：</td>
                <td class="blackbordertdpaddingcontent">
                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("ReturnDate") %>'></asp:Label></td>
            </tr>
            <tr>
                <td class="blackbordertd" style="width: 20%">
                    类型：</td>
                <td class="blackbordertdpaddingcontent">
                    <asp:Label ID="ArchivesTypeLabel" runat="server" Text='<%# Bind("ArchivesType") %>'></asp:Label></td>
            </tr>
            <tr>
                <td class="blackbordertd" style="width: 20%">
                    复印/借用原因：</td>
                <td colspan="3" class="blackbordertdpaddingcontent">
                    <asp:Label ID="Label10" runat="server" Text='<%# Bind("Reason") %>'></asp:Label></td>
            </tr>
        </table>
    </ItemTemplate>
</asp:FormView>
<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="RmsOA.MODEL.GK_OA_ArchivesCopyModel"
    DeleteMethod="Delete" InsertMethod="Insert" OnSelected="ObjectDataSource1_Selected"
    SelectMethod="GetGK_OA_ArchivesCopyListOne" TypeName="RmsOA.BFL.GK_OA_ArchivesCopyBFL"
    UpdateMethod="Update">
    <SelectParameters>
        <asp:QueryStringParameter Name="Code" QueryStringField="ArchivesCopyCode" Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>
