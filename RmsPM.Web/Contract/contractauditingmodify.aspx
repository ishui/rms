<%@ Reference Control="~/usercontrols/inputcostbudgetdtl.ascx" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Register TagPrefix="uc1" TagName="InputCostBudgetDtl" Src="../UserControls/InputCostBudgetDtl.ascx" %>
<%@ Page  language="c#" Inherits="RmsPM.Web.Contract.ContractAuditingModify" CodeFile="ContractAuditingModify.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>合同审核</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
	</HEAD>
	<body scroll="no">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" bgColor="#ffffff" border="0">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">合同审核</td>
				</tr>
				<tr>
					<td vAlign="top" align="center">
						<table class="form" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD class="form-item" width="20%">合同名称：</TD>
								<TD><asp:label id="LabelContractName" runat="server"></asp:label></TD>
								<TD class="form-item" width="20%">合同编号：</TD>
								<TD><span runat="server" id="divContractIDEdit"><input class="input" id="txtContractID" type="text" name="txtContractID" runat="server"><font color="red">*</font></span>
								    <span runat="server" id="divContractIDView" visible="false"><asp:Label runat="server" ID="lblContractID"></asp:Label><asp:Label runat="server" ID="lblContractIDAutoCreate" ForeColor="red" Visible="false">审核后自动生成</asp:Label></span>
									&nbsp;&nbsp;<asp:label id="LabelContractStatus" runat="server" ForeColor="red"></asp:label>
								</TD>
							</TR>
							<TR>
								<TD class="form-item">经 办 人：</TD>
								<TD><input class="input" id="txtContractPersonName" readOnly type="text" name="txtContractPersonName"
										runat="server"> <input id="txtContractPerson" readOnly type="hidden" name="txtContractPerson" runat="server">
									<A href="#" onclick="SelectContractPerson();return false;"><IMG src="../images/ToolsItemSearch.gif" border="0"></A>
									<font color="red">*</font>
								</TD>
								<TD class="form-item">生效时间：</TD>
								<TD><cc3:calendar id="ContractDate" runat="server" CalendarResource="../Images/CalendarResource/"
										ReadOnly="False" Display="True"></cc3:calendar><font color="red">*</font></TD>
							</TR>
							<TR id="trModifyCheckOpinion" runat="server">
								<TD class="form-item">审核意见：</TD>
								<TD colSpan="3"><asp:textbox id="TextBoxCheckOpinion" runat="server" Height="72px" Columns="5" Width="90%" TextMode="MultiLine"
										CssClass="textarea"></asp:textbox></TD>
							</TR>
							<TR id="trViewCheckOpinion" runat="server">
								<TD class="form-item">审核意见：</TD>
								<TD colSpan="3"><asp:label id="lblCheckOpinion" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="form-item">审 核 人：</TD>
								<TD><asp:label id="lblCheckPersonName" runat="server"></asp:label></TD>
								<TD class="form-item">审核时间：</TD>
								<TD><asp:label id="lblCheckOpinionDate" runat="server"></asp:label></TD>
							</TR>
						</table>
					</td>
				</tr>
				<tr height="100%">
					<td>
						<div style="OVERFLOW: auto; WIDTH: 100%; POSITION: absolute; HEIGHT: 100%">
							<table id="tableList" cellSpacing="0" cellPadding="0" width="100%" runat="server">
								<tr>
									<td align="center">
										<table cellSpacing="0" cellPadding="0" width="100%" border="0">
											<tr>
												<td class="intopic" width="200">合同款项明细</td>
												<td></td>
											</tr>
										</table>
										<asp:datagrid id="dgCostList" onkeydown="if(event.keyCode==13) event.keyCode=9" runat="server"
											Width="100%" CssClass="list" PageSize="15" AutoGenerateColumns="False" AllowSorting="True"
											ShowFooter="True">
											<AlternatingItemStyle CssClass="list-i"></AlternatingItemStyle>
											<HeaderStyle CssClass="list-title"></HeaderStyle>
											<FooterStyle CssClass="list-title"></FooterStyle>
											<Columns>
												<asp:BoundColumn Visible="False" DataField="ContractCostCode"></asp:BoundColumn>
												<asp:TemplateColumn HeaderText="序号" ItemStyle-Width="50">
													<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<%# Container.ItemIndex + 1 %>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn Visible="False">
													<ItemTemplate>
														<uc1:InputCostBudgetDtl id="ucCostBudgetDtl" runat="server"  CostBudgetSetCode='<%#DataBinder.Eval(Container, "DataItem.CostBudgetSetCode")%>' CostCode='<%#DataBinder.Eval(Container, "DataItem.CostCode")%>'></uc1:InputCostBudgetDtl>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="费用项">
													<ItemTemplate>
														<asp:Label ID="lblCostName" Runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="单位工程">
													<ItemTemplate>
														<asp:Label ID="lblPBSName" Runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>												
												<asp:TemplateColumn HeaderText="金 额" ItemStyle-Width="100">
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
													<ItemTemplate>
														<asp:Label id="lblMoney" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Money","{0:N}") %>'>
														</asp:Label>
													</ItemTemplate>
													<FooterTemplate>
														<asp:Label id="lblSumMoney" runat="server"></asp:Label>
													</FooterTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="Description" HeaderText="说明" HeaderStyle-Wrap="False" ItemStyle-Wrap="False"></asp:BoundColumn>
											</Columns>
											<PagerStyle NextPageText="下页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上页" HorizontalAlign="Right"
												CssClass="ListHeadTr"></PagerStyle>
										</asp:datagrid></td>
								</tr>
							</table>
						</div>
					</td>
				</tr>
				<tr>
					<td>
						<table cellSpacing="0" cellPadding="9" width="100%">
							<tr>
								<td align="center"><input class="submit" id="btnSave" onclick="javascript:if(!window.confirm('确实通过吗 ？')) return false;"
										type="button" value="审 核" name="btnSave" runat="server" onserverclick="btnSave_ServerClick">&nbsp; <input class="submit" id="btnNoPass" onclick="javascript:if(!window.confirm('确实要作废吗 ？')) return false;"
										type="button" value="作 废" name="btnDelete" runat="server" onserverclick="btnNoPass_ServerClick"> <input class="submit" id="btnCancel" onclick="javascript:self.close()" type="button" value="取 消"
										name="btnCancel" runat="server"> <INPUT class="submit" id="btnClose" onclick="javascript:self.close()" type="button" value="关 闭"
										name="btnCancel" runat="server">
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
		<script language="javascript">
<!--
	function doViewDynamicCostInfo( costCode )
	{
		OpenFullWindow( "../Cost/DynamicCostInfo.aspx?ProjectCode=<%=Request["ProjectCode"]%>&CostCode=" + costCode,"动态费用信息"  );
	}
	
	function DoSelectUser(userCode,userName)
	{
		Form1.txtContractPerson.value = userCode;
		Form1.txtContractPersonName.value = userName;
	}

	function SelectContractPerson()
	{
		OpenMiddleWindow('../SelectBox/SelectPerson.aspx?Flag=0&Type=Single','选择用户');
	}	
	
//-->
		</script>
	</body>
</HTML>
