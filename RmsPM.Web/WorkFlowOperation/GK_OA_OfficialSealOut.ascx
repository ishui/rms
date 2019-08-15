<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GK_OA_OfficialSealOut.ascx.cs"
    Inherits="WorkFlowOperation_GK_OA_OfficialSealOut" %>
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
                    部门：</td>
                <td class="blackbordertdpaddingcontent">
                    <asp:Label ID="UnitLabel" runat="server" Text='<%# Bind("UnitCode") %>'></asp:Label></td>
                <td class="blackbordertd" style="width: 20%">
                    日期：</td>
                <td class="blackbordertdpaddingcontent">
                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("RegiesterDate") %>'></asp:Label></td>
            </tr>
            <tr>
                <td class="blackbordertd" style="width: 20%">
                    公章/证照名：</td>
                <td colspan="3" class="blackbordertdpaddingcontent">
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("OfficialSealName") %>'></asp:Label></td>
            </tr>
            <tr>
                <td class="blackbordertd" style="width: 20%">
                    外出时间：</td>
                <td class="blackbordertdpaddingcontent">
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("OutDate") %>'></asp:Label></td>
                <td class="blackbordertd" style="width: 20%">
                    归还时间：</td>
                <td class="blackbordertdpaddingcontent">
                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("ReturnDate") %>'></asp:Label></td>
            </tr>
            <tr>
                <td class="blackbordertd" style="width: 20%">
                    事由：</td>
                <td colspan="3" class="blackbordertdpaddingcontent">
                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("Reason") %>'></asp:Label></td>
            </tr>
            <tr>
                <td class="blackbordertd" style="width: 20%">
                    备注：</td>
                <td colspan="3" class="blackbordertdpaddingcontent">
                    <asp:Label ID="Label11" runat="server" Text='<%# Bind("Remark") %>'></asp:Label></td>
            </tr>
        </table>
    </ItemTemplate>
</asp:FormView>
<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="RmsOA.MODEL.GK_OA_OfficialSealOutModel"
    DeleteMethod="Delete" InsertMethod="Insert" OnSelected="ObjectDataSource1_Selected"
    SelectMethod="GetGK_OA_OfficialSealOutListOne" TypeName="RmsOA.BFL.GK_OA_OfficialSealOutBFL"
    UpdateMethod="Update">
    <SelectParameters>
        <asp:QueryStringParameter Name="Code" QueryStringField="OfficialSealOutCode"
            Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>
