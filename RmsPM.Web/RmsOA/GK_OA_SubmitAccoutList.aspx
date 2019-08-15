<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GK_OA_SubmitAccoutList.aspx.cs"
    Inherits="RmsOA_GK_OA_SubmitAccoutList" %>

<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>车改人员报销列表</title>
    <link href="../Images/index.css" type="text/css" rel="stylesheet" />

    <script type="text/javascript" language="javascript" src="../Rms.js"></script>

    <script type="text/javascript">
		function SelectUnit()
		{
			OpenMiddleWindow("../SelectBox/SelectUnit.aspx?UnitCode=000000");
		}
		function SelectUnitReturn(code, name)
		{
			window.document.all.txtUnitName.value = name;
			window.document.all.txtUnit.value = code;
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
                                    id="spanTitle">车改人员报销列表</span></td>
                            <td width="9">
                                <img height="25" src="../images/topic_corr.gif" width="9"></td>
                        </tr>
                    </table>
                    <table class="table" id="tableToolBar" width="100%">
                        <tr>
                            <td class="tools-area" width="16">
                                <img src="../images/btn_li.gif" align="absMiddle"></td>
                            <td class="tools-area">
                                <input name="btnNew" id="NewButton" type="button" value=" 新增" class="button" runat="server">
                            </td>
                        </tr>
                    </table>
                    <table class="search-area" cellspacing="0" cellpadding="0" border="0">
                        <tr>
                            <td>
                                <table width="100%">
                                    <tr>
                                        <td runat="server" visible="false">
                                            质量记录分类号</td>
                                        <td>
                                            <asp:TextBox ID="SystemCodeTextBox" Visible="false" runat="server" CssClass="input"></asp:TextBox>
                                        </td>
                                        <td>
                                            标识序号</td>
                                        <td>
                                            <asp:TextBox ID="FileCodeTextBox" runat="server" CssClass="input"></asp:TextBox></td>
                                        <td>
                                            名称</td>
                                        <td>
                                            <asp:TextBox ID="NameTextBox" runat="server" CssClass="input"></asp:TextBox></td>
                                        <td>
                                            职务</td>
                                        <td>
                                            <asp:TextBox ID="DutiesTextBox" runat="server" CssClass="input"></asp:TextBox></td>
                                        <td rowspan="4" align="center">
                                            <input type="button" name="button" value="搜索" class="button" runat="server" id="Button1"
                                                onserverclick="Button1_ServerClick"></td>
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
                        AllowSorting="True" CssClass="list" Width="100%" DataSourceID="ObjectDataSource1" OnRowDataBound="GridView1_RowDataBound" >
                        <Columns>
                            <asp:TemplateField HeaderText="姓名">
                                <ItemTemplate>
                                    <a href="#" onclick="javascript:OpenMiddleWindow('GK_OA_SubmitAccountEdit.aspx?Code=<%# Eval("Code")%>','FileChangeDetail');return false;">
                                        <%# Eval("Name")%>
                                    </a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Duties" HeaderText="职务" SortExpression="Duties" />
                            <asp:TemplateField HeaderText="合计标准费用">
                                <ItemTemplate>
                                    <asp:Label ID="StandardCostLabel" runat="server" Text="Label"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerStyle CssClass="list-title" />
                        <HeaderStyle CssClass="list-title" />
                        <EmptyDataTemplate>
                            无匹配数据

                        </EmptyDataTemplate>
                    </asp:GridView>
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" EnablePaging="True" MaximumRowsParameterName="MaxRecords"
                        SelectMethod="GetGK_OA_SubmitAccountMainList" SortParameterName="SortColumns"
                        StartRowIndexParameterName="StartRecord" TypeName="RmsOA.BFL.GK_OA_SubmitAccountMainBFL" >
                        <SelectParameters>
                            <asp:Parameter Name="sortColumns" Type="String" />
                            <asp:Parameter Name="startRecord" Type="Int32" />
                            <asp:Parameter Name="maxRecords" Type="Int32" />
                            <asp:ControlParameter ControlID="SystemCodeTextBox" Name="SystemCodeEqual" PropertyName="Text"
                                Type="String" />
                            <asp:ControlParameter ControlID="FileCodeTextBox" Name="FileCodeEqual" PropertyName="Text"
                                Type="String" />
                            <asp:ControlParameter ControlID="NameTextBox" Name="NameEqual" PropertyName="Text"
                                Type="String" />
                            <asp:ControlParameter ControlID="DutiesTextBox" Name="DutiesEqual" PropertyName="Text"
                                Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                     
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
