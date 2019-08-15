function InputStationUser_GetControl(ClientID, objName)
{
	var obj = document.all(ClientID + ":" + objName);
	if (!obj)
		obj = document.all(ClientID + "_" + objName);
		
	return obj
}

function InputStationUser_Select(ClientID,ImagePath)
{
	var UserCodes = InputStationUser_GetControl(ClientID, "txtUserCodes").value;
	var StationCodes = InputStationUser_GetControl(ClientID, "txtStationCodes").value;
	OpenMiddleWindow(ImagePath+"../SelectBox/SelectSUMain.aspx?UserCodes="+UserCodes+"&StationCodes="+StationCodes+"&ReturnFunc=InputStationUser_SelectReturn&Flag=" + ClientID,'');
}

function InputStationUser_SelectReturn(userCodes,userNames,stationCodes,stationNames,ClientID)
{
	var txtUserCodes = InputStationUser_GetControl(ClientID, "txtUserCodes");
	var txtStationCodes = InputStationUser_GetControl(ClientID, "txtStationCodes");
	
	var txtUserNames = InputStationUser_GetControl(ClientID, "txtUserNames");
	var txtStationNames = InputStationUser_GetControl(ClientID, "txtStationNames");

	var divUserNames = InputStationUser_GetControl(ClientID, "divUserNames");
	var divStationNames = InputStationUser_GetControl(ClientID, "divStationNames");
	
	var divName = InputStationUser_GetControl(ClientID, "divName");
	var txtName = InputStationUser_GetControl(ClientID, "txtName");
	var txtHint = InputStationUser_GetControl(ClientID, "txtHint");
	var divHint = InputStationUser_GetControl(ClientID, "divHint");
	
	txtUserCodes.value = userCodes;
	txtStationCodes.value = stationCodes;
	
	txtUserNames.value = userNames;
	txtStationNames.value = stationNames;

	txtName.value = getString(userNames, stationNames);
	divName.innerText = txtName.value;
	
	txtHint.value = "";
	divHint.innerText = txtHint.value;
	
	if (!InputStationUser_MyOnClientPost()) return false;
}

function getString(str1,str2)
{
	if(str1.length>0&&str2.length>0)
	{
		return str1+','+str2;
	}
	else
		return str1+str2;
}

function InputStationUser_Null()
{
	return true;
}
