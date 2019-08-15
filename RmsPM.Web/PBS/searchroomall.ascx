<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics.WebUI.WebDataInput.v5.1, Version=5.1.20051.37, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Control Language="c#" Inherits="RmsPM.Web.PBS.SearchRoomAll" CodeFile="SearchRoomAll.ascx.cs" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<table>
	<tr>
		<td>竣工年份：<igtxt:webdatetimeedit id="dtJgYear" CssClass="infra-input-year" PromptChar=" " JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js"
				JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js" ImageDirectory="../images/infragistics/images/"
				Width="50px" EditModeFormat="yyyy" DisplayModeFormat="yyyy" runat="server">
				<SpinButtons Display="OnRight" EnsureFocus="True"></SpinButtons>
			</igtxt:webdatetimeedit>
			&nbsp;&nbsp;项目： <span id="divProjectName" runat="server"></span><INPUT class="button-small" id="btnSelectProject" onclick="doOpenSelectProject();" type="button"
				value="选择项目" name="btnSelectProject">
		</td>
	</tr>
	<tr>
		<td noWrap>楼栋：<input class="input" id="txtSearchBuildingName" type="text" size="20" name="txtSearchBuildingName"
				runat="server"> &nbsp;&nbsp;门牌号：<input class="input" id="txtSearchChamberName" type="text" size="10" name="txtSearchChamberName"
				runat="server"> &nbsp;&nbsp;室号：<input class="input" id="txtSearchRoomName" type="text" size="10" name="txtSearchRoomName"
				runat="server"></td>
	</tr>
</table>
<table id="divAdvSearch">
	<tr>
		<TD noWrap>产品类型：<SELECT class="select" id="sltSearchPBSTypeCode" name="sltSearchPBSTypeCode" runat="server">
				<OPTION value="" selected>--请选择--</OPTION>
			</SELECT>
			&nbsp;&nbsp;去向：<input class="input" id="txtSearchOutAspect" type="text" size="10" name="txtSearchOutAspect"
				runat="server"> <A title="选择去向" onclick="SelectDict('去向', 'txtSearchOutAspect')" href="#">
				<IMG src="../images/ToolsItemSearch.gif" border="0"></A>
		</TD>
	</tr>
	<tr>
		<td noWrap>总 层 数：<igtxt:webnumericedit id="txtIFloorCountBegin" CssClass="infra-input-nember" JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js"
				JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js" ImageDirectory="../images/infragistics/images/"
				Width="40" runat="server"></igtxt:webnumericedit>—<igtxt:webnumericedit id="txtIFloorCountEnd" CssClass="infra-input-nember" JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js"
				JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js" ImageDirectory="../images/infragistics/images/" Width="40" runat="server"></igtxt:webnumericedit>
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
			&nbsp;&nbsp;入库日期：<cc3:calendar id="dtSearchInDateBegin" runat="server" Value="" Display="True" ReadOnly="False"
				CalendarResource="../Images/CalendarResource/"></cc3:calendar>
			&nbsp;到：<cc3:calendar id="dtSearchInDateEnd" runat="server" Value="" Display="True" ReadOnly="False" CalendarResource="../Images/CalendarResource/"></cc3:calendar>
		</td>
	</tr>
	<tr>
		<TD noWrap>调拨状态：<SELECT class="select" id="sltSearchOutState" name="sltSearchOutState" runat="server">
				<OPTION value="" selected>--请选择--</OPTION>
				<OPTION value=" ">未调拨</OPTION>
				<OPTION value="调拨">调拨</OPTION>
			</SELECT>
			&nbsp;&nbsp;出库日期：<cc3:calendar id="dtSearchOutDateBegin" runat="server" Value="" Display="True" ReadOnly="False"
				CalendarResource="../Images/CalendarResource/"></cc3:calendar>
			&nbsp;到：<cc3:calendar id="dtSearchOutDateEnd" runat="server" Value="" Display="True" ReadOnly="False"
				CalendarResource="../Images/CalendarResource/"></cc3:calendar>
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
<input id="txtProjectName" type="hidden" name="txtProjectName" runat="server">
<script language="javascript">
<!--
//选择项目
function doOpenSelectProject()
{
	OpenMiddleWindow('../SelectBox/SelectProject.aspx?Type=multi' ,'选择项目');
}

//选择项目返回
function DoSelectProject(projectCode,projectName)
{
	document.all(SearchRoomAllClientID + "_divProjectName").innerHTML = projectName;
	document.all(SearchRoomAllClientID + "_txtProjectName").value = projectName;
	document.all(SearchRoomAllClientID + "_txtProjectCode").value = projectCode;
}

//选择字典
function SelectDict(DictName, flag)
{
	OpenCustomWindow("../SelectBox/SelectDictItem.aspx?DictionaryName=" + escape(DictName) + "&type=multi&flag=" + flag, "选择字典" + DictName, 500, 560);
}

//选择字典返回
function SelectDictItemReturn(code, name, flag)
{
	document.all(SearchRoomAllClientID + ":" + flag).value = name;
}

//-->
</script>

