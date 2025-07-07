using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using server.Data;
using server.DTOs.PromptHistory;
using server.Models;

namespace server.Services
{
    public class PromptHistoryService
    {
        private readonly RepGPTContext _context;

        public PromptHistoryService(RepGPTContext context)
        {
            _context = context;
        }

        public async Task<List<PromptHistoryReadDto>> GetAllAsync()
        {
            try
            {
                return await _context.PromptHistorys
                    .Select(p => new PromptHistoryReadDto
                    {
                        Id = p.Id,
                        PromptText = p.PromptText,
                        ResponseText = p.ResponseText,
                        CreatedAt = p.CreatedAt
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to fetch prompt history", ex);
            }
        }

        public async Task<PromptHistoryReadDto?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.PromptHistorys
                    .Where(p => p.Id == id)
                    .Select(p => new PromptHistoryReadDto
                    {
                        Id = p.Id,
                        PromptText = p.PromptText,
                        ResponseText = p.ResponseText,
                        CreatedAt = p.CreatedAt
                    })
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Failed to fetch prompt history with id {id}", ex);
            }
        }

        public async Task<PromptHistoryReadDto> CreateAsync(PromptHistoryCreateDto dto)
        {
            var prompt = new PromptHistory
            {
                PromptText = dto.PromptText,
                ResponseText = dto.ResponseText,
                CreatedAt = dto.CreatedAt,
                UserId = dto.UserId
            };

            _context.PromptHistorys.Add(prompt);
            await _context.SaveChangesAsync();

            return new PromptHistoryReadDto
            {
                Id = prompt.Id,
                PromptText = prompt.PromptText,
                ResponseText = prompt.ResponseText,
                CreatedAt = prompt.CreatedAt
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var prompt = await _context.PromptHistorys.FindAsync(id);
                if (prompt == null) return false;

                _context.PromptHistorys.Remove(prompt);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Failed to delete prompt history with id {id}", ex);
            }
        }
    }
}
