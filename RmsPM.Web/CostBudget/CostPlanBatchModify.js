/*************************************/
/*            金额               */
/*************************************/

//格式化计划付款金额
function FormatMoney(val)
{
	var s = "";
	var num;
	
	if (val == "") return s;
	
	var num = ConvertFloat(val);
	if (isNaN(num)) return s;
	
	//0不显示
	if (num == 0) return s;
	
	s = formatNumber(num, "#,###.00");
	
	return s;
}

//格式化计划付款金额
function FormatMoneyObject(obj)
{
	if (obj)
	{
		obj.value = FormatMoney(obj.value);
	}
}

//取子节点的总金额
function GetChildSumMoney(ParentCode, txtId)
{
	var SumMoney = 0;
	var objRow;
	var Money;
	
	var objTree = document.all(m_treeId);
	
	var rowCount = objTree.rows.length;
	for(var i=m_headCount;i<rowCount;i++)
	{
		objRow = objTree.rows[i];
		if (objRow.ParentCode == ParentCode)
    	{
//    	    var objTxt = document.all(txtId)[objRow.RowIndex];
			var objTxt = document.all("dgList:_ctl" + objRow.RowIndex + ":" + txtId);
			if (objTxt)
			{
				Money = ConvertFloat(objTxt.value);
				SumMoney = SumMoney + Money;
			}
		}
	}
	
//	alert(ParentCode + ":" + SumMoney);
	return SumMoney;
}

//更新父节点的金额
function CalcParentMoney(sender)
{
	if (!m_isAutoCalc) return;

	var r = sender.RowIndex;
	if ((r == null) || (r == "")) return;
	
	var ParentCode = sender.ParentCode;
	var ParentRowIndex = CBTree_GetRowIndexByKey(ParentCode);
	if (ParentRowIndex != "")
	{
		var txtId = sender.id.replace("dgList__ctl" + r + "_", "");
//		var objParentTxt = document.all(txtId)[ParentRowIndex];
		var objParentTxt = document.all("dgList:_ctl" + ParentRowIndex + ":" + txtId);
		
		if (objParentTxt)
		{
			objParentTxt.value = GetChildSumMoney(ParentCode, txtId);
			
//			MoneyBlur(objParentTxt, true);
			
			MoneyBlur(objParentTxt, false);

			//更新父节点的金额
			CalcParentMoney(objParentTxt);
		}
	}
}

//金额输入前
function MoneyFocus(sender)
{
	//还原成数值
	CBTree_RevertMoneyObject(sender);
}

//金额输入后
function MoneyBlur(sender, isAutoCalc)
{
	//格式化金额
	FormatMoneyObject(sender);
	
	//同步显示数据和录入数据
//	CBTree_SynSpanTxt(sender);

	if (isAutoCalc)
	{
		//更新父节点的金额
//		CalcParentMoney(sender);
		
		//更新总额
//		var TotalMoney = GetChildSumMoney("", "txtItemMoney");
//		document.all("txtMoney").value = FormatMoney(TotalMoney);
		
	}
}

