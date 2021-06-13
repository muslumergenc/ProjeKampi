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
            RuleFor(x => x.AdminRole).NotEmpty().WithMessage("Rol Seçmek Zorundasınız.");
            RuleFor(x => x.AdminUserName).MinimumLength(3).WithMessage("En Az 3 Karakter girişi yapalısınız !");
            RuleFor(x => x.AdminUserName).MaximumLength(50).WithMessage("Max 50 Karakter girişi yapalısınız !");
        }
    }
}
