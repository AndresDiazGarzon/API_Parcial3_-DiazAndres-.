using HotelNetwork.DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HotelNetwork.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class HotelsControllerBase
    {

        // en un controlador los metodos cambian de nombre, y realmente se llaman ACCIONES (ACTIONS) - Si es una
        // api, se denomina ENDPOINT.
        // Todo Endpoint retorna un ActionResult, significa que retorna el resultado de una ACCION.

        [HttpGet, ActionName("Get")]
        [Route("GetAll")]// Aqui concateno la URL inicial: URL = api/hotels/get
        public async Task<ActionResult<IEnumerable<Hotel>>> GetHotelsAsync(void hotels)
        {
            if (hotels == null || !hotels.Any()) // el metodo Any () significa si hay al menos un elemento.
                                                 // el metodo !Any() significa si no hay absoluta/ nada.
            {
                return NotFound();// NotFound = 404 Http Status Code
            }
            return Ok(hotels);// ok = 200 Http Status Code
        }
    }
}