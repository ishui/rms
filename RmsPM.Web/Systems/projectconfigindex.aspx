<%@ Page language="c#" Inherits="RmsPM.Web.Systems.ProjectConfigIndex" CodeFile="ProjectConfigIndex.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>��Ŀ����</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/Style.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<table class="table" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td bgColor="#e4eff6" height="6"></td>
				</tr>
				<tr>
					<td height="25">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="topic" background="../images/topic_bg.gif"><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle">
									��Ŀ����
								</td>
								<td style="CURSOR: hand" onclick="window.navigate('../Desktop.aspx'); return false;"
									width="79"><IMG height="25" src="../images/btn_back.jpg" width="79"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td class="table" vAlign="top" align="center">
						<TABLE cellSpacing="10" cellPadding="10" width="60%" border="0">
							<TR>
								<TD><A onclick="goBudgetConfig();" href="##">��ͬԤ������</A></TD>
								<TD><A onclick="goPeriodDifine();" href="##">�ƻ����ڶ���</A></TD>
							</TR>
							<TR>
								<TD><A onclick="goProportionConfig();" href="##">����Ȩ�ؿ���</A></TD>
								<TD><A onclick="goContractOldMoneyConfig();" href="##">��ͬ�ܼ۱ȽϿ���</A></TD>
							</TR>
							<TR>
								<TD></TD>
								<TD></TD>
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
					<td bgColor="#e4eff6" height="6"></td>
				</tr>
			</table>
		</form>
		<script language="javascript">
<!--
	function goBudgetConfig()
	{
		OpenMiddleWindow( 'ProjectConfig.aspx?ProjectCode=<%=Request["ProjectCode"]%>','Ԥ������' );
	}
	
	function goPeriodDifine()
	{
		OpenMiddleWindow( 'PeriodDefine.aspx?ProjectCode=<%=Request["ProjectCode"]%>','�ƻ����ڶ���' );
	}
	function goProportionConfig()
	{
		OpenMiddleWindow( 'WBSProportionConfig.aspx?ProjectCode=<%=Request["ProjectCode"]%>','����Ȩ�ؿ���' );
	}
	function goContractOldMoneyConfig()
	{
		OpenMiddleWindow( 'ContractOldMoney.aspx?ProjectCode=<%=Request["ProjectCode"]%>','��ͬ�ܼ۱ȽϿ���' );
	}
	
	
//-->
		</script>
	</body>
</HTML>
