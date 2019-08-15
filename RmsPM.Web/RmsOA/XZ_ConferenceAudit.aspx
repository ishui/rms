<%@ Page Language="C#" MasterPageFile="~/RmsOA/XZ_ConferenceMasterPage.master"
    AutoEventWireup="true" CodeFile="XZ_ConferenceAudit.aspx.cs" Inherits="RmsOA_XZ_ConferenceAudit"
    Title="������Ȩ" %>

<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">

    <script src="../Rms.js" type="text/javascript"></script>

    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" MaximumRowsParameterName="MaxRecords"
        SelectMethod="GetConferenceManageList" SortParameterName="SortColumns" StartRowIndexParameterName="StartRecord"
        TypeName="RmsOA.BFL.ConferenceManageBFL">
        <SelectParameters>
            <asp:Parameter Name="SortColumns" Type="String" />
            <asp:Parameter DefaultValue="0" Name="StartRecord" Type="Int32" />
            <asp:Parameter DefaultValue="-1" Name="MaxRecords" Type="Int32" />
            <asp:Parameter DefaultValue="" Name="CodeEqual" Type="Int32" />
            <asp:Parameter Name="TopicEqual" Type="String" />
            <asp:Parameter Name="ChaterMemberEqual" Type="String" />
            <asp:Parameter Name="TypeEqual" Type="String" />
            <asp:Parameter Name="PlaceEqual" Type="String" />
            <asp:Parameter Name="DeptEqual" Type="String" />
            <asp:Parameter Name="StartTimeEqual" Type="DateTime" />
            <asp:Parameter Name="EndTimeEqual" Type="DateTime" />
            <asp:Parameter DefaultValue="" Name="RemarkEqual" Type="String" />
            <asp:Parameter DefaultValue="UnAuthored" Name="StateEqual" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:GridView ID="GridView1" runat="server" AllowPaging="true" AutoGenerateColumns="False"
        CssClass="list" DataSourceID="ObjectDataSource1" Width="100%">
        <HeaderStyle CssClass="list-title" HorizontalAlign="center" />
        <FooterStyle CssClass="list-title" HorizontalAlign="center" />
        <Columns>
            <asp:TemplateField HeaderText="��������" SortExpression="Topic">
                <HeaderStyle HorizontalAlign="center" />
                <ItemStyle HorizontalAlign="center" />
                <ItemTemplate>
                    <a href="#" onclick="OpenLargeWindow('XZ_Conference.aspx?Type=Read&Code=<%#Eval("Code")%>','Conference')">
                        <%#Eval("Topic")%>
                    </a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="���쵥λ" ItemStyle-HorizontalAlign="center">
                <ItemTemplate>
                    <%# RmsPM.BLL.SystemRule.GetUnitName((string)Eval("Dept"))%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="����ص�">
                <ItemStyle HorizontalAlign="center" />
                <ItemTemplate>
                    <%# RmsOA.BFL.ConferenceUserListBFLFacade.GetMeetRoomName((string) Eval("Place"))%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="StartTime" HeaderText="���鿪ʼʱ��" ItemStyle-HorizontalAlign="center"
                SortExpression="StartTime" />
            <asp:BoundField DataField="EndTime" HeaderText="�������ʱ��" ItemStyle-HorizontalAlign="center"
                SortExpression="EndTime" />
            <asp:TemplateField HeaderText=" ��Ȩ " SortExpression="Code">
                <HeaderStyle HorizontalAlign="center" />
                <ItemStyle HorizontalAlign="center" />
                <ItemTemplate>
                    <input id="btAudit" class="submit" onclick="location.href('XZ_ConferenceAudit.aspx?Code=<%#Eval("Code") %>')"
                        type="button" value=" ͬ�� " /></ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>
            <p>
                <span style="font-weight: bolder; text-align: center;">������ĿǰΪֹ��û����Ҫ�������飡</span>
            </p>
        </EmptyDataTemplate>
    </asp:GridView>
</asp:Content>
