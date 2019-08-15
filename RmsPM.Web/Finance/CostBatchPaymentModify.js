//��ʼ����
function CostBatchPaymentModify_InitTree(treeId, imgPlusFileName, imgMinusFileName, headCount)
{
	var objTree = document.all(treeId);
	var rowCount = objTree.rows.length;
	
	for(var i=headCount;i<rowCount;i++)
	{
		var objRow = objTree.rows[i];
		
		//����ֻ��Ҷ����¼��
		CostBatchPaymentModify_SetItemRowInput(objRow);
	}
}

/*************************************/
/*            ���               */
/*************************************/

//��ʽ�������
function FormatMoney(val)
{
	var s = "";
	var num;
	
	if (val == "") return s;
	
	var num = ConvertFloat(val);
	if (isNaN(num)) return s;
	
	//0����ʾ
	if (num == 0) return s;
	
	s = formatNumber(num, "#,###.00");
	
	return s;
}

//��ʽ�������
function FormatMoneyObject(obj)
{
	if (obj)
	{
		obj.value = FormatMoney(obj.value);
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
			
//			MoneyBlur(objParentTxt, true);
			
			MoneyBlur(objParentTxt, false);

			//���¸��ڵ�Ľ��
			CalcParentMoney(objParentTxt);
		}
	}
}

function CostBatchPaymentModify_SetItemRowInput(objRow)
{
	var r = parseInt(objRow.RowIndex);

	var display_txt;
	var display_span;

	//�Ƿ��¼��	
	if ((objRow.ChildCount) && (objRow.ChildCount != "0")) //��Ҷ���
	{
		display_span = "";
		display_txt = "none";
	}
	else  //Ҷ���
	{
		display_span = "none";
		display_txt = "";
	}
	
	//����/��ʾ���
	CBTree_SetObjectDisplay(document.all("dgList__ctl" + r + "_spanItemMoney"), display_span);
	CBTree_SetObjectDisplay(document.all("dgList:_ctl" + r + ":txtItemMoney"), display_txt);
}

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
	FormatMoneyObject(sender);
	
	//ͬ����ʾ���ݺ�¼������
	CBTree_SynSpanTxt(sender);

	if (isAutoCalc)
	{
		//���¸��ڵ�Ľ��
		CalcParentMoney(sender);
		
		//�����ܶ�
		var TotalMoney = GetChildSumMoney("", "txtItemMoney");
		document.all("txtMoney").value = FormatMoney(TotalMoney);
		
	}
}

