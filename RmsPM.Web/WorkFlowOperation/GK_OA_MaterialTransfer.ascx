<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GK_OA_MaterialTransfer.ascx.cs" Inherits="WorkFlowOperation_GK_OA_MaterialTransfer" %>
<asp:FormView ID="FormView1" runat="server" DataSourceID="ObjectDataSource1" Width="100%">
    <ItemTemplate>
        <table border="0" cellpadding="0" cellspacing="0" class="form" width="100%">
            <tr>
                <td class="form-item" style="width: 100px;">
                    名称：
                </td>
                <td>
                    <asp:Label ID="NameLabel" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                </td>
                <td class="form-item" style="width: 100px;">
                    类别：
                </td>
                <td>
                    <asp:Label ID="TypeLabel" runat="server" Text='<%# Bind("Type") %>'></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="form-item" style="width: 100px;">
                    编号：
                </td>
                <td>
                    <asp:Label ID="NumberLabel" runat="server" Text='<%# Bind("Number") %>'></asp:Label>
                </td>
                <td class="form-item" style="width: 100px;">
                    数量/单位 ：
                </td>
                <td>
                    <asp:Label ID="NumUnitLabel" runat="server" Text='<%# Bind("NumUnit") %>'></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="form-item" style="width: 100px;">
                    原始单价：
                </td>
                <td colspan="3">
                    <asp:Label ID="OriginalPriceLabel" runat="server" Text='<%# Bind("OriginalPrice") %>'>
                    </asp:Label>
                </td>
            </tr>
            <tr>
                <td class="form-item" style="width: 100px;">
                    变更原因：
                </td>
                <td colspan="3">
                    <asp:Label ID="ReasonLabel" runat="server" Text='<%# Bind("Reason") %>'></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="form-item" style="width: 100px;">
                    前使用人：
                </td>
                <td>
                    <asp:Label ID="PreUserLabel" runat="server" Text='<%# Bind("PreUser") %>'></asp:Label>
                </td>
                <td class="form-item" style="width: 100px;">
                    后使用人：
                </td>
                <td>
                    <asp:Label ID="LaterUserLabel" runat="server" Text='<%# Bind("LaterUser") %>'></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="form-item" style="width: 100px;">
                    转交部门：
                </td>
                <td>
                    <asp:Label ID="PreDeptLabel" runat="server" Text='<%# Bind("PreDept") %>'></asp:Label>
                </td>
                <td class="form-item" style="width: 100px;">
                    接收部门：
                </td>
                <td>
                    <asp:Label ID="LaterDeptLabel" runat="server" Text='<%# Bind("LaterDept") %>'></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="form-item" style="width: 100px;">
                    转交部门负责人：
                </td>
                <td>
                    <asp:Label ID="TransferMasterLabel" runat="server" Text='<%# Bind("TransferMaster") %>'>
                    </asp:Label>
                </td>
                <td class="form-item" style="width: 100px;">
                    接收部门负责人：
                </td>
                <td>
                    <asp:Label ID="ReciveMasterLabel" runat="server" Text='<%# Bind("ReciveMaster") %>'>
                    </asp:Label>
                </td>
            </tr>
            <tr>
                <td class="form-item" style="width: 100px;">
                    转交经办人：
                </td>
                <td>
                    <asp:Label ID="TransferHanderLabel" runat="server" Text='<%# Bind("TransferHander") %>'>
                    </asp:Label>
                </td>
                <td class="form-item" style="width: 100px;">
                    接收经办人：
                </td>
                <td>
                    <asp:Label ID="ReciveHanderLabel" runat="server" Text='<%# Bind("ReciveHander") %>'>
                    </asp:Label>
                </td>
            </tr>
            <tr>
                <td class="form-item" style="width: 100px;">
                    转交日期：
                </td>
                <td>
                    <asp:Label ID="TransferDateLabel" runat="server" Text='<%# Bind("TransferDate") %>'>
                    </asp:Label>
                </td>
                <td class="form-item" style="width: 100px;">
                    接收日期：
                </td>
                <td>
                    <asp:Label ID="ReciveDateLabel" runat="server" Text='<%# Bind("ReciveDate") %>'>
                    </asp:Label>
                </td>
            </tr>
        </table>
    </ItemTemplate>
</asp:FormView>
<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetGK_OA_MaterialTransferListOne"
    TypeName="RmsOA.BFL.GK_OA_MaterialTransferBFL">
    <SelectParameters>
        <asp:QueryStringParameter Name="Code" QueryStringField="Code" Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>
<br />
<br />

