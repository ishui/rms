<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GK_OA_MaterialTransferList.aspx.cs"
    Inherits="RmsOA_GK_OA_MaterialTransferList" %>

<%@ Register Assembly="AspWebControl" Namespace="AspWebControl" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../Images/index.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="../Rms.js"></script>
    <title>�̶��ʲ�/�ͺ�Ʒת���б�</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td background="../images/topic_bg.gif" class="topic">
                        <img align="absMiddle" height="25" src="../images/topic_li.jpg" width="35">�̶��ʲ�/�ͺ�Ʒ����>�̶��ʲ��ͺ�Ʒת��
                    </td>
                    <td width="9">
                        <img height="25" src="../images/topic_corr.gif" width="9"></td>
                </tr>
                <tr>
                    <td class="tools-area" style="width: 100%;" valign="top">
                        <img align="absMiddle" src="../images/btn_li.gif">
                        <input class="button" onclick="OpenLargeWindow('GK_OA_MaterialTransferEdit.aspx?Type=Add','Card_Modify')"
                            type="button" value="����" />
                    </td>
                </tr>
            </table>
            <table border="0" cellpadding="0" cellspacing="0" class="search-area" width="100%">
                <tr>
                    <td>
                        ����<asp:TextBox ID="NameTextBox" runat="server" CssClass="input"></asp:TextBox>ת�Ʋ���<asp:TextBox
                            ID="TranferDeptTextBox" runat="server" CssClass="input"></asp:TextBox>���ղ���<asp:TextBox ID="ReciveDeptTextBox"
                                runat="server" CssClass="input"></asp:TextBox>
                                <br />
                        ���<asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="DropDownListObjectDataSource">
                        </asp:DropDownList>ת��ʱ��<cc1:Calendar ID="StartDate"
                                    runat="server" CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                                    Display="True" ReadOnly="False">
                                </cc1:Calendar>��<cc1:Calendar ID="EndDate" runat="server" CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                            Display="True" ReadOnly="False">
                        </cc1:Calendar>
                        <input id="btnSearch" runat="server" class="submit" onserverclick="btSearch_Click"
                            type="button" value="����" />
                    </td>
                </tr>
            </table>
            <asp:GridView ID="TransferGridView" runat="server" AllowPaging="True" CssClass="list"
                HorizontalAlign="Center" Width="100%" AutoGenerateColumns="False" DataSourceID="GridViewObjectDataSource">
                <HeaderStyle CssClass="list-title" />
                <Columns>
                <asp:TemplateField HeaderText="����">
                    <ItemTemplate>
                        <a href="#" onclick="javascript:OpenLargeWindow('GK_OA_MaterialTransferEdit.aspx?Code=<%# Eval("Code")%>','EvectionApply');return false;">
                            <%# Eval("Name")%>
                        </a>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                    <asp:BoundField DataField="Type" HeaderText="���" SortExpression="Type" >
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="NumUnit" HeaderText="����/��λ" SortExpression="NumUnit" >
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="PreDept" HeaderText="ת�Ʋ���" SortExpression="PreDept" />
                    <asp:BoundField DataField="LaterDept" HeaderText="���ղ���" SortExpression="LaterDept" >
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TransferDate" HeaderText="ת��ʱ��" SortExpression="TransferDate" >
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ReciveDate" HeaderText="����ʱ��" SortExpression="ReciveDate">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
            <asp:ObjectDataSource ID="GridViewObjectDataSource" runat="server" MaximumRowsParameterName="MaxRecords"
                OldValuesParameterFormatString="original_{0}" SelectMethod="GetGK_OA_MaterialTransferList"
                SortParameterName="SortColumns" StartRowIndexParameterName="StartRecord" TypeName="RmsOA.BFL.GK_OA_MaterialTransferBFL" EnablePaging="true">
                <SelectParameters>
                    <asp:Parameter Name="SortColumns" Type="String" />
                    <asp:Parameter DefaultValue="0" Name="StartRecord" Type="Int32" />
                    <asp:Parameter DefaultValue="-1" Name="MaxRecords" Type="Int32" />
                    <asp:Parameter Name="CodeEqual" Type="Int32" />
                    <asp:ControlParameter ControlID="NameTextBox" Name="NameEqual" PropertyName="Text"
                        Type="String" />
                    <asp:Parameter Name="TypeEqual" Type="String" />
                    <asp:Parameter Name="NumberEqual" Type="String" />
                    <asp:Parameter Name="NumUnitEqual" Type="String" />
                    <asp:Parameter Name="ReasonEqual" Type="String" />
                    <asp:Parameter Name="OriginalPriceEqual" Type="Decimal" />
                    <asp:Parameter Name="PreUserEqual" Type="String" />
                    <asp:ControlParameter ControlID="TranferDeptTextBox" Name="PreDeptEqual" PropertyName="Text"
                        Type="String" />
                    <asp:Parameter Name="LaterUserEqual" Type="String" />
                    <asp:ControlParameter ControlID="ReciveDeptTextBox" Name="LaterDeptEqual" PropertyName="Text"
                        Type="String" />
                    <asp:Parameter Name="TransferHanderEqual" Type="String" />
                    <asp:Parameter Name="ReciveHanderEqual" Type="String" />
                    <asp:Parameter Name="TransferMasterEqual" Type="String" />
                    <asp:Parameter Name="ReciveMasterEqual" Type="String" />
                    <asp:Parameter Name="TransferDateEqual" Type="DateTime" />
                    <asp:Parameter Name="ReciveDateEqual" Type="DateTime" />
                    <asp:Parameter Name="QualityNOEqual" Type="String" />
                    <asp:Parameter Name="SNRuleEqual" Type="String" />
                    <asp:Parameter Name="StatusEqual" Type="String" />
                    <asp:ControlParameter ControlID="StartDate" Name="StartDate" PropertyName="Value"
                        Type="DateTime" />
                    <asp:ControlParameter ControlID="EndDate" Name="EndDate" PropertyName="Value"
                        Type="DateTime" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="DropDownListObjectDataSource" runat="server" SelectMethod="GetMaterialType" TypeName="RmsOA.BFL.GK_OA_MaterialTransferBFL"></asp:ObjectDataSource>
        </div>
    </form>
</body>
</html>
