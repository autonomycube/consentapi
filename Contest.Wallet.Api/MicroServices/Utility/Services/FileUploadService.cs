﻿using AutoMapper;
using Contest.Wallet.Common.ApplicationMonitoring.Abstract;
using Contest.Wallet.Api.Utility.Services.Abstract;
using System;
using System.Threading.Tasks;

namespace Contest.Wallet.Api.Utility.Services
{
    public class FileUploadService : IFileUploadService
    {
        #region Private Variables

        private readonly IMapper _mapper;
        private readonly ILogger<FileUploadService> _logger;

        #endregion

        #region Constructor

        public FileUploadService(IMapper mapper,
            ILogger<FileUploadService> logger)
        {
            _mapper = mapper
                ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger
                ?? throw new ArgumentNullException(nameof(logger));
        }

        #endregion

        #region CRUD - R

        public async Task<string[]> Get()
        {
            return await Task.FromResult(new string[] { "Test Method" });
        }

        #endregion

    }
}