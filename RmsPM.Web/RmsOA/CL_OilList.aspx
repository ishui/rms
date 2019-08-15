<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CL_OilList.aspx.cs" Inherits="RmsOA_CL_OilList" %>

<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>油料领用列表</title>
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
<body style="BORDER-RIGHT: 0px" scroll="no">
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
                                    id="spanTitle">油料领用列表</span></td>
                            <td width="9">
                                <img height="25" src="../images/topic_corr.gif" width="9"></td>
                        </tr>
                    </table>
                    <table class="table" id="tableToolBar" width="100%">
                        <tr>
                            <td class="tools-area" width="16">
                                <img src="../images/btn_li.gif" align="absMiddle"></td>
                            <td class="tools-area">
                                <input name="btnNew" id="NewButton" type="button" value="新增油料领用" class="button" runat="server">
                            </td>
                        </tr>
                    </table>
                    <table class="search-area" cellspacing="0" cellpadding="0" border="0">
                        <tr>
                            <td>
                                <table width="100%">
                                    <tr>
                                        <td>
                                            标识序号</td>
                                        <td>
                                            <asp:TextBox ID="IndexNum" runat="server" CssClass="input"></asp:TextBox></td>
                                        <td>
                                            车号</td>
                                        <td>
                                            <asp:TextBox ID="Car_Id" runat="server" CssClass="input"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            领用日期</td>
                                        <td colspan="3">
                                            <cc3:Calendar ID="GetDateStart" runat="server" CalendarResource="../Images/CalendarResource/"
                                                ReadOnly="False" CalendarMode="Date" Display="True" Value="">
                                            </cc3:Calendar>
                                            至<cc3:Calendar ID="GetDateEnd" runat="server" CalendarResource="../Images/CalendarResource/"
                                                ReadOnly="False" CalendarMode="Date" Display="True" Value="">
                                            </cc3:Calendar>
                                        </td>
                                        <td rowspan="4" align="center">
                                            <input type="button" name="button" value="搜索" class="button" runat="server" id="Button1"
                                                onserverclick="Button1_ServerClick" /></td>
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
                        AllowSorting="True" CssClass="list" Width="100%" DataSourceID="ObjectDataSource1"   GridLines="Horizontal" ShowFooter="True">
                        <Columns>
                            <asp:TemplateField HeaderText="领用人">
                                <ItemTemplate>
                                    <a href="#" onClick="javascript:OpenMiddleWindow('CL_OilEdit.aspx?Code=<%# Eval("Code")%>','ManPowerDetail');return false;">
                                        <%# Eval("GetMan")%>
                                    </a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Car_Id" HeaderText="车号" SortExpression="Car_Id" />
                            <asp:BoundField DataField="GetNum" HeaderText="领用数量" SortExpression="GetNum" />
                            <asp:BoundField DataField="GetDate" HeaderText="领用日期" SortExpression="GetDate" />
                            <asp:BoundField DataField="FirstMil" HeaderText="上次公里数" SortExpression="FirstMil" />
                            <asp:BoundField DataField="ThisMil" HeaderText="本次公里数" SortExpression="ThisMil" />
                            <asp:BoundField DataField="FactMil" HeaderText="实际公里数" SortExpression="FactMil" />
                        </Columns>
                        <PagerStyle CssClass="list-title" />
                        <HeaderStyle CssClass="list-title" />
                        <EmptyDataTemplate>
                            无匹配数据
                        </EmptyDataTemplate>
                    </asp:GridView>
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" EnablePaging="True" MaximumRowsParameterName="MaxRecords"
                        SelectMethod="GetGK_OA_OilList" SortParameterName="SortColumns" StartRowIndexParameterName="StartRecord"  OnSelected="ObjectDataSource1_Selected"
                        TypeName="RmsOA.BFL.GK_OA_OilBFL">
                        <SelectParameters>
                            <asp:Parameter Name="sortColumns" Type="String" />
                            <asp:Parameter Name="startRecord" Type="Int32" />
                            <asp:Parameter Name="maxRecords" Type="Int32" />
                            <asp:ControlParameter ControlID="IndexNum" Name="IndexNumLike" PropertyName="Text"
                                Type="String" />
                            <asp:ControlParameter ControlID="Car_Id" Name="Car_IdLike" PropertyName="Text" Type="String" />
                            <asp:ControlParameter ControlID="GetDateStart" Name="GetDateStartEqual" PropertyName="Value"
                                Type="DateTime" />
                            <asp:ControlParameter ControlID="GetDateEnd" Name="GetDateEndEqual" PropertyName="Value"
                                Type="DateTime" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </td>
            </tr>
            <tr>
                <td height="12">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td background="../images/corl_bg.gif">
                                <img height="12" src="../images/corl.gif" width="12" /></td>
                            <td width="12">
                                <img height="12" src="../images/corr.gif" width="12" /></td>
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
