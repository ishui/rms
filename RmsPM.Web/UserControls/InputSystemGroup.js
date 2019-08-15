function GetSystemGroupNameHint(Value, ClientID, type, SelectAllLeaf)
{
	var arr = new Array();
	var GroupCode = "";
	var GroupName = "";
	var Hint = "";
	var IsExists = "";
	var SortID = "";
	var FullID = "";
	
	if (Value != "")
	{
		var ClassCode = InputSystemGroup_GetControl(ClientID, "txtClassCode").value;
		if( ClassCode == "1603")
		    var items = GetXMLResult("../../Systems/GetSystemGroupData.aspx?ClassCode=" + ClassCode + "&Value=" + escape(Value) + "&type=code" + "&SelectAllLeaf=" + SelectAllLeaf);
		else
		    var items = GetXMLResult("../Systems/GetSystemGroupData.aspx?ClassCode=" + ClassCode + "&Value=" + escape(Value) + "&type=" + type + "&SelectAllLeaf=" + SelectAllLeaf);
		//alert("../../Systems/GetSystemGroupData.aspx?ClassCode=" + ClassCode + "&Value=" + escape(Value) + "&type=" + type);    
		    
		GroupCode = GetXMLTagData(items, "GroupCode");
		GroupName = GetXMLTagData(items, "GroupFullName");
		Hint = GetXMLTagData(items, "Hint");
		IsExists = GetXMLTagData(items, "IsExists");
		SortID = GetXMLTagData(items, "SortID");
		FullID = GetXMLTagData(items, "FullID");
		
		//必须选择叶结点时，若录入的不是叶结点则不能保存
		if ((SelectAllLeaf != "1") && (Hint.length > 0))
		{
			GroupCode = "";
		}
	}

	arr.push(GroupCode);
	arr.push(GroupName);
	arr.push(Hint);
	arr.push(IsExists);
	arr.push(SortID);
	arr.push(FullID);
	return arr;
}

function InputSystemGroup_GetControl(ClientID, objName)
{
	var obj = document.all(ClientID + ":" + objName);
	if (!obj)
		obj = document.all(ClientID + "_" + objName);
		
	return obj
}

function InputSystemGroup_Select(ClientID,ImagePath,ProjectCode)
{
	var ClassCode = InputSystemGroup_GetControl(ClientID, "txtClassCode").value;
	var val = InputSystemGroup_GetControl(ClientID, "txtCode").value;
	var SelectAllLeaf = InputSystemGroup_GetControl(ClientID, "txtSelectAllLeaf").value;
	OpenMiddleWindow(ImagePath+"../SelectBox/SelectSystemGroup.aspx?ProjectCode="+ProjectCode+"&Type=Single&ClassCode=" + ClassCode + "&GroupCodes=" + val + "&SelectAllLeaf=" + SelectAllLeaf + "&ReturnFunc=InputSystemGroup_SelectReturn&Flag=" + ClientID,null);

}

function InputSystemGroup_SelectReturn(code, name, ClientID)
{
	InputSystemGroup_GetData(ClientID, code, "code");
}

function InputSystemGroup_CodeFocus(obj, ClientID)
{
	InputSystemGroup_GetControl(ClientID, "txtOldValue").value = obj.value;
}

function InputSystemGroup_CodeBlur(obj, ClientID)
{
	var NewValue = obj.value;
	if (NewValue != InputSystemGroup_GetControl(ClientID, "txtOldValue").value)
	{
		InputSystemGroup_GetControl(ClientID, "txtOldValue").value = obj.value;
		InputSystemGroup_GetData(ClientID, NewValue, "");
	}
}

function InputSystemGroup_GetData(ClientID, val, type)
{
	var txtCode = InputSystemGroup_GetControl(ClientID, "txtCode");
	var txtName = InputSystemGroup_GetControl(ClientID, "txtName");
	var divName = InputSystemGroup_GetControl(ClientID, "divName");
	var txtHint = InputSystemGroup_GetControl(ClientID, "txtHint");
	var divHint = InputSystemGroup_GetControl(ClientID, "divHint");
	var txtInput = InputSystemGroup_GetControl(ClientID, "txtInput");
	var txtSortID = InputSystemGroup_GetControl(ClientID, "txtSortID");
	var txtFullID = InputSystemGroup_GetControl(ClientID, "txtFullID");
	var SelectAllLeaf = InputSystemGroup_GetControl(ClientID, "txtSelectAllLeaf").value;

	var arr = GetSystemGroupNameHint(val, ClientID, type, SelectAllLeaf);
	txtCode.value = arr[0];
	txtName.value = arr[1];
	txtHint.value = arr[2];
	txtSortID.value = arr[4];
	txtFullID.value = arr[5];
	
	divName.innerText = txtName.value;
	divHint.innerText = txtHint.value;
	
	if (arr[3] == "1")
	{
		txtInput.value = txtSortID.value;
		InputSystemGroup_GetControl(ClientID, "txtOldValue").value = txtInput.value;
	}
	var btnChange = InputSystemGroup_GetControl(ClientID, "btnChange")
	if(btnChange)
	{
	    btnChange.click();
	}
}
