namespace ECC_APP_2.Models
{
    public class BussinessProposalTemplate
    {
        public BussinessProposalTemplate() { }

        public BussinessProposalTemplate(int id, string executiveSummary, string introduction, string proposedSolution, string problemSolution, string budgetandPenalties, string benefits, string termsandConditions, string appendix)
        {
            Id = id;
            ExecutiveSummary = executiveSummary;
            Introduction = introduction;
            ProposedSolution = proposedSolution;
            this.problemSolution = problemSolution;
            BudgetandPenalties = budgetandPenalties;
            Benefits = benefits;
            TermsandConditions = termsandConditions;
            Appendix = appendix;
        }

        public int Id { get; set; }  // Add the Id property
        public string ExecutiveSummary { get; set; }
        public string Introduction { get; set; }
        public string ProposedSolution { get; set; }
        public string problemSolution { get; set; }
        public string BudgetandPenalties { get; set; }
        public string Benefits { get; set; }
        public string TermsandConditions { get; set; }
        public string Appendix { get; set; }
    }
}
