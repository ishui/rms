<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Page language="c#" Inherits="RmsPM.Web.Finance.SubjectSetModify" CodeFile="SubjectSetModify.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>帐套信息</title>
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
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">帐套信息</td>
				</tr>
				<tr height="100%">
					<td class="topic" vAlign="top" align="center">
						<table class="form" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD class="form-item" width="100">帐套名称：</TD>
								<TD><INPUT class="input" id="txtSubjectSetName" type="text" name="txtSubjectSetName" runat="server"><font color="red">*</font></TD>
							</TR>
							<TR>
								<TD class="form-item">科目编码规则：</TD>
								<TD><INPUT class="input" id="txtSubjectRule" type="text" name="txtSubjectRule" runat="server"></TD>
							</TR>
							<TR>
								<TD class="form-item">财务系统接口：</TD>
								<TD><select class="select" id="sltFinanceInterface" name="sltFinanceInterface" runat="server">
										<option value="" selected>--请选择--</option>
									</select>
								</TD>
							</TR>
							<TR>
								<TD class="form-item">导出凭证类型：</TD>
								<TD><select class="select" id="sltFinanceInterfaceExportName" name="sltFinanceInterfaceExportName"
										runat="server">
										<option value="" selected>按编码</option>
										<option value="Name">按名称</option>
									</select>
								</TD>
							</TR>
							<TR>
								<TD class="form-item">凭证部门：</TD>
								<TD><select class="select" id="sltFinanceInterfaceUnit" name="sltFinanceInterfaceUnit" runat="server">
										<option value="" selected>--请选择--</option>
										<option value="PaymentApply">请款部门</option>
										<option value="PaymentApply,User">请款部门,请款人</option>
									</select>
								</TD>
							</TR>
							<TR>
								<TD class="form-item">凭证人员：</TD>
								<TD><select class="select" id="sltFinanceInterfaceUser" name="sltFinanceInterfaceUser" runat="server">
										<option value="" selected>--请选择--</option>
										<option value="PaymentApply">请款人</option>
										<option value="PaymentCheck">请款审批人</option>
									</select>
								</TD>
							</TR>
							<TR>
								<TD class="form-item">厂商财务编码：</TD>
								<TD><select class="select" id="sltFinanceInterfaceSupplierCode" name="sltFinanceInterfaceSupplierCode" runat="server">
										<option value="" selected>分项目</option>
										<option value="ByGroup">不分项目</option>
									</select>
								</TD>
							</TR>
							<TR>
								<TD class="form-item">备注：</TD>
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
									<input class="submit" id="btnSave" type="button" value="确 定" name="btnOK" runat="server" onserverclick="btnSave_ServerClick">&nbsp;
									<INPUT class="submit" id="btnDelete" type="button" value="删 除" onclick="if (!confirm('确实要删除吗？')) return false; "
										runat="server" onserverclick="btnDelete_ServerClick">&nbsp; <INPUT class="submit" onclick="window.close();" type="button" value="取 消"></td>
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
