namespace CourseProject.BLL.DTO {
    public class DtoListWithPossibleEntitiesCount<TDto> {

        public IEnumerable<TDto> Dtos { get; set; }

        public int PossibleDtosCount { get; set; }
    }
}