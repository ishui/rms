<%@ Register TagPrefix="uc1" TagName="ExchangeRate" Src="../UserControls/ExchangeRateControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="InputSubject" Src="../UserControls/InputSubject.ascx" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Page language="c#" Inherits="RmsPM.Web.Finance.PayoutCheckInput" CodeFile="PayoutCheckInput.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>付款单审核</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0" bgcolor="#ffffff">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">付款单审核</td>
				</tr>
				<tr>
					<td class="topic" vAlign="top" align="center">
						<table class="form" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="form-item">付款单号：</TD>
								<td><asp:label id="lblPayoutID" runat="server"></asp:label>
								</TD>
								<td class="form-item">付款总额：</TD>
								<td><asp:label id="lblMoney" runat="server"></asp:label></TD>
							</tr>
							<tr>
							    <td class="form-item">贷方科目：</td>
							    <td colspan="3"><uc1:InputSubject id="ucInputSubject" runat="server"></uc1:InputSubject><font color="red">*</font></td>
							</tr>
				        </table>
				    </td>
				</tr>
				<tr height="100%">
					<td vAlign="top">
						<div style="OVERFLOW: auto; WIDTH: 100%; POSITION: absolute; HEIGHT: 100%">
							<TABLE height="100%" cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
								<TR>
									<TD vAlign="top"><asp:datagrid id="dgList" runat="server" DataKeyField="PayoutItemCode" CellPadding="0" AllowSorting="True"
											GridLines="Both" AutoGenerateColumns="False" PageSize="15" ShowFooter="True" Width="100%" CssClass="list" OnItemCreated="dgList_ItemCreated" OnItemDataBound="dgList_ItemDataBound">
											<HeaderStyle CssClass="list-title"></HeaderStyle>
											<FooterStyle CssClass="list-title"></FooterStyle>
											<Columns>
												<asp:TemplateColumn HeaderText="序号">
													<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<%# Container.ItemIndex + 1 %>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="单位工程">
													<HeaderStyle HorizontalAlign="Left" Wrap="false"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%# RmsPM.BLL.CostBudgetRule.GetPBSName(RmsPM.BLL.ConvertRule.ToString(DataBinder.Eval(Container, "DataItem.CostBudgetSetCode"))) %>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="款项" FooterText="合计">
													<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
										                <%# DataBinder.Eval(Container, "DataItem.Summary") %>
										                <input type="hidden" id="txtPayoutItemCode" runat="server" value='<%# DataBinder.Eval(Container, "DataItem.PayoutItemCode") %>' />
										                <input type="hidden" id="txtPaymentItemCode" runat="server" value='<%# DataBinder.Eval(Container, "DataItem.PaymentItemCode") %>' />
										                <input type="hidden" id="txtSummary" runat="server" value='<%# DataBinder.Eval(Container, "DataItem.Summary") %>' />
										                <input type="hidden" id="txtTotalPayoutCash" runat="server" value='<%# DataBinder.Eval(Container, "DataItem.TotalPayoutCash") %>' />
										                <input type="hidden" id="txtRemainItemCash" runat="server" value='<%# DataBinder.Eval(Container, "DataItem.RemainItemCash") %>' />
                                                        <input type="hidden" id="txtItemMoney" runat="server" value='<%# DataBinder.Eval(Container, "DataItem.ItemMoney") %>' />
                                                        <input type="hidden" id="txtPaymentMoneyType" runat="server" value='<%# DataBinder.Eval(Container, "DataItem.PaymentMoneyType") %>' />
                                                        <input type="hidden" id="txtPaymentExchangeRate" runat="server" value='<%# DataBinder.Eval(Container, "DataItem.PaymentExchangeRate") %>' />
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Center"></FooterStyle>
												</asp:TemplateColumn>
								                <asp:TemplateColumn HeaderText="请款金额" ItemStyle-Wrap="false">
								                    <ItemTemplate>
								                        <asp:Label ID="lblPaymentMoneyType" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PaymentMoneyType") %>'></asp:Label>：&nbsp;&nbsp;
								                        <asp:Label ID="lblPaymentCash" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ItemCash","{0:N}") %>'></asp:Label>
								                    </ItemTemplate>
								                </asp:TemplateColumn>
								                <asp:BoundColumn HeaderText="已付金额<br />（原币种）" DataField="TotalPayoutCash" DataFormatString="{0:N}"  ItemStyle-Wrap="false" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="right"/>
								                <asp:BoundColumn HeaderText="未付金额<br />（原币种）" DataField="RemainItemCash" DataFormatString="{0:N}" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="right"/>
								                <asp:BoundColumn HeaderText="本次付款金额<br />（原币种）" DataField="PayoutCash" DataFormatString="{0:N}" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="right"/>
                                                <asp:TemplateColumn HeaderText="汇率"  ItemStyle-Wrap="false">
								                    <ItemTemplate>
								                        <asp:Label ID="lblPayoutExchangeRate" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ExchangeRate","{0:N}") %>'></asp:Label>
					        	                    </ItemTemplate>
								                </asp:TemplateColumn>
								                <asp:TemplateColumn HeaderText="本次支付金额<br>（RMB）"  ItemStyle-Wrap="false" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="right">
								                    <ItemTemplate>
								                        <asp:Label ID="lblPayoutItemMoney" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PayoutMoney","{0:N}") %>' ></asp:Label>
								                    </ItemTemplate>
								                </asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="科目" HeaderStyle-HorizontalAlign="Center" ItemStyle-Wrap="False">
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
													<ItemTemplate>
														<uc1:InputSubject id="ucInputSubject" runat="server" Value='<%#DataBinder.Eval(Container, "DataItem.SubjectCode")%>'>
														</uc1:InputSubject>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="费用项">
													<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<span id="divCostName" runat="server"><a href="#" onclick="ViewCostCode(this.code);return false;" code='<%#  DataBinder.Eval(Container.DataItem, "CostCode") %>'>
																<%#DataBinder.Eval(Container, "DataItem.CostName")%>
															</a></span>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="分摊到楼栋" Visible="False">
													<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<span id="divBuildingNameAll" runat="server">
															<%# DataBinder.Eval(Container, "DataItem.BuildingNameAll") %>
														</span>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="请款单号">
													<HeaderStyle HorizontalAlign="Center" Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%# DataBinder.Eval(Container, "DataItem.PaymentID") %>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle NextPageText="下页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上页" HorizontalAlign="Center"
												CssClass="ListHeadTr"></PagerStyle>
										</asp:datagrid></TD>
								</TR>
							</TABLE>
						</div>
					</td>
				</tr>
				<tr height="100%" style="display:none">
					<td class="topic" vAlign="top" align="center">
						<table class="form" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<TD class="form-item" width="100">审核意见：</TD>
								<TD><textarea runat="server" class="textarea" id="txtCheckOpinion" name="txtCheckOpinion" rows="5"
										style="WIDTH:100%"></textarea>
								</TD>
							</tr>
						</table>
					</td>
				</tr>
				<tr style="display:none">
					<td>
						<table cellpadding="0" width="100%">
							<tr>
								<TD class="note">确实要审核吗？</TD>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td>
						<table cellSpacing="10" width="100%">
							<tr>
								<td align="center"><input class="submit" id="btnSave" type="button" value="审 核" name="btnSave" runat="server" onclick="javascript:if(!window.confirm('是否审核通过？')) return false;"  onserverclick="btnSave_ServerClick">
									<input class="submit" id="btnCancel" onclick="javascript:self.close()" type="button" value="取 消"
										name="btnCancel">
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server"> <INPUT id="txtIsNew" type="hidden" name="txtIsNew" runat="server">
			<INPUT id="txtContractCode" type="hidden" name="txtContractCode" runat="server">
			<INPUT id="txtIsContract" type="hidden" name="txtCode" runat="server"> <INPUT id="txtPayoutCode" type="hidden" name="txtPayoutCode" runat="server">
			<INPUT id="txtSupplyCode" type="hidden" name="txtSupplyCode" runat="server">
			<INPUT id="txtSubjectSetCode" type="hidden" name="txtSubjectSetCode" runat="server">
		</form>
		<script language="javascript">
		</script>
	</body>
</HTML>
