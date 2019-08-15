function GetCostNameHint(Value, ProjectCode, type, SelectAllLeaf)
{
	var arr = new Array();
	var CostCode = "";
	var CostName = "";
	var Hint = "";
	var IsExists = "";
	var SortID = "";
	var FullName = "";

	if (Value != "")
	{
		var items = GetXMLResult("../Cost/GetCostData.aspx?Value=" + escape(Value) + "&ProjectCode=" + ProjectCode + "&type=" + type + "&SelectAllLeaf=" + SelectAllLeaf);
		CostCode = GetXMLTagData(items, "CostCode");
		CostName = GetXMLTagData(items, "CostName");
		Hint = GetXMLTagData(items, "Hint");
		IsExists = GetXMLTagData(items, "IsExists");
		SortID = GetXMLTagData(items, "SortID");
		FullName = GetXMLTagData(items,"FullName");
	}

	arr.push(CostCode);
	arr.push(CostName);
	arr.push(Hint);
	arr.push(IsExists);
	arr.push(SortID);
	arr.push(FullName);	
	return arr;
}

function InputCostBudgetDtl_GetControl(ClientID, objName)
{
	var obj = document.all(ClientID + ":" + objName);
	if (!obj)
		obj = document.all(ClientID + "_" + objName);
		
	return obj
}

function InputCostBudgetDtl_SelectCost(ClientID)
{
	var val = InputCostBudgetDtl_GetControl(ClientID, "txtCode").value;
	var CostBudgetSetCode = InputCostBudgetDtl_GetControl(ClientID, "txtCostBudgetSetCode").value;
	var ProjectCode = InputCostBudgetDtl_GetControl(ClientID, "txtProjectCode").value;
	var SelectAllLeaf = InputCostBudgetDtl_GetControl(ClientID, "txtSelectAllLeaf").value;
	OpenMiddleWindow("../SelectBox/SelectCostBudgetDtl.aspx?&Type=Single&ProjectCode=" + ProjectCode + "&CostCodes=" + val + "&CostBudgetSetCode=" + CostBudgetSetCode + "&SelectAllLeaf=" + SelectAllLeaf +"&ReturnFunc=InputCostBudgetDtl_SelectCostReturn&Flag=" + ClientID, "SelectCostBudgetDtl");
}

function InputCostBudgetDtl_SelectCostReturn(code, name, ClientID, CostBudgetSetCode, PBSType, PBSCode, PBSName)
{
	InputCostBudgetDtl_GetData(ClientID, code, "code", name, CostBudgetSetCode, PBSType, PBSCode, PBSName);
}

function InputCostBudgetDtl_CodeFocus(obj, ClientID)
{
	InputCostBudgetDtl_GetControl(ClientID, "txtOldValue").value = obj.value;
}

function InputCostBudgetDtl_CodeBlur(obj, ClientID)
{
	var NewValue = obj.value;
	if (NewValue != InputCostBudgetDtl_GetControl(ClientID, "txtOldValue").value)
	{
		InputCostBudgetDtl_GetControl(ClientID, "txtOldValue").value = obj.value;
		InputCostBudgetDtl_GetData(ClientID, NewValue, "");
	}
}

function InputCostBudgetDtl_GetData(ClientID, val, type, name, CostBudgetSetCode, PBSType, PBSCode, PBSName)
{
	var ProjectCode = InputCostBudgetDtl_GetControl(ClientID, "txtProjectCode").value;
	
	var txtCode = InputCostBudgetDtl_GetControl(ClientID, "txtCode");
	var txtName = InputCostBudgetDtl_GetControl(ClientID, "txtName");
	var divName = InputCostBudgetDtl_GetControl(ClientID, "divName");
	var txtHint = InputCostBudgetDtl_GetControl(ClientID, "txtHint");
	var divHint = InputCostBudgetDtl_GetControl(ClientID, "divHint");
	var txtInput = InputCostBudgetDtl_GetControl(ClientID, "txtInput");
	var txtSortID = InputCostBudgetDtl_GetControl(ClientID, "txtSortID");
	var txtFullName = InputCostBudgetDtl_GetControl(ClientID, "txtFullName");

	var txtCostBudgetSetCode = InputCostBudgetDtl_GetControl(ClientID, "txtCostBudgetSetCode");
	var txtPBSType = InputCostBudgetDtl_GetControl(ClientID, "txtPBSType");
	var txtPBSCode = InputCostBudgetDtl_GetControl(ClientID, "txtPBSCode");
	var txtPBSName = InputCostBudgetDtl_GetControl(ClientID, "txtPBSName");

	var txtDesc = InputCostBudgetDtl_GetControl(ClientID, "txtDesc");
	var divDesc = InputCostBudgetDtl_GetControl(ClientID, "divDesc");

	//是否可任意选择费用项	
	var SelectAllLeaf = InputCostBudgetDtl_GetControl(ClientID, "txtSelectAllLeaf").value;

	var arr = GetCostNameHint(val, ProjectCode, type, SelectAllLeaf);
	txtCode.value = arr[0];
	txtName.value = arr[1];
	txtHint.value = arr[2];
	txtSortID.value = arr[4];
	txtFullName.value = arr[5];
	
	txtCostBudgetSetCode.value = CostBudgetSetCode;
	txtPBSType.value = PBSType;
	txtPBSCode.value = PBSCode;
	txtPBSName.value = PBSName;
	
	txtDesc.value = "";
	if (txtPBSName.value != "")
	{
		txtDesc.value = "单位工程：" + txtPBSName.value;
	}
	
//	divName.innerText = txtName.value;
	divName.innerText = txtFullName.value;
	divHint.innerText = txtHint.value;
	divDesc.innerText = txtDesc.value;
	
	if (arr[3] == "1")
	{
		txtInput.value = txtSortID.value;
		InputCostBudgetDtl_GetControl(ClientID, "txtOldValue").value = txtInput.value;
	}
}

