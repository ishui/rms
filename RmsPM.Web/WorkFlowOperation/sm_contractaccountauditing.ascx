<%@ Control Language="c#" Inherits="WorkFlowOperation_SM_ContractAccountAuditing" CodeFile="SM_ContractAccountAuditing.ascx.cs" %>
<%@ Register TagPrefix="uc1" TagName="AttachMentAdd" Src="../UserControls/AttachMentAdd.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AttachMentList" Src="../UserControls/AttachMentList.ascx" %>
<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics.WebUI.WebDataInput.v5.1, Version=5.1.20051.37, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<link href="/../Images/index.css" rel="stylesheet" type="text/css" />
<input id="hidOriginalMoney" runat="server" type="hidden" NAME="hidOriginalMoney">
<div id="OperableDiv" runat="server" onkeydown="if(event.keyCode==13) event.keyCode=9">
	<table cellSpacing="0" cellPadding="0" width="100%" border="0">
		<tr>
			<td class="blackbordertd"  align="right" colspan="2">项目名称：&nbsp;</td>
			<td class="blackbordertdpaddingcontent" colspan="2">
				<asp:label id="lblOperProjectName" Runat="server"></asp:label>&nbsp;
			</td>
			<td class="blackbordertd" align="right">合同编号：&nbsp;</td>
			<td class="blackbordertdpaddingcontent">
				<asp:label id="lblOperContractID" Runat="server"></asp:label>&nbsp;
			</td>
		</tr>
		<tr>
			<td class="blackbordertd" align="right" colspan="2">合同名称：&nbsp;</td>
			<td class="blackbordertdpaddingcontent" colspan="4">
				<asp:label id="lblOperContractName" Runat="server"></asp:label>&nbsp;
			</td>
		</tr>
		<tr>
			<td class="blackbordertd" align="right" colspan="2">承包人（分包）：&nbsp;</td>
			<td class="blackbordertdpaddingcontent" colspan="2">
				<asp:label id="lblOperSupplierName" Runat="server"></asp:label>&nbsp;
			</td>
			<td class="blackbordertd" align="right">审批表编号：&nbsp;</td>
			<td class="blackbordertdpaddingcontent">
				<INPUT class="input" id="txtOperContractAccountID" type="text" name="txtOperPayNumber"
					runat="server">
			</td>
		</tr>
		<tr>
			<td class="blackbordertd" align="right" colspan="2">承包人（总包）：&nbsp;</td>
			<td class="blackbordertdpaddingcontent" colspan="2">
				<asp:label id="lblOperSupplier2Name" Runat="server"></asp:label>&nbsp;
			</td>
			<td class="blackbordertd" align="right">楼栋号：&nbsp;</td>
			<td class="blackbordertdpaddingcontent">
				<asp:label id="lblOperBuilding" Runat="server"></asp:label>&nbsp;
			</td>
		</tr>		
		<tr>
			<td colspan="2" valign="top" height="100%">
				<table cellSpacing="0" cellPadding="0" width="100%" border="0" height="100%">
					<tr>
						<td class="blackbordertdpaddingcontent" width="20" valign="middle">
							<br>
							原<br>
							因<br>
							及<br>
							摘<br>
							要<br>
						</td>
						<td>
							<table cellSpacing="0" cellPadding="0" width="100%" border="0" height="100%">
								<tr>
									<td class="blackbordertdpaddingcontent" style="HEIGHT:auto">
										<TEXTAREA id="txtOperReason" style="width: 98%;height:98%;" name="txtOperReason" rows="12" runat="server"></TEXTAREA>&nbsp;
									</td>
								</tr>
								<tr id="trOperAttachment" runat="server">
									<td class="blackbordertdpaddingcontent">
										<uc1:attachmentadd id="myOperAttachMentAdd" runat="server"></uc1:attachmentadd>&nbsp;
									</td>
								</tr>
							</table>
						</td>
					</tr>
				</table>
			</td>
			<td colspan="4" valign="top" height="100%">
				<table cellSpacing="0" cellPadding="0" width="100%" height="100%" border="0" id="tabOperMoney"
					runat="server">
					<tr>
						<td class="blackbordertd">&nbsp;</td>
						<td class="blackbordertdpaddingcontent" colspan="3">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;（ 
							RMB ）</td>
					</tr>
					<tr>
						<td class="blackbordertd" nowrap align="right">原合同金额：&nbsp;</td>
						<td class="blackbordertdpaddingcontent" colspan="3">
							<table cellSpacing="0" cellPadding="0" border="0">
								<tr>
									<td width="15" align="center">&nbsp;</td>
									<td>
										<INPUT class="infra-input-nember" id="txtOperBudgetMoney" style="FLOAT: left; WIDTH: 80px; TEXT-ALIGN: right"
											readOnly type="text" name="txtOperBudgetMoney" runat="server">&nbsp;
									</td>
								</tr>
							</table>
						</td>
					</tr>
					<tr>
						<td class="blackbordertd"  align="right">暂定金额/指定金额：&nbsp;</td>
						<td class="blackbordertdpaddingcontent" colspan="3">
							<table cellSpacing="0" cellPadding="0" border="0">
								<tr>
									<td width="15" align="center">-</td>
									<td>
										<INPUT class="infra-input-nember" id="txtOperAdjustMoney" style="FLOAT: left; WIDTH: 80px; TEXT-ALIGN: right"
											readOnly type="text" name="txtOperAdjustMoney" runat="server">&nbsp;
									</td>
								</tr>
							</table>
						</td>
					</tr>
					<tr>
						<td class="blackbordertd" align="right">实际金额：&nbsp;</td>
						<td class="blackbordertdpaddingcontent" colspan="3">
							<table cellSpacing="0" cellPadding="0" border="0">
								<tr>
									<td width="15" align="center">=</td>
									<td><INPUT class="infra-input-nember" id="txtOperOriginalMoney" style="FLOAT: left; WIDTH: 80px; TEXT-ALIGN: right"
											readOnly type="text" name="txtOperOriginalMoney" runat="server">&nbsp;</td>
								</tr>
							</table>
						</td>
					</tr>
					<tr>
						<td class="blackbordertd" align="right">累计已批变更：&nbsp;</td>
						<td class="blackbordertdpaddingcontent" colspan="3">
							<table cellSpacing="0" cellPadding="0" border="0">
								<tr>
									<td width="15" align="center">+</td>
									<td>
										<INPUT class="infra-input-nember" id="txtOperTotalChangeMoney" style="FLOAT: left; WIDTH: 80px; TEXT-ALIGN: right"
											readOnly type="text" name="txtOperTotalChangeMoney" runat="server">&nbsp;
									</td>
								</tr>
							</table>
						</td>
					</tr>
					
					
					
					
					
					<tr>
						<td height="1" bgcolor="black" colspan="4"></td>
					</tr>
					<tr>
						<td class="blackbordertd" align="right">合同总额预计：&nbsp;</td>
						<td class="blackbordertdpaddingcontent" colspan="3">
							<table cellSpacing="0" cellPadding="0" border="0">
								<tr>
									<td width="15" align="center">=</td>
									<td>
										<INPUT class="infra-input-nember" id="txtOperNewTotalMoney" style="FLOAT: left; WIDTH: 80px; TEXT-ALIGN: right"
											readOnly type="text" name="txtOperNewTotalMoney" runat="server">
									</td>
								</tr>
							</table>
						</td>
					</tr>
					<tr id="trOperEstimateChangeMoney" runat="server" Visable="False">
						<td class="blackbordertd" align="right">估计变更总额：&nbsp;</td>
						<td class="blackbordertdpaddingcontent" colspan="3">
							<table cellSpacing="0" cellPadding="0" border="0">
								<tr>
									<td width="15" align="center">&nbsp;</td>
									<td>
										<INPUT class="infra-input-nember" id="txtOperEstimateChangeMoney" style="FLOAT: left; WIDTH: 80px; TEXT-ALIGN: right"
											readOnly type="text" name="txtOperEstimateChangeMoney" runat="server">
									</td>
								</tr>
							</table>
						</td>
					</tr>
					<tr>
						<td class="blackbordertd" width="22%" align="center">&nbsp;</td>
						<td class="blackbordertd"  align="center">承包商申请<br>
							RMB</td>
						<td class="blackbordertd" align="center">顾问估算师审核<br>
							RMB</td>
						<td class="blackbordertd" align="center">项目合约部审核<br>
							RMB</td>
					</tr>
					<tr>
						<td class="blackbordertd"  align="right">1.原合同金额：&nbsp;</td>
						<td class="blackbordertdpaddingcontent">
							<INPUT class="infra-input-nember" id="txtOperSupplierOriginalMoney" style="FLOAT: left; WIDTH: 80px; TEXT-ALIGN: right"
								readOnly type="text" name="txtOperSupplierOriginalMoney" runat="server">&nbsp;
						</td>
						<td class="blackbordertdpaddingcontent">
							<INPUT class="infra-input-nember" id="txtOperConsultantOriginalMoney" style="FLOAT: left; WIDTH: 80px; TEXT-ALIGN: right"
								readOnly type="text" name="txtOperConsultantOriginalMoney" runat="server">&nbsp;
						</td>
						<td class="blackbordertdpaddingcontent">
							<INPUT class="infra-input-nember" id="txtOperProjectOriginalMoney" style="FLOAT: left; WIDTH: 80px; TEXT-ALIGN: right"
								readOnly type="text" name="txtOperProjectOriginalMoney" runat="server">&nbsp;
						</td>
					</tr>
					<tr>
						<td class="blackbordertd" align="right">2.变更总额：&nbsp;</td>
						<td class="blackbordertdpaddingcontent">
							<INPUT class="infra-input-nember" id="txtOperSupplierTotalChangeMoney" style="FLOAT: left; WIDTH: 80px; TEXT-ALIGN: right"
								readOnly type="text" name="txtOperSupplierTotalChangeMoney" runat="server">&nbsp;
						</td>
						<td class="blackbordertdpaddingcontent">
							<INPUT class="infra-input-nember" id="txtOperConsultantTotalAuditMoney" style="FLOAT: left; WIDTH: 80px; TEXT-ALIGN: right"
								readOnly type="text" name="txtOperConsultantTotalAuditMoney" runat="server">&nbsp;
						</td>
						<td class="blackbordertdpaddingcontent">
							<INPUT class="infra-input-nember" id="txtOperProjectTotalAuditMoney" style="FLOAT: left; WIDTH: 80px; TEXT-ALIGN: right"
								readOnly type="text" name="txtOperProjectTotalAuditMoney" runat="server">&nbsp;
						</td>
					</tr>
					<tr>
						<td class="blackbordertd" align="right">3. 其他(调整/扣款):&nbsp;</td>
						<td class="blackbordertdpaddingcontent">
							<INPUT class="infra-input-nember" id="txtOperSupplierAdjustMoney" style="FLOAT: left; WIDTH: 80px; TEXT-ALIGN: right"
								readOnly type="text" name="txtOperSupplierAdjustMoney" runat="server">&nbsp;
						</td>
						<td class="blackbordertdpaddingcontent">
							<INPUT class="infra-input-nember" id="txtOperConsultantAdjustMoney" style="FLOAT: left; WIDTH: 80px; TEXT-ALIGN: right"
								readOnly type="text" name="txtOperConsultantAdjustMoney" runat="server">&nbsp;
						</td>
						<td class="blackbordertdpaddingcontent">
							<INPUT class="infra-input-nember" id="txtOperProjectAdjustMoney" style="FLOAT: left; WIDTH: 80px; TEXT-ALIGN: right"
								readOnly type="text" name="txtOperProjectAdjustMoney" runat="server">&nbsp;
						</td>
					</tr>
					<tr>
						<td class="blackbordertd" align="right">结算金额：&nbsp;</td>
						<td class="blackbordertdpaddingcontent">
							<INPUT class="infra-input-nember" id="txtOperSupplierTotalMoney" style="FLOAT: left; WIDTH: 80px; TEXT-ALIGN: right"
								readOnly type="text" name="txtOperSupplierTotalMoney" runat="server">&nbsp;
						</td>
						<td class="blackbordertdpaddingcontent">
							<INPUT class="infra-input-nember" id="txtOperConsultantTotalMoney" style="FLOAT: left; WIDTH: 80px; TEXT-ALIGN: right"
								readOnly type="text" name="txtOperConsultantTotalMoney" runat="server">&nbsp;
						</td>
						<td class="blackbordertdpaddingcontent">
							<INPUT class="infra-input-nember" id="txtOperProjectTotalMoney" style="FLOAT: left; WIDTH: 80px; TEXT-ALIGN: right"
								readOnly type="text" name="txtOperProjectTotalMoney" runat="server">&nbsp;
						</td>
					</tr>
				</table>
			</td>
		</tr>
	</table>
</div>
<div id="EyeableDiv" runat="server">
	<table cellSpacing="0" cellPadding="0" width="100%" border="0">
		<tr>
			<td class="blackbordertd" align="right" colspan="2">项目名称：&nbsp;</td>
			<td class="blackbordertdpaddingcontent"  colspan="2">
				<asp:label id="lblEyeProjectName" Runat="server"></asp:label>&nbsp;
			</td>
			<td class="blackbordertd" align="right" >合同编号：&nbsp;</td>
			<td class="blackbordertdpaddingcontent">
				<asp:label id="lblEyeContractID" Runat="server"></asp:label>&nbsp;
			</td>
		</tr>
		<tr>
			<td class="blackbordertd" align="right" colspan="2">合同名称：&nbsp;</td>
			<td class="blackbordertdpaddingcontent" colspan="4">
				<asp:label id="lblEyeContractName" Runat="server"></asp:label>&nbsp;
			</td>
		</tr>
		<tr>
			<td class="blackbordertd" align="right" colspan="2">承 包 人：&nbsp;</td>
			<td class="blackbordertdpaddingcontent" colspan="2">
				<asp:label id="lblEyeSupplierName" Runat="server"></asp:label>&nbsp;
			</td>
			<td class="blackbordertd" align="right">审批表编号：&nbsp;</td>
			<td class="blackbordertdpaddingcontent">
				<asp:Label ID="lblEyeContractAccountID" runat="server"></asp:Label>&nbsp;
			</td>
		</tr>
		<tr>
			<td class="blackbordertd" align="right" colspan="2">承包人（总包）：&nbsp;</td>
			<td class="blackbordertdpaddingcontent" colspan="2">
				<asp:label id="lblEyeSupplier2Name" Runat="server"></asp:label>&nbsp;
			</td>
			<td class="blackbordertd" align="right">楼栋号：&nbsp;</td>
			<td class="blackbordertdpaddingcontent">
				<asp:label id="lblEyeBuilding" Runat="server"></asp:label>&nbsp;
			</td>
		</tr>
		<tr>
			<td colspan="2" valign="top" height="100%">
				<table cellSpacing="0" cellPadding="0" width="100%" border="0" height="100%">
					<tr>
						<td class="blackbordertdpaddingcontent" width="30" valign="middle">
							<br>
							原<br>
							因<br>
							及<br>
							摘<br>
							要<br>
						</td>
						<td>
							<table cellSpacing="0" cellPadding="0" width="100%" border="0" height="100%">
								<tr>
									<td class="blackbordertdpaddingcontent" style="HEIGHT:auto">
										<asp:Label ID="lblEyeReason" runat="server"></asp:Label>&nbsp;
									</td>
								</tr>
								<tr id="trEyeAttachment" runat="server">
									<td class="blackbordertdpaddingcontent">
										<table cellSpacing="0" cellPadding="0" width="100%" border="0" height="100%">
											<tr>
												<td width="40">附件：</td>
												<td align="left"><uc1:attachmentlist id="myEyeAttachMentList" runat="server"></uc1:attachmentlist></td>
											</tr>
										</table>
									</td>
								</tr>
							</table>
						</td>
					</tr>
				</table>
			</td>
			<td colspan="4" valign="top" height="100%">
				<table cellSpacing="0" cellPadding="0" width="100%" height="100%" border="0" id="tabEyeMoney"
					runat="server">
					<tr>
						<td class="blackbordertd">&nbsp;</td>
						<td class="blackbordertdpaddingcontent" colspan="3">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;（ 
							RMB ）</td>
					</tr>
					<tr>
						<td class="blackbordertd" align="right">原合同金额：&nbsp;</td>
						<td class="blackbordertdpaddingcontent" colspan="3">
							<table cellSpacing="0" cellPadding="0" border="0">
								<tr>
									<td width="15" align="center">&nbsp;</td>
									<td>
										<INPUT class="infra-input-nember" id="txtEyeBudgetMoney" style="FLOAT: left; WIDTH: 80px; TEXT-ALIGN: right"
											readOnly type="text" name="txtEyeBudgetMoney" runat="server">&nbsp;
									</td>
								</tr>
							</table>
						</td>
					</tr>
					<tr>
						<td class="blackbordertd" align="right">暂定金额/指定金额：&nbsp;</td>
						<td class="blackbordertdpaddingcontent" colspan="3">
							<table cellSpacing="0" cellPadding="0" border="0">
								<tr>
									<td width="15" align="center">-</td>
									<td>
										<INPUT class="infra-input-nember" id="txtEyeAdjustMoney" style="FLOAT: left; WIDTH: 80px; TEXT-ALIGN: right"
											readOnly type="text" name="txtEyeAdjustMoney" runat="server">&nbsp;
									</td>
								</tr>
							</table>
						</td>
					</tr>
					<tr>
						<td class="blackbordertd" align="right">实际金额：&nbsp;</td>
						<td class="blackbordertdpaddingcontent" colspan="3">
							<table cellSpacing="0" cellPadding="0" border="0">
								<tr>
									<td width="15" align="center">=</td>
									<td><INPUT class="infra-input-nember" id="txtEyeOriginalMoney" style="FLOAT: left; WIDTH: 80px; TEXT-ALIGN: right"
											readOnly type="text" name="txtEyeOriginalMoney" runat="server">&nbsp;</td>
								</tr>
							</table>
						</td>
					</tr>
					<tr>
						<td class="blackbordertd" align="right">累计已批变更：&nbsp;</td>
						<td class="blackbordertdpaddingcontent" colspan="3">
							<table cellSpacing="0" cellPadding="0" border="0">
								<tr>
									<td width="15" align="center">+</td>
									<td>
										<INPUT class="infra-input-nember" id="txtEyeTotalChangeMoney" style="FLOAT: left; WIDTH: 80px; TEXT-ALIGN: right"
											readOnly type="text" name="txtEyeTotalChangeMoney" runat="server">&nbsp;
									</td>
								</tr>
							</table>
						</td>
					</tr>
					
					
					
					
					<tr>
						<td height="1" bgcolor="black" colspan="4"></td>
					</tr>
					<tr>
						<td class="blackbordertd" align="right">合同总额预计：&nbsp;</td>
						<td class="blackbordertdpaddingcontent" colspan="3">
							<table cellSpacing="0" cellPadding="0" border="0">
								<tr>
									<td width="15" align="center">=</td>
									<td>
										<INPUT class="infra-input-nember" id="txtEyeNewTotalMoney" style="FLOAT: left; WIDTH: 80px; TEXT-ALIGN: right"
											readOnly type="text" name="txtEyeNewTotalMoney" runat="server">
									</td>
								</tr>
							</table>
						</td>
					</tr>
					<tr id="trEyeEstimateChangeMoney" runat="server" Visable="False">
						<td class="blackbordertd" align="right">估计变更总额：&nbsp;</td>
						<td class="blackbordertdpaddingcontent" colspan="3">
							<table cellSpacing="0" cellPadding="0" border="0">
								<tr>
									<td width="15" align="center">&nbsp;</td>
									<td>
										<INPUT class="infra-input-nember" id="txtEyeEstimateChangeMoney" style="FLOAT: left; WIDTH: 80px; TEXT-ALIGN: right"
											readOnly type="text" name="txtEyeEstimateChangeMoney" runat="server">
									</td>
								</tr>
							</table>
						</td>
					</tr>
					<tr>
						<td class="blackbordertd" width="22%" align="center">&nbsp;</td>
						<td class="blackbordertd" width="26%" align="center">承包商申请<br>
							RMB</td>
						<td class="blackbordertd" width="26%" align="center">顾问估算师审核<br>
							RMB</td>
						<td class="blackbordertd" width="26%" align="center">项目合约部审核<br>
							RMB</td>
					</tr>
					<tr>
						<td class="blackbordertd" align="right">1.原合同金额：&nbsp;</td>
						<td class="blackbordertdpaddingcontent">
							<INPUT class="infra-input-nember" id="txtEyeSupplierOriginalMoney" style="FLOAT: left; WIDTH: 80px; TEXT-ALIGN: right"
								readOnly type="text" name="txtEyeSupplierOriginalMoney" runat="server">&nbsp;
						</td>
						<td class="blackbordertdpaddingcontent">
							<INPUT class="infra-input-nember" id="txtEyeConsultantOriginalMoney" style="FLOAT: left; WIDTH: 80px; TEXT-ALIGN: right"
								readOnly type="text" name="txtEyeConsultantOriginalMoney" runat="server">&nbsp;
						</td>
						<td class="blackbordertdpaddingcontent">
							<INPUT class="infra-input-nember" id="txtEyeProjectOriginalMoney" style="FLOAT: left; WIDTH: 80px; TEXT-ALIGN: right"
								readOnly type="text" name="txtEyeProjectOriginalMoney" runat="server">&nbsp;
						</td>
					</tr>
					<tr>
						<td class="blackbordertd" align="right">2.变更总额：&nbsp;</td>
						<td class="blackbordertdpaddingcontent">
							<INPUT class="infra-input-nember" id="txtEyeSupplierTotalChangeMoney" style="FLOAT: left; WIDTH: 80px; TEXT-ALIGN: right"
								readOnly type="text" name="txtEyeSupplierTotalChangeMoney" runat="server">&nbsp;
						</td>
						<td class="blackbordertdpaddingcontent">
							<INPUT class="infra-input-nember" id="txtEyeConsultantTotalAuditMoney" style="FLOAT: left; WIDTH: 80px; TEXT-ALIGN: right"
								readOnly type="text" name="txtEyeConsultantTotalAuditMoney" runat="server">&nbsp;
						</td>
						<td class="blackbordertdpaddingcontent">
							<INPUT class="infra-input-nember" id="txtEyeProjectTotalAuditMoney" style="FLOAT: left; WIDTH: 80px; TEXT-ALIGN: right"
								readOnly type="text" name="txtEyeProjectTotalAuditMoney" runat="server">&nbsp;
						</td>
					</tr>
					<tr>
						<td class="blackbordertd" align="right">3. 其他(调整/扣款):&nbsp;</td>
						<td class="blackbordertdpaddingcontent">
							<INPUT class="infra-input-nember" id="txtEyeSupplierAdjustMoney" style="FLOAT: left; WIDTH: 80px; TEXT-ALIGN: right"
								readOnly type="text" name="txtEyeSupplierAdjustMoney" runat="server">&nbsp;
						</td>
						<td class="blackbordertdpaddingcontent">
							<INPUT class="infra-input-nember" id="txtEyeConsultantAdjustMoney" style="FLOAT: left; WIDTH: 80px; TEXT-ALIGN: right"
								readOnly type="text" name="txtEyeConsultantAdjustMoney" runat="server">&nbsp;
						</td>
						<td class="blackbordertdpaddingcontent">
							<INPUT class="infra-input-nember" id="txtEyeProjectAdjustMoney" style="FLOAT: left; WIDTH: 80px; TEXT-ALIGN: right"
								readOnly type="text" name="txtEyeProjectAdjustMoney" runat="server">&nbsp;
						</td>
					</tr>
					<tr>
						<td class="blackbordertd" align="right">结算金额：&nbsp;</td>
						<td class="blackbordertdpaddingcontent">
							<INPUT class="infra-input-nember" id="txtEyeSupplierTotalMoney" style="FLOAT: left; WIDTH: 80px; TEXT-ALIGN: right"
								readOnly type="text" name="txtEyeSupplierTotalMoney" runat="server">&nbsp;
						</td>
						<td class="blackbordertdpaddingcontent">
							<INPUT class="infra-input-nember" id="txtEyeConsultantTotalMoney" style="FLOAT: left; WIDTH: 80px; TEXT-ALIGN: right"
								readOnly type="text" name="txtEyeConsultantTotalMoney" runat="server">&nbsp;
						</td>
						<td class="blackbordertdpaddingcontent">
							<INPUT class="infra-input-nember" id="txtEyeProjectTotalMoney" style="FLOAT: left; WIDTH: 80px; TEXT-ALIGN: right"
								readOnly type="text" name="txtEyeProjectTotalMoney" runat="server">&nbsp;
						</td>
					</tr>
				</table>
			</td>
		</tr>
	</table>
</div>
