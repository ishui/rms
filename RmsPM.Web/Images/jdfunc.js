ERR_NUMBER = "非法的数值！";
ERR_BIG_NUMBER = "数额太大，系统无法接受！";
ERR_STRING_LONG = "输入超长。最大长度为";
ERR_YEAR = "非法的年份！";
ERR_MONTH = "非法的月份！";
ERR_DAY = "非法的日期！";
ERR_EMAIL = "非法的Email地址！";
ERR_NUMBER_NULL = "必须输入数值";
ERR_INT_NULL = "必须输入数值";
ERR_STRING_NULL = "必须输入";
ERR_SELECT_NULL = "必须选择";

ERR_NUMBER2 = "Invalid number";
ERR_BIG_NUMBER2 = "Number too big to be accepted by system";
ERR_STRING_LONG2 = "Input too long. Maximal length is ";
ERR_YEAR2 = "Invalid year";
ERR_MONTH2 = "Invalid month";
ERR_DAY2 = "Invalid date";
ERR_EMAIL2 = "Invalid e-mail address";
ERR_NUMBER_NULL2 = "Must input";
ERR_INT_NULL2 = "Must input";
ERR_STRING_NULL2 = "Must input";
ERR_SELECT_NULL = " Must select";

ERR_INT2 = "Must input integer";
ERR_INT = "非法的整数";

ERR_POSITIVE_INT2 = "Must input positive integer";
ERR_POSITIVE_INT = "非法的正整数";

msgLang=1;
function trim(val)
{
	var str = val+"";
	if (str.length == 0) return str;
	var re = /^\s*/;
	str = str.replace(re,'');
	re = /\s*$/;
	return str.replace(re,'');
}

function checknumber(data,lbl){
	var tmp ;
	if (data == "") return true;
	var re = /^[\-\+]?([1-9]\d*|0|[1-9]\d{0,2}(,\d{3})*)(\.\d+)?([Ee][\-\+]?\d+)?$/;
	if (re.test(data)){
		gar = data + '.';
		tmp = gar.split('.');
		if (tmp[0].length > 15) {
			if (msgLang==2)
				alert(lbl+":"+ERR_BIG_NUMBER2);
			else
				alert(lbl+":"+ERR_BIG_NUMBER);
			return false;
		}
		return true;
	}
	//if (msgLang==2)
	//	alert(lbl+":"+ERR_NUMBER2);
	//else
		alert(lbl+":"+ERR_NUMBER);
	return false;
}

function checknumber_null(data,lbl){
	if (trim(data)==""){
		if (msgLang==2)
			alert(lbl+":"+ERR_NUMBER_NULL2);
		else
			alert(lbl+":"+ERR_NUMBER_NULL);
		return false;
	}
	return true;
}

function checkint_null(data,lbl){
	if (trim(data)==""){
		if (msgLang==2)
			alert(lbl+":"+ERR_INT_NULL2);
		else
			alert(lbl+":"+ERR_INT_NULL);
		return false;
	}
	return true;
}



function checkstring_null(data,lbl)
{
	if (trim(data)==""){
		if (msgLang==2)
			alert(lbl+":"+ERR_STRING_NULL2);
		else
			alert(lbl+":"+ERR_STRING_NULL);
		return false;
	}
	return true;
}

function checkint(data,lbl)
{
	if (data == "") return true;
	var re = /^[\-\+]?([1-9]\d*|0|[1-9]\d{0,2}(,\d{3})*)$/;
	if (re.test(data)) 
		return true;
	if (msgLang==2)
		alert(lbl+":"+ERR_NUMBER2);
	else
		alert(lbl+":"+ERR_NUMBER);
	return false;
}

function checkpositiveint(data,lbl)
{
	if (data == "") return false;
	var dataTemp = parseInt(data);
	if (isNAN(dataTemp)) 
	{
		if (msgLang==2)
			alert(lbl+":"+ERR_INT2);
		else
			alert(lbl+":"+ERR_INT);
		return false;
	}
	else
	{
		if ( dataTemp>0 )
			return ture;
		else
		{
			if (msgLang==2)
				alert(lbl+":"+ERR_POSITIVE_INT2);
			else
				alert(lbl+":"+ERR_POSITIVE_INT);
			return false;		
		}
	}
}

function checkint0(str,lbl)
{

	if (str == "") return false;
	var dataTemp = parseInt(str);
	if ( !isNAN(dataTemp))
		return true;	
	
	if (msgLang==2)
		alert(lbl+":"+ERR_INT2);
	else
		alert(lbl+":"+ERR_INT);
	return false;

}

function checkstring(str,maxlen,lbl)
{
	if (str.length > maxlen){
		if (msgLang==2)
			alert(lbl+":"+ERR_STRING_LONG2+maxlen);
		else
			alert(lbl+":"+ERR_STRING_LONG+maxlen);
		return false;
	}
	return true;
}

function checkyear(year,lbl)
{
	if (year.length == 0) return true;
	var temp = parseInt(year);
	if (!isNaN(temp)){
		if (year == 0) return true;
		low = 1900;
		high = 2150;
		if ((year >= low) && (year <=high)) return true;
	}

	errorYear(lbl);
	return false;
}

function checkmonth(month,low,high,lbl)
{
	var temp = parseInt(month);
	if (!isNaN(temp)){
		temp = parseInt(low);
		if (isNaN(temp)) low = 1;
		temp = parseInt(high);
		if (isNaN(temp)) high = 12;
		if ((year >= low) && (year <=high)) return true;
	}
	errorMonth(lbl);
	return false;
}

function errorYear(lbl)
{
	if (msgLang==2)
		alert(lbl+":"+ERR_YEAR2);
	else
		alert(lbl+":"+ERR_YEAR);
}
function errorMonth(lbl)
{
	if (msgLang==2)
		alert(lbl+":"+ERR_MONTH2);
	else
		alert(lbl+":"+ERR_MONTH);
}
function errorDay(lbl)
{
	if (msgLang==2)
		alert(lbl+":"+ERR_DAY2);
	else
		alert(lbl+":"+ERR_DAY);
}

function checkday(day,year,month,lbl)
{
	err = false;

	if (!checkint(year) || (year < 1900)) {
		errorYear(lbl);
		return false;
	}
	if (!checkint(month) || (month < 1) || (month > 12)){
		errorMonth(lbl);
		return false;
	}
	if (!checkint(day) || (day < 1) || (day > 31)){
		errorDay(lbl);
		return false;
	}
	
	switch (parseInt(month)){
		case 2:
			high =28;
			if ((year % 4 == 0) && (year % 100 != 0))
				{high =29;}
			else if (year % 400 == 0) {high=29;}
			break;
		case 1:
		case 3:
		case 5:
		case 7:
		case 8:
		case 10:
		case 12:
			high =31;
			break;
		default:
			high =30;
	}
	if ((day < 1) || (day > high)){
		errorDay(lbl);
		return false;
	}
	return true;
}

function checkemail(umail,lbl)
{
	umail=trim(umail);
	if (umail.length == 0) return true;
	var re="/^[\-!#\$%&'\*\+\\\.\/0-9=\?A-Z\^_`a-z{|}~]+@[\-!#\$%&'\*\+\\\.\/0-9=\?A-Z\^_`a-z{|}~]+(\.[\-!#\$%&'\*\+\\\.\/0-9=\?A-Z\^_`a-z{|}~]+)+$/";
	if (re.test(umail))
		return true;
	if (msgLang==2)
		alert(lbl+":"+ERR_EMAIL2);
	else
		alert(lbl+":"+ERR_EMAIL);
	return false;
}

function checkemail0(umail,lbl)
{
	if (umail == "") return true;
	var re=/^[\-!#\$%&'\*\+\\\.\/0-9=\?A-Z\^_`a-z{|}~]+@[\-!#\$%&'\*\+\\\.\/0-9=\?A-Z\^_`a-z{|}~]+(\.[\-!#\$%&'\*\+\\\.\/0-9=\?A-Z\^_`a-z{|}~]+)+$/;
	if (re.test(umail))
		return true;
	if (msgLang==2)
		alert(lbl+":"+ERR_EMAIL2);
	else
		alert(lbl+":"+ERR_EMAIL);
	return false;
}


function checktime(ctime)
{
	if (ctime.length == 0) return true;

	var re=/^(([0-9]|[01][0-9]|2[0-3])(:([0-9]|[0-5][0-9])){0,2}|(0?[0-9]|1[0-1])(:([0-9]|[0-5][0-9])){0,2}\s?[aApP][mM])?$/;
	return re.test(ctime);
}

function checkdate(bdate){
	if (bdate.length == 0) return true;
	//var re = /^([1-2]\d{3})\-(0?[1-9]|11|12|10)\-([1-2]?[0-9]|0[1-9]|30|31)$/;
	var re=/^(([1-2]\d{3})\-(0?[1|3|5|7|8]|12|10)\-([1-2]?[0-9]|0[1-9]|30|31)|([1-2]\d{3})\-(0?[4|6|9]|11)\-([1-2]?[0-9]|0[1-9]|30)|([1-2]\d{3})\-(0?[2])\-([1-2]?[0-9]|0[1-9]))$/;
	if (re.test(bdate)){
		if ((parseInt(bdate.split("-")[1])==2)&&(parseInt(bdate.split("-")[2])==29)){
			if (!(parseInt(bdate.split("-")[0])%4==0)&&(!parseInt(bdate.split("-")[0])%10==0)|(parseInt(bdate.split("-")[0])%40==0)){
				return false;
			}
		}
	}
	return re.test(bdate);	
}

function disp_something(str){
	obj=document.all(str);
	if(obj){
		if(obj.style.display==""){
			obj.style.display="none";
		}else if(obj.style.display=="none"){
			obj.style.display="";
		}
	}
}

function MS_over(obj){
	obj.borderColorDark='#808080';
	obj.borderColorLight='#ffffff';
}

function MS_out(obj){
	obj.borderColorDark='#E1E5F4';
	obj.borderColorLight='#E1E5F4';
}

function MS_down(obj){
	obj.borderColorDark='#ffffff';
	obj.borderColorLight='#808080';
}

function MS_up(obj){
	obj.borderColorDark='#E1E5F4';
	obj.borderColorLight='#E1E5F4';
}

function jsLTrim(str){
	var rtnStr;
	rtnStr=""
	for (var i=0;i<str.length;i++){
		if (str.charAt(i)!=" "){
			rtnStr=str.substr(i);
			break;
		}
	}
	return rtnStr;
}

//==========================================
//Purpose: Trim right spaces
//==========================================
function jsRTrim(str){
	var rtnStr;
	rtnStr=""
	str=str+" ";
	for (var i=str.length-1;i>=0;i--){
		if (str.charAt(i)!=" "){
			rtnStr=str.substring(0,i+1);
			break;
		}
	}
	return rtnStr;
}

//==========================================
//Purpose: Trim both left and right spaces
//==========================================
function jsTrim(str){
	return(jsLTrim(jsRTrim(str)));
}

//分别给不同情况的表单添加不同的样式表
function SetEditMode(ExceptFld){
//必须预先初始化变量 CanEdit
	var i,x;
	for (i=0;i<document.all.length;i++){
		x=document.all(i);
		if (x.tagName=="INPUT"|x.tagName=="TEXTAREA"|x.tagName=="SELECT"){
			if (!CanEdit&&x.name!=ExceptFld){
				x.disabled=true;
				x.className="line";
			}else{
				x.disabled=false;
				x.className="line3";
			};
			if(x.type=='hidden'){
				x.disabled=false;
			};
		};

	};
}
//将得到的数字数据四舍五入
function round(money){
	var strMoney ;
	if (money>=0){
		strMoney = money + 0.005;
	}
	else{
		strMoney = money - 0.005;
	}
	strMoney = strMoney * 100;
	strMoney = parseInt(strMoney);
	return strMoney/100;
}

//检验用户提交的数字数据，能分辨财务类型
function saveMoney(parameter){
	var submoney;
	var ceasePosition;
	var money;
	var subMoney;
	var i;
	money = parameter.value;
	money = jsTrim(money);
	if (  money==""  ){
		alert("请输入数值！");
		return false;
	}
	ceasePosition = money.indexOf(".");
	if ( ceasePosition > 0 ) {
		subMoney = money.substring(ceasePosition+1);
		for (i=0;i < subMoney.length;i++){ 
			if (isNaN(subMoney.substring(i,i+1))){
				alert("你所输入的金额不是货值型！");
				return false;
			}
		} 
	}
	var loopN = 0;
	var loopI = 1;
	var loopArray = new Array();
	var commaStr
	commaStr = ceasePosition
	if (ceasePosition < 0 )  commaStr = money.length; 
	while (( loopN < commaStr ) || ( loopN == commaStr )){
		if (( loopN < commaStr ) || ( loopN == commaStr )){
			loopN  = loopN + 4;
			loopArray[loopI]= loopN;
			loopI ++;
		}
	}
	
	var j=1;
	for ( i=1;i< commaStr+1;i++ ){
		if ( i != loopArray[j] ){
			if ( money.substring( commaStr-i+1,commaStr-i )=="," ){
				alert("你所输入的金额不是货值型！");
				return false;
			}
		}
		else j ++;
	}
	if ( money.indexOf(",") >= 0 ){
		var moneyArray = new Array();
		moneyArray = money.split(",");
		money="";
		for ( i=0; i<moneyArray.length; i++){
			money = money + moneyArray[i];
		} 
	}
	//alert(money);
	if (! isNaN( money )){
		money = round(Number(money));
	} 
	else { 
		alert("你所输入的金额不是货值型！");
		return false;
	};
	//alert(money);
	parameter.value=money;
	return true;
	//alert(parameter.value);
}
function formats(expr,decplaces){
	var str=""+Math.round(eval(expr)*Math.pow(10,decplaces));
	while(str.length<=decplaces){
		str="0"+str;
	}
	var decpoint=str.length-decplaces;
	if (decplaces==0){
		return str.substring(0,decpoint);
	}
	else{
		return str.substring(0,decpoint)+"."+str.substring(decpoint,str.length);
	}	
}

//去头尾空格
function jstrim(str){
	for (i=1;;i++){
		if (str.length>=1){
			if (str.substring(0,1)==" "){
				str=str.substring(1,str.length);
			}
			else{
				break;
			}
		}
		else{
			break;
		}
	}
	for (i=1;;i++){
		if (str.length>=1){
			if (str.substring(str.length-1,str.length)==" "){
				str=str.substring(0,str.length-1);
			}
			else{
				break;
			}
		}
		else{
			break;
		}
	}
	return str;
}

//返回固定日期+num的新日期
//返回固定日期+num的新日期
function getDateStr(olddate,num){
	var datestr,num,newdate,newdateMs,newdatestr,month;
	datestr=olddate.split('-');
	newdate=new Date(datestr[0],datestr[1]-1,datestr[2]);
	newdateMs=newdate.getTime()+1000*60*60*24*parseInt(num);
	newdate.setTime(newdateMs);
	//if (newdate.getMonth()=="0"){
	//	month="12";
	//}
	//else{
		month=newdate.getMonth()+1;
	//}
	newdatestr=newdate.getYear()+"-"+month+"-"+newdate.getDate();
	return newdatestr;
}