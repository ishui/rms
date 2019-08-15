function GetTaskNameHint(Value, ProjectCode, type)
{
	var arr = new Array();
	var TaskCode = "";
	var TaskName = "";
	var Hint = "";
	var IsExists = "";
	var SortID = "";
	
	if (Value != "")
	{
		var items = GetXMLResult("../Project/GetTaskData.aspx?Value=" + escape(Value) + "&ProjectCode=" + ProjectCode + "&type=" + type);

		TaskCode = GetXMLTagData(items, "TaskCode");
		TaskName = GetXMLTagData(items, "TaskName");
		Hint = GetXMLTagData(items, "Hint");
		IsExists = GetXMLTagData(items, "IsExists");
		SortID = GetXMLTagData(items, "SortID");
	}

	arr.push(TaskCode);
	arr.push(TaskName);
	arr.push(Hint);
	arr.push(IsExists);
	arr.push(SortID);
	return arr;
}

function InputTask_GetControl(ClientID, objName)
{
	var obj = document.all(ClientID + ":" + objName);
	if (!obj)
		obj = document.all(ClientID + "_" + objName);
		
	return obj
}

function InputTask_SelectTask(ClientID)
{
	var val = InputTask_GetControl(ClientID, "txtCode").value;
	var ProjectCode = InputTask_GetControl(ClientID, "txtProjectCode").value;
	OpenMiddleWindow("../SelectBox/SelectTask.aspx?ProjectCode=" + ProjectCode + "&Flag=1&WBSCode=" + val + "&ReturnFunc=InputTask_SelectTaskReturn&Define1=" + ClientID);
}

function InputTask_SelectTaskReturn(code, name, ClientID)
{
	InputTask_GetData(ClientID, code, "code");
}

function InputTask_CodeFocus(obj, ClientID)
{
	InputTask_GetControl(ClientID, "txtOldValue").value = obj.value;
}

function InputTask_CodeBlur(obj, ClientID)
{
	var NewValue = obj.value;
	if (NewValue != InputTask_GetControl(ClientID, "txtOldValue").value)
	{
		InputTask_GetControl(ClientID, "txtOldValue").value = obj.value;
		InputTask_GetData(ClientID, NewValue, "");
	}
}

function InputTask_GetData(ClientID, val, type)
{
	var ProjectCode = InputTask_GetControl(ClientID, "txtProjectCode").value;
	
	var txtCode = InputTask_GetControl(ClientID, "txtCode");
	var txtName = InputTask_GetControl(ClientID, "txtName");
	var divName = InputTask_GetControl(ClientID, "divName");
	var txtHint = InputTask_GetControl(ClientID, "txtHint");
	var divHint = InputTask_GetControl(ClientID, "divHint");
	var txtInput = InputTask_GetControl(ClientID, "txtInput");
	var txtSortID = InputTask_GetControl(ClientID, "txtSortID");

	var arr = GetTaskNameHint(val, ProjectCode, type);
	txtCode.value = arr[0];
	txtName.value = arr[1];
	txtHint.value = arr[2];
	txtSortID.value = arr[4];
	
	divName.innerText = txtName.value;
	divHint.innerText = txtHint.value;
	
	if (arr[3] == "1")
	{
		txtInput.value = txtSortID.value;
	}
	
	InputTask_OnChange(ClientID);
}

