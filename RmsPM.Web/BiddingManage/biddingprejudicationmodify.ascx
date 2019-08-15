<%@ Reference Control="~/workflowcontrol/workflowtoolbar.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AttachMentList" Src="../UserControls/AttachMentList.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AttachMentAdd" Src="../UserControls/AttachMentAdd.ascx" %>
<%@ Control Language="c#" Inherits="RmsPM.Web.BiddingManage.BiddingPrejudicationModify" CodeFile="BiddingPrejudicationModify.ascx.cs" %>
<div id="OperableDiv" runat="server">
	<table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
		<tr>
			<td class="blackbordertd" width="25%">&nbsp;&nbsp;&nbsp;��Ŀ���ƣ�</td>
			<td class="blackbordertdcontent" id="txtProjectName" width="45%" runat="server">&nbsp;</td>
			<td class="blackbordertd" width="15%">&nbsp;&nbsp;&nbsp;��&nbsp;&nbsp;&nbsp;&nbsp;�ţ�</td>
			<td class="blackbordertdcontent" width="15%"><INPUT class="input-nember" id="txtNumber" name="txtNumber" type="text" runat="server"
					onblur="BiddingPrejudicationNemberCheckMoney(this);" style="WIDTH: 97px" size="10">
				<FONT face="����" color="#cc0066">*</FONT></td>
		</tr>
		<tr>
			<td class="blackbordertd">&nbsp;&nbsp;&nbsp;���б��Σ�</td>
			<td class="blackbordertdcontent" id="txtBiddingTitle" runat="server">&nbsp;</td>
			<td class="blackbordertd">&nbsp;&nbsp;&nbsp;�ƻ��������ڣ�</td>
			<td class="blackbordertdcontent" id="txtEmitDate" runat="server">&nbsp;</td>
		</tr>
		<tr>
			<td class="blackbordertd" colSpan="2">&nbsp;&nbsp;&nbsp;1.���б���֮������Χ����(�����б귶Χʾ��ͼ)
				<uc1:AttachMentAdd id="AttachMentAdd1" runat="server" CtrlPath="../UserControls/"></uc1:AttachMentAdd>
				&nbsp;&nbsp;&nbsp;2.���б굥λ֮�ʸ�Ҫ��/��������(����)
				<uc1:AttachMentAdd id="AttachMentAdd2" runat="server" CtrlPath="../UserControls/"></uc1:AttachMentAdd>
				&nbsp;&nbsp;&nbsp;3.�μ��ʸ�Ԥ��ĵ�λ�����������������:
				<uc1:AttachMentAdd id="AttachMentAdd3" runat="server" CtrlPath="../UserControls/"></uc1:AttachMentAdd>
			</td>
			<td class="blackbordertd" colSpan="2" valign="bottom">������</td>
		</tr>
	</table>
</div>
<div id="EyeableDiv" runat="server">
	<TABLE cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
		<TBODY>
			<TR>
				<TD class="blackbordertd" width="25%">
					&nbsp;&nbsp;&nbsp;��Ŀ���ƣ�</TD>
				<TD class="blackbordertdcontent" id="tdProjectName" width="45%" runat="server">&nbsp;</TD>
				<TD class="blackbordertd" width="15%">
					&nbsp;&nbsp;&nbsp;�� �ţ�</TD>
				<TD class="blackbordertdcontent" width="15%" runat="server" id="tdNumber">&nbsp;</TD>
			</TR>
			<TR>
				<TD class="blackbordertd">
					&nbsp;&nbsp;&nbsp;���б��Σ�</TD>
				<TD class="blackbordertdcontent" id="tdBiddingTitle" runat="server">&nbsp;</TD>
				<TD class="blackbordertd">
					&nbsp;&nbsp;&nbsp;�ƻ��������ڣ�</TD>
				<TD class="blackbordertdcontent" id="tdEmitDate" runat="server">&nbsp;</TD>
			</TR>
			<TR>
				<TD class="blackbordertd" colSpan="2">1.���б���֮������Χ����(�����б귶Χʾ��ͼ)<br>
					2.���б굥λ֮�ʸ�Ҫ��/��������(����)<br>
					3.�μ��ʸ�Ԥ��ĵ�λ�����������������:<br>
				</TD>
				<TD class="blackbordertd" colSpan="2"><FONT face="����"></FONT>
					<uc1:AttachMentList id="AttachMentList1" runat="server" CtrlPath="../UserControls/"></uc1:AttachMentList><BR>
					<uc1:AttachMentList id="AttachMentList2" runat="server" CtrlPath="../UserControls/"></uc1:AttachMentList>
					<BR>
					<uc1:AttachMentList id="AttachMentList3" runat="server" CtrlPath="../UserControls/"></uc1:AttachMentList>������</TD>
			</TR>
		</TBODY>
	</TABLE>
</div>
<script language="javascript">
function BiddingPrejudicationCheckSubmit()
{
	//if(document.all("<%=ClientID%>_txtNumber").value == "")
	//{
	//	alert('��ű�����д');
       // return false;
	//}
	return true;
}
function BiddingPrejudicationNemberCheckMoney(obj)
{
	/*if(obj.value.length>0)
	{
		if(obj.value.match("^[0-9]+(\.[0-9]+)?$")==null)
		{
			obj.select();
			obj.focus();
			alert("�������������");
			obj.select();
			return false;
		}
	}*/
	//return true;				
}
function BiddingPrejudicationOpenSupplierPage(code,state,selectstate)
{
	OpenLargeWindow('BiddingSupplierList.aspx?BiddingPrejudicationCode='+code+'&State='+state+'&Select='+selectstate,'Ͷ�굥λѡ��');
}
</script>
