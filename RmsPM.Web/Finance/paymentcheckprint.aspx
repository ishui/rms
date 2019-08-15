<%@ Page language="c#" Inherits="RmsPM.Web.Finance.PaymentCheckPrint" CodeFile="PaymentCheckPrint.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>����������</title>
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
									<input class="button" onclick="DoPrint();return false;" type="button" value="��ӡ" name="btnAccount">
									<input class="button" type="button" value="�ر�" NAME="" onclick="window.close();">
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
									<font size="3"><STRONG>����������</STRONG></font><br>
									<br>
								</td>
							</tr>
							<TR>
								<td class="blackbordertd" width="10%" align="right">��Ŀ���ƣ�&nbsp;&nbsp;</td>
								<td class="blackbordertdpaddingcontent" width="60%"><asp:Label ID="lblProjectName" Runat="server"></asp:Label>&nbsp;</td>
								<td class="blackbordertd" width="10%" align="right">����������&nbsp;&nbsp;</td>
								<td class="blackbordertdpaddingcontent" width="20%">&nbsp;&nbsp;&nbsp;&nbsp;��&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;��</td>
							</TR>
							<tr>
								<td class="blackbordertd" align="right">��ͬ���ƣ�&nbsp;&nbsp;</td>
								<td class="blackbordertdpaddingcontent"><asp:Label ID="lblContractName" Runat="server"></asp:Label>&nbsp;</td>
								<td class="blackbordertd" align="right">��ͬ��ţ�&nbsp;&nbsp;</td>
								<td class="blackbordertdpaddingcontent" width="30%"><asp:Label ID="lblContractID" Runat="server"></asp:Label>&nbsp;</td>
							</tr>
							<tr>
								<td class="blackbordertd" align="right">�տλ��&nbsp;&nbsp;</td>
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
															��ͬ��Լ�����<br>
															<asp:Label ID="lblCheckOpinion" Runat="server"></asp:Label>
														</td>
													</tr>
													<tr>
														<td class="blackbordertdpaddingcontent">
															������<br>
															&nbsp;&nbsp;
															<asp:CheckBox ID="chkAttachment1" Runat="server"></asp:CheckBox>1. �а��̵ĸ�������<br>
															&nbsp;&nbsp;
															<asp:CheckBox ID="chkAttachment2" Runat="server"></asp:CheckBox>2. ���ʹ���ʦ�ĸ���֤��<br>
															&nbsp;&nbsp;
															<asp:CheckBox ID="chkAttachment3" Runat="server"></asp:CheckBox>3. �����¼���ۼƱ�<br>
															&nbsp;&nbsp;
															<asp:CheckBox ID="chkAttachment4" Runat="server"></asp:CheckBox>4. �������<br>
															&nbsp;&nbsp;
															<asp:CheckBox ID="chkAttachment5" Runat="server"></asp:CheckBox>5. 
															���̲�/����������֮רҵ���<br>
															&nbsp;&nbsp;
															<asp:CheckBox ID="chkAttachment6" Runat="server"></asp:CheckBox>6. ��Ʊ<br>
															&nbsp;&nbsp;
															<asp:CheckBox ID="chkAttachment7" Runat="server"></asp:CheckBox>7. ������<br>
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
														<td class="blackbordertd" align="right">��ͬ��&nbsp;&nbsp;</td>
														<td class="blackbordertd" align="left">
															<table cellSpacing="0" cellPadding="0" border="0" width="200">
																<tr>
																	<td align="right"><asp:Label id="lblTotalMoney" Runat="server"></asp:Label>&nbsp;</td>
																</tr>
															</table>
														</td>
													</tr>
													<tr>
														<td class="blackbordertd" align="right">���������&nbsp;&nbsp;</td>
														<td class="blackbordertd" align="left">
															<table cellSpacing="0" cellPadding="0" border="0" width="200">
																<tr>
																	<td align="right"><asp:Label id="lblTotalChangeMoney" Runat="server"></asp:Label>&nbsp;</td>
																</tr>
															</table>
														</td>
													</tr>
													<tr>
														<td class="blackbordertd" align="right">δ�����(����)��&nbsp;&nbsp;</td>
														<td class="blackbordertd" align="left">
															<table cellSpacing="0" cellPadding="0" border="0" width="200">
																<tr>
																	<td align="right"><asp:Label id="lblChangeMoney" Runat="server"></asp:Label>&nbsp;</td>
																</tr>
															</table>
														</td>
													</tr>
													<tr>
														<td class="blackbordertd" align="right">��ͬ�ܶ�Ԥ�ƣ�&nbsp;&nbsp;</td>
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
														<td class="blackbordertd" align="right">�ۼ�Ӧ���&nbsp;&nbsp;</td>
														<td class="blackbordertd" align="left">
															<table cellSpacing="0" cellPadding="0" border="0" width="200">
																<tr>
																	<td align="right"><asp:Label id="lblTotalPayMoney" Runat="server"></asp:Label>&nbsp;</td>
																</tr>
															</table>
														</td>
													</tr>
													<tr>
														<td class="blackbordertd" align="right">���ۼ������&nbsp;&nbsp;</td>
														<td class="blackbordertd" align="left">
															<table cellSpacing="0" cellPadding="0" border="0" width="200">
																<tr>
																	<td align="right"><asp:Label id="lblNegAHMoney" Runat="server"></asp:Label>&nbsp;</td>
																</tr>
															</table>
														</td>
													</tr>
													<tr>
														<td class="blackbordertd" align="right">����Ӧ���&nbsp;&nbsp;</td>
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
																	<td height="20" width="60%">&nbsp;&nbsp;����ʦ����۵�λ����</td>
																	<td width="40%">���ڣ�</td>
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
														<td colspan="3">&nbsp;&nbsp;���ʽ��&nbsp;֧Ʊ/����/���/����
														</td>
													</tr>
													<tr>
														<td width="50%">&nbsp;&nbsp;�տ��������ƣ�__________________________��������ã�</td>
														<td width="30%">�а���λ�����������ڣ�</td>
														<td width="20%">�����ڼ��㷽ʽ��</td>
													</tr>
													<tr>
														<td>&nbsp;&nbsp;�տλ�����ʺţ�__________________________��������ã�</td>
														<td>����������:</td>
														<td>����:21+28��</td>
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
												��<BR>
												Ŀ<BR>
												��<BR>
											</TD>
											<TD width="95%" colspan="3">
												<table cellpadding="0" cellspacing="0" border="0" width="100%" height="100%">
													<tr>
														<td class="blackbordertd">
															<table cellSpacing="0" cellPadding="0" width="100%" border="0" height="100%">
																<tr>
																	<td colspan="3" valign="top">&nbsp;�����</td>
																</tr>
																<tr>
																	<td height="20" width="60%"></td>
																	<td width="20%">�����ˣ�</td>
																	<td width="20%">���ڣ�</td>
																</tr>
															</table>
														</td>
														<td class="blackbordertd">
															<table cellSpacing="0" cellPadding="0" width="100%" border="0" height="100%">
																<tr>
																	<td colspan="3" valign="top">&nbsp;�����</td>
																</tr>
																<tr>
																	<td height="20" width="60%"></td>
																	<td width="20%">����</td>
																	<td width="20%">���ڣ�</td>
																</tr>
															</table>
														</td>
													</tr>
													<tr>
														<td class="blackbordertd" width="50%">
															<table cellSpacing="0" cellPadding="0" width="100%" border="0" height="100%">
																<tr>
																	<td colspan="3" valign="top">&nbsp;�����</td>
																</tr>
																<tr>
																	<td height="20" width="60%"></td>
																	<td width="20%">�˼ۣ�</td>
																	<td width="20%">���ڣ�</td>
																</tr>
															</table>
														</td>
														<td class="blackbordertd" width="50%">
															<table cellSpacing="0" cellPadding="0" width="100%" border="0" height="100%">
																<tr>
																	<td colspan="3" valign="top">&nbsp;�����</td>
																</tr>
																<tr>
																	<td height="20" width="60%"></td>
																	<td width="20%">�����ܼࣺ</td>
																	<td width="20%">���ڣ�</td>
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
												��<BR>
												��<BR>
												��<BR>
												��<BR>
											</TD>
											<td class="blackbordertd">
												<table cellSpacing="0" cellPadding="0" width="100%" border="0" height="100%">
													<tr>
														<td colspan="3" valign="top">&nbsp;�����ˣ�</td>
													</tr>
													<tr>
														<td height="20"></td>
														<td width="40%">ǩ�֣�</td>
														<td width="40%">���ڣ�</td>
													</tr>
												</table>
											</td>
											<td class="blackbordertd">
												<table cellSpacing="0" cellPadding="0" width="100%" border="0" height="100%">
													<tr>
														<td colspan="3" valign="top">&nbsp;���񲿾���</td>
													</tr>
													<tr>
														<td height="20"></td>
														<td width="40%">ǩ�֣�</td>
														<td width="40%">���ڣ�</td>
													</tr>
												</table>
											</td>
											<td class="blackbordertd">
												<table cellSpacing="0" cellPadding="0" width="100%" border="0" height="100%">
													<tr>
														<td colspan="3" valign="top">&nbsp;�����ܼࣺ</td>
													</tr>
													<tr>
														<td height="20"></td>
														<td width="40%">ǩ�֣�</td>
														<td width="40%">���ڣ�</td>
													</tr>
												</table>
											</td>
										</TR>
										<TR>
											<TD class="blackbordertd" style="HEIGHT: 130px; TEXT-ALIGN: center">
												<BR>
												��<BR>
												˾<BR>
												��<BR>
												��<BR>
											</TD>
											<td class="blackbordertd">
												<table cellSpacing="0" cellPadding="0" width="100%" border="0" height="100%">
													<tr>
														<td colspan="3" valign="top">&nbsp;���ܣ�</td>
													</tr>
													<tr>
														<td height="20"></td>
														<td width="40%">ǩ�֣�</td>
														<td width="40%">���ڣ�</td>
													</tr>
												</table>
											</td>
											<td class="blackbordertd">
												<table cellSpacing="0" cellPadding="0" width="100%" border="0" height="100%">
													<tr>
														<td colspan="3" valign="top">&nbsp;�ܾ���</td>
													</tr>
													<tr>
														<td height="20"></td>
														<td width="40%">ǩ�֣�</td>
														<td width="40%">���ڣ�</td>
													</tr>
												</table>
											</td>
											<td class="blackbordertd">
												<table cellSpacing="0" cellPadding="0" width="100%" border="0" height="100%">
													<tr>
														<td colspan="3" valign="top">&nbsp;���³���</td>
													</tr>
													<tr>
														<td height="20"></td>
														<td width="40%">ǩ�֣�</td>
														<td width="40%">���ڣ�</td>
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
		OpenPrintWindow("../Report/PrintList.aspx?FromControlID=td_Print", "��ӡ");
	}	

//-->
		</script>
	</body>
</HTML>
