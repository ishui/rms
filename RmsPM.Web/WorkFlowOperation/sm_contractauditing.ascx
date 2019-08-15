<%@ Control Language="c#" Inherits="WorkFlowOperation_SM_ContractAuditing" CodeFile="SM_ContractAuditing.ascx.cs" %>
<%@ Register TagPrefix="uc1" TagName="AttachMentAdd" Src="../UserControls/AttachMentAdd.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AttachMentList" Src="../UserControls/AttachMentList.ascx" %>
<%@ Register TagPrefix="uc1" TagName="InputSystemGroup" Src="../UserControls/InputSystemGroup.ascx" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
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
	
	function doSelectSupplier()
	{
		var sClintID = '<% =this.ClientID.ToString() %>';

		var supplierCode = document.all(sClintID + "_txtOperSupplierCode").value;
		OpenLargeWindow('../SelectBox/SelectSupplier.aspx?SupplierCode=' + supplierCode ,'选择供应商');
	}

    function doSelectSupplier2()
    {
	    var supplierCode = Form1.txtSupplier2Code.value;
	    OpenLargeWindow('../SelectBox/SelectSupplier.aspx?SupplierCode=' + supplierCode + '&returnFunctionName=1' ,'选择供应商');
    }

	function DoSelectSupplierReturn ( code,name )
	{
		var sClintID = '<% =this.ClientID.ToString() %>';

		document.all(sClintID + "_txtOperSupplierCode").value = code;
		document.all(sClintID + "_txtOperSupplierName").value = name;
	}
	
	function DoSelectSupplierReturn1 ( code,name )
	{
		var sClintID = '<% =this.ClientID.ToString() %>';

		document.all(sClintID + "_txtOperSupplier2Code").value = code;
		document.all(sClintID + "_txtOperSupplier2Name").value = name;
	}

//-->
</script>
<div id="OperableDiv" runat="server" onkeydown="if(event.keyCode==13) event.keyCode=9">
	<table cellSpacing="0" cellPadding="0" width="100%" border="0">
		<tr>
			<td class="blackbordertd" width="20%" align="right">项目名称：&nbsp;</td>
			<td class="blackbordertdpaddingcontent" width="30%">
				<asp:label id="lblOperProjectName" Runat="server"></asp:label>&nbsp;
			</td>
			<td class="blackbordertd" width="20%" align="right">合同编号：&nbsp;</td>
			<td class="blackbordertdpaddingcontent" width="30%">
				<INPUT class="input" id="txtOperContractID" type="text" name="txtOperContractID" runat="server" readonly>
			</td>
		</tr>
		<tr>
			<td class="blackbordertd" align="right">合同名称：&nbsp;</td>
			<td class="blackbordertdpaddingcontent">
			    <asp:label id="lblOperContractName" Runat="server"></asp:label>&nbsp;
			</td>
			<td class="blackbordertd" align="right">合同类型：&nbsp;</td>
			<td class="blackbordertdpaddingcontent">
				<uc1:inputsystemgroup id="inputOperSystemGroup" runat="server"></uc1:inputsystemgroup><font color="red">*</font>
			</td>
		</tr>
		<tr>
			<td class="blackbordertd" align="right">签约单位（承包人）：&nbsp;</td>
			<td class="blackbordertdpaddingcontent">
				<INPUT class="input" id="txtOperSupplierName" readOnly type="text" size="32" name="txtOperSupplierName"
					runat="server"> <FONT color="#ff0000">*</FONT> <INPUT id="txtOperSupplierCode" style="WIDTH: 18px; HEIGHT: 22px" type="hidden" name="txtOperSupplierCode"
					runat="server"> <A onclick="doSelectSupplier();return false;" href="##"><IMG src="../images/ToolsItemSearch.gif" border="0"></A>
			</td>
			<td class="blackbordertd" align="right">预计签约日期：&nbsp;</td>
			<td class="blackbordertdpaddingcontent">
				<cc3:calendar id="OperContractDate" runat="server" CalendarResource="../Images/CalendarResource/"
					ReadOnly="False" Display="True"></cc3:calendar><font color="red">*</font>
			</td>
		</tr>
		<tr>
			<td class="blackbordertd" align="right">签约单位（总承包）：&nbsp;</td>
			<td class="blackbordertdpaddingcontent" colspan="3">
				<INPUT class="input" id="txtOperSupplier2Name" readOnly type="text" size="32" name="txtOperSupplier2Name"
					runat="server"> <input id="txtOperSupplier2Code" style="WIDTH: 18px; HEIGHT: 22px" type="hidden" name="txtOperSupplierCode"
					runat="server"> <A onclick="doSelectSupplier2();return false;" href="##"><IMG src="../images/ToolsItemSearch.gif" border="0"></A>
			</td>
		
		</tr>
		<tr>
			<td colspan="3" valign="top" height="200">
				<table cellSpacing="0" cellPadding="0" width="100%" border="0" height="100%">
					<tr>
						<td class="blackbordertd" align="center" width="37" valign="middle" height="100%">
							<br>
							合<br>
							同<br>
							概<br>
							述<br>
						</td>
						<td>
							<table cellSpacing="0" cellPadding="0" width="100%" border="0" height="100%">
								<tr>
									<td class="blackbordertdpaddingcontent" style="HEIGHT:auto">
										<TEXTAREA id="txtOperContractObject" style="WIDTH: 100%" name="txtOperContractObject" rows="10"
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
			<td >
				<table cellSpacing="0" cellPadding="0" width="100%" height="100%" border="0" id="tabOperMoney"
					runat="server">
					<tr>
						<td class="blackbordertd" width="43%">&nbsp;</td>
						<td class="blackbordertdpaddingcontent" width="57%" align="center"><asp:Label ID="lblOperMoneyType" runat="server"></asp:Label></td>
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
									<td><INPUT class="infra-input-nember" id="txtOperOriginalMoney" style="FLOAT: left; WIDTH: 120px; TEXT-ALIGN: right"
											readOnly type="text" name="txtOperOriginalMoney" runat="server">&nbsp;</td>
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
											readOnly type="text" name="txtOperNewTotalMoney" runat="server">
									</td>
								</tr>
							</table>
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr>
			<td height="1" bgcolor="black" colspan="4"></td>
		</tr>
	</table>
</div>
<div id="EyeableDiv" runat="server">
	<table cellSpacing="0" cellPadding="0" width="100%" border="0">
		<tr>
			<td class="blackbordertd" width="20%" align="right">项目名称：&nbsp;</td>
			<td class="blackbordertdpaddingcontent" width="30%">
				<asp:label id="lblEyeProjectName" Runat="server"></asp:label>&nbsp;
			</td>
			<td class="blackbordertd" width="20%" align="right">合同编号：&nbsp;</td>
			<td class="blackbordertdpaddingcontent" width="30%">
				<asp:label id="lblEyeContractID" Runat="server"></asp:label>&nbsp;
			</td>
		</tr>
		<tr>
			<td class="blackbordertd" align="right">合同名称：&nbsp;</td>
			<td class="blackbordertdpaddingcontent">
				<asp:label id="lblEyeContractName" Runat="server"></asp:label>&nbsp;
			</td>
			<td class="blackbordertd" align="right">合同类型：&nbsp;</td>
			<td class="blackbordertdpaddingcontent">
				<asp:label id="lblEyeSystemGroupName" Runat="server"></asp:label>&nbsp;
			</td>
		</tr>
		<tr>
			<td class="blackbordertd" align="right">签约单位（承包人）：&nbsp;</td>
			<td class="blackbordertdpaddingcontent">
				<asp:label id="lblEyeSupplierName" Runat="server"></asp:label>&nbsp;
			</td>
			<td class="blackbordertd" align="right">预计签约日期：&nbsp;</td>
			<td class="blackbordertdpaddingcontent">
				<asp:label id="lblEyeContractDate" Runat="server"></asp:label>&nbsp;
			</td>
		</tr>
		<tr>
			<td class="blackbordertd" align="right">签约单位（总承包）：&nbsp;</td>
			<td class="blackbordertdpaddingcontent" colspan="3">
				<asp:label id="lblEyeSupplier2Name" Runat="server"></asp:label>&nbsp;
			</td>
			
		</tr>		
		<tr>
			<td colspan="3" valign="top" height="200">
				<table cellSpacing="0" cellPadding="0" width="100%" border="0" height="100%">
					<tr>
						<td class="blackbordertd" align="center" width="37" valign="middle" height="100%">
							<br>
							合<br>
							同<br>
							概<br>
							述<br>
						</td>
						<td>
							<table cellSpacing="0" cellPadding="0" width="100%" border="0" height="100%">
								<tr>
									<td class="blackbordertdpaddingcontent" style="HEIGHT:auto">
										<asp:Label ID="lblEyeContractObject" runat="server"></asp:Label>&nbsp;
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
			<td>
				<table cellSpacing="0" cellPadding="0" width="100%" height="100%" border="0" id="tabEyeMoney"
					runat="server">
					<tr>
						<td class="blackbordertd" width="43%">&nbsp;</td>
						<td class="blackbordertdpaddingcontent" width="57%" align="center"><asp:Label ID="lblEyeMoneyType" runat="server"></asp:Label></td>
					</tr>
					<tr>
						<td class="blackbordertd" align="right" >原合同金额：&nbsp;</td>
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
									<td><INPUT class="infra-input-nember" id="txtEyeOriginalMoney" style="FLOAT: left; WIDTH: 120px; TEXT-ALIGN: right"
											readOnly type="text" name="txtEyeOriginalMoney" runat="server">&nbsp;</td>
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
											readOnly type="text" name="txtEyeNewTotalMoney" runat="server">
									</td>
								</tr>
							</table>
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr>
			<td height="1" bgcolor="black" colspan="4"></td>
		</tr>
	</table>
</div>
