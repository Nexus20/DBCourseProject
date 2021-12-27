﻿using CourseProject.BLL.DataHandlers;
using CourseProject.BLL.FilterModels;
using CourseProject.DAL.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace CourseProject.BLL.PipelineBuilders {
    public class CarSelectionPipelineBuilder : IPipelineBuilder<Car, CarFilterModel> {

        private readonly IServiceProvider _serviceProvider;

        private IDataHandler<Car, CarFilterModel> _firstDataHandler;

        private IDataHandler<Car, CarFilterModel> _currentDataHandler;

        public CarSelectionPipelineBuilder(IServiceProvider serviceProvider) {
            _serviceProvider = serviceProvider;
        }

        public IPipelineBuilder<Car, CarFilterModel> SetFirstChainPart<T>() where T : IDataHandler<Car, CarFilterModel> {

            var dataHandler = _serviceProvider.GetRequiredService<T>();
            _firstDataHandler = _currentDataHandler = dataHandler;
            return this;
        }

        public IPipelineBuilder<Car, CarFilterModel> SetNextChainPart<T>() where T : IDataHandler<Car, CarFilterModel> {

            var dataHandler = _serviceProvider.GetRequiredService<T>();
            _currentDataHandler = _currentDataHandler.SetNext(dataHandler);
            return this;
        }

        public IDataHandler<Car, CarFilterModel> GetPipeline() {
            return _firstDataHandler;
        }
    }
}