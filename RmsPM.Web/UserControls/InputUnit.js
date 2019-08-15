function GetUnitName(Value)
{
	if (Value == "") return "";

	var UnitName = "";

	var items = GetXMLResult("../Systems/GetUnitData.aspx?Value=" + escape(Value));	

	UnitName = GetXMLTagData(items, "UnitName");
	
	return UnitName;
}

function GetUnitNameHint(Value)
{
	var arr = new Array();
	var UnitCode = "";
	var UnitName = "";
	var Hint = "";
	var IsExists = "";
	
	if (Value != "")
	{
		var items = GetXMLResult("../Systems/GetUnitData.aspx?Value=" + escape(Value));

		UnitCode = GetXMLTagData(items, "UnitCode");
		UnitName = GetXMLTagData(items, "UnitName");
		Hint = GetXMLTagData(items, "Hint");
		IsExists = GetXMLTagData(items, "IsExists");
	}

	arr.push(UnitCode);
	arr.push(UnitName);
	arr.push(Hint);
	arr.push(IsExists);
	return arr;
}

function InputUnit_GetControl(ClientID, objName)
{
	var obj = document.all(ClientID + ":" + objName);
	if (!obj)
		obj = document.all(ClientID + "_" + objName);
		
	return obj
}

function InputUnit_SelectUnit(ClientID,ImagePath)
{
	var val = InputUnit_GetControl(ClientID, "txtCode").value;
	//alert(ImagePath);
	OpenMiddleWindow(ImagePath+"../SelectBox/SelectUnit.aspx?&Type=single&SelectCode="+val+"&ReturnFunc=InputUnit_SelectUnitReturn&Define1=" + ClientID,'');
}

function InputUnit_SelectUnitReturn(code, name, ClientID)
{
	var txtCode = InputUnit_GetControl(ClientID, "txtCode");
//	var divName = InputUnit_GetControl(ClientID, "divName");
	var txtName = InputUnit_GetControl(ClientID, "txtName");
	var txtHint = InputUnit_GetControl(ClientID, "txtHint");
	var divHint = InputUnit_GetControl(ClientID, "divHint");
	var txtInput = InputUnit_GetControl(ClientID, "txtInput");
	
	txtCode.value = code;
	txtName.value = name;
	txtInput.value = name;
	txtHint.value = "";
	divHint.innerText = txtHint.value;
	
//	divName.innerText = txtName.value;
}

function InputUnit_CodeFocus(obj, ClientID)
{
	InputUnit_GetControl(ClientID, "txtOldValue").value = obj.value;
}

function InputUnit_CodeBlur(obj, ClientID)
{
//	var divName = InputUnit_GetControl(ClientID, "divName");
	var txtCode = InputUnit_GetControl(ClientID, "txtCode");
	var txtName = InputUnit_GetControl(ClientID, "txtName");
	var txtHint = InputUnit_GetControl(ClientID, "txtHint");
	var divHint = InputUnit_GetControl(ClientID, "divHint");
	var txtInput = InputUnit_GetControl(ClientID, "txtInput");
	
	var NewValue = obj.value;
	if (NewValue != InputUnit_GetControl(ClientID, "txtOldValue").value)
	{
		InputUnit_GetControl(ClientID, "txtOldValue").value = obj.value;

		var arr = GetUnitNameHint(NewValue);
		
		txtCode.value = arr[0];
		txtName.value = arr[1];
		txtHint.value = arr[2];
		
		divHint.innerText = txtHint.value;
		
		if (arr[3] == "1")
		{
			txtInput.value = txtName.value;
		}
		
//		divName.innerText = name;
	}
}
