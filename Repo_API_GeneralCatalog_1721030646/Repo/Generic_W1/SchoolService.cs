using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Repo_API_GeneralCatalog_1721030646.Data;
using Repo_API_GeneralCatalog_1721030646.Models;
using Repo_API_1721030646.Repo.Generic_W1;

namespace Repo_API_GeneralCatalog_1721030646.Repo.Generic_W1
{
    public class SchoolService : IRepo<School>
    {
        private readonly GeneralCatalogContext _context;
        private readonly IMapper _mapper;

        public SchoolService(GeneralCatalogContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<School>> GetListAsync()
        {
            return await _context.Schools.ToListAsync();
        }

        public async Task<School> GetAsync(int id, bool useDTO = true)
        {
            if (useDTO)
                return await _context.Schools.FindAsync(id);
            else
            {
                var entity = await _context.Schools
                                .FirstOrDefaultAsync(a => a.Id == id);
                return entity!;
            }
        }

        public async Task<IEnumerable<School>> SearchAsync(Expression<Func<School, bool>> expression, bool useDTO = true)
        {
            if (useDTO)
                return await _context.Schools.Where(expression).ToListAsync();
            else
            {
                var list = await _context.Schools
                        .Where(expression)
                        .Select(a => new School
                        {
                            Id = a.Id,
                            SchoolCode = a.SchoolCode,
                            SchoolLevel = a.SchoolLevel,
                            CountryId = a.CountryId,
                            ProvinceId = a.ProvinceId,
                            Name = a.Name,
                            NameEn = a.NameEn,
                            Status = a.Status,
                            Remark = a.Remark,
                            CreatedAt = a.CreatedAt,
                            CreatedBy = a.CreatedBy,
                            UpdatedAt = a.UpdatedAt,
                            UpdatedBy = a.UpdatedBy,    
                            Timer = a.Timer
                        })
                        .ToListAsync();
                return list;
            }
        }
        public async Task<School> CreateAsync(School entity)
        {
            _context.Schools.Add(entity);
            if (await _context.SaveChangesAsync() > 0)
                return entity;

            return null!;
        }

        public async Task<School> UpdateAsync(School entity)
        {
            if (CheckExists(entity.Id))
            {
                _context.Entry(entity).State = EntityState.Modified;
                if (await _context.SaveChangesAsync() > 0)
                    return entity;
            }
            return null!;
        }

        public int Delete(int id)
        {
            var entity = _context.Schools.Find(id);
            if (entity != null)
            {
                _context.Remove(entity);
                if (_context.SaveChanges() > 0)
                    return 1;
            }
            return 0;
        }

        public bool CheckExists(int id)
        {
            return _context.Schools.Any(e => e.Id == id);
        }

        public async Task<int> MaxIdAsync(int id)
        {
            return await _context.Schools.MaxAsync(x => x.Id);
        }

        public async Task<int> MinIdAsync(int id)
        {
            return await _context.Schools.MinAsync(x => x.Id);
        }
    }
}
