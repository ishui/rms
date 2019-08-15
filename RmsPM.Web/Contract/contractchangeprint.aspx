<%@ Page language="c#" Inherits="RmsPM.Web.Contract.ContractChangePrint" CodeFile="ContractChangePrint.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>变更审批表</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<LINK href="../Images/infra.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td height="25">
						<table class="table" id="tableToolBar" width="100%">
							<tr>
								<td class="tools-area" width="16"><IMG src="../images/btn_li.gif" align="absMiddle"></td>
								<td class="tools-area">
									<input class="button" onclick="DoPrint();return false;" type="button" value="打印" name="btnAccount">
									<input class="button" type="button" value="关闭" NAME="" onclick="window.close();">
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td vAlign="top" id="td_Print" runat="server">
						<TABLE class="blackbordertable" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="blackbordertd" align="center" colspan="4"><br>
									<font size="3"><STRONG>变更审批表</STRONG></font><br>
									<br>
								</td>
							</tr>
							<TR>
								<td class="blackbordertd" width="20%" align="right">项目名称：&nbsp;&nbsp;</td>
								<td class="blackbordertdpaddingcontent" width="30%"><asp:Label ID="lblProjectName" Runat="server"></asp:Label>&nbsp;</td>
								<td class="blackbordertd" width="20%" align="right">合同编号：&nbsp;&nbsp;</td>
								<td class="blackbordertdpaddingcontent" width="30%"><asp:Label ID="lblContractID" Runat="server"></asp:Label>&nbsp;</td>
							</TR>
							<tr>
								<td class="blackbordertd" align="right">合同名称：&nbsp;&nbsp;</td>
								<td class="blackbordertdpaddingcontent"><asp:Label ID="lblContractName" Runat="server"></asp:Label>&nbsp;</td>
								<td class="blackbordertd" align="right">&nbsp;&nbsp;变更依据：&nbsp;&nbsp;<br>
									（AI/SI NO.）&nbsp;</td>
								<td class="blackbordertdpaddingcontent"><asp:Label ID="lblVoucher" Runat="server"></asp:Label>&nbsp;</td>
							</tr>
							<tr>
								<td class="blackbordertd" align="right">签约单位：&nbsp;&nbsp;</td>
								<td class="blackbordertdpaddingcontent"><asp:Label ID="lblSupplierName" Runat="server"></asp:Label>&nbsp;</td>
								<td class="blackbordertd" align="right">审批表编号：&nbsp;&nbsp;</td>
								<td class="blackbordertdpaddingcontent"><asp:Label ID="lblChangeID" Runat="server"></asp:Label>&nbsp;</td>
							</tr>
							<TR>
								<td colspan="2" height="100%">
									<table cellSpacing="0" cellPadding="0" width="100%" border="0" height="100%">
										<tr>
											<TD class="blackbordertd" width="5%" style="VERTICAL-ALIGN: middle; TEXT-ALIGN: center"
												rowspan="2">
												<BR>
												变<BR>
												更<BR>
												原<BR>
												因<BR>
												及<BR>
												摘<BR>
												要<BR>
											</TD>
											<td class="blackbordertdpaddingcontent" style="HEIGHT:auto">
												（详细内容可另附）<br>
												<asp:Label ID="lblChangeReason" Runat="server"></asp:Label>
											</td>
										</tr>
										<tr>
											<td class="blackbordertdpaddingcontent">
												附件：<br>
												&nbsp;&nbsp;
												<asp:CheckBox ID="chkAttachment1" Runat="server"></asp:CheckBox>1. 
												工程指示XXXX或签证XXXX<br>
												&nbsp;&nbsp;
												<asp:CheckBox ID="chkAttachment2" Runat="server"></asp:CheckBox>2. 承包商结算申请<br>
												&nbsp;&nbsp;
												<asp:CheckBox ID="chkAttachment3" Runat="server"></asp:CheckBox>3. 顾问估算师审核意见<br>
												&nbsp;&nbsp;
												<asp:CheckBox ID="chkAttachment5" Runat="server"></asp:CheckBox>4. 其他部门、顾问审核意见<br>
												&nbsp;&nbsp;
												<asp:CheckBox ID="chkAttachment6" Runat="server"></asp:CheckBox>5. 变更总汇
											</td>
										</tr>
									</table>
								</td>
								<td colspan="2" valign="top">
									<table cellSpacing="0" cellPadding="0" width="100%" border="0">
										<tr>
											<td class="blackbordertd" width="40%">&nbsp;</td>
											<td class="blackbordertdcontent">RMB</td>
										</tr>
										<tr>
											<td class="blackbordertd" align="right">原合同金额：&nbsp;&nbsp;</td>
											<td class="blackbordertd" align="left">
												<table cellSpacing="0" cellPadding="0" border="0" width="200">
													<tr>
														<td align="right"><asp:Label id="lblTotalMoney" Runat="server"></asp:Label>&nbsp;</td>
													</tr>
												</table>
											</td>
										</tr>
										<tr>
											<td class="blackbordertd" align="right">累计已批变更：&nbsp;&nbsp;</td>
											<td class="blackbordertd" align="left">
												<table cellSpacing="0" cellPadding="0" border="0" width="200">
													<tr>
														<td align="right"><asp:Label id="lblTotalChangeMoney" Runat="server"></asp:Label>&nbsp;</td>
													</tr>
												</table>
											</td>
										</tr>
										<tr>
											<td class="blackbordertd" align="right">本次上报变更金额：&nbsp;&nbsp;</td>
											<td class="blackbordertd" align="left">
												<table cellSpacing="0" cellPadding="0" border="0" width="200">
													<tr>
														<td align="right"><asp:Label id="lblChangeMoney" Runat="server"></asp:Label>&nbsp;</td>
													</tr>
												</table>
											</td>
										</tr>
										<tr>
											<td class="blackbordertd" align="right">合计（估计结算金额）：&nbsp;&nbsp;</td>
											<td class="blackbordertd" align="left">
												<table cellSpacing="0" cellPadding="0" border="0" width="200">
													<tr>
														<td align="right"><asp:Label id="lblNewTotalMoney" Runat="server"></asp:Label>&nbsp;</td>
													</tr>
												</table>
											</td>
										</tr>
										<tr>
											<td colspan="4" bgcolor="black" height="1"></td>
										</tr>
										<tr>
											<td class="blackbordertd" align="right">承包商本次变更申请金额：&nbsp;&nbsp;</td>
											<td class="blackbordertd" align="left">
												<table cellSpacing="0" cellPadding="0" border="0" width="200">
													<tr>
														<td align="right"><asp:Label id="lblSupplierChangeMoney" Runat="server"></asp:Label>&nbsp;</td>
													</tr>
												</table>
											</td>
										</tr>
										<tr>
											<td class="blackbordertd" align="right">顾问估算师审核金额：&nbsp;&nbsp;</td>
											<td class="blackbordertd" align="left">
												<table cellSpacing="0" cellPadding="0" border="0" width="200">
													<tr>
														<td align="right"><asp:Label id="lblConsultantAuditMoney" Runat="server"></asp:Label>&nbsp;</td>
													</tr>
												</table>
											</td>
										</tr>
										<tr>
											<td class="blackbordertd" align="right">项目合约部审核金额：&nbsp;&nbsp;</td>
											<td class="blackbordertd" align="left">
												<table cellSpacing="0" cellPadding="0" border="0" width="200">
													<tr>
														<td align="right"><asp:Label id="lblProjectAuditMoney" Runat="server"></asp:Label>&nbsp;</td>
													</tr>
												</table>
											</td>
										</tr>
										<tr>
											<td colspan="4" bgcolor="black" height="1"></td>
										</tr>
										<tr>
											<td colspan="4" class="blackbordertd" style="HEIGHT:129px">
												<table cellSpacing="0" cellPadding="0" width="100%" border="0" height="100%">
													<tr>
														<td valign="top">&nbsp;</td>
													</tr>
													<tr>
														<td height="20" width="60%">&nbsp;&nbsp;主管估算师：</td>
														<td width="40%">日期：</td>
													</tr>
												</table>
											</td>
										</tr>
										<tr>
											<td colspan="4" class="blackbordertd" style="HEIGHT:129px">
												<table cellSpacing="0" cellPadding="0" width="100%" border="0" height="100%">
													<tr>
														<td valign="top">&nbsp;</td>
													</tr>
													<tr>
														<td height="20" width="60%">&nbsp;&nbsp;项目部经理：</td>
														<td width="40%">日期：</td>
													</tr>
												</table>
											</td>
										</tr>
									</table>
								</td>
							</TR>
							<tr>
								<td colspan="4">
									<table cellSpacing="0" cellPadding="0" width="100%" border="0">
										<TR>
											<TD class="blackbordertd" width="5%" style="HEIGHT: 130px; TEXT-ALIGN: center">
												<BR>
												<BR>
												项<BR>
												目<BR>
												部<BR>
											</TD>
											<TD width="16%" class="blackbordertd">&nbsp;</TD>
											<TD width="16%" class="blackbordertd">&nbsp;</TD>
											<TD width="16%" class="blackbordertd">&nbsp;</TD>
											<TD width="16%" class="blackbordertd">&nbsp;</TD>
											<TD width="16%" class="blackbordertd">&nbsp;</TD>
											<TD width="16%" class="blackbordertd">&nbsp;</TD>
										</TR>
										<TR>
											<TD class="blackbordertd" style="HEIGHT: 130px; TEXT-ALIGN: center">
												<BR>
												<BR>
												房<BR>
												产<BR>
												部<BR>
											</TD>
											<TD colspan="6" class="blackbordertd">
												<table cellSpacing="0" cellPadding="0" width="100%" border="0" height="100%">
													<tr>
														<td colspan="3" valign="top">&nbsp;意见：</td>
													</tr>
													<tr>
														<td height="20" width="60%"></td>
														<td width="20%">签名：</td>
														<td width="20%">日期：</td>
													</tr>
												</table>
											</TD>
										</TR>
										<TR>
											<TD class="blackbordertd" style="HEIGHT: 130px; TEXT-ALIGN: center">
												<BR>
												总<BR>
												部<BR>
												财<BR>
												务<BR>
											</TD>
											<TD colspan="3" class="blackbordertd">
												<table cellSpacing="0" cellPadding="0" width="100%" border="0" height="100%">
													<tr>
														<td colspan="3" valign="top">&nbsp;财务部：</td>
													</tr>
													<tr>
														<td height="20" width="60%"></td>
														<td width="40%">签名：</td>
													</tr>
												</table>
											</TD>
											<TD colspan="3" class="blackbordertd">
												<table cellSpacing="0" cellPadding="0" width="100%" border="0" height="100%">
													<tr>
														<td colspan="3" valign="top">&nbsp;财务总监：</td>
													</tr>
													<tr>
														<td height="20" width="60%"></td>
														<td width="40%">签名：</td>
													</tr>
												</table>
											</TD>
										</TR>
										<TR>
											<TD class="blackbordertd" style="HEIGHT: 130px; TEXT-ALIGN: center">
												<BR>
												公<BR>
												司<BR>
												领<BR>
												导<BR>
											</TD>
											<TD colspan="2" class="blackbordertd">
												<table cellSpacing="0" cellPadding="0" width="100%" border="0" height="100%">
													<tr>
														<td colspan="3" valign="top">&nbsp;副总：</td>
													</tr>
													<tr>
														<td height="20" width="60%"></td>
														<td width="40%">签名：</td>
													</tr>
												</table>
											</TD>
											<TD colspan="2" class="blackbordertd">
												<table cellSpacing="0" cellPadding="0" width="100%" border="0" height="100%">
													<tr>
														<td colspan="3" valign="top">&nbsp;总经理：</td>
													</tr>
													<tr>
														<td height="20" width="60%"></td>
														<td width="40%">签名：</td>
													</tr>
												</table>
											</TD>
											<TD colspan="2" class="blackbordertd">
												<table cellSpacing="0" cellPadding="0" width="100%" border="0" height="100%">
													<tr>
														<td colspan="3" valign="top">&nbsp;董事长：</td>
													</tr>
													<tr>
														<td height="20" width="60%"></td>
														<td width="40%">签名：</td>
													</tr>
												</table>
											</TD>
										</TR>
									</table>
								</td>
							</tr>
						</TABLE>
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
					<td height="6" bgcolor="#e4eff6"></td>
				</tr>
			</table>
		</form>
		<script language="javascript">
<!--
	
	function DoPrint()
	{
		OpenPrintWindow("../Report/PrintList.aspx?FromControlID=td_Print", "打印");
	}	

//-->
		</script>
	</body>
</HTML>
