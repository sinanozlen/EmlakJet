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
    public class ContactAddressManager : IContactAddressService
    {
        private readonly IContactAddressDal _contactAddressDal;

        public ContactAddressManager(IContactAddressDal contactAddressDal)
        {
            _contactAddressDal = contactAddressDal;
        }

        public void TAdd(ContactAddress entity)
        {
            _contactAddressDal.Add(entity);
        }

        public void TDelete(ContactAddress entity)
        {
            _contactAddressDal.Delete(entity);
        }

        public ContactAddress TGetbyID(int ID)
        {
           var values= _contactAddressDal.GetbyID(ID);
            return values;

        }

        public List<ContactAddress> TGetListAll()
        {
           var values = _contactAddressDal.GetListAll();
            return values;
        }

        public void TUpdate(ContactAddress entity)
        {
            _contactAddressDal.Update(entity);
        }
    }
}
