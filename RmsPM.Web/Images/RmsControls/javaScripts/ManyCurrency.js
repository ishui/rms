function MoneyTypeChanged(id)
{
  //ShowExchange(document.getElementById(id).value);
  //idlength=id.length
	//alert(id);
	myid = document.getElementById(id);   
	ShowExchange(myid.options[myid.selectedIndex].text,id);
	//alert(myid.options[myid.selectedIndex].text);
	//alert(myid.options[myid.selectedIndex].text);
}
function InputChange(id)
{
	CashChange();
}  
function CashChange(id)
{
   // alert("CashChange:");
    //e=window.event;
    //window
     //gaoyuan1.options[gaoyuan1.selectedIndex].
}
//显示默认汇率信息
function ShowExchange(moneyType,typeId)
{
        var idlength = typeId.length;
       // var str=typeId;  
        id=typeId.substring(0,idlength-2)+"_E";
        id2=typeId.substring(0,idlength-2)+"_F";
    	var xmlDoc = new ActiveXObject("Microsoft.XMLDOM");
		xmlDoc.async="false";
		var name = "ExchangeRate.xml";					
		xmlDoc.load(name);
		xmlObj=xmlDoc.documentElement;
		nodes = xmlDoc.documentElement.childNodes;
		lenght = xmlObj.childNodes.length;
		//id=id+"_"+id+"_M";
		//id2=id+"_"+id+"_V";
		
		
		
		for (var i=0;i<lenght;i++)
		{
			if(moneyType==xmlObj.childNodes(i).selectSingleNode("MoneyType").nodeTypedValue)
			{
			     //if(  "中间价"= xmlObj.childNodes(i).selectSingleNode("ExchangeRate").nodeTypedValue;)
			    document.getElementById(id).value = xmlObj.childNodes(i).selectSingleNode("RemittanceAverage").nodeTypedValue;
				document.getElementById(id2).value = xmlObj.childNodes(i).selectSingleNode("RemittanceAverage").nodeTypedValue;
				if(moneyType="人民币 (RMB)")
				{
					document.getElementById(id).disabled=true;
				}			     
			     return;
			}			
			else if(i==lenght-1)
			{
				document.getElementById(id).value = "00";	
				document.getElementById(id2).value = "00";			
						
			}			
		}
		ResetRMB(id);
		
}
//汇率改变
function ExchangeChanged(id)
{
}
function SumToRMB(id)
{
		
}

		//发生在窗口得到焦点的时候
		function Moneyonfocus(id)
		{
			//alert();
			//ResetMoney(pid);
		}
		//失去焦点的时候
		function Moneyonblur(id)
		{
			SetMoney(id);
			ResetRMB(id);	
		}
		function JHshNumberText(id)
		{
			if ( !(((window.event.keyCode >= 48) && (window.event.keyCode <= 57)) 
			|| (window.event.keyCode == 13) || (window.event.keyCode == 46) 
			|| (window.event.keyCode == 45)))
			{
				window.event.keyCode = 0;
			}
		}
		function MoneyChange(id)
		{
			//SetMoney(pid);
			//ResetRMB(pid);			
		}
		function ResetRMB(id)
		{
			idlength=id.length;
			myvalue = (document.getElementById(id.substring(0,idlength-2)+"_V").value)*(document.getElementById(id.substring(0,idlength-2)+"_F").value);
			//alert(id);
			//ManyCurrency1_ManyCurrency1_R
			//alert(id+"_"+id+"_R");
			document.getElementById(id.substring(0,idlength-2)+"_R").innerHTML=myvalue;	
			//ManyCurrency1_ManyCurrency1_R.innerHTML						
		}
		function SetMoney(id)
		{
			//alert(id+"_"+id+"_E_"+id+"_"+id+"_E"+"_V");
			idlength=id.length;
			document.getElementById(id.substring(0,idlength-2)+"_V").value=document.getElementById(id.substring(0,idlength-2)+"_C").value;
			document.getElementById(id.substring(0,idlength-2)+"_F").value=document.getElementById(id.substring(0,idlength-2)+"_E").value
		}