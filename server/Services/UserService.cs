using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using server.Data;
using server.DTOs.User;
using server.DTOs.WorkoutDay;
using server.DTOs.WorkoutExercise;
using server.DTOs.WorkoutPlan;
using server.Models;

namespace server.Services
{
    public class UserService
    {
        private readonly RepGPTContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserService(RepGPTContext context, IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;

        }
        public async Task<List<UserReadDto>> GetAllAsync()
        {
            try
            {
                return await _context.Users
                    .Select(u => new UserReadDto
                    {
                        Id = u.Id,
                        Name = u.Name,
                        Email = u.Email,
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to fetch users", ex);
            }
        }
        public async Task<UserReadDto?> GetByIdAsync(Guid id)
        {
            return await _context.Users
                .Include(u => u.WorkoutPlan)
                    .ThenInclude(p => p.WorkoutDays)
                        .ThenInclude(d => d.WorkoutExercises)
                .Where(u => u.Id == id)
                .Select(u => new UserReadDto
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    WorkoutPlan = u.WorkoutPlan == null ? null : new WorkoutPlanReadDto
                    {
                        Id = u.WorkoutPlan.Id,
                        Name = u.WorkoutPlan.Name,
                        Goal = u.WorkoutPlan.Goal,
                        UserId = u.WorkoutPlan.UserId,
                        WorkoutDays = u.WorkoutPlan.WorkoutDays.Select(d => new WorkoutDayReadDto
                        {
                            Id = d.Id,
                            DayOfTheWeek = d.DayOfTheWeek,
                            Notes = d.Notes,
                            WorkoutExercises = d.WorkoutExercises.Select(e => new WorkoutExerciseReadDto
                            {
                                Id = e.Id,
                                Sets = e.Sets,
                                Reps = e.Reps,
                                TargetWeight = e.TargetWeight,
                                TargetTime = e.TargetTime
                            }).ToList()
                        }).ToList()
                    }
                })
                .FirstOrDefaultAsync();
        }


        public async Task<UserReadDto> CreateAsync(UserCreateDto dto)
        {
            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email
            };

            user.Password = _passwordHasher.HashPassword(user, dto.Password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new UserReadDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            };
        }

        public async Task<bool> UpdateAsync(int id, UserUpdateDto dto)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            user.Name = dto.Name;
            user.Email = dto.Email;

            if (!string.IsNullOrWhiteSpace(dto.Password)) // if a password is included, rehashes
            {
                user.Password = _passwordHasher.HashPassword(user, dto.Password);
            }

            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);
                if (user == null) return false;

                _context.Users.Remove(user);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Failed to delete user with id {id}", ex);
            }
        }

    }
}