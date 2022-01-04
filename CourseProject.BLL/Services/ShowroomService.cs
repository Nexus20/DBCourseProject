using AutoMapper;
using CourseProject.BLL.DTO;
using CourseProject.BLL.FilterModels;
using CourseProject.BLL.Interfaces;
using CourseProject.BLL.Pipeline;
using CourseProject.BLL.PipelineBuilders;
using CourseProject.BLL.Validation;
using CourseProject.DAL.Entities;
using CourseProject.DAL.Interfaces;

namespace CourseProject.BLL.Services;

public class ShowroomService : IShowroomService {

    private readonly IUnitOfWork _unitOfWork;

    private readonly IMapper _mapper;

    private readonly IPipelineBuilderDirector<Showroom, ShowroomFilterModel> _builderDirector;

    public ShowroomService(IUnitOfWork unitOfWork, IMapper mapper, IPipelineBuilderDirector<Showroom, ShowroomFilterModel> builderDirector) {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _builderDirector = builderDirector;
    }

    public async Task<OperationResult> CreateShowroomAsync(ShowroomDto dto) {

        var operationResult = new OperationResult();

        var entity = _mapper.Map<ShowroomDto, Showroom>(dto);

        await _unitOfWork.GetRepository<IRepository<Showroom>, Showroom>().CreateAsync(entity);

        await _unitOfWork.SaveChangesAsync();

        return operationResult;
    }

    public async Task<OperationResult> EditShowroomAsync(ShowroomDto dto) {

        var operationResult = new OperationResult();

        var entity = _mapper.Map<ShowroomDto, Showroom>(dto);

        _unitOfWork.GetRepository<IRepository<Showroom>, Showroom>().Update(entity);

        await _unitOfWork.SaveChangesAsync();

        return operationResult;
    }

    public async Task<OperationResult> DeleteShowroomAsync(int id) {

        var operationResult = new OperationResult();

        _unitOfWork.GetRepository<IRepository<Showroom>, Showroom>().Delete(m => m.Id == id);
        await _unitOfWork.SaveChangesAsync();

        return operationResult;
    }

    public IEnumerable<ShowroomDto> GetAllShowrooms() {
        //var source = modelFilterModel == null
        //    ? _unitOfWork.GetRepository<IRepository<Model>, Model>()
        //        .Find(null, null, null, null, m => m.Brand, m => m.Cars)
        //    : _unitOfWork.GetRepository<IRepository<Model>, Model>()
        //        .Find(modelFilterModel.FilterExpression, null, null, null, m => m.Brand, m => m.Cars);

        var source = _unitOfWork.GetRepository<IRepository<Showroom>, Showroom>()
            .Find(null, null, null, null, s => s.Managers);

        return _mapper.Map<IEnumerable<Showroom>, IEnumerable<ShowroomDto>>(source);
    }

    public async Task<OperationResult<ShowroomDto>> GetShowroomByIdAsync(int id) {

        var operationResult = new OperationResult<ShowroomDto>();

        var model = await _unitOfWork.GetRepository<IRepository<Showroom>, Showroom>()
            .FirstOrDefaultWithDetailsAsync(s => s.Id == id);

        if (model == null) {
            operationResult.AddError(nameof(id), "Such showroom not found");
            return operationResult;
        }

        operationResult.Result = _mapper.Map<Showroom, ShowroomDto>(model);

        return operationResult;
    }

    public async Task<DtoListWithPossibleEntitiesCount<ShowroomDto>> GetAllShowroomsAsync(ShowroomFilterModel filterModel) {

        var pipeline = new SelectionPipeline<Showroom, ShowroomFilterModel>(filterModel, _builderDirector);

        var expressions = pipeline.Process();

        var source = _unitOfWork.GetRepository<IRepository<Showroom>, Showroom>().FindAllWithDetails(expressions);

        var possibleCarsCount = await _unitOfWork.GetRepository<IRepository<Showroom>, Showroom>()
            .CountAsync(expressions.FilterExpressions);

        //logger.LogInformation($"All games were returned. Returned games count: {source.Count()}");

        return new DtoListWithPossibleEntitiesCount<ShowroomDto>() {
            Dtos = _mapper.Map<IEnumerable<Showroom>, IEnumerable<ShowroomDto>>(source),
            PossibleDtosCount = possibleCarsCount
        };
    }
}