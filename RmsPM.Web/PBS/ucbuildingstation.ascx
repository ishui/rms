<%@ Control Language="c#" Inherits="RmsPM.Web.PBS.UCBuildingStation" CodeFile="UCBuildingStation.ascx.cs" %>
<table class="form" id="ViewSingleTable" cellSpacing="0" cellPadding="0" width="100%" border="0"
	runat="server">
	<TR>
		<TD class="form-item" noWrap width="100">���ƣ�</TD>
		<TD noWrap><asp:label id="LabelStationName" runat="server"></asp:label></TD>
	</TR>
	<TR>
		<TD class="form-item" noWrap width="100">��Ԫ����</TD>
		<TD noWrap><asp:label id="LabelStationNum" runat="server"></asp:label></TD>
	</TR>
	<TR>
		<TD class="form-item" noWrap width="100">�����</TD>
		<TD noWrap><asp:label id="LabelStationArea" runat="server"></asp:label>ƽ��</TD>
	</TR>
	<TR>
		<TD class="form-item" noWrap width="100">�����������</TD>
		<TD noWrap>
			<asp:Label id="LabelAreaForVolumeRate" runat="server"></asp:Label>ƽ��</TD>
	</TR>
	<TR>
		<TD class="form-item" noWrap width="100">��ע��</TD>
		<TD noWrap>
			<asp:Label id="LabelStationRemark" runat="server"></asp:Label></TD>
	</TR>
</table>
<table id="ModifySingleTableTable" runat="server" class="form" cellSpacing="0" cellPadding="0"
	width="100%" border="0">
	<TR>
		<TD class="form-item" noWrap width="100">���ƣ�</TD>
		<TD noWrap><font color="red"><INPUT class="input" type="text" id="TextStationName" name="TextStationName" runat="server">*</font></TD>
	</TR>
	<TR>
		<TD class="form-item" noWrap width="100">��Ԫ����</TD>
		<TD noWrap><INPUT class="input-nember" type="text" id="TextStationNum" name="TextStationNum" runat="server"></TD>
	</TR>
	<TR>
		<TD class="form-item" noWrap width="100">�����</TD>
		<TD noWrap><INPUT class="input-nember" type="text" id="TextStationArea" name="TextStationArea" runat="server">ƽ��</TD>
	</TR>
	<TR>
		<TD class="form-item" noWrap width="100">�����������</TD>
		<TD noWrap><INPUT class="input-nember" type="text" id="TextAreaForVolumeRate" name="TextAreaForVolumeRate"
				runat="server">ƽ��</TD>
	</TR>
	<TR>
		<TD class="form-item" noWrap width="100">��ע��</TD>
		<TD noWrap><TEXTAREA rows="3" cols="40" id="TextAreaStationRemark" name="TextAreaStationRemark" runat="server"></TEXTAREA></TD>
	</TR>
</table>
<INPUT id="HideBuildingCode" type="hidden" name="HideBuildingCode" runat="server"><INPUT id="HideBuildingStationCode" type="hidden" name="HideBuildingStationCode" runat="server"><INPUT id="HideDoType" type="hidden" name="HideDoType" runat="server">