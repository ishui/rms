<%@ Page language="c#" Inherits="RmsPM.Web.WorkFlowContral.WorkFlowStart" CodeFile="WorkFlowStart.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>启动流程</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
	</HEAD>
	<body style="BORDER-RIGHT: 0px" scroll="no">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td bgColor="#e4eff6" height="6"></td>
				</tr>
				<tr>
					<td height="25">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="topic" background="../images/topic_bg.gif"><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle"><span id="spanTitle" runat="server">
										流程&nbsp;启动流程</span></td>
								<td width="9"><IMG height="25" src="../images/topic_corr.gif" width="9"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr height="100%">
					<td class="table" vAlign="top" align="center">
						<table width="400" border="0" cellspacing="0" cellpadding="0">
							<tr>
								<td>&nbsp;
								</td>
							</tr>
							<tr>
								<td class="flowbg">
									<table width="100%" border="0" cellspacing="0" cellpadding="0">
										<tr id="cachet" runat="server">
											<td>- <a href="#" onClick="javascript:gourl('../bill/BillManage.aspx');return false;"><strong>
														公章(介绍信)申请</strong></a></td>
										</tr>
										<tr id="ComputerMaintenance" runat="server">
											<td>- <a href="#" onClick="javascript:gourl('../bill/ComputerManage.aspx');return false;">
													<strong>计算机维护</strong></a></td>
										</tr>
										<tr id="Equipment" runat="server">
											<td>- <a href="#" onClick="javascript:gourl('../Bill/EquipmentApplyView.aspx');return false;">
													<strong>IT设备请购</strong></a></td>
										</tr>
										<tr id="VehicleApply" runat="server" style="DISPLAY:none">
											<td>- <a href="#" onClick="javascript:gourl('../oa/vehicle/VehicleApplyadd.aspx');return false;">
													<strong>用车申请</strong></a></td>
										</tr>
										<tr id="Purchase" runat="server">
											<td>- <a href="#" onClick="javascript:gourl('../Purchase/PurchaseManage.aspx');return false;">
													<strong>物资请购</strong></a></td>
										</tr>
										<tr id="ChequeDrow" runat="server" style="DISPLAY:none">
											<td>- <a href="#" onClick="javascript:gourl('../oa/ChequeDrow/ChequeDrowEdit.aspx');return false;">
													<strong>支票领用（借款）单</strong></a></td>
										</tr>
										<tr id="Contract" runat="server">
											<td>- <a href="#" onClick="javascript:gourl('../ContractFlow/ContractModify.aspx?act=Add','contract');return false;">
													<strong>合同申请审核</strong></a></td>
										</tr>
										<tr id="Leave" runat="server">
											<td>- <a href="#" onClick="javascript:gourl('../LeaveManage/LeaveManage.aspx?act=Add');return false;">
													<strong>员工请假</strong></a></td>
										</tr>
										<tr id="Leave1" runat="server">
											<td>- <a href="#" onClick="javascript:gourl('../LeaveManage/LeaveManage1.aspx?act=Add');return false;">
													<strong>员工外出申请</strong></a></td>
										</tr>
										<tr id="Contact" runat="server">
											<td>- <a href="#" onClick="javascript:gourl('../Bill/ContactManage.aspx');return false;">
													<strong>权限修改申请</strong></a></td>
										</tr>
										<tr id="WorkContact" runat="server">
											<td>- <a href="#" onClick="javascript:gourl('../Bill/ContactManage1.aspx');return false;">
													<strong>工作联系单</strong></a></td>
										</tr>
									</table>
									<br>
									<br>
								</td>
							</tr>
							<tr>
								<td align="center"><br>
								</td>
							</tr>
						</table>
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
			</table>
		</form>
		<script>
	function gourl ( url,procedureName )
	{
		OpenFullWindow(url,procedureName);
		
	}
	
		</script>
	</body>
</HTML>
