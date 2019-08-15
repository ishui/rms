function GetUserName(Value)
{
	if (Value == "") return "";

	var UserName = "";

	var items = GetXMLResult("../Systems/GetUserData.aspx?Value=" + escape(Value));	

	UserName = GetXMLTagData(items, "UserName");
	
	return UserName;
}

function GetUserNameHint(Value,ImagePath)
{
	var arr = new Array();
	var UserCode = "";
	var UserName = "";
	var Hint = "";
	var IsExists = "";
	
	if (Value != "")
	{
		var items = GetXMLResult(ImagePath+"../Systems/GetUserData.aspx?Value=" + escape(Value));

		UserCode = GetXMLTagData(items, "UserCode");
		UserName = GetXMLTagData(items, "UserName");
		Hint = GetXMLTagData(items, "Hint");
		IsExists = GetXMLTagData(items, "IsExists");
	}

	arr.push(UserCode);
	arr.push(UserName);
	arr.push(Hint);
	arr.push(IsExists);
	return arr;
}

function InputUser_GetControl(ClientID, objName)
{
	var obj = document.all(ClientID + ":" + objName);
	if (!obj)
		obj = document.all(ClientID + "_" + objName);
		
	return obj
}

function InputUser_SelectUser(ClientID,ImagePath)
{
	var val = InputUser_GetControl(ClientID, "txtCode").value;
	OpenMiddleWindow(ImagePath+"../SelectBox/SelectPerson.aspx?&Type=Single&UserCodes="+val+"&ReturnFunc=InputUser_SelectUserReturn&Flag=" + ClientID,'—°‘Ò»À‘±');
}

function InputUser_SelectUserReturn(code, name, ClientID)
{
	var txtCode = InputUser_GetControl(ClientID, "txtCode");
//	var divName = InputUser_GetControl(ClientID, "divName");
	var txtName = InputUser_GetControl(ClientID, "txtName");
	var txtHint = InputUser_GetControl(ClientID, "txtHint");
	var divHint = InputUser_GetControl(ClientID, "divHint");
	var txtInput = InputUser_GetControl(ClientID, "txtInput");
	
	txtCode.value = code;
	txtName.value = name;
	txtInput.value = name;
	txtHint.value = "";
	divHint.innerText = txtHint.value;
	
//	divName.innerText = txtName.value;
}

function InputUser_CodeFocus(obj, ClientID)
{
	InputUser_GetControl(ClientID, "txtOldValue").value = obj.value;
}

function InputUser_CodeBlur(obj, ClientID,ImagePath)
{
//	var divName = InputUser_GetControl(ClientID, "divName");
	var txtCode = InputUser_GetControl(ClientID, "txtCode");
	var txtName = InputUser_GetControl(ClientID, "txtName");
	var txtHint = InputUser_GetControl(ClientID, "txtHint");
	var divHint = InputUser_GetControl(ClientID, "divHint");
	var txtInput = InputUser_GetControl(ClientID, "txtInput");
	
	var NewValue = obj.value;
	if (NewValue != InputUser_GetControl(ClientID, "txtOldValue").value)
	{
		InputUser_GetControl(ClientID, "txtOldValue").value = obj.value;

		var arr = GetUserNameHint(NewValue,ImagePath);
		
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
