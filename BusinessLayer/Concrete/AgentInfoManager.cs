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
    public class AgentInfoManager : IAgentInfoService
    {
        private readonly IAgentInfoDal _agentInfoDal;

        public AgentInfoManager(IAgentInfoDal agentInfoDal)
        {
            _agentInfoDal = agentInfoDal;
        }

        public void TAdd(AgentInfo entity)
        {
            _agentInfoDal.Add(entity);
            
        }

        public void TDelete(AgentInfo entity)
        {
           _agentInfoDal.Delete(entity);
        }

        public AgentInfo TGetbyID(int ID)
        {
            var values= _agentInfoDal.GetbyID(ID);
            return values;
        }

        public List<AgentInfo> TGetListAll()
        {
           var values= _agentInfoDal.GetListAll();
            return values;

        }

        public void TUpdate(AgentInfo entity)
        {
            _agentInfoDal.Update(entity);
        }
    }
}
