<%@ Reference Page="~/pbs/pbstypelayout.aspx" %>
<%@ Page language="c#" Inherits="RmsPM.Web.PBS.PBSTypeLayoutModify" CodeFile="PBSTypeLayoutModify.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>PBSTypeLayoutModify</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<SCRIPT language="javascript" src="../Images/convert.js"></SCRIPT>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0" onkeydown="if(event.keyCode==13) event.keyCode=9">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td bgColor="#e4eff6" height="6"></td>
				</tr>
				<tr>
					<td height="25">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="topic" background="../images/topic_bg.gif"><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle">��Ʒ���� 
									- �滮Ҫ��༭</td>
								<td width="9"><IMG height="25" src="../images/topic_corr.gif" width="9"></td>
							</tr>
						</table>
					</td>
				</tr>
				<TR>
					<td class="tools-area" vAlign="top">
						<IMG src="../images/btn_li.gif" align="absMiddle">
						<input class="button" id="btnSave" type="button" value="�� ��" name="btnSave" runat="server" onserverclick="btnSave_ServerClick">
						<input style="display:none" class="button" id="btnRollback" type="button" value="�� ��" name="btnRollback" runat="server" onserverclick="btnRollback_ServerClick">
						<input class="button" id="btnRead" type="button" value="���ܸ���" name="btnRead" runat="server" onserverclick="btnRead_ServerClick">
					</TD>
				</TR>
				<tr>
					<td class="table" valign="top">
						<table border="0" cellpadding="0" cellspacing="0">
							<tr>
								<td width="200" height="25" valign="bottom" class="note">��Ŀ��Ϣ</td>
							</tr>
						</table>
						<table class="form" cellSpacing="0" cellPadding="5" width="100%" border="0">
							<tr height="30">
								<td class="form-item" width="15%">��ռ�������</td>
								<td class="tdBlank" width="15%"><asp:label id="lblTotalFloorSpace" Runat="server"></asp:label></td>
								<td class="form-item" width="15%">����ռ�������</td>
								<td class="tdBlank" width="15%"><asp:label id="lblBuildSpace" Runat="server"></asp:label></td>
								<td class="form-item" width="15%">�̻��ʣ�</td>
								<td class="tdBlank" width="15%"><asp:label id="lblAfforestingRate" Runat="server"></asp:label></td>
							</tr>
							<tr height="30">
								<td class="form-item">�ܽ��������</td>
								<td class="tdBlank"><asp:label id="lblTotalBuildingSpace" Runat="server"></asp:label></td>
								<td class="form-item">�ݻ��ʣ�</td>
								<td class="tdBlank"><asp:label id="lblPlannedVolumeRate" Runat="server"></asp:label></td>
								<td class="form-item">�����ܶȣ�</td>
								<td class="tdBlank"><asp:label id="lblBuildingDensity" Runat="server"></asp:label></td>
							</tr>
							<tr height="30">
								<td class="form-item">���ݻ��ʽ��������</td>
								<td class="tdBlank"><asp:label id="lblBuildingSpaceForVolumeRate" Runat="server"></asp:label></td>
								<td class="form-item">�����ݻ��ʽ��������</td>
								<td class="tdBlank"><asp:label id="lblBuildingSpaceNotVolumeRate" Runat="server"></asp:label></td>
								<td class="tdBlank"></td>
								<td class="tdBlank"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr height="100%">
					<td class="table">
						<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td>
									<table border="0" cellpadding="0" cellspacing="0">
										<tr>
											<td width="200" height="25" valign="bottom" class="note">�滮Ҫ��</td>
										</tr>
									</table>
								</td>
							</tr>
							<tr vAlign="top" height="100%">
								<td>
									<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
										<table id="tbDtl" cellSpacing="0" cellPadding="3" width="100%" border="0" runat="server"
											class="list">
											<tr align="center" class="list-title">
												<td width="16%" colSpan="2">��Ʒ���</td>
												<td width="10%" align="right">ռ�����<br>(ƽ��)</td>
												<td width="10%" align="right">�ݻ���</td>
												<td width="10%" align="right">�������<br>(ƽ��)</td>
												<td width="10%" align="right">������</td>
												<td width="10%" align="right">�������<br>(ƽ��)</td>
												<td width="10%" align="right" style="DISPLAY: none">��Ʒ����</td>
												<td width="10%" align="right">ƽ��ÿ�����<br>(ƽ��)</td>
												<td width="10%" align="right">�ܻ���</td>
											</tr>
										</table>
									</div>
								</td>
							</tr>
						</table>
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
			<input type="hidden" id="txtFromUrl" name="txtFromUrl" runat="server"> <input type="hidden" id="txtProjectCode" name="txtProjectCode" runat="server">
		</form>
		<SCRIPT language="javascript">
<!--

//������� = ռ����� *���ݻ���
function CalcBuildingSpace(objFloorSpace, objVolumeRate, objBuildingSpace)
{
	var FloorSpace = ConvertFloat(objFloorSpace.value);
	var VolumeRate = ConvertFloat(objVolumeRate.value);
	var BuildingSpace = Math.round(FloorSpace * VolumeRate * 10000) / 10000;
//	var BuildingSpace = Math.round(FloorSpace * VolumeRate / 100 * 10000) / 10000;
  
	if (BuildingSpace == 0)
		objBuildingSpace.value = "";
	else
		objBuildingSpace.value = FormatNumber(BuildingSpace, 2);
  
	objBuildingSpace.onblur();
}

//������� = ������� *��������
function CalcSaleArea(objBuildingSpace, objSaleRate, objSaleArea)
{
	var BuildingSpace = ConvertFloat(objBuildingSpace.value);
	var SaleRate = ConvertFloat(objSaleRate.value);
	var SaleArea = Math.round(BuildingSpace * SaleRate / 100 * 10000) / 10000;
  
	if (SaleArea == "0")
		objSaleArea.value = "";
	else
		objSaleArea.value = FormatNumber(SaleArea, 2);

	objSaleArea.onblur();
}

//�ܻ��� = ������� /��ƽ��ÿ�����
function CalcHouseCount(objSaleArea, objHouseAreaAvg, objHouseCount)
{
	var SaleArea = ConvertFloat(objSaleArea.value);
	var HouseAreaAvg = ConvertFloat(objHouseAreaAvg.value);
	var HouseCount = 0;

	if (HouseAreaAvg != 0)
	{
		HouseCount = Math.round(SaleArea / HouseAreaAvg);
	}
  
	if (HouseCount == "0")
		objHouseCount.value == "";
	else
		objHouseCount.value = FormatNumber(HouseCount, 0);
}

/*
//ƽ��ÿ����� = ������� /���ܻ���
function CalcHouseAreaAvg(objSaleArea, objHouseAreaAvg, objHouseCount)
{
	var SaleArea = ConvertFloat(objSaleArea.value);
	var HouseCount = ConvertFloat(objHouseCount.value);
	var HouseAreaAvg = 0;

	if (HouseCount != 0)
	{
		HouseAreaAvg = Math.round(SaleArea / HouseCount * 10000) / 10000;
	}
  
	objHouseAreaAvg.value = HouseAreaAvg;
}
*/
//-->
		</SCRIPT>
	</body>
</HTML>
