<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CL_CarRegisterList.aspx.cs"
    Inherits="RmsOA_CL_CarRegisterList" %>

<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>车辆登记列表</title>
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
		function OpenReport()
        {
            window.navigate('CL_CarReport.aspx');
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
                                    id="spanTitle">车辆登记列表</span></td>
                            <td width="9">
                                <img height="25" src="../images/topic_corr.gif" width="9"></td>
                        </tr>
                    </table>
                    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table" id="tableToolBar">
                        <tr>
                            <td class="tools-area" width="16">
                                <img src="../images/btn_li.gif" align="absMiddle"></td>
                            <td class="tools-area">
                                <input name="btnNew" id="NewButton" type="button" value="新 增" class="button" runat="server">
                                <input name="btnReport" id="btnReport" type="button" value="报 表 " class="button"
                                    runat="server" onClick="javascript:OpenReport();return false;">
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
                                            <asp:TextBox ID="Index_Num" runat="server" CssClass="input"></asp:TextBox></td>
                                        <td>
                                            车号</td>
                                        <td>
                                            <asp:TextBox ID="Car_Id" runat="server" CssClass="input"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            车型</td>
                                        <td>
                                            <asp:TextBox ID="Car_Type" runat="server" CssClass="input"></asp:TextBox></td>
                                        <td>
                                            购买时间</td>
                                        <td>
                                            <cc3:Calendar ID="BuyDateStart" runat="server" CalendarResource="../Images/CalendarResource/"
                                                ReadOnly="False" CalendarMode="Date" Display="True" Value="">
                                            </cc3:Calendar>
                                            至<cc3:Calendar ID="BuyDateEnd" runat="server" CalendarResource="../Images/CalendarResource/"
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
                            <asp:TemplateField HeaderText="标识序号">
                                <ItemTemplate>
                                    <a href="#" onClick="javascript:OpenMiddleWindow('CL_CarRegisterEdit.aspx?Car_code=<%# Eval("Car_code")%>','ManPowerDetail');return false;">
                                        <%# Eval("Index_num")%>
                                    </a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Car_Id" HeaderText="车号" SortExpression="Car_Id" />
                            <asp:BoundField DataField="Car_Type" HeaderText="车型" SortExpression="Car_Type" />
                            <asp:TemplateField HeaderText="购买日期">
                                <ItemTemplate>
                                    <%# Eval("Buy_Date").ToString().Substring(0,10)%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Chejia_Id" HeaderText="车架号" SortExpression="Chejia_Id" />
                            <asp:BoundField DataField="Fadongji_Id" HeaderText="发动机号" SortExpression="Fadongji_Id" />
                        </Columns>
                        <PagerStyle CssClass="list-title" />
                        <HeaderStyle CssClass="list-title" />
                        <EmptyDataTemplate>
                            无匹配数据

                        </EmptyDataTemplate>
                    </asp:GridView>
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" EnablePaging="True" MaximumRowsParameterName="MaxRecords"
                        SelectMethod="GetGK_OA_CarList" SortParameterName="SortColumns" StartRowIndexParameterName="StartRecord"
                        TypeName="RmsOA.BFL.GK_OA_CarBFL">
                        <SelectParameters>
                            <asp:Parameter Name="sortColumns" Type="String" />
                            <asp:Parameter Name="startRecord" Type="Int32" />
                            <asp:Parameter Name="maxRecords" Type="Int32" />
                            <asp:ControlParameter ControlID="Index_Num" Name="Index_NumLike" PropertyName="Text"
                                Type="String" />
                            <asp:ControlParameter ControlID="Car_Id" Name="Car_IdLike" PropertyName="Text" Type="String" />
                            <asp:ControlParameter ControlID="Car_Type" Name="Car_TypeLike" PropertyName="Text"
                                Type="String" />
                            <asp:ControlParameter ControlID="BuyDateStart" Name="Buy_DateStartEqual" PropertyName="Value"
                                Type="DateTime" />
                            <asp:ControlParameter ControlID="BuyDateEnd" Name="Buy_DateEndEqual" PropertyName="Value"
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
