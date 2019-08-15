<%@ Register TagPrefix="uc1" TagName="InputUsers" Src="../UserControls/InputUsers.ascx" %>
<%@ Page language="c#" Inherits="RmsPM.Web.Project.ProjectModify" CodeFile="ProjectModify.aspx.cs" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics.WebUI.WebDataInput.v5.1, Version=5.1.20051.37, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>��Ŀ�޸�</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<LINK href="../Images/infra.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
	</HEAD>
	<body scroll="no">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" bgColor="white" border="0">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">��Ŀ�޸�</td>
				</tr>
				<tr height="100%">
					<td vAlign="top">
						<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
							<TABLE class="form" id="Table4" cellSpacing="0" cellPadding="0" width="100%" border="0">
								<TR>
									<TD class="form-item">���ƣ�</TD>
									<TD><asp:textbox id="TextBoxProjectName" runat="server" CssClass="input"></asp:textbox><font color="red">*</font><span id="worningChange" runat="server"></span></TD>
									<TD class="form-item">��ƣ�</TD>
									<TD width="20%"><asp:textbox id="TextBoxProjectShortName" runat="server" CssClass="input"></asp:textbox></TD>
									<TD class="form-item">�׶Σ�</TD>
									<TD width="20%"><SELECT id="SelectStatus" name="SelectStatus" runat="server">
											<OPTION value="" selected>��ѡ��</OPTION>
										</SELECT><font color="red">*</font></TD>
								</TR>
								<TR>
								    <TD class="form-item" runat="server" id="ShortUserTitle">���ñ�����</TD>
									<TD runat="server" id="ShortUserValue">
                                        <asp:RadioButtonList ID="RadioUseShortUserName" runat="server" RepeatColumns="2" RepeatLayout="Flow">
                                            <asp:ListItem Value="1">��</asp:ListItem>
                                            <asp:ListItem Value="0" Selected=True>��</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </TD>
<%--									<TD class="form-item">����˾��</TD>
									<TD width="20%"><SELECT id="sltUnit" name="sltSubjectSet" runat="server"></SELECT></TD>--%>
									<TD class="form-item"><asp:label ID="lblSalProjectName0" runat="server">����ϵͳ��Ŀ��</asp:label></TD>
									<TD><SELECT id="sltSalProjectCode" name="sltSalProjectCode" runat="server">
											<OPTION value="" selected>--------��ѡ��--------</OPTION>
										</SELECT>
									</TD>
									<TD class="form-item">��ţ�</TD>
									<TD><asp:textbox id="txtProjectID" runat="server" CssClass="input"></asp:textbox></TD>
								</TR>
								<tr>
									<TD class="form-item">���赥λ��</TD>
									<TD><SELECT id="sltDevelopUnit" style="WIDTH: 136px" name="sltDevelopUnit" runat="server">
											<OPTION value="" selected>--------��ѡ��--------</OPTION>
										</SELECT></TD>
									<TD class="form-item">ע���ַ��</TD>
									<TD colSpan="3"><asp:textbox id="txtDevelopUnitAddress" runat="server" CssClass="input" Width="250"></asp:textbox></TD>
								</tr>
								<TR>
									<TD class="form-item">���У�</TD>
									<TD><asp:textbox id="TextBoxCity" runat="server" CssClass="input"></asp:textbox></TD>
									<TD class="form-item">����</TD>
									<TD><asp:textbox id="TextBoxArea" runat="server" CssClass="input"></asp:textbox></TD>
									<TD class="form-item">��ַ��</TD>
									<TD><asp:textbox id="ProjectAddress" runat="server" CssClass="input"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="form-item">ռ�������</TD>
									<TD><igtxt:webnumericedit id="txtTotalFloorSpace" runat="server" CssClass="infra-input-nember" Width="100px"
											JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js"
											ImageDirectory="../images/infragistics/images/" MinDecimalPlaces="none"></igtxt:webnumericedit>ƽ��</TD>
									<TD class="form-item">�õ������</TD>
									<TD><igtxt:webnumericedit id="txtBuildSpace" runat="server" CssClass="infra-input-nember" Width="100px" JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js"
											JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js" ImageDirectory="../images/infragistics/images/"
											MinDecimalPlaces="none"></igtxt:webnumericedit>ƽ��</TD>
									<TD class="form-item">���������</TD>
									<TD><igtxt:webnumericedit id="txtTotalBuildingSpace" runat="server" CssClass="infra-input-nember" Width="100px"
											JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js"
											ImageDirectory="../images/infragistics/images/" MinDecimalPlaces="none"></igtxt:webnumericedit>ƽ��</TD>
								</TR>
								<tr>
									<TD class="form-item">���Ͻ��������</TD>
									<TD><igtxt:webnumericedit id="txtHouseBuildingSpace" runat="server" CssClass="infra-input-nember" Width="100px"
											JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js"
											ImageDirectory="../images/infragistics/images/" MinDecimalPlaces="none"></igtxt:webnumericedit>ƽ��</TD>
									<!--<TD class="form-item">�̰콨�������</TD>
									<TD><igtxt:webnumericedit id="txtBsBuildingSpace" runat="server" CssClass="infra-input-nember" Width="100px"
											JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js"
											ImageDirectory="../images/infragistics/images/" MinDecimalPlaces="none"></igtxt:webnumericedit>ƽ��</TD>-->
									<TD class="form-item">���½��������</TD>
									<TD colspan="3"><igtxt:webnumericedit id="txtUnderBuildingSpace" runat="server" CssClass="infra-input-nember" Width="100px"
											JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js"
											ImageDirectory="../images/infragistics/images/" MinDecimalPlaces="none"></igtxt:webnumericedit>ƽ��</TD>
								</tr>
								<TR>
									<TD class="form-item">�� �� �ʣ�</TD>
									<TD><asp:textbox id="TextBoxPlannedVolumeRate" runat="server" CssClass="input-nember" Width="100"></asp:textbox></TD>
									<TD class="form-item">�����ܶȣ�</TD>
									<TD><igtxt:webnumericedit id="txtBuildingDensity" runat="server" CssClass="infra-input-nember" Width="100px"
											JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js"
											ImageDirectory="../images/infragistics/images/" MinDecimalPlaces="none"></igtxt:webnumericedit>%</TD>
									<td class="form-item">��Ŀ�ܼࣺ</td>
									<td><uc1:inputusers id="ucManager" runat="server"></uc1:inputusers></td>
								</TR>
								<TR>
									<TD class="form-item">���������</TD>
									<TD><igtxt:webnumericedit id="txtBuildingSpaceForVolumeRate" runat="server" CssClass="infra-input-nember"
											Width="100px" JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js"
											ImageDirectory="../images/infragistics/images/" MinDecimalPlaces="none"></igtxt:webnumericedit>ƽ��</TD>
									<TD class="form-item">�������������</TD>
									<TD colspan="3"><igtxt:webnumericedit id="txtBuildingSpaceNotVolumeRate" runat="server" CssClass="infra-input-nember"
											Width="100px" JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js"
											ImageDirectory="../images/infragistics/images/" MinDecimalPlaces="none"></igtxt:webnumericedit>ƽ��</TD>
								</TR>
								<TR>
									<TD class="form-item">�̻������</TD>
									<TD><igtxt:webnumericedit id="txtAfforestingSpace" runat="server" CssClass="infra-input-nember" Width="100px"
											JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js"
											ImageDirectory="../images/infragistics/images/" MinDecimalPlaces="none"></igtxt:webnumericedit>ƽ��</TD>
									<TD class="form-item">�� �� �ʣ�</TD>
									<TD colspan="3"><igtxt:webnumericedit id="txtAfforestingRate" runat="server" CssClass="infra-input-nember" Width="100px"
											JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js"
											ImageDirectory="../images/infragistics/images/" MinDecimalPlaces="none"></igtxt:webnumericedit>%</TD>
									<!--<TD class="form-item">�����̵������</TD>
									<TD><igtxt:webnumericedit id="txtCenterAfforestingSpace" runat="server" CssClass="infra-input-nember" Width="100px"
											JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js"
											ImageDirectory="../images/infragistics/images/" MinDecimalPlaces="none"></igtxt:webnumericedit>ƽ��</TD>-->
								</TR>
								<TR>
								<!--<TD class="form-item">�����̻��ʣ�</TD>
									<TD><igtxt:webnumericedit id="txtCenterAfforestingRate" runat="server" CssClass="infra-input-nember" Width="100px"
											JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js"
											ImageDirectory="../images/infragistics/images/" MinDecimalPlaces=None></igtxt:webnumericedit>%</TD>-->
									<TD class="form-item">ˮ�������</TD>
									<TD><igtxt:webnumericedit id="txtWaterSpace1" runat="server" CssClass="infra-input-nember" Width="100px"
											JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js"
											ImageDirectory="../images/infragistics/images/" MinDecimalPlaces="none"></igtxt:webnumericedit>ƽ��</TD>
									<TD class="form-item">��Χ�����</TD>
									<TD colspan="3"><igtxt:webnumericedit id="txtPeripherySpace1" runat="server" CssClass="infra-input-nember" Width="100px"
											JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js"
											ImageDirectory="../images/infragistics/images/"></igtxt:webnumericedit>ƽ��</TD>
									
								</TR>
								<TR>
									
									<TD class="form-item">����ͣ��λ��</TD>
									<TD><igtxt:webnumericedit id="txtParkingSpace" runat="server" CssClass="infra-input-nember" Width="100px"
											JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js"
											ImageDirectory="../images/infragistics/images/"></igtxt:webnumericedit></TD>
									<TD class="form-item">����ͣ��λ��</TD>
									<TD colspan="3"><igtxt:webnumericedit id="txtUnderParkingSpace" runat="server" CssClass="infra-input-nember" Width="100px"
											JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js"
											ImageDirectory="../images/infragistics/images/"></igtxt:webnumericedit></TD>
								</TR>
								<TR>
									<TD class="form-item">�������ڣ�</TD>
									<TD><cc3:calendar id="kgDate" runat="server" CalendarResource="../Images/CalendarResource/" ReadOnly="False"
											Display="True" Value=""></cc3:calendar><font color="red">*</font></TD>
									<TD class="form-item">�������ڣ�</TD>
									<TD><cc3:calendar id="jgDate" runat="server" CalendarResource="../Images/CalendarResource/" ReadOnly="False"
											Display="True" Value=""></cc3:calendar><font color="red">*</font></TD>
									<TD class="form-item">�� �� ����</TD>
									<TD><igtxt:webnumericedit id="txtHouseCount" runat="server" CssClass="infra-input-nember" Width="100px" JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js"
											JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js" ImageDirectory="../images/infragistics/images/"></igtxt:webnumericedit></TD>
								</TR>
								<!--
								<TR>
									<TD class="form-item">סլ��;��</TD>
									<TD colspan="5"><asp:textbox id="txtHouseUse" runat="server" CssClass="input"></asp:textbox></TD>-->
									<!--
									<TD class="form-item">���׷����ͣ�</TD>
									<TD><asp:textbox id="txtPTFeeType" runat="server" CssClass="input"></asp:textbox></TD>
									<TD class="form-item">���׷�ƾ֤�ţ�</TD>
									<TD><asp:textbox id="txtPTFeeVoucherID" runat="server" CssClass="input"></asp:textbox></TD>
								</TR>
								-->
								<TR>
									<TD class="form-item">��ע��</TD>
									<TD colSpan="5"><asp:textbox id="TextBoxRemark" runat="server" Rows="3" width="100%" TextMode="MultiLine"></asp:textbox></TD>
								</TR>
								<tr style="DISPLAY: none">
									<TD class="form-item">�ؿ飺</TD>
									<TD><asp:textbox id="TextBoxBlockName" runat="server" CssClass="input"></asp:textbox></TD>
									<TD class="form-item">�ؿ��ţ�</TD>
									<TD><asp:textbox id="TextBoxBlockID" runat="server" CssClass="input"></asp:textbox></TD>
								</tr>
								<TR style="DISPLAY: none">
									<TD class="form-item">��ʼ���ڣ�</TD>
									<TD><cc3:calendar id="PlanStartDate" runat="server" CalendarResource="../Images/CalendarResource/"
											ReadOnly="False" Display="True" Value=""></cc3:calendar></TD>
									<TD class="form-item">�������ڣ�</TD>
									<TD><cc3:calendar id="PlanEndDate" runat="server" CalendarResource="../Images/CalendarResource/" ReadOnly="False"
											Display="True" Value=""></cc3:calendar></TD>
								</TR>
							</TABLE>
						</div>
					</td>
				</tr>
				<tr>
					<td>
						<table cellSpacing="10" width="100%">
							<tr>
								<td align="center"><input class="submit" id="btnSave" type="button" value="ȷ ��" name="btnSave" runat="server" onserverclick="btnSave_ServerClick">
									<input class="submit" id="btnCancel" onclick="javascript:self.close()" type="button" value="ȡ ��"
										name="btnCancel">
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<input id="txtFromUrl" type="hidden" name="txtFromUrl" runat="server"><input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server">
			<input id="txtUsers" type="hidden" name="txtUsers" runat="server"><input id="sltUnit" type="hidden" name="sltUnit" runat="server">
		</form>
		<SCRIPT language="javascript">
<!--

function GoBack()
{
	window.location.href = Form1.txtFromUrl.value;
}
var tmp = '<%=Request["hSelect"]%>';
if(tmp.length>0)
	window.document.all.SelectName.innerText = '<%=Request["hSelect"]%>';
	

//-->
		</SCRIPT>
	</body>
</HTML>
