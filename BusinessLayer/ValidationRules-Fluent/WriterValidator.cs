﻿using EntityLayer.Concrete;
using EntityLayer.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules_FluentValidation
{
    public class WriterValidator : AbstractValidator<WriterEditDto>
    {
        public WriterValidator()
        {
            RuleFor(x => x.WriterName).NotEmpty().WithMessage("Yazar Adını Boş Geçemezsiniz.");
            RuleFor(x => x.WriterSurname).NotEmpty().WithMessage("Yazar Soyadını Boş Geçemezsiniz.");
            RuleFor(x => x.WriterAbout).NotEmpty().WithMessage("Yazar Hakkındayı Boş Geçemezsiniz.");
            RuleFor(x => x.WriterPassword).NotEmpty().WithMessage("Şifreyi Boş Geçemezsiniz");
            RuleFor(x => x.WriterTitle).NotEmpty().WithMessage("Yazar Ünvanı Boş Geçemezsiniz.");
            RuleFor(x => x.WriterSurname).MinimumLength(2).WithMessage("En Az 3 Karakter Girişi Yapın.");
            RuleFor(x => x.WriterSurname).MaximumLength(20).WithMessage("En Fazla 20 Karakter Girişi Yapın.");
            RuleFor(x => x.WriterAbout).Must(MustBeA).WithMessage("Hakkında Kısmında Mutlaka A Harfi Olmalıdır.");
        }
        private bool MustBeA(string arg)
        {
            return arg.Contains("A");
        }
    }
}
