namespace ECC_APP_2.Models
{
    public class BussinessProposalTemplate
    {
        public BussinessProposalTemplate() { }
        public BussinessProposalTemplate(string executiveSummary, string introduction, string proposedSolution, string problemSolution, string budgetandPenalties, string benefits, string termsandConditions, string appendix)
        {
            ExecutiveSummary = executiveSummary;
            Introduction = introduction;
            ProposedSolution = proposedSolution;
            this.problemSolution = problemSolution;
            BudgetandPenalties = budgetandPenalties;
            Benefits = benefits;
            TermsandConditions = termsandConditions;
            Appendix = appendix;
        }

        public String ExecutiveSummary { get; set; }
        public String Introduction { get; set; }
        public String ProposedSolution { get; set; }
        public String problemSolution { get; set; }
        public String BudgetandPenalties { get; set; }
        public String Benefits { get; set; }
        public String TermsandConditions { get; set; }
        public String Appendix { get; set; }
    }
}
