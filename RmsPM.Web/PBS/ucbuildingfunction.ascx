<%@ Control Language="c#" Inherits="RmsPM.Web.PBS.UCBuildingFunction" CodeFile="UCBuildingFunction.ascx.cs" %>
<table id="ViewSingleTable" class="form" cellSpacing="0" cellPadding="0" width="100%" border="0"
	runat="server">
	<TR>
		<TD class="form-item" noWrap width="100">名称：</TD>
		<TD noWrap>
			<asp:Label id="LabelFunctionName" runat="server"></asp:Label>
		</TD>
	</TR>
	<TR>
		<TD class="form-item" noWrap width="100">单元数：</TD>
		<TD noWrap><FONT face="宋体">
				<asp:Label id="LabelFunctionNum" runat="server"></asp:Label></FONT>
		</TD>
	</TR>
	<TR>
		<TD class="form-item" noWrap width="100">面积：</TD>
		<TD noWrap>
			<asp:Label id="LabelFunctionArea" runat="server"></asp:Label>
			平米</TD>
	</TR>
	<TR>
		<TD class="form-item" noWrap width="100">备注：</TD>
		<TD noWrap><FONT face="宋体">
				<asp:Label id="LabelFunctionRemark" runat="server"></asp:Label></FONT>
		</TD>
	</TR>
</table>
<table id="ModifySingleTableTable" class="form" cellSpacing="0" cellPadding="0" width="100%"
	border="0" runat="server">
	<TR>
		<TD class="form-item" noWrap width="100">名称：</TD>
		<TD noWrap><INPUT type="text" id="TextFunctionName" name="TextFunctionName" runat="server" class="input"><font color="red">*</font></TD>
	</TR>
	<TR>
		<TD class="form-item" noWrap width="100">单元数：</TD>
		<TD noWrap><INPUT type="text" class="input-nember" id="TextFunctionNum" name="TextFunctionNum" runat="server"></TD>
	</TR>
	<TR>
		<TD class="form-item" noWrap width="100">面积：</TD>
		<TD noWrap><INPUT type="text" class="input-nember" id="TextFunctionArea" name="TextFunctionArea" runat="server">平米</TD>
	</TR>
	<TR>
		<TD class="form-item" noWrap width="100">备注：</TD>
		<TD noWrap><TEXTAREA rows="3" cols="40" id="TextAreaFunctionRemark" name="TextAreaFunctionRemark" runat="server"></TEXTAREA></TD>
	</TR>
</table>
<INPUT type="hidden" id="HideBuildingCode" name="HideBuildingCode" runat="server"><INPUT type="hidden" id="HideBuildingFunctionCode" name="HideBuildingFunctionCode" runat="server"><INPUT type="hidden" id="HideDoType" name="HideDoType" runat="server">
