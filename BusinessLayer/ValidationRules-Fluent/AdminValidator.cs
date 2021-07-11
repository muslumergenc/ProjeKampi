using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules_Fluent
{
    public class AdminValidator : AbstractValidator<Admin>
    {
        public AdminValidator() 
        {
            RuleFor(x => x.AdminUserName).NotEmpty().WithMessage("Kullanıcı Adı Boş Geçilemez");
        }
    }
}
