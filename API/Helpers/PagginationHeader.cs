using System;

namespace API.Helpers;

public class PagginationHeader(int currentpage,int itemsperpage,int totalPages,int totalitems)
{
    public int CurrentPage { get; set; }=currentpage;
    public int Itemsperpage { get; set;}=itemsperpage;
    public int Totalitems { get; set;} =totalitems;
    public int Totalpages { get; set;}=totalPages;

}
