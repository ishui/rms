<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LocaleViseList.aspx.cs" Inherits="LocaleVise_LocaleViseList" %>

<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Register TagPrefix="uc2" TagName="InputUnit" Src="../UserControls/InputUnit.ascx" %>
<%@ Register TagPrefix="uc1" TagName="InputUser" Src="../UserControls/InputUser.ascx" %>
<%@ Register TagPrefix="uc1" TagName="InputSupplier" Src="../UserControls/InputSupplier.ascx" %>
<%@ Register TagPrefix="cc4" Namespace="Rms.ControlLb" Assembly="RmsPM.BLL" %>
<%@ Register Src="../UserControls/inputcontract.ascx" TagName="inputcontract" TagPrefix="uc3" %>
<%@ Register TagPrefix="cc1" Namespace="Tiannuo.Web.Controls" Assembly="DataGridWebControls" %>
<%@ Register TagPrefix="uc1" TagName="InputSystemGroup" Src="../UserControls/InputSystemGroup.ascx" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>签证</title>
    <link href="../Images/index.css" type="text/css" rel="stylesheet">

    <script language="javascript" src="../Rms.js"></script>

    <script>
	function OpenNew()
	{
		OpenFullWindow('LocaleViseInfo.aspx?ProjectCode=<%= Request["ProjectCode"]+"" %>','现场签证新增');
	}
	function OpenModify(Code)
	{
		OpenFullWindow('LocaleViseInfo.aspx?ProjectCode=<%= Request["ProjectCode"]+"" %>&ViseCode='+Code,'现场签证修改');
	}
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <table height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td bgcolor="#e4eff6" height="6">
                </td>
            </tr>
            <tr>
                <td height="25">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="topic" background="../images/topic_bg.gif">
                                <img height="25" src="../images/topic_li.jpg" width="35" align="absMiddle"><span
                                    id="spanTitle"> 项目管理 > 签证管理</span></td>
                            <td width="9">
                                <img height="25" src="../images/topic_corr.gif" width="9"></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="tools-area" valign="top">
                    <img src="../images/btn_li.gif" align="absMiddle">
                    <input name="btnNew" id="btnNew" type="button" value=" 新增 " class="button" runat="server"
                        onclick="javascript:OpenNew();">
                </td>
            </tr>
            <tr height="100%">
                <td class="table" valign="top">
                    <table width="100%" height="100%">
                        <tr>
                            <td>
                                <table class="search-area" cellspacing="0" width="100%" cellpadding="0" border="0">
                                    <tr>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td nowrap>
                                                        签证编号：</td>
                                                    <td>
                                                        <asp:TextBox ID="ViseIdTextBox" runat="server" CssClass="input"></asp:TextBox></td>
                                                    <td nowrap>
                                                        签证名称：</td>
                                                    <td>
                                                        <asp:TextBox ID="ViseNameTextBox" runat="server" CssClass="input"></asp:TextBox></td>
                                                    <td nowrap>
                                                        经办人：</td>
                                                    <td nowrap>
                                                        <uc1:InputUser ID="VisePersonTextBox" runat="server"></uc1:InputUser>
                                                    </td>
                                                    <td rowspan="5" nowrap>
                                                        &nbsp;<asp:Button ID="btnQuery" runat="server" Text="搜 索" CssClass="submit" OnClick="btnQuery_Click" />&nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td nowrap>
                                                        承 包 商：</td>
                                                    <td>
                                                        <uc1:InputSupplier ID="ViseSupplierTextBox" runat="server"></uc1:InputSupplier>
                                                    </td>
                                                    <td nowrap>
                                                        合同相关：</td>
                                                    <td colspan="3">
                                                        <uc3:inputcontract ID="ViseContractCodeTextBox" runat="server" ImagePath="../Images/" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td nowrap>
                                                        部 门：</td>
                                                    <td colspan="5">
                                                        <uc2:InputUnit ID="ViseUnitTextBox" runat="server"></uc2:InputUnit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td nowrap>
                                                        签证日期：</td>
                                                    <td colspan="1" nowrap>
                                                        <cc3:Calendar ID="ViseDateStartTextBox" runat="server" CalendarResource="../Images/CalendarResource/"
                                                            ReadOnly="False" Display="True" Value="">
                                                        </cc3:Calendar>
                                                        --&gt;
                                                        <cc3:Calendar ID="ViseDateEndTextBox" runat="server" CalendarResource="../Images/CalendarResource/"
                                                            ReadOnly="False" Display="True" Value="">
                                                        </cc3:Calendar>
                                                    </td>
                                                    <td align="right" nowrap>
                                                        状 态：</td>
                                                    <td colspan="3" nowrap>
                                                        <asp:CheckBoxList ID="ViseStatusCheckBoxList" runat="server" RepeatColumns="4" RepeatDirection="Horizontal">
                                                            <asp:ListItem Value="1">待审</asp:ListItem>
                                                            <asp:ListItem Value="2">审核中</asp:ListItem>
                                                            <asp:ListItem Value="3">已审</asp:ListItem>
                                                            <asp:ListItem Value="4">作废</asp:ListItem>
                                                        </asp:CheckBoxList></td>
                                                </tr>
                                                <tr>
                                                    <td nowrap>
                                                        签证期限：</td>
                                                    <td colspan="1">
                                                        <cc3:Calendar ID="ViseEndDateStartTextBox" runat="server" CalendarResource="../Images/CalendarResource/"
                                                            ReadOnly="False" Display="True" Value="">
                                                        </cc3:Calendar>
                                                        --&gt;
                                                        <cc3:Calendar ID="ViseEndDateEndTextBox" runat="server" CalendarResource="../Images/CalendarResource/"
                                                            ReadOnly="False" Display="True" Value="">
                                                        </cc3:Calendar>
                                                    </td>
                                                    <td align="right" nowrap>
                                                        类 型：</td>
                                                    <td colspan="3">
                                                        <uc1:InputSystemGroup ID="inputSystemGroup" ClassCode="2201" runat="server" SelectAllLeaf="True">
                                                        </uc1:InputSystemGroup>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr height="100%">
                            <td class="table" valign="top">
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                                    AllowSorting="True" DataKeyNames="ViseCode" CssClass="list" OnDataBinding="GridView1_DataBinding"
                                    Width="100%" DataSourceID="ObjectDataSource1">
                                    <Columns>
                                        <asp:BoundField DataField="ViseId" HeaderText="编号" SortExpression="ViseId" />
                                        <asp:TemplateField HeaderText="名称" SortExpression="ViseName">
                                            <ItemTemplate>
                                                <a href="#" onclick="javascript:OpenModify('<%# Eval("ViseCode") %>');">
                                                    <%# Eval("ViseName") %>
                                                </a>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="经办人" SortExpression="VisePerson">
                                            <ItemTemplate>
                                                <%# RmsPM.BLL.SystemRule.GetUserName((string)Eval("VisePerson")) %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="类型" SortExpression="ViseType">
                                            <ItemTemplate>
                                                <%# RmsPM.BLL.SystemGroupRule.GetSystemGroupFullName((string)Eval("ViseType"))%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ViseEndDate" HeaderText="办理期限" SortExpression="ViseEndDate"
                                            DataFormatString="{0:d}" HtmlEncode="False" />
                                        <asp:BoundField DataField="ViseDate" HeaderText="签证日期" SortExpression="ViseDate"
                                            DataFormatString="{0:d}" HtmlEncode="False" />
                                        <asp:TemplateField HeaderText="结算" SortExpression="ViseBalanceStatus">
                                            <ItemTemplate>
                                                <%# ((int)Eval("ViseBalanceStatus") == 1) ? "未结算" : (((int)Eval("ViseBalanceStatus") == 2) ? "已结算" : "待结算")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="状态" SortExpression="ViseStatus">
                                            <ItemTemplate>
                                                <%# RmsPM.BFL.LocaleViseBFL.GetViseStatusName((int)Eval("ViseStatus")) %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle CssClass="list-title" />
                                    <HeaderStyle CssClass="list-title" />
                                    <EmptyDataTemplate>
                                        无匹配数据

                                    </EmptyDataTemplate>
                                    <RowStyle HorizontalAlign="Center" />
                                </asp:GridView>
                                <table width="100%" class="list">
                                    <tr class="list-title">
                                        <td style="height: 23px">
                                            共

                                            <asp:Label runat="server" ID="lblRecordCount">0</asp:Label>
                                            条

                                        </td>
                                    </tr>
                                </table>
                                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetLocalVises"
                                    TypeName="RmsPM.BFL.LocaleViseBFL" EnablePaging="True" MaximumRowsParameterName="MaxRecords"
                                    SortParameterName="SortColumns" StartRowIndexParameterName="StartRecord" OnSelected="ObjectDataSource1_Selected">
                                    <SelectParameters>
                                        <asp:Parameter Name="SortColumns" Type="String" />
                                        <asp:Parameter Name="StartRecord" Type="Int32" />
                                        <asp:Parameter Name="MaxRecords" Type="Int32" />
                                        <asp:ControlParameter ControlID="ViseIdTextBox" Name="ViseId" PropertyName="Text"
                                            Type="String" />
                                        <asp:ControlParameter ControlID="ViseNameTextBox" Name="ViseName" PropertyName="Text"
                                            Type="String" />
                                        <asp:ControlParameter ControlID="VisePersonTextBox" Name="VisePerson" PropertyName="Value"
                                            Type="String" />
                                        <asp:ControlParameter ControlID="ViseSupplierTextBox" Name="ViseSupplier" PropertyName="Value"
                                            Type="String" />
                                        <asp:ControlParameter ControlID="ViseContractCodeTextBox" Name="ViseContractCode"
                                            PropertyName="Value" Type="String" />
                                        <asp:ControlParameter ControlID="ViseUnitTextBox" Name="ViseUnit" PropertyName="Value"
                                            Type="String" />
                                        <asp:ControlParameter ControlID="ViseDateStartTextBox" Name="ViseDateStart" PropertyName="Value"
                                            Type="DateTime" />
                                        <asp:ControlParameter ControlID="ViseDateEndTextBox" Name="ViseDateEnd" PropertyName="Value"
                                            Type="DateTime" />
                                        <asp:ControlParameter ControlID="ViseEndDateStartTextBox" Name="ViseEndDateStart"
                                            PropertyName="Value" Type="DateTime" />
                                        <asp:ControlParameter ControlID="ViseEndDateEndTextBox" Name="ViseEndDateEnd" PropertyName="Value"
                                            Type="DateTime" />
                                        <asp:QueryStringParameter Name="ViseProject" QueryStringField="ProjectCode" Type="String" />
                                        <asp:ControlParameter ControlID="inputSystemGroup" Name="ViseType" PropertyName="Value"
                                            Type="String" />
                                        <asp:Parameter Name="ViseBalanceStatusInStr" Type="String" />
                                        <asp:Parameter Name="ViseStatusInStr" Type="String" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td height="12">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td background="../images/corl_bg.gif">
                                <img height="12" src="../images/corl.gif" width="12"></td>
                            <td width="12">
                                <img height="12" src="../images/corr.gif" width="12"></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td bgcolor="#e4eff6" height="6">
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
