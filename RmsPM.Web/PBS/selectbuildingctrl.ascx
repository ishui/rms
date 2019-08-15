<%@ Control Language="c#" Inherits="RmsPM.Web.PBS.SelectBuildingCtrl" CodeFile="SelectBuildingCtrl.ascx.cs" %>
<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
<style>
TABLE.mylist TD { BORDER-RIGHT: #eaeff4 0px solid; PADDING-RIGHT: 5px; PADDING-LEFT: 5px; BORDER-BOTTOM: #ededed 0px solid; HEIGHT: 23px }
.mylist { BORDER-TOP: #a6c6dd 0px solid; BORDER-BOTTOM: #a6c6dd 0px solid; BORDER-COLLAPSE: collapse; BACKGROUND-COLOR: #ffffff }
.mylist-2 { BACKGROUND-COLOR: #e5f0ce }
</style>
<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr vAlign="top">
		<td>
			<table class="mylist" cellpadding="0" cellspacing="0" border="0" id="tbList" width="100%">
				<asp:Repeater Runat="server" ID="dgList">
					<ItemTemplate>
						<tr val='<%# DataBinder.Eval(Container, "DataItem.BuildingCode") %>'>
							<td>
								<a val='<%# DataBinder.Eval(Container, "DataItem.BuildingCode") %>' index='<%# Container.ItemIndex%>' href="#" onclick="javascript:BuildingCtrlView(this);">
									<%# DataBinder.Eval(Container, "DataItem.BuildingName") %>
								</a>
							</td>
						</tr>
					</ItemTemplate>
				</asp:Repeater>
			</table>
		</td>
	</tr>
</table>
<input type="hidden" runat="server" id="txtProjectCode" name="txtProjectCode"><input type="hidden" runat="server" id="txtMultiBuildingCode" name="txtMultiBuildingCode">
<input type="hidden" runat="server" id="txtMulti" name="txtMulti">
<script language="javascript">
<!--

var BuildingCtrlSelectIndex = -1;
var SelectObj;

//查看
function BuildingCtrlView(obj)
{	
	var code = obj.val;
	var index = obj.index;
	BuildingCtrlSelectIndex = index;
	SelectObj = obj;
	
	if (code == "-1")
	{
		BuildingCtrlSelectBuilding();
	}
	else
	{
		BuildingCtrlSelectRow(index);
		parent.GotoBuilding(code);
	}
	
//	SetBuilding(code, index);
}

//清空列表选择行
function BuildingCtrlClearSelect()
{
	var objTable = document.all("tbList");
	if (!objTable) return;

	var count = objTable.rows.length;

	for (i=0;i<count;i++)
	{
		objTable.rows(i).className = "";
	}
}

//列表选择一行（按行号）
function BuildingCtrlSelectRow(index)
{
//	if (index == "") return;
	
	var i = parseInt(index);
	if (i < 0) return;
//	var objTable = document.all(BuildingCtrlClientID + "_tbList");
	var objTable = document.all("tbList");
	var selectedClass = "mylist-2";
	
	if (!objTable) return;
	if (!objTable.rows(i)) return;

	BuildingCtrlClearSelect();
	objTable.rows(i).className = selectedClass;
}

//列表选择一行（按关键字）
function BuildingCtrlSelectRowByCode(code, isgoto)
{
	BuildingCtrlClearSelect();

	var objTable = document.all("tbList");
	if (!objTable) return;
	var index = -1;
	
	var i = 0;
	var row = objTable.rows(i);
	while (row)
	{
		if (row.val == code)
		{
			index = i;
			break;
		}
		
		i++;
		row = objTable.rows(i);
	}
	
	BuildingCtrlSelectRow(index);
	
	if (isgoto)
	{
		parent.GotoBuilding(code);
	}
}

//取列表第一行的关键字值
function BuildingCtrlGetFirstRowCode()
{
	var code = "";
	var objTable = document.all("tbList");
	if (!objTable) return;
	
	var Multi = document.all(BuildingCtrlClientID + "_txtMulti").value;

	if (Multi == "1")
	{
		if (objTable.rows.length > 1)
		{
			var row = objTable.rows(1);
			code = row.val;
		}
	}
	else
	{
		if (objTable.rows.length > 0)
		{
			var row = objTable.rows(0);
			code = row.val;
		}
	}
	
	return code;
}

//选择楼栋
function BuildingCtrlSelectBuilding()
{
	var code = "";
	
	code = document.all(BuildingCtrlClientID + "_txtMultiBuildingCode").value;
	var ProjectCode = document.all(BuildingCtrlClientID + "_txtProjectCode").value;
	
	OpenCustomWindow("SelectBuilding.aspx?CanSelectArea=0&ProjectCode=" + ProjectCode + "&SelectCode=" + escape(code) + "&SelectName=" + escape(code) + "&ReturnFunc=BuildingCtrlSelectBuildingReturn", "选择楼栋", 400, 540);
}

//选择楼栋返回
function BuildingCtrlSelectBuildingReturn(code, name)
{
	document.all(BuildingCtrlClientID + "_txtMultiBuildingCode").value = code;
	BuildingCtrlSelectRow(BuildingCtrlSelectIndex);

	if (SelectObj)
		SelectObj.title = name;
	
	parent.GotoBuilding(code);
}

//-->
</script>
