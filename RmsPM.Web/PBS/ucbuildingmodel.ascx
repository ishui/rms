<%@ Control Language="c#" Inherits="RmsPM.Web.PBS.UCBuildingModel" CodeFile="UCBuildingModel.ascx.cs" %>
<table class="form" id="ViewSingleTable" cellSpacing="0" cellPadding="0" width="100%" border="0"
	runat="server">
	<TR>
		<TD class="form-item" style="HEIGHT: 17px" noWrap width="100">户型名称：</TD>
		<TD style="HEIGHT: 17px" noWrap colSpan="3"><FONT face="宋体"><asp:label id="LabelModelName" runat="server"></asp:label></FONT></TD>
	</TR>
	<TR>
		<TD class="form-item" noWrap width="100">位置：</TD>
		<TD noWrap width="50%"><FONT face="宋体"><asp:label id="LabelStationName" runat="server"></asp:label></FONT></TD>
		<TD class="form-item" noWrap width="100">功能：</TD>
		<TD noWrap width="50%"><asp:label id="LabelFunctionName" runat="server"></asp:label></TD>
	</TR>
	<TR>
		<TD class="form-item" noWrap width="100">单元数：</TD>
		<TD noWrap width="50%"><FONT face="宋体"><asp:label id="LabelBModelNum" runat="server"></asp:label></FONT></TD>
		<TD class="form-item" noWrap width="100">面积：</TD>
		<TD noWrap width="50%"><asp:label id="LabelBModelArea" runat="server"></asp:label></TD>
	</TR>
	<TR>
		<TD class="form-item" noWrap width="100">户型构成：</TD>
		<TD noWrap width="50%"><FONT face="宋体"><asp:label id="LabelStructure" runat="server"></asp:label></FONT></TD>
		<TD class="form-item" noWrap width="100">产品类型：</TD>
		<TD noWrap width="50%"><asp:label id="LabelHouseTypeName" runat="server"></asp:label></TD>
	</TR>
	<TR>
		<TD class="form-item" noWrap width="100">建筑面积：</TD>
		<TD noWrap width="50%"><asp:label id="LabelBuildArea" runat="server"></asp:label></TD>
		<TD class="form-item" noWrap width="100">套内面积：</TD>
		<TD noWrap width="50%"><asp:label id="LabelRoomArea" runat="server"></asp:label></TD>
	</TR>
	<TR>
		<TD class="form-item" noWrap width="100">备注：</TD>
		<TD noWrap colSpan="3"><asp:label id="LabelBModelRemark" runat="server"></asp:label></TD>
	</TR>
</table>
<table class="form" id="ModifySingleTableTable" cellSpacing="0" cellPadding="0" width="100%"
	border="0" runat="server">
	<TR>
		<TD class="form-item" noWrap width="100">户型：</TD>
		<TD noWrap colSpan="3"><font color="red"><SELECT id="SelectModelCode" name="SelectModelCode" runat="server">
					<OPTION value="" selected>-- 请选择 --</OPTION>
				</SELECT>*</font></TD>
	</TR>
	<TR>
		<TD class="form-item" noWrap width="100">位置：</TD>
		<TD noWrap width="50%"><FONT face="宋体"><SELECT id="SelectBuildingStationCode" name="SelectBuildingStationCode" runat="server">
					<OPTION value="" selected>-- 请选择 --</OPTION>
				</SELECT></FONT></TD>
		<TD class="form-item" noWrap width="100">功能：</TD>
		<TD noWrap width="50%"><SELECT id="SelectBuildingFunctionCode" name="SelectBuildingFunctionCode" runat="server">
				<OPTION value="" selected>-- 请选择 --</OPTION>
			</SELECT></TD>
	</TR>
	<TR>
		<TD class="form-item" noWrap width="100">单元数：</TD>
		<TD noWrap width="50%"><INPUT class="input-nember" id="TextBModelNum" type="text" name="TextBModelNum" runat="server"></TD>
		<TD class="form-item" noWrap width="100">面积：</TD>
		<TD noWrap width="50%"><INPUT class="input-nember" id="TextBModelArea" type="text" name="TextBModelArea" runat="server">平米</TD>
	</TR>
	<TR>
		<TD class="form-item" noWrap width="100">备注：</TD>
		<TD noWrap colSpan="3"><TEXTAREA id="TextAreaBModelRemark" name="TextAreaBModelRemark" rows="3" cols="40" runat="server"></TEXTAREA></TD>
	</TR>
</table>
<table id="ViewImgTable" runat="server" height="55%" cellSpacing="0" cellPadding="0" width="100%"
	border="0">
	<tr>
		<td class="table">
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="note">户型图</td>
				</tr>
			</table>
		</td>
	</tr>
	<tr height="100%">
		<td class="table" vAlign="top" align="left">
			<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%"><IMG id="imgMain" src="" name="imgMain" runat="server">
			</div>
		</td>
	</tr>
</table>
<INPUT id="HideProjectCode" type="hidden" name="HideProjectCode" runat="server">
<INPUT id="HideBuildingCode" type="hidden" name="HideBuildingCode" runat="server"><INPUT id="HideBuildingModelCode" type="hidden" name="HideBuildingModelCode" runat="server"><INPUT id="HideDoType" type="hidden" name="HideDoType" runat="server">
