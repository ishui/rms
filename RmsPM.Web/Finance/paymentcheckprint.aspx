<%@ Page language="c#" Inherits="RmsPM.Web.Finance.PaymentCheckPrint" CodeFile="PaymentCheckPrint.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>付款审批表</title>
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
									<font size="3"><STRONG>付款审批表</STRONG></font><br>
									<br>
								</td>
							</tr>
							<TR>
								<td class="blackbordertd" width="10%" align="right">项目名称：&nbsp;&nbsp;</td>
								<td class="blackbordertdpaddingcontent" width="60%"><asp:Label ID="lblProjectName" Runat="server"></asp:Label>&nbsp;</td>
								<td class="blackbordertd" width="10%" align="right">付款期数：&nbsp;&nbsp;</td>
								<td class="blackbordertdpaddingcontent" width="20%">&nbsp;&nbsp;&nbsp;&nbsp;第&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;期</td>
							</TR>
							<tr>
								<td class="blackbordertd" align="right">合同名称：&nbsp;&nbsp;</td>
								<td class="blackbordertdpaddingcontent"><asp:Label ID="lblContractName" Runat="server"></asp:Label>&nbsp;</td>
								<td class="blackbordertd" align="right">合同编号：&nbsp;&nbsp;</td>
								<td class="blackbordertdpaddingcontent" width="30%"><asp:Label ID="lblContractID" Runat="server"></asp:Label>&nbsp;</td>
							</tr>
							<tr>
								<td class="blackbordertd" align="right">收款单位：&nbsp;&nbsp;</td>
								<td class="blackbordertdpaddingcontent" colspan="3"><asp:Label ID="lblSupplierName" Runat="server"></asp:Label>&nbsp;</td>
							</tr>
							<TR>
								<td colspan="4">
									<table cellSpacing="0" cellPadding="0" width="100%" border="0">
										<tr>
											<td height="100%" width="50%">
												<table cellSpacing="0" cellPadding="0" width="100%" border="0" height="100%">
													<tr>
														<td class="blackbordertdpaddingcontent" style="HEIGHT:auto">
															合同履约情况：<br>
															<asp:Label ID="lblCheckOpinion" Runat="server"></asp:Label>
														</td>
													</tr>
													<tr>
														<td class="blackbordertdpaddingcontent">
															附件：<br>
															&nbsp;&nbsp;
															<asp:CheckBox ID="chkAttachment1" Runat="server"></asp:CheckBox>1. 承包商的付款申请<br>
															&nbsp;&nbsp;
															<asp:CheckBox ID="chkAttachment2" Runat="server"></asp:CheckBox>2. 顾问估算师的付款证书<br>
															&nbsp;&nbsp;
															<asp:CheckBox ID="chkAttachment3" Runat="server"></asp:CheckBox>3. 付款记录（累计表）<br>
															&nbsp;&nbsp;
															<asp:CheckBox ID="chkAttachment4" Runat="server"></asp:CheckBox>4. 监理意见<br>
															&nbsp;&nbsp;
															<asp:CheckBox ID="chkAttachment5" Runat="server"></asp:CheckBox>5. 
															工程部/建筑开发部之专业意见<br>
															&nbsp;&nbsp;
															<asp:CheckBox ID="chkAttachment6" Runat="server"></asp:CheckBox>6. 发票<br>
															&nbsp;&nbsp;
															<asp:CheckBox ID="chkAttachment7" Runat="server"></asp:CheckBox>7. 其他：<br>
														</td>
													</tr>
												</table>
											</td>
											<td width="50%">
												<table cellSpacing="0" cellPadding="0" width="100%" border="0">
													<tr>
														<td class="blackbordertd" width="40%">&nbsp;</td>
														<td class="blackbordertdcontent">RMB</td>
													</tr>
													<tr>
														<td class="blackbordertd" align="right">合同金额：&nbsp;&nbsp;</td>
														<td class="blackbordertd" align="left">
															<table cellSpacing="0" cellPadding="0" border="0" width="200">
																<tr>
																	<td align="right"><asp:Label id="lblTotalMoney" Runat="server"></asp:Label>&nbsp;</td>
																</tr>
															</table>
														</td>
													</tr>
													<tr>
														<td class="blackbordertd" align="right">已批变更：&nbsp;&nbsp;</td>
														<td class="blackbordertd" align="left">
															<table cellSpacing="0" cellPadding="0" border="0" width="200">
																<tr>
																	<td align="right"><asp:Label id="lblTotalChangeMoney" Runat="server"></asp:Label>&nbsp;</td>
																</tr>
															</table>
														</td>
													</tr>
													<tr>
														<td class="blackbordertd" align="right">未批变更(估计)：&nbsp;&nbsp;</td>
														<td class="blackbordertd" align="left">
															<table cellSpacing="0" cellPadding="0" border="0" width="200">
																<tr>
																	<td align="right"><asp:Label id="lblChangeMoney" Runat="server"></asp:Label>&nbsp;</td>
																</tr>
															</table>
														</td>
													</tr>
													<tr>
														<td class="blackbordertd" align="right">合同总额预计：&nbsp;&nbsp;</td>
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
														<td class="blackbordertd" align="right">累计应付款：&nbsp;&nbsp;</td>
														<td class="blackbordertd" align="left">
															<table cellSpacing="0" cellPadding="0" border="0" width="200">
																<tr>
																	<td align="right"><asp:Label id="lblTotalPayMoney" Runat="server"></asp:Label>&nbsp;</td>
																</tr>
															</table>
														</td>
													</tr>
													<tr>
														<td class="blackbordertd" align="right">减累计已批款：&nbsp;&nbsp;</td>
														<td class="blackbordertd" align="left">
															<table cellSpacing="0" cellPadding="0" border="0" width="200">
																<tr>
																	<td align="right"><asp:Label id="lblNegAHMoney" Runat="server"></asp:Label>&nbsp;</td>
																</tr>
															</table>
														</td>
													</tr>
													<tr>
														<td class="blackbordertd" align="right">本期应付款：&nbsp;&nbsp;</td>
														<td class="blackbordertd" align="left">
															<table cellSpacing="0" cellPadding="0" border="0" width="200">
																<tr>
																	<td align="right"><asp:Label id="lblTotalItemMoney" Runat="server"></asp:Label>&nbsp;</td>
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
																	<td height="20" width="60%">&nbsp;&nbsp;估算师（审价单位）：</td>
																	<td width="40%">日期：</td>
																</tr>
															</table>
														</td>
													</tr>
												</table>
											</td>
										</tr>
										<tr>
											<td colspan="2" class="blackbordertd">
												<table cellpadding="0" cellspacing="0" border="0" width="100%">
													<tr>
														<td colspan="3">&nbsp;&nbsp;付款方式：&nbsp;支票/贷记/电汇/其他
														</td>
													</tr>
													<tr>
														<td width="50%">&nbsp;&nbsp;收款银行名称：__________________________（电汇适用）</td>
														<td width="30%">承包单位付款申请日期：</td>
														<td width="20%">付款期计算方式：</td>
													</tr>
													<tr>
														<td>&nbsp;&nbsp;收款单位银行帐号：__________________________（电汇适用）</td>
														<td>最晚付款日期:</td>
														<td>例如:21+28天</td>
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
											<TD class="blackbordertd" width="5%" style="VERTICAL-ALIGN: middle; HEIGHT: 200px; TEXT-ALIGN: center">
												<BR>
												项<BR>
												目<BR>
												部<BR>
											</TD>
											<TD width="95%" colspan="3">
												<table cellpadding="0" cellspacing="0" border="0" width="100%" height="100%">
													<tr>
														<td class="blackbordertd">
															<table cellSpacing="0" cellPadding="0" width="100%" border="0" height="100%">
																<tr>
																	<td colspan="3" valign="top">&nbsp;意见：</td>
																</tr>
																<tr>
																	<td height="20" width="60%"></td>
																	<td width="20%">经办人：</td>
																	<td width="20%">日期：</td>
																</tr>
															</table>
														</td>
														<td class="blackbordertd">
															<table cellSpacing="0" cellPadding="0" width="100%" border="0" height="100%">
																<tr>
																	<td colspan="3" valign="top">&nbsp;意见：</td>
																</tr>
																<tr>
																	<td height="20" width="60%"></td>
																	<td width="20%">经理：</td>
																	<td width="20%">日期：</td>
																</tr>
															</table>
														</td>
													</tr>
													<tr>
														<td class="blackbordertd" width="50%">
															<table cellSpacing="0" cellPadding="0" width="100%" border="0" height="100%">
																<tr>
																	<td colspan="3" valign="top">&nbsp;意见：</td>
																</tr>
																<tr>
																	<td height="20" width="60%"></td>
																	<td width="20%">核价：</td>
																	<td width="20%">日期：</td>
																</tr>
															</table>
														</td>
														<td class="blackbordertd" width="50%">
															<table cellSpacing="0" cellPadding="0" width="100%" border="0" height="100%">
																<tr>
																	<td colspan="3" valign="top">&nbsp;意见：</td>
																</tr>
																<tr>
																	<td height="20" width="60%"></td>
																	<td width="20%">财务总监：</td>
																	<td width="20%">日期：</td>
																</tr>
															</table>
														</td>
													</tr>
												</table>
											</TD>
										</TR>
										<TR>
											<TD class="blackbordertd" style="VERTICAL-ALIGN: middle;HEIGHT: 130px;TEXT-ALIGN: center">
												<BR>
												总<BR>
												部<BR>
												财<BR>
												务<BR>
											</TD>
											<td class="blackbordertd">
												<table cellSpacing="0" cellPadding="0" width="100%" border="0" height="100%">
													<tr>
														<td colspan="3" valign="top">&nbsp;经办人：</td>
													</tr>
													<tr>
														<td height="20"></td>
														<td width="40%">签字：</td>
														<td width="40%">日期：</td>
													</tr>
												</table>
											</td>
											<td class="blackbordertd">
												<table cellSpacing="0" cellPadding="0" width="100%" border="0" height="100%">
													<tr>
														<td colspan="3" valign="top">&nbsp;财务部经理：</td>
													</tr>
													<tr>
														<td height="20"></td>
														<td width="40%">签字：</td>
														<td width="40%">日期：</td>
													</tr>
												</table>
											</td>
											<td class="blackbordertd">
												<table cellSpacing="0" cellPadding="0" width="100%" border="0" height="100%">
													<tr>
														<td colspan="3" valign="top">&nbsp;财务总监：</td>
													</tr>
													<tr>
														<td height="20"></td>
														<td width="40%">签字：</td>
														<td width="40%">日期：</td>
													</tr>
												</table>
											</td>
										</TR>
										<TR>
											<TD class="blackbordertd" style="HEIGHT: 130px; TEXT-ALIGN: center">
												<BR>
												公<BR>
												司<BR>
												领<BR>
												导<BR>
											</TD>
											<td class="blackbordertd">
												<table cellSpacing="0" cellPadding="0" width="100%" border="0" height="100%">
													<tr>
														<td colspan="3" valign="top">&nbsp;副总：</td>
													</tr>
													<tr>
														<td height="20"></td>
														<td width="40%">签字：</td>
														<td width="40%">日期：</td>
													</tr>
												</table>
											</td>
											<td class="blackbordertd">
												<table cellSpacing="0" cellPadding="0" width="100%" border="0" height="100%">
													<tr>
														<td colspan="3" valign="top">&nbsp;总经理：</td>
													</tr>
													<tr>
														<td height="20"></td>
														<td width="40%">签字：</td>
														<td width="40%">日期：</td>
													</tr>
												</table>
											</td>
											<td class="blackbordertd">
												<table cellSpacing="0" cellPadding="0" width="100%" border="0" height="100%">
													<tr>
														<td colspan="3" valign="top">&nbsp;董事长：</td>
													</tr>
													<tr>
														<td height="20"></td>
														<td width="40%">签字：</td>
														<td width="40%">日期：</td>
													</tr>
												</table>
											</td>
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
