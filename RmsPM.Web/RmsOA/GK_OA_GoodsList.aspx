<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GK_OA_GoodsList.aspx.cs" Inherits="RmsOA_GK_OA_GoodsList" %>

<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>低耗品列表</title>
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
                                    id="spanTitle">低耗品列表</span></td>
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
                                        <td>
                                            低耗品名称</td>
                                        <td>
                                            <asp:TextBox ID="GoodsNameTextBox" runat="server" CssClass="input"></asp:TextBox></td>
                                        <td>
                                            低耗品编号</td>
                                        <td>
                                            <asp:TextBox ID="GoodsCodeTextBox" runat="server" CssClass="input"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            部门</td>
                                        <td>
                                            <input id="txtUnit" runat="server" class="input" name="txtUnit" type="hidden" /><input
                                                id="txtUnitName" runat="server" class="input" name="txtUnit" type="text" /><img onclick="SelectUnit();return false;"
                                                    src="../images/ToolsItemSearch.gif" style="cursor: hand" /></td>
                                        <td>
                                            入库时间</td>
                                        <td>
                                            <cc3:Calendar ID="RegisterDateStart" runat="server" CalendarResource="../Images/CalendarResource/"
                                                ReadOnly="False" CalendarMode="Date" Display="True" Value="">
                                            </cc3:Calendar>
                                            至<cc3:Calendar ID="RegisterDateEnd" runat="server" CalendarResource="../Images/CalendarResource/"
                                                ReadOnly="False" CalendarMode="Date" Display="True" Value="">
                                            </cc3:Calendar>
                                        </td>
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
                        AllowSorting="True" CssClass="list" Width="100%" DataSourceID="ObjectDataSource1">
                        <Columns>
                            <asp:TemplateField HeaderText="低耗品名称">
                                <ItemTemplate>
                                    <a href="#" onclick="javascript:OpenMiddleWindow('GK_OA_GoodsEdit.aspx?Code=<%# Eval("Code")%>','GoodsDetail');return false;">
                                        <%# Eval("GoodsName")%>
                                    </a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="GoodsCode" HeaderText="低耗品编号" SortExpression="GoodsCode" />
                            <asp:BoundField DataField="GoodsType" HeaderText="类别" SortExpression="GoodsType" />
                            <asp:BoundField DataField="GoodsPart" HeaderText="规格" SortExpression="GoodsPart" />
                            <asp:BoundField DataField="GoodsNumber" HeaderText="数量" SortExpression="GoodsNumber" />
                            <asp:BoundField DataField="GoodsPrice" HeaderText="单价" SortExpression="GoodsPrice" />
                            <asp:BoundField DataField="InputDate" HeaderText="入库时间" SortExpression="InputDate" />
                        </Columns>
                        <PagerStyle CssClass="list-title" />
                        <HeaderStyle CssClass="list-title" />
                        <EmptyDataTemplate>
                            无匹配数据

                        </EmptyDataTemplate>
                    </asp:GridView>
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" EnablePaging="True" MaximumRowsParameterName="MaxRecords"
                        SelectMethod="GetGK_OA_GoodsList" SortParameterName="SortColumns" StartRowIndexParameterName="StartRecord"
                        TypeName="RmsOA.BFL.GK_OA_GoodsBFL">
                        <SelectParameters>
                            <asp:Parameter Name="sortColumns" Type="String" />
                            <asp:Parameter Name="startRecord" Type="Int32" />
                            <asp:Parameter Name="maxRecords" Type="Int32" />
                            
                            <asp:ControlParameter ControlID="GoodsNameTextBox" Name="GoodsNameEqual" PropertyName="Text"
                                Type="String" />
                            <asp:ControlParameter ControlID="GoodsCodeTextBox" Name="GoodsCodeEqual" PropertyName="Text"
                                Type="String" />
                                
                            <asp:ControlParameter ControlID="txtUnit" Name="UnitCodeEqual" PropertyName="Value"
                                Type="String" />
                            <asp:ControlParameter ControlID="RegisterDateStart" Name="InputDateEqualStart"
                                PropertyName="Value" Type="DateTime" />
                            <asp:ControlParameter ControlID="RegisterDateEnd" Name="InputDateEqualEnd"
                                PropertyName="Value" Type="DateTime" />
                                
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
