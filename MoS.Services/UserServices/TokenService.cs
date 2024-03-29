﻿using System;
using static MoS.Models.Constants.Enums.Exception;

namespace MoS.Services.UserServices
{
    public class TokenService
	{
		private readonly ITokenService _repository;

		public TokenService(ITokenService tokenService)
		{
			_repository = tokenService;
		}

		public class TokenProps
		{
			public Guid Id { get; set; }
			public string Role { get; set; }
		}

		public class TokenInfo
		{
			public string Token { get; set; }
		}

		public interface ITokenService
		{
			string BuildToken(TokenProps info);
			void ExtractToken(TokenInfo token, Action<TokenProps> onSuccess, Action<Guid> onFail);
		}

		public string BuildToken(TokenProps info)
		{
			return _repository.BuildToken(info);
		}

		public void ExtractToken(TokenInfo token, Action<TokenProps> onSuccess, Action<Guid> onFail)
		{
			_repository.ExtractToken(token, onSuccess, onFail);
		}
	}
}
