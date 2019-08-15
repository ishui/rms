<%@ Control Language="C#" AutoEventWireup="true" CodeFile="XZ_EvectionApply.ascx.cs" Inherits="WorkFlowOperation_XZ_EvectionApply" %>
<asp:FormView ID="FormView1" runat="server" DataSourceID="ObjectDataSource1" Width="100%" >
<ItemTemplate>
    <table border="0" cellpadding="0" cellspacing="0" class="form" width="100%">
        <tr>
            <td class="blackbordertd" style="width: 100px">
                ���벿�ţ�</td>
            <td class="blackbordertdpaddingcontent">
                <asp:Label ID="DeptLabel" runat="server" Text='<%# RmsPM.BLL.SystemRule.GetUnitName((string)Eval("Dept")) %>'></asp:Label>
            </td>
            <td class="blackbordertd" style="width: 100px;">
                ����ʱ�䣺</td>
            <td class="blackbordertdpaddingcontent">
                <asp:Label ID="ApplyDateLabel" runat="server" Text='<%# Bind("ApplyDate") %>'></asp:Label><br />
            </td>
        </tr>
        <tr>
            <td class="blackbordertd" style="width: 100px;">
                �����ˣ�</td>
            <td class="blackbordertdpaddingcontent">
                 <%# RmsPM.BLL.SystemRule.GetUserName((string)Eval("Applyer"))%>
            </td>
            <td class="blackbordertd" style="width: 100px;">
                ������Ա��</td>
            <td class="blackbordertdpaddingcontent">
                <asp:Label ID="UsersLabel" runat="server" Text='<%# Bind("Users") %>'></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="blackbordertd" style="width: 100px;">
                �������ɣ�</td>
            <td class="blackbordertdpaddingcontent" colspan="3">
                <asp:Label ID="ReasonLabel" runat="server" Text='<%# Bind("Reason") %>'></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="blackbordertd" style="width: 100px;">
                ����·�ߣ�</td>
            <td class="blackbordertdpaddingcontent" colspan="3">
                <asp:Label ID="RountLabel" runat="server" Text='<%# Bind("Rount") %>'></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="blackbordertd" style="width: 100px;">
                �������ڣ�
            </td>
            <td class="blackbordertdpaddingcontent" colspan="3">
                <asp:Label ID="StartDataLabel" runat="server" Text='<%# Bind("StartData") %>'></asp:Label>
                ��
                <asp:Label ID="EndDateLabel" runat="server" Text='<%# Bind("EndDate") %>'></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="blackbordertd" style="width: 100px;">
                ��ͨ���ߣ�
            </td>
            <td class="blackbordertdpaddingcontent">
                <asp:Label ID="VehicleLabel" runat="server" Text='<%# Bind("Vehicle") %>'></asp:Label>
            </td>
            <td class="blackbordertd" style="width: 100px;">
                ס�ޱ�׼��
            </td>
            <td class="blackbordertdpaddingcontent">
                <asp:Label ID="LiveLevelLabel" runat="server" Text='<%# Bind("LiveLevel") %>'></asp:Label>�Ǳ���
            </td>
        </tr>
        <tr>
            <td class="blackbordertd" style="width: 100px;">
                Ԥ�Ʒ��ã�
            </td>
            <td class="blackbordertdpaddingcontent">
                <asp:Label ID="BudgetMoneyLabel" runat="server" Text='<%# Bind("BudgetMoney") %>'>
                </asp:Label>
            </td>
        </tr>
    </table>
</ItemTemplate>
</asp:FormView>
<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetGK_OA_EvectionApplyListOne"
    TypeName="RmsOA.BFL.GK_OA_EvectionApplyBFL" OnSelected="ObjectDataSource1_Selected">
    <SelectParameters>
        <asp:QueryStringParameter Name="Code" QueryStringField="Code" Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>
