<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GK_OA_EvectionApplyList.aspx.cs"
    Inherits="RmsOA_GK_OA_EvectionApply" %>

<%@ Register Src="../UserControls/inputuser.ascx" TagName="inputuser" TagPrefix="uc2" %>

<%@ Register Src="../UserControls/inputunit.ascx" TagName="inputunit" TagPrefix="uc1" %>
<%@ Register Assembly="AspWebControl" Namespace="AspWebControl" TagPrefix="cc3" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../Images/index.css" type="text/css" rel="stylesheet" />

    <script language="javascript" src="../Rms.js" type="text/javascript"></script>

    <title>Ա�����������б�</title>
</head>
<body  style="BORDER-RIGHT: 0px" scroll="no">
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
                        <img align="absMiddle" height="25" src="../images/topic_li.jpg" width="35">�����칫>��������                    </td>
                    <td width="9">
                        <img height="25" src="../images/topic_corr.gif" width="9"></td>
                </tr>
                <tr>
                    <td colspan="2" valign="top" class="tools-area" style="width: 100%;">
                        <img align="absMiddle" src="../images/btn_li.gif">
                    <input class="button" onClick="OpenLargeWindow('GK_OA_EvectionApplyEdit.aspx?Type=Add','EvectionApply')"
                            runat="server" id="NewButton" type="button" value="����" /></td>
                </tr>
            </table>
            <table border="0" cellpadding="0" cellspacing="0" class="search-area" width="100%">
                <tr>
                    <td>
                        <table width="100%">
                            <tr>
                                <td>
                                    ���벿��</td>
                                <td>
                                    <uc1:inputunit ID="DeptTextBox" runat="server" />
                                </td>
                                <td>
                                    ������</td>
                                <td>

                                    <uc2:inputuser ID="ApplyerTextBox" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    ס�ޱ�׼
                                </td>
                                <td>
                                    <asp:TextBox ID="LiveTextBox" Width="80px" runat="server" CssClass="input"></asp:TextBox>�Ǳ���
                                </td>
                                <td>
                                    ����ʱ��
                                </td>
                                <td colspan="3">
                                    <cc3:Calendar ID="StartDateCalendar" runat="server" CalendarResource="../Images/CalendarResource/"
                                        ReadOnly="False" CalendarMode="Date" Display="True" Value="">
                                    </cc3:Calendar>
                                    ��<cc3:Calendar ID="EndDateCalendar" runat="server" CalendarResource="../Images/CalendarResource/"
                                        ReadOnly="False" CalendarMode="Date" Display="True" Value="">
                                    </cc3:Calendar>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    ״̬</td>
                                <td colspan="3">
                                    <asp:CheckBoxList ID="cblStatus" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Selected="True" Value="0">����</asp:ListItem>
                                        <asp:ListItem Value="1">�����</asp:ListItem>
                                        <asp:ListItem Value="2">����</asp:ListItem>
                                        <asp:ListItem Value="3">δͨ��</asp:ListItem>
                                        <asp:ListItem Value="4">����</asp:ListItem>
                                    </asp:CheckBoxList></td>
                                <td align="center" colspan="4">
                                    <input id="btnSearch" runat="server" class="submit" onserverclick="btSearch_Click"
                                        type="button" value="����" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <asp:GridView ID="EvectionGridView" runat="server" AllowPaging="True" CssClass="list"
                HorizontalAlign="Center" Width="100%" DataSourceID="GridViewObjectDataSource"
                AutoGenerateColumns="False" PageSize="17">
                <HeaderStyle CssClass="list-title" />
                <Columns>
                    <asp:TemplateField HeaderText="������">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <a href="#" onClick="javascript:OpenLargeWindow('GK_OA_EvectionApplyEdit.aspx?Code=<%# Eval("Code")%>','EvectionApply');return false;">
                                <%# RmsPM.BLL.SystemRule.GetUserName((string)Eval("Applyer"))%>
                            </a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="����">
                    <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <%# RmsPM.BLL.SystemRule.GetUnitName((string)Eval("Dept"))%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="��������">
                    <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <%# RmsPM.BLL.StringRule.ShowDate((DateTime)Eval("ApplyDate"))%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="LiveLevel" HeaderText="���ݱ�׼" SortExpression="LiveLevel">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Vehicle" HeaderText="��ͨ����" SortExpression="Vehicle">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Code" HeaderText="Code" Visible="False" SortExpression="Code" />
                    <asp:TemplateField HeaderText="״̬" SortExpression="Status">
                    <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <%# RmsOA.BFL.GK_OA_FileChangeBFL.GetManpowerNeedStatusName((string)Eval("Status")) %>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="center" />
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    ��ƥ������
                </EmptyDataTemplate>
            </asp:GridView>
            <asp:ObjectDataSource ID="GridViewObjectDataSource" runat="server" EnablePaging="True"
                MaximumRowsParameterName="MaxRecords" SelectMethod="GetGK_OA_EvectionApplyList"
                SortParameterName="SortColumns" StartRowIndexParameterName="StartRecord" TypeName="RmsOA.BFL.GK_OA_EvectionApplyBFL"
                OldValuesParameterFormatString="original_{0}">
                <SelectParameters>
                    <asp:Parameter Name="sortColumns" Type="String" />
                    <asp:Parameter Name="startRecord" Type="Int32" DefaultValue="0" />
                    <asp:Parameter Name="maxRecords" Type="Int32" DefaultValue="-1" />
                    <asp:ControlParameter ControlID="DeptTextBox" Name="DeptEqual" PropertyName="Value"
                        Type="String" />
                    <asp:ControlParameter ControlID="LiveTextBox" Name="LiveLevelEqual" PropertyName="Text"
                        Type="String" />
                    <asp:ControlParameter ControlID="ApplyerTextBox" Name="ApplyerEqual" PropertyName="Value"
                        Type="String" />
                    <asp:ControlParameter ControlID="StartDateCalendar" Name="ApplyStartDate" PropertyName="Value"
                        Type="DateTime" />
                    <asp:ControlParameter ControlID="EndDateCalendar" Name="ApplyEndDate" PropertyName="Value"
                        Type="DateTime" />
                    <asp:Parameter Name="StatusEqual" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
      </div>
    </form>
</body>
</html>
