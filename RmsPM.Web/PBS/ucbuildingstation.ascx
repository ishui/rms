<%@ Control Language="c#" Inherits="RmsPM.Web.PBS.UCBuildingStation" CodeFile="UCBuildingStation.ascx.cs" %>
<table class="form" id="ViewSingleTable" cellSpacing="0" cellPadding="0" width="100%" border="0"
	runat="server">
	<TR>
		<TD class="form-item" noWrap width="100">名称：</TD>
		<TD noWrap><asp:label id="LabelStationName" runat="server"></asp:label></TD>
	</TR>
	<TR>
		<TD class="form-item" noWrap width="100">单元数：</TD>
		<TD noWrap><asp:label id="LabelStationNum" runat="server"></asp:label></TD>
	</TR>
	<TR>
		<TD class="form-item" noWrap width="100">面积：</TD>
		<TD noWrap><asp:label id="LabelStationArea" runat="server"></asp:label>平米</TD>
	</TR>
	<TR>
		<TD class="form-item" noWrap width="100">可销售面积：</TD>
		<TD noWrap>
			<asp:Label id="LabelAreaForVolumeRate" runat="server"></asp:Label>平米</TD>
	</TR>
	<TR>
		<TD class="form-item" noWrap width="100">备注：</TD>
		<TD noWrap>
			<asp:Label id="LabelStationRemark" runat="server"></asp:Label></TD>
	</TR>
</table>
<table id="ModifySingleTableTable" runat="server" class="form" cellSpacing="0" cellPadding="0"
	width="100%" border="0">
	<TR>
		<TD class="form-item" noWrap width="100">名称：</TD>
		<TD noWrap><font color="red"><INPUT class="input" type="text" id="TextStationName" name="TextStationName" runat="server">*</font></TD>
	</TR>
	<TR>
		<TD class="form-item" noWrap width="100">单元数：</TD>
		<TD noWrap><INPUT class="input-nember" type="text" id="TextStationNum" name="TextStationNum" runat="server"></TD>
	</TR>
	<TR>
		<TD class="form-item" noWrap width="100">面积：</TD>
		<TD noWrap><INPUT class="input-nember" type="text" id="TextStationArea" name="TextStationArea" runat="server">平米</TD>
	</TR>
	<TR>
		<TD class="form-item" noWrap width="100">可销售面积：</TD>
		<TD noWrap><INPUT class="input-nember" type="text" id="TextAreaForVolumeRate" name="TextAreaForVolumeRate"
				runat="server">平米</TD>
	</TR>
	<TR>
		<TD class="form-item" noWrap width="100">备注：</TD>
		<TD noWrap><TEXTAREA rows="3" cols="40" id="TextAreaStationRemark" name="TextAreaStationRemark" runat="server"></TEXTAREA></TD>
	</TR>
</table>
<INPUT id="HideBuildingCode" type="hidden" name="HideBuildingCode" runat="server"><INPUT id="HideBuildingStationCode" type="hidden" name="HideBuildingStationCode" runat="server"><INPUT id="HideDoType" type="hidden" name="HideDoType" runat="server">