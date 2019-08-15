<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ZC_AssetTransfer.ascx.cs" Inherits="WorkFlowOperation_ZC_AssetTransfer" %>
<asp:FormView ID="FormView1" runat="server" DataKeyNames="Code" DataSourceID="ObjectDataSource1" Width="100%">
 <ItemTemplate>
     <table border="0" cellpadding="0" cellspacing="0" class="form" width="100%">
         <tr>
             <td class="blackbordertd" style="width: 100px;">
                 名称：
             </td>
             <td class="blackbordertdpaddingcontent">
                 <asp:Label ID="Label1" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
             </td>
             <td class="blackbordertd" style="width: 100px;">
                 类别：
             </td>
             <td class="blackbordertdpaddingcontent">
                 <asp:Label ID="TypeLabel" runat="server" Text='<%# Bind("Sort") %>'></asp:Label>
             </td>
         </tr>
         <tr>
             <td class="blackbordertd" style="width: 100px;">
                 编号：
             </td>
             <td class="blackbordertdpaddingcontent">
                 <asp:Label ID="Label2" runat="server" Text='<%# Bind("Number") %>'></asp:Label>
             </td>
             <td class="blackbordertd" style="width: 100px;">
                 数量/单位 ：
             </td>
             <td class="blackbordertdpaddingcontent">
                 <asp:Label ID="Label3" runat="server" Text='<%# Bind("NumUnit") %>'></asp:Label>
             </td>
         </tr>
         <tr>
             <td class="blackbordertd" style="width: 100px;">
                 原始单价：
             </td>
             <td class="blackbordertdpaddingcontent">
                 <asp:Label ID="OriginalPriceLabel" runat="server" Text='<%# Bind("OriginalPrice") %>'>
                 </asp:Label><br />
             </td>
             <td class="blackbordertd" style="width: 100px;">
                 使用部门：</td>
             <td class="blackbordertdpaddingcontent">
                 <asp:Label ID="Label12" runat="server" Text='<%# RmsPM.BLL.SystemRule.GetUnitName((string)Eval("PreDept"))%>'></asp:Label>
             </td>
         </tr>
         <tr>
             <td class="blackbordertd" style="width: 100px;">
                 前使用人：
             </td>
             <td class="blackbordertdpaddingcontent">
                 <asp:Label ID="Label6" runat="server" Text='<%# Bind("PreUser") %>'></asp:Label>
             </td>
             <td class="blackbordertd" style="width: 100px;">
                 后使用人：
             </td>
             <td class="blackbordertdpaddingcontent">
                 <asp:Label ID="LaterUserLabel" runat="server" Text='<%# Bind("PostUser") %>'></asp:Label>
             </td>
         </tr>
         <tr>
             <td class="blackbordertd" style="width: 100px;">
                 变更原因：
             </td>
             <td class="blackbordertdpaddingcontent" colspan="3">
                 <asp:Label ID="Label5" runat="server" Text='<%# Bind("Reason") %>'></asp:Label>
             </td>
         </tr>
     </table>    
 </ItemTemplate>
</asp:FormView>
<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetGK_OA_AssetTransferListOne"
    TypeName="RmsOA.BFL.GK_OA_AssetTransferBFL" OnSelected="ObjectDataSource1_Selected">
    <SelectParameters>
        <asp:QueryStringParameter Name="Code" QueryStringField="Code" Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>
