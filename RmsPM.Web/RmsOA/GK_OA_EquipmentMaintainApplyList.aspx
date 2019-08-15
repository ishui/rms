<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GK_OA_EquipmentMaintainApplyList.aspx.cs"
    Inherits="RmsOA_GK_OA_EquipmentMaintainApplyList" %>

<%@ Register Src="../UserControls/inputuser.ascx" TagName="inputuser" TagPrefix="uc2" %>

<%@ Register Src="../UserControls/inputunit.ascx" TagName="inputunit" TagPrefix="uc1" %>
<%@ Register Assembly="AspWebControl" Namespace="AspWebControl" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../Images/index.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript" src="../Rms.js"></script>

    <title>�豸/��ʩά�������б�</title>
</head>
<body style="BORDER-RIGHT: 0px" scroll="no">
    <form id="form1" runat="server">
        <div>
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
              <tr>
                <td height="6" bgcolor="#e4eff6"></td>
              </tr>
            </table>
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td width="986" background="../images/topic_bg.gif" class="topic">
                        <img align="absMiddle" height="25" src="../images/topic_li.jpg" width="35">�̶��ʲ�/�ͺ�Ʒ����>�̶��ʲ�ά��                    </td>
                    <td width="9">
                        <img height="25" src="../images/topic_corr.gif" width="9"></td>
                </tr>
                <tr>
                    <td colspan="2" valign="top" class="tools-area" style="width: 100%;">
                        <img align="absMiddle" src="../images/btn_li.gif">
                        <input class="button" runat="server" onClick="OpenLargeWindow('GK_OA_EquipmentMaintainApplyEdit.aspx?Type=Add','Card_Modify')"
                            id="NewButton" type="button" value="����" />                    </td>
                </tr>
            </table>
            <table border="0" cellpadding="0" cellspacing="0" class="search-area" width="100%">
                <tr>
                    <td>
                        <table width="100%">
                            <tr>
                                <td style="height: 22px">
                                    ����
                                </td>
                                <td style="height: 22px">
                                    <asp:TextBox ID="NameTextBox" runat="server" CssClass="input"></asp:TextBox>
                                </td>
                                <td style="height: 22px">
                                    ������
                                </td>
                                <td style="height: 22px">
                                    
                                    <uc2:inputuser ID="ApplyerTextBox" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    ���벿��
                                </td>
                                <td>
                                    <uc1:inputunit ID="DeptTextBox" runat="server" />
                                </td>
                                <td>
                                    ���
                                </td>
                                <td>
                                    <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="DropDownObjectDataSource"
                                        Font-Size="9pt">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    ����ʱ��
                                </td>
                                <td>
                                    <cc1:Calendar ID="StartDateCalendar" runat="server" CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                                        Value="">
                                    </cc1:Calendar>
                                    &nbsp; �� &nbsp;
                                    <cc1:Calendar ID="EndDateCalendar" runat="server" CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                                        Value="">
                                    </cc1:Calendar>
                                </td>
                                <td>״̬</td>
                                <td >
                                    <asp:CheckBoxList ID="cblStatus" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Selected="True" Value="0">����</asp:ListItem>
                                        <asp:ListItem Value="1">�����</asp:ListItem>
                                        <asp:ListItem Value="2">����</asp:ListItem>
                                        <asp:ListItem Value="3">δͨ��</asp:ListItem>
                                        <asp:ListItem Value="4">����</asp:ListItem>
                                    </asp:CheckBoxList></td>
                                <td colspan="4">
                                    <input id="btnSearch" runat="server" class="submit" onserverclick="btSearch_Click"
                                        type="button" value="����" />
                                </td>
                            </tr>
                        </table>
                </tr>
            </table>
            <asp:GridView ID="EquipmentGridView" runat="server" AllowPaging="True" CssClass="list"
                HorizontalAlign="Center" Width="100%" AutoGenerateColumns="False" DataSourceID="GridViewObjectDataSource">
                <HeaderStyle CssClass="list-title" HorizontalAlign="Center" />
                <Columns>
                    <asp:TemplateField HeaderStyle-HorizontalAlign="center" HeaderText="����">
                        <ItemTemplate>
                            <a href="#" onClick="javascript:OpenLargeWindow('GK_OA_EquipmentMaintainApplyEdit.aspx?Code=<%# Eval("Code")%>','EvectionApply');return false;">
                                <%# Eval("Name")%>
                            </a>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="center" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="ModelNO" HeaderText="���" SortExpression="ModelNO">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Type" HeaderText="����" SortExpression="Type">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderStyle-HorizontalAlign="center" HeaderText="����">
                        <ItemTemplate>
                            <%# RmsPM.BLL.SystemRule.GetUnitName((string)Eval("Dept"))%>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="������">
                    <ItemTemplate>
                    <%# RmsPM.BLL.SystemRule.GetUserName((string)Eval("UserName"))%>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="center" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="ApplyDate" HeaderText="��������" SortExpression="ApplyDate">
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="״̬" SortExpression="State">
                        <ItemTemplate>
                            <%# RmsOA.BFL.GK_OA_FileChangeBFL.GetManpowerNeedStatusName((string)Eval("State")) %>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="center" />
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    ��ƥ������
                </EmptyDataTemplate>
            </asp:GridView>
            <asp:ObjectDataSource ID="GridViewObjectDataSource" runat="server" MaximumRowsParameterName="MaxRecords"
                EnablePaging="True" SelectMethod="GetGK_OA_EquipmentMaintainApplyList" SortParameterName="SortColumns"
                StartRowIndexParameterName="StartRecord" TypeName="RmsOA.BFL.GK_OA_EquipmentMaintainApplyBFL"
                OldValuesParameterFormatString="original_{0}">
                <SelectParameters>
                    <asp:Parameter Name="SortColumns" Type="String" />
                    <asp:Parameter DefaultValue="0" Name="StartRecord" Type="Int32" />
                    <asp:Parameter DefaultValue="-1" Name="MaxRecords" Type="Int32" />
                    <asp:Parameter Name="CodeEqual" Type="Int32" />
                    <asp:ControlParameter ControlID="NameTextBox" Name="NameEqual" PropertyName="Text"
                        Type="String" />
                    <asp:ControlParameter ControlID="DeptTextBox" Name="DeptEqual" PropertyName="Value"
                        Type="String" />
                    <asp:Parameter Name="ModelNOEqual" Type="String" />
                    <asp:Parameter Name="TypeEqual" Type="String" />
                    <asp:Parameter Name="NumberEqual" Type="String" />
                    <asp:Parameter Name="BudgetMoneyEqual" Type="Decimal" />
                    <asp:ControlParameter ControlID="ApplyerTextBox" Name="UserNameEqual" PropertyName="Value"
                        Type="String" />
                    <asp:Parameter Name="ApplyDateEqual" Type="DateTime" />
                    <asp:Parameter Name="ReasonEqual" Type="String" />
                    <asp:Parameter Name="StateEqual" Type="String" />
                    <asp:Parameter Name="QualityNOEqual" Type="String" />
                    <asp:Parameter Name="SNRuleEqual" Type="String" />
                    <asp:ControlParameter ControlID="StartDateCalendar" Name="StartDate" Type="DateTime"
                        PropertyName="Value" />
                    <asp:ControlParameter ControlID="EndDateCalendar" Name="EndDate" PropertyName="Value"
                        Type="DateTime" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="DropDownObjectDataSource" runat="server" SelectMethod="GetEquipmentType"
                TypeName="RmsOA.BFL.GK_OA_EquipmentMaintainApplyBFL" OldValuesParameterFormatString="original_{0}">
            </asp:ObjectDataSource>
      </div>
    </form>
</body>
</html>
