using Ganss.XSS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using VeterinaryClinic.Common;
using VeterinaryClinic.Data.Models;
using VeterinaryClinic.Data.Models.Enumerations;
using VeterinaryClinic.Services.Data;
using VeterinaryClinic.Web.ViewModels.ChatMessages;

namespace VeterinaryClinic.Web.Areas.Vet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesApiController : Controller
    {
        private readonly IChatMessagesService chatMessagesService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IOwnersService ownersService;
        private readonly IVetsService vetsService;
            





        public MessagesApiController(IChatMessagesService chatMessagesService, UserManager<ApplicationUser> userManager, IOwnersService ownersService, IVetsService vetsService)
        {
            this.chatMessagesService = chatMessagesService;
            this.userManager = userManager;
            this.ownersService = ownersService;
            this.vetsService = vetsService;
        }

        [HttpPost]
        [Route("/api/Messages")]
        public async Task<IActionResult> Post(SendChatMessageInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.NoContent();
            }

            var sanitizer = new HtmlSanitizer();
            var message = sanitizer.Sanitize(model.Message);
            
            if (this.User.IsInRole(GlobalConstants.OwnerRoleName))
            {
                var senderRoleName = RoleName.Owner;
                var receiverRoleName = RoleName.Vet;
                //var userSenderId= this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                //var userSenderId = await this.userManager.FindByIdAsync(model.CallerId);
                var ownerIdSender = this.ownersService.GetOwnerId(model.CallerId);
                var vetReceiverId =this.vetsService.GetVetId(model.UserId);
                
                await this.chatMessagesService.CreateAsync(senderRoleName, receiverRoleName,ownerIdSender,vetReceiverId,message);
                //return this.Json(chatMessage);
                return this.Ok();
            }

            var senderRoleNameVet = RoleName.Vet;
            var receiverRoleNameOwner = RoleName.Owner;
            //var userSenderId= this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            //var userSenderId = await this.userManager.FindByIdAsync(model.CallerId);
            var ownerIdReceiver = this.ownersService.GetOwnerId(model.UserId);
            var vetSenderId = this.vetsService.GetVetId(model.CallerId);

            await this.chatMessagesService.CreateAsync(senderRoleNameVet, receiverRoleNameOwner, ownerIdReceiver, vetSenderId, message);
            
            //return this.Json(chat);
            return this.Ok();
        }

        [HttpGet]
        [Route("/api/Messages")]
        public async Task<IActionResult> Get(string recieverId, string senderId)
        {
            // var model = new ChatViewModel();
            // model.Messages = await this.messagesService.GetMessagesAsync<MessageViewModel>(senderId, recieverId);

            //return this.Json(model);
            return this.NoContent();
        }
    }
}
