using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Erp.Data;
using Erp.Data.Entities;
using Erp.Hubs;
using Erp.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Erp.Repository
{
    public class NodeLangRepository : Repository<NodeLangWorkflow, DataDbContext> , INodeLangRepository
    {
        private readonly DataDbContext _dataDbContext;
        private readonly AccountDbContext _accountDbContext;
        private readonly Management _management;

        public NodeLangRepository(IConfiguration config, ILogger<NodeLangRepository> ilogger, IHttpContextAccessor httpContextAccessor, IHubContext<NotificationHub> hubContext, AccountDbContext accountDbContext, Management management, DataDbContext datadbContext, UserManager<ApplicationUser> userManager)
            : base(config, ilogger, httpContextAccessor, management, datadbContext, accountDbContext, userManager)
        {
            _dataDbContext = datadbContext;
            _accountDbContext = accountDbContext;
            _management = management;
        }

        //public async updateNumOfInstance()
        /*public async Task AddRunningInstance(string WorkflowId, string InstanceId, string currentNode)
        {
            await Task.Run(() => InitiateConnection());
            var workflow = _dataDbContext.NodeLangWorkflow.Where(w => w.Id == WorkflowId).First();
            if (workflow != null)
            {
                if (workflow.WorkflowInstances == null)
                    workflow.WorkflowInstances = new List<WorkflowInstance>();
                workflow.WorkflowInstances.Add(new WorkflowInstance
                {
                    InstanceId = InstanceId,
                    CurrentNodeId = currentNode,
                    NodeLangWorkflow = workflow
                });
                _dataDbContext.NodeLangWorkflow.Update(workflow);
                _dataDbContext.SaveChanges();
            }
            else
            {
                Console.WriteLine(Guid.Parse(WorkflowId));
            }
            
        }*/

        /*public async Task<string> GetCurrentNode(string InstanceId)
        {
            await Task.Run(() => InitiateConnection());
            var cur = _dataDbContext.WorkflowInstances.Where(wf => wf.InstanceId == InstanceId).First();
            return cur.CurrentNodeId;
        }*/

        /*public async Task<int> GetRunningInstanceCount(string WorkflowId)
        {
            await Task.Run(() => InitiateConnection());
            var instances =_dataDbContext.WorkflowInstances.Where(wf => wf.NodeLangWorkflow.Id == WorkflowId).ToList();
            return instances.Count;
        }

        public async Task<List<WorkflowInstance>> GetRunningInstances(string WorkflowId)
        {
            await Task.Run(() => InitiateConnection());
            var instances = _dataDbContext.WorkflowInstances.Where(wf => wf.NodeLangWorkflow.Id == WorkflowId).ToList();
            return instances;
        }

        public async Task UpdateCurrentNode(string InstanceId, string NodeId)
        {
            await Task.Run(() => InitiateConnection());
            var instance = _dataDbContext.WorkflowInstances.Where(ins => ins.InstanceId == InstanceId).FirstOrDefault();
            if (instance != null)
            {
                instance.CurrentNodeId = NodeId;
                _dataDbContext.WorkflowInstances.Update(instance);
                _dataDbContext.SaveChanges();
            }
            

        }*/
    }
}
