function TopPageTo(url){
	document.all("TreeSplitTop").src=url;
}
function TopReload(){
	GetTopFrame().location.reload();
}
function BottomPageTo(url){
	document.all("TreeSplitBottom").src=url;
}
function GetTopFrame(){
	return eval("TreeSplitTop");
}
function GetBottomFrame(){
	return eval("TreeSplitBottom");
}
function HiddenBottom(){
	var obj=document.all("TreeSplitBottom").parentNode.parentNode;
	obj.style.display="none";
}
function ShowBottom(){
	var obj=document.all("TreeSplitBottom").parentNode.parentNode;
	obj.style.display="";
}
function updateChildNodes(id){
	GetTopFrame().updateChildNodes(id);
	HiddenBottom();
}
function updateNode(id){
	GetTopFrame().updateNode(id);
	HiddenBottom();
}