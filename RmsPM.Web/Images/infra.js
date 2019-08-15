function InfraSetNumericEditValue(id, value)
{
	igedit_getById(id).setValue(value);

/*
	var obj = document.getElementById(id);
	var obj_p = document.getElementById(id + "_p");
	var obj_show = document.getElementById(id + "_t");

	obj.value = value;
	obj_p.value = value;

	if (value == "")
	{
		obj_show.value = value;
	}
	else
	{
		obj_show.value = formatNumber(value, "#,###.00");
	}
*/
}

function InfraSetNumericEditVisible(id, isVisible)
{
	var objTable = document.getElementById("igtxt" + id);

	if (isVisible)
	{
		objTable.style.display = "";
	}
	else
	{
		objTable.style.display = "none";
	}
}
