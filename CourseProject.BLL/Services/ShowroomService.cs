using AutoMapper;
using CourseProject.BLL.DTO;
using CourseProject.BLL.Interfaces;
using CourseProject.BLL.Validation;
using CourseProject.DAL.Entities;
using CourseProject.DAL.Interfaces;

namespace CourseProject.BLL.Services;

public class ShowroomService : IShowroomService {

    private readonly IUnitOfWork _unitOfWork;

    private readonly IMapper _mapper;

    public ShowroomService(IUnitOfWork unitOfWork, IMapper mapper) {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
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
            .FirstOrDefaultAsync(s => s.Id == id);

        operationResult.Result = _mapper.Map<Showroom, ShowroomDto>(model);

        return operationResult;
    }
}