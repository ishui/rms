<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GK_OA_FileChange.ascx.cs"
    Inherits="WorkFlowOperation_GK_OA_FileChange" %>
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
                    归档编号：</td>
                <td class="blackbordertdpaddingcontent">
                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("SystemCode") %>'></asp:Label></td>
                <td class="blackbordertd" style="width: 20%">
                    标识序号：</td>
                <td class="blackbordertdpaddingcontent">
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("FileSystemCode") %>'></asp:Label></td>
            </tr>
            <tr>
                <td class="blackbordertd" style="width: 20%">
                    文件名称：</td>
                <td class="blackbordertdpaddingcontent">
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("FileName") %>'></asp:Label></td>
                <td class="blackbordertd" style="width: 20%">
                    文件编号：</td>
                <td class="blackbordertdpaddingcontent">
                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("FileCode") %>'></asp:Label></td>
            </tr>
            <tr>
                <td class="blackbordertd" style="width: 20%">
                    原版本号：</td>
                <td class="blackbordertdpaddingcontent">
                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("OldVersionNumber") %>'></asp:Label></td>
                <td class="blackbordertd" style="width: 20%">
                    现版本号：</td>
                <td class="blackbordertdpaddingcontent">
                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("NewVersionNumber") %>'></asp:Label></td>
            </tr>
            <tr>
                <td class="blackbordertd" style="width: 20%">
                    申请部门：</td>
                <td class="blackbordertdpaddingcontent">
                    <asp:Label ID="UnitLabel" runat="server" Text='<%# Bind("UnitCode") %>'></asp:Label></td>
                <td class="blackbordertd" style="width: 20%">
                    提交日期：</td>
                <td class="blackbordertdpaddingcontent">
                    <asp:Label ID="Label8" runat="server" Text='<%# Bind("SubmitDate") %>'></asp:Label></td>
            </tr>
            <tr>
                <td class="blackbordertd" style="width: 20%">
                    更改原因：</td>
                <td colspan="3" class="blackbordertdpaddingcontent">
                    <asp:Label ID="Label11" runat="server" Text='<%# Bind("ChangeReason") %>'></asp:Label></td>
            </tr>
            <tr>
                <td class="blackbordertd" style="width: 20%">
                    原文件内容：</td>
                <td colspan="3" class="blackbordertdpaddingcontent">
                    <asp:Label ID="Label9" runat="server" Text='<%# Bind("OldContext") %>'></asp:Label></td>
            </tr>
            <tr>
                <td class="blackbordertd" style="width: 20%">
                    更改后文件内容：</td>
                <td colspan="3" class="blackbordertdpaddingcontent">
                    <asp:Label ID="Label10" runat="server" Text='<%# Bind("NewContext") %>'></asp:Label></td>
            </tr>
        </table>
    </ItemTemplate>
</asp:FormView>
<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="RmsOA.MODEL.GK_OA_FileChangeModel"
    DeleteMethod="Delete" InsertMethod="Insert" OnSelected="ObjectDataSource1_Selected"
    SelectMethod="GetGK_OA_FileChangeListOne" TypeName="RmsOA.BFL.GK_OA_FileChangeBFL"
    UpdateMethod="Update">
    <SelectParameters>
        <asp:QueryStringParameter Name="Code" QueryStringField="FileChangeCode" Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>
