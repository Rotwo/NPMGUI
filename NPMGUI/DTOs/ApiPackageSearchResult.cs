using System;
using System.Collections.Generic;

namespace NPMGUI.DTOs;

public class ApiPackageSearchResult
{
    public int Total { get; set; }
    public Result[] Results { get; set; }
}

public class Result
{
    public Package Package { get; set; }
    //public Score  Score { get; set; }
    public double SearchScore  { get; set; }   
}

public class Package
{
    public string Name { get; set; }
    public string Scope { get; set; }
    public string Version { get; set; }
    public string Description { get; set; }
    public string[] Keywords { get; set; }
    public DateTime date { get; set; }
    public Dictionary<string, string> Links { get; set; }
}