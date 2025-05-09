using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agricultural.Core.Models;
using Agricultural.Repo.Data;
using Agricultural.Repo.Repositories;
using AutoMapper;
using Agricultural.Core.DTOs;
using Microsoft.EntityFrameworkCore;


namespace Agricultural.Serv.Services
{
    public class PlantsService
    {
        private readonly IGenericRepository<PlantsInfo> _plantsRepository;
        private readonly PlanetContext _context;

        public PlantsService(IGenericRepository<PlantsInfo> plantsRepository, PlanetContext context)
        {
            _plantsRepository = plantsRepository;
            _context = context;
        }

        public async Task<IEnumerable<PlantsInfo>> GetAllPlantsAsync()
        {
            return await _context.PlantsInfo
                .Include(p => p.PlantImages)
                .ToListAsync();
        }

        public async Task<(IEnumerable<PlantsInfo> plants, IEnumerable<string> suggestions)> SearchPlantsAsync(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return (new List<PlantsInfo>(), new List<string>());
            }
            // for decoding the query 
            query = System.Web.HttpUtility.UrlDecode(query);


            // جلب الاقتراحات (أسماء تبدأ بالـ query)
            var suggestions = await _context.PlantsInfo
                .Where(p => p.PlantName.StartsWith(query))
                .Select(p => p.PlantName)
                .Take(3) // عدد الاقتراحات 3
                .ToListAsync();

            // جلب النتايج الكاملة (نباتات تحتوي على الـ query)
            var plants = await _context.PlantsInfo
                .Include(p => p.PlantImages)
                .Where(p => p.PlantName.Contains(query))
                .ToListAsync();

            return (plants, suggestions);
        }
    }
}
