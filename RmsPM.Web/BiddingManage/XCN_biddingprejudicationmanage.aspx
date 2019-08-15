<%@ Page Language="C#" AutoEventWireup="true" CodeFile="XCN_biddingprejudicationmanage.aspx.cs" Inherits="BiddingManage_XCN_biddingprejudicationmanage" %>


<%@ Register TagPrefix="uc1" TagName="BiddingPrejudicationModify" Src="BiddingPrejudicationModify.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WorkFlowCaseState" Src="../WorkFlowControl/WorkFlowCaseState.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WorkFlowOpinion" Src="../WorkFlowControl/WorkFlowFormOpinion.ascx" %>
<%@ Register TagPrefix="uc1" TagName="UCBiddingSupplierModify" Src="UCBiddingSupplierModify.ascx" %>
<%@ Register TagPrefix="uc1" TagName="UCBiddingSupplierList" Src="UCBiddingSupplierList.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WorkFlowToolbar" Src="../WorkFlowControl/WorkFlowToolbar.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>BiddingPrejudicationManage</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
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
								<td class="tools-area"><uc1:workflowtoolbar id="WorkFlowToolbar1" runat="server" Visible=false></uc1:workflowtoolbar><INPUT class="button" id="btnSave" type="button" value=" 保 存 " name="btnSave" runat="server" onserverclick="btnSave_ServerClick"><INPUT class="button" id="btnUpdate" type="button" value=" 修 改 " name="btnUpdate" runat="server" onserverclick="btnUpdate_ServerClick"><INPUT class="button" id="btnClose" onclick="javascript:window.close();" type="button"
	value="关闭窗口" runat="server"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td class="table" vAlign="top">
						<TABLE class="blackbordertable" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TBODY>
								<tr>
									<td class="blackbordertd" align="center" colSpan="5"><FONT face="宋体"></FONT><br>
										<font size="3"><STRONG>投标单位审批</STRONG></font><br>
										<br>
									</td>
								</tr>
								<TR>
									<TD colSpan="5"><uc1:biddingprejudicationmodify id="BiddingPrejudicationModify1" runat="server"></uc1:biddingprejudicationmodify></TD>
								</TR>
								<tr>
									<td colSpan="5"><uc1:ucbiddingsuppliermodify id="UCBiddingSupplierModify1" runat="server"></uc1:ucbiddingsuppliermodify><uc1:ucbiddingsupplierlist id="UCBiddingSupplierList1" runat="server"></uc1:ucbiddingsupplierlist></td>
								</tr>
								
				
			</table>
			</table>
		</form>
		<SCRIPT language="javascript">
<!--

//-->
		</SCRIPT>
		
	</body>
</HTML>
