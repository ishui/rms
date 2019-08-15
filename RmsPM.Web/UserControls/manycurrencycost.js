function SetTotalMoney(id,clientid)
{
	//try
	{
		var myvalue;
		myle=id.length;
		idstring = id.substring(0,myle-2);
		//alert(idstring+"_R");
		myid=document.getElementById(idstring+"R");
		Oldmyid=document.getElementById(idstring+"O");
		Totalid=document.getElementById(clientid+'_TxtTotalMoney');
		ValueTotalid=document.getElementById(clientid+'_TxtTotalMoney_V');		
		//alert(Totalid.value);
		//alert(Oldmyid.value);
		//alert("原来部值"+ValueTotalid.value);
		//alert(Oldmyid.value);
		myvalue=parseFloat(ValueTotalid.value)-parseFloat(Oldmyid.value);
		//alert("第一"+myvalue);
		//alert("myid"+myid.value);
		ValueTotalid.value=parseFloat(myvalue)+parseFloat(myid.value);//ConvertFloat(Totalid.value-Oldmyid.value)+ConvertFloat(myid.value);
		//alert(Totalid.value);
		Totalid.value=formatNumber(ValueTotalid.value, "#,###.00");
		//myid.value;
		//ValueTotalid.value=0;
		//TxtTotalMoney_V.
	}		
}
function SetTempMoney(id)
{
        var myvalue;
		myle=id.length;
		idstring = id.substring(0,myle-2);
		//alert(idstring+"_R");
		myid=document.getElementById(idstring+"R");
		Oldmyid=document.getElementById(idstring+"O");
		Totalid=document.getElementById('<%=ClientID%>_TxtTotalMoney');
		ValueTotalid=document.getElementById('<%=ClientID%>_TxtTotalMoney_V');		
		//alert(Totalid.value);
		//alert(Oldmyid.value);
		//alert("原来部值"+ValueTotalid.value);
		//alert(Oldmyid.value);
		myvalue=parseFloat(ValueTotalid.value)-parseFloat(Oldmyid.value);
		//alert("第一"+myvalue);
		//alert("myid"+myid.value);
		ValueTotalid.value=parseFloat(myvalue)+parseFloat(myid.value);//ConvertFloat(Totalid.value-Oldmyid.value)+ConvertFloat(myid.value);
		//alert(Totalid.value);
		Totalid.value=formatNumber(ValueTotalid.value, "#,###.00");
}
function CloseDetail(clientid)
{
	document.getElementById(clientid+'_tdDetial').style.display="none";
	document.getElementById(clientid+'_CostDetail').style.display="none";
	document.getElementById(clientid+'_Bt_AddMoney').style.display="";
	document.getElementById(clientid+'_btnSave').style.display="none";
	document.getElementById(clientid+'_Bt_Add').style.display="none";
	document.getElementById(clientid+'_Bt_Close').style.display="none";
	return false;
}
function ShowCostDetail(clientid)
{
	document.getElementById(clientid+'_Bt_AddMoney').style.display="none";
	document.getElementById(clientid+'_tdDetial').style.display="";
	document.getElementById(clientid+'_CostDetail').style.display="";
	document.getElementById(clientid+'_Bt_Close').style.display="";
	if(document.getElementById(clientid+'_Bt_AddMoney').value==" 查看 ")
	{
		document.getElementById(clientid+'_btnSave').style.display="none";
		document.getElementById(clientid+'_Bt_Add').style.display="none";
	}
	else
	{
		document.getElementById(clientid+'_btnSave').style.display="";
		document.getElementById(clientid+'_Bt_Add').style.display="";
	}
	return false;
}
function ObjectSpread(obj)
{
    obj.style.height = "100px";
}
function ObjectReduce(obj)
{
    obj.style.height = "90%";
}