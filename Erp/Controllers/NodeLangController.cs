using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Erp.Data;
using Erp.Data.Entities;
using Erp.Hubs;
using Erp.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Erp.Controllers
{
    [Authorize(Roles = "Adminstrator")]
    public class NodeLangController : Controller
    {
        [HttpGet("monitoring")]
        public IActionResult Monitoring()
        {
            return View();
        }

        private readonly UserManager<ApplicationUser> _usermanager;
        private readonly IHubContext<MonitoringHub> _hubcontext;
        private readonly MonitoringHub _monitoringHub;
        private INodeLangRepository _nodeLangRepository;

        public NodeLangController(INodeLangRepository nodeLangRepository, UserManager<ApplicationUser> userManager, IHubContext<MonitoringHub> hubcontext)
        {
            _usermanager = userManager;
            _hubcontext = hubcontext;
            _nodeLangRepository = nodeLangRepository;
        }
        [HttpPost("UploadWorkFlow")]
        public async Task<IActionResult> UploadWorkFlow(IFormFile file)
        {
            //try
            //{
                bool isCopied = false;
                //1 check if the file length is greater than 0 bytes 
                if (file.Length > 0)
                {
                    string fileName = file.FileName;
                    //2 Get the extension of the file
                    string extension = Path.GetExtension(fileName);
                    //3 check the file extension as png
                    if (extension == ".xml")
                    {
                        string workFlowStr = null;
                        string WorkFlowName = null;
                        using (Stream stream = file.OpenReadStream())
                        {
                            BinaryReader br = new BinaryReader(stream);
                            byte[] fileBytes = br.ReadBytes((int)stream.Length);
                            workFlowStr = Encoding.ASCII.GetString(fileBytes);

                            XmlDocument xmlDoc = new XmlDocument();
                            stream.Position = 0;
                            xmlDoc.Load(stream);
                            var element = xmlDoc.GetElementsByTagName("nodes")[0];
                            if (element == null)
                            {
                                throw new Exception("This xml file is not supported");
                            }
                            WorkFlowName = element.Attributes["name"].Value;
                            Console.WriteLine(WorkFlowName);
                        }
                        //4 set the path where file will be copied
                        string filePath = Path.GetFullPath(
                            Path.Combine(Directory.GetCurrentDirectory(),
                                                        "wwwroot/WorkFlows"));
                        //5 copy the file to the path
                        using (var fileStream = new FileStream(
                            Path.Combine(filePath, fileName),
                                           FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                            isCopied = true;


                        }
                        if (isCopied)
                        {
                            Guid id = Guid.NewGuid();
                            string idstr = id.ToString();
                            Console.WriteLine(idstr);
                         _nodeLangRepository.User = User;
                            await _nodeLangRepository.Create(new NodeLangWorkflow
                                {
                                    Id = id,
                                    Name = WorkFlowName,
                                    WorkFlow = workFlowStr,
                                    RuningInstances = 0
                                }
                            );


                        await _hubcontext.Clients.All.SendAsync("updateDeployList", idstr, WorkFlowName);                      
                        }
                    }
                    else
                    {
                        throw new Exception("File must be either .xml");
                    }
                }
            return RedirectToAction("Monitoring");

        }
    }
}