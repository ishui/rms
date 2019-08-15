var headCount = 2;

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
	CBTree_FormatMoneyObject(sender);
	
	//同步显示数据和录入数据
	CBTree_SynSpanTxt(sender);

	if (isAutoCalc)
	{
		//计算数量
		CalcQtyByMoney(sender);
		
		//更新父节点的金额
		CalcParentMoney(sender);
	}
}

//单价输入后
function PriceBlur(sender, isAutoCalc)
{
	//格式化金额
	CBTree_FormatMoneyObject(sender);
	
	//同步显示数据和录入数据
	CBTree_SynSpanTxt(sender);
	
	if (isAutoCalc)
	{
		//计算金额 = 单价 * 数量
		CalcMoneyByPrice(sender);
	}
}

//数量输入后
function QtyBlur(sender, isAutoCalc)
{
	//格式化数量
	CBTree_FormatQtyObject(sender);
	
	//同步显示数据和录入数据
	CBTree_SynSpanTxt(sender);

	if (isAutoCalc)
	{
		//计算金额 = 单价 * 数量
		CalcMoneyByQty(sender);

		//更新父节点的数量
		CalcParentQty(sender);
	}
}

//展开时，预算金额只读；折叠时，预算金额可录入
function SetRowInput(objRow, flag, StartY, EndY)
{
	var r = parseInt(objRow.RowIndex);
	
	var display_txt;
	var display_span;

    var key = CBTree_GetTreeNodeKey(objRow);
    
    var expand_result;
    if (flag == 1) //after
    {
        expand_result = objRow.expand;
    }
    else
    {
        expand_result = CBTree_GetImgExpandResult(objRow.expand);
    }
    
	if (key == "R_0") //合计行始终不可录入
	{
		display_span = "";
		display_txt = "none";

		//隐藏说明
		CBTree_SetObjectDisplay(document.all("dgList:_ctl" + r + ":txtDescription"), display_txt);
	}
	else
	{
		//是否可录入	
		if (expand_result == "1")  //折叠 -> 展开
		{
			display_span = "";
			display_txt = "none";
		}
		else  //展开 -> 折叠
		{
			display_span = "none";
			display_txt = "";
		}
	}
	
	//隐藏/显示单价
	CBTree_SetObjectDisplay(document.all("dgList__ctl" + r + "_spanPrice"), display_span);
	CBTree_SetObjectDisplay(document.all("dgList:_ctl" + r + ":txtPrice"), display_txt);

	//隐藏/显示数量
	CBTree_SetObjectDisplay(document.all("dgList__ctl" + r + "_spanQty"), display_span);
	CBTree_SetObjectDisplay(document.all("dgList:_ctl" + r + ":txtQty"), display_txt);

	//隐藏/显示预算总额
	CBTree_SetObjectDisplay(document.all("dgList__ctl" + r + "_spanBudgetMoney"), display_span);
	CBTree_SetObjectDisplay(document.all("dgList:_ctl" + r + ":txtBudgetMoney"), display_txt);

	//设置预算计划金额是否可录入
	var StartY = parseInt(StartY);
	var EndY = parseInt(EndY);
	
	for(var y=StartY;y<=EndY;y++)
	{
		for(var m=0;m<=12;m++)
		{
			var ym = y.toString() + CBTree_FormatMonth(m);
			CBTree_SetObjectDisplay(document.all("dgList__ctl" + r + "_spanBudgetMoney_" + ym), display_span);
			CBTree_SetObjectDisplay(document.all("dgList:_ctl" + r + ":txtBudgetMoney_" + ym), display_txt);
		}
	}
}

//展开树（按节点的缺省是否展开属性）
function ExpandTreeByNodeDefaultExpand()
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
				var objImg = CBTree_GetSubChildNodeById(objRow.childNodes[0], m_imgId + CBTree_GetTreeNodeKey(objRow));
				ImgExpand(objImg, -1);
//				objImg.click();
				CBTree_ImgExpandClickOnTimer(objImg);
			}
		}
	}
}

//金额 = 单价 * 数量
function GetMoneyByPriceQty(price, qty)
{
	var money = Math.round(ConvertFloat(price) * ConvertFloat(qty) * 10000) / 10000;
	return money;
}

//单价 = 金额 / 数量
function GetPriceByMoneyQty(money, qty)
{
	var price = 0;
	var f_qty = ConvertFloat(qty);
	
	if (f_qty != 0)
	{
		price = Math.round(ConvertFloat(money) / f_qty  * 10000) / 10000;
	}
	
	return price;
}

//数量 = 金额 / 单价
function GetQtyByMoneyPrice(money, price)
{
	var qty = 0;
	var f_price = ConvertFloat(price);
	
	if (f_price != 0)
	{
		qty = Math.round(ConvertFloat(money) / f_price  * 10000) / 10000;
	}
	
	return qty;
}

//单价变化时计算金额
function CalcMoneyByPrice(sender)
{
	if (!m_isAutoCalc) return;
	
	var qtyId = sender.id.replace("Price", "Qty");
	var moneyId = sender.id.replace("Price", "BudgetMoney");
	
	var objQty = document.all(qtyId);
	var objMoney = document.all(moneyId);
	if ((objQty) && (objMoney))
	{
		objMoney.value = GetMoneyByPriceQty(sender.value, objQty.value);
		MoneyBlur(objMoney);

		//更新父节点的金额
		CalcParentMoney(objMoney);
	}
}

//数量变化时计算金额
function CalcMoneyByQty(sender)
{
	if (!m_isAutoCalc) return;

	var priceId = sender.id.replace("Qty", "Price");
	var moneyId = sender.id.replace("Qty", "BudgetMoney");
	
	var objPrice = document.all(priceId);
	var objMoney = document.all(moneyId);
	if ((objPrice) && (objMoney))
	{
		objMoney.value = GetMoneyByPriceQty(objPrice.value, sender.value);
		MoneyBlur(objMoney);

		//更新父节点的金额
		CalcParentMoney(objMoney);
	}
}

//金额变化时计算单价
function CalcPriceByMoney(sender)
{
	if (!m_isAutoCalc) return;

	var qtyId = sender.id.replace("BudgetMoney", "Qty");
	var priceId = sender.id.replace("BudgetMoney", "Price");
	
	var objQty = document.all(qtyId);
	var objPrice = document.all(priceId);
	if ((objQty) && (objPrice))
	{
		objPrice.value = GetPriceByMoneyQty(sender.value, objQty.value);
		MoneyBlur(objPrice);
	}
}

//金额变化时计算数量
function CalcQtyByMoney(sender)
{
	if (!m_isAutoCalc) return;

	var qtyId = sender.id.replace("BudgetMoney", "Qty");
	var priceId = sender.id.replace("BudgetMoney", "Price");
	
	var objQty = document.all(qtyId);
	var objPrice = document.all(priceId);
	if ((objQty) && (objPrice))
	{
		objQty.value = GetQtyByMoneyPrice(sender.value, objPrice.value);
		MoneyBlur(objQty);
	}
}

//数量变化时计算单价
function CalcPriceByQty(sender)
{
	if (!m_isAutoCalc) return;

	var moneyId = sender.id.replace("Qty", "BudgetMoney");
	var priceId = sender.id.replace("Qty", "Price");
	
	var objMoney = document.all(moneyId);
	var objPrice = document.all(priceId);
	if ((objMoney) && (objPrice))
	{
		objPrice.value = GetPriceByMoneyQty(objMoney.value, sender.value);
		MoneyBlur(objPrice);
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
			var objTxt = document.all("dgList:_ctl" + objRow.RowIndex + ":" + txtId);
			if (objTxt)
			{
				Money = ConvertFloat(objTxt.value);
				SumMoney = SumMoney + Money;
			}
		}
	}
	
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
		var objParentTxt = document.all("dgList:_ctl" + ParentRowIndex + ":" + txtId);
		if (objParentTxt)
		{
			objParentTxt.value = GetChildSumMoney(ParentCode, txtId);
			
			MoneyBlur(objParentTxt, false);

			//计算单价
			CalcPriceByMoney(objParentTxt);
		
			//更新父节点的金额
			CalcParentMoney(objParentTxt);
		}
	}
}

//更新父节点的数量
function CalcParentQty(sender)
{
	if (!m_isAutoCalc) return;

	var r = sender.RowIndex;
	if ((r == null) || (r == "")) return;
	
	var ParentCode = sender.ParentCode;
	var ParentRowIndex = CBTree_GetRowIndexByKey(ParentCode);
	if (ParentRowIndex != "")
	{
		var txtId = sender.id.replace("dgList__ctl" + r + "_", "");
		var objParentTxt = document.all("dgList:_ctl" + ParentRowIndex + ":" + txtId);
		if (objParentTxt)
		{
			objParentTxt.value = GetChildSumMoney(ParentCode, txtId);
			
//			QtyBlur(objParentTxt, true);
			
			QtyBlur(objParentTxt, false);

			//计算单价
			CalcPriceByQty(objParentTxt);

			//更新父节点的数量
			CalcParentQty(objParentTxt);
		}
	}
}

