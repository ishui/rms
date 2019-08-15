<%@ Page language="c#" Inherits="RmsPM.Web.Contract.ContractAccountPrint" CodeFile="ContractAccountPrint.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>��ͬ������</title>
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
								<td class="blackbordertd" width="15%" align="right">��Ŀ���ƣ�&nbsp;&nbsp;</td>
								<td class="blackbordertdpaddingcontent" width="30%"><asp:Label ID="lblProjectName" Runat="server"></asp:Label>&nbsp;</td>
								<td class="blackbordertd" width="20%" align="right">��ͬ��ţ�&nbsp;&nbsp;</td>
								<td class="blackbordertdpaddingcontent" width="35%"><asp:Label ID="lblContractID" Runat="server"></asp:Label>&nbsp;</td>
							</TR>
							<tr>
								<td class="blackbordertd" align="right">��ͬ���ƣ�&nbsp;&nbsp;</td>
								<td class="blackbordertdpaddingcontent" colspan="3"><asp:Label ID="lblContractName" Runat="server"></asp:Label>&nbsp;</td>
							</tr>
							<tr>
								<td class="blackbordertd" align="right">��&nbsp;��&nbsp;�ˣ�&nbsp;&nbsp;</td>
								<td class="blackbordertdpaddingcontent"><asp:Label ID="lblSupplierName" Runat="server"></asp:Label>&nbsp;</td>
								<td class="blackbordertd" align="right">�������ţ�&nbsp;&nbsp;</td>
								<td class="blackbordertdpaddingcontent">&nbsp;</td>
							</tr>
							<TR>
								<td colspan="2">
									<table cellSpacing="0" cellPadding="0" width="100%" border="0">
										<tr>
											<TD class="blackbordertd" width="5%" style="VERTICAL-ALIGN: middle; TEXT-ALIGN: center"
												rowspan="2">
												<BR>
												ԭ<BR>
												��<BR>
												��<BR>
												ժ<BR>
												Ҫ<BR>
											</TD>
											<td class="blackbordertdpaddingcontent" style="HEIGHT:419px">����ϸ���ݿ�����</td>
										</tr>
										<tr>
											<td class="blackbordertdpaddingcontent">
												������<br>
												&nbsp;&nbsp;
												<asp:CheckBox ID="chkAttachment1" Runat="server"></asp:CheckBox>1. 
												����ָʾXXXX��ǩ֤XXXX<br>
												&nbsp;&nbsp;
												<asp:CheckBox ID="chkAttachment2" Runat="server"></asp:CheckBox>2. �а��̽�������<br>
												&nbsp;&nbsp;
												<asp:CheckBox ID="chkAttachment3" Runat="server"></asp:CheckBox>3. ���ʹ���ʦ������<br>
												&nbsp;&nbsp;
												<asp:CheckBox ID="chkAttachment4" Runat="server"></asp:CheckBox>4. ������ݸ�<br>
												&nbsp;&nbsp;
												<asp:CheckBox ID="chkAttachment5" Runat="server"></asp:CheckBox>5. �������š�����������<br>
												&nbsp;&nbsp;
												<asp:CheckBox ID="chkAttachment6" Runat="server"></asp:CheckBox>6. ����ܻ�
											</td>
										</tr>
									</table>
								</td>
								<td colspan="2" valign="top">
									<table cellSpacing="0" cellPadding="0" width="100%" border="0">
										<tr>
											<td class="blackbordertd" width="37%">&nbsp;</td>
											<td class="blackbordertdcontent" colspan="3">RMB</td>
										</tr>
										<tr>
											<td class="blackbordertd" align="right">��Ŀǰ�ۼ����������&nbsp;&nbsp;</td>
											<td class="blackbordertd" colspan="3" align="left">
												<table cellSpacing="0" cellPadding="0" border="0" width="200">
													<tr>
														<td align="right"><asp:Label id="lblTotalChangeMoney" Runat="server"></asp:Label>&nbsp;</td>
													</tr>
												</table>
											</td>
										</tr>
										<tr>
											<td class="blackbordertd" align="right">��֪��δ�������&nbsp;&nbsp;</td>
											<td class="blackbordertd" colspan="3" align="left">
												<table cellSpacing="0" cellPadding="0" border="0" width="200">
													<tr>
														<td align="right"><asp:Label id="lblChangeMoney" Runat="server"></asp:Label>&nbsp;</td>
													</tr>
												</table>
											</td>
										</tr>
										<tr>
											<td class="blackbordertd" align="right">���Ʊ���ܶ&nbsp;&nbsp;</td>
											<td class="blackbordertd" colspan="3" align="left">
												<table cellSpacing="0" cellPadding="0" border="0" width="200">
													<tr>
														<td align="right"><asp:Label id="lblEstimateChangeMoney" Runat="server"></asp:Label>&nbsp;</td>
													</tr>
												</table>
											</td>
										</tr>
										<tr>
											<td colspan="4" bgcolor="black" height="1"></td>
										</tr>
										<tr>
											<td class="blackbordertd">&nbsp;</td>
											<td class="blackbordertd" align="center" width="21%">�а�������<br>
												RMB</td>
											<td class="blackbordertd" align="center" width="21%">���ʹ���ʦ<br>���<br>
												RMB</td>
											<td class="blackbordertd" align="center" width="21%">��Ŀ��Լ�����<br>
												RMB</td>
										</tr>
										<tr>
											<td class="blackbordertd" align="right">1.ԭ��ͬ��&nbsp;&nbsp;</td>
											<td class="blackbordertd">&nbsp;</td>
											<td class="blackbordertd">&nbsp;</td>
											<td class="blackbordertd">&nbsp;</td>
										</tr>
										<tr>
											<td class="blackbordertd" align="right">2.����ܶ&nbsp;&nbsp;</td>
											<td class="blackbordertd">&nbsp;</td>
											<td class="blackbordertd">&nbsp;</td>
											<td class="blackbordertd">&nbsp;</td>
										</tr>
										<tr>
											<td class="blackbordertd" align="right">3. ����(����/�ۿ�)��&nbsp;&nbsp;</td>
											<td class="blackbordertd">&nbsp;</td>
											<td class="blackbordertd">&nbsp;</td>
											<td class="blackbordertd">&nbsp;</td>
										</tr>
										<tr>
											<td class="blackbordertd" align="right">�����&nbsp;&nbsp;</td>
											<td class="blackbordertd">&nbsp;</td>
											<td class="blackbordertd">&nbsp;</td>
											<td class="blackbordertd">&nbsp;</td>
										</tr>
										<tr>
											<td colspan="4" bgcolor="black" height="1"></td>
										</tr>
										<tr>
											<td colspan="4" class="blackbordertd" style="HEIGHT:143px">
												<table cellSpacing="0" cellPadding="0" width="100%" border="0" height="100%">
													<tr>
														<td colspan="3" valign="top">&nbsp;</td>
													</tr>
													<tr>
														<td height="20" width="60%">&nbsp;&nbsp;���ܹ���ʦ��</td>
														<td width="40%">���ڣ�</td>
													</tr>
												</table>
											</td>
										</tr>
										<tr>
											<td colspan="4" class="blackbordertd" style="HEIGHT:143px">
												<table cellSpacing="0" cellPadding="0" width="100%" border="0" height="100%">
													<tr>
														<td colspan="3" valign="top">&nbsp;</td>
													</tr>
													<tr>
														<td height="20" width="60%">&nbsp;&nbsp;��Ŀ������</td>
														<td width="40%">���ڣ�</td>
													</tr>
												</table>
											</td>
										</tr>
									</table>
								</td>
							</TR>
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
