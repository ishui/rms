function CostBudgetSelectYm_GetControl(ClientID, objName)
{
	var obj = document.all(ClientID + ":" + objName);
	if (!obj)
		obj = document.all(ClientID + "_" + objName);
		
	return obj;
}

function CostBudgetSelectYm_ShowMonthClick(ClientID, sender)
{
	var chkShowMonth = CostBudgetSelectYm_GetControl(ClientID, "chkShowMonth");
	if (chkShowMonth)
	{
		var display = (chkShowMonth.checked)?"":"none";
		CostBudgetSelectYm_GetControl(ClientID, "spanMonth").style.display = display;
	}

	if ((sender) && (!sender.checked))
	{
		CostBudgetSelectYm_GetControl(ClientID, "btnGotoMonth").click();
	}
}

function CostBudgetSelectYm_Null()
{
	return true;
}

function CostBudgetSelectYm_GotoMonth(ClientID)
{
	var StartY = CostBudgetSelectYm_GetStartY(ClientID);
	var EndY = CostBudgetSelectYm_GetEndY(ClientID);
	
	if ((StartY != "") && (EndY != ""))
	{
		var txtMaxYearsBetween = CostBudgetSelectYm_GetControl(ClientID, "txtMaxYearsBetween");
		
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
	
	if (!CostBudgetSelectYm_MyOnClientPost()) return false;
	
	document.all.divHintLoad.style.display = '';
	return true;
}

function CostBudgetSelectYm_GetStartY(ClientID)
{
	return CostBudgetSelectYm_GetControl(ClientID, "dtMonthStart_t").value;
}

function CostBudgetSelectYm_GetEndY(ClientID)
{
	return CostBudgetSelectYm_GetControl(ClientID, "dtMonthEnd_t").value;
}