function CostBudgetSelectMonth_GetControl(ClientID, objName)
{
	var obj = document.all(ClientID + ":" + objName);
	if (!obj)
		obj = document.all(ClientID + "_" + objName);
		
	return obj;
}

function CostBudgetSelectMonth_ShowMonthClick(ClientID, sender)
{
	var chkShowMonth = CostBudgetSelectMonth_GetControl(ClientID, "chkShowMonth");
	if (chkShowMonth)
	{
		var display = (chkShowMonth.checked)?"":"none";
		CostBudgetSelectMonth_GetControl(ClientID, "spanMonth").style.display = display;
	}

	if ((sender) && (!sender.checked))
	{
		CostBudgetSelectMonth_GetControl(ClientID, "btnGotoMonth").click();
	}
}

function CostBudgetSelectMonth_Null()
{
	return true;
}

function CostBudgetSelectMonth_GotoMonth(ClientID)
{
	var StartY = CostBudgetSelectMonth_GetStartY(ClientID);
	var EndY = CostBudgetSelectMonth_GetEndY(ClientID);
	
	if ((StartY != "") && (EndY != ""))
	{
		var txtMaxYearsBetween = CostBudgetSelectMonth_GetControl(ClientID, "txtMaxYearsBetween");
		
		if (txtMaxYearsBetween.value != "")
		{
			var MaxYearsBetween = parseInt(txtMaxYearsBetween.value);
			if ((parseInt(EndY) - parseInt(StartY)) > MaxYearsBetween)
			{
				alert("年跨度不能超过 " + MaxYearsBetween + " 年");
				return false;
			}
		}
	}
	
	if (!CostBudgetSelectMonth_MyOnClientPost()) return false;
	
	document.all.divHintLoad.style.display = '';
	return true;
}

function CostBudgetSelectMonth_GetStartY(ClientID)
{
	return CostBudgetSelectMonth_GetControl(ClientID, "dtMonthStart_t").value;
}

function CostBudgetSelectMonth_GetEndY(ClientID)
{
	return CostBudgetSelectMonth_GetControl(ClientID, "dtMonthEnd_t").value;
}