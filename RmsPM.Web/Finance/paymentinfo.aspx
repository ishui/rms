<%@ Reference Control="~/usercontrols/exchangeratecontrol.ascx" %>
<%@ Page language="c#" Inherits="RmsPM.Web.Finance.PaymentInfo" CodeFile="PaymentInfo.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="ExchangeRate" Src="../UserControls/ExchangeRateControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AttachMentList" Src="../UserControls/AttachMentList.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WorkFlowList" Src="../WorkFlowControl/WorkFlowList.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>��</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
</SCRIPT>
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
								<td class="topic" background="../images/topic_bg.gif"><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle"><span id="spanTitle" runat="server">��Ŀ����>�������>������>����Ϣ</span></td>
								<td width="9"><IMG height="25" src="../images/topic_corr.gif" width="9"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td class="tools-area" vAlign="top"><IMG src="../images/btn_li.gif" align="absMiddle">
						<input class="button" id="btnModify" onclick="Modify();" type="button" value="�� ��" name="btnModify" runat="server" /> 
						<input class="button" id="btnModifyEx" style="DISPLAY: none" onclick="AuditedModify();" type="button" value="��˺��޸�" name="btnModifyEx" runat="server" /> 
						<input class="button" id="btnDelete" onclick="javascript:if(!window.confirm('ȷʵҪɾ��������¼��')) return false;" type="button" value="ɾ ��" name="btnDelete" runat="server" onserverclick="btnDelete_ServerClick" /> 
							<input class="button" id="btnCheckDelete" value="��˺�ɾ��" onclick="javascript:if(!window.confirm('ȷʵҪɾ��������¼��')) return false;" type="button" name="btnCheckDelete" onserverclick="btnCheckDelete_ServerClick" runat="server">
						<input class="button" id="btnCheck" onclick="javascript:DoCheck();" type="button" value="�ύ" name="btnCheck" runat="server" /> 
						
						<input class="button" id="btnSubmitAccount"  visible="false" onclick="javascript:DoSubmitAccount();" type="button" value="�ύ���ñ���������" name="btnCheck" runat="server" /> 
						<input class="button" id="btnCheckPaymentCheck" onclick="javascript:DoCheckPaymentCheck();" type="button" value="֧Ʊ���ύ" name="BtCheckPaymentCheck" runat="server" /> 
						<input class="button" id="btnOldCheck" onclick="javascript:DoOldCheck();" type="button" value="�� ��" name="btnOldCheck" runat="server" />
						<input class="button" id="btnPayout" onclick="javascript:Payout();" type="button" value="�� ��" name="btnPayout" runat="server" /> 
						<input class="button" id="btnAccount" onclick="javascript:if (!Account()) return false;" type="button" value="�� ��" name="btnAccount" runat="server" onserverclick="btnAccount_ServerClick" />
						<input class="button" id="btnPrint" onclick="DoPrint();return false;" type="button" value="�����������ӡ"	runat="server" NAME="btnPrint" />
					    
						<input class="button" id="btnClose" style="DISPLAY: none" onclick="window.close();" type="button" value="�� ��" name="btnClose" />
					</td>
				</tr>
				<tr>
					<td class="table">
						<TABLE class="form" id="Table4" cellSpacing="0" cellPadding="0" width="100%">
							<TR>
								<TD class="form-item">���ţ�</TD>
								<TD><asp:label id="lblPaymentID" runat="server"></asp:label></TD>
								<TD class="form-item">������ͣ�</TD>
								<TD><asp:label id="lblIsContractName" Runat="server"></asp:label></TD>
								<TD class="form-item">״ ̬��</TD>
								<TD><asp:label id="lblStatusName" Runat="server"></asp:label></TD>
								<TD class="form-item">����ܶ<span id="spanOldMoney2" style="DISPLAY: none" runat="server"><br>
										ԭ����ܶ</span></TD>
								<TD><asp:label id="lblMoney" runat="server"></asp:label><span id="spanOldMoney" style="DISPLAY: none" runat="server"><br>
										<asp:label id="lblOldMoney" Runat="server"></asp:label></span></TD>
							</TR>
							<tr id="TrPayment" runat="server">
			                    <td class="form-item" width="10%">������ƣ�</td>
				                <td colspan="7">
				                <asp:label id="lblPaymentName" Width="45%" Runat="server"></asp:label>&nbsp;
				                </td>
							</tr>
							<TR>
								<TD class="form-item">�ܿλ��</TD>
								<TD colSpan="3"><asp:label id="lblSupplyName" runat="server"></asp:label></TD>
								<TD class="form-item">�� �� �ˣ�</TD>
								<TD><asp:label id="lblPayer" runat="server"></asp:label></TD>
                                <TD class="form-item">���ҵ��</TD>
								<TD><asp:label id="lblPaymentTitle" runat="server"></asp:label></TD>								
							</TR>
							<tr>
								<TD class="form-item">���ţ�</TD>
								<TD colSpan="3"><asp:label id="lblUnitName" runat="server"></asp:label></TD>
								<TD class="form-item">��󸶿��գ�</TD>
								<TD><asp:label id="lblPayDate" runat="server"></asp:label></TD>
								<TD class="form-item">������ͣ�</TD>
								<TD><asp:label id="lblGroupName" runat="server"></asp:label></TD>
							</tr>
							<TR id="trContract" runat="server">
								<TD class="form-item">��ͬ���ƣ�</TD>
								<TD colSpan="3"><A href="#" onclick="ViewContractInfo();return false;"><asp:label id="lblContractName" runat="server"></asp:label></A></TD>
								<TD class="form-item">��ͬ��ţ�</TD>
								<TD colSpan="3"><asp:label id="lblContractID" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="form-item">�� �� �ˣ�</TD>
								<TD><asp:label id="lblApplyPersonName" runat="server"></asp:label></TD>
								<TD class="form-item">�������ڣ�</TD>
								<TD><asp:label id="lblApplyDate" runat="server"></asp:label></TD>
								<TD class="form-item">�� �� �ˣ�</TD>
								<TD><asp:label id="lblCheckPersonName" runat="server"></asp:label></TD>
								<TD class="form-item">������ڣ�</TD>
								<TD><asp:label id="lblCheckDate" runat="server"></asp:label></TD>
							</TR>
							<TR >
								<TD class="form-item">�������У�</TD>
								<TD colSpan="3"><asp:label id="lblBankName" runat="server"></asp:label></TD>
								<TD class="form-item">�����ʺţ�</TD>
								<TD colSpan="3"><asp:label id="lblBankAccount" runat="server"></asp:label></TD>
							</TR>							
							<TR>
								<TD class="form-item">��ע��</TD>
								<TD colSpan="7"><asp:label id="lblRemark" runat="server"></asp:label></TD>
							</TR>
							<TR style="DISPLAY: none">
								<TD class="form-item">������;��</TD>
								<TD colSpan="5"><asp:label id="lblPurpose" runat="server"></asp:label></TD>
								<TD class="form-item">����������</TD>
								<TD><asp:label id="lblRecieptCount" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="form-item">�ϴ�������</TD>
								<TD colspan="7"><uc1:attachmentlist id="myAttachMentList" runat="server"></uc1:attachmentlist></TD>
							</TR>
							<TR id="trCostBatchPayment" runat="server" style="display:none">
								<TD class="form-item">Ԥ���</TD>
								<td colspan="7"><asp:Label runat="server" ID="lblCostBudgetSetName"></asp:Label></td>
							</TR>
						</TABLE>
					</td>
				</tr>
				<tr height="100%">
					<td class="table" vAlign="top">
						<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
							<table cellSpacing="0" cellPadding="0" border="0">
								<tr>
									<td class="intopic" width="200">�����ϸ</td>
								</tr>
							</table>
							<TABLE cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
								<TR>
									<TD vAlign="top">
									    <asp:datagrid id="dgList" runat="server" DataKeyField="PaymentItemCode" CellPadding="0" AllowSorting="True"
											GridLines="Both" AutoGenerateColumns="False" PageSize="15" ShowFooter="True" Width="100%" CssClass="List">
											<HeaderStyle CssClass="list-title"></HeaderStyle>
											<FooterStyle CssClass="list-title"></FooterStyle>
											<Columns>
												<asp:TemplateColumn HeaderText="���">
													<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<%# Container.ItemIndex + 1 %>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="��λ����">
													<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%# RmsPM.BLL.CostBudgetRule.GetPBSName(RmsPM.BLL.ConvertRule.ToString(DataBinder.Eval(Container, "DataItem.CostBudgetSetCode"))) %>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="��������" FooterText="�ϼ�">
													<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%# DataBinder.Eval(Container, "DataItem.Summary") %>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Center"></FooterStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="�����">
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
								                        <asp:Label ID="lblPaymentMoneyType" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MoneyType") %>'></asp:Label>��&nbsp;&nbsp;
								                        <asp:Label ID="lblPaymentCash" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ItemCash","{0:N}") %>'></asp:Label>
												    </ItemTemplate>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="�����(Ԫ)" Visible="False">
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<%# DataBinder.Eval(Container, "DataItem.ItemMoney", "{0:N}") %>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="ԭ�����(Ԫ)" Visible="False">
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<%# DataBinder.Eval(Container, "DataItem.OldItemMoney", "{0:N}") %>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="������">
													<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<a style="cursor:hand" onclick="javascript:ViewCostCode(this.code);" code='<%#  DataBinder.Eval(Container.DataItem, "CostCode") %>'>
															<%# DataBinder.Eval(Container, "DataItem.CostName") %>
														</a>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="��̯" Visible="False">
													<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%# DataBinder.Eval(Container, "DataItem.BuildingNameAll") %>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="�Ѹ����">
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<%# DataBinder.Eval(Container, "DataItem.TotalPayoutCash", "{0:N}") %>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="δ�����">
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<%# DataBinder.Eval(Container, "DataItem.RemainItemCash", "{0:N}") %>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
												</asp:TemplateColumn>
											
												<asp:TemplateColumn HeaderText="�Ѹ����(Ԫ)" Visible="false">
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<%# DataBinder.Eval(Container, "DataItem.TotalPayoutMoney", "{0:N}") %>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="δ�����(Ԫ)" Visible="false">
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<%# DataBinder.Eval(Container, "DataItem.RemainItemMoney", "{0:N}") %>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle NextPageText="��ҳ&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;��ҳ" HorizontalAlign="Center"
												CssClass="ListHeadTr"></PagerStyle>
										</asp:datagrid>
									</TD>
								</TR>
								<tr>
								    <td>
								        <br />
								        <asp:GridView ID="gvPaymentDetail" runat="server" DataKeyField="PaymentItemCode" CellPadding="0" AllowSorting="True"
											GridLines="Both" AutoGenerateColumns="False" PageSize="15" ShowFooter="false" Width="100%" CssClass="List">
                                            <RowStyle CssClass="list-i" />
											<HeaderStyle CssClass="list-title" Wrap="false" HorizontalAlign="center"/>
											<FooterStyle CssClass="list-title" Wrap="false" HorizontalAlign="right"/>
											<Columns>
												<asp:TemplateField HeaderText="���">
													<HeaderStyle Wrap="False" HorizontalAlign="Center" />
													<ItemStyle HorizontalAlign="Center" Wrap="False" />
													<ItemTemplate>
														<%# Container.DataItemIndex + 1 %>
													</ItemTemplate>
												</asp:TemplateField>
												<asp:TemplateField HeaderText="��λ����">
													<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%# RmsPM.BLL.CostBudgetRule.GetPBSName(RmsPM.BLL.ConvertRule.ToString(DataBinder.Eval(Container, "DataItem.CostBudgetSetCode"))) %>
													</ItemTemplate>
												</asp:TemplateField>
												<asp:BoundField HeaderText="��������" FooterText="�ϼ�" DataField="Summary" />
												<asp:TemplateField HeaderText="��������">
												    <ItemTemplate>
														<uc1:ExchangeRate id="ucExchangeRate" runat="server" />
													</ItemTemplate>
												</asp:TemplateField>
												<asp:TemplateField  HeaderText="Ԥ����">
												    <HeaderStyle HorizontalAlign="Left" />
													<ItemStyle Wrap="False" />
													<FooterStyle HorizontalAlign="right" />
													<ItemTemplate>
													    <asp:Label ID="lblItemCash1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ItemCash1","{0:N}") %>'></asp:Label>
													</ItemTemplate>
													<FooterTemplate>
													    <asp:Label ID="lblSumItemMoney1" runat="server"></asp:Label>
													</FooterTemplate>
												</asp:TemplateField>
												<asp:TemplateField  HeaderText="���տ�">
												    <HeaderStyle HorizontalAlign="Left" />
													<ItemStyle Wrap="False" />
													<FooterStyle HorizontalAlign="right" />
													<ItemTemplate>
													    <asp:Label ID="lblItemCash2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ItemCash2","{0:N}") %>'></asp:Label>
													</ItemTemplate>
													<FooterTemplate>
													    <asp:Label ID="lblSumItemMoney2" runat="server"></asp:Label>
													</FooterTemplate>
												</asp:TemplateField>
												<asp:TemplateField  HeaderText="���޿�">
												    <HeaderStyle HorizontalAlign="Left" />
													<ItemStyle Wrap="False" />
													<FooterStyle HorizontalAlign="right" />
													<ItemTemplate>
													    <asp:Label ID="lblItemCash3" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ItemCash3","{0:N}") %>'></asp:Label>
													</ItemTemplate>
													<FooterTemplate>
													    <asp:Label ID="lblSumItemMoney3" runat="server"></asp:Label>
													</FooterTemplate>
												</asp:TemplateField>
												<asp:TemplateField  HeaderText="����ۿ�">
												    <HeaderStyle HorizontalAlign="Left" />
													<ItemStyle Wrap="False" />
													<FooterStyle HorizontalAlign="right" />
													<ItemTemplate>
													    <asp:Label ID="lblItemCash4" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ItemCash4","{0:N}") %>'></asp:Label>
													</ItemTemplate>
													<FooterTemplate>
													    <asp:Label ID="lblSumItemMoney4" runat="server"></asp:Label>
													</FooterTemplate>
												</asp:TemplateField>
												<asp:TemplateField  HeaderText="�ۿ��">
												    <HeaderStyle HorizontalAlign="Left" />
													<ItemStyle Wrap="False" />
													<FooterStyle HorizontalAlign="right" />
													<ItemTemplate>
													    <asp:Label ID="lblItemCash5" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ItemCash5","{0:N}") %>'></asp:Label>
													</ItemTemplate>
													<FooterTemplate>
													    <asp:Label ID="lblSumItemMoney5" runat="server"></asp:Label>
													</FooterTemplate>
												</asp:TemplateField>
												<asp:TemplateField  HeaderText="�����ۿ�">
												    <HeaderStyle HorizontalAlign="Left" />
													<ItemStyle Wrap="False" />
													<FooterStyle HorizontalAlign="right" />
													<ItemTemplate>
													    <asp:Label ID="lblItemCash6" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ItemCash6","{0:N}") %>'></asp:Label>
													</ItemTemplate>
													<FooterTemplate>
													    <asp:Label ID="lblSumItemMoney6" runat="server"></asp:Label>
													</FooterTemplate>
												</asp:TemplateField>
											</Columns>
								        </asp:GridView>
								        
								        
								    </td>
								</tr>
							</TABLE>
							<table id="trContract2" style="DISPLAY: none" cellSpacing="0" cellPadding="0" border="0"
								runat="server">
								<tr>
									<td class="intopic" width="200">��ͬ������ϸ</td>
								</tr>
							</table>
							<TABLE id="trContract3" style="DISPLAY: none" cellSpacing="0" cellPadding="0" width="100%"
								align="center" border="0" runat="server">
								<TR>
									<TD vAlign="top"><asp:datagrid id="dgContractAllocation" runat="server" DataKeyField="AllocateCode" CellPadding="0"
											AllowSorting="True" GridLines="Both" AutoGenerateColumns="False" PageSize="15" ShowFooter="True" Width="100%"
											CssClass="List">
											<HeaderStyle CssClass="list-title"></HeaderStyle>
											<FooterStyle CssClass="list-title"></FooterStyle>
											<Columns>
												<asp:TemplateColumn HeaderText="���">
													<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<%# Container.ItemIndex + 1 %>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="��������" FooterText="�ϼ�">
													<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%# DataBinder.Eval(Container, "DataItem.ItemName") %>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Center"></FooterStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="������">
													<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%# RmsPM.BLL.CBSRule.GetCostName(RmsPM.BLL.ConvertRule.ToString(DataBinder.Eval(Container, "DataItem.CostCode")))%>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="��ͬ���(Ԫ)">
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<%# DataBinder.Eval(Container, "DataItem.Money", "{0:N}") %>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="�������(Ԫ)">
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<%# DataBinder.Eval(Container, "DataItem.TotalPaymentMoney", "{0:N}") %>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle NextPageText="��ҳ&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;��ҳ" HorizontalAlign="Center"
												CssClass="ListHeadTr"></PagerStyle>
										</asp:datagrid></TD>
								</TR>
							</TABLE>
							<table id="trContract4" style="DISPLAY: none" cellSpacing="0" cellPadding="0" width="100%"
								border="0" runat="server">
								<tr>
									<td class="intopic" width="200">��ͬ����ƻ�</td>
									<td></td>
								</tr>
							</table>
							<TABLE id="trContract5" style="DISPLAY: none" cellSpacing="0" cellPadding="0" width="100%"
								align="center" border="0" runat="server">
								<TR>
									<TD vAlign="top"><asp:datagrid id="dgContractPaymentPlan" runat="server" CellPadding="0" AllowSorting="True" GridLines="Horizontal"
											AutoGenerateColumns="False" PageSize="15" ShowFooter="True" Width="100%" CssClass="list">
											<AlternatingItemStyle CssClass="AlterGridTr"></AlternatingItemStyle>
											<HeaderStyle CssClass="list-title"></HeaderStyle>
											<FooterStyle CssClass="list-title"></FooterStyle>
											<Columns>
												<asp:TemplateColumn HeaderText="���">
													<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<%# Container.ItemIndex + 1 %>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="PlanStep" HeaderText="����ƻ�����" FooterText="�ϼ�"></asp:BoundColumn>
												<asp:BoundColumn DataField="PlanningPayDate" HeaderText="����ʱ��" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn>
												<asp:BoundColumn DataField="PlanningPayCondition" HeaderText="��������"></asp:BoundColumn>
												<asp:BoundColumn DataField="Money" HeaderText="�� ��" DataFormatString="{0:N}">
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
												</asp:BoundColumn>
											</Columns>
											<PagerStyle NextPageText="��ҳ&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;��ҳ" HorizontalAlign="Right"
												CssClass="list-title"></PagerStyle>
										</asp:datagrid></TD>
								</TR>
							</TABLE>
							<br>
							<table cellSpacing="0" cellPadding="0" border="0">
								<tr>
									<td class="intopic" width="200">������ϸ</td>
								</tr>
							</table>
							<TABLE cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
								<TR>
									<TD vAlign="top"><asp:datagrid id="dgPayoutItem" runat="server" DataKeyField="PayoutItemCode" CellPadding="0" AllowSorting="True"
											GridLines="Both" AutoGenerateColumns="False" PageSize="15" ShowFooter="True" Width="100%" CssClass="List">
											<HeaderStyle CssClass="list-title"></HeaderStyle>
											<FooterStyle CssClass="list-title"></FooterStyle>
											<Columns>
												<asp:TemplateColumn HeaderText="���">
													<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<%# Container.ItemIndex + 1 %>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="��������" FooterText="�ϼ�">
													<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%# DataBinder.Eval(Container, "DataItem.PayoutDate", "{0:yyyy-MM-dd}") %>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Center"></FooterStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="��λ����">
													<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%# RmsPM.BLL.CostBudgetRule.GetPBSName(RmsPM.BLL.ConvertRule.ToString(DataBinder.Eval(Container, "DataItem.CostBudgetSetCode"))) %>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="��������">
													<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%# DataBinder.Eval(Container, "DataItem.Summary") %>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Center"></FooterStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="������">
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<%# DataBinder.Eval(Container, "DataItem.PayoutCash", "{0:N}") %>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="������">
													<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<a style="cursor:hand" onclick="javascript:ViewCostCode(this.code);" code='<%#  DataBinder.Eval(Container.DataItem, "CostCode") %>'>
															<%# RmsPM.BLL.CBSRule.GetCostName(RmsPM.BLL.ConvertRule.ToString(DataBinder.Eval(Container, "DataItem.CostCode")))%>
														</a>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="��Ŀ">
													<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
													<ItemStyle Wrap="True"></ItemStyle>
													<ItemTemplate>
														<%# DataBinder.Eval(Container, "DataItem.SubjectName") %>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="״̬">
													<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%# DataBinder.Eval(Container, "DataItem.StatusName") %>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="�����">
													<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<a style="cursor:hand" onclick="javascript:GotoPayout(this.code);" code='<%#  DataBinder.Eval(Container.DataItem, "PayoutCode") %>'>
															<%# DataBinder.Eval(Container, "DataItem.PayoutID") %>
														</a>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle NextPageText="��ҳ&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;��ҳ" HorizontalAlign="Center"
												CssClass="ListHeadTr"></PagerStyle>
										</asp:datagrid></TD>
								</TR>
							</TABLE>
							<br>
							<table id="tbOpiniont" visible="false" cellSpacing="0" cellPadding="0" width="100%" border="0"
								runat="server">
								<tr>
									<td class="intopic" width="200">������</td>
									<td></td>
								</tr>
							</table>
							<TABLE id="tbOpinionv" visible="false" cellSpacing="0" cellPadding="0" width="100%" align="center"
								border="0" runat="server" class="List">
								<TR>
									<TD vAlign="top"><asp:label id="lblOpinion" runat="server"></asp:label></TD>
								</TR>
							</TABLE>
	                        <table cellSpacing="0" cellPadding="0" width="100%" border="0">
							    <tr>
								    <td class="intopic" width="200">�������</td>
								</tr>
							</table>
							<uc1:WorkFlowList id="ucWorkFlowList" runat="server"></uc1:WorkFlowList>							
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
			<input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server"><input id="txtFromUrl" type="hidden" name="txtFromUrl" runat="server">
			<INPUT id="txtPaymentCode" type="hidden" name="txtPaymentCode" runat="server"> <INPUT id="txtContractCode" type="hidden" name="txtContractCode" runat="server">
			<INPUT id="txtIsContract" type="hidden" name="txtContractCode" runat="server"> <INPUT id="txtStatus" type="hidden" name="txtStatus" runat="server">
			<INPUT id="txtSubjectSetCode" type="hidden" name="txtSubjectSetCode" runat="server">
		</form>
		<script language="javascript">
<!--

//����
function GoBack()
{
	if (Form1.txtFromUrl.value == "")
	{
		window.history.go(-1);
//		window.location.href = "PaymentList.aspx?ProjectCode=" + Form1.txtProjectCode.value;
	}
	else
	{
		window.history.go(-1);
//		window.location.href = Form1.txtFromUrl.value;
	}
}

//�޸�
function Modify()
{
	var paymentCode = Form1.txtPaymentCode.value;
	
	OpenCustomWindow("../Finance/PaymentDetailModify.aspx?PaymentCode=" + paymentCode, "���޸�", 780, 560);
}

//�޸�
function AuditedModify()
{
	var paymentCode = Form1.txtPaymentCode.value;
	
	OpenCustomWindow("../Finance/PaymentDetailModify.aspx?Act=AuditedModify&PaymentCode=" + paymentCode, "���޸�", 780, 560);
}
	
//���
function DoCheck()
{
	var paymentCode = Form1.txtPaymentCode.value;
	var projectCode = Form1.txtProjectCode.value;
	OpenFullWindow('<%=ViewState["_AuditingURL"]%>?ProjectCode=' + projectCode + '&PaymentCode=' + paymentCode + "&ApplicationCode1=" + paymentCode,'�����_' + paymentCode);
//	OpenCustomWindow('../Finance/PaymentCheck.aspx?PaymentCode=' + paymentCode,"�����", 600, 400);
}
//���±�����˵�
function DoSubmitAccount()
{
	var paymentCode = Form1.txtPaymentCode.value;
	var projectCode = Form1.txtProjectCode.value;
	OpenFullWindow('<%=ViewState["_AuditingAccountURL"]%>?ProjectCode=' + projectCode + '&PaymentCode=' + paymentCode + "&ApplicationCode1=" + paymentCode,'�����_' + paymentCode);
}

function DoCheckPaymentCheck()
{
    var paymentCode = Form1.txtPaymentCode.value;
	var projectCode = Form1.txtProjectCode.value;
	OpenFullWindow('<%=ViewState["_CheckAuditingURL"]%>?ProjectCode=' + projectCode + '&PaymentCode=' + paymentCode + "&ApplicationCode1=" + paymentCode,'�����_' + paymentCode);
}

//ԭ���
function DoOldCheck()
{
	var paymentCode = Form1.txtPaymentCode.value;
	var projectCode = Form1.txtProjectCode.value;
	OpenCustomWindow('../Finance/PaymentCheck.aspx?PaymentCode=' + paymentCode,"�����", 600, 400);
}

//����
function Payout()
{
	var paymentCode = Form1.txtPaymentCode.value;
	
	var type =  '<%=ViewState["_TypeSortID"] %>';
	
	OpenCustomWindow("../Finance/PayoutDetailModify.aspx?PaymentCode=" + paymentCode + "&ProjectCode=" + Form1.txtProjectCode.value + "&Type=" + type, "����޸�", 780, 560);
}

//����
function Account()
{
	if (!confirm("����δ���壬ȷʵҪ������"))
		return false;
		
	return true;
}

//��ͬ��Ϣ	
function ViewContractInfo()
{
	OpenFullWindow("../Contract/ContractInfo.aspx?ProjectCode=" + Form1.txtProjectCode.value + "&ContractCode=" + Form1.txtContractCode.value,'��ͬ��Ϣ');
}

//��������Ϣ
function ViewCostCode(code)
{
	OpenFullWindow('../Cost/DynamicCostInfo.aspx?ProjectCode=' + Form1.txtProjectCode.value + '&CostCode=' + code ,'��̬��������Ϣ');
}

//�鿴���
function GotoPayout(PayoutCode)
{
	OpenCustomWindow("../Finance/PayoutInfo.aspx?PayoutCode=" + PayoutCode + "&Open=1", "���", 760, 540);
}

function DoPrint()
{
	var paymentCode = Form1.txtPaymentCode.value;
	var projectCode = Form1.txtProjectCode.value;
	
	var PrintUrl = '<%=ViewState["_PrintURL"] %>';
	
	OpenFullWindow( PrintUrl ,'��ӡԤ��');	
//	OpenFullWindow('../Finance/PaymentCheckPrint.aspx?ProjectCode=' + projectCode + '&PaymentCode=' + paymentCode,'��ӡԤ��');
}

if (window.opener)
{
//	Form1.btnClose.style.display = "";
//	Form1.btnGoBack.style.display = "none";
}
	
//-->
        </script>
	</body>
</HTML>
