<%@ Page language="c#" Inherits="RmsPM.Web.Construct.ConstructPlanInfo" CodeFile="ConstructPlanInfo.aspx.cs" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Register TagPrefix="uc1" TagName="ConstructPlanChart" Src="ConstructPlanChart.ascx" %>
<%@ Register TagPrefix="uc2" TagName="SelectPBSUnitCtrl" Src="../PBS/SelectPBSUnitCtrl.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ConstructPlanInfo</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../Images/ContentMenu.js"></script>
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
	</HEAD>
	<body style="BORDER-RIGHT: 0px" scroll="no">
		<form id="Form1" method="post" runat="server">
			<div style="DISPLAY: none"><input id="btnHiddenYear" type="button" name="btnHiddenYear" runat="server" onserverclick="btnHiddenYear_ServerClick">
				<input id="btnSetPBSUnit" type="button" value="btnSetPBSUnit" name="btnSetPBSUnit" runat="server" onserverclick="btnSetPBSUnit_ServerClick">
			</div>
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="tools-area" vAlign="top"><IMG src="../images/btn_li.gif" align="absMiddle">
						<input class="button" id="btnModify" onclick="Modify();" type="button" value="修改计划" name="btnModify"
							runat="server"> <span style="DISPLAY: none"><input class="button" id="btnDelete" style="DISPLAY: none" onclick="Delete();" type="button"
								value="删除计划" name="btnDelete" runat="server"> <input class="button" id="btnAdd" style="DISPLAY: none" onclick="Add();" type="button"
								value="新年度计划" name="btnAdd" runat="server"> <input class="button" id="btnModifyProgressStep" onclick="ModifyProgressStep();" type="button"
								value="修改进度" name="btnModifyProgressStep" runat="server"> </span><input class="button" id="btnShowPBSUnitWindow" style="DISPLAY: none" onclick="parent.OpenPBSUnitWindow();"
							type="button" value="显示单位工程" name="btnShowPBSUnitWindow" runat="server"> <input class="button" id="btnGoBackList" onclick="GoBackList();" type="button" value="返回计划列表"
							name="btnGoBackList"> <IMG src="../images/btn_li.gif" align="absMiddle"> 年度：<select id="sltYear" onchange="btnHiddenYear.click();" name="sltYear" runat="server"></select>
					</td>
				</tr>
				<tr height="100%">
					<td class="table" valign="top">
						<!--						<div id="divMain" style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%;POSITION: absolute">-->
						<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td vAlign="top">
									<TABLE class="form" id="Table4" cellSpacing="1" cellPadding="0" width="100%">
										<TBODY>
											<TR>
												<TD class="form-item" width="100">单位工程：</TD>
												<TD noWrap><A href="#" onclick="GotoPBSUnitInfo();return false;"><asp:label id="lblPBSUnitName" Runat="server"></asp:label></A></TD>
												<TD class="form-item" width="100">当前形象进度：</TD>
												<TD noWrap><asp:label id="lblPBSUnitVisualProgress" Runat="server"></asp:label></A></TD>
												<TD class="form-item" noWrap width="110">本年计划形象进度：</TD>
												<TD noWrap><asp:label id="lblVisualProgress" Runat="server"></asp:label></TD>
											</TR>
											<TR>
												<td class="form-item" noWrap>楼栋数：</td>
												<td noWrap><asp:label id="lblBuildingCount0" Runat="server" Visible="False">0</asp:label><A id="hrefBuilding" href="#" onclick="javascript:ShowEditMenu(this);" runat="server"><asp:label id="lblBuildingCount" Runat="server"></asp:label></A></td>
												<TD class="form-item" noWrap>建筑面积：</TD>
												<TD noWrap align="right"><asp:label id="lblBuildArea" Runat="server"></asp:label></TD>
												<TD class="form-item" noWrap>上年结转面积：</TD>
												<TD noWrap align="right"><asp:label id="lblLCFArea" Runat="server"></asp:label></TD>
											</TR>
											<TR>
												<TD class="form-item" noWrap>计划总投资：</TD>
												<TD noWrap align="right"><asp:label id="lblTotalPInvest" Runat="server"></asp:label></TD>
												<TD class="form-item" noWrap>上年完成投资：</TD>
												<TD noWrap align="right"><asp:label id="lblInvestBefore" Runat="server"></asp:label></TD>
												<TD class="form-item" noWrap>本年计划投资：</TD>
												<TD noWrap align="right"><asp:label id="lblPInvest" Runat="server"></asp:label></TD>
											</TR>
										</TBODY>
									</TABLE>
								</td>
							</tr>
							<tr height="100%">
								<td vAlign="top" style="PADDING-TOP:9px">
									<uc1:constructplanchart id="tbConstructPlanChart" runat="server"></uc1:constructplanchart>
								</td>
							</tr>
							<tr>
								<td vAlign="top" style="PADDING-TOP:9px">
									<table cellSpacing="0" cellPadding="0" width="100%" border="0">
										<tr>
											<td class="intopic" width="200">进度报告</td>
											<td width="160"><input class="button-small" onclick="AddProgress()" type="button" value="新增进度报告" name="btnAddProgress"
													id="btnAddProgress" runat="server"></td>
											<td><A href="#" onclick="GotoProgressReportList();return false;">更多进度报告...</A></td>
										</tr>
									</table>
									<asp:datagrid id="dgProgress" runat="server" Width="100%" CssClass="list" CellPadding="0" AllowSorting="True"
										AutoGenerateColumns="False" PageSize="15">
										<ItemStyle CssClass=""></ItemStyle>
										<HeaderStyle CssClass="list-title"></HeaderStyle>
										<FooterStyle CssClass="list-title"></FooterStyle>
										<Columns>
											<asp:TemplateColumn HeaderText="报告日期">
												<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
												<ItemStyle HorizontalAlign="Left"></ItemStyle>
												<ItemTemplate>
													<a href="#" onclick="ViewProgress(this.Code);return false;" Code='<%# DataBinder.Eval(Container, "DataItem.ProgressCode") %>'><%# DataBinder.Eval(Container, "DataItem.ReportDate", "{0:yyyy-MM-dd}") %></a>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="报告人">
												<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
												<ItemStyle HorizontalAlign="Left"></ItemStyle>
												<ItemTemplate>
													<%# RmsPM.BLL.SystemRule.GetUserName(RmsPM.BLL.ConvertRule.ToString(DataBinder.Eval(Container, "DataItem.ReportPerson")) ) %>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:BoundColumn DataField="VisualProgressName" HeaderText="形象进度">
												<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
												<ItemStyle HorizontalAlign="Left"></ItemStyle>
											</asp:BoundColumn>
											<asp:TemplateColumn HeaderText="目前施工层数">
												<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
												<ItemStyle HorizontalAlign="Right"></ItemStyle>
												<ItemTemplate>
													<%# DataBinder.Eval(Container, "DataItem.CurrentLayer") %>
												</ItemTemplate>
												<FooterStyle HorizontalAlign="Right"></FooterStyle>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="进度报告内容">
												<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
												<ItemStyle HorizontalAlign="Left"></ItemStyle>
												<ItemTemplate>
													<%# RmsPM.BLL.StringRule.TruncText(DataBinder.Eval(Container, "DataItem.Content"), 20) %>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="风险描述">
												<HeaderStyle HorizontalAlign="Left" Wrap="False"></HeaderStyle>
												<ItemStyle HorizontalAlign="Left"></ItemStyle>
												<ItemTemplate>
													<%# RmsPM.BLL.StringRule.TruncText(DataBinder.Eval(Container, "DataItem.RiskRemark"), 20) %>
												</ItemTemplate>
											</asp:TemplateColumn>
										</Columns>
										<PagerStyle NextPageText="下页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上页" HorizontalAlign="Center"
											CssClass="ListHeadTr"></PagerStyle>
									</asp:datagrid></td>
							</tr>
						</table>
						<!--						</div>-->
					</td>
				</tr>
			</table>
			<input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server"> <input id="txtPBSUnitCode" type="hidden" name="txtPBSUnitCode" runat="server">
			<input id="txtFromUrl" type="hidden" name="txtFromUrl" runat="server"> <input id="txtAnnualPlanCode" type="hidden" name="txtAnnualPlanCode" runat="server">
			<input id="txtBuildingCodes" type="hidden" name="txtBuildingCodes" runat="server"><input id="txtBuildingNames" type="hidden" name="txtBuildingNames" runat="server">
		</form>
		<SCRIPT language="javascript">
<!--

var CurrUrl = window.location.href;

function GoBack()
{
	window.location.href = Form1.txtFromUrl.value;
}

function GoBackList()
{
	window.parent.location.href = "../PBS/PBSUnitPlanList.aspx?ProjectCode=" + Form1.txtProjectCode.value + "&param=plan";
}

function Add()
{
	var y = Form1.sltYear.value;
	if (y != "")
	{
		if (!confirm("确实要结转" + y + "年的计划吗？"))
			return;
	}
	
	OpenCustomWindow("../Construct/AnnualPlanModify.aspx?PBSUnitCode=" + Form1.txtPBSUnitCode.value + "&action=insert", "计划修改", 600, 500);
}

//修改计划
function Modify()
{
	OpenCustomWindow("../Construct/AnnualPlanModify.aspx?PBSUnitCode=" + Form1.txtPBSUnitCode.value + "&IYear=" + Form1.sltYear.value + "&action=modify", "计划修改", 600, 500);
}

//删除年度计划
function Delete()
{
	var y = Form1.sltYear.value;
	if(!window.confirm('确实要删除' + y + '年度计划吗？'))
		return false;
		
	OpenCustomWindow("../Construct/AnnualPlanModify.aspx?AnnualPlanCode=" + Form1.txtAnnualPlanCode.value + "&action=delete", "计划修改", 400, 300);
	return false;
}

//新增进度报告
function AddProgress()
{
	OpenFullWindow("../Construct/ConstructProgressModify.aspx?PBSUnitCode=" + Form1.txtPBSUnitCode.value + "&action=insert", "进度报告修改", 750, 540);
}

//查看进度报告
function ViewProgress(ProgressCode)
{
	OpenCustomWindow("../Construct/ProgressReportInfo.aspx?ProgressCode=" + ProgressCode, "进度报告", 750, 540);
}

//进度填写
function ModifyProgressStep()
{
	OpenCustomWindow("../Construct/ConstructProgressStepModify.aspx?PBSUnitCode=" + Form1.txtPBSUnitCode.value, "进度修改", 500, 400);
}

//查看单位工程
function GotoPBSUnitInfo()
{
	OpenCustomWindow("../PBS/PBSUnitInfo.aspx?action=view&FromUrl=" + escape(parent.location) + "&PBSUnitCode=" + Form1.txtPBSUnitCode.value + "&ProjectCode=" + Form1.txtProjectCode.value, "单位工程", 700, 500);
//	window.parent.location.href = "../PBS/PBSUnitFrame.aspx?FromUrl=" + escape(parent.location) + "&PBSUnitCode=" + Form1.txtPBSUnitCode.value + "&ProjectCode=" + Form1.txtProjectCode.value;
}

//进度报告列表
function GotoProgressReportList()
{
	OpenLargeWindow("../Construct/ProgressReportList.aspx?PBSUnitCode=" + Form1.txtPBSUnitCode.value + "&ProjectCode=" + Form1.txtProjectCode.value, "进度报告列表");
//	window.parent.location.href = "../Construct/ProgressReport.aspx?FromUrl=" + escape(parent.location) + "&PBSUnitCode=" + Form1.txtPBSUnitCode.value + "&ProjectCode=" + Form1.txtProjectCode.value;
}

function ShowEditMenu(obj){
	var cssFile="../Images/ContentMenu.css";		
	var BuildingCodes = Form1.txtBuildingCodes.value;
	var BuildingNames = Form1.txtBuildingNames.value;
	
	if (BuildingCodes == "") return;
	
	var arrCode = BuildingCodes.split(",");
	var arrName = BuildingNames.split(",");

	var len = arrCode.length;
	if (len == 0) return;
	
	if (len == 1)
	{
		OpenBuildingInfo(arrCode[0]);
	}
	else
	{
		var Items = new Array(len - 1);

		for(var i=0;i<len;i++)
		{
			Items[i] = new Array(3);
			Items[i][0] = arrName[i];
			Items[i][1] = "";
			Items[i][2] = "OpenBuildingInfo('" + arrCode[i] + "');";
		}

		CreateContentMenu(Items,cssFile,event.x-1,event.y-1);
	}
}

//查看楼栋
function OpenBuildingInfo(BuildingCode)
{
	OpenCustomWindow("../PBS/PBSBuildInfo.aspx?BuildingCode=" + BuildingCode + "&OpenModal=open&action=view", "BuildingInfo", 700, 540);
}

/*
function SetPBSUnit(code, index)
{
	Form1.txtPBSUnitCode.value = code;
	Form1.btnSetPBSUnit.click();
}

function ShowPBSUnitCtrl()
{
	var ctrl = document.all.tdSelectPBSUnitCtrl;
	
	if (ctrl.style.display == "none")
	{
		OpenPBSUnitCtrl();
	}
	else
	{
		ClosePBSUnitCtrl();
	}
}

function OpenPBSUnitCtrl()
{
	var ctrl = document.all.tdSelectPBSUnitCtrl;
	
	Form1.btnShowPBSUnitCtrl.value = "隐藏单位工程";
	Form1.btnShowPBSUnitCtrl.style.display = "none";
	ctrl.style.display = "block";
}

function ClosePBSUnitCtrl()
{
	var ctrl = document.all.tdSelectPBSUnitCtrl;
	
	Form1.btnShowPBSUnitCtrl.value = "显示单位工程";
	Form1.btnShowPBSUnitCtrl.style.display = "block";
	ctrl.style.display = "none";
}

//OpenPBSUnitCtrl();
//PBSUnitCtrlSelectRow(Form1.txtPBSUnitCtrlIndex.value);
*/

//-->
		</SCRIPT>
	</body>
</HTML>
