<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GK_OA_OutFileSign.ascx.cs"
    Inherits="WorkFlowOperation_GK_OA_OutFileSign" %>
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
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("FileCode") %>'></asp:Label></td>
            </tr>
            <tr>
                <td class="blackbordertd" style="width: 20%">
                    文件标题：</td>
                <td class="blackbordertdpaddingcontent">
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("FileTitle") %>'></asp:Label></td>
                <td class="blackbordertd" style="width: 20%">
                    文件编号：</td>
                <td class="blackbordertdpaddingcontent">
                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("OutFileCode") %>'></asp:Label></td>
            </tr>
            <tr>
                <td class="blackbordertd" style="width: 20%">
                    机密程度：</td>
                <td class="blackbordertdpaddingcontent">
                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("Secret") %>'></asp:Label></td>
                <td class="blackbordertd" style="width: 20%">
                    紧急程度：</td>
                <td class="blackbordertdpaddingcontent">
                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("Urgent") %>'></asp:Label></td>
            </tr>
            <tr>
                <td class="blackbordertd" style="width: 20%">
                    拟稿部门：</td>
                <td class="blackbordertdpaddingcontent">
                    <asp:Label ID="UnitLabel2" runat="server" Text='<%# Bind("NB_UnitCode") %>'></asp:Label></td>
                <td class="blackbordertd" style="width: 20%">
                    拟稿人：</td>
                <td class="blackbordertdpaddingcontent">
                    <asp:Label ID="Label8" runat="server" Text='<%# Bind("NB_UserCode") %>'></asp:Label></td>
            </tr>
            <tr>
                <td class="blackbordertd" style="width: 20%">
                    经办部门：</td>
                <td class="blackbordertdpaddingcontent">
                    <asp:Label ID="UnitLabel" runat="server" Text='<%# Bind("JB_UnitCode") %>'></asp:Label></td>
                <td class="blackbordertd" style="width: 20%">
                    经办人：</td>
                <td class="blackbordertdpaddingcontent">
                    <asp:Label ID="Label10" runat="server" Text='<%# Bind("JB_UserCode") %>'></asp:Label></td>
            </tr>
            <tr>
                <td class="blackbordertd" style="width: 20%">
                    核稿：</td>
                <td class="blackbordertdpaddingcontent">
                    <asp:Label ID="Label11" runat="server" Text='<%# Bind("HG_UserCode") %>'></asp:Label></td>
                <td class="blackbordertd" style="width: 20%">
                    登记日期：</td>
                <td class="blackbordertdpaddingcontent">
                    <asp:Label ID="Label12" runat="server" Text='<%# Bind("RegisterDate") %>'></asp:Label></td>
            </tr>
        </table>
    </ItemTemplate>
</asp:FormView>
<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="RmsOA.MODEL.GK_OA_OutFileSignModel"
    DeleteMethod="Delete" InsertMethod="Insert" OnSelected="ObjectDataSource1_Selected"
    SelectMethod="GetGK_OA_OutFileSignListOne" TypeName="RmsOA.BFL.GK_OA_OutFileSignBFL"
    UpdateMethod="Update">
    <SelectParameters>
        <asp:QueryStringParameter Name="Code" QueryStringField="OutFileSignCode" Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>
