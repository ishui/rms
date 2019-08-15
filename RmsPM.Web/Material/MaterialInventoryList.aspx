<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MaterialInventoryList.aspx.cs" Inherits="Material_MaterialInventoryList" %>
<%@ Register TagPrefix="cc4" Namespace="Rms.ControlLb" Assembly="RmsPM.BLL" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<title>MaterialInventoryList</title>
<link href="../Images/index.css" type="text/css" rel="stylesheet">
<LINK href="../Images/infra.css" type="text/css" rel="stylesheet">
 <script language="javascript" src="../Rms.js"></script>
 <script language="javascript">
function OpenModify(Code)
{   
	OpenLargeWindow('MaterialInventoryInfo.aspx?ProjectCode=<%= Request["ProjectCode"] %>&MaterialCode='+Code,'材料维护');
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
                                            <asp:TemplateField HeaderText="材料名称" SortExpression="MaterialName">
                                                <ItemTemplate>
                                                    <a href="#" onclick="javascript:OpenModify('<%# Eval("MaterialCode") %>');return false;">
                                                        <%# Eval("MaterialName")%>
                                                    </a>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="材料类型" SortExpression="GroupCode">
                                                <ItemTemplate>
                                                    <%# RmsPM.BLL.SystemGroupRule.GetSystemGroupFullName((string)Eval("GroupCode"))%>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="规格" SortExpression="Spec">
                                                <ItemTemplate>
                                                        <%# Eval("Spec")%>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="单位" SortExpression="Unit">
                                                <ItemTemplate>
                                                        <%# Eval("Unit")%>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="入库数量" SortExpression="InQty">
                                                <ItemTemplate>
                                                        <%#RmsPM.BLL.MathRule.GetDecimalShowString( Eval("InQty"))%>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="领料数量" SortExpression="OutQty">
                                                <ItemTemplate>
                                                   <%# RmsPM.BLL.MathRule.GetDecimalShowString(Eval("OutQty"))%>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="库存数量" SortExpression="InvQty">
                                                <ItemTemplate>
                                                   <%#RmsPM.BLL.MathRule.GetDecimalShowString( Eval("InvQty"))%>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
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
                                    runat="server" SelectMethod="GetV_MaterialInventoryList"
                                        TypeName="RmsPM.BFL.MaterialInventoryBFL" EnablePaging="True" MaximumRowsParameterName="MaxRecords" SortParameterName="SortColumns" StartRowIndexParameterName="StartRecord" OldValuesParameterFormatString="original_{0}">
                                        <SelectParameters>
                                            <asp:Parameter Name="SortColumns" Type="String" />
                                            <asp:Parameter Name="StartRecord" Type="Int32" />
                                            <asp:Parameter Name="MaxRecords" Type="Int32" />
                                            <asp:Parameter Name="AccessRange" Type="String" />
                                            <asp:QueryStringParameter Name="ProjectCodeEqual" QueryStringField="ProjectCode"
                                                Type="String" />
                                            <asp:Parameter Name="ProjectNameEqual" Type="String" />
                                            <asp:Parameter Name="MaterialCodeEqual" Type="String" />
                                            <asp:QueryStringParameter Name="InQtyRange1" QueryStringField="txtInQtyMin" Type="String" />
                                            <asp:QueryStringParameter Name="InQtyRange2" QueryStringField="txtInQtyMax" Type="String" />
                                            <asp:Parameter Name="InMoneyRange1" Type="String" />
                                            <asp:Parameter Name="InMoneyRange2" Type="String" />
                                            <asp:QueryStringParameter Name="OutQtyRange1" QueryStringField="txtOutQtyMin" Type="String" />
                                            <asp:QueryStringParameter Name="OutQtyRange2" QueryStringField="txtOutQtyMax" Type="String" />
                                            <asp:QueryStringParameter Name="InvQtyRange1" QueryStringField="txtInvQtyMin" Type="String" />
                                            <asp:QueryStringParameter Name="InvQtyRange2" QueryStringField="txtInvQtyMax" Type="String" />
                                            <asp:QueryStringParameter Name="MaterialNameEqual" QueryStringField="MaterialName" Type="String" />
                                            <asp:QueryStringParameter Name="SpecEqual" QueryStringField="Spec" Type="String" />
                                            <asp:QueryStringParameter Name="UnitEqual" QueryStringField="Unit" Type="String" />
                                            <asp:QueryStringParameter Name="StandardPriceRange1" QueryStringField="StandardPriceMin"
                                                Type="String" />
                                            <asp:QueryStringParameter Name="StandardPriceRange2" QueryStringField="StandardPriceMax"
                                                Type="String" />
                                            <asp:QueryStringParameter Name="GroupCodeEqual" QueryStringField="MaterialTypeCode"
                                                Type="String" />
                                            <asp:Parameter Name="GroupNameEqual" Type="String" />
                                            <asp:Parameter Name="GroupFullIDEqual" Type="String" />
                                            <asp:Parameter Name="GroupSortIDEqual" Type="String" />
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
