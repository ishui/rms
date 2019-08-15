function GetPBSNameHint(Value, ProjectCode, type)
{
/*
	var arr = new Array();
	var PBSCode = "";
	var PBSName = "";
	var Hint = "";
	var IsExists = "";
	var SortID = "";
	
	if (Value != "")
	{
		var items = GetXMLResult("../Cost/GetCostData.aspx?Value=" + escape(Value) + "&ProjectCode=" + ProjectCode + "&type=" + type);
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
*/
}

function InputPBS_GetControl(ClientID, objName)
{
	var obj = document.all(ClientID + ":" + objName);
	if (!obj)
		obj = document.all(ClientID + "_" + objName);
		
	return obj
}

function InputPBS_SelectPBS(ClientID)
{
	var val = InputPBS_GetControl(ClientID, "txtCode").value;
	var ProjectCode = InputPBS_GetControl(ClientID, "txtProjectCode").value;
	OpenMiddleWindow("../SelectBox/SelectPBS.aspx?&Type=Single&ProjectCode=" + ProjectCode + "&Flag=" + ClientID);
}

function SelectPBSReturn(PBSType, code, name, ClientID)
{
	InputPBS_GetData(ClientID, PBSType, code, name);
}

function InputPBS_CodeFocus(obj, ClientID)
{
	InputPBS_GetControl(ClientID, "txtOldValue").value = obj.value;
}

function InputPBS_CodeBlur(obj, ClientID)
{
	var NewValue = obj.value;
	if (NewValue != InputPBS_GetControl(ClientID, "txtOldValue").value)
	{
		InputPBS_GetControl(ClientID, "txtOldValue").value = obj.value;
		InputPBS_GetData(ClientID, NewValue, "");
	}
}

function InputPBS_GetData(ClientID, PBSType, code, name)
{
	var ProjectCode = InputPBS_GetControl(ClientID, "txtProjectCode").value;
	
	var txtPBSType = InputPBS_GetControl(ClientID, "txtPBSType");
	var txtCode = InputPBS_GetControl(ClientID, "txtCode");
	var txtName = InputPBS_GetControl(ClientID, "txtName");
	var divName = InputPBS_GetControl(ClientID, "divName");
	var txtHint = InputPBS_GetControl(ClientID, "txtHint");
	var divHint = InputPBS_GetControl(ClientID, "divHint");
	var txtInput = InputPBS_GetControl(ClientID, "txtInput");

	txtPBSType.value = PBSType;
	txtCode.value = code;
	txtName.value = name;
	
/*
	var arr = GetPBSNameHint(val, ProjectCode, type);
	txtCode.value = arr[0];
	txtName.value = arr[1];
	txtHint.value = arr[2];
	*/
	
	divName.innerText = txtName.value;
	divHint.innerText = txtHint.value;

//	if (arr[3] == "1")
//	{
		txtInput.value = txtName.value;
		InputPBS_GetControl(ClientID, "txtOldValue").value = txtInput.value;
//	}

	InputPBS_OnChange(ClientID);
}

