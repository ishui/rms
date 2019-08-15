<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MaterialInventoryInfo.aspx.cs" Inherits="Material_MaterialInventoryInfo" %>

<%@ Register TagPrefix="uc1" TagName="InputSystemGroup" Src="../UserControls/InputSystemGroup.ascx" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Register TagPrefix="uc1" TagName="InputUser" Src="../UserControls/InputUser.ascx" %>
<%@ Register TagPrefix="uc1" TagName="inputmaterialin" Src="../UserControls/inputmaterialin.ascx" %>
<%@ Register TagPrefix="uc1" TagName="inputcontract" Src="../UserControls/inputcontract.ascx" %>
<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics.WebUI.WebDataInput.v5.1, Version=5.1.20051.37, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>材料库存信息</title>
    <link href="../Images/index.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../Rms.js"></script>
	<LINK href="../Images/infra.css" type="text/css" rel="stylesheet">
    <link href="/../Images/index.css" rel="stylesheet" type="text/css" />
<script language="javascript">
function OpenModify(IOType,Code)
{   
    if(IOType== 'O')
    var Modify = OpenLargeWindow('MaterialOutInfo.aspx?ProjectCode=<%= Request["ProjectCode"]+"" %>&MaterialOutCode='+Code,'材料领用维护');
	else if(IOType=='I')
	var Modify =OpenLargeWindow('MaterialInInfo.aspx?ProjectCode=<%= Request["ProjectCode"]+"" %>&MaterialInCode='+Code,'材料入库维护');
	
}
</script>
</head>
<body scroll="no">
    <form id="form1" runat="server">
        <table height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td>
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="topic" align="center" background="../images/topic_bg.gif" height="25">
                                材料库存信息</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr height="100%">
                <td valign="top">
                    <asp:FormView ID="FormView1" runat="server" DataSourceID="ObjectDataSource1"
                         DataKeyNames="MaterialCode" Width="100%"  Height="100%">
                        <EditItemTemplate>

                        </EditItemTemplate>
                        <InsertItemTemplate>

                        </InsertItemTemplate>
                        <ItemTemplate>
                        <table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="#ffffff">
                            <tr><td>
                               <table class="form" cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tr>
                                    <td class="form-item" width="80" nowrap>
                                        材料名称：</td>
                                    <td>
                                        <%# Eval("MaterialName") %>
                                    </td>
                                    <td class="form-item" width="80" nowrap>
                                        规格：</td>
                                    <td>
                                       <%# Eval("Spec") %>
                                    </td>
                                    <td class="form-item" width="80">
                                        单位：</td>
                                    <td>
                                        <%# (Eval("Unit"))%>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item" nowrap>
                                        材料类型：</td>
                                    <td>
                                         <%# RmsPM.BLL.SystemGroupRule.GetSystemGroupFullName((string)Eval("groupcode"))%>
                                    </td>
                                    <td class="form-item">
                                        入库数量：</td>
                                    <td>
                                         <%# (Eval("InQty"))%>
                                    </td>
                                    <td class="form-item">
                                        领料数量：</td>
                                    <td>
                                         <%# (Eval("OutQty"))%>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item">
                                        库存数量：</td>
                                    <td>
                                         <%# (Eval("InvQty"))%>
                                    </td>
                                </tr>
                        </table>
                            </td>
                            </tr>
                            <tr><td>
						    <table cellSpacing="0" cellPadding="0" border="0">
							    <tr>
								    <td class="intopic">材料出入库明细</td>
							    </tr>
						    </table>
						     </td></tr>
						     <tr height="100%"><td>
						     <div style="OVERFLOW:auto;WIDTH:100%;POSITION:absolute;HEIGHT:100%">
                         <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                            AllowPaging="True"  CssClass="list" PageSize="11" 
                            Width="100%" DataSourceID="ObjectDataSource2" GridLines="Horizontal" ShowFooter="True">
					        <HeaderStyle CssClass="list-title" />
					        <FooterStyle CssClass="list-title" />
                            <Columns>
								    <asp:TemplateField HeaderText="序号" FooterText="合计">
									    <HeaderStyle Wrap="False" HorizontalAlign="Center" />
									    <FooterStyle Wrap="False" HorizontalAlign="Center" />
									    <ItemStyle HorizontalAlign="Center" Wrap="False" />
									    <ItemTemplate>
										    <%# Container.DataItemIndex + 1 %>
									    </ItemTemplate>
								    </asp:TemplateField>
                                <asp:TemplateField HeaderText="单据号" >
                                    <ItemTemplate>
                                    <a href="#" onclick="javascript:OpenModify('<%# Eval("IOType") %>','<%# Eval("MaterialIOID") %>');return false;"> 
                                    <%# DataBinder.Eval(Container.DataItem, "MaterialIOID")%>
                                    </a>      
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="日期" >
                                    <ItemTemplate>
                                    <%#RmsPM.BLL.StringRule.ShowDate(DataBinder.Eval(Container.DataItem, "IODate"))%>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                    <FooterStyle Wrap="False" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="入库数量" >
                                    <ItemTemplate>
                                    <%#RmsPM.BLL.MathRule.GetDecimalShowString(DataBinder.Eval(Container.DataItem, "InQty"))%>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Wrap="False" HorizontalAlign="Right" />
                                </asp:TemplateField> 
                                <asp:TemplateField HeaderText="出库数量" >
                                    <ItemTemplate>
                                    <%#RmsPM.BLL.MathRule.GetDecimalShowString(DataBinder.Eval(Container.DataItem, "OutQty"))%>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Wrap="False" HorizontalAlign="Right" />
                                </asp:TemplateField> 
                            </Columns>
                            <EmptyDataTemplate>
                                无匹配数据
                            </EmptyDataTemplate>
                           </asp:GridView>
                       </div>   
                       </td></tr></table>
                        </ItemTemplate>
                    </asp:FormView>
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetV_MaterialInventoryByMaterialProject" 
                    TypeName="RmsPM.BFL.MaterialInventoryBFL"  OldValuesParameterFormatString="original_{0}">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="MaterialCode" QueryStringField="MaterialCode" Type="String" />
                            <asp:QueryStringParameter Name="ProjectCode" QueryStringField="ProjectCode" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="ObjectDataSource2" TypeName="RmsPM.BFL.MaterialInventoryBFL"  runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetV_MaterialInventoryIOListByMaterialProject" Onselected="ObjectDataSource2_Selected">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="MaterialCode" QueryStringField="MaterialCode" Type="String" />
                            <asp:QueryStringParameter Name="ProjectCode" QueryStringField="ProjectCode" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </td>
            </tr>
    </table>
    </form>
</body>
</html>
