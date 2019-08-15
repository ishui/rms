<%@ Page language="c#" Inherits="RmsPM.Web.PBS.PBSType" CodeFile="PBSType.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>WBS</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Images/SplitPage.js"></SCRIPT>
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<SCRIPT language="javascript">
		</SCRIPT>
	</HEAD>
	<body scroll="no">
		<form id="Form" method="post" runat="server">
			<TABLE id="Table1" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tbody>
					<tr>
						<td bgColor="#e4eff6" height="6"></td>
					</tr>
					<tr>
						<td height="25">
							<table cellSpacing="0" cellPadding="0" width="100%" border="0">
								<tr>
									<td class="topic" background="../images/topic_bg.gif"><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle">产品管理 
										- 产品组合</td>
									<td style="CURSOR: hand;display:none" onclick="GoBack();" width="79"><IMG height="25" src="../images/btn_back.jpg" width="79"></td>
								</tr>
							</table>
						</td>
					</tr>
					<TR>
						<td class="table" vAlign="top">
							<TABLE class="form1" id="Table4" cellSpacing="0" cellPadding="0" width="100%" height="100%" border="0">
									<TR>
										<TD>
											<TABLE id="Table3" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
												<TR>
													<TD vAlign="top" align="left"><iframe id="TreeSplitTop" src='../PBS/PBSTypeTree.aspx?ProjectCode=<%=Request["ProjectCode"]%>' frameBorder="0" width="100%" scrolling="auto"
															height="100%" style="PADDING-RIGHT: 0px; PADDING-LEFT: 0px; PADDING-BOTTOM: 0px; MARGIN: 0px; PADDING-TOP: 0px"></iframe>
													</TD>
												</TR>
												<TR>
													<TD height="5"><IMG src="../Images/spacer.gif"></TD>
												</TR>
												<TR style="DISPLAY: none">
													<TD><iframe id="TreeSplitBottom" src="about:blank" frameBorder="0" width="100%" scrolling="auto"
															height="100%"></iframe>
													</TD>
												</TR>
											</TABLE>
										</TD>
									</TR>
								</TABLE>
						</TD>
					</TR>
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
				</tbody>
			</TABLE>
		</form>
	</body>
</HTML>
