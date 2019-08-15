// JScript File
function inputmaterialinType_selectmaterialin(ClientID,ImagePath,ProjectCode)
{

	var val = inputmaterialin_GetControl(ClientID, "txtCode").value;
	OpenMiddleWindow(ImagePath+"../SelectBox/selectmaterialin.aspx?ReturnFunc=inputmaterialin_selectmaterialinReturn&Flag=" + ClientID+"&ProjectCode="+ProjectCode,'选择材料');
	//OpenMiddleWindow(ImagePath+"../SelectBox/selectmaterialintype.aspx?&Type=Single&UserCodes="+val+"&ReturnFunc=inputmaterialin_selectmaterialinReturn&Flag=" + ClientID,'选择人员');
}

function inputmaterialin_GetControl(ClientID, objName)
{
	var obj = document.all(ClientID + ":" + objName);
	if (!obj)
		obj = document.all(ClientID + "_" + objName);
		
	return obj
}
function inputmaterialin_selectmaterialinReturn(code, name, ClientID,MaterialCode,OutPrice,Spec,Unit,InQty,InDate,InPrice)
{
	var txtCode = inputmaterialin_GetControl(ClientID, "txtCode");
	var divName = inputmaterialin_GetControl(ClientID, "divName");
	var txtName = inputmaterialin_GetControl(ClientID, "txtName");
	var txtHint = inputmaterialin_GetControl(ClientID, "txtHint");
	var divHint = inputmaterialin_GetControl(ClientID, "divHint");
	//var txtInput = inputmaterialin_GetControl(ClientID, "txtInput");
	
	var txtMaterialCode = inputmaterialin_GetControl(ClientID, "txtMaterialCode");
	var txtOutPrice = inputmaterialin_GetControl(ClientID, "txtOutPrice");
	txtCode.value = code;
	txtName.value = name;
	//txtInput.value = name;
	txtHint.value = "";
	divHint.innerText = txtHint.value;
	divName.innerText = txtName.value;
	txtMaterialCode.value = MaterialCode;
	txtOutPrice.value = OutPrice;
	divName.title = "规格" + Spec + " 单位" + Unit + " 库存量" + InQty + " 入库日期" + InDate + " 入库单价" + InPrice;
}
function inputmaterialin_CodeFocus(obj, ClientID)
{
	inputmaterialin_GetControl(ClientID, "txtOldValue").value = obj.value;
}
function inputmaterialin_CodeBlur(obj, ClientID,ImagePath)
{
}
