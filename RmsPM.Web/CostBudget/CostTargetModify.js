var headCount = 2;

//�������ǰ
function MoneyFocus(sender)
{
	//��ԭ����ֵ
	CBTree_RevertMoneyObject(sender);
}

//��������
function MoneyBlur(sender, isAutoCalc)
{
	//��ʽ�����
	CBTree_FormatMoneyObject(sender);
	
	//ͬ����ʾ���ݺ�¼������
	CBTree_SynSpanTxt(sender);

	if (isAutoCalc)
	{
		//��������
		CalcQtyByMoney(sender);
		
		//���¸��ڵ�Ľ��
		CalcParentMoney(sender);
	}
}

//���������
function PriceBlur(sender, isAutoCalc)
{
	//��ʽ�����
	CBTree_FormatMoneyObject(sender);
	
	//ͬ����ʾ���ݺ�¼������
	CBTree_SynSpanTxt(sender);
	
	if (isAutoCalc)
	{
		//������ = ���� * ����
		CalcMoneyByPrice(sender);
	}
}

//���������
function QtyBlur(sender, isAutoCalc)
{
	//��ʽ������
	CBTree_FormatQtyObject(sender);
	
	//ͬ����ʾ���ݺ�¼������
	CBTree_SynSpanTxt(sender);

	if (isAutoCalc)
	{
		//������ = ���� * ����
		CalcMoneyByQty(sender);

		//���¸��ڵ������
		CalcParentQty(sender);
	}
}

//չ��ʱ��Ԥ����ֻ�����۵�ʱ��Ԥ�����¼��
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
    
	if (key == "R_0") //�ϼ���ʼ�ղ���¼��
	{
		display_span = "";
		display_txt = "none";

		//����˵��
		CBTree_SetObjectDisplay(document.all("dgList:_ctl" + r + ":txtDescription"), display_txt);
	}
	else
	{
		//�Ƿ��¼��	
		if (expand_result == "1")  //�۵� -> չ��
		{
			display_span = "";
			display_txt = "none";
		}
		else  //չ�� -> �۵�
		{
			display_span = "none";
			display_txt = "";
		}
	}
	
	//����/��ʾ����
	CBTree_SetObjectDisplay(document.all("dgList__ctl" + r + "_spanPrice"), display_span);
	CBTree_SetObjectDisplay(document.all("dgList:_ctl" + r + ":txtPrice"), display_txt);

	//����/��ʾ����
	CBTree_SetObjectDisplay(document.all("dgList__ctl" + r + "_spanQty"), display_span);
	CBTree_SetObjectDisplay(document.all("dgList:_ctl" + r + ":txtQty"), display_txt);

	//����/��ʾԤ���ܶ�
	CBTree_SetObjectDisplay(document.all("dgList__ctl" + r + "_spanBudgetMoney"), display_span);
	CBTree_SetObjectDisplay(document.all("dgList:_ctl" + r + ":txtBudgetMoney"), display_txt);

	//����Ԥ��ƻ�����Ƿ��¼��
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

//չ���������ڵ��ȱʡ�Ƿ�չ�����ԣ�
function ExpandTreeByNodeDefaultExpand()
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
				var objImg = CBTree_GetSubChildNodeById(objRow.childNodes[0], m_imgId + CBTree_GetTreeNodeKey(objRow));
				ImgExpand(objImg, -1);
//				objImg.click();
				CBTree_ImgExpandClickOnTimer(objImg);
			}
		}
	}
}

//��� = ���� * ����
function GetMoneyByPriceQty(price, qty)
{
	var money = Math.round(ConvertFloat(price) * ConvertFloat(qty) * 10000) / 10000;
	return money;
}

//���� = ��� / ����
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

//���� = ��� / ����
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

//���۱仯ʱ������
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

		//���¸��ڵ�Ľ��
		CalcParentMoney(objMoney);
	}
}

//�����仯ʱ������
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

		//���¸��ڵ�Ľ��
		CalcParentMoney(objMoney);
	}
}

//���仯ʱ���㵥��
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

//���仯ʱ��������
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

//�����仯ʱ���㵥��
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

//ȡ�ӽڵ���ܽ��
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

//���¸��ڵ�Ľ��
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

			//���㵥��
			CalcPriceByMoney(objParentTxt);
		
			//���¸��ڵ�Ľ��
			CalcParentMoney(objParentTxt);
		}
	}
}

//���¸��ڵ������
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

			//���㵥��
			CalcPriceByQty(objParentTxt);

			//���¸��ڵ������
			CalcParentQty(objParentTxt);
		}
	}
}

