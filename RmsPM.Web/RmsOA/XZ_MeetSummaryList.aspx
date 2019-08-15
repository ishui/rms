<%@ Page Language="C#" AutoEventWireup="true" CodeFile="XZ_MeetSummaryList.aspx.cs"
    Inherits="RmsOA_XZ_MeetSummaryList" %>

<%@ Register Src="../UserControls/inputuser.ascx" TagName="inputuser" TagPrefix="uc2" %>

<%@ Register Src="../UserControls/inputunit.ascx" TagName="inputunit" TagPrefix="uc1" %>
<%@ Register Assembly="AspWebControl" Namespace="AspWebControl" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../Images/index.css" type="text/css" rel="stylesheet" />

    <script language="javascript" src="../Rms.js" type="text/javascript"></script>

    <title>�����Ҫ�б�</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td background="../images/topic_bg.gif" class="topic">
                        <img align="absMiddle" height="25" src="../images/topic_li.jpg" width="35">��������>�����Ҫ
                    </td>
                    <td width="9">
                        <img height="25" src="../images/topic_corr.gif" width="9"></td>
                </tr>
            </table>
            <table width="100%" cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td class="tools-area" valign="top">
                        <img align="absMiddle" src="../images/btn_li.gif">
                        <input id="NewButton" runat="server" class="button" onclick="OpenLargeWindow('XZ_MeetSummaryEdit.aspx?Type=Add','MeetSummaryEdit')"
                            type="button" value="����" />
                    </td>
                </tr>
            </table>
            <table border="0" cellpadding="0" cellspacing="0" class="search-area" width="100%">
                <tr>
                    <td>
                        <table width="100%">
                            <tr>
                                <td>��������
                                </td>
                                <td>
                                    <asp:TextBox ID="TitleTextBox" runat="server" CssClass="input"></asp:TextBox>
                                </td>
                                <td>
                                    ����
                                </td>
                                <td>
                                    <uc1:inputunit ID="DeptInputunit" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    ��¼��
                                </td>
                                <td>
                                    &nbsp;<uc2:inputuser ID="RecorderInputuser" runat="server" />
                                </td>
                                <td>
                                    ��������
                                </td>
                                <td>
                                    <asp:DropDownList ID="TypeDropDownList" runat="server" DataSourceID="ObjectDataSource2" Font-Size="9pt">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    ʱ��
                                </td>
                                <td>
                                    <cc1:Calendar ID="dateBegin" runat="server" CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                                        Value="">
                                    </cc1:Calendar>
                                    &nbsp;-->
                                    <cc1:Calendar ID="dateEnd" runat="server" CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                                        Value="">
                                    </cc1:Calendar>
                                </td>
                                <td>
                                    ״̬</td>
                                <td>
                                    <asp:CheckBoxList ID="cblStatus" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Selected="True" Value="0">����</asp:ListItem>
                                        <asp:ListItem Value="1">�����</asp:ListItem>
                                        <asp:ListItem Value="2">����</asp:ListItem>
                                        <asp:ListItem Value="3">δͨ��</asp:ListItem>
                                        <asp:ListItem Value="4">����</asp:ListItem>
                                    </asp:CheckBoxList></td>
                                <td colspan="4" align="center">
                                    <input id="Button1" runat="server" class="submit" onserverclick="SearchButton_Click"
                                        type="button" value="����" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <asp:GridView ID="SummaryGridView" runat="server" AllowPaging="True" PageSize="15"
                Width="100%" CssClass="list" AutoGenerateColumns="False" DataSourceID="ObjectDataSource1">
                <FooterStyle CssClass="list-title" />
                <RowStyle HorizontalAlign="center" />
                <HeaderStyle CssClass="list-title" HorizontalAlign="Center" />
                <Columns>
                    <asp:TemplateField HeaderText="��������">
                        <ItemTemplate>
                            <a href="#" onclick="OpenLargeWindow('XZ_MeetSummaryEdit.aspx?Type=Read&Code=<%#Eval("Code")%>','MeetSummary')">
                                <%# Eval("Title")%>
                            </a>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle CssClass="list-title" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:BoundField SortExpression="MeetStartTime" DataField="MeetStartTime" HeaderText="����ʱ��" DataFormatString="{0: yyyy��MM��dd�� HH:mm}" HtmlEncode="false" />
                    <asp:TemplateField HeaderText="���鲿��">
                    <ItemTemplate>
                        <%# RmsPM.BLL.SystemRule.GetUnitName((string)Eval("Dept"))%>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="Type" HeaderText="��������" SortExpression="Type">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="�� &#160;&#160; ¼">
                    <ItemTemplate>
                       <%# RmsPM.BLL.SystemRule.GetUserName((string)Eval("Recoder"))%>
                    </ItemTemplate>                   
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="״̬" SortExpression="Status">
                        <ItemTemplate>
                            <%# RmsOA.BFL.GK_OA_FileChangeBFL.GetManpowerNeedStatusName((string)Eval("Status")) %>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    ��ƥ������
                </EmptyDataTemplate>
            </asp:GridView>
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetGK_OA_MeetSummaryList"
                TypeName="RmsOA.BFL.GK_OA_MeetSummaryBFL" SortParameterName="SortColumns" StartRowIndexParameterName="StartRecord" MaximumRowsParameterName="MaxRecords" OldValuesParameterFormatString="original_{0}">
                <SelectParameters>
                    <asp:Parameter Name="SortColumns" Type="String"/>
                    <asp:Parameter DefaultValue="0" Name="StartRecord" Type="Int32" />
                    <asp:Parameter DefaultValue="-1" Name="MaxRecords" Type="Int32" />
                    <asp:Parameter Name="CodeEqual" Type="Int32" />
                    <asp:Parameter Name="SortCodeEqual" Type="String" />
                    <asp:Parameter DefaultValue="" Name="CodeRuleEqual" Type="String" />
                    <asp:Parameter Name="MeetStartTimeEqual" Type="DateTime" DefaultValue="" />
                    <asp:Parameter Name="PlaceEqual" Type="String" />
                    <asp:ControlParameter ControlID="TitleTextBox" Name="TitleEqual" PropertyName="Text"
                        Type="String" />
                    <asp:Parameter Name="AttendPersonsEqual" Type="String" />
                    <asp:ControlParameter Name="RecoderEqual" Type="String" ControlID="RecorderInputuser" PropertyName="Value"/>
                    <asp:Parameter Name="ContextEqual" Type="String" />
                    <asp:Parameter Name="OtherContextEqual" Type="String" />
                    <asp:ControlParameter ControlID="TypeDropDownList" Name="TypeEqual" PropertyName="SelectedValue"
                        Type="String" />
                        <asp:ControlParameter Type="String" Name="DeptEqual" PropertyName="Value" ControlID="DeptInputunit" />
                    <asp:Parameter Name="StatusEqual" Type="String" />
                    <asp:Parameter Name="CharterMemberEqual" Type="String" />
                    <asp:Parameter Name="OtherPersonEqual" Type="String" />
                    <asp:Parameter Name="MeetEndTimeEqual" Type="DateTime" />
                    <asp:Parameter Name="SmallTitleEqual" Type="String" />
                    <asp:Parameter Name="SendStatusEqual" Type="String" />
                    <asp:Parameter Name="PreLeaveEqual" Type="String" />
                    <asp:Parameter Name="SubmitTimeEqual" Type="DateTime" />
                    <asp:Parameter Name="SubmiterEqual" Type="String" />
                    <asp:ControlParameter Name="startTime" ControlID="dateBegin" Type="DateTime" PropertyName="Value" />
                    <asp:ControlParameter Name="endTime" ControlID="dateEnd" Type="DateTime" PropertyName="Value" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="GetConferenceType"
                TypeName="RmsOA.BFL.ConferenceManageBFL"></asp:ObjectDataSource>
        </div>
    </form>
</body>
</html>
