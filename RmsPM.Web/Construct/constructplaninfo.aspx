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
						<input class="button" id="btnModify" onclick="Modify();" type="button" value="�޸ļƻ�" name="btnModify"
							runat="server"> <span style="DISPLAY: none"><input class="button" id="btnDelete" style="DISPLAY: none" onclick="Delete();" type="button"
								value="ɾ���ƻ�" name="btnDelete" runat="server"> <input class="button" id="btnAdd" style="DISPLAY: none" onclick="Add();" type="button"
								value="����ȼƻ�" name="btnAdd" runat="server"> <input class="button" id="btnModifyProgressStep" onclick="ModifyProgressStep();" type="button"
								value="�޸Ľ���" name="btnModifyProgressStep" runat="server"> </span><input class="button" id="btnShowPBSUnitWindow" style="DISPLAY: none" onclick="parent.OpenPBSUnitWindow();"
							type="button" value="��ʾ��λ����" name="btnShowPBSUnitWindow" runat="server"> <input class="button" id="btnGoBackList" onclick="GoBackList();" type="button" value="���ؼƻ��б�"
							name="btnGoBackList"> <IMG src="../images/btn_li.gif" align="absMiddle"> ��ȣ�<select id="sltYear" onchange="btnHiddenYear.click();" name="sltYear" runat="server"></select>
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
												<TD class="form-item" width="100">��λ���̣�</TD>
												<TD noWrap><A href="#" onclick="GotoPBSUnitInfo();return false;"><asp:label id="lblPBSUnitName" Runat="server"></asp:label></A></TD>
												<TD class="form-item" width="100">��ǰ������ȣ�</TD>
												<TD noWrap><asp:label id="lblPBSUnitVisualProgress" Runat="server"></asp:label></A></TD>
												<TD class="form-item" noWrap width="110">����ƻ�������ȣ�</TD>
												<TD noWrap><asp:label id="lblVisualProgress" Runat="server"></asp:label></TD>
											</TR>
											<TR>
												<td class="form-item" noWrap>¥������</td>
												<td noWrap><asp:label id="lblBuildingCount0" Runat="server" Visible="False">0</asp:label><A id="hrefBuilding" href="#" onclick="javascript:ShowEditMenu(this);" runat="server"><asp:label id="lblBuildingCount" Runat="server"></asp:label></A></td>
												<TD class="form-item" noWrap>���������</TD>
												<TD noWrap align="right"><asp:label id="lblBuildArea" Runat="server"></asp:label></TD>
												<TD class="form-item" noWrap>�����ת�����</TD>
												<TD noWrap align="right"><asp:label id="lblLCFArea" Runat="server"></asp:label></TD>
											</TR>
											<TR>
												<TD class="form-item" noWrap>�ƻ���Ͷ�ʣ�</TD>
												<TD noWrap align="right"><asp:label id="lblTotalPInvest" Runat="server"></asp:label></TD>
												<TD class="form-item" noWrap>�������Ͷ�ʣ�</TD>
												<TD noWrap align="right"><asp:label id="lblInvestBefore" Runat="server"></asp:label></TD>
												<TD class="form-item" noWrap>����ƻ�Ͷ�ʣ�</TD>
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
											<td class="intopic" width="200">���ȱ���</td>
											<td width="160"><input class="button-small" onclick="AddProgress()" type="button" value="�������ȱ���" name="btnAddProgress"
													id="btnAddProgress" runat="server"></td>
											<td><A href="#" onclick="GotoProgressReportList();return false;">������ȱ���...</A></td>
										</tr>
									</table>
									<asp:datagrid id="dgProgress" runat="server" Width="100%" CssClass="list" CellPadding="0" AllowSorting="True"
										AutoGenerateColumns="False" PageSize="15">
										<ItemStyle CssClass=""></ItemStyle>
										<HeaderStyle CssClass="list-title"></HeaderStyle>
										<FooterStyle CssClass="list-title"></FooterStyle>
										<Columns>
											<asp:TemplateColumn HeaderText="��������">
												<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
												<ItemStyle HorizontalAlign="Left"></ItemStyle>
												<ItemTemplate>
													<a href="#" onclick="ViewProgress(this.Code);return false;" Code='<%# DataBinder.Eval(Container, "DataItem.ProgressCode") %>'><%# DataBinder.Eval(Container, "DataItem.ReportDate", "{0:yyyy-MM-dd}") %></a>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="������">
												<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
												<ItemStyle HorizontalAlign="Left"></ItemStyle>
												<ItemTemplate>
													<%# RmsPM.BLL.SystemRule.GetUserName(RmsPM.BLL.ConvertRule.ToString(DataBinder.Eval(Container, "DataItem.ReportPerson")) ) %>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:BoundColumn DataField="VisualProgressName" HeaderText="�������">
												<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
												<ItemStyle HorizontalAlign="Left"></ItemStyle>
											</asp:BoundColumn>
											<asp:TemplateColumn HeaderText="Ŀǰʩ������">
												<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
												<ItemStyle HorizontalAlign="Right"></ItemStyle>
												<ItemTemplate>
													<%# DataBinder.Eval(Container, "DataItem.CurrentLayer") %>
												</ItemTemplate>
												<FooterStyle HorizontalAlign="Right"></FooterStyle>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="���ȱ�������">
												<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
												<ItemStyle HorizontalAlign="Left"></ItemStyle>
												<ItemTemplate>
													<%# RmsPM.BLL.StringRule.TruncText(DataBinder.Eval(Container, "DataItem.Content"), 20) %>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="��������">
												<HeaderStyle HorizontalAlign="Left" Wrap="False"></HeaderStyle>
												<ItemStyle HorizontalAlign="Left"></ItemStyle>
												<ItemTemplate>
													<%# RmsPM.BLL.StringRule.TruncText(DataBinder.Eval(Container, "DataItem.RiskRemark"), 20) %>
												</ItemTemplate>
											</asp:TemplateColumn>
										</Columns>
										<PagerStyle NextPageText="��ҳ&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;��ҳ" HorizontalAlign="Center"
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
		if (!confirm("ȷʵҪ��ת" + y + "��ļƻ���"))
			return;
	}
	
	OpenCustomWindow("../Construct/AnnualPlanModify.aspx?PBSUnitCode=" + Form1.txtPBSUnitCode.value + "&action=insert", "�ƻ��޸�", 600, 500);
}

//�޸ļƻ�
function Modify()
{
	OpenCustomWindow("../Construct/AnnualPlanModify.aspx?PBSUnitCode=" + Form1.txtPBSUnitCode.value + "&IYear=" + Form1.sltYear.value + "&action=modify", "�ƻ��޸�", 600, 500);
}

//ɾ����ȼƻ�
function Delete()
{
	var y = Form1.sltYear.value;
	if(!window.confirm('ȷʵҪɾ��' + y + '��ȼƻ���'))
		return false;
		
	OpenCustomWindow("../Construct/AnnualPlanModify.aspx?AnnualPlanCode=" + Form1.txtAnnualPlanCode.value + "&action=delete", "�ƻ��޸�", 400, 300);
	return false;
}

//�������ȱ���
function AddProgress()
{
	OpenFullWindow("../Construct/ConstructProgressModify.aspx?PBSUnitCode=" + Form1.txtPBSUnitCode.value + "&action=insert", "���ȱ����޸�", 750, 540);
}

//�鿴���ȱ���
function ViewProgress(ProgressCode)
{
	OpenCustomWindow("../Construct/ProgressReportInfo.aspx?ProgressCode=" + ProgressCode, "���ȱ���", 750, 540);
}

//������д
function ModifyProgressStep()
{
	OpenCustomWindow("../Construct/ConstructProgressStepModify.aspx?PBSUnitCode=" + Form1.txtPBSUnitCode.value, "�����޸�", 500, 400);
}

//�鿴��λ����
function GotoPBSUnitInfo()
{
	OpenCustomWindow("../PBS/PBSUnitInfo.aspx?action=view&FromUrl=" + escape(parent.location) + "&PBSUnitCode=" + Form1.txtPBSUnitCode.value + "&ProjectCode=" + Form1.txtProjectCode.value, "��λ����", 700, 500);
//	window.parent.location.href = "../PBS/PBSUnitFrame.aspx?FromUrl=" + escape(parent.location) + "&PBSUnitCode=" + Form1.txtPBSUnitCode.value + "&ProjectCode=" + Form1.txtProjectCode.value;
}

//���ȱ����б�
function GotoProgressReportList()
{
	OpenLargeWindow("../Construct/ProgressReportList.aspx?PBSUnitCode=" + Form1.txtPBSUnitCode.value + "&ProjectCode=" + Form1.txtProjectCode.value, "���ȱ����б�");
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

//�鿴¥��
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
	
	Form1.btnShowPBSUnitCtrl.value = "���ص�λ����";
	Form1.btnShowPBSUnitCtrl.style.display = "none";
	ctrl.style.display = "block";
}

function ClosePBSUnitCtrl()
{
	var ctrl = document.all.tdSelectPBSUnitCtrl;
	
	Form1.btnShowPBSUnitCtrl.value = "��ʾ��λ����";
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
