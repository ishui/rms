<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CL_CarReport.aspx.cs" Inherits="RmsOA_CL_CarReport" %>

<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>车辆维修费用报表</title>
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
                                    id="spanTitle">车辆维修费用报表</span></td>
                            <td width="9">
                                <img height="25" src="../images/topic_corr.gif" width="9"></td>
                        </tr>
                    </table>
                    <table class="search-area" cellspacing="0" cellpadding="0" border="0">
                        <tr>
                            <td>
                                <table width="100%">
                                    <tr>
                                        <td>
                                            车牌号</td>
                                        <td>
                                            <asp:TextBox ID="txtCarID" runat="server" CssClass="input"></asp:TextBox></td>
                                        <td>
                                            维修日期</td>
                                        <td>
                                            <cc3:Calendar ID="dtDateBegin" runat="server" CalendarResource="../Images/CalendarResource/"
                                                ReadOnly="False" CalendarMode="Date" Display="True" Value="">
                                            </cc3:Calendar>
                                            至<cc3:Calendar ID="dtDateEnd" runat="server" CalendarResource="../Images/CalendarResource/"
                                                ReadOnly="False" CalendarMode="Date" Display="True" Value="">
                                            </cc3:Calendar>
                                        </td>
                                        <td rowspan="4" align="center">
                                            <input type="button" name="button" value="统计" class="button" runat="server" id="Button1"
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
                        AllowSorting="True" CssClass="list" Width="100%">
                        <Columns>
                            <asp:TemplateField HeaderText="车号">
                                <ItemTemplate>
                                    <a href="#" onclick="javascript:OpenMiddleWindow('CL_CarMaintenanceList.aspx?Car_Code=<%# Eval("Car_Code")%>','Detail');return false;">
                                        <%# Eval("Car_Id")%>
                                    </a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Car_Id" HeaderText="车号" />
                            <asp:BoundField DataField="Car_Type" HeaderText="车型" />
                            <asp:TemplateField HeaderText="购买日期">
                                <ItemTemplate>
                                    <%# Eval("Buy_Date").ToString().Substring(0,10)%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Chejia_Id" HeaderText="车架号" />
                            <asp:BoundField DataField="Fadongji_Id" HeaderText="发动机号" />
                            <asp:TemplateField HeaderText="合计公里数">
                                <ItemTemplate>
                                    <a href="#" onclick="javascript:OpenMiddleWindow('CL_CarMaintenanceList.aspx?Car_Code=<%# Eval("Car_Code")%>','Detail');return false;">
                                        <%# Eval("SumMil")%>
                                    </a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="合计维修费">
                                <ItemTemplate>
                                    <a href="#" onclick="javascript:OpenMiddleWindow('CL_CarMaintenanceList.aspx?Car_Code=<%# Eval("Car_Code")%>','Detail');return false;">
                                        <%# Eval("SumPrice")%>
                                    </a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                        </Columns>
                        <PagerStyle CssClass="list-title" />
                        <HeaderStyle CssClass="list-title" />
                        <EmptyDataTemplate>
                            无匹配数据

                        </EmptyDataTemplate>
                    </asp:GridView>
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
