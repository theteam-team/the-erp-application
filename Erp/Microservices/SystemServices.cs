using Erp.BackgroundServices;
using Erp.BackgroundServices.Entities;
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
        public IServiceProvider Services { get; }

        public SystemServices( CommonNeeds common, IActionDescriptorCollectionProvider actionDescriptorCollectionProvider, TaskResponseQueue responseQueue)
        {
            _common = common;
            _actionDescriptorCollectionProvider = actionDescriptorCollectionProvider;
          
            _responseQueue = responseQueue;
        }

        public async Task findServiceAsync(BpmTask bpmTask, IServiceScope serviceScope)
        {
            var controllers = _actionDescriptorCollectionProvider
                .ActionDescriptors
                .Items
                .OfType<ControllerActionDescriptor>()
                .Where(a => a.ActionName == bpmTask.TaskName);          
            foreach (var item in controllers)
            {
                try
                {
                    var id = new ClaimsIdentity();
                    string httpMethode = item.ActionConstraints.OfType<HttpMethodActionConstraint>().SingleOrDefault().HttpMethods.FirstOrDefault();
                    MethodInfo  methodInfo = item.ControllerTypeInfo.GetMethod(bpmTask.TaskName);                  
                    List<ParameterInfo> parameters = methodInfo.GetParameters().ToList();           
                    List<object> methodeParam = new List<object>();
                    if (parameters.Count == bpmTask.TaskParam.Length)
                    {
                        Console.WriteLine("here");
                        if (httpMethode != "POST")
                        {
                            for (int i = 0; i < parameters.Count; ++i)
                            {
                                var taskParameterobj = bpmTask.TaskParam[i];
                                string json = JsonConvert.SerializeObject(taskParameterobj);
                                JObject ob = JObject.Parse(json);                                                     
                                methodeParam.Add(Convert.ChangeType(ob.Properties().FirstOrDefault().Value, parameters[i].ParameterType));

                            }
                        }
                        else
                        {
                            for (int i = 0; i < parameters.Count; ++i)
                            {
                                var taskParameterobj = bpmTask.TaskParam[i];
                                string json = JsonConvert.SerializeObject(taskParameterobj);
                                JObject ob = JObject.Parse(json);
                                Type mytype = parameters[0].ParameterType;
                                if (ob.Properties().Count()> 1)
                                {                                   
                                    methodeParam.Add(JsonConvert.DeserializeObject(json, mytype));
                                }
                                else
                                {
                                    methodeParam.Add(Convert.ChangeType(ob.Properties().FirstOrDefault().Value, parameters[i].ParameterType));
                                }
                                
                            }
                        }
                        var controller = (ControllerBase)serviceScope.ServiceProvider.GetService(item.ControllerTypeInfo);
                        var properties = controller.GetType().GetProperties().ToList();
                        Console.WriteLine("Count " + properties.Count);
                        foreach (var property in properties)
                        {
                            try
                            {
                                var proType = property.GetValue(controller).GetType();
                                if (proType != null)
                                {
                                    MethodInfo _methodInfo = proType.GetMethod("setConnectionString");
                                    if (_methodInfo != null)
                                    {
                                        Console.WriteLine(property.GetValue(controller));
                                        var result = _methodInfo.Invoke(property.GetValue(controller), new object[] {bpmTask.databaseName });
                                    }
                                }
                            }
                            catch
                            {
                                continue;
                            }
                        }


                        Console.WriteLine("here");
                        var resutlt = methodInfo.Invoke(controller,  methodeParam.ToArray() );
                    }
            
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    continue;
                    throw;
                }
            }

        }
       /* public async void AddCustomer(BpmTask bpmTask)
        {
            Console.WriteLine("Hereee");
            //await findServiceAsync(bpmTask);
            string json = JsonConvert.SerializeObject(bpmTask.TaskParam);
            //string json = ((JObject[])bpmTask.TaskParam)[0].ToString(Formatting.None);
            Customer customer = JsonConvert.DeserializeObject<List<Customer>>(json)[0];
            _crmApiController._customeRepository.setConnectionString(bpmTask.databaseName);
            var result = await _crmApiController.AddCustomer(customer);
            try
            {
                OkObjectResult x = (OkObjectResult)result.Result;
                BpmResponse bpmResponse = new BpmResponse
                {
                    databaseName = bpmTask.databaseName,
                    status = "success",
                    WorkflowName = bpmTask.WorkflowName,
                    instanceID = bpmTask.instanceID,
                    taskID = bpmTask.taskID,
                    Type = bpmTask.Type,
                    ResponseParam = x.Value

                };
                _responseQueue.QueueExection(bpmResponse);
                Console.WriteLine((string)x.Value);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
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

        }*/

    }
}
