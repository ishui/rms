var m_treeId;
var m_rowId;
var m_imgId;
var m_spanId;
var m_headCount;

var m_imgPlusFileName, m_imgMinusFileName;

var m_isAutoCalc = true;  //是否自动计算金额、单价

var m_LastSelectedRow;
var m_LastSelectedRowClass;

//记录展开的结点号
var m_arrExpandNode = new Array();

function CBTree_ShowLoading()
{
	if (document.all.divHintLoad) document.all.divHintLoad.style.display = "";
}

function CBTree_HideLoading()
{
	if (document.all.divHintLoad) document.all.divHintLoad.style.display = "none";
}

function CBTree_GetTreeNodeKey(sender)
{
    var key = sender.key;
    
    if (!key)
    {
//    	var m_rowId = m_treeId + "Node_";
	    key = sender.id.replace(m_rowId, "");
    }
    
    return key;
}

function CBTree_GetImgKey(sender)
{
    var key = sender.key;
    
    if (!key)
    {
//    	var m_imgId = m_treeId + "NodeImg_";
	    key = sender.id.replace(m_imgId, "");
    }
    
    return key;
}

function CBTree_ImgExpandClick(sender)
{
//var d1 = new Date();

	CBTree_ShowLoading();
//	ImgExpandClickOnTimer(sender);

	obj = eval(sender);
	setTimeout("CBTree_ImgExpandClickOnTimer(obj)", 1);
}

function CBTree_GetImgExpandResult(expand)
{
	//展开、折叠
	if (expand == "1")
		return "0";
	else
		return "1";
}

function CBTree_ImgExpandClickOnTimer(sender)
{
	var key = CBTree_GetImgKey(sender);

	var objTree = document.all(m_treeId);
	
	var currRow = document.all(m_rowId + key);
	
	//展开、折叠
    currRow.expand = CBTree_GetImgExpandResult(currRow.expand);

	CBTree_NodeExpand(key, currRow.expand);	
	
	CBTree_HideLoading();
}

function CBTree_RemoveItem(ar,code)
{
/*
	var iCount = ar.length;
	for ( var i=0; i<iCount; i++)
	{
		if ( ar[i] == code )
		{
			ar.shift(i);
			return;
		}
	}
*/

	var tempArray = new Array();
	var iCount = ar.length;
	for ( var i=0; i<iCount; i++)
	{
		if ( ar[i] != code )
			tempArray.push(ar[i]);
	}
	return tempArray;
}

//记录展开/折叠的状态	
function CBTree_SetExpandStatus(currRow, isExpand)
{
    var txtIsExpand;
    
    if (currRow.RowIndex)
    {
    	var r = parseInt(currRow.RowIndex);
    	var txtIsExpand = document.all("dgList:_ctl" + r + ":txtIsExpand");
    }
	
	if (txtIsExpand)
		txtIsExpand.value = isExpand;
	else
	{
		var code = CBTree_GetTreeNodeKey(currRow);
		
		if (isExpand == "1")
		{
			m_arrExpandNode.push(code);
		}
		else
		{
			m_arrExpandNode = CBTree_RemoveItem(m_arrExpandNode, code);
		}
	}
}

//展开或折叠一个节点
function CBTree_NodeExpand(key, isExpand)
{
	var objTree = document.all(m_treeId);
	var currRow = document.all(m_rowId + key);

	if (currRow) currRow.expand = isExpand;
	
	if (currRow && (currRow.childNodes.length > 0))
	{
		CBTree_SetExpandStatus(currRow, isExpand);
	}

	//图片	
	var objImg = document.all(m_imgId + key);
	if (objImg)
	{
		if (isExpand == "1")
			objImg.src = m_imgMinusFileName;
		else
			objImg.src = m_imgPlusFileName;
	}

	var rowCount = objTree.rows.length;
	for(var i=m_headCount;i<rowCount;i++)
	{
		var objRow = objTree.rows[i];

		if (objRow.ParentCode == key)  //1级子结点
		{
			if (isExpand == "1")  //将要展开
			{
				objRow.style.display = "";
			}
			else  //将要折叠
			{
				//折叠子节点下的所有子节点
				CBTree_NodeExpand(CBTree_GetTreeNodeKey(objRow), isExpand);

				objRow.style.display = "none";
			}

		}
	}		
}

//初始化树
function CBTree_InitTree(treeId, imgPlusFileName, imgMinusFileName, headCount)
{
	m_treeId = treeId;
	var objTree = document.all(treeId);
	
	m_rowId = treeId + "Node_";
	m_imgId = treeId + "NodeImg_";
	m_spanId = treeId + "NodeSpan_";
	
	m_headCount = headCount;
	
	m_imgPlusFileName = imgPlusFileName;
	m_imgMinusFileName = imgMinusFileName;
	
	var rowCount = objTree.rows.length;
	
	//数组记录关键字值
	var arrKey = new Array();
	
	//取最小层次
	var firstDeep = "";
	for(var i=headCount;i<rowCount;i++)
	{
		var objRow = objTree.rows[i];
		var key = CBTree_GetTreeNodeKey(objRow);
		
		if (key) arrKey.push(key);
		
		if ((objRow.Deep != null) && (objRow.Deep != ""))
		{
			var Deep = parseInt(objRow.Deep);
			if ((firstDeep == "") || (Deep < parseInt(firstDeep)))
			{
				firstDeep = Deep;
			}
		}
	}
	
	if (firstDeep == "") firstDeep = 0;
	
	for(var i=headCount;i<rowCount;i++)
	{
		var objRow = objTree.rows[i];
		
		var key = CBTree_GetTreeNodeKey(objRow);
		var ParentCode = objRow.ParentCode;
		var Deep = parseInt(objRow.Deep);
		
		//样式
		objRow.className = "list-" + (Deep - firstDeep + 1);
		
		//图片
		if ((objRow.ChildCount) && (objRow.ChildCount != "0")) //有子节点
		{
//			var objImg = document.all(m_imgId + key);
			var objImg = CBTree_GetSubChildNodeById(objRow.childNodes[0], m_imgId + key);
			if (objImg)
			{
				objImg.style.display = "";
				objImg.src = imgPlusFileName;
			}
		}
		else  //无子节点
		{
		/*
			if (objImg)
			{
				objImg.style.display = "none";
			}
			*/
		}
		
		//设置缩进
		objSpan = CBTree_GetChildNodeById(objRow.childNodes[0], m_spanId + key); //document.all(m_spanId + key);
		if (objSpan)
		{
			var spanText = "";
			for(var j=firstDeep;j<Deep;j++)
			{
				spanText += "&nbsp;&nbsp;&nbsp;";
			}
			
			objSpan.innerHTML = spanText;
		}
		
		//行是否显示，初始时只显示根节点
		objRow.style.display = "none";
		if (objRow.ParentCode != null)
		{
			if ((objRow.ParentCode == "") || (CBTree_GetArrayIndexOf(arrKey, objRow.ParentCode) < 0))  //无父节点
			{
				objRow.style.display = "";
			}
		}
		
		//缺省均为折叠状态
		objRow.expand = "0";
	}
}

//展开树（按节点的缺省是否展开属性）
function CBTree_ExpandTreeByNodeDefaultExpand()
{
	var objTree = document.all(m_treeId);

	var rowCount = objTree.rows.length;
	
	for(var i=m_headCount;i<rowCount;i++)
	{
		var objRow = objTree.rows[i];
		
		if ((objRow.ChildCount) && (objRow.ChildCount != "0")) //有子节点
		{
			if (objRow.DefaultExpand && (objRow.DefaultExpand == "1"))  //缺省展开
			{
//				objImg = document.all(m_imgId + GetTreeNodeKey(objRow));
				var objImg = CBTree_GetSubChildNodeById(objRow.childNodes[0], m_imgId + CBTree_GetTreeNodeKey(objRow));
//				objImg.click();
				CBTree_ImgExpandClickOnTimer(objImg);
			}
		}
	}
}

function CBTree_GetRowIndexByKey(key)
{
	var objTree = document.all(m_treeId);
	
	var rowCount = objTree.rows.length;
	for(var i=m_headCount;i<rowCount;i++)
	{
		var objRow = objTree.rows[i];
		if (CBTree_GetTreeNodeKey(objRow) == key)
		{
			return objRow.RowIndex;
		}
		
	}
	
	return "";
}

function CBTree_GetArrayIndexOf(arr, val)
{
	var index = -1;
	
	if (!arr) return index;
	
	for(var i=0;i<arr.length;i++)
	{
		if (arr[i] == val)
		{
			index = i;
			return index;
		}
	}
	
	return index;
}

//取子节点（可到二级）
function CBTree_GetSubChildNodeById(objParent, ChildId)
{
	var objChild = null;
	if (!objParent) return objChild;
	
	var l = objParent.childNodes.length;
	var childs = objParent.childNodes;
	
	for(var i=0;i<l;i++)
	{
		//一级子节点
		if(childs[i].id == ChildId)
		{
			objChild = childs[i];
			break;
		}

		//二级子节点
		objChild = CBTree_GetChildNodeById(childs[i], ChildId);
		if(objChild)
		{
			break;
		}
	}
	
	return objChild;
}

//取直系子节点
function CBTree_GetChildNodeById(objParent, ChildId)
{
	var objChild = null;
	var l = objParent.childNodes.length;
	var childs = objParent.childNodes;
	
	for(var i=0;i<l;i++)
	{
		if(childs[i].id == ChildId)
		{
			objChild = childs[i];
			break;
		}
	}
	
	return objChild;
}

/*************************************/
/*            年度展开               */
/*************************************/

function CBTree_FormatMonth(m)
{
	if (m.toString().length < 2)
		m = "0" + m.toString();
		
	return m;
}

//年度展开、折叠
function YearExpand(sender)
{
	//Repeater时使用
	var objTree = document.all(m_treeId);
	var rowCount = objTree.rows.length;
	var DetailRowCount = rowCount - m_headCount;

	var year = sender.key;
	
	if ((sender.expand) && (sender.expand == "1"))
	{
		sender.expand = "0";
		sender.title = "展开到月度";
	}
	else
	{
		sender.expand = "1";
		sender.title = "折叠到年度";
	}
	
	//隐藏、显示月份
	for(var i=1;i<=12;i++)
	{
		var ym = year + CBTree_FormatMonth(i);
		
		var strDisplay;
		if (sender.expand == "1")  //展开
		{
			strDisplay = "";
		}
		else  //折叠
		{
			strDisplay = "none";
		}

		//月份标题
		CBTree_SetObjectDisplay(document.all("YearTitle_" + ym), strDisplay);
		
		//月份数据
		CBTree_SetObjectDisplay(document.all("YearData_" + ym), strDisplay);
		
		//Repeater中的月份数据
		for(var j=0;j<DetailRowCount;j++)
		{
			CBTree_SetObjectDisplay(document.all("dgList__ctl" + j + "_YearData_" + ym), strDisplay);
		}
	}
	
	//年度标题跨列
	var objYearTitle = document.all("YearTitle_" + year);
	if (sender.expand == "1")  //展开
	{
		objYearTitle.colSpan = 13;
	}
	else  //折叠
	{
		objYearTitle.colSpan = 1;
	}
}

function CBTree_SetObjectDisplay(obj, strDisplay)
{
	if (!obj) return;
	
	if (obj[0])  //数组
	{
		for(var j=0;j<obj.length;j++)
		{
			obj[j].style.display = strDisplay;
		}
	}
	else
	{
		obj.style.display = strDisplay;
	}
}

/*************************************/
/*            金额               */
/*************************************/

//格式化预算金额
function CBTree_FormatMoney(val)
{
	var s = "";
	var num;
	
	if (val == "") return s;
	
	var num = ConvertFloat(val);
	if (isNaN(num)) return s;
	
	//0不显示
	if (num == 0) return s;
	
	s = formatNumber(num, "#,###.####");
	
	return s;
}

//格式化预算金额
function CBTree_FormatMoneyObject(obj)
{
	if (obj)
	{
		obj.value = CBTree_FormatMoney(obj.value);
	}
}

//金额字符串转成数值
function CBTree_RevertMoney(val)
{
	var f = val;
	
	if (f != "")
	{
		f = ConvertFloat(f);
	}
	
	return f;
}

//金额字符串转成数值
function CBTree_RevertMoneyObject(obj)
{
	if (obj)
	{
		obj.value = CBTree_RevertMoney(obj.value);
	}
}

//格式化数量
function CBTree_FormatQty(val)
{
	var s = "";
	var num;
	
	if (val == "") return s;
	
	var num = ConvertFloat(val);
	if (isNaN(num)) return s;
	
	//0不显示
	if (num == 0) return s;
	
	s = formatNumber(num, "#,###.####");
	
	return s;
}

//格式化数量
function CBTree_FormatQtyObject(obj)
{
	if (obj)
	{
		obj.value = CBTree_FormatQty(obj.value);
	}
}

//同步显示数据和录入数据
function CBTree_SynSpanTxt(sender)
{
	var spanId = sender.id;
	spanId = spanId.replace("txt", "span");
	var objSpan = document.all(spanId);
	if (objSpan)
	{
		objSpan.innerHTML = sender.value;
	}
}

//设置行选中
function CBTree_SetRowSelected(sender)
{
	if (m_LastSelectedRow)
	{
		m_LastSelectedRow.className = m_LastSelectedRowClass;
		
		m_LastSelectedRowClass = "";
		m_LastSelectedRow = "";
	}
	
	m_LastSelectedRow = sender;
	m_LastSelectedRowClass = m_LastSelectedRow.className;
	m_LastSelectedRow.className = "list-highlight";
}

/*
//计算余额
function CalcBalance(sender)
{
	if (!m_isAutoCalc) return;

	//余额显示控件
	var spanId = sender.id.replace("txtBudgetMoney", "spanBalance");
	var objSpan = document.all(spanId);
	if (objSpan)
	{
		//预算总额
		var BudgetMoney = ConvertFloat(sender.value);

		//合同总额
		var ContractTotalMoney = 0;
		var txtId = sender.id.replace("txtBudgetMoney", "txtContractTotalMoney");
		var objTxt = document.all(txtId);
		if (objTxt)
		{
			ContractTotalMoney = ConvertFloat(objTxt.value);
		}
		
		//余额 = 预算总额 - 合同总额
		var Balance = BudgetMoney - ContractTotalMoney;
		objSpan.innerText = CostBudget_FormatMoney(Balance);
	}
}
*/
