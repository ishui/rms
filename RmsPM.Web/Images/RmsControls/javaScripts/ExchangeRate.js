function MoneyTypeChanged(id)
{
	myid = document.getElementById(id);   
	ShowExchange(myid.options[myid.selectedIndex].text,id);
}
//��ʾĬ�ϻ�����Ϣ
function ShowExchange(moneyType,typeId)
{
        var idlength = typeId.length;
        id=typeId.substring(0,idlength-2)+"_E_t";
        id2=typeId.substring(0,idlength-2)+"_E";
    	var xmlDoc = new ActiveXObject("Microsoft.XMLDOM");
		xmlDoc.async="false";
		var name = "ExchangeRate.xml";					
		xmlDoc.load(name);
		xmlObj=xmlDoc.documentElement;
		nodes = xmlDoc.documentElement.childNodes;
		lenght = xmlObj.childNodes.length;
		
		for (var i=0;i<lenght;i++)
		{
			if(moneyType==xmlObj.childNodes(i).selectSingleNode("MoneyType").nodeTypedValue)
			{
			     //if(  "�м��"= xmlObj.childNodes(i).selectSingleNode("ExchangeRate").nodeTypedValue;)
			    document.getElementById(id).value = xmlObj.childNodes(i).selectSingleNode("RemittanceAverage").nodeTypedValue;
				document.getElementById(id2).value = xmlObj.childNodes(i).selectSingleNode("RemittanceAverage").nodeTypedValue;
				//alert(moneyType.substring(0,3));
				if(moneyType.substring(0,3)=="�����")
				{
					//alert(moneyType);
					document.getElementById(id).disabled=true;
				}
				else
				{
					document.getElementById(id).disabled=false;
				}		     
			    break;;
			}			
			else if(i==lenght-1)
			{
				document.getElementById(id).value = "00";	
				document.getElementById(id2).value = "00";					
				document.getElementById(id).disabled=false;
			}			
		}
		ResetRMB(typeId);
		//mychange();
		
}
//���ʸı�
function SumToRMB(id)
{
		
}

		function CashChange(oEdit, oldValue, oEvent)
		{
			//alert(id);
			ReWriteCashChange();
			//mychange(id);
		}
		//���ʸı�ʱ
		function ExchangeChanged(oEdit, oldValue, oEvent)
		{
			//mychange(id);
			ReWriteCashChange()
		}
		function mychange()
		{
			ResetRMB(id);				
		}
		function ResetRMB(id)
		{
			idlength=id.length;
			
			myvalue = (document.getElementById(id.substring(0,idlength-2)+"_E").value)*(document.getElementById(id.substring(0,idlength-2)+"_C").value);
			document.getElementById(id.substring(0,idlength-2)+"_R").innerHTML=myvalue;
			if(myvalue!="0")
			{
				(document.getElementById(id.substring(0,idlength-2)+"_Y").InnerHtml="Ԫ";				
			}		
			//alert(document.getElementById(id.substring(0,idlength-2)+"_C").value);			
			//alert(document.getElementById(id.substring(0,idlength-2)+"_E").value);				
		}
		//��д���ı亯��	
		function ReWriteCashChange()
		{
			var id //CashID
			//ResetRMB(id);
		}
		function ReWriteCashChange()
		{
			var id //ChangeID
			//ResetRMB(id);		
		}