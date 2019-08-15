<%@ Page Language="C#" AutoEventWireup="true" CodeFile="selectmateriallist.aspx.cs" Inherits="SelectBox_selectmateriallist" %>
<%@ Register TagPrefix="cc4" Namespace="Rms.ControlLb" Assembly="RmsPM.BLL" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head id="Head1" runat="server">
<title>MaterialInfo</title>
<link href="../Images/index.css" type="text/css" rel="stylesheet">
<LINK href="../Images/infra.css" type="text/css" rel="stylesheet">
<meta content="JavaScript" name="vs_defaultClientScript">
 <script language="javascript" src="../Rms.js"></script>
 		<SCRIPT language="javascript" src="../Images/XMLTree.js"></SCRIPT>
		<script language="javascript" src="../Images/ContentMenu.js"></script>
<script language="javascript">
function doSelectMaterial(MaterialCode,MaterialName,Unit,Spec,flag)
{
    //alert(MaterialCode+"fff"+MaterialName);
    var flag = '<%=Request["Flag"]%>';
    //Unit是自动在选择材料的时候传的单位暂时没用
    window.parent.opener.<%=ViewState["ReturnFunc"]%>(MaterialCode,MaterialName,flag,Unit,Spec);
   // var subUnit = flag.substr(0,flag.length-"inputmaterial".length)+"UnitBox";
  // alert(Unit);
    //window.parent.document.all.UnitBox.value=Unit;
    window.parent.close();
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
                                        AllowPaging="True" AllowSorting="True" CssClass="list" PageSize="10" 
                                        Width="100%" DataSourceID="ObjectDataSource1" GridLines="Horizontal">
                                        <Columns>
                                            <asp:TemplateField HeaderText="材料名" SortExpression="MaterialName">
                                                <ItemTemplate>
									                <a href="##"  onclick="doSelectMaterial(MaterialCode,MaterialName,Unit,Spec)"
									                 MaterialCode='<%#  DataBinder.Eval(Container.DataItem, "MaterialCode")  %>'
									                MaterialName='<%#  DataBinder.Eval(Container.DataItem, "MaterialName")  %>'
									                 Unit='<%#  DataBinder.Eval(Container.DataItem, "Unit")  %>'
									                  Spec='<%#  DataBinder.Eval(Container.DataItem, "Spec")  %>'
									                >
										                <%#  DataBinder.Eval(Container.DataItem, "MaterialName")%>
									                </a>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="标准价" SortExpression="StandardPrice">
                                                <ItemTemplate>
                                                        <%# Eval("StandardPrice")%>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
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
                                            <asp:TemplateField HeaderText="备注" SortExpression="Remark">
                                                <ItemTemplate>
                                                   <%# RmsPM.BLL.StringRule.TruncText((Object)Eval("Remark"), 5)%>
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
                                    runat="server" SelectMethod="GetMaterialList"
                                        TypeName="RmsPM.BFL.MaterialBFL" EnablePaging="True" MaximumRowsParameterName="MaxRecords" SortParameterName="SortColumns" StartRowIndexParameterName="StartRecord" DataObjectTypeName="TiannuoPM.MODEL.MaterialModel" DeleteMethod="Delete" InsertMethod="Insert" UpdateMethod="Update" OldValuesParameterFormatString="original_{0}">
                                        <SelectParameters>
                                            <asp:Parameter Name="SortColumns" Type="String" />
                                            <asp:Parameter Name="StartRecord" Type="Int32" />
                                            <asp:Parameter Name="MaxRecords" Type="Int32" />
                                            <asp:Parameter Name="MaterialCodeEqual" Type="Int32" />
                                            <asp:QueryStringParameter Name="MaterialNameEqual" QueryStringField="MaterialName" Type="String" />
                                            <asp:QueryStringParameter Name="GroupCodeEqual" QueryStringField="MaterialTypeCode"
                                                Type="String" />
                                            <asp:QueryStringParameter Name="SpecEqual" QueryStringField="Spec" Type="String" />
                                            <asp:QueryStringParameter Name="UnitEqual" QueryStringField="Unit" Type="String" />
                                            <asp:QueryStringParameter Name="StandardPriceRange1" QueryStringField="StandardPriceMin"
                                                Type="String" />
                                            <asp:QueryStringParameter Name="StandardPriceRange2" QueryStringField="StandardPriceMax"
                                                Type="String" />
                                            <asp:Parameter Name="InputPerson" Type="String" />
                                            <asp:Parameter Name="InputDateRange1" Type="DateTime" />
                                            <asp:Parameter Name="InputDateRange2" Type="DateTime" />
                                            <asp:QueryStringParameter Name="RemarkEqual" QueryStringField="Remark" Type="String" />
                                            <asp:Parameter Name="AccessRange" Type="String" />
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
