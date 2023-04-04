namespace RecipeSharingApi.DataLayer.Models.DTOs.Instruction
{
    public class RecipeInstructionUpdateDTO
    {
        public Guid Id { get; set; }
        public int StepNumber { get; set; }
        public string StepDescription { get; set; }
    }
}
