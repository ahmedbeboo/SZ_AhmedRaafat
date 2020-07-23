using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Net_AhmedRaafat.BL;
using Net_AhmedRaafat_Entities;
using Net_AhmedRaafat_Repository;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Net_AhmedRaafat.BackgroundService
{
    public class EmailTask : IHostedService
    {
        private Task _executingTask;
        private readonly CancellationTokenSource _stoppingCts = new CancellationTokenSource();

        private readonly IServiceScopeFactory _serviceScopeFactory;
        IHubContext<NotifyHub, ITypedHubClient> _hubContext;
        private IHttpContextAccessor _httpContextAccessor;




        public EmailTask(IServiceScopeFactory serviceScopeFactory, IHubContext<NotifyHub, ITypedHubClient> hubContext, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _hubContext = hubContext;
            _serviceScopeFactory = serviceScopeFactory;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            // throw new NotImplementedException();
            _executingTask = ExecuteAsync(_stoppingCts.Token);
            // If the task is completed then return it,
            // this will bubble cancellation and failure to the caller
            if (_executingTask.IsCompleted)
            {
                return _executingTask;
            }

            // Otherwise it's running
            return Task.CompletedTask;

        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            // throw new NotImplementedException();
            return Task.CompletedTask;
        }

        protected virtual async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //stoppingToken.Register(() =>
            // _logger.LogDebug($" GracePeriod background task is stopping."));

            do
            {
                await Process();
                //await Task.Delay(60000, stoppingToken); //60 seconds delay
                await Task.Delay(43200000, stoppingToken); //12 hours delay
                
            }
            while (!stoppingToken.IsCancellationRequested);
        }




        protected async Task Process()
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var _toDoRepository = scope.ServiceProvider.GetService<IBaseRepository<ToDo>>();
                var _emailSender = scope.ServiceProvider.GetService<IEmailSender>();
                var _userManager = scope.ServiceProvider.GetService<UserManager<ApplicationUser>>();

                var OverTimeToDO = _toDoRepository.Where(i => i.CreatedDate < DateTime.Now && ! i.isNotifiedTimeOver).Result.ToList();
                foreach (var item in OverTimeToDO)
                {
                    var userModel = _userManager.Users.Where(i => i.Id == item.userId && !i.isDeleted).FirstOrDefault();

                    string htmlString = $"<html> <body> Your ToDo note with text:[ {item.text} ] ... has a time over .</body> </html>";
                    var result = _emailSender.SendEmail("ToDo note", htmlString, "tt436209@gmail.com", "cornetelevated", userModel.Email, userModel.firstName + " " + userModel.lastName,
                                            null, null, null, null, null,
                                            null, 0, null
                                            );

                    if (result)
                    {
                        var entity = item;
                        entity.Id = item.Id;
                        entity.isNotifiedTimeOver = true;
                        _toDoRepository.Update(entity);
                    }



                    try
                    {
                        string msg = "Your ToDo note with text:[" + item.text + " ] ... has a time over";
                        var user = _httpContextAccessor.HttpContext.Request.HttpContext.User.Identity.Name;

                        //await _hubContext.Clients.All.BroadcastMessage("Time Over", msg);
                        await _hubContext.Clients.User(user).BroadcastMessage("Time Over", msg);

                    }
                    catch (Exception e)
                    {
                    }

                    await Task.Delay(2500); //0.25 seconds delay


                }

            }

            
            

        }



    }

}
