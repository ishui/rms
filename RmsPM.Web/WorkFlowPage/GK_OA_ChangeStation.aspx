<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GK_OA_ChangeStation.aspx.cs" Inherits="WorkFlowPage_GK_OA_ChangeStation" %>

<%@ Register TagPrefix="uc1" TagName="OperationControl" Src="../WorkFlowOperation/GK_OA_ChangeStation.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WorkFlowOpinion" Src="../WorkFlowControl/WorkFlowFormOpinion.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WorkFlowToolbar" Src="../WorkFlowControl/WorkFlowToolbar.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WorkFlowCaseState" Src="../WorkFlowControl/WorkFlowCaseState.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//Dtd HTML 4.0 Transitional//EN" >

<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<title>员工转岗流程审批表</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR" />
		<meta content="C#" name="CODE_LANGUAGE" />
		<meta content="JavaScript" name="vs_defaultClientScript" />
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
		<link href="../Images/index.css" type="text/css" rel="stylesheet" />
		
		<script  type ="text/javascript" language="javascript" src="../Rms.js"></script>
		<script type ="text/javascript">
		    function SelectUnit()
		    {
			    OpenSmallWindow("../SelectBox/SelectUnit.aspx?UnitCode=000000");
		    }
		    function SelectUnitReturn(code, name)
		    {
			    window.document.all.ucOperationControl_FormView1_txtUnitName.value = name;
			    window.document.all.ucOperationControl_FormView1_txtUnit.value = code;
		    }	
	</script>
	</head>
	<body>
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
				<tr>
					<td height="25">
						<table class="table" id="tableToolBar" width="100%">
							<tr>
								<td class="tools-area" width="16">
								    <img src="../images/btn_li.gif" align="absMiddle" />
								</td>
								<td class="tools-area">
									<uc1:WorkFlowToolbar id="wftToolbar" runat="server"></uc1:WorkFlowToolbar>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td class="table" valign="top" id="td_Print" runat="server">
						<table class="blackbordertable" cellspacing="0" cellpadding="0" width="100%" border="0">
							<tr>
								<td class="blackbordertd" align="center" colspan="2">
								    <br /><font size="3"><strong>
								    <asp:Label ID="lblWorkFlowName" runat="server"></asp:Label>
								    </strong></font><br /><br />
								</td>
							</tr>
							<tr>
								<td colspan="2" class="blackbordertd">
									<uc1:OperationControl id="ucOperationControl" runat="server"></uc1:OperationControl>
								</td>
							</tr>
							<tr>
								<td class="blackbordertdcontent" style="vertical-align: middle;" width="5%">
									申<br />
									请<br />
									人<br />
									意<br />
									见

								</td>
								<td class="blackbordertd">
								    <uc1:WorkFlowOpinion id="wfoOpinion1" runat="server"></uc1:WorkFlowOpinion>
								</td>
					        </tr>
							<tr id="tr_SignSheet" runat="server">
								<td class="blackbordertdcontent" style="vertical-align: middle;">
									审<br />
									批<br />
									栏

								</td>
								<td valign="top">
									<table width="100%" cellpadding="0" cellspacing="0" border="0">
										<tr>
											<td valign="top" colspan="2">
												<asp:DataGrid ID="dgSignSheet" Runat="server" Width="100%" AutoGenerateColumns="False" ShowHeader="False" ShowFooter="False" BorderWidth="0" CellPadding="0" CellSpacing="0">
													<Columns>
														<asp:TemplateColumn ItemStyle-Width="100%" ItemStyle-CssClass="blackbordertd">
															<ItemTemplate>
																<uc1:WorkFlowOpinion id="wfoSignSheet" runat="server"></uc1:WorkFlowOpinion>
															</ItemTemplate>
														</asp:TemplateColumn>
													</Columns>
												</asp:DataGrid>
											</td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td colspan="2">
									<uc1:WorkFlowCaseState id="wfcCaseState" runat="server"></uc1:WorkFlowCaseState>
							    </td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td height="12">
						<table cellspacing="0" cellpadding="0" width="100%" border="0">
							<tr>
								<td background="../images/corl_bg.gif">
								    <img height="12" src="../images/corl.gif" width="12" />
								</td>
								<td width="12">
								    <img height="12" src="../images/corr.gif" width="12" />
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td height="6" bgcolor="#e4eff6"></td>
				</tr>
			</table>
		</form>
	</body>
</html>