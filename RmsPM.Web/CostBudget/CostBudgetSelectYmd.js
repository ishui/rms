function CostBudgetSelectYmd_GetControl(ClientID, objName)
{
	var obj = document.all(ClientID + ":" + objName);
	if (!obj)
		obj = document.all(ClientID + "_" + objName);
		
	return obj;
}

function CostBudgetSelectYmd_ShowMonthClick(ClientID, sender)
{
	var chkShowMonth = CostBudgetSelectYmd_GetControl(ClientID, "chkShowMonth");
	if (chkShowMonth)
	{
		var display = (chkShowMonth.checked)?"":"none";
		CostBudgetSelectYmd_GetControl(ClientID, "spanMonth").style.display = display;
	}

	if ((sender) && (!sender.checked))
	{
		CostBudgetSelectYmd_GetControl(ClientID, "btnGotoMonth").click();
	}
}

function CostBudgetSelectYmd_Null()
{
	return true;
}

function CostBudgetSelectYmd_GotoMonth(ClientID)
{
	var StartY = CostBudgetSelectYmd_GetStartY(ClientID);
	var EndY = CostBudgetSelectYmd_GetEndY(ClientID);
	
	if ((StartY != "") && (EndY != ""))
	{
		var txtMaxYearsBetween = CostBudgetSelectYmd_GetControl(ClientID, "txtMaxYearsBetween");
		
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
	
	if (!CostBudgetSelectYmd_MyOnClientPost()) return false;
	
	document.all.divHintLoad.style.display = '';
	return true;
}

function CostBudgetSelectYmd_GetStartY(ClientID)
{
	return CostBudgetSelectYmd_GetControl(ClientID, "dtMonthStart_t").value;
}

function CostBudgetSelectYmd_GetEndY(ClientID)
{
	return CostBudgetSelectYmd_GetControl(ClientID, "dtMonthEnd_t").value;
}