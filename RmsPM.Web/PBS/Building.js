function doRoomChoose(str1,str2,str3,str4,str5){
	var obj1=window.opener.document.all(str1);
	var obj2=window.opener.document.all(str2);
	if(obj1){
		obj1.innerHTML=str3;
	}
	if(obj2){
		obj2.value=str4;
	}
	if(str5!=""){
		window.opener.eval(str5);
	}
	WinClose();
}

var nowRoom=null;
function doRoomDetails(id,obj){
alert("")
	if(nowRoom!=null){
		changeBackGroud(nowRoom,"");
	}
	nowRoom=obj;
	changeBackGroud(nowRoom,"../images/bgs.gif");
	document.all("room_details").src="room.asp?room_id="+id;
}

function doRoomSplitX(id){
	if(window.confirm("您确认要进行物业横向拆分吗？")){
		PageTo("action.asp?action=room_splitX&building_id="+id+"&room_id="+room_id+"&room_x="+room_x+"&room_y="+room_y);
	}
}
function doRoomSplitY(id){
	if(window.confirm("您确认要进行物业纵向拆分吗？")){
		PageTo("action.asp?action=room_splitY&building_id="+id+"&room_id="+room_id+"&room_x="+room_x+"&room_y="+room_y);
	}
}
function doRoomUniteX(id){
	if(window.confirm("您确认要进行物业合并吗？")){
		PageTo("action.asp?action=room_uniteX&building_id="+id+"&room_id="+room_id+"&room_x="+room_x+"&room_y="+room_y);
	}
}
function doRoomUniteY(id){
	if(window.confirm("您确认要进行物业合并吗？")){
		PageTo("action.asp?action=room_uniteY&building_id="+id+"&room_id="+room_id+"&room_x="+room_x+"&room_y="+room_y);
	}
}
function doRoomSplit(id){
	document.all("UniteTD").style.display="none";
	document.all("SplitTD").style.display="none";
	document.all("DelTD").style.display="none";
	document.all("NewTD").style.display="none";
	document.all("UniteXTD").style.display="none";
	document.all("UniteYTD").style.display="none";
	document.all("CancelUniteTD").style.display="none";
	document.all("SplitXTD").style.display="none";
	document.all("SplitYTD").style.display="none";
	document.all("CancelSplitTD").style.display="";
	if(parseInt(cs)>1){
		document.all("SplitXTD").style.display="";
	}
	if(parseInt(rs)>1){
		document.all("SplitYTD").style.display="";
	}
}
function doCancelSplit(id){
	document.all("UniteTD").style.display="";
	document.all("SplitTD").style.display="none";
	document.all("DelTD").style.display="";
	document.all("NewTD").style.display="none";
	document.all("UniteXTD").style.display="none";
	document.all("UniteYTD").style.display="none";
	document.all("CancelUniteTD").style.display="none";
	document.all("SplitXTD").style.display="none";
	document.all("SplitYTD").style.display="none";
	document.all("CancelSplitTD").style.display="none";
	if(parseInt(rs)>1||parseInt(cs)>1){
		document.all("SplitTD").style.display="";
	}
}
function doRoomUnite(id){
	document.all("UniteTD").style.display="none";
	document.all("SplitTD").style.display="none";
	document.all("DelTD").style.display="none";
	document.all("NewTD").style.display="none";
	document.all("UniteXTD").style.display="";
	document.all("UniteYTD").style.display="";
	document.all("CancelUniteTD").style.display="";
	document.all("SplitXTD").style.display="none";
	document.all("SplitYTD").style.display="none";
	document.all("CancelSplitTD").style.display="none";
}
function doCancelUnite(id){
	document.all("UniteTD").style.display="";
	document.all("SplitTD").style.display="none";
	document.all("DelTD").style.display="";
	document.all("NewTD").style.display="none";
	document.all("UniteXTD").style.display="none";
	document.all("UniteYTD").style.display="none";
	document.all("CancelUniteTD").style.display="none";
	document.all("SplitXTD").style.display="none";
	document.all("SplitYTD").style.display="none";
	document.all("CancelSplitTD").style.display="none";
	if(parseInt(rs)>1||parseInt(cs)>1){
		document.all("SplitTD").style.display="";
	}
}
function doRoomNew(id){
	if(window.confirm("您确认要在这里新增一个物业吗？")){
		PageTo("action.asp?action=room_new&building_id="+id+"&room_x="+room_x+"&room_y="+room_y);
	}
}
function doRoomDel(id){
	if(window.confirm("您确认要删除这个物业吗？")){
		PageTo("action.asp?action=room_del&building_id="+id+"&room_id="+room_id);
	}
}
document.body.onmousedown=doMenuHidden;
var canHidden=true;
var MenuObj=document.all("MenuDiv");
var rs,cs,room_id,room_x,room_y;
function doCanHiddenTrue(){
	canHidden=true;
}
function doCanHiddenFalse(){
	canHidden=false;
}
function doModifyBuilding(obj){
	if(MenuObj){
		MenuObj.style.left=document.body.scrollLeft+event.clientX-5;
		MenuObj.style.top=document.body.scrollTop+event.clientY-5;
		MenuObj.style.display="";
		room_id=obj.room_id;
		rs=obj.rs;
		cs=obj.cs;
		room_x=obj.x;
		room_y=obj.y;
		if(room_id!=""){
			document.all("UniteTD").style.display="";
			document.all("SplitTD").style.display="none";
			document.all("DelTD").style.display="";
			document.all("NewTD").style.display="none";
			document.all("UniteXTD").style.display="none";
			document.all("UniteYTD").style.display="none";
			document.all("CancelUniteTD").style.display="none";
			document.all("SplitXTD").style.display="none";
			document.all("SplitYTD").style.display="none";
			document.all("CancelSplitTD").style.display="none";
			if(parseInt(rs)>1||parseInt(cs)>1){
				document.all("SplitTD").style.display="";
			}
		}else{
			document.all("UniteTD").style.display="none";
			document.all("SplitTD").style.display="none";
			document.all("DelTD").style.display="none";
			document.all("NewTD").style.display="";
			document.all("UniteXTD").style.display="none";
			document.all("UniteYTD").style.display="none";
			document.all("CancelUniteTD").style.display="none";
			document.all("SplitXTD").style.display="none";
			document.all("SplitYTD").style.display="none";
			document.all("CancelSplitTD").style.display="none";
		}
	}
}
function doMenuHidden(){
	if(canHidden&&MenuObj){
		MenuObj.style.display="none";
		room_id="";
		rs="";
		cs="";
		room_x="";
		room_y="";
	}
}
function doRoomDimChange0(obj){
	if(isNumerics(obj.value)){
		var objs,x;
		x=obj.x.toString();
		for(var i=1;i<parseInt(obj.floor_count)+1;i++){
			objs=document.all("room_dim_"+i+"_"+x);
			if(objs){
				objs.value=obj.value;
			}
		}
	}else{
		alert("请输入数字！");
		obj.focus();
	}
}
function doRoomDimChange1(obj){
	if(isNumerics(obj.value)){
		var objs,y;
		y=obj.y.toString();
		for(var i=1;i<parseInt(obj.room_count)+1;i++){
			objs=document.all("room_dim_"+y+"_"+i);
			if(objs){
				objs.value=obj.value;
			}
		}
	}else{
		alert("请输入数字！");
		obj.focus();
	}
}
function doBuildingDimChange0(obj){
	if(isNumerics(obj.value)){
		var objs,x;
		x=obj.x.toString();
		for(var i=1;i<parseInt(obj.floor_count)+1;i++){
			objs=document.all("building_dim_"+i+"_"+x);
			if(objs){
				objs.value=obj.value;
			}
		}
	}else{
		alert("请输入数字！");
		obj.focus();
	}
}
function doBuildingDimChange1(obj){
	if(isNumerics(obj.value)){
		var objs,y;
		y=obj.y.toString();
		for(var i=1;i<parseInt(obj.room_count)+1;i++){
			objs=document.all("building_dim_"+y+"_"+i);
			if(objs){
				objs.value=obj.value;
			}
		}
	}else{
		alert("请输入数字！");
		obj.focus();
	}
}
function doBuildingDimChanges(obj){
	if(!isNumerics(obj.value)){
		obj.value=0;
	}
}
function doRoomDimChanges(obj){
	if(!isNumerics(obj.value)){
		obj.value=0;
	}
}
function doRoomModelChange(obj){
	var objs,x;
	x=obj.x.toString();
	for(var i=1;i<parseInt(obj.floor_count)+1;i++){
		objs=document.all("room_model_"+i+"_"+x);
		if(objs){
			objs.options[obj.selectedIndex].selected=true;
		}
	}
}
function doWGFChange0(obj){
	var objs,x;
	x=obj.x.toString();
	for(var i=1;i<parseInt(obj.floor_count)+1;i++){
		objs=document.all("wy_price_type_"+i+"_"+x);
		if(objs){
			objs.options[obj.selectedIndex].selected=true;
		}
	}
}
function doWGFChange1(obj){
	var objs,y;
	y=obj.y.toString();
	for(var i=1;i<parseInt(obj.floor_count)+1;i++){
		objs=document.all("wy_price_type_"+y+"_"+i);
		if(objs){
			objs.options[obj.selectedIndex].selected=true;
		}
	}
}
function doFloorModelChange(obj){
	var objs,y;
	y=obj.y.toString();
	for(var i=1;i<parseInt(obj.room_count)+1;i++){
		objs=document.all("room_model_"+y+"_"+i);
		if(objs){
			objs.options[obj.selectedIndex].selected=true;
		}
	}
}
function doBaseSubmit(obj,floor_count){
	document.all("building_modify_submit").disabled=true;
	var i;
	var room_count=parseInt(document.all("room_counts").value);
	var itemArray=new Array(1+floor_count);
	itemArray[0]=new Array("building_name",0,"text","请输入楼栋名称！");
	for(i=0;i<floor_count;i++){
		itemArray[i+1]=new Array("floor_list",i,"text","请输入楼层名称！");
	}
	for(i=0;i<room_count;i++){
		itemArray[i+floor_count+1]=new Array("room_list",i,"text","请输入室号！");
	}
	if(chkForm(itemArray)){
		obj.submit();
	}else{
		document.all("building_modify_submit").disabled=false;
	}
}
function doBaseRoomNameChange(obj){
	var objs,objy,x,y,j;
	objy=document.all("floor_list");
	x=obj.x.toString();
	val=obj.value.toString();
	y="";
	for(var i=1;i<parseInt(obj.floor_count)+1;i++){
		if(objy[0]){
			for(j=0;j<objy.length;j++){
				if(objy[j].y.toString()==i.toString()){
					y=objy[j].value;
					break;
				}else{
					y="";
				}
			}
		}else{
			if(objy.y.toString()==i.toString()){
				y=objy.value
			}else{
				y=""
			}
		}
		objs=document.all("room_name_"+i+"_"+x);
		if(objs){
			objs.value=y.toString()+""+val.toString();
		}
	}
}
function doBaseFloorNameChange(obj){
	var objs,objx,y,x,j;
	objx=document.all("room_list");
	y=obj.y.toString();
	val=obj.value.toString();
	x="";
	for(var i=1;i<parseInt(obj.room_count)+1;i++){
		if(objx[0]){
			for(j=0;j<objx.length;j++){
				if(objx[j].x.toString()==i.toString()){
					x=objx[j].value;
					break;
				}else{
					x="";
				}
			}
		}else{
			if(objy.x.toString()==i.toString()){
				x=objx.value
			}else{
				x=""
			}
		}
		objs=document.all("room_name_"+y+"_"+i);
		if(objs){
			objs.value=val.toString()+""+x.toString();
		}
	}
}
function doBuildingModifyCancel(id){
	document.all("Cancel_Button").disabled=true;
	PageTo("building.asp?building_id="+id);
}
var roomDiv=document.all("room_info");
function document.body.onscroll(){
	if(document.all("room_info")){
		if(document.body.scrollTop<20){
			roomDiv.style.pixelTop=document.body.scrollTop+(20-document.body.scrollTop);
		}else{
			roomDiv.style.pixelTop=document.body.scrollTop;
		}
	}
}

function changeBgColor(obj,color){
	obj.style.backgroundColor=color;
}
function changeBorderColor(obj,color){
	obj.style.borderColor=color;
}
function changeBackGroud(obj,str){
	obj.background=str;
}