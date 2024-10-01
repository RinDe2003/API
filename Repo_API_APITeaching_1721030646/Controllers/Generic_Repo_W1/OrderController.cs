using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repo_API_1721030646.DTO;
using Repo_API_1721030646.Models;
using Repo_API_1721030646.Repo.Generic_W1;
using System.Linq.Expressions;

namespace Repo_API_1721030646.Controllers.Generic_Repo_W1
{
    [Route("APITeaching/Generic_Repo/Way_1/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        //First approach (way 1) about Repository Patten in Powerpoint file
        private readonly IRepo<Order> _OrderService;
        private readonly IMapper _mapper;
        public OrderController(IRepo<Order> OrderService, IMapper mapper)
        {
            _OrderService = OrderService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDTO>> Get(int id)
        {
            var entity = await _OrderService.GetAsync(id);
            if (entity != null)
            {
                var model = new OrderDTO();
                _mapper.Map(entity, model);
                return Ok(model);
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<ActionResult<Order>> GetFull(int id)
        {
            // Returns the Order Entity with full information such as: Country, Province, District, Ward...
            return await _OrderService.GetAsync(id, false);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetList()
        {
            var entityList = await _OrderService.GetListAsync();
            if (entityList != null)
            {
                var dtoList = new List<OrderDTO>();
                _mapper.Map(entityList, dtoList);
                return Ok(dtoList);
            }
            return NoContent();
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var result = _OrderService.Delete(id);
            if (result == 1)
            {
                return Ok(new { success = true, message = "Record is deleted." });
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<OrderDTO>> Create(OrderDTO model)
        {
            // Get Max Id in table of Database --> set for model + 1
            model.Id = await _OrderService.MaxIdAsync(model.Id) + 1;

            //Mapp data model --> newModel
            var newModel = new Order();
            _mapper.Map(model, newModel);
            if (await _OrderService.CreateAsync(newModel) != null)
                return Ok(model);
            else
                return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult<OrderDTO>> Update(OrderDTO model)
        {
            if (_OrderService.CheckExists(model.Id))
            {
                var entity = new Order();
                _mapper.Map(model, entity);
                if (await _OrderService.UpdateAsync(entity) != null)
                    return Ok(model);
            }
            return NotFound();
        }
    }
}

