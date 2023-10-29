﻿using Microsoft.AspNetCore.Mvc;
using HotelNetwork.DAL.Entities;
using HotelNetwork.Domain.Interfaces;

namespace HotelNetwork.Controllers
{
    [ApiController]
    [Route("api/[controller]")]// esta es la primera parte de la URL de esta API: URL = api/countries
    public class StatesController : Controller
    {
        private readonly IStateService _stateService;

        public StatesController(IStateService stateService)
        {
            _stateService = stateService;
        }
        // en un controlador los metodos cambian de nombre, y realmente se llaman ACCIONES (ACTIONS) - Si es una
        // api, se denomina ENDPOINT.
        // Todo Endpoint retorna un ActionResult, significa que retorna el resultado de una ACCION.

        [HttpGet, ActionName("Get")]
        [Route("GetAll")]// Aqui concateno la URL inicial: URL = api/countries/get
        public async Task<ActionResult<IEnumerable<State>>> GetStatesAsync()
        {
            var states = await _stateService.GetStatesAsync();// aqui estoy yebdo a mi capa de Domain para traer la lista de paises
            if (states == null || !states.Any()) // el metodo Any () significa si hay al menos un elemento.
                                                       // el metodo !Any() significa si no hay absoluta/ nada.
            {
                return NotFound();// NotFound = 404 Http Status Code
            }
            return Ok(states);// ok = 200 Http Status Code
        }

        [HttpPost, ActionName("Create")]
        [Route("Create")]
        public async Task<ActionResult> CreateStateAsync(Country state)
        {
            try
            {
                var createdCountry = await _stateService.CreateStateAsync(state);
                if (createdCountry == null)
                {
                    return NotFound();// NotFound = 484 Http Status Code
                }
                return Ok(createdState);// Retorne un 200 y el objeto Country
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("duplicate"))
                {
                    return Conflict(string.Format("El pais {0} ya existe.", state.Name));
                }
                return Conflict(ex.Message);
            }
        }

        [HttpGet, ActionName("Get")]
        [Route("GetById/{id}")]
        public async Task<ActionResult<IEnumerable<State>>> GetCountryByIdAsync(Guid id)
        {
            if (id == null) return BadRequest("Id es requerido!");

            var country = await _stateService.GetStateByIdAsync(id);

            if (country == null) return NotFound();// NotFound = 404 Http Status Code

            return Ok(country);// ok = 200 Http Status Code
        }

        [HttpGet, ActionName("GetByName")]
        [Route("GetByName/{name}")]
        public async Task<ActionResult<IEnumerable<State>>> GetStateByNameAsync(string name)
        {
            if (name == null) return BadRequest("Nombre del pais es requerido!");

            var state = await _stateService.GetStateByNameAsync(name);

            if (state == null) return NotFound();// NotFound = 404 Http Status Code

            return Ok(state);// ok = 200 Http Status Code
        }

        [HttpPut, ActionName("Edit")]// put es para editar
        [Route("Edit")]
        public async Task<ActionResult<State>> EditCountryAsync(State state)
        {
            try
            {
                var editedState= await _stateService.EditStateAsync(state);
               
                return Ok(editedState);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("duplicate"))
                    return Conflict(string.Format("{0} ya existe.", state.Name));
                
                return Conflict(ex.Message);
            }
        }

        [HttpDelete, ActionName("Delete")]
        [Route("Delete")]
        public async Task<ActionResult<State>> DeleteStateAsync(Guid id)
        {
           if (id == null) return BadRequest("Id es requerido!");

           var deletedState = await _stateService.DeleteStateAsync(id);

           if (deletedState == null) return NotFound("Estado/Departamento no encontrado");
           return Ok(deletedState);
        }
    }
}
