<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GK_OA_WorkEvaluation.ascx.cs"
    Inherits="WorkFlowOperation_GK_OA_WorkEvaluation" %>
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
                    所在部门：</td>
                <td class="blackbordertdpaddingcontent">
                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("UnitCode") %>'></asp:Label></td>
                <td class="blackbordertd" style="width: 20%">
                    入公司时间：</td>
                <td class="blackbordertdpaddingcontent">
                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("InComDate") %>'></asp:Label></td>
            </tr>
            <tr>
                <td class="blackbordertd" style="width: 20%">
                    姓名：</td>
                <td class="blackbordertdpaddingcontent">
                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("PersonName") %>'></asp:Label></td>
                <td class="blackbordertd" style="width: 20%">
                    岗位：</td>
                <td class="blackbordertdpaddingcontent">
                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("StationCode") %>'></asp:Label></td>
            </tr>
            <tr>
                <td class="blackbordertd" style="width: 20%">
                    工作表现：</td>
                <td colspan="3" class="blackbordertdpaddingcontent">
                    <asp:Label ID="Label7" runat="server" Text='<%# Bind("WorkPperation") %>'></asp:Label></td>
            </tr>
            <tr>
                <td class="blackbordertd" style="width: 20%">
                    优缺点：</td>
                <td colspan="3" class="blackbordertdpaddingcontent">
                    <asp:Label ID="Label8" runat="server" Text='<%# Bind("MeritsAndDemerits") %>'></asp:Label></td>
            </tr>
            <tr>
                <td class="blackbordertd" style="width: 20%">
                    决定批示：</td>
                <td colspan="3" class="blackbordertdpaddingcontent">
                    <asp:Label ID="Label11" runat="server" Text='<%# Bind("Decide") %>'></asp:Label></td>
            </tr>
        </table>
    </ItemTemplate>
</asp:FormView>
<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="RmsOA.MODEL.GK_OA_WorkEvaluationModel"
    DeleteMethod="Delete" InsertMethod="Insert" OnSelected="ObjectDataSource1_Selected"
    SelectMethod="GetGK_OA_WorkEvaluationListOne" TypeName="RmsOA.BFL.GK_OA_WorkEvaluationBFL"
    UpdateMethod="Update">
    <SelectParameters>
        <asp:QueryStringParameter Name="Code" QueryStringField="Code" Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>
