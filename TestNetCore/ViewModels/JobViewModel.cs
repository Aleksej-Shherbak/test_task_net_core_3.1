using System.ComponentModel.DataAnnotations;

namespace TestNetCore.ViewModels
{
    public class JobViewModel
    {    
        [Required(ErrorMessage = "Заголовок должен быть обязательно передан")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Описание должно быть передано")]
        public string Description { get; set; }
    }
}