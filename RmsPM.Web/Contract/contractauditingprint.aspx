<%@ Page language="c#" Inherits="RmsPM.Web.Contract.ContractAuditingtPrint" CodeFile="ContractAuditingPrint.aspx.cs" %>
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
								<td class="blackbordertd" align="center" colspan="7"><br>
									<font size="3"><STRONG>��ͬ������</STRONG></font><br>
									<br>
								</td>
							</tr>
							<TR>
								<TD width="100%" colspan="7">
									<table cellSpacing="0" cellPadding="0" width="100%" border="0">
										<tr>
											<td class="blackbordertd" width="20%">&nbsp;&nbsp;��Ŀ���ƣ�</td>
											<td class="blackbordertdpaddingcontent" width="30%"><asp:Label ID="lblProjectName" Runat="server"></asp:Label>&nbsp;</td>
											<td class="blackbordertd" width="20%">&nbsp;&nbsp;��ͬ��ţ�</td>
											<td class="blackbordertdpaddingcontent" width="30%"><asp:Label ID="lblContractID" Runat="server"></asp:Label>&nbsp;</td>
										</tr>
										<tr>
											<td class="blackbordertd">&nbsp;&nbsp;��ͬ���ƣ�</td>
											<td class="blackbordertdpaddingcontent"><asp:Label ID="lblContractName" Runat="server"></asp:Label>&nbsp;</td>
											<td class="blackbordertd">&nbsp;&nbsp;��ͬ��</td>
											<td class="blackbordertdpaddingcontent"><asp:Label ID="lblMoney" Runat="server"></asp:Label> Ԫ&nbsp;</td>
										</tr>
										<tr>
											<td class="blackbordertd">&nbsp;&nbsp;ǩԼ��λ��</td>
											<td class="blackbordertdpaddingcontent"><asp:Label ID="lblSupplierName" Runat="server"></asp:Label>&nbsp;</td>
											<td class="blackbordertd">&nbsp;&nbsp;Ԥ��ǩԼ���ڣ�</td>
											<td class="blackbordertdpaddingcontent">&nbsp;</td>
										</tr>
									</table>
								</TD>
							</TR>
							<TR>
								<TD class="blackbordertd" width="5%" style="HEIGHT: 130px; TEXT-ALIGN: center">
									<BR>
									��<BR>
									ͬ<BR>
									��<BR>
									��<BR>
								</TD>
								<TD width="95%" colspan="6" class="blackbordertd">
									<asp:label id="lblContractObject" runat="server"></asp:label>
								</TD>
							</TR>
							<TR>
								<TD class="blackbordertd" style="HEIGHT: 130px; TEXT-ALIGN: center">
									<BR>
									<BR>
									��<BR>
									Ŀ<BR>
									��<BR>
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
									��<BR>
									��<BR>
									��<BR>
								</TD>
								<TD colspan="6" class="blackbordertd">
									<table cellSpacing="0" cellPadding="0" width="100%" border="0" height="100%">
										<tr>
											<td colspan="3" valign="top">&nbsp;�����</td>
										</tr>
										<tr>
											<td height="20" width="60%"></td>
											<td width="20%">ǩ����</td>
											<td width="20%">���ڣ�</td>
										</tr>
									</table>
								</TD>
							</TR>
							<TR>
								<TD class="blackbordertd" style="HEIGHT: 130px; TEXT-ALIGN: center">
									<BR>
									��<BR>
									��<BR>
									��<BR>
									��<BR>
								</TD>
								<TD colspan="6" class="blackbordertd">
									<table cellSpacing="0" cellPadding="0" width="100%" border="0" height="100%">
										<tr>
											<td colspan="3" valign="top">&nbsp;�����</td>
										</tr>
										<tr>
											<td height="20" width="60%"></td>
											<td width="20%">ǩ����</td>
											<td width="20%">���ڣ�</td>
										</tr>
									</table>
								</TD>
							</TR>
							<TR>
								<TD class="blackbordertd" style="HEIGHT: 130px; TEXT-ALIGN: center">
									<BR>
									��<BR>
									��<BR>
									��<BR>
									��<BR>
								</TD>
								<TD colspan="3" class="blackbordertd">
									<table cellSpacing="0" cellPadding="0" width="100%" border="0" height="100%">
										<tr>
											<td colspan="3" valign="top">&nbsp;���񲿣�</td>
										</tr>
										<tr>
											<td height="20" width="60%"></td>
											<td width="40%">ǩ����</td>
										</tr>
									</table>
								</TD>
								<TD colspan="3" class="blackbordertd">
									<table cellSpacing="0" cellPadding="0" width="100%" border="0" height="100%">
										<tr>
											<td colspan="3" valign="top">&nbsp;�����ܼࣺ</td>
										</tr>
										<tr>
											<td height="20" width="60%"></td>
											<td width="40%">ǩ����</td>
										</tr>
									</table>
								</TD>
							</TR>
							<TR>
								<TD class="blackbordertd" style="HEIGHT: 130px; TEXT-ALIGN: center">
									��<BR>
									��<BR>
									��<BR>
									˾<BR>
									��<BR>
									��<BR>
								</TD>
								<TD colspan="2" class="blackbordertd">
									<table cellSpacing="0" cellPadding="0" width="100%" border="0" height="100%">
										<tr>
											<td colspan="3" valign="top">&nbsp;���ܣ�</td>
										</tr>
										<tr>
											<td height="20" width="60%"></td>
											<td width="40%">ǩ����</td>
										</tr>
									</table>
								</TD>
								<TD colspan="2" class="blackbordertd">
									<table cellSpacing="0" cellPadding="0" width="100%" border="0" height="100%">
										<tr>
											<td colspan="3" valign="top">&nbsp;�ܾ���</td>
										</tr>
										<tr>
											<td height="20" width="60%"></td>
											<td width="40%">ǩ����</td>
										</tr>
									</table>
								</TD>
								<TD colspan="2" class="blackbordertd">
									<table cellSpacing="0" cellPadding="0" width="100%" border="0" height="100%">
										<tr>
											<td colspan="3" valign="top">&nbsp;���³���</td>
										</tr>
										<tr>
											<td height="20" width="60%"></td>
											<td width="40%">ǩ����</td>
										</tr>
									</table>
								</TD>
							</TR>
						</TABLE>
						<span class="text">ע:�����������ҳ����</span>
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
