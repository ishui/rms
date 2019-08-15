<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CL_CarMaintenanceList.aspx.cs"
    Inherits="RmsOA_CL_CarMaintenanceList" %>

<%@ Register Src="../WorkFlowControl/workflowtoolbar.ascx" TagName="workflowtoolbar"
    TagPrefix="uc5" %>
<%@ Register Src="../WorkFlowControl/WorkFlowList.ascx" TagName="WorkFlowList" TagPrefix="uc4" %>
<%@ Register Src="../UserControls/inputcontract.ascx" TagName="inputcontract" TagPrefix="uc3" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Register TagPrefix="uc2" TagName="InputUnit" Src="../UserControls/InputUnit.ascx" %>
<%@ Register TagPrefix="uc1" TagName="InputUser" Src="../UserControls/InputUser.ascx" %>
<%@ Register TagPrefix="uc1" TagName="InputSupplier" Src="../UserControls/InputSupplier.ascx" %>
<%@ Register TagPrefix="cc4" Namespace="Rms.ControlLb" Assembly="RmsPM.BLL" %>
<%@ Register TagPrefix="uc1" TagName="AttachMentAdd" Src="../UserControls/AttachMentAdd.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AttachMentList" Src="../UserControls/AttachMentList.ascx" %>
<%@ Register TagPrefix="uc1" TagName="InputSystemGroup" Src="../UserControls/InputSystemGroup.ascx" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>车辆登记</title>
    <link href="../Images/index.css" type="text/css" rel="stylesheet" />

    <script type="text/javascript" language="javascript" src="../Rms.js"></script>

    <script type="text/javascript" language="javascript">
        
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <table height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td height="25">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="topic" align="center" background="../images/topic_bg.gif" height="25">
                                车辆维修记录</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <table cellspacing="0" cellpadding="0" width="470" border="0">
                        <tr id="webtabs">
                            <td width="20">
                            </td>
                            <td class="TabDisplay" id="workflowmsg" runat="server" width="185">
                                维修保养记录明细</td>
                        </tr>
                    </table>
                    <table cellspacing="0" cellpadding="0" width="100%" border="0" id="tabdiv">
                        <tr>
                            <td>
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                                    AllowSorting="True" CssClass="list" Width="100%" DataSourceID="ObjectDataSource1"
                                    GridLines="Horizontal" ShowFooter="True">
                                    <HeaderStyle CssClass="list-title" />
                                    <FooterStyle CssClass="list-title" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="维修/保养内容">
                                            <ItemTemplate>
                                                <%# Eval("MValue")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="日期">
                                            <ItemTemplate>
                                                <%# Eval("MDate").ToString().Substring(0,10)%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Mil" HeaderText="公里数" SortExpression="Mil"  />
                                        <asp:BoundField DataField="MPrice" HeaderText="花费金额" SortExpression="MPrice" />
                                    </Columns>
                                    <PagerStyle CssClass="list-title" />
                                    <HeaderStyle CssClass="list-title" />
                                    <EmptyDataTemplate>
                                        无匹配数据

                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" EnablePaging="True" MaximumRowsParameterName="MaxRecords"
                        SelectMethod="GetGK_OA_CarMaintenanceList" SortParameterName="SortColumns" StartRowIndexParameterName="StartRecord"
                        TypeName="RmsOA.BFL.GK_OA_CarMaintenanceBFL" OnSelected="ObjectDataSource1_Selected">
                        <SelectParameters>
                            <asp:Parameter Name="sortColumns" Type="String" />
                            <asp:Parameter Name="startRecord" Type="Int32" />
                            <asp:Parameter Name="maxRecords" Type="Int32" />
                            <asp:QueryStringParameter Name="Car_CodeEqual" QueryStringField="Car_Code" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
