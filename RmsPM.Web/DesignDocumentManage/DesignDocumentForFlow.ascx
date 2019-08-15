<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DesignDocumentForFlow.ascx.cs" Inherits="RmsPM.Web.DesignDocumentManage.DesignDocumentManage_DesignDocumentForFlow" %>
<%@ Register TagPrefix="uc1" TagName="AttachMentAdd" Src="../UserControls/AttachMentAdd.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AttachMentList" Src="../UserControls/AttachMentList.ascx" %>
<%@ Register TagPrefix="uc1" TagName="UCDuty" Src="../UserControls/UCDuty.ascx" %>

<div id="OperableDiv" runat="server">
	<table class="table" cellSpacing="0" cellPadding="0" width="100%" align="center" border="1">
		<tr>
			<td class="blackbordertd">方案名称</td>
			<td class="blackbordertd"><INPUT id="txtTitle" type="text" runat="server" class="input"><font color="red">*</font>
			<td class="blackbordertd">所属项目</td>
			<td runat="server" id="txtProjectCode" class="blackbordertd"></td>
		</tr>
		<tr>
			<td class="blackbordertd">发起部门</td>
			<td colspan="3" class="blackbordertd"><uc1:ucduty id="txtUnitCode" runat="server" CtrlPath="../UserControls/"></uc1:ucduty>
		</tr>
		<tr>
			<td class="blackbordertd">摘要</td>
			<td colspan="3" class="blackbordertd">
                <textarea id="txtContext" style="width:100%; height: 200px;" rows="10" runat="server" class="input"></textarea>
        </tr>
        <tr>
        <td class="blackbordertd">附件</td>
        <td colspan="3" class="blackbordertd"><uc1:AttachMentAdd id="AttachMentAdd1" runat="server"></uc1:AttachMentAdd></td>
        </tr>
	</table>
</div>
<div id="EyeableDiv" runat="server">
	<table class="table" cellSpacing="0" cellPadding="0" width="100%" align="center" border="1">
		<tr>
			<td class="blackbordertd">方案名称</td>
			<td runat="server" id="tdTitle" class="blackbordertdcontent"></td>
			<td class="blackbordertd">所属项目</td>
			<td runat="server" id="tdProjectCode" class="blackbordertdcontent"></td>
		</tr>
		<tr>
			<td class="blackbordertd">发起部门</td>
			<td runat="server" id="tdUnitCode" colspan="3" class="blackbordertd"></td>
		</tr>
		<tr>
			<td class="blackbordertd">摘要</td>
			<td runat="server" id="tdContext" colspan="3" class="blackbordertd"></td>
		</tr>
		<tr>
			<td class="blackbordertd">附件</td>
			<td runat="server" colspan="3" class="blackbordertd"><uc1:AttachMentList id="AttachMentList1" runat="server"></uc1:AttachMentList>&nbsp;</td>
		</tr>
	</table>
</div>
