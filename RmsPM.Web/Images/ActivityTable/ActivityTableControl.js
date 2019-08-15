
// Start Table Control ----------------------------------------------------------------------------------------------------------

var TableObj = null;
var curRow	= null;
var curCol	= null;
var sortCol	= null;

function setDefault(){
	for(i=0;i<TableObj.rows.length;i++){
		for(j=0;j<TableObj.rows[i].cells.length;j++){
			if( 0!=i && 0!=j ){
				//var myi=( (i-1)*(TableObj.rows[i].cells.length-1) )+j;
			}
		}
	}
}

function ClearStyle(){
	if(null!=curCol){
		for(i=1;i<TableObj.rows.length;i++){
			with(TableObj.rows[i].cells[curCol]){
				if(0==curCol){
					className="list-c";
				}else{
					className="";
				}
			}
		}
	}
	if(null!=curRow){
		for(i=1;i<TableObj.rows[curRow].cells.length;i++){
			with(TableObj.rows[curRow].cells[i]){
				if(0==i){
					className="list-c";
				}else{
					className="";
				}
			}
		}
	}
}

function MyGetElement(eleObj,theTag){
	theTag = theTag.toLowerCase();
	if(theTag==eleObj.tagName.toLowerCase()){return eleObj;}
	while(eleObj=eleObj.offsetParent){
		if(theTag==eleObj.tagName.toLowerCase()){return eleObj;}
	}
	return(null);
}

function ChangeRow(line1,line2){
	TableObj.rows[line1].swapNode(TableObj.rows[line2]);
}

function ChangeCol(line1,line2){
	for(i=0;i<TableObj.rows.length;i++){
		TableObj.rows[i].cells[line1].swapNode(TableObj.rows[i].cells[line2]);
	}
}

function MoveUp(){
	event.cancelBubble=true;
	if(null==curRow || 1>=curRow){return;}
	ChangeRow(curRow,--curRow);
	setDefault();
}

function MoveDown(){
	event.cancelBubble=true;
	if(null==curRow || curRow==TableObj.rows.length-1 || 0==curRow){return;}
	ChangeRow(curRow,++curRow);
	setDefault();
}

function MoveLeft(){
	event.cancelBubble=true;
	if(null==curCol || 0==curCol || 1==curCol){return;}
	ChangeCol(curCol,--curCol);
	if(curCol==sortCol){sortCol=curCol+1;}
	else if(curCol+1==sortCol){sortCol=curCol;}
	setDefault();
}

function MoveRight(){
	event.cancelBubble=true;
	if(null==curCol || 0==curCol || curCol==TableObj.rows[0].cells.length-1){return;}
	ChangeCol(curCol,++curCol);
	if(curCol==sortCol){sortCol=curCol-1;}
	else if(curCol-1==sortCol){sortCol=curCol;}
	setDefault();
}

function AddRow(){
	event.cancelBubble=true;
	var the_row,the_cell;
	the_row = ( null==curRow?-1:(curRow+1) );
	ClearStyle();
	var newrow=TableObj.insertRow(the_row);
	for (i=0;i<TableObj.rows[0].cells.length;i++) {
		the_cell=newrow.insertCell(i);
		//the_cell.parentElement.rowIndex
		the_cell.className=( 0==i?"list-c":"" );
		the_cell.innerHTML=( 0==i?theRowTitle:"" );
		the_cell.align="right";
	}
	setDefault();
}

function AddCol(){
	event.cancelBubble=true;
	var the_col,i,the_cell;
	the_col = ( null==curCol?-1:(curCol+1) );
	if(-1!=the_col && the_col<=sortCol && null!=sortCol)sortCol++;
	ClearStyle();
	for(i=0;i<TableObj.rows.length;i++){
		the_cell=TableObj.rows[i].insertCell(the_col);
		//the_cell.cellIndex
		the_cell.innerHTML=( 0==i?theColTitle:"" );
		the_cell.align="right";
	}
	setDefault();
}

function DelRow(){
	if(1==TableObj.rows.length){return;}
	var the_row;
	the_row = ( (null==curRow || 0==curRow)?-1:curRow );
	TableObj.deleteRow(the_row);
	curRow = null;
	setDefault();
}

function DelCol(){
	if(0==curCol){return;}
	if(1==TableObj.rows[0].cells.length){return;}
	var the_col,the_cell;
	the_col = ( null==curCol?(TableObj.rows[0].cells.length-1):curCol );
	if(-1!=the_col && the_col<sortCol && null!=sortCol)sortCol--;
	else if(the_col==sortCol)sortCol=null;
	for(i=0;i<TableObj.rows.length;i++){TableObj.rows[i].deleteCell(the_col);}
	curCol = null;
	setDefault();
}

function clickItem(){
	event.cancelBubble=true;
	var the_obj = event.srcElement;
	var i = 0 ,j = 0;
	if( "table" != the_obj.tagName.toLowerCase() &&  "tbody" != the_obj.tagName.toLowerCase() &&  "tr" != the_obj.tagName.toLowerCase() ){
		var the_td	= MyGetElement(the_obj,"td");
		if(null==the_td) return;
		var the_tr	= the_td.parentElement;
		var TableObj	= MyGetElement(the_td,"table");
		var i 		= 0;
		ClearStyle();
		curRow = the_tr.rowIndex;
		curCol = the_td.cellIndex;
		if(0!=curRow){
			for(i=1;i<the_tr.cells.length;i++){
				with(the_tr.cells[i]){
					className="sum";
				}
			}
			if(0!=curCol){
				for(i=1;i<TableObj.rows.length;i++){
					TableObj.rows[i].cells[curCol].className="sum";
				}
			}
		}else{
			if(0!=curCol){
				for(i=1;i<TableObj.rows.length;i++){
					TableObj.rows[i].cells[curCol].className="sum";
				}
			}
			sortCol=curCol;
		}
	}
}

function document.onclick(){
	ClearStyle();
	curRow  = null;
	curCol  = null;
}


// End Table Control ----------------------------------------------------------------------------------------------------------


// Start Cell Value Check ----------------------------------------------------------------------------------------------------------

function checkValue(obj){
	var strValue = obj.value;
	if( -1!=strValue.indexOf(",") ){
		alert('Please don\'t import ","');
		obj.value = strValue.replace(",","");
		obj.focus();
	}
}

// End Cell Value Check ----------------------------------------------------------------------------------------------------------

