using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface ISkillService
    {
        List<Skills> GetList();
        void SkillAddBL(Skills skill);
        Skills GetById(int id);
        void SkillsDelete(Skills skill);
        void SkillsUpdate(Skills skill);
    }
}
