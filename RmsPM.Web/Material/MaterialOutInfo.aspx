<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MaterialOutInfo.aspx.cs" Inherits="Material_MaterialOutInfo" %>
<%@ Register TagPrefix="uc1" TagName="InputSystemGroup" Src="../UserControls/InputSystemGroup.ascx" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Register TagPrefix="uc1" TagName="InputUser" Src="../UserControls/InputUser.ascx" %>
<%@ Register TagPrefix="uc1" TagName="inputmaterialin" Src="../UserControls/inputmaterialin.ascx" %>
<%@ Register TagPrefix="uc1" TagName="inputcontract" Src="../UserControls/inputcontract.ascx" %>
<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics.WebUI.WebDataInput.v5.1, Version=5.1.20051.37, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>材料领用</title>
    <link href="../Images/index.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../Rms.js"></script>
	<LINK href="../Images/infra.css" type="text/css" rel="stylesheet">
    <link href="/../Images/index.css" rel="stylesheet" type="text/css" />
<script language="javascript">
function OpenModify(Code)
{   
	var Modify = OpenMiddleWindow('Materialinfo.aspx?ProjectCode=<%= Request["ProjectCode"] %>&MaterialCode='+Code,'材料信息');
}
function OpenInInfoModify(Code)
{   
    OpenLargeWindow('MaterialInInfo.aspx?ProjectCode=<%= Request["ProjectCode"]+"" %>&MaterialInCode='+Code,'材料入库信息');

}
function doViewContractInfo(code,Projectcode)
{
	OpenFullWindow('../Contract/ContractInfo.aspx?ProjectCode='+Projectcode+'&ContractCode=' + code,'合同信息');
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
                                材料领用</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr height="100%">
                <td valign="top">
                    <asp:FormView ID="FormView1" runat="server" DataSourceID="ObjectDataSource1" OnItemInserted="FormView1_ItemInserted"
                        OnItemDeleted="FormView1_ItemDeleted" OnItemUpdated="FormView1_ItemUpdated" DataKeyNames="MaterialOutCode"
                        Width="100%" OnItemInserting="FormView1_ItemInserting" Height="100%"
                        OnDataBound="FormView1_DataBound" OnItemUpdating="FormView1_ItemUpdating">
                        <EditItemTemplate>
                        <table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="#ffffff"><tr><td>
                            <table class="table" id="tableToolBar" width="100%">
                                <tr>
                                    <td class="tools-area" width="16">
                                        <img src="../images/btn_li.gif" align="absMiddle"></td>
                                    <td class="tools-area">
                                        <asp:Button ID="btnSave" Text=" 保存 " CausesValidation="true" CssClass="button" runat="server"
                                            CommandName="Update" />
                                        <asp:Button ID="btnCancel" Text=" 取消 " CssClass="button" runat="server" CommandName="Cancel" />
                                        <input name="btnClose" id="btnClose" type="button" value=" 关闭 " class="button" runat="server"
                                            onclick="javascript:window.close();">
                                    </td>
                                </tr>
                            </table>
                            </td></tr>
                            <tr><td>
                            <table class="form" cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tr>
                                    <td class="form-item" nowrap>
                                        领料类型：</td>
                                    <td >
                                        <uc1:InputSystemGroup id="inputSystemGroup" ClassCode="1505" Value='<%# Bind("GroupCode") %>'  runat="server" SelectAllLeaf="True"></uc1:InputSystemGroup>
                                        <font color="red">*</font><span runat="server" id="GroupSpan"></span>
                                    </td>
                                    <td class="form-item" nowrap>
                                        调拨人：</td>
								    <td><uc1:InputUser id="OutPersonBox" runat="server" Value='<%# Bind("OutPerson") %>'></uc1:InputUser>
								    </td>
                                    <td class="form-item" nowrap>
                                        领料日期：</td>
                                    <td >
                                        <cc3:Calendar ID="OutDateBox" runat="server" CalendarResource="../Images/CalendarResource/"
                                            ReadOnly="False" Display="True" Value='<%# Bind("OutDate") %>'>
                                        </cc3:Calendar>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item" nowrap>
                                       合同：</td>
								    <td><uc1:inputcontract id="ContractCode" runat="server" Value='<%# Bind("ContractCode") %>' ProjectCode='<%# Request["ProjectCode"]+"" %>'></uc1:inputcontract>
								    </td>
                                </tr>
                            </table>
                            </td></tr>
                            <tr><td>
						    <table cellSpacing="0" cellPadding="0" border="0">
							    <tr>
								    <td class="intopic" width="200">领用明细</td>
								    <td><input class="button-small" id="btnAddDtl" type="button" value="新 增" name="btnAddDtl"
										    runat="server" onserverclick="btnAddDtl_ServerClick"></td>
							    </tr>
						    </table>
						    </td></tr>
                            <tr height="100%"><td>
						    <div style="OVERFLOW:auto;WIDTH:100%;POSITION:absolute;HEIGHT:100%">
					        <asp:DataGrid id="dgDtl" onkeydown="if(event.keyCode==13) event.keyCode=9" runat="server" DataKeyField="MaterialOutDtlCode"
							    CellPadding="0" AutoGenerateColumns="False" GridLines="Horizontal"
							    Width="100%" CssClass="list" OnDeleteCommand="dgDtl_DeleteCommand">
							        <HeaderStyle CssClass="list-title" />
							        <FooterStyle CssClass="list-title" />
							        <Columns>
								    <asp:TemplateColumn HeaderText="序号">
									    <HeaderStyle Wrap="False" HorizontalAlign="Center" />
									    <ItemStyle HorizontalAlign="Center" Wrap="False" />
									    <ItemTemplate>
										    <%# Container.ItemIndex + 1 %>
									    </ItemTemplate>
								    </asp:TemplateColumn>
								    <asp:TemplateColumn>
								  <HeaderTemplate >材料名称<font color="red">*</font></HeaderTemplate>
									    <HeaderStyle HorizontalAlign="Left" />
									    <ItemStyle HorizontalAlign="Left" Wrap="False" />
									    <ItemTemplate>
                                        <uc1:inputmaterialin id="inputmaterialin" MaterialInDtlCode='<%# DataBinder.Eval(Container, "DataItem.MaterialInDtlCode") %>' runat="server" ></uc1:inputmaterialin>
									    </ItemTemplate>
								    </asp:TemplateColumn>
								    <asp:TemplateColumn>
								      <HeaderTemplate >领用数量<font color="red">*</font></HeaderTemplate>
									    <HeaderStyle HorizontalAlign="Right"/>
									    <ItemStyle HorizontalAlign="Right" Wrap="False" />
									    <ItemTemplate>
										    <igtxt:WebNumericEdit id="txtOutQty" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.OutQty") %>' CssClass="infra-input-nember" JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js" ImageDirectory="../images/infragistics/images/" MinDecimalPlaces="Two">
										    </igtxt:WebNumericEdit>
									    </ItemTemplate>
								    </asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="删除">
										<ItemStyle Wrap="False" HorizontalAlign="Center"/>
										<HeaderStyle Wrap="False" HorizontalAlign="Center" />
										<ItemTemplate>
											<asp:LinkButton id="btnDelete" runat="server" Text="<img src=../images/del.gif width=16 height=16 border=0>"
												CausesValidation="false" CommandName="Delete"></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
						            </Columns>
						        </asp:DataGrid>
                            </div>
                          </td></tr>
                          </table>
                        </EditItemTemplate>
                        <InsertItemTemplate>
                         <table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="#ffffff">
                         <tr><td>
                            <table class="table" id="tableToolBar" width="100%">
                                <tr>
                                    <td class="tools-area" width="16">
                                        <img src="../images/btn_li.gif" align="absMiddle"></td>
                                    <td class="tools-area">
                                        <asp:Button ID="btnSave" Text=" 保存 " CausesValidation="true" CssClass="button" runat="server"
                                            CommandName="Insert" />
                                        <input name="btnClose" id="btnClose" type="button" value=" 关闭 " class="button" runat="server"
                                            onclick="javascript:window.close();">
                                    </td>
                                </tr>
                            </table>
                            </td></tr>
                            <tr><td>
                            <table class="form" cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tr>
                                    <td class="form-item" nowrap>
                                        领料类型：</td>
                                    <td>
                                        <uc1:InputSystemGroup id="inputSystemGroup" ClassCode="1505" Value='<%# Bind("GroupCode") %>'  runat="server" SelectAllLeaf="True"></uc1:InputSystemGroup>
                                        <font color="red">*</font><span runat="server" id="GroupSpan"></span>
                                    </td>
                                    <td class="form-item" nowrap>
                                        调拨人：</td>
								    <td><uc1:InputUser id="OutPersonBox" runat="server" Value='<%# Bind("OutPerson") %>'></uc1:InputUser>
								    </td>
                                    <td class="form-item" nowrap>
                                        领料日期：</td>
                                    <td>
                                        <cc3:Calendar ID="OutDateBox" runat="server" CalendarResource="../Images/CalendarResource/"
                                            ReadOnly="False" Display="True" Value='<%# Bind("OutDate") %>'>
                                        </cc3:Calendar>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item" nowrap>
                                       合同：</td>
								    <td><uc1:inputcontract id="ContractCode" runat="server" Value='<%# Bind("ContractCode") %>' ProjectCode='<%# Request["ProjectCode"]+"" %>'></uc1:inputcontract>
								    </td>
                                </tr>
                            </table>
                             </td></tr>
                            <tr><td>
						    <table cellSpacing="0" cellPadding="0" border="0">
							    <tr>
								    <td class="intopic" width="200">领用明细</td>
								    <td><input class="button-small" id="btnAddDtl" type="button" value="新 增" name="btnAddDtl"
										    runat="server" onserverclick="btnAddDtl_ServerClick"></td>
							    </tr>
						    </table>
						     </td></tr>
                            <tr height="100%"><td>
                            <div style="OVERFLOW:auto;WIDTH:100%;POSITION:absolute;HEIGHT:100%">
					        <asp:DataGrid id="dgDtl" onkeydown="if(event.keyCode==13) event.keyCode=9" runat="server" DataKeyField="MaterialOutDtlCode"
							    CellPadding="0" GridLines="Horizontal" AutoGenerateColumns="False" PageSize="15"
							    Width="100%" CssClass="list" OnDeleteCommand="dgDtl_DeleteCommand">
							        <HeaderStyle CssClass="list-title" />
							        <FooterStyle CssClass="list-title" />
							        <Columns>
								    <asp:TemplateColumn HeaderText="序号">
									    <HeaderStyle Wrap="False" HorizontalAlign="Center" />
									    <ItemStyle HorizontalAlign="Center" Wrap="False" />
									    <ItemTemplate>
										    <%# Container.ItemIndex + 1 %>
									    </ItemTemplate>
								    </asp:TemplateColumn>
								    <asp:TemplateColumn>
								    <HeaderTemplate >材料名称<font color="red">*</font></HeaderTemplate>
									    <HeaderStyle HorizontalAlign="Left" />
									    <ItemStyle HorizontalAlign="Left" Wrap="False" />
									    <ItemTemplate>
                                        <uc1:inputmaterialin id="inputmaterialin" MaterialInDtlCode='<%# Bind("MaterialInDtlCode") %>' runat="server" ></uc1:inputmaterialin>
									    </ItemTemplate>
								    </asp:TemplateColumn>
								    <asp:TemplateColumn HeaderText="领用数量">
									    <HeaderStyle HorizontalAlign="Right"/>
									    <HeaderTemplate >领用数量<font color="red">*</font></HeaderTemplate>
									    <ItemStyle HorizontalAlign="Right" Wrap="False" />
									    <ItemTemplate>
										    <igtxt:WebNumericEdit id="txtOutQty" runat="server" Value='<%# Bind("OutQty") %>' CssClass="infra-input-nember" JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js" ImageDirectory="../images/infragistics/images/" MinDecimalPlaces="Two">
										    </igtxt:WebNumericEdit>
										    
									    </ItemTemplate>
								    </asp:TemplateColumn>

									<asp:TemplateColumn HeaderText="删除">
										<ItemStyle Wrap="False" HorizontalAlign="Center"/>
										<HeaderStyle Wrap="False" HorizontalAlign="Center" />
										<ItemTemplate>
											<asp:LinkButton id="btnDelete" runat="server" Text="<img src=../images/del.gif width=16 height=16 border=0>"
												CausesValidation="false" CommandName="Delete"></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
						            </Columns>
						        </asp:DataGrid>
						      </div>  </td></tr>
                        </table>
                        </InsertItemTemplate>
                        <ItemTemplate>
                        <table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="#ffffff">
                        <tr><td>
                            <table class="table" id="tableToolBar" width="100%">
                                <tr>
                                    <td class="tools-area" width="16">
                                        <img src="../images/btn_li.gif" align="absMiddle"></td>
                                    <td class="tools-area">
                                        <asp:Button ID="btnModify" Text=" 修改 " CssClass="button" runat="server" CommandName="Edit" />
                                        <asp:Button ID="btnDelete" Text=" 删除 " CssClass="button" runat="server" CommandName="Delete" />
                                        <input name="btnClose" id="btnClose" type="button" value=" 关闭 " class="button" runat="server"
                                            onclick="javascript:window.close();">
                                    </td>
                                </tr>
                            </table>
                            </td></tr>
                            <tr><td>
                            <table class="form" cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tr>
                                    <td class="form-item" width="10%" nowrap>
                                        领料单号：</td>
                                    <td witdh="16%">
                                        <%# Eval("MaterialOutID")%>
                                    </td>
                                    <td class="form-item" width="10%" nowrap>
                                        领料类型：</td>
                                    <td width="24%">
                                         <%# RmsPM.BLL.SystemGroupRule.GetSystemGroupFullName((string)Eval("GroupCode"))%>
                                    </td>
                                    <td class="form-item" width="10%" nowrap>
                                        领料日期：</td>
                                    <td>
                                        <%# RmsPM.BLL.StringRule.ShowDate(Eval("OutDate"))%>
                                    </td>
                                 </tr>   
                                <tr>
                                    <td class="form-item" width="10%" nowrap>
                                        调拨人：</td>
                                    <td width="30%">
                                    <%# RmsPM.BLL.SystemRule.GetUserName(RmsPM.BLL.ConvertRule.ToString(Eval("OutPerson")))%>
                                    </td>
                                    <td class="form-item">
                                        制单人：</td>
                                    <td>
                                    <%# RmsPM.BLL.SystemRule.GetUserName(RmsPM.BLL.ConvertRule.ToString(Eval("InputPerson")))%>
                                    </td>
                                    <td class="form-item">
                                        制单日期：</td>
                                    <td colspan="5">
                                        <%# RmsPM.BLL.StringRule.ShowDate(Eval("InputDate"))%>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item" nowrap>
                                       合同名称：</td>
                                    <td>
                                    <a href="#" onclick="doViewContractInfo(this.code,this.Projectcode);return false;" code='<%#  DataBinder.Eval(Container.DataItem, "ContractCode") %>'
                                    Projectcode='<%#  DataBinder.Eval(Container.DataItem, "Projectcode") %>'>
                                    <%#  DataBinder.Eval(Container.DataItem, "ContractName") %>
                                    </a>   
                                       
                                    </td>
                                    <td class="form-item" nowrap>
                                       合同编号：</td>
                                    <td>
                                        <%# Eval("ContractID")%>
                                    </td>
                                </tr>
                            </table>
                            </td></tr>
                            <tr><td>
						    <table cellSpacing="0" cellPadding="0" border="0">
							    <tr>
								    <td class="intopic">领用明细</td>
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
                                <asp:TemplateField HeaderText="材料名称" >
                                    <ItemTemplate>
                                    <a href="#" onclick="javascript:OpenModify('<%# Eval("MaterialCode") %>');return false;"> 
                                    <%# DataBinder.Eval(Container.DataItem, "MaterialName")  %>
                                    </a>      
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="规格" >
                                    <ItemTemplate>
                                    <%#(DataBinder.Eval(Container.DataItem, "Spec"))%>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                    <FooterStyle Wrap="False" HorizontalAlign="Left" />
                                </asp:TemplateField> 
                                <asp:TemplateField HeaderText="单位" ShowHeader="False" >
                                    <ItemTemplate>
                                    <%#(DataBinder.Eval(Container.DataItem, "Unit"))%>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                    <FooterStyle Wrap="False" HorizontalAlign="Left" />
                                </asp:TemplateField> 
                                <asp:TemplateField HeaderText="领用数量" >
                                    <ItemTemplate>
                                    <%#RmsPM.BLL.MathRule.GetDecimalShowString(DataBinder.Eval(Container.DataItem, "OutQty"))%>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Wrap="False" HorizontalAlign="Right" />
                                </asp:TemplateField>    
                                <asp:TemplateField HeaderText="领用单价(元)" >
                                    <ItemTemplate>
                                    <%#RmsPM.BLL.MathRule.GetDecimalShowString(DataBinder.Eval(Container.DataItem, "OutPrice"))%>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Wrap="False" HorizontalAlign="Right" />
                                </asp:TemplateField>      

                                <asp:TemplateField HeaderText="领用金额(元)" >
                                    <ItemTemplate>
                                    <%#RmsPM.BLL.MathRule.GetDecimalShowString(DataBinder.Eval(Container.DataItem, "OutMoney"))%>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Wrap="False" HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="入库日期" >
                                    <ItemTemplate>
                                    <%#RmsPM.BLL.StringRule.ShowDate(DataBinder.Eval(Container.DataItem, "InDate"))%>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                    <FooterStyle Wrap="False" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="入库单号" SortExpression="MaterialInID">
                                    <ItemTemplate>
                                        <a href="#" onclick="javascript:OpenInInfoModify('<%# Eval("MaterialInID") %>');return false;">
                                            <%# Eval("MaterialInID")%>
                                        </a>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                无匹配数据
                            </EmptyDataTemplate>
                           </asp:GridView>
                       </div>   
                       </td></tr>
                        </table>
                        </ItemTemplate>
                    </asp:FormView>
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetMaterialOut" 
                    TypeName="RmsPM.BFL.MaterialOutBFL" OnInserted="ObjectDataSource1_Inserted" OldValuesParameterFormatString="original_{0}" DataObjectTypeName="TiannuoPM.MODEL.MaterialOutModel" DeleteMethod="Delete" InsertMethod="Insert" UpdateMethod="Update">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="Code" QueryStringField="MaterialOutCode" Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" Style="display: none">刷新</asp:LinkButton>
                    <asp:ObjectDataSource ID="ObjectDataSource2" TypeName="RmsPM.BFL.MaterialOutBFL"  runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetMaterialOutDtlList" OnSelected="ObjectDataSource2_Selected">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="MaterialOutCode" QueryStringField="MaterialOutCode" Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </td>
            </tr>
    </table>
    </form>
</body>
</html>
