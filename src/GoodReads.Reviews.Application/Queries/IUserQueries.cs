﻿using GoodReads.Reviews.Application.DTO;
using GoodReads.Reviews.Domain.Repositories;

namespace GoodReads.Reviews.Application.Queries
{
    public interface IUserQueries
    {
        Task<UserDTO> GetByIdAsync(Guid id);
    }

    public class UserQueries : IUserQueries
    {
        private readonly IUserRepository _userRepository;

        public UserQueries(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDTO> GetByIdAsync(Guid id)
        {
            var user = await _userRepository.GetById(id.ToString());

            if (user is null)
                return null;

            return new UserDTO(user.Id, user.Name);
        }
    }
}
