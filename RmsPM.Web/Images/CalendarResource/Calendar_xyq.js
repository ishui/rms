<!--
var mainIFrame = null;
var exIFrame = null;

var tableDate = null;
var imgCurrent = null;
var _bCanHide = true;
var _bHaveUpdated = false;
var _bHaveShown = false;

var _bHaveSelectNewValue = false;

var pressWhickTextBox =null;

// 声明　年、月、日、时、分　的变量


var _dCurYear  = null;
var _dCurMonth = null;
var _dCurDay   = null;
var _dCurHour  = null;
var _dCurMinute= null;

var objDate =null;
var objYear =null;
var objMonth=null;
var objDay  =null;
var objHour =null;
var objMinute =null;

var dateValue = "";
var dateName ="";
var strMonth ="";
var strYear  ="";
var isReSet  = false;

var iWidth = 330;	// 日历的宽度


var iHeight= 166;	// 日历的高度 (不可变)

var _nShadowLength = 8;

var _sNeededFilePath = _DatePickerImagePath;

var _sBrowserVersion = navigator.appVersion.substring(navigator.appVersion.indexOf("MSIE") + 5,navigator.appVersion.indexOf("Windows") - 2);
var ieVer55	= _sBrowserVersion >= 5.5 ? true : false;

// 定义所需图片  
var imageReset1 = new Image();
	imageReset1.src = _sNeededFilePath + "reset1.gif";
var imageReset2 = new Image();
	imageReset2.src = _sNeededFilePath + "reset2.gif";
var imageUpDownYear = new Image();
	imageUpDownYear.src = _sNeededFilePath + "btnYears.gif";
var imageToday1 = new Image();
	imageToday1.src = _sNeededFilePath + "bgToday1.gif";
var frameCssPath = _DatePickerCssPath;

var _monthDays = new Array(12);
    _monthDays[ 0] = 31;
    _monthDays[ 1] = 28;
    /*
    bug:未考虑润年2月份可以为29天的情况 ----find by 翟雪东
    */
    _monthDays[ 2] = 31;
    _monthDays[ 3] = 30;
    _monthDays[ 4] = 31;
    _monthDays[ 5] = 30;
    _monthDays[ 6] = 31;
    _monthDays[ 7] = 31;
    _monthDays[ 8] = 30;
    _monthDays[ 9] = 31;
    _monthDays[10] = 30;
    _monthDays[11] = 31;
    
var _weeks = new Array(7);    
	_weeks[0] = "日";
	_weeks[1] = "一";
	_weeks[2] = "二";
	_weeks[3] = "三";
	_weeks[4] = "四";
	_weeks[5] = "五";
	_weeks[6] = "六";
	
var _months = new Array(12);
	_months[ 0] = "1";
	_months[ 1] = "2";
	_months[ 2] = "3";
	_months[ 3] = "4";
	_months[ 4] = "5";
	_months[ 5] = "6";
	_months[ 6] = "7";
	_months[ 7] = "8";
	_months[ 8] = "9";
	_months[ 9] = "10";
	_months[10] = "11";
	_months[11] = "12";
	
var defaultDate = new Date(); 


var _docClick = null;

//xyq 2004.9
function ConvertName(name)
{
	//例：dgList__ctl1_txtStartDate 替换成 dglist:_ctl1:txtStartDate
	
	var s = name;
	var s1;
	var i, i;
	var l = s.length;
	
	i = s.indexOf("_");
	if (i >= 0)
	{
		s = s.substring(0, i) + ":" + s.substring(i+1, l);
		l = s.length;
		
		i = -1;
		for (j=l-1;j>=0;j--)
		{
			i = s.indexOf("_", j);
			if (i >= 0)
			{
				break;
			}
		}
		
		if (i >= 0)
		{
			l = s.length;
			s = s.substring(0, i) + ":" + s.substring(i+1, l);
		}
	}
	alert(s);
	
	return s;
}

function GetCalendar(imgClick,name, year, month, day){	
 	if(_bHaveUpdated && imgClick == imgCurrent) return;
 	
	//xyq 2004.9
	if (!document.all(name))
	{
		name = ConvertName(name);
	}
	
 	_docClick = document.onclick;
 	document.onclick = hideCldTabFrm;
 	
	objYear	=document.getElementById(name + "_year");
	objMonth=document.getElementById(name + "_month");
	objDay	=document.getElementById(name + "_day");
	objHour =document.getElementById(name + "_hour");
	objMinute=document.getElementById(name + "_minute");
	objDate =document.getElementById(name);

	exIFrame = document.all("iFrmCalendar");
	exIFrame.style.position = "absolute";
	
 	dateValue = objDate.value;
	_bHaveSelectNewValue = false;
	
	this.hidDate = (imgClick == null ? document.body : imgClick);
 	var _oldDate = objDate.value;
 	
	var _tmpDate = getValidateDate(_oldDate);
	this._year  = _tmpDate.getFullYear();
	this._month = _tmpDate.getMonth();
	this._day  = _tmpDate.getDate();

 	_dCurYear = this._year;
	_dCurMonth = this._month;
	_dCurDay = this._day;

	_dCurHour =  (objHour==null) ? null : (objHour.value=="" ? 0 : parseInt(objHour.value));
	_dCurMinute = (objMinute==null) ? null : (objMinute.value=="" ? 0 : parseInt(objMinute.value) );
	
 	if(tableDate == null)
	{
		this.createFrame();
		InitDropMonth();
		InitDropYear(true);
	}	
	exIFrame.style.display = "inline";
	if(!_bHaveShown || imgClick != imgCurrent) this.Orientation();

	this.fillCldTabFrm(this._year, this._month, this._day);
	
	
	if(!ieVer55){
		removeShadowDiv();
		MakeDivShadowEffect(tableDate, '#aaaaaa', _nShadowLength);
	}	
	imgCurrent = imgClick;
	
	_bHaveUpdated = true;
	_bHaveShown = true;
}
 

function createFrame(){
	// HTML的基本信息(head body)
	var _sz = "<HTML>"
			+ "<HEAD><link href='" + frameCssPath +"' rel=stylesheet type='text/css'>"
			+ "<meta http-equiv=\"Content-Type\" content=\"text/html; charset=gb2312\"></HEAD>"
			+ "<BODY onselectstart='return false;' leftmargin=0 topmargin=0 rightmargin=0 bottommargin=0 style='cursor:default;background-color:transparent;border:0px solid black;scroll:no'>"
 			+ "</BODY></HTML>";

	mainIFrame = iFrmCalendar;

	if(ieVer55)
		exIFrame.style.filter = "progid:DXImageTransform.Microsoft.Shadow(direction=135,color=#aaaaaa,strength=" + _nShadowLength + ")";
	
	mainIFrame.document.open("text/html","replace");
	mainIFrame.document.write(_sz);
	mainIFrame.document.close();
	
	// table 　 
	tableDate = mainIFrame.document.createElement("TABLE");
	mainIFrame.document.body.insertBefore(tableDate);
	exIFrame.style.display = "inline";
	exIFrame.style.zIndex = "10";
	
 	tableDate.id = "GreatCalendar"
	tableDate.style.position = "absolute";
	tableDate.className = "calendar";
	tableDate.border = 0;

	tableDate.style.pixelWidth = iWidth ;
	tableDate.cellSpacing = 1;
	tableDate.cellPadding = 1;
	tableDate.bgColor = "ffffff";
	tableDate.attachEvent("onmouseover", whenMouseOverCldTabFrm);
	tableDate.attachEvent("onmouseout", whenMouseOutCldTabFrm);

	var _TR = tableDate.insertRow();
	var _TD = _TR.insertCell();
	_TD.colSpan = 7;
	_TD.align = "center";
	_TD.innerHTML = "&nbsp;";		
	// 关键部份 7*7
	for(var i = 0; i < 7; i++){
		_TR = tableDate.insertRow();
		for(var j = 0; j < 7; j++){
			_TD = _TR.insertCell();
			_TD.style.cursor = "default";
			_TD.align = "center";
			_TD.width = 50;
			_TD.innerHTML = "*";
			if(i != 0){
				_TD.style.cursor = "hand";
				_TD.attachEvent("onmouseover", whenMouseOverDateItem);
				_TD.attachEvent("onmouseout", whenMouseOutDateItem);
				_TD.attachEvent("onclick", whenClickDateItem);
			}	
			if(i == 0) _TD.innerHTML = "<b>" + _weeks[j] + "</b>";
			if(i == 0 && (j == 0 || j == 6)) _TD.className = "tdHoliday";
		}
	}
	// 最后一行(today cancel)
	_TR = tableDate.insertRow(2);
	_TD = _TR.insertCell();
	_TD.colSpan = 7;
	_TD.height = 1;
	_TD.bgColor = "black";
	_TR = tableDate.insertRow();
	_TD = _TR.insertCell();
	_TD.colSpan = 7;
	_TD.innerHTML = "<table cellspacing=0 cellpadding=0 class=calendar style='border:0px solid;width:100%'>"
		+ "<tr><td width=160 title=   '转到今天 | Switch to today.'style='cursor:hand'onclick=\"parent._bHaveSelectNewValue=true;parent.setTargetFormaValue(" + defaultDate.getFullYear() + "," + (defaultDate.getMonth()) + "," + defaultDate.getDate() + ");parent.hideCldTabFrm();return;parent.fillCldTabFrm(" + defaultDate.getFullYear() + "," + defaultDate.getMonth() + "," + defaultDate.getDate() +");\">"
		+ "<b>&nbsp;<img src='" + imageToday1.src + "' width=30px> 今 天 :  " + defaultDate.getFullYear() + "-" + (defaultDate.getMonth() + 1) + "-" + defaultDate.getDate()
		+ "</td>"
		+ "<td width=30>"
		+ "<img style='cursor:hand' onclick=parent.resetTargetValue() title='清空日期 | clear the target value.' src='" + imageReset1.src + "' onmouseover=this.src='" + imageReset2.src + "' onmouseout=this.src='" + imageReset1.src + "'></td>"
		+ "<td>&nbsp;&nbsp;</td></tr></table>";
	
}

function InitDropMonth()
{
	strMonth ="	<select valign=\"top\" id=\"dropMonth\" onmouseout='parent.HideMonthDrop();' onchange=\"return parent.ChangeMonth(this);\" style=\"display:none;FONT-SIZE:12px;WIDTH:70px;HEIGHT:20px\" accesskey=\"M\" bordercolor=\"#e0e3f7\">"+
							"<OPTION class='dropItem'  value =\"0\" selected>一月</OPTION>"+
							"<OPTION class='dropItem'  value =\"1\">二月</OPTION>"+
							"<OPTION class='dropItem'  value =\"2\">三月</OPTION>"+
							"<OPTION class='dropItem'  value =\"3\">四月</OPTION>"+
							"<OPTION class='dropItem'  value =\"4\">五月</OPTION>"+
							"<OPTION class='dropItem'  value =\"5\">六月</OPTION>"+
							"<OPTION class='dropItem'  value =\"6\">七月</OPTION>"+
							"<OPTION class='dropItem'  value =\"7\">八月</OPTION>"+
							"<OPTION class='dropItem'  value =\"8\">九月</OPTION>"+
							"<OPTION class='dropItem'  value =\"9\">十月</OPTION>"+
							"<OPTION class='dropItem'  value =\"10\">十一月</OPTION>"+
							"<OPTION class='dropItem'  value =\"11\">十二月</OPTION>"+
						"</select>";
}

function InitDropYear(isFirstTime)
{
	var year = parseInt(_dCurYear);
	if(isFirstTime)
	{
		strYear ="	<select valign=\"top\" id=\"dropYear\" onmouseout='parent.HideYearDrop();' onchange=\"return parent.ChangeYear(this);\" style=\"display:none;FONT-SIZE:12px;WIDTH:70px;HEIGHT:20px\" accesskey=\"M\" bordercolor=\"#e0e3f7\">"
		strYear +="</select>";
	}
	else
	{
		for(var i = year -25;i< year +10;i++)
		{
			var item = mainIFrame.document.createElement("option");
 			mainIFrame.dropYear.options.add(item);
			item.innerText =i+ " 年";
			item.value = i;
			item.className ="dropItem";
			if(year ==i) 
				item.selected =true;
		}	
	}
  }

function fillCldTabFrm(year, month, day){
	var dCurDate = 0;
	var dNextMonthDate = 1;
	var iDateStartRow = 3;
	var _d = new Date(year, month, 1);
	var _day = _d.getDay();
	var _td = null;

	_dCurYear = year;
	_dCurMonth = month;
	_dCurDay = day;

	if (((_dCurYear % 4 == 0) && !(_dCurYear % 100 == 0))
		||(_dCurYear % 400 == 0)) _monthDays[1] = 29;
	else _monthDays[1] = 28;
	
			
	// 初始化头 (年月部份)
	tableDate.rows(0).cells(0).innerHTML
	 = "<table bgcolor=#420042 class=calendar style=\"color:white;font-weight:bolder;border:0px solid;width:100%;height:20px;\" cellspacing=0 cellpadding=0>"
		+ "<tr><td width=10% height=22>"
		+ "&nbsp;<a style='cursor:hand' title='上月份' onclick=\"parent.switchLastMonth()\"><<</a>"
		+ "</td>"
		+ "<td align=right vAlign=middle width=40%>"
		+ strYear
		+ "<span style='cursor:hand' onmouseout=\"this.className='Month_MouseOver'\" onmouseover='parent.SelectYear(this);' id='spanYear' name='spanYear'>"+ _dCurYear + " 年 </span>"
		+ "<map name=mapUpDonwYear><area title='下一年' onclick=parent.switchLastYear() shape=rect coords=0,0,10,5><area title='前一年' onclick=parent.switchNextYear() shape=rect coords=0,5,10,15></map>"		
		+ "<img border=0 align=absMiddle src='" + imageUpDownYear.src + "' style='height:12px;width:10px' usemap=#mapUpDonwYear>"
		+ "</td><td width=40%>"
		+ strMonth 
		+ "&nbsp;&nbsp;&nbsp;&nbsp;<span style='cursor:hand' onmouseout=\"this.className='Month_MouseOut'\" onmouseover='parent.SelectMonth(this);' id='spanMonth' name='spanMonth'>"+ _months[_dCurMonth] + " 月 </span>&nbsp;&nbsp;&nbsp;"
		+ "</td>"
		+ "<td align=right width=10%>"
		+ "<a style='cursor:hand' title='下月份' onclick=\"parent.switchNextMonth()\">>></a>&nbsp"
		+ "</td></tr></table>"
			
	_day = (_day == 0 ? 7 : _day);
	
	for(var i = _day - 1, dlt = 0; i >= 0; i--){
		_td = tableDate.rows(iDateStartRow).cells(i);
		_td.className = "lastMonth";
		_td.title = "";
		_td.name = "LASTMONTH";
		_td.style.border = "0px solid";
		var _nextMonth = _dCurMonth - 1;
		if(_nextMonth < 0) _nextMonth = 11;
		_td.innerText = (_monthDays[_nextMonth] - (dlt++));
	}
	i = _day;
	for(var d = 1, iRow = iDateStartRow; d <= _monthDays[_dCurMonth] || iRow < 9; ){
		for(; i < 7; i++){
			dCurDate = d++;
			_td = tableDate.rows(iRow).cells(i);
			_td.disabled = false;
			_td.className = "normal";
			_td.name = "CURRENTMONTH";
			_td.style.border = "0px solid";
			if(i == 0 || i == 6) _td.className = "tdHoliday";
			if(d - 1 > _monthDays[_dCurMonth]){
				dCurDate = dNextMonthDate++;
				_td.className = "nextMonth";
				_td.name = "NEXTMONTH";
			}
			else if(dCurDate != _dCurDay) _td.overClassName = "Day_MouseOver";
			_td.innerHTML = dCurDate;

			if(dCurDate == _dCurDay && _td.name == "CURRENTMONTH"){
 				_td.className = "tdCurDate";
			}	
			if(dCurDate == defaultDate.getDate() 
				&& _dCurMonth == defaultDate.getMonth() 
				&& _dCurYear == defaultDate.getFullYear()
				&& _td.name == "CURRENTMONTH")
			{
				if(isEmpty || dCurDate != _dCurDay)
				{
					_td.className = "tdToday";
				}
				else
				{
					_td.className = "tdTodayCurDate";
				}
			}	
			if(dCurDate == defaultDate.getDate() && dCurDate == _dCurDay && _td.name == "CURRENTMONTH"){
 			}	
			
		}
		i = 0;
		iRow++;
	}
}

// 定位处理
function Orientation(){
	
	var _rect = objYear.getBoundingClientRect();
	var _bodyWidth = document.body.clientWidth;
	var _bodyHeight = document.body.clientHeight;
 	var _tmp = mainIFrame;
	mainIFrame = exIFrame;

	exIFrame.style.pixelWidth =  iWidth + (ieVer55 ? 0 : _nShadowLength);
	exIFrame.style.pixelHeight = iHeight + (ieVer55 ? 0 : _nShadowLength);	
	
	mainIFrame.style.pixelLeft = _rect.left - 2 + document.body.scrollLeft;
	mainIFrame.style.pixelTop = _rect.bottom - 2 + document.body.scrollTop;
	
	var _cldTabFrmRect = mainIFrame.getBoundingClientRect();

	if(_cldTabFrmRect.right > _bodyWidth){
		mainIFrame.style.pixelLeft -= (_cldTabFrmRect.right - _bodyWidth + _nShadowLength/3);
	}
	
	if(_cldTabFrmRect.bottom > _bodyHeight){
		mainIFrame.style.pixelTop = _rect.top - _nShadowLength/5;
		mainIFrame.style.pixelTop -= (_cldTabFrmRect.bottom - _cldTabFrmRect.top - document.body.scrollTop );
	}
	
	mainIFrame = _tmp;
}

function whenMouseOverCldTabFrm(){
	_bCanHide = false;
}

function whenMouseOutCldTabFrm(){
	_bCanHide = true;
}

function getNextDate(year, month, day){
	if(day == null) day = 1;
	if(day > _monthDays[month + 1]) day = _monthDays[month + 1];
	return new Date(year, month + 1, day);
}

function getLastDate(year, month, day){
	if(day == null) day = 1;
	if(day > _monthDays[month - 1]) day = _monthDays[month - 1];
	return new Date(year, month - 1, day);
}

function switchLastMonth(bLast){
	if(bLast == null) bLast = true;
	var _tmpdate = null;
	// 实际：2003-5-6  此处显示：2003-3-6
	_tmpdate = bLast ?  getLastDate(_dCurYear, _dCurMonth, _dCurDay) : 
						getNextDate(_dCurYear, _dCurMonth, _dCurDay);

	setTargetFormaValue(_tmpdate.getFullYear(), _tmpdate.getMonth(), _tmpdate.getDate());
	_bHaveUpdated = false;
	fillCldTabFrm(_tmpdate.getFullYear(), _tmpdate.getMonth(), _tmpdate.getDate());;
}
function switchNextMonth(){
	switchLastMonth(false);
}
function switchLastYear(){
	setTargetFormaValue(_dCurYear * 1 + 1, _dCurMonth * 1, _dCurDay);
	_bHaveUpdated = false;
	fillCldTabFrm(_dCurYear * 1 + 1, _dCurMonth * 1, _dCurDay);
}
function switchNextYear(){
	setTargetFormaValue(_dCurYear * 1 - 1, _dCurMonth * 1, _dCurDay);
	_bHaveUpdated = false;
	fillCldTabFrm(_dCurYear * 1 - 1, _dCurMonth * 1, _dCurDay);
}

function resetTargetValue(){
	isReSet = true;
	objYear.value  = "";
	objMonth.value = "";
	objDay.value   = "";
	
	if(objHour !=null)
	{
		objHour.value	="";
		objMinute.value	="";
	}
	
	objDate.value  = "";	

	_bHaveSelectNewValue = true;
	hideCldTabFrm();	
}

function setTargetFormaValue(year, month, day,hour,minute){
	var _year, _month, _day,_hour,_minute;
	var _date ;
	if(hour ==null)
		_date =new Date(year,month,day);
	else
		_date =new Date(year,month,day,hour,minute,0);	
	
	_year	= _date.getFullYear();
	_month	= _date.getMonth ()  + 1;
	_hour	= _date.getHours();
	_minute = _date.getMinutes();
	
	if(_month ==0 )		_month =1;
 	//if(_minute ==0 )	_minute =60;
	if(_year<1753)_year=1753;
 	objYear.value  = _year;
	objMonth.value = _month;
	
	//if(pressWhickTextBox!=null && pressWhickTextBox=='day' || pressWhickTextBox=='hour' || pressWhickTextBox=='minute')
	//{
		_day	= _date.getDate();
		objDay.value   = _day;	
	//}
	if(objHour!=null)
	{
		objHour.value   = hour==null?_dCurHour:_hour;
		objMinute.value = minute==null?_dCurMinute:_minute;

		objDate.value = _year + "-"+_month+"-"+_day+" "+objHour.value+":"+objMinute.value;
	}
	else
	{
		objDate.value  = _year + "-" + _month + "-" + _day;	
	}
 }

function whenMouseOverDateItem(){
	var e = mainIFrame.event.srcElement;
	var _tmpdate = null;
	if(e.tagName == "TD"){
		if(e.name == "LASTMONTH"){
			_tmpdate = getLastDate(_dCurYear, _dCurMonth);
			e.title = "上一月 : " + _tmpdate.getFullYear() + "-" + (_tmpdate.getMonth() + 1) + "-" + e.innerText;
			return;
		}
		if(e.name == "NEXTMONTH"){
			_tmpdate = getNextDate(_dCurYear, _dCurMonth);
			e.title = "下一月 : " + _tmpdate.getFullYear() + "-" + (_tmpdate.getMonth() + 1) + "-" + e.innerText;
			return;
		}
	
		var sCurDate = _dCurYear + "-" + (_dCurMonth + 1) + "-" + e.innerText;
		e.title = sCurDate;
		
		if(e.overClassName)
		{
			var temp = e.className;
			e.className = e.overClassName;
			e.overClassName = temp;
		}
		
//		e.style.backgroundColor = "#EFEFEF";///////////////////////////
//		setTargetFormaValue(_dCurYear, _dCurMonth + 1, e.innerText);
	}	
}


function whenMouseOutDateItem(){
	var e = mainIFrame.event.srcElement;
	if(e.tagName == "TD")
	{
		if(e.overClassName)
		{
			var temp = e.className;
			e.className = e.overClassName;
			e.overClassName = temp;
		}
	//	alert(e.className);
	}
	//e.style.backgroundColor = "";
}
function whenClickDateItem(){

	var e = mainIFrame.event.srcElement;
	var _tmpdate = null;
	var _month = null;
	var _day = null;
	if(e.tagName == "TD"){
		_bHaveUpdated = false;
		if(e.name == "LASTMONTH"){
			_tmpdate = getLastDate(_dCurYear, _dCurMonth);
			setTargetFormaValue(_tmpdate.getFullYear(), _tmpdate.getMonth(), e.innerText);
			fillCldTabFrm(_tmpdate.getFullYear(), _tmpdate.getMonth(), e.innerText);
			return;
		}
		if(e.name == "NEXTMONTH"){
			_tmpdate = getNextDate(_dCurYear, _dCurMonth);
			setTargetFormaValue(_tmpdate.getFullYear(), _tmpdate.getMonth(), e.innerText);
			fillCldTabFrm(_tmpdate.getFullYear(), _tmpdate.getMonth(), e.innerText);
			return;
		}
		_bHaveSelectNewValue = true;
		setTargetFormaValue(_dCurYear, _dCurMonth, e.innerText);
		hideCldTabFrm();
	}
}

function hideCldTabFrm(){
 	if(!_bHaveSelectNewValue)
		if(mainIFrame == null || !_bCanHide) return;
	var oFiredObj = null;
	try{
		oFiredObj = event.srcElement;
	}catch(e){
		oFiredObj = mainIFrame.event.srcElement;
	}
	if(oFiredObj == imgCurrent) return;
		
	exIFrame.style.display = "none";
	
	_bHaveUpdated = false;
	_bHaveShown = false;
	removeShadowDiv();
	//if(!_bHaveSelectNewValue)
	//{
	//	var tmpDate = getValidateDate(dateValue);
	//	setTargetFormaValue(tmpDate.getFullYear(),tmpDate.getMonth() ,tmpDate.getDate());
 	//}

 	document.onclick = _docClick;
 }

function removeShadowDiv(){
	try
	{
		var arrShadowDiv = eval("window.document.arr" + tableDate.id);
		for(var i = 0; i < arrShadowDiv.length; i++)
			arrShadowDiv[i].removeNode(true);
	}catch(e){}
}

var isEmpty = false;
function getValidateDate(sDate){
	isEmpty = false;
	if(sDate == null)
	{
		isEmpty = true;
		return new Date();
	}
	var value=sDate.replace(/(\/|-|\s)/gi,":");
	var values = value.split(':');
	var lYear=2002,lMonth=12,lDay=31,lHour=0,lMinute=0;
	var flag=true;
	if(values.length>=3)
	{
		if(!isNaN(parseInt(values[0],10)))lYear=parseInt(values[0],10);
		else flag = false;
		if(!isNaN(parseInt(values[1],10)))lMonth=parseInt(values[1],10);
		else flag=false;
		if(!isNaN(parseInt(values[2],10)))lDay=parseInt(values[2],10);
		else flag=false;
		if(values.length>3)
		{
			if(!isNaN(parseInt(values[3],10)))lHour=parseInt(values[3],10);
			else flag=false;
			if(values.length>4)
			{
				if(!isNaN(parseInt(values[4],10)))lMinute=parseInt(values[4],10);
				else flag=false;
			}
		}
	}
	else
	{
		if(values.length>=2)
		{
			if(!isNaN(parseInt(values[0],10)))lHour=parseInt(values[0],10);
			else flag = false;
			if(!isNaN(parseInt(values[1],10)))lMinute=parseInt(values[1],10);
			else flag=false;
		}
		else flag=false;
	}
	if(flag)return new Date(lYear,lMonth-1,lDay,lHour,lMinute);
	else
	{
		isEmpty = true;
		return new Date();
	}	
}


function createDateBox(sBoxName, sDfltValue){
	var _d = getValidateDate(sDfltValue);
	var _month = _d.getMonth() + 1;
	var _day = _d.getDate();
	if(_month * 1 < 10) _month = "0" + _month;
	if(_day * 1 < 10) _day = "0" + _day;
	sDfltValue = _d.getFullYear() + "-" + _month + "-" + _day;

	var _str = "<INPUT READONLY STYLE='border:1px solid gray;text-align:center;cursor:default;' NAME='" + sBoxName + "' VALUE='" + sDfltValue + "' ONCLICK=JSCalendar(this)>";
	document.write(_str);
}

function MakeDivShadowEffect(divObj, color, nLength)
{
 	var tmpstr = "window.document.arr" + divObj.id + " = new Array();";
	eval(tmpstr);
 	var arrShadowDiv = eval("window.document.arr" + divObj.id);
 	var _rect = divObj.getBoundingClientRect();
	for( i = nLength; i > 0; i --)
	{
		var rect = mainIFrame.document.createElement( "DIV" );
		rect.style.position = "absolute";
		rect.style.left = (divObj.style.posLeft + i ) + "px";
		rect.style.top = (divObj.style.posTop + i ) + "px";
		rect.style.width = divObj.offsetWidth + "px";
		rect.style.height = divObj.offsetHeight + "px";
		rect.style.backgroundColor = color;
		var opacity = 1 - i / (i + 1);
 		rect.style.filter = 'alpha(opacity=' + (100 * opacity) + ')';
		rect.style.zIndex = divObj.style.zIndex - 1;
 		mainIFrame.document.body.insertBefore(rect);
		arrShadowDiv[arrShadowDiv.length] = rect;
 	}	
}

// 年份处理
 
function SelectYear(yearSpan)
{
 	InitDropYear(false);
 	var yearDrop = mainIFrame.dropYear;
	yearSpan.style.display ="none";
	yearDrop.style.display ="inline";
 }

function HideYearDrop()
{
	var yearSpan = mainIFrame.spanYear;
	var yearDrop = mainIFrame.dropYear;
	yearDrop.style.display ="none";
	yearSpan.style.display ="inline";
}


function ChangeYear(yearDrop)
{
	HideYearDrop();
	_dCurYear = yearDrop.value ;
	RefreshDate();
}

// 年份选择处理
function SelectMonth(monthSpan)
{
	var monthDrop = mainIFrame.dropMonth;
	monthSpan.style.display ="none";
	monthDrop.style.display ="inline";
	monthDrop.selectedIndex =_dCurMonth;
 }

function HideMonthDrop()
{
	var monthSpan = mainIFrame.spanMonth;
	var monthDrop = mainIFrame.dropMonth;
	monthDrop.style.display ="none";
	monthSpan.style.display ="inline";
}

function ChangeMonth(monthDrop)
{
	HideMonthDrop();
	_dCurMonth = monthDrop.selectedIndex ;
	RefreshDate();
}

function RefreshDate()
{
  	setTargetFormaValue(_dCurYear, _dCurMonth ,_dCurDay);
	_bHaveUpdated = false;

	fillCldTabFrm(_dCurYear, _dCurMonth ,_dCurDay);
}

function dp_focus(srcType)
{   
	var src=event.srcElement;
	if(src && src.tagName=="INPUT")
	{   
		switch(srcType)
		{
			case 'year':  break;
			case 'month': break;
			case 'day':	  break;
			default:;
		}
		src.select();
	}
}

function dp_keyDown(srcType)
{  
	pressWhickTextBox = srcType;
	var dp=getParentFromSrc(event.srcElement,"SPAN")
	if(dp)
	{
		var bRefresh=true;
	
		var lStep=0;
		switch(event.keyCode)
		{
			case 38:		lStep= 1;
				break;
			case 40:		lStep=-1;
				break;
			case 39:		event.keyCode=9;		// ->
				break;
			case 37:							;   // <-
				break;
			case 13:		event.keyCode=9;
				break;
			default:		bRefresh=false;
		}
		
			objYear	=dp.children[0];
			objMonth=dp.children[2];
			objDay	=dp.children[4];
			objDate =dp.children[11];
			if(objDate )
			{
				objHour =dp.children[6];
				objMinute=dp.children[8];
			}
			else
			{
				objDate=dp.children[7];
				objHour = null;
				objMinute = null;
			}
			
			var lyear =parseInt(objYear.value);
			var lmonth =parseInt(objMonth.value)-1;
			var lday  =parseInt(objDay.value);
			var lhour,lminute;
			if(objHour!=null)
			{
				lhour     =parseInt(objHour.value);
				lminute   =parseInt(objMinute.value);
			}
		if(!isNaN(lyear) && !isNaN(lmonth) && !isNaN(lday))
		{
 			switch(srcType)
			{
				case 'year':		lyear +=lStep;
					break;
				case 'month':		lmonth+=lStep;
					break;
				case 'day':			lday+=lStep;
					break;
				case 'hour':		lhour +=lStep;
					break;
				case 'minute':		lminute+=lStep;
					break;
				default:;
			}
 			if(bRefresh)
 			{
				setTargetFormaValue(lyear,lmonth,lday,lhour,lminute);
				dateValue =objDate.value;
			}
		}
	}
	return true;
}
function dp_change()
{
	var dp=getParentFromSrc(event.srcElement,"SPAN")
	if(dp)
	{
		objYear	=dp.children[0];
		objMonth=dp.children[2];
		objDay	=dp.children[4];
		objDate =dp.children[11];
		if(objDate )
		{
			objHour =dp.children[6];
			objMinute=dp.children[8];
		}
		else
		{
			objDate=dp.children[7];
			objHour = null;
			objMinute = null;
		}
		
		var lyear,lmonth,lday,lhour,lminute;
		var dt = new Date();
	lyear	= dt.getFullYear();
	lmonth	= dt.getMonth ()  + 1;
	lday = dt.getDate();
	lhour	= dt.getHours();
	lminute = dt.getMinutes();
		
		var temp = parseInt(objYear.value);
		if(!isNaN(temp))lyear=temp;
		temp = parseInt(objMonth.value)-1;
		if(!isNaN(temp))lmonth=temp;
		temp = parseInt(objDay.value);
		if(!isNaN(temp))lday=temp;

		if(objHour!=null)
		{
			temp = parseInt(objHour.value);
			if(!isNaN(temp))lhour=temp;
			temp = parseInt(objMinute.value);
			if(!isNaN(temp))lminute=temp;
		}
		setTargetFormaValue(lyear,lmonth,lday,lhour,lminute);
	}

}
function dp_blur()
{
	var src=event.srcElement;
	var dp=getParentFromSrc(src,"SPAN")
	if(dp)
	{
		var srcHidden = dp.children[7];
		if(srcHidden && (typeof(srcHidden.onblur)=="function"))srcHidden.onblur();
		else
		{
			srcHidden = dp.children[11];
			if(srcHidden && (typeof(srcHidden.onblur)=="function"))srcHidden.onblur();
		}
	}
}
function getParentFromSrc(src,parTag)
{
	if(src && src.tagName!=parTag)
		src=getParentFromSrc(src.parentElement,parTag);
	return src;
}
function KeyFilter(type)
{
	var berr=false;
	
	switch(type)
	{
		case 'date':
			if (!(event.keyCode == 45 || event.keyCode == 47 || (event.keyCode>=48 && event.keyCode<=57)))
				berr=true;
			break;
		case 'number':
			if (!(event.keyCode>=48 && event.keyCode<=57))
				berr=true;
			break;
		case 'cy':
			if (!(event.keyCode == 46 || (event.keyCode>=48 && event.keyCode<=57)))
				berr=true;
			break;
		case 'long':
			if (!(event.keyCode == 45 || (event.keyCode>=48 && event.keyCode<=57)))
				berr=true;
			break;
		case 'double':
			if (!(event.keyCode == 45 || event.keyCode == 46 || (event.keyCode>=48 && event.keyCode<=57)))
				berr=true;
			break;
		default:
			if (event.keyCode == 35 || event.keyCode == 37 || event.keyCode==38)
				berr=true;
	}
	return !berr;
}
//-->