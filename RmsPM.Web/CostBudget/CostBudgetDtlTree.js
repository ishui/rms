var m_treeId;
var m_rowId;
var m_imgId;
var m_spanId;
var m_headCount;

var m_imgPlusFileName, m_imgMinusFileName;

var m_isAutoCalc = true;  //�Ƿ��Զ����������

var m_LastSelectedRow;
var m_LastSelectedRowClass;

//��¼չ���Ľ���
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
	//չ�����۵�
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
	
	//չ�����۵�
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

//��¼չ��/�۵���״̬	
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

//չ�����۵�һ���ڵ�
function CBTree_NodeExpand(key, isExpand)
{
	var objTree = document.all(m_treeId);
	var currRow = document.all(m_rowId + key);

	if (currRow) currRow.expand = isExpand;
	
	if (currRow && (currRow.childNodes.length > 0))
	{
		CBTree_SetExpandStatus(currRow, isExpand);
	}

	//ͼƬ	
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

		if (objRow.ParentCode == key)  //1���ӽ��
		{
			if (isExpand == "1")  //��Ҫչ��
			{
				objRow.style.display = "";
			}
			else  //��Ҫ�۵�
			{
				//�۵��ӽڵ��µ������ӽڵ�
				CBTree_NodeExpand(CBTree_GetTreeNodeKey(objRow), isExpand);

				objRow.style.display = "none";
			}

		}
	}		
}

//��ʼ����
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
	
	//�����¼�ؼ���ֵ
	var arrKey = new Array();
	
	//ȡ��С���
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
		
		//��ʽ
		objRow.className = "list-" + (Deep - firstDeep + 1);
		
		//ͼƬ
		if ((objRow.ChildCount) && (objRow.ChildCount != "0")) //���ӽڵ�
		{
//			var objImg = document.all(m_imgId + key);
			var objImg = CBTree_GetSubChildNodeById(objRow.childNodes[0], m_imgId + key);
			if (objImg)
			{
				objImg.style.display = "";
				objImg.src = imgPlusFileName;
			}
		}
		else  //���ӽڵ�
		{
		/*
			if (objImg)
			{
				objImg.style.display = "none";
			}
			*/
		}
		
		//��������
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
		
		//���Ƿ���ʾ����ʼʱֻ��ʾ���ڵ�
		objRow.style.display = "none";
		if (objRow.ParentCode != null)
		{
			if ((objRow.ParentCode == "") || (CBTree_GetArrayIndexOf(arrKey, objRow.ParentCode) < 0))  //�޸��ڵ�
			{
				objRow.style.display = "";
			}
		}
		
		//ȱʡ��Ϊ�۵�״̬
		objRow.expand = "0";
	}
}

//չ���������ڵ��ȱʡ�Ƿ�չ�����ԣ�
function CBTree_ExpandTreeByNodeDefaultExpand()
{
	var objTree = document.all(m_treeId);

	var rowCount = objTree.rows.length;
	
	for(var i=m_headCount;i<rowCount;i++)
	{
		var objRow = objTree.rows[i];
		
		if ((objRow.ChildCount) && (objRow.ChildCount != "0")) //���ӽڵ�
		{
			if (objRow.DefaultExpand && (objRow.DefaultExpand == "1"))  //ȱʡչ��
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

//ȡ�ӽڵ㣨�ɵ�������
function CBTree_GetSubChildNodeById(objParent, ChildId)
{
	var objChild = null;
	if (!objParent) return objChild;
	
	var l = objParent.childNodes.length;
	var childs = objParent.childNodes;
	
	for(var i=0;i<l;i++)
	{
		//һ���ӽڵ�
		if(childs[i].id == ChildId)
		{
			objChild = childs[i];
			break;
		}

		//�����ӽڵ�
		objChild = CBTree_GetChildNodeById(childs[i], ChildId);
		if(objChild)
		{
			break;
		}
	}
	
	return objChild;
}

//ȡֱϵ�ӽڵ�
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
/*            ���չ��               */
/*************************************/

function CBTree_FormatMonth(m)
{
	if (m.toString().length < 2)
		m = "0" + m.toString();
		
	return m;
}

//���չ�����۵�
function YearExpand(sender)
{
	//Repeaterʱʹ��
	var objTree = document.all(m_treeId);
	var rowCount = objTree.rows.length;
	var DetailRowCount = rowCount - m_headCount;

	var year = sender.key;
	
	if ((sender.expand) && (sender.expand == "1"))
	{
		sender.expand = "0";
		sender.title = "չ�����¶�";
	}
	else
	{
		sender.expand = "1";
		sender.title = "�۵������";
	}
	
	//���ء���ʾ�·�
	for(var i=1;i<=12;i++)
	{
		var ym = year + CBTree_FormatMonth(i);
		
		var strDisplay;
		if (sender.expand == "1")  //չ��
		{
			strDisplay = "";
		}
		else  //�۵�
		{
			strDisplay = "none";
		}

		//�·ݱ���
		CBTree_SetObjectDisplay(document.all("YearTitle_" + ym), strDisplay);
		
		//�·�����
		CBTree_SetObjectDisplay(document.all("YearData_" + ym), strDisplay);
		
		//Repeater�е��·�����
		for(var j=0;j<DetailRowCount;j++)
		{
			CBTree_SetObjectDisplay(document.all("dgList__ctl" + j + "_YearData_" + ym), strDisplay);
		}
	}
	
	//��ȱ������
	var objYearTitle = document.all("YearTitle_" + year);
	if (sender.expand == "1")  //չ��
	{
		objYearTitle.colSpan = 13;
	}
	else  //�۵�
	{
		objYearTitle.colSpan = 1;
	}
}

function CBTree_SetObjectDisplay(obj, strDisplay)
{
	if (!obj) return;
	
	if (obj[0])  //����
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
/*            ���               */
/*************************************/

//��ʽ��Ԥ����
function CBTree_FormatMoney(val)
{
	var s = "";
	var num;
	
	if (val == "") return s;
	
	var num = ConvertFloat(val);
	if (isNaN(num)) return s;
	
	//0����ʾ
	if (num == 0) return s;
	
	s = formatNumber(num, "#,###.####");
	
	return s;
}

//��ʽ��Ԥ����
function CBTree_FormatMoneyObject(obj)
{
	if (obj)
	{
		obj.value = CBTree_FormatMoney(obj.value);
	}
}

//����ַ���ת����ֵ
function CBTree_RevertMoney(val)
{
	var f = val;
	
	if (f != "")
	{
		f = ConvertFloat(f);
	}
	
	return f;
}

//����ַ���ת����ֵ
function CBTree_RevertMoneyObject(obj)
{
	if (obj)
	{
		obj.value = CBTree_RevertMoney(obj.value);
	}
}

//��ʽ������
function CBTree_FormatQty(val)
{
	var s = "";
	var num;
	
	if (val == "") return s;
	
	var num = ConvertFloat(val);
	if (isNaN(num)) return s;
	
	//0����ʾ
	if (num == 0) return s;
	
	s = formatNumber(num, "#,###.####");
	
	return s;
}

//��ʽ������
function CBTree_FormatQtyObject(obj)
{
	if (obj)
	{
		obj.value = CBTree_FormatQty(obj.value);
	}
}

//ͬ����ʾ���ݺ�¼������
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

//������ѡ��
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
//�������
function CalcBalance(sender)
{
	if (!m_isAutoCalc) return;

	//�����ʾ�ؼ�
	var spanId = sender.id.replace("txtBudgetMoney", "spanBalance");
	var objSpan = document.all(spanId);
	if (objSpan)
	{
		//Ԥ���ܶ�
		var BudgetMoney = ConvertFloat(sender.value);

		//��ͬ�ܶ�
		var ContractTotalMoney = 0;
		var txtId = sender.id.replace("txtBudgetMoney", "txtContractTotalMoney");
		var objTxt = document.all(txtId);
		if (objTxt)
		{
			ContractTotalMoney = ConvertFloat(objTxt.value);
		}
		
		//��� = Ԥ���ܶ� - ��ͬ�ܶ�
		var Balance = BudgetMoney - ContractTotalMoney;
		objSpan.innerText = CostBudget_FormatMoney(Balance);
	}
}
*/
