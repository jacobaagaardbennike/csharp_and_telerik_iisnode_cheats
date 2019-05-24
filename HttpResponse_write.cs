protected void btnAdelingEnkelt_Click(object sender, EventArgs e)
{
       	HttpResponse _Response = Prepare("Filnavn");
	string sb = GetString(dateStart.SelectedDate.Value, dateEnd.SelectedDate.Value);
    	_Response.BinaryWrite(Encoding.UTF8.GetPreamble());
      	_Response.Write(sb);
       	_Response.End();
}

private HttpResponse Prepare(string filename)
{
	string attachment = "attachment; filename=" + filename + "-" + DateTime.Now.ToString("dd-MM-yyyy") + ".csv";
	HttpResponse _Response = HttpContext.Current.Response;
	_Response.Clear();
	_Response.ClearHeaders();
	_Response.ClearContent();
	_Response.AddHeader("content-disposition", attachment);
	_Response.ContentType = "text/csv";
	_Response.AddHeader("Pragma", "public");
	_Response.ContentType = "text/csv; charset=UTF-8";
	_Response.Charset = "UTF-8";
	_Response.ContentEncoding = Encoding.UTF8;
	return _Response;
}

public string GetString(DateTime start, DateTime end)
{
	StringBuilder csv = new StringBuilder("Column1;Column2;Column3\n");
	IQueryable<IEntity> entities = Repo.Query.Where(or => or.DeliveryDate >= start && or.DeliveryDate <= end && or.Department == null);
	foreach (Ienityt e in entities)
	{
		csv.Append(e.column1).Append(delimiter);
		csv.Append(e.column2).Append(delimiter);
		csv.Append(e.column3).Append(newline);
}
return csv.ToString();
}
