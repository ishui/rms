function GetCostNameHint(Value, ProjectCode, type, SelectAllLeaf)
{
	var arr = new Array();
	var CostCode = "";
	var CostName = "";
	var Hint = "";
	var IsExists = "";
	var SortID = "";

	if (Value != "")
	{
		var items = GetXMLResult("../Cost/GetCostData.aspx?Value=" + escape(Value) + "&ProjectCode=" + ProjectCode + "&type=" + type + "&SelectAllLeaf=" + SelectAllLeaf);
		CostCode = GetXMLTagData(items, "CostCode");
		CostName = GetXMLTagData(items, "CostName");
		Hint = GetXMLTagData(items, "Hint");
		IsExists = GetXMLTagData(items, "IsExists");
		SortID = GetXMLTagData(items, "SortID");
	}

	arr.push(CostCode);
	arr.push(CostName);
	arr.push(Hint);
	arr.push(IsExists);
	arr.push(SortID);
	return arr;
}

function InputCost_GetControl(ClientID, objName)
{
	var obj = document.all(ClientID + ":" + objName);
	if (!obj)
		obj = document.all(ClientID + "_" + objName);
		
	return obj
}

function InputCost_SelectCost(ClientID)
{
	var val = InputCost_GetControl(ClientID, "txtCode").value;
	var ProjectCode = InputCost_GetControl(ClientID, "txtProjectCode").value;
	var SelectAllLeaf = InputCost_GetControl(ClientID, "txtSelectAllLeaf").value;
	OpenMiddleWindow("../SelectBox/SelectCost.aspx?&Type=Single&ProjectCode=" + ProjectCode + "&CostCodes=" + val + "&SelectAllLeaf=" + SelectAllLeaf +"&ReturnFunc=InputCost_SelectCostReturn&Flag=" + ClientID, "SelectCBS");
}

function InputCost_SelectCostReturn(code, name, ClientID)
{
	InputCost_GetData(ClientID, code, "code");
}

function InputCost_CodeFocus(obj, ClientID)
{
	InputCost_GetControl(ClientID, "txtOldValue").value = obj.value;
}

function InputCost_CodeBlur(obj, ClientID)
{
	var NewValue = obj.value;
	if (NewValue != InputCost_GetControl(ClientID, "txtOldValue").value)
	{
		InputCost_GetControl(ClientID, "txtOldValue").value = obj.value;
		InputCost_GetData(ClientID, NewValue, "");
	}
}

function InputCost_GetData(ClientID, val, type)
{
	var ProjectCode = InputCost_GetControl(ClientID, "txtProjectCode").value;
	
	var txtCode = InputCost_GetControl(ClientID, "txtCode");
	var txtName = InputCost_GetControl(ClientID, "txtName");
	var divName = InputCost_GetControl(ClientID, "divName");
	var txtHint = InputCost_GetControl(ClientID, "txtHint");
	var divHint = InputCost_GetControl(ClientID, "divHint");
	var txtInput = InputCost_GetControl(ClientID, "txtInput");
	var txtSortID = InputCost_GetControl(ClientID, "txtSortID");

	//是否可任意选择费用项	
	var SelectAllLeaf = InputCost_GetControl(ClientID, "txtSelectAllLeaf").value;

	var arr = GetCostNameHint(val, ProjectCode, type, SelectAllLeaf);
	txtCode.value = arr[0];
	txtName.value = arr[1];
	txtHint.value = arr[2];
	txtSortID.value = arr[4];
	
	divName.innerText = txtName.value;
	divHint.innerText = txtHint.value;
	
	if (arr[3] == "1")
	{
		txtInput.value = txtSortID.value;
		InputCost_GetControl(ClientID, "txtOldValue").value = txtInput.value;
	}
}

