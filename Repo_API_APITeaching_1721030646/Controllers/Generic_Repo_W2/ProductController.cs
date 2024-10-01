using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repo_API_1721030646.Repo.Generic_W2;
using System.Linq.Expressions;
using Repo_API_1721030646.Models;
using Repo_API_1721030646.DTO;

namespace Repo_API_1721030646.Controllers.Generic_Repo_W2
{
    [Route("APITeaching/Generic_Repo/Way_2/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        //Second approach (way 2) about Repository Patten in Powerpoint file
        private readonly IGenericRepo<Product> _ProductRepo;
        private readonly IMapper _mapper;
        public ProductController(IGenericRepo<Product> ProductRepo, IMapper mapper)
        {
            _ProductRepo = ProductRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<ProductDTO>> Get(int id)
        {
            var entity = await _ProductRepo.GetAsync(id);
            if (entity != null)
            {
                var dto = new ProductDTO();
                _mapper.Map(entity, dto);
                return Ok(dto);
            }
            else
                return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<Product>> GetFull(int id)
        {
            return await _ProductRepo.GetAsync(id);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetList()
        {
            var entityList = await _ProductRepo.GetListAsync();
            if (entityList != null)
            {
                var dtoList = new List<ProductDTO>();
                _mapper.Map(entityList, dtoList);
                return Ok(dtoList);
            }
            else
                return NoContent();
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var entity = _ProductRepo.GetAsync(id);
            if (entity != null)
            {
                var result = _ProductRepo.Delete(entity.Result);
                if (result > 0)
                    return Ok("Record is deleted");
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<ProductDTO>> Create(ProductDTO model)
        {
            Expression<Func<Product, int>> filter = (x => x.Id);
            // Get Max Id in table of Database --> set for model + 1
            model.Id = await _ProductRepo.MaxIdAsync(filter) + 1;

            //Mapp data model --> newModel
            var newModel = new Product();
            //newModel. = DateTime.Now;
            _mapper.Map(model, newModel);

            if (await _ProductRepo.CreateAsync(newModel) != null)
                return Ok(model);
            else
                return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult<ProductDTO>> Update(ProductDTO model)
        {
            var entity = await _ProductRepo.GetAsync(model.Id);
            if (entity != null)
            {
                _mapper.Map(model, entity);
                if (await _ProductRepo.UpdateAsync(entity) != null)
                    return Ok(model);
            }
            return NotFound();
        }
    }
}
