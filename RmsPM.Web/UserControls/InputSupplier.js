function GetSupplierNameHint(Value, ClientID, type)
{
	var arr = new Array();
	var SupplierCode = "";
	var SupplierName = "";
	var Hint = "";
	var IsExists = "";
	var SortID = "";
	if (Value != "")
	{
	    var items = GetXMLResult("../../Systems/GetSupplierData.aspx?Value=" + escape(Value));
		SupplierCode = GetXMLTagData(items, "SupplierCode");
			alert(Value);	
		SupplierName = GetXMLTagData(items, "SupplierFullName");
		Hint = GetXMLTagData(items, "Hint");
		IsExists = GetXMLTagData(items, "IsExists");
		SortID = GetXMLTagData(items, "SortID");
	}

	arr.push(SupplierCode);
	arr.push(SupplierName);
	arr.push(Hint);
	arr.push(IsExists);
	arr.push(SortID);
	return arr;
}

function InputSupplier_GetControl(ClientID, objName)
{
	var obj = document.all(ClientID + ":" + objName);
	if (!obj)
		obj = document.all(ClientID + "_" + objName);
		
	return obj
}

function InputSupplier_Select(ClientID,ImagePath)
{
	var supplierCode = InputSupplier_GetControl(ClientID, "txtCode").value;
	OpenLargeWindow(ImagePath+"../SelectBox/SelectSupplier.aspx?SupplierCode=" + supplierCode + "&Flag=" + ClientID ,null);
}

function DoSelectSupplierReturn ( code,name )
{
	var ClientID = GetInputSupplierClientID();
	InputSupplier_GetControl(ClientID,"txtCode").value = code;
	InputSupplier_GetControl(ClientID,"txtInput").value = name;
}


function InputSupplier_CodeFocus(obj, ClientID)
{
	InputSupplier_GetControl(ClientID, "txtOldValue").value = obj.value;
}

function InputSupplier_CodeBlur(obj, ClientID)
{
	var NewValue = obj.value;


	if (NewValue != InputSupplier_GetControl(ClientID, "txtOldValue").value)
	{
		InputSupplier_GetControl(ClientID, "txtOldValue").value = obj.value;
		InputSupplier_GetData(ClientID, NewValue, "");
	}
}

function InputSupplier_GetData(ClientID, val, type)
{

	var txtCode = InputSupplier_GetControl(ClientID, "txtCode");
	var txtName = InputSupplier_GetControl(ClientID, "txtName");
	var divName = InputSupplier_GetControl(ClientID, "divName");
	var txtHint = InputSupplier_GetControl(ClientID, "txtHint");
	var divHint = InputSupplier_GetControl(ClientID, "divHint");
	var txtInput = InputSupplier_GetControl(ClientID, "txtInput");
	var txtSortID = InputSupplier_GetControl(ClientID, "txtSortID");
	

	var arr = GetSupplierNameHint(val, ClientID, type);
	txtCode.value = arr[0];
	txtName.value = arr[1];
	txtHint.value = arr[2];
	txtSortID.value = arr[4];
	
//	divName.innerText = txtName.value;
	divHint.innerText = txtHint.value;
	
	if (arr[3] == "1")
	{
		txtInput.value = txtName.value;
		InputSupplier_GetControl(ClientID, "txtOldValue").value = txtInput.value;
	}
}

