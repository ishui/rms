function InputSubject_GetControl(ClientID, objName)
{
	var obj = document.all(ClientID + ":" + objName);
	if (!obj)
		obj = document.all(ClientID + "_" + objName);
		
	return obj
}

function InputSubject_SelectSubject(ProjectControl,ImagePath,ClientID)
{
	var val = InputSubject_GetControl(ClientID, "txtCode").value;
	var strUrl = "";
	
	if(ProjectControl != "")
	    strUrl = ImagePath+"Finance/SelectSubjectTree.aspx?ReturnFunc=InputSubject_SelectSubjectReturn&ProjectCode=" + document.all(ProjectControl).value + "&SubjectCode=" + val + "&Define1=" + ClientID;
	else
	    strUrl = ImagePath+"Finance/SelectSubjectTree.aspx?ReturnFunc=InputSubject_SelectSubjectReturn&SubjectSetCode=" + InputSubject_GetControl(ClientID, "txtSubjectSetCode").value + "&SubjectCode=" + val + "&Define1=" + ClientID;
	
	var iWidth = 500;
	var iHeight = 600;
	window.open(strUrl,"SelectSubject","width="+iWidth+",height="+iHeight+",fullscreen=0,top="+(window.screen.height-iHeight)/2+",left="+(window.screen.width-iWidth)/2+",menubar=no,toolbar=no,scrollbars=yes,status=no,titlebar=no,resizable=yes,location=no");
}

function InputSubject_SelectSubjectReturn(code, name, ClientID)
{
	var txtCode = InputSubject_GetControl(ClientID, "txtCode");
	var divName = InputSubject_GetControl(ClientID, "divName");
	var divHint = InputSubject_GetControl(ClientID, "divHint");
	var txtName = InputSubject_GetControl(ClientID, "txtName");
	var txtHint = InputSubject_GetControl(ClientID, "txtHint");

	txtCode.value = code;
	txtName.value = name;
	txtHint.value = "";

	divName.innerText = txtName.value;//arr[0];
	divHint.innerText = "";//arr[1];
}

function InputSubject_CodeFocus(obj, ClientID)
{
	InputSubject_GetControl(ClientID, "txtOldValue").value = obj.value;
}

function InputSubject_CodeBlur(obj, ClientID)
{
	var divName = InputSubject_GetControl(ClientID, "divName");
	var divHint = InputSubject_GetControl(ClientID, "divHint");
	var txtName = InputSubject_GetControl(ClientID, "txtName");
	var txtHint = InputSubject_GetControl(ClientID, "txtHint");
	
	var NewValue = obj.value;
	if (NewValue != InputSubject_GetControl(ClientID, "txtOldValue").value)
	{
		InputSubject_GetControl(ClientID, "txtOldValue").value = obj.value;

		var arr = GetSubjectFullNameHint(NewValue, InputSubject_GetControl(ClientID, "txtSubjectSetCode").value);
		
		txtName.value = arr[0];
		txtHint.value = arr[1];
		
		divName.innerText = arr[0];
		divHint.innerText = arr[1];
	}
}
