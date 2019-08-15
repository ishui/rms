<%@ Control Language="c#" Inherits="RmsPM.Web.PBS.SelectPBSUnitCtrl" CodeFile="SelectPBSUnitCtrl.ascx.cs" %>
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
						<tr val='<%# DataBinder.Eval(Container, "DataItem.PBSUnitCode") %>'>
							<td>
								<a val='<%# DataBinder.Eval(Container, "DataItem.PBSUnitCode") %>' index='<%# Container.ItemIndex%>' href="#" onclick="javascript:PBSUnitCtrlView(this);">
									<%# DataBinder.Eval(Container, "DataItem.PBSUnitName") %>
								</a>
							</td>
						</tr>
					</ItemTemplate>
				</asp:Repeater>
			</table>
		</td>
	</tr>
</table>
<input type="hidden" runat="server" id="txtProjectCode" name="txtProjectCode">
<script language="javascript">
<!--

//查看
function PBSUnitCtrlView(obj)
{	
	var code = obj.val;
	var index = obj.index;
	PBSUnitCtrlSelectRow(index);
	parent.GotoPBSUnit(code);
//	SetPBSUnit(code, index);
}

//清空列表选择行
function PBSUnitCtrlClearSelect()
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
function PBSUnitCtrlSelectRow(index)
{
//	if (index == "") return;
	
	var i = parseInt(index);
	if (i < 0) return;
//	var objTable = document.all(PBSUnitCtrlClientID + "_tbList");
	var objTable = document.all("tbList");
	var selectedClass = "mylist-2";
	
	if (!objTable) return;
	if (!objTable.rows(i)) return;

	PBSUnitCtrlClearSelect();
	objTable.rows(i).className = selectedClass;
}

//列表选择一行（按关键字）
function PBSUnitCtrlSelectRowByCode(code, isgoto)
{
	PBSUnitCtrlClearSelect();

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
	
	PBSUnitCtrlSelectRow(index);
	
	if (isgoto)
	{
		parent.GotoPBSUnit(code);
	}
}

//取列表第一行的关键字值
function PBSUnitCtrlGetFirstRowCode()
{
	var code = "";
	var objTable = document.all("tbList");
	if (!objTable) return;
	
	if (objTable.rows.length > 0)
	{
		var row = objTable.rows(0);
		code = row.val;
	}
	
	return code;
}

//-->
</script>
