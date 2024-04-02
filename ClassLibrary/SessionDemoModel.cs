using System;
using System.ComponentModel.DataAnnotations;

namespace ClassLibrary;

[Serializable]
public class SessionDemoModel
{
    [Display(Name = "Integer session item")]
    public int? IntSessionItem { get; set; }

    [Display(Name = "String session item")]
    public string StringSessionItem { get; set; }
}
