function LockColumn(TableID, ColCount)
{
	var table = document.getElementById(TableID);
	var trs = table.getElementsByTagName("TR");
	
	for(var i=0;i<trs.length;i++)
	{
		var tr = trs.item(i);
		
		for(var j=0;j<ColCount;j++)
		{
			if (tr.cells[j])
			{
				tr.cells[j].className = "locked";
			}
		}
	}
}

function LockRow(TableID, RowCount)
{
	var table = document.getElementById(TableID);
	
	if (table.getElementsByTagName("TH").length == 0)
	{
		var trs = table.getElementsByTagName("TR");
		
		for(var i=0;i<RowCount;i++)
		{
			if ((i + 1) > trs.length) break;
			
			var tr = trs.item(i);
			
			for(var j=0;j<tr.cells.length;j++)
			{
				tr.cells[j].className = "th";
			}
		}
	}
}
