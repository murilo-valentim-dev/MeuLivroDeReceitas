﻿namespace MyRecipeBook.Domain.Repositories.User
{
    public interface IUserReadOnlyRepository
    {
        public Task<bool> ExistActiveUserWithWmail(string email);
    }
}
