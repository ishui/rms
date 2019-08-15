<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GK_OA_ChangeStationNotice.ascx.cs"
    Inherits="WorkFlowOperation_GK_OA_ChangeStationNotice" %>
<%@ Register TagPrefix="uc1" TagName="AttachMentList" Src="../UserControls/AttachMentList.ascx" %>
<%@ Register TagPrefix="uc2" TagName="InputUser" Src="../UserControls/InputUser.ascx" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<asp:FormView ID="FormView1" runat="server" DataKeyNames="Code" DataSourceID="ObjectDataSource1"
    Width="100%">
    <EditItemTemplate>
    </EditItemTemplate>
    <InsertItemTemplate>
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
            <tr>
                <td class="blackbordertd" style="width: 20%">
                    姓名：</td>
                <td class="blackbordertdpaddingcontent">
                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("PersonName") %>'></asp:Label></td>
                <td class="blackbordertd" style="width: 20%">
                    性别：</td>
                <td class="blackbordertdpaddingcontent">
                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("Sex") %>'></asp:Label></td>
            </tr>
            <tr>
                <td class="blackbordertd" style="width: 20%">
                    入职时间：</td>
                <td class="blackbordertdpaddingcontent">
                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("InComDate") %>'></asp:Label></td>
                <td class="blackbordertd" style="width: 20%">
                    日期：</td>
                <td class="blackbordertdpaddingcontent">
                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("ChangeStationDate") %>'></asp:Label></td>
            </tr>
            <tr>
                <td class="blackbordertd" style="width: 20%">
                    原因：</td>
                <td colspan="3" class="blackbordertdpaddingcontent">
                    <asp:Label ID="Label7" runat="server" Text='<%# Bind("Reason") %>'></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 20%">
                </td>
                <td class="blackbordertdpaddingcontent">
                    由</td>
                <td style="width: 20%">
                    去</td>
                <td class="blackbordertdpaddingcontent">
                    备注</td>
            </tr>
            <tr>
                <td class="blackbordertd" style="width: 20%">
                    部门：</td>
                <td class="blackbordertdpaddingcontent">
                    <asp:Label ID="Label8" runat="server" Text='<%# Bind("OldUnitCode") %>'></asp:Label></td>
                <td class="blackbordertdpaddingcontent">
                    <asp:Label ID="Label9" runat="server" Text='<%# Bind("NewUnitCode") %>'></asp:Label></td>
                <td class="blackbordertdpaddingcontent">
                    <asp:Label ID="Label10" runat="server" Text='<%# Bind("Remark1") %>' Width="100%"></asp:Label></td>
            </tr>
            <tr>
                <td class="blackbordertd" style="width: 20%">
                    岗位：</td>
                <td class="blackbordertdpaddingcontent">
                    <asp:Label ID="Label11" runat="server" Text='<%# Bind("OldStation") %>'></asp:Label></td>
                <td class="blackbordertdpaddingcontent">
                    <asp:Label ID="Label12" runat="server" Text='<%# Bind("NewStation") %>'></asp:Label></td>
                <td class="blackbordertdpaddingcontent">
                    <asp:Label ID="Label13" runat="server" Text='<%# Bind("Remark2") %>' Width="100%"></asp:Label></td>
            </tr>
            <tr>
                <td class="blackbordertd" style="width: 20%">
                    岗位级别：</td>
                <td class="blackbordertdpaddingcontent">
                    <asp:Label ID="Label14" runat="server" Text='<%# Bind("OldStationLeavel") %>'></asp:Label></td>
                <td class="blackbordertdpaddingcontent">
                    <asp:Label ID="Label15" runat="server" Text='<%# Bind("NewStationLeavel") %>'></asp:Label></td>
                <td class="blackbordertdpaddingcontent">
                    <asp:Label ID="Label16" runat="server" Text='<%# Bind("Remark3") %>' Width="100%"></asp:Label></td>
            </tr>
        </table>
    </ItemTemplate>
</asp:FormView>
<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="RmsOA.MODEL.GK_OA_ChangeStationNoticeModel"
    DeleteMethod="Delete" InsertMethod="Insert" OnSelected="ObjectDataSource1_Selected"
    SelectMethod="GetGK_OA_ChangeStationNoticeListOne" TypeName="RmsOA.BFL.GK_OA_ChangeStationNoticeBFL"
    UpdateMethod="Update">
    <SelectParameters>
        <asp:QueryStringParameter Name="Code" QueryStringField="Code" Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>
