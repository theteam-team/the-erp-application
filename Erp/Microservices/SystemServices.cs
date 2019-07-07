using Erp.BackgroundServices;
using Erp.BackgroundServices.Entities;
using Erp.Controllers;
using Erp.Data;
using Erp.Data.Entities;
using Erp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<SystemServices> _logger;
        private readonly Emergency _common;
        private readonly IActionDescriptorCollectionProvider _actionDescriptorCollectionProvider;
        private readonly AccountDbContext _accountDbContext;
        private readonly CrmApiController _crmApiController;
        public IServiceProvider Services { get; }

        public SystemServices(ILogger<SystemServices> logger, AccountDbContext accountDbContext ,Emergency common, IActionDescriptorCollectionProvider actionDescriptorCollectionProvider, TaskResponseQueue responseQueue)
        {
            _logger = logger;
            _common = common;
            _actionDescriptorCollectionProvider = actionDescriptorCollectionProvider;
            _accountDbContext = accountDbContext;
            _responseQueue = responseQueue;
        }

        public async Task findServiceAsync(BpmTask bpmTask, IServiceScope serviceScope)
        {
            var services = _actionDescriptorCollectionProvider
                .ActionDescriptors
                .Items
                .OfType<ControllerActionDescriptor>()
                .Where(a => a.ActionName == bpmTask.TaskName);
            if (services.Count() > 0)
            {
                foreach (var item in services)
                {
                    try
                    {
                        var id = new ClaimsIdentity();
                        string httpMethode = item.ActionConstraints.OfType<HttpMethodActionConstraint>().SingleOrDefault().HttpMethods.FirstOrDefault();
                        MethodInfo methodInfo = item.ControllerTypeInfo.GetMethod(bpmTask.TaskName);
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

                                try
                                {
                                    if (isClass)
                                    {

                                        methodeParam.Add(JsonConvert.DeserializeObject(json, methodParamtype));
                                    }        
                                    else if (jsonObProperties.Count() == 1)
                                    {
                                   
                                            methodeParam.Add(Convert.ChangeType(jsonObProperties.FirstOrDefault().Value, parameters[i].ParameterType));

                                  
                                    }
                                }
                                catch (Exception)
                                {
                                    createFailedResponse("Cannont convert paramenter" + json + "to the task paramerter " + i, bpmTask);
                                    return;
                                }
                            }
                            var controller = (ControllerBase)serviceScope.ServiceProvider.GetService(item.ControllerTypeInfo);
                            var httpContext = new DefaultHttpContext(); 
                            httpContext.Request.Headers["organization"] = bpmTask.databaseName; 

                            var controllerContext = new ControllerContext()
                            {
                                HttpContext = httpContext,
                            };
                            controller.ControllerContext = controllerContext;
                            var contProperties = controller.GetType().GetProperties().ToList();
                            foreach (var property in contProperties)
                            {
                                try
                                {
                                    var proType = property.GetValue(controller).GetType();
                                    if (proType != null)
                                    {
                                        Console.WriteLine(property.Name);
                                        MethodInfo _methodInfo = proType.GetMethod("setConnectionString");
                                        if (_methodInfo != null)
                                        {
                                            var result = _methodInfo.Invoke(property.GetValue(controller), new object[] { bpmTask.databaseName });
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {

                                    continue;
                                }
                            }
                            if (bpmTask.type == "userTask")
                            {
                                var IsBpmEngine = methodeParam[0].GetType().GetProperty("IsBpmEngine");
                                IsBpmEngine.SetValue(methodeParam[0], bpmTask.IsBpm);
                                var InvokerId = methodeParam[0].GetType().GetProperty("InvokerId");
                                InvokerId.SetValue(methodeParam[0], bpmTask.InvokerId);
                            }
                            _logger.LogInformation("Invoking " + bpmTask.TaskName);
                            var resutlt = await (dynamic)methodInfo.Invoke(controller, methodeParam.ToArray());
                            
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
                        else
                        {
                            createFailedResponse("Parameter Don't match service Parameter", bpmTask);
                        }

                    }
                    catch (Exception ex)
                    {

                        createFailedResponse("Problem has occurred while Trying to execute this Task", bpmTask);
                        throw;

                    }
                }
            }
            else
            {
                createFailedResponse("This Task Does not exist", bpmTask);
            }

        }

      
      
        private void createResponse(ActionResult result, BpmTask bpmTask)
        {
            try
            {
                try
                {
                    OkResult x = (OkResult)result;
                    if (bpmTask.type != "userTask")
                        createSuccessResponse(new object[] {}, bpmTask);
                }
                catch (Exception)
                {

                    OkObjectResult x = (OkObjectResult)result;
                    if(bpmTask.type != "userTask")
                         createSuccessResponse(new object[] { x.Value }, bpmTask);
                }
            }
            catch (Exception ex)
            {
                BadRequestObjectResult x = (BadRequestObjectResult)result;

                createFailedResponse(x.Value, bpmTask);
               
                
            }
        }

        private void createSuccessResponse(object[] param, BpmTask bpmTask)
        {
            ServiceResponse Response = new ServiceResponse
            {
                Organization = bpmTask.databaseName,
                InvokerId = bpmTask.InvokerId,
                status = "success",    
                Type = bpmTask.type,
                IsBpm = bpmTask.IsBpm,
                parameters = param

            };
            _responseQueue.QueueExection(Response);
        
        }
        private void createFailedResponse(object message, BpmTask bpmTask)
        {
            ServiceResponse bpmResponse = new ServiceResponse
            {
                Organization = bpmTask.databaseName,
                status = "Failed",
                IsBpm = bpmTask.IsBpm,
                InvokerId = bpmTask.InvokerId,
                Type = bpmTask.type,
                parameters = message,
                
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
