ns4 = (document.layers)? true:false;
ie4 = (document.all)? true:false;
var setUnvisible=" ";
var setVisible=" ";
var x=0,y=0,xstep=0,ystep=0;

var objJoyBox;
var objTable;
var offsetParent;
var myOffsetTop;
var myOffsetRight;
var myOffsetBottom;

var divMain;

function init(obj, objTab, objTitle, s){
	objJoyBox = obj;
	objTable = objTab;

	if (obj.myDiv)
	{
		divMain = document.all(obj.myDiv);
	}
	else
	{
		divMain = document.body;
	}

	if (obj.myOffsetTop)
	{
		myOffsetTop = parseInt(obj.myOffsetTop);
	}
	else
	{
		myOffsetTop = 0;
	}

	if (obj.myOffsetRight)
	{
		myOffsetRight = parseInt(obj.myOffsetRight);
	}
	else
	{
		myOffsetRight = 30;
	}

	if (obj.myOffsetBottom)
	{
		myOffsetBottom = parseInt(obj.myOffsetBottom);
	}
	else
	{
		myOffsetBottom = 30;
	}

//	myOffsetTop = obj.offsetTop;

    objTitle.innerHTML = s
    
    if (s != "")
    {
		obj.style.display = ""
	}
    
	if (ns4) {
		joy = obj
		joy.xpos = joy.left
		joy.ypos = joy.top
	}
	if (ie4) {
		joy = obj.style
		joy.xpos = joy.pixelLeft
		joy.ypos = joy.pixelTop		
	}
	
	if ((obj) && (obj.offsetParent))
	{
		offsetParent = obj.offsetParent;
	}
	else
	{
		offsetParent = "";
	}

	mouseOver()
}

function mouseOver(e) {
	if (ns4) {x=e.pageX+5; y=e.pageY}
	if (ie4) {x=window.event.x+5;y=window.event.y}
	
	if (offsetParent)
	{
//		x = x + offsetParent.offsetLeft;
//		y = y + offsetParent.offsetTop;
	}
	
//	alert(window.event.x);
//	alert(window.event.y);
	moveTo(joy, x, y)
}

function moveTo(obj,x,y) {
	obj.xpos = x
//	obj.left = obj.xpos
	obj.ypos = y
//	obj.top = obj.ypos

//	obj.top = obj.ypos + divMain.scrollTop;
	obj.top = obj.ypos + divMain.scrollTop - myOffsetTop;

	if (parseInt(obj.top) + parseInt(objTable.height) > divMain.offsetHeight + divMain.scrollTop)
	{
		obj.top = divMain.offsetHeight + divMain.scrollTop - objTable.height - myOffsetBottom
//		obj.top = y + divMain.scrollTop - objTable.height - myOffsetBottom
	}
	
//alert("x:" + x + "\n" + "table width:" + parseInt(objTable.width));
//alert(document.body.offsetWidth);
	if (x + parseInt(objTable.width) > document.body.offsetWidth)
	{
		obj.left = document.body.offsetWidth - objTable.width - myOffsetRight
//		obj.left = x + document.body.scrollLeft - objTable.width
	}
	else
	{
		obj.left = obj.xpos + document.body.scrollLeft
	}
}

function mouseend(){
	objJoyBox.style.display="none"
}

/*
function  window_onscroll(){
	if (document.all('legend')){
		legend.style.top= basetb.offsetTop +document.body.scrollTop+30;
		if (basetb.offsetLeft+ document.body.scrollLeft-80>=0)
			legend.style.left= basetb.offsetLeft+ document.body.scrollLeft-150 
		else
			legend.style.left= 0;
		
	}
}
*/
//window.onresize =window_onscroll;
//window.onload = window_onscroll;
//window.onscroll = window_onscroll;
