// JScript File

function InputMaterialType_SelectMaterial(ClientID,ImagePath)
{ 
    //如果有领料则不能修改名称
    var OutQty = InputMaterial_GetControl(ClientID, "txtOutQty").value;
    
    if(OutQty != 0)
    {
       alert("已经领料，不能再修改入库材料名称");
       return;
    }
	
	OpenMiddleWindow(ImagePath+"../SelectBox/selectmaterial.aspx?ReturnFunc=InputMaterial_SelectMaterialReturn&Flag=" + ClientID,'选择材料');
	//OpenMiddleWindow(ImagePath+"../SelectBox/Selectmaterialtype.aspx?&Type=Single&UserCodes="+val+"&ReturnFunc=InputMaterial_SelectMaterialReturn&Flag=" + ClientID,'选择人员');
}

function InputMaterial_GetControl(ClientID, objName)
{
	var obj = document.all(ClientID + ":" + objName);
	if (!obj)
		obj = document.all(ClientID + "_" + objName);
		
	return obj
}
function InputMaterial_SelectMaterialReturn(code, name, ClientID,unit,spec)
{
	var txtCode = InputMaterial_GetControl(ClientID, "txtCode");
	var divName = InputMaterial_GetControl(ClientID, "divName");
	var txtName = InputMaterial_GetControl(ClientID, "txtName");
	var txtHint = InputMaterial_GetControl(ClientID, "txtHint");
	var divHint = InputMaterial_GetControl(ClientID, "divHint");
	var txtUnit = InputMaterial_GetControl(ClientID, "txtUnit");
	txtCode.value = code;
	txtName.value = name;
	//txtInput.value = name;
	txtHint.value = "";
	divHint.innerText = txtHint.value;
	
	divName.innerText = txtName.value;
	divName.title = "规格" + spec + " 单位" + unit;
}
function InputMaterial_CodeFocus(obj, ClientID)
{
	InputMaterial_GetControl(ClientID, "txtOldValue").value = obj.value;
}
function InputMaterial_CodeBlur(obj, ClientID,ImagePath)
{/*
//	var divName = InputMaterial_GetControl(ClientID, "divName");
	var txtCode = InputMaterial_GetControl(ClientID, "txtCode");
	var txtName = InputMaterial_GetControl(ClientID, "txtName");
	var txtHint = InputMaterial_GetControl(ClientID, "txtHint");
	var divHint = InputMaterial_GetControl(ClientID, "divHint");
	var txtInput = InputMaterial_GetControl(ClientID, "txtInput");
	
	var NewValue = obj.value;
	if (NewValue != InputMaterial_GetControl(ClientID, "txtOldValue").value)
	{
		InputMaterial_GetControl(ClientID, "txtOldValue").value = obj.value;

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
	}*/
}