	function ChkSelectAll(objCheck, ischecked)
	{
		var SelectedCount = 0;
		
		if (!objCheck)
			return SelectedCount;
			
		if(objCheck[0])
		{
			l = objCheck.length;
		
			for(var i=0;i<l;i++)
			{
//				objCheck[i].checked=ischecked;
				
				if (objCheck[i].checked != ischecked)
				{
					objCheck[i].click();
				}
				
				if (objCheck[i].checked)
				{
					SelectedCount = SelectedCount + 1;
				}
			}
		}
		else
		{
			if(objCheck)
			{
//				objCheck.checked=ischecked;

				if (objCheck.checked != ischecked)
				{
					objCheck.click();
				}
				
				if (objCheck.checked)
				{
					SelectedCount = SelectedCount + 1;
				}
			}
		}

		return SelectedCount;
	}

	//get selected checkbox value, seprate:,
	function ChkGetSelected(objCheck)
	{
		var SelectedCount = 0;
		var s = "";

		if (!objCheck) return s;
			
		if(objCheck[0])
		{
			for(var i=0;i<objCheck.length;i++)
			{
				if (objCheck[i].checked)
				{
					if (s != "")
					{
						s = s + ",";
					}
					s = s + objCheck[i].value;
					
					SelectedCount = SelectedCount + 1;
				}
			}
		}
		else
		{
			if(objCheck)
			{
				if (objCheck.checked)
				{
					s = objCheck.value;
					SelectedCount = SelectedCount + 1;
				}
			}
		}

		return s;
	}

	//get selected checkbox key value and diaplay value, seprate:,
	function ChkGetSelectedCodeName(objCheck)
	{
		var SelectedCount = 0;
		var code = "";
		var name = "";
		var arr = new Array();
		arr.push("");
		arr.push("");

		if (!objCheck) return arr;
			
		if(objCheck[0])
		{
			for(var i=0;i<objCheck.length;i++)
			{
				if (objCheck[i].checked)
				{
					if (code != "")
					{
						code = code + ",";
						name = name + ",";
					}
					code = code + objCheck[i].value;
					name = name + objCheck[i].title;
					
					SelectedCount = SelectedCount + 1;
				}
			}
		}
		else
		{
			if(objCheck)
			{
				if (objCheck.checked)
				{
					code = objCheck.value;
					name = objCheck.title;
					SelectedCount = SelectedCount + 1;
				}
			}
		}

		if (SelectedCount > 0)
		{
			arr[0] = code;
			arr[1] = name;
		}
		
		return arr;
	}

function ChkSelectRow(index, objCheck, objTable, selectedClass, unselectedClass)
{
	var i = parseInt(index);
	
	if (!objTable) return;
	if (!objTable.rows(i)) return;

	if (objCheck.checked)
	{
		objTable.rows(i).className = selectedClass;
	}
	else
	{
		objTable.rows(i).className = unselectedClass;
	}
	
//Form1.tbMain.className 
//alert(document.all.dgList.rows.length);
}

function InfraChkSelectRow(index, objCheck, table_name, selectedClass, unselectedClass)
{
	var i = parseInt(index);
	
	var trId = table_name + "r_" + index;
	var objTr = document.all(trId);
	
	if (!objTr) return;

	if (objCheck.checked)
	{
		objTr.className = selectedClass;
	}
	else
	{
		objTr.className = unselectedClass;
	}
}
