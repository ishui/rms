<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ZC_AssertAcount.ascx.cs"
    Inherits="WorkFlowOperation_ZC_AssertAcount" %>
<asp:FormView ID="FormView1" runat="server" DataKeyNames="Code" DataSourceID="ObjectDataSource1"
    Width="100%">
    <ItemTemplate>
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td class="blackbordertd" style="width: 100px;">
                    名称：
                </td>
                <td class="blackbordertdpaddingcontent">
                    <asp:Label ID="NameLabel" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                </td>
                <td class="blackbordertd" style="width: 100px;">
                    类别：
                </td>
                <td class="blackbordertdpaddingcontent">
                    <asp:Label ID="TypeLabel" runat="server" Text='<%# Bind("Type") %>'></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="blackbordertd" style="width: 100px;">
                    编号：
                </td>
                <td class="blackbordertdpaddingcontent">
                    <asp:Label ID="NumberLabel" runat="server" Text='<%# Bind("Number") %>'></asp:Label>
                </td>
                <td class="blackbordertd" style="width: 100px;">
                    购买日期：
                </td>
                <td class="blackbordertdpaddingcontent">
                    <asp:Label ID="BuydateLabel" runat="server" Text='<%# Bind("Buydate") %>'></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="blackbordertd" style="width: 100px;">
                    购买数量：
                </td>
                <td class="blackbordertdpaddingcontent">
                    <asp:Label ID="BuyCountLabel" runat="server" Text='<%# Bind("BuyCount") %>'></asp:Label>
                </td>
                <td class="blackbordertd" style="width: 100px;">
                    单位：
                </td>
                <td class="blackbordertdpaddingcontent">
                    <asp:Label ID="UnitLabel" runat="server" Text='<%# Bind("Unit") %>'></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="blackbordertd" style="width: 100px;">
                    金额：
                </td>
                <td class="blackbordertdpaddingcontent">
                    <asp:Label ID="PriceLabel" runat="server" Text='<%# Bind("Price") %>'></asp:Label>
                </td>
                <td class="blackbordertd" style="width: 100px;">
                    使用部门：
                </td>
                <td class="blackbordertdpaddingcontent">
                    <asp:Label ID="DeptLabel" runat="server" Text='<%# RmsPM.BLL.SystemRule.GetUnitName((string)Eval("Dept"))%>'>
                    </asp:Label>
                </td>
            </tr>
            <tr>
                <td class="blackbordertd" style="width: 100px;">
                    使用人：
                </td>
                <td class="blackbordertdpaddingcontent">
                    <asp:Label ID="UserNameLabel" runat="server" Text='<%# Bind("UserName") %>'></asp:Label>
                </td>
                <td class="blackbordertd" style="width: 100px;">
                    存放地点：
                </td>
                <td class="blackbordertdpaddingcontent">
                    <asp:Label ID="PlaceLabel" runat="server" Text='<%# Bind("Place") %>'></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="blackbordertd" style="width: 100px;">
                    变更情况：</td>
                <td class="blackbordertdpaddingcontent" colspan="3">
                    <asp:Label ID="TransferRecordLabel" runat="server" Text='<%# Bind("TransferRecord") %>'>
                    </asp:Label>
                </td>
            </tr>
            <tr>
                <td class="blackbordertd" style="width: 100px;">
                    备注：</td>
                <td class="blackbordertdpaddingcontent" colspan="3">
                    <asp:Label ID="RemarkLabel" runat="server" Text='<%# Bind("Remark") %>'></asp:Label>
                </td>
            </tr>
        </table>
    </ItemTemplate>
</asp:FormView>
<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="RmsOA.MODEL.GK_OA_CapitalAssertAcountModel"
    SelectMethod="GetGK_OA_CapitalAssertAcountListOne" TypeName="RmsOA.BFL.GK_OA_CapitalAssertAcountBFL">
    <SelectParameters>
        <asp:QueryStringParameter Name="Code" QueryStringField="Code" Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>
