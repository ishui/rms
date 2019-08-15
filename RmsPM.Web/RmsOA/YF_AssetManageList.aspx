<%@ Page Language="C#" AutoEventWireup="true" CodeFile="YF_AssetManageList.aspx.cs"
    Inherits="RmsOA_YF_AssetManageList" %>

<%@ Register Src="../UserControls/inputunit.ascx" TagName="inputunit" TagPrefix="uc1" %>
<%@ Register Assembly="AspWebControl" Namespace="AspWebControl" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>�ʲ�����</title>
    <link href="../Images/index.css" rel="stylesheet" type="text/css" />

    <script language="javascript" src="../Rms.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div>
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td background="../images/topic_bg.gif" class="topic">
                            <img align="absMiddle" height="25" src="../images/topic_li.jpg" width="35">�̶��ʲ�
                        </td>
                        <td width="9">
                            <img height="25" src="../images/topic_corr.gif" width="9"></td>
                    </tr>
                </table>
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td class="tools-area" valign="top">
                            <img align="absMiddle" src="../images/btn_li.gif">
                            <input id="NewButton" runat="server" class="button" onclick="OpenLargeWindow('YF_AssetEdit.aspx?Type=Add','AssetEdit')"
                                type="button" value="����" />
                        </td>
                    </tr>
                </table>
                <table border="0" cellpadding="0" cellspacing="0" class="search-area" width="100%">
                    <tr>
                        <td>
                            <table width="100%">
                                <tr>
                                    <td>
                                        �豸����
                                    </td>
                                    <td>
                                        <asp:TextBox ID="NameTextBox" runat="server" CssClass="input"></asp:TextBox>
                                    </td>
                                    <td>
                                        �豸���
                                    </td>
                                    <td>
                                        <asp:TextBox ID="NumberTextBox" runat="server" CssClass="input"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Ʒ��
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="ProducerTextBox" CssClass="input"></asp:TextBox>
                                    </td>
                                    <td>
                                        �ʲ�����
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="SortDropDownList" runat="server" Font-Size="9pt" DataSourceID="ObjectDataSource2">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        ��Ӧ��
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ProviderTextBox" runat="server" CssClass="input"></asp:TextBox>
                                    </td>
                                    <td>
                                        �Ǽ���
                                    </td>
                                    <td>
                                        <asp:TextBox ID="RegisterTextBox" runat="server" CssClass="input" Width="50px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        ��������
                                    </td>
                                    <td>
                                        <cc1:Calendar ID="StartDate" runat="server" CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                                            Value="">
                                        </cc1:Calendar>
                                        &nbsp;�� &nbsp;
                                        <cc1:Calendar ID="EndDate" runat="server" CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                                            Value="">
                                        </cc1:Calendar>
                                    </td>
                                    <td colspan="4">
                                        <input id="SearchButton" runat="server" class="submit" onserverclick="SearchButton_Click"
                                            type="button" value="����" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <asp:GridView ID="GridView1" runat="server" Width="100%" CssClass="list" AutoGenerateColumns="False"
            DataSourceID="ObjectDataSource1" PageSize="20" AllowPaging="True">
            <HeaderStyle CssClass="list-title" HorizontalAlign="Center" />
            <RowStyle HorizontalAlign="Center" />
            <FooterStyle CssClass="list-title" HorizontalAlign="Center" />
            <Columns>
                <asp:TemplateField HeaderText="�豸����">
                    <ItemTemplate>
                        <a href="#" onclick="OpenLargeWindow('YF_AssetEdit.aspx?Code=<%#Eval("Code")%>','YF_AssetEdit')">
                            <%#Eval("FacilityName") %>
                        </a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Producer" HeaderText="Ʒ��" SortExpression="Producer" />
                <asp:BoundField DataField="ModelNO" HeaderText="�ͺ�" SortExpression="ModelNO" />
                <asp:TemplateField HeaderText="ʹ�ò���">
                <ItemTemplate>
                    <%# RmsPM.BLL.SystemRule.GetUnitName((string)Eval("UseDept"))%>
                </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="BuyDate" HeaderText="��������" SortExpression="BuyDate" />
                <asp:BoundField DataField="EquiType" HeaderText="�豸����" SortExpression="EquiType" />
                <asp:BoundField DataField="Provider" HeaderText="��Ӧ��" SortExpression="Provider" />
                <asp:BoundField DataField="FreeMain" HeaderText="������" SortExpression="FreeMain" />
            </Columns>
            <PagerSettings FirstPageText="�� ҳ" LastPageText="β ҳ" Mode="NextPreviousFirstLast"
                NextPageText="��һҳ" PageButtonCount="4" PreviousPageText="��һҳ" />
        </asp:GridView>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" MaximumRowsParameterName="MaxRecords"
            SelectMethod="GetYF_AssetManageList" EnablePaging="True" SortParameterName="SortColumns"
            StartRowIndexParameterName="StartRecord" TypeName="RmsOA.BFL.YF_AssetManageBFL"
            OldValuesParameterFormatString="original_{0}">
            <SelectParameters>
                <asp:Parameter Name="SortColumns" Type="String" DefaultValue="" />
                <asp:Parameter DefaultValue="0" Name="StartRecord" Type="Int32" />
                <asp:Parameter DefaultValue="-1" Name="MaxRecords" Type="Int32" />
                <asp:Parameter DefaultValue="" Name="CodeEqual" Type="Int32" />
                <asp:ControlParameter ControlID="NameTextBox" Name="FacilityNameEqual" PropertyName="Text"
                    Type="String" />
                <asp:Parameter Name="AreaEqual" Type="String" />
                <asp:Parameter Name="ProducerEqual" Type="String" />
                <asp:Parameter Name="BuyCorpEqual" Type="String" />
                <asp:Parameter Name="ModelNOEqual" Type="String" />
                <asp:Parameter Name="LayCorpEqual" Type="String" />
                <asp:Parameter Name="TypeEqual" Type="String" />
                <asp:Parameter Name="CountsEqual" Type="String" />
                <asp:Parameter Name="UseDeptEqual" Type="String" />
                <asp:Parameter Name="LayPlaceEqual" Type="String" />
                <asp:Parameter Name="CountUnitEqual" Type="String" />
                <asp:Parameter Name="ProdAreaEqual" Type="String" />
                <asp:Parameter Name="SortNOEqual" Type="String" />
                <asp:Parameter Name="SortTypeEqual" Type="String" />
                <asp:Parameter Name="FreeMainEqual" Type="String" />
                <asp:Parameter Name="EquiTypeEqual" Type="String" />
                <asp:ControlParameter ControlID="ProviderTextBox" Name="ProviderEqual" PropertyName="Text"
                    Type="String" />
                <asp:Parameter Name="BuyDateEqual" Type="DateTime" />
                <asp:Parameter Name="PriceEqual" Type="Decimal" />
                <asp:ControlParameter ControlID="RegisterTextBox" Name="RegisterEqual" PropertyName="Text"
                    Type="String" />
                <asp:Parameter Name="MainCardPlaceEqual" Type="String" />
                <asp:Parameter Name="StoreStatusEqual" Type="String" />
                <asp:Parameter Name="RemarkEqual" Type="String" />
                <asp:Parameter Name="StatusEqual" Type="String" />
                <asp:Parameter Name="DeptUnitEqual" Type="String" />
                <asp:Parameter Name="BookINTimeEqual" Type="DateTime" />
                <asp:Parameter Name="BuyTypeEqual" Type="String" />
                <asp:ControlParameter ControlID="NumberTextBox" Name="CodeNOEqual" PropertyName="Text"
                    Type="string" />
                <asp:ControlParameter ControlID="StartDate" Name="StartTimeEqual" PropertyName="Value"
                    Type="DateTime" />
                <asp:ControlParameter ControlID="EndDate" Name="EndTimeEqual" PropertyName="Value"
                    Type="DateTime" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="AssetSortType"
            TypeName="RmsOA.BFL.YDictionaryExpand"></asp:ObjectDataSource>
    </form>
</body>
</html>
