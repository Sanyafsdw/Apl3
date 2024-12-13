using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Data.Models
{
    public class Order
    {
        [BindNever]
        public int OrderId { get; set; }
        [Display(Name = "Имя")]
        [StringLength(50)]
        [Required(ErrorMessage = "Вы не ввели имя")]
        public string Name { get; set; }
        [Display(Name = "Фамилия")]
        [StringLength(50)]
        [Required(ErrorMessage = "Вы не ввели фамилию")]
        public string Surname { get; set; }
        [Display(Name = "Адрес")]
        [StringLength(200)]
        [Required(ErrorMessage = "Вы не ввели адрес")]
        public string Adress { get; set; }
        [Display(Name = "Номер телефона")]
        [StringLength(11)]
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Вы не ввели номер")]
        public string Phone { get; set; }
        [Display(Name = "E-mail")]
        [StringLength(50)]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Вы не ввели почту")]
        public string Email { get; set; }
        [BindNever]
        [ScaffoldColumn(false)]
        public DateTime OrderTime { get; set; }

        public List<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();


    }
}
