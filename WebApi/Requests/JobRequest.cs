using System.ComponentModel.DataAnnotations;

namespace WebApi.Requests
{
    public class JobRequest
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Заголовок должен быть обязательно передан")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Описание должно быть передано")]
        public string Description { get; set; }
    }
}