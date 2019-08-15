<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics.WebUI.WebDataInput.v5.1, Version=5.1.20051.37, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Control Language="c#" Inherits="RmsPM.Web.PBS.SearchRoom" CodeFile="SearchRoom.ascx.cs" %>
<table width="100%">
	<tr>
		<td noWrap>¥����<input class="input" id="txtSearchBuildingName" type="text" size="20" name="txtSearchBuildingName"
				runat="server"><A href="#" onclick="SelectBuilding();return false;"><IMG src="../images/ToolsItemSearch.gif" border="0"></A></td>
		<td noWrap>���ƺţ�<input class="input" id="txtSearchChamberName" type="text" size="10" name="txtSearchChamberName"
				runat="server"></td>
		<td noWrap>�Һţ�<input class="input" id="txtSearchRoomName" type="text" size="10" name="txtSearchRoomName"
				runat="server"></td>
	</tr>
</table>
<table id="divAdvSearch">
	<tr>
		<TD noWrap>��Ʒ���ͣ�<SELECT class="select" id="sltSearchPBSTypeCode" name="sltSearchPBSTypeCode" runat="server">
				<OPTION value="" selected>--��ѡ��--</OPTION>
			</SELECT>
			&nbsp;&nbsp;���ͣ�<SELECT class="select" id="sltSearchModelCode" name="sltSearchModelCode" runat="server">
				<OPTION value="" selected>--��ѡ��--</OPTION>
			</SELECT>
			&nbsp;&nbsp;ȥ��<input class="input" id="txtSearchOutAspect" type="text" size="10" name="txtSearchOutAspect"
				runat="server"> <A title="ѡ��ȥ��" onclick="SelectDict('ȥ��', 'txtSearchOutAspect')" href="#">
				<IMG src="../images/ToolsItemSearch.gif" border="0"></A>
		</TD>
	</tr>
	<tr>
		<td noWrap>�� �� ����<igtxt:webnumericedit id="txtIFloorCountBegin" runat="server" JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js"
				JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js" ImageDirectory="../images/infragistics/images/"
				CssClass="infra-input-nember" Width="40"></igtxt:webnumericedit>��<igtxt:webnumericedit id="txtIFloorCountEnd" runat="server" JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js"
				JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js" ImageDirectory="../images/infragistics/images/" CssClass="infra-input-nember" Width="40"></igtxt:webnumericedit>
			&nbsp;&nbsp;Ͷ�����ʣ�<input class="input" id="txtSearchInvestType" type="text" size="10" name="txtSearchInvestType"
				runat="server"> <A title="ѡ��Ͷ������" onclick="SelectDict('Ͷ������', 'txtSearchInvestType')" href="#">
				<IMG src="../images/ToolsItemSearch.gif" border="0"></A> &nbsp;&nbsp;ʹ�����ʣ�<input class="input" id="txtSearchUseType" type="text" size="10" name="txtSearchUseType"
				runat="server"> <A title="ѡ��ʹ������" onclick="SelectDict('¥��ʹ������', 'txtSearchUseType')" href="#">
				<IMG src="../images/ToolsItemSearch.gif" border="0"></A>
		</td>
	</tr>
	<tr>
		<td noWrap>���״̬��<SELECT class="select" id="sltSearchInvState" name="sltSearchInvState" runat="server">
				<OPTION value="" selected>--��ѡ��--</OPTION>
				<OPTION value=" ">δ���</OPTION>
				<OPTION value="���,����">�����</OPTION>
				<OPTION value="���">�ڿ�</OPTION>
				<OPTION value="����">����</OPTION>
				<OPTION value="�˿�">�˿�</OPTION>
			</SELECT>
			&nbsp;&nbsp;������ڣ�<cc3:calendar id="dtSearchInDateBegin" runat="server" CalendarResource="../Images/CalendarResource/"
				ReadOnly="False" Display="True" Value=""></cc3:calendar>
			&nbsp;����<cc3:calendar id="dtSearchInDateEnd" runat="server" CalendarResource="../Images/CalendarResource/"
				ReadOnly="False" Display="True" Value=""></cc3:calendar>
		</td>
	</tr>
	<tr>
		<TD noWrap>����״̬��<SELECT class="select" id="sltSearchOutState" name="sltSearchOutState" runat="server">
				<OPTION value="" selected>--��ѡ��--</OPTION>
				<OPTION value=" ">δ����</OPTION>
				<OPTION value="����">����</OPTION>
			</SELECT>
			&nbsp;&nbsp;�������ڣ�<cc3:calendar id="dtSearchOutDateBegin" runat="server" CalendarResource="../Images/CalendarResource/"
				ReadOnly="False" Display="True" Value=""></cc3:calendar>
			&nbsp;����<cc3:calendar id="dtSearchOutDateEnd" runat="server" CalendarResource="../Images/CalendarResource/"
				ReadOnly="False" Display="True" Value=""></cc3:calendar>
		</TD>
	</tr>
	<tr>
		<td noWrap>����״̬��<SELECT class="select" id="sltSearchSalState" name="sltSearchSalState" runat="server">
				<OPTION value="" selected>--��ѡ��--</OPTION>
				<OPTION value=" ">δ��</OPTION>
				<OPTION value="����">����</OPTION>
			</SELECT>
			&nbsp;&nbsp;��������(����/���/���)��<SELECT class="select" id="sltSearchBofangType" name="sltSearchBofangType" runat="server">
				<OPTION value="" selected>����</OPTION>
				<OPTION value="ס">סլ</OPTION>
				<OPTION value="��">����</OPTION>
				<OPTION value="��">�˷�</OPTION>
			</SELECT>
			<igtxt:webdatetimeedit id="dtSearchBofangYear" runat="server" JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js"
				JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js" ImageDirectory="../images/infragistics/images/"
				CssClass="infra-input-year" Width="50px" DisplayModeFormat="yyyy" EditModeFormat="yyyy" PromptChar=" ">
				<SpinButtons Display="OnRight" EnsureFocus="True"></SpinButtons>
			</igtxt:webdatetimeedit><igtxt:webnumericedit id="txtSearchBofangSnoBegin" runat="server" JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js"
				JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js" ImageDirectory="../images/infragistics/images/" CssClass="infra-input-nember"
				Width="30" ToolTip="��ʼ���������"></igtxt:webnumericedit>��<igtxt:webnumericedit id="txtSearchBofangSnoEnd" runat="server" JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js"
				JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js" ImageDirectory="../images/infragistics/images/" CssClass="infra-input-nember" Width="30"
				ToolTip="��ֹ���������"></igtxt:webnumericedit>
		</td>
	</tr>
</table>
<input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server"> <input id="txtSelectBuildingCode" type="hidden" name="txtSelectBuildingCode" runat="server">
<script language="javascript">
<!--

//ѡ��¥��
function SelectBuilding()
{
	var w = 400;
	var h = 540;
	var code = "";
//	code = document.all(ClientID + "_txtSelectBuildingCode").value;

	var ProjectCode = document.all(ClientID + "_txtProjectCode").value;
	
	window.open("SelectBuilding.aspx?CanSelectArea=0&ProjectCode=" + ProjectCode + "&SelectCode=" + escape(code) + "&ReturnFunc=SelectBuildingReturn", "" , "top="+(screen.height-h)/2+",left="+(screen.width-w)/2+",width="+w+",height="+h+",scrollbars=0,resizable=1,status:no;");
}

//ѡ��¥������
function SelectBuildingReturn(code, name)
{
	document.all(ClientID + "_txtSelectBuildingCode").value = code;
	document.all(ClientID + "_txtSearchBuildingName").value = name;
//	document.all.btnSelectBuildingReturn.click();
}

//ѡ���ֵ�
function SelectDict(DictName, flag)
{
	OpenCustomWindow("../SelectBox/SelectDictItem.aspx?DictionaryName=" + escape(DictName) + "&type=multi&flag=" + flag, "ѡ���ֵ�" + DictName, 500, 560);
}

//ѡ���ֵ䷵��
function SelectDictItemReturn(code, name, flag)
{
	document.all(ClientID + ":" + flag).value = name;
}

//-->
</script>
