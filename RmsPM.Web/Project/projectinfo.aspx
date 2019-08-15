<%@ Page language="c#" Inherits="RmsPM.Web.Project.ProjectInfo" CodeFile="ProjectInfo.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="InputUsers" Src="../UserControls/InputUsers.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>项目基本信息</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
	</HEAD>
	<body scroll="no">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td bgColor="#e4eff6" height="6"></td>
				</tr>
				<tr>
					<td height="25">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="topic" background="../images/topic_bg.gif"><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle">项目管理>项目信息>
									项目概况</td>
								<td width="9"><IMG height="25" src="../images/topic_corr.gif" width="9"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td class="tools-area" vAlign="top"><IMG src="../images/btn_li.gif" align="absMiddle">
						<input class="button" id="btnModify" onclick="DoModify();return false;" type="button" value="修 改"
							name="btnModify" runat="server">
					</td>
				</tr>
				<tr height="100%">
					<td class="table" vAlign="top">
						<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
							<table class="form" cellSpacing="0" cellPadding="0" width="100%" border="0">
								<TR>
									<td class="form-item">名  称：</td>
									<TD><asp:label id="lblProjectName" runat="server"></asp:label></TD>
									<td class="form-item">
                                        简  称：</td>
									<TD><asp:label id="lblProjectShortName" runat="server"></asp:label></TD>
									<TD class="form-item">阶  段：</TD>
									<TD><asp:label id="lblStatus" runat="server"></asp:label></TD>
								</TR>
								<tr>
								<!--
									<TD class="form-item">帐  套：</TD>
									<TD><asp:label id="LabelSubjectSet" runat="server"></asp:label></TD>
									-->
									<TD class="form-item">城 市：</TD>
									<TD><asp:label id="LabelCity" runat="server"></asp:label></TD>
									<TD class="form-item">区 域：</TD>
									<TD><asp:label id="LabelArea" runat="server"></asp:label></TD>
									<TD class="form-item">编 号：</TD>
									<TD><asp:label id="lblProjectID" runat="server"></asp:label></TD>
								</tr>
								<TR>
								    <TD class="form-item" runat="server" id="ShortUserTitle">启用别名：</TD>
									<TD runat="server" id="ShortUserValue">
                                        <asp:label id="LabelUseShortUserName" runat="server"></asp:label>
                                    </TD>
									<TD class="form-item">地 址：</TD>
									<TD colSpan="3"><asp:label id="lblProjectAddress" runat="server"></asp:label></TD>
								</TR>
								<TR>
									<TD class="form-item">建设单位：</TD>
									<TD><asp:label id="lblDevelopUnit" runat="server"></asp:label></TD>
									<TD class="form-item">注册地址：</TD>
									<TD colspan="3"><asp:label id="lblDevelopUnitAddress" runat="server"></asp:label></TD>
								</TR>
								<TR>
									<TD class="form-item">占地面积：</TD>
									<TD><asp:label id="LabelTotalFloorSpace" runat="server"></asp:label></TD>
									<TD class="form-item">用地面积：</TD>
									<TD><asp:label id="LabelBuildSpace" runat="server"></asp:label></TD>
									<TD class="form-item">建筑面积：</TD>
									<TD><asp:label id="LabelTotalBuildingSpace" runat="server"></asp:label></TD>
								</TR>
								<tr>
									<TD class="form-item">地上建筑面积：</TD>
									<TD><asp:label id="LabelHouseBuildingSpace" runat="server"></asp:label></TD>
									<!--<TD class="form-item">商办建筑面积：</TD>
									<TD><asp:label id="LabelBsBuildingSpace" runat="server"></asp:label></TD>-->
									<TD class="form-item">地下建筑面积：</TD>
									<TD colspan="3"><asp:label id="LabelUnderBuildingSpace" runat="server"></asp:label></TD>
								</tr>
								<tr>
									<TD class="form-item">容 积 率：</TD>
									<TD><asp:label id="LabelPlannedVolumeRate" runat="server"></asp:label></TD>
									<TD class="form-item">覆盖密度：</TD>
									<TD><asp:label id="LabelBuildingDensity" runat="server"></asp:label></TD>
									<TD class="form-item">项目总监：</TD>
									<TD><uc1:InputUsers id="ucManager" runat="server" State="View" ></uc1:InputUsers></TD>
								</tr>
								<TR>
									<TD class="form-item">可售面积：</TD>
									<TD><asp:label id="LabelBuildingSpaceForVolumeRate" runat="server"></asp:label></TD>
									<TD class="form-item">不可销售面积：</TD>
									<TD colSpan="3"><asp:label id="LabelBuildingSpaceNotVolumeRate" runat="server"></asp:label></TD>
								</TR>
								<TR>
									<TD class="form-item">绿化面积：</TD>
									<TD><asp:label id="LabelAfforestingSpace" runat="server"></asp:label></TD>
									<TD class="form-item">绿 化 率：</TD>
									<TD colSpan="3"><asp:label id="LabelAfforestingRate" runat="server"></asp:label></TD>
								</TR>
								<tr>
									<!--<TD class="form-item">集中绿地面积：</TD>
									<TD><asp:label id="LabelCenterAfforestingSpace" runat="server"></asp:label></TD>
									<TD class="form-item">集中绿化率：</TD>
									<TD colspan="3"><asp:label id="LabelCenterAfforestingRate" runat="server"></asp:label></TD>-->
									<TD class="form-item">水面面积：</TD>
									<TD><asp:label id="LabelwaterSpace" runat="server"></asp:label></TD>
									<TD class="form-item">外围面积：</TD>
									<TD colspan="3"><asp:label id="Labelperipheryspace" runat="server"></asp:label></TD>
								</tr>
								<tr>
									<td class="form-item">地上停车位：</td>
									<td><asp:label id="LabelParkingSpace" runat="server"></asp:label></td>
									<td class="form-item">地下停车位：</td>
									<td><asp:label id="LabelUnderParkingSpace" runat="server"></asp:label></td>
									<td class="form-item">总 户 数：</td>
									<td><asp:label id="LabelHouseCount" runat="server"></asp:label></td>
								</tr>
								<TR>
									<TD class="form-item">开工日期：</TD>
									<TD><asp:label id="lblkgDate" Runat="server"></asp:label></TD>
									<TD class="form-item">竣工日期：</TD>
									<TD><asp:label id="lbljgDate" Runat="server"></asp:label></TD>
									<TD class="form-item"><asp:label ID="lblSalProjectName0" runat="server">销售系统项目：</asp:label></TD>
									<TD><asp:label id="lblSalProjectName" Runat="server"></asp:label></TD>
								</TR>
								<!--
								<TR>
									<TD class="form-item">住宅用途：</TD>
									<TD colspan="5"><asp:label id="lblHouseUse" runat="server"></asp:label></TD>-->
									<!--
									<TD class="form-item">配套费类型：</TD>
									<TD><asp:label id="lblPTFeeType" runat="server"></asp:label></TD>
									<TD class="form-item">配套费凭证号：</TD>
									<TD><asp:label id="lblPTFeeVoucherID" runat="server"></asp:label></TD>
								</TR>
								-->
								<TR>
									<TD class="form-item">备注：</TD>
									<TD colSpan="5"><asp:label id="LabelRemark" runat="server"></asp:label></TD>
								</TR>
								<tr style="DISPLAY: none">
									<TD class="form-item">开始日期：</TD>
									<TD><asp:label id="lblPlanStartDate" Runat="server"></asp:label></TD>
									<TD class="form-item">结束日期：</TD>
									<TD colSpan="3"><asp:label id="lblPlanEndDate" Runat="server"></asp:label></TD>
								</tr>
								<TR style="DISPLAY: none">
									<TD class="form-item">地块：</TD>
									<TD colSpan="3"><asp:label id="LabelBlockName" runat="server"></asp:label></TD>
									<TD class="form-item">地块编号：</TD>
									<TD><asp:label id="LabelBlockID" runat="server"></asp:label></TD>
								</TR>
							</table>
						</div>
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
			<input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server">
		</form>
		<script language="javascript">
<!--
	function DoModify()
	{
		var ProjectCode = Form1.txtProjectCode.value;
		OpenFullWindow("ProjectModify.aspx?ProjectCode="+ProjectCode + "&FromUrl=" + escape(window.location.href), "项目修改");
//		OpenCustomWindow("../Project/ProjectModify.aspx?ProjectCode="+ProjectCode + "&FromUrl=" + escape(window.location.href), "项目修改", 780, 560);
//		window.navigate("../Project/ProjectModify.aspx?ProjectCode="+ProjectCode + "&FromUrl=" + escape(window.location.href));
	}
	
	function DoViewBuildingList()
	{
		var ProjectCode = Form1.txtProjectCode.value;
		window.navigate("../pbs/Building_Dl.aspx.aspx?ProjectCode="+ProjectCode);
	}
	
//-->
		</script>
	</body>
</HTML>
