﻿using Core.DTOs;

namespace Core.Interfaces
{
    public interface IAdvertisementService
    {
        Task<IEnumerable<GetAdvertisementDTO>> GetAll();
        Task<GetAdvertisementDTO?> GetById(int id);
        Task<IEnumerable<GetAdvertisementDTO>> GetByCategoryId(int categoryId);
        Task<IEnumerable<GetAdvertisementDTO>> GetByUserId(string userId);
        Task<IEnumerable<GetAdvertisementDTO>> GetByCategoryName(string categoryName);
        Task Create(CreateAdvertisementDTO advertisment);
        Task Edit(int advertismentId, CreateAdvertisementDTO advertisment);
        Task Delete(int id);
        Task<IEnumerable<GetAdvertisementDTO>> Search(string query);
        Task<IEnumerable<GetAdvertisementDTO>> SearchByCategory(string query, string categoryName);
    }
}
