function ConvertFloat(val)
{
	var sval = val.toString();
	var re = /,/g;
	sval = sval.replace(re, "");
	
	if (sval == "")
		f = 0;
	else
		f = parseFloat(sval);
		
	return f;
}

function FormatNumber(num,dotLen)
{
	var srcStr;
	var resultStr;
	var nTen;
	var i;

	srcStr = ""+num+"";
	strLen = srcStr.length;

	dotPos = srcStr.indexOf(".",0);
	if (dotPos == -1)
	{
		resultStr = srcStr;
		
		if (dotLen > 0)
		{
			resultStr = resultStr+".";
			for (i=0;i<dotLen;i++)
			{
				resultStr = resultStr+"0";
			}
		}
		return resultStr;
	}
	else
	{
		if ((strLen - dotPos - 1) >= dotLen)
		{
			nAfter = dotPos + dotLen + 1;
			nTen =1;
			for(j=0;j<dotLen;j++)
			{
				nTen = nTen*10;
			}
			resultStr = Math.round(parseFloat(srcStr)*nTen)/nTen;
			return resultStr;
		}
		else
		{
			resultStr = srcStr;
			for (i=0;i<(dotLen - strLen + dotPos + 1);i++)
			{
				resultStr = resultStr+"0";
			}
			return resultStr;
		}
	}
}

/// example:
///	alert(formatNumber(0,''));
///	alert(formatNumber(12432.21,'#,###'));
///	alert(formatNumber(12432.21,'#,###.000#'));
///	alert(formatNumber(12432,'#,###.00'));
///	alert(formatNumber(12432.419,'#,###.0#'));
///	alert(formatNumber(12432.419,'#,###.####'));
function formatNumber(number,pattern)
{
	var str			= number.toString();
	var number2 = number;
	var strInt;
	var strFloat;
	var formatInt;
	var formatFloat;
	
	//负数去掉“-”  2005.9.28
	var neg = "";
	if (str.substring(0, 1) == "-")
	{
		neg = "-";
		str = str.substring(1, str.length);
		number2 = str;
	}
	
	if(/\./g.test(pattern))
	{
		formatInt		= pattern.split('.')[0];
		formatFloat		= pattern.split('.')[1];
	}
	else
	{
		formatInt		= pattern;
		formatFloat		= null;
	}

	if(/\./g.test(str))
	{
		if(formatFloat!=null)
		{
			var tempFloat	= Math.round(parseFloat('0.'+str.split('.')[1])*Math.pow(10,formatFloat.length))/Math.pow(10,formatFloat.length);
			strInt		= (Math.floor(number2)+Math.floor(tempFloat)).toString();				
			strFloat	= /\./g.test(tempFloat.toString())?tempFloat.toString().split('.')[1]:'0';			
		}
		else
		{
			strInt		= Math.round(number2).toString();
			strFloat	= '0';
		}
	}
	else
	{
		strInt		= str;
		strFloat	= '0';
	}
	
	if(formatInt!=null)
	{
		var outputInt	= '';
		var zero		= formatInt.match(/0*$/)[0].length;
		var comma		= null;
		if(/,/g.test(formatInt))
		{
			comma		= formatInt.match(/,[^,]*/)[0].length-1;
		}
		var newReg		= new RegExp('(\\d{'+comma+'})','g');

		if(strInt.length<zero)
		{
			outputInt		= new Array(zero+1).join('0')+strInt;
			outputInt		= outputInt.substr(outputInt.length-zero,zero)
		}
		else
		{
			outputInt		= strInt;
		}

		var 
		outputInt			= outputInt.substr(0,outputInt.length%comma)+outputInt.substring(outputInt.length%comma).replace(newReg,(comma!=null?',':'')+'$1')
		outputInt			= outputInt.replace(/^,/,'');

		strInt	= outputInt;
	}

	if(formatFloat!=null)
	{
		var outputFloat	= '';
		var zero		= formatFloat.match(/^0*/)[0].length;

		if(strFloat.length<zero)
		{
			outputFloat		= strFloat+new Array(zero+1).join('0');
			//outputFloat		= outputFloat.substring(0,formatFloat.length);
			var outputFloat1	= outputFloat.substring(0,zero);
			var outputFloat2	= outputFloat.substring(zero,formatFloat.length);
			outputFloat		= outputFloat1+outputFloat2.replace(/0*$/,'');
		}
		else
		{
			outputFloat		= strFloat.substring(0,formatFloat.length);
		}

		strFloat	= outputFloat;

		//num"0", format".#" -> ""    xyq 2005.9.28
		if ((strFloat == "0") && (zero == 0))
		{
			strFloat = "";
		}
		
	}
	else
	{
		if(pattern!='' || (pattern=='' && strFloat=='0'))
		{
			strFloat	= '';
		}
	}

	//负数加上“-”  2005.9.28
	if (neg == "-")
	{
		strInt = neg + strInt;
	}

	return strInt+(strFloat==''?'':'.'+strFloat);
}
