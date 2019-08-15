<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TY_UseCarList.aspx.cs" Inherits="RmsOA_TY_UseCarList" %>

<%@ Register TagPrefix="uc1" TagName="InputUser" Src="../UserControls/InputUser.ascx" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>用车列表</title>
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
                                    id="spanTitle">用车列表</span></td>
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
                                            用车部门</td>
                                        <td>
                                            <input id="txtUnit" runat="server" class="input" name="txtUnit" size="8" type="hidden" />&nbsp;
                                            <input id="txtUnitName" runat="server" class="input" name="txtUnit" type="text" />
                                            <img onclick="SelectUnit();return false;" src="../images/ToolsItemSearch.gif" style="cursor: hand" /></td>
                                        <td>
                                            申请人</td>
                                        <td>
                                            <uc1:InputUser ID="ucAccountant" runat="server"></uc1:InputUser>
                                        </td>
                                        <td>
                                            车牌号</td>
                                        <td>
                                            <asp:TextBox ID="txtCarCode"  CssClass="input" runat="server"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            驾驶时间</td>
                                        <td colspan="5">
                                            <cc3:Calendar ID="startDateTime" runat="server" CalendarResource="../Images/CalendarResource/"
                                                ReadOnly="False" CalendarMode="Date" Display="True" Value="">
                                            </cc3:Calendar>
                                            至&nbsp;<cc3:Calendar ID="endDateTime" runat="server" CalendarResource="../Images/CalendarResource/"
                                                ReadOnly="False" CalendarMode="Date" Display="True" Value="">
                                            </cc3:Calendar>
                                        </td>
                                        <td rowspan="2" align="center">
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
                    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" CssClass="list" AllowSorting="True"
                        AutoGenerateColumns="False" DataKeyNames="Code" DataSourceID="ObjectDataSource1"
                        Font-Names="宋体" Font-Size="12px" RowStyle-Height="25" Width="100%" GridLines="Horizontal">
                        <Columns>
                            <asp:TemplateField HeaderText="申请人">
                                <ItemTemplate>
                                    <a href="#" onclick="javascript:OpenMiddleWindow('../WorkFlowPage/YF_OA_UseCar.aspx?ProcedureCode=100002&frameType=List&CaseCode=<%# RmsPM.BLL.WorkFlowRule.GetCaseCodeByProcedureNameAndApplicationCode("用车申请",Eval("Code").ToString()) %>&ApplicationCode=<%# Eval("Code")%>','UseCarDetail');return false;">
                                        <%# RmsPM.BLL.SystemRule.GetUserName((string)Eval("UsePerson"))%>
                                    </a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="UsePerson" HeaderText="使用人" Visible="False" />
                            <asp:TemplateField HeaderText="车牌号">
                                <ItemTemplate>
                                    <%# Eval("CarCode")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="用车部门">
                                <ItemTemplate>
                                    <%# RmsPM.BLL.SystemRule.GetUnitName((string)Eval("DepartCode"))%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="StartDateTime" HeaderText="用车开始时间" />
                            <asp:BoundField DataField="EndDateTime" HeaderText="用车结束时间" />
                            <asp:BoundField DataField="RunKilometres" HeaderText="公里数" />
                            <asp:BoundField DataField="AdressTo" HeaderText="用车地点" />
                            <asp:BoundField DataField="Driver" HeaderText=" 司机" />
                        </Columns>
                        <RowStyle Height="25px" />
                        <HeaderStyle Height="25px" />
                    </asp:GridView>
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" EnablePaging="True" MaximumRowsParameterName="MaxRecords"
                        SelectMethod="GetYF_UseCarFlowList" SortParameterName="SortColumns" StartRowIndexParameterName="StartRecord"
                        TypeName="RmsOA.BFL.YF_UseCarFlowBFL">
                        <SelectParameters>
                            <asp:Parameter Name="sortColumns" Type="String" />
                            <asp:Parameter Name="startRecord" Type="Int32" />
                            <asp:Parameter Name="maxRecords" Type="Int32" />
                            <asp:ControlParameter ControlID="txtCarCode" Name="CarCodeEqual" PropertyName="Text"
                                Type="String" />
                            <asp:ControlParameter ControlID="txtUnit" Name="DepartCodeEqual" PropertyName="Value"
                                Type="String" />
                            <asp:ControlParameter ControlID="ucAccountant" Name="UsePersonEqual" PropertyName="Value"
                                Type="String" />
                            <asp:ControlParameter ControlID="startDateTime" Name="StartDateTimeEqual" PropertyName="Value"
                                Type="DateTime" />
                            <asp:ControlParameter ControlID="endDateTime" Name="EndDateTimeEqual" PropertyName="Value"
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
