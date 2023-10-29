using HotelNetwork.DAL.Entities;
using HotelNetwork.Domain.Interfaces;
using HotelNetwork.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelNetwork.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HotelsController : HotelsControllerBase
    {
        public HotelsController(IHotelService hotelService)
        {
            HotelService = hotelService;
        }

        public IHotelService HotelService { get; }
    }
}
