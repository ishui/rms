<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GK_OA_ManpowerNeed.ascx.cs" Inherits="WorkFlowOperation_GK_OA_ManpowerNeed" %>
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
                <td  style="width: 20%" class="blackbordertd">
                    文件编号：</td>
                <td class="blackbordertdpaddingcontent">
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("SystemCode") %>'></asp:Label></td>
                <td  style="width: 20%" class="blackbordertd">
                    标识序号：</td>
                <td class="blackbordertdpaddingcontent">
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("FileCode") %>'></asp:Label></td>
            </tr>
            <tr>
                <td  style="width: 20%" class="blackbordertd">
                    需求部门：</td>
                <td class="blackbordertdpaddingcontent">
                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("UnitCode") %>'></asp:Label></td>
                <td  style="width: 20%" class="blackbordertd">
                    日期：</td>
                <td class="blackbordertdpaddingcontent">
                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("RegistDate") %>'></asp:Label></td>
            </tr>
            <tr>
                <td  style="width: 20%" class="blackbordertd">
                    岗位名称：</td>
                <td class="blackbordertdpaddingcontent">
                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("StationCode") %>'></asp:Label></td>
                <td  style="width: 20%" class="blackbordertd">
                    人数：</td>
                <td class="blackbordertdpaddingcontent">
                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("PersonNumber") %>'></asp:Label></td>
            </tr>
            <tr>
                <td  style="width: 20%" class="blackbordertd">
                    学历：</td>
                <td colspan="3" class="blackbordertdpaddingcontent">
                    <asp:Label ID="Label7" runat="server" Text='<%# Bind("Education") %>'></asp:Label></td>
            </tr>
            <tr>
                <td  style="width: 20%" class="blackbordertd">
                    资格：</td>
                <td colspan="3" class="blackbordertdpaddingcontent">
                    <asp:Label ID="Label8" runat="server" Text='<%# Bind("Treatment") %>'></asp:Label></td>
            </tr>
            <tr>
                <td  style="width: 20%" class="blackbordertd">
                    从业年限：</td>
                <td class="blackbordertdpaddingcontent">
                    <asp:Label ID="Label9" runat="server" Text='<%# Bind("YearOfWork") %>'></asp:Label></td>
                <td  style="width: 20%" class="blackbordertd">
                    待遇：</td>
                <td class="blackbordertdpaddingcontent">
                    <asp:Label ID="Label10" runat="server" Text='<%# Bind("Seniority") %>'></asp:Label></td>
            </tr>
            <tr>
                <td  style="width: 20%" class="blackbordertd">
                    备注：</td>
                <td colspan="3" class="blackbordertdpaddingcontent">
                    <asp:Label ID="Label11" runat="server" Text='<%# Bind("Remark") %>'></asp:Label></td>
            </tr>
        </table>
    </ItemTemplate>
</asp:FormView>
<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="RmsOA.MODEL.GK_OA_ManpowerNeedModel"
    DeleteMethod="Delete" InsertMethod="Insert"  OnSelected="ObjectDataSource1_Selected"
    SelectMethod="GetGK_OA_ManpowerNeedListOne" TypeName="RmsOA.BFL.GK_OA_ManpowerNeedBFL"
    UpdateMethod="Update">
    <SelectParameters>
        <asp:QueryStringParameter Name="Code" QueryStringField="Code" Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>

