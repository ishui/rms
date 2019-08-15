function OpenSmallWindow(strUrl,strName){
	return window.open(strUrl,strName,"width=400,height=300,fullscreen=0,top="+(window.screen.height-300)/2+",left="+(window.screen.width-400)/2+",menubar=no,toolbar=no,scrollbars=yes,status=no,titlebar=no,resizable=yes,location=no");
}
function OpenMiddleWindow(strUrl,strName){
	return window.open(strUrl,strName,"width=640,height=480,fullscreen=0,top="+(window.screen.height-480)/2+",left="+(window.screen.width-640)/2+",menubar=no,toolbar=no,scrollbars=yes,status=no,titlebar=no,resizable=yes,location=no");
}
function OpenLargeWindow(strUrl,strName){
	return window.open(strUrl,strName,"width=800,height=600,fullscreen=0,top="+(window.screen.height-600)/2+",left="+(window.screen.width-800)/2+",menubar=no,toolbar=no,scrollbars=yes,status=no,titlebar=no,resizable=yes,location=no");
}
function OpenFullWindow(strUrl,strName){
	var Wins=window.open(strUrl,strName,"width="+window.screen.width+",height="+(window.screen.height-55)+",menubar=no,toolbar=no,scrollbars=yes,status=no,titlebar=no,resizable=yes,location=no");
	Wins.moveTo(-3,0);
	return Wins;
}
function OpenCustomWindow(strUrl,strName,iWidth,iHeight){
	return window.open(strUrl,strName,"width="+iWidth+",height="+iHeight+",fullscreen=0,top="+(window.screen.height-iHeight)/2+",left="+(window.screen.width-iWidth)/2+",menubar=no,toolbar=no,scrollbars=yes,status=no,titlebar=no,resizable=yes,location=no");
}
function OpenPrintWindow(strUrl,strName){
	var Wins=window.open(strUrl,strName,"width="+window.screen.width+",height="+(window.screen.height-100)+",menubar=yes,toolbar=no,scrollbars=yes,status=no,titlebar=no,resizable=yes,location=no");
	Wins.moveTo(-3,0);
	return Wins;
}
function OpenNormalWindow(strUrl,strName)
{
  var Wins=window.open(strUrl,strName,"width="+window.screen.width+",height="+(window.screen.height-55));
	Wins.moveTo(3,3);
	return Wins;
}

function OpenCustomDialog(strUrl, strName, iWidth, iHeight)
{
	return window.showModalDialog(strUrl, "", "dialogWidth=" + iWidth + ";dialogHeight=" + iHeight + ";status=no;help=no");
}

function HasString( searchString , targetString ){
	var retemp = new RegExp( targetString,"i");
	return retemp.test(searchString);
}

// 鼎耀软件
function AccessRange( relationCode, accessRangeType, name ,unitFullName)
{
	this.relationCode=relationCode;
	this.accessRangeType=accessRangeType;
	this.name = name;
	this.unitFullName = unitFullName
}
function divdisplay(){
	var sInsert="<div id='divShowWaitingMsg' style='position:absolute; display:inline; left:0px; top:0px; width:100%; height:100%; z-index:1000;BACKGROUND-COLOR: white;FILTER: Alpha(Opacity=100);'>";
		sInsert+="<iframe id=frmShowWaitingMsg  width='100%' height='100%' border=0 >"
		sInsert1="<head><meta http-equiv='content-type' content='text/html; charset=utf-8'></head><body>"
		sInsert1+="<style type=text/css>TD{COLOR: #000031; FONT-SIZE: 12px}</style>"
		sInsert1+="<table width='100%' height='100%'>";
		sInsert1+="<tr>";
		sInsert1+="<td>";
  	sInsert1+="<table height=50 width=200 bgcolor=#E1E5F4 border=1 cellpadding=0 cellspacing=0 align='center'  bordercolorlight='#B5BCE8' bordercolordark='#666666'  style='border-collapse: collapse'>";
       	sInsert1+="  <tr>";
		sInsert1+="           <td nowrap align='center'>数据载入中, 请稍候.... </td>";
       	sInsert1+="  </tr>";
       	sInsert1+="</table>";
		sInsert1+="</td>";
		sInsert1+="</tr>";
		sInsert1+="</table>";
		sInsert1+="</body>";
		sInsert+="</iframe>";
		sInsert+="</div>";
		try{document.body.insertAdjacentHTML ("AfterBegin", sInsert);}catch(e){}
		try{frmShowWaitingMsg.document.write (sInsert1);}catch(e){}
}
function DivManage(DivName)
{
    if(document.all(DivName).style.display == "none")
    {
        document.all(DivName).style.display = "block";
    }
    else
    {
        document.all(DivName).style.display = "none";
    }
}

//window.attachEvent('onbeforeunload',divdisplay);
if (window.attachEvent) {
    window.attachEvent('onbeforeunload', divdisplay);
} else if (window.addEventListener) {
    window.addEventListener("onbeforeunload", divdisplay, false);
}       

// Start TextArea Rows Control	--------------------------------------------------------------------------------------
	function WriteTextAreaControl(objname,IsControlRows){
		if( document.all(objname) ){
			if(IsControlRows){
				document.all(objname).rows=document.all(objname).value.split("\n").length+1;
			}
			document.write('<input onclick="RowsAdd(document.all(\''+objname+'\'));" type="button" value="+">&nbsp;<input onclick="RowsReduce(document.all(\''+objname+'\'));" type="button" value="-">');
		}
	}
	function RowsAdd(obj){
		obj.rows+=2;
	}
	function RowsReduce(obj){
		if( 2<obj.rows ){
			obj.rows-=2;
		}		
	}
	function OpenMyStaionSetWindow(str)
	{
		OpenMiddleWindow(str,"岗位设置");
				
	}
	function IsValidator(id,error)
	{
		if(document.all(id).value =="")
		{
			document.all(id+"Er").innerHTML="<FONT color='red'>"+error+"</FONT>";
		}
		else
            document.all(id + "Er").innerHTML = "<FONT color='red'>*</FONT>";
    }
// End TextArea Rows Control	--------------------------------------------------------------------------------------



function GetObjectInDataGrid( dgName,i, ctlName)
{
    var Name1, Name2;
    
    Name1 = dgName + "__" + "ctl" + i + "_" + ctlName ;
    if ( i >= 10 )
    {
        Name2 = dgName + "_" + "ctl" + i + "_" + ctlName ;
    }
    else
    {
        Name2 = dgName + "_" + "ctl0" + i + "_" + ctlName ;
    }
    
    if ( document.all(Name1) )
    {
//        alert(Name1);
        return document.all(Name1);
    }
    
    if ( document.all(Name2) )
    {
//        alert(Name2);
        return document.all(Name2);
    }
    
    alert(Name1);
    return document.all(Name1);
}

function GetObjectNameInDataGrid( dgName,i, ctlName)
{
    var Name1, Name2;
    
    Name1 = dgName + "__" + "ctl" + i + "_" + ctlName ;
    if ( i >= 10 )
    {
        Name2 = dgName + "_" + "ctl" + i + "_" + ctlName ;
    }
    else
    {
        Name2 = dgName + "_" + "ctl0" + i + "_" + ctlName ;
    }
    
    if ( document.all(Name1) )
    {
//        alert(Name1);
        return Name1;
    }
    
    if ( document.all(Name2) )
    {
//        alert(Name2);
        return Name2;
    }
    
    return Name1;
}

function GetObjectNameInControl( ctlName, innerctlName)
{

    var ObjectName = ctlName + "_" + innerctlName;

    if ( !document.all(ObjectName) )
    {
        alert( ObjectName + " 不存在！");
    }
    
    return ObjectName;
}


function GetObjectByName( cltName )
{
    if ( !document.all(cltName) )
    {
        alert( cltName + " 不存在！");
    }
    
    return document.all(cltName);
}

function GetObjectInControl( ctlName, innerctlName)
{

    var Object = GetObjectByName(GetObjectNameInControl(ctlName,innerctlName));
    
    return Object;
}

function GetOpinionName(value)
{
    if(value=="Approve")
    {
        return "同意";
    }
    else if(value=="Reject")
    {
        return "否决";
    }
    else
    {
        return "待选择";
    }
    
}
function PrintList(contentID)
{
    OpenPrintWindow("../Report/PrintList.aspx?FromControlID="+contentID+"&css=" + escape("../CostBudget/CostBudget.css"), "打印");

}
