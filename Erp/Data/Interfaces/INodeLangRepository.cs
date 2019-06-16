using Erp.Data;
using Erp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Interfaces
{
    public interface INodeLangRepository : IRepository<NodeLangWorkflow, DataDbContext>
    {
        /*Task<List<WorkflowInstance>> GetRunningInstances(string WorkflowId);
        Task<int> GetRunningInstanceCount(string WorkflowId);
        Task AddRunningInstance(string WorkflowId, string InstanceId, string currentNode);
        Task<string> GetCurrentNode(string InstanceId);
        Task UpdateCurrentNode(string InstanceId, string NodeId);*/
        
    }
}
