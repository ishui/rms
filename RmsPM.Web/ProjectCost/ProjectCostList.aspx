<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProjectCostList.aspx.cs" Inherits="ProjectCost_ProjectCostList" %>
<%@ Register TagPrefix="cc4" Namespace="Rms.ControlLb" Assembly="RmsPM.BLL" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<title>ProjectCostList</title>
<link href="../Images/index.css" type="text/css" rel="stylesheet">
<LINK href="../Images/infra.css" type="text/css" rel="stylesheet">
 <script language="javascript" src="../Rms.js"></script>
 <script language="javascript">
function OpenModify(Code)
{   
	OpenLargeWindow('ProjectCostInfo.aspx?ProjectCode=<%= Request["ProjectCode"] %>&ProjectCostCode='+Code,'材料维护');
}
</script>
 
 
</head>
<body bgcolor="#ffffff" style="BORDER-RIGHT:0px">
    <form id="form1" runat="server">
        <table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="#ffffff">
	        <tr height="100%">
	            <td>
			        <table width="100%" height="100%">
			            <tr height="100%">
			                <td>
	                           <div style="OVERFLOW:auto;WIDTH:100%;POSITION:absolute;HEIGHT:100%">
                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                                        AllowPaging="True" AllowSorting="True" CssClass="list" PageSize="15" 
                                        Width="100%" DataSourceID="ObjectDataSource1" GridLines="Horizontal">
                                        <Columns>
                                            <asp:TemplateField HeaderText="项目名称" SortExpression="ProjectName">
                                                <ItemTemplate>
                                                    <a href="#" onclick="javascript:OpenModify('<%# Eval("ProjectCostCode") %>');return false;">
                                                        <%# Eval("ProjectName")%>
                                                    </a>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="费用项" SortExpression="GroupCode">
                                                <ItemTemplate>
                                                    <%# RmsPM.BLL.SystemGroupRule.GetSystemGroupFullName((string)Eval("GroupCode"))%>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="面 积(平米)" SortExpression="Area">
                                                <ItemTemplate>
                                                        <%#  RmsPM.BLL.MathRule.GetDecimalShowString(Eval("Area"))%>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="单方造价(元)" SortExpression="Price">
                                                <ItemTemplate>
                                                   <%#  RmsPM.BLL.MathRule.GetDecimalShowString(Eval("Price"))%>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="总价(元)" SortExpression="Money">
                                                <ItemTemplate>
                                                   <%# RmsPM.BLL.MathRule.GetDecimalShowString( Eval("Money"))%>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="说 明" SortExpression="Remark">
                                                <ItemTemplate>
                                                   <%# RmsPM.BLL.StringRule.TruncText((Object)Eval("Remark"), 20)%>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerStyle CssClass="list-title" />
                                        <HeaderStyle CssClass="list-title" />
                                        <EmptyDataTemplate>
                                            无匹配数据
                                        </EmptyDataTemplate>
                                        <RowStyle HorizontalAlign="Center" />
                                    </asp:GridView>
                                   
                                    <asp:ObjectDataSource ID="ObjectDataSource1" OnSelected="ObjectDataSource1_Selected"
                                    runat="server" SelectMethod="GetProjectCostList"
                                        TypeName="RmsPM.BFL.ProjectCostBFL" EnablePaging="True" MaximumRowsParameterName="MaxRecords" SortParameterName="SortColumns" StartRowIndexParameterName="StartRecord" OldValuesParameterFormatString="original_{0}">
                                        <SelectParameters>
                                            <asp:Parameter Name="SortColumns" Type="String" />
                                            <asp:Parameter Name="StartRecord" Type="Int32" />
                                            <asp:Parameter Name="MaxRecords" Type="Int32" />
                                            <asp:Parameter Name="AccessRange" Type="String" />
                                            <asp:Parameter Name="ProjectCostCodeEqual" Type="String" />
                                            <asp:QueryStringParameter Name="ProjectNameEqual" QueryStringField="ProjectNameTextBox"
                                                Type="String" />
                                            <asp:QueryStringParameter Name="GroupCodeEqual" QueryStringField="ProjectCostTypeCode"
                                                Type="String" />
                                            <asp:QueryStringParameter Name="AreaRange1" QueryStringField="txtAreaMin" Type="String" />
                                            <asp:QueryStringParameter Name="AreaRange2" QueryStringField="txtAreaMax" Type="String" />
                                            <asp:QueryStringParameter Name="PriceRange1" QueryStringField="txtPriceMin" Type="String" />
                                            <asp:QueryStringParameter Name="PriceRange2" QueryStringField="txtPriceMax" Type="String" />
                                            <asp:QueryStringParameter Name="MoneyRange1" QueryStringField="txtMoneyMin" Type="String" />
                                            <asp:QueryStringParameter Name="MoneyRange2" QueryStringField="txtMoneyMax" Type="String" />
                                            <asp:Parameter Name="QtyRange1" Type="String" />
                                            <asp:Parameter Name="QtyRange2" Type="String" />
                                            <asp:QueryStringParameter Name="UnitEqual" QueryStringField="Unit" Type="String" />
                                            <asp:Parameter Name="InputPersonEqual" Type="String" />
                                            <asp:Parameter Name="InputDateRange1" Type="String" />
                                            <asp:Parameter Name="InputDateRange2" Type="String" />
                                            <asp:QueryStringParameter Name="RemarkEqual" QueryStringField="Remark" Type="String" />
                                        </SelectParameters>
                                    </asp:ObjectDataSource>
								<table width="100%" class="list">
									<tr class="list-title">
										<td>
											共
											<asp:Label Runat="server" ID="lblRecordCount">0</asp:Label>
											条
										</td>
									</tr>
								</table>
                               </div> 
                             </td>
                         </tr>
                     </table>
                 </td>
	        </tr>
        </table>
    </form>
</body>
</html>
