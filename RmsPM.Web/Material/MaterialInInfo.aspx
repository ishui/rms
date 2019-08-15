<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MaterialInInfo.aspx.cs" Inherits="Material_MaterialInInfo" %>
<%@ Register TagPrefix="uc1" TagName="InputSystemGroup" Src="../UserControls/InputSystemGroup.ascx" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Register TagPrefix="uc1" TagName="InputUser" Src="../UserControls/InputUser.ascx" %>
<%@ Register TagPrefix="uc1" TagName="InputMaterial" Src="../UserControls/inputmaterial.ascx" %>
<%@ Register TagPrefix="uc1" TagName="inputcontract" Src="../UserControls/inputcontract.ascx" %>
<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics.WebUI.WebDataInput.v5.1, Version=5.1.20051.37, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>�������</title>
    <link href="../Images/index.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../Rms.js"></script>
	<LINK href="../Images/infra.css" type="text/css" rel="stylesheet">
    <link href="/../Images/index.css" rel="stylesheet" type="text/css" />
    <link href="../Images/GridPaginationSource/PaginationControlStyle.css" rel="stylesheet"
        type="text/css" />
<script language="javascript">
function OpenModify(Code)
{   
	var Modify = OpenMiddleWindow('MaterialInfo.aspx?ProjectCode=<%= Request["ProjectCode"]+"" %>&MaterialCode='+Code,'������Ϣ');
	
}
function OpenOutModify(Code)
{   
    OpenLargeWindow('MaterialOutInfo.aspx?ProjectCode=<%= Request["ProjectCode"]+"" %>&MaterialOutCode='+Code,'��������ά��');
}

</script>
</head>
<body scroll="no">
    <form id="form1" runat="server">
        <table height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td height="25">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="topic" align="center" background="../images/topic_bg.gif" height="25">
                                �������</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr height="100%">
                <td valign="top">
                    <asp:FormView ID="FormView1" runat="server" DataSourceID="ObjectDataSource1" OnItemInserted="FormView1_ItemInserted"
                        OnItemDeleted="FormView1_ItemDeleted" OnItemUpdated="FormView1_ItemUpdated" DataKeyNames="MaterialInCode"
                        Width="100%" OnItemInserting="FormView1_ItemInserting" Height="100%"
                        OnDataBound="FormView1_DataBound" OnItemUpdating="FormView1_ItemUpdating">
                        <EditItemTemplate>
                        <table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="#ffffff"><tr><td>
                            <table class="table" id="tableToolBar" width="100%">
                                <tr>
                                    <td class="tools-area" width="16">
                                        <img src="../images/btn_li.gif" align="absMiddle"></td>
                                    <td class="tools-area">
                                        <asp:Button ID="btnSave" Text=" ���� " CausesValidation="true" CssClass="button" runat="server"
                                            CommandName="Update" />
                                        <asp:Button ID="btnCancel" Text=" ȡ�� " CssClass="button" runat="server" CommandName="Cancel" />
                                        <input name="btnClose" id="btnClose" type="button" value=" �ر� " class="button" runat="server"
                                            onclick="javascript:window.close();">
                                    </td>
                                </tr>
                            </table>
                            </td></tr>
                            <tr><td>
                            <table class="form" cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tr>
                                    <td class="form-item" nowrap>
                                        ������ͣ�</td>
                                    <td >
                                        <uc1:InputSystemGroup id="inputSystemGroup" ClassCode="1503" Value='<%# Bind("GroupCode") %>'  runat="server" SelectAllLeaf="True"></uc1:InputSystemGroup>
                                        <font color="red">*</font><span runat="server" id="GroupSpan"></span>
                                    </td>
                                    <td class="form-item" nowrap>
                                        �ɹ��ˣ�</td>
								    <td><uc1:InputUser id="InPersonBox" runat="server" Value='<%# Bind("InPerson") %>'></uc1:InputUser>
								    </td>
                                    <td class="form-item">
                                        ������ڣ�</td>
                                    <td >
                                        <cc3:Calendar ID="InDateBox" runat="server" CalendarResource="../Images/CalendarResource/"
                                            ReadOnly="False" Display="True" Value='<%# Bind("InDate") %>'>
                                        </cc3:Calendar>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item" nowrap>
                                       ��ͬ��</td>
								    <td><uc1:inputcontract id="ContractCode" runat="server" Value='<%# Bind("ContractCode") %>' ProjectCode='<%# Request["ProjectCode"]+"" %>'></uc1:inputcontract>
								    </td>
                                </tr>
                            </table>
                            </td></tr>
                            <tr><td>
						    <table cellSpacing="0" cellPadding="0" border="0">
							    <tr>
								    <td class="intopic" width="200">�����ϸ</td>
								    <td><input class="button-small" id="btnAddDtl" type="button" value="�� ��" name="btnAddDtl"
										    runat="server" onserverclick="btnAddDtl_ServerClick"></td>
							    </tr>
						    </table>
						    </td></tr>
                            <tr height="100%"><td>
						    <div style="OVERFLOW:auto;WIDTH:100%;POSITION:absolute;HEIGHT:100%">
					        <asp:DataGrid id="dgDtl" onkeydown="if(event.keyCode==13) event.keyCode=9" runat="server" DataKeyField="MaterialInDtlCode"
							    CellPadding="0" AutoGenerateColumns="False" GridLines="Horizontal"
							    Width="100%" CssClass="list" OnDeleteCommand="dgDtl_DeleteCommand" >
							        <HeaderStyle CssClass="list-title" />
							        <FooterStyle CssClass="list-title" />
							        <Columns>
								    <asp:TemplateColumn HeaderText="���">
									    <HeaderStyle Wrap="False" HorizontalAlign="Center" />
									    <ItemStyle HorizontalAlign="Center" Wrap="False" />
									    <ItemTemplate>
										    <%# Container.ItemIndex + 1 %>
									    </ItemTemplate>
								    </asp:TemplateColumn>
								    <asp:TemplateColumn>
								  <HeaderTemplate >��������<font color="red">*</font></HeaderTemplate>
									    <HeaderStyle HorizontalAlign="Left" />
									    <ItemStyle HorizontalAlign="Left" Wrap="False" />
									    <ItemTemplate>
                                        <uc1:InputMaterial id="InputMaterial" Value='<%# DataBinder.Eval(Container, "DataItem.MaterialCode") %>' OutQty='<%# DataBinder.Eval(Container, "DataItem.OutQty") %>'  runat="server" ></uc1:InputMaterial>
									    </ItemTemplate>
								    </asp:TemplateColumn>
								    <asp:TemplateColumn>
								      <HeaderTemplate >�������<font color="red">*</font></HeaderTemplate>
									    <HeaderStyle HorizontalAlign="Right"/>
									    <ItemStyle HorizontalAlign="Right" Wrap="False" />
									    <ItemTemplate>
										    <igtxt:WebNumericEdit id="txtInQty" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.InQty") %>' CssClass="infra-input-nember" JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js" ImageDirectory="../images/infragistics/images/" MinDecimalPlaces="Two">
										    </igtxt:WebNumericEdit>
									    </ItemTemplate>
								    </asp:TemplateColumn>
								    <asp:TemplateColumn HeaderText="��ⵥ��(Ԫ)">
									    <HeaderStyle HorizontalAlign="Right" />
									    <ItemStyle HorizontalAlign="Right" Wrap="False" />
									    <ItemTemplate>
										    <igtxt:WebNumericEdit id="txtInPrice" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.InPrice") %>' CssClass="infra-input-nember" JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js" ImageDirectory="../images/infragistics/images/" MinDecimalPlaces="Two">
										    </igtxt:WebNumericEdit>
									    </ItemTemplate>
								    </asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="ɾ��">
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
                                        <asp:Button ID="btnSave" Text=" ���� " CausesValidation="true" CssClass="button" runat="server"
                                            CommandName="Insert" />
                                        <input name="btnClose" id="btnClose" type="button" value=" �ر� " class="button" runat="server"
                                            onclick="javascript:window.close();">
                                    </td>
                                </tr>
                            </table>
                            </td></tr>
                            <tr><td>
                            <table class="form" cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tr>
                                    <td class="form-item" nowrap>
                                        ������ͣ�</td>
                                    <td>
                                        <uc1:InputSystemGroup id="inputSystemGroup" ClassCode="1503" Value='<%# Bind("GroupCode") %>'  runat="server" SelectAllLeaf="True"></uc1:InputSystemGroup>
                                        <font color="red">*</font><span runat="server" id="GroupSpan"></span>
                                    </td>
                                    <td class="form-item" nowrap>
                                        �ɹ��ˣ�</td>
								    <td><uc1:InputUser id="InPersonBox" runat="server" Value='<%# Bind("InPerson") %>'></uc1:InputUser>
								    </td>
                                    <td class="form-item">
                                        ������ڣ�</td>
                                    <td>
                                        <cc3:Calendar ID="Calendar1" runat="server" CalendarResource="../Images/CalendarResource/"
                                            ReadOnly="False" Display="True" Value='<%# Bind("InDate") %>'>
                                        </cc3:Calendar>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item" nowrap>
                                       ��ͬ��</td>
								    <td><uc1:inputcontract id="ContractCode" runat="server" Value='<%# Bind("ContractCode") %>' ProjectCode='<%# Request["ProjectCode"]+"" %>'></uc1:inputcontract>
								    </td>
                                </tr>
                            </table>
                             </td></tr>
                            <tr><td>
						    <table cellSpacing="0" cellPadding="0" border="0">
							    <tr>
								    <td class="intopic" width="200">�����ϸ</td>
								    <td><input class="button-small" id="btnAddDtl" type="button" value="�� ��" name="btnAddDtl"
										    runat="server" onserverclick="btnAddDtl_ServerClick"></td>
							    </tr>
						    </table>
						     </td></tr>
                            <tr height="100%"><td>
                            <div style="OVERFLOW:auto;WIDTH:100%;POSITION:absolute;HEIGHT:100%">
					        <asp:DataGrid id="dgDtl" onkeydown="if(event.keyCode==13) event.keyCode=9" runat="server" DataKeyField="MaterialInDtlCode"
							    CellPadding="0" GridLines="Horizontal" AutoGenerateColumns="False" PageSize="15"
							    Width="100%" CssClass="list" OnDeleteCommand="dgDtl_DeleteCommand">
							        <HeaderStyle CssClass="list-title" />
							        <FooterStyle CssClass="list-title" />
							        <Columns>
								    <asp:TemplateColumn HeaderText="���">
									    <HeaderStyle Wrap="False" HorizontalAlign="Center" />
									    <ItemStyle HorizontalAlign="Center" Wrap="False" />
									    <ItemTemplate>
										    <%# Container.ItemIndex + 1 %>
									    </ItemTemplate>
								    </asp:TemplateColumn>
								    <asp:TemplateColumn HeaderText="">
								    <HeaderTemplate >��������<font color="red">*</font></HeaderTemplate>
									    <HeaderStyle HorizontalAlign="Left" />
									    <ItemStyle HorizontalAlign="Left" Wrap="False" />
									    <ItemTemplate>
                                        <uc1:InputMaterial id="InputMaterial" Value='<%# Bind("MaterialCode") %>'  runat="server" ></uc1:InputMaterial>
									    </ItemTemplate>
								    </asp:TemplateColumn>
								    <asp:TemplateColumn HeaderText="�������">
									    <HeaderStyle HorizontalAlign="Right"/>
									    <HeaderTemplate >�������<font color="red">*</font></HeaderTemplate>
									    <ItemStyle HorizontalAlign="Right" Wrap="False" />
									    <ItemTemplate>
										    <igtxt:WebNumericEdit id="txtInQty" runat="server" Value='<%# Bind("InQty") %>' CssClass="infra-input-nember" JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js" ImageDirectory="../images/infragistics/images/" MinDecimalPlaces="Two">
										    </igtxt:WebNumericEdit>
									    </ItemTemplate>
								    </asp:TemplateColumn>
								    <asp:TemplateColumn HeaderText="��ⵥ��(Ԫ)">
									    <HeaderStyle HorizontalAlign="Right" />
									    <ItemStyle HorizontalAlign="Right" Wrap="False" />
									    <ItemTemplate>
										    <igtxt:WebNumericEdit id="txtInPrice" runat="server" Value='<%# Bind("InPrice") %>' CssClass="infra-input-nember" JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js" ImageDirectory="../images/infragistics/images/" MinDecimalPlaces="Two">
										    </igtxt:WebNumericEdit>
									    </ItemTemplate>
								    </asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="ɾ��">
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
                       
                        <table height="100%" width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="#ffffff">
                           <tr>
                          <td>
                            <table class="table" id="tableToolBar" width="100%">
                                <tr>
                                    <td class="tools-area" width="16">
                                        <img src="../images/btn_li.gif" align="absMiddle"></td>
                                    <td class="tools-area">
                                        <asp:Button ID="btnModify" Text=" �޸� " CssClass="button" runat="server" CommandName="Edit" />
                                        <asp:Button ID="btnDelete" Text=" ɾ�� " CssClass="button" runat="server" CommandName="Delete" />
                                        <input name="btnClose" id="btnClose" type="button" value=" �ر� " class="button" runat="server"
                                            onclick="javascript:window.close();">
                                    </td>
                                </tr>
                            </table>
                            </td>
                        </tr>
                           <tr height="100%">
                             <td>
                               <div style="OVERFLOW:auto;POSITION:absolute;WIDTH:100%;HEIGHT:100%">
                               <table  cellpadding="0" width="100%" border="0">
                                <tr>
                                  <td valign="top">
                                    <table class="form" cellspacing="0" cellpadding="0" width="100%" border="0">
                                      <tr>
                                        <td class="form-item" width="10%" nowrap>
                                        ��ⵥ�ţ�</td>
                                        <td witdh="16%">
                                        <%# Eval("MaterialInID")%>
                                        </td>
                                        <td class="form-item" width="10%" nowrap>
                                        ������ͣ�</td>
                                        <td width="24%">
                                             <%# RmsPM.BLL.SystemGroupRule.GetSystemGroupFullName((string)Eval("GroupCode"))%>
                                        </td>
                                        <td class="form-item">
                                            ������ڣ�</td>
                                        <td>
                                            <%# RmsPM.BLL.StringRule.ShowDate(Eval("InDate"))%>
                                        </td>
                                      </tr>   
                                      <tr>
                                        <td class="form-item" width="10%" nowrap>
                                            �ɹ��ˣ�</td>
                                        <td width="30%">
                                        <%# RmsPM.BLL.SystemRule.GetUserName(RmsPM.BLL.ConvertRule.ToString(Eval("InPerson")))%>
                                        </td>
                                        <td class="form-item">
                                            �Ƶ��ˣ�</td>
                                        <td>
                                        <%# RmsPM.BLL.SystemRule.GetUserName(RmsPM.BLL.ConvertRule.ToString(Eval("InputPerson")))%>
                                        </td>
                                        <td class="form-item">
                                            �Ƶ����ڣ�</td>
                                        <td colspan="5">
                                            <%# RmsPM.BLL.StringRule.ShowDate(Eval("InputDate"))%>
                                        </td>
                                       </tr>
                                <tr>
                                    <td class="form-item" nowrap>
                                       ��ͬ���ƣ�</td>
                                    <td>
                                    <a href="#" onclick="doViewContractInfo(this.code,this.Projectcode);return false;" code='<%#  DataBinder.Eval(Container.DataItem, "ContractCode") %>'
                                    Projectcode='<%#  DataBinder.Eval(Container.DataItem, "Projectcode") %>'>
                                    <%#  DataBinder.Eval(Container.DataItem, "ContractName")%>
                                    </a>   
                                       
                                    </td>
                                    <td class="form-item" nowrap>
                                       ��ͬ��ţ�</td>
                                    <td>
                                        <%# Eval("ContractID")%>
                                    </td>
                                </tr>
                            </table>
                            </td></tr>
                            <tr><td>
						    <table cellSpacing="0" cellPadding="0" border="0">
							    <tr>
								    <td class="intopic" width="200">�����ϸ</td>
							    </tr>
						    </table>
						     </td></tr>
						     <tr><td>
                         <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                            AllowPaging="True"  CssClass="list" PageSize="11" 
                            Width="100%" DataSourceID="ObjectDataSource2" GridLines="Horizontal" ShowFooter="True">
					        <HeaderStyle CssClass="list-title" />
					        <FooterStyle CssClass="list-title" />
                            <Columns>
								    <asp:TemplateField HeaderText="���" FooterText="�ϼ�">
									    <HeaderStyle Wrap="False" HorizontalAlign="Center" />
									    <FooterStyle Wrap="False" HorizontalAlign="Center" />
									    <ItemStyle HorizontalAlign="Center" Wrap="False" />
									    <ItemTemplate>
										    <%# Container.DataItemIndex + 1 %>
									    </ItemTemplate>
								    </asp:TemplateField>
                                <asp:TemplateField HeaderText="��������" >
                                    <ItemTemplate>
                                    <a href="#" onclick="javascript:OpenModify('<%# Eval("MaterialCode") %>');return false;"> 
                                    <%# DataBinder.Eval(Container.DataItem, "MaterialName")  %>
                                    </a>      
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="Spec" HeaderText="���"  >
                                <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Unit" HeaderText="��λ"  >
                                <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="�������" >
                                    <ItemTemplate>
                                    <%#RmsPM.BLL.MathRule.GetDecimalShowString(DataBinder.Eval(Container.DataItem, "InQty"))%>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Wrap="False" HorizontalAlign="Right" />
                                </asp:TemplateField>    
                                <asp:TemplateField HeaderText="��ⵥ��(Ԫ)" >
                                    <ItemTemplate>
                                    <%#RmsPM.BLL.MathRule.GetDecimalShowString(DataBinder.Eval(Container.DataItem, "InPrice"))%>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Wrap="False" HorizontalAlign="Right" />
                                </asp:TemplateField>      
                                <asp:TemplateField HeaderText="�����(Ԫ)" >
                                    <ItemTemplate>
                                    <%#RmsPM.BLL.MathRule.GetDecimalShowString(DataBinder.Eval(Container.DataItem, "InMoney"))%>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Wrap="False" HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="��������" >
                                    <ItemTemplate>
                                    <%#RmsPM.BLL.MathRule.GetDecimalShowString(DataBinder.Eval(Container.DataItem, "OutQty"))%>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Wrap="False" HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="�����" >
                                    <ItemTemplate>
                                    <%#RmsPM.BLL.MathRule.GetDecimalShowString(DataBinder.Eval(Container.DataItem, "InvQty"))%>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Wrap="False" HorizontalAlign="Right" />
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                ��ƥ������
                            </EmptyDataTemplate>
                           </asp:GridView>  
                       </td></tr>
                            <tr><td>
						    <table cellSpacing="0" cellPadding="0" border="0">
							    <tr>
								    <td class="intopic" width="200">������ϸ</td>
							    </tr>
						    </table>
						     </td></tr>
						     <tr><td>
						    
                         <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False"
                            AllowPaging="True"  CssClass="list" PageSize="11" 
                            Width="100%" DataSourceID="ObjectDataSource3" GridLines="Horizontal" ShowFooter="True">
					        <HeaderStyle CssClass="list-title" />
					        <FooterStyle CssClass="list-title" />
                            <Columns>
								    <asp:TemplateField HeaderText="���" FooterText="�ϼ�">
									    <HeaderStyle Wrap="False" HorizontalAlign="Center" />
									    <FooterStyle Wrap="False" HorizontalAlign="Center" />
									    <ItemStyle HorizontalAlign="Center" Wrap="False" />
									    <ItemTemplate>
										    <%# Container.DataItemIndex + 1 %>
									    </ItemTemplate>
								    </asp:TemplateField>
                                <asp:TemplateField HeaderText="��������" >
                                    <ItemTemplate>
                                    <a href="#" onclick="javascript:OpenModify('<%# Eval("MaterialCode") %>');return false;"> 
                                    <%# DataBinder.Eval(Container.DataItem, "MaterialName")  %>
                                    </a>      
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="���" >
                                    <ItemTemplate>
                                    <%#(DataBinder.Eval(Container.DataItem, "Spec"))%>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                    <FooterStyle Wrap="False" HorizontalAlign="Left" />
                                </asp:TemplateField> 
                                <asp:TemplateField HeaderText="��λ" ShowHeader="False" >
                                    <ItemTemplate>
                                    <%#(DataBinder.Eval(Container.DataItem, "Unit"))%>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                    <FooterStyle Wrap="False" HorizontalAlign="Left" />
                                </asp:TemplateField> 
                                <asp:TemplateField HeaderText="��������" >
                                    <ItemTemplate>
                                    <%#RmsPM.BLL.MathRule.GetDecimalShowString(DataBinder.Eval(Container.DataItem, "OutQty"))%>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Wrap="False" HorizontalAlign="Right" />
                                </asp:TemplateField>    
                                <asp:TemplateField HeaderText="���õ���(Ԫ)" >
                                    <ItemTemplate>
                                    <%#RmsPM.BLL.MathRule.GetDecimalShowString(DataBinder.Eval(Container.DataItem, "OutPrice"))%>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Wrap="False" HorizontalAlign="Right" />
                                </asp:TemplateField>      

                                <asp:TemplateField HeaderText="���ý��(Ԫ)" >
                                    <ItemTemplate>
                                    <%#RmsPM.BLL.MathRule.GetDecimalShowString(DataBinder.Eval(Container.DataItem, "OutMoney"))%>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Wrap="False" HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="��������">
                                    <ItemTemplate>
                                    <%# RmsPM.BLL.StringRule.ShowDate((DataBinder.Eval(Container.DataItem, "OutDate")))%>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                    <FooterStyle Wrap="False" HorizontalAlign="Left" />
                                </asp:TemplateField> 
                                <asp:TemplateField HeaderText="���ϵ���">
                                    <ItemTemplate>
                                        <a href="#" onclick="javascript:OpenOutModify('<%# Eval("MaterialOutCode") %>');return false;">
                                            <%# Eval("MaterialOutID")%>
                                        </a>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                ��ƥ������
                            </EmptyDataTemplate>
                           </asp:GridView>
                         
                       </td></tr>
                        </table>
                              </div> 
                            </td>
                           </tr>
                       </table>
                        </ItemTemplate>
                    </asp:FormView>
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetMaterialIn" 
                    TypeName="RmsPM.BFL.MaterialInBFL" OnInserted="ObjectDataSource1_Inserted" OldValuesParameterFormatString="original_{0}" DataObjectTypeName="TiannuoPM.MODEL.MaterialInModel" DeleteMethod="Delete" InsertMethod="Insert" UpdateMethod="Update">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="Code" QueryStringField="MaterialInCode" Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="ObjectDataSource3" TypeName="RmsPM.BFL.MaterialOutBFL"  runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetMaterialOutDtlListByMaterialInCode" OnSelected="ObjectDataSource3_Selected">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="MaterialInCode" QueryStringField="MaterialInCode" Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>


                    <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" Style="display: none">ˢ��</asp:LinkButton>
                    <asp:ObjectDataSource ID="ObjectDataSource2" TypeName="RmsPM.BFL.MaterialInBFL"  runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetMaterialInDtlList" OnSelected="ObjectDataSource2_Selected">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="MaterialInCode" QueryStringField="MaterialInCode" Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </td>
            </tr>
    </table>
    <asp:TextBox runat="server" id="MaterialInCode"></asp:TextBox><asp:TextBox runat="server" id="Source3Count"></asp:TextBox>
    </form>
<script language="javascript">
function doViewContractInfo(code,Projectcode)
{
	OpenFullWindow('../Contract/ContractInfo.aspx?ProjectCode='+Projectcode+'&ContractCode=' + code,'��ͬ��Ϣ');
}
</script>
</body>
</html>
