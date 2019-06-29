using Erp.BackgroundServices;
using Erp.BackgroundServices.Entities;
using Erp.Controllers;
using Erp.Data;
using Erp.Data.Entities;
using Erp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Erp.Microservices
{
    public class SystemServices
    {
        //private CrmApiController _crmApiController;
        private TaskResponseQueue _responseQueue;
        private readonly CommonNeeds _common;
        private readonly IActionDescriptorCollectionProvider _actionDescriptorCollectionProvider;
        private readonly AccountDbContext _accountDbContext;
        private readonly CrmApiController _crmApiController;
        public IServiceProvider Services { get; }

        public SystemServices(AccountDbContext accountDbContext ,CommonNeeds common, IActionDescriptorCollectionProvider actionDescriptorCollectionProvider, TaskResponseQueue responseQueue)
        {
            _common = common;
            _actionDescriptorCollectionProvider = actionDescriptorCollectionProvider;
            _accountDbContext = accountDbContext;
            _responseQueue = responseQueue;
        }

        public async Task findServiceAsync(BpmTask bpmTask, IServiceScope serviceScope)
        {
            Console.WriteLine(bpmTask.TaskName);
            var controllers = _actionDescriptorCollectionProvider
                .ActionDescriptors
                .Items
                .OfType<ControllerActionDescriptor>()
                .Where(a => a.ActionName == bpmTask.TaskName);          
            foreach (var item in controllers)
            {
                Console.WriteLine(bpmTask.TaskName);
                try
                {
                    var id = new ClaimsIdentity();
                    string httpMethode = item.ActionConstraints.OfType<HttpMethodActionConstraint>().SingleOrDefault().HttpMethods.FirstOrDefault();
                    MethodInfo  methodInfo = item.ControllerTypeInfo.GetMethod(bpmTask.TaskName);                  
                    List<ParameterInfo> parameters = methodInfo.GetParameters().ToList();           
                    List<object> methodeParam = new List<object>();
                    if (parameters.Count == bpmTask.TaskParam.Length)
                    {
                        for (int i = 0; i < parameters.Count; ++i)
                        {
                            var taskParameterobj = bpmTask.TaskParam[i];
                            string json = JsonConvert.SerializeObject(taskParameterobj);
                            JObject jsonOb = JObject.Parse(json);
                            var jsonObProperties = jsonOb.Properties();
                            Type methodParamtype = parameters[i].ParameterType;
                            bool isClass = checkedClassType(methodParamtype);
                            if (isClass)
                            {

                                methodeParam.Add(JsonConvert.DeserializeObject(json, methodParamtype));
                            }
                            else if(jsonObProperties.Count() == 1)
                            {
                                methodeParam.Add(Convert.ChangeType(jsonObProperties.FirstOrDefault().Value, parameters[i].ParameterType));                              
                            }
                        }                      
                        var controller = (ControllerBase)serviceScope.ServiceProvider.GetService(item.ControllerTypeInfo);
                        var contProperties = controller.GetType().GetProperties().ToList();                         
                        foreach (var property in contProperties)
                        {
                            try
                            {
                                var proType = property.GetValue(controller).GetType();
                                if (proType != null)
                                {
                                    MethodInfo _methodInfo = proType.GetMethod("setConnectionString");
                                    if (_methodInfo != null)
                                    {                                       
                                        var result = _methodInfo.Invoke(property.GetValue(controller), new object[] {bpmTask.databaseName });
                                    }
                                }
                            }
                            catch(Exception ex)
                            {
                               
                                continue;
                            }
                        }
                        if (bpmTask.Type == "UserTask")
                        {
                            var IsBpmEngine = methodeParam[0].GetType().GetProperty("IsBpmEngine");
                            IsBpmEngine.SetValue(methodeParam[0], true);
                            var InvokerId = methodeParam[0].GetType().GetProperty("InvokerId");
                            InvokerId.SetValue(methodeParam[0], bpmTask.InvokerId);
                        }
                        var resutlt = await (dynamic)methodInfo.Invoke(controller, methodeParam.ToArray());
                        if (bpmTask.Type != "UserTask")
                        {
                            var prop = resutlt.GetType().GetProperty("Result");
                            if (prop != null)
                            {
                                var action = prop.GetValue(resutlt);
                                createResponse((ActionResult)action, bpmTask);
                            }
                            else
                            {
                                createResponse((ActionResult)(resutlt), bpmTask);
                            }
                        }

                    }
                    else
                    {
                        createFailedResponse("Parameter Don't match service Parameter", bpmTask);
                    }
            
                }
                catch (Exception ex)
                {
                    createFailedResponse("Problem has occurred while Trying to execute this Task", bpmTask);


                }
            }

        }

      
      
        private void createResponse(ActionResult result, BpmTask bpmTask)
        {
            try
            {
                OkObjectResult x = (OkObjectResult)result;
                createSuccessResponse(new object[] { x.Value }, bpmTask);
            }
            catch (Exception ex)
            {
                createFailedResponse(ex.Message, bpmTask);
                Console.WriteLine(ex.Message);
                
            }
        }

        private void createSuccessResponse(object[] param, BpmTask bpmTask)
        {
            BpmResponse bpmResponse = new BpmResponse
            {
                databaseName = bpmTask.databaseName,
                status = "success",
                WorkflowName = bpmTask.WorkflowName,
                instanceID = bpmTask.instanceID,
                taskID = bpmTask.taskID,
                Type = bpmTask.Type,
                ResponseParam = param

            };
            _responseQueue.QueueExection(bpmResponse);
        
        }
        private void createFailedResponse(string message, BpmTask bpmTask)
        {
            BpmResponse bpmResponse = new BpmResponse
            {
                databaseName = bpmTask.databaseName,
                status = "Failed",
                WorkflowName = bpmTask.WorkflowName,
                instanceID = bpmTask.instanceID,
                taskID = bpmTask.taskID,
                Type = bpmTask.Type,


            };
            _responseQueue.QueueExection(bpmResponse);
        }

        private bool checkedClassType(Type parmtype)
        {


            if (parmtype.IsValueType || parmtype == typeof(string))
            {
                return false;
            }
            else
                return true;
        }



       //public async void AddCustomer(BpmTask bpmTask)
       // {
       //     Console.WriteLine("Hereee");
       //     //await findServiceAsync(bpmTask);
       //     string json = JsonConvert.SerializeObject(bpmTask.TaskParam);
       //     //string json = ((JObject[])bpmTask.TaskParam)[0].ToString(Formatting.None);
       //     Customer customer = JsonConvert.DeserializeObject<List<Customer>>(json)[0];
       //     _crmApiController._customeRepository.setConnectionString(bpmTask.databaseName);
       //     var result = await _crmApiController.AddCustomer(customer);
       //     ActionResult action = result.Result;
            

       // }

    }
}
