using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repo_API_GeneralCatalog_1721030646.DTO;
using Repo_API_GeneralCatalog_1721030646.Models;
using Repo_API_1721030646.Repo.Generic_W2;
using System.Linq.Expressions;

namespace Repo_API_GeneralCatalog_1721030646.Controllers.Generic_Repo_W2
{
    [Route("APITeaching/Generic_Repo/Way_2/[controller]/[action]")]
    [ApiController]
    public class FolkController : ControllerBase
    {
        //Second approach (way 2) about Repository Patten in Powerpoint file
        private readonly IGenericRepo<Folk> _FolkRepo;
        private readonly IMapper _mapper;
        public FolkController(IGenericRepo<Folk> FolkRepo, IMapper mapper)
        {
            _FolkRepo = FolkRepo;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<FolkDTO>> Get(int id)
        {
            var entity = await _FolkRepo.GetAsync(id);
            if (entity != null)
            {
                var dto = new FolkDTO();
                _mapper.Map(entity, dto);
                return Ok(dto);
            }
            else
                return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<Folk>> GetFull(int id)
        {
            return await _FolkRepo.GetAsync(id);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FolkDTO>>> GetList()
        {
            var entityList = await _FolkRepo.GetListAsync();
            if (entityList != null)
            {
                var dtoList = new List<FolkDTO>();
                _mapper.Map(entityList, dtoList);
                return Ok(dtoList);
            }
            else
                return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FolkDTO>>> Search(string txtSearch)
        {
            Expression<Func<Folk, bool>> filter = a => a.Status != -1 && a.Name!.Contains(txtSearch);
            var entityList = await _FolkRepo.SearchAsync(filter);
            if (entityList != null)
            {
                var dtoList = new List<FolkDTO>();
                _mapper.Map(entityList, dtoList);
                return Ok(dtoList);
            }
            else
                return NoContent();
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var entity = _FolkRepo.GetAsync(id);
            if (entity != null)
            {
                var result = _FolkRepo.Delete(entity.Result);
                if (result > 0)
                    return Ok("Record is deleted");
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<FolkDTO>> Create(FolkDTO model)
        {
            Expression<Func<Folk, int>> filter = x => x.Id;
            model.Id = await _FolkRepo.MaxIdAsync(filter) + 1;

            var newModel = new Folk();
            _mapper.Map(model, newModel);

            if (await _FolkRepo.CreateAsync(newModel) != null)
                return Ok(model);
            else
                return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult<FolkDTO>> Update(FolkDTO model)
        {
            var entity = await _FolkRepo.GetAsync(model.Id);
            if (entity != null)
            {
                _mapper.Map(model, entity);
                if (await _FolkRepo.UpdateAsync(entity) != null)
                    return Ok(model);
            }
            return NotFound();
        }
    }
}
