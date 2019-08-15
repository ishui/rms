function UCSelectProjectMulti_GetControl(ClientID, objName)
{
	var obj = document.all(ClientID + ":" + objName);
	if (!obj)
		obj = document.all(ClientID + "_" + objName);
		
	return obj;
}

function UCSelectProjectMulti_Select(ClientID)
{
	var val = UCSelectProjectMulti_GetControl(ClientID, "txtCode").value;
	var access = UCSelectProjectMulti_GetControl(ClientID, "txtAccess").value;
	OpenMiddleWindow("../SelectBox/SelectProject.aspx?&Type=Multi&access=" + access + "&SelectCodes=" + val +"&ReturnFunc=UCSelectProjectMulti_SelectReturn&Flag=" + ClientID);
}

function UCSelectProjectMulti_SelectReturn(code, name, ClientID)
{
	var txtCode = UCSelectProjectMulti_GetControl(ClientID, "txtCode");
	var txtName = UCSelectProjectMulti_GetControl(ClientID, "txtName");
	var divName = UCSelectProjectMulti_GetControl(ClientID, "divName");

    txtCode.value = code;
    txtName.value = name;

	divName.innerText = txtName.value;
}
