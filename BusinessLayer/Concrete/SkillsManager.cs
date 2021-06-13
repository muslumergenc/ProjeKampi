using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class SkillsManager : ISkillService
    {
        private readonly ISkillDal _skillDal;

        public SkillsManager(ISkillDal skillDal)
        {
            _skillDal = skillDal;
        }

        public void SkillAddBL(Skills skill)
        {
            _skillDal.Insert(skill);
        }

        public Skills GetById(int id)
        {
            return _skillDal.Get(x => x.SkillId == id);
        }

        public List<Skills> GetList()
        {
            return _skillDal.List();
        }

        public void SkillsDelete(Skills skill)
        {
            _skillDal.Delete(skill);
        }

        public void SkillsUpdate(Skills skill)
        {
            _skillDal.Update(skill);
        }
    }
}
