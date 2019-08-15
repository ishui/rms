<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Page language="c#" Inherits="RmsPM.Web.Finance.SubjectSetModify" CodeFile="SubjectSetModify.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>������Ϣ</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" bgColor="#ffffff" border="0">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">������Ϣ</td>
				</tr>
				<tr height="100%">
					<td class="topic" vAlign="top" align="center">
						<table class="form" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD class="form-item" width="100">�������ƣ�</TD>
								<TD><INPUT class="input" id="txtSubjectSetName" type="text" name="txtSubjectSetName" runat="server"><font color="red">*</font></TD>
							</TR>
							<TR>
								<TD class="form-item">��Ŀ�������</TD>
								<TD><INPUT class="input" id="txtSubjectRule" type="text" name="txtSubjectRule" runat="server"></TD>
							</TR>
							<TR>
								<TD class="form-item">����ϵͳ�ӿڣ�</TD>
								<TD><select class="select" id="sltFinanceInterface" name="sltFinanceInterface" runat="server">
										<option value="" selected>--��ѡ��--</option>
									</select>
								</TD>
							</TR>
							<TR>
								<TD class="form-item">����ƾ֤���ͣ�</TD>
								<TD><select class="select" id="sltFinanceInterfaceExportName" name="sltFinanceInterfaceExportName"
										runat="server">
										<option value="" selected>������</option>
										<option value="Name">������</option>
									</select>
								</TD>
							</TR>
							<TR>
								<TD class="form-item">ƾ֤���ţ�</TD>
								<TD><select class="select" id="sltFinanceInterfaceUnit" name="sltFinanceInterfaceUnit" runat="server">
										<option value="" selected>--��ѡ��--</option>
										<option value="PaymentApply">����</option>
										<option value="PaymentApply,User">����,�����</option>
									</select>
								</TD>
							</TR>
							<TR>
								<TD class="form-item">ƾ֤��Ա��</TD>
								<TD><select class="select" id="sltFinanceInterfaceUser" name="sltFinanceInterfaceUser" runat="server">
										<option value="" selected>--��ѡ��--</option>
										<option value="PaymentApply">�����</option>
										<option value="PaymentCheck">���������</option>
									</select>
								</TD>
							</TR>
							<TR>
								<TD class="form-item">���̲�����룺</TD>
								<TD><select class="select" id="sltFinanceInterfaceSupplierCode" name="sltFinanceInterfaceSupplierCode" runat="server">
										<option value="" selected>����Ŀ</option>
										<option value="ByGroup">������Ŀ</option>
									</select>
								</TD>
							</TR>
							<TR>
								<TD class="form-item">��ע��</TD>
								<TD><TEXTAREA id="txtRemark" style="WIDTH: 80%" name="txtRemark" rows="3" runat="server">									</TEXTAREA></TD>
							</TR>
						</table>
					</td>
				</tr>
				<tr>
					<td>
						<TABLE cellSpacing="0" cellPadding="9" width="100" align="center" border="0">
							<tr>
								<td align="center">
									<input class="submit" id="btnSave" type="button" value="ȷ ��" name="btnOK" runat="server" onserverclick="btnSave_ServerClick">&nbsp;
									<INPUT class="submit" id="btnDelete" type="button" value="ɾ ��" onclick="if (!confirm('ȷʵҪɾ����')) return false; "
										runat="server" onserverclick="btnDelete_ServerClick">&nbsp; <INPUT class="submit" onclick="window.close();" type="button" value="ȡ ��"></td>
							</tr>
						</TABLE>
					</td>
				</tr>
			</table>
			<input type="hidden" runat="server" id="txtSubjectSetCode" name="txtSubjectSetCode">
			<SCRIPT language="javascript">
<!--
//-->
			</SCRIPT>
		</form>
	</body>
</HTML>
