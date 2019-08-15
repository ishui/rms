<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DesignDocument.ascx.cs" Inherits="RmsPM.Web.DesignDocumentManage.DesignDocumentManage_DesignDocument" %>
<%@ Register TagPrefix="uc1" TagName="AttachMentAdd" Src="../UserControls/AttachMentAdd.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AttachMentList" Src="../UserControls/AttachMentList.ascx" %>
<%@ Register TagPrefix="uc1" TagName="UCDuty" Src="../UserControls/UCDuty.ascx" %>

<div id="OperableDiv" runat="server">
	<table class="form" cellSpacing="0" cellPadding="0" width="100%" align="center" border="1">
		<tr>
			<td class="form-item">方案名称</td>
			<td><INPUT id="txtTitle" type="text" runat="server" class="input"><font color="red">*</font></td>
			<td class="form-item">所属项目</td>
			<td runat="server" id="txtProjectCode"></td>
		</tr>
		<tr>
			<td class="form-item">发起部门</td>
			<td colspan="3"><uc1:ucduty id="txtUnitCode" runat="server" CtrlPath="../UserControls/"></uc1:ucduty>
		</tr>
		<tr>
			<td class="form-item">摘要</td>
			<td colspan="3">
                <textarea id="txtContext" style="width:100%; height: 190px;" runat="server" class="input"></textarea>
        </tr>
        <tr>
        <td class="form-item">附件</td>
        <td colspan="3"><uc1:AttachMentAdd id="AttachMentAdd1" runat="server"></uc1:AttachMentAdd></td>
        </tr>
	</table>
</div>
<div id="EyeableDiv" runat="server">
	<table class="form" cellSpacing="0" cellPadding="0" width="100%" align="center" border="1">
		<tr>
			<td class="form-item">方案名称</td>
			<td runat="server" id="tdTitle"></td>
			<td class="form-item">所属项目</td>
			<td runat="server" id="tdProjectCode"></td>
		</tr>
		<tr>
			<td class="form-item">发起部门</td>
			<td runat="server" id="tdUnitCode" colspan="3"></td>
		</tr>
		<tr>
			<td class="form-item">摘要</td>
			<td runat="server" id="tdContext" colspan="3"></td>
		</tr>
		<tr>
			<td class="form-item">附件</td>
			<td runat="server" colspan="3"><uc1:AttachMentList id="AttachMentList1" runat="server"></uc1:AttachMentList>&nbsp;</td>
		</tr>
	</table>
</div>
