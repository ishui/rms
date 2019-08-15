function DropYearInit(slt)
{
	if (!slt)
		return;
	
	DropYearFill(slt, slt.defaultValue);
}

function DropYearChange(slt)
{
	if (!slt)
		return;
	
	DropYearFill(slt, slt.value);
}

function DropYearFill(slt, defaultValue)
{
	if (!slt)
		return;
		
	var allowNull = slt.allowNull;
	
	var YearCount = 6;
	var year;
	var iYear = (new Date()).getFullYear();
	
	if (defaultValue && (defaultValue != ""))
	{
		if (defaultValue == "today")
		{
			defaultValue = iYear;
		}

		iYear = parseInt(defaultValue);
	}
	
	var l = slt.options.length;
	for (var i = l-1;i>=0;i--)
	{
		slt.options[i] = null;
	}
	
	if ((allowNull) && (allowNull == "1"))
	{
		var item = document.createElement("option");
		slt.options.add(item);
		item.innerText = "";
		item.value = "";
	}
	
	for(var i = -YearCount;i<=YearCount;i++)
	{
		year = iYear + i;
		var item = document.createElement("option");
 		slt.options.add(item);
		item.innerText = year;
		item.value = year;

		if(year == defaultValue) 
			item.selected =true;
	}
}

