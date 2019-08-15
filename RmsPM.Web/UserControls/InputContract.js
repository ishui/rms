function InputContract_GetControl(ClientID, objName)
{
	var obj = document.all(ClientID + ":" + objName);
	if (!obj)
		obj = document.all(ClientID + "_" + objName);
		
	return obj
}

function InputContract_SelectContract(ClientID,ImagePath)
{
    var ProjectCode = InputContract_GetControl(ClientID, "txtProjectCode").value;
 
	OpenLargeWindow(ImagePath+"../SelectBox/SelectSinglecontract.aspx?ProjectCode="+ProjectCode+"&Status=1&ID=" + ClientID,"选择");
}

function InputContract_GetReturnValue(contractName,contractCode,ClientID)
{
	var txtContractCode = InputContract_GetControl(ClientID, "txtCode");
	var txtContractName = InputContract_GetControl(ClientID, "txtName");
	var divHint = InputContract_GetControl(ClientID, "divHint");
	
	txtContractCode.value = contractCode;
	txtContractName.value = contractName;
	divHint.innerText = contractName;
	
	var doothers=new InputContract_DoOthers(contractName,contractCode,ClientID);
	doothers.DoSomething();


}
//以下是返回后可执行其它js函数的扩展 by simon
//inputcontract_doothers是扩展载体，请不要改动
function InputContract_DoOthers(contractName,contractCode,clientID)
{
    this.contractName=contractName;
    this.contractCode=contractCode;
    this.ClientID=clientID    
}
InputContract_DoOthers.prototype.DoSomething=function(){};

//使用说明：
//InputContract_DoOthers.prototype.DoSomething=function()
//{
//alert(this.contractCode);
//};
