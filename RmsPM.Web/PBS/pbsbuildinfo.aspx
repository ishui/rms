<%@ Reference Control="~/pbs/buildingtree.ascx" %>
<%@ Page language="c#" Inherits="RmsPM.Web.PBS.PBSBuildInfo" CodeFile="PBSBuildInfo.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="UCBuildingModelList" Src="UCBuildingModelList.ascx" %>
<%@ Register TagPrefix="uc1" TagName="UCBuildingStationList" Src="UCBuildingStationList.ascx" %>
<%@ Register TagPrefix="uc1" TagName="UCBuildingFunctionList" Src="UCBuildingFunctionList.ascx" %>
<%@ Register TagPrefix="igtab" Namespace="Infragistics.WebUI.UltraWebTab" Assembly="Infragistics.WebUI.UltraWebTab.v5.1, Version=5.1.20051.37, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="uc1" TagName="UCPicGroup" Src="../PicGroup/UCPicGroup.ascx" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Register TagPrefix="uc1" TagName="BuildingTree" Src="BuildingTree.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>楼栋信息</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../PBS/PBSUnitNav.js"></SCRIPT>
		<SCRIPT language="javascript" src="../Images/XmlTree.js"></SCRIPT>
		<SCRIPT language="javascript" src="map.js"></SCRIPT>
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
	</HEAD>
	<body scroll="no" onload="winload();">
		<form id="Form1" method="post" runat="server">
			<table id="Table1" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TBODY>
					<tr id="trA1" runat="server">
						<td bgColor="#e4eff6" height="6"></td>
					</tr>
					<tr id="trA2" runat="server">
						<td height="25">
							<table id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
								<tr>
									<td class="topic" width="100%" background="../images/topic_bg.gif"><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle">产品管理 
										- <span id="spanTitle" runat="server">楼栋</span>信息
									</td>
									<td><IMG height="25" src="../images/topic_corr.gif"></td>
								</tr>
							</table>
						</td>
					</tr>
					<tr id="trB1" style="DISPLAY: none" runat="server">
						<td class="topic" align="center" background="../images/topic_bg.gif" height="25">项目管理>项目信息>项目概况>楼栋信息</td>
					</tr>
					<tr id="trToolBar" runat="server">
						<TD class="tools-area" vAlign="top"><IMG src="../images/btn_li.gif" align="absMiddle">
							<input class="button" id="btnModify" onclick="Modify();" type="button" value="修 改" name="btnModify"
								runat="server"> <input class="button" id="btnDelete" onclick="javascript:if(!window.confirm('确实要删除这条记录吗？')) return false;"
								type="button" value="删 除" name="btnDelete" runat="server" onserverclick="btnDelete_ServerClick"> <input class="button" id="btnAddArea" onclick="doCreateArea(Form1.txtProjectCode.value, Form1.txtBuildingCode.value);"
								type="button" value="新增区域" name="btnAddArea" runat="server"> <input class="button" id="btnAddBuilding" onclick="doCreateBuilding(Form1.txtProjectCode.value, Form1.txtBuildingCode.value);"
								type="button" value="新增楼栋" name="btnAddBuilding" runat="server"> <IMG src="../images/btn_li.gif" align="absMiddle">
							<input class="button" onclick="GotoBuildingGraph(Form1.txtProjectCode.value, Form1.txtBuildingCode.value);"
								type="button" value="分布图" name="btnGotoBuildingGraph"> <input class="button" onclick="document.all.divHintLoad.style.display = ''; GotoBuildingPart(Form1.txtBuildingCode.value);"
								type="button" value="房控图" name="btnGotoBuildingPart" id="btnGotoBuildingPart" runat="server"> <input class="button" onclick="document.all.divHintLoad.style.display = '';GotoBuildingList(Form1.txtProjectCode.value);"
								type="button" value="返回" name="btnGotoBuildingList"> <IMG src="../images/btn_li.gif" align="absMiddle">
							转到楼栋：<select class="select" id="sltBuilding" onchange="document.all.divHintLoad.style.display='';GotoBuildingInfo(this.value);"
								name="sltBuilding" runat="server"></select>
						</TD>
					</tr>
					<tr id="trArea" style="DISPLAY: none" runat="server">
						<td class="table" vAlign="top">
							<table id="Table3" cellSpacing="0" cellPadding="0" width="100%" border="0">
								<TR>
									<TD nowrap class="note">区域名称：<asp:label id="lblAreaName" Runat="server"></asp:label></TD>
									<td nowrap style="padding-left:80px"><a id="hrefShowDistrict" href="#" onclick="ShowDistrict();return false;">展开区域信息</a></td>
									<td width="100%"></td>
								</TR>
							</table>
							<table id="tableDistrictMore" style="display:none" class="form" cellSpacing="0" cellPadding="0" width="100%" border="0">
								<TR>
									<TD class="form-item" width="100px">占地面积：</TD>
									<TD><asp:label id="LabelTotalFloorSpace" runat="server"></asp:label></TD>
									<TD class="form-item" width="100px">用地面积：</TD>
									<TD><asp:label id="LabelBuildSpace" runat="server"></asp:label></TD>
									<TD class="form-item" width="100px">建筑面积：</TD>
									<TD><asp:label id="LabelTotalBuildingSpace" runat="server"></asp:label></TD>
								</TR>
								<tr>
									<TD class="form-item">地上建筑面积：</TD>
									<TD><asp:label id="LabelHouseBuildingSpace" runat="server"></asp:label></TD>
									<TD class="form-item">地下建筑面积：</TD>
									<TD colspan="3"><asp:label id="LabelUnderBuildingSpace" runat="server"></asp:label></TD>
								</tr>
								<tr>
									<TD class="form-item">容 积 率：</TD>
									<TD><asp:label id="LabelPlannedVolumeRate" runat="server"></asp:label></TD>
									<TD class="form-item">覆盖密度：</TD>
									<TD colspan="3"><asp:label id="LabelBuildingDensity" runat="server"></asp:label></TD>
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
									<TD class="form-item">备注：</TD>
									<TD colSpan="5"><asp:label id="lblDistrictRemark" Runat="server"></asp:label></TD>
								</TR>
							</table>
						</td>
					</tr>
					<tr id="trBuildingTree" style="DISPLAY: none" height="100%" runat="server">
						<td class="table" vAlign="top">
							<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%"><uc1:buildingtree id="ucBuildingTree" runat="server"></uc1:buildingtree></div>
						</td>
					</tr>
					<tr id="trBuilding" height="100%" runat="server">
						<td class="table" vAlign="top">
							<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
								<table id="Table4" cellSpacing="0" cellPadding="4" width="100%" height="100%">
									<tr>
										<td vAlign="top" noWrap align="center">
											<TABLE class="form" id="Table6" cellSpacing="0" cellPadding="0" width="100%">
												<TR>
													<TD class="form-item" noWrap width="110">名称：</TD>
													<TD noWrap width="22%"><asp:label id="lblBuildingName" Runat="server"></asp:label></TD>
													<!--<TD class="form-item" noWrap width="110">楼栋简称：</TD>
													<TD noWrap width="22%"><asp:label id="lblBuildingShortName" Runat="server"></asp:label></TD>-->
													<TD class="form-item" noWrap width="80">总 层 数：</TD>
													<TD noWrap width="22%"><asp:label id="lblFloorCount" Runat="server"></asp:label></TD>
												</TR>
												<TR>
													<TD class="form-item">产品类型：</TD>
													<TD><asp:label id="lblPBSTypeName" Runat="server"></asp:label></TD>
													<TD class="form-item">投资性质：</TD>
													<TD><asp:label id="lblInvestType" Runat="server"></asp:label></TD>
													<TD class="form-item">使用性质：</TD>
													<TD><asp:label id="lblUseType" Runat="server"></asp:label></TD>
												</TR>
												<TR>
													<TD class="form-item">所属区域：</TD>
													<TD><asp:label id="lblParentName" Runat="server"></asp:label></TD>
													<TD class="form-item">朝向：</TD>
													<TD><asp:label id="lblDirection" Runat="server"></asp:label></TD>
													<TD class="form-item">分配去向：</TD>
													<TD><asp:label id="lblWhither" Runat="server"></asp:label></TD>
												</TR>
												<tr>
													<TD class="form-item">计划面积：</TD>
													<TD><asp:label id="lblHouseArea" Runat="server"></asp:label></TD>
													<TD class="form-item">实测面积：</TD>
													<TD colspan="3"><asp:label id="lblRoomArea" Runat="server"></asp:label></TD>
													<!--
													<TD class="form-item">财务编码：</TD>
													<TD><asp:Label Runat="server" ID="lblSubjectSetDesc"></asp:Label></TD>
													-->
												</tr>
												<TR>
													<TD class="form-item">单位：</TD>
													<TD colspan="5"><asp:label id="lblDevelopUnit" Runat="server"></asp:label></TD>
												</TR>
												<TR>
													<TD class="form-item">备注：</TD>
													<TD colSpan="5"><asp:label id="lblRemark" Runat="server"></asp:label></TD>
												</TR>
											</TABLE>
											<table id="Table7" cellSpacing="0" cellPadding="4" width="100%">
												<tr>
													<td><igtab:ultrawebtab id="UltraWebTab1" runat="server" Width="100%" ImageDirectory="../Images/infragistics/images/"
															JavaScriptFileName="../Images/infragistics/20051/scripts/ig_webtab.js" JavaScriptFileNameCommon="../Images/infragistics/20051/scripts/ig_shared.js"
															BorderColor="#949878" BorderStyle="Solid" ThreeDEffect="False" BorderWidth="1px">
															<DefaultTabStyle Height="22px" Font-Size="8pt" Font-Names="Microsoft Sans Serif" ForeColor="Black"
																BackColor="#FEFCFD">
																<Padding Top="2px"></Padding>
															</DefaultTabStyle>
															<RoundedImage LeftSideWidth="7" RightSideWidth="6" ShiftOfImages="2" SelectedImage="ig_tab_winXP1.gif"
																NormalImage="ig_tab_winXP3.gif" HoverImage="ig_tab_winXP2.gif" FillStyle="LeftMergedWithCenter"></RoundedImage>
															<SelectedTabStyle>
																<Padding Bottom="2px"></Padding>
															</SelectedTabStyle>
															<Tabs>
																<igtab:Tab Text="- 楼栋户型 -">
																	<ContentTemplate>
																		<TABLE cellSpacing="0" cellPadding="0" border="0" width="100%">
																			<TR>
																				<TD>
																					&nbsp;&nbsp; <INPUT class="button-small" onclick="doBModelBase('','SingleModify');return false;" type="button"
																						value=" 新 增 " id="btnModelAdd" runat="server">
																				</TD>
																			</TR>
																		</TABLE>
																		<TABLE cellSpacing="0" cellPadding="5" border="0" width="100%">
																			<tr>
																				<td>
																					<uc1:UCBuildingModelList id="UCBuildingModelList1" runat="server"></uc1:UCBuildingModelList></td>
																			</tr>
																		</TABLE>
																	</ContentTemplate>
																</igtab:Tab>
																<igtab:Tab Text="- 位 置 -">
																	<ContentTemplate>
																		<TABLE cellSpacing="0" cellPadding="0" border="0" width="100%">
																			<TR>
																				<TD>
																					&nbsp;&nbsp; <INPUT class="button-small" onclick="doBStationBase('','SingleModify');return false;" type="button"
																						value=" 新 增 " id="btnStationAdd" runat="server">
																				</TD>
																			</TR>
																		</TABLE>
																		<TABLE cellSpacing="0" cellPadding="5" border="0" width="100%">
																			<tr>
																				<td>
																					<uc1:UCBuildingStationList id="UCBuildingStationList1" runat="server"></uc1:UCBuildingStationList></td>
																			</tr>
																		</TABLE>
																	</ContentTemplate>
																</igtab:Tab>
																<igtab:Tab Text="- 功 能 -">
																	<ContentTemplate>
																		<TABLE cellSpacing="0" cellPadding="0" border="0" width="100%">
																			<TR>
																				<TD>
																					&nbsp;&nbsp; <INPUT class="button-small" onclick="doBFunctionBase('','SingleModify');return false;"
																						type="button" value=" 新 增 " id="btnFunctionAdd" runat="server">
																				</TD>
																			</TR>
																		</TABLE>
																		<TABLE cellSpacing="0" cellPadding="5" border="0" width="100%">
																			<tr>
																				<td>
																					<uc1:UCBuildingFunctionList id="UCBuildingFunctionList1" runat="server"></uc1:UCBuildingFunctionList></td>
																			</tr>
																		</TABLE>
																	</ContentTemplate>
																</igtab:Tab>
															</Tabs>
														</igtab:ultrawebtab></td>
												</tr>
											</table>
										</td>
										<td vAlign="top" noWrap align="center" width="200"><uc1:ucpicgroup id="UCPicGroup1" runat="server"></uc1:ucpicgroup></td>
									</tr>
								</table>
							</div>
						</td>
					</tr>
					<tr id="trA3" runat="server">
						<td height="12">
							<table id="Table11" cellSpacing="0" cellPadding="0" width="100%" border="0">
								<tr>
									<td background="../images/corl_bg.gif"><IMG height="12" src="../images/corl.gif" width="12"></td>
									<td width="12"><IMG height="12" src="../images/corr.gif" width="12"></td>
								</tr>
							</table>
						</td>
					</tr>
					<tr id="trA4" runat="server">
						<td bgColor="#e4eff6" height="6"></td>
					</tr>
					<tr id="trB2" style="DISPLAY: none" height="30" runat="server">
						<td class="table" vAlign="middle" align="center"><input class="submit" id="btnClose" onclick="javascript:self.close()" type="button" value="关 闭"
								name="btnClose">
						</td>
					</tr>
				</TBODY>
			</table>
			<div id="divHintLoad" style="DISPLAY: none; LEFT: 1px; WIDTH: 100%; POSITION: absolute; TOP: 200px; BACKGROUND-COLOR: transparent">
				<TABLE id="tableHintLoad" height="100" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD vAlign="top" align="center"><iframe id="frameLoad" src="../Cost/LoadingPrepare.htm" frameBorder="no" width="100%" scrolling="auto"
								height="100%"></iframe>
						</TD>
					</TR>
				</TABLE>
			</div>
			<div id="divHintSave" style="DISPLAY: none; LEFT: 1px; WIDTH: 100%; POSITION: absolute; TOP: 200px">
				<TABLE id="tableHintSave" height="100" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD vAlign="top" align="center"><iframe id="frameSave" src="../Cost/SavingWating.htm" frameBorder="no" width="100%" scrolling="auto"
								height="100%"></iframe>
						</TD>
					</TR>
				</TABLE>
			</div>
			<input id="txtBuildingCode" type="hidden" name="txtBuildingCode" runat="server">
			<input id="txtAct" type="hidden" name="txtAct" runat="server"> <input id="txtFromUrl" type="hidden" name="txtFromUrl" runat="server">
			<input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server"> <input id="txtIsArea" type="hidden" name="txtIsArea" runat="server">
			<input id="txtParentCode" type="hidden" name="txtParentCode" runat="server"> <input id="txtOpenModal" type="hidden" name="txtOpenModal" runat="server">
			<input id="txtPBSUnitCode" type="hidden" name="txtPBSUnitCode" runat="server">
		</form>
		<SCRIPT language="javascript">
<!--

var CurrUrl = "PBSBuildInfo.aspx?BuildingCode=" + Form1.txtBuildingCode.value + "&FromUrl=" + Form1.txtFromUrl.value;

function GoBack()
{
	if (Form1.txtFromUrl.value == "")
	{
		window.history.go(-1);
	}
	else
	{
		window.location.href = Form1.txtFromUrl.value;
	}
}

	//编辑区域、楼栋
	function Modify(){
		var w = 700;
		var h = 540;
		
		/*
		if (Form1.txtIsArea.value == "1")
		{
			w = 400;
			h = 160;
		}
		*/
		
		window.open("PBSBuildModify.aspx?Action=Modify&BuildingCode="+Form1.txtBuildingCode.value, "楼栋修改" , "top="+(screen.height-h)/2+",left="+(screen.width-w)/2+",width="+w+",height="+h+",scrollbars=0,resizable=1,status:no;");
//	window.location.href = "PBSBuildModify.aspx?FromUrl=" + escape(CurrUrl) + "&BuildingCode=" + Form1.txtBuildingCode.value;
	}

//显示按钮
function winload()
{
	if (Form1.txtIsArea.value == "1")
	{
		Form1.btnAddArea.style.display = "";
		Form1.btnAddBuilding.style.display = "";
		Form1.btnGotoBuildingGraph.style.display = "";
		Form1.btnGotoBuildingPart.style.display = "none";
	}
	else
	{
		Form1.btnAddArea.style.display = "none";
		Form1.btnAddBuilding.style.display = "none";
		Form1.btnGotoBuildingGraph.style.display = "none";
		Form1.btnGotoBuildingPart.style.display = "";
	}
}

//查看单位工程
function GotoPBSUnitInfo()
{
	OpenCustomWindow("../PBS/PBSUnitInfo.aspx?action=view&FromUrl=" + escape(window.location) + "&PBSUnitCode=" + Form1.txtPBSUnitCode.value + "&ProjectCode=" + Form1.txtProjectCode.value, "单位工程", 700, 500);
//	document.all.divHintLoad.style.display = '';
//	window.location.href = "../PBS/PBSUnitFrame.aspx?FromUrl=" + escape(window.location) + "&PBSUnitCode=" + Form1.txtPBSUnitCode.value + "&ProjectCode=" + Form1.txtProjectCode.value;
}

//查看户型
function ViewModel(code)
{
	OpenLargeWindow("RoomModel.aspx?ProjectCode=" + Form1.txtProjectCode.value + "&ModelCode=" + code + "&FromUrl=" + escape(window.location), "");
}

//上传图
function UploadDtl(AttachMentCode, MasterCode, AttachMentType)
{
	OpenCustomWindow("../UserControls/SaveAttach.aspx?AttachMentCode=" + AttachMentCode + "&strMasterCode=" + MasterCode + "&strAttachMentType=" + AttachMentType, null,400,300);
}

function doBModelBase(code,dotype){
	var NowTime = new Date();
	var strBuildingCode = Form1.txtBuildingCode.value;
	var strProjectCode= Form1.txtProjectCode.value;
	
	var strURL = './BuildingModelModify.aspx?BuildingCode=' + strBuildingCode;
	
	strURL += '&ProjectCode=' + strProjectCode;
	
	strURL += '&CellCode=' + code;
	
	strURL += '&DoType=' + dotype;
	
	strURL += '&ct_'+ NowTime.getFullYear().toString() + '_' + NowTime.getMonth().toString() + '_' + NowTime.getDay().toString() + '_' + NowTime.getHours().toString() + '_' + NowTime.getMinutes().toString() + '_' + NowTime.getSeconds().toString() + '_' + NowTime.getMilliseconds().toString();
	
	var theWin = OpenLargeWindow(strURL,"BuildingModelModify");
	theWin.focus();
}

function doBStationBase(code,dotype){
	var NowTime = new Date();
	var strBuildingCode = Form1.txtBuildingCode.value;
	
	var strURL = './BuildingStationModify.aspx?BuildingCode=' + strBuildingCode;
	
	strURL += '&CellCode=' + code;
	
	strURL += '&DoType=' + dotype;
	
	strURL += '&ct_'+ NowTime.getFullYear().toString() + '_' + NowTime.getMonth().toString() + '_' + NowTime.getDay().toString() + '_' + NowTime.getHours().toString() + '_' + NowTime.getMinutes().toString() + '_' + NowTime.getSeconds().toString() + '_' + NowTime.getMilliseconds().toString();
	
	var theWin = OpenMiddleWindow(strURL,"BuildingStationModify");
	theWin.focus();
}

function doBFunctionBase(code,dotype){
	var NowTime = new Date();
	var strBuildingCode = Form1.txtBuildingCode.value;
	
	var strURL = './BuildingFunctionModify.aspx?BuildingCode=' + strBuildingCode;
	
	strURL += '&CellCode=' + code;
	
	strURL += '&DoType=' + dotype;
	
	strURL += '&ct_'+ NowTime.getFullYear().toString() + '_' + NowTime.getMonth().toString() + '_' + NowTime.getDay().toString() + '_' + NowTime.getHours().toString() + '_' + NowTime.getMinutes().toString() + '_' + NowTime.getSeconds().toString() + '_' + NowTime.getMilliseconds().toString();
	
	var theWin = OpenMiddleWindow(strURL,"BuildingFunctionModify");
	theWin.focus();
}

function ShowDistrict()
{
    var div = document.all("tableDistrictMore");
    if (div.style.display == "")
    {
        div.style.display = "none";
        document.all("hrefShowDistrict").innerText = "展开区域信息";
    }
    else
    {
        div.style.display = "";
        document.all("hrefShowDistrict").innerText = "折叠区域信息";
    }
}

//-->
		</SCRIPT>
	</body>
</HTML>
