<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SM_LocalPaymenAuditingV2.ascx.cs" Inherits="WorkFlowOperation_SM_LocalPaymenAuditingV2" %>
<%@ Register TagPrefix="uc1" TagName="AttachMentAdd" Src="../UserControls/AttachMentAdd.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AttachMentList" Src="../UserControls/AttachMentList.ascx" %>
<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics.WebUI.WebDataInput.v5.1, Version=5.1.20051.37, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>

<div id="OperableDiv" onkeydown="if(event.keyCode==13) event.keyCode=9" runat="server">
	<table cellSpacing="0" cellPadding="0" width="100%" border="0">
		<tr>
			<td class="blackbordertd" align="right" width="20%">项目名称：&nbsp;</td>
			<td class="blackbordertdpaddingcontent" width="40%"><asp:label id="lblOperProjectName" Runat="server"></asp:label>&nbsp;
			</td>
			<td class="blackbordertd" align="right" width="15%">付款期数：&nbsp;</td>
			<td class="blackbordertdpaddingcontent" width="25%">第&nbsp;<INPUT class="input" id="txtOperIssue" style="WIDTH: 50px; TEXT-ALIGN: right" type="text"
					name="txtOperIssue" runat="server"> &nbsp;期
			</td>
		</tr>
		<tr>
			<td class="blackbordertd" align="right">请款名称：&nbsp;</td>
			<td class="blackbordertdpaddingcontent" colspan="3"><asp:label id="lblPaymentName" Runat="server"></asp:label>&nbsp;
			</td>
			
		</tr>
		<tr>
			<td class="blackbordertd" align="right">合同名称：&nbsp;</td>
			<td class="blackbordertdpaddingcontent"><asp:label id="lblOperContractName" Runat="server"></asp:label>&nbsp;
			</td>
			<td class="blackbordertd" align="right">合同编号：&nbsp;</td>
			<td class="blackbordertdpaddingcontent"><asp:label id="lblOperContractID" Runat="server"></asp:label>&nbsp;
                <asp:HyperLink ID="HyperLink1" runat="server" Target="_blank">请款单</asp:HyperLink>
			</td>
		</tr>
		<tr>
			<td class="blackbordertd" align="right">承包人（分包）：&nbsp;</td>
			<td class="blackbordertdpaddingcontent" colSpan="3"><asp:label id="lblOperSupplierName" Runat="server"></asp:label>&nbsp;
			</td>
		</tr>
		<tr>
			<td class="blackbordertd" align="right">承包人（总包）：&nbsp;</td>
			<td class="blackbordertdpaddingcontent">
				<asp:label id="lblOperSupplier2Name" Runat="server"></asp:label>&nbsp;
			</td>
			<td class="blackbordertd" align="right">楼栋号：&nbsp;</td>
			<td class="blackbordertdpaddingcontent">
				<asp:label id="lblOperBuilding" Runat="server"></asp:label>&nbsp;
			</td>
		</tr>
		<tr>
			<td vAlign="top" colSpan="2" height="100%">
				<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<tr>
						<td class="blackbordertdpaddingcontent">合同履约情况：（合约部综合评述）<font color="red">*</font>
						</td>
					</tr>
					<tr>
						<td>
							<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
								<tr>
									<td class="blackbordertdpaddingcontent" style="HEIGHT: auto"><TEXTAREA id="txtOperCheckOpinion" style="WIDTH: 100%" name="txtOperCheckOpinion" rows="10"
											runat="server"></TEXTAREA>&nbsp;
									</td>
								</tr>
							</table>
						</td>
					</tr>
				</table>
			</td>
			<td vAlign="top" colSpan="2" height="100%">
				<table id="tabOperMoney" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0"
					runat="server">
					<tr>
						<td class="blackbordertd" width="40%">&nbsp;</td>
						<td class="blackbordertdpaddingcontent" width="60%">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;（ 
							RMB ）</td>
					</tr>
					<tr>
						<td class="blackbordertd" align="right">原合同金额：&nbsp;</td>
						<td class="blackbordertdpaddingcontent">
							<table cellSpacing="0" cellPadding="0" border="0">
								<tr>
									<td align="center" width="15">&nbsp;</td>
									<td><INPUT class="infra-input-nember" id="txtOperBudgetMoney" style="FLOAT: left; WIDTH: 120px; TEXT-ALIGN: right"
											readOnly type="text" name="txtOperBudgetMoney" runat="server">&nbsp;
									</td>
								</tr>
							</table>
						</td>
					</tr>
					<tr>
						<td class="blackbordertd" align="right">暂定金额/指定金额：&nbsp;</td>
						<td class="blackbordertdpaddingcontent">
							<table cellSpacing="0" cellPadding="0" border="0">
								<tr>
									<td align="center" width="15">-</td>
									<td><INPUT class="infra-input-nember" id="txtOperAdjustMoney" style="FLOAT: left; WIDTH: 120px; TEXT-ALIGN: right"
											readOnly type="text" name="txtOperAdjustMoney" runat="server">&nbsp;
									</td>
								</tr>
							</table>
						</td>
					</tr>
					<tr>
						<td class="blackbordertd" align="right">实际金额：&nbsp;</td>
						<td class="blackbordertdpaddingcontent">
							<table cellSpacing="0" cellPadding="0" border="0">
								<tr>
									<td align="center" width="15">=</td>
									<td><INPUT class="infra-input-nember" id="txtOperOriginalMoney" style="FLOAT: left; WIDTH: 120px; TEXT-ALIGN: right"
											readOnly type="text" name="txtOperOriginalMoney" runat="server">&nbsp;
									</td>
								</tr>
							</table>
						</td>
					</tr>
					<tr>
						<td class="blackbordertd" align="right">累计已批变更：&nbsp;</td>
						<td class="blackbordertdpaddingcontent">
							<table cellSpacing="0" cellPadding="0" border="0">
								<tr>
									<td align="center" width="15">+</td>
									<td><INPUT class="infra-input-nember" id="txtOperTotalChangeMoney" style="FLOAT: left; WIDTH: 120px; TEXT-ALIGN: right"
											readOnly type="text" name="txtOperTotalChangeMoney" runat="server">&nbsp;
									</td>
								</tr>
							</table>
						</td>
					</tr>
					<tr>
						<td class="blackbordertd" align="right">合同总额预计：&nbsp;</td>
						<td class="blackbordertdpaddingcontent">
							<table cellSpacing="0" cellPadding="0" border="0">
								<tr>
									<td align="center" width="15">=</td>
									<td><INPUT class="infra-input-nember" id="txtOperNewTotalMoney" style="FLOAT: left; WIDTH: 120px; TEXT-ALIGN: right"
											readOnly type="text" name="txtOperNewTotalMoney" runat="server">
									</td>
								</tr>
							</table>
						</td>
					</tr>
					<tr>
						<td class="blackbordertd" align="right">累计应付款：&nbsp;</td>
						<td class="blackbordertdpaddingcontent">
							<table cellSpacing="0" cellPadding="0" border="0">
								<tr>
									<td align="center" width="15">&nbsp;</td>
									<td><INPUT class="infra-input-nember" id="txtOperTotalPayMoney" style="FLOAT: left; WIDTH: 120px; TEXT-ALIGN: right"
											readOnly type="text" name="txtOperTotalPayMoney" runat="server">&nbsp;
									</td>
								</tr>
							</table>
						</td>
					</tr>
					<tr>
						<td class="blackbordertd" align="right">减累计已批款：&nbsp;</td>
						<td class="blackbordertdpaddingcontent">
							<table cellSpacing="0" cellPadding="0" border="0">
								<tr>
									<td align="center" width="15">+</td>
									<td><INPUT class="infra-input-nember" id="txtOperNegAHMoney" style="FLOAT: left; WIDTH: 120px; TEXT-ALIGN: right"
											readOnly type="text" name="txtOperNegAHMoney" runat="server">&nbsp;
									</td>
								</tr>
							</table>
						</td>
					</tr>
					<tr>
						<td bgColor="black" colSpan="2" height="1"></td>
					</tr>
					<tr>
						<td class="blackbordertd" align="right">本期应付款：&nbsp;</td>
						<td class="blackbordertdpaddingcontent">
							<table cellSpacing="0" cellPadding="0" border="0">
								<tr>
									<td align="center" width="15">=</td>
									<td><INPUT class="infra-input-nember" id="txtOperTotalItemMoney" style="FLOAT: left; WIDTH: 120px; TEXT-ALIGN: right"
											readOnly type="text" name="txtOperTotalItemMoney" runat="server">&nbsp;
									</td>
								</tr>
							</table>
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr id="trOperAttachment" runat="server">
			<td colSpan="4">
				<table cellSpacing="0" cellPadding="0" width="100%" border="0">
					<tr>
						<td class="blackbordertd" colSpan="2">附件清单：</td>
					</tr>
					<tr>
						<td class="blackbordertd" width="40%"><asp:checkbox id="chkOperAttachMent1" Runat="server" Text="1.承包商的付款申请："></asp:checkbox></td>
						<td class="blackbordertdpaddingcontent"><uc1:attachmentadd id="myOperAttachMentAdd1" runat="server"></uc1:attachmentadd>&nbsp;
						</td>
					</tr>
					<tr>
						<td class="blackbordertd"><asp:checkbox id="chkOperAttachMent2" Runat="server" Text="2.顾问估算师的付款证书："></asp:checkbox></td>
						<td class="blackbordertdpaddingcontent"><uc1:attachmentadd id="myOperAttachMentAdd2" runat="server"></uc1:attachmentadd>&nbsp;
						</td>
					</tr>
					<tr>
						<td class="blackbordertd"><asp:checkbox id="chkOperAttachMent3" Runat="server" Text="3.付款记录（累计表）："></asp:checkbox></td>
						<td class="blackbordertdpaddingcontent"><uc1:attachmentadd id="myOperAttachMentAdd3" runat="server"></uc1:attachmentadd>&nbsp;
						</td>
					</tr>
					<tr>
						<td class="blackbordertd"><asp:checkbox id="chkOperAttachMent4" Runat="server" Text="4.监理意见："></asp:checkbox></td>
						<td class="blackbordertdpaddingcontent"><uc1:attachmentadd id="myOperAttachMentAdd4" runat="server"></uc1:attachmentadd>&nbsp;
						</td>
					</tr>
					<tr>
						<td class="blackbordertd"><asp:checkbox id="chkOperAttachMent5" Runat="server" Text="5.工程部/建筑开发部之专业意见："></asp:checkbox></td>
						<td class="blackbordertdpaddingcontent"><uc1:attachmentadd id="myOperAttachMentAdd5" runat="server"></uc1:attachmentadd>&nbsp;
						</td>
					</tr>
					<tr>
						<td class="blackbordertd"><asp:checkbox id="chkOperAttachMent6" Runat="server" Text="6.发票："></asp:checkbox></td>
						<td class="blackbordertdpaddingcontent"><uc1:attachmentadd id="myOperAttachMentAdd6" runat="server"></uc1:attachmentadd>&nbsp;
						</td>
					</tr>
					<tr>
						<td class="blackbordertd"><asp:checkbox id="chkOperAttachMent7" Runat="server" Text="7.其他："></asp:checkbox><asp:textbox id="txtOperOtherAttachMent" Runat="server" Width="100" CssClass="infra-input-nember"></asp:textbox></td>
						<td class="blackbordertdpaddingcontent"><uc1:attachmentadd id="myOperAttachMentAdd7" runat="server"></uc1:attachmentadd>&nbsp;
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr>
			<td class="blackbordertdpaddingcontent" colSpan="4">
				<table cellSpacing="0" cellPadding="0" width="100%" border="0">
					<tr>
						<td width="120">付款方式：</td>
						<td><asp:radiobuttonlist id="rdoOperPayType" Runat="server" RepeatColumns="4">
								<asp:ListItem Value="1" Selected>支票</asp:ListItem>
								<asp:ListItem Value="2">贷记</asp:ListItem>
								<asp:ListItem Value="3">电汇</asp:ListItem>
								<asp:ListItem Value="4">其他</asp:ListItem>
							</asp:radiobuttonlist></td>
					</tr>
				</table>
			</td>
		</tr>
		<tr>
			<td colSpan="4">
				<table cellSpacing="0" cellPadding="0" width="100%" border="0">
					<tr>
						<td class="blackbordertd" align="right" width="15%">收款银行名称：&nbsp;</td>
						<td class="blackbordertdpaddingcontent" width="18%"><INPUT class="infra-input-nember" id="txtOperBankName" style="WIDTH: 150px" type="text"
								name="txtOperBankName" runat="server">&nbsp;</td>
						<td class="blackbordertd" align="right" width="15%">付款申请日期：&nbsp;</td>
						<td class="blackbordertdpaddingcontent" width="18%"><cc3:calendar id="dtOperInputDate" runat="server" ReadOnly="False" Display="True" CalendarResource="../Images/CalendarResource/"></cc3:calendar></td>
						<td class="blackbordertd" align="right" width="15%">最晚付款日期：&nbsp;</td>
						<td class="blackbordertdpaddingcontent" width="19%"><cc3:calendar id="dtOperPayDate" runat="server" ReadOnly="False" Display="True" CalendarResource="../Images/CalendarResource/"></cc3:calendar><font color="red">*</font></td>
					</tr>
					<tr>
						<td class="blackbordertd" align="right">收款单位银行帐号：&nbsp;</td>
						<td class="blackbordertdpaddingcontent"><INPUT class="infra-input-nember" id="txtOperBankAccount" style="WIDTH: 150px" type="text"
								name="txtOperBankAccount" runat="server">&nbsp;</td>
						<td class="blackbordertd" align="right">付款期计算方式：&nbsp;</td>
						<td class="blackbordertdpaddingcontent" colspan="3"><INPUT class="infra-input-nember" id="txtOperIssueMode" style="WIDTH: 400px" type="text"
								name="txtIssueMode" runat="server">&nbsp;</td>
					</tr>
				</table>
			</td>
		</tr>
	</table>
</div>
<div id="EyeableDiv" runat="server">
	<table cellSpacing="0" cellPadding="0" width="100%" border="0">
		<tr>
			<td class="blackbordertd" align="right" width="20%">项目名称：&nbsp;</td>
			<td class="blackbordertdpaddingcontent" width="40%"><asp:label id="lblEyeProjectName" Runat="server"></asp:label>&nbsp;
			</td>
			<td class="blackbordertd" align="right" width="15%">付款期数：&nbsp;</td>
			<td class="blackbordertdpaddingcontent" width="25%">第&nbsp;<asp:label id="lblEyeIssue" runat="server"></asp:label>&nbsp;期
			</td>
		</tr>
		<tr>
			<td class="blackbordertd" align="right">合同名称：&nbsp;</td>
			<td class="blackbordertdpaddingcontent"><asp:label id="lblEyeContractName" Runat="server"></asp:label>&nbsp;
			</td>
			<td class="blackbordertd" align="right">合同编号：&nbsp;</td>
			<td class="blackbordertdpaddingcontent"><asp:label id="lblEyeContractID" Runat="server"></asp:label>&nbsp;
			</td>
		</tr>
		<tr>
			<td class="blackbordertd" align="right">承包人（分包）：&nbsp;</td>
			<td class="blackbordertdpaddingcontent" colSpan="3"><asp:label id="lblEyeSupplierName" Runat="server"></asp:label>&nbsp;
			</td>
		</tr>
		<tr>
			<td class="blackbordertd" align="right">承包人（总包）：&nbsp;</td>
			<td class="blackbordertdpaddingcontent">
				<asp:label id="lblEyeSupplier2Name" Runat="server"></asp:label>&nbsp;
			</td>
			<td class="blackbordertd" align="right">楼栋号：&nbsp;</td>
			<td class="blackbordertdpaddingcontent">
				<asp:label id="lblEyeBuilding" Runat="server"></asp:label>&nbsp;
			</td>
		</tr>
		<tr>
			<td vAlign="top" colSpan="2" height="100%">
				<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<tr>
						<td class="blackbordertdpaddingcontent">合同履约情况：（合约部综合评述）
						</td>
					</tr>
					<tr>
						<td>
							<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
								<tr>
									<td class="blackbordertdpaddingcontent" style="HEIGHT: auto"><asp:label id="lblEyeCheckOpinion" runat="server"></asp:label>&nbsp;
									</td>
								</tr>
							</table>
						</td>
					</tr>
				</table>
			</td>
			<td vAlign="top" colSpan="2" height="100%">
				<table id="tabEyeMoney" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0"
					runat="server">
					<tr>
						<td class="blackbordertd" width="40%">&nbsp;</td>
						<td class="blackbordertdpaddingcontent" width="60%">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;（ 
							RMB ）</td>
					</tr>
					<tr>
						<td class="blackbordertd" align="right">原合同金额：&nbsp;</td>
						<td class="blackbordertdpaddingcontent">
							<table cellSpacing="0" cellPadding="0" border="0">
								<tr>
									<td align="center" width="15">&nbsp;</td>
									<td><INPUT class="infra-input-nember" id="txtEyeBudgetMoney" style="FLOAT: left; WIDTH: 120px; TEXT-ALIGN: right"
											readOnly type="text" name="txtEyeBudgetMoney" runat="server">&nbsp;
									</td>
								</tr>
							</table>
						</td>
					</tr>
					<tr>
						<td class="blackbordertd" align="right">暂定金额/指定金额：&nbsp;</td>
						<td class="blackbordertdpaddingcontent">
							<table cellSpacing="0" cellPadding="0" border="0">
								<tr>
									<td align="center" width="15">-</td>
									<td><INPUT class="infra-input-nember" id="txtEyeAdjustMoney" style="FLOAT: left; WIDTH: 120px; TEXT-ALIGN: right"
											readOnly type="text" name="txtEyeAdjustMoney" runat="server">&nbsp;
									</td>
								</tr>
							</table>
						</td>
					</tr>
					<tr>
						<td class="blackbordertd" align="right">实际金额：&nbsp;</td>
						<td class="blackbordertdpaddingcontent">
							<table cellSpacing="0" cellPadding="0" border="0">
								<tr>
									<td align="center" width="15">=</td>
									<td><INPUT class="infra-input-nember" id="txtEyeOriginalMoney" style="FLOAT: left; WIDTH: 120px; TEXT-ALIGN: right"
											readOnly type="text" name="txtEyeOriginalMoney" runat="server">&nbsp;
									</td>
								</tr>
							</table>
						</td>
					</tr>
					<tr>
						<td class="blackbordertd" align="right">累计已批变更：&nbsp;</td>
						<td class="blackbordertdpaddingcontent">
							<table cellSpacing="0" cellPadding="0" border="0">
								<tr>
									<td align="center" width="15">+</td>
									<td><INPUT class="infra-input-nember" id="txtEyeTotalChangeMoney" style="FLOAT: left; WIDTH: 120px; TEXT-ALIGN: right"
											readOnly type="text" name="txtEyeTotalChangeMoney" runat="server">&nbsp;
									</td>
								</tr>
							</table>
						</td>
					</tr>
					<tr>
						<td class="blackbordertd" align="right">合同总额预计：&nbsp;</td>
						<td class="blackbordertdpaddingcontent">
							<table cellSpacing="0" cellPadding="0" border="0">
								<tr>
									<td align="center" width="15">=</td>
									<td><INPUT class="infra-input-nember" id="txtEyeNewTotalMoney" style="FLOAT: left; WIDTH: 120px; TEXT-ALIGN: right"
											readOnly type="text" name="txtEyeNewTotalMoney" runat="server">
									</td>
								</tr>
							</table>
						</td>
					</tr>
					<tr>
						<td class="blackbordertd" align="right">累计应付款：&nbsp;</td>
						<td class="blackbordertdpaddingcontent">
							<table cellSpacing="0" cellPadding="0" border="0">
								<tr>
									<td align="center" width="15">&nbsp;</td>
									<td>
										<INPUT class="infra-input-nember" id="txtEyeTotalPayMoney" style="FLOAT: left; WIDTH: 120px; TEXT-ALIGN: right"
											readOnly type="text" name="txtEyeTotalPayMoney" runat="server">
									</td>
								</tr>
							</table>
						</td>
					</tr>
					<tr>
						<td class="blackbordertd" align="right">减累计已批款：&nbsp;</td>
						<td class="blackbordertdpaddingcontent">
							<table cellSpacing="0" cellPadding="0" border="0">
								<tr>
									<td align="center" width="15">+</td>
									<td><INPUT class="infra-input-nember" id="txtEyeNegAHMoney" style="FLOAT: left; WIDTH: 120px; TEXT-ALIGN: right"
											readOnly type="text" name="txtEyeNegAHMoney" runat="server">&nbsp;
									</td>
								</tr>
							</table>
						</td>
					</tr>
					<tr>
						<td bgColor="black" colSpan="2" height="1"></td>
					</tr>
					<tr>
						<td class="blackbordertd" align="right">本期应付款：&nbsp;</td>
						<td class="blackbordertdpaddingcontent">
							<table cellSpacing="0" cellPadding="0" border="0">
								<tr>
									<td align="center" width="15">=</td>
									<td><INPUT class="infra-input-nember" id="txtEyeTotalItemMoney" style="FLOAT: left; WIDTH: 120px; TEXT-ALIGN: right"
											readOnly type="text" name="txtEyeTotalItemMoney" runat="server">&nbsp;
									</td>
								</tr>
							</table>
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr id="trEyeAttachment" runat="server">
			<td colSpan="4">
				<table cellSpacing="0" cellPadding="0" width="100%" border="0">
					<tr>
						<td class="blackbordertd" colSpan="2">附件清单：</td>
					</tr>
					<tr>
						<td class="blackbordertd" width="40%"><asp:checkbox id="chkEyeAttachMent1" Runat="server" Text="1.承包商的付款申请：" Enabled="False"></asp:checkbox></td>
						<td class="blackbordertdpaddingcontent"><uc1:attachmentlist id="myEyeAttachMentList1" runat="server"></uc1:attachmentlist>&nbsp;
						</td>
					</tr>
					<tr>
						<td class="blackbordertd"><asp:checkbox id="chkEyeAttachMent2" Runat="server" Text="2.顾问估算师的付款证书：" Enabled="False"></asp:checkbox></td>
						<td class="blackbordertdpaddingcontent"><uc1:attachmentlist id="myEyeAttachMentList2" runat="server"></uc1:attachmentlist>&nbsp;
						</td>
					</tr>
					<tr>
						<td class="blackbordertd"><asp:checkbox id="chkEyeAttachMent3" Runat="server" Text="3.付款记录（累计表）：" Enabled="False"></asp:checkbox></td>
						<td class="blackbordertdpaddingcontent"><uc1:attachmentlist id="myEyeAttachMentList3" runat="server"></uc1:attachmentlist>&nbsp;
						</td>
					</tr>
					<tr>
						<td class="blackbordertd"><asp:checkbox id="chkEyeAttachMent4" Runat="server" Text="4.监理意见：" Enabled="False"></asp:checkbox></td>
						<td class="blackbordertdpaddingcontent"><uc1:attachmentlist id="myEyeAttachMentList4" runat="server"></uc1:attachmentlist>&nbsp;
						</td>
					</tr>
					<tr>
						<td class="blackbordertd"><asp:checkbox id="chkEyeAttachMent5" Runat="server" Text="5.工程部/建筑开发部之专业意见：" Enabled="False"></asp:checkbox></td>
						<td class="blackbordertdpaddingcontent"><uc1:attachmentlist id="myEyeAttachMentList5" runat="server"></uc1:attachmentlist>&nbsp;
						</td>
					</tr>
					<tr>
						<td class="blackbordertd"><asp:checkbox id="chkEyeAttachMent6" Runat="server" Text="6.发票：" Enabled="False"></asp:checkbox></td>
						<td class="blackbordertdpaddingcontent"><uc1:attachmentlist id="myEyeAttachMentList6" runat="server"></uc1:attachmentlist>&nbsp;
						</td>
					</tr>
					<tr>
						<td class="blackbordertd"><asp:checkbox id="chkEyeAttachMent7" Runat="server" Text="7.其他：" Enabled="False"></asp:checkbox><asp:textbox id="txtEyeOtherAttachMent" Runat="server" Width="100" CssClass="infra-input-nember"
								Enabled="False"></asp:textbox></td>
						<td class="blackbordertdpaddingcontent"><uc1:attachmentlist id="myEyeAttachMentList7" runat="server"></uc1:attachmentlist>&nbsp;
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr>
			<td class="blackbordertdpaddingcontent" colSpan="4">
				<table cellSpacing="0" cellPadding="0" width="100%" border="0">
					<tr>
						<td width="120">付款方式：</td>
						<td><asp:radiobuttonlist id="rdoEyePayType" Runat="server" RepeatColumns="4" Enabled="False">
								<asp:ListItem Value="1" Selected>支票</asp:ListItem>
								<asp:ListItem Value="2">贷记</asp:ListItem>
								<asp:ListItem Value="3">电汇</asp:ListItem>
								<asp:ListItem Value="4">其他</asp:ListItem>
							</asp:radiobuttonlist></td>
					</tr>
				</table>
			</td>
		</tr>
		<tr>
			<td colSpan="4">
				<table cellSpacing="0" cellPadding="0" width="100%" border="0">
					<tr>
						<td class="blackbordertd" align="right" width="15%">收款银行名称：&nbsp;</td>
						<td class="blackbordertdpaddingcontent" width="18%"><asp:label id="lblEyeBankName" runat="server"></asp:label>&nbsp;
						</td>
						<td class="blackbordertd" align="right" width="15%">付款申请日期：&nbsp;</td>
						<td class="blackbordertdpaddingcontent" width="18%"><cc3:calendar id="dtEyeInputDate" runat="server" ReadOnly="true" Display="True" CalendarResource="../Images/CalendarResource/"></cc3:calendar></td>
						<td class="blackbordertd" align="right" width="15%">最晚付款日期：&nbsp;</td>
						<td class="blackbordertdpaddingcontent" width="19%"><cc3:calendar id="dtEyePayDate" runat="server" ReadOnly="True" Display="True" CalendarResource="../Images/CalendarResource/"></cc3:calendar></td>
					</tr>
					<tr>
						<td class="blackbordertd" align="right">收款单位银行帐号：&nbsp;</td>
						<td class="blackbordertdpaddingcontent"><asp:label id="lblEyeBankAccount" runat="server"></asp:label>&nbsp;</td>
						<td class="blackbordertd" align="right">付款期计算方式：&nbsp;</td>
						<td class="blackbordertdpaddingcontent" colspan="3"><asp:label id="lblEyeIssueMode" Runat="server"></asp:label>&nbsp;</td>
					</tr>
				</table>
			</td>
		</tr>
	</table>
</div>
