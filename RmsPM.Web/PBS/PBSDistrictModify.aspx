<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics.WebUI.WebDataInput.v5.1, Version=5.1.20051.37, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Page language="c#" Inherits="RmsPM.Web.PBS.PBSDistrictModify" CodeFile="PBSDistrictModify.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>区域修改</title>
		<META http-equiv="Content-Type" content="text/html; charset=gb2312">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<LINK href="../Images/infra.css" type="text/css" rel="stylesheet">
		<script>
		/*
	function iniBody()
	{
		var strName=document.all("txtIsArea").value;
		
		if (strName=="1")
		{
			document.all("trBuilding").style.display="none";
			document.all("trArea").style.display="block";
		}
		else
		{
			document.all("trBuilding").style.display="block";
			document.all("trArea").style.display="none";
		}
		
		
	}
	*/
		</script>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" bgColor="white" border="0">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">区域修改</td>
				</tr>
				<trheight="100%">
					<td class="topic" vAlign="top" align="center">
						<table class="form" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD class="form-item" noWrap width="100">区域名称：</TD>
								<TD noWrap colspan="3"><input class="input" id="txtBuildingName" type="text" size="30" name="txtBuildingName"
										runat="server"><font color="red">*</font></TD>
								<!--
								<TD class="form-item" noWrap width="100">区域简称：</TD>
								<TD noWrap><input class="input" id="txtBuildingShortName" type="text" size="30" name="txtBuildingShortName"
										runat="server"></TD>
										-->
								<TD class="form-item">所属区域：</TD>
								<TD><SELECT id="sltParentCode" name="sltParentCode" runat="server">
										<OPTION value="" selected>------请选择------</OPTION>
									</SELECT></TD>
							</tr>
							<TR>
								<TD class="form-item">占地面积：</TD>
								<TD><igtxt:webnumericedit id="txtTotalFloorSpace" runat="server" CssClass="infra-input-nember" Width="100px"
										JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js"
										ImageDirectory="../images/infragistics/images/" MinDecimalPlaces="none"></igtxt:webnumericedit>平米</TD>
								<TD class="form-item">用地面积：</TD>
								<TD><igtxt:webnumericedit id="txtBuildSpace" runat="server" CssClass="infra-input-nember" Width="100px" JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js"
										JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js" ImageDirectory="../images/infragistics/images/"
										MinDecimalPlaces="none"></igtxt:webnumericedit>平米</TD>
								<TD class="form-item">建筑面积：</TD>
								<TD><igtxt:webnumericedit id="txtTotalBuildingSpace" runat="server" CssClass="infra-input-nember" Width="100px"
										JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js"
										ImageDirectory="../images/infragistics/images/" MinDecimalPlaces="none"></igtxt:webnumericedit>平米</TD>
							</TR>
							<tr>
								<TD class="form-item">地上建筑面积：</TD>
								<TD><igtxt:webnumericedit id="txtHouseBuildingSpace" runat="server" CssClass="infra-input-nember" Width="100px"
										JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js"
										ImageDirectory="../images/infragistics/images/" MinDecimalPlaces="none"></igtxt:webnumericedit>平米</TD>
								<TD class="form-item">地下建筑面积：</TD>
								<TD colspan="3"><igtxt:webnumericedit id="txtUnderBuildingSpace" runat="server" CssClass="infra-input-nember" Width="100px"
										JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js"
										ImageDirectory="../images/infragistics/images/" MinDecimalPlaces="none"></igtxt:webnumericedit>平米</TD>
							</tr>
							<TR>
								<TD class="form-item">容 积 率：</TD>
								<TD><asp:textbox id="TextBoxPlannedVolumeRate" runat="server" CssClass="input-nember" Width="100"></asp:textbox></TD>
								<TD class="form-item">覆盖密度：</TD>
								<TD colspan="3"><igtxt:webnumericedit id="txtBuildingDensity" runat="server" CssClass="infra-input-nember" Width="100px"
										JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js"
										ImageDirectory="../images/infragistics/images/" MinDecimalPlaces="none"></igtxt:webnumericedit>%</TD>
							</TR>
							<TR>
								<TD class="form-item">可售面积：</TD>
								<TD><igtxt:webnumericedit id="txtBuildingSpaceForVolumeRate" runat="server" CssClass="infra-input-nember"
										Width="100px" JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js"
										ImageDirectory="../images/infragistics/images/" MinDecimalPlaces="none"></igtxt:webnumericedit>平米</TD>
								<TD class="form-item">不可销售面积：</TD>
								<TD colspan="3"><igtxt:webnumericedit id="txtBuildingSpaceNotVolumeRate" runat="server" CssClass="infra-input-nember"
										Width="100px" JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js"
										ImageDirectory="../images/infragistics/images/" MinDecimalPlaces="none"></igtxt:webnumericedit>平米</TD>
							</TR>
							<TR>
								<TD class="form-item">绿化面积：</TD>
								<TD><igtxt:webnumericedit id="txtAfforestingSpace" runat="server" CssClass="infra-input-nember" Width="100px"
										JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js"
										ImageDirectory="../images/infragistics/images/" MinDecimalPlaces="none"></igtxt:webnumericedit>平米</TD>
								<TD class="form-item">绿 化 率：</TD>
								<TD colspan="3"><igtxt:webnumericedit id="txtAfforestingRate" runat="server" CssClass="infra-input-nember" Width="100px"
										JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js"
										ImageDirectory="../images/infragistics/images/" MinDecimalPlaces="none"></igtxt:webnumericedit>%</TD>
							</TR>
							<TR>
								<TD class="form-item">水面面积：</TD>
								<TD><igtxt:webnumericedit id="txtWaterSpace" runat="server" CssClass="infra-input-nember" Width="100px"
										JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js"
										ImageDirectory="../images/infragistics/images/" MinDecimalPlaces="none"></igtxt:webnumericedit>平米</TD>
								<TD class="form-item">外围面积：</TD>
								<TD colspan="3"><igtxt:webnumericedit id="txtPeripherySpace1" runat="server" CssClass="infra-input-nember" Width="100px"
										JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js"
										ImageDirectory="../images/infragistics/images/"></igtxt:webnumericedit>平米</TD>
								
							</TR>
							<TR>
								
								<TD class="form-item">地上停车位：</TD>
								<TD><igtxt:webnumericedit id="txtParkingSpace" runat="server" CssClass="infra-input-nember" Width="100px"
										JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js"
										ImageDirectory="../images/infragistics/images/"></igtxt:webnumericedit></TD>
								<TD class="form-item">地下停车位：</TD>
								<TD><igtxt:webnumericedit id="txtUnderParkingSpace" runat="server" CssClass="infra-input-nember" Width="100px"
										JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js"
										ImageDirectory="../images/infragistics/images/"></igtxt:webnumericedit></TD>
								<TD class="form-item">总 户 数：</TD>
								<TD><igtxt:webnumericedit id="txtHouseCount" runat="server" CssClass="infra-input-nember" Width="100px" JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js"
										JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js" ImageDirectory="../images/infragistics/images/"></igtxt:webnumericedit></TD>
							</TR>
							<TR>
								<TD class="form-item">备注：</TD>
								<TD colSpan="5"><textarea id="txtRemark" style="WIDTH: 100%" name="txtRemark" rows="5" runat="server"></textarea></TD>
							</TR>
						</table>
					</td>
				</tr>
				<tr>
					<td>
						<table cellSpacing="10" width="100%">
							<tr>
								<td align="center"><input class="submit" id="btnSave" type="button" value="确 定" name="btnSave" runat="server" onserverclick="btnSave_ServerClick">
									<input class="submit" id="btnDelete" style="DISPLAY: none" onclick="javascript:if(!window.confirm('确实要删除这条记录吗？')) return false;"
										type="button" value="删 除" name="btnDelete" runat="server" onserverclick="btnDelete_ServerClick"> <input class="submit" id="btnCancel" onclick="javascript:self.close()" type="button" value="取 消"
										name="btnCancel">
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<input id="txtBuildingCode" type="hidden" name="txtBuildingCode" runat="server">
			<input id="txtParentCode" type="hidden" name="txtParentCode" runat="server"><input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server">
		</form>
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<script language="javascript">
							<!--
							//-->
		</script>
	</body>
</HTML>
