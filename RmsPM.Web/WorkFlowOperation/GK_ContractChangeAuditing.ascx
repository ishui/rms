<%@ Control Language="c#" Inherits="WorkFlowOperation_GK_ContractChangeAuditing" CodeFile="GK_ContractChangeAuditing.ascx.cs" %>
<%@ Register TagPrefix="uc1" TagName="AttachMentAdd" Src="../UserControls/AttachMentAdd.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AttachMentList" Src="../UserControls/AttachMentList.ascx" %>
<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics.WebUI.WebDataInput.v5.1, Version=5.1.20051.37, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<script language="javascript">
<!--
	function DoChange()
	{
		var sClintID = '<% =this.ClientID.ToString() %>';
		var ProjectCode = document.all(sClintID + "_txtProjectCode").value;
		var ContractCode = document.all(sClintID + "_txtContractCode").value;
		
		OpenFullWindow('ContractChange.aspx?Act=Edit&ProjectCode=' + ProjectCode + '&ContractCode=' + ContractCode + '&Return=true' ,'合同信息');
	}

	function DoChangeReturn( NewTotalMoney,ChangeMoney)
	{
		var sClintID = '<% =this.ClientID.ToString() %>';
		
		//格式化
		NewTotalMoney = formatNumber(NewTotalMoney, "#,###.00");
		ChangeMoney = formatNumber(ChangeMoney, "#,###.00");
		
		document.all(sClintID + "_txtOperChangeMoney").value = ChangeMoney;
		document.all(sClintID + "_txtOperNewTotalMoney").value = NewTotalMoney;
		document.all(sClintID + "_txtEyeChangeMoney").value = ChangeMoney;
		document.all(sClintID + "_txtEyeNewTotalMoney").value = NewTotalMoney;
	}
//-->
</script>
<table cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td class="blackbordertd" width="20%" align="right">项目名称：&nbsp;</td>
		<td class="blackbordertdpaddingcontent" width="40%">
			<asp:label id="lblProjectName" Runat="server"></asp:label>&nbsp;
		</td>
		<td class="blackbordertd" width="10%" align="right">合同编号：&nbsp;</td>
		<td class="blackbordertdpaddingcontent" width="30%">
			<asp:label id="lblContractID" Runat="server"></asp:label>&nbsp;
		</td>
	</tr>
</table>
<div id="OperableDiv" runat="server" onkeydown="if(event.keyCode==13) event.keyCode=9">
	<table cellSpacing="0" cellPadding="0" width="100%" border="0">
		<tr>
			<td class="blackbordertd" width="20%" align="right">合同名称：&nbsp;</td>
			<td class="blackbordertdpaddingcontent" width="40%">
				<asp:label id="lblOperContractName" Runat="server"></asp:label>&nbsp;
			</td>
			<td class="blackbordertd" width="10%" align="right">变更依据&nbsp;&nbsp;&nbsp;&nbsp;<br>
				（AI/SI NO.）</td>
			<td class="blackbordertdpaddingcontent" width="30%">
				<INPUT class="input" id="txtOperVoucher" type="text" size="32" name="txtOperVoucher" runat="server">&nbsp;
			</td>
		</tr>
		<tr>
			<td class="blackbordertd" align="right">承包人（分包）：&nbsp;</td>
			<td class="blackbordertdpaddingcontent">
				<asp:label id="lblOperSupplierName" Runat="server"></asp:label>&nbsp;
			</td>
			<td class="blackbordertd" align="right">审批表编号：&nbsp;</td>
			<td class="blackbordertdpaddingcontent">
				<INPUT class="input" id="txtOperChangeId" type="text" size="32" name="txtOperChangeId"
					runat="server">
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
			<td colspan="2" valign="top" height="100%">
				<table cellSpacing="0" cellPadding="0" width="100%" border="0" height="100%">
					<tr>
						<td class="blackbordertd" align="center" width="37" valign="middle" height="100%"><br>
							变<br>
							更<br>
							原<br>
							因<br>
							及<br>
							摘<br>
							要</td>
						<td>
							<table cellSpacing="0" cellPadding="0" width="100%" border="0" height="100%">
								<tr>
									<td class="blackbordertdpaddingcontent" style="HEIGHT:auto">
										<TEXTAREA id="txtOperChangeReason" style="WIDTH: 100%" name="txtOperChangeReason" rows="10"
											runat="server"></TEXTAREA>&nbsp;
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
			<td colspan="2" valign="top" height="100%">
				<table cellSpacing="0" cellPadding="0" width="100%" height="100%" border="0" id="tabOperMoney"
					runat="server">
					<tr>
						<td class="blackbordertd" width="43%">&nbsp;</td>
						<td class="blackbordertdpaddingcontent" width="57%">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;（ 
							RMB ）</td>
					</tr>
					<tr>
						<td class="blackbordertd" align="right">原合同金额：&nbsp;</td>
						<td class="blackbordertdpaddingcontent">
							<table cellSpacing="0" cellPadding="0" border="0">
								<tr>
									<td width="15" align="center">&nbsp;</td>
									<td>
										<INPUT class="infra-input-nember" id="txtOperBudgetMoney" style="FLOAT: left; WIDTH: 120px; TEXT-ALIGN: right"
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
									<td width="15" align="center">-</td>
									<td>
										<INPUT class="infra-input-nember" id="txtOperAdjustMoney" style="FLOAT: left; WIDTH: 120px; TEXT-ALIGN: right"
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
									<td width="15" align="center">=</td>
									<td>
										<INPUT class="infra-input-nember" id="txtOperOriginalMoney" style="FLOAT: left; WIDTH: 120px; TEXT-ALIGN: right"
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
									<td width="15" align="center">+</td>
									<td>
										<INPUT class="infra-input-nember" id="txtOperTotalChangeMoney" style="FLOAT: left; WIDTH: 120px; TEXT-ALIGN: right"
											readOnly type="text" name="txtOperTotalChangeMoney" runat="server">&nbsp;
									</td>
								</tr>
							</table>
						</td>
					</tr>
					<tr>
						<td class="blackbordertd" align="right">本次上报变更金额：&nbsp;</td>
						<td class="blackbordertdpaddingcontent">
							<table cellSpacing="0" cellPadding="0" border="0">
								<tr>
									<td width="15" align="center">+</td>
									<td>
										<INPUT class="infra-input-nember" id="txtOperChangeMoney" style="WIDTH: 120px; TEXT-ALIGN: right"
											readOnly type="text" name="txtOperChangeMoney" runat="server">&nbsp;
									</td>
								</tr>
							</table>
						</td>
					</tr>
					<tr>
						<td height="1" bgcolor="black" colspan="2"></td>
					</tr>
					<tr>
						<td class="blackbordertd" align="right">合同总额预计：&nbsp;</td>
						<td class="blackbordertdpaddingcontent">
							<table cellSpacing="0" cellPadding="0" border="0">
								<tr>
									<td width="15" align="center">=</td>
									<td>
										<INPUT class="infra-input-nember" id="txtOperNewTotalMoney" style="FLOAT: left; WIDTH: 120px; TEXT-ALIGN: right"
											readOnly type="text" name="txtOperNewTotalMoney" runat="server">&nbsp;
									</td>
								</tr>
							</table>
						</td>
					</tr>
					<tr>
						<td height="1" bgcolor="black" colspan="2"></td>
					</tr>
					<tr>
						<td class="blackbordertd" align="right">承包商本次变更申请金额：&nbsp;</td>
						<td class="blackbordertdpaddingcontent">
							<table cellSpacing="0" cellPadding="0" border="0">
								<tr>
									<td width="15" align="center">&nbsp;</td>
									<td width="120">
										<igtxt:webnumericedit id="txtOperSupplierChangeMoney" runat="server" JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js"
											JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js" ImageDirectory="../images/infragistics/images/"
											CssClass="infra-input-nember" MinDecimalPlaces="Two" Width="120"></igtxt:webnumericedit>&nbsp;
									</td>
								</tr>
							</table>
						</td>
					</tr>
					<tr>
						<td class="blackbordertd" align="right">顾问估算师审核金额：&nbsp;</td>
						<td class="blackbordertdpaddingcontent">
							<table cellSpacing="0" cellPadding="0" border="0">
								<tr>
									<td width="15" align="center">&nbsp;</td>
									<td width="120">
										<igtxt:webnumericedit id="txtOperConsultantAuditMoney" runat="server" JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js"
											JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js" ImageDirectory="../images/infragistics/images/"
											CssClass="infra-input-nember" MinDecimalPlaces="Two" Width="120"></igtxt:webnumericedit>&nbsp;
									</td>
								</tr>
							</table>
						</td>
					</tr>
					<tr>
						<td class="blackbordertd" align="right">项目合约部审核金额：&nbsp;</td>
						<td class="blackbordertdpaddingcontent">
							<table cellSpacing="0" cellPadding="0" border="0">
								<tr>
									<td width="15" align="center">&nbsp;</td>
									<td width="120">
										<igtxt:webnumericedit id="txtOperProjectAuditMoney" runat="server" JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js"
											JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js" ImageDirectory="../images/infragistics/images/"
											CssClass="infra-input-nember" MinDecimalPlaces="Two" Width="120"></igtxt:webnumericedit>&nbsp;
									</td>
								</tr>
							</table>
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
			<td class="blackbordertd" width="20%" align="right">合同名称：&nbsp;</td>
			<td class="blackbordertdpaddingcontent" width="40%">
				<asp:label id="lblEyeContractName" Runat="server"></asp:label>&nbsp;
			</td>
			<td class="blackbordertd" width="10%" align="right">变更依据&nbsp;&nbsp;&nbsp;&nbsp;<br>
				（AI/SI NO.）</td>
			<td class="blackbordertdpaddingcontent" width="30%">
				<asp:Label id="lblEyeVoucher" runat="server"></asp:Label>&nbsp;
			</td>
		</tr>
		<tr>
			<td class="blackbordertd" align="right">承包人（分包）：&nbsp;</td>
			<td class="blackbordertdpaddingcontent">
				<asp:label id="lblEyeSupplierName" Runat="server"></asp:label>&nbsp;
			</td>
			<td class="blackbordertd" align="right">审批表编号：&nbsp;</td>
			<td class="blackbordertdpaddingcontent">
				<asp:Label ID="lblEyeChangeId" runat="server"></asp:Label>&nbsp;
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
			<td colspan="2" valign="top" height="100%">
				<table cellSpacing="0" cellPadding="0" width="100%" border="0" height="100%">
					<tr>
						<td class="blackbordertd" align="center" width="37" valign="middle" height="100%"><br>
							变<br>
							更<br>
							原<br>
							因<br>
							及<br>
							摘<br>
							要</td>
						<td>
							<table cellSpacing="0" cellPadding="0" width="100%" border="0" height="100%">
								<tr>
									<td class="blackbordertdpaddingcontent" style="HEIGHT:auto">
										<asp:Label ID="lblEyeChangeReason" runat="server"></asp:Label>&nbsp;
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
			<td colspan="2" valign="top" height="100%">
				<table cellSpacing="0" cellPadding="0" width="100%" height="100%" border="0" id="tabEyeMoney"
					runat="server">
					<tr>
						<td class="blackbordertd" width="43%">&nbsp;</td>
						<td class="blackbordertdpaddingcontent" width="57%">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;（ 
							RMB ）</td>
					</tr>
					<tr>
						<td class="blackbordertd" align="right">原合同金额：&nbsp;</td>
						<td class="blackbordertdpaddingcontent">
							<table cellSpacing="0" cellPadding="0" border="0">
								<tr>
									<td width="15" align="center">&nbsp;</td>
									<td>
										<INPUT class="infra-input-nember" id="txtEyeBudgetMoney" style="FLOAT: left; WIDTH: 120px; TEXT-ALIGN: right"
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
									<td width="15" align="center">-</td>
									<td>
										<INPUT class="infra-input-nember" id="txtEyeAdjustMoney" style="FLOAT: left; WIDTH: 120px; TEXT-ALIGN: right"
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
									<td width="15" align="center">=</td>
									<td>
										<INPUT class="infra-input-nember" id="txtEyeOriginalMoney" style="FLOAT: left; WIDTH: 120px; TEXT-ALIGN: right"
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
									<td width="15" align="center">+</td>
									<td>
										<INPUT class="infra-input-nember" id="txtEyeTotalChangeMoney" style="FLOAT: left; WIDTH: 120px; TEXT-ALIGN: right"
											readOnly type="text" name="txtEyeTotalChangeMoney" runat="server">&nbsp;
									</td>
								</tr>
							</table>
						</td>
					</tr>
					<tr>
						<td class="blackbordertd" align="right">本次上报变更金额：&nbsp;</td>
						<td class="blackbordertdpaddingcontent">
							<table cellSpacing="0" cellPadding="0" border="0">
								<tr>
									<td width="15" align="center">+</td>
									<td>
										<INPUT class="infra-input-nember" id="txtEyeChangeMoney" style="WIDTH: 120px; TEXT-ALIGN: right"
											readOnly type="text" name="txtEyeChangeMoney" runat="server">&nbsp;
									</td>
								</tr>
							</table>
						</td>
					</tr>
					<tr>
						<td height="1" bgcolor="black" colspan="2"></td>
					</tr>
					<tr>
						<td class="blackbordertd" align="right">合同总额预计：&nbsp;</td>
						<td class="blackbordertdpaddingcontent">
							<table cellSpacing="0" cellPadding="0" border="0">
								<tr>
									<td width="15" align="center">=</td>
									<td>
										<INPUT class="infra-input-nember" id="txtEyeNewTotalMoney" style="FLOAT: left; WIDTH: 120px; TEXT-ALIGN: right"
											readOnly type="text" name="txtEyeNewTotalMoney" runat="server">&nbsp;
									</td>
								</tr>
							</table>
						</td>
					</tr>
					<tr>
						<td height="1" bgcolor="black" colspan="2"></td>
					</tr>
					<tr>
						<td class="blackbordertd" align="right">承包商本次变更申请金额：&nbsp;</td>
						<td class="blackbordertdpaddingcontent">
							<table cellSpacing="0" cellPadding="0" border="0">
								<tr>
									<td width="15" align="center">&nbsp;</td>
									<td width="120" align="right">
										<asp:Label ID="lblEyeSupplierChangeMoney" Runat="server"></asp:Label>
									</td>
								</tr>
							</table>
						</td>
					</tr>
					<tr>
						<td class="blackbordertd" align="right">顾问估算师审核金额：&nbsp;</td>
						<td class="blackbordertdpaddingcontent">
							<table cellSpacing="0" cellPadding="0" border="0">
								<tr>
									<td width="15" align="center">&nbsp;</td>
									<td width="120" align="right">
										<asp:Label ID="lblEyeConsultantAuditMoney" Runat="server"></asp:Label>
									</td>
								</tr>
							</table>
						</td>
					</tr>
					<tr>
						<td class="blackbordertd" align="right">项目合约部审核金额：&nbsp;</td>
						<td class="blackbordertdpaddingcontent">
							<table cellSpacing="0" cellPadding="0" border="0">
								<tr>
									<td width="15" align="center">&nbsp;</td>
									<td width="120" align="right">
										<asp:Label ID="lblEyeProjectAuditMoney" Runat="server"></asp:Label>
									</td>
								</tr>
							</table>
						</td>
					</tr>
				</table>
			</td>
		</tr>
	</table>
</div>
