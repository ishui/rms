<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics.WebUI.WebDataInput.v5.1, Version=5.1.20051.37, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Control Language="c#" Inherits="RmsPM.Web.PBS.SearchRoom" CodeFile="SearchRoom.ascx.cs" %>
<table width="100%">
	<tr>
		<td noWrap>楼栋：<input class="input" id="txtSearchBuildingName" type="text" size="20" name="txtSearchBuildingName"
				runat="server"><A href="#" onclick="SelectBuilding();return false;"><IMG src="../images/ToolsItemSearch.gif" border="0"></A></td>
		<td noWrap>门牌号：<input class="input" id="txtSearchChamberName" type="text" size="10" name="txtSearchChamberName"
				runat="server"></td>
		<td noWrap>室号：<input class="input" id="txtSearchRoomName" type="text" size="10" name="txtSearchRoomName"
				runat="server"></td>
	</tr>
</table>
<table id="divAdvSearch">
	<tr>
		<TD noWrap>产品类型：<SELECT class="select" id="sltSearchPBSTypeCode" name="sltSearchPBSTypeCode" runat="server">
				<OPTION value="" selected>--请选择--</OPTION>
			</SELECT>
			&nbsp;&nbsp;户型：<SELECT class="select" id="sltSearchModelCode" name="sltSearchModelCode" runat="server">
				<OPTION value="" selected>--请选择--</OPTION>
			</SELECT>
			&nbsp;&nbsp;去向：<input class="input" id="txtSearchOutAspect" type="text" size="10" name="txtSearchOutAspect"
				runat="server"> <A title="选择去向" onclick="SelectDict('去向', 'txtSearchOutAspect')" href="#">
				<IMG src="../images/ToolsItemSearch.gif" border="0"></A>
		</TD>
	</tr>
	<tr>
		<td noWrap>总 层 数：<igtxt:webnumericedit id="txtIFloorCountBegin" runat="server" JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js"
				JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js" ImageDirectory="../images/infragistics/images/"
				CssClass="infra-input-nember" Width="40"></igtxt:webnumericedit>—<igtxt:webnumericedit id="txtIFloorCountEnd" runat="server" JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js"
				JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js" ImageDirectory="../images/infragistics/images/" CssClass="infra-input-nember" Width="40"></igtxt:webnumericedit>
			&nbsp;&nbsp;投资性质：<input class="input" id="txtSearchInvestType" type="text" size="10" name="txtSearchInvestType"
				runat="server"> <A title="选择投资性质" onclick="SelectDict('投资性质', 'txtSearchInvestType')" href="#">
				<IMG src="../images/ToolsItemSearch.gif" border="0"></A> &nbsp;&nbsp;使用性质：<input class="input" id="txtSearchUseType" type="text" size="10" name="txtSearchUseType"
				runat="server"> <A title="选择使用性质" onclick="SelectDict('楼栋使用性质', 'txtSearchUseType')" href="#">
				<IMG src="../images/ToolsItemSearch.gif" border="0"></A>
		</td>
	</tr>
	<tr>
		<td noWrap>库存状态：<SELECT class="select" id="sltSearchInvState" name="sltSearchInvState" runat="server">
				<OPTION value="" selected>--请选择--</OPTION>
				<OPTION value=" ">未入库</OPTION>
				<OPTION value="入库,出库">已入库</OPTION>
				<OPTION value="入库">在库</OPTION>
				<OPTION value="出库">出库</OPTION>
				<OPTION value="退库">退库</OPTION>
			</SELECT>
			&nbsp;&nbsp;入库日期：<cc3:calendar id="dtSearchInDateBegin" runat="server" CalendarResource="../Images/CalendarResource/"
				ReadOnly="False" Display="True" Value=""></cc3:calendar>
			&nbsp;到：<cc3:calendar id="dtSearchInDateEnd" runat="server" CalendarResource="../Images/CalendarResource/"
				ReadOnly="False" Display="True" Value=""></cc3:calendar>
		</td>
	</tr>
	<tr>
		<TD noWrap>调拨状态：<SELECT class="select" id="sltSearchOutState" name="sltSearchOutState" runat="server">
				<OPTION value="" selected>--请选择--</OPTION>
				<OPTION value=" ">未调拨</OPTION>
				<OPTION value="调拨">调拨</OPTION>
			</SELECT>
			&nbsp;&nbsp;出库日期：<cc3:calendar id="dtSearchOutDateBegin" runat="server" CalendarResource="../Images/CalendarResource/"
				ReadOnly="False" Display="True" Value=""></cc3:calendar>
			&nbsp;到：<cc3:calendar id="dtSearchOutDateEnd" runat="server" CalendarResource="../Images/CalendarResource/"
				ReadOnly="False" Display="True" Value=""></cc3:calendar>
		</TD>
	</tr>
	<tr>
		<td noWrap>销售状态：<SELECT class="select" id="sltSearchSalState" name="sltSearchSalState" runat="server">
				<OPTION value="" selected>--请选择--</OPTION>
				<OPTION value=" ">未售</OPTION>
				<OPTION value="已售">已售</OPTION>
			</SELECT>
			&nbsp;&nbsp;调拨单号(类型/年度/序号)：<SELECT class="select" id="sltSearchBofangType" name="sltSearchBofangType" runat="server">
				<OPTION value="" selected>所有</OPTION>
				<OPTION value="住">住宅</OPTION>
				<OPTION value="公">公建</OPTION>
				<OPTION value="防">人防</OPTION>
			</SELECT>
			<igtxt:webdatetimeedit id="dtSearchBofangYear" runat="server" JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js"
				JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js" ImageDirectory="../images/infragistics/images/"
				CssClass="infra-input-year" Width="50px" DisplayModeFormat="yyyy" EditModeFormat="yyyy" PromptChar=" ">
				<SpinButtons Display="OnRight" EnsureFocus="True"></SpinButtons>
			</igtxt:webdatetimeedit><igtxt:webnumericedit id="txtSearchBofangSnoBegin" runat="server" JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js"
				JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js" ImageDirectory="../images/infragistics/images/" CssClass="infra-input-nember"
				Width="30" ToolTip="起始调拨单序号"></igtxt:webnumericedit>—<igtxt:webnumericedit id="txtSearchBofangSnoEnd" runat="server" JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js"
				JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js" ImageDirectory="../images/infragistics/images/" CssClass="infra-input-nember" Width="30"
				ToolTip="终止调拨单序号"></igtxt:webnumericedit>
		</td>
	</tr>
</table>
<input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server"> <input id="txtSelectBuildingCode" type="hidden" name="txtSelectBuildingCode" runat="server">
<script language="javascript">
<!--

//选择楼栋
function SelectBuilding()
{
	var w = 400;
	var h = 540;
	var code = "";
//	code = document.all(ClientID + "_txtSelectBuildingCode").value;

	var ProjectCode = document.all(ClientID + "_txtProjectCode").value;
	
	window.open("SelectBuilding.aspx?CanSelectArea=0&ProjectCode=" + ProjectCode + "&SelectCode=" + escape(code) + "&ReturnFunc=SelectBuildingReturn", "" , "top="+(screen.height-h)/2+",left="+(screen.width-w)/2+",width="+w+",height="+h+",scrollbars=0,resizable=1,status:no;");
}

//选择楼栋返回
function SelectBuildingReturn(code, name)
{
	document.all(ClientID + "_txtSelectBuildingCode").value = code;
	document.all(ClientID + "_txtSearchBuildingName").value = name;
//	document.all.btnSelectBuildingReturn.click();
}

//选择字典
function SelectDict(DictName, flag)
{
	OpenCustomWindow("../SelectBox/SelectDictItem.aspx?DictionaryName=" + escape(DictName) + "&type=multi&flag=" + flag, "选择字典" + DictName, 500, 560);
}

//选择字典返回
function SelectDictItemReturn(code, name, flag)
{
	document.all(ClientID + ":" + flag).value = name;
}

//-->
</script>
