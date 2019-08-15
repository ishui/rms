function doAddBuildingCancel(projectCode){
	PageTo("Building_l.asp?ProjectCode="+projectCode);
}

//graph: click building, goto building info
function doMapBuilding(id, FromUrl){
//	window.location.href = "PBSBuildInfo.aspx?BuildingCode="+id + "&FromUrl=" + escape(FromUrl);
	PageTo("BuildingPart.aspx?BuildingCode="+id + "&FromUrl=" + escape(FromUrl));
}
function doMapArea(projectCode,parentCode){
    
	PageTo("Building_l.aspx?ProjectCode="+projectCode+"&ParentCode="+parentCode);
}

function doChooseBuilding(id,str1,str2,str5){
	PageTo("choose_room.asp?BuildingCode="+id+"&str1="+str1+"&str2="+str2+"&str5="+str5);
//	WinOpen_Max("building.asp?building_id="+id,"BuildingWin",1,0,0,1);
}
function doChooseArea(id,str1,str2,str5){
	PageTo("choose_building.asp?area_id="+id+"&str1="+str1+"&str2="+str2+"&str5="+str5);
}

function doUploadMap(projectCode,parentCode){
	OpenCustomWindow("UploadImg.aspx?action=up_group_map&ProjectCode="+projectCode+"&ParentCode="+parentCode, "MapUploadWin", 400, 160);
//	WinOpen("UploadImg.aspx?action=up_group_map&ProjectCode="+projectCode+"&ParentCode="+parentCode,"MapUploadWin",300,150,0,0,0,0);
}

//create building
function doCreateBuilding(projectCode,parentCode){
	OpenCustomWindow("PBSBuildModify.aspx?Action=Insert&ParentCode="+parentCode+"&IsArea=2&ProjectCode=" + projectCode, "CreateAreaWin" , 760, 540);
//	PageTo("Building_Step1.aspx?ProjectCode="+projectCode+"&ParentCode="+parentCode+"&Action=Insert&IsArea=2");
//	WinOpen_Max("create_building_1.asp?area_id="+area_id+"&group_id="+id,"CreateBuildingWin",0,0,0,1);
}

//create building wizard
function doCreateBuildingWizard(projectCode,parentCode){
	PageTo("Building_Step1.aspx?ProjectCode="+projectCode+"&ParentCode="+parentCode+"&Action=Insert&IsArea=2");
//	WinOpen_Max("create_building_1.asp?area_id="+area_id+"&group_id="+id,"CreateBuildingWin",0,0,0,1);
}

function doModifyBuildingWizard(BuildingCode){
	PageTo("Building_Step1.aspx?BuildingCode="+BuildingCode+"&Action=Modify");
//	WinOpen_Max("create_building_1.asp?area_id="+area_id+"&group_id="+id,"CreateBuildingWin",0,0,0,1);
}

//modify building
function doModifyBuilding(buildingCode,projectCode){
	OpenCustomWindow("PBSBuildModify.aspx?Action=Modify&BuildingCode=" + buildingCode + "&IsArea=2&ProjectCode=" + projectCode, "CreateAreaWin" , 700, 540);
}

//create area
function doCreateArea(projectCode,parentCode){
	OpenCustomWindow("PBSBuildModify.aspx?Action=Insert&ParentCode="+parentCode+"&IsArea=1&ProjectCode=" + projectCode, "CreateAreaWin" , 700, 540);
//	WinOpen("CreatArea.aspx?ProjectCode="+projectCode+"&ParentCode="+parentCode,"CreateAreaWin",300,100,0,0,0,0);
}

//modify area
function doModifyArea(buildingCode,projectCode){
	OpenCustomWindow("PBSBuildModify.aspx?Action=Modify&BuildingCode=" + buildingCode + "&IsArea=1&ProjectCode=" + projectCode, "CreateAreaWin" , 700, 540);
}

//change location
function doChangeLocation(projectCode,parentCode){
	PageTo("Building_Location.aspx?ProjectCode="+projectCode+"&ParentCode="+parentCode);
	
}

//goto building list
function GotoBuildingList(projectCode) {
	window.location.href = "../PBS/PBSBuildingTree.aspx?ProjectCode=" + projectCode;
}

//goto building area graph
function GotoBuildingGraph(projectCode, buildingCode) {
	window.location.href = "../PBS/Building_l.aspx?ProjectCode=" + Form1.txtProjectCode.value + "&ParentCode=" + buildingCode;
}

//goto building info
function GotoBuildingInfo(BuildingCode) {
	window.location.href = "../PBS/PBSBuildInfo.aspx?BuildingCode=" + BuildingCode;
}
	
//open window of building info
function OpenBuildingInfo(BuildingCode) {
	OpenCustomWindow("../PBS/PBSBuildInfo.aspx?BuildingCode=" + BuildingCode + "&OpenModal=open&action=view", "BuildingInfo", 700, 540);
}
	
//goto building part
function GotoBuildingPart(BuildingCode) {
	window.location.href = "../PBS/BuildingPart.aspx?BuildingCode=" + BuildingCode;
}

//modify building structure	
function doModifyBuildingStructure(BuildingCode) {
	window.location.href = "../PBS/BuildingStructureModify.aspx?BuildingCode=" + BuildingCode + "&action=structure";
}

//modify room name
function doModifyRoomName(BuildingCode) {
	window.location.href = "../PBS/BuildingStructureModify.aspx?BuildingCode=" + BuildingCode + "&action=base";
}

//modify room model
function doModifyRoomModel(BuildingCode) {
	window.location.href = "../PBS/BuildingStructureModify.aspx?BuildingCode=" + BuildingCode + "&action=room_model";
}

//modify room area
function doModifyRoomArea(BuildingCode) {
	OpenCustomWindow("../PBS/RoomModifyArea.aspx?BuildingCode=" + BuildingCode + "&action=building", "ModifyArea" , 760, 540);
}

function doSaveLocation(projectCode,parentCode){
	var obj=document.all("DivBuilding");
	
	var str="";
	if(obj&&obj[0]){
		for(var i=0;i<obj.length;i++){
			str+=""+obj[i].building_type+"|"+obj[i].building_id+"|"+obj[i].style.pixelLeft.toString()+"|"+obj[i].style.pixelTop.toString()+"$";
		}
	}else{
		if(obj){
			str=""+obj.building_type+"|"+obj.building_id+"|"+obj.style.pixelLeft.toString()+"|"+obj.style.pixelTop.toString()+"$";
		}
	}
	
	PageTo("LocationSava.aspx?ProjectCode="+projectCode+"&ParentCode="+parentCode+"&str="+str);
}


function doSaveMap(obj){
	document.all("map_save_submit").disabled=true;
	var itemArray=new Array(4);
	itemArray[0]=new Array("map_width",0,"text","请输入图片宽度！");
	itemArray[1]=new Array("map_width",0,"numeric","请正确输入图片宽度！");
	itemArray[2]=new Array("map_high",0,"text","请输入图片高度！");
	itemArray[3]=new Array("map_high",0,"numeric","请正确输入图片宽度！");
	if(chkForm(itemArray)){
		obj.action=obj.action+"&width="+document.all("map_width").value+"&high="+document.all("map_high").value;
		obj.submit();
	}else{
		document.all("map_save_submit").disabled=false;
	}
}
function doCreateAreaSubmit(obj){
	document.all("create_area_submit").disabled=true;
	
	var itemArray=new Array(1);
	itemArray[0]=new Array("building_name",0,"text","请输入区域名称！");
	if(chkForm(itemArray)){
		obj.submit();
	}else{
		document.all("create_area_submit").disabled=false;
	}
}

/*
function doCreateBuilding1Submit(obj){

	document.all("create_building_1_submit").disabled=true;

	var itemArray=new Array(5);
	itemArray[0]=new Array("building_name",0,"text","请输入楼栋名称！");
	itemArray[1]=new Array("building_type",0,"select","请输入楼栋类型！");
	itemArray[2]=new Array("building_code",0,"text","请输入楼栋编号（或简称）！");
	itemArray[3]=new Array("floor_count",0,"text","请输入总层数！");
	itemArray[4]=new Array("floor_count",0,"numeric","请正确输入总层数！");

	if(chkForm(itemArray)){
		obj.submit();
	}else{
		document.all("create_building_1_submit").disabled=false;
	}
}
function doCreateBuilding2Submit(count,obj){
	document.all("create_building_2_submit").disabled=true;
	var itemArray=new Array(count+2);
	for(var i=0;i<count;i++){
		itemArray[i]=new Array("floor_list",i,"text","请输入楼层名称！");
	}
	itemArray[count]=new Array("door_count",0,"text","请输入楼梯数（门牌数）！");
	itemArray[count+1]=new Array("door_count",0,"numeric","请正确输入楼梯数（门牌数）！");
	if(chkForm(itemArray)){
		obj.submit();
	}else{
		document.all("create_building_2_submit").disabled=false;
	}
}
function doCreateBuilding3Submit(count,obj){
	document.all("create_building_3_submit").disabled=true;
	var itemArray=new Array(count*4);
	for(var i=0;i<count;i++){
		itemArray[i]=new Array("room_count",i,"text","请输入每梯的户数！");
		itemArray[i+count]=new Array("room_count",i,"numeric","请正确输入每梯的户数！");
		itemArray[i+count*2]=new Array("door_name",i,"text","请输入门牌号！");
		itemArray[i+count*3]=new Array("door_code",i,"text","请输入单元编号！");
	}
	if(chkForm(itemArray)){
		obj.submit();
	}else{
		document.all("create_building_3_submit").disabled=false;
	}
}
function doCreateBuilding4Submit(count,obj){
	document.all("create_building_4_submit").disabled=true;
	var itemArray=new Array(count*2);
	for(var i=0;i<count;i++){
		itemArray[i]=new Array("room_name",i,"text","请输入室号！");
		itemArray[i+count]=new Array("room_name",i,"numeric","请正确输入室号！");
	}
	if(chkForm(itemArray)){
		obj.submit();
	}else{
		document.all("create_building_4_submit").disabled=false;
	}
}
function doCreateBuilding5Submit(obj){
	document.all("create_building_5_submit").disabled=true;
	obj.submit();
}
*/

function doChangeModelCreate(str,obj){

	var tempObj;
	for(var i=1;;i++){
		tempObj=document.all("room_model_"+str+"_"+i);
		if(tempObj){
			tempObj.options[obj.selectedIndex].selected=true;
		}else{
			break;
		}
	}
}

function doMoves(obj, divMain){
	canMove=!canMove;
	moveObj=obj;
	moveDiv = divMain;
}

var MarginX = 5;
var MarginY = 5;

function goMoves(){
	if(canMove){
		var obj0=document.body;
		
		if (moveDiv)
		{
			moveObj.style.left=obj0.scrollLeft + moveDiv.scrollLeft + event.clientX - MarginX;
			moveObj.style.top=obj0.scrollTop + moveDiv.scrollTop + event.clientY - MarginY;
		}
		else
		{
			moveObj.style.left=obj0.scrollLeft + event.clientX - MarginX;
			moveObj.style.top=obj0.scrollTop + event.clientY - MarginY;
		}
	}
}

var canHidden=true;
function doCanHiddenTrue(){
	canHidden=true;
}
function doCanHiddenFalse(){
	canHidden=false;
}
function doMenu(){
	var obj0=document.body;
	var obj=document.all("RightMenu");
	
	//xyq 2004.8.20 add: if
	if (obj) {
		obj.style.left=obj0.scrollLeft+event.clientX-5;
		obj.style.top=obj0.scrollTop+event.clientY-5-30;
		obj.style.display="";
	}
	return false; 
}
function doMenuHidden(){
	if(canHidden){
		var obj=document.all("RightMenu");
		
		//xyq 2004.8.20 add: if
		if (obj) {
			obj.style.display="none";
		}
	}
}
function changeClass(obj,className){
	obj.className = className;
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

if(document.all("group_map")){
	document.all("group_map").oncontextmenu=doMenu;
	
	document.body.onmousedown=doMenuHidden;
	document.all("group_map").onmousemove=goMoves;
}

var moveObj;
var canMove=false;


	function WinOpen(url,name,width,height,resize,status,menubar,scrollbars){
		window.open(url,name,'width='+width+',height='+height+',resizable='+resize+',status='+status+',menubar='+menubar+',scrollbars='+scrollbars+'');
	}
	
	function PageTo(url){
		window.location.href=url;
	}
	function chkForm(items){
	var tempObj=null;
	var chkWhat="";
	var errMsg="";
	var isOk=true;
	for(var i=0;i<items.length;i++){
		chkWhat=items[i][2];
		if(items[i][1]==0){
			if(document.all(items[i][0])[0]&&chkWhat!="select"){
				tempObj=document.all(items[i][0])[0];
			}else{
				tempObj=document.all(items[i][0]);
			}
		}else{
			tempObj=document.all(items[i][0])[items[i][1]];
		}
		if(isOk&&chkWhat=="text"){
			if(!chkTextInput(tempObj)){
				isOk=false;
				errMsg=items[i][3];
				break;
			}
		}
		if(isOk&&chkWhat=="hidden"){
			if(!chkTextInput(tempObj)){
				isOk=false;
				errMsg=items[i][3];
				break;
			}
		}
		if(isOk&&chkWhat=="select"){
			if(!chkSelect(tempObj)){
				isOk=false;
				errMsg=items[i][3];
				break;
			}
		}
		if(isOk&&chkWhat=="selects"){
			if(!chkSelect(tempObj)){
				isOk=false;
				errMsg=items[i][3];
				break;
			}
		}
		if(isOk&&chkWhat=="numeric"){
			if(!chkNumeric(tempObj)){
				isOk=false;
				errMsg=items[i][3];
				break;
			}
		}
	}
	if(!isOk){
		alert(errMsg);
		if(chkWhat=="hidden"){
		}else{
			tempObj.focus();
		}
	}
	return isOk;
}

function chkTextInput(obj){
	if(obj.value==""){
		return false;
	}else{
		return true;
	}
}
function chkSelect(obj){
	if(obj.options[obj.selectedIndex].value==""){
		return false;
	}else{
		return true;
	}
}
function chkNumeric(obj){
	if(!isNumerics(obj.value)){
		return false;
	}else{
		return true;
	}
}
	
