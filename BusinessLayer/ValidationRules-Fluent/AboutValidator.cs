using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules_Fluent
{
    public class AboutValidator : AbstractValidator<About>
    {
        public AboutValidator()
        {
            RuleFor(x => x.AboutDetails1).NotEmpty().WithMessage("Başlık Boş Geçilemez");
            RuleFor(x => x.AboutDetails2).NotEmpty().WithMessage("Açıklama Alanı Boş Geçilemez.");
            RuleFor(x => x.AboutImage1).NotEmpty().WithMessage("Lütfen Görsel Ekleyiniz!");
            RuleFor(x => x.AboutDetails1).MinimumLength(3).WithMessage("Min 3 Karakter girişi yapalısınız !");
        }
    }
}
