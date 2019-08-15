<%@ Reference Control="~/usercontrols/exchangeratecontrol.ascx" %>
<%@ Page language="c#" Inherits="RmsPM.Web.Finance.PaymentInfo" CodeFile="PaymentInfo.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="ExchangeRate" Src="../UserControls/ExchangeRateControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AttachMentList" Src="../UserControls/AttachMentList.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WorkFlowList" Src="../WorkFlowControl/WorkFlowList.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>请款单</title>
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
								<td class="topic" background="../images/topic_bg.gif"><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle"><span id="spanTitle" runat="server">项目管理>付款管理>请款管理>请款单信息</span></td>
								<td width="9"><IMG height="25" src="../images/topic_corr.gif" width="9"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td class="tools-area" vAlign="top"><IMG src="../images/btn_li.gif" align="absMiddle">
						<input class="button" id="btnModify" onclick="Modify();" type="button" value="修 改" name="btnModify" runat="server" /> 
						<input class="button" id="btnModifyEx" style="DISPLAY: none" onclick="AuditedModify();" type="button" value="审核后修改" name="btnModifyEx" runat="server" /> 
						<input class="button" id="btnDelete" onclick="javascript:if(!window.confirm('确实要删除这条记录吗？')) return false;" type="button" value="删 除" name="btnDelete" runat="server" onserverclick="btnDelete_ServerClick" /> 
							<input class="button" id="btnCheckDelete" value="审核后删除" onclick="javascript:if(!window.confirm('确实要删除这条记录吗？')) return false;" type="button" name="btnCheckDelete" onserverclick="btnCheckDelete_ServerClick" runat="server">
						<input class="button" id="btnCheck" onclick="javascript:DoCheck();" type="button" value="提交" name="btnCheck" runat="server" /> 
						
						<input class="button" id="btnSubmitAccount"  visible="false" onclick="javascript:DoSubmitAccount();" type="button" value="提交费用报销审批单" name="btnCheck" runat="server" /> 
						<input class="button" id="btnCheckPaymentCheck" onclick="javascript:DoCheckPaymentCheck();" type="button" value="支票请款单提交" name="BtCheckPaymentCheck" runat="server" /> 
						<input class="button" id="btnOldCheck" onclick="javascript:DoOldCheck();" type="button" value="审 核" name="btnOldCheck" runat="server" />
						<input class="button" id="btnPayout" onclick="javascript:Payout();" type="button" value="付 款" name="btnPayout" runat="server" /> 
						<input class="button" id="btnAccount" onclick="javascript:if (!Account()) return false;" type="button" value="付 讫" name="btnAccount" runat="server" onserverclick="btnAccount_ServerClick" />
						<input class="button" id="btnPrint" onclick="DoPrint();return false;" type="button" value="付款审批表打印"	runat="server" NAME="btnPrint" />
					    
						<input class="button" id="btnClose" style="DISPLAY: none" onclick="window.close();" type="button" value="关 闭" name="btnClose" />
					</td>
				</tr>
				<tr>
					<td class="table">
						<TABLE class="form" id="Table4" cellSpacing="0" cellPadding="0" width="100%">
							<TR>
								<TD class="form-item">请款单号：</TD>
								<TD><asp:label id="lblPaymentID" runat="server"></asp:label></TD>
								<TD class="form-item">请款类型：</TD>
								<TD><asp:label id="lblIsContractName" Runat="server"></asp:label></TD>
								<TD class="form-item">状 态：</TD>
								<TD><asp:label id="lblStatusName" Runat="server"></asp:label></TD>
								<TD class="form-item">请款总额：<span id="spanOldMoney2" style="DISPLAY: none" runat="server"><br>
										原请款总额：</span></TD>
								<TD><asp:label id="lblMoney" runat="server"></asp:label><span id="spanOldMoney" style="DISPLAY: none" runat="server"><br>
										<asp:label id="lblOldMoney" Runat="server"></asp:label></span></TD>
							</TR>
							<tr id="TrPayment" runat="server">
			                    <td class="form-item" width="10%">请款名称：</td>
				                <td colspan="7">
				                <asp:label id="lblPaymentName" Width="45%" Runat="server"></asp:label>&nbsp;
				                </td>
							</tr>
							<TR>
								<TD class="form-item">受款单位：</TD>
								<TD colSpan="3"><asp:label id="lblSupplyName" runat="server"></asp:label></TD>
								<TD class="form-item">受 款 人：</TD>
								<TD><asp:label id="lblPayer" runat="server"></asp:label></TD>
                                <TD class="form-item">相关业务：</TD>
								<TD><asp:label id="lblPaymentTitle" runat="server"></asp:label></TD>								
							</TR>
							<tr>
								<TD class="form-item">请款部门：</TD>
								<TD colSpan="3"><asp:label id="lblUnitName" runat="server"></asp:label></TD>
								<TD class="form-item">最后付款日：</TD>
								<TD><asp:label id="lblPayDate" runat="server"></asp:label></TD>
								<TD class="form-item">请款类型：</TD>
								<TD><asp:label id="lblGroupName" runat="server"></asp:label></TD>
							</tr>
							<TR id="trContract" runat="server">
								<TD class="form-item">合同名称：</TD>
								<TD colSpan="3"><A href="#" onclick="ViewContractInfo();return false;"><asp:label id="lblContractName" runat="server"></asp:label></A></TD>
								<TD class="form-item">合同编号：</TD>
								<TD colSpan="3"><asp:label id="lblContractID" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="form-item">申 请 人：</TD>
								<TD><asp:label id="lblApplyPersonName" runat="server"></asp:label></TD>
								<TD class="form-item">申请日期：</TD>
								<TD><asp:label id="lblApplyDate" runat="server"></asp:label></TD>
								<TD class="form-item">审 核 人：</TD>
								<TD><asp:label id="lblCheckPersonName" runat="server"></asp:label></TD>
								<TD class="form-item">审核日期：</TD>
								<TD><asp:label id="lblCheckDate" runat="server"></asp:label></TD>
							</TR>
							<TR >
								<TD class="form-item">开户银行：</TD>
								<TD colSpan="3"><asp:label id="lblBankName" runat="server"></asp:label></TD>
								<TD class="form-item">银行帐号：</TD>
								<TD colSpan="3"><asp:label id="lblBankAccount" runat="server"></asp:label></TD>
							</TR>							
							<TR>
								<TD class="form-item">备注：</TD>
								<TD colSpan="7"><asp:label id="lblRemark" runat="server"></asp:label></TD>
							</TR>
							<TR style="DISPLAY: none">
								<TD class="form-item">付款用途：</TD>
								<TD colSpan="5"><asp:label id="lblPurpose" runat="server"></asp:label></TD>
								<TD class="form-item">单据张数：</TD>
								<TD><asp:label id="lblRecieptCount" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="form-item">上传附件：</TD>
								<TD colspan="7"><uc1:attachmentlist id="myAttachMentList" runat="server"></uc1:attachmentlist></TD>
							</TR>
							<TR id="trCostBatchPayment" runat="server" style="display:none">
								<TD class="form-item">预算表：</TD>
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
									<td class="intopic" width="200">请款明细</td>
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
												<asp:TemplateColumn HeaderText="序号">
													<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<%# Container.ItemIndex + 1 %>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="单位工程">
													<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%# RmsPM.BLL.CostBudgetRule.GetPBSName(RmsPM.BLL.ConvertRule.ToString(DataBinder.Eval(Container, "DataItem.CostBudgetSetCode"))) %>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="款项名称" FooterText="合计">
													<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%# DataBinder.Eval(Container, "DataItem.Summary") %>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Center"></FooterStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="请款金额">
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
								                        <asp:Label ID="lblPaymentMoneyType" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MoneyType") %>'></asp:Label>：&nbsp;&nbsp;
								                        <asp:Label ID="lblPaymentCash" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ItemCash","{0:N}") %>'></asp:Label>
												    </ItemTemplate>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="请款金额(元)" Visible="False">
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<%# DataBinder.Eval(Container, "DataItem.ItemMoney", "{0:N}") %>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="原请款金额(元)" Visible="False">
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<%# DataBinder.Eval(Container, "DataItem.OldItemMoney", "{0:N}") %>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="费用项">
													<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<a style="cursor:hand" onclick="javascript:ViewCostCode(this.code);" code='<%#  DataBinder.Eval(Container.DataItem, "CostCode") %>'>
															<%# DataBinder.Eval(Container, "DataItem.CostName") %>
														</a>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="分摊" Visible="False">
													<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%# DataBinder.Eval(Container, "DataItem.BuildingNameAll") %>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="已付金额">
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<%# DataBinder.Eval(Container, "DataItem.TotalPayoutCash", "{0:N}") %>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="未付金额">
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<%# DataBinder.Eval(Container, "DataItem.RemainItemCash", "{0:N}") %>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
												</asp:TemplateColumn>
											
												<asp:TemplateColumn HeaderText="已付金额(元)" Visible="false">
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<%# DataBinder.Eval(Container, "DataItem.TotalPayoutMoney", "{0:N}") %>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="未付金额(元)" Visible="false">
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<%# DataBinder.Eval(Container, "DataItem.RemainItemMoney", "{0:N}") %>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle NextPageText="下页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上页" HorizontalAlign="Center"
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
												<asp:TemplateField HeaderText="序号">
													<HeaderStyle Wrap="False" HorizontalAlign="Center" />
													<ItemStyle HorizontalAlign="Center" Wrap="False" />
													<ItemTemplate>
														<%# Container.DataItemIndex + 1 %>
													</ItemTemplate>
												</asp:TemplateField>
												<asp:TemplateField HeaderText="单位工程">
													<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%# RmsPM.BLL.CostBudgetRule.GetPBSName(RmsPM.BLL.ConvertRule.ToString(DataBinder.Eval(Container, "DataItem.CostBudgetSetCode"))) %>
													</ItemTemplate>
												</asp:TemplateField>
												<asp:BoundField HeaderText="款项名称" FooterText="合计" DataField="Summary" />
												<asp:TemplateField HeaderText="款项名称">
												    <ItemTemplate>
														<uc1:ExchangeRate id="ucExchangeRate" runat="server" />
													</ItemTemplate>
												</asp:TemplateField>
												<asp:TemplateField  HeaderText="预付款">
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
												<asp:TemplateField  HeaderText="验收款">
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
												<asp:TemplateField  HeaderText="保修款">
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
												<asp:TemplateField  HeaderText="结算扣款">
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
												<asp:TemplateField  HeaderText="扣款保留">
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
												<asp:TemplateField  HeaderText="其他扣款">
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
									<td class="intopic" width="200">合同费用明细</td>
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
												<asp:TemplateColumn HeaderText="序号">
													<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<%# Container.ItemIndex + 1 %>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="款项名称" FooterText="合计">
													<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%# DataBinder.Eval(Container, "DataItem.ItemName") %>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Center"></FooterStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="费用项">
													<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%# RmsPM.BLL.CBSRule.GetCostName(RmsPM.BLL.ConvertRule.ToString(DataBinder.Eval(Container, "DataItem.CostCode")))%>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="合同金额(元)">
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<%# DataBinder.Eval(Container, "DataItem.Money", "{0:N}") %>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="已请款金额(元)">
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<%# DataBinder.Eval(Container, "DataItem.TotalPaymentMoney", "{0:N}") %>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle NextPageText="下页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上页" HorizontalAlign="Center"
												CssClass="ListHeadTr"></PagerStyle>
										</asp:datagrid></TD>
								</TR>
							</TABLE>
							<table id="trContract4" style="DISPLAY: none" cellSpacing="0" cellPadding="0" width="100%"
								border="0" runat="server">
								<tr>
									<td class="intopic" width="200">合同付款计划</td>
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
												<asp:TemplateColumn HeaderText="序号">
													<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<%# Container.ItemIndex + 1 %>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="PlanStep" HeaderText="付款计划步骤" FooterText="合计"></asp:BoundColumn>
												<asp:BoundColumn DataField="PlanningPayDate" HeaderText="付款时间" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn>
												<asp:BoundColumn DataField="PlanningPayCondition" HeaderText="付款条件"></asp:BoundColumn>
												<asp:BoundColumn DataField="Money" HeaderText="金 额" DataFormatString="{0:N}">
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
												</asp:BoundColumn>
											</Columns>
											<PagerStyle NextPageText="下页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上页" HorizontalAlign="Right"
												CssClass="list-title"></PagerStyle>
										</asp:datagrid></TD>
								</TR>
							</TABLE>
							<br>
							<table cellSpacing="0" cellPadding="0" border="0">
								<tr>
									<td class="intopic" width="200">付款明细</td>
								</tr>
							</table>
							<TABLE cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
								<TR>
									<TD vAlign="top"><asp:datagrid id="dgPayoutItem" runat="server" DataKeyField="PayoutItemCode" CellPadding="0" AllowSorting="True"
											GridLines="Both" AutoGenerateColumns="False" PageSize="15" ShowFooter="True" Width="100%" CssClass="List">
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
												<asp:TemplateColumn HeaderText="付款日期" FooterText="合计">
													<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%# DataBinder.Eval(Container, "DataItem.PayoutDate", "{0:yyyy-MM-dd}") %>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Center"></FooterStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="单位工程">
													<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%# RmsPM.BLL.CostBudgetRule.GetPBSName(RmsPM.BLL.ConvertRule.ToString(DataBinder.Eval(Container, "DataItem.CostBudgetSetCode"))) %>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="款项名称">
													<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%# DataBinder.Eval(Container, "DataItem.Summary") %>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Center"></FooterStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="付款金额">
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<%# DataBinder.Eval(Container, "DataItem.PayoutCash", "{0:N}") %>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="费用项">
													<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<a style="cursor:hand" onclick="javascript:ViewCostCode(this.code);" code='<%#  DataBinder.Eval(Container.DataItem, "CostCode") %>'>
															<%# RmsPM.BLL.CBSRule.GetCostName(RmsPM.BLL.ConvertRule.ToString(DataBinder.Eval(Container, "DataItem.CostCode")))%>
														</a>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="科目">
													<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
													<ItemStyle Wrap="True"></ItemStyle>
													<ItemTemplate>
														<%# DataBinder.Eval(Container, "DataItem.SubjectName") %>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="状态">
													<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%# DataBinder.Eval(Container, "DataItem.StatusName") %>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="付款单号">
													<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<a style="cursor:hand" onclick="javascript:GotoPayout(this.code);" code='<%#  DataBinder.Eval(Container.DataItem, "PayoutCode") %>'>
															<%# DataBinder.Eval(Container, "DataItem.PayoutID") %>
														</a>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle NextPageText="下页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上页" HorizontalAlign="Center"
												CssClass="ListHeadTr"></PagerStyle>
										</asp:datagrid></TD>
								</TR>
							</TABLE>
							<br>
							<table id="tbOpiniont" visible="false" cellSpacing="0" cellPadding="0" width="100%" border="0"
								runat="server">
								<tr>
									<td class="intopic" width="200">审核意见</td>
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
								    <td class="intopic" width="200">相关流程</td>
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

//返回
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

//修改
function Modify()
{
	var paymentCode = Form1.txtPaymentCode.value;
	
	OpenCustomWindow("../Finance/PaymentDetailModify.aspx?PaymentCode=" + paymentCode, "请款单修改", 780, 560);
}

//修改
function AuditedModify()
{
	var paymentCode = Form1.txtPaymentCode.value;
	
	OpenCustomWindow("../Finance/PaymentDetailModify.aspx?Act=AuditedModify&PaymentCode=" + paymentCode, "请款单修改", 780, 560);
}
	
//审核
function DoCheck()
{
	var paymentCode = Form1.txtPaymentCode.value;
	var projectCode = Form1.txtProjectCode.value;
	OpenFullWindow('<%=ViewState["_AuditingURL"]%>?ProjectCode=' + projectCode + '&PaymentCode=' + paymentCode + "&ApplicationCode1=" + paymentCode,'请款单审核_' + paymentCode);
//	OpenCustomWindow('../Finance/PaymentCheck.aspx?PaymentCode=' + paymentCode,"请款单审核", 600, 400);
}
//益新报销审核单
function DoSubmitAccount()
{
	var paymentCode = Form1.txtPaymentCode.value;
	var projectCode = Form1.txtProjectCode.value;
	OpenFullWindow('<%=ViewState["_AuditingAccountURL"]%>?ProjectCode=' + projectCode + '&PaymentCode=' + paymentCode + "&ApplicationCode1=" + paymentCode,'请款单审核_' + paymentCode);
}

function DoCheckPaymentCheck()
{
    var paymentCode = Form1.txtPaymentCode.value;
	var projectCode = Form1.txtProjectCode.value;
	OpenFullWindow('<%=ViewState["_CheckAuditingURL"]%>?ProjectCode=' + projectCode + '&PaymentCode=' + paymentCode + "&ApplicationCode1=" + paymentCode,'请款单审核_' + paymentCode);
}

//原审核
function DoOldCheck()
{
	var paymentCode = Form1.txtPaymentCode.value;
	var projectCode = Form1.txtProjectCode.value;
	OpenCustomWindow('../Finance/PaymentCheck.aspx?PaymentCode=' + paymentCode,"请款单审核", 600, 400);
}

//付款
function Payout()
{
	var paymentCode = Form1.txtPaymentCode.value;
	
	var type =  '<%=ViewState["_TypeSortID"] %>';
	
	OpenCustomWindow("../Finance/PayoutDetailModify.aspx?PaymentCode=" + paymentCode + "&ProjectCode=" + Form1.txtProjectCode.value + "&Type=" + type, "付款单修改", 780, 560);
}

//结算
function Account()
{
	if (!confirm("请款单尚未付清，确实要结算吗？"))
		return false;
		
	return true;
}

//合同信息	
function ViewContractInfo()
{
	OpenFullWindow("../Contract/ContractInfo.aspx?ProjectCode=" + Form1.txtProjectCode.value + "&ContractCode=" + Form1.txtContractCode.value,'合同信息');
}

//费用项信息
function ViewCostCode(code)
{
	OpenFullWindow('../Cost/DynamicCostInfo.aspx?ProjectCode=' + Form1.txtProjectCode.value + '&CostCode=' + code ,'动态费用项信息');
}

//查看付款单
function GotoPayout(PayoutCode)
{
	OpenCustomWindow("../Finance/PayoutInfo.aspx?PayoutCode=" + PayoutCode + "&Open=1", "付款单", 760, 540);
}

function DoPrint()
{
	var paymentCode = Form1.txtPaymentCode.value;
	var projectCode = Form1.txtProjectCode.value;
	
	var PrintUrl = '<%=ViewState["_PrintURL"] %>';
	
	OpenFullWindow( PrintUrl ,'打印预览');	
//	OpenFullWindow('../Finance/PaymentCheckPrint.aspx?ProjectCode=' + projectCode + '&PaymentCode=' + paymentCode,'打印预览');
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
