<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectMaterialinlist.aspx.cs" Inherits="SelectBox_selectmaterialinlist" %>
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
function doSelectMaterialin(MaterialInDtlCode,MaterialName,MaterialCode,OutPrice,Spec,Unit,InQty,InDate,InPrice,flag)
{
    //alert(OutPrice);
    var flag = '<%=Request["Flag"]%>';
    //Unit是自动在选择材料的时候传的单位暂时没用
    window.parent.opener.<%=ViewState["ReturnFunc"]%>(MaterialInDtlCode,MaterialName,flag,MaterialCode,OutPrice,Spec,Unit,InQty,InDate,InPrice);
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
                                        AllowPaging="True" AllowSorting="True" CssClass="list" 
                                        Width="100%" DataSourceID="ObjectDataSource1" GridLines="Horizontal">
                                        <Columns>
                                            <asp:TemplateField HeaderText="材料名称" SortExpression="MaterialName">
                                                <ItemTemplate>
									                <a href="##"  onclick="doSelectMaterialin(MaterialInDtlCode,MaterialName,MaterialCode,OutPrice,Spec,Unit,InQty,InDate,InPrice)"
									                 MaterialInDtlCode='<%#  DataBinder.Eval(Container.DataItem, "MaterialInDtlCode")  %>'
									                MaterialName='<%#  DataBinder.Eval(Container.DataItem, "MaterialName")  %>'
									                MaterialCode='<%#  DataBinder.Eval(Container.DataItem, "MaterialCode")  %>'
									                OutPrice='<%#  DataBinder.Eval(Container.DataItem, "InPrice")  %>'
									                Spec='<%#  DataBinder.Eval(Container.DataItem, "Spec")  %>'
									                Unit='<%#  DataBinder.Eval(Container.DataItem, "Unit")  %>'
									                InQty='<%#  DataBinder.Eval(Container.DataItem, "InQty")  %>'
									                InDate='<%#  RmsPM.BLL.StringRule.ShowDate(DataBinder.Eval(Container.DataItem, "InDate"))  %>'
									                InPrice='<%#  DataBinder.Eval(Container.DataItem, "InPrice")  %>'
									                >
										                <%#  DataBinder.Eval(Container.DataItem, "MaterialName")%>
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
                                            <asp:TemplateField HeaderText="规 格" SortExpression="Spec">
                                                <ItemTemplate>
                                                       <%# Eval("Spec")%>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="单 位" SortExpression="Unit">
                                                <ItemTemplate>
                                                        <%# Eval("Unit")%>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="库存量" SortExpression="InvQty">
                                                <ItemTemplate>
                                                        <%# Eval("InvQty")%>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="入库单价" SortExpression="InPrice">
                                                <ItemTemplate>
                                                        <%# Eval("InPrice")%>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="入库日期" SortExpression="InDate" >
                                                <ItemTemplate>
                                                  <%# RmsPM.BLL.StringRule.ShowDate(Eval("InDate"))%>
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
                                    runat="server" SelectMethod="SelectMaterialInDtlList"
                                        TypeName="RmsPM.BFL.MaterialInBFL" EnablePaging="True" MaximumRowsParameterName="MaxRecords" SortParameterName="SortColumns" StartRowIndexParameterName="StartRecord" OldValuesParameterFormatString="original_{0}">
                                        <SelectParameters>
                                            <asp:Parameter Name="SortColumns" Type="String" />
                                            <asp:Parameter Name="StartRecord" Type="Int32" />
                                            <asp:Parameter Name="MaxRecords" Type="Int32" />
                                            <asp:Parameter Name="AccessRange" Type="String" />
                                            <asp:Parameter Name="MaterialInDtlCodeEqual" Type="String" />
                                            <asp:Parameter Name="MaterialInCodeEqual" Type="String" />
                                            <asp:Parameter Name="MaterialCodeEqual" Type="String" />
                                            <asp:Parameter Name="InQtyEqual" Type="String" />
                                            <asp:Parameter Name="InPriceEqual" Type="String" />
                                            <asp:Parameter Name="InMoneyEqual" Type="String" />
                                            <asp:Parameter Name="OutQtyEqual" Type="String" />
                                            <asp:QueryStringParameter Name="MaterialNameEqual" QueryStringField="MaterialName" Type="String" />
                                            <asp:QueryStringParameter Name="SpecEqual" QueryStringField="Spec" Type="String" />
                                            <asp:QueryStringParameter Name="UnitEqual" QueryStringField="Unit" Type="String" />
                                            <asp:QueryStringParameter Name="GroupCodeEqual" QueryStringField="MaterialTypeCode"
                                                Type="String" />
                                            <asp:Parameter Name="GroupNameEqual" Type="String" />
                                            <asp:Parameter Name="GroupFullIDEqual" Type="String" />
                                            <asp:Parameter Name="GroupSortIDEqual" Type="String" />
                                            <asp:Parameter Name="InvQtyEqual" Type="String" />
                                            <asp:Parameter Name="InvMoneyEqual" Type="String" />
                                            <asp:Parameter Name="MaterialInIDEqual" Type="String" />
                                            <asp:QueryStringParameter Name="InDateRange1" QueryStringField="InDateStart" Type="String" />
                                            <asp:QueryStringParameter Name="InDateRange2" QueryStringField="InDateEnd" Type="String" />
                                            <asp:Parameter Name="ContractCodeEqual" Type="String" />
                                            <asp:Parameter Name="InGroupCodeEqual" Type="String" />
                                            <asp:Parameter Name="InGroupNameEqual" Type="String" />
                                            <asp:QueryStringParameter Name="ProjectCodeEqual" QueryStringField="ProjectCode"
                                                Type="String" />

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
