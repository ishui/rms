<%@ Reference Control="~/usercontrols/inputcostbudgetdtl.ascx" %>
<%@ Reference Control="~/usercontrols/exchangeratecontrol.ascx" %>
<%@ Register TagPrefix="uc1" TagName="InputCostBudgetDtl" Src="../UserControls/InputCostBudgetDtl.ascx" %>
<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics.WebUI.WebDataInput.v5.1, Version=5.1.20051.37, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Register TagPrefix="uc1" TagName="AttachMentList" Src="../UserControls/AttachMentList.ascx" %>
<%@ Page language="c#" Inherits="RmsPM.Web.Contract.ContractChangeInfo" CodeFile="ContractChangeInfo.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="ExchangeRate" Src="../UserControls/ExchangeRateControl.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>��ͬ�����Ϣ</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<LINK href="../Images/infra.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<TABLE height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td bgColor="#e4eff6" height="6"></td>
				</tr>
				<tr>
					<td height="25">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="topic" background="../images/topic_bg.gif"><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle"><span id="spanTitle" runat="server">��ͬ���� 
										- ��ͬ�����Ϣ</span></td>
								<td width="9"><IMG height="25" src="../images/topic_corr.gif" width="9"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td class="tools-area" vAlign="top"><IMG src="../images/btn_li.gif" align="absMiddle">
						<input class="button" id="btnModify" value="�� ��" onclick="Modify();" type="button" name="btnModify"
							runat="server"> <input class="button" id="btnDelete" value="ɾ ��" onclick="javascript:if(!window.confirm('ȷʵҪɾ��������¼��')) return false;"
							type="button" name="btnDelete" onserverclick="btnDelete_ServerClick" runat="server">
							<input class="button" id="btnCheckDelete" value="��˺�ɾ��" onclick="javascript:if(!window.confirm('ȷʵҪɾ��������¼��')) return false;"
							type="button" name="btnCheckDelete" onserverclick="btnCheckDelete_ServerClick" runat="server">
							 <input class="button" id="btnCheck" value="�ύ����" onclick="javascript:DoCheck();" type="button"
							name="btnCheck" runat="server"> <input class="button" id="btnOldCheck" value="���" onclick="javascript:if(!window.confirm('ȷʵҪ��׼���α����')) return false;"
							type="button" name="btnOldCheck" runat="server" onserverclick="btnOldCheck_ServerClick"> <input class="button" id="btnPrint" onclick="DoPrint();return false;" type="button" value="����������ӡ"
							runat="server" NAME="btnPrint"> <input class="button" id="btnClose" value="�� ��" onclick="window.close();" type="button"
							name="btnClose">
					</td>
				</tr>
				<tr>
					<td class="table">
						<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
							<table class="form" cellSpacing="0" cellPadding="0" width="100%" border="0">
								<tr>
									<td class="form-item" width="150">��Ŀ���ƣ�</td>
									<td><asp:label id="lblProjectName" Runat="server"></asp:label>&nbsp;</td>
									<td class="form-item" width="150">��ͬ��ţ�</td>
									<td><asp:label id="lblContractID" Runat="server"></asp:label>&nbsp;</td>
								</tr>
								<tr>
									<td class="form-item">��ͬ���ƣ�</td>
									<td><asp:label id="lblContractName" Runat="server"></asp:label>&nbsp;</td>
									<td class="form-item">������ݣ�</td>
									<td><asp:Label ID="lblVoucher" runat="server"></asp:Label>&nbsp;</td>
								</tr>
								<tr>
									<td class="form-item">�� Ӧ �̣�</td>
									<td><asp:label id="lblSupplierName" Runat="server"></asp:label>&nbsp;</td>
									<td class="form-item">�������ţ�</td>
									<td><asp:Label ID="lblChangeId" Runat="server"></asp:Label>&nbsp;</td>
								</tr>
								<tr>
									<td class="form-item" vAlign="top">���ԭ��ժҪ��</td>
									<td colSpan="3">
										<asp:Label ID="lblChangeReason" Runat="server"></asp:Label><br>
										<table cellpadding="0" cellspacing="0" border="0">
											<tr>
												<td>������</td>
												<td><uc1:attachmentlist id="myAttachMentList" runat="server"></uc1:attachmentlist></td>
											</tr>
										</table>
									</td>
								</tr>
								<tr>
									<td class="form-item">ԭ��ͬ��</td>
									<td><INPUT class="infra-input-nember" id="txtBudgetMoney" style="FLOAT: left; WIDTH: 120px; TEXT-ALIGN: right"
											readOnly type="text" name="txtBudgetMoney" runat="server">
                                        <asp:Label runat="server" ID="lblForeignMoneyType"></asp:Label>
									</td>
									<td class="form-item">��Ӧ�̱��α�������</td>
									<td><igtxt:webnumericedit id="txtSupplierChangeMoney" runat="server" JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js"
											JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js" ImageDirectory="../images/infragistics/images/"
											CssClass="infra-input-nember" MinDecimalPlaces="Two" Width="100" readonly></igtxt:webnumericedit>
                                        <asp:Label runat="server" ID="lblForeignMoneyType2"></asp:Label>
									</td>
								</tr>
								<tr>
									<td class="form-item">�ݶ����/ָ����</td>
									<td><INPUT class="infra-input-nember" id="txtAdjustMoney" style="FLOAT: left; WIDTH: 120px; TEXT-ALIGN: right"
											readOnly type="text" name="txtAdjustMoney" runat="server">
                                        <asp:Label runat="server" ID="lblForeignMoneyType3"></asp:Label>
									</td>
									<td class="form-item">���ʹ���ʦ��˽�</td>
									<td><igtxt:webnumericedit id="txtConsultantAuditMoney" runat="server" JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js"
											JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js" ImageDirectory="../images/infragistics/images/"
											CssClass="infra-input-nember" MinDecimalPlaces="Two" Width="100" readonly></igtxt:webnumericedit>
                                        <asp:Label runat="server" ID="lblForeignMoneyType4"></asp:Label>
								    </td>
								</tr>
								<tr>
									<td class="form-item">ʵ�ʽ�</td>
									<td><input id="hidOriginalMoney" type="hidden" name="hidOriginalMoney" runat="server">
										<input class="infra-input-nember" id="txtOriginalMoney" style="FLOAT: left; WIDTH: 120px; TEXT-ALIGN: right"
											readOnly type="text" name="txtOriginalMoney" runat="server">
                                        <asp:Label runat="server" ID="lblForeignMoneyType5"></asp:Label>
									</td>
									<td class="form-item">��Ŀ��Լ����˽�</td>
									<td><igtxt:webnumericedit id="txtProjectAuditMoney" runat="server" JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js"
											JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js" ImageDirectory="../images/infragistics/images/"
											CssClass="infra-input-nember" MinDecimalPlaces="Two" Width="100" readonly></igtxt:webnumericedit>
                                        <asp:Label runat="server" ID="lblForeignMoneyType6"></asp:Label>
									</td>
								</tr>
								<tr>
									<td class="form-item">�ۼ����������</td>
									<td><input id="hidTotalChangeMoney" type="hidden" name="hidTotalChangeMoney" runat="server">
										<input class="infra-input-nember" id="txtTotalChangeMoney" style="FLOAT: left; WIDTH: 120px; TEXT-ALIGN: right"
											readOnly type="text" name="txtTotalChangeMoney" runat="server">
                                        <asp:Label runat="server" ID="lblForeignMoneyType7"></asp:Label>
									</td>
									<td class="form-item">��ͬ������ͣ�</td>
									<td><asp:Label ID="lblChangeType" runat="server"></asp:Label>&nbsp;</td>
								</tr>
								<tr>
									<td class="form-item">�����ϱ������</td>
									<td><input class="infra-input-nember" id="txtChangeMoney" style="WIDTH: 120px; TEXT-ALIGN: right"
											readOnly type="text" name="txtChangeMoney" runat="server">
                                        <asp:Label runat="server" ID="lblForeignMoneyType8"></asp:Label>
									</td>
									<td></td>
									<td></td>
								</tr>
								<tr>
									<td class="form-item">��ͬ�ܶ�Ԥ�ƣ�</td>
									<td><input class="infra-input-nember" id="txtNewTotalMoney" style="FLOAT: left; WIDTH: 120px; TEXT-ALIGN: right"
											readOnly type="text" name="txtNewTotalMoney" runat="server">
                                        <asp:Label runat="server" ID="lblForeignMoneyType9"></asp:Label>
									</td>
									<td></td>
									<td></td>
								</tr>
							</table>
							<br>
							<table cellSpacing="0" cellPadding="0" width="100%" border="0">
								<tr>
									<td class="intopic" width="200">��ͬ������ϸ</td>
									<td></td>
								</tr>
							</table>
							<TABLE cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
								<TR>
									<TD><asp:datagrid id="dgCostList" onkeydown="if(event.keyCode==13) event.keyCode=9" runat="server"
											CssClass="list" Width="100%" ShowFooter="True" AllowSorting="True" AutoGenerateColumns="False"
											PageSize="15">
											<AlternatingItemStyle CssClass="list-i"></AlternatingItemStyle>
											<HeaderStyle CssClass="list-title"></HeaderStyle>
											<FooterStyle CssClass="list-title"></FooterStyle>
											<Columns>
												<asp:BoundColumn Visible="False" DataField="ContractCostChangeCode"></asp:BoundColumn>
												<asp:BoundColumn Visible="False" DataField="ContractCostCode"></asp:BoundColumn>
												<asp:TemplateColumn HeaderText="���" ItemStyle-Width="40">
													<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<%# Container.ItemIndex + 1 %>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="������&lt;font color=red&gt;*&lt;/font&gt;" FooterText="�ϼƣ�RMB����" ItemStyle-Width="250"
													FooterStyle-HorizontalAlign="Right">
													<ItemTemplate>
														<uc1:InputCostBudgetDtl id="ucCostBudgetDtl" runat="server" CostBudgetSetCode='<%# DataBinder.Eval(Container, "DataItem.CostBudgetSetCode") %>' CostCode='<%# DataBinder.Eval(Container, "DataItem.CostCode") %>'>
														</uc1:InputCostBudgetDtl>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="ԭʼ���" ItemStyle-Width="100">
													<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
													<ItemTemplate>
															<uc1:ExchangeRate id="ucExchangeRate" runat="server"></uc1:ExchangeRate>
													</ItemTemplate>
													<FooterTemplate>
														<asp:Label id="lblSumCostOriginalMoney" runat="server"></asp:Label>&nbsp;
													</FooterTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="ԭʼ���" ItemStyle-Width="100" Visible="False">
													<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
													<ItemTemplate>
														<asp:Label id="lblCostOriginalMoney" runat="server" Text='<%# RmsPM.BLL.MathRule.GetDecimalShowString(DataBinder.Eval(Container, "DataItem.OriginalMoney"),"",true) %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="�ۼ��������" ItemStyle-Width="100">
													<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
													<ItemTemplate>
														<asp:Label id="lblCostTotalChangeCash" runat="server" Text='<%# RmsPM.BLL.MathRule.GetDecimalShowString(DataBinder.Eval(Container, "DataItem.TotalChangeCash"),"",true) %>'>
														</asp:Label>
													</ItemTemplate>
													<FooterTemplate>
														<asp:Label id="lblSumCostTotalChangeMoney" runat="server"></asp:Label>
													</FooterTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="���α�����" ItemStyle-Width="100">
													<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
													<ItemTemplate>
														<asp:Label id="lblCostChangeCash" runat="server" Text='<%# RmsPM.BLL.MathRule.GetDecimalShowString(DataBinder.Eval(Container, "DataItem.ChangeCash")) %>'>
														</asp:Label>
													</ItemTemplate>
													<FooterTemplate>
														<asp:Label id="lblSumCostChangeMoney" runat="server"></asp:Label>
													</FooterTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Ԥ�ƽ��" ItemStyle-Width="100">
													<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
													<ItemTemplate>
														<asp:Label id="lblCostNewCash" runat="server" Text='<%# RmsPM.BLL.MathRule.GetDecimalShowString(DataBinder.Eval(Container, "DataItem.NewCash"),"",true) %>'>
														</asp:Label>
													</ItemTemplate>
													<FooterTemplate>
														<asp:Label id="lblSumCostNewMoney" runat="server"></asp:Label>
													</FooterTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="˵��" ItemStyle-Width="200">
													<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblDescription" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Description") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
										</asp:datagrid>
									</TD>
								</TR>
							</TABLE>
                            <br>
							<table cellSpacing="0" cellPadding="0" width="100%" border="0">
								<tr>
									<td class="intopic" width="200">��ص���</td>
									<td></td>
								</tr>
							</table>
							<table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
								<tr>
									<td>
									    <asp:GridView ID="gvNexusList" runat="server" AutoGenerateColumns="false" CssClass="list"
									        ShowFooter="true" ShowHeader="true" Width="100%">
                                            <RowStyle CssClass="list-i" />
											<HeaderStyle CssClass="list-title" Wrap="false" HorizontalAlign="center"/>
											<FooterStyle CssClass="list-title" Wrap="false" HorizontalAlign="right"/>
											<Columns>
											    <asp:TemplateField HeaderText="���">
											        <ItemTemplate>
											            <%# Container.DisplayIndex + 1 %>
											        </ItemTemplate>
											    </asp:TemplateField>
											    <asp:BoundField DataField="ContractCode" HeaderText="��ͬϵͳ���" Visible="false" />
											    <asp:BoundField DataField="ContractChangeCode" HeaderText="��ͬ���ϵͳ���" Visible="false" />
											    <asp:BoundField DataField="Code" HeaderText="ϵͳ���" Visible="false" />
											    <asp:BoundField DataField="Type" HeaderText="��������" Visible="false" />
											    <asp:TemplateField HeaderText="��������">
											        <ItemStyle HorizontalAlign="center" />
											        <ItemTemplate>
											            <asp:Label id="lblNexusType" runat="server"></asp:Label>
											        </ItemTemplate>
											    </asp:TemplateField>
											    <asp:TemplateField HeaderText="����">
											        <ItemStyle HorizontalAlign="center" />
											        <ItemTemplate>
											            <a href="#" onclick="javascript:OpenFullWindow(' <%# DataBinder.Eval(Container, "DataItem.Path" )  %>','');"><%# DataBinder.Eval(Container, "DataItem.Name" )  %></a>
											        </ItemTemplate>
											    </asp:TemplateField>
											    <asp:BoundField DataField="ID" HeaderText="���" ItemStyle-HorizontalAlign="center"/>
											    
											    <asp:TemplateField HeaderText="���">
											        <ItemStyle HorizontalAlign="right" />
											        <FooterStyle HorizontalAlign="right" />
											        <ItemTemplate>
											            <%# DataBinder.Eval(Container, "DataItem.Money","{0:N}" )%>
											        </ItemTemplate>
											        <FooterTemplate>
											            �ϼƣ�&nbsp;&nbsp;<asp:Label id="lblSumMoney" runat="server"></asp:Label>
											        </FooterTemplate>
											    </asp:TemplateField>
											    <asp:TemplateField HeaderText="������">
											        <ItemStyle HorizontalAlign="center" />
											        <ItemTemplate>
											            <asp:Label ID="lblPersonName" runat="server"></asp:Label>
											        </ItemTemplate>
											    </asp:TemplateField>
					                            <asp:BoundField DataField="Date" HeaderText="����" ItemStyle-HorizontalAlign="center"/>
											</Columns>
									    </asp:GridView>
									</TD>
								</TR>
							</TABLE>							
						</div>
					</td>
				</tr>
				<tr>
					<td height="12">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td background="../images/corl_bg.gif"><IMG height="12" src="../images/corl.gif" width="12"></td>
								<td width="12"><IMG height="12" src="../images/corr.gif" width="12"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td bgColor="#e4eff6" height="6"></td>
				</tr>
			</TABLE>
			<input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server"> <input id="txtFromUrl" type="hidden" name="txtFromUrl" runat="server">
			<INPUT id="txtContractCode" type="hidden" name="txtContractCode" runat="server">
			<INPUT id="txtContractChangeCode" type="hidden" name="txtContractChangeCode" runat="server">
			<INPUT id="txtStatus" type="hidden" name="txtStatus" runat="server">
		</form>
		<script language="javascript">
<!--

//�޸�
function Modify()
{
	window.location.href = "../Contract/ContractChange.aspx?ProjectCode=<%=Request["ProjectCode"]%>&Act=Edit&ContractCode=<%=Request["ContractCode"]%>&ContractChangeCode=<%=Request["ContractChangeCode"]%>";
}
	
//���
function DoCheck()
{
	OpenFullWindow('<%=ViewState["_AuditingURL"]%>?ProjectCode=<%=Request["ProjectCode"]%>&ContractChangeCode=<%=Request["ContractChangeCode"]%>&ApplicationCode1=<%=Request["ContractChangeCode"]%>','��ͬ������_<%=Request["ContractChangeCode"]%>');
}

function DoPrint()
{
	OpenFullWindow('../Contract/ContractChangePrint.aspx?ProjectCode=<%=Request["ProjectCode"]%>&ContractCode=<%=Request["ContractCode"]%>&ContractChangeCode=<%=Request["ContractChangeCode"]%>','��ӡԤ��');
}

//-->
		</script>
	</body>
</HTML>
