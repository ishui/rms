<%@ Page language="c#" Inherits="RmsPM.Web.BiddingManage.BiddingPrejudicationManage" CodeFile="BiddingPrejudicationManage.aspx.cs" %>
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
		<SCRIPT language="javascript">   
         function OpenBidding(code)
        {
            
	         OpenFullWindow('biddingmodify.aspx?BiddingCode='+code);
        }
		</SCRIPT>
	</HEAD>
	
	<body>
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td height="25">
						<table class="table" id="tableToolBar" width="100%">
							<tr>
								<td class="tools-area" width="16"><IMG src="../images/btn_li.gif" align="absMiddle"></td>
								<td class="tools-area"><uc1:workflowtoolbar id="WorkFlowToolbar1" runat="server"></uc1:workflowtoolbar></td>
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
									<td colSpan="5"><input  class="button" id="btnAddPrice" type="button" value=" 明细查看 " visible=false name="btnDel" runat="server"><span id="spMoney" visible="false" runat="server"></span><uc1:ucbiddingsuppliermodify id="UCBiddingSupplierModify1" runat="server"></uc1:ucbiddingsuppliermodify><uc1:ucbiddingsupplierlist id="UCBiddingSupplierList1" runat="server"></uc1:ucbiddingsupplierlist></td>
								</tr>
								<TR>
									<TD class="blackbordertdcontent"><BR>
										建<BR>
										筑<BR>
										设<BR>
										计<BR>
										部
									</TD>
									<TD class="blackbordertd" width="95%" colSpan="4"><uc1:workflowopinion id="WorkFlowOpinion1" runat="server"></uc1:workflowopinion><FONT face="宋体">&nbsp;</FONT></TD>
								</TR>
								<TR>
									<TD class="blackbordertdcontent"><BR>
										工<BR>
										程<BR>
										部<BR>
										<BR>
									</TD>
									<TD class="blackbordertd" width="95%" colSpan="4"><uc1:workflowopinion id="WorkFlowOpinion2" runat="server"></uc1:workflowopinion><FONT face="宋体">&nbsp;</FONT></TD>
								</TR>
								<TR>
									<TD class="blackbordertdcontent"><BR>
										合<BR>
										约<BR>
										部<BR>
										<BR>
									</TD>
									<TD class="blackbordertd" width="95%" colSpan="4"><uc1:workflowopinion id="WorkFlowOpinion3" runat="server"></uc1:workflowopinion><FONT face="宋体">&nbsp;</FONT></TD>
								</TR>
								<TR>
									<TD class="blackbordertdcontent" style="VERTICAL-ALIGN: middle"><BR>
										项<BR>
										目<BR>
										总<BR>
										监<BR>
										<BR>
									</TD>
									<TD class="blackbordertd" width="95%" colSpan="4"><asp:panel id="WorkFlow4" Visible="False" Runat="server"><FONT face="宋体">
												<uc1:workflowopinion id="WorkFlowOpinion4" runat="server"></uc1:workflowopinion></FONT>
										</asp:panel>
										
										<asp:Repeater ID="rptMeetSign" Runat="server">
											<HeaderTemplate>
												<table cellpadding="0" cellspacing="0" border="0" width="100%" height="100%">
											</HeaderTemplate>
											<ItemTemplate>
												<tr>
													<td><uc1:WorkFlowOpinion id="wfoItemOpinion" runat="server"></uc1:WorkFlowOpinion></td>
											</ItemTemplate>
											<AlternatingItemTemplate>
												<td width="50%" style="BORDER-LEFT: #000000 1px solid; "><uc1:WorkFlowOpinion id="wfoAlternatingItemOpinion" runat="server"></uc1:WorkFlowOpinion></td>
								</TR>
								</AlternatingItemTemplate>
								<FooterTemplate>
						</TABLE>
						</FooterTemplate> </asp:Repeater><FONT face="宋体">&nbsp;</FONT></td>
				</TD>
				<TR>
					<TD class="blackbordertdcontent"><BR>
						总<BR>
						部<BR>
						总<BR>
						监<BR>
						<BR>
					</TD>
					<TD class="blackbordertd" colSpan="2"><uc1:workflowopinion id="WorkFlowOpinion5" runat="server"></uc1:workflowopinion><FONT face="宋体">&nbsp;</FONT></TD>
					<TD class="blackbordertd" colSpan="2"><uc1:workflowopinion id="WorkFlowOpinion6" runat="server"></uc1:workflowopinion><FONT face="宋体">&nbsp;</FONT></TD>
				</TR>
				<TR>
					<TD class="blackbordertdcontent"><BR>
						董<BR>
						事<BR>
						会<BR>
						<BR>
					</TD>
					<TD class="blackbordertd"><uc1:workflowopinion id="WorkFlowOpinion7" runat="server"></uc1:workflowopinion><FONT face="宋体">&nbsp;</FONT></TD>
					<TD class="blackbordertd"><uc1:workflowopinion id="WorkFlowOpinion8" runat="server"></uc1:workflowopinion><FONT face="宋体">&nbsp;</FONT></TD>
					<TD class="blackbordertd"><uc1:workflowopinion id="WorkFlowOpinion9" runat="server"></uc1:workflowopinion><FONT face="宋体">&nbsp;</FONT></TD>
					<TD class="blackbordertd"><uc1:workflowopinion id="WorkFlowOpinion10" runat="server"></uc1:workflowopinion><FONT face="宋体">&nbsp;</FONT></TD>
				</TR>
				<TR>
					<TD colSpan="5"></TD>
				</TR>
				<TR>
					<TD colSpan="5"></TD>
				</TR>
				<TR>
					<TD colSpan="5"><uc1:workflowcasestate id="WorkFlowCaseState1" runat="server"></uc1:workflowcasestate></TD>
				</TR>
				</TD> </tr>
				<tr>
					<td height="12">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td background="../images/corl_bg.gif"><IMG height="12" src="../images/corl.gif" width="12"></td>
								<td width="12"><IMG height="12" src="../images/corr.gif" width="12"></td>
							</tr>
						</table>
						<FONT face="宋体">&nbsp;</FONT>
					</td>
				</tr>
				<tr>
					<td bgColor="#e4eff6" height="6"></td>
				</tr>
			</table>
		</form>
		
		</TD></TR></TBODY></TABLE>
	</body>
</HTML>
