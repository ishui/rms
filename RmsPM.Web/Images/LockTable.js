function DrawTable(scrTable,newTable,iStart,iEnd,jEnd){
	var i,j,k=0,newTR,newTD,intWidth=0,intHeight=0;intStdWidth=0;
	
	newTable.mergeAttributes(scrTable);
	for (i=iStart;i<iEnd;i++){
		newTR=newTable.insertRow(k)
		newTR.mergeAttributes(scrTable.rows[i]);
		intHeight += scrTable.rows[i].offsetHeight;
		intWidth=0;
		for(j=0;j<(jEnd==-1?scrTable.rows[i].cells.length:jEnd);j++){
			newTD=scrTable.rows[i].cells[j].cloneNode(true);
			intWidth+= scrTable.rows[i].cells[j].offsetWidth;
			newTR.insertBefore(newTD);
			newTD.style.pixelWidth=scrTable.rows[i].cells[j].offsetWidth;
		}
		
		k++
	}
	newTable.style.pixelWidth=intWidth;
	newTable.style.pixelHeight=intHeight;
}

function DrawTable1(scrTable,newTable,iStart,iEnd,jEnd){
	var i,j,k=0,newTR,newTD,intWidth=0,intHeight=0;intStdWidth=0;
	
	newTable.mergeAttributes(scrTable);
	for (i=iStart;i<iEnd;i++){
		newTR=newTable.insertRow(k);
		newTR.mergeAttributes(scrTable.rows[i]);
		intHeight += scrTable.rows[i].offsetHeight;
		intWidth=0;
		for(j=0;j<(jEnd==-1?scrTable.rows[i].cells.length:jEnd);j++){
			if (data[i][j].tagName=="BR") continue;
			newTD=data[i][j].cloneNode(true);
			intWidth+= data[i][j].offsetWidth;
			newTR.insertBefore(newTD);
			newTD.style.pixelWidth=data[i][j].offsetWidth;
		}
		
		k++;
	}
	newTable.style.pixelWidth=intWidth;
	newTable.style.pixelHeight=intHeight;
}
var data;
function LockTable(arTable,ColNum,RowHead,RowFoot){
	var IsNoCreate = true;
	
	var c=ColNum;
	data = new Array(arTable.rows.length)

	for (var r = 0; r < arTable.rows.length; r++)
	{
		data[r] = new Array(c);
	}

	for (var r = 0; r < arTable.rows.length; r++)
	{
		phxcols = 0;

		for (var i = 0; i < c; i++)
		{
			if (typeof(data[r][i])== "undefined")
			{
				for (var row = 0; row < arTable.rows[r].cells[phxcols].getAttribute("rowspan"); row++)
				{
					for (var cols = 0; cols < arTable.rows[r].cells[phxcols].getAttribute("colspan"); cols++)
					{
						if (row==0&&cols==0){
							//newTD=arTable.rows[r].cells[phxcols].cloneNode(true);
							data[r + row][i + cols] = arTable.rows[r].cells[phxcols];
						}else{
							newTD=document.createElement("BR");
							data[r + row][i + cols] = newTD;
						}
					}
				}
				phxcols++;
			}
		}
	}
	
	arTable.HeadRow=RowHead;
	var objDivMaster=arTable.parentElement;
	if(objDivMaster.tagName!='DIV')return;
	if((arTable.offsetHeight > objDivMaster.offsetHeight)&&(arTable.offsetWidth > objDivMaster.offsetWidth)){
		//表格宽高都超出范围
		if((ColNum > 0) && (RowHead > 0)){
			//左上均有固定，创建左上部 
			var objTableLH=document.createElement("TABLE");
			var newTBody=document.createElement("TBODY");
			objTableLH.insertBefore(newTBody);
			objTableLH.id="objTableLH";
			objDivMaster.parentElement.insertBefore(objTableLH);
			DrawTable1(arTable,objTableLH,0,RowHead,ColNum)
			objTableLH.srcTable=arTable;
			with(objTableLH.style){
				zIndex=14;
				position='absolute';
				pixelLeft=objDivMaster.offsetLeft;
				pixelTop=objDivMaster.offsetTop;
			}
		}
	
		if((ColNum > 0) && (RowFoot > 0)){
			//左下均有固定，创建左下部 
			var objTableLF=document.createElement("TABLE");
			var newTBody=document.createElement("TBODY");
			objTableLF.insertBefore(newTBody);
			objTableLF.id="objTableLF";
			objDivMaster.parentElement.insertBefore(objTableLF);
			DrawTable1(arTable,objTableLF,arTable.rows.length - RowFoot,arTable.rows.length,ColNum)
			objTableLF.srcTable=arTable;
			with(objTableLF.style){
				zIndex=13;
				position='absolute';
				pixelLeft=objDivMaster.offsetLeft;
				pixelTop=objDivMaster.offsetTop + objDivMaster.offsetHeight - objTableLF.offsetHeight - 16;
			}
		}
	}

	if((RowHead > 0) && (arTable.offsetHeight > objDivMaster.offsetHeight)){
		//有表头超高
		IsNoCreate = true;
		try{
			if(DivHeadTar){
				IsNoCreate=true;
			}else{
				IsNoCreate=false;
			}
		}catch(e){
			IsNoCreate=false;
		}
		
		var DivHead;
		if(IsNoCreate){
			//不创建
			DivHead=document.all('DivHeadTar');
			DivHead.innerHTML="";
		}else{
			//创建
			DivHead=document.createElement("DIV");
			DivHead.id="DivHeadTar";
			objDivMaster.parentElement.insertBefore(DivHead);
		}
		var objTableHead=document.createElement("TABLE");
		var newTBody=document.createElement("TBODY");
		objTableHead.id="HeadTar";
		objTableHead.style.position="relative"
		objTableHead.insertBefore(newTBody);
		DivHead.insertBefore(objTableHead);
		DrawTable(arTable,objTableHead,0,RowHead,-1);
		HeadTar.srcTable=arTable;
		with(DivHead.style){
			overflow="hidden";
			zIndex=12;
			pixelWidth=objDivMaster.offsetWidth - 16;
			position='absolute';
			pixelLeft=objDivMaster.offsetLeft;
			pixelTop=objDivMaster.offsetTop;
		}
		objDivMaster.attachEvent("onscroll",divScroll1);
	}
	
	if((RowFoot > 0) && (arTable.offsetHeight > objDivMaster.offsetHeight)){
		//有脚超高
		IsNoCreate = true;
		try{
			if(DivFootTar){
				IsNoCreate=true;
			}else{
				IsNoCreate=false;
			}
		}catch(e){
			IsNoCreate=false;
		}
		
		var DivFoot;
		if(IsNoCreate){
			//不创建
			DivFoot=document.all('DivFootTar');
			DivFoot.innerHTML="";
		}else{
			//创建
			DivFoot=document.createElement("DIV");
			DivFoot.id="DivFootTar";
			objDivMaster.parentElement.insertBefore(DivFoot);
		}
		
		var objTableFoot=document.createElement("TABLE");
		var newTBody=document.createElement("TBODY");
		objTableFoot.insertBefore(newTBody);
		objTableFoot.id="FootTar";
		objTableFoot.style.position="relative"
		DivFoot.insertBefore(objTableFoot);
		DrawTable(arTable,objTableFoot,arTable.rows.length - RowFoot,arTable.rows.length,-1);
		objTableFoot.srcTable=arTable;
		with(DivFoot.style){
			overflow="hidden";
			zIndex=11;
			pixelWidth=objDivMaster.offsetWidth - 16;
			position='absolute';
			pixelLeft=objDivMaster.offsetLeft;
			pixelTop=objDivMaster.offsetTop + objDivMaster.offsetHeight - DivFoot.offsetHeight - 16;
		}
		objDivMaster.attachEvent("onscroll",divScroll2);
	}
	
	if((ColNum > 0) && (arTable.offsetWidth > objDivMaster.offsetWidth)){
		//有左超宽
		IsNoCreate = true;
		try{
			if(DivLeftTar){
				IsNoCreate=true;
			}else{
				IsNoCreate=false;
			}
		}catch(e){
			IsNoCreate=false;
		}
		
		var DivLeft;
		if(IsNoCreate){
			//不创建
			DivLeft=document.all('DivLeftTar');
			DivLeft.innerHTML="";
		}else{
			//创建
			DivLeft=document.createElement("DIV");
			DivLeft.id="DivLeftTar";
			objDivMaster.parentElement.insertBefore(DivLeft);
		}
		
		var objTableLeft=document.createElement("TABLE");
		var newTBody=document.createElement("TBODY");
		objTableLeft.insertBefore(newTBody);
		objTableLeft.id="LeftTar";
		objTableLeft.style.position="relative";
		DivLeft.insertBefore(objTableLeft);
		DrawTable1(arTable,objTableLeft,0,arTable.rows.length,ColNum);
		LeftTar.srcTable=arTable;
		with(DivLeft.style){
			overflow="hidden";
			zIndex=10;
			pixelWidth=objDivMaster.offsetWidth - 16;
			pixelHeight=objDivMaster.offsetHeight - 16;
			position='absolute';
			pixelLeft=objDivMaster.offsetLeft;
			pixelTop=objDivMaster.offsetTop;
		}
		objDivMaster.attachEvent("onscroll",divScroll3);
	}
}

function divScroll1(){
	//表头横向滚动
	var tbl=document.all('HeadTar').srcTable,parDiv=tbl.parentElement;
	while(parDiv.tagName!='DIV')parDiv=parDiv.parentElement;
	window.status=-parDiv.scrollLeft;
	document.all('HeadTar').style.pixelLeft= -parDiv.scrollLeft;
}

function divScroll2(){
	//表脚横向滚动
	var tbl=document.all('FootTar').srcTable,parDiv=tbl.parentElement;
	while(parDiv.tagName!='DIV')parDiv=parDiv.parentElement;
	window.status=-parDiv.scrollLeft;
	document.all('FootTar').style.pixelLeft= -parDiv.scrollLeft;
}

function divScroll3(){
	//左标题纵向滚动
	var tbl=document.all('LeftTar').srcTable,parDiv=tbl.parentElement;
	while(parDiv.tagName!='DIV')parDiv=parDiv.parentElement;
	window.status=-parDiv.scrollLeft;
	document.all('LeftTar').style.pixelTop= -parDiv.scrollTop;
}
