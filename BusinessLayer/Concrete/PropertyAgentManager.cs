using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class PropertyAgentManager : IPropertyAgentService
    {
        private readonly IPropertyAgentDal _propertyAgentDal;

        public PropertyAgentManager(IPropertyAgentDal propertyAgentDal)
        {
            _propertyAgentDal = propertyAgentDal;
        }

        public void TAdd(PropertyAgent entity)
        {
            _propertyAgentDal.Add(entity);
        }

        public void TDelete(PropertyAgent entity)
        {
           _propertyAgentDal.Delete(entity);
        }

        public PropertyAgent TGetbyID(int ID)
        {
           var values = _propertyAgentDal.GetbyID(ID);
            return values;
        }

        public List<PropertyAgent> TGetListAll()
        {
            var values = _propertyAgentDal.GetListAll();
            return values;
        }

        public void TUpdate(PropertyAgent entity)
        {
            _propertyAgentDal.Update(entity);
        }
    }
}
