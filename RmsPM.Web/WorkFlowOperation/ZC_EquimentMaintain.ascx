<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ZC_EquimentMaintain.ascx.cs"
    Inherits="WorkFlowOperation_ZC_EquimentMaintain" %>
<asp:FormView ID="FormView1" Width="100%" DataKeyNames="Code" runat="server" DataSourceID="ObjectDataSource1" OnDataBound="FormView1_DataBound">
    <ItemTemplate>
        <table border="0" cellpadding="0" cellspacing="0" class="form" width="100%">
            <tr>
                <td class="blackbordertd" style="width: 120px;">
                    �豸/��ʩ���ƣ�
                </td>
                <td class="blackbordertdpaddingcontent">
                    <asp:Label ID="NameLabel" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                </td>
                <td class="blackbordertd" style="width: 100px;">
                    �������ţ�
                </td>
                <td class="blackbordertdpaddingcontent">
                    <asp:Label ID="DeptLabel" runat="server" Text='<%# RmsPM.BLL.SystemRule.GetUnitName((string)Eval("Dept"))%>'></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="blackbordertd" style="width: 120px;">
                    �豸/��ʩ�ͺţ�
                </td>
                <td class="blackbordertdpaddingcontent">
                    <asp:Label ID="ModelNOLabel" runat="server" Text='<%# Bind("ModelNO") %>'></asp:Label>
                </td>
                <td class="blackbordertd" style="width: 100px;">
                    ���
                </td>
                <td class="blackbordertdpaddingcontent">
                    <asp:Label ID="TypeLabel" runat="server" Text='<%# Bind("Type") %>'></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="blackbordertd" style="width: 120px;">
                    �豸/��ʩ��ţ�
                </td>
                <td class="blackbordertdpaddingcontent">
                    <asp:Label ID="NumberLabel" runat="server" Text='<%# Bind("Number") %>'></asp:Label>
                </td>
                <td class="blackbordertd" style="width: 100px;">
                    Ԥ����(Ԫ)��
                </td>
                <td class="blackbordertdpaddingcontent">
                    <asp:Label ID="BudgetMoneyLabel" runat="server" Text='<%# Bind("BudgetMoney") %>'>
                    </asp:Label>
                </td>
            </tr>
            <tr>
                <td class="blackbordertd" style="width: 120px;">
                    ������/�����ˣ�
                </td>
                <td class="blackbordertdpaddingcontent">
                    <asp:Label ID="UserCodeLabel" runat="server"></asp:Label>
                    <asp:HiddenField ID="UserHiddenField" runat="server" Value='<%# Bind("UserName") %>' />
                </td>
                <td class="blackbordertd" style="width: 100px;">
                    �������ڣ�
                </td>
                <td class="blackbordertdpaddingcontent">
                    <asp:Label ID="ApplyDateLabel" runat="server" Text='<%# Bind("ApplyDate") %>'></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="blackbordertd" style="width: 120px;">
                    ����ԭ��
                </td>
                <td class="blackbordertdpaddingcontent" colspan="3">
                    <asp:Label ID="ReasonLabel" runat="server" Text='<%# Bind("Reason") %>'></asp:Label>
                </td>
            </tr>
        </table>
    </ItemTemplate>
</asp:FormView>
<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetGK_OA_EquipmentMaintainApplyListOne"
    TypeName="RmsOA.BFL.GK_OA_EquipmentMaintainApplyBFL" OnSelected="ObjectDataSource1_Selected">
    <SelectParameters>
        <asp:QueryStringParameter Name="Code" QueryStringField="Code" Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>
