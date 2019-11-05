using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MattEland.TestManager.Models
{
    public enum TestStatus
    {
        [Display(Name = "Not Run")]
        NotRun,
        Passing,
        Failed,
        Ignored
    }
}