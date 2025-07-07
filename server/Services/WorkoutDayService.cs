using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using server.Data;
using server.DTOs.WorkoutDay;
using server.Models;

namespace server.Services
{
    public class WorkoutDayService
    {
        private readonly RepGPTContext _context;

        public WorkoutDayService(RepGPTContext context)
        {
            _context = context;
        }

        public async Task<List<WorkoutDayReadDto>> GetAllAsync()
        {
            return await _context.WorkoutDays
                .Select(d => new WorkoutDayReadDto
                {
                    Id = d.Id,
                    DayOfTheWeek = d.DayOfTheWeek,
                    Notes = d.Notes
                })
                .ToListAsync();
        }

        public async Task<WorkoutDayReadDto?> GetByIdAsync(int id)
        {
            return await _context.WorkoutDays
                .Where(d => d.Id == id)
                .Select(d => new WorkoutDayReadDto
                {
                    Id = d.Id,
                    DayOfTheWeek = d.DayOfTheWeek,
                    Notes = d.Notes
                })
                .FirstOrDefaultAsync();
        }

        public async Task<WorkoutDayReadDto> CreateAsync(WorkoutDayCreateDto dto)
        {
            var day = new WorkoutDay
            {
                DayOfTheWeek = dto.DayOfTheWeek,
                Notes = dto.Notes,
                WorkoutPlanId = dto.WorkoutPlanId
            };

            _context.WorkoutDays.Add(day);
            await _context.SaveChangesAsync();

            return new WorkoutDayReadDto
            {
                Id = day.Id,
                DayOfTheWeek = day.DayOfTheWeek,
                Notes = day.Notes
            };
        }

        public async Task<bool> UpdateAsync(int id, WorkoutDayUpdateDto dto)
        {
            var day = await _context.WorkoutDays.FindAsync(id);
            if (day == null) return false;

            day.DayOfTheWeek = dto.DayOfTheWeek;
            day.Notes = dto.Notes;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var day = await _context.WorkoutDays.FindAsync(id);
            if (day == null) return false;

            _context.WorkoutDays.Remove(day);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
