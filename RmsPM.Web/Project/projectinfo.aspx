<%@ Page language="c#" Inherits="RmsPM.Web.Project.ProjectInfo" CodeFile="ProjectInfo.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="InputUsers" Src="../UserControls/InputUsers.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>��Ŀ������Ϣ</title>
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
								<td class="topic" background="../images/topic_bg.gif"><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle">��Ŀ����>��Ŀ��Ϣ>
									��Ŀ�ſ�</td>
								<td width="9"><IMG height="25" src="../images/topic_corr.gif" width="9"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td class="tools-area" vAlign="top"><IMG src="../images/btn_li.gif" align="absMiddle">
						<input class="button" id="btnModify" onclick="DoModify();return false;" type="button" value="�� ��"
							name="btnModify" runat="server">
					</td>
				</tr>
				<tr height="100%">
					<td class="table" vAlign="top">
						<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
							<table class="form" cellSpacing="0" cellPadding="0" width="100%" border="0">
								<TR>
									<td class="form-item">��  �ƣ�</td>
									<TD><asp:label id="lblProjectName" runat="server"></asp:label></TD>
									<td class="form-item">
                                        ��  �ƣ�</td>
									<TD><asp:label id="lblProjectShortName" runat="server"></asp:label></TD>
									<TD class="form-item">��  �Σ�</TD>
									<TD><asp:label id="lblStatus" runat="server"></asp:label></TD>
								</TR>
								<tr>
								<!--
									<TD class="form-item">��  �ף�</TD>
									<TD><asp:label id="LabelSubjectSet" runat="server"></asp:label></TD>
									-->
									<TD class="form-item">�� �У�</TD>
									<TD><asp:label id="LabelCity" runat="server"></asp:label></TD>
									<TD class="form-item">�� ��</TD>
									<TD><asp:label id="LabelArea" runat="server"></asp:label></TD>
									<TD class="form-item">�� �ţ�</TD>
									<TD><asp:label id="lblProjectID" runat="server"></asp:label></TD>
								</tr>
								<TR>
								    <TD class="form-item" runat="server" id="ShortUserTitle">���ñ�����</TD>
									<TD runat="server" id="ShortUserValue">
                                        <asp:label id="LabelUseShortUserName" runat="server"></asp:label>
                                    </TD>
									<TD class="form-item">�� ַ��</TD>
									<TD colSpan="3"><asp:label id="lblProjectAddress" runat="server"></asp:label></TD>
								</TR>
								<TR>
									<TD class="form-item">���赥λ��</TD>
									<TD><asp:label id="lblDevelopUnit" runat="server"></asp:label></TD>
									<TD class="form-item">ע���ַ��</TD>
									<TD colspan="3"><asp:label id="lblDevelopUnitAddress" runat="server"></asp:label></TD>
								</TR>
								<TR>
									<TD class="form-item">ռ�������</TD>
									<TD><asp:label id="LabelTotalFloorSpace" runat="server"></asp:label></TD>
									<TD class="form-item">�õ������</TD>
									<TD><asp:label id="LabelBuildSpace" runat="server"></asp:label></TD>
									<TD class="form-item">���������</TD>
									<TD><asp:label id="LabelTotalBuildingSpace" runat="server"></asp:label></TD>
								</TR>
								<tr>
									<TD class="form-item">���Ͻ��������</TD>
									<TD><asp:label id="LabelHouseBuildingSpace" runat="server"></asp:label></TD>
									<!--<TD class="form-item">�̰콨�������</TD>
									<TD><asp:label id="LabelBsBuildingSpace" runat="server"></asp:label></TD>-->
									<TD class="form-item">���½��������</TD>
									<TD colspan="3"><asp:label id="LabelUnderBuildingSpace" runat="server"></asp:label></TD>
								</tr>
								<tr>
									<TD class="form-item">�� �� �ʣ�</TD>
									<TD><asp:label id="LabelPlannedVolumeRate" runat="server"></asp:label></TD>
									<TD class="form-item">�����ܶȣ�</TD>
									<TD><asp:label id="LabelBuildingDensity" runat="server"></asp:label></TD>
									<TD class="form-item">��Ŀ�ܼࣺ</TD>
									<TD><uc1:InputUsers id="ucManager" runat="server" State="View" ></uc1:InputUsers></TD>
								</tr>
								<TR>
									<TD class="form-item">���������</TD>
									<TD><asp:label id="LabelBuildingSpaceForVolumeRate" runat="server"></asp:label></TD>
									<TD class="form-item">�������������</TD>
									<TD colSpan="3"><asp:label id="LabelBuildingSpaceNotVolumeRate" runat="server"></asp:label></TD>
								</TR>
								<TR>
									<TD class="form-item">�̻������</TD>
									<TD><asp:label id="LabelAfforestingSpace" runat="server"></asp:label></TD>
									<TD class="form-item">�� �� �ʣ�</TD>
									<TD colSpan="3"><asp:label id="LabelAfforestingRate" runat="server"></asp:label></TD>
								</TR>
								<tr>
									<!--<TD class="form-item">�����̵������</TD>
									<TD><asp:label id="LabelCenterAfforestingSpace" runat="server"></asp:label></TD>
									<TD class="form-item">�����̻��ʣ�</TD>
									<TD colspan="3"><asp:label id="LabelCenterAfforestingRate" runat="server"></asp:label></TD>-->
									<TD class="form-item">ˮ�������</TD>
									<TD><asp:label id="LabelwaterSpace" runat="server"></asp:label></TD>
									<TD class="form-item">��Χ�����</TD>
									<TD colspan="3"><asp:label id="Labelperipheryspace" runat="server"></asp:label></TD>
								</tr>
								<tr>
									<td class="form-item">����ͣ��λ��</td>
									<td><asp:label id="LabelParkingSpace" runat="server"></asp:label></td>
									<td class="form-item">����ͣ��λ��</td>
									<td><asp:label id="LabelUnderParkingSpace" runat="server"></asp:label></td>
									<td class="form-item">�� �� ����</td>
									<td><asp:label id="LabelHouseCount" runat="server"></asp:label></td>
								</tr>
								<TR>
									<TD class="form-item">�������ڣ�</TD>
									<TD><asp:label id="lblkgDate" Runat="server"></asp:label></TD>
									<TD class="form-item">�������ڣ�</TD>
									<TD><asp:label id="lbljgDate" Runat="server"></asp:label></TD>
									<TD class="form-item"><asp:label ID="lblSalProjectName0" runat="server">����ϵͳ��Ŀ��</asp:label></TD>
									<TD><asp:label id="lblSalProjectName" Runat="server"></asp:label></TD>
								</TR>
								<!--
								<TR>
									<TD class="form-item">סլ��;��</TD>
									<TD colspan="5"><asp:label id="lblHouseUse" runat="server"></asp:label></TD>-->
									<!--
									<TD class="form-item">���׷����ͣ�</TD>
									<TD><asp:label id="lblPTFeeType" runat="server"></asp:label></TD>
									<TD class="form-item">���׷�ƾ֤�ţ�</TD>
									<TD><asp:label id="lblPTFeeVoucherID" runat="server"></asp:label></TD>
								</TR>
								-->
								<TR>
									<TD class="form-item">��ע��</TD>
									<TD colSpan="5"><asp:label id="LabelRemark" runat="server"></asp:label></TD>
								</TR>
								<tr style="DISPLAY: none">
									<TD class="form-item">��ʼ���ڣ�</TD>
									<TD><asp:label id="lblPlanStartDate" Runat="server"></asp:label></TD>
									<TD class="form-item">�������ڣ�</TD>
									<TD colSpan="3"><asp:label id="lblPlanEndDate" Runat="server"></asp:label></TD>
								</tr>
								<TR style="DISPLAY: none">
									<TD class="form-item">�ؿ飺</TD>
									<TD colSpan="3"><asp:label id="LabelBlockName" runat="server"></asp:label></TD>
									<TD class="form-item">�ؿ��ţ�</TD>
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
		OpenFullWindow("ProjectModify.aspx?ProjectCode="+ProjectCode + "&FromUrl=" + escape(window.location.href), "��Ŀ�޸�");
//		OpenCustomWindow("../Project/ProjectModify.aspx?ProjectCode="+ProjectCode + "&FromUrl=" + escape(window.location.href), "��Ŀ�޸�", 780, 560);
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
