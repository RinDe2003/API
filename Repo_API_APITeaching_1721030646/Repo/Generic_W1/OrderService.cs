using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Repo_API_1721030646.Data;
using Repo_API_1721030646.Models;
using System.Linq.Expressions;
using Repo_API_1721030646.Repo.Generic_W1;
using Repo_API_1721030646.Models;

namespace Repo_API_1721030646.Repo.Generic_W1
{
    public class OrderService : IRepo<Order>
    {
        private readonly ApiteachingContext _context;
        private readonly IMapper _mapper;

        public OrderService(ApiteachingContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Order>> GetListAsync()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task<Order> GetAsync(int id, bool useDTO = true)
        {
            if (useDTO)
                return await _context.Orders.FindAsync(id);
            else
            {
                var entity = await _context.Orders
                                .Include(c => c.Customer)
                                .Include(e => e.Employee)
                                .Include(s => s.Ship)
                                .FirstOrDefaultAsync(a => a.Id == id);
                return entity!;
            }
        }

        public async Task<IEnumerable<Order>> SearchAsync(Expression<Func<Order, bool>> expression, bool useDTO = true)
        {
            if (useDTO)
                return await _context.Orders.Where(expression).ToListAsync();
            else
            {
                var list = await _context.Orders
                        .Include(c => c.Customer)
                        .Include(e => e.Employee)
                        .Include(s => s.Ship)
                        .Where(expression)
                        .Select(a => new Order
                        {
                            Id = a.Id,
                            CustomerId = a.CustomerId,
                            EmployeeId = a.EmployeeId,
                            OrderDate = a.OrderDate,
                            RequiredDate = a.RequiredDate,
                            ShippedDate = a.ShippedDate,
                            ShipId = a.ShipId,
                            Freight = a.Freight,
                            ShipAddress = a.ShipAddress,
                            Status = a.Status,
                            OrderDetails = a.OrderDetails,
                            Customer = a.Customer != null ? new Customer
                            {
                                Id = a.Customer.Id,
                                Code = a.Customer.Code,
                                CompanyName = a.Customer.CompanyName,
                                ContactName = a.Customer.ContactName,
                                ContactTitle = a.Customer.ContactTitle,
                                Phone = a.Customer.Phone,
                                AddressId = a.Customer.AddressId,
                                AccountId = a.Customer.AccountId,
                                Status = a.Customer.Status,
                                Account = a.Customer.Account,
                                Orders = null // Ngăn chặn truy vấn ngược
                            } : null,

                            Employee = a.Employee != null ? new Employee
                            {
                                Id = a.Employee.Id,
                                LastName = a.Employee.LastName,
                                FirstName = a.Employee.FirstName,
                                Title = a.Employee.Title,
                                BirthDate = a.Employee.BirthDate,
                                HireDate = a.Employee.HireDate,
                                Phone = a.Employee.Phone,
                                Photo = a.Employee.Photo,
                                PhotoPath = a.Employee.PhotoPath,
                                AddressId = a.Employee.AddressId,
                                AccountId = a.Employee.AccountId,
                                Status = a.Employee.Status,
                                Account = a.Employee.Account,
                                Orders = null // Đảm bảo không truy vấn ngược
                            } : null,

                            Ship = a.Ship != null ? new Shipper
                            {
                                Id = a.Ship.Id,
                                CompanyName = a.Ship.CompanyName,
                                Phone = a.Ship.Phone,
                                AddressId = a.Ship.AddressId,
                                Status = a.Ship.Status,
                                Orders = null //  Ngăn chặn truy vấn ngược
                            } : null,
                        })
                        .ToListAsync();
                return list;
            }
        }
        public async Task<Order> CreateAsync(Order entity)
        {
            _context.Orders.Add(entity);
            if (await _context.SaveChangesAsync() > 0)
                return entity;

            return null!;
        }

        public async Task<Order> UpdateAsync(Order entity)
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
            var entity = _context.Orders.Find(id);
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
            return _context.Orders.Any(e => e.Id == id);
        }

        public async Task<int> MaxIdAsync(int id)
        {
            return await _context.Orders.MaxAsync(x => x.Id);
        }

        public async Task<int> MinIdAsync(int id)
        {
            return await _context.Orders.MinAsync(x => x.Id);
        }
    }
}
